using System;
namespace ESP.Media.Entity
{
	public class CityInfo
	{
		#region 构造函数
        public CityInfo()
		{
			city_id = 0;//City_ID
			city_name = string.Empty;//City_Name
			city_level = string.Empty;//City_Level
			province_id = 0;//Province_ID
		}
		#endregion


		#region 属性
		/// <summary>
		/// City_ID
		/// </summary>
		private int city_id;
		public int City_id
		{
			get
			{
				return city_id;
			}
			set
			{
				city_id = value ;
			}
		}


		/// <summary>
		/// City_Name
		/// </summary>
		private string city_name;
		public string City_name
		{
			get
			{
				return city_name;
			}
			set
			{
				city_name = value ;
			}
		}


		/// <summary>
		/// City_Level
		/// </summary>
		private string city_level;
		public string City_level
		{
			get
			{
				return city_level;
			}
			set
			{
				city_level = value ;
			}
		}


		/// <summary>
		/// Province_ID
		/// </summary>
		private int province_id;
		public int Province_id
		{
			get
			{
				return province_id;
			}
			set
			{
				province_id = value ;
			}
		}


		#endregion
	}
}
