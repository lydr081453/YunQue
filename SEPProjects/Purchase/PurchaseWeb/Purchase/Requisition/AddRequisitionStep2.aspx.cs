using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ESP.Purchase.Common;

public partial class Purchase_Requisition_AddRequisitionStep2 : ESP.Purchase.WebPage.EditPageForPR
{
    int generalid = 0;
    int projectid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CurrentUser.DimissionStatus == ESP.HumanResource.Common.Status.DimissionReceiving || CurrentUser.DimissionStatus == ESP.HumanResource.Common.Status.DimissionFinanceAudit)
        {
            Response.Redirect("/Purchase/Requisition/RequisitionSaveList.aspx");
        }
        if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
        {
            generalid = int.Parse(Request[RequestName.GeneralID].ToString());
        }
        if (!string.IsNullOrEmpty(Request[RequestName.ProjectID]))
        {
            projectid = Convert.ToInt32(Request[RequestName.ProjectID]);
        }
        if (!IsPostBack)
        {
            BindInfo();
        }
    }

    public void BindInfo()
    {

        ESP.Purchase.Entity.GeneralInfo model = null;
        if (generalid > 0)
            model = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(generalid);

        projectInfo.Model = model;
        projectInfo.CurrentUser = CurrentUser;
        projectInfo.BindInfo();
    }

    public int SaveInfo()
    {
        ESP.Purchase.Entity.GeneralInfo g = new ESP.Purchase.Entity.GeneralInfo();
        if (generalid > 0)
            g = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(generalid);
        int oldProjectId = g.Project_id;
        projectInfo.CurrentUser = CurrentUser;
        projectInfo.Model = g;
        g = projectInfo.setModelInfo();
        int newProjectId = g.Project_id;
        ESP.Purchase.Entity.GeneralInfo newModel = new ESP.Purchase.Entity.GeneralInfo();
        projectInfo.Model = newModel;
        newModel = projectInfo.setModelInfo();

        g.status = State.requisition_save;
        g.Addstatus = 2;

        IList<ESP.Finance.Entity.ProjectInfo> projectList = ESP.Finance.BusinessLogic.ProjectManager.GetList(" and projectcode='" + g.project_code+"'");

        if (g.Project_id == 0)
        {
            g.oldFlag = true;
            if (projectList != null && projectList.Count > 0)
            {
                return -2;
            }
        }
        else
        {
            if (projectList != null && projectList.Count > 0)
            {
                if (projectList[0].ProjectId != g.Project_id)
                    return -2;
                else
                {
                    g.oldFlag = projectList[0].OldFlag;
                }
            }
            else
            {
                g.Project_id = 0;
                g.oldFlag = true;
            }
         
        }
        try
        {
            if (generalid == 0)
            {
                if (g.Project_id == 0 && g.project_code.Substring(2, 3).ToUpper() != "GM*" && (g.project_code.Substring(8, 4) == "0903" || g.project_code.Substring(8, 4) == "0904" || g.project_code.Substring(8, 4) == "0905" || g.project_code.Substring(8, 4) == "0906"))
                {
                    return -1;
                }
               

                generalid = ESP.Purchase.BusinessLogic.GeneralInfoManager.Add(g, CurrentUserID, CurrentUserName);
                return generalid;
            }
            else
            {
                if (oldProjectId != 0 && newProjectId != 0 && (oldProjectId != newProjectId))
                {

                    if (!ESP.Purchase.BusinessLogic.GeneralInfoManager.ClearAllByGeneralId(generalid))
                        throw new Exception();

                    newModel.id = g.id;
                    newModel.PrNo = g.PrNo;
                    newModel.app_date = g.app_date;
                    newModel.lasttime = g.lasttime;
                    newModel.InUse = g.InUse;
                    g = newModel;
                }
                ESP.Purchase.BusinessLogic.GeneralInfoManager.Update(g);
                return 1;
            }
        }
        catch (Exception e)
        {
            ESP.Logging.Logger.Add("创建PR申请", "", ESP.Logging.LogLevel.Error, e);
            return 0;
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        int result = SaveInfo();
        if (result > 0)
        {
            //记录操作日志
           // ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, generalid.ToString(), "创建采购申请单第一步跳转第二步"), "创建采购申请单");

            string query = Request.Url.Query;
            query = query.AddParam(RequestName.GeneralID, generalid);
            Response.Redirect("AddRequisitionStep3.aspx?" + query);
        }
        else if (result == -1)
        {

            ClientScript.RegisterStartupScript(typeof(string), "", "alert('2009年3月（含）以后申请PR单，请选择项目号，勿填写!');", true);
        }
        else if (result == -2)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('财务系统中已存在该项目号，请从系统中选择!');", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据提交失败!');", true);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int result = SaveInfo();
        if (result > 0)
        {
            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, generalid.ToString(), "创建采购申请单第一步保存并返回列表页"), "创建采购申请单");

            btnSave.Disabled = true;
            btnNext.Disabled = true;
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据保存成功!');window.location='RequisitionSaveList.aspx'", true);
        }
        else
        {
            if (result == -1)
            {

                ClientScript.RegisterStartupScript(typeof(string), "", "alert('2009年3月（含）以后申请PR单，请选择项目号，勿填写!');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据保存失败!');", true);
            }
        }

    }
}
