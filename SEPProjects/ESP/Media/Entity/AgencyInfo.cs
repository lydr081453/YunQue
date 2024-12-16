using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 机构
 * 
 * 
 */
namespace ESP.Media.Entity
{
    public class AgencyInfo
    {
        public AgencyInfo()
        { }
        #region Model
        private int _agencyid;
        private string _agencycname;
        private string _agencyename;
        private string _cshortname;
        private string _eshortname;
        private int _agencytype;
        private int _status;
        private int _createdbyuserid;
        private DateTime _createddate;
        private int _lastmodifiedbyuserid;
        private DateTime _lastmodifieddate;
        private string _mediumsort;
        private string _readersort;
        private string _governingbody;
        private string _frontfor;
        private string _responsibleperson;
        private string _contacter;
        private string _telephoneexchange;
        private string _fax;
        private string _addressone;
        private string _addresstwo;
        private string _webaddress;
        private string _cooperate;
        private string _phoneone;
        private string _phonetwo;
        private string _agencylogo;
        private string _agencyintro;
        private string _engintro;
        private string _remarks;
        private string _topicname;
        private int _topicproperty;
        private string _overriderange;
        private string _channelwebaddress;
        private int _countryid;
        private int _provinceid;
        private int _cityid;
        private int _del;
        private string _mediatype;
        private int _addr1_provinceid;
        private int _agency;
        private int _addr1_countryid;
        private int _addr2_provinceid;
        private int _addr2_cityid;
        private int _addr2_countryid;
        private string _postcode;
        private int _mediaid;
        private int _regionattribute;
        /// <summary>
        /// 媒体ID
        /// </summary>
        public int AgencyID
        {
            set { _agencyid = value; }
            get { return _agencyid; }
        }
        /// <summary>
        /// 媒体中文名称
        /// </summary>
        public string AgencyCName
        {
            set { _agencycname = value; }
            get { return _agencycname; }
        }
        /// <summary>
        /// 媒体英文名称
        /// </summary>
        public string AgencyEName
        {
            set { _agencyename = value; }
            get { return _agencyename; }
        }
        /// <summary>
        /// 媒体中文简称
        /// </summary>
        public string CShortName
        {
            set { _cshortname = value; }
            get { return _cshortname; }
        }
        /// <summary>
        /// 媒体英文简称
        /// </summary>
        public string EShortName
        {
            set { _eshortname = value; }
            get { return _eshortname; }
        }
        /// <summary>
        /// 媒体类型1平面2网络3电视4广播
        /// </summary>
        public int AgencyType
        {
            set { _agencytype = value; }
            get { return _agencytype; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 创建用户id
        /// </summary>
        public int CreatedByUserID
        {
            set { _createdbyuserid = value; }
            get { return _createdbyuserid; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
        /// <summary>
        /// 修改id
        /// </summary>
        public int LastModifiedByUserID
        {
            set { _lastmodifiedbyuserid = value; }
            get { return _lastmodifiedbyuserid; }
        }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime LastModifiedDate
        {
            set { _lastmodifieddate = value; }
            get { return _lastmodifieddate; }
        }
        /// <summary>
        /// 形态属性
        /// </summary>
        public string MediumSort
        {
            set { _mediumsort = value; }
            get { return _mediumsort; }
        }
        /// <summary>
        /// 受众属性
        /// </summary>
        public string ReaderSort
        {
            set { _readersort = value; }
            get { return _readersort; }
        }
        /// <summary>
        /// 主管（所属）单位
        /// </summary>
        public string GoverningBody
        {
            set { _governingbody = value; }
            get { return _governingbody; }
        }
        /// <summary>
        /// 主办单位
        /// </summary>
        public string FrontFor
        {
            set { _frontfor = value; }
            get { return _frontfor; }
        }
        /// <summary>
        /// 负责人
        /// </summary>
        public string ResponsiblePerson
        {
            set { _responsibleperson = value; }
            get { return _responsibleperson; }
        }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contacter
        {
            set { _contacter = value; }
            get { return _contacter; }
        }
        /// <summary>
        /// 总机
        /// </summary>
        public string TelephoneExchange
        {
            set { _telephoneexchange = value; }
            get { return _telephoneexchange; }
        }
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax
        {
            set { _fax = value; }
            get { return _fax; }
        }
        /// <summary>
        /// 地址1
        /// </summary>
        public string AddressOne
        {
            set { _addressone = value; }
            get { return _addressone; }
        }
        /// <summary>
        /// 地址2
        /// </summary>
        public string AddressTwo
        {
            set { _addresstwo = value; }
            get { return _addresstwo; }
        }
        /// <summary>
        /// 网址
        /// </summary>
        public string WebAddress
        {
            set { _webaddress = value; }
            get { return _webaddress; }
        }
        /// <summary>
        /// 合作方式
        /// </summary>
        public string Cooperate
        {
            set { _cooperate = value; }
            get { return _cooperate; }
        }
        /// <summary>
        /// 热线1
        /// </summary>
        public string PhoneOne
        {
            set { _phoneone = value; }
            get { return _phoneone; }
        }
        /// <summary>
        /// 热线2
        /// </summary>
        public string PhoneTwo
        {
            set { _phonetwo = value; }
            get { return _phonetwo; }
        }
        /// <summary>
        /// 媒体LOGO
        /// </summary>
        public string AgencyLogo
        {
            set { _agencylogo = value; }
            get { return _agencylogo; }
        }
        /// <summary>
        /// 机构简介
        /// </summary>
        public string AgencyIntro
        {
            set { _agencyintro = value; }
            get { return _agencyintro; }
        }
        /// <summary>
        /// 英文简介
        /// </summary>
        public string EngIntro
        {
            set { _engintro = value; }
            get { return _engintro; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string TopicName
        {
            set { _topicname = value; }
            get { return _topicname; }
        }
        /// <summary>
        /// 栏目类型
        /// </summary>
        public int TopicProperty
        {
            set { _topicproperty = value; }
            get { return _topicproperty; }
        }
        /// <summary>
        /// 覆盖范围
        /// </summary>
        public string OverrideRange
        {
            set { _overriderange = value; }
            get { return _overriderange; }
        }
        /// <summary>
        /// 频道网址
        /// </summary>
        public string ChannelWebAddress
        {
            set { _channelwebaddress = value; }
            get { return _channelwebaddress; }
        }
        /// <summary>
        /// 国家id
        /// </summary>
        public int countryid
        {
            set { _countryid = value; }
            get { return _countryid; }
        }
        /// <summary>
        /// 省id

        /// </summary>
        public int provinceid
        {
            set { _provinceid = value; }
            get { return _provinceid; }
        }
        /// <summary>
        /// 城市id

        /// </summary>
        public int cityid
        {
            set { _cityid = value; }
            get { return _cityid; }
        }
        /// <summary>
        /// 删除标记
        /// </summary>
        public int del
        {
            set { _del = value; }
            get { return _del; }
        }
        /// <summary>
        /// 媒体类别
        /// </summary>
        public string MediaType
        {
            set { _mediatype = value; }
            get { return _mediatype; }
        }
        /// <summary>
        /// 地址1省id

        /// </summary>
        public int addr1_provinceid
        {
            set { _addr1_provinceid = value; }
            get { return _addr1_provinceid; }
        }
        /// <summary>
        /// 地址1城市id

        /// </summary>
        public int Agency
        {
            set { _agency = value; }
            get { return _agency; }
        }
        /// <summary>
        /// 地址1国家id
        /// </summary>
        public int addr1_countryid
        {
            set { _addr1_countryid = value; }
            get { return _addr1_countryid; }
        }
        /// <summary>
        /// 地址2省id
        /// </summary>
        public int addr2_provinceid
        {
            set { _addr2_provinceid = value; }
            get { return _addr2_provinceid; }
        }
        /// <summary>
        /// 地址2城市id
        /// </summary>
        public int addr2_cityid
        {
            set { _addr2_cityid = value; }
            get { return _addr2_cityid; }
        }
        /// <summary>
        /// 地址2国家id
        /// </summary>
        public int addr2_countryid
        {
            set { _addr2_countryid = value; }
            get { return _addr2_countryid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PostCode
        {
            set { _postcode = value; }
            get { return _postcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int MediaID
        {
            set { _mediaid = value; }
            get { return _mediaid; }
        }

        /// <summary>
        /// 地域属性ID
        /// </summary>
        public int RegionAttribute
        {
            set { _regionattribute = value; }
            get { return _regionattribute; }
        }
        #endregion Model

    }
}
