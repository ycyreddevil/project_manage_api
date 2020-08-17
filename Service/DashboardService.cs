using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project_manage_api.Infrastructure;
using project_manage_api.Model;
using SqlSugar;

namespace project_manage_api.Service
{
    /// <summary>
    /// 首页工作台service
    /// </summary>
    public class DashboardService : SugarDBContext<Comment>
    {
        private IHttpContextAccessor _httpContextAccessor;
        private ICacheContext _cacheContext;
        private UserAuthSession user;
        
        public DashboardService(IHttpContextAccessor httpContextAccessor, ICacheContext cacheContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _cacheContext = cacheContext;

            string token = _httpContextAccessor.HttpContext.Request.Headers[Define.TOKEN_NAME];
            user = _cacheContext.Get<UserAuthSession>(token);
        }

        /// <summary>
        /// 首页工作台 待办事项数量
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> getDashboardPendingNum()
        {
            // 获取待审批数量
            var toBePendingNum = Db.Queryable<TaskRecord, ApproveApprover>((tr, aa) => new object[]
            {
                JoinType.Left, tr.Id == aa.DocId
            }).Where((tr, aa) => tr.Status == 0 && aa.ApproverId == user.UserId && aa.Level > 0);
            
            // 获取我的项目数量 如果是管理员 才查看所有项目 否则查询自己负责或者自己提交的项目
            var roleId = Db.Queryable<UserRole>().Where(u => u.UserId == user.UserId).Select(u => u.RoleId).First();
            var myProjectNum = roleId == 1 ? Db.Queryable<Project>().Count() : Db.Queryable<Project>()
                .Where(u => u.ChargeUserId == user.UserId || u.SubmitterId == user.UserId).Count();
            
            // 获取我的任务数量 获取我的项目数量 如果是管理员 才查看所有项目 否则查询自己负责或者自己提交的项目
            var myTaskNum = roleId == 1 ? Db.Queryable<Task>().Count() : Db.Queryable<Task, Project>((t, p) => new object[]
            {
                JoinType.Left, t.ProjectId == p.Id
            }).Where((t, p) => t.ChargeUserId == user.UserId || t.SubmitterId == user.UserId || p.ChargeUserId == user.UserId).Count();
            
            // 获取我的消息数量
            var toBeReplied = 0;

            return new Dictionary<string, object>
            {
                {"toBePendingNum", toBePendingNum},
                {"myProjectNum", myProjectNum},
                {"myTaskNum", myTaskNum},
                {"toBeReplied", toBeReplied},
            };
        }
    }
}