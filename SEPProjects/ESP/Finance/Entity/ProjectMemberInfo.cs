using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类ProjectMemberInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ProjectMemberInfo
    {
        public ProjectMemberInfo()
        { }
        #region Model
        private int _memberid;
        private int _projectid;
        private string _projectcode;
        private int? _memberuserid;
        private string _memberusername;
        private string _membercode;
        private string _memberemployeename;
        private DateTime? _createtime;
        private byte[] _lastupdatetime;
        /// <summary>
        /// 
        /// </summary>
        public int MemberId
        {
            set { _memberid = value; }
            get { return _memberid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ProjectId
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
        /// 成员ID自增长
        /// </summary>
        public int? MemberUserID
        {
            set { _memberuserid = value; }
            get { return _memberuserid; }
        }
        /// <summary>
        /// 成员登录帐号
        /// </summary>
        public string MemberUserName
        {
            set { _memberusername = value; }
            get { return _memberusername; }
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
        /// 
        /// </summary>
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public byte[] Lastupdatetime
        {
            set { _lastupdatetime = value; }
            get { return _lastupdatetime; }
        }
        #endregion Model

    }

    public partial class ProjectMemberInfo
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