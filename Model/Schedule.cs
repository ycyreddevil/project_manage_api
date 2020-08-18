using SqlSugar;

namespace project_manage_api.Model
{
    public class Schedule
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public int SubmitterUserId { get; set; }
        public string Text { get; set; }
        public int Done { get; set; }
    }
}