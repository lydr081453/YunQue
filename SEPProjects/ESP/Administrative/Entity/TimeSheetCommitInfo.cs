using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    public partial class TimeSheetCommitInfo
    {
        public TimeSheetCommitInfo()
        { }
        #region Model
        private int _id;
        private int? _weekid;
        private int? _userid;
        private string _usercode;
        private string _username;
        private int? _departmentid;
        private string _departmentname;
        private DateTime? _createdate;
        private DateTime? _commitdate;
        private int? _status;
        private string _ip;
        private DateTime? _goworktime;
        private DateTime? _offworktime;
        private DateTime? _currentdate;
        private string _committype;
        private string _description;
        


        public int TimeSheetCount { get; set; }
        public decimal TimeSheetHours { get; set; }
        public DateTime? CurrentGoWorkTime { get; set; }
        public DateTime? CurrentOffWorkTime { get; set; }


        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int WaiChu { get; set; }
        public int QingJia { get; set; }
        public int ChuChai { get; set; }
        public int JiaBan { get; set; }
        public int TiaoXiu { get; set; }
        public int ChiDao { get; set; }
        public int ZaoTui { get; set; }
        public int KuangGong { get; set; }


        public string SerialNo { get; set; }

        /// <summary>
        /// 非TimeSheetCommit字段
        /// </summary>
        public string WorkItem { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value;}
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? WeekId
        {
            set { _weekid = value; }
            get { return _weekid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserCode
        {
            set { _usercode = value; }
            get { return _usercode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DepartmentId
        {
            set { _departmentid = value; }
            get { return _departmentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DepartmentName
        {
            set { _departmentname = value; }
            get { return _departmentname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CommitDate
        {
            set { _commitdate = value; }
            get { return _commitdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IP
        {
            set { _ip = value; }
            get { return _ip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? GoWorkTime
        {
            set { _goworktime = value; }
            get { return _goworktime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? OffWorkTime
        {
            set { _offworktime = value; }
            get { return _offworktime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CurrentDate
        {
            set { _currentdate = value; }
            get { return _currentdate; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CommitType
        {
            set { _committype = value; }
            get { return _committype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }



        public decimal OverWorkHours { get; set; }
        public decimal OverWorkRemain { get; set; }
        public DateTime AuditDate { get; set; }
        #endregion Model

    }
}
