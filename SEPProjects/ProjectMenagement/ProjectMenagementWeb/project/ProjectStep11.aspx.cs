using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

public partial class project_ProjectStep11 : ESP.Finance.WebPage.EditPageForProject
{
    private int projectid = 0;
    string query = string.Empty;
    ESP.Finance.Entity.ProjectInfo projectinfo;

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Server.ScriptTimeout = 600;
        if (!IsPostBack)
        {
            ProjectMember.CurrentUser = CurrentUser;

            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
            {
                projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
                projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModel(projectid);
                if (projectinfo.Status != (int)ESP.Finance.Utility.Status.Saved && projectinfo.Status != (int)ESP.Finance.Utility.Status.BizReject && projectinfo.Status != (int)ESP.Finance.Utility.Status.FinanceReject && projectinfo.Status != (int)ESP.Finance.Utility.Status.ContractReject)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('你没有访问该数据的权限！');window.location.href='Default.aspx';", true);
                }
                this.ProjectMember.ProjectInfo = projectinfo;
            }


        }
        query = Request.Url.Query;
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        this.btnNext.Enabled = false;
        if (projectinfo == null)
        {
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
            {
                projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
                projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModel(projectid);
            }
            if (projectinfo.Members == null || projectinfo.Members.Count == 0)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('请添加项目成员!');", true);
                this.btnNext.Enabled = true;
                return;
            }
            if (projectinfo.Step < 2)
                projectinfo.Step = 2;
            projectinfo.SubmitDate = DateTime.Now;
            UpdateResult result = ESP.Finance.BusinessLogic.ProjectManager.Update(projectinfo);
            if (result == UpdateResult.Succeed)
            {
                ESP.Logging.Logger.Add(string.Format("{0}对F_Project表中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, Request[RequestName.ProjectID], "创建项目号申请单第2步保存并返回列表页"), "创建项目号申请单");
           
                this.btnNext.Enabled = true;
                Response.Redirect("ProjectStep2.aspx" + query);
            }
            else
            {
                this.btnNext.Enabled = true;
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + result.ToString() + "!');", true);
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
    protected void btnPre_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProjectStep1.aspx" + query);
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Edit/ProjectTabEdit.aspx" + query);
    }
}
