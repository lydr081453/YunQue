using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Compatible;
using ESP.Finance.Utility;

public partial class project_ProjectStep1 : ESP.Finance.WebPage.EditPageForProject
{
    int projectid = 0;
    string query = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CurrentUser.DimissionStatus == ESP.HumanResource.Common.Status.DimissionReceiving || CurrentUser.DimissionStatus == ESP.HumanResource.Common.Status.DimissionFinanceAudit)
        {
            Response.Redirect("/Edit/ProjectTabEdit.aspx");
        }
        this.PrepareInfo.CurrentUser = this.CurrentUser;
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
            {
                projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
                ESP.Finance.Entity.ProjectInfo p = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
                if (p.Status != (int)ESP.Finance.Utility.Status.Saved && p.Status != (int)ESP.Finance.Utility.Status.BizReject && p.Status != (int)ESP.Finance.Utility.Status.FinanceReject && p.Status!=(int)ESP.Finance.Utility.Status.ContractReject)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('你没有访问该数据的权限！');window.location.href='Default.aspx';", true);
                }
            }
            query = Request.Url.Query;
        }
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        projectid = SavePrepareInfo();
        if (projectid >0)
        {
            ESP.Logging.Logger.Add(string.Format("{0}对F_Project表中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, projectid.ToString(), "创建项目号申请单第一步保存并返回列表页"), "创建项目号申请单");
            query = query.AddParam(RequestName.ProjectID, projectid);
            Response.Redirect("ProjectStep11.aspx?" +query);
        }
        else if (projectid == -1)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('项目名称或项目号重复!');", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据保存失败!');", true);
        }
        this.btnNext.Enabled = true;
    }
    private int SavePrepareInfo()
    {
       // int descid = 0;
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
        {
            projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
            this.PrepareInfo.ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
       
        }
        this.PrepareInfo.setProjectModel();
        this.PrepareInfo.ProjectModel.CreateDate = DateTime.Now;
        this.PrepareInfo.ProjectModel.CreatorID = int.Parse(CurrentUser.SysID);
        this.PrepareInfo.ProjectModel.CreatorUserID = CurrentUser.ID;
        this.PrepareInfo.ProjectModel.CreatorName = CurrentUserName;
        this.PrepareInfo.ProjectModel.CreatorCode = CurrentUser.ITCode;
        this.PrepareInfo.ProjectModel.Step = 1;
        this.PrepareInfo.ProjectModel.SubmitDate = DateTime.Now;
        if (projectid <= 0)
        {
            int ret = ESP.Finance.BusinessLogic.ProjectManager.Add(this.PrepareInfo.ProjectModel);
            return ret;
        }
        else
        {
            UpdateResult result = ESP.Finance.BusinessLogic.ProjectManager.Update(this.PrepareInfo.ProjectModel);
            if(result==UpdateResult.Succeed)
            {
               return projectid;
            }
            else if (result == UpdateResult.Iterative)
            {
                return -1;
            }
            else
            {
                return -2;
            }
               
        }

    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Edit/ProjectTabEdit.aspx" + query);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        projectid=SavePrepareInfo();
        if (projectid>0)
        {
            string str = string.Format("window.location=window.location.pathname+'?{0}={1}';alert('数据保存成功!');", RequestName.ProjectID, projectid);
            ClientScript.RegisterStartupScript(typeof(string), "", str, true);
        }
        else if (projectid == -1)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('项目名称或项目号重复!');", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据保存失败!');", true);
        }
    }
}
