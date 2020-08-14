using System;
using Microsoft.AspNetCore.Mvc;
using project_manage_api.Infrastructure;
using project_manage_api.Model;
using project_manage_api.Model.RequestModel;
using project_manage_api.Service;

namespace project_manage_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PendingController : ControllerBase
    {
        private PendingService _service;

        public PendingController(PendingService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取待我审批列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public PageResponse<string> getToBeApprovedList(QueryPendingRequest request)
        {
            var result = new PageResponse<string>();

            try
            {
                result = _service.getToBeApprovedList(request);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
        
        /// <summary>
        /// 获取我已审批列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public PageResponse<string> getHaveApprovedList(QueryPendingRequest request)
        {
            var result = new PageResponse<string>();

            try
            {
                result = _service.getHaveApprovedList(request);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
        
        /// <summary>
        /// 获取我已提交列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public PageResponse<string> getMySubmitList(QueryPendingRequest request)
        {
            var result = new PageResponse<string>();

            try
            {
                result = _service.getMySubmitList(request);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
        
        /// <summary>
        /// 审批同意
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<int> agree(int taskRecordId)
        {
            var result = new Response<int>();

            try
            {
                result.Result = _service.agree(taskRecordId);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
        
        /// <summary>
        /// 审批同意
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<int> disagree(int taskRecordId, string opinion)
        {
            var result = new Response<int>();

            try
            {
                result.Result = _service.disagree(taskRecordId, opinion);
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