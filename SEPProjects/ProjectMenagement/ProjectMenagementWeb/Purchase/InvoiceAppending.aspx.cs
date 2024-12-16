using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using ESP.Finance.Entity;
namespace FinanceWeb.Purchase
{
    public partial class InvoiceAppending : System.Web.UI.Page
    {
        int returnId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ReturnID]))
                {
                    returnId = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]);
                }
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
                loginfo += model.AuditorEmployeeName + "(" + model.AuditorUserName + ")" + austatus + "[" + auditdate + "]" + model.Suggestion + "<br/>";

            }
            return loginfo;
        }
        private void BindInfo()
        {
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);

            if (returnModel != null && returnModel.PRID != null)
                TopMessage.Model = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(returnModel.PRID.Value);
            lblApplicant.Text = returnModel.RequestEmployeeName;
            lblApplicant.Attributes["onclick"] = "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(returnModel.RequestorID.Value) + "');";
            lblBeginDate.Text = returnModel.PreBeginDate == null ? "" : returnModel.PreBeginDate.Value.ToString("yyyy-MM-dd");
            // lblEndDate.Text = returnModel.PreEndDate == null ? "" : returnModel.PreEndDate.Value.ToString("yyyy-MM-dd");
            lblInceptDate.Text = returnModel.ReturnPreDate == null ? "" : returnModel.ReturnPreDate.Value.ToString("yyyy-MM-dd");
            lblInceptPrice.Text = returnModel.PreFee == null ? "" : returnModel.PreFee.Value.ToString("#,##0.00");
            lblPeriodType.Text = returnModel.PaymentTypeName;
            lblPayRemark.Text = returnModel.ReturnContent;
            lblPRNo.Text = returnModel.PRNo;
            lblProjectCode.Text = returnModel.ProjectCode;
            lblReturnCode.Text = returnModel.ReturnCode;
            lblPayCode.Text = returnModel.PaymentTypeCode;

            lblStatus.Text = ESP.Finance.Utility.ReturnPaymentType.ReturnStatusString(returnModel.ReturnStatus.Value, 0, returnModel.IsDiscount);
            if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.CommonPR || returnModel.ReturnType == null)
                lblPRNo.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + returnModel.PRID.ToString() + "'style='cursor: hand' target='_blank'>" + returnModel.PRNo + "</a>";
            else
                lblPRNo.Text = returnModel.PRNo;
            ESP.Finance.Entity.ReturnGeneralInfoListViewInfo vmodel = ESP.Finance.BusinessLogic.ReturnGeneralInfoListViewManager.GetModel(returnModel.ReturnID);
            //从重汇列表获取供应商信息
            IList<ESP.Finance.Entity.BankCancelInfo> cancelList = ESP.Finance.BusinessLogic.BankCancelManager.GetList(" ReturnID=" + returnModel.ReturnID.ToString() + " and (ordertype is null or ordertype=1 )");
            if (cancelList != null && cancelList.Count > 0)
            {
                this.lblSupplierName.Text = cancelList[cancelList.Count - 1].OldBankAccountName;
                this.lblSupplierBank.Text = cancelList[cancelList.Count - 1].NewBankName;
                this.lblSupplierAccount.Text = cancelList[cancelList.Count - 1].NewBankAccount;
            }
            else if (vmodel != null)
            {
                this.lblSupplierName.Text = vmodel.Account_name;
                this.lblSupplierBank.Text = vmodel.Account_bank;
                this.lblSupplierAccount.Text = vmodel.Account_number;
            }
            lblFactFee.Text = returnModel.FactFee == null ? "" : returnModel.FactFee.Value.ToString("#,##0.00");
            lblPreDate.Text = returnModel.ReturnPreDate == null ? "" : returnModel.ReturnPreDate.Value.ToString("yyyy-MM-dd");
            lblBankName.Text = returnModel.BankName;
            lblAccountName.Text = returnModel.BankAccountName;
            lblAccount.Text = returnModel.BankAccount;
            lblBankAddress.Text = returnModel.BankAddress;
            lblPaymentType.Text = returnModel.PaymentTypeName;
            this.lblLog.Text = this.GetAuditLog(returnModel.ReturnID);
            labDepartment.Text = returnModel.DepartmentName;

            //发票接收人
            ESP.Finance.Entity.ReturnInvoiceInfo invoiceModel = ESP.Finance.BusinessLogic.ReturnInvoiceManager.GetModelByReturnID(returnId);
        
            if (invoiceModel == null || invoiceModel.Status == 0)
            {
                ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(returnModel.ProjectCode.Substring(0, 1));
                int FirstFinanceID = branchModel.FirstFinanceID;

                ESP.Finance.Entity.BranchDeptInfo branchdept = ESP.Finance.BusinessLogic.BranchDeptManager.GetModel(branchModel.BranchID, returnModel.DepartmentID.Value);
                if (branchdept != null)
                    FirstFinanceID = branchdept.FianceFirstAuditorID;

                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(FirstFinanceID);
                this.txtInvoiceCode.Text = "";
                this.hidReceiver.Value = emp.SysID;
                this.lblReceiver.Text = emp.Name;
            }
            else if (invoiceModel.Status == 1)
            {
                this.txtInvoiceCode.Text = invoiceModel.InvoiceCode;
                this.hidReceiver.Value = string.Empty;
                this.lblReceiver.Text = string.Empty;
            }
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ReturnID]))
            {
                returnId = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]);
            }
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
            ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(returnModel.ProjectCode.Substring(0, 1));
            ESP.Finance.Entity.ReturnInvoiceInfo invoiceModel = ESP.Finance.BusinessLogic.ReturnInvoiceManager.GetModelByReturnID(returnId);

            int FirstFinanceID = branchModel.FirstFinanceID;

            ESP.Finance.Entity.BranchDeptInfo branchdept = ESP.Finance.BusinessLogic.BranchDeptManager.GetModel(branchModel.BranchID, returnModel.DepartmentID.Value);
            if (branchdept != null)
                FirstFinanceID = branchdept.FianceFirstAuditorID;

            if (invoiceModel == null)
            {
                invoiceModel = new ReturnInvoiceInfo();
                invoiceModel.FAID = Convert.ToInt32(this.hidReceiver.Value);
                invoiceModel.RequestorID = returnModel.RequestorID.Value;
                invoiceModel.RequestRemark = this.txtInvoiceDesc.Text.Trim();
                invoiceModel.ReturnID = returnModel.ReturnID;
                invoiceModel.Status = 1;
                invoiceModel.FinanceID = FirstFinanceID;
                invoiceModel.InvoiceCode = this.txtInvoiceCode.Text.Trim();
                ESP.Finance.BusinessLogic.ReturnInvoiceManager.Add(invoiceModel);
            }
            else
            {
                if (invoiceModel.Status == 0)
                {
                    invoiceModel.FARemark = this.txtInvoiceDesc.Text.Trim();
                    invoiceModel.InvoiceCode = this.txtInvoiceCode.Text.Trim();
                    invoiceModel.Status = 1;
                    ESP.Finance.BusinessLogic.ReturnInvoiceManager.Update(invoiceModel);
                }
                else if (invoiceModel.Status == 1)
                {
                    invoiceModel.FinanceRemark = this.txtInvoiceDesc.Text.Trim();
                    invoiceModel.Status = 2;
                    invoiceModel.InvoiceCode = this.txtInvoiceCode.Text.Trim();
                    ESP.Finance.BusinessLogic.ReturnInvoiceManager.Update(invoiceModel);
                }
            }
            Response.Redirect("/Edit/ReturnTabEdit.aspx");
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Edit/ReturnTabEdit.aspx");
        }
    }
}
