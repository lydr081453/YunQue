using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AdministrativeService
{
    public class Status
    {
        public Status()
        { }

        /// <summary>
        /// 备份时间
        /// </summary>
        //public static string[] BackUpTime = { "18:35:00", "18:40:00", "18:45:00", "18:50:00", "18:55:00" };
        private string[] _backuptime;

        public string[] Backuptime
        {
            get
            {
                string backuptimestr = ConfigurationManager.AppSettings["BackUpTime"];
                if (!string.IsNullOrEmpty(backuptimestr))
                {
                    _backuptime = backuptimestr.Split(new char[] { ',' });
                }
                return _backuptime;
            }
        }

        /// <summary>
        /// 是否执行弹性工作日 1表示是，0表示否
        /// </summary>
        public static string FlexibleWorking = ConfigurationManager.AppSettings["FlexibleWorking"];

        // 迟到时间点
        private static TimeSpan _lateTime;
        /// <summary>
        /// 迟到时间点
        /// </summary>
        public static TimeSpan LateTime
        {
            get
            {
                string[] lateTimeArr =
                    ConfigurationManager.AppSettings["LateTime"].Split(new char[] { ':' });
                _lateTime = new TimeSpan(Convert.ToInt32(lateTimeArr[0]), Convert.ToInt32(lateTimeArr[1]), 0);
                return _lateTime;
            }
        }

        //上午旷工时间点
        private static TimeSpan _amAbsentTime;
        /// <summary>
        /// 上午旷工时间点
        /// </summary>
        public static TimeSpan AMAbsentTime
        {
            get
            {
                string[] amAbsentTimeArr =
                    ConfigurationManager.AppSettings["AMAbsentTime"].Split(new char[] { ':' });
                _amAbsentTime = new TimeSpan(Convert.ToInt32(amAbsentTimeArr[0]), Convert.ToInt32(amAbsentTimeArr[1]), 0);
                return _amAbsentTime;
            }

        }

        // 下午旷工时间点
        private static TimeSpan _pmAbsentTime;
        /// <summary>
        /// 下午旷工时间点
        /// </summary>
        public static TimeSpan PMAbsentTime
        {
            get
            {
                string[] pmAbsentTime =
                    ConfigurationManager.AppSettings["PMAbsentTime"].Split(new char[] { ':' });
                _pmAbsentTime = new TimeSpan(Convert.ToInt32(pmAbsentTime[0]), Convert.ToInt32(pmAbsentTime[1]), 0);
                return _pmAbsentTime;
            }
        }

        /// <summary>
        /// 每日工作小时数
        /// </summary>
        public static TimeSpan HoursWorked =
            new TimeSpan(Convert.ToInt32(ConfigurationManager.AppSettings["HoursWorked"]), 0, 0);

        // 下班时间点
        private static TimeSpan _offWorkTime;
        /// <summary>
        /// 下班时间点
        /// </summary>
        public static TimeSpan OffWorkTime
        {
            get
            {
                string[] offWorkTimeArr =
                    ConfigurationManager.AppSettings["OffWorkTime"].Split(new char[] { ':' });
                _offWorkTime = new TimeSpan(Convert.ToInt32(offWorkTimeArr[0]), Convert.ToInt32(offWorkTimeArr[1]), 0);
                return _offWorkTime;
            }
        }

        // 上班时间点
        private static TimeSpan _goWorkTime;
        /// <summary>
        /// 上班时间点
        /// </summary>
        public static TimeSpan GoWorkTime
        {
            get
            {
                string[] goWorkTimeArr =
                    ConfigurationManager.AppSettings["GoWorkTime"].Split(new char[] { ':' });
                _goWorkTime = new TimeSpan(Convert.ToInt32(goWorkTimeArr[0]), Convert.ToInt32(goWorkTimeArr[1]), 0);
                return _goWorkTime;
            }
        }

        // OT时间点
        private static TimeSpan _overTime;
        /// <summary>
        /// OT时间点
        /// </summary>
        public static TimeSpan OverTime
        {
            get
            {
                string[] overTimeArr =
                    ConfigurationManager.AppSettings["OverTime"].Split(new char[] { ':' });
                _overTime = new TimeSpan(Convert.ToInt32(overTimeArr[0]), Convert.ToInt32(overTimeArr[1]), 0);
                return _overTime;
            }
        }
    }
}
