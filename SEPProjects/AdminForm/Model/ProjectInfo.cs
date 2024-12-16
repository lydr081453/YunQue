using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminForm.Model
{
    /// <summary>
    /// 实体类ProjectInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ProjectInfo
    {
        public ProjectInfo()
        { }
        #region Model
        private int _projectid;
        private string _branchcode;
        private int? _businesstypeid;
        private string _businesstypename;
        private int? _groupid;
        private string _groupname;
        private int? _projecttypeid;
        private string _projecttypename;
        private string _projecttypecode;
        private int? _businessdesid;
        private string _businessdescription;
        private string _projectcode;
        private DateTime? _begindate;
        private DateTime? _enddate;
        private decimal? _totalamount;
        private decimal? _contractservicefee;
        private int? _contracttaxid;
        private decimal? _contracttax;
        private string _contractcostdetail;
        private int? _customerid;
        private int? _isneedinvoice;
        private string _paycycle;
        private int? _contractstatusid;
        private string _otherrequest;
        private int _creatorid;
        private string _creatorname;
        private string _creatorcode;
        private string _creatoruserid;
        private int _applicantuserid;
        private string _applicantusername;
        private string _applicantcode;
        private string _applicantemployeename;
        private string _applicantUserEmail;
        private string _applicantUserPhone;
        private string _applicantUserPosition;
        private int? _leaderuserid;
        private string _contractstatusname;
        private string _leaderusername;
        private string _leadercode;
        private string _leaderemployeename;
        private DateTime? _createdate;
        private DateTime? _submitdate;
        private int _status;
        private int _step;
        private string _attachment;
        private int? _attachtype;
        private byte[] _lastupdatetime;
        private int? _bdprojectid;
        private string _bdprojectcode;
        private bool _isfromjoint;
        private int? _branchid;
        private string _branchname;
        private int _checkContract;
        private bool _oldFlag;
        private int _inUse;
        public string ProfileReason { get; set; }
        public int BankId { get; set; }
        public int IsCalculateByVAT { get; set; }
        public int IsDigital { get; set; }
        public bool OldFlag
        {
            get { return _oldFlag; }
            set { _oldFlag = value; }
        }

        public string CustomerCode { get; set; }


        /// <summary>
        /// 是否使用 0：停用 1：使用
        /// </summary>
        public int InUse
        {
            get { return _inUse; }
            set { _inUse = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ProjectId
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
        /// <summary>
        /// 分公司代码
        /// </summary>
        public string BranchCode
        {
            set { _branchcode = value; }
            get { return _branchcode; }
        }
        /// <summary>
        /// 业务类型ID
        /// </summary>
        public int? BusinessTypeID
        {
            set { _businesstypeid = value; }
            get { return _businesstypeid; }
        }
        /// <summary>
        /// 业务类型名称
        /// </summary>
        public string BusinessTypeName
        {
            set { _businesstypename = value; }
            get { return _businesstypename; }
        }
        /// <summary>
        /// 项目组别Id
        /// </summary>
        public int? GroupID
        {
            set { _groupid = value; }
            get { return _groupid; }
        }
        /// <summary>
        /// 项目组别名称
        /// </summary>
        public string GroupName
        {
            set { _groupname = value; }
            get { return _groupname; }
        }
        /// <summary>
        /// 项目类型
        /// </summary>
        public int? ProjectTypeID
        {
            set { _projecttypeid = value; }
            get { return _projecttypeid; }
        }
        /// <summary>
        /// 项目类型名称
        /// </summary>
        public string ProjectTypeName
        {
            set { _projecttypename = value; }
            get { return _projecttypename; }
        }
        /// <summary>
        /// 项目类型代码
        /// </summary>
        public string ProjectTypeCode
        {
            set { _projecttypecode = value; }
            get { return _projecttypecode; }
        }
        /// <summary>
        /// 业务类型ID
        /// </summary>
        public int? BusinessDesID
        {
            set { _businessdesid = value; }
            get { return _businessdesid; }
        }
        /// <summary>
        /// 业务描述
        /// </summary>
        public string BusinessDescription
        {
            set { _businessdescription = value; }
            get { return _businessdescription; }
        }
        /// <summary>
        /// 确认项目号
        /// </summary>
        public string ProjectCode
        {
            set { _projectcode = value; }
            get { return _projectcode; }
        }
        /// <summary>
        /// 业务开始日期
        /// </summary>
        public DateTime? BeginDate
        {
            set { _begindate = value; }
            get { return _begindate; }
        }
        /// <summary>
        /// 业务结束日期
        /// </summary>
        public DateTime? EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 合同总金额
        /// </summary>
        public decimal? TotalAmount
        {
            set { _totalamount = value; }
            get { return _totalamount; }
        }
        /// <summary>
        /// 合同服务费金额
        /// </summary>
        public decimal? ContractServiceFee
        {
            set { _contractservicefee = value; }
            get { return _contractservicefee; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ContractTaxID
        {
            set { _contracttaxid = value; }
            get { return _contracttaxid; }
        }
        /// <summary>
        /// 合同税金
        /// </summary>
        public decimal? ContractTax
        {
            set { _contracttax = value; }
            get { return _contracttax; }
        }
        /// <summary>
        /// 合同成本明细
        /// </summary>
        public string ContractCostDetail
        {
            set { _contractcostdetail = value; }
            get { return _contractcostdetail; }
        }
        /// <summary>
        /// 客户编号
        /// </summary>
        public int? CustomerID
        {
            set { _customerid = value; }
            get { return _customerid; }
        }
        /// <summary>
        /// 是否需要第三方客户发票

        /// </summary>
        public int? IsNeedInvoice
        {
            set { _isneedinvoice = value; }
            get { return _isneedinvoice; }
        }
        /// <summary>
        /// 预计付款周期
        /// </summary>
        public string PayCycle
        {
            set { _paycycle = value; }
            get { return _paycycle; }
        }
        /// <summary>
        /// 合同状态ID
        /// </summary>
        public int? ContractStatusID
        {
            set { _contractstatusid = value; }
            get { return _contractstatusid; }
        }
        /// <summary>
        /// 其他客户特殊要求
        /// </summary>
        public string OtherRequest
        {
            set { _otherrequest = value; }
            get { return _otherrequest; }
        }
        /// <summary>
        /// 创建人sysid
        /// </summary>
        public int CreatorID
        {
            set { _creatorid = value; }
            get { return _creatorid; }
        }
        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string CreatorName
        {
            set { _creatorname = value; }
            get { return _creatorname; }
        }
        /// <summary>
        /// 创建人Code(英文名)
        /// </summary>
        public string CreatorCode
        {
            set { _creatorcode = value; }
            get { return _creatorcode; }
        }
        /// <summary>
        /// 创建人员工编号(例如:00588)
        /// </summary>
        public string CreatorUserID
        {
            set { _creatoruserid = value; }
            get { return _creatoruserid; }
        }
        /// <summary>
        /// 申请人自增长ID
        /// </summary>
        public int ApplicantUserID
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
        /// 申请人公司内的编号如：000518
        /// </summary>
        public string ApplicantCode
        {
            set { _applicantcode = value; }
            get { return _applicantcode; }
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
        /// 申请人Email
        /// </summary>
        public string ApplicantUserEmail
        {
            set { _applicantUserEmail = value; }
            get { return _applicantUserEmail; }
        }

        /// <summary>
        /// 申请人电话
        /// </summary>
        public string ApplicantUserPhone
        {
            get { return _applicantUserPhone; }
            set { _applicantUserPhone = value; }
        }

        /// <summary>
        /// 申请人职位
        /// </summary>
        public string ApplicantUserPosition
        {
            get { return _applicantUserPosition; }
            set { _applicantUserPosition = value; }
        }

        /// <summary>
        /// 项目负责人ID自增长
        /// </summary>
        public int? LeaderUserID
        {
            set { _leaderuserid = value; }
            get { return _leaderuserid; }
        }
        /// <summary>
        /// 合同状态名称
        /// </summary>
        public string ContractStatusName
        {
            set { _contractstatusname = value; }
            get { return _contractstatusname; }
        }
        /// <summary>
        /// 项目负责人登录帐号
        /// </summary>
        public string LeaderUserName
        {
            set { _leaderusername = value; }
            get { return _leaderusername; }
        }
        /// <summary>
        /// 负责人公司内的编号如：000518
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
        /// 创建日期
        /// </summary>
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 提交日期
        /// </summary>
        public DateTime? SubmitDate
        {
            set { _submitdate = value; }
            get { return _submitdate; }
        }
        /// <summary>
        /// 项目号申请单状态
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Step
        {
            set { _step = value; }
            get { return _step; }
        }
        /// <summary>
        /// 合同/确认邮件的附件
        /// </summary>
        public string Attachment
        {
            set { _attachment = value; }
            get { return _attachment; }
        }
        /// <summary>
        /// 附件类型
        /// </summary>
        public int? AttachType
        {
            set { _attachtype = value; }
            get { return _attachtype; }
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
        /// 相关BD项目ID
        /// </summary>
        public int? BDProjectID
        {
            set { _bdprojectid = value; }
            get { return _bdprojectid; }
        }
        /// <summary>
        /// 相关BD项目号
        /// </summary>
        public string BDProjectCode
        {
            set { _bdprojectcode = value; }
            get { return _bdprojectcode; }
        }
        /// <summary>
        /// 项目是否来自合资方
        /// </summary>
        public bool IsFromJoint
        {
            set { _isfromjoint = value; }
            get { return _isfromjoint; }
        }
        /// <summary>
        /// 分公司ID
        /// </summary>
        public int? BranchID
        {
            set { _branchid = value; }
            get { return _branchid; }
        }
        /// <summary>
        /// 分公司名称
        /// </summary>
        public string BranchName
        {
            set { _branchname = value; }
            get { return _branchname; }
        }

        /// <summary>
        /// 检查合同
        /// </summary>
        public int CheckContract
        {
            get { return _checkContract; }
            set { _checkContract = value; }
        }
        #endregion Model



        string _serialCode;
        /// <summary>
        /// 项目流水号
        /// </summary>
        public string SerialCode
        {
            get { return _serialCode; }
            set { _serialCode = value; }
        }
        int _del;
        /// <summary>
        /// 删除标记
        /// </summary>
        public int Del
        {
            get { return _del; }
            set { _del = value; }
        }



        int? _auditorSysUserID;

        /// <summary>
        /// 最后审批人 自增长系统ID
        /// </summary>
        public int? AuditorSysUserID
        {
            get { return _auditorSysUserID; }
            set { _auditorSysUserID = value; }
        }

        string _auditorUserCode;
        /// <summary>
        /// 最后审批人 员工编号
        /// </summary>
        public string AuditorUserCode
        {
            get { return _auditorUserCode; }
            set { _auditorUserCode = value; }
        }

        string _auditorUserName;
        /// <summary>
        /// 最后审批人 登录名
        /// </summary>
        public string AuditorUserName
        {
            get { return _auditorUserName; }
            set { _auditorUserName = value; }
        }

        string _auditorEmployeeName;
        /// <summary>
        /// 最后审批人 真实姓名
        /// </summary>
        public string AuditorEmployeeName
        {
            get { return _auditorEmployeeName; }
            set { _auditorEmployeeName = value; }
        }

        DateTime? _lastAuditDate;
        /// <summary>
        /// 最后审批时间
        /// </summary>
        public DateTime? LastAuditDate
        {
            get { return _lastAuditDate; }
            set { _lastAuditDate = value; }
        }

        string _customerRemark;
        /// <summary>
        /// 客户其它要求
        /// </summary>
        public string CustomerRemark
        {
            get { return _customerRemark; }
            set { _customerRemark = value; }
        }

        string _remark;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

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

    }

}
