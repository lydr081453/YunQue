using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    public class SingleOvertimeInfo : BaseEntityInfo
    {
        public SingleOvertimeInfo()
        { }
        #region Model
        private int _id;
        private int _userid;
        private string _usercode;
        private string _employeename;
        private DateTime _apptime;
        private DateTime _begintime;
        private DateTime _endtime;
        private decimal _overtimehours;
        private string _overtimecause;
        private int _overtimetype;
        private int _projectid;
        private string _projectno;
        private int _approveid;
        private string _approvename;
        private string _approveremark;
        private bool _isused;
        private int _state;
        private int _approvestate;
        private int _remaininghours;
        private bool _deleted;
        private DateTime _createtime;
        private DateTime _updatetime;
        private int _operateorid;
        private int _operateordept;
        private int _sort;
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
        /// 申请时间
        /// </summary>
        public DateTime AppTime
        {
            set { _apptime = value; }
            get { return _apptime; }
        }
        /// <summary>
        /// OT开始时间
        /// </summary>
        public DateTime BeginTime
        {
            set { _begintime = value; }
            get { return _begintime; }
        }
        /// <summary>
        /// OT结束时间
        /// </summary>
        public DateTime EndTime
        {
            set { _endtime = value; }
            get { return _endtime; }
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
        /// OT事由
        /// </summary>
        public string OverTimeCause
        {
            set { _overtimecause = value; }
            get { return _overtimecause; }
        }
        /// <summary>
        /// OT类型，1工作日,2节假日
        /// </summary>
        public int OverTimeType
        {
            set { _overtimetype = value; }
            get { return _overtimetype; }
        }
        /// <summary>
        /// 项目号ID
        /// </summary>
        public int ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
        /// <summary>
        /// 项目号
        /// </summary>
        public string ProjectNo
        {
            set { _projectno = value; }
            get { return _projectno; }
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
        /// 审批人姓名
        /// </summary>
        public string ApproveName
        {
            set { _approvename = value; }
            get { return _approvename; }
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
        /// 
        /// </summary>
        public bool IsUsed
        {
            set { _isused = value; }
            get { return _isused; }
        }
        /// <summary>
        /// OT单状态
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 审批状态
        /// </summary>
        public int Approvestate
        {
            get { return _approvestate; }
            set { _approvestate = value; }
        }
        /// <summary>
        /// OT单剩余小时数
        /// </summary>
        public int Remaininghours
        {
            get { return _remaininghours; }
            set { _remaininghours = value; }
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
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public int OperateorID
        {
            set { _operateorid = value; }
            get { return _operateorid; }
        }
        /// <summary>
        /// 操作人部门
        /// </summary>
        public int OperateorDept
        {
            set { _operateordept = value; }
            get { return _operateordept; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort
        {
            set { _sort = value; }
            get { return _sort; }
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
            var objAppTime = r["AppTime"];
            if (!(objAppTime is DBNull))
            {
                _apptime = (DateTime)objAppTime;
            }
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
            if (r["OverTimeHours"].ToString() != "")
            {
                _overtimehours = decimal.Parse(r["OverTimeHours"].ToString());
            }
            _overtimecause = r["OverTimeCause"].ToString();
            if (r["OverTimeType"].ToString() != "")
            {
                _overtimetype = int.Parse(r["OverTimeType"].ToString());
            }
            if (r["ProjectID"].ToString() != "")
            {
                _projectid = int.Parse(r["ProjectID"].ToString());
            }
            _projectno = r["ProjectNo"].ToString();
            if (r["ApproveID"].ToString() != "")
            {
                _approveid = int.Parse(r["ApproveID"].ToString());
            }
            _approvename = r["ApproveName"].ToString();
            _approveremark = r["ApproveRemark"].ToString();
            if (r["State"].ToString() != "")
            {
                _state = int.Parse(r["State"].ToString());
            }
            if (r["ApproveState"].ToString() != "")
            {
                _approvestate = int.Parse(r["ApproveState"].ToString());
            }
            if (r["RemainingHours"].ToString() != "")
            {
                _remaininghours = int.Parse(r["RemainingHours"].ToString());
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
            if (r["UserID"].ToString() != "")
            {
                _userid = int.Parse(r["UserID"].ToString());
            }
            _usercode = r["UserCode"].ToString();
            _employeename = r["EmployeeName"].ToString();
            var objAppTime = r["AppTime"];
            if (!(objAppTime is DBNull))
            {
                _apptime = (DateTime)objAppTime;
            }
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
            if (r["OverTimeHours"].ToString() != "")
            {
                _overtimehours = decimal.Parse(r["OverTimeHours"].ToString());
            }
            _overtimecause = r["OverTimeCause"].ToString();
            if (r["OverTimeType"].ToString() != "")
            {
                _overtimetype = int.Parse(r["OverTimeType"].ToString());
            }
            if (r["ProjectID"].ToString() != "")
            {
                _projectid = int.Parse(r["ProjectID"].ToString());
            }
            _projectno = r["ProjectNo"].ToString();
            if (r["ApproveID"].ToString() != "")
            {
                _approveid = int.Parse(r["ApproveID"].ToString());
            }
            _approvename = r["ApproveName"].ToString();
            _approveremark = r["ApproveRemark"].ToString();
            if (r["State"].ToString() != "")
            {
                _state = int.Parse(r["State"].ToString());
            }
            if (r["ApproveState"].ToString() != "")
            {
                _approvestate = int.Parse(r["ApproveState"].ToString());
            }
            if (r["RemainingHours"].ToString() != "")
            {
                _remaininghours = int.Parse(r["RemainingHours"].ToString());
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