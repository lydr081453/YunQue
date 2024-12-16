using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.ExpenseAccount
{
    public partial class TicketConfirm :ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindInfo();
        }

        private void BindInfo()
        {
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                int returnId = Convert.ToInt32(Request["id"]);
                ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
                this.lblReturnCode.Text = returnModel.ReturnCode;
                if (returnModel.TicketNo != 0)
                    this.lblReturnCode.Text += "-" + returnModel.TicketNo.ToString();
                this.labPreFee.Text = returnModel.PreFee.Value.ToString("#,##0.00");
                this.labRequestDate.Text = returnModel.RequestDate == null ? "" : returnModel.RequestDate.Value.ToString("yyyy-MM-dd");
                this.labRequestUserCode.Text = returnModel.RequestUserCode;
                this.labRequestUserName.Text = returnModel.RequestEmployeeName;
                this.labSuggestion.Text = "";
                this.lblProjectCode.Text = returnModel.ProjectCode;
                this.lblProjectDesc.Text = returnModel.ReturnContent;

                List<ESP.Finance.Entity.ExpenseAuditDetailInfo> auditLogs = ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.GetList(" and ExpenseAuditID = " + returnModel.ReturnID);
                foreach (ESP.Finance.Entity.ExpenseAuditDetailInfo log in auditLogs)
                {
                    ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(log.AuditorUserID.Value);
                    if (emp != null)
                        labSuggestion.Text += log.AuditorEmployeeName + "(" + emp.FullNameEN + ")" + ";&nbsp;&nbsp;&nbsp;" + log.Suggestion + ";&nbsp;&nbsp;&nbsp;" + ((returnModel.RequestorID.Value == log.AuditorUserID.Value && log.AuditType.Value == 0) ? "提交" : ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Names[log.AuditeStatus.Value]) + ";&nbsp;&nbsp;&nbsp;" + log.AuditeDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>";
                }

                List<ESP.Finance.Entity.ExpenseAccountDetailInfo> list = null;
                list = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(" and ReturnID = " + returnModel.ReturnID.ToString());
                list = list.OrderByDescending(N => N.CreateTime).ToList();
                gvG.DataSource = list;
                gvG.DataBind();
            }
        }

        protected void btnAuditConfirm_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(Request["id"]))
            {
                int returnId = Convert.ToInt32(Request["id"]);
                ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);

                returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.Ticket_Received;
                returnModel.PreFee = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(returnModel.ReturnID);
                returnModel.FactFee = returnModel.PreFee;
                ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);

                ESP.Finance.Entity.ExpenseAuditDetailInfo log = new ESP.Finance.Entity.ExpenseAuditDetailInfo();
                log.AuditeDate = DateTime.Now;
                log.AuditorEmployeeName = CurrentUser.Name;
                log.AuditorUserID = int.Parse(CurrentUser.SysID);
                log.AuditorUserCode = CurrentUser.ID;
                log.AuditorUserName = CurrentUser.ITCode;
                log.AuditeStatus = (int)ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Passed;
                log.ExpenseAuditID = returnModel.ReturnID;
                log.Suggestion = "确认已使用机票";
                ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.Add(log);
                Response.Redirect("/Edit/TicketReceipient.aspx");
            }
        }

        protected void btnAuditReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Edit/TicketReceipient.aspx");
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }

    }
}
