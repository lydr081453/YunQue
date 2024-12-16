using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    /// <summary>
    /// 员工各月考勤统计信息表
    /// </summary>
    public class MonthStatInfo : BaseEntityInfo
    {
        public MonthStatInfo()
        { }
        #region Model
        private int _id;
        private int _userid;
        private string _usercode;
        private string _employeename;
        private int _year;
        private int _month;
        private decimal _workhours;
        private int _latecount;
        private int _leaveearlycount;
        private decimal _absentdays;
        private decimal _overtimehours;
        private decimal _annualleavedays;
        private decimal _evectiondays;
        private decimal _egresshours;
        private int _state;
        private int _approveid;
        private DateTime _approvetime = DateTime.Parse("1900-01-01");
        private string _approveremark;
        private int _statisticiansid;
        private DateTime _statisticianstime = DateTime.Parse("1900-01-01");
        private int _hradminid;
        private DateTime _hradmintime = DateTime.Parse("1900-01-01");
        private int _managerid;
        private DateTime _managertime = DateTime.Parse("1900-01-01");
        private int _adadminid;
        private DateTime _adadmintime = DateTime.Parse("1900-01-01");
        private decimal _deductsum;
        private decimal _sickleavehours;
        private decimal _affairleavehours;
        private decimal _maternityleavehours;
        private decimal _marriageleavehours;
        private decimal _funeralleavehours;
        private decimal _offtunehours;
        private decimal _overannualleavedays;
        private decimal _holidayovertimehours;
        private decimal _prenatalcheckhours;
        private decimal _incentivehours;
        private string _other;
        private int _attendancesubtype;
        private bool _ishavepcrefund;
        private int _pcrefundamount;
        public decimal LastAnnualDays { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 员工编号
        /// </summary>
        public string UserCode
        {
            set { _usercode = value; }
            get { return _usercode; }
        }
        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string EmployeeName
        {
            set { _employeename = value; }
            get { return _employeename; }
        }
        /// <summary>
        /// 年份
        /// </summary>
        public int Year
        {
            set { _year = value; }
            get { return _year; }
        }
        /// <summary>
        /// 月份
        /// </summary>
        public int Month
        {
            set { _month = value; }
            get { return _month; }
        }
        /// <summary>
        /// 工作总小时数
        /// </summary>
        public decimal WorkHours
        {
            set { _workhours = value; }
            get { return _workhours; }
        }
        /// <summary>
        /// 迟到次数
        /// </summary>
        public int LateCount
        {
            set { _latecount = value; }
            get { return _latecount; }
        }
        /// <summary>
        /// 早退次数
        /// </summary>
        public int LeaveEarlyCount
        {
            set { _leaveearlycount = value; }
            get { return _leaveearlycount; }
        }
        /// <summary>
        /// 病假小时数
        /// </summary>
        public decimal SickLeaveHours
        {
            set { _sickleavehours = value; }
            get { return _sickleavehours; }
        }
        /// <summary>
        /// 事假小时数
        /// </summary>
        public decimal AffairLeaveHours
        {
            set { _affairleavehours = value; }
            get { return _affairleavehours; }
        }
        /// <summary>
        /// 年休假天数
        /// </summary>
        public decimal AnnualLeaveDays
        {
            set { _annualleavedays = value; }
            get { return _annualleavedays; }
        }
        /// <summary>
        /// 产假天数
        /// </summary>
        public decimal MaternityLeaveHours
        {
            set { _maternityleavehours = value; }
            get { return _maternityleavehours; }
        }
        /// <summary>
        /// 婚假天数
        /// </summary>
        public decimal MarriageLeaveHours
        {
            set { _marriageleavehours = value; }
            get { return _marriageleavehours; }
        }
        /// <summary>
        /// 丧假天数
        /// </summary>
        public decimal FuneralLeaveHours
        {
            set { _funeralleavehours = value; }
            get { return _funeralleavehours; }
        }
        /// <summary>
        /// 旷工天数
        /// </summary>
        public decimal AbsentDays
        {
            set { _absentdays = value; }
            get { return _absentdays; }
        }
        /// <summary>
        /// OT小时数
        /// </summary>
        public decimal OverTimeHours
        {
            set { _overtimehours = value; }
            get { return _overtimehours; }
        }
        /// <summary>
        /// 出差天数
        /// </summary>
        public decimal EvectionDays
        {
            set { _evectiondays = value; }
            get { return _evectiondays; }
        }
        /// <summary>
        /// 外出小时数
        /// </summary>
        public decimal EgressHours
        {
            set { _egresshours = value; }
            get { return _egresshours; }
        }
        /// <summary>
        /// 考勤统计信息状态
        /// </summary>
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }
        /// <summary>
        /// 总监审批人ID
        /// </summary>
        public int ApproveID
        {
            set { _approveid = value; }
            get { return _approveid; }
        }
        /// <summary>
        /// 总监审批时间
        /// </summary>
        public DateTime ApproveTime
        {
            set { _approvetime = value; }
            get { return _approvetime; }
        }
        /// <summary>
        /// 审批备注
        /// </summary>
        public string ApproveRemark
        {
            set { _approveremark = value; }
            get { return _approveremark; }
        }
        /// <summary>
        /// 考勤统计员ID
        /// </summary>
        public int StatisticiansID
        {
            set { _statisticiansid = value; }
            get { return _statisticiansid; }
        }
        /// <summary>
        /// 考勤统计员审批时间
        /// </summary>
        public DateTime StatisticiansTime
        {
            set { _statisticianstime = value; }
            get { return _statisticianstime; }
        }
        /// <summary>
        /// HRAdminID
        /// </summary>
        public int HRAdminID
        {
            set { _hradminid = value; }
            get { return _hradminid; }
        }
        /// <summary>
        /// HRAdmin审批时间
        /// </summary>
        public DateTime HRAdminTime
        {
            set { _hradmintime = value; }
            get { return _hradmintime; }
        }
        /// <summary>
        /// 团队经理ID
        /// </summary>
        public int ManagerID
        {
            set { _managerid = value; }
            get { return _managerid; }
        }
        /// <summary>
        /// 团队经理审批时间
        /// </summary>
        public DateTime ManagerTime
        {
            set { _managertime = value; }
            get { return _managertime; }
        }
        /// <summary>
        /// 考勤管理员ID
        /// </summary>
        public int ADAdminID
        {
            set { _adadminid = value; }
            get { return _adadminid; }
        }
        /// <summary>
        /// 考勤管理员审批时间
        /// </summary>
        public DateTime ADAdminTime
        {
            set { _adadmintime = value; }
            get { return _adadmintime; }
        }
        /// <summary>
        /// 实扣金额数
        /// </summary>
        public decimal DeductSum
        {
            set { _deductsum = value; }
            get { return _deductsum; }
        }
        /// <summary>
        /// 调休小时数
        /// </summary>
        public decimal Offtunehours
        {
            get { return _offtunehours; }
            set { _offtunehours = value; }
        }
        /// <summary>
        /// 剩余年假天数
        /// </summary>
        public decimal Overannualleavedays
        {
            get { return _overannualleavedays; }
            set { _overannualleavedays = value; }
        }
        /// <summary>
        /// 节假日OT
        /// </summary>
        public decimal HolidayOverTimeHours
        {
            set { _holidayovertimehours = value; }
            get { return _holidayovertimehours; }
        }
        /// <summary>
        /// 产前检查
        /// </summary>
        public decimal PrenatalCheckHours
        {
            set { _prenatalcheckhours = value; }
            get { return _prenatalcheckhours; }
        }
        /// <summary>
        /// 奖励假
        /// </summary>
        public decimal IncentiveHours
        {
            set { _incentivehours = value; }
            get { return _incentivehours; }
        }
        /// <summary>
        /// 其他
        /// </summary>
        public string Other
        {
            get { return _other; }
            set { _other = value; }
        }
        /// <summary>
        /// 其他
        /// </summary>
        public int AttendanceSubType
        {
            get { return _attendancesubtype; }
            set { _attendancesubtype = value; }
        }
        /// <summary>
        /// 当月是否有笔记本报销
        /// </summary>
        public bool IsHavePCRefund
        {
            set { _ishavepcrefund = value; }
            get { return _ishavepcrefund; }
        }
        /// <summary>
        /// 当月笔记本报销金额
        /// </summary>
        public int PCRefundAmount
        {
            set { _pcrefundamount = value; }
            get { return _pcrefundamount; }
        }
        #endregion Model

        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public void PopupData(System.Data.IDataReader r)
        {
            if (r["ID"].ToString() != "")
            {
                _id = int.Parse(r["ID"].ToString());
            }
            if (r["UserID"].ToString() != "")
            {
                _userid = int.Parse(r["UserID"].ToString());
            }
            _usercode = r["UserCode"].ToString();
            _employeename = r["EmployeeName"].ToString();
            if (r["Year"].ToString() != "")
            {
                _year = int.Parse(r["Year"].ToString());
            }
            if (r["Month"].ToString() != "")
            {
                _month = int.Parse(r["Month"].ToString());
            }
            if (r["WorkHours"].ToString() != "")
            {
                _workhours = decimal.Parse(r["WorkHours"].ToString());
            }
            if (r["LateCount"].ToString() != "")
            {
                _latecount = int.Parse(r["LateCount"].ToString());
            }
            if (r["LeaveEarlyCount"].ToString() != "")
            {
                _leaveearlycount = int.Parse(r["LeaveEarlyCount"].ToString());
            }
            if (r["SickLeaveHours"].ToString() != "")
            {
                _sickleavehours = decimal.Parse(r["SickLeaveHours"].ToString());
            }
            if (r["AffairLeaveHours"].ToString() != "")
            {
                _affairleavehours = decimal.Parse(r["AffairLeaveHours"].ToString());
            }
            if (r["MaternityLeaveHours"].ToString() != "")
            {
                _maternityleavehours = decimal.Parse(r["MaternityLeaveHours"].ToString());
            }
            if (r["MarriageLeaveHours"].ToString() != "")
            {
                _marriageleavehours = decimal.Parse(r["MarriageLeaveHours"].ToString());
            }
            if (r["FuneralLeaveHours"].ToString() != "")
            {
                _funeralleavehours = decimal.Parse(r["FuneralLeaveHours"].ToString());
            }
            if (r["AbsentDays"].ToString() != "")
            {
                _absentdays = decimal.Parse(r["AbsentDays"].ToString());
            }
            if (r["OverTimeHours"].ToString() != "")
            {
                _overtimehours = decimal.Parse(r["OverTimeHours"].ToString());
            }
            if (r["AnnualLeaveDays"].ToString() != "")
            {
                _annualleavedays = decimal.Parse(r["AnnualLeaveDays"].ToString());
            }
            if (r["LastAnnualDays"].ToString() != "")
            {
                LastAnnualDays = decimal.Parse(r["LastAnnualDays"].ToString());
            }
            if (r["OverAnnualLeaveDays"].ToString() != "")
            {
                _overannualleavedays = decimal.Parse(r["OverAnnualLeaveDays"].ToString());
            }
            if (r["EvectionDays"].ToString() != "")
            {
                _evectiondays = decimal.Parse(r["EvectionDays"].ToString());
            }
            if (r["EgressHours"].ToString() != "")
            {
                _egresshours = decimal.Parse(r["EgressHours"].ToString());
            }
            if (r["State"].ToString() != "")
            {
                _state = int.Parse(r["State"].ToString());
            }
            if (r["ApproveID"].ToString() != "")
            {
                _approveid = int.Parse(r["ApproveID"].ToString());
            }
            var objApproveTime = r["ApproveTime"];
            if (!(objApproveTime is DBNull))
            {
                _approvetime = (DateTime)objApproveTime;
            }
            _other = r["other"].ToString();
            _approveremark = r["ApproveRemark"].ToString();
            if (r["StatisticiansID"].ToString() != "")
            {
                _statisticiansid = int.Parse(r["StatisticiansID"].ToString());
            }
            // StatisticiansTime
            var objStatisticiansTime = r["StatisticiansTime"];
            if (!(objStatisticiansTime is DBNull))
            {
                _statisticianstime = (DateTime)objStatisticiansTime;
            }
            if (r["HRAdminID"].ToString() != "")
            {
                _hradminid = int.Parse(r["HRAdminID"].ToString());
            }
            var objHRAdminTime = r["HRAdminTime"];
            if (!(objHRAdminTime is DBNull))
            {
                _hradmintime = (DateTime)objHRAdminTime;
            }
            if (r["ManagerID"].ToString() != "")
            {
                _managerid = int.Parse(r["ManagerID"].ToString());
            }
            var objManagerTime = r["ManagerTime"];
            if (!(objManagerTime is DBNull))
            {
                _managertime = (DateTime)objManagerTime;
            }
            if (r["ADAdminID"].ToString() != "")
            {
                _adadminid = int.Parse(r["ADAdminID"].ToString());
            }
            var objADAdminTime = r["ADAdminTime"];
            if (!(objADAdminTime is DBNull))
            {
                _adadmintime = (DateTime)objADAdminTime;
            }
            if (r["DeductSum"].ToString() != "")
            {
                _deductsum = decimal.Parse(r["DeductSum"].ToString());
            }
            if (r["Deleted"].ToString() != "")
            {
                if ((r["Deleted"].ToString() == "1") || (r["Deleted"].ToString().ToLower() == "true"))
                {
                    Deleted = true;
                }
                else
                {
                    Deleted = false;
                }
            }
            var objCreateTime = r["CreateTime"];
            if (!(objCreateTime is DBNull))
            {
                CreateTime = (DateTime)objCreateTime;
            }
            var objUpdateTime = r["UpdateTime"];
            if (!(objUpdateTime is DBNull))
            {
                UpdateTime = (DateTime)objUpdateTime;
            }
            if (r["OperateorID"].ToString() != "")
            {
                OperateorID = int.Parse(r["OperateorID"].ToString());
            }
            if (r["OperateorDept"].ToString() != "")
            {
                OperateorDept = int.Parse(r["OperateorDept"].ToString());
            }
            if (r["Sort"].ToString() != "")
            {
                Sort = int.Parse(r["Sort"].ToString());
            }
            if (r["OffTuneHours"].ToString() != "")
            {
                _offtunehours = decimal.Parse(r["OffTuneHours"].ToString());
            }
            if (r["HolidayOverTimeHours"].ToString() != "")
            {
                _holidayovertimehours = decimal.Parse(r["HolidayOverTimeHours"].ToString());
            }
            if (r["PrenatalCheckHours"].ToString() != "")
            {
                _prenatalcheckhours = decimal.Parse(r["PrenatalCheckHours"].ToString());
            }
            if (r["IncentiveHours"].ToString() != "")
            {
                _incentivehours = decimal.Parse(r["IncentiveHours"].ToString());
            }
            if (r["AttendanceSubType"].ToString() != "")
            {
                _attendancesubtype = int.Parse(r["AttendanceSubType"].ToString());
            }
            if (r["IsHavePCRefund"].ToString() != "")
            {
                if ((r["IsHavePCRefund"].ToString() == "1") || (r["IsHavePCRefund"].ToString().ToLower() == "true"))
                {
                    _ishavepcrefund = true;
                }
                else
                {
                    _ishavepcrefund = false;
                }
            }
            if (r["PCRefundAmount"].ToString() != "")
            {
                _pcrefundamount = int.Parse(r["PCRefundAmount"].ToString());
            }
        }

        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public void PopupData(System.Data.DataRow r)
        {
            if (r["ID"].ToString() != "")
            {
                _id = int.Parse(r["ID"].ToString());
            }
            if (r["UserID"].ToString() != "")
            {
                _userid = int.Parse(r["UserID"].ToString());
            }
            _usercode = r["UserCode"].ToString();
            _employeename = r["EmployeeName"].ToString();
            if (r["Year"].ToString() != "")
            {
                _year = int.Parse(r["Year"].ToString());
            }
            if (r["Month"].ToString() != "")
            {
                _month = int.Parse(r["Month"].ToString());
            }
            if (r["WorkHours"].ToString() != "")
            {
                _workhours = decimal.Parse(r["WorkHours"].ToString());
            }
            if (r["LateCount"].ToString() != "")
            {
                _latecount = int.Parse(r["LateCount"].ToString());
            }
            if (r["LeaveEarlyCount"].ToString() != "")
            {
                _leaveearlycount = int.Parse(r["LeaveEarlyCount"].ToString());
            }
            if (r["SickLeaveHours"].ToString() != "")
            {
                _sickleavehours = decimal.Parse(r["SickLeaveHours"].ToString());
            }
            if (r["AffairLeaveHours"].ToString() != "")
            {
                _affairleavehours = decimal.Parse(r["AffairLeaveHours"].ToString());
            }
            if (r["MaternityLeaveHours"].ToString() != "")
            {
                _maternityleavehours = decimal.Parse(r["MaternityLeaveHours"].ToString());
            }
            if (r["MarriageLeaveHours"].ToString() != "")
            {
                _marriageleavehours = decimal.Parse(r["MarriageLeaveHours"].ToString());
            }
            if (r["FuneralLeaveHours"].ToString() != "")
            {
                _funeralleavehours = decimal.Parse(r["FuneralLeaveHours"].ToString());
            }
            if (r["AbsentDays"].ToString() != "")
            {
                _absentdays = decimal.Parse(r["AbsentDays"].ToString());
            }
            if (r["OverTimeHours"].ToString() != "")
            {
                _overtimehours = decimal.Parse(r["OverTimeHours"].ToString());
            }
            if (r["AnnualLeaveDays"].ToString() != "")
            {
                _annualleavedays = decimal.Parse(r["AnnualLeaveDays"].ToString());
            }
            if (r["LastAnnualDays"].ToString() != "")
            {
                LastAnnualDays = decimal.Parse(r["LastAnnualDays"].ToString());
            }
            if (r["OverAnnualLeaveDays"].ToString() != "")
            {
                _overannualleavedays = decimal.Parse(r["OverAnnualLeaveDays"].ToString());
            }
            if (r["EvectionDays"].ToString() != "")
            {
                _evectiondays = decimal.Parse(r["EvectionDays"].ToString());
            }
            if (r["EgressHours"].ToString() != "")
            {
                _egresshours = decimal.Parse(r["EgressHours"].ToString());
            }
            if (r["State"].ToString() != "")
            {
                _state = int.Parse(r["State"].ToString());
            }
            if (r["ApproveID"].ToString() != "")
            {
                _approveid = int.Parse(r["ApproveID"].ToString());
            }
            var objApproveTime = r["ApproveTime"];
            if (!(objApproveTime is DBNull))
            {
                _approvetime = (DateTime)objApproveTime;
            }
            _other = r["other"].ToString();
            _approveremark = r["ApproveRemark"].ToString();
            if (r["StatisticiansID"].ToString() != "")
            {
                _statisticiansid = int.Parse(r["StatisticiansID"].ToString());
            }
            // StatisticiansTime
            var objStatisticiansTime = r["StatisticiansTime"];
            if (!(objStatisticiansTime is DBNull))
            {
                _statisticianstime = (DateTime)objStatisticiansTime;
            }
            if (r["HRAdminID"].ToString() != "")
            {
                _hradminid = int.Parse(r["HRAdminID"].ToString());
            }
            var objHRAdminTime = r["HRAdminTime"];
            if (!(objHRAdminTime is DBNull))
            {
                _hradmintime = (DateTime)objHRAdminTime;
            }
            if (r["ManagerID"].ToString() != "")
            {
                _managerid = int.Parse(r["ManagerID"].ToString());
            }
            var objManagerTime = r["ManagerTime"]; 
            if (!(objManagerTime is DBNull))
            {
                _managertime = (DateTime)objManagerTime;
            }
            if (r["ADAdminID"].ToString() != "")
            {
                _adadminid = int.Parse(r["ADAdminID"].ToString());
            }
            var objADAdminTime = r["ADAdminTime"];
            if (!(objADAdminTime is DBNull))
            {
                _adadmintime = (DateTime)objADAdminTime;
            }
            if (r["DeductSum"].ToString() != "")
            {
                _deductsum = decimal.Parse(r["DeductSum"].ToString());
            }
            if (r["Deleted"].ToString() != "")
            {
                if ((r["Deleted"].ToString() == "1") || (r["Deleted"].ToString().ToLower() == "true"))
                {
                    Deleted = true;
                }
                else
                {
                    Deleted = false;
                }
            }
            var objCreateTime = r["CreateTime"];
            if (!(objCreateTime is DBNull))
            {
                CreateTime = (DateTime)objCreateTime;
            }
            var objUpdateTime = r["UpdateTime"];
            if (!(objUpdateTime is DBNull))
            {
                UpdateTime = (DateTime)objUpdateTime;
            }
            if (r["OperateorID"].ToString() != "")
            {
                OperateorID = int.Parse(r["OperateorID"].ToString());
            }
            if (r["OperateorDept"].ToString() != "")
            {
                OperateorDept = int.Parse(r["OperateorDept"].ToString());
            }
            if (r["Sort"].ToString() != "")
            {
                Sort = int.Parse(r["Sort"].ToString());
            }
            if (r["OffTuneHours"].ToString() != "")
            {
                _offtunehours = decimal.Parse(r["OffTuneHours"].ToString());
            }
            if (r["HolidayOverTimeHours"].ToString() != "")
            {
                _holidayovertimehours = decimal.Parse(r["HolidayOverTimeHours"].ToString());
            }
            if (r["PrenatalCheckHours"].ToString() != "")
            {
                _prenatalcheckhours = decimal.Parse(r["PrenatalCheckHours"].ToString());
            }
            if (r["IncentiveHours"].ToString() != "")
            {
                _incentivehours = decimal.Parse(r["IncentiveHours"].ToString());
            }
            if (r["AttendanceSubType"].ToString() != "")
            {
                _attendancesubtype = int.Parse(r["AttendanceSubType"].ToString());
            }
            if (r["IsHavePCRefund"].ToString() != "")
            {
                if ((r["IsHavePCRefund"].ToString() == "1") || (r["IsHavePCRefund"].ToString().ToLower() == "true"))
                {
                    _ishavepcrefund = true;
                }
                else
                {
                    _ishavepcrefund = false;
                }
            }
            if (r["PCRefundAmount"].ToString() != "")
            {
                _pcrefundamount = int.Parse(r["PCRefundAmount"].ToString());
            }
        }
    }
}