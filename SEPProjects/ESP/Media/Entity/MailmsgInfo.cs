using System;
namespace ESP.Media.Entity
{
	public class MailmsgInfo
	{
		#region ���캯��
        public MailmsgInfo()
		{
			id = 0;//id
			subject = string.Empty;//����

			body = string.Empty;//����
			createid = 0;//����id

			createdate = string.Empty;//����ʱ��

			senddate = string.Empty;//����ʱ��

			attachmentspath = string.Empty;//AttachmentsPath
			del = 0;//ɾ�����
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
		/// ����

		/// </summary>
		private string subject;
		public string Subject
		{
			get
			{
				return subject;
			}
			set
			{
				subject = value ;
			}
		}


		/// <summary>
		/// ����
		/// </summary>
		private string body;
		public string Body
		{
			get
			{
				return body;
			}
			set
			{
				body = value ;
			}
		}


		/// <summary>
		/// ����id

		/// </summary>
		private int createid;
		public int Createid
		{
			get
			{
				return createid;
			}
			set
			{
				createid = value ;
			}
		}


		/// <summary>
		/// ����ʱ��

		/// </summary>
		private string createdate;
		public string Createdate
		{
			get
			{
				return createdate;
			}
			set
			{
				createdate = value ;
			}
		}


		/// <summary>
		/// ����ʱ��

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
		/// AttachmentsPath
		/// </summary>
		private string attachmentspath;
		public string Attachmentspath
		{
			get
			{
				return attachmentspath;
			}
			set
			{
				attachmentspath = value ;
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


		#endregion
	}
}