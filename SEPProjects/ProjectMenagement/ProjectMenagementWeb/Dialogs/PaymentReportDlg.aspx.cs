using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.BusinessLogic;
using ExtExtenders;
using ESP.Finance.Utility;


namespace FinanceWeb.Dialogs
{
    public partial class PaymentReportDlg : System.Web.UI.Page
    {
        private ESP.Finance.Entity.ProjectInfo projectModel;
        private ESP.Finance.Entity.PaymentInfo paymentModel;
        int paymentId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            paymentId = int.Parse(Request["PaymentId"].ToString());
            if (!IsPostBack)
            {
                bindYearMonth();
                BindInfo();
            }

        }

        private void bindYearMonth()
        {
            ddlYear.Items.Add(new ListItem("0", "0"));
            ddlYear.Items.Add(new ListItem((DateTime.Now.Year - 3).ToString(), (DateTime.Now.Year - 3).ToString()));
            ddlYear.Items.Add(new ListItem((DateTime.Now.Year - 2).ToString(), (DateTime.Now.Year - 2).ToString()));
            ddlYear.Items.Add(new ListItem((DateTime.Now.Year - 1).ToString(), (DateTime.Now.Year - 1).ToString()));
            ddlYear.Items.Add(new ListItem((DateTime.Now.Year).ToString(), (DateTime.Now.Year).ToString()));
            ddlYear.Items.Add(new ListItem((DateTime.Now.Year + 1).ToString(), (DateTime.Now.Year + 1).ToString()));

            ddlYear.SelectedValue = "0";

            ddlMonth.Items.Add(new ListItem("0", "0"));
            ddlMonth.Items.Add(new ListItem("1", "1"));
            ddlMonth.Items.Add(new ListItem("2", "2"));
            ddlMonth.Items.Add(new ListItem("3", "3"));
            ddlMonth.Items.Add(new ListItem("4", "4"));
            ddlMonth.Items.Add(new ListItem("5", "5"));
            ddlMonth.Items.Add(new ListItem("6", "6"));
            ddlMonth.Items.Add(new ListItem("7", "7"));
            ddlMonth.Items.Add(new ListItem("8", "8"));
            ddlMonth.Items.Add(new ListItem("9", "9"));
            ddlMonth.Items.Add(new ListItem("10", "10"));
            ddlMonth.Items.Add(new ListItem("11", "11"));
            ddlMonth.Items.Add(new ListItem("12", "12"));

            ddlMonth.SelectedValue = "0";


            ddlRebateYear.Items.Add(new ListItem("0", "0"));
            ddlRebateYear.Items.Add(new ListItem((DateTime.Now.Year - 3).ToString(), (DateTime.Now.Year - 3).ToString()));
            ddlRebateYear.Items.Add(new ListItem((DateTime.Now.Year - 2).ToString(), (DateTime.Now.Year - 2).ToString()));
            ddlRebateYear.Items.Add(new ListItem((DateTime.Now.Year - 1).ToString(), (DateTime.Now.Year - 1).ToString()));
            ddlRebateYear.Items.Add(new ListItem((DateTime.Now.Year).ToString(), (DateTime.Now.Year).ToString()));
            ddlRebateYear.Items.Add(new ListItem((DateTime.Now.Year + 1).ToString(), (DateTime.Now.Year + 1).ToString()));

            ddlRebateYear.SelectedValue = "0";

            ddlRebateMonth.Items.Add(new ListItem("0", "0"));
            ddlRebateMonth.Items.Add(new ListItem("1", "1"));
            ddlRebateMonth.Items.Add(new ListItem("2", "2"));
            ddlRebateMonth.Items.Add(new ListItem("3", "3"));
            ddlRebateMonth.Items.Add(new ListItem("4", "4"));
            ddlRebateMonth.Items.Add(new ListItem("5", "5"));
            ddlRebateMonth.Items.Add(new ListItem("6", "6"));
            ddlRebateMonth.Items.Add(new ListItem("7", "7"));
            ddlRebateMonth.Items.Add(new ListItem("8", "8"));
            ddlRebateMonth.Items.Add(new ListItem("9", "9"));
            ddlRebateMonth.Items.Add(new ListItem("10", "10"));
            ddlRebateMonth.Items.Add(new ListItem("11", "11"));
            ddlRebateMonth.Items.Add(new ListItem("12", "12"));

            ddlRebateMonth.SelectedValue = "0";
        }


        private void BindInfo()
        {
            paymentModel = ESP.Finance.BusinessLogic.PaymentManager.GetModel(paymentId);
            projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(paymentModel.ProjectID);
            lblProjectCode.Text = projectModel.ProjectCode;
            txtPaymentCode.Text = paymentModel.PaymentCode;
            txtBudget.Text = paymentModel.PaymentBudget.Value.ToString("#,##0.00");
            txtBudgetConfirm.Text = paymentModel.PaymentBudgetConfirm == null ? "" : paymentModel.PaymentBudgetConfirm.Value.ToString("#,##0.00");

            txtBudgetForiegn.Text = paymentModel.BudgetForiegn.ToString("#,##0.00");
            ddlCurrency.SelectedValue = paymentModel.BudgetForiegnUnit;
            txtPaymentDate.Text = paymentModel.PaymentPreDate.ToString("yyyy-MM-dd");
            txtDes.Text = paymentModel.PaymentContent;
            txtRemark.Text = paymentModel.Remark;

            if (paymentModel.BadDebt == 1)
                chkBad.Checked = true;

            if (paymentModel.InnerRelation == 1)
                chkInner.Checked = true;

            if (string.IsNullOrEmpty(paymentModel.InvoiceTitle))
            {
                ESP.Finance.Entity.CustomerTmpInfo customModel = ESP.Finance.BusinessLogic.CustomerTmpManager.GetModel(projectModel.CustomerID.Value);

                txtInvoiceTitle.Text = customModel.NameCN1;
                //返点title
                txtRebateTitle.Text = customModel.NameCN1; 
            }
            else
            {
                txtInvoiceTitle.Text = paymentModel.InvoiceTitle;
                //返点title
                txtRebateTitle.Text = paymentModel.ReBateTitle;
            }

            txtInvoiceNo.Text = paymentModel.InvoiceNo;
            txtInvoiceDate.Text = paymentModel.InvoiceDate == null ? "" : paymentModel.InvoiceDate.Value.ToString("yyyy-MM-dd");
            txtInvoiceAmount.Text = paymentModel.InvoiceAmount.ToString("#,##0.00");
            txtInvoiceReceiver.Text = paymentModel.InvoiceReceiver;
            txtInvoiceSignIn.Text = paymentModel.InvoiceSignIn;
            txtInvoiceType.Text = paymentModel.InvoiceType;

            ddlYear.SelectedValue = paymentModel.ConfirmYear.ToString();
            ddlMonth.SelectedValue = paymentModel.ConfirmMonth.ToString();
            
            if (paymentModel.ConfirmYear == null || paymentModel.ConfirmYear == 0)
            {
                ddlYear.SelectedValue = projectModel.EndDate == null ? "0" : projectModel.EndDate.Value.Year.ToString();
            }
            if (paymentModel.ConfirmMonth == null || paymentModel.ConfirmMonth == 0)
            {
                ddlMonth.SelectedValue = projectModel.EndDate == null ? "0" : projectModel.EndDate.Value.Month.ToString();
            }

            //返点发票信息
            txtRebateNo.Text = paymentModel.RebateNo;
            txtRebateDate.Text = paymentModel.RebateDate == null ? "" : paymentModel.RebateDate.Value.ToString("yyyy-MM-dd");
            txtRebateAmount.Text = paymentModel.RebateAmount.ToString("#,##0.00");
            txtRebateReceiver.Text = paymentModel.RebateReceiver;
            txtRebateSignIn.Text = paymentModel.RebateSignIn;
            txtRebateType.Text = paymentModel.RebateType;

            ddlRebateYear.SelectedValue = paymentModel.RebateYear.ToString();
            ddlRebateMonth.SelectedValue = paymentModel.RebateMonth.ToString();

            if (paymentModel.RebateYear == null || paymentModel.RebateYear == 0)
            {
                ddlRebateYear.SelectedValue = projectModel.EndDate == null ? "0" : projectModel.EndDate.Value.Year.ToString();
            }
            if (paymentModel.RebateMonth == null || paymentModel.RebateMonth == 0)
            {
                ddlRebateMonth.SelectedValue = projectModel.EndDate == null ? "0" : projectModel.EndDate.Value.Month.ToString();
            }

            int bankId = 0;
            if (projectModel.BankId != 0)
            {
                bankId = projectModel.BankId;
            }

            if (bankId != 0)
            {
                ESP.Finance.Entity.BankInfo bankModel = ESP.Finance.BusinessLogic.BankManager.GetModel(bankId);
                lblBankName.Text = bankModel.BankName;
                lblAccountName.Text = bankModel.BankAccountName;
                lblAccount.Text = bankModel.BankAccount;
                lblBankAddress.Text = bankModel.Address;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            paymentModel = ESP.Finance.BusinessLogic.PaymentManager.GetModel(paymentId);
            projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(paymentModel.ProjectID);

            paymentModel.PaymentCode = txtPaymentCode.Text;

            paymentModel.PaymentBudget = decimal.Parse(string.IsNullOrEmpty(txtBudget.Text) ? "0" : txtBudget.Text);
            paymentModel.PaymentBudgetConfirm = decimal.Parse(string.IsNullOrEmpty(txtBudgetConfirm.Text) ? "0" : txtBudgetConfirm.Text);

            paymentModel.BudgetForiegn = decimal.Parse(string.IsNullOrEmpty(txtBudgetForiegn.Text) ? "0" : txtBudgetForiegn.Text);
            paymentModel.BudgetForiegnUnit = ddlCurrency.SelectedValue;

            if (!string.IsNullOrEmpty(txtPaymentDate.Text))
                paymentModel.PaymentPreDate = DateTime.Parse(txtPaymentDate.Text);
            paymentModel.PaymentContent = txtDes.Text;
            paymentModel.Remark = txtRemark.Text;

            if (chkBad.Checked == true)
                paymentModel.BadDebt = 1;
            else
                paymentModel.BadDebt = 0;

            if (chkInner.Checked == true)
                paymentModel.InnerRelation = 1;
            else
                paymentModel.InnerRelation = 0;

            //开票信息
            paymentModel.InvoiceTitle = txtInvoiceTitle.Text;
            paymentModel.InvoiceNo = txtInvoiceNo.Text;
            if (!string.IsNullOrEmpty(txtInvoiceDate.Text))
                paymentModel.InvoiceDate = DateTime.Parse(txtInvoiceDate.Text);

  
            paymentModel.InvoiceAmount = decimal.Parse(string.IsNullOrEmpty(txtInvoiceAmount.Text) ? "0" : txtInvoiceAmount.Text);
            paymentModel.InvoiceReceiver = txtInvoiceReceiver.Text;
            paymentModel.InvoiceSignIn = txtInvoiceSignIn.Text;
            paymentModel.InvoiceType = txtInvoiceType.Text;

            paymentModel.ConfirmYear = int.Parse(ddlYear.SelectedValue);
            paymentModel.ConfirmMonth = int.Parse(ddlMonth.SelectedValue);

            //返点发票信息
            paymentModel.ReBateTitle = txtRebateTitle.Text;
            paymentModel.RebateNo = txtRebateNo.Text;
            if (!string.IsNullOrEmpty(txtRebateDate.Text))
                paymentModel.RebateDate = DateTime.Parse(txtRebateDate.Text);


            paymentModel.RebateAmount = decimal.Parse(string.IsNullOrEmpty(txtRebateAmount.Text) ? "0" : txtRebateAmount.Text);
            paymentModel.RebateReceiver = txtRebateReceiver.Text;
            paymentModel.RebateSignIn = txtRebateSignIn.Text;
            paymentModel.RebateType = txtRebateType.Text;

            paymentModel.RebateYear = int.Parse(ddlRebateYear.SelectedValue);
            paymentModel.RebateMonth = int.Parse(ddlRebateMonth.SelectedValue);

            bool paymentCodeExist = ESP.Finance.BusinessLogic.PaymentManager.PaymentCodeExist(paymentModel.PaymentID, paymentModel.PaymentCode);

            if (paymentCodeExist)
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('付款通知号码重复，请检查!');", true);
            }
            else
            {
                UpdateResult ret = ESP.Finance.BusinessLogic.PaymentManager.Update(paymentModel);

                if (ret == UpdateResult.Succeed)
                {
                    Page.ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('保存成功!');", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('保存付款通知信息时出现错误，请检查!');", true);
                }
            }

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 生成付款通知号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            txtPaymentCode.Text = ESP.Finance.BusinessLogic.PaymentManager.CreateCode(paymentId);
        }
    }
}