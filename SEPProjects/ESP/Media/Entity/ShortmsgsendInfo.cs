using System;
namespace ESP.Media.Entity
{
	public class ShortmsgsendInfo
	{
		#region 构造函数
        public ShortmsgsendInfo()
		{
			id = 0;//id
			shortmsgid = 0;//短信息id
			recvuserid = 0;//收信人id

			recvusertype = 0;//收信人类型
			recvphoneno = string.Empty;//recvphoneNO
			senduserid = 0;//发送人id

			senddate = string.Empty;//发送日期
			status = 0;//状态
			del = 0;//删除标记
			sendsubject = string.Empty;//sendSubject
			sendbody = string.Empty;//sendBody
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
		/// 短信息id
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
		/// 收信人id

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
		/// 收信人类型
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
		/// 发送人id

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


		#endregion
	}
}
