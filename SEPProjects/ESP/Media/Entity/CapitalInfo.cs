using System;
namespace ESP.Media.Entity
{
	public class CapitalInfo
	{
		#region 构造函数
        public CapitalInfo()
		{
			cityid = 0;//cityID
			cityname = string.Empty;//cityname
			province = string.Empty;//Province
		}
		#endregion


		#region 属性
		/// <summary>
		/// cityID
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


		/// <summary>
		/// Province
		/// </summary>
		private string province;
		public string Province
		{
			get
			{
				return province;
			}
			set
			{
				province = value ;
			}
		}


		#endregion
	}
}
