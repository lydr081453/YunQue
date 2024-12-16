using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    /// <summary>
    /// 员工日常考勤记录表
    /// </summary>
    public class AttendanceInfo : BaseEntityInfo
    {
        public AttendanceInfo()
        { }
        #region Model
        private int _id;
        private int _userid;
        private string _usercode;
        private string _employeename;
        private DateTime _attendancedate;
        private DateTime _goworktime;
        private DateTime _offworktime;
        private bool _islate;
        private bool _isleaveearly;
        private bool _isleave;
        private int _leaveid;
        private int _leavetype;
        private bool _isabsent;
        private decimal _absentdays;
        private bool _isovertime;
        private decimal _overtimehours;
        private bool _isannualleave;
        private decimal _annualleavedays;
        private bool _isevection;
        private int _evectiondays;
        private bool _isegress;
        private decimal _egresshours;
        private string _other;
        private string _remark;
        private bool _isofftune;
        private decimal _offtunehours;
        private int _singleovertimeid;
        
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
        /// 真实姓名
        /// </summary>
        public string EmployeeName
        {
            set { _employeename = value; }
            get { return _employeename; }
        }
        /// <summary>
        /// 实际考勤日期
        /// </summary>
        public DateTime Attendancedate
        {
            get { return _attendancedate; }
            set { _attendancedate = value; }
        }
        /// <summary>
        /// 上班时间
        /// </summary>
        public DateTime GoWorkTime
        {
            set { _goworktime = value; }
            get { return _goworktime; }
        }
        /// <summary>
        /// 下班时间
        /// </summary>
        public DateTime OffWorkTime
        {
            set { _offworktime = value; }
            get { return _offworktime; }
        }
        /// <summary>
        /// 是否迟到 0,表示未迟到，1.表示迟到
        /// </summary>
        public bool IsLate
        {
            set { _islate = value; }
            get { return _islate; }
        }
        /// <summary>
        /// 是否早退 0,表示未早退,1.表示早退
        /// </summary>
        public bool IsLeaveEarly
        {
            set { _isleaveearly = value; }
            get { return _isleaveearly; }
        }
        /// <summary>
        /// 是否请假，0.表示不是请假，1.表示请假
        /// </summary>
        public bool IsLeave
        {
            set { _isleave = value; }
            get { return _isleave; }
        }
        /// <summary>
        /// 请假单ID
        /// </summary>
        public int LeaveID
        {
            set { _leaveid = value; }
            get { return _leaveid; }
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
        /// 是否旷工 0.表示未旷工，1.表示旷工
        /// </summary>
        public bool IsAbsent
        {
            set { _isabsent = value; }
            get { return _isabsent; }
        }
        /// <summary>
        /// 旷工天数存在三种情况0.5、1、0 按天计算
        /// </summary>
        public decimal AbsentDays
        {
            set { _absentdays = value; }
            get { return _absentdays; }
        }
        /// <summary>
        /// 是否OT， 0.表示未OT，1.表示OT
        /// </summary>
        public bool IsOverTime
        {
            set { _isovertime = value; }
            get { return _isovertime; }
        }
        /// <summary>
        /// OT以小时数计算，这个字段保存的是分钟数
        /// </summary>
        public decimal OverTimeHours
        {
            set { _overtimehours = value; }
            get { return _overtimehours; }
        }
        /// <summary>
        /// 是否年休假 0. 表示非年休假，1.表示年休假
        /// </summary>
        public bool IsAnnualLeave
        {
            set { _isannualleave = value; }
            get { return _isannualleave; }
        }
        /// <summary>
        /// 年休假天数 年休假的天数0.5、1天
        /// </summary>
        public decimal AnnualLeaveDays
        {
            set { _annualleavedays = value; }
            get { return _annualleavedays; }
        }
        /// <summary>
        /// 是否出差，0. 表示非出差，1.表示出差
        /// </summary>
        public bool IsEvection
        {
            set { _isevection = value; }
            get { return _isevection; }
        }
        /// <summary>
        /// 出差天数，按照天数计算
        /// </summary>
        public int EvectionDays
        {
            set { _evectiondays = value; }
            get { return _evectiondays; }
        }
        /// <summary>
        /// 是否外出 0非外出，1外出
        /// </summary>
        public bool IsEgress
        {
            set { _isegress = value; }
            get { return _isegress; }
        }
        /// <summary>
        /// 外出以小时数计算，这里保存的是总分钟数
        /// </summary>
        public decimal EgressHours
        {
            set { _egresshours = value; }
            get { return _egresshours; }
        }
        /// <summary>
        /// 其他
        /// </summary>
        public string Other
        {
            set { _other = value; }
            get { return _other; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 是否调休
        /// </summary>
        public bool Isofftune
        {
            get { return _isofftune; }
            set { _isofftune = value; }
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
        /// OT单ID
        /// </summary>
        public int Singleovertimeid
        {
            get { return _singleovertimeid; }
            set { _singleovertimeid = value; }
        }
        #endregion Model

        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public void PopupData(System.Data.IDataReader r)
        {
            if (r["ID"].ToString() != "")
                _id = int.Parse(r["ID"].ToString());

            if (r["UserID"].ToString() != "")
                _userid = int.Parse(r["UserID"].ToString());

            _usercode = r["UserCode"].ToString();
            _employeename = r["EmployeeName"].ToString();
            if (r["AttendanceDate"].ToString() != "")
                _attendancedate = DateTime.Parse(r["AttendanceDate"].ToString());

            if (r["GoWorkTime"].ToString() != "")
                _goworktime = DateTime.Parse(r["GoWorkTime"].ToString());

            if (r["OffWorkTime"].ToString() != "")
                _offworktime = DateTime.Parse(r["OffWorkTime"].ToString());

            if (r["IsLate"].ToString() != "")
            {
                if ((r["IsLate"].ToString() == "1") || (r["IsLate"].ToString().ToLower() == "true"))
                    _islate = true;
                else
                    _islate = false;
            }

            if (r["IsLeaveEarly"].ToString() != "")
            {
                if ((r["IsLeaveEarly"].ToString() == "1") || (r["IsLeaveEarly"].ToString().ToLower() == "true"))
                    _isleaveearly = true;
                else
                    _isleaveearly = false;
            }

            if (r["IsLeave"].ToString() != "")
            {
                if ((r["IsLeave"].ToString() == "1") || (r["IsLeave"].ToString().ToLower() == "true"))
                    _isleave = true;
                else
                    _isleave = false;
            }

            if (r["LeaveID"].ToString() != "")
                _leaveid = int.Parse(r["LeaveID"].ToString());

            if (r["LeaveType"].ToString() != "")
                _leavetype = int.Parse(r["LeaveType"].ToString());

            if (r["IsAbsent"].ToString() != "")
            {
                if ((r["IsAbsent"].ToString() == "1") || (r["IsAbsent"].ToString().ToLower() == "true"))
                    _isabsent = true;
                else
                    _isabsent = false;
            }

            if (r["AbsentDays"].ToString() != "")
                _absentdays = decimal.Parse(r["AbsentDays"].ToString());

            if (r["IsOverTime"].ToString() != "")
            {
                if ((r["IsOverTime"].ToString() == "1") || (r["IsOverTime"].ToString().ToLower() == "true"))
                    _isovertime = true;
                else
                    _isovertime = false;
            }
            if (r["SingleOverTimeID"].ToString() != "")
                _singleovertimeid = int.Parse(r["SingleOverTimeID"].ToString());

            if (r["OverTimeHours"].ToString() != "")
                _overtimehours = decimal.Parse(r["OverTimeHours"].ToString());

            if (r["IsAnnualLeave"].ToString() != "")
            {
                if ((r["IsAnnualLeave"].ToString() == "1") || (r["IsAnnualLeave"].ToString().ToLower() == "true"))
                    _isannualleave = true;
                else
                    _isannualleave = false;
            }

            if (r["AnnualLeaveDays"].ToString() != "")
                _annualleavedays = decimal.Parse(r["AnnualLeaveDays"].ToString());

            if (r["IsEvection"].ToString() != "")
            {
                if ((r["IsEvection"].ToString() == "1") || (r["IsEvection"].ToString().ToLower() == "true"))
                    _isevection = true;
                else
                    _isevection = false;
            }

            if (r["EvectionDays"].ToString() != "")
                _evectiondays = int.Parse(r["EvectionDays"].ToString());

            if (r["IsEgress"].ToString() != "")
            {
                if ((r["IsEgress"].ToString() == "1") || (r["IsEgress"].ToString().ToLower() == "true"))
                    _isegress = true;
                else
                    _isegress = false;
            }

            if (r["EgressHours"].ToString() != "")
            {
                _egresshours = decimal.Parse(r["EgressHours"].ToString());
            }
            _other = r["Other"].ToString();
            _remark = r["Remark"].ToString();
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

            if (r["CreateTime"].ToString() != "")
            {
                CreateTime = DateTime.Parse(r["CreateTime"].ToString());
            }
            if (r["UpdateTime"].ToString() != "")
            {
                UpdateTime = DateTime.Parse(r["UpdateTime"].ToString());
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
            if (r["IsOffTune"].ToString() != "")
            {
                if ((r["IsOffTune"].ToString() == "1") || (r["IsOffTune"].ToString().ToLower() == "true"))
                    _isofftune = true;
                else
                    _isofftune = false;
            }
            if (r["OffTuneHours"].ToString() != "")
            {
                _offtunehours = decimal.Parse(r["OffTuneHours"].ToString());
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
            if (r["AttendanceDate"].ToString() != "")
            {
                _attendancedate = DateTime.Parse(r["AttendanceDate"].ToString());
            }
            if (r["GoWorkTime"].ToString() != "")
            {
                _goworktime = DateTime.Parse(r["GoWorkTime"].ToString());
            }
            if (r["OffWorkTime"].ToString() != "")
            {
                _offworktime = DateTime.Parse(r["OffWorkTime"].ToString());
            }
            if (r["IsLate"].ToString() != "")
            {
                if ((r["IsLate"].ToString() == "1") || (r["IsLate"].ToString().ToLower() == "true"))
                {
                    _islate = true;
                }
                else
                {
                    _islate = false;
                }
            }

            if (r["IsLeaveEarly"].ToString() != "")
            {
                if ((r["IsLeaveEarly"].ToString() == "1") || (r["IsLeaveEarly"].ToString().ToLower() == "true"))
                {
                    _isleaveearly = true;
                }
                else
                {
                    _isleaveearly = false;
                }
            }

            if (r["IsLeave"].ToString() != "")
            {
                if ((r["IsLeave"].ToString() == "1") || (r["IsLeave"].ToString().ToLower() == "true"))
                {
                    _isleave = true;
                }
                else
                {
                    _isleave = false;
                }
            }

            if (r["LeaveID"].ToString() != "")
            {
                _leaveid = int.Parse(r["LeaveID"].ToString());
            }
            if (r["LeaveType"].ToString() != "")
            {
                _leavetype = int.Parse(r["LeaveType"].ToString());
            }
            if (r["IsAbsent"].ToString() != "")
            {
                if ((r["IsAbsent"].ToString() == "1") || (r["IsAbsent"].ToString().ToLower() == "true"))
                {
                    _isabsent = true;
                }
                else
                {
                    _isabsent = false;
                }
            }

            if (r["AbsentDays"].ToString() != "")
            {
                _absentdays = decimal.Parse(r["AbsentDays"].ToString());
            }
            if (r["IsOverTime"].ToString() != "")
            {
                if ((r["IsOverTime"].ToString() == "1") || (r["IsOverTime"].ToString().ToLower() == "true"))
                {
                    _isovertime = true;
                }
                else
                {
                    _isovertime = false;
                }
            }
            if (r["SingleOverTimeID"].ToString() != "")
                _singleovertimeid = int.Parse(r["SingleOverTimeID"].ToString());

            if (r["OverTimeHours"].ToString() != "")
            {
                _overtimehours = decimal.Parse(r["OverTimeHours"].ToString());
            }
            if (r["IsAnnualLeave"].ToString() != "")
            {
                if ((r["IsAnnualLeave"].ToString() == "1") || (r["IsAnnualLeave"].ToString().ToLower() == "true"))
                {
                    _isannualleave = true;
                }
                else
                {
                    _isannualleave = false;
                }
            }

            if (r["AnnualLeaveDays"].ToString() != "")
            {
                _annualleavedays = decimal.Parse(r["AnnualLeaveDays"].ToString());
            }
            if (r["IsEvection"].ToString() != "")
            {
                if ((r["IsEvection"].ToString() == "1") || (r["IsEvection"].ToString().ToLower() == "true"))
                {
                    _isevection = true;
                }
                else
                {
                    _isevection = false;
                }
            }

            if (r["EvectionDays"].ToString() != "")
            {
                _evectiondays = int.Parse(r["EvectionDays"].ToString());
            }
            if (r["IsEgress"].ToString() != "")
            {
                if ((r["IsEgress"].ToString() == "1") || (r["IsEgress"].ToString().ToLower() == "true"))
                {
                    _isegress = true;
                }
                else
                {
                    _isegress = false;
                }
            }

            if (r["EgressHours"].ToString() != "")
            {
                _egresshours = decimal.Parse(r["EgressHours"].ToString());
            }
            _other = r["Other"].ToString();
            _remark = r["Remark"].ToString();
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

            if (r["CreateTime"].ToString() != "")
            {
                CreateTime = DateTime.Parse(r["CreateTime"].ToString());
            }
            if (r["UpdateTime"].ToString() != "")
            {
                UpdateTime = DateTime.Parse(r["UpdateTime"].ToString());
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
            //if (r["IsOffTune"].ToString() != "")
            //{
            //    if ((r["IsOffTune"].ToString() == "1") || (r["IsOffTune"].ToString().ToLower() == "true"))
            //        _isofftune = true;
            //    else
            //        _isofftune = false;
            //}
            //if (r["OffTuneHours"].ToString() != "")
            //{
            //    _offtunehours = decimal.Parse(r["OffTuneHours"].ToString());
            //}
        }
    }
}