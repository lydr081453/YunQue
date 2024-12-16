using System;
namespace ESP.Media.Entity
{
	public class EventsInfo
	{
		#region 构造函数
        public EventsInfo()
		{
			eventid = 0;//EventID
			projectid = 0;//ProjectID
			createdbyuserid = 0;//CreatedByUserID
			createdip = string.Empty;//CreatedIP
			createddate = string.Empty;//CreatedDate
			del = 0;//删除标记
			eventname = string.Empty;//EventName
			eventcontent = string.Empty;//EventContent
			eventstarttime = string.Empty;//EventStartTime
			eventstatus = 0;//Eventstatus
		}
		#endregion


		#region 属性
		/// <summary>
		/// EventID
		/// </summary>
		private int eventid;
		public int Eventid
		{
			get
			{
				return eventid;
			}
			set
			{
				eventid = value ;
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
		/// EventName
		/// </summary>
		private string eventname;
		public string Eventname
		{
			get
			{
				return eventname;
			}
			set
			{
				eventname = value ;
			}
		}


		/// <summary>
		/// EventContent
		/// </summary>
		private string eventcontent;
		public string Eventcontent
		{
			get
			{
				return eventcontent;
			}
			set
			{
				eventcontent = value ;
			}
		}


		/// <summary>
		/// EventStartTime
		/// </summary>
		private string eventstarttime;
		public string Eventstarttime
		{
			get
			{
				return eventstarttime;
			}
			set
			{
				eventstarttime = value ;
			}
		}


		/// <summary>
		/// Eventstatus
		/// </summary>
		private int eventstatus;
		public int Eventstatus
		{
			get
			{
				return eventstatus;
			}
			set
			{
				eventstatus = value ;
			}
		}


		#endregion
	}
}
