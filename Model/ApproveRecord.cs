using System;
using SqlSugar;

namespace project_manage_api.Model
{
    [SugarTable("approve_record")]
    public class ApproveRecord
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public int DocId { get; set; }
        public int ApproverId { get; set; }
        public string Result { get; set; }
        public string Opinion { get; set; }
        public DateTime CreateTime { get; set; }

        public ApproveRecord()
        {
            CreateTime = DateTime.Now;
        }
    }
}