using System;
using Microsoft.AspNetCore.Mvc;
using project_manage_api.Infrastructure;
using project_manage_api.Service;

namespace project_manage_api.Controllers
{
    /// <summary>
    /// 用户登录controller
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginService _loginService;
        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }
        
        /// <summary>
        /// 用户名密码登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        public Response Login(string username, string password)
        {
            var result = new Response();
            try
            {
                result = _loginService.Login(username, password);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
        
        /// <summary>
        /// 检查token是否有效
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Response<bool> CheckToken()
        {
            var result = new Response<bool>();
            try
            {
                result.Result = _loginService.CheckToken();
            }
            catch (Exception ex)
            {
                result.Code = Define.INVALID_TOKEN;
                result.Message = ex.Message;
            }

            return result;
        }
        
        /// <summary>
        /// 注销登录
        /// </summary>
        /// <param name="token"></param>
        /// <param name="requestid">备用参数.</param>
        [HttpPost]
        public Response<bool> Logout()
        {
            var resp = new Response<bool>();
            try
            {
                resp.Result = _loginService.Logout();
            }
            catch (Exception e)
            {
                resp.Result = false;
                resp.Message = e.Message;
            }

            return resp;
        }
    }
}