using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using ESP.Framework.BusinessLogic;


namespace FinanceWeb.project
{
    public partial class FactoringEdit : ESP.Web.UI.PageBase
    {

        private ESP.Finance.Entity.ReturnInfo returnModel;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindInfo();
                bindDDL();
            }
        }

        private string GetAuditLog(ESP.Purchase.Entity.GeneralInfo pr, ReturnInfo ReturnModel)
        {
            IList<ESP.Purchase.Entity.AuditLogInfo> oploglist = null;

            if (pr != null)
            {
                if (pr.ValueLevel == 1)
                {
                    oploglist = ESP.Purchase.BusinessLogic.AuditLogManager.GetModelListByGID(pr.id);
                }
            }

            IList<ESP.Finance.Entity.AuditLogInfo> histList = ESP.Finance.BusinessLogic.AuditLogManager.GetReturnList(ReturnModel.ReturnID);

            System.Text.StringBuilder log = new System.Text.StringBuilder();

            if (oploglist != null && oploglist.Count > 0)
            {
                foreach (var l in oploglist)
                {
                    log.AppendFormat("{0:yyyy/MM/dd hh:mm:ss}", l.remarkDate).Append(" ")
                        .Append(l.auditUserName)
                        .Append(ESP.Purchase.Common.State.operationAudit_statusName[l.auditType]).Append(" ")
                        .Append(l.remark).Append("<br/>");
                }
            }

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

                log.AppendFormat("{0:yyyy/MM/dd hh:mm:ss}", l.AuditDate).Append(" ")
                    .Append(l.AuditorEmployeeName).Append("(").Append(l.AuditorUserName).Append(") ")
                    .Append(austatus).Append(" ")
                    .Append(l.Suggestion).Append("<br/>");
            }

            return log.ToString();
        }


        private void BindInfo()
        {
            int returnId;
            if (!int.TryParse(Request[ESP.Finance.Utility.RequestName.ReturnID], out returnId) || returnId <= 0)
                return;

            returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);

            var parentId = returnModel.ParentID ?? 0;
            var parentModel = parentId > 0 ? ReturnManager.GetModel(parentId) : null;
            ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(returnModel.PRID.Value);
            ESP.Purchase.Entity.PaymentPeriodInfo paymentPeriod = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetModelByPN(returnModel.ReturnID);

            if (parentModel != null)
            {
                this.panParent.Visible = true;

                this.lblParentAmount.Text = parentModel.FactFee.Value.ToString("#,##0.00");
                this.lblParentCode.Text = parentModel.ReturnCode;
                this.lblFee.Text = "<font color='red'>退款金额:</font>";
                lblInceptPrice.Text = returnModel.PreFee == null ? "" : "<font color='red'>" + returnModel.PreFee.Value.ToString("#,##0.00") + "</font>";

                if (generalModel != null)
                {
                    this.lblParentPrNo.Text = generalModel.PrNo;
                    this.lblParentPrTotal.Text = generalModel.totalprice.ToString("#,##0.00");
                }
            }
            else
            {
                lblInceptPrice.Text = returnModel.PreFee == null ? "" : returnModel.PreFee.Value.ToString("#,##0.00");
            }

            hidPrID.Value = returnModel.PRID.ToString();
            hidProjectID.Value = returnModel.ProjectID.ToString();

            lblApplicant.Text = returnModel.RequestEmployeeName;
            lblApplicant.Attributes["onclick"] = "javascript:showUserInfoAsync(" + (returnModel.RequestorID ?? 0) + ");";

            lblBeginDate.Text = returnModel.PreBeginDate == null ? "" : returnModel.PreBeginDate.Value.ToString("yyyy-MM-dd");
            lblInceptDate.Text = returnModel.ReturnPreDate == null ? "" : returnModel.ReturnPreDate.Value.ToString("yyyy-MM-dd");

            lblPeriodType.Text = returnModel.PaymentTypeName;
            lblPayRemark.Text = returnModel.ReturnContent;
            lblOtherRemark.Text = returnModel.Remark;

            if (returnModel.ReturnType == null || returnModel.ReturnType.Value == (int)ESP.Purchase.Common.PRTYpe.CommonPR)
                lblPRNo.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + returnModel.PRID + "'style='cursor: hand' target='_blank'>" + returnModel.PRNo + "</a>";
            else
                lblPRNo.Text = returnModel.PRNo;

            lblProjectCode.Text = returnModel.ProjectCode;
            lblReturnCode.Text = returnModel.ReturnCode;

            lblStatus.Text = ESP.Finance.Utility.ReturnPaymentType.ReturnStatusString(returnModel.ReturnStatus.Value, 0, returnModel.IsDiscount);

            this.lblSupplierName.Text = returnModel.SupplierName;
            this.lblSupplierBank.Text = returnModel.SupplierBankName;
            this.lblSupplierAccount.Text = returnModel.SupplierBankAccount;

            this.lblLog.Text = this.GetAuditLog(generalModel, returnModel);

            labDepartment.Text = returnModel.DepartmentName;
        }

        private void bindDDL()
        {
            IList<ESP.Finance.Entity.BankInfo> bankList = ESP.Finance.BusinessLogic.BankManager.GetList(" IsFactoring=1");
            ddlFactoring.DataSource = bankList;
            ddlFactoring.DataTextField = "BankAccountName";
            ddlFactoring.DataValueField = "BankID";
            ddlFactoring.DataBind();

            if (bankList != null && bankList.Count > 0)
            {
                lblFactoringAccount.Text = bankList[0].BankAccount;
                lblFactoringBank.Text = bankList[0].BankName;
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("FactoringList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int returnId;
            if (!int.TryParse(Request[ESP.Finance.Utility.RequestName.ReturnID], out returnId) || returnId <= 0)
                return;

            returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);

            ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(returnModel.PRID.Value);

            generalModel.IsFactoring = int.Parse(ddlFactoring.SelectedValue);

            if (!string.IsNullOrEmpty(txtFactorDate.Text))
            {
                generalModel.FactoringDate = DateTime.Parse(txtFactorDate.Text);
            }

            ESP.Purchase.BusinessLogic.GeneralInfoManager.Update(generalModel);

            Response.Redirect("FactoringList.aspx");
        }

        protected void ddlFactoring_SelectedIndexChanged(object sender, EventArgs e)
        {
            ESP.Finance.Entity.BankInfo bankModel = ESP.Finance.BusinessLogic.BankManager.GetModel(int.Parse(ddlFactoring.SelectedValue));
            lblFactoringAccount.Text = bankModel.BankAccount;
            lblFactoringBank.Text = bankModel.BankName;
        }
    }
}