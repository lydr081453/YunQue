using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Entity;

namespace AdministrativeService
{
    public class Check
    {
        /// <summary>
        /// 判断是否已经迟到(迟到时间点之后，上午旷工时间点之前)
        /// </summary>
        /// <param name="span">被检测的时间点</param>
        /// <returns>如果迟到返回true，否则返回false</returns>
        public bool CheckIsLate(TimeSpan span)
        {
            bool b = false;
            if (span != null && span > Status.LateTime
                && span <= Status.AMAbsentTime)
            {
                b = true;
            }
            return b;
        }

        /// <summary>
        /// 判断是否上午旷工（上午旷工时间点之后，下午旷工时间时间点之前）
        /// </summary>
        /// <param name="span">被检测的时间点</param>
        /// <returns>如果旷工返回true，否则返回false</returns>
        public bool CheckIsAMAbsent(TimeSpan span)
        {
            bool b = false;
            if (span != null && span > Status.AMAbsentTime
                && span <= Status.PMAbsentTime)
            {
                b = true;
            }
            return b;
        }

        /// <summary>
        /// 判断是否下午旷工（下午旷工时间点之后）
        /// </summary>
        /// <param name="span">被检测的时间</param>
        /// <returns></returns>
        public bool CheckIsPMAbsent(TimeSpan span)
        {
            bool b = false;
            if (span != null && span > Status.PMAbsentTime
                && span <= Status.OffWorkTime)
            {
                b = true;
            }
            return b;
        }

        /// <summary>
        /// 判断是否是早退
        /// </summary>
        /// <param name="span">被检测的时间</param>
        /// <returns>如果是早退返回true，否则返回false</returns>
        public bool CheckIsLeaveEarly(TimeSpan span)
        {
            bool b = false;
            if (span != null && span < Status.OffWorkTime)
            {
                b = true;
            }
            return b;
        }

        /// <summary>
        /// 判断是否是OT
        /// </summary>
        /// <param name="span">被检测时间</param>
        /// <returns>如果是OT返回true，否则返回false</returns>
        public bool CheckIsOverTime(TimeSpan span)
        {
            bool b = false;
            if (span != null && span > Status.OverTime)
            {
                b = true;
            }
            return b;
        }

        /// <summary>
        /// 判断是否假期
        /// </summary>
        /// <param name="date">被检测时间</param>
        /// <returns>如果是则返回true, 否则返回false</returns>
        public bool CheckIsHoliday(DateTime date)
        {
            bool b = false;
            if (date != null)
            {
                HolidaysInfoManager holiManager = new HolidaysInfoManager();
                HolidaysInfo holidaysinfo = holiManager.GetHolideysInfoByDatetime(date);
                if (holidaysinfo != null && holidaysinfo.ID > 0)
                {
                    b = true;
                }
            }
            return b;
        }
    }
}
