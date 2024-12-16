using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.DataAccess;
using ESP.Administrative.Entity;

namespace ESP.Administrative.BusinessLogic
{
    public class WeekSettingManager
    {
        public readonly static WeekSettingDataProvider Provider = new WeekSettingDataProvider();

        public static int Add(WeekSettingInfo model)
        {
            return Provider.Add(model);
        }

        public static bool Update(WeekSettingInfo model)
        {
            return Provider.Update(model);
        }

        public static bool Delete(int Id)
        {
            return Provider.Delete(Id);
        }

        public static bool DeleteList(string Idlist)
        {
            return Provider.DeleteList(Idlist);
        }

        public static WeekSettingInfo GetModel(int Id)
        {
            return Provider.GetModel(Id);
        }

        public static List<WeekSettingInfo> GetList(string strWhere)
        {
            return Provider.GetList(strWhere);
        }

        public static List<WeekSettingInfo> GetList(int Top, string strWhere, string filedOrder)
        {
            return Provider.GetList(Top, strWhere, filedOrder);
        }

        public static int GetRecordCount(string strWhere)
        {
            return Provider.GetRecordCount(strWhere);
        }

        public static List<WeekSettingInfo> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return Provider.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        /// <summary>
        /// 根据日期获得日期所在的周
        /// </summary>
        public static WeekSettingInfo GetWeekByDate(DateTime date)
        {
            WeekSettingInfo week = Provider.GetWeekByDate(date);
            if(week == null)
            {
                int year = date.Year;
                int month = date.Month;
                DateTime monday = GetMondayByNow(date);
                DateTime sunday = monday.AddDays(6);
                week = new WeekSettingInfo();
                week.Year = year;
                week.Month = month;
                week.WeekBegin = new DateTime(monday.Year, monday.Month, monday.Day, 0, 0, 0);
                week.WeekEnd = new DateTime(sunday.Year, sunday.Month, sunday.Day, 23, 59, 59);
                week.Id = Provider.Add(week);

            }
            return week;
        }

        /// <summary>
        /// 获得当前周的星期一的日期
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        public static DateTime GetMondayByNow(DateTime now)
        {
            DateTime monday = DateTime.Now;
            int dayOfWeek = (int)now.DayOfWeek;
            switch (dayOfWeek)
            {
                case 1:
                    monday = now;
                    break;
                case 2:
                    monday = now.AddDays(-1);
                    break;
                case 3:
                    monday = now.AddDays(-2);
                    break;
                case 4:
                    monday = now.AddDays(-3);
                    break;
                case 5:
                    monday = now.AddDays(-4);
                    break;
                case 6:
                    monday = now.AddDays(-5);
                    break;
                case 0:
                    monday = now.AddDays(-6);
                    break;
            }
            return monday;
        }

        /// <summary>
        /// 获得中文的星期
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        public static string GetWeekCn(int dayOfWeek)
        {
            string weekcn = "";
            switch (dayOfWeek)
            {
                case 1:
                    weekcn = "星期一";
                    break;
                case 2:
                    weekcn = "星期二";
                    break;
                case 3:
                    weekcn = "星期三";
                    break;
                case 4:
                    weekcn = "星期四";
                    break;
                case 5:
                    weekcn = "星期五";
                    break;
                case 6:
                    weekcn = "星期六";
                    break;
                case 7:
                    weekcn = "星期日";
                    break;
                case 0:
                    weekcn = "星期日";
                    break;
            }
            return weekcn;
        }
    }
}
