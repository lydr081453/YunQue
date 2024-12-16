using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类BizDescriptionInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    /// 
    [Serializable]
    public partial class BizDescriptionInfo
    {
        public BizDescriptionInfo()
        { }
        #region Model
        private int _bizdescid;
        private string _bizdescription;
        /// <summary>
        /// 
        /// </summary>
        public int BizDescID
        {
            set { _bizdescid = value; }
            get { return _bizdescid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BizDescription
        {
            set { _bizdescription = value; }
            get { return _bizdescription; }
        }
        #endregion Model

    }
}