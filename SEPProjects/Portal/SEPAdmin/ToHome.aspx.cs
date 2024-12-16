using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.BusinessLogic;
using ESP.Framework.Entity;

namespace SEPAdmin
{
    public partial class ToHome : ESP.Web.UI.PageBase
    {
        protected override void OnPreInit(EventArgs e)
        {
            SettingsInfo settings = SettingManager.Get();
            WebSiteInfo portal = WebSiteManager.Get(settings.PortalWebSite);

            Response.Redirect(portal.HttpRootUrl, true);
        }
    }
}
