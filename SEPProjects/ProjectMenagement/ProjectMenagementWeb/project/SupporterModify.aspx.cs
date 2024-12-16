using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Utility;

public partial class project_SupporterModify : ESP.Finance.WebPage.EditPageForSupporter
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProjectInfo project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
            TopMessage.ProjectModel = project;
        }
    }

    private int SaveSupporterInfo()
    {
        int ret = 0;
        SupporterInfo supporter = this.SupporterInfo.GetSupporterToSave();
        if (supporter != null)
        {
            IList<SupporterScheduleInfo> listSchedule = ESP.Finance.BusinessLogic.SupporterScheduleManager.GetList(" SupporterID=" + supporter.SupportID.ToString());
            if (listSchedule != null && listSchedule.Count > 0)
            {
                decimal totalSchedule = 0;
                foreach (SupporterScheduleInfo schedule in listSchedule)
                {
                    totalSchedule += schedule.Fee == null ? 0 : schedule.Fee.Value;
                }
                if (supporter.IncomeType != "Cost" && totalSchedule != ESP.Finance.BusinessLogic.CheckerManager.GetSupporterOddAmount(supporter.SupportID))
                {
                    return 4;
                }
                UpdateResult result = ESP.Finance.BusinessLogic.SupporterManager.Update(supporter);
                if (result == UpdateResult.Succeed)
                {
                    if (supporter.Status == (int)ESP.Finance.Utility.Status.Saved)
                    { ret = 3; }
                    else
                        ret = 1;
                }
                else
                {
                    ret = 0;
                }
            }
            else
            {
                if (supporter.IncomeType == "Cost")
                    ret = 1;
                else
                    ret = 2;
            }
        }
        return ret;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int ret = SaveSupporterInfo();
        switch (ret)
        {
            case 1:
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存成功!');", true);
                Response.Redirect(Request["BackURL"]);
                break;
            case 0:
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存时出现未知异常!');", true);
                break;
            case 2:
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('预计各月百分比为必填!');", true);
                break;
            case 3:
                string query = Request.Url.Query;
                Response.Redirect("SetSupporterAuditor.aspx" + query);
                break;
            case 4:
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('预计各月百分比填写有误!');", true);
                break;
        }

    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[RequestName.BackUrl]))
        {
            Response.Redirect(Request[RequestName.BackUrl]);
        }
        else
            Response.Redirect("SupporterModifyList.aspx");
    }
}