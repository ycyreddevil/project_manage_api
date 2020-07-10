using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using project_manage_api.Infrastructure;
using project_manage_api.Model;
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
        [AllowAnonymous]
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
            var result = new Response<bool>();
            try
            {
                result.Result = _loginService.Logout();
            }
            catch (Exception e)
            {
                result.Result = false;
                result.Message = e.Message;
            }

            return result;
        }

        /// <summary>
        /// 通过token获取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        public Response<Dictionary<string, object>> GetInfo(string token)
        {
            var result = new Response<Dictionary<string, object>>();
            try
            {
                result.Result = _loginService.getInfo(token);
            }
            catch (Exception e)
            {
                result.Code = 500;
                result.Message = e.Message;
            }

            return result;
        }

        /// <summary>
        /// 获取用户可访问的菜单
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        public Response<IEnumerable<TreeItem<Module>>> GetModulesTree(string token)
        {
            var result = new Response<IEnumerable<TreeItem<Module>>>();
            try
            {
                result.Result = _loginService.getModulesTree(token);
            }
            catch (Exception e)
            {
                result.Code = 500;
                result.Message = e.Message;
            }

            return result;
        }
    }
}