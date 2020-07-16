using SqlSugar;

namespace project_manage_api.Model
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("project_member")]
    public class ProjectMember
    {
        /// <summary>
        /// 
        /// </summary>
        public ProjectMember()
        {
        }

        private System.Int32 _Id;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 Id { get { return this._Id; } set { this._Id = value; } }

        private System.Int32 _ProjectId;
        /// <summary>
        /// 项目id
        /// </summary>
        public System.Int32 ProjectId { get { return this._ProjectId; } set { this._ProjectId = value; } }

        private System.String _UserId;
        /// <summary>
        /// 成员id
        /// </summary>
        public System.String UserId { get { return this._UserId; } set { this._UserId = value; } }
        
        private System.String _UserName;
        /// <summary>
        /// 成员id
        /// </summary>
        public System.String UserName { get { return this._UserName; } set { this._UserName = value; } }

        private System.String _ProjectRole;
        /// <summary>
        /// 角色
        /// </summary>
        public System.String ProjectRole { get { return this._ProjectRole; } set { this._ProjectRole = value; } }
        
        private System.Int32 _Status;
        /// <summary>
        /// 角色
        /// </summary>
        public System.Int32 Status { get { return this._Status; } set { this._Status = value; } }
        
        private System.DateTime _ModifyTime;
        /// <summary>
        /// 角色
        /// </summary>
        public System.DateTime ModifyTime { get { return this._ModifyTime; } set { this._ModifyTime = value; } }
    }
}