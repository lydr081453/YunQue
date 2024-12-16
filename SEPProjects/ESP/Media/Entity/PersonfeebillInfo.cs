using System;
namespace ESP.Media.Entity
{
	public class PersonfeebillInfo
	{
		#region 构造函数
        public PersonfeebillInfo()
		{
			personfeebillid = 0;//personfeebillid
			createip = string.Empty;//createip
			createdate = string.Empty;//createdate
			userid = 0;//userid
			username = string.Empty;//username
			userdepartmentid = 0;//userdepartmentid
			userdepartmentname = string.Empty;//userdepartmentname
			describe = string.Empty;//describe
			del = 0;//del
			projectid = 0;//projectid
			projectcode = string.Empty;//projectcode
			userextensioncode = string.Empty;//userextensioncode
			reimbursedate = string.Empty;//ReimburseDate
			status = 0;//Status
		}
		#endregion


		#region 属性
		/// <summary>
		/// personfeebillid
		/// </summary>
		private int personfeebillid;
		public int Personfeebillid
		{
			get
			{
				return personfeebillid;
			}
			set
			{
				personfeebillid = value ;
			}
		}


		/// <summary>
		/// createip
		/// </summary>
		private string createip;
		public string Createip
		{
			get
			{
				return createip;
			}
			set
			{
				createip = value ;
			}
		}


		/// <summary>
		/// createdate
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
		/// userid
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
		/// username
		/// </summary>
		private string username;
		public string Username
		{
			get
			{
				return username;
			}
			set
			{
				username = value ;
			}
		}


		/// <summary>
		/// userdepartmentid
		/// </summary>
		private int userdepartmentid;
		public int Userdepartmentid
		{
			get
			{
				return userdepartmentid;
			}
			set
			{
				userdepartmentid = value ;
			}
		}


		/// <summary>
		/// userdepartmentname
		/// </summary>
		private string userdepartmentname;
		public string Userdepartmentname
		{
			get
			{
				return userdepartmentname;
			}
			set
			{
				userdepartmentname = value ;
			}
		}


		/// <summary>
		/// describe
		/// </summary>
		private string describe;
		public string Describe
		{
			get
			{
				return describe;
			}
			set
			{
				describe = value ;
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
		/// projectid
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
		/// projectcode
		/// </summary>
		private string projectcode;
		public string Projectcode
		{
			get
			{
				return projectcode;
			}
			set
			{
				projectcode = value ;
			}
		}


		/// <summary>
		/// userextensioncode
		/// </summary>
		private string userextensioncode;
		public string Userextensioncode
		{
			get
			{
				return userextensioncode;
			}
			set
			{
				userextensioncode = value ;
			}
		}


		/// <summary>
		/// ReimburseDate
		/// </summary>
		private string reimbursedate;
		public string Reimbursedate
		{
			get
			{
				return reimbursedate;
			}
			set
			{
				reimbursedate = value ;
			}
		}


		/// <summary>
		/// Status
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


		#endregion
	}
}
