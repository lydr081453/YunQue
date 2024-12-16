using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    /// <summary>
    /// 上下班时间数据类
    /// </summary>
    public class CommuterTimeInfo
    {
        public CommuterTimeInfo()
        { }
        #region Model
        private int _id;
        private int _userid;
        private string _usercode;
        private string _username;
        private DateTime? _begintime;
        private DateTime? _endtime;
        private DateTime _goworktime;
        private DateTime _offworktime;
        private int _attendancetype;
        private bool _deleted;
        private DateTime _createtime;
        private DateTime _updatetime;
        private int _operatorid;
        private int _operatordept;
        private int _sort;
        private float _workingDays_OverTime1;
        private float _workingDays_OverTime2;
        private float _lateGoWorkTime_OverTime1;

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
        /// 用户姓名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 开始时间，考勤时间启用的时间
        /// </summary>
        public DateTime? BeginTime
        {
            set { _begintime = value; }
            get { return _begintime; }
        }
        /// <summary>
        /// 结束时间，考勤时间结束的时间，这个时间是允许为空的，如果为空默认就为当前时间。
        /// </summary>
        public DateTime? EndTime
        {
            set { _endtime = value; }
            get { return _endtime; }
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
        /// 考勤类型，1正常，2弹性工作制，3特殊，4不记录考勤
        /// </summary>
        public int AttendanceType
        {
            set { _attendancetype = value; }
            get { return _attendancetype; }
        }
        /// <summary>
        /// 有效性标识
        /// </summary>
        public bool Deleted
        {
            set { _deleted = value; }
            get { return _deleted; }
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
        /// 最后修改时间
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public int OperatorID
        {
            set { _operatorid = value; }
            get { return _operatorid; }
        }
        /// <summary>
        /// 操作人部门
        /// </summary>
        public int OperatorDept
        {
            set { _operatordept = value; }
            get { return _operatordept; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        /// <summary>
        /// 工作日OT截止时间点1。
        /// </summary>
        public float WorkingDays_OverTime1
        {
            set { _workingDays_OverTime1 = value; }
            get { return _workingDays_OverTime1; }
        }
        /// <summary>
        /// 工作日OT截止时间点2。
        /// </summary>
        public float WorkingDays_OverTime2
        {
            set { _workingDays_OverTime2 = value; }
            get { return _workingDays_OverTime2; }
        }
        /// <summary>
        /// 工作日OT第二天上午享受调休的时长。
        /// </summary>
        public float LateGoWorkTime_OverTime1
        {
            set { _lateGoWorkTime_OverTime1 = value; }
            get { return _lateGoWorkTime_OverTime1; }
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
            var objGoWorkTime = r["GoWorkTime"];
            if (!(objGoWorkTime is DBNull))
            {
                _goworktime = (DateTime)objGoWorkTime;
            }
            var objOffWorkTime = r["OffWorkTime"];
            if (!(objOffWorkTime is DBNull))
            {
                _offworktime = (DateTime)objOffWorkTime;
            }
            if (r["AttendanceType"].ToString() != "")
            {
                _attendancetype = int.Parse(r["AttendanceType"].ToString());
            }
            if (r["Deleted"].ToString() != "")
            {
                if ((r["Deleted"].ToString() == "1") || (r["Deleted"].ToString().ToLower() == "true"))
                {
                    _deleted = true;
                }
                else
                {
                    _deleted = false;
                }
            }
            var objCreateTime = r["CreateTime"];
            if (!(objCreateTime is DBNull))
            {
                _createtime = (DateTime)objCreateTime;
            }
            var objUpdateTime = r["UpdateTime"];
            if (!(objUpdateTime is DBNull))
            {
                _updatetime = (DateTime)objUpdateTime;
            }
            if (r["OperatorID"].ToString() != "")
            {
                _operatorid = int.Parse(r["OperatorID"].ToString());
            }
            if (r["OperatorDept"].ToString() != "")
            {
                _operatordept = int.Parse(r["OperatorDept"].ToString());
            }
            if (r["Sort"].ToString() != "")
            {
                _sort = int.Parse(r["Sort"].ToString());
            }
            if (r["WorkingDays_OverTime1"].ToString() != "")
            {
                _workingDays_OverTime1 = float.Parse(r["WorkingDays_OverTime1"].ToString());
            }
            if (r["WorkingDays_OverTime2"].ToString() != "")
            {
                _workingDays_OverTime2 = float.Parse(r["WorkingDays_OverTime2"].ToString());
            }
            if (r["LateGoWorkTime_OverTime1"].ToString() != "")
            {
                _lateGoWorkTime_OverTime1 = float.Parse(r["LateGoWorkTime_OverTime1"].ToString());
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
            var objGoWorkTime = r["GoWorkTime"];
            if (!(objGoWorkTime is DBNull))
            {
                _goworktime = (DateTime)objGoWorkTime;
            }
            var objOffWorkTime = r["OffWorkTime"];
            if (!(objOffWorkTime is DBNull))
            {
                _offworktime = (DateTime)objOffWorkTime;
            }
            if (r["AttendanceType"].ToString() != "")
            {
                _attendancetype = int.Parse(r["AttendanceType"].ToString());
            }
            if (r["Deleted"].ToString() != "")
            {
                if ((r["Deleted"].ToString() == "1") || (r["Deleted"].ToString().ToLower() == "true"))
                {
                    _deleted = true;
                }
                else
                {
                    _deleted = false;
                }
            }
            var objCreateTime = r["CreateTime"];
            if (!(objCreateTime is DBNull))
            {
                _createtime = (DateTime)objCreateTime;
            }
            var objUpdateTime = r["UpdateTime"];
            if (!(objUpdateTime is DBNull))
            {
                _updatetime = (DateTime)objUpdateTime;
            }
            if (r["OperatorID"].ToString() != "")
            {
                _operatorid = int.Parse(r["OperatorID"].ToString());
            }
            if (r["OperatorDept"].ToString() != "")
            {
                _operatordept = int.Parse(r["OperatorDept"].ToString());
            }
            if (r["Sort"].ToString() != "")
            {
                _sort = int.Parse(r["Sort"].ToString());
            }
            if (r["WorkingDays_OverTime1"].ToString() != "")
            {
                _workingDays_OverTime1 = float.Parse(r["WorkingDays_OverTime1"].ToString());
            }
            if (r["WorkingDays_OverTime2"].ToString() != "")
            {
                _workingDays_OverTime2 = float.Parse(r["WorkingDays_OverTime2"].ToString());
            }
            if (r["LateGoWorkTime_OverTime1"].ToString() != "")
            {
                _lateGoWorkTime_OverTime1 = float.Parse(r["LateGoWorkTime_OverTime1"].ToString());
            }
        }
    }
}