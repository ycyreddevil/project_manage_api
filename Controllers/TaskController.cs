using System;
using Microsoft.AspNetCore.Mvc;
using project_manage_api.Infrastructure;
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
        public Response<string> getTaskList(QueryProjectOrTaskRequest request)
        {
            var result = new Response<string>();

            try
            {
                result.Result = _service.getTaskList(request);
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