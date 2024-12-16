using System;
namespace ESP.Framework.Entity
{
    ///<summary>
    ///页面表
    ///</summary>
    [Serializable]
    public class WebPageInfo
    {
        #region "variables"
        private Int32 _WebPageID;
        private String _AppRelativePath;
        private String _Description;
        private Int32 _WebSiteID;
        private Int32 _ModuleID;

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
        public Int32 WebPageID
        {
            get{return _WebPageID;}
            set{_WebPageID = value;}
        }
        /// <summary>
        /// 所属站点ID
        /// </summary>
        public Int32 WebSiteID
        {
            get { return _WebSiteID; }
            set { _WebSiteID = value; }
        }
        ///<summary>
        ///所属模块ID
        ///</summary>
        public Int32 ModuleID
        {
            get { return _ModuleID; }
            set { _ModuleID = value; }
        }
        ///<summary>
        ///相对于应用程序根的路径
        ///</summary>
        public String AppRelativePath
        {
            get{return _AppRelativePath;}
            set{_AppRelativePath = value;}
        }
        ///<summary>
        ///描述
        ///</summary>
        public String Description
        {
            get{return _Description;}
            set{_Description = value;}
        }


        /// <summary>
        /// 创建者
        /// </summary>
        public int Creator
        {
            get { return _Creator; }
            set { _Creator = value; }
        }

        /// <summary>
        /// 最后修改者
        /// </summary>
        public int LastModifier
        {
            get { return _LastModifier; }
            set { _LastModifier = value; }
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
        /// 最后修改时间
        /// </summary>
        public DateTime LastModifiedTime
        {
            get { return _LastModifiedTime; }
            set { _LastModifiedTime = value; }
        }

        /// <summary>
        /// 创建人用户名
        /// </summary>
        public string CreatorName
        {
            get { return _CreatorName; }
            set { _CreatorName = value; }
        }

        /// <summary>
        /// 最后修改人用户名
        /// </summary>
        public string LastModifierName
        {
            get { return _LastModifierName; }
            set { _LastModifierName = value; }
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
}
