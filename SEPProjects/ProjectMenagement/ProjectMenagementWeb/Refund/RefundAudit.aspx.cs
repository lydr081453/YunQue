using ESP.Finance.BusinessLogic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.Refund
{
    public partial class RefundAudit : ESP.Web.UI.PageBase
    {
        private ESP.Purchase.Entity.GeneralInfo generalModel = null;
        private ESP.Finance.Entity.RefundInfo refundModel=null;

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
                refundModel = ESP.Finance.BusinessLogic.RefundManager.GetModel(id);
                this.lblPRNO.Text = refundModel.PRNO;
                generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(refundModel.PRID);

                if (refundModel.Status == (int)ESP.Finance.Utility.PaymentStatus.MajorAudit || refundModel.Status == (int)ESP.Finance.Utility.PaymentStatus.FinanceLevel1 )
                {
                    trNext.Visible = true;
                }
                else
                    trNext.Visible = false;
            }


            if (generalModel != null)
            {
                this.hidPRID.Value = generalModel.id.ToString();
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
            }

            if (refundModel != null)
            {
                this.txtFee.Text = refundModel.Amounts.ToString("#,##0.00");
                this.txtRefundDate.Text = refundModel.RefundDate.ToString("yyyy-MM-dd");
                this.txtRemark.Text = refundModel.Remark;
            }

            this.lblLog.Text = GetAuditLog(refundModel);

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


        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("RefundAuditList.aspx");
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            int id = int.Parse(Request[RequestName.ModelID]);
            refundModel = ESP.Finance.BusinessLogic.RefundManager.GetModel(id);
            int nextFinance = 0;
            if ((refundModel.Status == (int)ESP.Finance.Utility.PaymentStatus.MajorAudit || refundModel.Status == (int)ESP.Finance.Utility.PaymentStatus.FinanceLevel1 ) && string.IsNullOrEmpty(hidNextAuditor.Value))
            {
                ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('请选择下一级审核人!');", true);
                return;
            }
            if (!string.IsNullOrEmpty(hidNextAuditor.Value))
            {
                nextFinance = int.Parse(hidNextAuditor.Value);
            }
            refundModel.Remark = this.txtRemark.Text;
            refundModel.RefundDate = DateTime.Parse( this.txtRefundDate.Text);

            int retvalue = RefundManager.RefundAudit(refundModel, CurrentUser, (int)AuditHistoryStatus.PassAuditing, txtAudit.Text, nextFinance);
            if (retvalue > 0)
            {
                ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('退款申请审批成功!');window.location.href='RefundAuditList.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {

            int id = int.Parse(Request[RequestName.ModelID]);
            refundModel = ESP.Finance.BusinessLogic.RefundManager.GetModel(id);

            int retvalue = RefundManager.RefundAudit(refundModel, CurrentUser, (int)AuditHistoryStatus.TerminateAuditing, txtAudit.Text, 0);
            if (retvalue > 0)
            {
                ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('退款申请驳回成功!');window.location.href='RefundAuditList.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            }
        }

        protected void btnTip_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAudit.Text))
                return;
            if (refundModel == null)
            {
                int id = int.Parse(Request[RequestName.ModelID]);
                refundModel = ESP.Finance.BusinessLogic.RefundManager.GetModel(id);
            }
                ESP.Finance.Entity.AuditLogInfo audit = new ESP.Finance.Entity.AuditLogInfo();
                audit.FormID = refundModel.Id;
                audit.Suggestion = this.txtAudit.Text;
                audit.AuditDate = DateTime.Now;
                audit.AuditorSysID = int.Parse(CurrentUser.SysID);
                audit.AuditorUserCode = CurrentUser.ID;
                audit.AuditorEmployeeName = CurrentUser.Name;
                audit.AuditorUserName = CurrentUser.ITCode;
                audit.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.Tip;
                audit.FormType = (int)ESP.Finance.Utility.FormType.Refund;
                int ret = ESP.Finance.BusinessLogic.AuditLogManager.Add(audit);
                if (ret > 0)
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='RefundAuditList.aspx';alert('留言保存成功！');", true);
            
        }
  
    }
}