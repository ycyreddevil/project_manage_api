using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using project_manage_api.Infrastructure;
using project_manage_api.Model;
using project_manage_api.Model.QueryModel;
using SqlSugar;

namespace project_manage_api.Service
{
    public class TaskService : SugarDBContext<Task>
    {
        private IHttpContextAccessor _httpContextAccessor;
        private ICacheContext _cacheContext;
        private UserAuthSession user;

        public TaskService(IHttpContextAccessor httpContextAccessor, ICacheContext cacheContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _cacheContext = cacheContext;

            string token = _httpContextAccessor.HttpContext.Request.Headers[Define.TOKEN_NAME];
            user = _cacheContext.Get<UserAuthSession>(token);
        }

        /// <summary>
        /// 获取我的任务列表
        /// </summary>
        /// <returns></returns>
        public string getTaskList(QueryProjectOrTaskRequest request)
        {
            var result = Db.Queryable<Task, Project>((t, p) => new object[]
                {
                    JoinType.Left, t.ProjectId == p.Id
                })
                .Where((t, p) => t.ChargeUserId == user.UserId && SqlFunc.Between(t.CreateTime, request.startTime, request.endTime))
                .Select((t, p) => new
                {
                    id = t.Id, projectId = t.ProjectId, taskName = t.TaskName, weight = t.Weight, progress = t.Progress,
                    status = t.Status, priority = t.Priority, startTime = t.StartTime, endTime = t.EndTime,
                    submitterName = t.SubmitterName, projectName = p.Name
                }).ToJson();
            return result;
        }
    }
}