using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;

public partial class project_ProjectStep4 : ESP.Finance.WebPage.EditPageForProject
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
    private int saveProjectInfo()
    {
        if (projectinfo == null)
        {
            projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
            projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
        }
        if (projectinfo.Step<6)
        projectinfo.Step = 6;
        projectinfo.SubmitDate = DateTime.Now;
        UpdateResult result = ESP.Finance.BusinessLogic.ProjectManager.Update(projectinfo);
        if (result == UpdateResult.Succeed)
        {
            return 1;
        }
        else {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + result.ToString() + "');", true);
            return -1;
        }
    }
    protected void btnPre_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProjectStep3.aspx"+query);
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Edit/ProjectTabEdit.aspx" + query);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        string term = "projectid=@projectid";
        List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
        paramlist.Add(new System.Data.SqlClient.SqlParameter("@projectid", Request[ESP.Finance.Utility.RequestName.ProjectID]));
        //IList<ProjectExpenseInfo> costlist =ESP.Finance.BusinessLogic.ProjectExpenseManager.GetList(term, paramlist);
        //if (costlist.Count == 0)
        //{
        //    ClientScript.RegisterStartupScript(typeof(string), "", "alert('必须填写OOP成本.');", true);
        //}
        //else
        //{
            if (saveProjectInfo() == 1)
                Response.Redirect("ProjectStep5.aspx" + query);
        //}
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (saveProjectInfo() == 1)
        {
            ESP.Logging.Logger.Add(string.Format("{0}对F_Project表中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, Request[RequestName.ProjectID], "创建项目号申请单第6步保存并返回列表页"), "创建项目号申请单");
           
            Response.Redirect("ProjectStep4.aspx" + query);
        }
    }
}
