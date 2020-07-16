using System.Collections.Generic;

namespace project_manage_api.Model.RequestModel
{
    public class AddOrUpdateProjectRequest
    {
        public Project project { get; set; }

        public string projectMembers { get; set; }
    }
}