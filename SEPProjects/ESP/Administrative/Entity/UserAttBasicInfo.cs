using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    /// <summary>
    /// 人员卡号信息表
    /// </summary>
    public class UserAttBasicInfo : BaseEntityInfo
    {
        public UserAttBasicInfo()
        { }
        #region Model
        private int _id;
        private int _userid;
        private string _usercode;
        private string _username;
        private string _employeename;
        private string _cardno;
        private string _goworktime;
        private string _offworktime;
        private int _attendancetype;
        private int _annualLeaveBase;
        private int _cardstate;
        private DateTime? _cardenabletime;
        private DateTime? _cardunenabletime;
        private string _remark;
        private int _areaid;
        private DateTime? _worktimebegindate;

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
        public int Userid
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
        /// 用户登录账号，如：system.admin
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
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
        /// 门卡号
        /// </summary>
        public string CardNo
        {
            set { _cardno = value; }
            get { return _cardno; }
        }
        /// <summary>
        /// 上班时间
        /// </summary>
        public string GoWorkTime
        {
            set { _goworktime = value; }
            get { return _goworktime; }
        }
        /// <summary>
        /// 下班时间
        /// </summary>
        public string OffWorkTime
        {
            set { _offworktime = value; }
            get { return _offworktime; }
        }
        /// <summary>
        /// 考勤类型，1.正常、2.弹性、3.特殊
        /// </summary>
        public int AttendanceType
        {
            set { _attendancetype = value; }
            get { return _attendancetype; }
        }
        /// <summary>
        /// 年假基数，不同的职位基数不同
        /// </summary>
        public int AnnualLeaveBase
        {
            get { return _annualLeaveBase; }
            set { _annualLeaveBase = value; }
        }
        /// <summary>
        /// 门卡状态
        /// </summary>
        public int CardState
        {
            set { _cardstate = value; }
            get { return _cardstate; }
        }
        /// <summary>
        /// 门卡启用时间
        /// </summary>
        public DateTime? CardEnableTime
        {
            set { _cardenabletime = value; }
            get { return _cardenabletime; }
        }
        /// <summary>
        /// 门卡停用时间
        /// </summary>
        public DateTime? CardUnEnableTime
        {
            set { _cardunenabletime = value; }
            get { return _cardunenabletime; }
        }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 地区ID，北京、上海、广州
        /// </summary>
        public int AreaID
        {
            set { _areaid = value; }
            get { return _areaid; }
        }
        /// <summary>
        /// 上下班时间开始启用日期
        /// </summary>
        public DateTime? WorkTimeBeginDate
        {
            set { _worktimebegindate = value; }
            get { return _worktimebegindate; }
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
            if (r["Userid"].ToString() != "")
            {
                _userid = int.Parse(r["Userid"].ToString());
            }
            _usercode = r["UserCode"].ToString();
            _username = r["UserName"].ToString();
            _employeename = r["EmployeeName"].ToString();
            _cardno = r["CardNo"].ToString();
            var objWorkTimeBeginDate = r["WorkTimeBeginDate"];
            if (!(objWorkTimeBeginDate is DBNull))
            {
                _worktimebegindate = (DateTime)objWorkTimeBeginDate;
            }
            _goworktime = r["GoWorkTime"].ToString();
            _offworktime = r["OffWorkTime"].ToString();
            if (r["AttendanceType"].ToString() != "")
            {
                _attendancetype = int.Parse(r["AttendanceType"].ToString());
            }
            if (r["AnnualLeaveBase"].ToString() != "")
            {
                _annualLeaveBase = int.Parse(r["AnnualLeaveBase"].ToString());
            }
            if (r["CardState"].ToString() != "")
            {
                _cardstate = int.Parse(r["CardState"].ToString());
            }
            var objCardEnableTime = r["CardEnableTime"];
            if (!(objCardEnableTime is DBNull))
            {
                _cardenabletime = (DateTime)objCardEnableTime;
            }
            var objCardUnEnableTime = r["CardUnEnableTime"];
            if (!(objCardUnEnableTime is DBNull))
            {
                _cardunenabletime = (DateTime)objCardUnEnableTime;
            }
            _remark = r["Remark"].ToString();
            if (r["AreaID"].ToString() != "")
            {
                _areaid = int.Parse(r["AreaID"].ToString());
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
            if (r["Userid"].ToString() != "")
            {
                _userid = int.Parse(r["Userid"].ToString());
            }
            _usercode = r["UserCode"].ToString();
            _username = r["UserName"].ToString();
            _employeename = r["EmployeeName"].ToString();
            _cardno = r["CardNo"].ToString();
            var objWorkTimeBeginDate = r["WorkTimeBeginDate"];
            if (!(objWorkTimeBeginDate is DBNull))
            {
                _worktimebegindate = (DateTime)objWorkTimeBeginDate;
            }
            _goworktime = r["GoWorkTime"].ToString();
            _offworktime = r["OffWorkTime"].ToString();
            if (r["AttendanceType"].ToString() != "")
            {
                _attendancetype = int.Parse(r["AttendanceType"].ToString());
            }
            if (r["AnnualLeaveBase"].ToString() != "")
            {
                _annualLeaveBase = int.Parse(r["AnnualLeaveBase"].ToString());
            }
            if (r["CardState"].ToString() != "")
            {
                _cardstate = int.Parse(r["CardState"].ToString());
            }
            var objCardEnableTime = r["CardEnableTime"];
            if (!(objCardEnableTime is DBNull))
            {
                _cardenabletime = (DateTime)objCardEnableTime;
            }
            var objCardUnEnableTime = r["CardUnEnableTime"];
            if (!(objCardUnEnableTime is DBNull))
            {
                _cardunenabletime = (DateTime)objCardUnEnableTime;
            }
            _remark = r["Remark"].ToString();
            if (r["AreaID"].ToString() != "")
            {
                _areaid = int.Parse(r["AreaID"].ToString());
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
        }
    }
}