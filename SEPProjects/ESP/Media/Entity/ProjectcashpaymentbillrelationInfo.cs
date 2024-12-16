using System;
namespace ESP.Media.Entity
{
	public class ProjectcashpaymentbillrelationInfo
	{
		#region 构造函数
        public ProjectcashpaymentbillrelationInfo()
		{
			id = 0;//id
			cashpaymentbillid = 0;//cashpaymentbillid
			projectid = 0;//projectid
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
