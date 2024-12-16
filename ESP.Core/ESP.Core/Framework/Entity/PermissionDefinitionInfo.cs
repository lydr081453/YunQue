using System;
namespace ESP.Framework.Entity
{
    ///<summary>
    ///权限定义表
    ///</summary>
    [Serializable]
    public class PermissionDefinitionInfo
    {
        #region Fields
        private Int32 _PermissionDefinitionID;
        private String _PermissionName;
        private Int32 _ReferredEntityID;
        private EntityType _ReferredEntityType;
        private String _Description;

        private Int32 _Creator;
        private DateTime _CreatedTime;

        #endregion


        #region "Properties"
        ///<summary>
        ///标识列
        ///</summary>
        public Int32 PermissionDefinitionID
        {
            get { return _PermissionDefinitionID; }
            set { _PermissionDefinitionID = value; }
        }
        ///<summary>
        ///权限的名字
        ///</summary>
        public String PermissionName
        {
            get { return _PermissionName; }
            set { _PermissionName = value; }
        }
        ///<summary>
        ///权限控制的对象的ID
        ///</summary>
        public Int32 ReferredEntityID
        {
            get { return _ReferredEntityID; }
            set { _ReferredEntityID = value; }
        }
        ///<summary>
        ///权限控制的对象的类型（站点、页面、模块）
        ///</summary>
        public EntityType ReferredEntityType
        {
            get { return _ReferredEntityType; }
            set { _ReferredEntityType = value; }
        }
        ///<summary>
        ///描述
        ///</summary>
        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
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
}
