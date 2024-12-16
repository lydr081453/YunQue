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

public partial class Purchase_Requisition_Print_PaymantPrint : ESP.Web.UI.PageBase
{

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

        ESP.Finance.Entity.ReturnInfo ReturnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(rid);
        ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(ReturnModel.PRID.Value);
       // ESP.Purchase.Entity.OrderInfo orderModel = ESP.Purchase.BusinessLogic.OrderInfoManager.GetModelByGeneralID(generalModel.id);
        ESP.Purchase.Entity.PaymentPeriodInfo paymentPeriod = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetModelByPN(ReturnModel.ReturnID);
        ESP.Purchase.Entity.PeriodRecipientInfo periodRecipient = null;
        ESP.Purchase.Entity.RecipientInfo recipient = null;
        ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(generalModel.project_code.Substring(0, 1));

        if (generalModel.PRType == 6 && generalModel.HaveInvoice == false && paymentPeriod.TaxTypes != 0)
        {
            double tax = ESP.Purchase.BusinessLogic.OrderInfoManager.TaxCalculator(double.Parse(ReturnModel.PreFee.Value.ToString()), 1);
            lblTaxDesc.Text = "个税金额:" + tax.ToString() + ";     税后支付金额:" + (ReturnModel.PreFee.Value-decimal.Parse(tax.ToString())).ToString();
        }

        if (paymentPeriod != null)
        {
            periodRecipient = ESP.Purchase.BusinessLogic.PeriodRecipientManager.GetModelByPeriodId(paymentPeriod.id);
        }
        if (periodRecipient != null)
        {
            recipient = ESP.Purchase.BusinessLogic.RecipientManager.GetModel(periodRecipient.recipientId);
        }

        
        logoImg.ImageUrl = "/images/" + branchModel.Logo;

        if (!string.IsNullOrEmpty(ReturnModel.ProjectCode))
        {
            lblPRNo.Text = ReturnModel.PRNo;
        }
        labProjectCode.Text = ReturnModel.ProjectCode;
        try
        {
            if (recipient != null)
                labReturnFactDate.Text = recipient.RecipientDate.ToString("yyyy-MM-dd");
            else
                labReturnFactDate.Text = ReturnModel.ReturnFactDate == null ? "" : ReturnModel.ReturnFactDate.Value.ToString("yyyy-MM-dd");
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
                    if (ReturnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || ReturnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
                    {
                        this.lblTitle.Text = "现金付款申请单";
                    }
                }
                else if (paymentModel.Tag == "Cash")
                {
                    if (ReturnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_KillCash || ReturnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || ReturnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
                    {
                        this.lblTitle.Text = "现金付款申请单";
                    }
                    else if (ReturnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_Cash10Down)
                    {
                        if (ReturnModel.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete)
                            this.lblTitle.Text = "现金付款申请单";
                        else
                            this.lblTitle.Text = "现金借款申请单";
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

                else if (ReturnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ForeGift && (ReturnModel.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.WaitReceiving || ReturnModel.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete))
                {
                    this.lblTitle.Text = "押金冲销单";
                }
            }
        }
        lblCommitDate.Text = ReturnModel.RequestDate == null ? "" : ReturnModel.RequestDate.Value.ToString("yyyy-MM-dd");
        lblPN.Text = ReturnModel.ReturnCode;
        labRequestorUserName.Text = ReturnModel.RequestEmployeeName;
        labRequestorID.Text = new ESP.Compatible.Employee(ReturnModel.RequestorID.Value).ID.ToString();
        labDepartment.Text = generalModel.Department;
        labReturnContent.Text = ReturnModel.ReturnContent;
        lblRemark.Text = ReturnModel.Remark;
        labPreFee.Text = (ReturnModel.FactFee == null || ReturnModel.FactFee == 0) ? ReturnModel.PreFee.Value.ToString("#,##0.00") : ReturnModel.FactFee.Value.ToString("#,##0.00");
        labOrderid.Text = generalModel.orderid;

        lab_TotalPrice.Text = (ReturnModel.FactFee == null || ReturnModel.FactFee == 0) ? ReturnModel.PreFee.Value.ToString("#,##0.00") : ReturnModel.FactFee.Value.ToString("#,##0.00");
        labAccountName.Text = generalModel.account_name;
        labAccountBankName.Text = generalModel.account_bank;
        labAccountBankNo.Text = generalModel.account_number;

        IList<ESP.Finance.Entity.AuditLogInfo> histList = ESP.Finance.BusinessLogic.AuditLogManager.GetReturnList(ReturnModel.ReturnID);
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

        foreach (ESP.Finance.Entity.AuditLogInfo item in histList)
        {
            auditDate = item.AuditDate == null ? "" : item.AuditDate.Value.ToString("yyyy-MM-dd HH:mm:ss");

            if (string.IsNullOrEmpty(item.Suggestion))
            {
                if (item.AuditStatus == (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing)
                {
                    item.Suggestion = "审批通过";
                }
                else
                {
                    item.Suggestion = "审批驳回";
                }
            }
            rethist += "审批人:  " + item.AuditorEmployeeName + "  [" + auditDate + "]  " + item.Suggestion + "<br/>";
        }
        this.lblAuditList.Text = rethist;
    }


}

