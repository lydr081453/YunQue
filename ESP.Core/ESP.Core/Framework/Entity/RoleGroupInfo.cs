using System;
namespace ESP.Framework.Entity
{
    ///<summary>
    ///角色组（方便角色管理）
    ///</summary>
    [Serializable]
    public class RoleGroupInfo
    {
        #region "variables"
        private Int32 _RoleGroupID;
        private String _RoleGroupName;
        private Int32 _ParentID;
        private String _Description;
        private Int32 _RoleGroupLevel;

        private Int32 _Creator;
        private Int32 _LastModifier;
        private DateTime _CreatedTime;
        private DateTime _LastModifiedTime;
        private string _CreatorName;
        private string _LastModifierName;

        private Byte[] _RowVersion;
        #endregion

		
        #region "Properties"
        ///<summary>
        ///标识列
        ///</summary>
        public Int32 RoleGroupID
        {
            get{return _RoleGroupID;}
            set{_RoleGroupID = value;}
        }
        ///<summary>
        ///角色组名称
        ///</summary>
        public String RoleGroupName
        {
            get{return _RoleGroupName;}
            set{_RoleGroupName = value;}
        }
        ///<summary>
        ///上一级分组的标识
        ///</summary>
        public Int32 ParentID
        {
            get{return _ParentID;}
            set{_ParentID = value;}
        }
        ///<summary>
        ///描述
        ///</summary>
        public String Description
        {
            get{return _Description;}
            set{_Description = value;}
        }
        ///<summary>
        ///在角色组树中的级次
        ///</summary>
        public Int32 RoleGroupLevel
        {
            get{return _RoleGroupLevel;}
            set{_RoleGroupLevel = value;}
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
        /// 记录版本
        /// </summary>
        public Byte[] RowVersion
        {
            get { return _RowVersion; }
            set { _RowVersion = value; }
        }

        #endregion
    }
}
