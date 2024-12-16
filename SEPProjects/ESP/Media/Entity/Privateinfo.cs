using System;
namespace ESP.Media.Entity
{
	public class Privateinfo
	{
		#region ���캯��
        public Privateinfo()
		{
			id = 0;//id
			reporterid = 0;//ReporterID
			remark = string.Empty;//Remark
			del = 0;//Del
			paytype = 0;//PayType
			bankname = string.Empty;//����������
			bankcardcode = string.Empty;//�ʺ�
			bankcardname = string.Empty;//���п�����
			bankacountname = string.Empty;//��������
			writingfee = 0.0D;//writingfee
			referral = string.Empty;//�ο�
			haveinvoice = 0;//haveInvoice
			paystatus = 0;//paystatus
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
		/// �ʺ�
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
		/// ���п�����
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
		/// ��������
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


		#endregion
	}
}
