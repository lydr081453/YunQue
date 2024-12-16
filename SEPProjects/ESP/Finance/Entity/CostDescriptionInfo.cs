using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类CostDescriptionInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class CostDescriptionInfo
    {
        public CostDescriptionInfo()
        { }
        #region Model
        private int _costdescid;
        private string _costdescription;
        /// <summary>
        /// 
        /// </summary>
        public int CostDescID
        {
            set { _costdescid = value; }
            get { return _costdescid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CostDescription
        {
            set { _costdescription = value; }
            get { return _costdescription; }
        }
        #endregion Model

    }
}