using System;

namespace project_manage_api.Model.QueryModel
{
    public class QueryProjectRequest : PageRequest
    {
        public string type { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }

        public QueryProjectRequest()
        {
            type = string.Empty;
            startTime = DateTime.Now.AddMonths(-1);
            endTime = DateTime.Now;
        }
    }
}