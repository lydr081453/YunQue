using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;

public partial class project_SupporterEdit : ESP.Finance.WebPage.EditPageForSupporter
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SupporterInfo supporter = ESP.Finance.BusinessLogic.SupporterManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]));
            TopMessage.ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(supporter.ProjectID);
            if (supporter.Status == (int)Status.FinanceAuditComplete)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('你没有访问该数据的权限！');window.location.href='Default.aspx';", true);
            }
        }
    }

    private int  SaveSupporterInfo()
    {
        int ret = 0;
        SupporterInfo supporter = this.SupporterInfo.GetSupporterToSave();
        
        if (supporter != null)
        {
            IList<SupporterScheduleInfo> listSchedule = ESP.Finance.BusinessLogic.SupporterScheduleManager.GetList(" SupporterID=" + supporter.SupportID.ToString());
            if (listSchedule != null && listSchedule.Count > 0)
            {
                decimal totalSchedule=0;
                foreach (SupporterScheduleInfo schedule in listSchedule)
                {
                    totalSchedule += schedule.Fee == null ? 0 : schedule.Fee.Value;
                }

                if (supporter.IncomeType != "Cost" && totalSchedule != ESP.Finance.BusinessLogic.CheckerManager.GetSupporterOddAmount(supporter.SupportID))
                {
                    return 3;
                }
                UpdateResult result = ESP.Finance.BusinessLogic.SupporterManager.Update(supporter);
                if (result == UpdateResult.Succeed)
                {
                    ret = 1;
                }
                else
                {
                    ret = 0;
                }
            }
            else
            {
                if (supporter.IncomeType != "Cost")
                    ret = 2;
                else
                    ret = 1;
            }
        }
        return ret;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int ret=SaveSupporterInfo();
        switch (ret)
        {
            case 1:
                ESP.Logging.Logger.Add(string.Format("{0}对F_Supporter表中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, Request[RequestName.SupportID], "编辑支持方申请单未设置审批人"), "编辑支持方申请单");
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存成功!');", true);
                if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.BackUrl]))
                Response.Redirect(Request[ESP.Finance.Utility.RequestName.BackUrl]);
                else
                    Response.Redirect("SupporterList.aspx");
                break;
            case 0:
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存时出现未知异常!');", true);
                break;
            case 2:
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('预计各月百分比为必填!');", true);
                break;
            case 3:
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('预计各月百分比填写有误!');", true);
                break;
        }

    }

    protected void btnSaveAndSubmit_Click(object sender, EventArgs e)
    {
        string query = Request.Url.Query;
        int ret = SaveSupporterInfo();
        switch (ret)
        {
            case 1:
                ESP.Logging.Logger.Add(string.Format("{0}对F_Supporter表中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, Request[RequestName.SupportID], "编辑支持方申请单并设置审批人"), "编辑支持方申请单");
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存成功!');", true);
                Response.Redirect("SetSupporterAuditor.aspx" + query);
                break;
            case 0:
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存时出现未知异常!');", true);
                break;
            case 2:
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('预计各月百分比为必填!');", true);
                break;
            case 3:
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('预计各月百分比填写有误!');", true);
                break;
        }
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.BackUrl]))
            Response.Redirect(Request[ESP.Finance.Utility.RequestName.BackUrl]);
        else
            Response.Redirect("SupporterList.aspx");

    }
}