using System;
namespace ESP.Media.Entity
{
    [Serializable]
	public class WritingfeeitemInfo
	{
		#region 构造函数
        public WritingfeeitemInfo()
		{
			writingfeeitemid = 0;//WritingFeeitemid
			writingfeebillid = 0;//WritingFeeBillID
			projectid = 0;//projectid
			projectcode = string.Empty;//projectcode
			happendate = string.Empty;//happendate
			userid = 0;//userid
			username = string.Empty;//username
			userdepartmentid = 0;//userdepartmentid
			userdepartmentname = string.Empty;//userdepartmentname
			mediaid = 0;//mediaid
			medianame = string.Empty;//medianame
			writingsubject = string.Empty;//writingsubject
			issuedate = string.Empty;//issuedate
			wordscount = 0;//wordscount
			areawordscount = 0;//areawordscount
			unitprice = 0.0D;//unitprice
			amountprice = 0.0D;//amountprice
			subtotal = 0.0D;//subtotal
			linkmanid = 0;//linkmanid
			linkmanname = string.Empty;//linkmanname
			recvmanname = string.Empty;//recvmanname
			cityid = 0;//cityid
			cityname = string.Empty;//cityname
			bankname = string.Empty;//bankname
			bankaccount = string.Empty;//bankaccount
			bankcardcode = string.Empty;//bankcardcode
			idcardcode = string.Empty;//IDcardcode
			phoneno = string.Empty;//phoneno
			del = 0;//del
			propagatetype = 0;//PropagateType
			propagateid = 0;//PropagateID
			propagatename = string.Empty;//PropagateName
			uploadstartdate = string.Empty;//Uploadstartdate
			uploadenddate = string.Empty;//Uploadenddate
			paytype = 0;//支付类型,刊前\刊后
		}
		#endregion


		#region 属性

        private string filename;

        public string FileName
        {
            get { return filename; }
            set { filename = value; }
        }
        private byte[] filedata;

        public byte[] FileData
        {
            get { return filedata; }
            set { filedata = value; }
        }

		/// <summary>
		/// WritingFeeitemid
		/// </summary>
		private int writingfeeitemid;
		public int Writingfeeitemid
		{
			get
			{
				return writingfeeitemid;
			}
			set
			{
				writingfeeitemid = value ;
			}
		}


		/// <summary>
		/// WritingFeeBillID
		/// </summary>
		private int writingfeebillid;
		public int Writingfeebillid
		{
			get
			{
				return writingfeebillid;
			}
			set
			{
				writingfeebillid = value ;
			}
		}


		/// <summary>
		/// projectid
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
		/// projectcode
		/// </summary>
		private string projectcode;
		public string Projectcode
		{
			get
			{
				return projectcode;
			}
			set
			{
				projectcode = value ;
			}
		}


		/// <summary>
		/// happendate
		/// </summary>
		private string happendate;
		public string Happendate
		{
			get
			{
				return happendate;
			}
			set
			{
				happendate = value ;
			}
		}


		/// <summary>
		/// userid
		/// </summary>
		private int userid;
		public int Userid
		{
			get
			{
				return userid;
			}
			set
			{
				userid = value ;
			}
		}


		/// <summary>
		/// username
		/// </summary>
		private string username;
		public string Username
		{
			get
			{
				return username;
			}
			set
			{
				username = value ;
			}
		}


		/// <summary>
		/// userdepartmentid
		/// </summary>
		private int userdepartmentid;
		public int Userdepartmentid
		{
			get
			{
				return userdepartmentid;
			}
			set
			{
				userdepartmentid = value ;
			}
		}


		/// <summary>
		/// userdepartmentname
		/// </summary>
		private string userdepartmentname;
		public string Userdepartmentname
		{
			get
			{
				return userdepartmentname;
			}
			set
			{
				userdepartmentname = value ;
			}
		}


		/// <summary>
		/// mediaid
		/// </summary>
		private int mediaid;
		public int Mediaid
		{
			get
			{
				return mediaid;
			}
			set
			{
				mediaid = value ;
			}
		}


		/// <summary>
		/// medianame
		/// </summary>
		private string medianame;
		public string Medianame
		{
			get
			{
				return medianame;
			}
			set
			{
				medianame = value ;
			}
		}


		/// <summary>
		/// writingsubject
		/// </summary>
		private string writingsubject;
		public string Writingsubject
		{
			get
			{
				return writingsubject;
			}
			set
			{
				writingsubject = value ;
			}
		}


		/// <summary>
		/// issuedate
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
		/// wordscount
		/// </summary>
		private int wordscount;
		public int Wordscount
		{
			get
			{
				return wordscount;
			}
			set
			{
				wordscount = value ;
			}
		}


		/// <summary>
		/// areawordscount
		/// </summary>
		private int areawordscount;
		public int Areawordscount
		{
			get
			{
				return areawordscount;
			}
			set
			{
				areawordscount = value ;
			}
		}


		/// <summary>
		/// unitprice
		/// </summary>
		private double unitprice;
		public double Unitprice
		{
			get
			{
				return unitprice;
			}
			set
			{
				unitprice = value ;
			}
		}


		/// <summary>
		/// amountprice
		/// </summary>
		private double amountprice;
		public double Amountprice
		{
			get
			{
				return amountprice;
			}
			set
			{
				amountprice = value ;
			}
		}


		/// <summary>
		/// subtotal
		/// </summary>
		private double subtotal;
		public double Subtotal
		{
			get
			{
				return subtotal;
			}
			set
			{
				subtotal = value ;
			}
		}


		/// <summary>
		/// linkmanid
		/// </summary>
		private int linkmanid;
		public int Linkmanid
		{
			get
			{
				return linkmanid;
			}
			set
			{
				linkmanid = value ;
			}
		}


		/// <summary>
		/// linkmanname
		/// </summary>
		private string linkmanname;
		public string Linkmanname
		{
			get
			{
				return linkmanname;
			}
			set
			{
				linkmanname = value ;
			}
		}


		/// <summary>
		/// recvmanname
		/// </summary>
		private string recvmanname;
		public string Recvmanname
		{
			get
			{
				return recvmanname;
			}
			set
			{
				recvmanname = value ;
			}
		}


		/// <summary>
		/// cityid
		/// </summary>
		private int cityid;
		public int Cityid
		{
			get
			{
				return cityid;
			}
			set
			{
				cityid = value ;
			}
		}


		/// <summary>
		/// cityname
		/// </summary>
		private string cityname;
		public string Cityname
		{
			get
			{
				return cityname;
			}
			set
			{
				cityname = value ;
			}
		}


		/// <summary>
		/// bankname
		/// </summary>
		private string bankname;
		public string Bankname
		{
			get
			{
				return bankname;
			}
			set
			{
				bankname = value ;
			}
		}


		/// <summary>
		/// bankaccount
		/// </summary>
		private string bankaccount;
		public string Bankaccount
		{
			get
			{
				return bankaccount;
			}
			set
			{
				bankaccount = value ;
			}
		}


		/// <summary>
		/// bankcardcode
		/// </summary>
		private string bankcardcode;
		public string Bankcardcode
		{
			get
			{
				return bankcardcode;
			}
			set
			{
				bankcardcode = value ;
			}
		}


		/// <summary>
		/// IDcardcode
		/// </summary>
		private string idcardcode;
		public string Idcardcode
		{
			get
			{
				return idcardcode;
			}
			set
			{
				idcardcode = value ;
			}
		}


		/// <summary>
		/// phoneno
		/// </summary>
		private string phoneno;
		public string Phoneno
		{
			get
			{
				return phoneno;
			}
			set
			{
				phoneno = value ;
			}
		}


		/// <summary>
		/// del
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
		/// PropagateType
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
		/// PropagateID
		/// </summary>
		private int propagateid;
		public int Propagateid
		{
			get
			{
				return propagateid;
			}
			set
			{
				propagateid = value ;
			}
		}


		/// <summary>
		/// PropagateName
		/// </summary>
		private string propagatename;
		public string Propagatename
		{
			get
			{
				return propagatename;
			}
			set
			{
				propagatename = value ;
			}
		}


		/// <summary>
		/// Uploadstartdate
		/// </summary>
		private string uploadstartdate;
		public string Uploadstartdate
		{
			get
			{
				return uploadstartdate;
			}
			set
			{
				uploadstartdate = value ;
			}
		}


		/// <summary>
		/// Uploadenddate
		/// </summary>
		private string uploadenddate;
		public string Uploadenddate
		{
			get
			{
				return uploadenddate;
			}
			set
			{
				uploadenddate = value ;
			}
		}


		/// <summary>
		/// 支付类型,刊前\刊后
		/// </summary>
		private int paytype;
		public int Paytype
		{
			get
			{
				return paytype;
			}
			set
			{
				paytype = value ;
			}
		}


		#endregion
	}
}
