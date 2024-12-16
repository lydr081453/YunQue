using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类ProjectTypeInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ProjectTypeInfo
    {
        public ProjectTypeInfo()
        { }
        #region Model
        private int _projecttypeid;
        private string _projecttypename;
        private string _typecode;
        private string _description;
        /// <summary>
        /// 
        /// </summary>
        public int ProjectTypeID
        {
            set { _projecttypeid = value; }
            get { return _projecttypeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProjectTypeName
        {
            set { _projecttypename = value; }
            get { return _projecttypename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TypeCode
        {
            set { _typecode = value; }
            get { return _typecode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }

        /// <summary>
        /// 所属分类ID
        /// </summary>
        public int? ParentID { get; set; }

        /// <summary>
        /// 状态 0：删除 1：正常
        /// </summary>
        public int Status { get; set; }

        public string ParentTypeName{get;set;}

        /// <summary>
        /// 类型负责人ID
        /// </summary>
        public int ProjectHeadId { get; set; }

        /// <summary>
        /// 媒体返点比例
        /// </summary>
        public decimal? CostRate { get; set; }

        #endregion Model

    }
}