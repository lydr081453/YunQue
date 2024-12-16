using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    /// <summary>
    /// 系统日志信息表
    /// </summary>
    public class LogInfo
    {
        public LogInfo()
        { }
        #region Model
        private int _id;
        private string _content;
        private DateTime _time;
        private bool _issuccess;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 日志内容
        /// </summary>
        public string Content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time
        {
            set { _time = value; }
            get { return _time; }
        }
        /// <summary>
        /// 是否成功  0,表示不成功，1.表示成功
        /// </summary>
        public bool IsSuccess
        {
            set { _issuccess = value; }
            get { return _issuccess; }
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
            _content = r["Content"].ToString();
            var objTime = r["Time"];
            if (!(objTime is DBNull))
            {
                _time = (DateTime)objTime;
            }
            if (r["IsSuccess"].ToString() != "")
            {
                if ((r["IsSuccess"].ToString() == "1") || (r["IsSuccess"].ToString().ToLower() == "true"))
                {
                    _issuccess = true;
                }
                else
                {
                    _issuccess = false;
                }
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
            _content = r["Content"].ToString();
            var objTime = r["Time"];
            if (!(objTime is DBNull))
            {
                _time = (DateTime)objTime;
            }
            if (r["IsSuccess"].ToString() != "")
            {
                if ((r["IsSuccess"].ToString() == "1") || (r["IsSuccess"].ToString().ToLower() == "true"))
                {
                    _issuccess = true;
                }
                else
                {
                    _issuccess = false;
                }
            }
        }
    }
}