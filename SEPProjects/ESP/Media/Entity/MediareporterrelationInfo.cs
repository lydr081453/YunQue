using System;
namespace ESP.Media.Entity
{
	public class MediareporterrelationInfo
	{
		#region ���캯��
        public MediareporterrelationInfo()
		{
			mediaitemid = 0;//MediaItemID
			reporterid = 0;//ReporterID
			relationtype = 0;//RelationType
		}
		#endregion


		#region ����
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
		/// RelationType
		/// </summary>
		private int relationtype;
		public int Relationtype
		{
			get
			{
				return relationtype;
			}
			set
			{
				relationtype = value ;
			}
		}


		#endregion
	}
}
