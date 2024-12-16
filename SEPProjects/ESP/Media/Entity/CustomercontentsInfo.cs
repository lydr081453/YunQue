using System;
namespace ESP.Media.Entity
{
	public class CustomercontentsInfo
	{
		#region 构造函数
        public CustomercontentsInfo()
		{
			id = 0;//id
			customerid = 0;//CustomerID
			version = 0;//Version
			contentxml = string.Empty;//ContentXml
			modifiedbyuserid = 0;//ModifiedByUserID
			modifiedip = string.Empty;//ModifiedIP
			modifieddate = string.Empty;//ModifiedDate
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
		/// Version
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
		/// ContentXml
		/// </summary>
		private string contentxml;
		public string Contentxml
		{
			get
			{
				return contentxml;
			}
			set
			{
				contentxml = value ;
			}
		}


		/// <summary>
		/// ModifiedByUserID
		/// </summary>
		private int modifiedbyuserid;
		public int Modifiedbyuserid
		{
			get
			{
				return modifiedbyuserid;
			}
			set
			{
				modifiedbyuserid = value ;
			}
		}


		/// <summary>
		/// ModifiedIP
		/// </summary>
		private string modifiedip;
		public string Modifiedip
		{
			get
			{
				return modifiedip;
			}
			set
			{
				modifiedip = value ;
			}
		}


		/// <summary>
		/// ModifiedDate
		/// </summary>
		private string modifieddate;
		public string Modifieddate
		{
			get
			{
				return modifieddate;
			}
			set
			{
				modifieddate = value ;
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
