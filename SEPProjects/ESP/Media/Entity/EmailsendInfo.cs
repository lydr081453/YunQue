using System;
namespace ESP.Media.Entity
{
	public class EmailsendInfo
	{
		#region ���캯��
        public EmailsendInfo()
		{
			id = 0;//id
			emailid = 0;//Email��ID
			recvuserid = 0;//������id

			recvusertype = 0;//����������

			recvaddress = string.Empty;//���յ�ַ

			senduserid = 0;//���͵�ַ

			senddate = string.Empty;//��������

			status = 0;//״̬

			del = 0;//ɾ�����
			sendsubject = string.Empty;//sendSubject
			sendbody = string.Empty;//sendBody
			sendattachmentspath = string.Empty;//sendAttachmentsPath
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
		/// Email��ID
		/// </summary>
		private int emailid;
		public int Emailid
		{
			get
			{
				return emailid;
			}
			set
			{
				emailid = value ;
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
		/// ���յ�ַ

		/// </summary>
		private string recvaddress;
		public string Recvaddress
		{
			get
			{
				return recvaddress;
			}
			set
			{
				recvaddress = value ;
			}
		}


		/// <summary>
		/// ���͵�ַ

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


		/// <summary>
		/// sendAttachmentsPath
		/// </summary>
		private string sendattachmentspath;
		public string Sendattachmentspath
		{
			get
			{
				return sendattachmentspath;
			}
			set
			{
				sendattachmentspath = value ;
			}
		}


		#endregion
	}
}
