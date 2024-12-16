using System;
namespace ESP.Media.Entity
{
	public class ClientshistInfo
	{
		#region 构造函数
        public ClientshistInfo()
		{
			id = 0;//id
			version = 0;//版本号
			clientid = 0;//ClientID
			clientcfullname = string.Empty;//ClientCFullName
			clientcshortname = string.Empty;//ClientCShortName
			clientefullname = string.Empty;//ClientEFullName
			clienteshortname = string.Empty;//ClientEShortName
			clientlogo = string.Empty;//ClientLogo
			clientdescription = string.Empty;//ClientDescription
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
		/// ClientCFullName
		/// </summary>
		private string clientcfullname;
		public string Clientcfullname
		{
			get
			{
				return clientcfullname;
			}
			set
			{
				clientcfullname = value ;
			}
		}


		/// <summary>
		/// ClientCShortName
		/// </summary>
		private string clientcshortname;
		public string Clientcshortname
		{
			get
			{
				return clientcshortname;
			}
			set
			{
				clientcshortname = value ;
			}
		}


		/// <summary>
		/// ClientEFullName
		/// </summary>
		private string clientefullname;
		public string Clientefullname
		{
			get
			{
				return clientefullname;
			}
			set
			{
				clientefullname = value ;
			}
		}


		/// <summary>
		/// ClientEShortName
		/// </summary>
		private string clienteshortname;
		public string Clienteshortname
		{
			get
			{
				return clienteshortname;
			}
			set
			{
				clienteshortname = value ;
			}
		}


		/// <summary>
		/// ClientLogo
		/// </summary>
		private string clientlogo;
		public string Clientlogo
		{
			get
			{
				return clientlogo;
			}
			set
			{
				clientlogo = value ;
			}
		}


		/// <summary>
		/// ClientDescription
		/// </summary>
		private string clientdescription;
		public string Clientdescription
		{
			get
			{
				return clientdescription;
			}
			set
			{
				clientdescription = value ;
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
