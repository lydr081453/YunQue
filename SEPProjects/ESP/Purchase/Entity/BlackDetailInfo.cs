using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.Entity
{
   public  class BlackDetailInfo
    {
       public BlackDetailInfo()
		{}
		#region Model
		private int _blackdetailid;
		private int? _auditerid;
		private string _auditername;
		private DateTime? _audittime;
		private int? _orderid;
		private int? _ordertype;
		private DateTime? _sendmailtime;
		private string _senderid;
		private string _sendername;
		private DateTime? _responsetime;
		private string _responsecontent;
		private int? _status;
		/// <summary>
		/// 
		/// </summary>
		public int BlackDetailID
		{
			set{ _blackdetailid=value;}
			get{return _blackdetailid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? AuditerID
		{
			set{ _auditerid=value;}
			get{return _auditerid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AuditerName
		{
			set{ _auditername=value;}
			get{return _auditername;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? AuditTime
		{
			set{ _audittime=value;}
			get{return _audittime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? OrderID
		{
			set{ _orderid=value;}
			get{return _orderid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? OrderType
		{
			set{ _ordertype=value;}
			get{return _ordertype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? SendMailTime
		{
			set{ _sendmailtime=value;}
			get{return _sendmailtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SenderID
		{
			set{ _senderid=value;}
			get{return _senderid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SenderName
		{
			set{ _sendername=value;}
			get{return _sendername;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ResponseTime
		{
			set{ _responsetime=value;}
			get{return _responsetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ResponseContent
		{
			set{ _responsecontent=value;}
			get{return _responsecontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		#endregion Model
    }
}
