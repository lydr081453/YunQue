using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

public partial class project_ProjectCodeChanged : ESP.Finance.WebPage.ViewPageForProject
{
    string query = string.Empty;
    int projectid = 0;
    ESP.Finance.Entity.ProjectInfo projectinfo;
    protected void Page_Load(object sender, EventArgs e)
    {
        query = Request.Url.Query;
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
        {
            projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
        }
        if (!IsPostBack)
        {
            if (projectid > 0)
            {
                projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModel(projectid);
                PaymentDisplay.ProjectInfo = projectinfo;
                PaymentDisplay.InitProjectInfo();

                ProjectSupporterDisplay.InitProjectInfo();
            }
        }
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.BackUrl]))
        {
            Response.Redirect(Request[ESP.Finance.Utility.RequestName.BackUrl]);
        }
        else
            Response.Redirect("ProjectCodeChangedList.aspx");
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        if(projectinfo == null)
            projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
        string oldProjectCode = projectinfo.ProjectCode;
        projectinfo.ProjectCode = txtProjectCode.Text.Trim();
        if (ESP.Finance.BusinessLogic.ProjectManager.ProjectCodeChanged(projectinfo, oldProjectCode))
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('项目号变更成功！');window.location.href='ProjectCodeChangedList.aspx';", true);
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('项目号变更失败！');", true);
        }
    }
}
