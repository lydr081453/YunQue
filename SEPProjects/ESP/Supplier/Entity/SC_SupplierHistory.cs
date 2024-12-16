using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_SupplierHistory
    {
        public SC_SupplierHistory()
        { }
        #region Model
        private int _id;
        private int _supplierid;
        private int _historyedition;
        private byte[] _historyinfomation;
        private string _logname;
        private string _password;
        private string _supplier_name;
        private string _supplier_nameen;
        private string _supplier_sn;
        private int _supplier_area;
        private string _supplier_province;
        private string _supplier_city;
        private int _supplier_industry;
        private decimal _supplier_scale;
        private decimal _supplier_principal;
        private int _supplier_property;
        private string _supplier_builttime;
        private string _supplier_website;
        private int _supplier_source;
        private string _supplier_intro;
        private string _contact_fax;
        private string _contact_tel;
        private string _contact_mobile;
        private string _contact_email;
        private string _contact_address;
        private string _contact_zip;
        private string _contact_msn;
        private string _service_content;
        private string _service_area;
        private string _service_workamount;
        private string _service_customization;
        private string _service_ohter;
        private string _business_price;
        private string _business_paytime;
        private string _business_prepay;
        private string _evaluation_department;
        private string _evaluation_level;
        private string _evaluation_feedback;
        private string _evaluation_note;
        private string _account_name;
        private string _account_bank;
        private string _account_number;
        private string _introfile;
        private string _productfile;
        private string _pricefile;
        private int _filialeamount;
        private string _filialeaddress;
        private int _credit;
        private int _cachet;
        private int _money;
        private int _isperson;
        private DateTime _creattime;
        private string _creatip;
        private DateTime _lastupdatetime;
        private string _lastupdateip;
        private int _type;
        private int _status;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Supplierid
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int HistoryEdition
        {
            set { _historyedition = value; }
            get { return _historyedition; }
        }
        /// <summary>
        /// 
        /// </summary>
        public byte[] HistoryInfomation
        {
            set { _historyinfomation = value; }
            get { return _historyinfomation; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LogName
        {
            set { _logname = value; }
            get { return _logname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string supplier_name
        {
            set { _supplier_name = value; }
            get { return _supplier_name; }
        }
        /// <summary>
        /// 名称(英文)
        /// </summary>
        public string supplier_nameEN
        {
            set { _supplier_nameen = value; }
            get { return _supplier_nameen; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string supplier_sn
        {
            set { _supplier_sn = value; }
            get { return _supplier_sn; }
        }
        /// <summary>
        /// 基础信息-所在地区
        /// </summary>
        public int supplier_area
        {
            set { _supplier_area = value; }
            get { return _supplier_area; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string supplier_province
        {
            set { _supplier_province = value; }
            get { return _supplier_province; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string supplier_city
        {
            set { _supplier_city = value; }
            get { return _supplier_city; }
        }
        /// <summary>
        /// 基础信息-行业
        /// </summary>
        public int supplier_industry
        {
            set { _supplier_industry = value; }
            get { return _supplier_industry; }
        }
        /// <summary>
        /// 基础信息-规模

        /// </summary>
        public decimal supplier_scale
        {
            set { _supplier_scale = value; }
            get { return _supplier_scale; }
        }
        /// <summary>
        /// 基础信息-注册资本
        /// </summary>
        public decimal supplier_principal
        {
            set { _supplier_principal = value; }
            get { return _supplier_principal; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int supplier_property
        {
            set { _supplier_property = value; }
            get { return _supplier_property; }
        }
        /// <summary>
        /// 基础信息-成立年限
        /// </summary>
        public string supplier_builttime
        {
            set { _supplier_builttime = value; }
            get { return _supplier_builttime; }
        }
        /// <summary>
        /// 基础信息-网址
        /// </summary>
        public string supplier_website
        {
            set { _supplier_website = value; }
            get { return _supplier_website; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int supplier_source
        {
            set { _supplier_source = value; }
            get { return _supplier_source; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string supplier_Intro
        {
            set { _supplier_intro = value; }
            get { return _supplier_intro; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string contact_fax
        {
            set { _contact_fax = value; }
            get { return _contact_fax; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string contact_Tel
        {
            set { _contact_tel = value; }
            get { return _contact_tel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string contact_Mobile
        {
            set { _contact_mobile = value; }
            get { return _contact_mobile; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string contact_Email
        {
            set { _contact_email = value; }
            get { return _contact_email; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string contact_address
        {
            set { _contact_address = value; }
            get { return _contact_address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string contact_ZIP
        {
            set { _contact_zip = value; }
            get { return _contact_zip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string contact_msn
        {
            set { _contact_msn = value; }
            get { return _contact_msn; }
        }
        /// <summary>
        /// 产品服务信息-主要服务内容
        /// </summary>
        public string service_content
        {
            set { _service_content = value; }
            get { return _service_content; }
        }
        /// <summary>
        /// 产品服务信息-服务覆盖区域
        /// </summary>
        public string service_area
        {
            set { _service_area = value; }
            get { return _service_area; }
        }
        /// <summary>
        /// 产品服务信息-可同时承接的工作量
        /// </summary>
        public string service_workamount
        {
            set { _service_workamount = value; }
            get { return _service_workamount; }
        }
        /// <summary>
        /// 产品服务信息-定制服务
        /// </summary>
        public string service_customization
        {
            set { _service_customization = value; }
            get { return _service_customization; }
        }
        /// <summary>
        /// 产品服务信息-其他
        /// </summary>
        public string service_ohter
        {
            set { _service_ohter = value; }
            get { return _service_ohter; }
        }
        /// <summary>
        /// 商务条款-参考报价（附件形式）
        /// </summary>
        public string business_price
        {
            set { _business_price = value; }
            get { return _business_price; }
        }
        /// <summary>
        /// 商务条款-账期
        /// </summary>
        public string business_paytime
        {
            set { _business_paytime = value; }
            get { return _business_paytime; }
        }
        /// <summary>
        /// 商务条款-预付款
        /// </summary>
        public string business_prepay
        {
            set { _business_prepay = value; }
            get { return _business_prepay; }
        }
        /// <summary>
        /// 评估信息-采购部评价
        /// </summary>
        public string evaluation_department
        {
            set { _evaluation_department = value; }
            get { return _evaluation_department; }
        }
        /// <summary>
        /// 评估信息-推荐等级
        /// </summary>
        public string evaluation_level
        {
            set { _evaluation_level = value; }
            get { return _evaluation_level; }
        }
        /// <summary>
        /// 评估信息-业务反馈
        /// </summary>
        public string evaluation_feedback
        {
            set { _evaluation_feedback = value; }
            get { return _evaluation_feedback; }
        }
        /// <summary>
        /// 评估信息-备注
        /// </summary>
        public string evaluation_note
        {
            set { _evaluation_note = value; }
            get { return _evaluation_note; }
        }
        /// <summary>
        /// 帐户信息-开户公司名称
        /// </summary>
        public string account_name
        {
            set { _account_name = value; }
            get { return _account_name; }
        }
        /// <summary>
        /// 帐户信息-开户银行

        /// </summary>
        public string account_bank
        {
            set { _account_bank = value; }
            get { return _account_bank; }
        }
        /// <summary>
        /// 帐户信息-帐号
        /// </summary>
        public string account_number
        {
            set { _account_number = value; }
            get { return _account_number; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string introfile
        {
            set { _introfile = value; }
            get { return _introfile; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string productfile
        {
            set { _productfile = value; }
            get { return _productfile; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string pricefile
        {
            set { _pricefile = value; }
            get { return _pricefile; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int filialeamount
        {
            set { _filialeamount = value; }
            get { return _filialeamount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string filialeaddress
        {
            set { _filialeaddress = value; }
            get { return _filialeaddress; }
        }
        /// <summary>
        /// 信誉点
        /// </summary>
        public int Credit
        {
            set { _credit = value; }
            get { return _credit; }
        }
        /// <summary>
        /// 威望
        /// </summary>
        public int Cachet
        {
            set { _cachet = value; }
            get { return _cachet; }
        }
        /// <summary>
        /// 金钱
        /// </summary>
        public int Money
        {
            set { _money = value; }
            get { return _money; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsPerson
        {
            set { _isperson = value; }
            get { return _isperson; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatTime
        {
            set { _creattime = value; }
            get { return _creattime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreatIP
        {
            set { _creatip = value; }
            get { return _creatip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastUpdateTime
        {
            set { _lastupdatetime = value; }
            get { return _lastupdatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LastUpdateIP
        {
            set { _lastupdateip = value; }
            get { return _lastupdateip; }
        }
        /// <summary>
        /// 联系人类型
        /// </summary>
        public int Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 联系人状态
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        #endregion Model

    }
}
