using System;
namespace ESP.Media.Entity
{
	public class DailysInfo
	{
		#region 构造函数
        public DailysInfo()
		{
			dailyid = 0;//DailyID
			projectid = 0;//ProjectID
			createdbyuserid = 0;//CreatedByUserID
			createdip = string.Empty;//CreatedIP
			createddate = string.Empty;//CreatedDate
			del = 0;//删除标记
			dailyname = string.Empty;//DailyName
			dailycontent = string.Empty;//DailyContent
			dailystarttime = string.Empty;//DailyStartTime
			dailystatus = 0;//Dailystatus
			dailycycletype = 0;//DailyCycleType
			dailycycledays = 0;//DailyCycleDays
		}
		#endregion


		#region 属性
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
		/// ProjectID
		/// </summary>
		private int projectid;
		public int Projectid
		{
			get
			{
				return projectid;
			}
			set
			{
				projectid = value ;
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


		/// <summary>
		/// DailyName
		/// </summary>
		private string dailyname;
		public string Dailyname
		{
			get
			{
				return dailyname;
			}
			set
			{
				dailyname = value ;
			}
		}


		/// <summary>
		/// DailyContent
		/// </summary>
		private string dailycontent;
		public string Dailycontent
		{
			get
			{
				return dailycontent;
			}
			set
			{
				dailycontent = value ;
			}
		}


		/// <summary>
		/// DailyStartTime
		/// </summary>
		private string dailystarttime;
		public string Dailystarttime
		{
			get
			{
				return dailystarttime;
			}
			set
			{
				dailystarttime = value ;
			}
		}


		/// <summary>
		/// Dailystatus
		/// </summary>
		private int dailystatus;
		public int Dailystatus
		{
			get
			{
				return dailystatus;
			}
			set
			{
				dailystatus = value ;
			}
		}


		/// <summary>
		/// DailyCycleType
		/// </summary>
		private int dailycycletype;
		public int Dailycycletype
		{
			get
			{
				return dailycycletype;
			}
			set
			{
				dailycycletype = value ;
			}
		}


		/// <summary>
		/// DailyCycleDays
		/// </summary>
		private int dailycycledays;
		public int Dailycycledays
		{
			get
			{
				return dailycycledays;
			}
			set
			{
				dailycycledays = value ;
			}
		}


		#endregion
	}
}
