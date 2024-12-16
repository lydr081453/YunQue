using System;
namespace ESP.Media.Entity
{
	public class CustomersInfo
	{
		#region 构造函数
        public CustomersInfo()
		{
			customerid = 0;//CustomerID
			clientid = 0;//ClientID
			customername = string.Empty;//CustomerName
			currentversion = 0;//CurrentVersion
			status = 0;//Status
			createdbyuserid = 0;//CreatedByUserID
			createdbyusername = string.Empty;//CreatedByUserName
			createddate = string.Empty;//CreatedDate
			lastmodifiedbyuserid = 0;//LastModifiedByUserID
			lastmodifiedbyusername = string.Empty;//LastModifiedByUserName
			lastmodifieddate = string.Empty;//LastModifiedDate
			del = 0;//删除标记

		}
		#endregion


		#region 属性
		/// <summary>
		/// CustomerID
		/// </summary>
		private int customerid;
		public int Customerid
		{
			get
			{
				return customerid;
			}
			set
			{
				customerid = value ;
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
		/// CustomerName
		/// </summary>
		private string customername;
		public string Customername
		{
			get
			{
				return customername;
			}
			set
			{
				customername = value ;
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
		/// CreatedByUserName
		/// </summary>
		private string createdbyusername;
		public string Createdbyusername
		{
			get
			{
				return createdbyusername;
			}
			set
			{
				createdbyusername = value ;
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
		/// LastModifiedByUserName
		/// </summary>
		private string lastmodifiedbyusername;
		public string Lastmodifiedbyusername
		{
			get
			{
				return lastmodifiedbyusername;
			}
			set
			{
				lastmodifiedbyusername = value ;
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
