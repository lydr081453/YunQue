using System;
namespace ESP.Media.Entity
{
	public class ProductlineshistInfo
	{
		#region 构造函数
        public ProductlineshistInfo()
		{
			id = 0;//id
			version = 0;//版本号
			productlineid = 0;//ProductLineID
			productlinename = string.Empty;//ProductLineName
			productlinedescription = string.Empty;//ProductLineDescription
			productlinetitle = string.Empty;//ProductLineTitle
			clientid = 0;//ClientID
			currentversion = 0;//CurrentVersion
			status = 0;//Status
			createdbyuserid = 0;//CreatedByUserID
			createdip = string.Empty;//CreatedIP
			createddate = string.Empty;//CreatedDate
			lastmodifiedbyuserid = 0;//LastModifiedByUserID
			lastmodifiedip = string.Empty;//LastModifiedIP
			lastmodifieddate = string.Empty;//LastModifiedDate
			del = 0;//删除标记
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
		/// 版本号
		/// </summary>
		private int version;
		public int Version
		{
			get
			{
				return version;
			}
			set
			{
				version = value ;
			}
		}


		/// <summary>
		/// ProductLineID
		/// </summary>
		private int productlineid;
		public int Productlineid
		{
			get
			{
				return productlineid;
			}
			set
			{
				productlineid = value ;
			}
		}


		/// <summary>
		/// ProductLineName
		/// </summary>
		private string productlinename;
		public string Productlinename
		{
			get
			{
				return productlinename;
			}
			set
			{
				productlinename = value ;
			}
		}


		/// <summary>
		/// ProductLineDescription
		/// </summary>
		private string productlinedescription;
		public string Productlinedescription
		{
			get
			{
				return productlinedescription;
			}
			set
			{
				productlinedescription = value ;
			}
		}


		/// <summary>
		/// ProductLineTitle
		/// </summary>
		private string productlinetitle;
		public string Productlinetitle
		{
			get
			{
				return productlinetitle;
			}
			set
			{
				productlinetitle = value ;
			}
		}


		/// <summary>
		/// ClientID
		/// </summary>
		private int clientid;
		public int Clientid
		{
			get
			{
				return clientid;
			}
			set
			{
				clientid = value ;
			}
		}


		/// <summary>
		/// CurrentVersion
		/// </summary>
		private int currentversion;
		public int Currentversion
		{
			get
			{
				return currentversion;
			}
			set
			{
				currentversion = value ;
			}
		}


		/// <summary>
		/// Status
		/// </summary>
		private int status;
		public int Status
		{
			get
			{
				return status;
			}
			set
			{
				status = value ;
			}
		}


		/// <summary>
		/// CreatedByUserID
		/// </summary>
		private int createdbyuserid;
		public int Createdbyuserid
		{
			get
			{
				return createdbyuserid;
			}
			set
			{
				createdbyuserid = value ;
			}
		}


		/// <summary>
		/// CreatedIP
		/// </summary>
		private string createdip;
		public string Createdip
		{
			get
			{
				return createdip;
			}
			set
			{
				createdip = value ;
			}
		}


		/// <summary>
		/// CreatedDate
		/// </summary>
		private string createddate;
		public string Createddate
		{
			get
			{
				return createddate;
			}
			set
			{
				createddate = value ;
			}
		}


		/// <summary>
		/// LastModifiedByUserID
		/// </summary>
		private int lastmodifiedbyuserid;
		public int Lastmodifiedbyuserid
		{
			get
			{
				return lastmodifiedbyuserid;
			}
			set
			{
				lastmodifiedbyuserid = value ;
			}
		}


		/// <summary>
		/// LastModifiedIP
		/// </summary>
		private string lastmodifiedip;
		public string Lastmodifiedip
		{
			get
			{
				return lastmodifiedip;
			}
			set
			{
				lastmodifiedip = value ;
			}
		}


		/// <summary>
		/// LastModifiedDate
		/// </summary>
		private string lastmodifieddate;
		public string Lastmodifieddate
		{
			get
			{
				return lastmodifieddate;
			}
			set
			{
				lastmodifieddate = value ;
			}
		}


		/// <summary>
		/// 删除标记
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
