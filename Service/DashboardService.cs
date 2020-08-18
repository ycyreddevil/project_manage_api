using System;
using System.Collections.Generic;
using System.Linq;
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
            }).Where((tr, aa) => tr.Status == 0 && aa.ApproverId == user.UserId && aa.Level > 0).Count();
            
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
            var toBeReplied = Db.Queryable<Comment>().Where(c => c.TargetId == user.UserId
                         && SqlFunc.Subqueryable<Comment>().Where(u => u.ParentId == c.Id).Count() == 0).Count();

            return new Dictionary<string, object>
            {
                {"toBePendingNum", toBePendingNum},
                {"myProjectNum", myProjectNum},
                {"myTaskNum", myTaskNum},
                {"toBeReplied", toBeReplied},
            };
        }

        /// <summary>
        /// 获取项目分析表数据
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, object>> getProjectAnalyse()
        {
            var roleId = Db.Queryable<UserRole>().Where(u => u.UserId == user.UserId).Select(u => u.RoleId).First();
            var projectList = roleId == 1 ? Db.Queryable<Project>().ToList() : Db.Queryable<Project>()
                .Where(u => u.ChargeUserId == user.UserId || u.SubmitterId == user.UserId).ToList();

            var result = new List<Dictionary<string, object>>();
            
            foreach (var project in projectList)
            {
                // 获取项目完成率 通过根节点任务占比乘以任务完成率的和 得出项目完成率
                var rootTask = Db.Queryable<Task>().Where(u => u.ProjectId == project.Id && u.ParentId == 0).ToList();
                var complete_ratio = rootTask.Sum(item => item.Progress * item.Weight * 0.01);
                
                // 获取项目耗时 从项目开始时间到现在
                var day = (DateTime.Now - project.StartTime).Value.Days;
                
                // 获取项目的总任务数
                var task_num = Db.Queryable<Task>().Where(u => u.ProjectId == project.Id).Count();
                
                var dict = new Dictionary<string, object>
                {
                    {"name", project.Name},
                    {"code", project.Code},
                    {"complete_ratio", complete_ratio},
                    {"day", day},
                    {"task_num", task_num}
                };

                result.Add(dict);
            }
            
            return result;
        }

        /// <summary>
        /// 首页工作台 待办事项修改
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public Schedule addOrUpdateSchedule(Schedule schedule)
        {
            return Db.Saveable(schedule).ExecuteReturnEntity();
        }
        
        /// <summary>
        /// 首页工作台 待办事项修改
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public string getSchedule()
        {
            var json = Db.Queryable<Schedule>().Where(u => u.SubmitterUserId == user.UserId).Select(u => new
            {
                id = u.Id, text = u.Text, done = u.Done == 1, submitterUserId = u.SubmitterUserId
            }).ToJson();

            return json;
        }

        /// <summary>
        /// 首页工作台 删除事项
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public void deleteSchedule(int id)
        {
            Db.Deleteable<Schedule>().Where(new Schedule() { Id = id }).ExecuteCommand();
        }

        public string getSubmissionStatus()
        {
            var sugarQueryableList = Db.Queryable<Project>();
            
            // 如果是管理员 才查看所有项目 否则查询自己负责或者自己提交的项目
            var roleId = Db.Queryable<UserRole>().Where(u => u.UserId == user.UserId).Select(u => u.RoleId).First();

            if (roleId != 1)
                sugarQueryableList = sugarQueryableList.Where(u => u.ChargeUserId == user.UserId || u.SubmitterId == user.UserId);
            
            return null;
        }
    }
}