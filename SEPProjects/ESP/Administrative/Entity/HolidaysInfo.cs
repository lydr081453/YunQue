using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    public class HolidaysInfo : BaseEntityInfo
    {
        public HolidaysInfo()
        { }
        #region Model
        private int _id;
        private int _holiyear;
        private DateTime _holidate;
        private int _type;
        private DateTime _endDate;

        public DateTime EndDate
        {
            set { _endDate = value; }
            get { return _endDate; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime HoliDate
        {
            set { _holidate = value; }
            get { return _holidate; }
        }
        /// <summary>
        /// 1,节假，2.工作日，3.周末
        /// </summary>
        public int Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 年份
        /// </summary>
        public int Holiyear
        {
            get { return _holiyear; }
            set { _holiyear = value; }
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
            var objHoliDate = r["HoliDate"];
            if (!(objHoliDate is DBNull))
            {
                _holidate = (DateTime)objHoliDate;
            }
            if (r["Type"].ToString() != "")
            {
                _type = int.Parse(r["Type"].ToString());
            }
            if (r["HoliYear"].ToString() != "")
            {
                _holiyear = int.Parse(r["HoliYear"].ToString());
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
            var objEndTime = r["EndDate"];
            if (!(objEndTime is DBNull))
            {
                EndDate = (DateTime)objEndTime;
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
            var objHoliDate = r["HoliDate"];
            if (!(objHoliDate is DBNull))
            {
                _holidate = (DateTime)objHoliDate;
            }
            if (r["Type"].ToString() != "")
            {
                _type = int.Parse(r["Type"].ToString());
            }
            if (r["HoliYear"].ToString() != "")
            {
                _holiyear = int.Parse(r["HoliYear"].ToString());
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
            var objEndTime = r["EndDate"];
            if (!(objEndTime is DBNull))
            {
                EndDate = (DateTime)objEndTime;
            }
        }
    }
}