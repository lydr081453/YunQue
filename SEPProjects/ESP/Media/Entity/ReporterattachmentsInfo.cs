using System;
namespace ESP.Media.Entity
{
	public class ReporterattachmentsInfo
	{
		#region ���캯��
        public ReporterattachmentsInfo()
		{
			id = 0;//id
			reporterid = 0;//ReporterID
			version = 0;//Version
			attachmentid = 0;//AttachmentID
			type = 0;//Type
			createdbyuserid = 0;//CreatedByUserID
			createdbyusername = string.Empty;//CreatedByUserName
			createddate = string.Empty;//CreatedDate
			del = 0;//ɾ�����

		}
		#endregion


		#region ����
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
		/// AttachmentID
		/// </summary>
		private int attachmentid;
		public int Attachmentid
		{
			get
			{
				return attachmentid;
			}
			set
			{
				attachmentid = value ;
			}
		}


		/// <summary>
		/// Type
		/// </summary>
		private int type;
		public int Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value ;
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
		/// ɾ�����

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
