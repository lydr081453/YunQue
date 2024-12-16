using System;
namespace ESP.Media.Entity
{
	public class CheckpaymentitemInfo
	{
		#region 构造函数
        public CheckpaymentitemInfo()
		{
			checkpaymentitemid = 0;//checkpaymentitemid
			checkpaymentbillid = 0;//checkpaymentbillid
			projectid = 0;//projectid
			projectcode = string.Empty;//projectcode
			happendate = string.Empty;//happendate
			userid = 0;//userid
			username = string.Empty;//username
			userdepartmentid = 0;//userdepartmentid
			userdepartmentname = string.Empty;//userdepartmentname
			describe = string.Empty;//describe
			amount = 0.0D;//amount
			userextensioncode = string.Empty;//userextensioncode
			reimbursedate = string.Empty;//ReimburseDate
			del = 0;//del
		}
		#endregion


		#region 属性
		/// <summary>
		/// checkpaymentitemid
		/// </summary>
		private int checkpaymentitemid;
		public int Checkpaymentitemid
		{
			get
			{
				return checkpaymentitemid;
			}
			set
			{
				checkpaymentitemid = value ;
			}
		}


		/// <summary>
		/// checkpaymentbillid
		/// </summary>
		private int checkpaymentbillid;
		public int Checkpaymentbillid
		{
			get
			{
				return checkpaymentbillid;
			}
			set
			{
				checkpaymentbillid = value ;
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
		/// amount
		/// </summary>
		private double amount;
		public double Amount
		{
			get
			{
				return amount;
			}
			set
			{
				amount = value ;
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
