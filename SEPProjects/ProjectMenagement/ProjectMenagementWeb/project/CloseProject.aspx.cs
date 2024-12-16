using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
namespace FinanceWeb.project
{
    public partial class CloseProject : ESP.Web.UI.PageBase
    {
        int projectid = 0;
        ESP.Finance.Entity.ProjectInfo projectModel = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
            {
                projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
                projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModel(projectid);
                this.PaymentDisplay.ProjectInfo = projectModel;
                this.PaymentDisplay.InitProjectInfo();
                this.ProjectSupporterDisplay.InitProjectInfo();
                Search();
            }
        }

        protected void gvOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOrder.PageIndex = e.NewPageIndex;
            Search();
        }
        private void Search()
        {
            string term = string.Empty;
            List<System.Data.SqlClient.SqlParameter> parmList = new List<System.Data.SqlClient.SqlParameter>();
            term = " project_id=@projectID";
            System.Data.SqlClient.SqlParameter p = new System.Data.SqlClient.SqlParameter("@projectID", System.Data.SqlDbType.Int, 4);
            p.Value = Request[RequestName.ProjectID];
            parmList.Add(p);
            IList<ESP.Finance.Entity.GetAllPaymentViewInfo> ViewList = ESP.Finance.BusinessLogic.GetAllPayemtnViewManager.GetTotalList(term, parmList);
            this.gvOrder.DataSource = ViewList;
            this.gvOrder.DataBind();
        }
        protected void gvOrder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.GetAllPaymentViewInfo ViewModel = (ESP.Finance.Entity.GetAllPaymentViewInfo)e.Row.DataItem;
                Label lblNo = (Label)e.Row.FindControl("lblNo");
                if (lblNo != null)
                    lblNo.Text = e.Row.RowIndex.ToString() + 1;
                Label lblAmounts = (Label)e.Row.FindControl("lblAmounts");
                if (lblAmounts != null)
                    lblAmounts.Text = ViewModel.totalprice.Value.ToString("#,##0.00");
                Label lblOrderStatus = (Label)e.Row.FindControl("lblOrderStatus");
                lblOrderStatus.Text = ESP.Purchase.Common.State.requistionOrorder_state[ViewModel.status.Value];
                Label lblPaymentStatus = (Label)e.Row.FindControl("lblPaymentStatus");
                if (ViewModel.ReturnStatus != null && ViewModel.ReturnCode != null)
                    lblPaymentStatus.Text = ViewModel.ReturnCode + ":" + ESP.Finance.Utility.ReturnPaymentType.ReturnStatusString(ViewModel.ReturnStatus.Value, 0, null);
                else
                    lblPaymentStatus.Text = "未创建付款";
            }
        }
        protected void btnPreClose_Click(object sender, EventArgs e)
        {
            if (projectModel == null)
            {
                projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[RequestName.ProjectID]));
            }

            string mail = (new ESP.Compatible.Employee(projectModel.ApplicantUserID)).EMail;
            string exMail = string.Empty;
            projectModel.Status = (int)ESP.Finance.Utility.Status.ProjectPreClose;
            UpdateResult result = ESP.Finance.BusinessLogic.ProjectManager.Update(projectModel);
            if (result == UpdateResult.Succeed)
            {
                try
                {
                    SendMailHelper.SendMailPreCloseProject(projectModel, CurrentUserName, mail);
                }
                catch
                {
                    exMail = "(邮件发送失败!)";
                }
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('该项目号已经预关闭完毕！" + exMail + "')", true);
            }

        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            if (projectModel == null)
            {
                projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[RequestName.ProjectID]));
            }
            string term = string.Empty;
            int UnDoCounter = 0;//稿费未处理,对私未处理存在，无法关闭项目号
            term = " Project_Code=@ProjectCode and Status not in(0,-1,2,17,21)";

            List<System.Data.SqlClient.SqlParameter> parmList = new List<System.Data.SqlClient.SqlParameter>();
            term = " project_id=@projectID";
            System.Data.SqlClient.SqlParameter p = new System.Data.SqlClient.SqlParameter("@ProjectCode", System.Data.SqlDbType.NVarChar, 50);
            p.Value = projectModel.ProjectCode;
            parmList.Add(p);
            IList<ESP.Purchase.Entity.GeneralInfo> generalList = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModelList(term, parmList);
            UnDoCounter = generalList.Count;
            term = " ProjectCode=@ProjectCode and Status<>@Status";
            parmList.Clear();
            parmList.Add(p);
            System.Data.SqlClient.SqlParameter p2 = new System.Data.SqlClient.SqlParameter("@Status", System.Data.SqlDbType.Int, 4);
            p2.Value = ESP.Finance.Utility.PaymentStatus.FinanceComplete;
            parmList.Add(p2);

            IList<ESP.Finance.Entity.ReturnInfo> ReturnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(term, parmList);
            UnDoCounter += ReturnList.Count;

            //其他类型单据如果付款申请没有或付款完毕，无法关闭项目号
            //ViewList = ESP.Finance.BusinessLogic.GetAllPayemtnViewManager.GetTotalList(term, parmList);
            if (UnDoCounter > 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('该项目号还有未处理完成的数据，请检查！')", true);
                return;
            }
            else
            {
                projectModel.Status = (int)ESP.Finance.Utility.Status.ProjectClosed;
                string mail = (new ESP.Compatible.Employee(projectModel.ApplicantUserID)).EMail;
                UpdateResult result = ESP.Finance.BusinessLogic.ProjectManager.Update(projectModel);
                if (result == UpdateResult.Succeed)
                {
                    string exMail = string.Empty;
                    try
                    {
                        SendMailHelper.SendMailCloseProject(projectModel, CurrentUserName, mail);
                    }
                    catch
                    {
                        exMail = "(邮件发送失败!)";
                    }
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('该项目号已经关闭完毕！"+exMail+"')", true);
                }
            }
        }
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("CloseProjectList.aspx");
        }
    }
}
