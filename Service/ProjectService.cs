using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using NPOI.SS.Formula.Functions;
using project_manage_api.Infrastructure;
using project_manage_api.Model;
using project_manage_api.Model.QueryModel;
using project_manage_api.Model.RequestModel;
using SqlSugar;

namespace project_manage_api.Service
{
    public class ProjectService : SugarDBContext<Project>
    {
        private IHttpContextAccessor _httpContextAccessor;
        private ICacheContext _cacheContext;
        private UserAuthSession user;

        public ProjectService(IHttpContextAccessor httpContextAccessor, ICacheContext cacheContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _cacheContext = cacheContext;

            string token = _httpContextAccessor.HttpContext.Request.Headers[Define.TOKEN_NAME];
            user = _cacheContext.Get<UserAuthSession>(token);
        }

        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PageResponse<List<Project>> findProjects(QueryProjectRequest request)
        {
            var total = 0;

            var sugarQueryableList = SimpleDb.AsQueryable().Where(u =>
                u.Name.Contains(request.key) && SqlFunc.Between(u.CreateTime, request.startTime, request.endTime));

            if (!string.IsNullOrEmpty(request.type))
                sugarQueryableList = sugarQueryableList.Where(u => u.Type == request.type);

            var list = sugarQueryableList.OrderBy(u => request.sortColumn,
                    request.sortType == "asc" ? OrderByType.Asc : OrderByType.Desc)
                .ToPageList(request.page, request.limit, ref total);

            var pageResponse = new PageResponse<List<Project>> {Total = total, Result = list};

            return pageResponse;
        }

        /// <summary>
        /// 获取选中项目的项目团队
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public string findProjectMember(int projectId)
        {
            var list = Db
                .Queryable<ProjectMember, Users>(
                    (p, u) => new object[] {JoinType.Left, p.UserId == u.userId})
                .Where((p, u) => p.ProjectId == projectId && p.Status == 1)
                .Select((p, u) => new
                {
                    id = p.Id, projectId = p.ProjectId, userId = p.UserId, userName = p.UserName,
                    modifyTime = p.ModifyTime,
                    projectRole = p.ProjectRole, status = p.Status, mobile = u.mobilePhone, avatar = u.avatar
                }).ToJson();

            return list;
        }

        /// <summary>
        /// 创建或更新项目
        /// </summary>
        /// <param name="request"></param>
        public int addOrUpdateProject(Project project)
        {
            project.CreateTime = DateTime.Now;

            project.SubmitterId = user.UserId;
            project.SubmitterName = user.UserName;
            project.Level = 2;
            project.Status = "发布";

            return Db.Saveable(project).ExecuteCommand();
        }

        /// <summary>
        /// 创建或修改项目团队成员
        /// </summary>
        /// <param name="request"></param>
        public ProjectMember addOrUpdateProjectMember(ProjectMember projectMember)
        {
            projectMember.ModifyTime = DateTime.Now;

            if (projectMember.Id <= 0)
            {
                //如果是新增 则把手机号查询出来回显
                var mobilePhone = Db.Queryable<Users>().Where(u => u.userId == projectMember.UserId)
                    .Select(u => u.mobilePhone);
            }

            var result = Db.Saveable(projectMember).ExecuteReturnEntity();

            return result;
        }

        /// <summary>
        /// 通过id获取project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Project getProjectById(int id)
        {
            var project = SimpleDb.GetSingle(u => u.Id == id);
            return project;
        }

        /// <summary>
        /// 通过项目成员id来获取项目成员
        /// </summary>
        /// <param name="projectMemberId"></param>
        /// <returns></returns>
        public ProjectMember getProjectMemberById(int projectMemberId)
        {
            return Db.Queryable<ProjectMember>().Where(u => u.Id == projectMemberId).ToList()[0];
        }

        /// <summary>
        /// 前端展示项目任务树
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Dictionary<string, object> getProjectTaskTree(int projectId)
        {
            var member = Db.Queryable<ProjectMember>().Where(u => u.ProjectId == projectId && u.UserId == user.UserId)
                .ToList()[0];

            var treeList = new List<Task>();

            // 如果是项目经理或是管理员 才能查询到项目中所有子任务 否则查询到自己负责任务的所有下级任务以及和自己负责任务相关联的上级任务
            var allTaskList = Db.Queryable<Task>().Where(u => u.ProjectId == projectId);

            if (!member.ProjectRole.Equals("项目经理"))
            {
                var inferiorTaskList = allTaskList.Where(u => u.ChargeUserId == user.UserId).ToList();
                treeList.AddRange(inferiorTaskList);

                foreach (var inferiorTask in inferiorTaskList)
                {
                    var list = Db.Queryable<Task>().Where(u => !u.CascadeId.Equals(inferiorTask.CascadeId) && (
                            u.CascadeId.Contains(inferiorTask.CascadeId) ||
                            inferiorTask.CascadeId.Contains(u.CascadeId)))
                        .ToList();

                    treeList.AddRange(list);
                }
            }
            else
            {
                treeList = allTaskList.ToList();
            }

            var taskTree = treeList.GenerateVueTaskTree(u => u.Id, u => u.ParentId);

            // 处理返回tree 添加根节点 根节点名称为项目名称
            var project = SimpleDb.GetSingle(u => u.Id == projectId);
            var result = new Dictionary<string, object>
            {
                {"id", 0}, {"label", project.Name}, {"children", taskTree}, {"chargeUserName", project.ChargeUserName},
                {"status", "已审批"}
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
            return Db.Queryable<Task>().Where(u => u.Id == taskId).ToList()[0];
        }

        /// <summary>
        /// 获取项目动态 提交情况
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Dictionary<string, object> getTaskRecordByProjectId(QueryTaskRecordRequest request)
        {
            var dateQuery = Db.Queryable<TaskRecord, Task, Project>((tr, t, p) => new object[]
            {
                JoinType.Left, tr.TaskId == t.Id, JoinType.Left, t.ProjectId == p.Id
            }).Where((tr, t, p) => t.ProjectId == request.projectId && tr.Status == 1);

            if (!string.IsNullOrEmpty(request.startTime) && !string.IsNullOrEmpty(request.endTime))
                dateQuery = dateQuery.Where((tr, t, p) =>
                    SqlFunc.Between(tr.CreateTime, request.startTime, request.endTime));

            var dateList = dateQuery.GroupBy((tr, t, p) => SqlFunc.DateValue(tr.CreateTime, DateType.Year) + "-" +
                                                           SqlFunc.DateValue(tr.CreateTime, DateType.Month) + "-" +
                                                           SqlFunc.DateValue(tr.CreateTime, DateType.Day))
                .OrderBy((tr, t, p) => tr.CreateTime)
                .Select((tr, t, p) => tr.CreateTime).ToList();

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
        public List<CommentResponse> getProjectCommentById(int projectId)
        {
            var commentList = Db.Queryable<Comment>().Where(u => u.Type == 0 && u.DocId == projectId).ToList();

            var commentResponseList = commentList.GenerateVueCommentTree(u => u.Id, u => u.ParentId, Db).ToList();

            return commentResponseList;
        }

        /// <summary>
        /// 新增项目评论
        /// </summary>
        /// <param name="content"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public int addProjectComment(AddProjectCommentRequest request)
        {
            var comment = new Comment
            {
                Content = request.content,
                Type = 0,
                SubmitterId = user.UserId,
                CreateTime = DateTime.Now,
                DocId = request.projectId,
                TargetId = request.targetUserId
            };
            if (request.parentId > 0)
                comment.ParentId = request.parentId;
            return Db.Insertable(comment).ExecuteCommand();
        }
    }
}