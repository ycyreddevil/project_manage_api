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

        private System.String _Role;
        /// <summary>
        /// 角色
        /// </summary>
        public System.String Role { get { return this._Role; } set { this._Role = value; } }
    }
}