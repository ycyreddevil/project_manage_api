using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using project_manage_api.Infrastructure;
using project_manage_api.Model;
using project_manage_api.Service;

namespace project_manage_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private DashboardService _service;

        public DashboardController(DashboardService service)
        {
            _service = service;
        }

        /// <summary>
        /// 首页工作台 待办事项数量
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Response<Dictionary<string, object>> getDashboardPendingNum()
        {
            var result = new Response<Dictionary<string, object>>();

            try
            {
                result.Result = _service.getDashboardPendingNum();
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }
            
            return result;
        }

        /// <summary>
        /// 首页工作台 获取项目分析表数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Response<List<Dictionary<string, object>>> getProjectAnalyse()
        {
            var result = new Response<List<Dictionary<string, object>>>();

            try
            {
                result.Result = _service.getProjectAnalyse();
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }
            
            return result;
        }
        
        /// <summary>
        /// 首页工作台 待办事项修改
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Response<Schedule> addOrUpdateSchedule(Schedule schedule)
        {
            var result = new Response<Schedule>();

            try
            {
                result.Result = _service.addOrUpdateSchedule(schedule);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }
            
            return result;
        }
        
        /// <summary>
        /// 首页工作台 待办事项查询
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Response<string> getSchedule()
        {
            var result = new Response<string>();

            try
            {
                result.Result = _service.getSchedule();
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }
            
            return result;
        }
        
        /// <summary>
        /// 首页工作台 删除事项
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Response<string> deleteSchedule(int id)
        {
            var result = new Response<string>();

            try
            {
                _service.deleteSchedule(id);
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