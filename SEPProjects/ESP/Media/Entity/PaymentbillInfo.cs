using System;
namespace ESP.Media.Entity
{
	public class PaymentbillInfo
	{
		#region 构造函数
        public PaymentbillInfo()
		{
			paymentbillid = 0;//paymentbillid
			projectid = 0;//projectid
			paymentdate = string.Empty;//PaymentDate
			paymentuserid = 0;//PaymentUserid
			paymentusername = string.Empty;//PaymentUsername
			paymentremark = string.Empty;//PaymentRemark
			del = 0;//del
			financecode = string.Empty;//FinanceCode
			status = 0;//Status
		}
		#endregion


		#region 属性
		/// <summary>
		/// paymentbillid
		/// </summary>
		private int paymentbillid;
		public int Paymentbillid
		{
			get
			{
				return paymentbillid;
			}
			set
			{
				paymentbillid = value ;
			}
		}


		/// <summary>
		/// projectid
		/// </summary>
		private int projectid;
		public int Projectid
		{
			get
			{
				return projectid;
			}
			set
			{
				projectid = value ;
			}
		}


		/// <summary>
		/// PaymentDate
		/// </summary>
		private string paymentdate;
		public string Paymentdate
		{
			get
			{
				return paymentdate;
			}
			set
			{
				paymentdate = value ;
			}
		}


		/// <summary>
		/// PaymentUserid
		/// </summary>
		private int paymentuserid;
		public int Paymentuserid
		{
			get
			{
				return paymentuserid;
			}
			set
			{
				paymentuserid = value ;
			}
		}


		/// <summary>
		/// PaymentUsername
		/// </summary>
		private string paymentusername;
		public string Paymentusername
		{
			get
			{
				return paymentusername;
			}
			set
			{
				paymentusername = value ;
			}
		}


		/// <summary>
		/// PaymentRemark
		/// </summary>
		private string paymentremark;
		public string Paymentremark
		{
			get
			{
				return paymentremark;
			}
			set
			{
				paymentremark = value ;
			}
		}


		/// <summary>
		/// del
		/// </summary>
		private int del;
		public int Del
		{
			get
			{
				return del;
			}
			set
			{
				del = value ;
			}
		}


		/// <summary>
		/// FinanceCode
		/// </summary>
		private string financecode;
		public string Financecode
		{
			get
			{
				return financecode;
			}
			set
			{
				financecode = value ;
			}
		}


		/// <summary>
		/// Status
		/// </summary>
		private int status;
		public int Status
		{
			get
			{
				return status;
			}
			set
			{
				status = value ;
			}
		}


		#endregion
	}
}
