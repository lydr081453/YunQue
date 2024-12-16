using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    public class ALAndRLInfo
    {
        public ALAndRLInfo()
        { }
        #region Model
        private int _id;
        private int _userid;
        private string _usercode;
        private string _username;
        private string _employeename;
        private int _leaveyear;
        private int _leavemonth;
        private decimal _leavenumber;
        private decimal _remainingnumber;
        private int _leavetype;
        private DateTime _validto;
        private bool _deleted;
        private DateTime _createtime;
        private DateTime _updatetime;
        private int _operatorid;
        private int _operatordept;

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
        /// 用户英文名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 用户中文名
        /// </summary>
        public string EmployeeName
        {
            set { _employeename = value; }
            get { return _employeename; }
        }
        /// <summary>
        /// 假期年份
        /// </summary>
        public int LeaveYear
        {
            set { _leaveyear = value; }
            get { return _leaveyear; }
        }
        /// <summary>
        /// 假期月份
        /// </summary>
        public int LeaveMonth
        {
            set { _leavemonth = value; }
            get { return _leavemonth; }
        }
        /// <summary>
        /// 假期数量
        /// </summary>
        public decimal LeaveNumber
        {
            set { _leavenumber = value; }
            get { return _leavenumber; }
        }
        /// <summary>
        /// 假期剩余数量
        /// </summary>
        public decimal RemainingNumber
        {
            set { _remainingnumber = value; }
            get { return _remainingnumber; }
        }
        /// <summary>
        /// 假期类型,1年假，2奖励假
        /// </summary>
        public int LeaveType
        {
            set { _leavetype = value; }
            get { return _leavetype; }
        }
        /// <summary>
        /// 有效期至
        /// </summary>
        public DateTime ValidTo
        {
            set { _validto = value; }
            get { return _validto; }
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
        /// 操作人
        /// </summary>
        public int OperatorID
        {
            set { _operatorid = value; }
            get { return _operatorid; }
        }
        /// <summary>
        /// 操作部门
        /// </summary>
        public int OperatorDept
        {
            set { _operatordept = value; }
            get { return _operatordept; }
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
            if (r["LeaveYear"].ToString() != "")
            {
                _leaveyear = int.Parse(r["LeaveYear"].ToString());
            }
            if (r["LeaveMonth"].ToString() != "")
            {
                _leavemonth = int.Parse(r["LeaveMonth"].ToString());
            }
            if (r["LeaveNumber"].ToString() != "")
            {
                _leavenumber = decimal.Parse(r["LeaveNumber"].ToString());
            }
            if (r["RemainingNumber"].ToString() != "")
            {
                _remainingnumber = decimal.Parse(r["RemainingNumber"].ToString());
            }
            if (r["LeaveType"].ToString() != "")
            {
                _leavetype = int.Parse(r["LeaveType"].ToString());
            }
            var objValidTo = r["ValidTo"];
            if (!(objValidTo is DBNull))
            {
                _validto = (DateTime)objValidTo;
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
            if (r["LeaveYear"].ToString() != "")
            {
                _leaveyear = int.Parse(r["LeaveYear"].ToString());
            }
            if (r["LeaveMonth"].ToString() != "")
            {
                _leavemonth = int.Parse(r["LeaveMonth"].ToString());
            }
            if (r["LeaveNumber"].ToString() != "")
            {
                _leavenumber = decimal.Parse(r["LeaveNumber"].ToString());
            }
            if (r["RemainingNumber"].ToString() != "")
            {
                _remainingnumber = decimal.Parse(r["RemainingNumber"].ToString());
            }
            if (r["LeaveType"].ToString() != "")
            {
                _leavetype = int.Parse(r["LeaveType"].ToString());
            }
            var objValidTo = r["ValidTo"];
            if (!(objValidTo is DBNull))
            {
                _validto = (DateTime)objValidTo;
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
        }
    }
}