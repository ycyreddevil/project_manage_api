using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace project_manage_api.Infrastructure
{
    /// <summary>
    /// 添加httpHeader参数
    /// </summary>
    public class GlobalHttpHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            var actionAttrs = context.ApiDescription.Properties;
            var isAnony = actionAttrs.Any(a => a.GetType() == typeof(AllowAnonymousAttribute));

            //不是匿名，则添加默认的X-Token
            if (!isAnony)
            {
                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = Define.TOKEN_NAME,  
                    In = ParameterLocation.Header,
                    Description = "当前登录用户登录token",
                    Required = false
                });
            }
        }
    }
}
