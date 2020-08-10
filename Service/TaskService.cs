﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using project_manage_api.Infrastructure;
using project_manage_api.Model;
using project_manage_api.Model.QueryModel;
using project_manage_api.Model.RequestModel;
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

            var project = Db.Queryable<Project>().Where(u => u.Id == task.ProjectId).First();
            var list = Db.Queryable<Task>().Where(u => u.CascadeId.Contains(task.CascadeId)).ToList();
            treeList.AddRange(list);

            var taskTree = treeList.GenerateVueTaskTree(u => u.Id, u => u.ParentId);

            // 处理返回tree 添加根节点 根节点名称为选中任务本身
            var result = new Dictionary<string, object>
            {
                {"id", project.Id}, {"label", project.Name}, {"children", taskTree}, {"chargeUserName", project.ChargeUserName},{"status", 0}
            };

            return result;
        }

        /// <summary>
        /// 通过id获取task
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public Task getTaskById(int taskId)
        {
            return SimpleDb.GetSingle(u => u.Id == taskId);
        }
        
        /// <summary>
        /// 获取项目动态 提交情况
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Dictionary<string, object> getTaskRecordByTaskId(QueryTaskRecordRequest request)
        {
            var dateQuery = Db.Queryable<TaskRecord, Task>((tr, t) => new object[]
            {
                JoinType.Left, tr.TaskId == t.Id
            }).Where((tr, t) => t.Id == request.projectOrTaskId && tr.Status == 1);

            if (!string.IsNullOrEmpty(request.startTime) && !string.IsNullOrEmpty(request.endTime))
                dateQuery = dateQuery.Where((tr, t) =>
                    SqlFunc.Between(tr.CreateTime, request.startTime, request.endTime));

            var dateList = dateQuery.GroupBy((tr, t) => SqlFunc.DateValue(tr.CreateTime, DateType.Year) + "-" +
                                                           SqlFunc.DateValue(tr.CreateTime, DateType.Month) + "-" +
                                                           SqlFunc.DateValue(tr.CreateTime, DateType.Day))
                .OrderBy((tr, t) => tr.CreateTime)
                .Select((tr, t) => tr.CreateTime).ToList();

            var dict = new Dictionary<string, object>();
            foreach (var date in dateList)
            {
                var taskRecordList = Db.Queryable<TaskRecord>().Where(u => SqlFunc.DateIsSame(date, u.CreateTime))
                    .ToList();
                dict.Add(date.ToString("yyyy-MM-dd"), taskRecordList);
            }

            return dict;
        }

        /// <summary>
        /// 通过id获取任务评论
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<CommentResponse> getTaskCommentById(int taskId)
        {
            var commentList = Db.Queryable<Comment>().Where(u => u.Type == 1 && u.DocId == taskId)
                .OrderBy(u => u.CreateTime, OrderByType.Desc).ToList();

            var commentResponseList = commentList.GenerateVueCommentTree(u => u.Id, u => u.ParentId, Db).ToList();

            return commentResponseList;
        }
        
        /// <summary>
        /// 新增项目评论
        /// </summary>
        /// <param name="content"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public CommentResponse addTaskComment(AddTaskCommentRequest request)
        {
            var comment = new Comment
            {
                Content = request.content,
                Type = 1,
                SubmitterId = user.UserId,
                CreateTime = DateTime.Now,
                DocId = request.taskId,
                TargetId = request.targetUserId
            };
            if (request.parentId > 0)
                comment.ParentId = request.parentId;
            var returnId = Db.Insertable(comment).ExecuteCommand();

            var commentUser = Db.Queryable<Users>().Where(u => u.userId == user.UserId).Select(u =>
                new CommentUserResponse {id = u.userId, nickName = u.userName, avatar = u.avatar}).First();

            var targetUser = Db.Queryable<Users>().Where(u => u.userId == request.targetUserId).Select(u =>
                new CommentUserResponse {id = u.userId, nickName = u.userName, avatar = u.avatar}).First();

            var commentResponse = new CommentResponse
            {
                id = returnId,
                childrenList = null,
                commentUser = commentUser,
                targetUser = targetUser,
                content = request.content,
                createDate = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss")
            };

            return commentResponse;
        }
        
        /// <summary>
        /// 获取最新的5条任务记录
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<TaskRecord> getLatestTaskRecordByTaskId(int taskId)
        {
            var list = Db.Queryable<TaskRecord, Task>((tr, t) => new object[]
                {
                    JoinType.Left, tr.TaskId == t.Id
                }).Where((tr, t) => tr.TaskId == taskId && tr.Status == 1).
                OrderBy((tr, t) => tr.CreateTime, OrderByType.Desc).Select<TaskRecord>().Take(6).ToList();

            return list;
        }
        
        /// <summary>
        /// 创建或更新任务
        /// </summary>
        /// <param name="request"></param>
        public Task addOrUpdateTask(Task task)
        {
            task.CreateTime = DateTime.Now;
            task.SubmitterId = user.UserId;
            task.SubmitterName = user.UserName;
            ChangeModuleCascade(task);

            return Db.Saveable(task).ExecuteReturnEntity();
        }
        
        /// <summary>
        /// 如果一个类有层级结构（树状），则修改该节点时，要修改该节点的所有子节点
        /// //修改对象的级联ID，生成类似XXX.XXX.X.XX
        /// </summary>
        /// <param name="task"></param>
        private void ChangeModuleCascade(Task task)
        {
            string cascadeId;
            var currentCascadeId = 1; //当前结点的级联节点最后一位
            var sameLevels = SimpleDb.AsQueryable().Where(u => u.ParentId == task.ParentId && u.Id != task.Id).ToList();
            foreach (var obj in sameLevels)
            {
                var objCascadeId = int.Parse(obj.CascadeId.TrimEnd('.').Split('.').Last());
                if (currentCascadeId <= objCascadeId) currentCascadeId = objCascadeId + 1;
            }

            if (task.ParentId > 0)
            {
                var parenntTask = SimpleDb.GetSingle(u => u.Id == task.ParentId); 
                if (parenntTask != null)
                {
                    cascadeId = parenntTask.CascadeId + currentCascadeId + ".";
                }
                else
                {
                    throw new Exception("未能找到该组织的父节点信息");
                }
            }
            else
            {
                cascadeId = ".0." + currentCascadeId + ".";
            }

            task.CascadeId = cascadeId;
        }
    }
}