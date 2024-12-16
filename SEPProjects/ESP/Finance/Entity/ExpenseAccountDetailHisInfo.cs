using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类ExpenseAccountDetailHisInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ExpenseAccountDetailHisInfo
    {
        public ExpenseAccountDetailHisInfo()
        { }
        #region Model
        private int _hisid;
        private int? _expenseaccountdetailid;
        private int? _returnid;
        private string _expensedescold;
        private string _expensedescnew;
        private decimal? _expensemoneyold;
        private decimal? _expensemoneynew;
        private decimal? _currentprefeeold;
        private decimal? _currentprefeenew;
        private int? _modifyuserid;
        private string _modifyusername;
        private DateTime? _modifydatetime;
        /// <summary>
        /// 
        /// </summary>
        public int HisID
        {
            set { _hisid = value; }
            get { return _hisid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ExpenseAccountDetailID
        {
            set { _expenseaccountdetailid = value; }
            get { return _expenseaccountdetailid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ReturnID
        {
            set { _returnid = value; }
            get { return _returnid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExpenseDescOld
        {
            set { _expensedescold = value; }
            get { return _expensedescold; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExpenseDescNew
        {
            set { _expensedescnew = value; }
            get { return _expensedescnew; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? ExpenseMoneyOld
        {
            set { _expensemoneyold = value; }
            get { return _expensemoneyold; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? ExpenseMoneyNew
        {
            set { _expensemoneynew = value; }
            get { return _expensemoneynew; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? CurrentPreFeeOld
        {
            set { _currentprefeeold = value; }
            get { return _currentprefeeold; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? CurrentPreFeeNew
        {
            set { _currentprefeenew = value; }
            get { return _currentprefeenew; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ModifyUserID
        {
            set { _modifyuserid = value; }
            get { return _modifyuserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModifyUserName
        {
            set { _modifyusername = value; }
            get { return _modifyusername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ModifyDateTime
        {
            set { _modifydatetime = value; }
            get { return _modifydatetime; }
        }
        #endregion Model

    }
}

