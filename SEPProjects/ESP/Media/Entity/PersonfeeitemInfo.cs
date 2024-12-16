using System;
namespace ESP.Media.Entity
{
	public class PersonfeeitemInfo
	{
		#region 构造函数
        public PersonfeeitemInfo()
		{
			personfeeitemid = 0;//personfeeitemid
			personfeebillid = 0;//personfeebillid
			projectid = 0;//projectid
			projectcode = string.Empty;//projectcode
			happendate = string.Empty;//happendate
			userid = 0;//userid
			username = string.Empty;//username
			userdepartmentid = 0;//userdepartmentid
			userdepartmentname = string.Empty;//userdepartmentname
			describe = string.Empty;//describe
			cash = 0.0D;//cash
			del = 0;//del
		}
		#endregion


		#region 属性
		/// <summary>
		/// personfeeitemid
		/// </summary>
		private int personfeeitemid;
		public int Personfeeitemid
		{
			get
			{
				return personfeeitemid;
			}
			set
			{
				personfeeitemid = value ;
			}
		}


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
		/// happendate
		/// </summary>
		private string happendate;
		public string Happendate
		{
			get
			{
				return happendate;
			}
			set
			{
				happendate = value ;
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
		/// cash
		/// </summary>
		private double cash;
		public double Cash
		{
			get
			{
				return cash;
			}
			set
			{
				cash = value ;
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


		#endregion
	}
}
