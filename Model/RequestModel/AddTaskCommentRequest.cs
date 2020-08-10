namespace project_manage_api.Model.RequestModel
{
    public class AddTaskCommentRequest
    {
        public string content { get; set; }
        public int taskId { get; set; }
        public int parentId { get; set; }
        
        public int targetUserId { get; set; }
    }
}