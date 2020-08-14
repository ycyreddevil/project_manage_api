using System;
using Microsoft.AspNetCore.Http;
using project_manage_api.Infrastructure;
using project_manage_api.Model;
using project_manage_api.Model.RequestModel;
using SqlSugar;

namespace project_manage_api.Service
{
    public class PendingService : SugarDBContext<TaskRecord>
    {
        private IHttpContextAccessor _httpContextAccessor;
        private ICacheContext _cacheContext;
        private UserAuthSession user;
        
        public PendingService(IHttpContextAccessor httpContextAccessor, ICacheContext cacheContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _cacheContext = cacheContext;

            string token = _httpContextAccessor.HttpContext.Request.Headers[Define.TOKEN_NAME];
            user = _cacheContext.Get<UserAuthSession>(token);
        }

        /// <summary>
        /// 获取待我审批
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PageResponse<string> getToBeApprovedList(QueryPendingRequest request)
        {
            var query = Db.Queryable<TaskRecord, ApproveApprover>((tr, aa) => new object[]
            {
                JoinType.Left, tr.Id == aa.DocId
            }).Where((tr, aa) => tr.Desc.Contains(request.key) && tr.Status == 0 && aa.ApproverId == user.UserId && aa.Level > 0);
            
            if (!string.IsNullOrEmpty(request.startTime) && !string.IsNullOrEmpty(request.endTime))
                query = query.Where((tr, aa) => SqlFunc.Between(tr.CreateTime, request.startTime, request.endTime));

            var total = 0;
            var json = query.Select((tr, aa) => new
            {
                id = tr.Id, submitterName = tr.SubmitterName, desc = tr.Desc, status = tr.Status, createTime = tr.CreateTime,
                attachment = tr.Attachment, percent = tr.Percent, taskId = tr.TaskId
            }).ToPageList(request.page, request.limit, ref total).ToJson();
            
            // 此处返回json的原因是 目前审批类别只有任务完成情况 以后可能会新增审批类别 所以统一转成json返回
            var pageResponse = new PageResponse<string>{Total = total, Result = json};
            return pageResponse;
        }
        
        /// <summary>
        /// 获取我已审批
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PageResponse<string> getHaveApprovedList(QueryPendingRequest request)
        {
            var query = Db.Queryable<TaskRecord, ApproveRecord>((tr, ar) => new object[]
            {
                JoinType.Left, tr.Id == ar.DocId
            }).Where((tr, ar) => tr.Desc.Contains(request.key) && ar.ApproverId == user.UserId && !ar.Result.Equals("已提交"));
            
            if (!string.IsNullOrEmpty(request.startTime) && !string.IsNullOrEmpty(request.endTime))
                query = query.Where((tr, ar) => SqlFunc.Between(tr.CreateTime, request.startTime, request.endTime));

            var total = 0;
            var json = query.Select((tr, ar) => new
            {
                id = tr.Id, submitterName = tr.SubmitterName, desc = tr.Desc, status = tr.Status, createTime = tr.CreateTime,
                attachment = tr.Attachment, percent = tr.Percent, taskId = tr.TaskId
            }).ToPageList(request.page, request.limit, ref total).ToJson();
            
            // 此处返回json的原因是 目前审批类别只有任务完成情况 以后可能会新增审批类别 所以统一转成json返回
            var pageResponse = new PageResponse<string>{Total = total, Result = json};
            return pageResponse;
        }
        
        /// <summary>
        /// 获取我已提交
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PageResponse<string> getMySubmitList(QueryPendingRequest request)
        {
            var query = SimpleDb.AsQueryable().Where(u => u.SubmitterId == user.UserId && u.Desc.Contains(request.key));
            
            if (!string.IsNullOrEmpty(request.startTime) && !string.IsNullOrEmpty(request.endTime))
                query = query.Where(u => SqlFunc.Between(u.CreateTime, request.startTime, request.endTime));

            var total = 0;
            var json = query.Select(u => new
            {
                id = u.Id, submitterName = u.SubmitterName, desc = u.Desc, status = u.Status, createTime = u.CreateTime,
                attachment = u.Attachment, percent = u.Percent, taskId = u.TaskId
            }).ToPageList(request.page, request.limit, ref total).ToJson();

            // 此处返回json的原因是 目前审批类别只有任务完成情况 以后可能会新增审批类别 所以统一转成json返回
            var pageResponse = new PageResponse<string>{Total = total, Result = json};
            return pageResponse;
        }

        /// <summary>
        /// 审批通过
        /// </summary>
        /// <param name="taskRecordId"></param>
        public int agree(int taskRecordId)
        {
            // 新增approve record记录
            var approveRecord = new ApproveRecord
            {
                ApproverId = user.UserId,
                CreateTime = DateTime.Now,
                DocId = taskRecordId,
                Opinion = "审批同意",
                Result =  "审批同意",
                Type = 2
            };
            Db.Insertable(approveRecord).ExecuteReturnIdentity();
            
            // 修改task record状态
            var taskRecord = SimpleDb.GetSingle(u => u.Id == taskRecordId);
            taskRecord.Status = 1;
            SimpleDb.Update(taskRecord);
            
            // todo 发送相关消息

            return taskRecord.Id;
        }

        /// <summary>
        /// 审批不同意
        /// </summary>
        /// <param name="taskRecordId"></param>
        /// <param name="opinion"></param>
        public int disagree(int taskRecordId, string opinion)
        {
            // 新增approve record记录
            var approveRecord = new ApproveRecord
            {
                ApproverId = user.UserId,
                CreateTime = DateTime.Now,
                DocId = taskRecordId,
                Opinion = opinion,
                Result =  "审批拒绝",
                Type = 2
            };
            Db.Insertable(approveRecord).ExecuteReturnIdentity();
            
            // 修改task record状态
            var taskRecord = SimpleDb.GetSingle(u => u.Id == taskRecordId);
            taskRecord.Status = 2;
            SimpleDb.Update(taskRecord);
            
            // todo 发送相关消息

            return taskRecord.Id;
        }
    }
}