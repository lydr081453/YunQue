using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    public class PNBatchInfo
    {
        public PNBatchInfo()
		{}
		#region Model
        private string _createYearMonth;
        /// <summary>
        /// 批次创建年月
        /// </summary>
        public string CreateYearMonth
        {
            get { return _createYearMonth; }
            set { _createYearMonth = value; }
        }

		private int _batchid;
		private string _description;
		private string _batchcode;
		private decimal? _amounts;
		private int? _status;
		private int? _paymentuserid;
		private string _paymentcode;
		private string _paymentemployeename;
		private string _paymentusername;
		private string _supplierbankname;
		private int? _pnid;
		private string _supplierbankaccount;
		private int? _isinvoice;
		private string _invoiceno;
		private DateTime? _invoicedate;
		private DateTime? _lastupdatetime;
		private int? _bankid;
		private string _bankname;
		private string _dbcode;
		private string _dbmanager;
		private string _bankaccount;
		private int? _prid;
		private string _phoneno;
		private string _exchangeno;
		private string _requestphone;
		private string _bankaccountname;
		private string _bankaddress;
		private int? _periodid;
		private DateTime? _createdate;
		private int? _creatorid;
		private string _suppliername;
		private string _branchcode;
        private string _PaymentType;
        private string _PurchaseBatchCode;
        private decimal _total;
        private int? _batchType;

        /// <summary>
        /// 消耗导入证明附件
        /// </summary>
        public string ProveFile { get; set; }

        public decimal TicketReturnPoint { get; set; }
        /// <summary>
        /// 批次类型 1为PN批次  2为报效批次
        /// </summary>
        public int? BatchType
        {
            get { return _batchType; }
            set { _batchType = value; }
        }

        /// <summary>
        /// 不存在有效值
        /// </summary>
        public decimal Total
        {
            get { return _total; }
            set { _total = value; }
        }
        private int _pnCount;
        /// <summary>
        /// 不存在有效值
        /// </summary>
        public int PnCount
        {
            get { return _pnCount; }
            set { _pnCount = value; }
        }

        public string PurchaseBatchCode
        {
            get { return _PurchaseBatchCode; }
            set { _PurchaseBatchCode = value; }
        }
        public string PaymentType
        {
            get { return _PaymentType; }
            set { _PaymentType = value; }
        }
        private DateTime? _PaymentDate;

        public DateTime? PaymentDate
        {
            get { return _PaymentDate; }
            set { _PaymentDate = value; }
        }
        private int? _BranchID;

        public int? BranchID
        {
            get { return _BranchID; }
            set { _BranchID = value; }
        }
        private int? _PaymentTypeID;

        public int? PaymentTypeID
        {
            get { return _PaymentTypeID; }
            set { _PaymentTypeID = value; }
        }

        private List<ESP.Finance.Entity.ReturnInfo> _ReturnList;

        public List<ESP.Finance.Entity.ReturnInfo> ReturnList
        {
            get { return _ReturnList; }
            set { _ReturnList = value; }
        }

		/// <summary>
		/// 
		/// </summary>
		public int BatchID
		{
			set{ _batchid=value;}
			get{return _batchid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BatchCode
		{
			set{ _batchcode=value;}
			get{return _batchcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Amounts
		{
			set{ _amounts=value;}
			get{return _amounts;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PaymentUserID
		{
			set{ _paymentuserid=value;}
			get{return _paymentuserid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PaymentCode
		{
			set{ _paymentcode=value;}
			get{return _paymentcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PaymentEmployeeName
		{
			set{ _paymentemployeename=value;}
			get{return _paymentemployeename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PaymentUserName
		{
			set{ _paymentusername=value;}
			get{return _paymentusername;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SupplierBankName
		{
			set{ _supplierbankname=value;}
			get{return _supplierbankname;}
		}
		/// <summary>
		/// 财务付款申请ID
		/// </summary>
		public int? PNID
		{
			set{ _pnid=value;}
			get{return _pnid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SupplierBankAccount
		{
			set{ _supplierbankaccount=value;}
			get{return _supplierbankaccount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsInvoice
		{
			set{ _isinvoice=value;}
			get{return _isinvoice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string InvoiceNo
		{
			set{ _invoiceno=value;}
			get{return _invoiceno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? InvoiceDate
		{
			set{ _invoicedate=value;}
			get{return _invoicedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Lastupdatetime
		{
			set{ _lastupdatetime=value;}
			get{return _lastupdatetime;}
		}
		/// <summary>
		/// 分公司对应的银行ID
		/// </summary>
		public int? BankID
		{
			set{ _bankid=value;}
			get{return _bankid;}
		}
		/// <summary>
		/// 分公司对应的银行名
		/// </summary>
		public string BankName
		{
			set{ _bankname=value;}
			get{return _bankname;}
		}
		/// <summary>
		/// 数据库代码
		/// </summary>
		public string DBCode
		{
			set{ _dbcode=value;}
			get{return _dbcode;}
		}
		/// <summary>
		/// 管理数据库
		/// </summary>
		public string DBManager
		{
			set{ _dbmanager=value;}
			get{return _dbmanager;}
		}
		/// <summary>
		/// 银行帐号
		/// </summary>
		public string BankAccount
		{
			set{ _bankaccount=value;}
			get{return _bankaccount;}
		}
		/// <summary>
		/// 采购PR单ID
		/// </summary>
		public int? PRID
		{
			set{ _prid=value;}
			get{return _prid;}
		}
		/// <summary>
		/// 银行电话
		/// </summary>
		public string PhoneNo
		{
			set{ _phoneno=value;}
			get{return _phoneno;}
		}
		/// <summary>
		/// 交换行号
		/// </summary>
		public string ExchangeNo
		{
			set{ _exchangeno=value;}
			get{return _exchangeno;}
		}
		/// <summary>
		/// 银行查询电话
		/// </summary>
		public string RequestPhone
		{
			set{ _requestphone=value;}
			get{return _requestphone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BankAccountName
		{
			set{ _bankaccountname=value;}
			get{return _bankaccountname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BankAddress
		{
			set{ _bankaddress=value;}
			get{return _bankaddress;}
		}
		/// <summary>
		/// 账期ID改为判断是否是消耗调整
		/// </summary>
		public int? PeriodID
		{
			set{ _periodid=value;}
			get{return _periodid;}
		}
		/// <summary>
		/// 创建日期
		/// </summary>
		public DateTime? CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 创建人ID
		/// </summary>
		public int? CreatorID
		{
			set{ _creatorid=value;}
			get{return _creatorid;}
		}
		/// <summary>
		/// 供应商名称
		/// </summary>
		public string SupplierName
		{
			set{ _suppliername=value;}
			get{return _suppliername;}
		}
		/// <summary>
		/// 分公司代码
		/// </summary>
		public string BranchCode
		{
			set{ _branchcode=value;}
			get{return _branchcode;}
		}
        public string Creator { get; set; }
		#endregion Model

    }
}