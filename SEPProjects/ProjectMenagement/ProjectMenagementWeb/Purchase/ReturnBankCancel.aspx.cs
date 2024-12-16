using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
namespace FinanceWeb.Purchase
{
    public partial class ReturnBankCancel : ESP.Finance.WebPage.EditPageForReturn
    {
        private int returnId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ReturnID]))
            {
                returnId = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]);
            }
            BindInfo();
        }

        private void BindInfo()
        {
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
            //if (returnModel.ReturnStatus != (int)ESP.Finance.Utility.PaymentStatus.Save)
            //{
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('你没有访问该数据的权限！');window.location.href='Default.aspx';", true);
            //}
            hidPrID.Value = returnModel.PRID.ToString();
            hidProjectID.Value = returnModel.ProjectID.ToString();
            lblApplicant.Text = returnModel.RequestEmployeeName;
            lblApplicant.Attributes["onclick"] = "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(returnModel.RequestorID.Value) + "');";
            lblBeginDate.Text = returnModel.PreBeginDate == null ? "" : returnModel.PreBeginDate.Value.ToString("yyyy-MM-dd");
           // lblEndDate.Text = returnModel.PreEndDate == null ? "" : returnModel.PreEndDate.Value.ToString("yyyy-MM-dd");
            lblInceptDate.Text = returnModel.ReturnPreDate == null ? "" : returnModel.ReturnPreDate.Value.ToString("yyyy-MM-dd");
            lblInceptPrice.Text = returnModel.PreFee == null ? "" : returnModel.PreFee.Value.ToString("#,##0.00");
            lblPeriodType.Text = returnModel.PaymentTypeName;
            if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.CommonPR || returnModel.ReturnType == null)
                lblPRNo.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + returnModel.PRID.ToString() + "'style='cursor: hand' target='_blank'>" + returnModel.PRNo + "</a>";
            else
                lblPRNo.Text = returnModel.PRNo;
            lblProjectCode.Text = returnModel.ProjectCode;
            lblReturnCode.Text = returnModel.ReturnCode;
            txtReturnContent.Text = returnModel.ReturnContent;
            lblStatus.Text = ESP.Finance.Utility.ReturnPaymentType.ReturnStatusString(returnModel.ReturnStatus.Value,0,returnModel.IsDiscount);
            this.lblSugestion.Text = returnModel.RePaymentSuggestion;
            lblBeginDate.Text = returnModel.PreBeginDate == null ? "" : returnModel.PreBeginDate.Value.ToString("yyyy-MM-dd");
            //lblEndDate.Text = returnModel.PreEndDate == null ? "" : returnModel.PreEndDate.Value.ToString("yyyy-MM-dd");
            lblFee.Text = returnModel.PreFee == null ? "0.00" : returnModel.PreFee.Value.ToString("#,##0.00");
            lblPayDate.Text = returnModel.ReturnFactDate == null ? "" : returnModel.ReturnFactDate.Value.ToString("yyyy-MM-dd");

          //  ESP.Finance.Entity.ReturnGeneralInfoListViewInfo vmodel = ESP.Finance.BusinessLogic.ReturnGeneralInfoListViewManager.GetModel(returnModel.ReturnID);
         //   if (vmodel != null)
         //   {
            this.lblSupplierName.Text = returnModel.SupplierName;
            this.lblSupplierBank.Text = returnModel.SupplierBankName;
            this.lblSupplierAccount.Text =returnModel.SupplierBankAccount;
         //   }

            this.lblPayType.Text = returnModel.PaymentTypeName;
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Edit/ReturnTabEdit.aspx");
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]));
            ESP.Finance.Entity.BankCancelInfo bankInfo = new ESP.Finance.Entity.BankCancelInfo();
            ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(returnModel.ProjectCode.Substring(0, 1));
            
            int FirstFinanceID = branchModel.FirstFinanceID;
            ESP.Finance.Entity.BranchDeptInfo branchdept = ESP.Finance.BusinessLogic.BranchDeptManager.GetModel(branchModel.BranchID, returnModel.DepartmentID.Value);
            if (branchdept != null)
                FirstFinanceID = branchdept.FianceFirstAuditorID;

            bankInfo.ReturnID = returnModel.ReturnID;
            bankInfo.ReturnCode = returnModel.ReturnCode;
            bankInfo.RequestorID = Convert.ToInt32(CurrentUser.SysID);
            bankInfo.RequestorName = CurrentUser.ITCode;
            bankInfo.RequestorCode = CurrentUser.ID;
            bankInfo.RequestorEmpName = CurrentUser.Name;
            bankInfo.OldBankAccountName = this.lblSupplierName.Text;
            bankInfo.OldBankName = this.lblSupplierBank.Text;
            bankInfo.OldBankAccount = this.lblSupplierAccount.Text;
            bankInfo.NewBankAccount = this.txtSupplierAccount.Text.Trim();
            bankInfo.NewBankAccountName = this.lblSupplierName.Text.Trim();
            bankInfo.NewBankName = this.txtSupplierBank.Text.Trim();
            bankInfo.CommitDate = DateTime.Now;
            bankInfo.LastUpdateTime = DateTime.Now;
            bankInfo.RePaymentSuggestion = this.lblSugestion.Text;
            bankInfo.OrderType = 1;
            returnModel.SupplierName = this.lblSupplierName.Text.Trim();
            returnModel.SupplierBankName = this.txtSupplierBank.Text.Trim();
            returnModel.SupplierBankAccount = this.txtSupplierAccount.Text.Trim();
            returnModel.ReturnContent = txtReturnContent.Text.Trim();
            try
            {

                ESP.Compatible.Employee financeEmp = new ESP.Compatible.Employee(FirstFinanceID);
                    //付款申请审核人日志
                    ESP.Finance.Entity.ReturnAuditHistInfo FinanceModel = new ESP.Finance.Entity.ReturnAuditHistInfo();
                    FinanceModel.ReturnID = returnModel.ReturnID;
                    FinanceModel.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
                    FinanceModel.AuditorUserID = int.Parse(financeEmp.SysID);
                    FinanceModel.AuditorUserCode = financeEmp.ID;
                    FinanceModel.AuditorEmployeeName = financeEmp.Name;
                    FinanceModel.AuditorUserName = financeEmp.ITCode;
                    FinanceModel.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                    //付款申请表内的审批人记录
                    returnModel.PaymentUserID = int.Parse(financeEmp.SysID);
                    returnModel.PaymentEmployeeName = financeEmp.Name;
                    returnModel.PaymentCode = financeEmp.ID;
                    returnModel.PaymentUserName = financeEmp.ITCode;
                    returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.MajorAudit;
                   int ret =ESP.Finance.BusinessLogic.BankCancelManager.CommitReturnInfo(returnModel,FinanceModel,bankInfo);
                   if (ret > 0)
                   {
                       ESP.Finance.Entity.AuditLogInfo log = new ESP.Finance.Entity.AuditLogInfo();
                       log.AuditDate = DateTime.Now;
                       log.AuditorEmployeeName = CurrentUser.Name;
                       log.AuditorSysID = Convert.ToInt32(CurrentUser.SysID);
                       log.AuditorUserCode = CurrentUser.ID;
                       log.AuditorUserName = CurrentUser.ITCode;
                       log.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing;
                       log.FormID = returnModel.ReturnID;
                       log.FormType = (int)ESP.Finance.Utility.FormType.Return;
                       log.Suggestion = "重汇业务提交";
                       ESP.Finance.BusinessLogic.AuditLogManager.Add(log);
                       string exMail = string.Empty;
                       try
                       {
                           ESP.Finance.Utility.SendMailHelper.SendMailReturnCommit(returnModel, returnModel.RequestEmployeeName, financeEmp.Name, "", financeEmp.EMail);
                       }
                       catch
                       {
                           exMail = "(邮件发送失败!)";
                       }
                       ShowCompleteMessage("已提交至财务人员" + financeEmp.Name + "！"+exMail, "/Edit/ReturnTabEdit.aspx");
                   }
                   else
                   {
                       ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存失败！');", true);
                   }
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add(string.Format("{0}对F_Return表中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, Request[RequestName.ReturnID], "付款申请编辑"), "付款申请", ESP.Logging.LogLevel.Error, ex);
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('"+ex.Message+"');", true);
            }
        }
    }
}
