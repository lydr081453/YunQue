using System;
namespace ESP.Media.Entity
{
	public class PostsInfo
	{
		#region 构造函数
        public PostsInfo()
		{
			id = 0;//id
			parentid = 0;//上级id
			no = 0;//编号

			type = 0;//类型
			issysmsg = 0;//是否是系统消息
			issuedate = string.Empty;//发布时间
			userid = 0;//用户id

			subject = string.Empty;//主题

			body = string.Empty;//内容

			lastreplyuserid = 0;//最后回复id

			lastreplydate = string.Empty;//最后回复时间
			del = 0;//删除标记
			begindate = string.Empty;//beginDate
			enddate = string.Empty;//endDate
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
		/// 上级id
		/// </summary>
		private int parentid;
		public int Parentid
		{
			get
			{
				return parentid;
			}
			set
			{
				parentid = value ;
			}
		}


		/// <summary>
		/// 编号

		/// </summary>
		private int no;
		public int No
		{
			get
			{
				return no;
			}
			set
			{
				no = value ;
			}
		}


		/// <summary>
		/// 类型
		/// </summary>
		private int type;
		public int Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value ;
			}
		}


		/// <summary>
		/// 是否是系统消息
		/// </summary>
		private int issysmsg;
		public int Issysmsg
		{
			get
			{
				return issysmsg;
			}
			set
			{
				issysmsg = value ;
			}
		}


		/// <summary>
		/// 发布时间
		/// </summary>
		private string issuedate;
		public string Issuedate
		{
			get
			{
				return issuedate;
			}
			set
			{
				issuedate = value ;
			}
		}


		/// <summary>
		/// 用户id

		/// </summary>
		private int userid;
		public int Userid
		{
			get
			{
				return userid;
			}
			set
			{
				userid = value ;
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
		/// 最后回复id

		/// </summary>
		private int lastreplyuserid;
		public int Lastreplyuserid
		{
			get
			{
				return lastreplyuserid;
			}
			set
			{
				lastreplyuserid = value ;
			}
		}


		/// <summary>
		/// 最后回复时间
		/// </summary>
		private string lastreplydate;
		public string Lastreplydate
		{
			get
			{
				return lastreplydate;
			}
			set
			{
				lastreplydate = value ;
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
		/// beginDate
		/// </summary>
		private string begindate;
		public string Begindate
		{
			get
			{
				return begindate;
			}
			set
			{
				begindate = value ;
			}
		}


		/// <summary>
		/// endDate
		/// </summary>
		private string enddate;
		public string Enddate
		{
			get
			{
				return enddate;
			}
			set
			{
				enddate = value ;
			}
		}


		#endregion
	}
}
