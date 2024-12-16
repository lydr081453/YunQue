using System;
namespace ESP.Media.Entity
{
	public class ProvinceInfo
	{
		#region 构造函数
        public ProvinceInfo()
		{
			province_id = 0;//Province_ID
			province_name = string.Empty;//Province_Name
			country_id = 0;//Country_ID
		}
		#endregion


		#region 属性
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


		/// <summary>
		/// Province_Name
		/// </summary>
		private string province_name;
		public string Province_name
		{
			get
			{
				return province_name;
			}
			set
			{
				province_name = value ;
			}
		}


		/// <summary>
		/// Country_ID
		/// </summary>
		private int country_id;
		public int Country_id
		{
			get
			{
				return country_id;
			}
			set
			{
				country_id = value ;
			}
		}


		#endregion
	}
}
