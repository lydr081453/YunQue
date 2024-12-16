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
    public partial class ReturnDisplay : ESP.Finance.WebPage.ViewPageForReturn
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
            ESP.Purchase.Entity.GeneralInfo generalModel = null;
            if (ReturnModel != null)
            {
                if (ReturnModel.PRID != null && ReturnModel.PRID.Value != 0)
                {
                    generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(ReturnModel.PRID.Value);
                }
            }
            ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(ReturnModel.ProjectCode.Substring(0,1));
            if (CurrentUserID == branchModel.FirstFinanceID || CurrentUserID == branchModel.PaymentAccounter || CurrentUserID == branchModel.ProjectAccounter)
            {
                btnSave.Visible = true;
            }

            IList<ESP.Finance.Entity.AuditLogInfo> histList = ESP.Finance.BusinessLogic.AuditLogManager.GetReturnList(ReturnModel.ReturnID);
            string loginfo = string.Empty;

            if (generalModel != null && generalModel.ValueLevel == 1)
            {
                //业务审核日志
                IList<ESP.Purchase.Entity.AuditLogInfo> oploglist = ESP.Purchase.BusinessLogic.AuditLogManager.GetModelListByGID(generalModel.id);

                foreach (ESP.Purchase.Entity.AuditLogInfo log in oploglist)
                {
                    ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(log.auditUserId);
                    loginfo += log.auditUserName + "(" + emp.FullNameEN + ")" + ESP.Purchase.Common.State.operationAudit_statusName[log.auditType] + " " + log.remark + " " + log.remarkDate + "<br/>";
                }
            }

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
            lblCreateTime.Text = returnModel.RequestDate.Value.ToString();
            lblApplicant.Text = returnModel.RequestEmployeeName;
            lblApplicant.Attributes["onclick"] = "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(returnModel.RequestorID.Value) + "');";
            lblBeginDate.Text = returnModel.PreBeginDate == null ? "" : returnModel.PreBeginDate.Value.ToString("yyyy-MM-dd");
            lblInceptDate.Text = returnModel.ReturnPreDate == null ? "" : returnModel.ReturnPreDate.Value.ToString("yyyy-MM-dd");
            lblInceptPrice.Text = returnModel.PreFee == null ? "" : returnModel.PreFee.Value.ToString("#,##0.00");
            lblPayRemark.Text = returnModel.ReturnContent;
            lblPRNo.Text = returnModel.PRNo;
            lblProjectCode.Text = returnModel.ProjectCode;
            lblReturnCode.Text = returnModel.ReturnCode;
            lblPayCode.Text = returnModel.PaymentTypeCode;
            lblStatus.Text = ESP.Finance.Utility.ReturnPaymentType.ReturnStatusString(returnModel.ReturnStatus.Value,0,returnModel.IsDiscount);
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
            lblFactFee.Text = returnModel.FactFee==null?"":returnModel.FactFee.Value.ToString("#,##0.00");
            radioInvoice.SelectedValue = returnModel.IsInvoice == null ? "" : returnModel.IsInvoice.Value.ToString();
            lblPreDate.Text = returnModel.ReturnPreDate == null ? "" : returnModel.ReturnPreDate.Value.ToString("yyyy-MM-dd");
            lblBankName.Text = returnModel.BankName;
            lblAccountName.Text = returnModel.BankAccountName;
            lblAccount.Text = returnModel.BankAccount;
            lblBankAddress.Text = returnModel.BankAddress;
            lblPaymentType.Text = returnModel.PaymentTypeName;
            this.lblLog.Text = this.GetAuditLog(returnModel.ReturnID);
            if (returnModel.DepartmentID != null && returnModel.DepartmentID.Value!=0)
            {
                ESP.Compatible.Department dept = ESP.Compatible.DepartmentManager.GetDepartmentByPK(returnModel.DepartmentID.Value);
                labDepartment.Text = dept.DepartmentName;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ReturnID]))
            {
                returnId = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]);
                
                ESP.Finance.BusinessLogic.ReturnManager.UpdateIsInvoice(returnId, int.Parse(radioInvoice.SelectedValue));

                BindInfo();
            }
        }
    }
}

