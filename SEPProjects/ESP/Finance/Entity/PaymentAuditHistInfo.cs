using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类PaymentAuditHistInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class PaymentAuditHistInfo
    {
        public PaymentAuditHistInfo()
        { }
        #region Model
        private int _paymentauditid;
        private int? _paymentid;
        private int? _auditoruserid;
        private string _auditorusername;
        private string _auditorusercode;
        private string _auditoremployeename;
        private string _suggestion;
        private DateTime? _auditedate;
        private int? _auditestatus;
        private int? _nextauditorid;
        private string _nextauditorusercode;
        private string _nextauditorusername;
        private string _nextauditoremployeename;
        private int? _audittype;
        private int? _version;
        /// <summary>
        /// 
        /// </summary>
        public int PaymentAuditID
        {
            set { _paymentauditid = value; }
            get { return _paymentauditid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? PaymentID
        {
            set { _paymentid = value; }
            get { return _paymentid; }
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
        public int? AuditType
        {
            set { _audittype = value; }
            get { return _audittype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Version
        {
            set { _version = value; }
            get { return _version; }
        }
        #endregion Model

    }
}

