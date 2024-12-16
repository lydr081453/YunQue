using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类ReturnInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ReturnInfo
    {
        public ReturnInfo()
        { }
        #region Model
        private int _returnid;
        private decimal? _prefee;
        private decimal? _factfee;
        private decimal? _deferfee;
        private int? _deferday;
        private int? _returnstatus;
        private int? _isinvoice;
        private string _invoiceno;
        private DateTime? _invoicedate;
        private int? _branchid;
        private string _branchcode;
        private string _returncode;
        private string _branchname;
        private byte[] _lastupdatetime;
        private int? _purchasepayid;
        private int? _prid;
        private string _prno;
        private int? _paymentuserid;
        private string _paymentcode;
        private string _paymentemployeename;
        private string _paymentusername;
        private int? _requestorid;
        private int? _projectid;
        private string _requestusercode;
        private string _requestusername;
        private string _requestemployeename;
        private DateTime? _requestdate;
        private int? _workitemid;
        private string _workitemname;
        private int? _processid;
        private int? _instanceid;
        private string _attachment;
        private int? _bankid;
        private string _projectcode;
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
        private DateTime? _returnpredate;
        private DateTime? _prebegindate;
        private DateTime? _preenddate;
        private DateTime? _returnfactdate;
        private string _returncontent;
        private string _bankAccountName;
        private string _bankAddress;
        private int? _returnType;
        private DateTime? _lastUpdateDateTime;
        private string _mediaorderids;
        private string _SupplierName;
        private string _RePaymentSuggestion;
        private int? _FinanceHoldStatus;
        private string _CreditCode;
        private bool _NeedPurchaseAudit;
        private int? _DepartmentID;
        private string _DepartmentName;
        private string _remark;
        //private int? _CostDetailID;
        public string RecipientIds { get;set;}
        private DateTime? _commitDate;
        private int? _parentID;
        private DateTime? _lastAuditPassTime;
        private DateTime? _faAuditPassTime;

        private string _LastAuditUserName;
        private int? _LastAuditUserID;
        private string _FaAuditUserName;
        private int? _FaAuditUserID;

        public bool? IsDiscount { get; set;}
        public DateTime? DiscountDate { get; set; }
        public int TicketNo { get; set; }
        public int TicketSupplierId { get; set; }
        public int ReceptionId { get; set; }
        /// <summary>
        /// 如果PN是支票电汇，选择是否是限额支票
        /// </summary>
        public bool? IsFixCheque { get; set; }
        public string LastAuditUserName
        {
            get { return _LastAuditUserName; }
            set { _LastAuditUserName = value; }
        }

        public string FaAuditUserName
        {
            get { return _FaAuditUserName; }
            set { _FaAuditUserName = value; }
        }

        public int? LastAuditUserID
        {
            get { return _LastAuditUserID; }
            set { _LastAuditUserID = value; }
        }

        public int? FaAuditUserID
        {
            get { return _FaAuditUserID; }
            set { _FaAuditUserID = value; }
        }

        public DateTime? LastAuditPassTime
        {
            get { return _lastAuditPassTime; }
            set { _lastAuditPassTime = value; }
        }

        public DateTime? FaAuditPassTime
        {
            get { return _faAuditPassTime; }
            set { _faAuditPassTime = value; }
        }

        public DateTime? CommitDate
        {
            get { return _commitDate; }
            set { _commitDate = value; }
        }

        public int? ParentID
        {
            get { return _parentID; }
            set { _parentID = value; }
        }

        ///// <summary>
        ///// 成本明细ID
        ///// </summary>
        //public int? CostDetailID
        //{
        //    get { return _CostDetailID; }
        //    set { _CostDetailID = value; }
        //}

        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        /// <summary>
        /// 成本所属组ID
        /// </summary>
        public int? DepartmentID
        {
            get { return _DepartmentID; }
            set { _DepartmentID = value; }
        }

        /// <summary>
        /// 成本所属组名称
        /// </summary>
        public string DepartmentName
        {
            get { return _DepartmentName; }
            set { _DepartmentName = value; }
        }

        public string CreditCode
        {
            get { return _CreditCode; }
            set { _CreditCode = value; }
        }
        public int? FinanceHoldStatus
        {
         get{return _FinanceHoldStatus;}
            set{_FinanceHoldStatus=value;}
        }
        public string RePaymentSuggestion
        {
            get { return _RePaymentSuggestion; }
            set { _RePaymentSuggestion = value; }
        }

        public string SupplierName
        {
            get { return _SupplierName; }
            set { _SupplierName = value; }
        }
        private string _SupplierBankName;

        public string SupplierBankName
        {
            get { return _SupplierBankName; }
            set { _SupplierBankName = value; }
        }
        private string _SupplierBankAccount;

        public string SupplierBankAccount
        {
            get { return _SupplierBankAccount; }
            set { _SupplierBankAccount = value; }
        }

        public string MediaOrderIDs
        {
            get { return _mediaorderids; }
            set { _mediaorderids = value; }
        }

        public DateTime? LastUpdateDateTime
        {
            get { return _lastUpdateDateTime; }
            set { _lastUpdateDateTime = value; }
        }

        public int? ReturnType
        {
            get { return _returnType; }
            set { _returnType = value; }
        }

        public string BankAccountName
        {
            get { return _bankAccountName; }
            set { _bankAccountName = value; }
        }
        public string BankAddress
        {
            get { return _bankAddress; }
            set { _bankAddress = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ReturnID
        {
            set { _returnid = value; }
            get { return _returnid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? PreFee
        {
            set { _prefee = value; }
            get { return _prefee; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? FactFee
        {
            set { _factfee = value; }
            get { return _factfee; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? DeferFee
        {
            set { _deferfee = value; }
            get { return _deferfee; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DeferDay
        {
            set { _deferday = value; }
            get { return _deferday; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ReturnStatus
        {
            set { _returnstatus = value; }
            get { return _returnstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsInvoice
        {
            set { _isinvoice = value; }
            get { return _isinvoice; }
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
        public DateTime? InvoiceDate
        {
            set { _invoicedate = value; }
            get { return _invoicedate; }
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
        /// 
        /// </summary>
        public string ReturnCode
        {
            set { _returncode = value; }
            get { return _returncode; }
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
        public int? PurchasePayID
        {
            set { _purchasepayid = value; }
            get { return _purchasepayid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? PRID
        {
            set { _prid = value; }
            get { return _prid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PRNo
        {
            set { _prno = value; }
            get { return _prno; }
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
        public string PaymentCode
        {
            set { _paymentcode = value; }
            get { return _paymentcode; }
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
        public int? RequestorID
        {
            set { _requestorid = value; }
            get { return _requestorid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RequestUserCode
        {
            set { _requestusercode = value; }
            get { return _requestusercode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RequestUserName
        {
            set { _requestusername = value; }
            get { return _requestusername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RequestEmployeeName
        {
            set { _requestemployeename = value; }
            get { return _requestemployeename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? RequestDate
        {
            set { _requestdate = value; }
            get { return _requestdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? WorkItemID
        {
            set { _workitemid = value; }
            get { return _workitemid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WorkItemName
        {
            set { _workitemname = value; }
            get { return _workitemname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ProcessID
        {
            set { _processid = value; }
            get { return _processid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? InstanceID
        {
            set { _instanceid = value; }
            get { return _instanceid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Attachment
        {
            set { _attachment = value; }
            get { return _attachment; }
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
        /// 
        /// </summary>
        public string ProjectCode
        {
            set { _projectcode = value; }
            get { return _projectcode; }
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
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ReturnPreDate
        {
            set { _returnpredate = value; }
            get { return _returnpredate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? PreBeginDate
        {
            set { _prebegindate = value; }
            get { return _prebegindate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? PreEndDate
        {
            set { _preenddate = value; }
            get { return _preenddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ReturnFactDate
        {
            set { _returnfactdate = value; }
            get { return _returnfactdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReturnContent
        {
            set { _returncontent = value; }
            get { return _returncontent; }
        }

        /// <summary>
        /// 是否需要采购部审批
        /// </summary>
        public bool NeedPurchaseAudit
        {
            get { return _NeedPurchaseAudit; }
            set { _NeedPurchaseAudit = value; }
        }

        public DateTime? ReciptionDate { get; set; }
        /// <summary>
        /// 媒体预付申请
        /// </summary>
        public int IsMediaOrder { get; set; }
        #endregion Model

    }
}