using SqlSugar;

namespace project_manage_api.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Role
    {
        /// <summary>
        /// 
        /// </summary>
        public Role()
        {
        }

        private System.Int32 _Id;
        /// <summary>
        /// id
        /// </summary>
        public System.Int32 Id { get { return this._Id; } set { this._Id = value; } }

        private System.String _Name;
        /// <summary>
        /// 角色名称
        /// </summary>
        public System.String Name { get { return this._Name; } set { this._Name = value; } }

        private System.DateTime _CreateTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreateTime { get { return this._CreateTime; } set { this._CreateTime = value; } }
    }
}
