using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public class SupporterAuditHistInfo
    {
        public SupporterAuditHistInfo()
        { }
        #region Model
        private int _supporterauditid;
        private int? _auditstatus;
        private int? _supporterid;
        private int? _auditoruserid;
        private string _auditorusername;
        private string _auditorusercode;
        private string _auditoremployeename;
        private string _suggestion;
        private int? _squencelevel;
        private int? _totallevel;

        private int? _version;
        private int? _auditType;

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

        /// <summary>
        /// 
        /// </summary>
        public int SupporterAuditID
        {
            set { _supporterauditid = value; }
            get { return _supporterauditid; }
        }
        /// <summary>
        /// 审批状态：1审批通过 2审批驳回
        /// </summary>
        public int? AuditStatus
        {
            set { _auditstatus = value; }
            get { return _auditstatus; }
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
        /// 审批人信息
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
        /// 所处审批级别
        /// </summary>
        public int? SquenceLevel
        {
            set { _squencelevel = value; }
            get { return _squencelevel; }
        }
        /// <summary>
        /// 审批总级别
        /// </summary>
        public int? TotalLevel
        {
            set { _totallevel = value; }
            get { return _totallevel; }
        }

        DateTime? _auditDate;
        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? AuditDate
        {
            get { return _auditDate; }
            set { _auditDate = value; }
        }
        #endregion Model
    }
}
