using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    public partial class TimeSheetLogInfo
    {
        public TimeSheetLogInfo()
        { }
        #region Model
        private int _id;
        private int _commitid;
        private int _auditorid;
        private string _auditorname;
        private string _suggestion;
        private DateTime _auditdate;
        private int _status;
        private string _ip;
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
        public int CommitId
        {
            set { _commitid = value; }
            get { return _commitid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int AuditorId
        {
            set { _auditorid = value; }
            get { return _auditorid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AuditorName
        {
            set { _auditorname = value; }
            get { return _auditorname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Suggestion
        {
            set { _suggestion = value; }
            get { return _suggestion; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime AuditDate
        {
            set { _auditdate = value; }
            get { return _auditdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Status
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
        #endregion Model

    }
}
