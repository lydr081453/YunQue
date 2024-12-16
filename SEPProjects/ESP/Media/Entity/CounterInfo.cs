using System;
namespace ESP.Media.Entity
{
	public class CounterInfo
	{
		#region 构造函数
        public CounterInfo()
		{
			integralid = 0;//IntegralID
			userid = 0;//UserID
			usercode = string.Empty;//UserCode
			username = string.Empty;//UserName
			counts = 0;//counts
			operatedate = string.Empty;//operateDate
		}
		#endregion


		#region 属性
		/// <summary>
		/// IntegralID
		/// </summary>
		private int integralid;
		public int Integralid
		{
			get
			{
				return integralid;
			}
			set
			{
				integralid = value ;
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
		/// UserCode
		/// </summary>
		private string usercode;
		public string Usercode
		{
			get
			{
				return usercode;
			}
			set
			{
				usercode = value ;
			}
		}


		/// <summary>
		/// UserName
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
		/// counts
		/// </summary>
		private int counts;
		public int Counts
		{
			get
			{
				return counts;
			}
			set
			{
				counts = value ;
			}
		}


		/// <summary>
		/// operateDate
		/// </summary>
		private string operatedate;
		public string Operatedate
		{
			get
			{
				return operatedate;
			}
			set
			{
				operatedate = value ;
			}
		}


		#endregion
	}
}
