using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Configuration;
using System.Configuration;
using SEPCfgManager = ESP.Configuration.ConfigurationManager;
using ESP.Framework.DataAccess;
using ESP.Framework.Entity;
using System.Security.Cryptography;
using System.Text;
using ESP.Framework.BusinessLogic;
using ESP.Web.UI;
using ESP.Framework.DataAccess.Utilities;

namespace SEPAdmin
{
    public partial class old_Default : PageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected string WorkSpaceUrl
        {
            get
            {


                string workSpaceUrl = Request.QueryString["contentUrl"];
                if (workSpaceUrl != null)
                {
                    workSpaceUrl = workSpaceUrl.Trim().ToLower();
                    if (workSpaceUrl.Length > 0 && workSpaceUrl[0] == '/')
                        workSpaceUrl = workSpaceUrl.Substring(1);

                    int index = workSpaceUrl.IndexOf('?');
                    if (index == 0)
                    {
                        workSpaceUrl = string.Empty;
                    }
                    else if (index > 0)
                    {
                        workSpaceUrl = workSpaceUrl.Substring(0, index);
                    }

                }
                if (workSpaceUrl == null || workSpaceUrl.Length == 0 || "default.aspx" == workSpaceUrl)
                    return "HR/default.aspx";
                return workSpaceUrl;
            }
        }


    }
}

