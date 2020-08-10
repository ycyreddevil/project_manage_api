using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
        public PageResponse<List<Project>> findProjects(QueryProjectOrTaskRequest request)
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
        public Response<string> findProjectMember(int projectId)
        {
            var result = new Response<string>();

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
        public Response<ProjectMember> addOrUpdateProjectMember(ProjectMember projectMember)
        {
            var result = new Response<ProjectMember>();

            try
            {
                result.Result = _service.addOrUpdateProjectMember(projectMember);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 通过id获取project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<Project> getProjectById(int id)
        {
            var result = new Response<Project>();

            try
            {
                result.Result = _service.getProjectById(id);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
        
        /// <summary>
        /// 通过id获取projectMember
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<ProjectMember> getProjectMemberById(int id)
        {
            var result = new Response<ProjectMember>();

            try
            {
                result.Result = _service.getProjectMemberById(id);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 获取项目任务树
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<Dictionary<string, object>> getProjectTaskTree(int projectId)
        {
            var result = new Response<Dictionary<string, object>>();

            try
            {
                result.Result = _service.getProjectTaskTree(projectId);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
        
        /// <summary>
        /// 通过id获取task
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<Task> getTaskById(int taskId)
        {
            var result = new Response<Task>();

            try
            {
                result.Result = _service.getTaskById(taskId);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
        
        /// <summary>
        /// 通过id获取任务记录
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<Dictionary<string, object>> getTaskRecordByProjectId(QueryTaskRecordRequest request)
        {
            var result = new Response<Dictionary<string, object>>();

            try
            {
                result.Result = _service.getTaskRecordByProjectId(request);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
        
        /// <summary>
        /// 获取最新的5条任务记录
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<List<TaskRecord>> getLatestTaskRecordByProjectId(int projectId)
        {
            var result = new Response<List<TaskRecord>>();

            try
            {
                result.Result = _service.getLatestTaskRecordByProjectId(projectId);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
        
        /// <summary>
        /// 通过id获取任务评论
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<List<CommentResponse>> getProjectCommentById(int projectId)
        {
            var result = new Response<List<CommentResponse>>();

            try
            {
                result.Result = _service.getProjectCommentById(projectId);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
        
        /// <summary>
        /// 通过id获取任务评论
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<CommentResponse> addProjectComment(AddProjectCommentRequest request)
        {
            var result = new Response<CommentResponse>();
        
            try
            {
                result.Result = _service.addProjectComment(request);
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