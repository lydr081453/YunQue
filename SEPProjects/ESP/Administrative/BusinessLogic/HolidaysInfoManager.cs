using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.Entity;
using ESP.Administrative.DataAccess;
using System.Data;

namespace ESP.Administrative.BusinessLogic
{
    public class HolidaysInfoManager
    {
        private readonly HolidaysInfoDataProvider dal = new HolidaysInfoDataProvider();
        public HolidaysInfoManager()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(HolidaysInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(HolidaysInfo model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            dal.Delete(ID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HolidaysInfo GetModel(int ID)
        {
            return dal.GetModel(ID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return dal.GetList("");
        }

        /// <summary>
        /// 添加保存一年的假日信息
        /// </summary>
        /// <param name="yearvalue">年份</param>
        /// <param name="list">假日信息集合</param>
        /// <param name="UserID">操作用户ID</param>
        /// <returns>如果保存成功返回true，否则返回false</returns>
        public bool AddOneYearHolidays(int yearvalue, List<DateTime> list, int UserID)
        {
            bool b = false;
            if (list != null && list.Count > 0)
            {
                DataSet ds = this.GetList(" deleted = 0 and HoliYear=" + yearvalue);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows != null)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        this.Delete(int.Parse(dr["ID"].ToString()));
                    }
                }
                foreach (DateTime dt in list)
                {
                    HolidaysInfo holiday = new HolidaysInfo();
                    holiday.CreateTime = DateTime.Now;
                    holiday.Deleted = false;
                    holiday.HoliDate = dt;
                    holiday.OperateorDept = 0;
                    holiday.OperateorID = UserID;
                    holiday.Sort = 0;
                    holiday.Type = 1;
                    holiday.UpdateTime = DateTime.Now;
                    holiday.Holiyear = yearvalue;
                    this.Add(holiday);
                }
                b = true;
            }
            return b;
        }

        /// <summary>
        /// 获得一个年份的假日信息
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns>返回该年份的假日信息集合</returns>
        public List<DateTime> GetOneYearHolidays(int year)
        {
            List<DateTime> list = new List<DateTime>();
            DataSet ds = this.GetList(" deleted = 0 and HoliYear=" + year);
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    list.Add(DateTime.Parse(dr["HoliDate"].ToString()));
                }
                return list;
            }
            return null;
        }

        /// <summary>
        /// 通过时间获得一个假日信息对象
        /// </summary>
        /// <param name="datetime">要判断的时间</param>
        /// <returns>如果查询到就返回一个假日信息对象，否则返回null</returns>
        public HolidaysInfo GetHolideysInfoByDatetime(DateTime datetime)
        {
            if (datetime != null)
            {
                HolidaysInfo holidaysinfo = new HolidaysInfo();
                string holidate = datetime.ToString("yyyy.MM.dd");
                DataSet ds = this.GetList(" deleted=0 and CONVERT(varchar(30),HoliDate,102) ='" + holidate + "'");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    holidaysinfo.PopupData(ds.Tables[0].Rows[0]);
                    return holidaysinfo;
                }
            }
            return null;
        }

        /// <summary>
        /// 获得某个月的节假日信息
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>返回节假日信息集合</returns>
        public HashSet<int> GetHolidayListByMonth(int year, int month)
        {
            return dal.GetHolidayListByMonth(year, month);
        }

        /// <summary>
        /// 获得某年的节假日信息
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns>返回节假日信息集合</returns>
        public HashSet<int> GetHolidayListByMonth(int year)
        {
            return dal.GetHolidayListByMonth(year);
        }

        /// <summary>
        /// 获得某时间段的节假日信息
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>返回节假日信息集合</returns>
        public HashSet<int> GetHolidayListByMonth(DateTime beginTime, DateTime endTime)
        {
            return dal.GetHolidayListByMonth(beginTime, endTime);
        }

        #endregion  成员方法
    }
}

