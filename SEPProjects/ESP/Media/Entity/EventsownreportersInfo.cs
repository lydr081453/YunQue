using System;
namespace ESP.Media.Entity
{
	public class EventsownreportersInfo
	{
		#region ���캯��
        public EventsownreportersInfo()
		{
			eventid = 0;//EventID
			reporterid = 0;//ReporterID
		}
		#endregion


		#region ����
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


		#endregion
	}
}
