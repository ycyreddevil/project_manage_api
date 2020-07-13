using System.Collections.Generic;
using System.Linq;
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
            var list = Db.Queryable<Project>().Where(u => u.Name.Contains(request.Key)).
                OrderBy(u => request.SortColumn, OrderByType.Desc).ToList();
            return null;
        }
    }
}