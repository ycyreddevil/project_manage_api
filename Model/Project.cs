using SqlSugar;

namespace project_manage_api.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Project
    {
        /// <summary>
        /// 
        /// </summary>
        public Project()
        {
        }

        private System.Int32 _Id;
        /// <summary>
        /// id
        /// </summary>
        public System.Int32 Id { get { return this._Id; } set { this._Id = value; } }

        private System.String _Code;
        /// <summary>
        /// 项目编号
        /// </summary>
        public System.String Code { get { return this._Code; } set { this._Code = value; } }

        private System.String _Name;
        /// <summary>
        /// 项目名称
        /// </summary>
        public System.String Name { get { return this._Name; } set { this._Name = value; } }

        private System.String _Type;
        /// <summary>
        /// 项目类型
        /// </summary>
        public System.String Type { get { return this._Type; } set { this._Type = value; } }

        private System.DateTime? _StartTime;
        /// <summary>
        /// 项目开始时间
        /// </summary>
        public System.DateTime? StartTime { get { return this._StartTime; } set { this._StartTime = value; } }

        private System.DateTime? _EndTime;
        /// <summary>
        /// 项目结束时间
        /// </summary>
        public System.DateTime? EndTime { get { return this._EndTime; } set { this._EndTime = value; } }

        private System.String _TeamName;
        /// <summary>
        /// 项目团队名称
        /// </summary>
        public System.String TeamName { get { return this._TeamName; } set { this._TeamName = value; } }

        private System.String _Desc;
        /// <summary>
        /// 项目描述
        /// </summary>
        public System.String Desc { get { return this._Desc; } set { this._Desc = value; } }

        private System.Int32? _Level;
        /// <summary>
        /// 级别
        /// </summary>
        public System.Int32? Level { get { return this._Level; } set { this._Level = value; } }

        private System.String _Status;
        /// <summary>
        /// 项目状态
        /// </summary>
        public System.String Status { get { return this._Status; } set { this._Status = value; } }

        private System.Int32 _ChargeUserId;
        /// <summary>
        /// 项目负责人id
        /// </summary>
        public System.Int32 ChargeUserId { get { return this._ChargeUserId; } set { this._ChargeUserId = value; } }
        
        private System.String _ChargeUserName;
        /// <summary>
        /// 项目负责人名称
        /// </summary>
        public System.String ChargeUserName { get { return this._ChargeUserName; } set { this._ChargeUserName = value; } }

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

        private System.DateTime _CreateTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreateTime { get { return this._CreateTime; } set { this._CreateTime = value; } }
    }
}