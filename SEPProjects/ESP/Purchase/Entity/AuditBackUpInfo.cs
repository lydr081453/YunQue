namespace ESP.Purchase.Entity
{
    /// <summary>
    /// 实体类T_AuditBackUp 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class AuditBackUpInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuditBackUpInfo"/> class.
        /// </summary>
        public AuditBackUpInfo()
        { }

        #region Model
        private int _id;
        private int _userid;
        private int _backupuserid;
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
        /// 用户ID
        /// </summary>
        /// <value>The user id.</value>
        public int userId
        {
            set { _userid = value; }
            get { return _userid; }
        }

        /// <summary>
        /// 候补UserId
        /// </summary>
        /// <value>The backup user id.</value>
        public int backupUserId
        {
            set { _backupuserid = value; }
            get { return _backupuserid; }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        /// <value>The begin date.</value>
        public string BeginDate
        {
            set { _begindate = value; }
            get { return _begindate; }
        }

        /// <summary>
        /// 结束时间
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