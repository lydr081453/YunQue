using System;
namespace ESP.Media.Entity
{
	public class CountryInfo
	{
		#region 构造函数
        public CountryInfo()
		{
			countryid = 0;//CountryID
			countrynum = string.Empty;//CountryNum
			countryname = string.Empty;//CountryName
			regionattributeid = 0;//RegionAttributeID
		}
		#endregion


		#region 属性
		/// <summary>
		/// CountryID
		/// </summary>
		private int countryid;
		public int Countryid
		{
			get
			{
				return countryid;
			}
			set
			{
				countryid = value ;
			}
		}


		/// <summary>
		/// CountryNum
		/// </summary>
		private string countrynum;
		public string Countrynum
		{
			get
			{
				return countrynum;
			}
			set
			{
				countrynum = value ;
			}
		}


		/// <summary>
		/// CountryName
		/// </summary>
		private string countryname;
		public string Countryname
		{
			get
			{
				return countryname;
			}
			set
			{
				countryname = value ;
			}
		}


		/// <summary>
		/// RegionAttributeID
		/// </summary>
		private int regionattributeid;
		public int Regionattributeid
		{
			get
			{
				return regionattributeid;
			}
			set
			{
				regionattributeid = value ;
			}
		}


		#endregion
	}
}
