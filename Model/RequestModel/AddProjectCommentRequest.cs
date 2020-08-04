namespace project_manage_api.Model.RequestModel
{
    public class AddProjectCommentRequest
    {
        public string content { get; set; }
        public int projectId { get; set; }
        public int parentId { get; set; }
        
        public int targetUserId { get; set; }
    }
}