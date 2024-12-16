using System;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;

public partial class include_page_ToHome : System.Web.UI.Page
{
    protected override void OnPreInit(EventArgs e)
    {
        SettingsInfo settings = SettingManager.Get();
        WebSiteInfo portal = WebSiteManager.Get(settings.PortalWebSite);

        Response.Redirect(portal.HttpRootUrl, true);
    }
}