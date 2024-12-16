using System;
namespace ESP.Media.Entity
{
	public class ProjectpersonfeebillrelationInfo
	{
		#region 构造函数
        public ProjectpersonfeebillrelationInfo()
		{
			id = 0;//id
			personfeebillid = 0;//personfeebillid
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
