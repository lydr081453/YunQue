using System;
namespace ESP.Media.Entity
{
	public class ShortmsgInfo
	{
		#region 构造函数
        public ShortmsgInfo()
		{
			id = 0;//id
			subject = string.Empty;//主题
			body = string.Empty;//内容

			createid = 0;//创建id

			createdate = string.Empty;//创建日期
			senddate = string.Empty;//发送时间
			del = 0;//删除标记
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
		/// 主题
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
		/// 内容

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
		/// 创建id

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
		/// 创建日期
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
		/// 发送时间
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


		#endregion
	}
}
