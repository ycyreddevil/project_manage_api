using SqlSugar;

namespace project_manage_api.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Module
    {
        /// <summary>
        /// 
        /// </summary>
        public Module()
        {
        }

        private System.Int32 _Id;
        /// <summary>
        /// id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 Id { get { return this._Id; } set { this._Id = value; } }

        private System.String _CascadeId;
        /// <summary>
        /// 菜单节点
        /// </summary>
        public System.String CascadeId { get { return this._CascadeId; } set { this._CascadeId = value; } }

        private System.String _Name;
        /// <summary>
        /// 菜单名称
        /// </summary>
        public System.String Name { get { return this._Name; } set { this._Name = value; } }

        private System.String _Url;
        /// <summary>
        /// 菜单url
        /// </summary>
        public System.String Url { get { return this._Url; } set { this._Url = value; } }

        private System.String _IconName;
        /// <summary>
        /// 菜单icon
        /// </summary>
        public System.String IconName { get { return this._IconName; } set { this._IconName = value; } }

        private System.Int32? _ParentId;
        /// <summary>
        /// 父级菜单id
        /// </summary>
        public System.Int32? ParentId { get { return this._ParentId; } set { this._ParentId = value; } }

        private System.String _ParentName;
        /// <summary>
        /// 父级菜单名称
        /// </summary>
        public System.String ParentName { get { return this._ParentName; } set { this._ParentName = value; } }

        private System.Int32? _SortNo;
        /// <summary>
        /// 排序
        /// </summary>
        public System.Int32? SortNo { get { return this._SortNo; } set { this._SortNo = value; } }
    }
}
