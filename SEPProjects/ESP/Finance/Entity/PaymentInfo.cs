using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类PaymentInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class PaymentInfo
    {
        public PaymentInfo()
        { }
        #region Model
        private int _paymentid;
        private string _paymentcode;
        private int _projectid;
        private string _projectcode;
        private DateTime _paymentpredate;
        private DateTime? _paymentfactdate;
        private string _paymentcontent;
        private int _paymentstatus;
        private decimal? _paymentbudget;
        private decimal? _paymentbudgetConfirm;
        private decimal _paymentfee;
        private string _invoiceno;
        private DateTime? _invoicedate;
        private int? _branchid;
        private string _branchcode;
        private string _branchname;
        private byte[] _lastupdatetime;
        private string _remark;
        private int? _bankid;
        private string _bankname;
        private string _dbcode;
        private string _dbmanager;
        private string _bankaccount;
        private string _phoneno;
        private string _exchangeno;
        private string _requestphone;
        private int? _paymenttypeid;
        private string _paymenttypename;
        private string _paymenttypecode;
        private int? _paymentuserid;
        private string _paymentusercode;
        private string _paymentemployeename;
        private string _paymentusername;
        private string _creditCode;
        private decimal _USDDiffer;
        private int _paymentextensionstatus;
        private DateTime? _BillDate;
        private DateTime? _EstReturnDate;

        public int BadDebt { get; set; }

        #region 发票信息
        /// <summary>
        /// 发票开票日期
        /// </summary>
        public DateTime? InvoiceDate
        {
            set { _invoicedate = value; }
            get { return _invoicedate; }
        }
        /// <summary>
        /// 发票抬头
        /// </summary>
        public string InvoiceTitle { get; set; }
        /// <summary>
        /// 发票金额
        /// </summary>
        public decimal InvoiceAmount { get; set; }
        /// <summary>
        /// 发票领用人
        /// </summary>
        public string InvoiceReceiver { get; set; }
        /// <summary>
        /// 签收单
        /// </summary>
        public string InvoiceSignIn { get; set; }
        /// <summary>
        /// 确认收入状态
        /// </summary>
        public string ConfirmRemark { get; set; }

        public int ConfirmYear { get; set; }
        public int ConfirmMonth { get; set; }
        public int InnerRelation { get; set; }
        public string InvoiceType { get; set; }

        #endregion

        #region 返点发票信息
        public string ReBateTitle { get; set; }
        public string RebateNo { get; set; }
        public DateTime? RebateDate { get; set; }
        public decimal RebateAmount { get; set; }
        public string RebateReceiver { get; set; }
        public string RebateSignIn { get; set; }
        public int RebateYear { get; set; }
        public int RebateMonth { get; set; }
        /// <summary>
        /// 返点发票类型
        /// </summary>
        public string RebateType { get; set; }

        #endregion
        /// <summary>
        /// 付款通知金额
        /// </summary>
        public decimal? PaymentBudget
        {
            set { _paymentbudget = value; }
            get { return _paymentbudget; }
        }

        public decimal? PaymentBudgetConfirm
        {
            set { _paymentbudgetConfirm = value; }
            get { return _paymentbudgetConfirm; }
        }

        /// <summary>
        /// 付款通知金额(外币)
        /// </summary>
        public decimal BudgetForiegn { get; set; }
        /// <summary>
        /// 付款通知金额(外币单位)
        /// </summary>
        public string BudgetForiegnUnit { get; set; }
        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceNo
        {
            set { _invoiceno = value; }
            get { return _invoiceno; }
        }
       

        #region"出纳确认字段"
        /// <summary>
        /// 回款金额
        /// </summary>
        public decimal PaymentFee
        {
            set { _paymentfee = value; }
            get { return _paymentfee; }
        }
        /// <summary>
        /// 回款日期
        /// </summary>
        public DateTime? PaymentFactDate
        {
            set { _paymentfactdate = value; }
            get { return _paymentfactdate; }
        }
        /// <summary>
        /// 回款金额（外币）
        /// </summary>
        public decimal PaymentFactForiegn { get; set; }
        /// <summary>
        ///  回款金额（外币单位）
        /// </summary>
        public string PaymentFactForiegnUnit { get; set; }

        /// <summary>
        /// 汇兑损益（回款）
        /// </summary>
        public decimal USDDiffer
        {
            get { return _USDDiffer; }
            set { _USDDiffer = value; }
        }
        /// <summary>
        /// 汇票到期日
        /// </summary>
        public DateTime? BillDate
        {
            get { return _BillDate; }
            set { _BillDate = value; }
        }

        /// <summary>
        /// EstReturnDate
        /// </summary>
        public DateTime? EstReturnDate
        {
            get { return _EstReturnDate; }
            set { _EstReturnDate = value; }
        }

        #endregion


        public string CreditCode
        {
            get { return _creditCode; }
            set { _creditCode = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? PaymentUserID
        {
            set { _paymentuserid = value; }
            get { return _paymentuserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PaymentUserCode
        {
            set { _paymentusercode = value; }
            get { return _paymentusercode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PaymentEmployeeName
        {
            set { _paymentemployeename = value; }
            get { return _paymentemployeename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PaymentUserName
        {
            set { _paymentusername = value; }
            get { return _paymentusername; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int PaymentID
        {
            set { _paymentid = value; }
            get { return _paymentid; }
        }
        /// <summary>
        /// 付款通知号码
        /// </summary>
        public string PaymentCode
        {
            set { _paymentcode = value; }
            get { return _paymentcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProjectCode
        {
            set { _projectcode = value; }
            get { return _projectcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime PaymentPreDate
        {
            set { _paymentpredate = value; }
            get { return _paymentpredate; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string PaymentContent
        {
            set { _paymentcontent = value; }
            get { return _paymentcontent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int PaymentStatus
        {
            set { _paymentstatus = value; }
            get { return _paymentstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int PaymentExtensionStatus
        {
            set { _paymentextensionstatus = value; }
            get { return _paymentextensionstatus; }
        }


        /// <summary>
        /// 分公司ID
        /// </summary>
        public int? BranchID
        {
            set { _branchid = value; }
            get { return _branchid; }
        }
        /// <summary>
        /// 分公司代码
        /// </summary>
        public string BranchCode
        {
            set { _branchcode = value; }
            get { return _branchcode; }
        }
        /// <summary>
        /// 分公司名称
        /// </summary>
        public string BranchName
        {
            set { _branchname = value; }
            get { return _branchname; }
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
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 分公司对应的银行ID
        /// </summary>
        public int? BankID
        {
            set { _bankid = value; }
            get { return _bankid; }
        }
        /// <summary>
        /// 分公司对应的银行名
        /// </summary>
        public string BankName
        {
            set { _bankname = value; }
            get { return _bankname; }
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
        /// 银行帐号
        /// </summary>
        public string BankAccount
        {
            set { _bankaccount = value; }
            get { return _bankaccount; }
        }
        /// <summary>
        /// 银行电话
        /// </summary>
        public string PhoneNo
        {
            set { _phoneno = value; }
            get { return _phoneno; }
        }
        /// <summary>
        /// 交换行号
        /// </summary>
        public string ExchangeNo
        {
            set { _exchangeno = value; }
            get { return _exchangeno; }
        }
        /// <summary>
        /// 银行查询电话
        /// </summary>
        public string RequestPhone
        {
            set { _requestphone = value; }
            get { return _requestphone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? PaymentTypeID
        {
            set { _paymenttypeid = value; }
            get { return _paymenttypeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PaymentTypeName
        {
            set { _paymenttypename = value; }
            get { return _paymenttypename; }
        }
        /// <summary>
        /// 网银号或支票号
        /// </summary>
        public string PaymentTypeCode
        {
            set { _paymenttypecode = value; }
            get { return _paymenttypecode; }
        }

        string _bankAccountName;

        public string BankAccountName
        {
            get { return _bankAccountName; }
            set { _bankAccountName = value; }
        }
        string _bankAddress;

        public string BankAddress
        {
            get { return _bankAddress; }
            set { _bankAddress = value; }
        }
        #endregion Model

    }

}