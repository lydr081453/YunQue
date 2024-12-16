using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
	/// <summary>
	/// 付款申请银行信息不对，驳回重汇
	/// </summary>
	public class BankCancelInfo
	{
        public BankCancelInfo()
		{}
		#region Model
		private int _logid;
		private string _oldbankname;
		private string _newbankaccountname;
		private string _newbankaccount;
		private string _newbankname;
		private DateTime? _canceldate;
		private DateTime? _commitdate;
		private DateTime? _lastupdatetime;
		private int? _integral;
		private int? _operationid;
		private string _operationcode;
		private int? _returnid;
		private string _operationname;
		private string _operationempname;
		private int? _requestorid;
		private string _requestorcode;
		private string _requestorname;
		private string _requestorempname;
		private string _returncode;
		private string _oldbankaccountname;
		private string _oldbankaccount;
        private string _RePaymentSuggestion;

        public int OrderType { get; set; }
        public string RePaymentSuggestion
        {
            get { return _RePaymentSuggestion; }
            set { _RePaymentSuggestion = value; }
        }
		/// <summary>
		/// 
		/// </summary>
		public int LogID
		{
			set{ _logid=value;}
			get{return _logid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OldBankName
		{
			set{ _oldbankname=value;}
			get{return _oldbankname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NewBankAccountName
		{
			set{ _newbankaccountname=value;}
			get{return _newbankaccountname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NewBankAccount
		{
			set{ _newbankaccount=value;}
			get{return _newbankaccount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NewBankName
		{
			set{ _newbankname=value;}
			get{return _newbankname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CancelDate
		{
			set{ _canceldate=value;}
			get{return _canceldate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CommitDate
		{
			set{ _commitdate=value;}
			get{return _commitdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime
		{
			set{ _lastupdatetime=value;}
			get{return _lastupdatetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Integral
		{
			set{ _integral=value;}
			get{return _integral;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? OperationID
		{
			set{ _operationid=value;}
			get{return _operationid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperationCode
		{
			set{ _operationcode=value;}
			get{return _operationcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ReturnID
		{
			set{ _returnid=value;}
			get{return _returnid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperationName
		{
			set{ _operationname=value;}
			get{return _operationname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperationEmpName
		{
			set{ _operationempname=value;}
			get{return _operationempname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? RequestorID
		{
			set{ _requestorid=value;}
			get{return _requestorid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RequestorCode
		{
			set{ _requestorcode=value;}
			get{return _requestorcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RequestorName
		{
			set{ _requestorname=value;}
			get{return _requestorname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RequestorEmpName
		{
			set{ _requestorempname=value;}
			get{return _requestorempname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReturnCode
		{
			set{ _returncode=value;}
			get{return _returncode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OldBankAccountName
		{
			set{ _oldbankaccountname=value;}
			get{return _oldbankaccountname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OldBankAccount
		{
			set{ _oldbankaccount=value;}
			get{return _oldbankaccount;}
		}
		#endregion Model

	}
}
