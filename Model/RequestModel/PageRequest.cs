using System;
using SqlSugar;

namespace project_manage_api.Model
{
    /// <summary>
    /// 页面传参对象公共实体类
    /// </summary>
    public class PageRequest
    {
        public int Page { get; set; }
        public int Number { get; set; }
        public string Key { get; set; }
        public string SortColumn { get; set; }
        public string SortType { get; set; }

        public PageRequest()
        {
            Page = 1;
            Number = 10;
            Key = string.Empty;
            SortColumn = string.Empty;
            SortType = "asc";
        }
    }
}