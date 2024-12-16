using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public class CheckInfo
    {
        public CheckInfo()
        { }
        #region Model
        private int _checkid;
        private string _checksyscode;
        private string _checkcode;
        private DateTime? _createdate;
        private int? _checkstatus;
        private int? _creatorid;
        private string _creatorusercode;
        private string _creatorusername;
        private string _creatoremployeename;
        /// <summary>
        /// 
        /// </summary>
        public int CheckID
        {
            set { _checkid = value; }
            get { return _checkid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckSysCode
        {
            set { _checksyscode = value; }
            get { return _checksyscode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckCode
        {
            set { _checkcode = value; }
            get { return _checkcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CheckStatus
        {
            set { _checkstatus = value; }
            get { return _checkstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CreatorID
        {
            set { _creatorid = value; }
            get { return _creatorid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreatorUserCode
        {
            set { _creatorusercode = value; }
            get { return _creatorusercode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreatorUserName
        {
            set { _creatorusername = value; }
            get { return _creatorusername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreatorEmployeeName
        {
            set { _creatoremployeename = value; }
            get { return _creatoremployeename; }
        }
        #endregion Model
    }
}
