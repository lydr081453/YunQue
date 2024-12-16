using System;
namespace ESP.Media.Entity
{
    public class AttachmentsInfo
	{
		#region 构造函数
        public AttachmentsInfo()
		{
			attachmentid = 0;//AttachmentID
			contenttype = string.Empty;//ContentType
			contentlength = 0;//ContentLength
			contentbody = string.Empty;//ContentBody
			lastupdateddate = string.Empty;//LastUpdatedDate
			createddate = string.Empty;//CreatedDate
			isimage = false;//IsImage
			imageheight = 0;//ImageHeight
			imagewidth = 0;//ImageWidth
			del = 0;//删除标示位

		}
		#endregion


		#region 属性
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
		/// ContentType
		/// </summary>
		private string contenttype;
		public string Contenttype
		{
			get
			{
				return contenttype;
			}
			set
			{
				contenttype = value ;
			}
		}


		/// <summary>
		/// ContentLength
		/// </summary>
		private int contentlength;
		public int Contentlength
		{
			get
			{
				return contentlength;
			}
			set
			{
				contentlength = value ;
			}
		}


		/// <summary>
		/// ContentBody
		/// </summary>
		private string contentbody;
		public string Contentbody
		{
			get
			{
				return contentbody;
			}
			set
			{
				contentbody = value ;
			}
		}


		/// <summary>
		/// LastUpdatedDate
		/// </summary>
		private string lastupdateddate;
		public string Lastupdateddate
		{
			get
			{
				return lastupdateddate;
			}
			set
			{
				lastupdateddate = value ;
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
		/// IsImage
		/// </summary>
		private bool isimage;
		public bool Isimage
		{
			get
			{
				return isimage;
			}
			set
			{
				isimage = value ;
			}
		}


		/// <summary>
		/// ImageHeight
		/// </summary>
		private int imageheight;
		public int Imageheight
		{
			get
			{
				return imageheight;
			}
			set
			{
				imageheight = value ;
			}
		}


		/// <summary>
		/// ImageWidth
		/// </summary>
		private int imagewidth;
		public int Imagewidth
		{
			get
			{
				return imagewidth;
			}
			set
			{
				imagewidth = value ;
			}
		}


		/// <summary>
		/// 删除标示位

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
