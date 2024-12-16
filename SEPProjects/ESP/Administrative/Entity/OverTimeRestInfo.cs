using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    /// <summary>
    /// OT单和调休单的对应关系类
    /// </summary>
    public class OverTimeRestInfo
    {
        public OverTimeRestInfo()
        { }
        #region Model
        private int _id;
        private int _overtimeid;
        private int _matterid;
        private int _useovertimehours;
        
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// OT单ID
        /// </summary>
        public int OverTimeID
        {
            set { _overtimeid = value; }
            get { return _overtimeid; }
        }
        /// <summary>
        /// 调休单ID（考勤事宜表的ID）
        /// </summary>
        public int MatterID
        {
            set { _matterid = value; }
            get { return _matterid; }
        }
        /// <summary>
        /// 使用OT单的小时数
        /// </summary>
        public int Useovertimehours
        {
            get { return _useovertimehours; }
            set { _useovertimehours = value; }
        }
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
            if (r["OverTimeID"].ToString() != "")
            {
                _overtimeid = int.Parse(r["OverTimeID"].ToString());
            }
            if (r["MatterID"].ToString() != "")
            {
                _matterid = int.Parse(r["MatterID"].ToString());
            }
            if (r["UseOverTimeHours"].ToString() != "")
            {
                _useovertimehours = int.Parse(r["UseOverTimeHours"].ToString());
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
            if (r["OverTimeID"].ToString() != "")
            {
                _overtimeid = int.Parse(r["OverTimeID"].ToString());
            }
            if (r["MatterID"].ToString() != "")
            {
                _matterid = int.Parse(r["MatterID"].ToString());
            }
            if (r["UseOverTimeHours"].ToString() != "")
            {
                _useovertimehours = int.Parse(r["UseOverTimeHours"].ToString());
            }
        }
        #endregion Model
    }
}