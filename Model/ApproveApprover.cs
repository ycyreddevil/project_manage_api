using System;
using SqlSugar;

namespace project_manage_api.Model
{
    [SugarTable("approve_approver")]
    public class ApproveApprover
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public int DocId { get; set; }
        public int ApproverId { get; set; }
        public int Level { get; set; }
        public DateTime CreateTime { get; set; }

        public ApproveApprover()
        {
            CreateTime = DateTime.Now;
        }
    }
}