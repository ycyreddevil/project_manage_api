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
        
        private System.String _CascadeId;
        /// <summary>
        /// 项目id
        /// </summary>
        public System.String CascadeId { get { return this._CascadeId; } set { this._CascadeId = value; } }

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
        
        private System.String _TaskName;
        /// <summary>
        /// 任务名称
        /// </summary>
        public System.String TaskName { get { return this._TaskName; } set { this._TaskName = value; } }

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

        private System.Int32 _ChargeUserId;
        /// <summary>
        /// 负责人id
        /// </summary>
        public System.Int32 ChargeUserId { get { return this._ChargeUserId; } set { this._ChargeUserId = value; } }
        
        private System.String _ChargeUserName;
        /// <summary>
        /// 负责人姓名
        /// </summary>
        public System.String ChargeUserName { get { return this._ChargeUserName; } set { this._ChargeUserName = value; } }

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

        private System.Int32 _Progress;
        /// <summary>
        /// 任务进度
        /// </summary>
        public System.Int32 Progress { get { return this._Progress; } set { this._Progress = value; } }

        private System.Int32? _Priority;
        /// <summary>
        /// 任务优先级
        /// </summary>
        public System.Int32? Priority { get { return this._Priority; } set { this._Priority = value; } }

        private System.Int16 _Status;
        /// <summary>
        /// 任务状态
        /// </summary>
        public System.Int16 Status { get { return this._Status; } set { this._Status = value; } }

        private System.Int32 _SubmitterId;
        /// <summary>
        /// 提价人id
        /// </summary>
        public System.Int32 SubmitterId { get { return this._SubmitterId; } set { this._SubmitterId = value; } }
        
        private System.String _SubmitterName;
        /// <summary>
        /// 提价人id
        /// </summary>
        public System.String SubmitterName { get { return this._SubmitterName; } set { this._SubmitterName = value; } }

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