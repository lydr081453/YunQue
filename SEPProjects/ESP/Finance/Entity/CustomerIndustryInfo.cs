using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类CustomerIndustryInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class CustomerIndustryInfo
    {
        public CustomerIndustryInfo()
        { }
        #region Model
        private int _industryid;
        private string _industrycode;
        private string _categoryname;
        private string _description;
        private int _parentid;
        /// <summary>
        /// 
        /// </summary>
        public int IndustryID
        {
            set { _industryid = value; }
            get { return _industryid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IndustryCode
        {
            set { _industrycode = value; }
            get { return _industrycode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CategoryName
        {
            set { _categoryname = value; }
            get { return _categoryname; }
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
        /// 
        /// </summary>
        public int ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        #endregion Model

    }
}