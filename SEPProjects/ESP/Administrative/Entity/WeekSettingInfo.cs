using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    public partial class WeekSettingInfo
    {
        public WeekSettingInfo()
        { }
        #region Model
        private int _id;
        private int _year;
        private int _month;
        private DateTime _weekbegin;
        private DateTime _weekend;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Year
        {
            set { _year = value; }
            get { return _year; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Month
        {
            set { _month = value; }
            get { return _month; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime WeekBegin
        {
            set { _weekbegin = value; }
            get { return _weekbegin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime WeekEnd
        {
            set { _weekend = value; }
            get { return _weekend; }
        }
        #endregion Model

    }

    public class DayInfo
    {
        public DayInfo()
        {
            TimeSheets = new List<TimeSheetInfo>();
        }

        public string WeekCn { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public int Week { get; set; }

        public int Day { get; set; }

        public decimal TotalHours { get; set; }

        public TimeSheetCommitInfo TimeSheetCommitInfo { get; set; }

        public List<TimeSheetInfo> TimeSheets { get; set; }

        public string WorkHours { get; set; }

        public List<TimeSheetLogInfo> Logs { get; set; }

        public bool IsDimission { get; set; }
    }

    public class WeekInfoList
    {
        public WeekInfoList()
        {
            DayInfos = new List<DayInfo>();
        }
        public DateTime LastDayOfWeek { get; set; }
        public DateTime NextDayOfWeek { get; set; }
        public List<DayInfo> DayInfos { get; set; }
        public bool IsWeekSubmit { get; set; }
    }
}
