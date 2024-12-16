using System;
namespace ESP.Media.Entity
{
	public class EmailsendInfo
	{
		#region 构造函数
        public EmailsendInfo()
		{
			id = 0;//id
			emailid = 0;//Email的ID
			recvuserid = 0;//接收人id

			recvusertype = 0;//接收人类型

			recvaddress = string.Empty;//接收地址

			senduserid = 0;//发送地址

			senddate = string.Empty;//发送日期

			status = 0;//状态

			del = 0;//删除标记
			sendsubject = string.Empty;//sendSubject
			sendbody = string.Empty;//sendBody
			sendattachmentspath = string.Empty;//sendAttachmentsPath
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
		/// Email的ID
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
		/// 接收人id

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
		/// 接收人类型

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
		/// 接收地址

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
		/// 发送地址

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
		/// 发送日期

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
		/// 状态

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
		/// 删除标记
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
