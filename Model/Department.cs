using SqlSugar;

namespace project_manage_api.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Department
    {
        /// <summary>
        /// 
        /// </summary>
        public Department()
        {
        }

        private System.Int32 _Id;
        /// <summary>
        /// id
        /// </summary>
        public System.Int32 Id { get { return this._Id; } set { this._Id = value; } }

        private System.String _name;
        /// <summary>
        /// 部门名称
        /// </summary>
        public System.String name { get { return this._name; } set { this._name = value; } }

        private System.String _reportName;
        /// <summary>
        /// 部门报表名称
        /// </summary>
        public System.String reportName { get { return this._reportName; } set { this._reportName = value; } }

        private System.Int32 _parentId;
        /// <summary>
        /// 父部门id
        /// </summary>
        public System.Int32 parentId { get { return this._parentId; } set { this._parentId = value; } }

        private System.String _parentName;
        /// <summary>
        /// 父部门名称
        /// </summary>
        public System.String parentName { get { return this._parentName; } set { this._parentName = value; } }

        private System.String _remark;
        /// <summary>
        /// 备注
        /// </summary>
        public System.String remark { get { return this._remark; } set { this._remark = value; } }

        private System.String _state;
        /// <summary>
        /// 状态
        /// </summary>
        public System.String state { get { return this._state; } set { this._state = value; } }

        private System.UInt32? _orderForSameParent;
        /// <summary>
        /// 排序
        /// </summary>
        public System.UInt32? orderForSameParent { get { return this._orderForSameParent; } set { this._orderForSameParent = value; } }
    }
}