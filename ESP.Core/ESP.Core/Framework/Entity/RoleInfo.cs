using System;
namespace ESP.Framework.Entity
{
    ///<summary>
    ///角色表
    ///</summary>
    [Serializable]
    public class RoleInfo
    {
        #region "variables"
        private Int32 _RoleID;
        private String _RoleName;
        private String _Description;
        private Int32 _RoleGroupID;
        private string roleGroupName;

        private Int32 _Creator;
        private DateTime _CreatedTime;
        private Int32 _LastModifier;
        private DateTime _LastModifiedTime;
        private string _CreatorName;
        private string _LastModifierName;

        private Byte[] _RowVersion;
        #endregion


        #region "Properties"
        ///<summary>
        ///标识列
        ///</summary>
        public Int32 RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }
        ///<summary>
        ///角色名称
        ///</summary>
        public String RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }
        ///<summary>
        ///描述
        ///</summary>
        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        ///<summary>
        ///所属角色组ID
        ///</summary>
        public Int32 RoleGroupID
        {
            get { return _RoleGroupID; }
            set { _RoleGroupID = value; }
        }
        /// <summary>
        /// 所属角色组的名字
        /// </summary>
        public string RoleGroupName
        {
            get { return roleGroupName; }
            set { roleGroupName = value; }
        }
        /// <summary>
        /// 创建者ID
        /// </summary>
        public int Creator
        {
            get { return _Creator; }
            set { _Creator = value; }
        }
        /// <summary>
        /// 创建者用户名
        /// </summary>
        public String CreatorName
        {
            get { return _CreatorName; }
            set { _CreatorName = value; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime
        {
            get { return _CreatedTime; }
            set { _CreatedTime = value; }
        }
        /// <summary>
        /// 最后修改人用户ID
        /// </summary>
        public int LastModifier
        {
            get { return _LastModifier; }
            set { _LastModifier = value; }
        }
        /// <summary>
        /// 最后修改人用户名
        /// </summary>
        public String LastModifierName
        {
            get { return _LastModifierName; }
            set { _LastModifierName = value; }
        }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModifiedTime
        {
            get { return _LastModifiedTime; }
            set { _LastModifiedTime = value; }
        }

        /// <summary>
        /// 数据版本
        /// </summary>
        public Byte[] RowVersion
        {
            get { return _RowVersion; }
            set { _RowVersion = value; }
        }

        #endregion
    }

    /// <summary>
    /// 角色拥有者类型
    /// </summary>
    public enum RoleOwnerType
    {
        /// <summary>
        /// 用户
        /// </summary>
        User = 1,

        /// <summary>
        /// 部门
        /// </summary>
        Department = 2
    }
}
