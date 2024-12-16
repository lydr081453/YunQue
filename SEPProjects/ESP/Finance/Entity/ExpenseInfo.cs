using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类ExpenseInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ExpenseInfo
    {
        public ExpenseInfo()
        { }
        #region Model
        private int _expenseid;
        private int _deptid;
        private string _deptname;
        private int? _branchid;
        private string _branchname;
        private int? _expensetype;
        private decimal? _lastmonthbalance;
        private decimal? _debit;
        private decimal? _loan;
        private int? _confirmstatus;
        private decimal? _balance;
        private int? _projectid;
        private string _creditcode;
        private string _invoiceno;
        private string _returnorder;
        private byte[] _lastupdatetime;
        private string _projectcode;
        private int? _applicantuserid;
        private string _applicantusername;
        private string _applicantcode;
        private string _applicantemployeename;
        private string _description;
        private string _checkcode;
        /// <summary>
        /// ID
        /// </summary>
        public int ExpenseID
        {
            set { _expenseid = value; }
            get { return _expenseid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DeptName
        {
            set { _deptname = value; }
            get { return _deptname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? BranchID
        {
            set { _branchid = value; }
            get { return _branchid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BranchName
        {
            set { _branchname = value; }
            get { return _branchname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ExpenseType
        {
            set { _expensetype = value; }
            get { return _expensetype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? LastMonthBalance
        {
            set { _lastmonthbalance = value; }
            get { return _lastmonthbalance; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Debit
        {
            set { _debit = value; }
            get { return _debit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Loan
        {
            set { _loan = value; }
            get { return _loan; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ConfirmStatus
        {
            set { _confirmstatus = value; }
            get { return _confirmstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Balance
        {
            set { _balance = value; }
            get { return _balance; }
        }
        /// <summary>
        /// ProjectID
        /// </summary>
        public int? ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
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
        public string InvoiceNo
        {
            set { _invoiceno = value; }
            get { return _invoiceno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReturnOrder
        {
            set { _returnorder = value; }
            get { return _returnorder; }
        }
        /// <summary>
        /// 
        /// </summary>
        public byte[] Lastupdatetime
        {
            set { _lastupdatetime = value; }
            get { return _lastupdatetime; }
        }
        /// <summary>
        /// Project编号
        /// </summary>
        public string ProjectCode
        {
            set { _projectcode = value; }
            get { return _projectcode; }
        }
        /// <summary>
        /// ID自增长
        /// </summary>
        public int? ApplicantUserID
        {
            set { _applicantuserid = value; }
            get { return _applicantuserid; }
        }
        /// <summary>
        /// 申请人登录帐号
        /// </summary>
        public string ApplicantUserName
        {
            set { _applicantusername = value; }
            get { return _applicantusername; }
        }
        /// <summary>
        /// 内部编号
        /// </summary>
        public string ApplicantCode
        {
            set { _applicantcode = value; }
            get { return _applicantcode; }
        }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string ApplicantEmployeeName
        {
            set { _applicantemployeename = value; }
            get { return _applicantemployeename; }
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
        public string CheckCode
        {
            set { _checkcode = value; }
            get { return _checkcode; }
        }
        #endregion Model

    }
}