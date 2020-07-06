﻿using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
 using project_manage_api.Service;

 namespace project_manage_api.Infrastructure
{
    public class LoginFilter : IActionFilter
    {
        private readonly LoginService _service;

        public LoginFilter(LoginService service)
        {
            _service = service;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var description =
                (Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor;

            var Controllername = description.ControllerName.ToLower();
            var Actionname = description.ActionName.ToLower();

            //匿名标识
            var authorize = description.MethodInfo.GetCustomAttribute(typeof(AllowAnonymousAttribute));
            if (authorize != null)
            {
                return;
            }

            if (_service.CheckToken()) return;
            
            context.HttpContext.Response.StatusCode = 401;
            context.Result = new JsonResult(new Response
            {
                Code = 401,
                Message = "认证失败，请提供认证信息"
            });
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }
    }
}
