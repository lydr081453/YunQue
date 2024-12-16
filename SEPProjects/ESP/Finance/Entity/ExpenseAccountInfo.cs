using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类ExpenseAccountInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ExpenseAccountInfo
    {
        public ExpenseAccountInfo()
        { }
        #region Model
        private int _id;
        private int? _returnid;
        private decimal? _confirmfee;

        /// <summary>
        /// 主键
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// ReturnID
        /// </summary>
        public int? ReturnID
        {
            set { _returnid = value; }
            get { return _returnid; }
        }
        /// <summary>
        /// 收货金额
        /// </summary>
        public decimal? ConfirmFee
        {
            set { _confirmfee = value; }
            get { return _confirmfee; }
        }
        
        #endregion Model

    }
}
