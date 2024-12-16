using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类BranchInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    /// 
    [Serializable]
    public partial class BranchInfo
    {
        public BranchInfo()
        { }
        #region Model
        private int _branchid;
        private string _exchangeno;
        private string _requestphone;
        private string _branchcode;
        private string _branchname;
        private string _des;
        private string _dbcode;
        private string _dbmanager;
        private string _bankname;
        private string _bankaccount;
        private string _phoneno;
        private int _paymentAccounter;
        private int _projectAccounter;
        private int _ContractAccounter;
        private int _FinalAccounter;
        private string _OtherFinancialUsers;

        public int DepartmentId { get; set; }
        public int SalaryAccounter { get; set; }
        public string BusinessCardAccounter { get; set; }
        public int DimissionAuditor { get; set; }
        public string GMUsers { get; set; }
        /// <summary>
        /// 分公司LOGO
        /// </summary>
        public string Logo { get; set; }
        /// <summary>
        /// 分公司PO单条款附件
        /// </summary>
        public string POTerm{ get; set; }
        /// <summary>
        /// 分公司PO单行为规范
        /// </summary>
        public string POStandard { get; set; }
        /// <summary>
        /// 证据链审核人
        /// </summary>
        public int ContractAuditor { get; set; }
       
        public string OtherFinancialUsers
        {
            get { return _OtherFinancialUsers; }
            set { _OtherFinancialUsers = value; }
        }
        public int ContractAccounter
        {
            get { return _ContractAccounter; }
            set { _ContractAccounter = value; }
        }

        public int FinalAccounter
        {
            get { return _FinalAccounter; }
            set { _FinalAccounter = value; }
        }

        public int ProjectAccounter
        {
            get { return _projectAccounter; }
            set { _projectAccounter = value; }
        }

        public int PaymentAccounter
        {
            get { return _paymentAccounter; }
            set { _paymentAccounter = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int BranchID
        {
            set { _branchid = value; }
            get { return _branchid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExchangeNo
        {
            set { _exchangeno = value; }
            get { return _exchangeno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RequestPhone
        {
            set { _requestphone = value; }
            get { return _requestphone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BranchCode
        {
            set { _branchcode = value; }
            get { return _branchcode; }
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
        public string Des
        {
            set { _des = value; }
            get { return _des; }
        }
        /// <summary>
        /// 数据库代码
        /// </summary>
        public string DBCode
        {
            set { _dbcode = value; }
            get { return _dbcode; }
        }
        /// <summary>
        /// 管理数据库
        /// </summary>
        public string DBManager
        {
            set { _dbmanager = value; }
            get { return _dbmanager; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BankName
        {
            set { _bankname = value; }
            get { return _bankname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BankAccount
        {
            set { _bankaccount = value; }
            get { return _bankaccount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PhoneNo
        {
            set { _phoneno = value; }
            get { return _phoneno; }
        }

        int _firstFinanceID;

        public int FirstFinanceID
        {
            get { return _firstFinanceID; }
            set { _firstFinanceID = value; }
        }

       
        #endregion Model

    }
}