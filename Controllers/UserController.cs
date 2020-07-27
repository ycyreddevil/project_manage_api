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
    public class UserController : ControllerBase
    {
        private UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }
        
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<List<Users>> findUsers(string key)
        {
            var result = new Response<List<Users>>();

            try
            {
                result.Result = _userService.findUsers(key);
            }
            catch (Exception ex)
            {
                result.Code = 200;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}