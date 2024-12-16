using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Framework.Entity;
using System.Data;
namespace ESP.HumanResource.Entity
{
    public class SnapshotsInfo : ESP.Framework.Entity.EmployeeInfo
    {

        public SnapshotsInfo()
        { }
        #region Model
        private int _id;
        private string _education;
        private string _graduatedfrom;
        private string _major;
        private string _thisyearsalary;
        private int _status;
        private int _userid;
        private string _nowbasepay;
        private string _nowmeritpay;
        private string _newbasepay;
        private string _code;
        private string _newmeritpay;
        private int _creator;
        private DateTime _createdtime;
        private string _username;
        private string _creatorname;
        private DateTime _endowmentinsurancestartime = DateTime.Parse("1900-1-1 0:00:00");
        private DateTime _endowmentinsuranceendtime = DateTime.Parse("1900-1-1 0:00:00");
        private DateTime _unemploymentinsurancestartime = DateTime.Parse("1900-1-1 0:00:00");
        private DateTime _unemploymentinsuranceendtime = DateTime.Parse("1900-1-1 0:00:00");
        private DateTime _birthinsurancestartime = DateTime.Parse("1900-1-1 0:00:00");
        private int _typeid;
        private DateTime _birthinsuranceendtime = DateTime.Parse("1900-1-1 0:00:00");
        private DateTime _compoinsurancestartime = DateTime.Parse("1900-1-1 0:00:00");
        private DateTime _compoinsuranceendtime = DateTime.Parse("1900-1-1 0:00:00");
        private DateTime _medicalinsurancestartime = DateTime.Parse("1900-1-1 0:00:00");
        private DateTime _medicalinsuranceendtime = DateTime.Parse("1900-1-1 0:00:00");
        private DateTime _publicreservefundsstartime = DateTime.Parse("1900-1-1 0:00:00");
        private DateTime _publicreservefundsendtime = DateTime.Parse("1900-1-1 0:00:00");
        private DateTime _contractstartdate = DateTime.Parse("1900-1-1 0:00:00");
        private DateTime _contractenddate = DateTime.Parse("1900-1-1 0:00:00");
        private DateTime _probationenddate = DateTime.Parse("1900-1-1 0:00:00");
        private string _socialinsurancebase;
        private string _medicalinsurancebase;
        private string _publicreservefundsbase;
        private bool _isarchive;
        private string _contractsigninfo;
        private string _insuranceplace;

        private DateTime _birthday = DateTime.Parse("1900-1-1 0:00:00");
        private string _degree;
        private bool _isForeign;
        private bool _isCertification;
        private int _wageMonths;
        private decimal? _eiproportionoffirms;
        private decimal? _eiproportionofindividuals;
        private decimal? _uiproportionoffirms;
        private decimal? _uiproportionofindividuals;
        private decimal? _biproportionoffirms;
        private decimal? _biproportionofindividuals;
        private decimal? _ciproportionoffirms;
        private decimal? _ciproportionofindividuals;
        private decimal? _miproportionoffirms;
        private decimal? _miproportionofindividuals;
        private decimal? _mibigproportionofindividuals;
        private decimal? _prfproportionoffirms;
        private decimal? _prfproportionofindividuals;
        private string _eifirmscosts;
        private string _eiindividualscosts;
        private string _uifirmscosts;
        private string _uiindividualscosts;
        private string _bifirmscosts;
        private string _biindividualscosts;
        private string _cifirmscosts;
        private string _ciindividualscosts;
        private string _mifirmscosts;
        private string _miindividualscosts;
        private string _prfirmscosts;
        private string _priindividualscosts;
        private string _socialInsuranceCompany;

        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 学历
        /// </summary>
        new public string Education
        {
            set { _education = value; }
            get { return _education; }
        }
        /// <summary>
        /// 毕业院校
        /// </summary>
        public string GraduatedFrom
        {
            set { _graduatedfrom = value; }
            get { return _graduatedfrom; }
        }
        /// <summary>
        /// 专业/主修课程
        /// </summary>
        new public string Major
        {
            set { _major = value; }
            get { return _major; }
        }
        /// <summary>
        /// 本年度工资所属
        /// </summary>
        new public string ThisYearSalary
        {
            set { _thisyearsalary = value; }
            get { return _thisyearsalary; }
        }
        /// <summary>
        /// 员工状态
        /// </summary>
        new public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        new public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 现基本工资
        /// </summary>
        public string nowBasePay
        {
            set { _nowbasepay = value; }
            get { return _nowbasepay; }
        }
        /// <summary>
        /// 现绩效
        /// </summary>
        public string nowMeritPay
        {
            set { _nowmeritpay = value; }
            get { return _nowmeritpay; }
        }
        /// <summary>
        /// 新基本工资
        /// </summary>
        public string newBasePay
        {
            set { _newbasepay = value; }
            get { return _newbasepay; }
        }
        /// <summary>
        /// 员工编号
        /// </summary>
        new public string Code
        {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
        /// 新绩效
        /// </summary>
        public string newMeritPay
        {
            set { _newmeritpay = value; }
            get { return _newmeritpay; }
        }
        /// <summary>
        /// 
        /// </summary>
        new public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 
        /// </summary>
        new public DateTime CreatedTime
        {
            set { _createdtime = value; }
            get { return _createdtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        new public string CreatorName
        {
            set { _creatorname = value; }
            get { return _creatorname; }
        }
        /// <summary>
        /// 养老保险开始时间
        /// </summary>
        public DateTime endowmentInsuranceStarTime
        {
            set { _endowmentinsurancestartime = value; }
            get { return _endowmentinsurancestartime; }
        }
        /// <summary>
        /// 养老保险结束时间
        /// </summary>
        public DateTime endowmentInsuranceEndTime
        {
            set { _endowmentinsuranceendtime = value; }
            get { return _endowmentinsuranceendtime; }
        }
        /// <summary>
        /// 失业保险开始时间
        /// </summary>
        public DateTime unemploymentInsuranceStarTime
        {
            set { _unemploymentinsurancestartime = value; }
            get { return _unemploymentinsurancestartime; }
        }
        /// <summary>
        /// 失业保险结束时间
        /// </summary>
        public DateTime unemploymentInsuranceEndTime
        {
            set { _unemploymentinsuranceendtime = value; }
            get { return _unemploymentinsuranceendtime; }
        }
        /// <summary>
        /// 生育险开始时间
        /// </summary>
        public DateTime birthInsuranceStarTime
        {
            set { _birthinsurancestartime = value; }
            get { return _birthinsurancestartime; }
        }
        /// <summary>
        /// 员工类型ID（全职、兼职）
        /// </summary>
        new public int TypeID
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 生育险结束时间
        /// </summary>
        public DateTime birthInsuranceEndTime
        {
            set { _birthinsuranceendtime = value; }
            get { return _birthinsuranceendtime; }
        }
        /// <summary>
        /// 工伤险开始时间
        /// </summary>
        public DateTime compoInsuranceStarTime
        {
            set { _compoinsurancestartime = value; }
            get { return _compoinsurancestartime; }
        }
        /// <summary>
        /// 工伤险结束时间
        /// </summary>
        public DateTime compoInsuranceEndTime
        {
            set { _compoinsuranceendtime = value; }
            get { return _compoinsuranceendtime; }
        }
        /// <summary>
        /// 医疗保险开始时间
        /// </summary>
        public DateTime medicalInsuranceStarTime
        {
            set { _medicalinsurancestartime = value; }
            get { return _medicalinsurancestartime; }
        }
        /// <summary>
        /// 医疗保险结束时间
        /// </summary>
        public DateTime medicalInsuranceEndTime
        {
            set { _medicalinsuranceendtime = value; }
            get { return _medicalinsuranceendtime; }
        }
        /// <summary>
        /// 公积金开始时间
        /// </summary>
        public DateTime publicReserveFundsStarTime
        {
            set { _publicreservefundsstartime = value; }
            get { return _publicreservefundsstartime; }
        }
        /// <summary>
        /// 公积金结束时间
        /// </summary>
        public DateTime publicReserveFundsEndTime
        {
            set { _publicreservefundsendtime = value; }
            get { return _publicreservefundsendtime; }
        }
        /// <summary>
        /// 合同起始日
        /// </summary>
        public DateTime contractStartDate
        {
            set { _contractstartdate = value; }
            get { return _contractstartdate; }
        }
        /// <summary>
        /// 合同终止日
        /// </summary>
        public DateTime contractEndDate
        {
            set { _contractenddate = value; }
            get { return _contractenddate; }
        }
        /// <summary>
        /// 转正日期
        /// </summary>
        public DateTime probationEndDate
        {
            set { _probationenddate = value; }
            get { return _probationenddate; }
        }
        /// <summary>
        /// 婚姻状况
        /// </summary>
        new public int MaritalStatus { get; set; }
        /// <summary>
        /// 养老/失业/工伤/生育缴费基数
        /// </summary>
        public string socialInsuranceBase
        {
            set { _socialinsurancebase = value; }
            get { return _socialinsurancebase; }
        }
        /// <summary>
        /// 医疗基数
        /// </summary>
        public string medicalInsuranceBase
        {
            set { _medicalinsurancebase = value; }
            get { return _medicalinsurancebase; }
        }
        /// <summary>
        /// 公积金基数
        /// </summary>
        public string publicReserveFundsBase
        {
            set { _publicreservefundsbase = value; }
            get { return _publicreservefundsbase; }
        }
        /// <summary>
        /// 是否在公司挂档
        /// </summary>
        public bool isArchive
        {
            set { _isarchive = value; }
            get { return _isarchive; }
        }
        /// <summary>
        /// 合同签订情况
        /// </summary>
        public string contractSignInfo
        {
            set { _contractsigninfo = value; }
            get { return _contractsigninfo; }
        }
        /// <summary>
        /// 户口所在地
        /// </summary>
        public string InsurancePlace
        {
            set { _insuranceplace = value; }
            get { return _insuranceplace; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        new public int Gender { get;set;}
        /// <summary>
        /// 生日
        /// </summary>
        new public DateTime Birthday
        {
            set { _birthday = value; }
            get { return _birthday; }
        }
        /// <summary>
        /// 学位
        /// </summary>
        new public string Degree
        {
            set { _degree = value; }
            get { return _degree; }
        }

        /// <summary>
        /// 是否外籍员工
        /// </summary>
        public bool IsForeign
        {
            set { _isForeign = value; }
            get { return _isForeign; }
        }

        /// <summary>
        /// 是否有劳工证
        /// </summary>
        public bool IsCertification
        {
            set { _isCertification = value; }
            get { return _isCertification; }
        }

        /// <summary>
        /// 工资月数
        /// </summary>
        public int WageMonths
        {
            set { _wageMonths = value; }
            get { return _wageMonths; }
        }
        /// <summary>
        /// 养老保险公司比例(%)
        /// </summary>
        public decimal? EIProportionOfFirms
        {
            set { _eiproportionoffirms = value; }
            get { return _eiproportionoffirms; }
        }
        /// <summary>
        /// 养老保险个人比例(%)
        /// </summary>
        public decimal? EIProportionOfIndividuals
        {
            set { _eiproportionofindividuals = value; }
            get { return _eiproportionofindividuals; }
        }
        /// <summary>
        /// 失业保险公司比例(%)
        /// </summary>
        public decimal? UIProportionOfFirms
        {
            set { _uiproportionoffirms = value; }
            get { return _uiproportionoffirms; }
        }
        /// <summary>
        /// 失业保险个人比例(%)
        /// </summary>
        public decimal? UIProportionOfIndividuals
        {
            set { _uiproportionofindividuals = value; }
            get { return _uiproportionofindividuals; }
        }
        /// <summary>
        /// 生育保险公司比例(%)
        /// </summary>
        public decimal? BIProportionOfFirms
        {
            set { _biproportionoffirms = value; }
            get { return _biproportionoffirms; }
        }
        /// <summary>
        /// 生育保险个人比例(%)
        /// </summary>
        public decimal? BIProportionOfIndividuals
        {
            set { _biproportionofindividuals = value; }
            get { return _biproportionofindividuals; }
        }
        /// <summary>
        /// 工伤险公司比例(%)
        /// </summary>
        public decimal? CIProportionOfFirms
        {
            set { _ciproportionoffirms = value; }
            get { return _ciproportionoffirms; }
        }
        /// <summary>
        /// 工伤险个人比例(%)
        /// </summary>
        public decimal? CIProportionOfIndividuals
        {
            set { _ciproportionofindividuals = value; }
            get { return _ciproportionofindividuals; }
        }
        /// <summary>
        /// 医疗保险公司比例(%)
        /// </summary>
        public decimal? MIProportionOfFirms
        {
            set { _miproportionoffirms = value; }
            get { return _miproportionoffirms; }
        }
        /// <summary>
        /// 医疗保险个人比例(%)
        /// </summary>
        public decimal? MIProportionOfIndividuals
        {
            set { _miproportionofindividuals = value; }
            get { return _miproportionofindividuals; }
        }
        /// <summary>
        /// 医疗保险大额医疗个人支付额(元)
        /// </summary>
        public decimal? MIBigProportionOfIndividuals
        {
            set { _mibigproportionofindividuals = value; }
            get { return _mibigproportionofindividuals; }
        }
        /// <summary>
        /// 公积金公司比例(%)
        /// </summary>
        public decimal? PRFProportionOfFirms
        {
            set { _prfproportionoffirms = value; }
            get { return _prfproportionoffirms; }
        }
        /// <summary>
        /// 公积金个人比例(%)
        /// </summary>
        public decimal? PRFProportionOfIndividuals
        {
            set { _prfproportionofindividuals = value; }
            get { return _prfproportionofindividuals; }
        }
        /// <summary>
        /// 养老保险公司应缴费用
        /// </summary>
        public string EIFirmsCosts
        {
            set { _eifirmscosts = value; }
            get { return _eifirmscosts; }
        }
        /// <summary>
        /// 养老保险个人应缴费用
        /// </summary>
        public string EIIndividualsCosts
        {
            set { _eiindividualscosts = value; }
            get { return _eiindividualscosts; }
        }
        /// <summary>
        /// 失业保险公司应缴费用
        /// </summary>
        public string UIFirmsCosts
        {
            set { _uifirmscosts = value; }
            get { return _uifirmscosts; }
        }
        /// <summary>
        /// 失业保险个人应缴费用
        /// </summary>
        public string UIIndividualsCosts
        {
            set { _uiindividualscosts = value; }
            get { return _uiindividualscosts; }
        }
        /// <summary>
        /// 生育保险公司应缴费用
        /// </summary>
        public string BIFirmsCosts
        {
            set { _bifirmscosts = value; }
            get { return _bifirmscosts; }
        }
        /// <summary>
        /// 生育保险个人应缴费用
        /// </summary>
        public string BIIndividualsCosts
        {
            set { _biindividualscosts = value; }
            get { return _biindividualscosts; }
        }
        /// <summary>
        /// 工伤险公司应缴费用
        /// </summary>
        public string CIFirmsCosts
        {
            set { _cifirmscosts = value; }
            get { return _cifirmscosts; }
        }
        /// <summary>
        /// 工伤险个人应缴费用
        /// </summary>
        public string CIIndividualsCosts
        {
            set { _ciindividualscosts = value; }
            get { return _ciindividualscosts; }
        }
        /// <summary>
        /// 医疗保险公司应缴费用
        /// </summary>
        public string MIFirmsCosts
        {
            set { _mifirmscosts = value; }
            get { return _mifirmscosts; }
        }
        /// <summary>
        /// 医疗保险个人应缴费用
        /// </summary>
        public string MIIndividualsCosts
        {
            set { _miindividualscosts = value; }
            get { return _miindividualscosts; }
        }
        /// <summary>
        /// 公积金公司应缴费用
        /// </summary>
        public string PRFirmsCosts
        {
            set { _prfirmscosts = value; }
            get { return _prfirmscosts; }
        }
        /// <summary>
        /// 公积金个人应缴费用
        /// </summary>
        public string PRIIndividualsCosts
        {
            set { _priindividualscosts = value; }
            get { return _priindividualscosts; }
        }

        /// <summary>
        /// 社保所属公司
        /// </summary>
        public string socialInsuranceCompany
        {
            set { _socialInsuranceCompany = value; }
            get { return _socialInsuranceCompany;  }
        }
        private string _commonName;
        /// <summary>
        /// 公司常用姓名
        /// </summary>
        public string CommonName
        {
            get { return _commonName; }
            set { _commonName = value; }
        }
        #endregion Model


        public void PopupData(IDataReader r)
        {
            if (r["id"].ToString() != "")
            {
                _id = int.Parse(r["id"].ToString());
            }
            _education = r["Education"].ToString();
            _graduatedfrom = r["GraduatedFrom"].ToString();
            _major = r["Major"].ToString();
            if (r["ThisYearSalary"].ToString() != "")
            {
                _thisyearsalary = r["ThisYearSalary"].ToString();
            }
            if (r["Status"].ToString() != "")
            {
                _status = int.Parse(r["Status"].ToString());
            }
            if (r["UserID"].ToString() != "")
            {
                _userid = int.Parse(r["UserID"].ToString());
            }
            _nowbasepay = r["nowBasePay"].ToString();
            _nowmeritpay = r["nowMeritPay"].ToString();
            _newbasepay = r["newBasePay"].ToString();
            _code = r["Code"].ToString();
            _newmeritpay = r["newMeritPay"].ToString();
            
            if (r["Creator"].ToString() != "")
            {
                _creator = int.Parse(r["Creator"].ToString());
            }
            if (r["CreatedTime"].ToString() != "")
            {
                _createdtime = DateTime.Parse(r["CreatedTime"].ToString());
            }
            _username = r["UserName"].ToString();
            _creatorname = r["CreatorName"].ToString();
            if (r["endowmentInsuranceStarTime"].ToString() != "")
            {
                _endowmentinsurancestartime = DateTime.Parse(r["endowmentInsuranceStarTime"].ToString());
            }
            if (r["endowmentInsuranceEndTime"].ToString() != "")
            {
                _endowmentinsuranceendtime = DateTime.Parse(r["endowmentInsuranceEndTime"].ToString());
            }
            if (r["unemploymentInsuranceStarTime"].ToString() != "")
            {
                _unemploymentinsurancestartime = DateTime.Parse(r["unemploymentInsuranceStarTime"].ToString());
            }
            if (r["unemploymentInsuranceEndTime"].ToString() != "")
            {
                _unemploymentinsuranceendtime = DateTime.Parse(r["unemploymentInsuranceEndTime"].ToString());
            }
            if (r["birthInsuranceStarTime"].ToString() != "")
            {
                _birthinsurancestartime = DateTime.Parse(r["birthInsuranceStarTime"].ToString());
            }
            if (r["TypeID"].ToString() != "")
            {
                _typeid = int.Parse(r["TypeID"].ToString());
            }
            if (r["birthInsuranceEndTime"].ToString() != "")
            {
                _birthinsuranceendtime = DateTime.Parse(r["birthInsuranceEndTime"].ToString());
            }
            if (r["compoInsuranceStarTime"].ToString() != "")
            {
                _compoinsurancestartime = DateTime.Parse(r["compoInsuranceStarTime"].ToString());
            }
            if (r["compoInsuranceEndTime"].ToString() != "")
            {
                _compoinsuranceendtime = DateTime.Parse(r["compoInsuranceEndTime"].ToString());
            }
            if (r["medicalInsuranceStarTime"].ToString() != "")
            {
                _medicalinsurancestartime = DateTime.Parse(r["medicalInsuranceStarTime"].ToString());
            }
            if (r["medicalInsuranceEndTime"].ToString() != "")
            {
                _medicalinsuranceendtime = DateTime.Parse(r["medicalInsuranceEndTime"].ToString());
            }
            if (r["publicReserveFundsStarTime"].ToString() != "")
            {
                _publicreservefundsstartime = DateTime.Parse(r["publicReserveFundsStarTime"].ToString());
            }
            if (r["publicReserveFundsEndTime"].ToString() != "")
            {
                _publicreservefundsendtime = DateTime.Parse(r["publicReserveFundsEndTime"].ToString());
            }
            if (r["contractStartDate"].ToString() != "")
            {
                _contractstartdate = DateTime.Parse(r["contractStartDate"].ToString());
            }
            if (r["contractEndDate"].ToString() != "")
            {
                _contractenddate = DateTime.Parse(r["contractEndDate"].ToString());
            }
            if (r["probationEndDate"].ToString() != "")
            {
                _probationenddate = DateTime.Parse(r["probationEndDate"].ToString());
            }
            if (r["MaritalStatus"].ToString() != "")
            {
                MaritalStatus = int.Parse(r["MaritalStatus"].ToString());
            }
            
                _socialinsurancebase = r["socialInsuranceBase"].ToString();
           
                _medicalinsurancebase = r["medicalInsuranceBase"].ToString();
            
                _publicreservefundsbase = r["publicReserveFundsBase"].ToString();
            
            if (r["isArchive"].ToString() != "")
            {
                if ((r["isArchive"].ToString() == "1") || (r["isArchive"].ToString().ToLower() == "true"))
                {
                    _isarchive = true;
                }
                else
                {
                    _isarchive = false;
                }
            }
            _contractsigninfo = r["contractSignInfo"].ToString();
            _insuranceplace = r["InsurancePlace"].ToString();
            if (r["Gender"].ToString() != "")
            {
                Gender = int.Parse(r["Gender"].ToString());
            }
            if (r["Birthday"].ToString() != "")
            {
                _birthday = DateTime.Parse(r["Birthday"].ToString());
            }
            if (r["IsForeign"].ToString() != "")
            {
                if ((r["IsForeign"].ToString() == "1") || (r["IsForeign"].ToString().ToLower() == "true"))
                {
                    _isForeign = true;
                }
                else
                {
                    _isForeign = false;
                }
            }
            if (r["IsCertification"].ToString() != "")
            {
                if ((r["IsCertification"].ToString() == "1") || (r["IsCertification"].ToString().ToLower() == "true"))
                {
                    _isCertification = true;
                }
                else
                {
                    _isCertification = false;
                }
            }
            if (r["WageMonths"].ToString() != "")
            {
                _wageMonths = int.Parse(r["WageMonths"].ToString());
            }
            if (r["EIProportionOfFirms"].ToString() != "")
            {
                _eiproportionoffirms = decimal.Parse(r["EIProportionOfFirms"].ToString());
            }
            if (r["EIProportionOfIndividuals"].ToString() != "")
            {
                _eiproportionofindividuals = decimal.Parse(r["EIProportionOfIndividuals"].ToString());
            }
            if (r["UIProportionOfFirms"].ToString() != "")
            {
                _uiproportionoffirms = decimal.Parse(r["UIProportionOfFirms"].ToString());
            }
            if (r["UIProportionOfIndividuals"].ToString() != "")
            {
                _uiproportionofindividuals = decimal.Parse(r["UIProportionOfIndividuals"].ToString());
            }
            if (r["BIProportionOfFirms"].ToString() != "")
            {
                _biproportionoffirms = decimal.Parse(r["BIProportionOfFirms"].ToString());
            }
            if (r["BIProportionOfIndividuals"].ToString() != "")
            {
                _biproportionofindividuals = decimal.Parse(r["BIProportionOfIndividuals"].ToString());
            }
            if (r["CIProportionOfFirms"].ToString() != "")
            {
                _ciproportionoffirms = decimal.Parse(r["CIProportionOfFirms"].ToString());
            }
            if (r["CIProportionOfIndividuals"].ToString() != "")
            {
                _ciproportionofindividuals = decimal.Parse(r["CIProportionOfIndividuals"].ToString());
            }
            if (r["MIProportionOfFirms"].ToString() != "")
            {
                _miproportionoffirms = decimal.Parse(r["MIProportionOfFirms"].ToString());
            }
            if (r["MIProportionOfIndividuals"].ToString() != "")
            {
                _miproportionofindividuals = decimal.Parse(r["MIProportionOfIndividuals"].ToString());
            }
            if (r["MIBigProportionOfIndividuals"].ToString() != "")
            {
                _mibigproportionofindividuals = decimal.Parse(r["MIBigProportionOfIndividuals"].ToString());
            }
            if (r["PRFProportionOfFirms"].ToString() != "")
            {
                _prfproportionoffirms = decimal.Parse(r["PRFProportionOfFirms"].ToString());
            }
            if (r["PRFProportionOfIndividuals"].ToString() != "")
            {
                _prfproportionofindividuals = decimal.Parse(r["PRFProportionOfIndividuals"].ToString());
            }
            if (r["EIFirmsCosts"].ToString() != "")
            {
                _eifirmscosts = (r["EIFirmsCosts"].ToString());
            }
            if (r["EIIndividualsCosts"].ToString() != "")
            {
                _eiindividualscosts = (r["EIIndividualsCosts"].ToString());
            }
            if (r["UIFirmsCosts"].ToString() != "")
            {
                _uifirmscosts = (r["UIFirmsCosts"].ToString());
            }
            if (r["UIIndividualsCosts"].ToString() != "")
            {
                _uiindividualscosts = (r["UIIndividualsCosts"].ToString());
            }
            if (r["BIFirmsCosts"].ToString() != "")
            {
                _bifirmscosts = (r["BIFirmsCosts"].ToString());
            }
            if (r["BIIndividualsCosts"].ToString() != "")
            {
                _biindividualscosts = (r["BIIndividualsCosts"].ToString());
            }
            if (r["CIFirmsCosts"].ToString() != "")
            {
                _cifirmscosts = (r["CIFirmsCosts"].ToString());
            }
            if (r["CIIndividualsCosts"].ToString() != "")
            {
                _ciindividualscosts = (r["CIIndividualsCosts"].ToString());
            }
            if (r["MIFirmsCosts"].ToString() != "")
            {
                _mifirmscosts = (r["MIFirmsCosts"].ToString());
            }
            if (r["MIIndividualsCosts"].ToString() != "")
            {
                _miindividualscosts = (r["MIIndividualsCosts"].ToString());
            }
            if (r["PRFirmsCosts"].ToString() != "")
            {
                _prfirmscosts = (r["PRFirmsCosts"].ToString());
            }
            if (r["PRIIndividualsCosts"].ToString() != "")
            {
                _priindividualscosts = (r["PRIIndividualsCosts"].ToString());
            }
            _socialInsuranceCompany = r["socialInsuranceCompany"].ToString();
            _commonName = r["CommonName"].ToString();
        }

    }
}
