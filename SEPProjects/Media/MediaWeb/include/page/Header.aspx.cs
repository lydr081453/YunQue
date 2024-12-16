using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using ESP.Compatible;
using ESP.Framework.BusinessLogic;
using ESP.Framework.Entity;

namespace FrameSite.Web.include.page
{
	/// <summary>
	/// Header 的摘要说明。
	/// </summary>
	public partial class Header : System.Web.UI.Page
	{
        protected string PortalUrl
        {
            get
            {
                SettingsInfo settings = SettingManager.Get();
                WebSiteInfo portal = WebSiteManager.Get(settings.PortalWebSite);

                return portal.HttpRootUrl;
            }
        }

        protected string SignOutUrl
        {
            get
            {
                return ESP.Security.PassportAuthentication.GetLogoutUrl(ConfigManager.SiteLogonUrl);
            }
        }
	}
}