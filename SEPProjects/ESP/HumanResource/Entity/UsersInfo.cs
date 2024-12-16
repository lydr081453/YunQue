using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Framework.DataAccess.Utilities;
namespace ESP.HumanResource.Entity
{
    public class UsersInfo
    {
        public UsersInfo()
        { }
        #region Model
        private int _userid = NullValues.Int32;
        private int _status = NullValues.Int32;
        private string _password = NullValues.String;
        private string _passwordsalt = NullValues.String;
        private bool _isapproved = NullValues.Boolean;
        private bool _islockedout = NullValues.Boolean;
        private DateTime _lastlogindate = NullValues.DateTime;
        private DateTime _lastpasswordchangeddate = NullValues.DateTime;
        private DateTime _lastlockoutdate = NullValues.DateTime;
        private int _failedpasswordattemptcount = NullValues.Int32;
        private DateTime _failedpasswordattemptwindowstart = NullValues.DateTime;
        private string _username = NullValues.String;
        private string _comment = NullValues.String;
        private string _resetpasswordcode = NullValues.String;
        private bool _isdeleted = NullValues.Boolean;
        private string _firstnamecn = NullValues.String;
        private string _lastnamecn = NullValues.String;
        private string _firstnameen = NullValues.String;
        private string _lastnameen = NullValues.String;
        private string _email = NullValues.String;
        private DateTime _createddate = NullValues.DateTime;
        private DateTime _lastactivitydate = NullValues.DateTime;
        /// <summary>
        /// 
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PasswordSalt
        {
            set { _passwordsalt = value; }
            get { return _passwordsalt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsApproved
        {
            set { _isapproved = value; }
            get { return _isapproved; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsLockedOut
        {
            set { _islockedout = value; }
            get { return _islockedout; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastLoginDate
        {
            set { _lastlogindate = value; }
            get { return _lastlogindate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastPasswordChangedDate
        {
            set { _lastpasswordchangeddate = value; }
            get { return _lastpasswordchangeddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastLockoutDate
        {
            set { _lastlockoutdate = value; }
            get { return _lastlockoutdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int FailedPasswordAttemptCount
        {
            set { _failedpasswordattemptcount = value; }
            get { return _failedpasswordattemptcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime FailedPasswordAttemptWindowStart
        {
            set { _failedpasswordattemptwindowstart = value; }
            get { return _failedpasswordattemptwindowstart; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Username
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Comment
        {
            set { _comment = value; }
            get { return _comment; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ResetPasswordCode
        {
            set { _resetpasswordcode = value; }
            get { return _resetpasswordcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsDeleted
        {
            set { _isdeleted = value; }
            get { return _isdeleted; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FirstNameCN
        {
            set { _firstnamecn = value; }
            get { return _firstnamecn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LastNameCN
        {
            set { _lastnamecn = value; }
            get { return _lastnamecn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FirstNameEN
        {
            set { _firstnameen = value; }
            get { return _firstnameen; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LastNameEN
        {
            set { _lastnameen = value; }
            get { return _lastnameen; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastActivityDate
        {
            set { _lastactivitydate = value; }
            get { return _lastactivitydate; }
        }
        #endregion Model
    }
}
