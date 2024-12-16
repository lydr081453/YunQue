using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    /// <summary>
    /// 请假单表
    /// </summary>
    public class LeaveInfo : BaseEntityInfo
    {
        public LeaveInfo()
        { }
        #region Model
        private int _id;
        private int _userid;
        private string _usercode;
        private string _employeename;
        private int _leavetype;
        private string _leavetypename;
        private string _leavecause;
        private DateTime _begintime;
        private DateTime _endtime;
        private decimal _leavehours;
        private int _leavestate;
        private int _approveid;
        private int _auditingid;
        private string _approveremark;
        private string _auditingremark;
        private string _leavestatusname;
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
        /// 请假类型 1.病，2.事，3.年，4.产，5.婚，6.丧
        /// </summary>
        public int LeaveType
        {
            set { _leavetype = value; }
            get { return _leavetype; }
        }
        /// <summary>
        /// 请假类型名称
        /// </summary>
        public string LeaveTypeName
        {
            set { _leavetypename = value; }
            get { return _leavetypename; }
        }
        /// <summary>
        /// 请假事由
        /// </summary>
        public string LeaveCause
        {
            set { _leavecause = value; }
            get { return _leavecause; }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime
        {
            set { _begintime = value; }
            get { return _begintime; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }
        /// <summary>
        /// 请假单状态 1.未提交，2.待总监审批，3.待人力确认
        /// </summary>
        public int LeaveState
        {
            set { _leavestate = value; }
            get { return _leavestate; }
        }
        /// <summary>
        /// 总监审批批示
        /// </summary>
        public string ApproveRemark
        {
            set { _approveremark = value; }
            get { return _approveremark; }
        }
        /// <summary>
        /// 人力审核批示
        /// </summary>
        public string AuditingRemark
        {
            set { _auditingremark = value; }
            get { return _auditingremark; }
        }
        /// <summary>
        /// 总监审批ID
        /// </summary>
        public int ApproveID
        {
            set { _approveid = value; }
            get { return _approveid; }
        }
        /// <summary>
        /// 人力审核ID
        /// </summary>
        public int AuditingID
        {
            set { _auditingid = value; }
            get { return _auditingid; }
        }
        /// <summary>
        /// 请假小时数
        /// </summary>
        public decimal Leavehours
        {
            get { return _leavehours; }
            set { _leavehours = value; }
        }
        /// <summary>
        /// 请假单状态名字（内容不入库）
        /// </summary>
        public string LeaveStatusName
        {
            set { _leavestatusname = value; }
            get { return _leavestatusname; }
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
            if (r["LeaveType"].ToString() != "")
            {
                _leavetype = int.Parse(r["LeaveType"].ToString());
            }
            _leavetypename = r["LeaveTypeName"].ToString();
            _leavecause = r["LeaveCause"].ToString();
            var objBeginTime = r["BeginTime"];
            if (!(objBeginTime is DBNull))
            {
                _begintime = (DateTime)objBeginTime;
            }
            var objEndTime = r["EndTime"];
            if (!(objEndTime is DBNull))
            {
                _endtime = (DateTime)objEndTime;
            }
            if (r["Leavehours"].ToString() != "")
            {
                _leavehours = Decimal.Parse(r["Leavehours"].ToString());
            }
            if (r["LeaveState"].ToString() != "")
            {
                _leavestate = int.Parse(r["LeaveState"].ToString());
                if (_leavestate == 1)
                {
                    _leavestatusname = "未提交";
                }
                else if (_leavestate == 2)
                {
                    _leavestatusname = "待总监审批";
                }
                else if (_leavestate == 3)
                {
                    _leavestatusname = "待人力确认";
                }
                else
                {
                    _leavestatusname = "未知状态";
                }
            }
            if (r["ApproveID"].ToString() != "")
            {
                _approveid = int.Parse(r["ApproveID"].ToString());
            }
            _approveremark = r["ApproveRemark"].ToString();
            if (r["AuditingID"].ToString() != "")
            {
                _auditingid = int.Parse(r["AuditingID"].ToString());
            }
            _auditingremark = r["AuditingRemark"].ToString();
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
            if (r["LeaveType"].ToString() != "")
            {
                _leavetype = int.Parse(r["LeaveType"].ToString());
            }
            _leavetypename = r["LeaveTypeName"].ToString();
            _leavecause = r["LeaveCause"].ToString();
            var objBeginTime = r["BeginTime"];
            if (!(objBeginTime is DBNull))
            {
                _begintime = (DateTime)objBeginTime;
            }
            var objEndTime = r["EndTime"];
            if (!(objEndTime is DBNull))
            {
                _endtime = (DateTime)objEndTime;
            }
            if (r["Leavehours"].ToString() != "")
            {
                _leavehours = Decimal.Parse(r["Leavehours"].ToString());
            }
            if (r["LeaveState"].ToString() != "")
            {
                _leavestate = int.Parse(r["LeaveState"].ToString());
                if (_leavestate == 1)
                {
                    _leavestatusname = "未提交";
                }
                else if (_leavestate == 2)
                {
                    _leavestatusname = "待总监审批";
                }
                else if (_leavestate == 3)
                {
                    _leavestatusname = "待人力确认";
                }
                else
                {
                    _leavestatusname = "未知状态";
                }
            }
            if (r["ApproveID"].ToString() != "")
            {
                _approveid = int.Parse(r["ApproveID"].ToString());
            }
            _approveremark = r["ApproveRemark"].ToString();
            if (r["AuditingID"].ToString() != "")
            {
                _auditingid = int.Parse(r["AuditingID"].ToString());
            }
            _auditingremark = r["AuditingRemark"].ToString();
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
        }
    }
}