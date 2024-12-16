using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminForm.Model
{
    [Serializable]
    public class ReturnAuditHistInfo
    {
        public ReturnAuditHistInfo()
        { }
        #region Model
        private int _returnauditid;
        private string _nextauditorusercode;
        private string _nextauditorusername;
        private string _nextauditoremployeename;
        private int? _auditoruserid;
        private string _auditorusername;
        private string _auditorusercode;
        private string _auditoremployeename;
        private string _suggestion;
        private DateTime? _auditedate;
        private int? _auditestatus;
        private int? _nextauditorid;

        private int? _version;
        private int? _auditType;
        /// <summary>
        /// 
        /// </summary>
        public int ReturnAuditID
        {
            set { _returnauditid = value; }
            get { return _returnauditid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NextAuditorUserCode
        {
            set { _nextauditorusercode = value; }
            get { return _nextauditorusercode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NextAuditorUserName
        {
            set { _nextauditorusername = value; }
            get { return _nextauditorusername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NextAUditorEmployeeName
        {
            set { _nextauditoremployeename = value; }
            get { return _nextauditoremployeename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AuditorUserID
        {
            set { _auditoruserid = value; }
            get { return _auditoruserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AuditorUserName
        {
            set { _auditorusername = value; }
            get { return _auditorusername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AuditorUserCode
        {
            set { _auditorusercode = value; }
            get { return _auditorusercode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AuditorEmployeeName
        {
            set { _auditoremployeename = value; }
            get { return _auditoremployeename; }
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
        public DateTime? AuditeDate
        {
            set { _auditedate = value; }
            get { return _auditedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AuditeStatus
        {
            set { _auditestatus = value; }
            get { return _auditestatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? NextAuditorID
        {
            set { _nextauditorid = value; }
            get { return _nextauditorid; }
        }

        int _returnID;

        public int ReturnID
        {
            get { return _returnID; }
            set { _returnID = value; }
        }


        public int? AuditType
        {
            get { return _auditType; }
            set { _auditType = value; }
        }
        public int? Version
        {
            get { return _version; }
            set { _version = value; }
        }

        #endregion Model

    }
}
