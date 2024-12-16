using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;

namespace FinanceWeb.Purchase
{
    public partial class FinanceReFund : ESP.Finance.WebPage.EditPageForReturn
    {
        private int returnId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindInfo();
            }
        }

           private string GetAuditLog(int Rid)
        {
            ReturnInfo ReturnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(Rid);
            IList<ESP.Finance.Entity.AuditLogInfo> histList = ESP.Finance.BusinessLogic.AuditLogManager.GetReturnList(ReturnModel.ReturnID);
            string loginfo = string.Empty;
            foreach (ESP.Finance.Entity.AuditLogInfo model in histList)
            {
                string austatus = string.Empty;
                if (model.AuditStatus == (int)AuditHistoryStatus.PassAuditing)
                {
                    austatus = "审批通过";
                }
                else if (model.AuditStatus == (int)AuditHistoryStatus.TerminateAuditing)
                {
                    austatus = "审批驳回";
                }
                string auditdate = model.AuditDate == null ? "" : model.AuditDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                loginfo += model.AuditorEmployeeName + austatus + "[" + auditdate + "]" + model.Suggestion + "<br/>";

            }
            return loginfo;
        }

        private void BindInfo()
        {
            returnId = Convert.ToInt32(Request[RequestName.ReturnID]);
            ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
            PaymentTypeInfo paymentType = null;
            if (returnModel.PaymentTypeID != null)
                paymentType = ESP.Finance.BusinessLogic.PaymentTypeManager.GetModel(returnModel.PaymentTypeID.Value);
            lblApplicant.Text = returnModel.RequestEmployeeName;

            lblApplicant.Attributes["onclick"] = "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(returnModel.RequestorID.Value) + "');";
            lblBeginDate.Text = returnModel.PreBeginDate == null ? "" : returnModel.PreBeginDate.Value.ToString("yyyy-MM-dd");
            // lblEndDate.Text = returnModel.PreEndDate == null ? "" : returnModel.PreEndDate.Value.ToString("yyyy-MM-dd");
            lblInceptDate.Text = returnModel.ReturnPreDate == null ? "" : returnModel.ReturnPreDate.Value.ToString("yyyy-MM-dd");
            lblInceptPrice.Text = returnModel.PreFee == null ? "" : returnModel.PreFee.Value.ToString("#,##0.00");
            lblPeriodType.Text = returnModel.PaymentTypeName;
            lblPayRemark.Text = returnModel.ReturnContent;
            if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.CommonPR || returnModel.ReturnType == null)
                lblPRNo.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + returnModel.PRID.ToString() + "'style='cursor: hand' target='_blank'>" + returnModel.PRNo + "</a>";
            else
                lblPRNo.Text = returnModel.PRNo;
            lblProjectCode.Text = returnModel.ProjectCode;
            lblReturnCode.Text = returnModel.ReturnCode;
            radioInvoice.SelectedValue = returnModel.IsInvoice == null ? "-1" : returnModel.IsInvoice.Value.ToString();
            lblStatus.Text = ESP.Finance.Utility.ReturnPaymentType.ReturnStatusString(returnModel.ReturnStatus.Value, 0, returnModel.IsDiscount);
            this.lblLog.Text = this.GetAuditLog(returnId);
            ESP.Finance.Entity.ReturnGeneralInfoListViewInfo vmodel = ESP.Finance.BusinessLogic.ReturnGeneralInfoListViewManager.GetModel(returnModel.ReturnID);
            if (vmodel != null)
            {
                this.lblSupplierName.Text = vmodel.Account_name;
                this.lblSupplierBank.Text = vmodel.Account_bank;
                this.lblSupplierAccount.Text = vmodel.Account_number;
            }
            this.lblPaymentType.Text = returnModel.PaymentTypeName;
            this.lblAccount.Text = returnModel.BankAccount;
            this.lblAccountName.Text = returnModel.BankAccountName;
            this.lblBankAddress.Text = returnModel.BankAddress;
            this.lblPayCode.Text = returnModel.PaymentTypeCode;
            this.lblFactFee.Text = returnModel.FactFee == null ? "" : returnModel.FactFee.Value.ToString("#,##0.00");
            this.lblFactFee.Text = returnModel.PreFee == null ? "" : returnModel.PreFee.Value.ToString("#,##0.00");
            lblReturnPreDate.Text = Convert.ToDateTime(returnModel.ReturnPreDate).ToString("yyyy-MM-dd");
        }

        protected void btnRefund_Click(object sender, EventArgs e)
        {
            returnId = Convert.ToInt32(Request[RequestName.ReturnID]);
            ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
            
            returnModel.FactFee = returnModel.FactFee - Convert.ToDecimal(txtRefundFee.Text.Trim());
            returnModel.RePaymentSuggestion += this.txtRemark.Text.Trim();

            ESP.Finance.Entity.AuditLogInfo logModel = new AuditLogInfo();
            logModel.AuditDate = DateTime.Now;
            logModel.AuditorEmployeeName = CurrentUser.Name;
            logModel.AuditorSysID = Convert.ToInt32(CurrentUser.SysID);
            logModel.AuditorUserCode = CurrentUser.ID;
            logModel.AuditorUserName = CurrentUser.ITCode;
            logModel.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing;
            logModel.FormID = returnModel.ReturnID;
            logModel.FormType = (int)ESP.Finance.Utility.FormType.Return;
            logModel.Suggestion = "退款：" + this.txtRemark.Text.Trim();

            ESP.Purchase.Entity.OrderInfo orderModel = ESP.Purchase.BusinessLogic.OrderInfoManager.GetModelByGeneralID(returnModel.PRID.Value);
            orderModel.FactTotal = orderModel.total - Convert.ToDecimal(txtRefundFee.Text.Trim());

            ESP.Finance.Utility.UpdateResult result = ESP.Finance.BusinessLogic.ReturnManager.Refund(returnModel,orderModel, logModel);

            if (result == ESP.Finance.Utility.UpdateResult.Succeed)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('该付款申请单退款操作成功!');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('退款操作失败，请重新尝试!');", true);
            }

            if (!string.IsNullOrEmpty(Request["BackUrl"]))
                Response.Redirect(Request["BackUrl"]);
            else
                Response.Redirect("/Search/ReturnTabList.aspx?Type=return");
        }


        protected void btnReturn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["BackUrl"]))
                Response.Redirect(Request["BackUrl"]);
            else
                Response.Redirect("/Search/ReturnTabList.aspx?Type=return");
        }

    }
}