using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Utilities
{
    public static class AddWorkDays
    {
        public static DateTime addWorkDays(DateTime dt, int addDays)
        {
            DateTime dtStart = dt;
            DateTime dtEnd = dtStart.AddDays(addDays);
            int days = CalculateWeekendDays(dtStart, dtEnd);
            while (days > 0)
            {
                dtStart = dtEnd;
                if(addDays>=0)
                dtEnd = dtEnd.AddDays(days);
                else
                    dtEnd = dtEnd.AddDays(-days);
                days = CalculateWeekendDays(dtStart, dtEnd);
            }
            return dtEnd;
        }

        public static int CalculateWeekendDays(DateTime dtStart, DateTime dtEnd)
        {
            int count = 0;
            if (dtStart <= dtEnd)
            {
                for (DateTime dtTemp = dtStart.AddDays(1); dtTemp <= dtEnd; dtTemp = dtTemp.AddDays(1))
                {
                    if (dtTemp.DayOfWeek == DayOfWeek.Saturday || dtTemp.DayOfWeek == DayOfWeek.Sunday)
                    {
                        count++;
                    }
                }
            }
            else
            {
                for (DateTime dtTemp = dtEnd; dtTemp < dtStart; dtTemp = dtTemp.AddDays(1))
                {
                    if (dtTemp.DayOfWeek == DayOfWeek.Saturday || dtTemp.DayOfWeek == DayOfWeek.Sunday)
                    {
                        count++;
                    }
                }
            }
            return count;
        } 
    }
}
