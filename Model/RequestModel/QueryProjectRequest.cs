using System;

namespace project_manage_api.Model.QueryModel
{
    public class QueryProjectRequest : PageRequest
    {
        public string Type { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}