using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类ExpenseAccountInfo 
    /// </summary>
    [Serializable]
    public partial class ExpenseAuditDetailInfo
    {
        public ExpenseAuditDetailInfo()
        { }

        #region Model
        private int _id;
        private int? _expenseauditid;
        private int? _auditoruserid;
        private string _auditorusername;
        private string _auditorusercode;
        private string _auditoremployeename;
        private string _suggestion;
        private DateTime? _auditedate;
        private int? _auditestatus;
        private int? _audittype;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ExpenseAuditID
        {
            set { _expenseauditid = value; }
            get { return _expenseauditid; }
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
        public int? AuditType
        {
            set { _audittype = value; }
            get { return _audittype; }
        }
        #endregion Model

    }
}
