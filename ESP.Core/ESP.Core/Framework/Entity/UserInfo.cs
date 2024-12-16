using System;
using ESP.Framework.DataAccess.Utilities;
namespace ESP.Framework.Entity
{
    ///<summary>
    ///用户信息表
    ///</summary>
    [Serializable]
    public class UserInfo
    {
        #region "variables"
        private Int32 _UserID = NullValues.Int32;
        private String _Username = NullValues.String;
        private String _FirstNameCN = NullValues.String;
        private String _LastNameCN = NullValues.String;
        private String _FirstNameEN = NullValues.String;
        private String _LastNameEN = NullValues.String;
        private String _Email = NullValues.String;
        private DateTime _CreatedDate = NullValues.DateTime;
        private DateTime _LastActivityDate = NullValues.DateTime;
        private Int32 _Status = NullValues.Int32;
        private Boolean _IsApproved = NullValues.Boolean;
        private Boolean _IsLockedOut = NullValues.Boolean;
        private DateTime _LastLoginDate = NullValues.DateTime;
        private DateTime _LastPasswordChangedDate = NullValues.DateTime;
        private DateTime _LastLockoutDate = NullValues.DateTime;
        private String _Comment = NullValues.String;
        #endregion


        #region "Properties"
        ///<summary>
        ///标识列
        ///</summary>
        public Int32 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        ///<summary>
        ///用户名，全局唯一
        ///</summary>
        public String Username
        {
            get { return _Username; }
            set { _Username = value; }
        }
        /// <summary>
        /// 中文名字
        /// </summary>
        public String FirstNameCN
        {
            get { return _FirstNameCN; }
            set { _FirstNameCN = value; }
        }
        /// <summary>
        /// 中文姓氏
        /// </summary>
        public String LastNameCN
        {
            get { return _LastNameCN; }
            set { _LastNameCN = value; }
        }
        /// <summary>
        /// 中文全名
        /// </summary>
        public String FullNameCN
        {
            get { return _LastNameCN + _FirstNameCN; }
        }
        /// <summary>
        /// 英文名字
        /// </summary>
        public String FirstNameEN
        {
            get { return _FirstNameEN; }
            set { _FirstNameEN = value; }
        }
        /// <summary>
        /// 英文姓氏
        /// </summary>
        public String LastNameEN
        {
            get { return _LastNameEN; }
            set { _LastNameEN = value; }
        }
        /// <summary>
        /// 英文命名
        /// </summary>
        public String FullNameEN
        {
            get
            {
                if (_FirstNameEN == null || _FirstNameEN.Length == 0)
                    return _LastNameEN;
                if (_LastNameEN == null || _LastNameEN.Length == 0)
                    return _FirstNameEN;
                return _FirstNameEN + " " + _LastNameEN;
            }
        }
        ///<summary>
        ///邮件地址
        ///</summary>
        public String Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        ///<summary>
        ///创建日期/注册日期
        ///</summary>
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        ///<summary>
        ///最后一次活动时间
        ///</summary>
        public DateTime LastActivityDate
        {
            get { return _LastActivityDate; }
            set { _LastActivityDate = value; }
        }
        ///<summary>
        ///状态
        ///</summary>
        public Int32 Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        ///<summary>
        ///是否已通过审核
        ///</summary>
        public Boolean IsApproved
        {
            get { return _IsApproved; }
            set { _IsApproved = value; }
        }
        ///<summary>
        ///是否被锁定
        ///</summary>
        public Boolean IsLockedOut
        {
            get { return _IsLockedOut; }
            set { _IsLockedOut = value; }
        }
        ///<summary>
        ///最后登录时间
        ///</summary>
        public DateTime LastLoginDate
        {
            get { return _LastLoginDate; }
            set { _LastLoginDate = value; }
        }
        ///<summary>
        ///最后密码修改时间
        ///</summary>
        public DateTime LastPasswordChangedDate
        {
            get { return _LastPasswordChangedDate; }
            set { _LastPasswordChangedDate = value; }
        }
        ///<summary>
        ///最后锁定时间
        ///</summary>
        public DateTime LastLockoutDate
        {
            get { return _LastLockoutDate; }
            set { _LastLockoutDate = value; }
        }
        ///<summary>
        ///备注
        ///</summary>
        public String Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }


        #endregion
    }
}
