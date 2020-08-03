using System;

namespace project_manage_api.Model.RequestModel
{
    public class QueryTaskRecordRequest
    {
        public int projectId { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }

        public QueryTaskRecordRequest()
        {
            projectId = 0;
            startTime = string.Empty;
            endTime = string.Empty;
        }
    }
}