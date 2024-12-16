using System;
namespace ESP.Framework.Entity
{
    ///<summary>
    ///站点表
    ///</summary>
    [Serializable]
    public class WebSiteInfo
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public WebSiteInfo()
        {
            CreatedTime = ESP.Framework.DataAccess.Utilities.NullValues.DateTime;
            LastModifiedTime = ESP.Framework.DataAccess.Utilities.NullValues.DateTime;
        }


        #region Properties
        ///<summary>
        ///站点ID
        ///</summary>
        public Int32 WebSiteID { get; set; }

        ///<summary>
        ///站点名字
        ///</summary>
        public String WebSiteName { get; set; }

        ///<summary>
        ///站点描述
        ///</summary>
        public String Description { get; set; }

        ///<summary>
        ///站点的URL前辍（即 主机名:端口/应用程序路径）
        ///</summary>
        public String UrlPrefix { get; set; }

        ///<summary>
        ///在管理界面上的显示次序
        ///</summary>
        public Int32 Ordinal { get; set; }


        ///// <summary>
        ///// 包含用于在管理站点和业务站点间交换数据时进行加密的密钥
        ///// </summary>
        //public String Token 
        //{ 
        //    get
        //    {
        //        return ESP.Configuration.ConfigurationManager.WebSiteKey;
        //    }
        //}



        /// <summary>
        /// 创建者
        /// </summary>
        public int Creator { get; set; }


        /// <summary>
        /// 最后修改者
        /// </summary>
        public int LastModifier { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }


        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModifiedTime { get; set; }


        /// <summary>
        /// 创建人用户名
        /// </summary>
        public string CreatorName { get; set; }


        /// <summary>
        /// 最后修改人用户名
        /// </summary>
        public string LastModifierName { get; set; }


        ///<summary>
        ///行版本
        ///</summary>
        public Byte[] RowVersion { get; set; }


        /// <summary>
        /// 统一的框架页面的路径
        /// </summary>
        public String FramePagePath { get; set; }


        /// <summary>
        /// 站点的Http根Url
        /// </summary>
        public string HttpRootUrl
        {
            get { return "http://" + UrlPrefix; }
        }

        /// <summary>
        /// 站点的Https根Url
        /// </summary>
        public string HttpsRootUrl
        {
            get { return "https://" + UrlPrefix; }
        }
        #endregion
    }
}
