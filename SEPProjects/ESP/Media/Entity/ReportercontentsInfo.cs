using System;
namespace ESP.Media.Entity
{
	public class ReportercontentsInfo
	{
		#region 构造函数
        public ReportercontentsInfo()
		{
			id = 0;//id
			reporterid = 0;//ReporterID
			version = 0;//Version
			contentxml = string.Empty;//ContentXml
			modifiedbyuserid = 0;//ModifiedByUserID
			modifiedbyusername = string.Empty;//ModifiedByUserName
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
		/// ReporterID
		/// </summary>
		private int reporterid;
		public int Reporterid
		{
			get
			{
				return reporterid;
			}
			set
			{
				reporterid = value ;
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
		/// ModifiedByUserName
		/// </summary>
		private string modifiedbyusername;
		public string Modifiedbyusername
		{
			get
			{
				return modifiedbyusername;
			}
			set
			{
				modifiedbyusername = value ;
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
