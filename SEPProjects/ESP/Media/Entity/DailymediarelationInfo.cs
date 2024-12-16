using System;
namespace ESP.Media.Entity
{
	public class DailymediarelationInfo
	{
		#region 构造函数
        public DailymediarelationInfo()
		{
			id = 0;//id
			dailyid = 0;//DailyID
			mediaitemid = 0;//MediaItemID
			relationdate = string.Empty;//RelationDate
			relationuserid = 0;//RelationUserID
			remark = string.Empty;//Remark
			del = 0;//Del
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
		/// DailyID
		/// </summary>
		private int dailyid;
		public int Dailyid
		{
			get
			{
				return dailyid;
			}
			set
			{
				dailyid = value ;
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
		/// RelationDate
		/// </summary>
		private string relationdate;
		public string Relationdate
		{
			get
			{
				return relationdate;
			}
			set
			{
				relationdate = value ;
			}
		}


		/// <summary>
		/// RelationUserID
		/// </summary>
		private int relationuserid;
		public int Relationuserid
		{
			get
			{
				return relationuserid;
			}
			set
			{
				relationuserid = value ;
			}
		}


		/// <summary>
		/// Remark
		/// </summary>
		private string remark;
		public string Remark
		{
			get
			{
				return remark;
			}
			set
			{
				remark = value ;
			}
		}


		/// <summary>
		/// Del
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
