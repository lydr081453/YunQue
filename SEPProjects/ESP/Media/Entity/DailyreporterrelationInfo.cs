using System;
namespace ESP.Media.Entity
{
	public class DailyreporterrelationInfo
	{
		#region ���캯��
        public DailyreporterrelationInfo()
		{
			id = 0;//id
			dailyid = 0;//DailyID
			reporterid = 0;//ReporterID
			relationdate = string.Empty;//RelationDate
			relationuserid = 0;//RelationUserID
			remark = string.Empty;//Remark
			del = 0;//Del
			paytype = 0;//PayType
			bankname = string.Empty;//bankname
			bankcardcode = string.Empty;//bankcardcode
			bankcardname = string.Empty;//bankcardname
			bankacountname = string.Empty;//bankacountname
			writingfee = 0.0D;//writingfee
			referral = string.Empty;//referral
			haveinvoice = 0;//haveInvoice
			paystatus = 0;//paystatus
			financecode = string.Empty;//FinanceCode
			uploadstarttime = string.Empty;//�����ϴ���ʼʱ��
			uploadendtime = string.Empty;//�����ϴ�����ʱ��
			paymentmode = 0;//���ʽ
			privateremark = string.Empty;//PrivateRemark
			cooperatecircs = string.Empty;//�������
		}
		#endregion


		#region ����
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
		/// DailyID
		/// </summary>
		private int dailyid;
		public int Dailyid
		{
			get
			{
				return dailyid;
			}
			set
			{
				dailyid = value ;
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
		/// RelationDate
		/// </summary>
		private string relationdate;
		public string Relationdate
		{
			get
			{
				return relationdate;
			}
			set
			{
				relationdate = value ;
			}
		}


		/// <summary>
		/// RelationUserID
		/// </summary>
		private int relationuserid;
		public int Relationuserid
		{
			get
			{
				return relationuserid;
			}
			set
			{
				relationuserid = value ;
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
		/// bankname
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
		/// bankcardcode
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
		/// bankcardname
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
		/// bankacountname
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
		/// referral
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
		/// �����ϴ���ʼʱ��
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
		/// �����ϴ�����ʱ��
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
		/// ���ʽ
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
		/// �������
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
