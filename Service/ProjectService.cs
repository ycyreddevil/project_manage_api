﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
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
        
        public ProjectService(IHttpContextAccessor httpContextAccessor, ICacheContext cacheContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _cacheContext = cacheContext;
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
                request.sortType == "asc" ? OrderByType.Asc : OrderByType.Desc).ToPageList(request.page, request.limit, ref total);

            var pageResponse = new PageResponse<List<Project>> {Total = total, Result = list};

            return pageResponse;
        }

        /// <summary>
        /// 获取选中项目的项目团队
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<ProjectMember> findProjectMember(int projectId)
        {
            return Db.Queryable<ProjectMember>().Where(u => u.ProjectId == projectId && u.Status == 1).ToList();
        }

        /// <summary>
        /// 创建或更新项目
        /// </summary>
        /// <param name="request"></param>
        public int addOrUpdateProject(Project project)
        {
            project.CreateTime = DateTime.Now;
            
            string token = _httpContextAccessor.HttpContext.Request.Headers[Define.TOKEN_NAME];
            var result = _cacheContext.Get<UserAuthSession>(token);
            
            project.SubmitterId = result.UserId;
            project.SubmitterName = result.UserName;
            project.Level = 2;
            project.Status = "发布";

            return Db.Saveable(project).ExecuteCommand();
        }

        /// <summary>
        /// 创建或修改项目团队成员
        /// </summary>
        /// <param name="request"></param>
        public void addOrUpdateProjectMember(string projectMember)
        {
            var projectMemberList = projectMember.ToList<ProjectMember>();
            Db.Saveable(projectMemberList).ExecuteReturnList();
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
    }
}