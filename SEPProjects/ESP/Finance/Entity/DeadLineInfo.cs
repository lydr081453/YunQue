using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类DeadLineInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class DeadLineInfo
    {
        public DeadLineInfo()
        { }
        #region Model
        private int _deadlineid;
        private DateTime _deadline;
        private int _deadlineyear;
        private int _deadlinemonth;
        private int _deadlineday;
        private int _createUserID;
        private DateTime _expenseDeadLine;
        private int _status;

        public DateTime DeadLine2 { get; set; }
        public int DeadLineYear2 { get; set; }
        public int DeadLineMonth2 { get; set; }
        public int DeadLineDay2 { get; set; }
        public DateTime ExpenseCommitDeadLine2 { get; set; }
        public DateTime ExpenseAuditDeadLine2 { get; set; }
        public DateTime ExpenseDeadLine2 { get; set; }

        public DateTime ExpenseCommitDeadLine { get; set; }
        public DateTime ExpenseAuditDeadLine { get; set; }
        public DateTime ExpenseDeadLine
        {
            get { return _expenseDeadLine; }
            set { _expenseDeadLine = value; }
        }

        public DateTime ProjectDeadLine { get; set; }
        public int ProjectDeadLineYear { get; set; }
        public int ProjectDeadLineMonth { get; set; }
        public int ProjectDeadLineDay { get; set; }
        public DateTime SalaryDate { get; set; }
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public int CreateUserID
        {
            get { return _createUserID; }
            set { _createUserID = value; }
        }
        private string _createUserCode;

        public string CreateUserCode
        {
            get { return _createUserCode; }
            set { _createUserCode = value; }
        }
        private string _createUserName;

        public string CreateUserName
        {
            get { return _createUserName; }
            set { _createUserName = value; }
        }
        private string _createUserEmpName;

        public string CreateUserEmpName
        {
            get { return _createUserEmpName; }
            set { _createUserEmpName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int DeadLineID
        {
            set { _deadlineid = value; }
            get { return _deadlineid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime DeadLine
        {
            set { _deadline = value; }
            get { return _deadline; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int DeadLineYear
        {
            set { _deadlineyear = value; }
            get { return _deadlineyear; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int DeadLineMonth
        {
            set { _deadlinemonth = value; }
            get { return _deadlinemonth; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int DeadLineDay
        {
            set { _deadlineday = value; }
            get { return _deadlineday; }
        }
        #endregion Model

    }
}

