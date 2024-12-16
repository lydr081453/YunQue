using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class DimissionFormInfo
    {
        public DimissionFormInfo()
        { }

        #region Model
        private int _dimissionid;
        private int _userid;
        private string _usercode;
        private string _username;
        private int _departmentid;
        private string _departmentname;
        private DateTime? _hopelastday;
        private DateTime? _lastday;
        private DateTime? _appdate;
        private string _reason;
        private int _status;
        private DateTime _createtime;
        private string _mobilephone;
        private string _privatemail;
        private int _financeauditstatus;
        private int _itauditstatus;
        private int _adauditstatus;
        private int _hrauditstatus;
        private int _directorid;
        private string _directorname;
        private int _managerid;
        private string _managername;
        private bool _isnormalper;
        private decimal _sumperformance;
        private decimal _totalindemnityamount;
        public string HRReason{get;set;}
        public string HRReasonRemark{get;set;}
        /// <summary>
        /// 离职预审
        /// </summary>
        public int PreAuditorId { get; set; }
        /// <summary>
        /// 离职预审
        /// </summary>
        public string PreAuditor{get;set;}
        /// <summary>
        /// 离职单编号
        /// </summary>
        public int DimissionId
        {
            set { _dimissionid = value; }
            get { return _dimissionid; }
        }
        /// <summary>
        /// 离职员工ID
        /// </summary>
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 离职员工编号
        /// </summary>
        public string UserCode
        {
            set { _usercode = value; }
            get { return _usercode; }
        }
        /// <summary>
        /// 离职员工姓名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 离职员工部门ID
        /// </summary>
        public int DepartmentId
        {
            set { _departmentid = value; }
            get { return _departmentid; }
        }
        /// <summary>
        /// 离职员工部门名称
        /// </summary>
        public string DepartmentName
        {
            set { _departmentname = value; }
            get { return _departmentname; }
        }
        /// <summary>
        /// 希望离职最后日期
        /// </summary>
        public DateTime? HopeLastDay
        {
            set { _hopelastday = value; }
            get { return _hopelastday; }
        }
        /// <summary>
        /// 离职最后日期
        /// </summary>
        public DateTime? LastDay
        {
            set { _lastday = value; }
            get { return _lastday; }
        }
        /// <summary>
        /// 提交日期
        /// </summary>
        public DateTime? AppDate
        {
            set { _appdate = value; }
            get { return _appdate; }
        }
        /// <summary>
        /// 离职原因
        /// </summary>
        public string Reason
        {
            set { _reason = value; }
            get { return _reason; }
        }
        /// <summary>
        /// 离职单状态
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 手机号码。
        /// </summary>
        public string MobilePhone
        {
            set { _mobilephone = value; }
            get { return _mobilephone; }
        }
        /// <summary>
        /// 私人邮箱
        /// </summary>
        public string PrivateMail
        {
            set { _privatemail = value; }
            get { return _privatemail; }
        }
        /// <summary>
        /// 财务审批状态(暂时没用)
        /// </summary>
        public int FinanceAuditStatus
        {
            set { _financeauditstatus = value; }
            get { return _financeauditstatus; }
        }
        /// <summary>
        /// IT部审批状态
        /// </summary>
        public int ITAuditStatus
        {
            set { _itauditstatus = value; }
            get { return _itauditstatus; }
        }
        /// <summary>
        /// 集团行政审批状态(暂时没用)
        /// </summary>
        public int ADAuditStatus
        {
            set { _adauditstatus = value; }
            get { return _adauditstatus; }
        }
        /// <summary>
        /// 集团人力资源部审批状态
        /// </summary>
        public int HRAuditStatus
        {
            set { _hrauditstatus = value; }
            get { return _hrauditstatus; }
        }
        /// <summary>
        /// 总监ID
        /// </summary>
        public int DirectorId
        {
            set { _directorid = value; }
            get { return _directorid; }
        }
        /// <summary>
        /// 总监姓名
        /// </summary>
        public string DirectorName
        {
            set { _directorname = value; }
            get { return _directorname; }
        }
        /// <summary>
        /// 总经理ID
        /// </summary>
        public int ManagerId
        {
            set { _managerid = value; }
            get { return _managerid; }
        }
        /// <summary>
        /// 总经理姓名
        /// </summary>
        public string ManagerName
        {
            set { _managername = value; }
            get { return _managername; }
        }
        /// <summary>
        /// 绩效是否按实际工作日计算
        /// </summary>
        public bool IsNormalPer
        {
            set { _isnormalper = value; }
            get { return _isnormalper; }
        }
        /// <summary>
        /// 如果不是按实际工作日结算，总经理填写的金额数
        /// </summary>
        public decimal SumPerformance
        {
            set { _sumperformance = value; }
            get { return _sumperformance; }
        }
        /// <summary>
        /// 员工赔偿总金额
        /// </summary>
        public decimal TotalIndemnityAmount
        {
            set { _totalindemnityamount = value; }
            get { return _totalindemnityamount; }
        }
        #endregion Model

        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public void PopupData(System.Data.IDataReader r)
        {
            if (r["DimissionId"].ToString() != "")
            {
                _dimissionid = int.Parse(r["DimissionId"].ToString());
            }
            if (r["UserId"].ToString() != "")
            {
                _userid = int.Parse(r["UserId"].ToString());
            }
            _usercode = r["UserCode"].ToString();
            _username = r["UserName"].ToString();
            if (r["DepartmentId"].ToString() != "")
            {
                _departmentid = int.Parse(r["DepartmentId"].ToString());
            }
            _departmentname = r["DepartmentName"].ToString();
            var objHopeLastDay = r["HopeLastDay"];
            if (!(objHopeLastDay is DBNull))
            {
                _hopelastday = (DateTime)objHopeLastDay;
            }
            var objLastDay = r["LastDay"];
            if (!(objLastDay is DBNull))
            {
                _lastday = (DateTime)objLastDay;
            }
            var objAppDate = r["AppDate"];
            if (!(objAppDate is DBNull))
            {
                _appdate = (DateTime)objAppDate;
            }
            _reason = r["Reason"].ToString();
            if (r["Status"].ToString() != "")
            {
                _status = int.Parse(r["Status"].ToString());
            }
            var objCreateTime = r["CreateTime"];
            if (!(objCreateTime is DBNull))
            {
                _createtime = (DateTime)objCreateTime;
            }
            _mobilephone = r["MobilePhone"].ToString();
            _privatemail = r["PrivateMail"].ToString();
            if (r["FinanceAuditStatus"].ToString() != "")
            {
                _financeauditstatus = int.Parse(r["FinanceAuditStatus"].ToString());
            }
            if (r["ITAuditStatus"].ToString() != "")
            {
                _itauditstatus = int.Parse(r["ITAuditStatus"].ToString());
            }
            if (r["ADAuditStatus"].ToString() != "")
            {
                _adauditstatus = int.Parse(r["ADAuditStatus"].ToString());
            }
            if (r["HRAuditStatus"].ToString() != "")
            {
                _hrauditstatus = int.Parse(r["HRAuditStatus"].ToString());
            }
            if (r["DirectorId"].ToString() != "")
            {
                _directorid = int.Parse(r["DirectorId"].ToString());
            }
            _directorname = r["DirectorName"].ToString();
            if (r["ManagerId"].ToString() != "")
            {
                _managerid = int.Parse(r["ManagerId"].ToString());
            }
            _managername = r["ManagerName"].ToString();
            if (r["IsNormalPer"].ToString() != "")
            {
                if ((r["IsNormalPer"].ToString() == "1") || (r["IsNormalPer"].ToString().ToLower() == "true"))
                {
                    _isnormalper = true;
                }
                else
                {
                    _isnormalper = false;
                }
            }
            if (r["SumPerformance"].ToString() != "")
            {
                _sumperformance = decimal.Parse(r["SumPerformance"].ToString());
            }
            if (r["TotalIndemnityAmount"].ToString() != "")
            {
                _totalindemnityamount = decimal.Parse(r["TotalIndemnityAmount"].ToString());
            }

            if (r["PreAuditorId"].ToString() != "")
            {
                PreAuditorId = int.Parse(r["PreAuditorId"].ToString());
            }
            PreAuditor = r["PreAuditor"].ToString();

        }

        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public void PopupData(System.Data.DataRow r)
        {
            if (r["DimissionId"].ToString() != "")
            {
                _dimissionid = int.Parse(r["DimissionId"].ToString());
            }
            if (r["UserId"].ToString() != "")
            {
                _userid = int.Parse(r["UserId"].ToString());
            }
            _usercode = r["UserCode"].ToString();
            _username = r["UserName"].ToString();
            if (r["DepartmentId"].ToString() != "")
            {
                _departmentid = int.Parse(r["DepartmentId"].ToString());
            }
            _departmentname = r["DepartmentName"].ToString();

            var objHopeLastDay = r["HopeLastDay"];
            if (!(objHopeLastDay is DBNull))
            {
                _hopelastday = (DateTime)objHopeLastDay;
            }
            var objLastDay = r["LastDay"];
            if (!(objLastDay is DBNull))
            {
                _lastday = (DateTime)objLastDay;
            }
            var objAppDate = r["AppDate"];
            if (!(objAppDate is DBNull))
            {
                _appdate = (DateTime)objAppDate;
            }
            _reason = r["Reason"].ToString();
            if (r["Status"].ToString() != "")
            {
                _status = int.Parse(r["Status"].ToString());
            }
            var objCreateTime = r["CreateTime"];
            if (!(objCreateTime is DBNull))
            {
                _createtime = (DateTime)objCreateTime;
            }
            _mobilephone = r["MobilePhone"].ToString();
            _privatemail = r["PrivateMail"].ToString();
            if (r["FinanceAuditStatus"].ToString() != "")
            {
                _financeauditstatus = int.Parse(r["FinanceAuditStatus"].ToString());
            }
            if (r["ITAuditStatus"].ToString() != "")
            {
                _itauditstatus = int.Parse(r["ITAuditStatus"].ToString());
            }
            if (r["ADAuditStatus"].ToString() != "")
            {
                _adauditstatus = int.Parse(r["ADAuditStatus"].ToString());
            }
            if (r["HRAuditStatus"].ToString() != "")
            {
                _hrauditstatus = int.Parse(r["HRAuditStatus"].ToString());
            }
            if (r["DirectorId"].ToString() != "")
            {
                _directorid = int.Parse(r["DirectorId"].ToString());
            }
            _directorname = r["DirectorName"].ToString();
            if (r["ManagerId"].ToString() != "")
            {
                _managerid = int.Parse(r["ManagerId"].ToString());
            }
            _managername = r["ManagerName"].ToString();
            if (r["IsNormalPer"].ToString() != "")
            {
                if ((r["IsNormalPer"].ToString() == "1") || (r["IsNormalPer"].ToString().ToLower() == "true"))
                {
                    _isnormalper = true;
                }
                else
                {
                    _isnormalper = false;
                }
            }
            if (r["SumPerformance"].ToString() != "")
            {
                _sumperformance = decimal.Parse(r["SumPerformance"].ToString());
            }
            if (r["TotalIndemnityAmount"].ToString() != "")
            {
                _totalindemnityamount = decimal.Parse(r["TotalIndemnityAmount"].ToString());
            }
            HRReason = r["HRReason"].ToString();
            HRReasonRemark = r["HRReasonRemark"].ToString();

            if (r["PreAuditorId"].ToString() != "")
            {
                PreAuditorId = int.Parse(r["PreAuditorId"].ToString());
            }
            PreAuditor = r["PreAuditor"].ToString();
        }
    }
}
