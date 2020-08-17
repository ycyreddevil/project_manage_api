using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using project_manage_api.Infrastructure;
using project_manage_api.Model;
using project_manage_api.Model.RequestModel;
using SqlSugar;

namespace project_manage_api.Service
{
    public class MessageService : SugarDBContext<Comment>
    {
        private IHttpContextAccessor _httpContextAccessor;
        private ICacheContext _cacheContext;
        private UserAuthSession user;
        
        public MessageService(IHttpContextAccessor httpContextAccessor, ICacheContext cacheContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _cacheContext = cacheContext;

            string token = _httpContextAccessor.HttpContext.Request.Headers[Define.TOKEN_NAME];
            user = _cacheContext.Get<UserAuthSession>(token);
        }

        /// <summary>
        /// 获取待回复消息列表 查询待回复消息 需要判断是否回复过消息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PageResponse<string> getToBeReplied(QueryMessageRequest request)
        {
            var query = Db.Queryable<Comment, Users, Users>((c, u1, u2) => new object[]
            {
                JoinType.Left, c.SubmitterId == u1.userId, JoinType.Left, c.TargetId == u2.userId
            }).Where((c, u1, u2) => c.Content.Contains(request.key) && c.Type == request.type && c.TargetId == user.UserId
             && SqlFunc.Subqueryable<Comment>().Where(u => u.ParentId == c.Id).Count() == 0);

            query.ToSql();
            
            if (!string.IsNullOrEmpty(request.startTime) && !string.IsNullOrEmpty(request.endTime))
                query = query.Where((c, u1, u2) => SqlFunc.Between(c.CreateTime, request.startTime, request.endTime));

            var total = 0;
            var json = query.Select((c, u1, u2) => new
            {
                id = c.Id, submitterName = u1.userName, content = c.Content, type = c.Type, createTime = c.CreateTime,
                attachment = c.Attachment, targetName = u2.userName, docId = c.DocId, haveRead = c.HaveRead
            }).ToPageList(request.page, request.limit, ref total).ToJson();
            
            var pageResponse = new PageResponse<string>{Total = total, Result = json};
            return pageResponse;
        }

        /// <summary>
        /// 获取已回复消息 查询已回复消息 需要判断是否已回复过消息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PageResponse<string> getHaveRelied(QueryMessageRequest request)
        {
            var query = Db.Queryable<Comment, Users, Users>((c, u1, u2) => new object[]
            {
                JoinType.Left, c.SubmitterId == u1.userId, JoinType.Left, c.TargetId == u2.userId
            }).Where((c, u1, u2) => c.Content.Contains(request.key) && c.Type == request.type && c.TargetId == user.UserId
             && c.SubmitterId == user.UserId);
            
            if (!string.IsNullOrEmpty(request.startTime) && !string.IsNullOrEmpty(request.endTime))
                query = query.Where((c, u1, u2) => SqlFunc.Between(c.CreateTime, request.startTime, request.endTime));

            var total = 0;
            var json = query.Select((c, u1, u2) => new
            {
                id = c.Id, submitterName = u1.userName, content = c.Content, type = c.Type, createTime = c.CreateTime,
                attachment = c.Attachment, targetName = u2.userName, docId = c.DocId, haveRead = c.HaveRead
            }).ToPageList(request.page, request.limit, ref total).ToJson();
            
            var pageResponse = new PageResponse<string>{Total = total, Result = json};
            return pageResponse;
        }

        /// <summary>
        /// 快速回复消息
        /// </summary>
        public void quickComment(int parentId, string content, int docId, int targetId)
        {
            var comment = new Comment
            {
                ParentId = parentId,
                Attachment = string.Empty,
                Content = content,
                CreateTime = DateTime.Now,
                DocId = docId,
                HaveRead = 0,
                SubmitterId = user.UserId,
                TargetId = targetId,
                Type = 0
            };

            SimpleDb.Insert(comment);
        }
    }
}