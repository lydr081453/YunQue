using System;
namespace ESP.Media.Entity
{
	public class PurchasecontractsignedrecInfo
	{
		#region 构造函数
        public PurchasecontractsignedrecInfo()
		{
			id = 0;//id
			contractbody = string.Empty;//contractbody
			contractamount = 0.0D;//contractamount
			createip = string.Empty;//createip
			createdate = string.Empty;//createdate
			userid = 0;//userid
			username = string.Empty;//username
			userdepartmentid = 0;//userdepartmentid
			userdepartmentname = string.Empty;//userdepartmentname
			del = 0;//del
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
		/// contractbody
		/// </summary>
		private string contractbody;
		public string Contractbody
		{
			get
			{
				return contractbody;
			}
			set
			{
				contractbody = value ;
			}
		}


		/// <summary>
		/// contractamount
		/// </summary>
		private double contractamount;
		public double Contractamount
		{
			get
			{
				return contractamount;
			}
			set
			{
				contractamount = value ;
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
