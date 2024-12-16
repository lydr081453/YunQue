using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类SupportMemberInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class SupportMemberInfo
    {
        public SupportMemberInfo()
        { }
        #region Model
        private int _supportmemberid;
        private byte[] _lastupdatetime;
        private int _projectid;
        private string _projectcode;
        private int _supportid;
        private int? _memberuserid;
        private string _membercode;
        private string _memberemployeename;
        private string _memberusername;
        private DateTime? _createtime;
        /// <summary>
        /// 自增编号
        /// </summary>
        public int SupportMemberId
        {
            set { _supportmemberid = value; }
            get { return _supportmemberid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public byte[] Lastupdatetime
        {
            set { _lastupdatetime = value; }
            get { return _lastupdatetime; }
        }
        /// <summary>
        /// 项目号申请单编号
        /// </summary>
        public int ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProjectCode
        {
            set { _projectcode = value; }
            get { return _projectcode; }
        }
        /// <summary>
        /// 支持方编号
        /// </summary>
        public int SupportID
        {
            set { _supportid = value; }
            get { return _supportid; }
        }
        /// <summary>
        /// 成员Id自增长
        /// </summary>
        public int? MemberUserID
        {
            set { _memberuserid = value; }
            get { return _memberuserid; }
        }
        /// <summary>
        /// 公司内部编号
        /// </summary>
        public string MemberCode
        {
            set { _membercode = value; }
            get { return _membercode; }
        }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string MemberEmployeeName
        {
            set { _memberemployeename = value; }
            get { return _memberemployeename; }
        }
        /// <summary>
        /// 登录帐号
        /// </summary>
        public string MemberUserName
        {
            set { _memberusername = value; }
            get { return _memberusername; }
        }
        /// <summary>
        /// 加入时间
        /// </summary>
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        #endregion Model

    }

    public partial class SupportMemberInfo
    {
        int? _groupid;

        public int? GroupID
        {
            get { return _groupid; }
            set { _groupid = value; }
        }

        string _groupName;

        public string GroupName
        {
            get { return _groupName; }
            set { _groupName = value; }
        }

        string _memberEmail;

        public string MemberEmail
        {
            get { return _memberEmail; }
            set { _memberEmail = value; }
        }

        string _memberPhone;

        public string MemberPhone
        {
            get { return _memberPhone; }
            set { _memberPhone = value; }
        }

        int? _roleID;

        public int? RoleID
        {
            get { return _roleID; }
            set { _roleID = value; }
        }

        string _roleName;

        public string RoleName
        {
            get { return _roleName; }
            set { _roleName = value; }
        }
    }
}