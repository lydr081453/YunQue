using System;
namespace ESP.Media.Entity
{
	public class ShortmsgsendInfo
	{
		#region ���캯��
        public ShortmsgsendInfo()
		{
			id = 0;//id
			shortmsgid = 0;//����Ϣid
			recvuserid = 0;//������id

			recvusertype = 0;//����������
			recvphoneno = string.Empty;//recvphoneNO
			senduserid = 0;//������id

			senddate = string.Empty;//��������
			status = 0;//״̬
			del = 0;//ɾ�����
			sendsubject = string.Empty;//sendSubject
			sendbody = string.Empty;//sendBody
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
		/// ����Ϣid
		/// </summary>
		private int shortmsgid;
		public int Shortmsgid
		{
			get
			{
				return shortmsgid;
			}
			set
			{
				shortmsgid = value ;
			}
		}


		/// <summary>
		/// ������id

		/// </summary>
		private int recvuserid;
		public int Recvuserid
		{
			get
			{
				return recvuserid;
			}
			set
			{
				recvuserid = value ;
			}
		}


		/// <summary>
		/// ����������
		/// </summary>
		private int recvusertype;
		public int Recvusertype
		{
			get
			{
				return recvusertype;
			}
			set
			{
				recvusertype = value ;
			}
		}


		/// <summary>
		/// recvphoneNO
		/// </summary>
		private string recvphoneno;
		public string Recvphoneno
		{
			get
			{
				return recvphoneno;
			}
			set
			{
				recvphoneno = value ;
			}
		}


		/// <summary>
		/// ������id

		/// </summary>
		private int senduserid;
		public int Senduserid
		{
			get
			{
				return senduserid;
			}
			set
			{
				senduserid = value ;
			}
		}


		/// <summary>
		/// ��������
		/// </summary>
		private string senddate;
		public string Senddate
		{
			get
			{
				return senddate;
			}
			set
			{
				senddate = value ;
			}
		}


		/// <summary>
		/// ״̬
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


		/// <summary>
		/// ɾ�����
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
		/// sendSubject
		/// </summary>
		private string sendsubject;
		public string Sendsubject
		{
			get
			{
				return sendsubject;
			}
			set
			{
				sendsubject = value ;
			}
		}


		/// <summary>
		/// sendBody
		/// </summary>
		private string sendbody;
		public string Sendbody
		{
			get
			{
				return sendbody;
			}
			set
			{
				sendbody = value ;
			}
		}


		#endregion
	}
}
