using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public class ProjectScheduleInfo
    {
        #region Model
        private int _scheduleid;
        private int? _projectid;
        private int? _yearvalue;
        private int? _monthvalue;
        private decimal? _monthpercent;
        private decimal? _fee;
        /// <summary>
        /// 
        /// </summary>
        public int ScheduleID
        {
            set { _scheduleid = value; }
            get { return _scheduleid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? YearValue
        {
            set { _yearvalue = value; }
            get { return _yearvalue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? monthValue
        {
            set { _monthvalue = value; }
            get { return _monthvalue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? MonthPercent
        {
            set { _monthpercent = value; }
            get { return _monthpercent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Fee
        {
            set { _fee = value; }
            get { return _fee; }
        }

        public decimal OperationFee { get; set; }

        #endregion Model
    }
}
