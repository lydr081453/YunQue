using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

public partial class project_ProjectStep5 : ESP.Finance.WebPage.EditPageForProject
{
    string query = string.Empty;
    int projectid=0;
    ESP.Finance.Entity.ProjectInfo projectinfo;
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Server.ScriptTimeout = 600;
        query = Request.Url.Query;
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
            {
                projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
                projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
                if (projectinfo.Status != (int)ESP.Finance.Utility.Status.Saved && projectinfo.Status != (int)ESP.Finance.Utility.Status.BizReject && projectinfo.Status != (int)ESP.Finance.Utility.Status.FinanceReject && projectinfo.Status != (int)ESP.Finance.Utility.Status.ContractReject)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('你没有访问该数据的权限！');window.location.href='Default.aspx';", true);
                }
            }
        }
    }
    protected void btnPre_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProjectStep4.aspx" + query);
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Edit/ProjectTabEdit.aspx" + query);
    }
    private int SaveProjectInfo()
    {
        if (projectinfo == null)
        {
            projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
            projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
        }
        if (projectinfo.Step < 6)
        projectinfo.Step = 6;
        projectinfo.SubmitDate = DateTime.Now;
        UpdateResult result = ESP.Finance.BusinessLogic.ProjectManager.Update(projectinfo);
        if (result == UpdateResult.Succeed)
        {
            return 1;
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + result.ToString() + "');", true);
            return -1;
        }
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        if (SaveProjectInfo() == 1)
        {
            ESP.Logging.Logger.Add(string.Format("{0}对F_Project表中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, Request[RequestName.ProjectID], "创建项目号申请单第6步保存并返回列表页"), "创建项目号申请单");
           
            Response.Redirect("ProjectStep5.aspx" + query);
        }
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        this.btnNext.Enabled = false;
        if (SaveProjectInfo() == 1)
        { Response.Redirect("ProjectStep21.aspx" + query); }
        this.btnNext.Enabled = true;
    }
}
