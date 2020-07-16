using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using project_manage_api.Infrastructure;
using project_manage_api.Model;
using project_manage_api.Model.QueryModel;
using project_manage_api.Model.RequestModel;
using project_manage_api.Service;

namespace project_manage_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        public ProjectService _service;
        
        public ProjectController(ProjectService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public PageResponse<List<Project>> findProjects(QueryProjectRequest request)
        {
            var result = new PageResponse<List<Project>>();

            try
            {
                result = _service.findProjects(request);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
        
        /// <summary>
        /// 获取选中项目成员
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<List<ProjectMember>> findProjectMember(int projectId)
        {
            var result = new Response<List<ProjectMember>>();

            try
            {
                result.Result = _service.findProjectMember(projectId);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 创建或更新项目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<int> addOrUpdateProject(Project project)
        {
            var result = new Response<int>();

            try
            {
                result.Result = _service.addOrUpdateProject(project);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
        
        /// <summary>
        /// 创建或修改项目成员
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public Response addOrUpdateProjectMember(string projectMember)
        {
            var result = new Response();

            try
            {
                _service.addOrUpdateProjectMember(projectMember);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}