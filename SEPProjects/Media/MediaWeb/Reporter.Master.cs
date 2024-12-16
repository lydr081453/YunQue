using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;

namespace MediaWeb
{
    public partial class Reporter : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                litUser.Text = ESP.Framework.BusinessLogic.UserManager.Get().FullNameCN;
            }
        }

        protected string PortalUrl
        {
            get
            {
                SettingsInfo settings = SettingManager.Get();
                WebSiteInfo portal = WebSiteManager.Get(settings.PortalWebSite);

                return portal.HttpRootUrl;
            }
        }
    }
}
