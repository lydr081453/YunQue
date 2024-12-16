using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类LogInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class LogInfo
    {
        public LogInfo()
        { }
        #region Model
        private int _logid;
        private string _operation;
        private string _tablename;
        private string _contents;
        private int? _operatoruserid;
        private string _operatorcode;
        private string _operatorusername;
        private string _operatoremployeename;
        private DateTime? _operatedate;
        /// <summary>
        /// 
        /// </summary>
        public int LogID
        {
            set { _logid = value; }
            get { return _logid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Operation
        {
            set { _operation = value; }
            get { return _operation; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TableName
        {
            set { _tablename = value; }
            get { return _tablename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Contents
        {
            set { _contents = value; }
            get { return _contents; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? OperatorUserID
        {
            set { _operatoruserid = value; }
            get { return _operatoruserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OperatorCode
        {
            set { _operatorcode = value; }
            get { return _operatorcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OperatorUserName
        {
            set { _operatorusername = value; }
            get { return _operatorusername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OperatorEmployeeName
        {
            set { _operatoremployeename = value; }
            get { return _operatoremployeename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? OperateDate
        {
            set { _operatedate = value; }
            get { return _operatedate; }
        }
        #endregion Model

    }
}