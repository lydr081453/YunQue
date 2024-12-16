using System;
namespace ESP.Media.Entity
{
    [Serializable]
	public class ProjectbriefInfo
	{
		#region 构造函数
        public ProjectbriefInfo()
		{
			id = 0;//id
			projectid = 0;//ProjectID
			briefsubject = string.Empty;//BriefSubject
			wordsaccount = 0;//wordsaccount
			reporterid = 0;//reporterid
			mediaitemid = 0;//mediaitemid
			mediaitemtypeid = 0;//MediaItemTypeID
			des = string.Empty;//Des
			del = 0;//Del
			linkurl = string.Empty;//LinkUrl
			filepath = string.Empty;//FilePath
			createdbyuserid = 0;//CreatedByUserID
			createdip = string.Empty;//createdIP
			createddate = string.Empty;//createdDate
			paymentid = 0;//paymentid
			issuedate = string.Empty;//IssueDate
			propagatetype = 0;//Propagatetype
			progagateid = 0;//ProgagateID
		}
		#endregion


		#region 属性
        private byte[] filedata;

        public byte[] FileData
        {
            get { return filedata; }
            set { filedata = value; }
        }

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
		/// BriefSubject
		/// </summary>
		private string briefsubject;
		public string Briefsubject
		{
			get
			{
				return briefsubject;
			}
			set
			{
				briefsubject = value ;
			}
		}


		/// <summary>
		/// wordsaccount
		/// </summary>
		private int wordsaccount;
		public int Wordsaccount
		{
			get
			{
				return wordsaccount;
			}
			set
			{
				wordsaccount = value ;
			}
		}


		/// <summary>
		/// reporterid
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
		/// mediaitemid
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
		/// MediaItemTypeID
		/// </summary>
		private int mediaitemtypeid;
		public int Mediaitemtypeid
		{
			get
			{
				return mediaitemtypeid;
			}
			set
			{
				mediaitemtypeid = value ;
			}
		}


		/// <summary>
		/// Des
		/// </summary>
		private string des;
		public string Des
		{
			get
			{
				return des;
			}
			set
			{
				des = value ;
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


		/// <summary>
		/// LinkUrl
		/// </summary>
		private string linkurl;
		public string Linkurl
		{
			get
			{
				return linkurl;
			}
			set
			{
				linkurl = value ;
			}
		}


		/// <summary>
		/// FilePath
		/// </summary>
		private string filepath;
		public string Filepath
		{
			get
			{
				return filepath;
			}
			set
			{
				filepath = value ;
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
		/// createdIP
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
		/// createdDate
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
		/// paymentid
		/// </summary>
		private int paymentid;
		public int Paymentid
		{
			get
			{
				return paymentid;
			}
			set
			{
				paymentid = value ;
			}
		}


		/// <summary>
		/// IssueDate
		/// </summary>
		private string issuedate;
		public string Issuedate
		{
			get
			{
				return issuedate;
			}
			set
			{
				issuedate = value ;
			}
		}


		/// <summary>
		/// Propagatetype
		/// </summary>
		private int propagatetype;
		public int Propagatetype
		{
			get
			{
				return propagatetype;
			}
			set
			{
				propagatetype = value ;
			}
		}


		/// <summary>
		/// ProgagateID
		/// </summary>
		private int progagateid;
		public int Progagateid
		{
			get
			{
				return progagateid;
			}
			set
			{
				progagateid = value ;
			}
		}


		#endregion
	}
}
