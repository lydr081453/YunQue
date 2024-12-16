using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    /// <summary>
    /// 实体类SC_SupplierAnnex 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class SC_SupplierAnnex
    {
        public SC_SupplierAnnex()
        { }
        #region Model
        private int _supplierid;
        private string _legal;
        private int? _officearea;
        private string _licenseannexfile;
        private string _coveredcities;
        private string _branchcontact;
        private string _listed;
        private string _background;
        private string _litigate;
        private string _plans;
        private string _revenue;
        private string _serviceproviders;
        private string _business;
        private string _staff;
        private int? _lengthyears;
        private string _differentiatedproducts;
        private string _location;
        private string _corecompetence;
        private string _servicefeatures;
        private string _addedservices;
        private string _servicesprofile;
        private string _importantcustomer;
        private string _top3;
        private string _servicecase;
        private string _coststructure;
        private string _payment;
        private string _accountname;
        private string _bankname;
        private string _accountnum;
        private string _paymentcycles;
        private string _loan;
        private string _vatinvoices;
        private string _businessgoals;
        private string _leaderbackground;
        private string _organizationfile;
        private string _workflowfile;
        private string _financialrelations;
        private string _stafftraining;
        private string _projectevaluation;
        private string _qualitycontrol;
        private string _cycle;
        private string _costsavings;
        private string _satisfaction;
        private string _servicescertification;
        private string _suppliersdistribution;
        private string _suppliermanagement;
        private string _erp;
        private string _enterprisemail;
        private string _creatorip;
        private DateTime? _createrdate;
        private string _updaterip;
        private DateTime? _updatedate;
        private int? _type;
        private int? _status;
        private int? _version;
        private string _industrytype;


        public string IndustryType
        {
            set { _industrytype = value; }
            get { return _industrytype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int supplierId
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        /// <summary>
        /// 一般信息_法人代表
        /// </summary>
        public string legal
        {
            set { _legal = value; }
            get { return _legal; }
        }
        /// <summary>
        /// 一般信息_办公室面积
        /// </summary>
        public int? officeArea
        {
            set { _officearea = value; }
            get { return _officearea; }
        }
        /// <summary>
        /// 一般信息_营业执照税务等附件(###分隔)
        /// </summary>
        public string licenseAnnexFile
        {
            set { _licenseannexfile = value; }
            get { return _licenseannexfile; }
        }
        /// <summary>
        /// 一般信息_覆盖城市
        /// </summary>
        public string coveredCities
        {
            set { _coveredcities = value; }
            get { return _coveredcities; }
        }
        /// <summary>
        /// 一般信息_分公司联系方式(xml)
        /// </summary>
        public string branchContact
        {
            set { _branchcontact = value; }
            get { return _branchcontact; }
        }
        /// <summary>
        /// 行业背景_是否上市公司
        /// </summary>
        public string listed
        {
            set { _listed = value; }
            get { return _listed; }
        }
        /// <summary>
        /// 行业背景_一般信息_公司背景
        /// </summary>
        public string background
        {
            set { _background = value; }
            get { return _background; }
        }
        /// <summary>
        /// 行业背景_诉讼
        /// </summary>
        public string litigate
        {
            set { _litigate = value; }
            get { return _litigate; }
        }
        /// <summary>
        /// 行业背景_近两年是否有合并、重组或者破产等情况发生或计划
        /// </summary>
        public string plans
        {
            set { _plans = value; }
            get { return _plans; }
        }
        /// <summary>
        /// 行业背景_最近三年的营业收入(###分隔)
        /// </summary>
        public string revenue
        {
            set { _revenue = value; }
            get { return _revenue; }
        }
        /// <summary>
        /// 行业背景_曾经或目前为行业内的其他公司提供服务,如是请列出公司名称
        /// </summary>
        public string serviceProviders
        {
            set { _serviceproviders = value; }
            get { return _serviceproviders; }
        }
        /// <summary>
        /// 行业背景_是否曾经与我公司有过业务往来
        /// </summary>
        public string business
        {
            set { _business = value; }
            get { return _business; }
        }
        /// <summary>
        /// 行业背景_管理层及股东中有无我公司员工
        /// </summary>
        public string staff
        {
            set { _staff = value; }
            get { return _staff; }
        }
        /// <summary>
        /// 行业背景_从事现经营行业的年限
        /// </summary>
        public int? lengthYears
        {
            set { _lengthyears = value; }
            get { return _lengthyears; }
        }
        /// <summary>
        /// 行业背景_区别于行业内其他公司的产品与服务的特点与优势
        /// </summary>
        public string differentiatedProducts
        {
            set { _differentiatedproducts = value; }
            get { return _differentiatedproducts; }
        }
        /// <summary>
        /// 行业背景_目标客户群定位
        /// </summary>
        public string location
        {
            set { _location = value; }
            get { return _location; }
        }
        /// <summary>
        /// 行业背景_核心竞争力
        /// </summary>
        public string coreCompetence
        {
            set { _corecompetence = value; }
            get { return _corecompetence; }
        }
        /// <summary>
        /// 产品与服务_产品/服务的特点
        /// </summary>
        public string serviceFeatures
        {
            set { _servicefeatures = value; }
            get { return _servicefeatures; }
        }
        /// <summary>
        /// 产品与服务_其他增值服务
        /// </summary>
        public string addedServices
        {
            set { _addedservices = value; }
            get { return _addedservices; }
        }
        /// <summary>
        /// 产品与服务_客服简介
        /// </summary>
        public string servicesProfile
        {
            set { _servicesprofile = value; }
            get { return _servicesprofile; }
        }
        /// <summary>
        /// 产业与服务_重要客户介绍
        /// </summary>
        public string importantCustomer
        {
            set { _importantcustomer = value; }
            get { return _importantcustomer; }
        }
        /// <summary>
        /// 产品与服务_top 3 客户业务量百分比(###分隔)
        /// </summary>
        public string top3
        {
            set { _top3 = value; }
            get { return _top3; }
        }
        /// <summary>
        /// 产品与服务_成功案例(###分隔)
        /// </summary>
        public string serviceCase
        {
            set { _servicecase = value; }
            get { return _servicecase; }
        }
        /// <summary>
        /// 支付条件_成本构成
        /// </summary>
        public string costStructure
        {
            set { _coststructure = value; }
            get { return _coststructure; }
        }
        /// <summary>
        /// 支付条件_支付方式
        /// </summary>
        public string payment
        {
            set { _payment = value; }
            get { return _payment; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string accountName
        {
            set { _accountname = value; }
            get { return _accountname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string bankName
        {
            set { _bankname = value; }
            get { return _bankname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string accountNum
        {
            set { _accountnum = value; }
            get { return _accountnum; }
        }
        /// <summary>
        /// 支付条件_支付周期
        /// </summary>
        public string paymentCycles
        {
            set { _paymentcycles = value; }
            get { return _paymentcycles; }
        }
        /// <summary>
        /// 支付条件_是否可以垫付
        /// </summary>
        public string loan
        {
            set { _loan = value; }
            get { return _loan; }
        }
        /// <summary>
        /// 支付条件_是否可开增值税发票
        /// </summary>
        public string VATinvoices
        {
            set { _vatinvoices = value; }
            get { return _vatinvoices; }
        }
        /// <summary>
        /// 管理职责_商业目标
        /// </summary>
        public string businessGoals
        {
            set { _businessgoals = value; }
            get { return _businessgoals; }
        }
        /// <summary>
        /// 管理职责_负责人背景简介
        /// </summary>
        public string leaderBackground
        {
            set { _leaderbackground = value; }
            get { return _leaderbackground; }
        }
        /// <summary>
        /// 管理职责_公司组织机构图附件
        /// </summary>
        public string organizationFile
        {
            set { _organizationfile = value; }
            get { return _organizationfile; }
        }
        /// <summary>
        /// 管理职责_工作流程图附件
        /// </summary>
        public string workflowFile
        {
            set { _workflowfile = value; }
            get { return _workflowfile; }
        }
        /// <summary>
        /// 管理职责_总公司与分公司的财务关系
        /// </summary>
        public string financialRelations
        {
            set { _financialrelations = value; }
            get { return _financialrelations; }
        }
        /// <summary>
        /// 管理职责_是否有员工培训及发展计划
        /// </summary>
        public string staffTraining
        {
            set { _stafftraining = value; }
            get { return _stafftraining; }
        }
        /// <summary>
        /// 管理职责_贵公司是否有内部的项目管理机制
        /// </summary>
        public string projectEvaluation
        {
            set { _projectevaluation = value; }
            get { return _projectevaluation; }
        }
        /// <summary>
        /// 管理职责_是否有标准的质量管控体系
        /// </summary>
        public string qualityControl
        {
            set { _qualitycontrol = value; }
            get { return _qualitycontrol; }
        }
        /// <summary>
        /// 管理职责_是否有标准的生产周期
        /// </summary>
        public string cycle
        {
            set { _cycle = value; }
            get { return _cycle; }
        }
        /// <summary>
        /// 管理职责_是否有标准的成本节约管理体系
        /// </summary>
        public string costSavings
        {
            set { _costsavings = value; }
            get { return _costsavings; }
        }
        /// <summary>
        /// 管理职责_是否有标准的客户满意度调查及评估体系
        /// </summary>
        public string satisfaction
        {
            set { _satisfaction = value; }
            get { return _satisfaction; }
        }
        /// <summary>
        /// 管理职责_是否已经获得任何行业及服务认证
        /// </summary>
        public string servicesCertification
        {
            set { _servicescertification = value; }
            get { return _servicescertification; }
        }
        /// <summary>
        /// 管理职责_供应商的分布地区
        /// </summary>
        public string suppliersDistribution
        {
            set { _suppliersdistribution = value; }
            get { return _suppliersdistribution; }
        }
        /// <summary>
        /// 管理职责_是否有对供应商的管理制度及流程
        /// </summary>
        public string supplierManagement
        {
            set { _suppliermanagement = value; }
            get { return _suppliermanagement; }
        }
        /// <summary>
        /// 管理职责_是否运用ERP,SAP等生产或财务系统
        /// </summary>
        public string ERP
        {
            set { _erp = value; }
            get { return _erp; }
        }
        /// <summary>
        /// 管理职责_是否有企业邮箱
        /// </summary>
        public string enterpriseMail
        {
            set { _enterprisemail = value; }
            get { return _enterprisemail; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string creatorIP
        {
            set { _creatorip = value; }
            get { return _creatorip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? createrDate
        {
            set { _createrdate = value; }
            get { return _createrdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string updaterIP
        {
            set { _updaterip = value; }
            get { return _updaterip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? updateDate
        {
            set { _updatedate = value; }
            get { return _updatedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? version
        {
            set { _version = value; }
            get { return _version; }
        }
        #endregion Model

    }
}
