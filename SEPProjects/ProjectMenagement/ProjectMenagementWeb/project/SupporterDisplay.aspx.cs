using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class project_SupporterDisplay : ESP.Finance.WebPage.ViewPageForSupporter
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if(!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
            {
                TopMessage.ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(int.Parse(Request[ESP.Finance.Utility.RequestName.ProjectID]));
            }
        }
    }

}
