using System;
namespace ESP.Media.Entity
{
	public class ProjectmembersInfo
	{
		#region 构造函数
        public ProjectmembersInfo()
		{
			id = 0;//ID
			projectid = 0;//ProjectID
			userid = 0;//UserID
			role = 0;//Role
			del = 0;//del
			relationuserid = 0;//RelationUserid
			relationdate = string.Empty;//RelationDate
			remark = string.Empty;//Remark
		}
		#endregion


		#region 属性
		/// <summary>
		/// ID
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
		/// UserID
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
		/// Role
		/// </summary>
		private int role;
		public int Role
		{
			get
			{
				return role;
			}
			set
			{
				role = value ;
			}
		}


		/// <summary>
		/// del
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
		/// RelationUserid
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


		#endregion
	}
}
