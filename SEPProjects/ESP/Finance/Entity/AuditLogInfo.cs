using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类AuditLogInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class AuditLogInfo
    {
        public AuditLogInfo()
        { }
        #region Model
        private int _auditlogid;
        private int? _auditorsysid;
        private string _auditorusercode;
        private string _auditorusername;
        private string _auditoremployeename;
        private string _suggestion;
        private int? _formid;
        private int? _formtype;
        private DateTime? _auditdate;
        private int? _auditstatus;
        /// <summary>
        /// 
        /// </summary>
        public int AuditLogID
        {
            set { _auditlogid = value; }
            get { return _auditlogid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AuditorSysID
        {
            set { _auditorsysid = value; }
            get { return _auditorsysid; }
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
        public string AuditorUserName
        {
            set { _auditorusername = value; }
            get { return _auditorusername; }
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
        public int? FormID
        {
            set { _formid = value; }
            get { return _formid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FormType
        {
            set { _formtype = value; }
            get { return _formtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? AuditDate
        {
            set { _auditdate = value; }
            get { return _auditdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AuditStatus
        {
            set { _auditstatus = value; }
            get { return _auditstatus; }
        }
        #endregion Model

    }
}

