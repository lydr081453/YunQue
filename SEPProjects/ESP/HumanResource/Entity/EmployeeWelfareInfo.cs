using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class EmployeeWelfareInfo
    {
        public EmployeeWelfareInfo()
        { }
        #region Model
        private int _id;
        private int _sysid;
        private string _contracttype;
        private string _contractterm;
        private string _contractcompany;
        private DateTime _contractstartdate = DateTime.Parse("1900-1-1 0:00:00");
        private DateTime _contractenddate = DateTime.Parse("1900-1-1 0:00:00");
        private string _probationperiod;
        private DateTime _probationperioddeadline = DateTime.Parse("1900-1-1 0:00:00");
        private DateTime _probationenddate = DateTime.Parse("1900-1-1 0:00:00");
        private bool _endowmentinsurance;
        private bool _unemploymentinsurance;
        private bool _birthinsurance;
        private bool _compoinsurance;
        private bool _medicalinsurance;
        private string _socialinsurancecompany;
        private string _socialinsuranceaddress;
        private string _socialinsurancecode;
        private string _medicalinsurancecode;
        private string _socialinsurancebase;
        private string _medicalinsurancebase;
        private string _publicreservefundscompany;
        private string _publicreservefundsaddress;
        private string _publicreservefundsbase;
        private string _publicreservefundscode;
        private bool _isarchive;
        private string _archivecode;
        private string _archivedate;
        private string _memo;
        private bool _publicreservefunds;
        private int _contractrenewalcount;
        private string _contractsigninfo;
        private string _archiveplace;
        private string _insuranceplace;
        private DateTime _endowmentinsurancestartime = DateTime.Parse("1900-1-1 0:00:00");
        private DateTime _endowmentinsuranceendtime = DateTime.Parse("1900-1-1 0:00:00");
        private DateTime _unemploymentinsurancestartime = DateTime.Parse("1900-1-1 0:00:00");
        private DateTime _unemploymentinsuranceendtime = DateTime.Parse("1900-1-1 0:00:00");
        private DateTime _birthinsurancestartime = DateTime.Parse("1900-1-1 0:00:00");
        private DateTime _birthinsuranceendtime = DateTime.Parse("1900-1-1 0:00:00");
        private DateTime _compoinsurancestartime = DateTime.Parse("1900-1-1 0:00:00");
        private DateTime _compoinsuranceendtime = DateTime.Parse("1900-1-1 0:00:00");
        private DateTime _medicalinsurancestartime = DateTime.Parse("1900-1-1 0:00:00");
        private DateTime _medicalinsuranceendtime = DateTime.Parse("1900-1-1 0:00:00");
        private DateTime _publicreservefundsstartime = DateTime.Parse("1900-1-1 0:00:00");
        private DateTime _publicreservefundsendtime = DateTime.Parse("1900-1-1 0:00:00");
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
        private bool _complementarymedical;
        private bool _accidentinsurance;
        private decimal? _complementarymedicalcosts;
        private DateTime _accidentinsurancebeginTime = DateTime.Parse("1900-1-1 0:00:00");
        private DateTime _accidentinsuranceendTime = DateTime.Parse("1900-1-1 0:00:00");

        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 员工编号
        /// </summary>
        public int sysid
        {
            set { _sysid = value; }
            get { return _sysid; }
        }
        /// <summary>
        /// 合同类型
        /// </summary>
        public string contractType
        {
            set { _contracttype = value; }
            get { return _contracttype; }
        }
        /// <summary>
        /// 合同期限
        /// </summary>
        public string contractTerm
        {
            set { _contractterm = value; }
            get { return _contractterm; }
        }
        /// <summary>
        /// 合同公司
        /// </summary>
        public string contractCompany
        {
            set { _contractcompany = value; }
            get { return _contractcompany; }
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
        /// 试用期时长
        /// </summary>
        public string probationPeriod
        {
            set { _probationperiod = value; }
            get { return _probationperiod; }
        }
        /// <summary>
        /// 试用期截止日
        /// </summary>
        public DateTime probationPeriodDeadLine
        {
            set { _probationperioddeadline = value; }
            get { return _probationperioddeadline; }
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
        /// 养老保险
        /// </summary>
        public bool endowmentInsurance
        {
            set { _endowmentinsurance = value; }
            get { return _endowmentinsurance; }
        }
        /// <summary>
        /// 失业保险
        /// </summary>
        public bool unemploymentInsurance
        {
            set { _unemploymentinsurance = value; }
            get { return _unemploymentinsurance; }
        }
        /// <summary>
        /// 生育
        /// </summary>
        public bool birthInsurance
        {
            set { _birthinsurance = value; }
            get { return _birthinsurance; }
        }
        /// <summary>
        /// 工伤
        /// </summary>
        public bool compoInsurance
        {
            set { _compoinsurance = value; }
            get { return _compoinsurance; }
        }
        /// <summary>
        /// 医疗保险
        /// </summary>
        public bool medicalInsurance
        {
            set { _medicalinsurance = value; }
            get { return _medicalinsurance; }
        }
        /// <summary>
        /// 社保所在公司
        /// </summary>
        public string socialInsuranceCompany
        {
            set { _socialinsurancecompany = value; }
            get { return _socialinsurancecompany; }
        }
        /// <summary>
        /// 社保所属地点
        /// </summary>
        public string socialInsuranceAddress
        {
            set { _socialinsuranceaddress = value; }
            get { return _socialinsuranceaddress; }
        }
        /// <summary>
        /// 社保编号
        /// </summary>
        public string socialInsuranceCode
        {
            set { _socialinsurancecode = value; }
            get { return _socialinsurancecode; }
        }
        /// <summary>
        /// 医疗编号
        /// </summary>
        public string medicalInsuranceCode
        {
            set { _medicalinsurancecode = value; }
            get { return _medicalinsurancecode; }
        }
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
        /// 公积金所在公司
        /// </summary>
        public string publicReserveFundsCompany
        {
            set { _publicreservefundscompany = value; }
            get { return _publicreservefundscompany; }
        }
        /// <summary>
        /// 公积金所属地点
        /// </summary>
        public string publicReserveFundsAddress
        {
            set { _publicreservefundsaddress = value; }
            get { return _publicreservefundsaddress; }
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
        /// 公积金编号
        /// </summary>
        public string publicReserveFundsCode
        {
            set { _publicreservefundscode = value; }
            get { return _publicreservefundscode; }
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
        /// 档案编号
        /// </summary>
        public string ArchiveCode
        {
            set { _archivecode = value; }
            get { return _archivecode; }
        }
        /// <summary>
        /// 存档日期
        /// </summary>
        public string ArchiveDate
        {
            set { _archivedate = value; }
            get { return _archivedate; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string memo
        {
            set { _memo = value; }
            get { return _memo; }
        }
        /// <summary>
        /// 公积金
        /// </summary>
        public bool publicReserveFunds
        {
            set { _publicreservefunds = value; }
            get { return _publicreservefunds; }
        }
        /// <summary>
        /// 合同续签次数
        /// </summary>
        public int contractRenewalCount
        {
            set { _contractrenewalcount = value; }
            get { return _contractrenewalcount; }
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
        /// 社保户口所在地
        /// </summary>
        public string ArchivePlace
        {
            set { _archiveplace = value; }
            get { return _archiveplace; }
        }
        /// <summary>
        /// 挂档所在地
        /// </summary>
        public string InsurancePlace
        {
            set { _insuranceplace = value; }
            get { return _insuranceplace; }
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
        /// 补充医疗
        /// </summary>
        public bool ComplementaryMedical
        {
            set { _complementarymedical = value; }
            get { return _complementarymedical; }
        }

        /// <summary>
        /// 意外保险
        /// </summary>
        public bool AccidentInsurance
        {
            set { _accidentinsurance = value; }
            get { return _accidentinsurance; }
        }

        /// <summary>
        /// 补充医疗费用
        /// </summary>
        public decimal? ComplementaryMedicalCosts
        {
            set { _complementarymedicalcosts = value; }
            get { return _complementarymedicalcosts; }
        }

        /// <summary>
        /// 意外保险开始时间
        /// </summary>
        public DateTime AccidentInsuranceBeginTime
        {
            set { _accidentinsurancebeginTime = value; }
            get { return _accidentinsurancebeginTime; }
        }

        /// <summary>
        /// 意外保险结束时间
        /// </summary>
        public DateTime AccidentInsuranceEndTime
        {
            set { _accidentinsuranceendTime = value; }
            get { return _accidentinsuranceendTime; }
        }
        #endregion Model

    }
}
