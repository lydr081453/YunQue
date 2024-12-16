using System;
namespace ESP.Media.Entity
{
	public class MediaindustryrelationInfo
	{
		#region 构造函数
        public MediaindustryrelationInfo()
		{
			id = 0;//id
			mediaitemid = 0;//媒体id
			industryid = 0;//行业id

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
		/// 媒体id
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
		/// 行业id

		/// </summary>
		private int industryid;
		public int Industryid
		{
			get
			{
				return industryid;
			}
			set
			{
				industryid = value ;
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
