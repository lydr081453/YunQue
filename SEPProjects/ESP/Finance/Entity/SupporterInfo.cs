using System;
using System.Collections.Generic;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类SupporterInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class SupporterInfo
    {
        public SupporterInfo()
        { }
        #region Model
        private int _supportid;
        private string _leadercode;
        private string _leaderemployeename;
        private decimal? _budgetallocation;
        private string _completedpercent;
        private decimal? _incomefee;
        private decimal? _supportercost;
        private decimal? _billedtax;
        private int? _applicantuserid;
        private string _applicantusername;
        private string _applicantcode;
        private int _projectid;
        private string _applicantemployeename;
        private DateTime? _applicantdate;
        private string _incometype;
        private byte[] _lastupdatetime;
        private string _projectcode;
        private int? _groupid;
        private string _groupname;
        private string _servicetype;
        private string _servicedescription;
        private int? _leaderuserid;
        private string _leaderusername;
        private DateTime? _bizBeginDate;
        private DateTime? _bizEndDate;
        private bool? _taxType;

        public bool? TaxType
        {
            get { return _taxType; }
            set { _taxType = value; }
        }
        /// <summary>
        /// 自增编号
        /// </summary>
        public int SupportID
        {
            set { _supportid = value; }
            get { return _supportid; }
        }
        /// <summary>
        /// 负责人公司内部编号
        /// </summary>
        public string LeaderCode
        {
            set { _leadercode = value; }
            get { return _leadercode; }
        }
        /// <summary>
        /// 负责人真实姓名
        /// </summary>
        public string LeaderEmployeeName
        {
            set { _leaderemployeename = value; }
            get { return _leaderemployeename; }
        }
        /// <summary>
        /// 业务总额
        /// </summary>
        public decimal? BudgetAllocation
        {
            set { _budgetallocation = value; }
            get { return _budgetallocation; }
        }
        /// <summary>
        /// 预计完工百分比
        /// </summary>
        public string CompletedPercent
        {
            set { _completedpercent = value; }
            get { return _completedpercent; }
        }
        /// <summary>
        /// 服务费收入
        /// </summary>
        public decimal? IncomeFee
        {
            set { _incomefee = value; }
            get { return _incomefee; }
        }
        /// <summary>
        /// 成本明细总额
        /// </summary>
        public decimal? SupporterCost
        {
            set { _supportercost = value; }
            get { return _supportercost; }
        }
        /// <summary>
        /// 由客户支付之税金
        /// </summary>
        public decimal? BilledTax
        {
            set { _billedtax = value; }
            get { return _billedtax; }
        }
        /// <summary>
        /// 申请人Id自增长
        /// </summary>
        public int? ApplicantUserID
        {
            set { _applicantuserid = value; }
            get { return _applicantuserid; }
        }
        /// <summary>
        /// 申请人登录帐号
        /// </summary>
        public string ApplicantUserName
        {
            set { _applicantusername = value; }
            get { return _applicantusername; }
        }
        /// <summary>
        /// 申请人公司内部编号
        /// </summary>
        public string ApplicantCode
        {
            set { _applicantcode = value; }
            get { return _applicantcode; }
        }
        /// <summary>
        /// 项目号申请单ID
        /// </summary>
        public int ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
        /// <summary>
        /// 申请人真实姓名
        /// </summary>
        public string ApplicantEmployeeName
        {
            set { _applicantemployeename = value; }
            get { return _applicantemployeename; }
        }
        /// <summary>
        /// 申请日期
        /// </summary>
        public DateTime? ApplicantDate
        {
            set { _applicantdate = value; }
            get { return _applicantdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IncomeType
        {
            set { _incometype = value; }
            get { return _incometype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public byte[] Lastupdatetime
        {
            set { _lastupdatetime = value; }
            get { return _lastupdatetime; }
        }
        /// <summary>
        /// 项目号申请单项目号
        /// </summary>
        public string ProjectCode
        {
            set { _projectcode = value; }
            get { return _projectcode; }
        }
        /// <summary>
        /// 支持方组别Id
        /// </summary>
        public int? GroupID
        {
            set { _groupid = value; }
            get { return _groupid; }
        }
        /// <summary>
        /// 支持方组别名称
        /// </summary>
        public string GroupName
        {
            set { _groupname = value; }
            get { return _groupname; }
        }
        /// <summary>
        /// 服务类型
        /// </summary>
        public string ServiceType
        {
            set { _servicetype = value; }
            get { return _servicetype; }
        }
        /// <summary>
        /// 业务描述
        /// </summary>
        public string ServiceDescription
        {
            set { _servicedescription = value; }
            get { return _servicedescription; }
        }
        /// <summary>
        /// 项目经理Id自增长
        /// </summary>
        public int? LeaderUserID
        {
            set { _leaderuserid = value; }
            get { return _leaderuserid; }
        }
        /// <summary>
        /// 项目经理登录帐号
        /// </summary>
        public string LeaderUserName
        {
            set { _leaderusername = value; }
            get { return _leaderusername; }
        }

        /// <summary>
        /// 预计开始日期
        /// </summary>
        public DateTime? BizBeginDate
        {
            get { return _bizBeginDate; }
            set { _bizBeginDate = value; }
        }
        /// <summary>
        /// /预计完成日期
        /// </summary>
        public DateTime? BizEndDate
        {
            get { return _bizEndDate; }
            set { _bizEndDate = value; }
        }
        #endregion Model

        public IList<ESP.Finance.Entity.SupporterScheduleInfo> SupporterSchedules { get; set; }
        public IList<ESP.Finance.Entity.SupporterExpenseInfo> SupporterExpenses { get; set; }
        public IList<ESP.Finance.Entity.SupporterCostInfo> SupporterCosts { get; set; }
        public IList<ESP.Finance.Entity.SupportMemberInfo> SupporterMembers { get; set; }

    }

    public partial class SupporterInfo
    {
        int? _workItemID;

        /// <summary>
        /// 工作项ID
        /// </summary>
        public int? WorkItemID
        {
            get { return _workItemID; }
            set { _workItemID = value; }
        }

        string _workItemName;

        /// <summary>
        /// 工作项名称
        /// </summary>
        public string WorkItemName
        {
            get { return _workItemName; }
            set { _workItemName = value; }
        }

        int? _processID;

        /// <summary>
        /// 进度ID
        /// </summary>
        public int? ProcessID
        {
            get { return _processID; }
            set { _processID = value; }
        }

        int? _instanceID;

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public int? InstanceID
        {
            get { return _instanceID; }
            set { _instanceID = value; }
        }


        int _status;

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        string _supporterCode;

        /// <summary>
        /// 支持方序列号
        /// </summary>
        public string SupporterCode
        {
            get { return _supporterCode; }
            set { _supporterCode = value; }
        }
    }

}