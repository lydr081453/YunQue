using System;
namespace ESP.Media.Entity
{
	public class EventattachmentsInfo
	{
		#region ���캯��
        public EventattachmentsInfo()
		{
			id = 0;//id
			version = 0;//Version
			attachmentid = 0;//AttachmentID
			type = 0;//Type
			createdbyuserid = 0;//CreatedByUserID
			createdip = string.Empty;//Createdip
			createddate = string.Empty;//CreatedDate
			attachmentpath = string.Empty;//AttachmentPath
			del = 0;//ɾ�����
			eventid = 0;//EventID
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
		/// Createdip
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
		/// AttachmentPath
		/// </summary>
		private string attachmentpath;
		public string Attachmentpath
		{
			get
			{
				return attachmentpath;
			}
			set
			{
				attachmentpath = value ;
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


		#endregion
	}
}
