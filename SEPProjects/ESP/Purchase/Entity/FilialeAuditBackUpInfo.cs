namespace ESP.Purchase.Entity
{
    /// <summary>
    /// 实体类T_FilialeAuditBackUp 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class FilialeAuditBackUpInfo
    {
        public FilialeAuditBackUpInfo()
        { }

        #region Model
        private int _id;
        private int _userid;
        private int _isbackupuser;
        private string _begindate;
        private string _enddate;

        /// <summary>
        /// 流水号
        /// </summary>
        /// <value>The id.</value>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// Gets or sets the userid.
        /// </summary>
        /// <value>The userid.</value>
        public int userid
        {
            set { _userid= value; }
            get { return _userid; }
        }

        /// <summary>
        /// Gets or sets the is backup user.
        /// </summary>
        /// <value>The is backup user.</value>
        public int isBackupUser
        {
            set { _isbackupuser = value; }
            get { return _isbackupuser; }
        }

        /// <summary>
        /// Gets or sets the begin date.
        /// </summary>
        /// <value>The begin date.</value>
        public string BeginDate
        {
            set { _begindate = value; }
            get { return _begindate; }
        }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>The end date.</value>
        public string EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        #endregion Model
    }
}