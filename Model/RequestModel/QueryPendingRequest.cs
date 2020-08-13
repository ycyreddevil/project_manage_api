using System;

namespace project_manage_api.Model.RequestModel
{
    public class QueryPendingRequest : PageRequest
    {
        public string startTime { get; set; }
        public string endTime { get; set; }
    }
}