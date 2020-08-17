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
    public class MessageController : ControllerBase
    {
        private MessageService _service;

        public MessageController(MessageService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取待回复消息列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public PageResponse<string> getToBeReplied(QueryMessageRequest request)
        {
            var result = new PageResponse<string>();

            try
            {
                result = _service.getToBeReplied(request);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
        
        /// <summary>
        /// 获取已回复消息列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public PageResponse<string> getHaveRelied(QueryMessageRequest request)
        {
            var result = new PageResponse<string>();

            try
            {
                result = _service.getHaveRelied(request);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
        
        /// <summary>
        /// 快速回复消息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public Response quickComment(int parentId, string content, int docId, int targetId)
        {
            var result = new Response();

            try
            {
                _service.quickComment(parentId, content, docId, targetId);
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