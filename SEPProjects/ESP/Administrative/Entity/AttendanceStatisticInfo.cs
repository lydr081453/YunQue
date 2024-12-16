using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    /// <summary>
    /// 考勤统计数据对象
    /// </summary>
    public class AttendanceStatisticInfo
    {
        public AttendanceStatisticInfo()
        { }

        #region Model
        private int _id;
        private int _userid;
        private string _usercode;
        private string _username;
        private string _employeename;
        private int _level1id;
        private int _level2id;
        private int _level3id;
        private string _department;
        private string _position;
        private int _attendanceyear;
        private int _attendancemonth;
        private int _latecount1;
        private int _latecount2;
        private int _overtimecount;
        private int _absentcount1;
        private int _absentcount2;
        private int _absentcount3;
        private int _leaveearly;
        private int _attendancetype;

        /// <summary>
        /// 编号
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 用户系统编号
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
        /// 用户登陆名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string EmployeeName
        {
            set { _employeename = value; }
            get { return _employeename; }
        }
        /// <summary>
        /// 一级部门ID
        /// </summary>
        public int Level1ID
        {
            set { _level1id = value; }
            get { return _level1id; }
        }
        /// <summary>
        /// 二级部门ID
        /// </summary>
        public int Level2ID
        {
            set { _level2id = value; }
            get { return _level2id; }
        }
        /// <summary>
        /// 三级部门ID
        /// </summary>
        public int Level3ID
        {
            set { _level3id = value; }
            get { return _level3id; }
        }
        /// <summary>
        /// 部门
        /// </summary>
        public string Department
        {
            set { _department = value; }
            get { return _department; }
        }
        /// <summary>
        /// 职位
        /// </summary>
        public string Position
        {
            set { _position = value; }
            get { return _position; }
        }
        /// <summary>
        /// 考勤月份
        /// </summary>
        public int AttendanceYear
        {
            set { _attendanceyear = value; }
            get { return _attendanceyear; }
        }
        /// <summary>
        /// 考勤月份
        /// </summary>
        public int AttendanceMonth
        {
            set { _attendancemonth = value; }
            get { return _attendancemonth; }
        }
        /// <summary>
        /// 迟到30分钟内的次数
        /// </summary>
        public int LateCount1
        {
            set { _latecount1 = value; }
            get { return _latecount1; }
        }
        /// <summary>
        /// 迟到30分钟以上的次数
        /// </summary>
        public int LateCount2
        {
            set { _latecount2 = value; }
            get { return _latecount2; }
        }
        /// <summary>
        /// OT次数
        /// </summary>
        public int OverTimeCount
        {
            set { _overtimecount = value; }
            get { return _overtimecount; }
        }
        /// <summary>
        /// 旷工半天次数
        /// </summary>
        public int AbsentCount1
        {
            set { _absentcount1 = value; }
            get { return _absentcount1; }
        }
        /// <summary>
        /// 旷工一天次数
        /// </summary>
        public int AbsentCount2
        {
            set { _absentcount2 = value; }
            get { return _absentcount2; }
        }
        /// <summary>
        /// 上、下班打卡记录不全次数
        /// </summary>
        public int AbsentCount3
        {
            set { _absentcount3 = value; }
            get { return _absentcount3; }
        }
        /// <summary>
        /// 早退次数
        /// </summary>
        public int LeaveEarly
        {
            set { _leaveearly = value; }
            get { return _leaveearly; }
        }
        /// <summary>
        /// 早退次数
        /// </summary>
        public int AttendanceType
        {
            set { _attendancetype = value; }
            get { return _attendancetype; }
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
            _username = r["UserName"].ToString();
            _employeename = r["EmployeeName"].ToString();
            if (r["Level1ID"].ToString() != "")
            {
                _level1id = int.Parse(r["Level1ID"].ToString());
            }
            if (r["Level2ID"].ToString() != "")
            {
                _level2id = int.Parse(r["Level2ID"].ToString());
            }
            if (r["Level3ID"].ToString() != "")
            {
                _level3id = int.Parse(r["Level3ID"].ToString());
            }
            _department = r["Department"].ToString();
            _position = r["Position"].ToString();
            if (r["AttendanceYear"].ToString() != "")
            {
                _attendanceyear = int.Parse(r["AttendanceYear"].ToString());
            }
            if (r["AttendanceMonth"].ToString() != "")
            {
                _attendancemonth = int.Parse(r["AttendanceMonth"].ToString());
            }
            if (r["LateCount1"].ToString() != "")
            {
                _latecount1 = int.Parse(r["LateCount1"].ToString());
            }
            if (r["LateCount2"].ToString() != "")
            {
                _latecount2 = int.Parse(r["LateCount2"].ToString());
            }
            if (r["OverTimeCount"].ToString() != "")
            {
                _overtimecount = int.Parse(r["OverTimeCount"].ToString());
            }
            if (r["AbsentCount1"].ToString() != "")
            {
                _absentcount1 = int.Parse(r["AbsentCount1"].ToString());
            }
            if (r["AbsentCount2"].ToString() != "")
            {
                _absentcount2 = int.Parse(r["AbsentCount2"].ToString());
            }
            if (r["AbsentCount3"].ToString() != "")
            {
                _absentcount3 = int.Parse(r["AbsentCount3"].ToString());
            }
            if (r["LeaveEarly"].ToString() != "")
            {
                _leaveearly = int.Parse(r["LeaveEarly"].ToString());
            }
            if (r["AttendanceType"].ToString() != "")
            {
                _attendancetype = int.Parse(r["AttendanceType"].ToString());
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
            _username = r["UserName"].ToString();
            _employeename = r["EmployeeName"].ToString();
            if (r["Level1ID"].ToString() != "")
            {
                _level1id = int.Parse(r["Level1ID"].ToString());
            }
            if (r["Level2ID"].ToString() != "")
            {
                _level2id = int.Parse(r["Level2ID"].ToString());
            }
            if (r["Level3ID"].ToString() != "")
            {
                _level3id = int.Parse(r["Level3ID"].ToString());
            }
            _department = r["Department"].ToString();
            _position = r["Position"].ToString();
            if (r["AttendanceYear"].ToString() != "")
            {
                _attendanceyear = int.Parse(r["AttendanceYear"].ToString());
            }
            if (r["AttendanceMonth"].ToString() != "")
            {
                _attendancemonth = int.Parse(r["AttendanceMonth"].ToString());
            }
            if (r["LateCount1"].ToString() != "")
            {
                _latecount1 = int.Parse(r["LateCount1"].ToString());
            }
            if (r["LateCount2"].ToString() != "")
            {
                _latecount2 = int.Parse(r["LateCount2"].ToString());
            }
            if (r["OverTimeCount"].ToString() != "")
            {
                _overtimecount = int.Parse(r["OverTimeCount"].ToString());
            }
            if (r["AbsentCount1"].ToString() != "")
            {
                _absentcount1 = int.Parse(r["AbsentCount1"].ToString());
            }
            if (r["AbsentCount2"].ToString() != "")
            {
                _absentcount2 = int.Parse(r["AbsentCount2"].ToString());
            }
            if (r["AbsentCount3"].ToString() != "")
            {
                _absentcount3 = int.Parse(r["AbsentCount3"].ToString());
            }
            if (r["LeaveEarly"].ToString() != "")
            {
                _leaveearly = int.Parse(r["LeaveEarly"].ToString());
            }
            if (r["AttendanceType"].ToString() != "")
            {
                _attendancetype = int.Parse(r["AttendanceType"].ToString());
            }
        }
    }
}