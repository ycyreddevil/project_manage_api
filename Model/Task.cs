using SqlSugar;

namespace project_manage_api.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Task
    {
        /// <summary>
        /// 
        /// </summary>
        public Task()
        {
        }

        private System.Int32 _Id;
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 Id { get { return this._Id; } set { this._Id = value; } }

        private System.Int32 _ProjectId;
        /// <summary>
        /// 项目id
        /// </summary>
        public System.Int32 ProjectId { get { return this._ProjectId; } set { this._ProjectId = value; } }

        private System.Int32 _ParentId;
        /// <summary>
        /// 父任务id
        /// </summary>
        public System.Int32 ParentId { get { return this._ParentId; } set { this._ParentId = value; } }

        private System.String _TaskDesc;
        /// <summary>
        /// 任务描述
        /// </summary>
        public System.String TaskDesc { get { return this._TaskDesc; } set { this._TaskDesc = value; } }

        private System.String _RequireDesc;
        /// <summary>
        /// 需求描述
        /// </summary>
        public System.String RequireDesc { get { return this._RequireDesc; } set { this._RequireDesc = value; } }

        private System.String _AcceptanceCriteria;
        /// <summary>
        /// 验收标准
        /// </summary>
        public System.String AcceptanceCriteria { get { return this._AcceptanceCriteria; } set { this._AcceptanceCriteria = value; } }

        private System.String _ChargeUserId;
        /// <summary>
        /// 负责人id
        /// </summary>
        public System.String ChargeUserId { get { return this._ChargeUserId; } set { this._ChargeUserId = value; } }

        private System.Double _Weight;
        /// <summary>
        /// 任务权重
        /// </summary>
        public System.Double Weight { get { return this._Weight; } set { this._Weight = value; } }

        private System.String _TaskType;
        /// <summary>
        /// 任务类型
        /// </summary>
        public System.String TaskType { get { return this._TaskType; } set { this._TaskType = value; } }

        private System.Int32? _Priority;
        /// <summary>
        /// 任务优先级
        /// </summary>
        public System.Int32? Priority { get { return this._Priority; } set { this._Priority = value; } }

        private System.String _Status;
        /// <summary>
        /// 任务状态
        /// </summary>
        public System.String Status { get { return this._Status; } set { this._Status = value; } }

        private System.String _SubmitterId;
        /// <summary>
        /// 提价人id
        /// </summary>
        public System.String SubmitterId { get { return this._SubmitterId; } set { this._SubmitterId = value; } }

        private System.DateTime _CreateTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreateTime { get { return this._CreateTime; } set { this._CreateTime = value; } }

        private System.DateTime? _StartTime;
        /// <summary>
        /// 任务开始时间
        /// </summary>
        public System.DateTime? StartTime { get { return this._StartTime; } set { this._StartTime = value; } }

        private System.DateTime? _EndTime;
        /// <summary>
        /// 任务结束时间
        /// </summary>
        public System.DateTime? EndTime { get { return this._EndTime; } set { this._EndTime = value; } }

        private System.String _Attachment;
        /// <summary>
        /// 任务附件
        /// </summary>
        public System.String Attachment { get { return this._Attachment; } set { this._Attachment = value; } }
    }
}