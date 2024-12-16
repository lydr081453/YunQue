using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.DataAccess;
using ESP.Administrative.Entity;
using System.Data;

namespace ESP.Administrative.Common
{
    public class Utility
    {
        private ApproveLogDataProvider dal = new ApproveLogDataProvider();
        private AttendanceManager attendanceManager = new AttendanceManager();
        private SingleOvertimeManager singleManager = new SingleOvertimeManager();
        private LeaveManager leaveManager = new LeaveManager();
        public Utility()
        {
        }

        /// <summary>
        /// 判断一个日期是否是周末
        /// </summary>
        /// <param name="date">被判断的日期</param>
        /// <returns>如果是周末返回true, 否则返回false</returns>
        public static bool CheckDateIsWeekend(DateTime date)
        {
            bool b = false;
            if (date != null)
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    b = true;
                }
            }
            return b;
        }

        /// <summary>
        /// 计算服务年限
        /// </summary>
        /// <param name="CutOffDate">截止日期</param>
        /// <param name="EntryTime">入职日期</param>
        /// <returns>返回用户的服务年限</returns>
        public static int GetServiceAge(DateTime CutOffDate, DateTime EntryTime)
        {
            // 服务年限
            int serviceAge = (int)((CutOffDate.Year - EntryTime.Year)
                + (CutOffDate.Month - EntryTime.Month) * 0.01
                + (CutOffDate.Day - EntryTime.Day) * 0.01 * 0.01);
            return serviceAge;
        }
    }
}
