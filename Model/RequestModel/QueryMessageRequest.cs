namespace project_manage_api.Model.RequestModel
{
    public class QueryMessageRequest : PageRequest
    {
        public string startTime { get; set; }
        public string endTime { get; set; }

        public int type { get; set; }
    }
}