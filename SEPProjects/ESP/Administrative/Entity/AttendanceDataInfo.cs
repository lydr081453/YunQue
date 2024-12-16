using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    /// <summary>
    /// 考勤信息数据对象
    /// </summary>
    [Serializable]
    public class AttendanceDataInfo
    {
        #region 成员变量
        private int _lateCount = 0;                // 迟到次数
        private int _leaveEarlyCount = 0;          // 早退次数
        private decimal _sickLeaveHours = 0;       // 病假小时数
        private decimal _affiairLeaveHours = 0;    // 事假小时数
        private decimal _annualLeaveHours = 0;     // 年假小时数
        private decimal _maternityLeaveHours = 0;  // 产假小时数
        private decimal _marriageLeaveHours = 0;   // 婚假小时数
        private decimal _funeralLeaveHours = 0;    // 丧假小时数
        private decimal _absentHours = 0;          // 旷工小时数
        private decimal _overTimeHours = 0;        // OT小时数
        private decimal _evectionHours = 0;        // 出差小时数
        private decimal _egressHours = 0;          // 外出小时数
        private string _other = "";                // 其他
        private decimal _offTuneHours = 0;         // 调休小时数
        private decimal _holidayOverTimeHours = 0; // 节假日OT小时数
        private decimal _prenatalCheckHours = 0;   // 产前检查小时数
        private decimal _incentiveHours = 0;       // 奖励假小时数
        #endregion

        #region 成员属性
        /// <summary>
        /// 迟到次数
        /// </summary>
        public int LateCount
        {
            get { return _lateCount; }
            set { _lateCount = value; }
        }
        /// <summary>
        /// 早退次数
        /// </summary>
        public int LeaveEarlyCount
        {
            get { return _leaveEarlyCount; }
            set { _leaveEarlyCount = value; }
        }
        /// <summary>
        /// 病假小时数
        /// </summary>
        public decimal SickLeaveHours
        {
            get { return _sickLeaveHours; }
            set { _sickLeaveHours = value; }
        }

        public decimal SickByYear { get; set; }
        /// <summary>
        /// 事假小时数
        /// </summary>
        public decimal AffiairLeaveHours
        {
            get { return _affiairLeaveHours; }
            set { _affiairLeaveHours = value; }
        }

        public decimal AffairByYear { get; set; }


        /// <summary>
        /// 年假小时数
        /// </summary>
        public decimal AnnualLeaveHours
        {
            get { return _annualLeaveHours; }
            set { _annualLeaveHours = value; }
        }

        public decimal LastAnnualHours { get; set; }

        public decimal AnnualLeaveByYear { get; set; }

        /// <summary>
        /// 产假小时数
        /// </summary>
        public decimal MaternityLeaveHours
        {
            get { return _maternityLeaveHours; }
            set { _maternityLeaveHours = value; }
        }
        /// <summary>
        /// 婚假小时数
        /// </summary>
        public decimal MarriageLeaveHours
        {
            get { return _marriageLeaveHours; }
            set { _marriageLeaveHours = value; }
        }
        /// <summary>
        /// 丧假小时数
        /// </summary>
        public decimal FuneralLeaveHours
        {
            get { return _funeralLeaveHours; }
            set { _funeralLeaveHours = value; }
        }
        /// <summary>
        /// 旷工小时数
        /// </summary>
        public decimal AbsentHours
        {
            get { return _absentHours; }
            set { _absentHours = value; }
        }
        /// <summary>
        /// OT小时数
        /// </summary>
        public decimal OverTimeHours
        {
            get { return _overTimeHours; }
            set { _overTimeHours = value; }
        }
        /// <summary>
        /// 出差小时数
        /// </summary>
        public decimal EvectionHours
        {
            get { return _evectionHours; }
            set { _evectionHours = value; }
        }
        /// <summary>
        /// 外出小时数
        /// </summary>
        public decimal EgressHours
        {
            get { return _egressHours; }
            set { _egressHours = value; }
        }
        /// <summary>
        /// 其他
        /// </summary>
        public string Other
        {
            get { return _other; }
            set { _other = value; }
        }
        /// <summary>
        /// 调休小时数
        /// </summary>
        public decimal OffTuneHours
        {
            get { return _offTuneHours; }
            set { _offTuneHours = value; }
        }
        /// <summary>
        /// 节假日OT小时数
        /// </summary>
        public decimal HolidayOverTimeHours
        {
            get { return _holidayOverTimeHours; }
            set { _holidayOverTimeHours = value; }
        }

        /// <summary>
        /// 产前检查小时数
        /// </summary>
        public decimal PrenatalCheckHours
        {
            get { return _prenatalCheckHours; }
            set { _prenatalCheckHours = value; }
        }
        /// <summary>
        /// 奖励假小时数
        /// </summary>
        public decimal IncentiveHours
        {
            get { return _incentiveHours; }
            set { _incentiveHours = value; }
        }

        public decimal IncentiveByYear { get; set; }

        public decimal PeiChanJia { get; set; }
        #endregion
    }
}