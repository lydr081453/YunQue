using System;
namespace ESP.Media.Entity
{
	public class PostsInfo
	{
		#region ���캯��
        public PostsInfo()
		{
			id = 0;//id
			parentid = 0;//�ϼ�id
			no = 0;//���

			type = 0;//����
			issysmsg = 0;//�Ƿ���ϵͳ��Ϣ
			issuedate = string.Empty;//����ʱ��
			userid = 0;//�û�id

			subject = string.Empty;//����

			body = string.Empty;//����

			lastreplyuserid = 0;//���ظ�id

			lastreplydate = string.Empty;//���ظ�ʱ��
			del = 0;//ɾ�����
			begindate = string.Empty;//beginDate
			enddate = string.Empty;//endDate
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
		/// �ϼ�id
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
		/// ���

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
		/// ����
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
		/// �Ƿ���ϵͳ��Ϣ
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
		/// ����ʱ��
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
		/// �û�id

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
		/// ���ظ�id

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
		/// ���ظ�ʱ��
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
