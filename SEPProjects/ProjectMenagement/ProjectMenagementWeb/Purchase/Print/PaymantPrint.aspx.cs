using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ESP.Finance.Utility;
public partial class Purchase_Print_PaymantPrint : ESP.Web.UI.PageBase
{
    //int num = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.lblTitle.Text = "支票/电汇付款申请单";
        }
        if (!string.IsNullOrEmpty(Request["viewButton"]) && Request["viewButton"] == "no")
        {
            btnClose.Visible = false;
            btnPrint.Visible = false;
        }
        initPrintPage();
    }

    private void initPrintPage()
    {
        int rid = int.Parse(Request[ESP.Finance.Utility.RequestName.ReturnID].ToString());

        ESP.Finance.Entity.ReturnGeneralInfoListViewInfo model = ESP.Finance.BusinessLogic.ReturnGeneralInfoListViewManager.GetModel(rid);
        ESP.Finance.Entity.ReturnInfo ReturnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(rid);
        ESP.Purchase.Entity.PaymentPeriodInfo paymentPeriod = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetModelByPN(ReturnModel.ReturnID);
        ESP.Purchase.Entity.PeriodRecipientInfo periodRecipient = null;
        ESP.Purchase.Entity.RecipientInfo recipient = null;
        ESP.Purchase.Entity.GeneralInfo generalModel = null;
        
        if (ReturnModel != null)
        {
            if (ReturnModel.PRID != null && ReturnModel.PRID.Value != 0)
            {
                generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(ReturnModel.PRID.Value);

                if (generalModel.PRType == 6 && generalModel.HaveInvoice == false && paymentPeriod.TaxTypes!=0)
                {
                    double tax = ESP.Purchase.BusinessLogic.OrderInfoManager.TaxCalculator(double.Parse(ReturnModel.PreFee.Value.ToString()), 1);
                    lblTaxDesc.Text = "个税金额:" + tax.ToString() + ";     税后支付金额:" + (ReturnModel.PreFee.Value - decimal.Parse(tax.ToString())).ToString("#0.00");
                }
            }
        }
       
        if (paymentPeriod != null)
        {
            periodRecipient = ESP.Purchase.BusinessLogic.PeriodRecipientManager.GetModelByPeriodId(paymentPeriod.id);
        }
        if (periodRecipient != null)
        {
            recipient = ESP.Purchase.BusinessLogic.RecipientManager.GetModel(periodRecipient.recipientId);
        }
        ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(generalModel.project_code.Substring(0,1));
        logoImg.ImageUrl = "/images/"+branchModel.Logo;

        if (!string.IsNullOrEmpty(model.ProjectCode))
        {
            lblPRNo.Text = ReturnModel.PRNo;
        }
        labProjectCode.Text = ReturnModel.ProjectCode;
        try
        {
            lblCommitDate.Text = ReturnModel.RequestDate == null ? "" : ReturnModel.RequestDate.Value.ToString("yyyy-MM-dd");
            //PN如果是标准条款，费用发生日期应该是收货日期
            if (recipient != null)
                labReturnFactDate.Text = recipient.RecipientDate.ToString("yyyy-MM-dd");
            else
                labReturnFactDate.Text = model.ReturnFactDate == null ? "" : model.ReturnFactDate.Value.ToString("yyyy-MM-dd");
            lblPreDate.Text = ReturnModel.PreBeginDate == null ? "" : ReturnModel.PreBeginDate.Value.ToString("yyyy-MM-dd");
        }
        catch { }
        if (ReturnModel.PaymentTypeID != null)
        {
            ESP.Finance.Entity.PaymentTypeInfo paymentModel = ESP.Finance.BusinessLogic.PaymentTypeManager.GetModel(ReturnModel.PaymentTypeID.Value);
            if (paymentModel != null)
            {
                if (paymentModel.Tag == "PR")
                {
                    this.lblTitle.Text = "支票/电汇付款申请单";
                    
                    if (ReturnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ForeGift)
                    {
                        this.lblTitle.Text += "（押金）";
                    }
                }
                else if (paymentModel.Tag == "Cash")
                {
                    if (ReturnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_KillCash)
                    {
                        this.lblTitle.Text = "现金付款申请单";
                    }
                    else if (ReturnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ForeGift)
                    {
                            this.lblTitle.Text = "现金借款申请单（押金）";
                    }
                    else
                    {
                        if (ReturnModel.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete)
                            this.lblTitle.Text = "现金付款申请单";
                        else
                            this.lblTitle.Text = "现金借款申请单";
                    }
                }
                else if (paymentModel.Tag == "Card")
                {
                    this.lblTitle.Text = "商务卡付款申请单";
                }
                //else if (ReturnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ForeGift && (ReturnModel.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.WaitReceiving || ReturnModel.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete))
                //{
                //    this.lblTitle.Text = "押金冲销单";
                //}
            }
        }
        lblPN.Text = model.ReturnCode;
        labRequestorUserName.Text = model.RequestEmployeeName;
        labRequestorID.Text = new ESP.Compatible.Employee(model.RequestorID.Value).ID.ToString();
        labDepartment.Text = model.Department;
        labReturnContent.Text = model.ReturnContent;
        lblRemark.Text = ReturnModel.Remark;
        if (model.FactFee != null && model.FactFee != 0)
        {
            labPreFee.Text = model.FactFee.Value.ToString("#,##0.00");
            lab_TotalPrice.Text = model.FactFee.Value.ToString("#,##0.00");
        }
        else
        {
            labPreFee.Text = model.PreFee.Value.ToString("#,##0.00");
            lab_TotalPrice.Text = model.PreFee.Value.ToString("#,##0.00");
        }
        labOrderid.Text = model.Orderid;
        IList<ESP.Finance.Entity.BankCancelInfo> cancelList = ESP.Finance.BusinessLogic.BankCancelManager.GetList(" ReturnID=" + model.ReturnID + " and (ordertype is null or ordertype=1 )");
        if (cancelList != null && cancelList.Count > 0)
        {
            this.labAccountName.Text = cancelList[cancelList.Count - 1].OldBankAccountName;
            this.labAccountBankName.Text = cancelList[cancelList.Count - 1].NewBankName;
            this.labAccountBankNo.Text = cancelList[cancelList.Count - 1].NewBankAccount;
        }
        else
        {
            if (model != null && model.PRID != null)
            {
                if (string.IsNullOrEmpty(model.Account_name))
                {
                    if (generalModel == null)
                    {
                        labAccountName.Text = ReturnModel.SupplierName;
                        labAccountBankName.Text = ReturnModel.SupplierBankName;
                        labAccountBankNo.Text = ReturnModel.SupplierBankAccount;
                    }
                    else
                    {
                        labAccountName.Text = generalModel.account_name;
                        labAccountBankName.Text = generalModel.account_bank;
                        labAccountBankNo.Text = generalModel.account_number;
                    }
                }
                else
                {
                    labAccountName.Text = model.Account_name;
                    labAccountBankName.Text = model.Account_bank;
                    labAccountBankNo.Text = model.Account_number;
                }
            }
            else
            {
                labAccountName.Text = ReturnModel.SupplierName;
                labAccountBankName.Text = ReturnModel.SupplierBankName;
                labAccountBankNo.Text = ReturnModel.SupplierBankAccount;
            }
        }
        string rethist = string.Empty;
        string auditDate = string.Empty;
        if (generalModel != null && generalModel.ValueLevel == 1)
        {
            //业务审核日志
            IList<ESP.Purchase.Entity.AuditLogInfo> oploglist = ESP.Purchase.BusinessLogic.AuditLogManager.GetModelListByGID(generalModel.id);

            foreach (ESP.Purchase.Entity.AuditLogInfo log in oploglist)
            {
                ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(log.auditUserId);
                rethist += log.auditUserName + "(" + emp.FullNameEN + ")" + ESP.Purchase.Common.State.operationAudit_statusName[log.auditType] + " " + log.remark + " " + log.remarkDate + "<br/>";
            }
        }

        IList<ESP.Finance.Entity.AuditLogInfo> histList = ESP.Finance.BusinessLogic.AuditLogManager.GetReturnList(ReturnModel.ReturnID);
       
        foreach (ESP.Finance.Entity.AuditLogInfo item in histList)
        {
            auditDate = item.AuditDate == null ? "" : item.AuditDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
            if (item.AuditStatus == (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing)
            {
                item.Suggestion = "审批通过:" + item.Suggestion;
            }
            else if (item.AuditStatus == (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing)
            {
                item.Suggestion = "审批驳回:" + item.Suggestion;
            }
            else
            {
                item.Suggestion = item.Suggestion;
            }
            rethist += "审批人:  " + item.AuditorEmployeeName + "(" + item.AuditorUserName + ")" + "  [" + auditDate + "]  " + item.Suggestion + "<br/>";
        }
        this.lblAuditList.Text = rethist;
    }


}
