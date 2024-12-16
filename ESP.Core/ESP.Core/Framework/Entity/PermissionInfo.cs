using System;
namespace ESP.Framework.Entity
{
    ///<summary>
    ///权限分配表
    ///</summary>
    [Serializable]
    public class PermissionInfo
    {
        #region "variables"
        private Int32 _PermissionID;
        private Int32 _PermissionDefinitionID;
        private Int32 _ReferredEntityID;
        private EntityType _ReferredEntityType;
        private Int32 _OwnerID;
        private PermissionOwnerTypes _OwnerType;
        private Int32 _Creator;
        private DateTime _CreatedTime;

        #endregion

		
        #region "Properties"
        ///<summary>
        ///标识列
        ///</summary>
        public Int32 PermissionID
        {
            get{return _PermissionID;}
            set{_PermissionID = value;}
        }
        
        /// <summary>
        /// 权限定义ID
        /// </summary>
        public Int32 PermissionDefinitionID
        {
            get{return _PermissionDefinitionID;}
            set{_PermissionDefinitionID = value;}
        }

        /// <summary>
        /// 权限控制的对象的ID
        /// </summary>
        public Int32 ReferredEntityID
        {
            get { return _ReferredEntityID; }
            set { _ReferredEntityID = value; }
        }
        /// <summary>
        /// 权限控制的对象的类型
        /// </summary>
        public EntityType ReferredEntityType
        {
            get { return _ReferredEntityType; }
            set { _ReferredEntityType = value; }
        }

        ///<summary>
        ///权限的所有者的ID
        ///</summary>
        public Int32 OwnerID
        {
            get{return _OwnerID;}
            set{_OwnerID = value;}
        }
        ///<summary>
        ///权限的所有者的类型（用户、角色）
        ///</summary>
        public PermissionOwnerTypes OwnerType
        {
            get{return _OwnerType;}
            set{_OwnerType = value;}
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
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime
        {
            get { return _CreatedTime; }
            set { _CreatedTime = value; }
        }
        #endregion
    }

    /// <summary>
    /// 实体类型
    /// </summary>
    public enum EntityType
    {
        /// <summary>
        /// 站点
        /// </summary>
        WebSite = 1,

        /// <summary>
        /// 模块
        /// </summary>
        Module = 2,

        /// <summary>
        /// 页面
        /// </summary>
        WebPage = 3
    }

    /// <summary>
    /// 权限所有者类型
    /// </summary>
    public enum PermissionOwnerTypes
    {
        /// <summary>
        /// 伪角色
        /// </summary>
        FakeRole = 1,

        /// <summary>
        /// 角色
        /// </summary>
        Role = 2,

        /// <summary>
        /// 用户
        /// </summary>
        User = 3
    }

    /// <summary>
    /// 伪角色(内置角色)
    /// </summary>
    public static class FakeRoles
    {
        /// <summary>
        /// 所有访问者
        /// </summary>
        public const int Everyone = 1;

        /// <summary>
        /// 匿名访问者
        /// </summary>
        public const int Anonymous = 2;

        /// <summary>
        /// 注册用户
        /// </summary>
        public const int Registered = 3;
    }
}
