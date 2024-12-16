using System;
namespace ESP.Media.Entity
{
    [Serializable]
	public class ReportersInfo
	{
		#region 构造函数
        public ReportersInfo()
		{
			reporterid = 0;//ReporterID
			currentversion = 0;//CurrentVersion
			sn = string.Empty;//SN
			status = 0;//Status
			createdbyuserid = 0;//CreatedByUserID
			createdip = string.Empty;//CreatedIP
			createddate = string.Empty;//CreatedDate
			lastmodifiedbyuserid = 0;//LastModifiedByUserID
			lastmodifiedip = string.Empty;//LastModifiedIP
			lastmodifieddate = string.Empty;//LastModifiedDate
			reportername = string.Empty;//ReporterName
			penname = string.Empty;//PenName
			sex = 0;//Sex
			birthday = string.Empty;//Birthday
			cardnumber = string.Empty;//CardNumber
            media_id = 0;
			attention = string.Empty;//Attention
			reporterlevel = string.Empty;//ReporterLevel
			reporterposition = string.Empty;//ReporterPosition
			tel_o = string.Empty;//Tel_O
			tel_h = string.Empty;//Tel_H
			address_h = string.Empty;//Address_H
			postcode_h = string.Empty;//Postcode_H
			character = string.Empty;//Character
			hobby = string.Empty;//Hobby
			marriage = 0;//Marriage
			writing = string.Empty;//Writing
			family = string.Empty;//Family
			usualmobile = string.Empty;//UsualMobile
			backupmobile = string.Empty;//BackupMobile
			fax = string.Empty;//Fax
			qq = string.Empty;//QQ
			msn = string.Empty;//MSN
			emailone = string.Empty;//EmailOne
			emailtwo = string.Empty;//EmailTwo
			emailthree = string.Empty;//EmailThree
			unittype = 0;//UnitType
			unitname = string.Empty;//UnitName
			photo = string.Empty;//Photo
			experience = string.Empty;//Experience
			education = string.Empty;//Education
			del = 0;//del
			cityid = 0;//城市编号
            cityname = string.Empty;
			bankname = string.Empty;//bankname
			responsibledomain = string.Empty;//ResponsibleDomain
			paytype = 0;//PayType
			bankcardcode = string.Empty;//bankcardcode
			bankcardname = string.Empty;//bankcardname
			bankacountname = string.Empty;//bankacountname
			writingfee = 0.0D;//writingfee
			referral = string.Empty;//referral
			haveinvoice = 0;//haveInvoice
			paystatus = 0;//paystatus
			uploadstarttime = string.Empty;//剪报上传起始时间
			uploadendtime = string.Empty;//剪报上传结束时间
			paymentmode = 0;//付款方式
			privateremark = string.Empty;//PrivateRemark
			cooperatecircs = string.Empty;//cooperatecircs
			hometown = string.Empty;//籍贯
			othermessagesoftware = string.Empty;//OtherMessageSoftware
			remark = string.Empty;//Remark
		}
		#endregion

        private string officeaddr;
        public string OfficeAddress
        {
            get { return officeaddr; }
            set { officeaddr = value; }
        }

        /// <summary>
        /// Media_ID
        /// </summary>
        private int media_id;
        public int Media_id
        {
            get
            {
                return media_id;
            }
            set
            {
                media_id = value;
            }
        }

        private string officepostid;
        public string OfficePostID
        {
            get { return officepostid; }
            set { officepostid = value; }
        }

		#region 属性
        private string cityname;
        public string CityName
        {
            get { return cityname; }
            set { cityname = value; }
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
		/// CurrentVersion
		/// </summary>
		private int currentversion;
		public int Currentversion
		{
			get
			{
				return currentversion;
			}
			set
			{
				currentversion = value ;
			}
		}


		/// <summary>
		/// SN
		/// </summary>
		private string sn;
		public string Sn
		{
			get
			{
				return sn;
			}
			set
			{
				sn = value ;
			}
		}


		/// <summary>
		/// Status
		/// </summary>
		private int status;
		public int Status
		{
			get
			{
				return status;
			}
			set
			{
				status = value ;
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
		/// LastModifiedByUserID
		/// </summary>
		private int lastmodifiedbyuserid;
		public int Lastmodifiedbyuserid
		{
			get
			{
				return lastmodifiedbyuserid;
			}
			set
			{
				lastmodifiedbyuserid = value ;
			}
		}


		/// <summary>
		/// LastModifiedIP
		/// </summary>
		private string lastmodifiedip;
		public string Lastmodifiedip
		{
			get
			{
				return lastmodifiedip;
			}
			set
			{
				lastmodifiedip = value ;
			}
		}


		/// <summary>
		/// LastModifiedDate
		/// </summary>
		private string lastmodifieddate;
		public string Lastmodifieddate
		{
			get
			{
				return lastmodifieddate;
			}
			set
			{
				lastmodifieddate = value ;
			}
		}


		/// <summary>
		/// ReporterName
		/// </summary>
		private string reportername;
		public string Reportername
		{
			get
			{
				return reportername;
			}
			set
			{
				reportername = value ;
			}
		}


		/// <summary>
		/// PenName
		/// </summary>
		private string penname;
		public string Penname
		{
			get
			{
				return penname;
			}
			set
			{
				penname = value ;
			}
		}


		/// <summary>
		/// Sex
		/// </summary>
		private int sex;
		public int Sex
		{
			get
			{
				return sex;
			}
			set
			{
				sex = value ;
			}
		}


		/// <summary>
		/// Birthday
		/// </summary>
		private string birthday;
		public string Birthday
		{
			get
			{
				return birthday;
			}
			set
			{
				birthday = value ;
			}
		}


		/// <summary>
		/// CardNumber
		/// </summary>
		private string cardnumber;
		public string Cardnumber
		{
			get
			{
				return cardnumber;
			}
			set
			{
				cardnumber = value ;
			}
		}


		/// <summary>
		/// Attention
		/// </summary>
		private string attention;
		public string Attention
		{
			get
			{
				return attention;
			}
			set
			{
				attention = value ;
			}
		}


		/// <summary>
		/// ReporterLevel
		/// </summary>
		private string reporterlevel;
		public string Reporterlevel
		{
			get
			{
				return reporterlevel;
			}
			set
			{
				reporterlevel = value ;
			}
		}


		/// <summary>
		/// ReporterPosition
		/// </summary>
		private string reporterposition;
		public string Reporterposition
		{
			get
			{
				return reporterposition;
			}
			set
			{
				reporterposition = value ;
			}
		}


		/// <summary>
		/// Tel_O
		/// </summary>
		private string tel_o;
		public string Tel_o
		{
			get
			{
				return tel_o;
			}
			set
			{
				tel_o = value ;
			}
		}


		/// <summary>
		/// Tel_H
		/// </summary>
		private string tel_h;
		public string Tel_h
		{
			get
			{
				return tel_h;
			}
			set
			{
				tel_h = value ;
			}
		}


		/// <summary>
		/// Address_H
		/// </summary>
		private string address_h;
		public string Address_h
		{
			get
			{
				return address_h;
			}
			set
			{
				address_h = value ;
			}
		}


		/// <summary>
		/// Postcode_H
		/// </summary>
		private string postcode_h;
		public string Postcode_h
		{
			get
			{
				return postcode_h;
			}
			set
			{
				postcode_h = value ;
			}
		}


		/// <summary>
		/// Character
		/// </summary>
		private string character;
		public string Character
		{
			get
			{
				return character;
			}
			set
			{
				character = value ;
			}
		}


		/// <summary>
		/// Hobby
		/// </summary>
		private string hobby;
		public string Hobby
		{
			get
			{
				return hobby;
			}
			set
			{
				hobby = value ;
			}
		}


		/// <summary>
		/// Marriage
		/// </summary>
		private int marriage;
		public int Marriage
		{
			get
			{
				return marriage;
			}
			set
			{
				marriage = value ;
			}
		}


		/// <summary>
		/// Writing
		/// </summary>
		private string writing;
		public string Writing
		{
			get
			{
				return writing;
			}
			set
			{
				writing = value ;
			}
		}


		/// <summary>
		/// Family
		/// </summary>
		private string family;
		public string Family
		{
			get
			{
				return family;
			}
			set
			{
				family = value ;
			}
		}


		/// <summary>
		/// UsualMobile
		/// </summary>
		private string usualmobile;
		public string Usualmobile
		{
			get
			{
				return usualmobile;
			}
			set
			{
				usualmobile = value ;
			}
		}


		/// <summary>
		/// BackupMobile
		/// </summary>
		private string backupmobile;
		public string Backupmobile
		{
			get
			{
				return backupmobile;
			}
			set
			{
				backupmobile = value ;
			}
		}


		/// <summary>
		/// Fax
		/// </summary>
		private string fax;
		public string Fax
		{
			get
			{
				return fax;
			}
			set
			{
				fax = value ;
			}
		}


		/// <summary>
		/// QQ
		/// </summary>
		private string qq;
		public string Qq
		{
			get
			{
				return qq;
			}
			set
			{
				qq = value ;
			}
		}


		/// <summary>
		/// MSN
		/// </summary>
		private string msn;
		public string Msn
		{
			get
			{
				return msn;
			}
			set
			{
				msn = value ;
			}
		}


		/// <summary>
		/// EmailOne
		/// </summary>
		private string emailone;
		public string Emailone
		{
			get
			{
				return emailone;
			}
			set
			{
				emailone = value ;
			}
		}


		/// <summary>
		/// EmailTwo
		/// </summary>
		private string emailtwo;
		public string Emailtwo
		{
			get
			{
				return emailtwo;
			}
			set
			{
				emailtwo = value ;
			}
		}


		/// <summary>
		/// EmailThree
		/// </summary>
		private string emailthree;
		public string Emailthree
		{
			get
			{
				return emailthree;
			}
			set
			{
				emailthree = value ;
			}
		}


		/// <summary>
		/// UnitType
		/// </summary>
		private int unittype;
		public int Unittype
		{
			get
			{
				return unittype;
			}
			set
			{
				unittype = value ;
			}
		}


		/// <summary>
		/// UnitName
		/// </summary>
		private string unitname;
		public string Unitname
		{
			get
			{
				return unitname;
			}
			set
			{
				unitname = value ;
			}
		}


		/// <summary>
		/// Photo
		/// </summary>
		private string photo;
		public string Photo
		{
			get
			{
				return photo;
			}
			set
			{
				photo = value ;
			}
		}


		/// <summary>
		/// Experience
		/// </summary>
		private string experience;
		public string Experience
		{
			get
			{
				return experience;
			}
			set
			{
				experience = value ;
			}
		}


		/// <summary>
		/// Education
		/// </summary>
		private string education;
		public string Education
		{
			get
			{
				return education;
			}
			set
			{
				education = value ;
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
		/// 城市编号
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
		/// ResponsibleDomain
		/// </summary>
		private string responsibledomain;
		public string Responsibledomain
		{
			get
			{
				return responsibledomain;
			}
			set
			{
				responsibledomain = value ;
			}
		}


		/// <summary>
		/// PayType
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
		/// bankcardname
		/// </summary>
		private string bankcardname;
		public string Bankcardname
		{
			get
			{
				return bankcardname;
			}
			set
			{
				bankcardname = value ;
			}
		}


		/// <summary>
		/// bankacountname
		/// </summary>
		private string bankacountname;
		public string Bankacountname
		{
			get
			{
				return bankacountname;
			}
			set
			{
				bankacountname = value ;
			}
		}


		/// <summary>
		/// writingfee
		/// </summary>
		private double writingfee;
		public double Writingfee
		{
			get
			{
				return writingfee;
			}
			set
			{
				writingfee = value ;
			}
		}


		/// <summary>
		/// referral
		/// </summary>
		private string referral;
		public string Referral
		{
			get
			{
				return referral;
			}
			set
			{
				referral = value ;
			}
		}


		/// <summary>
		/// haveInvoice
		/// </summary>
		private int haveinvoice;
		public int Haveinvoice
		{
			get
			{
				return haveinvoice;
			}
			set
			{
				haveinvoice = value ;
			}
		}


		/// <summary>
		/// paystatus
		/// </summary>
		private int paystatus;
		public int Paystatus
		{
			get
			{
				return paystatus;
			}
			set
			{
				paystatus = value ;
			}
		}


		/// <summary>
		/// 剪报上传起始时间
		/// </summary>
		private string uploadstarttime;
		public string Uploadstarttime
		{
			get
			{
				return uploadstarttime;
			}
			set
			{
				uploadstarttime = value ;
			}
		}


		/// <summary>
		/// 剪报上传结束时间
		/// </summary>
		private string uploadendtime;
		public string Uploadendtime
		{
			get
			{
				return uploadendtime;
			}
			set
			{
				uploadendtime = value ;
			}
		}


		/// <summary>
		/// 付款方式
		/// </summary>
		private int paymentmode;
		public int Paymentmode
		{
			get
			{
				return paymentmode;
			}
			set
			{
				paymentmode = value ;
			}
		}


		/// <summary>
		/// PrivateRemark
		/// </summary>
		private string privateremark;
		public string Privateremark
		{
			get
			{
				return privateremark;
			}
			set
			{
				privateremark = value ;
			}
		}


		/// <summary>
		/// cooperatecircs
		/// </summary>
		private string cooperatecircs;
		public string Cooperatecircs
		{
			get
			{
				return cooperatecircs;
			}
			set
			{
				cooperatecircs = value ;
			}
		}


		/// <summary>
		/// 籍贯
		/// </summary>
		private string hometown;
		public string Hometown
		{
			get
			{
				return hometown;
			}
			set
			{
				hometown = value ;
			}
		}


		/// <summary>
		/// OtherMessageSoftware
		/// </summary>
		private string othermessagesoftware;
		public string Othermessagesoftware
		{
			get
			{
				return othermessagesoftware;
			}
			set
			{
				othermessagesoftware = value ;
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


		#endregion
	}
}
