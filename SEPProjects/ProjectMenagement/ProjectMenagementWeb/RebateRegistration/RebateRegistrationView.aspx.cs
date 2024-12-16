using ESP.Finance.BusinessLogic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.RebateRegistration
{
    public partial class RebateRegistrationView : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindList();
            }
        }

        private void bindList()
        {
            if (!string.IsNullOrEmpty(Request["batchId"]))
            {
                int batchId = int.Parse(Request["batchId"]);
                ESP.Finance.Entity.PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchId);
                var RebateRegistrationList = ESP.Finance.BusinessLogic.RebateRegistrationManager.GetList(" a.batchid=" + batchId);

                this.lblAmount.Text = batchModel.Amounts.Value.ToString("#,##0.00");
                this.lblBatchCode.Text = batchModel.PurchaseBatchCode;
                this.lblCreator.Text = batchModel.Creator;
                this.lblDate.Text = batchModel.CreateDate.Value.ToString("yyyy-MM-dd");
                this.lblDesc.Text = batchModel.Description;
                this.lblLog.Text = GetAuditLog(batchId);
                this.linkProve.NavigateUrl = "/Dialogs/ContractFileDownLoad.aspx?ContractID=" + batchModel.BatchID.ToString() + "&RebateRegistration=1";
                this.gvImp.DataSource = RebateRegistrationList;
                this.gvImp.DataBind();
            }
        }

        private string GetAuditLog(int batchId)
        {
            System.Text.StringBuilder log = new System.Text.StringBuilder();
            IList<ESP.Finance.Entity.AuditLogInfo> auditList = ESP.Finance.BusinessLogic.AuditLogManager.GetRebateRegistrationList(batchId);

            foreach (var l in auditList)
            {
                string austatus = string.Empty;
                if (l.AuditStatus == (int)AuditHistoryStatus.PassAuditing)
                {
                    austatus = "审批通过";
                }
                else if (l.AuditStatus == (int)AuditHistoryStatus.TerminateAuditing)
                {
                    austatus = "审批驳回";
                }

                log.AppendFormat("{0:yyyy/MM/dd hh:mm:ss}", l.AuditDate).Append(" ")
                    .Append(l.AuditorEmployeeName).Append("(").Append(l.AuditorUserName).Append(") ")
                    .Append(austatus).Append(" ")
                    .Append(l.Suggestion).Append("<br/>");
            }

            return log.ToString();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("RebateRegistrationList.aspx?Type=rr");
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            int batchId = int.Parse(Request["batchId"]);
            PNBatchManager.ExportRebateRegistration(batchId, Response);
        }

    }
}