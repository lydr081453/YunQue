using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类SupporterScheduleInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class SupporterScheduleInfo
    {
        public SupporterScheduleInfo()
        { }
        #region Model
        private int _supporterscheduleid;
        private int? _projectid;
        private string _projectcode;
        private int? _supporterid;
        private int? _yearvalue;
        private int? _monthvalue;
        private decimal? _monthpercent;
        private decimal? _fee;
        /// <summary>
        /// 
        /// </summary>
        public int SupporterScheduleID
        {
            set { _supporterscheduleid = value; }
            get { return _supporterscheduleid; }
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
        public string ProjectCode
        {
            set { _projectcode = value; }
            get { return _projectcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SupporterID
        {
            set { _supporterid = value; }
            get { return _supporterid; }
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
        #endregion Model

    }
}


