using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public PageResponse<string> getTaskList(QueryProjectOrTaskRequest request)
        {
            var total = 0;
            var query = Db.Queryable<Task, Project>((t, p) => new object[]
                {
                    JoinType.Left, t.ProjectId == p.Id
                }).Where((t, p) =>
                    t.ChargeUserId == user.UserId && t.TaskName.Contains(request.key) &&
                    SqlFunc.Between(t.CreateTime, request.startTime, request.endTime));

            if (SqlFunc.HasValue(request.type))
                query = query.Where((t, p) => t.Status == int.Parse(request.type));
            
            // 如果是管理员 才查看所有项目 否则查询自己负责或者自己提交的项目
            var roleId = Db.Queryable<UserRole>().Where(u => u.UserId == user.UserId).Select(u => u.RoleId).First();

            if (roleId != 1)
                query = query.Where((t, p) => t.ChargeUserId == user.UserId || t.SubmitterId == user.UserId);

            var result = query.Select((t, p) => new
                {
                    id = t.Id, projectId = t.ProjectId, taskName = t.TaskName, weight = t.Weight, progress = t.Progress,
                    status = t.Status, priority = t.Priority, startTime = t.StartTime, endTime = t.EndTime,
                    submitterName = t.SubmitterName, projectName = p.Name
                });

            var json = result.ToPageList(request.page, request.limit, ref total).ToJson();

            var pageResponse = new PageResponse<string>{Total = total, Result = json};
            return pageResponse;
        }

        /// <summary>
        /// 开始任务操作按钮
        /// </summary>
        /// <param name="taskId"></param>
        public void beginTask(int taskId)
        {
            // 修改任务状态为 进行中
            var task = SimpleDb.AsQueryable().Where(u => u.Id == taskId).First();
            task.Status = 1;
            SimpleDb.Update(task);

            // 新增任务记录：开始任务
            var taskRecord = new TaskRecord
            {
                Status = 1,
                CreateTime = DateTime.Now,
                Desc = "任务开始",
                SubmitterId = user.UserId,
                SubmitterName = user.UserName,
                TaskId = taskId
            };
            Db.Insertable(taskRecord).ExecuteCommand();
        }

        /// <summary>
        /// 结束任务
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="approverId"></param>
        public void endTask(int taskId, int approverId)
        {
            // 新增任务记录： 结束任务 任务状态： 待审批
            var taskRecord = new TaskRecord{Status = 0,CreateTime = DateTime.Now,Desc = "任务完成",SubmitterId = user.UserId,SubmitterName = user.UserName,TaskId = taskId};
            var taskRecordId = Db.Insertable(taskRecord).ExecuteReturnIdentity();
            
            // 新增任务审批记录
            var approverList = new List<ApproveApprover>();
            var submitter = new ApproveApprover {DocId = taskRecordId, ApproverId = user.UserId, Type = 1, Level = 0};
            approverList.Add(submitter);
            
            var approver = new ApproveApprover {DocId = taskRecordId, ApproverId = approverId, Type = 1, Level = 1};
            approverList.Add(approver);
            Db.Insertable(approverList).ExecuteReturnIdentity();
            
            // 新增任务审批记录
            var record = new ApproveRecord{DocId = taskRecordId,ApproverId = user.UserId, Type = 1, Opinion = "",Result = "已提交"};
            Db.Insertable(record).ExecuteReturnIdentity();
        }
        
        /// <summary>
        /// 任务取消
        /// </summary>
        /// <param name="taskId"></param>
        public void cancelTask(int taskId)
        {
            // 修改任务状态为 已取消
            var task = SimpleDb.AsQueryable().Where(u => u.Id == taskId).First();
            task.Status = 4;
            SimpleDb.Update(task);

            // 新增任务记录：开始任务
            var taskRecord = new TaskRecord
            {
                Status = 1,
                CreateTime = DateTime.Now,
                Desc = "任务取消",
                SubmitterId = user.UserId,
                SubmitterName = user.UserName,
                TaskId = taskId
            };
            Db.Insertable(taskRecord).ExecuteCommand();
        }
        
        /// <summary>
        /// 前端展示子任务任务树
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Dictionary<string, object> getChilrenTaskTree(int taskId)
        {
            var treeList = new List<Task>();

            // 查询到选中任务的子任务
            var task = Db.Queryable<Task>().Where(u => u.Id == taskId).First();

            var project = Db.Queryable<Project>().Where(u => u.Id == task.Id).First();
            var list = Db.Queryable<Task>().Where(u => u.CascadeId.Contains(task.CascadeId)).ToList();
            treeList.AddRange(list);

            var taskTree = treeList.GenerateVueTaskTree(u => u.Id, u => u.ParentId);

            // 处理返回tree 添加根节点 根节点名称为选中任务本身
            var result = new Dictionary<string, object>
            {
                {"id", project.Id}, {"label", project.Name}, {"children", taskTree}, {"chargeUserName", project.ChargeUserName},{"status", "已审批"}
            };

            return result;
        }
    }
}