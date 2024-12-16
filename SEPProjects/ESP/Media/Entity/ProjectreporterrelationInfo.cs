using System;
namespace ESP.Media.Entity
{
	public class ProjectreporterrelationInfo
	{
		#region ���캯��
        public ProjectreporterrelationInfo()
		{
			id = 0;//id
			projectid = 0;//ProjectID
			reporterid = 0;//ReporterID
			relationdate = string.Empty;//RelationDate
			relationuserid = 0;//RelationUserID
			privateinfoid = 0;//˽����Ϣid
			remark = string.Empty;//Remark
			del = 0;//Del
			paytype = 0;//PayType
			bankname = string.Empty;//����������
			bankcardcode = string.Empty;//���п���(�ʺ�)
			bankcardname = string.Empty;//���п���
			bankacountname = string.Empty;//�ʻ�����
			writingfee = 0.0D;//writingfee
			referral = string.Empty;//�ο�
			haveinvoice = 0;//haveInvoice
			paystatus = 0;//paystatus
			uploadstarttime = string.Empty;//�����ϴ���ʼʱ��
			uploadendtime = string.Empty;//�����ϴ�����ʱ��
			paymentmode = 0;//���ʽ
			privateremark = string.Empty;//PrivateRemark
			cooperatecircs = string.Empty;//�������
			financecode = string.Empty;//FinanceCode
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
		/// ProjectID
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
		/// ˽����Ϣid
		/// </summary>
		private int privateinfoid;
		public int Privateinfoid
		{
			get
			{
				return privateinfoid;
			}
			set
			{
				privateinfoid = value ;
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
		/// ����������
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
		/// ���п���(�ʺ�)
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
		/// ���п���
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
		/// �ʻ�����
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
		/// �ο�
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


		#endregion
	}
}
