using SqlSugar;

namespace project_manage_api.Model
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("user_department_post")]
    public class UserDepartmentPost
    {
        /// <summary>
        /// 
        /// </summary>
        public UserDepartmentPost()
        {
        }

        private System.Int32 _Id;
        /// <summary>
        /// id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 Id { get { return this._Id; } set { this._Id = value; } }

        private System.String _wechatUserId;
        /// <summary>
        /// 微信userid
        /// </summary>
        public System.String wechatUserId { get { return this._wechatUserId; } set { this._wechatUserId = value; } }

        private System.Int32 _userId;
        /// <summary>
        /// userid
        /// </summary>
        public System.Int32 userId { get { return this._userId; } set { this._userId = value; } }

        private System.Int32 _departmentId;
        /// <summary>
        /// 部门id
        /// </summary>
        public System.Int32 departmentId { get { return this._departmentId; } set { this._departmentId = value; } }

        private System.Int32? _isHead;
        /// <summary>
        /// 是否负责人
        /// </summary>
        public System.Int32? isHead { get { return this._isHead; } set { this._isHead = value; } }
    }
}