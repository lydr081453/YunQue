using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    public class GetAllPaymentViewInfo
    {
		public GetAllPaymentViewInfo()
		{}
		#region Model
		private int _id;
		private string _orderid;
		private string _first_assessorname;
		private int? _status;
		private string _thirdparty_materieldesc;
		private string _filiale_auditname;
		private int? _departmentid;
		private string _department;
		private int? _requisitionflow;
		private decimal? _totalprice;
		private int? _prtype;
		private string _prno;
		private string _account_name;
		private string _account_bank;
			private string _account_number;
		private int? _operationtype;
		private int _pid;
		private int? _periodtype;
		private decimal? _expectpaymentprice;
		private decimal? _inceptprice;
		private int? _paymentstatus;
		private int? _returnid;
		private int? _requestor;
		private string _returncode;
		private string _returncontent;
		private decimal? _prefee;
		private decimal? _factfee;
		private int? _returnstatus;
		private int? _isinvoice;
		private string _moneytype;
		private string _requestorname;
		private string _project_code;
		private int? _project_id;
		private string _project_descripttion;
		private string _supplier_name;
		private string _source;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string orderid
		{
			set{ _orderid=value;}
			get{return _orderid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string first_assessorname
		{
			set{ _first_assessorname=value;}
			get{return _first_assessorname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string thirdParty_materielDesc
		{
			set{ _thirdparty_materieldesc=value;}
			get{return _thirdparty_materieldesc;}
		}
			/// <summary>
		/// 
		/// </summary>
		public string Filiale_AuditName
		{
			set{ _filiale_auditname=value;}
			get{return _filiale_auditname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DepartmentId
		{
			set{ _departmentid=value;}
			get{return _departmentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Department
		{
			set{ _department=value;}
			get{return _department;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? requisitionflow
		{
			set{ _requisitionflow=value;}
			get{return _requisitionflow;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? totalprice
		{
			set{ _totalprice=value;}
			get{return _totalprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PRType
		{
			set{ _prtype=value;}
			get{return _prtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string prNo
		{
			set{ _prno=value;}
			get{return _prno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string account_name
		{
			set{ _account_name=value;}
			get{return _account_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string account_bank
		{
			set{ _account_bank=value;}
			get{return _account_bank;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string account_number
		{
			set{ _account_number=value;}
			get{return _account_number;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? OperationType
		{
			set{ _operationtype=value;}
			get{return _operationtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int pid
		{
			set{ _pid=value;}
			get{return _pid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? periodType
		{
			set{ _periodtype=value;}
			get{return _periodtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? expectPaymentPrice
		{
			set{ _expectpaymentprice=value;}
			get{return _expectpaymentprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? inceptPrice
		{
			set{ _inceptprice=value;}
			get{return _inceptprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? paymentstatus
		{
			set{ _paymentstatus=value;}
			get{return _paymentstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ReturnId
		{
			set{ _returnid=value;}
			get{return _returnid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? requestor
		{
			set{ _requestor=value;}
			get{return _requestor;}
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
		public string ReturnContent
		{
			set{ _returncontent=value;}
			get{return _returncontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? PreFee
		{
			set{ _prefee=value;}
			get{return _prefee;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? FactFee
		{
			set{ _factfee=value;}
			get{return _factfee;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ReturnStatus
		{
			set{ _returnstatus=value;}
			get{return _returnstatus;}
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
		public string moneytype
		{
			set{ _moneytype=value;}
			get{return _moneytype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string requestorname
		{
			set{ _requestorname=value;}
			get{return _requestorname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string project_code
		{
			set{ _project_code=value;}
			get{return _project_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? project_id
		{
			set{ _project_id=value;}
			get{return _project_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string project_descripttion
		{
			set{ _project_descripttion=value;}
			get{return _project_descripttion;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string supplier_name
		{
			set{ _supplier_name=value;}
			get{return _supplier_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string source
		{
			set{ _source=value;}
			get{return _source;}
		}
		#endregion Model

	}
}