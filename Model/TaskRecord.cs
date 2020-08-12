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

        private System.Int32 _SubmitterId;
        /// <summary>
        /// 提交人id
        /// </summary>
        public System.Int32 SubmitterId { get { return this._SubmitterId; } set { this._SubmitterId = value; } }
        
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

        private System.Int32 _Percent;
        /// <summary>
        /// 审批意见
        /// </summary>
        public System.Int32 Percent { get { return this._Percent; } set { this._Percent = value; } }

        private System.DateTime _CreateTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreateTime { get { return this._CreateTime; } set { this._CreateTime = value; } }
    }
}