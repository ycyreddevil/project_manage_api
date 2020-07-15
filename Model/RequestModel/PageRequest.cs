using System;
using SqlSugar;

namespace project_manage_api.Model
{
    /// <summary>
    /// 页面传参对象公共实体类
    /// </summary>
    public class PageRequest
    {
        public int page { get; set; }
        public int limit { get; set; }
        public string key { get; set; }
        public string sortColumn { get; set; }
        public string sortType { get; set; }

        public PageRequest()
        {
            page = 1;
            limit = 20;
            key = string.Empty;
            sortColumn = "Id";
            sortType = "asc";
        }
    }
}