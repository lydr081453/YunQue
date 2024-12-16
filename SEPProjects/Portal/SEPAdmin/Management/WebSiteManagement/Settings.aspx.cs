using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Web.UI;
using ESP.Framework.BusinessLogic;
using SEPConfigurationManager = ESP.Configuration.ConfigurationManager;
using ESP.Framework.Entity;

namespace SEPAdmin.Management.WebSiteManagement
{
    public partial class Settings : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IList<SettingInfo> settings = SettingManager.GetSettings(SEPConfigurationManager.WebSiteID);

            gvSettings.DataSource = settings;
            gvSettings.DataBind();
        }
    }
}
