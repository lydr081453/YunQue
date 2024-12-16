using System;
using System.Collections.Generic;
using System.Text;

namespace ESP.Framework.Entity
{

    /// <summary>
    /// 模块实体类。
    /// </summary>
    [Serializable]
    public class ModuleInfo
    {
        #region Fields
        private int _ModuleID;
        private int _WebSiteID;
        private string _ModuleName;
        private string _Description;
        private int _ParentID;
        private int _DefaultPageID;
        private string _DefaultPageUrl;
        private int _Creator;
        private String _CreatorName;
        private DateTime _CreatedTime;
        private int _LastModifier;
        private String _LastModifierName;
        private DateTime _LastModifiedTime;
        private string _Node;
        private string _NodePath;
        private int _NodeLevel;
        private ModuleType _NodeType;
        private Int32 _Ordinal;
        private byte[] _RowVersion;
        #endregion

        /// <summary>
        /// 模块ID
        /// </summary>
        public int ModuleID
        {
            get { return _ModuleID; }
            set { _ModuleID = value; }
        }
        /// <summary>
        /// 模块所属站点ID
        /// </summary>
        public int WebSiteID
        {
            get { return _WebSiteID; }
            set { _WebSiteID = value; }
        }
        /// <summary>
        /// 模块名
        /// </summary>
        public string ModuleName
        {
            get { return _ModuleName; }
            set { _ModuleName = value; }
        }
        /// <summary>
        /// 模块描述
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        /// <summary>
        /// 上一级模块ID
        /// </summary>
        public int ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }
        /// <summary>
        /// 默认页ID
        /// </summary>
        public int DefaultPageID
        {
            get { return _DefaultPageID; }
            set { _DefaultPageID = value; }
        }
        /// <summary>
        /// 默认页面的Url
        /// </summary>
        public string DefaultPageUrl
        {
            get { return _DefaultPageUrl; }
            set { _DefaultPageUrl = value; }
        }

        /// <summary>
        /// 节点编号 ("00" -> "99")
        /// </summary>
        public string Node
        {
            get { return _Node; }
            set { _Node = value; }
        }
        /// <summary>
        /// 节点路径
        /// </summary>
        public string NodePath
        {
            get { return _NodePath; }
            set { _NodePath = value; }
        }
        /// <summary>
        /// 节点级别
        /// </summary>
        public int NodeLevel
        {
            get { return _NodeLevel; }
            set { _NodeLevel = value; }
        }
        /// <summary>
        /// 节点类型，1为非叶子节点，2为叶子节点
        /// </summary>
        public ModuleType NodeType
        {
            get { return _NodeType; }
            set { _NodeType = value; }
        }
        /// <summary>
        /// 排序次序
        /// </summary>
        public Int32 Ordinal
        {
            get { return _Ordinal; }
            set { _Ordinal = value; }
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
        /// 数据行版本
        /// </summary>
        public byte[] RowVersion
        {
            get { return _RowVersion; }
            set { _RowVersion = value; }
        }
    }

    public class UserMenuTreeInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isexpand { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<UserMenuTreeInfo> children { get; set; }
    }


    /// <summary>
    /// 模块类型
    /// </summary>
    public enum ModuleType
    {
        /// <summary>
        /// 功能模块
        /// </summary>
        Module = 1,

        /// <summary>
        /// 文件夹
        /// </summary>
        Folder = 2,

        /// <summary>
        /// 站点
        /// </summary>
        WebSite = 3
    }
}
