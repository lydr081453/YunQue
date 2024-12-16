using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类ContractStatusInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ContractStatusInfo
    {
        public ContractStatusInfo()
        { }
        #region Model
        private int _contractstatusid;
        private string _contractstatusname;
        private string _description;
        /// <summary>
        /// 
        /// </summary>
        public int ContractStatusID
        {
            set { _contractstatusid = value; }
            get { return _contractstatusid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ContractStatusName
        {
            set { _contractstatusname = value; }
            get { return _contractstatusname; }
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