using System;
namespace ESP.Media.Entity
{
    [Serializable]
	public class MediaitemsInfo
	{
		#region 构造函数
        public MediaitemsInfo()
		{
			mediaitemid = 0;//MediaitemID
			mediacname = string.Empty;//MediaCName
			mediaename = string.Empty;//MediaEName
			cshortname = string.Empty;//CShortName
			eshortname = string.Empty;//EShortName
			mediaitemtype = 0;//MediaItemType
			currentversion = 0;//CurrentVersion
			status = 0;//Status
			createdbyuserid = 0;//CreatedByUserID
			createdip = string.Empty;//CreatedIP
			createddate = string.Empty;//CreatedDate
			lastmodifiedbyuserid = 0;//LastModifiedByUserID
			lastmodifieddate = string.Empty;//LastModifiedDate
			lastmodifiedip = string.Empty;//LastModifiedIP
			mediumsort = string.Empty;//MediumSort
			readersort = string.Empty;//ReaderSort
			governingbody = string.Empty;//GoverningBody
			frontfor = string.Empty;//FrontFor
			proprieter = string.Empty;//Proprieter
			subproprieter = string.Empty;//SubProprieter
			chiefeditor = string.Empty;//ChiefEditor
			admineditor = string.Empty;//AdminEditor
			subeditor = string.Empty;//Subeditor
			manager = string.Empty;//Manager
			zhuren = string.Empty;//Zhuren
			producer = string.Empty;//Producer
			startpublication = string.Empty;//StartPublication
			publishdate = string.Empty;//PublishDate
			telephoneexchange = string.Empty;//TelephoneExchange
			fax = string.Empty;//Fax
			addressone = string.Empty;//AddressOne
			addresstwo = string.Empty;//AddressTwo
			webaddress = string.Empty;//WebAddress
			issn = string.Empty;//ISSN
			cooperate = string.Empty;//Cooperate
			circulation = 0;//Circulation
			publishchannels = string.Empty;//PublishChannels
			phoneone = string.Empty;//PhoneOne
			phonetwo = string.Empty;//PhoneTwo
			endpublication = string.Empty;//EndPublication
			adsphone = string.Empty;//AdsPhone
			adsprice = 0;//AdsPrice
			medialogo = string.Empty;//MediaLogo
			briefing = 0;//Briefing
			mediaintro = string.Empty;//MediaIntro
			engintro = string.Empty;//EngIntro
			remarks = string.Empty;//Remarks
			channelname = string.Empty;//ChannelName
			dabfm = string.Empty;//DABFM
			topicname = string.Empty;//TopicName
			topicproperty = 0;//TopicProperty
			overriderange = string.Empty;//OverrideRange
			rating = string.Empty;//Rating
			topictime = string.Empty;//TopicTime
			channelwebaddress = string.Empty;//ChannelWebAddress
			countryid = 0;//国家id
			provinceid = 0;//省id

			cityid = 0;//城市id

			del = 0;//删除标记
			mediatype = string.Empty;//MediaType
			addr1_provinceid = 0;//地址1省id

			addr1_cityid = 0;//地址1城市id

			addr1_countryid = 0;//地址1国家id
			addr2_provinceid = 0;//地址2省id
			addr2_cityid = 0;//地址2城市id
			addr2_countryid = 0;//地址2国家id
			postcode = string.Empty;//PostCode
			regionattribute = 0;//RegionAttribute
			override_countryid = 0;//Override_Countryid
			override_provinceid = 0;//Override_Provinceid
			override_cityid = 0;//Override_Cityid
			override_describe = string.Empty;//Override_describe
			industryid = 0;//IndustryID
			issueregion = string.Empty;//IssueRegion
			publishperiods = string.Empty;//PublishPeriods
		}
		#endregion


		#region 属性
		/// <summary>
		/// MediaitemID
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
		/// MediaCName
		/// </summary>
		private string mediacname;
		public string Mediacname
		{
			get
			{
				return mediacname;
			}
			set
			{
				mediacname = value ;
			}
		}


		/// <summary>
		/// MediaEName
		/// </summary>
		private string mediaename;
		public string Mediaename
		{
			get
			{
				return mediaename;
			}
			set
			{
				mediaename = value ;
			}
		}


		/// <summary>
		/// CShortName
		/// </summary>
		private string cshortname;
		public string Cshortname
		{
			get
			{
				return cshortname;
			}
			set
			{
				cshortname = value ;
			}
		}


		/// <summary>
		/// EShortName
		/// </summary>
		private string eshortname;
		public string Eshortname
		{
			get
			{
				return eshortname;
			}
			set
			{
				eshortname = value ;
			}
		}


		/// <summary>
		/// MediaItemType
		/// </summary>
		private int mediaitemtype;
		public int Mediaitemtype
		{
			get
			{
				return mediaitemtype;
			}
			set
			{
				mediaitemtype = value ;
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
		/// MediumSort
		/// </summary>
		private string mediumsort;
		public string Mediumsort
		{
			get
			{
				return mediumsort;
			}
			set
			{
				mediumsort = value ;
			}
		}


		/// <summary>
		/// ReaderSort
		/// </summary>
		private string readersort;
		public string Readersort
		{
			get
			{
				return readersort;
			}
			set
			{
				readersort = value ;
			}
		}


		/// <summary>
		/// GoverningBody
		/// </summary>
		private string governingbody;
		public string Governingbody
		{
			get
			{
				return governingbody;
			}
			set
			{
				governingbody = value ;
			}
		}


		/// <summary>
		/// FrontFor
		/// </summary>
		private string frontfor;
		public string Frontfor
		{
			get
			{
				return frontfor;
			}
			set
			{
				frontfor = value ;
			}
		}


		/// <summary>
		/// Proprieter
		/// </summary>
		private string proprieter;
		public string Proprieter
		{
			get
			{
				return proprieter;
			}
			set
			{
				proprieter = value ;
			}
		}


		/// <summary>
		/// SubProprieter
		/// </summary>
		private string subproprieter;
		public string Subproprieter
		{
			get
			{
				return subproprieter;
			}
			set
			{
				subproprieter = value ;
			}
		}


		/// <summary>
		/// ChiefEditor
		/// </summary>
		private string chiefeditor;
		public string Chiefeditor
		{
			get
			{
				return chiefeditor;
			}
			set
			{
				chiefeditor = value ;
			}
		}


		/// <summary>
		/// AdminEditor
		/// </summary>
		private string admineditor;
		public string Admineditor
		{
			get
			{
				return admineditor;
			}
			set
			{
				admineditor = value ;
			}
		}


		/// <summary>
		/// Subeditor
		/// </summary>
		private string subeditor;
		public string Subeditor
		{
			get
			{
				return subeditor;
			}
			set
			{
				subeditor = value ;
			}
		}


		/// <summary>
		/// Manager
		/// </summary>
		private string manager;
		public string Manager
		{
			get
			{
				return manager;
			}
			set
			{
				manager = value ;
			}
		}


		/// <summary>
		/// Zhuren
		/// </summary>
		private string zhuren;
		public string Zhuren
		{
			get
			{
				return zhuren;
			}
			set
			{
				zhuren = value ;
			}
		}


		/// <summary>
		/// Producer
		/// </summary>
		private string producer;
		public string Producer
		{
			get
			{
				return producer;
			}
			set
			{
				producer = value ;
			}
		}


		/// <summary>
		/// StartPublication
		/// </summary>
		private string startpublication;
		public string Startpublication
		{
			get
			{
				return startpublication;
			}
			set
			{
				startpublication = value ;
			}
		}


		/// <summary>
		/// PublishDate
		/// </summary>
		private string publishdate;
		public string Publishdate
		{
			get
			{
				return publishdate;
			}
			set
			{
				publishdate = value ;
			}
		}


		/// <summary>
		/// TelephoneExchange
		/// </summary>
		private string telephoneexchange;
		public string Telephoneexchange
		{
			get
			{
				return telephoneexchange;
			}
			set
			{
				telephoneexchange = value ;
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
		/// AddressOne
		/// </summary>
		private string addressone;
		public string Addressone
		{
			get
			{
				return addressone;
			}
			set
			{
				addressone = value ;
			}
		}


		/// <summary>
		/// AddressTwo
		/// </summary>
		private string addresstwo;
		public string Addresstwo
		{
			get
			{
				return addresstwo;
			}
			set
			{
				addresstwo = value ;
			}
		}


		/// <summary>
		/// WebAddress
		/// </summary>
		private string webaddress;
		public string Webaddress
		{
			get
			{
				return webaddress;
			}
			set
			{
				webaddress = value ;
			}
		}


		/// <summary>
		/// ISSN
		/// </summary>
		private string issn;
		public string Issn
		{
			get
			{
				return issn;
			}
			set
			{
				issn = value ;
			}
		}


		/// <summary>
		/// Cooperate
		/// </summary>
		private string cooperate;
		public string Cooperate
		{
			get
			{
				return cooperate;
			}
			set
			{
				cooperate = value ;
			}
		}


		/// <summary>
		/// Circulation
		/// </summary>
		private int circulation;
		public int Circulation
		{
			get
			{
				return circulation;
			}
			set
			{
				circulation = value ;
			}
		}


		/// <summary>
		/// PublishChannels
		/// </summary>
		private string publishchannels;
		public string Publishchannels
		{
			get
			{
				return publishchannels;
			}
			set
			{
				publishchannels = value ;
			}
		}


		/// <summary>
		/// PhoneOne
		/// </summary>
		private string phoneone;
		public string Phoneone
		{
			get
			{
				return phoneone;
			}
			set
			{
				phoneone = value ;
			}
		}


		/// <summary>
		/// PhoneTwo
		/// </summary>
		private string phonetwo;
		public string Phonetwo
		{
			get
			{
				return phonetwo;
			}
			set
			{
				phonetwo = value ;
			}
		}


		/// <summary>
		/// EndPublication
		/// </summary>
		private string endpublication;
		public string Endpublication
		{
			get
			{
				return endpublication;
			}
			set
			{
				endpublication = value ;
			}
		}


		/// <summary>
		/// AdsPhone
		/// </summary>
		private string adsphone;
		public string Adsphone
		{
			get
			{
				return adsphone;
			}
			set
			{
				adsphone = value ;
			}
		}


		/// <summary>
		/// AdsPrice
		/// </summary>
		private int adsprice;
		public int Adsprice
		{
			get
			{
				return adsprice;
			}
			set
			{
				adsprice = value ;
			}
		}


		/// <summary>
		/// MediaLogo
		/// </summary>
		private string medialogo;
		public string Medialogo
		{
			get
			{
				return medialogo;
			}
			set
			{
				medialogo = value ;
			}
		}


		/// <summary>
		/// Briefing
		/// </summary>
		private int briefing;
		public int Briefing
		{
			get
			{
				return briefing;
			}
			set
			{
				briefing = value ;
			}
		}


		/// <summary>
		/// MediaIntro
		/// </summary>
		private string mediaintro;
		public string Mediaintro
		{
			get
			{
				return mediaintro;
			}
			set
			{
				mediaintro = value ;
			}
		}


		/// <summary>
		/// EngIntro
		/// </summary>
		private string engintro;
		public string Engintro
		{
			get
			{
				return engintro;
			}
			set
			{
				engintro = value ;
			}
		}


		/// <summary>
		/// Remarks
		/// </summary>
		private string remarks;
		public string Remarks
		{
			get
			{
				return remarks;
			}
			set
			{
				remarks = value ;
			}
		}


		/// <summary>
		/// ChannelName
		/// </summary>
		private string channelname;
		public string Channelname
		{
			get
			{
				return channelname;
			}
			set
			{
				channelname = value ;
			}
		}


		/// <summary>
		/// DABFM
		/// </summary>
		private string dabfm;
		public string Dabfm
		{
			get
			{
				return dabfm;
			}
			set
			{
				dabfm = value ;
			}
		}


		/// <summary>
		/// TopicName
		/// </summary>
		private string topicname;
		public string Topicname
		{
			get
			{
				return topicname;
			}
			set
			{
				topicname = value ;
			}
		}


		/// <summary>
		/// TopicProperty
		/// </summary>
		private int topicproperty;
		public int Topicproperty
		{
			get
			{
				return topicproperty;
			}
			set
			{
				topicproperty = value ;
			}
		}


		/// <summary>
		/// OverrideRange
		/// </summary>
		private string overriderange;
		public string Overriderange
		{
			get
			{
				return overriderange;
			}
			set
			{
				overriderange = value ;
			}
		}


		/// <summary>
		/// Rating
		/// </summary>
		private string rating;
		public string Rating
		{
			get
			{
				return rating;
			}
			set
			{
				rating = value ;
			}
		}


		/// <summary>
		/// TopicTime
		/// </summary>
		private string topictime;
		public string Topictime
		{
			get
			{
				return topictime;
			}
			set
			{
				topictime = value ;
			}
		}


		/// <summary>
		/// ChannelWebAddress
		/// </summary>
		private string channelwebaddress;
		public string Channelwebaddress
		{
			get
			{
				return channelwebaddress;
			}
			set
			{
				channelwebaddress = value ;
			}
		}


		/// <summary>
		/// 国家id
		/// </summary>
		private int countryid;
		public int Countryid
		{
			get
			{
				return countryid;
			}
			set
			{
				countryid = value ;
			}
		}


		/// <summary>
		/// 省id

		/// </summary>
		private int provinceid;
		public int Provinceid
		{
			get
			{
				return provinceid;
			}
			set
			{
				provinceid = value ;
			}
		}


		/// <summary>
		/// 城市id

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
		/// MediaType
		/// </summary>
		private string mediatype;
		public string Mediatype
		{
			get
			{
				return mediatype;
			}
			set
			{
				mediatype = value ;
			}
		}


		/// <summary>
		/// 地址1省id

		/// </summary>
		private int addr1_provinceid;
		public int Addr1_provinceid
		{
			get
			{
				return addr1_provinceid;
			}
			set
			{
				addr1_provinceid = value ;
			}
		}


		/// <summary>
		/// 地址1城市id

		/// </summary>
		private int addr1_cityid;
		public int Addr1_cityid
		{
			get
			{
				return addr1_cityid;
			}
			set
			{
				addr1_cityid = value ;
			}
		}


		/// <summary>
		/// 地址1国家id
		/// </summary>
		private int addr1_countryid;
		public int Addr1_countryid
		{
			get
			{
				return addr1_countryid;
			}
			set
			{
				addr1_countryid = value ;
			}
		}


		/// <summary>
		/// 地址2省id
		/// </summary>
		private int addr2_provinceid;
		public int Addr2_provinceid
		{
			get
			{
				return addr2_provinceid;
			}
			set
			{
				addr2_provinceid = value ;
			}
		}


		/// <summary>
		/// 地址2城市id
		/// </summary>
		private int addr2_cityid;
		public int Addr2_cityid
		{
			get
			{
				return addr2_cityid;
			}
			set
			{
				addr2_cityid = value ;
			}
		}


		/// <summary>
		/// 地址2国家id
		/// </summary>
		private int addr2_countryid;
		public int Addr2_countryid
		{
			get
			{
				return addr2_countryid;
			}
			set
			{
				addr2_countryid = value ;
			}
		}


		/// <summary>
		/// PostCode
		/// </summary>
		private string postcode;
		public string Postcode
		{
			get
			{
				return postcode;
			}
			set
			{
				postcode = value ;
			}
		}


		/// <summary>
		/// RegionAttribute
		/// </summary>
		private int regionattribute;
		public int Regionattribute
		{
			get
			{
				return regionattribute;
			}
			set
			{
				regionattribute = value ;
			}
		}


		/// <summary>
		/// Override_Countryid
		/// </summary>
		private int override_countryid;
		public int Override_countryid
		{
			get
			{
				return override_countryid;
			}
			set
			{
				override_countryid = value ;
			}
		}


		/// <summary>
		/// Override_Provinceid
		/// </summary>
		private int override_provinceid;
		public int Override_provinceid
		{
			get
			{
				return override_provinceid;
			}
			set
			{
				override_provinceid = value ;
			}
		}


		/// <summary>
		/// Override_Cityid
		/// </summary>
		private int override_cityid;
		public int Override_cityid
		{
			get
			{
				return override_cityid;
			}
			set
			{
				override_cityid = value ;
			}
		}


		/// <summary>
		/// Override_describe
		/// </summary>
		private string override_describe;
		public string Override_describe
		{
			get
			{
				return override_describe;
			}
			set
			{
				override_describe = value ;
			}
		}


		/// <summary>
		/// IndustryID
		/// </summary>
		private int industryid;
		public int Industryid
		{
			get
			{
				return industryid;
			}
			set
			{
				industryid = value ;
			}
		}


		/// <summary>
		/// IssueRegion
		/// </summary>
		private string issueregion;
		public string Issueregion
		{
			get
			{
				return issueregion;
			}
			set
			{
				issueregion = value ;
			}
		}


		/// <summary>
		/// PublishPeriods
		/// </summary>
		private string publishperiods;
		public string Publishperiods
		{
			get
			{
				return publishperiods;
			}
			set
			{
				publishperiods = value ;
			}
		}


		#endregion
	}
}
