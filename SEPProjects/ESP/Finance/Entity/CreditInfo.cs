using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类CreditInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class CreditInfo
    {
        public CreditInfo()
        { }
        #region Model
        private int _creditid;
        private string _creditcode;
        private string _credittype;
        private string _description;
        /// <summary>
        /// 
        /// </summary>
        public int CreditID
        {
            set { _creditid = value; }
            get { return _creditid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreditCode
        {
            set { _creditcode = value; }
            get { return _creditcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreditType
        {
            set { _credittype = value; }
            get { return _credittype; }
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