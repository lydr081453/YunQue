using System;
namespace ESP.Media.Entity
{
	public class BranchInfo
	{
		#region 构造函数
        public BranchInfo()
		{
			id = 0;//id
			mediaitemid = 0;//mediaitemid
			cityid = 0;//cityid
			cityname = string.Empty;//cityname
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
		/// mediaitemid
		/// </summary>
		private int mediaitemid;
		public int Mediaitemid
		{
			get
			{
				return mediaitemid;
			}
			set
			{
				mediaitemid = value ;
			}
		}


		/// <summary>
		/// cityid
		/// </summary>
		private int cityid;
		public int Cityid
		{
			get
			{
				return cityid;
			}
			set
			{
				cityid = value ;
			}
		}


		/// <summary>
		/// cityname
		/// </summary>
		private string cityname;
		public string Cityname
		{
			get
			{
				return cityname;
			}
			set
			{
				cityname = value ;
			}
		}


		#endregion
	}
}
