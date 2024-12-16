using ESP.Finance.BusinessLogic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using ESP.Purchase.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.Refund
{
    public partial class RefundView : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindData();
            }
        }

        private void bindData()
        {
            if (!string.IsNullOrEmpty(Request[RequestName.ModelID]))
            {
                int id = int.Parse(Request[RequestName.ModelID]);
                RefundInfo refundModel = ESP.Finance.BusinessLogic.RefundManager.GetModel(id);
                this.lblPRNO.Text = refundModel.PRNO;
                GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(refundModel.PRID);

                this.lblProjectCode.Text = generalModel.project_code;
                this.lblApplicant.Text = generalModel.requestorname;
                this.lblTotalprice.Text = generalModel.totalprice.ToString("#,##0.00");
                this.lblSupplierName.Text = generalModel.account_name;
                this.lblBank.Text = generalModel.account_bank;
                this.lblAccount.Text = generalModel.account_number;

                var returnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(" prid= " + generalModel.id.ToString());
                this.gvG.DataSource = returnList;
                this.gvG.DataBind();

                this.lblCost.Text = generalModel.thirdParty_materielDesc;


                this.lblRefund.Text = refundModel.Amounts.ToString("#,##0.00");
                this.lblRefundDate.Text = refundModel.RefundDate.ToString("yyyy-MM-dd");
                this.lblDesc.Text = refundModel.Remark;


                this.lblLog.Text = GetAuditLog(refundModel);

            }
        }

        
        private string GetAuditLog(RefundInfo refundModel)
        {
            IList<ESP.Finance.Entity.AuditLogInfo> histList = ESP.Finance.BusinessLogic.AuditLogManager.GetRefundList(refundModel.Id);

            System.Text.StringBuilder log = new System.Text.StringBuilder();

            foreach (var l in histList)
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
                else if (l.AuditStatus == (int)AuditHistoryStatus.Tip)
                {
                    austatus = "留言";
                }

                log.AppendFormat("{0:yyyy/MM/dd hh:mm:ss}", l.AuditDate).Append(" ")
                    .Append(l.AuditorEmployeeName).Append("(").Append(l.AuditorUserName).Append(") ")
                    .Append(austatus).Append(" ")
                    .Append(l.Suggestion).Append("<br/>");
            }

            return log.ToString();
        }


        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            ESP.Finance.Entity.ReturnInfo returnModel = (ESP.Finance.Entity.ReturnInfo)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                Label lblAmounts = (Label)e.Row.FindControl("lblAmounts");

                lblStatus.Text = ReturnPaymentType.ReturnStatusString(returnModel.ReturnStatus ?? 0, 0, returnModel.IsDiscount);
                lblAmounts.Text = returnModel.FactFee == null ? returnModel.PreFee.Value.ToString("#,##0.00") : returnModel.FactFee.Value.ToString("#,##0.00");

            }
        }
    }
}