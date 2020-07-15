using System;
using System.Collections.Generic;
using System.Linq;
using project_manage_api.Infrastructure;
using project_manage_api.Model;
using project_manage_api.Model.QueryModel;
using SqlSugar;

namespace project_manage_api.Service
{
    public class ProjectService : SugarDBContext<Project>
    {
        public ProjectService()
        {
        }

        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<Project> findProjects(QueryProjectRequest request)
        {
            var sugarQueryableList = SimpleDb.AsQueryable().Where(u =>
                StringTools.IsEqualEngAndChinese(u.Name, request.key) && u.StartTime >= request.startTime && u.EndTime <= request.endTime);

            if (!string.IsNullOrEmpty(request.type))
                sugarQueryableList = sugarQueryableList.Where(u => u.Type == request.type);

            var list = sugarQueryableList.OrderBy(u => request.sortColumn,
                request.sortType == "asc" ? OrderByType.Asc : OrderByType.Desc).ToList();

            return list;
        }
    }
}