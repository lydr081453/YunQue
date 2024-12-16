using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Compatible;
using ESP.Finance.Utility;
public partial class project_SetAuditor : ESP.Web.UI.PageBase
{
    ESP.Finance.Entity.ProjectInfo projectModel;

    protected void Page_Load(object sender, EventArgs e)
    {
            projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(int.Parse(Request[ESP.Finance.Utility.RequestName.ProjectID]));
            this.SetAuditor.CurrentUser = this.CurrentUser;
            this.SetAuditor.DeptID = Convert.ToInt32(projectModel.GroupID);
               
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {

    }
    protected void btnTerminate_Click(object sender, EventArgs e)
    {

    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProjectList.aspx");
    }
    protected void btnEdite_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProjectEdit.aspx?"+RequestName.ProjectID+"="+Request[ESP.Finance.Utility.RequestName.ProjectID]);
    }
}
