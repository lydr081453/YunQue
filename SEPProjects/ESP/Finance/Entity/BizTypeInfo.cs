using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类BizTypeInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class BizTypeInfo
    {
        public BizTypeInfo()
        { }
        #region Model
        private int _bizid;
        private string _biztypename;
        private string _description;
        /// <summary>
        /// 
        /// </summary>
        public int BizID
        {
            set { _bizid = value; }
            get { return _bizid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BizTypeName
        {
            set { _biztypename = value; }
            get { return _biztypename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        #endregion Model

    }
}