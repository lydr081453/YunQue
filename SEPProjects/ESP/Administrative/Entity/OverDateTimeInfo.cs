using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    public class OverDateTimeInfo : BaseEntityInfo
    {
        public OverDateTimeInfo()
        { }
        #region Model
        private int _id;
        private int _singleovertimeid;
        private DateTime _overtimedatetime;
        private string _begintime;
        private string _endtime;
        private decimal _overtimehours;
        private string _overtimecause;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// OT单ID值
        /// </summary>
        public int SingleOvertimeID
        {
            set { _singleovertimeid = value; }
            get { return _singleovertimeid; }
        }
        /// <summary>
        /// OT日期
        /// </summary>
        public DateTime OverTimeDateTime
        {
            set { _overtimedatetime = value; }
            get { return _overtimedatetime; }
        }
        /// <summary>
        /// OT开始时间：如19:00
        /// </summary>
        public string BeginTime
        {
            set { _begintime = value; }
            get { return _begintime; }
        }
        /// <summary>
        /// OT结束时间， 如：23:00
        /// </summary>
        public string EndTime
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
            if (r["SingleOvertimeID"].ToString() != "")
            {
                _singleovertimeid = int.Parse(r["SingleOvertimeID"].ToString());
            }
            var objOverTimeDateTime = r["OverTimeDateTime"];
            if (!(objOverTimeDateTime is DBNull))
            {
                _overtimedatetime = (DateTime)objOverTimeDateTime;
            }
            _begintime = r["BeginTime"].ToString();
            _endtime = r["EndTime"].ToString();
            if (r["OverTimeHours"].ToString() != "")
            {
                _overtimehours = decimal.Parse(r["OverTimeHours"].ToString());
            }
            _overtimecause = r["OverTimeCause"].ToString();
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
            if (r["OperatorID"].ToString() != "")
            {
                OperateorID = int.Parse(r["OperatorID"].ToString());
            }
            if (r["OperatorDeptID"].ToString() != "")
            {
                OperateorDept = int.Parse(r["OperatorDeptID"].ToString());
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
            if (r["SingleOvertimeID"].ToString() != "")
            {
                _singleovertimeid = int.Parse(r["SingleOvertimeID"].ToString());
            }
            var objOverTimeDateTime = r["OverTimeDateTime"];
            if (!(objOverTimeDateTime is DBNull))
            {
                _overtimedatetime = (DateTime)objOverTimeDateTime;
            }
            _begintime = r["BeginTime"].ToString();
            _endtime = r["EndTime"].ToString();
            if (r["OverTimeHours"].ToString() != "")
            {
                _overtimehours = decimal.Parse(r["OverTimeHours"].ToString());
            }
            _overtimecause = r["OverTimeCause"].ToString();
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
            if (r["OperatorID"].ToString() != "")
            {
                OperateorID = int.Parse(r["OperatorID"].ToString());
            }
            if (r["OperatorDeptID"].ToString() != "")
            {
                OperateorDept = int.Parse(r["OperatorDeptID"].ToString());
            }
        }
    }
}