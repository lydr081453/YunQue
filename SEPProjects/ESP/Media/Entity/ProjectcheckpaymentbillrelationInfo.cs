using System;
namespace ESP.Media.Entity
{
	public class ProjectcheckpaymentbillrelationInfo
	{
		#region 构造函数
        public ProjectcheckpaymentbillrelationInfo()
		{
			id = 0;//id
			checkpaymentbillid = 0;//checkpaymentbillid
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
