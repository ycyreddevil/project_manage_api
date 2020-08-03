using SqlSugar;

namespace project_manage_api.Model
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("task_record")]
    public class TaskRecord
    {
        /// <summary>
        /// 
        /// </summary>
        public TaskRecord()
        {
        }

        private System.Int32 _Id;
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 Id { get { return this._Id; } set { this._Id = value; } }

        private System.Int32 _TaskId;
        /// <summary>
        /// 任务id
        /// </summary>
        public System.Int32 TaskId { get { return this._TaskId; } set { this._TaskId = value; } }

        private System.String _SubmitterId;
        /// <summary>
        /// 提交人id
        /// </summary>
        public System.String SubmitterId { get { return this._SubmitterId; } set { this._SubmitterId = value; } }
        
        private System.String _SubmitterName;
        /// <summary>
        /// 提交人id
        /// </summary>
        public System.String SubmitterName { get { return this._SubmitterName; } set { this._SubmitterName = value; } }

        private System.String _Desc;
        /// <summary>
        /// 完成情况描述
        /// </summary>
        public System.String Desc { get { return this._Desc; } set { this._Desc = value; } }

        private System.String _Attachment;
        /// <summary>
        /// 附件
        /// </summary>
        public System.String Attachment { get { return this._Attachment; } set { this._Attachment = value; } }

        private System.SByte _Status;
        /// <summary>
        /// -1-草稿 0-已提交 1-已确认 2-已驳回
        /// </summary>
        public System.SByte Status { get { return this._Status; } set { this._Status = value; } }

        private System.String _Opinion;
        /// <summary>
        /// 审批意见
        /// </summary>
        public System.String Opinion { get { return this._Opinion; } set { this._Opinion = value; } }

        private System.DateTime _CreateTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreateTime { get { return this._CreateTime; } set { this._CreateTime = value; } }

        private System.DateTime? _StartTime;
        /// <summary>
        /// 开始时间
        /// </summary>
        public System.DateTime? StartTime { get { return this._StartTime; } set { this._StartTime = value; } }

        private System.DateTime? _EndTime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public System.DateTime? EndTime { get { return this._EndTime; } set { this._EndTime = value; } }
    }
}