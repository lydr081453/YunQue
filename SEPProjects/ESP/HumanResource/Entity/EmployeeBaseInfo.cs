using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class EmployeeBaseInfo : ESP.Framework.Entity.EmployeeInfo
    {
        public EmployeeBaseInfo()
        { }
        #region Model

        private EmployeeJobInfo _employeeJobInfo;

        public EmployeeJobInfo EmployeeJobInfo
        {
            get { return _employeeJobInfo; }
            set { _employeeJobInfo = value; }
        }
        private EmployeeWelfareInfo _employeeWelfareInfo;

        public EmployeeWelfareInfo EmployeeWelfareInfo
        {
            get { return _employeeWelfareInfo; }
            set { _employeeWelfareInfo = value; }
        }

        List<EmployeesInPositionsInfo> _employeesInPositionsList;

        public List<EmployeesInPositionsInfo> EmployeesInPositionsList
        {
            get { return _employeesInPositionsList; }
            set { _employeesInPositionsList = value; }
        }

        List<SnapshotsInfo> _employeesSnapshotsList;

        public List<SnapshotsInfo> EmployeesSnapshotsList
        {
            get { return _employeesSnapshotsList; }
            set { _employeesSnapshotsList = value; }
        }

        SnapshotsInfo _employeesCurrentSnapshots;

        public SnapshotsInfo EmployeesCurrentSnapshot
        {
            get { return _employeesCurrentSnapshots; }
            set { _employeesCurrentSnapshots = value; }
        }

        private bool _isSendMail = false;
        public bool IsSendMail
        {
            get { return _isSendMail; }
            set { _isSendMail = value; }
        }

        private string _commonName;
        public string CommonName
        {
            get { return _commonName; }
            set { _commonName = value; }
        }

        public int DimissionStatus { get; set; }

        private string _privateemail;
        /// <summary>
        /// 员工个人邮箱。
        /// </summary>
        public string PrivateEmail
        {
            set { _privateemail = value; }
            get { return _privateemail; }
        }

        private bool _ownedpc;
        private int _offerlettertemplate;
        /// <summary>
        /// 自备电脑
        /// </summary>
        public bool OwnedPC
        {
            set { _ownedpc = value; }
            get { return _ownedpc; }
        }
        /// <summary>
        /// 入职通知模板
        /// </summary>
        public int OfferLetterTemplate
        {
            set { _offerlettertemplate = value; }
            get { return _offerlettertemplate; }
        }
        private int _offerLetterSender;
        public int OfferLetterSender
        {
            get { return _offerLetterSender; }
            set { _offerLetterSender = value; }
        }
        /// <summary>
        /// Offer Letter 发送时间。
        /// </summary>
        private DateTime? _offerLetterSendTime;
        public DateTime? OfferLetterSendTime
        {
            get { return _offerLetterSendTime; }
            set { _offerLetterSendTime = value; }
        }
        /// <summary>
        /// 是否是应届毕业生。
        /// </summary>
        private bool _isExamen;
        public bool IsExamen
        {
            get { return _isExamen; }
            set { _isExamen = value; }
        }
        /// <summary>
        /// 员工社会工龄。
        /// </summary>
        private int _seniority;
        public int Seniority
        {
            get { return _seniority; }
            set { _seniority = value; }
        }
        /// <summary>
        /// 工资
        /// </summary>
        private decimal _pay;
        public decimal Pay
        {
            get { return _pay; }
            set { _pay = value; }
        }
        private decimal _performance;
        public decimal Performance
        {
            get { return _performance; }
            set { _performance = value; }
        }
        /// <summary>
        /// 考勤绩效
        /// </summary>
        public decimal Attendance { get; set; }

        public string MateName { get; set; }
        public string AdrressNow { get; set; }
        public string PostCodeNow { get; set; }
        public string FamillyDesc { get; set; }

        public string Residence { get; set; }

        public string Political { get; set; }
        public string Nation { get; set; }
        public bool HasChild { get; set; }
        public int EmpProperty { get; set; }

        public string Appearance { get; set; }
        public string Quality { get; set; }
        public string Know { get; set; }
        public string Equal { get; set; }
        public string Motivation { get; set; }
        public string FourD { get; set; }
        public string EQ { get; set; }
        /// <summary>
        /// 首次参加工作日期
        /// </summary>
        public DateTime? WorkBegin { get; set; }

        public int SocialSecurityType { get; set; }

        public DateTime? JoinDate { get; set; }
        public string BranchCode { get; set; }
        /// <summary>
        /// 合同签订年数
        /// </summary>
        public int ContractYear { get; set; }
        /// <summary>
        /// 首次签订合同日期
        /// </summary>
        public DateTime? FirstContractBeginDate { get; set; }
        /// <summary>
        /// 首次签订合同结束日期
        /// </summary>
        public DateTime? FirstContractEndDate { get; set; }
        /// <summary>
        /// 最近一次签订合同日期
        /// </summary>
        public DateTime? ContractBeginDate { get; set; }
        /// <summary>
        /// 最近一次签订合同结束日期
        /// </summary>
        public DateTime? ContractEndDate { get; set; }
        /// <summary>
        /// 合同签订日期
        /// </summary>
        public DateTime? ContractSignDate { get; set; }
        /// <summary>
        /// 实习期至
        /// </summary>
        public DateTime? ProbationDate { get; set; }
        /// <summary>
        /// 年假基数
        /// </summary>
        public int AnnualLeaveBase { get; set; }
        /// <summary>
        /// 工资卡开户行
        /// </summary>
        public string SalaryBank{ get; set; }
        /// <summary>
        /// 工资卡开户账号
        /// </summary>
        public string SalaryCardNo { get; set; }

        public DateTime? IDValid { get; set; }
        #endregion Model
    }
}
