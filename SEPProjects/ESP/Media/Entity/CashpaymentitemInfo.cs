using System;
namespace ESP.Media.Entity
{
	public class CashpaymentitemInfo
	{
		#region 构造函数
        public CashpaymentitemInfo()
		{
			cashpaymentitemid = 0;//cashpaymentitemid
			cashpaymentbillid = 0;//cashpaymentbillid
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
			userextensioncode = string.Empty;//userextensioncode
			reimbursedate = string.Empty;//ReimburseDate
		}
		#endregion


		#region 属性
		/// <summary>
		/// cashpaymentitemid
		/// </summary>
		private int cashpaymentitemid;
		public int Cashpaymentitemid
		{
			get
			{
				return cashpaymentitemid;
			}
			set
			{
				cashpaymentitemid = value ;
			}
		}


		/// <summary>
		/// cashpaymentbillid
		/// </summary>
		private int cashpaymentbillid;
		public int Cashpaymentbillid
		{
			get
			{
				return cashpaymentbillid;
			}
			set
			{
				cashpaymentbillid = value ;
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


		#endregion
	}
}
