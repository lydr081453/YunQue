using System;
namespace ESP.Media.Entity
{
	public class MediaitemcontentsInfo
	{
		#region 构造函数
        public MediaitemcontentsInfo()
		{
			id = 0;//id
			mediaitemid = 0;//MediaItemID
			version = 0;//Version
			logo = string.Empty;//Logo
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
		/// MediaItemID
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
		/// Logo
		/// </summary>
		private string logo;
		public string Logo
		{
			get
			{
				return logo;
			}
			set
			{
				logo = value ;
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
