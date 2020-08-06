using System;
using Microsoft.AspNetCore.Mvc;
using project_manage_api.Infrastructure;
using project_manage_api.Model;
using project_manage_api.Model.QueryModel;
using project_manage_api.Service;

namespace project_manage_api.Controllers
{
    /// <summary>
    /// 用户任务controller
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private TaskService _service;
        public TaskController(TaskService taskService)
        {
            _service = taskService;
        }

        /// <summary>
        /// 获取我的任务列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PageResponse<string> getTaskList(QueryProjectOrTaskRequest request)
        {
            var result = new PageResponse<string>();

            try
            {
                result = _service.getTaskList(request);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
        
        /// <summary>
        /// 开始任务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Response beginTask(int taskId)
        {
            var result = new Response();

            try
            {
                _service.beginTask(taskId);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
        
        /// <summary>
        /// 结束任务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Response endTask(int taskId, int approverId)
        {
            var result = new Response();

            try
            {
                _service.endTask(taskId, approverId);
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