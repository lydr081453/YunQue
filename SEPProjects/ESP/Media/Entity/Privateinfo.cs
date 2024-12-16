using System;
namespace ESP.Media.Entity
{
	public class Privateinfo
	{
		#region 构造函数
        public Privateinfo()
		{
			id = 0;//id
			reporterid = 0;//ReporterID
			remark = string.Empty;//Remark
			del = 0;//Del
			paytype = 0;//PayType
			bankname = string.Empty;//开户行名称
			bankcardcode = string.Empty;//帐号
			bankcardname = string.Empty;//银行卡名称
			bankacountname = string.Empty;//开户名称
			writingfee = 0.0D;//writingfee
			referral = string.Empty;//参考
			haveinvoice = 0;//haveInvoice
			paystatus = 0;//paystatus
			uploadstarttime = string.Empty;//剪报上传起始时间
			uploadendtime = string.Empty;//剪报上传结束时间
			paymentmode = 0;//付款方式
			privateremark = string.Empty;//PrivateRemark
			cooperatecircs = string.Empty;//合作情况
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
		/// ReporterID
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
		/// Remark
		/// </summary>
		private string remark;
		public string Remark
		{
			get
			{
				return remark;
			}
			set
			{
				remark = value ;
			}
		}


		/// <summary>
		/// Del
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
		/// PayType
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
		/// 开户行名称
		/// </summary>
		private string bankname;
		public string Bankname
		{
			get
			{
				return bankname;
			}
			set
			{
				bankname = value ;
			}
		}


		/// <summary>
		/// 帐号
		/// </summary>
		private string bankcardcode;
		public string Bankcardcode
		{
			get
			{
				return bankcardcode;
			}
			set
			{
				bankcardcode = value ;
			}
		}


		/// <summary>
		/// 银行卡名称
		/// </summary>
		private string bankcardname;
		public string Bankcardname
		{
			get
			{
				return bankcardname;
			}
			set
			{
				bankcardname = value ;
			}
		}


		/// <summary>
		/// 开户名称
		/// </summary>
		private string bankacountname;
		public string Bankacountname
		{
			get
			{
				return bankacountname;
			}
			set
			{
				bankacountname = value ;
			}
		}


		/// <summary>
		/// writingfee
		/// </summary>
		private double writingfee;
		public double Writingfee
		{
			get
			{
				return writingfee;
			}
			set
			{
				writingfee = value ;
			}
		}


		/// <summary>
		/// 参考
		/// </summary>
		private string referral;
		public string Referral
		{
			get
			{
				return referral;
			}
			set
			{
				referral = value ;
			}
		}


		/// <summary>
		/// haveInvoice
		/// </summary>
		private int haveinvoice;
		public int Haveinvoice
		{
			get
			{
				return haveinvoice;
			}
			set
			{
				haveinvoice = value ;
			}
		}


		/// <summary>
		/// paystatus
		/// </summary>
		private int paystatus;
		public int Paystatus
		{
			get
			{
				return paystatus;
			}
			set
			{
				paystatus = value ;
			}
		}


		/// <summary>
		/// 剪报上传起始时间
		/// </summary>
		private string uploadstarttime;
		public string Uploadstarttime
		{
			get
			{
				return uploadstarttime;
			}
			set
			{
				uploadstarttime = value ;
			}
		}


		/// <summary>
		/// 剪报上传结束时间
		/// </summary>
		private string uploadendtime;
		public string Uploadendtime
		{
			get
			{
				return uploadendtime;
			}
			set
			{
				uploadendtime = value ;
			}
		}


		/// <summary>
		/// 付款方式
		/// </summary>
		private int paymentmode;
		public int Paymentmode
		{
			get
			{
				return paymentmode;
			}
			set
			{
				paymentmode = value ;
			}
		}


		/// <summary>
		/// PrivateRemark
		/// </summary>
		private string privateremark;
		public string Privateremark
		{
			get
			{
				return privateremark;
			}
			set
			{
				privateremark = value ;
			}
		}


		/// <summary>
		/// 合作情况
		/// </summary>
		private string cooperatecircs;
		public string Cooperatecircs
		{
			get
			{
				return cooperatecircs;
			}
			set
			{
				cooperatecircs = value ;
			}
		}


		#endregion
	}
}
