using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ESP.Finance.BusinessLogic;
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
