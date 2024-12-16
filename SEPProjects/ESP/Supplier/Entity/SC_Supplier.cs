using System;
using System.Data;

namespace ESP.Supplier.Entity
{
    public class SC_Supplier
    {
        #region Model
        private int _id;
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
        private string _regaddress;
        private string _knowWays;
        private string _reviewers;
        private string _supplier_code;
        private string _auditorid;
        private string _auditorname;
        private string _username;

        private string _qualificationAuditorId;
        private string _qualificationAuditorName;
        private string _qualificationusername;
        private int _issendmail;
        public string InvoiceTitle { get; set; }

        public string LinkRemark { get; set; }

        public int PriceLevel { get; set; }
        /// <summary>
        /// 审核备注
        /// </summary>
        public string AuditRemark { get; set; }

        public int issendmail
        {
            set { _issendmail = value; }
            get { return _issendmail; }
        }

        /// <summary>
        /// 资质审批人ID
        /// </summary>
        public string QualificationAuditorId
        {
            set { _qualificationAuditorId = value; }
            get { return _qualificationAuditorId; }
        }
        /// <summary>
        /// 资质审批人的名字
        /// </summary>
        public string QualificationAuditorName
        {
            set { _qualificationAuditorName = value; }
            get { return _qualificationAuditorName; }
        }
        /// <summary>
        /// 审批人的中文名字
        /// </summary>
        public string QualificationUserName
        {
            set { _qualificationusername = value; }
            get { return _qualificationusername; }
        }

        /// <summary>
        /// 审批人的中文名字
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        ///// <summary>
        ///// 供应商编号
        ///// </summary>
        //public string supplier_code
        //{
        //    set { _supplier_code = value; }
        //    get { return _supplier_code; }
        //}


        /// <summary>
        /// 
        /// </summary>
        public string AuditorId
        {
            set { _auditorid = value; }
            get { return _auditorid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AuditorName
        {
            set { _auditorname = value; }
            get { return _auditorname; }
        }

        /// <summary>
        /// 供应商编号
        /// </summary>
        public string Supplier_code
        {
            set { _supplier_code = value; }
            get { return _supplier_code; }
        }
        /// <summary>
        /// 资质审核人
        /// </summary>
        public string Reviewers
        {
            set { _reviewers = value; }
            get { return _reviewers; }
        }
        /// <summary>
        /// 了解该网站途径
        /// </summary>
        public string KnowWays
        {
            set { _knowWays = value; }
            get { return _knowWays; }
        }
        /// <summary>
        /// 注册地址
        /// </summary>
        public string RegAddress
        {
            set { _regaddress = value; }
            get { return _regaddress; }
        }
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
        /// 公司名称(中文)
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
        /// 所有权属性：(私营, 合资, 独资, 国企)
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
        /// 公司简介（可上传附件）
        /// </summary>
        public string introfile
        {
            set { _introfile = value; }
            get { return _introfile; }
        }
        /// <summary>
        /// 产品/服务介绍（可上传文档及图片）
        /// </summary>
        public string productfile
        {
            set { _productfile = value; }
            get { return _productfile; }
        }
        /// <summary>
        /// 报价（选填）（可上传附件）
        /// </summary>
        public string pricefile
        {
            set { _pricefile = value; }
            get { return _pricefile; }
        }
        /// <summary>
        /// 分公司数量
        /// </summary>
        public int filialeamount
        {
            set { _filialeamount = value; }
            get { return _filialeamount; }
        }
        /// <summary>
        /// 分公司所在地
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
        public bool IsGroup { get; set; }

        #endregion Model

        public void PopupData(IDataReader r)
        {

            if (null != r["id"] && r["id"].ToString() != "")
            {
                id = int.Parse(r["id"].ToString());
            }

            LogName = r["logname"].ToString();
            Password = r["password"].ToString();
            supplier_name = r["supplier_name"].ToString();
            supplier_nameEN = r["supplier_nameen"].ToString();
            supplier_sn = r["supplier_sn"].ToString();

            if (null != r["supplier_area"] && r["supplier_area"].ToString() != "")
            {
                supplier_area = int.Parse(r["supplier_area"].ToString());
            }
            if (null != r["supplier_province"] && r["supplier_province"].ToString() != "")
            {
                supplier_province = (r["supplier_province"].ToString());
            }
            if (null != r["supplier_city"] && r["supplier_city"].ToString() != "")
            {
                supplier_city = (r["supplier_city"].ToString());
            }
            if (null != r["supplier_industry"] && r["supplier_industry"].ToString() != "")
            {
                supplier_industry = int.Parse(r["supplier_industry"].ToString());
            }
            if (null != r["supplier_scale"] && r["supplier_scale"].ToString() != "")
            {
                supplier_scale = decimal.Parse(r["supplier_scale"].ToString());
            }
            if (null != r["supplier_principal"] && r["supplier_principal"].ToString() != "")
            {
                supplier_principal = decimal.Parse(r["supplier_principal"].ToString());
            }
            if (null != r["supplier_property"] && r["supplier_property"].ToString() != "")
            {
                supplier_property = int.Parse(r["supplier_property"].ToString());
            }

            supplier_builttime = r["supplier_builttime"].ToString();
            supplier_website = r["supplier_website"].ToString();

            if (null != r["supplier_source"] && r["supplier_source"].ToString() != "")
            {
                supplier_source = int.Parse(r["supplier_source"].ToString());
            }
            supplier_Intro = r["supplier_intro"].ToString();
            contact_fax = r["contact_fax"].ToString();
            contact_Tel = r["contact_tel"].ToString();
            contact_Mobile = r["contact_mobile"].ToString();
            contact_Email = r["contact_email"].ToString();
            contact_address = r["contact_address"].ToString();
            contact_ZIP = r["contact_ZIP"].ToString();
            contact_msn = r["contact_msn"].ToString();
            service_content = r["service_content"].ToString();
            service_area = r["service_area"].ToString();
            service_workamount = r["service_workamount"].ToString();
            service_customization = r["service_customization"].ToString();
            service_ohter = r["service_ohter"].ToString();
            business_price = r["business_price"].ToString();
            business_paytime = r["business_paytime"].ToString();
            business_prepay = r["business_prepay"].ToString();
            evaluation_department = r["evaluation_department"].ToString();
            evaluation_level = r["evaluation_level"].ToString();
            evaluation_feedback = r["evaluation_feedback"].ToString();
            evaluation_note = r["evaluation_note"].ToString();
            account_name = r["account_name"].ToString();
            account_bank = r["account_bank"].ToString();
            account_number = r["account_number"].ToString();

            introfile = r["introfile"].ToString();
            productfile = r["productfile"].ToString();
            pricefile = r["pricefile"].ToString();
            filialeaddress = r["filialeaddress"].ToString();

            if (null != r["filialeamount"] && r["filialeamount"].ToString() != "")
            {
                filialeamount = int.Parse(r["filialeamount"].ToString());
            }

            if (null != r["credit"] && r["credit"].ToString() != "")
            {
                Credit = int.Parse(r["credit"].ToString());
            }
            if (null != r["cachet"] && r["cachet"].ToString() != "")
            {
                Cachet = int.Parse(r["cachet"].ToString());
            }
            if (null != r["money"] && r["money"].ToString() != "")
            {
                Money = int.Parse(r["money"].ToString());
            }
            if (null != r["isperson"] && r["isperson"].ToString() != "")
            {
                IsPerson = int.Parse(r["isperson"].ToString());
            }

            if (null != r["creattime"] && r["creattime"].ToString() != "")
            {
                CreatTime = DateTime.Parse(r["creattime"].ToString());
            }
            if (null != r["lastupdatetime"] && r["lastupdatetime"].ToString() != "")
            {
                LastUpdateTime = DateTime.Parse(r["lastupdatetime"].ToString());
            }

            if (null != r["type"] && r["type"].ToString() != "")
            {
                Type = int.Parse(r["type"].ToString());
            }
            if (null != r["status"] && r["status"].ToString() != "")
            {
                Status = int.Parse(r["status"].ToString());
            }
            RegAddress = r["reg_address"].ToString();
            KnowWays = r["KnowWays"].ToString();
            Reviewers = r["Reviewers"].ToString();
            Supplier_code = r["supplier_code"].ToString();
            AuditorId = r["auditorId"].ToString();
            AuditorName = r["auditorname"].ToString();
            QualificationAuditorId = r["QualificationAuditorId"].ToString();
            QualificationAuditorName = r["QualificationAuditorName"].ToString();

            if (null != r["issendmail"] && r["issendmail"].ToString() != "")
            {
                issendmail = int.Parse(r["issendmail"].ToString());
            }
            LinkRemark = r["linkRemark"].ToString();
            if (null != r["isgroup"] && r["isgroup"].ToString() != "")
            {
                IsGroup = bool.Parse(r["isgroup"].ToString());
            }
            if (null != r["PriceLevel"] && r["PriceLevel"].ToString() != "")
            {
                PriceLevel = int.Parse(r["PriceLevel"].ToString());
            }
        }
    }

    public class SC_SupplierPriceFiles
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string Remark { get; set; }

        public void PopupData(IDataReader r)
        {

            if (null != r["Id"] && r["Id"].ToString() != "")
            {
                Id = int.Parse(r["Id"].ToString());
            }
            if (null != r["SupplierId"] && r["SupplierId"].ToString() != "")
            {
                Id = int.Parse(r["SupplierId"].ToString());
            }

            FileUrl = r["FileUrl"].ToString();
            FileName = r["FileName"].ToString();
            Remark = r["Remark"].ToString();
           
        }
    }
}
