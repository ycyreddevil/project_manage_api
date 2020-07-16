using System;

namespace project_manage_api.Model.QueryModel
{
    public class QueryProjectRequest : PageRequest
    {
        public string type { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }

        public QueryProjectRequest()
        {
            type = string.Empty;
            startTime = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd HH:mm:ss");
            endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}