using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类CustomerInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class CustomerInfo
    {
        public CustomerInfo()
        { }
        #region Model
        private int _customerid;
        private string _addresscode;
        private string _invoicetitle;
        private int? _areaid;
        private string _areacode;
        private string _areaname;
        private string _address1;
        private string _address2;
        private int? _industryid;
        private string _industrycode;
        private string _industryname;
        private string _customercode;
        private string _scale;
        private string _principal;
        private string _builttime;
        private string _contactname;
        private string _contactposition;
        private string _contacttel;
        private string _contactfax;
        private string _website;
        private string _contactmobile;
        private string _contactemail;
        
        private string _postcode;
        private string _accountname;
        private string _accountbank;
        private string _accountnumber;
        private string _remark;
        private string _logourl;
        private DateTime? _appdate;
        private string _appcompany;
        private byte[] _lastupdatetime;

        private string _namecn1;
        private string _namecn2;
        private string _nameen1;
        private string _nameen2;
        private string _shortcn;
        private string _shorten;
        private string _ao;
        /// <summary>
        /// 流水号
        /// </summary>
        public int CustomerID
        {
            set { _customerid = value; }
            get { return _customerid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AddressCode
        {
            set { _addresscode = value; }
            get { return _addresscode; }
        }
        /// <summary>
        /// 发票抬头
        /// </summary>
        public string InvoiceTitle
        {
            set { _invoicetitle = value; }
            get { return _invoicetitle; }
        }
        /// <summary>
        /// 所在地区ID
        /// </summary>
        public int? AreaID
        {
            set { _areaid = value; }
            get { return _areaid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AreaCode
        {
            set { _areacode = value; }
            get { return _areacode; }
        }
        /// <summary>
        /// 所在地区名称
        /// </summary>
        public string AreaName
        {
            set { _areaname = value; }
            get { return _areaname; }
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address1
        {
            set { _address1 = value; }
            get { return _address1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Address2
        {
            set { _address2 = value; }
            get { return _address2; }
        }
        /// <summary>
        /// 客户所在行业ID
        /// </summary>
        public int? IndustryID
        {
            set { _industryid = value; }
            get { return _industryid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IndustryCode
        {
            set { _industrycode = value; }
            get { return _industrycode; }
        }
        /// <summary>
        /// 所在行业名称
        /// </summary>
        public string IndustryName
        {
            set { _industryname = value; }
            get { return _industryname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CustomerCode
        {
            set { _customercode = value; }
            get { return _customercode; }
        }
        /// <summary>
        /// 基础信息-规模

        /// </summary>
        public string Scale
        {
            set { _scale = value; }
            get { return _scale; }
        }
        /// <summary>
        /// 基础信息-注册资本
        /// </summary>
        public string Principal
        {
            set { _principal = value; }
            get { return _principal; }
        }
        /// <summary>
        /// 基础信息-成立年限
        /// </summary>
        public string Builttime
        {
            set { _builttime = value; }
            get { return _builttime; }
        }
        /// <summary>
        /// 联系信息-联系人姓名
        /// </summary>
        public string ContactName
        {
            set { _contactname = value; }
            get { return _contactname; }
        }
        /// <summary>
        /// 联系信息-联系人职务
        /// </summary>
        public string ContactPosition
        {
            set { _contactposition = value; }
            get { return _contactposition; }
        }
        /// <summary>
        /// 联系信息-固话
        /// </summary>
        public string ContactTel
        {
            set { _contacttel = value; }
            get { return _contacttel; }
        }
        /// <summary>
        /// 联系信息-传真
        /// </summary>
        public string ContactFax
        {
            set { _contactfax = value; }
            get { return _contactfax; }
        }
        /// <summary>
        /// 联系信息-网址
        /// </summary>
        public string Website
        {
            set { _website = value; }
            get { return _website; }
        }
        /// <summary>
        /// 联系信息-移动电话
        /// </summary>
        public string ContactMobile
        {
            set { _contactmobile = value; }
            get { return _contactmobile; }
        }
        /// <summary>
        /// 联系信息-Email
        /// </summary>
        public string ContactEmail
        {
            set { _contactemail = value; }
            get { return _contactemail; }
        }
        /// <summary>
        /// 客户中文名称
        /// </summary>
        public string NameCN1
        {
            set { _namecn1 = value; }
            get { return _namecn1; }
        }
        /// <summary>
        /// 邮编
        /// </summary>
        public string PostCode
        {
            set { _postcode = value; }
            get { return _postcode; }
        }
        /// <summary>
        /// 帐户信息-开户公司名称
        /// </summary>
        public string AccountName
        {
            set { _accountname = value; }
            get { return _accountname; }
        }
        /// <summary>
        /// 帐户信息-开户银行

        /// </summary>
        public string AccountBank
        {
            set { _accountbank = value; }
            get { return _accountbank; }
        }
        /// <summary>
        /// 帐户信息-帐号
        /// </summary>
        public string AccountNumber
        {
            set { _accountnumber = value; }
            get { return _accountnumber; }
        }
        /// <summary>
        /// 其他信息-备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LogoUrl
        {
            set { _logourl = value; }
            get { return _logourl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? AppDate
        {
            set { _appdate = value; }
            get { return _appdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AppCompany
        {
            set { _appcompany = value; }
            get { return _appcompany; }
        }
        /// <summary>
        /// 时间戳
        /// </summary>
        public byte[] Lastupdatetime
        {
            set { _lastupdatetime = value; }
            get { return _lastupdatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NameCN2
        {
            set { _namecn2 = value; }
            get { return _namecn2; }
        }
        /// <summary>
        /// 客户英文名称
        /// </summary>
        public string NameEN1
        {
            set { _nameen1 = value; }
            get { return _nameen1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NameEN2
        {
            set { _nameen2 = value; }
            get { return _nameen2; }
        }
        /// <summary>
        /// 客户中文简称

        /// </summary>
        public string ShortCN
        {
            set { _shortcn = value; }
            get { return _shortcn; }
        }
        /// <summary>
        /// 客户英文简称
        /// </summary>
        public string ShortEN
        {
            set { _shorten = value; }
            get { return _shorten; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AO
        {
            set { _ao = value; }
            get { return _ao; }
        }

        /// <summary>
        /// 返点比例
        /// </summary>
        public decimal? RebateRate { get; set; }

        #endregion Model

    }

    public partial class CustomerInfo
    {
        public string FullNameCN
        {
            get { return NameCN1 + " " + NameCN2; }
        }

        public string FullNameEN
        {
            get { return NameEN1 + " " + NameEN2; }
        }

        public string FullAreaName
        {
            get { return AreaCode + " " + AreaName; }
        }

        public string FullIndustryName
        {
            get { return IndustryCode + " " + IndustryName; }
        }



        //private decimal? _defaultTaxRate;// 默认税率


        ///// <summary>
        ///// 默认税率
        ///// </summary>
        //public decimal? DefaultTaxRate
        //{
        //    get { return _defaultTaxRate; }
        //    set { _defaultTaxRate = value; }
        //}





        int? _isProxy;

        public int? IsProxy
        {
            get { return _isProxy; }
            set { _isProxy = value; }
        }


        int _creatorID;

        public int CreatorID
        {
            get { return _creatorID; }
            set { _creatorID = value; }
        }

        string _creatorName;

        public string CreatorName
        {
            get { return _creatorName; }
            set { _creatorName = value; }
        }

        string _creatorCode;

        public string CreatorCode
        {
            get { return _creatorCode; }
            set { _creatorCode = value; }
        }

        string _creatorUserID;

        public string CreatorUserID
        {
            get { return _creatorUserID; }
            set { _creatorUserID = value; }
        }

        DateTime? _createdate;

        public DateTime? Createdate
        {
            get { return _createdate; }
            set { _createdate = value; }
        }



    }
}