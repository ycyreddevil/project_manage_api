using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using project_manage_api.Infrastructure;

namespace project_manage_api.Model
{
    /// <summary>
    /// 分页response
    /// </summary>
    public class PageResponse<T>
    {
        public int Total { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
        public int Code { get; set; }

        public PageResponse()
        {
            Total = 0;
            Message = "success";
            Code = 200;
        }
    }
}