using SqlSugar;

namespace project_manage_api.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// 
        /// </summary>
        public UserRole()
        {
        }

        private System.Int32 _Id;
        /// <summary>
        /// Id
        /// </summary>
        public System.Int32 Id { get { return this._Id; } set { this._Id = value; } }

        private System.Int32? _UserId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? UserId { get { return this._UserId; } set { this._UserId = value; } }

        private System.Int32? _RoleId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? RoleId { get { return this._RoleId; } set { this._RoleId = value; } }

        private System.DateTime? _CreateTime;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? CreateTime { get { return this._CreateTime; } set { this._CreateTime = value; } }
    }
}
