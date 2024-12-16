using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    public class DisciplineInfo
    {
        public DisciplineInfo()
        { }
        #region Model
        private int _id;
        private int _userid;
        private string _username;
        private DateTime _begintime;
        private DateTime _endtime;
        private decimal _disciplinelength;
        private DateTime _disciplinetime;
        private int _type;
        private bool _isnoneed;
        private int _operatorid;
        private DateTime _operatertime;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime BeginTime
        {
            set { _begintime = value; }
            get { return _begintime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime EndTime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }
        /// <summary>
        /// 时长
        /// </summary>
        public decimal DisciplineLength
        {
            set { _disciplinelength = value; }
            get { return _disciplinelength; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime DisciplineTime
        {
            set { _disciplinetime = value; }
            get { return _disciplinetime; }
        }
        /// <summary>
        /// 类型：1、迟到；2、早退；3、旷工
        /// </summary>
        public int Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 不计算扣款
        /// </summary>
        public bool IsNoNeed
        {
            set { _isnoneed = value; }
            get { return _isnoneed; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int OperatorID
        {
            set { _operatorid = value; }
            get { return _operatorid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime OperaterTime
        {
            set { _operatertime = value; }
            get { return _operatertime; }
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
            if (r["DisciplineLength"].ToString() != "")
            {
                _disciplinelength = decimal.Parse(r["DisciplineLength"].ToString());
            }
            var objDisciplineTime = r["DisciplineTime"];
            if (!(objDisciplineTime is DBNull))
            {
                _disciplinetime = (DateTime)objDisciplineTime;
            }
            if (r["Type"].ToString() != "")
            {
                _type = int.Parse(r["Type"].ToString());
            }
            if (r["IsNoNeed"].ToString() != "")
            {
                if ((r["IsNoNeed"].ToString() == "1") || (r["IsNoNeed"].ToString().ToLower() == "true"))
                {
                    _isnoneed = IsNoNeed = true;
                }
                else
                {
                    _isnoneed = false;
                }
            }

            if (r["OperatorID"].ToString() != "")
            {
                _operatorid = int.Parse(r["OperatorID"].ToString());
            }
            var objOperaterTime = r["OperaterTime"];
            if (!(objOperaterTime is DBNull))
            {
                _operatertime = (DateTime)objOperaterTime;
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
            if (r["DisciplineLength"].ToString() != "")
            {
                _disciplinelength = decimal.Parse(r["DisciplineLength"].ToString());
            }
            var objDisciplineTime = r["DisciplineTime"];
            if (!(objDisciplineTime is DBNull))
            {
                _disciplinetime = (DateTime)objDisciplineTime;
            }
            if (r["Type"].ToString() != "")
            {
                _type = int.Parse(r["Type"].ToString());
            }
            if (r["IsNoNeed"].ToString() != "")
            {
                if ((r["IsNoNeed"].ToString() == "1") || (r["IsNoNeed"].ToString().ToLower() == "true"))
                {
                    _isnoneed = IsNoNeed = true;
                }
                else
                {
                    _isnoneed = false;
                }
            }

            if (r["OperatorID"].ToString() != "")
            {
                _operatorid = int.Parse(r["OperatorID"].ToString());
            }
            var objOperaterTime = r["OperaterTime"];
            if (!(objOperaterTime is DBNull))
            {
                _operatertime = (DateTime)objOperaterTime;
            }
        }
    }
}