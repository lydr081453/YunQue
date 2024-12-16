using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.Entity;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;
using System.Text;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Portal.WebSite
{
    public partial class Default2 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }


        /// <summary>
        ///  //跳过https证书认证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {

            return true;

        }

        /// <summary>
        /// 退出URL
        /// </summary>
        protected string LogoutUrl
        {
            get
            {
                return ESP.Security.PassportAuthentication.GetLogoutUrl();
            }
        }

        /// <summary>
        /// 网站系统信息
        /// </summary>
        protected IList<WebSiteInfo> WebSiteInfoList
        {
            get
            {
                IList<WebSiteInfo> list = ESP.Framework.BusinessLogic.WebSiteManager.GetByUser(ESP.Framework.BusinessLogic.UserManager.GetCurrentUserID());

                return list;
            }
        }

        /// <summary>
        /// 当前用户名称
        /// </summary>
        protected string UserName
        {
            get
            {
                return ESP.Framework.BusinessLogic.UserManager.Get().Username;
            }
        }

        protected string CurrentStyleSheet
        {
            get
            {
                return "<link href=\"" + Css + "\" media=\"screen, projection\" rel=\"Stylesheet\" type=\"text/css\" />";
            }
        }

        protected string Css
        {
            get
            {
                if (Request.Url.PathAndQuery.IndexOf("account/", StringComparison.CurrentCultureIgnoreCase) > 0)
                {
                    return "/css/a2.css?" + DateTime.Now.Ticks;
                }
                else
                {
                    return "";
                }
            }
        }

    }
}
