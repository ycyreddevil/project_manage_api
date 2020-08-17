using SqlSugar;

namespace project_manage_api.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// 
        /// </summary>
        public Comment()
        {
        }

        private System.Int32 _Id;
        /// <summary>
        /// id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 Id { get { return this._Id; } set { this._Id = value; } }

        private System.Int32 _ParentId;
        /// <summary>
        /// 父节点id 根节点为-1
        /// </summary>
        public System.Int32 ParentId { get { return this._ParentId; } set { this._ParentId = value; } }

        private System.Int32 _Type;
        /// <summary>
        /// 0-项目评论 1-任务评论 2-任务完成情况评论
        /// </summary>
        public System.Int32 Type { get { return this._Type; } set { this._Type = value; } }

        private System.Int32 _DocId;
        /// <summary>
        /// 单据id
        /// </summary>
        public System.Int32 DocId { get { return this._DocId; } set { this._DocId = value; } }

        private System.Int32 _SubmitterId;
        /// <summary>
        /// 提交人id
        /// </summary>
        public System.Int32 SubmitterId { get { return this._SubmitterId; } set { this._SubmitterId = value; } }
        
        private System.Int32 _TargetId;
        /// <summary>
        /// 被评论者id
        /// </summary>
        public System.Int32 TargetId { get { return this._TargetId; } set { this._TargetId = value; } }
        
        private System.Int32 _HaveRead;
        /// <summary>
        /// 是否已读
        /// </summary>
        public System.Int32 HaveRead { get { return this._HaveRead; } set { this._HaveRead = value; } }

        private System.String _Content;
        /// <summary>
        /// 评论内容
        /// </summary>
        public System.String Content { get { return this._Content; } set { this._Content = value; } }

        private System.String _Attachment;
        /// <summary>
        /// 评论附件
        /// </summary>
        public System.String Attachment { get { return this._Attachment; } set { this._Attachment = value; } }

        private System.DateTime _CreateTime;
        /// <summary>
        /// 提交时间
        /// </summary>
        public System.DateTime CreateTime { get { return this._CreateTime; } set { this._CreateTime = value; } }
    }
}