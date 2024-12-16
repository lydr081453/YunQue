using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;

namespace FinanceWeb.project
{
    public partial class SupporterAuditEdit : ESP.Finance.WebPage.EditPageForSupporter
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

        private int SaveSupporterInfo()
        {
            int ret = 0;
            SupporterInfo supporter = this.SupporterInfo.GetSupporterToSave();

            if (supporter != null)
            {
                IList<SupporterScheduleInfo> listSchedule = ESP.Finance.BusinessLogic.SupporterScheduleManager.GetList(" SupporterID=" + supporter.SupportID.ToString());
                if ((listSchedule != null && listSchedule.Count > 0) || supporter.IncomeType == "Cost")
                {

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
                        ret = 2;
                }
            }
            return ret;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int ret = SaveSupporterInfo();
            string query = Request.Url.Query;
            switch (ret)
            {
                case 1:
                    ESP.Logging.Logger.Add(string.Format("{0}对F_Supporter表中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, Request[RequestName.SupportID], "编辑支持方申请单未设置审批人"), "编辑支持方申请单");
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存成功!');", true);
                    if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.BackUrl]))
                    {
                        //query.RemoveParam(RequestName.BackUrl);
                        if (!string.IsNullOrEmpty(Request[RequestName.Operate]))
                        {
                            switch (Request[RequestName.Operate])
                            {
                                case "FinancialAudit":
                                    Response.Redirect("/project/FinancialSupporter.aspx" + Request.Url.Query);
                                    break;
                                case "": break;
                            }
                            
                        }
                    }
                    break;
                case 0:
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存时出现未知异常!');", true);
                    break;
                case 2:
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('预计个月百分比为必填!');", true);
                    break;
            }

        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            string query = Request.Url.Query;
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.BackUrl]))
            {
               // query.RemoveParam(RequestName.BackUrl);
                if (!string.IsNullOrEmpty(Request[RequestName.Operate]))
                {
                    switch (Request[RequestName.Operate])
                    {
                        case "FinancialAudit":
                            Response.Redirect("/project/FinancialSupporter.aspx" + Request.Url.Query);
                            break;
                        case "": break;
                    }

                }
            }
        }
    }
}
