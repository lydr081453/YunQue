using System;
namespace ESP.Media.Entity
{
	public class PaymentInfo
	{
		#region 构造函数
        public PaymentInfo()
		{
			id = 0;//id
			reporterid = 0;//reporterid
			propagatetype = 0;//Propagatetype
			progagateid = 0;//ProgagateID
			paymentdate = string.Empty;//PaymentDate
			paymentuserid = 0;//PaymentUserid
			paymentremark = string.Empty;//PaymentRemark
			paytype = 0;//支付类型
			payamount = 0.0D;//payamount
			financecode = string.Empty;//FinanceCode
			uploadstartdate = string.Empty;//UploadStartDate
			uploadenddate = string.Empty;//UploadEndDate
			del = 0;//del
			projectid = 0;//projectid
			paymentbillid = 0;//paymentbillid
		}
		#endregion


		#region 属性
		/// <summary>
		/// id
		/// </summary>
		private int id;
		public int Id
		{
			get
			{
				return id;
			}
			set
			{
				id = value ;
			}
		}


		/// <summary>
		/// reporterid
		/// </summary>
		private int reporterid;
		public int Reporterid
		{
			get
			{
				return reporterid;
			}
			set
			{
				reporterid = value ;
			}
		}


		/// <summary>
		/// Propagatetype
		/// </summary>
		private int propagatetype;
		public int Propagatetype
		{
			get
			{
				return propagatetype;
			}
			set
			{
				propagatetype = value ;
			}
		}


		/// <summary>
		/// ProgagateID
		/// </summary>
		private int progagateid;
		public int Progagateid
		{
			get
			{
				return progagateid;
			}
			set
			{
				progagateid = value ;
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
		/// 支付类型
		/// </summary>
		private int paytype;
		public int Paytype
		{
			get
			{
				return paytype;
			}
			set
			{
				paytype = value ;
			}
		}


		/// <summary>
		/// payamount
		/// </summary>
		private double payamount;
		public double Payamount
		{
			get
			{
				return payamount;
			}
			set
			{
				payamount = value ;
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
		/// UploadStartDate
		/// </summary>
		private string uploadstartdate;
		public string Uploadstartdate
		{
			get
			{
				return uploadstartdate;
			}
			set
			{
				uploadstartdate = value ;
			}
		}


		/// <summary>
		/// UploadEndDate
		/// </summary>
		private string uploadenddate;
		public string Uploadenddate
		{
			get
			{
				return uploadenddate;
			}
			set
			{
				uploadenddate = value ;
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


		#endregion
	}
}
