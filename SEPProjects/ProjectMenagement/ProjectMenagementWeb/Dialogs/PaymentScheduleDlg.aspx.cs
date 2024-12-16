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
    public partial class PaymentScheduleDlg : System.Web.UI.Page
    {
        private ESP.Finance.Entity.ProjectInfo projectModel;
        private ESP.Finance.Entity.PaymentInfo paymentModel;
        int paymentId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            paymentId = int.Parse(Request["PaymentId"].ToString());
            if (!IsPostBack)
            {
                BindInfo();
            }
        }


        private void BindInfo()
        {
            paymentModel = ESP.Finance.BusinessLogic.PaymentManager.GetModel(paymentId);
            projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(paymentModel.ProjectID);

            lblProjectCode.Text = projectModel.ProjectCode;
            lblPaymentCode.Text = paymentModel.PaymentCode;
            lblAmount.Text = paymentModel.PaymentBudget.Value.ToString("#,##0.00");
            lblBudgetConfirm.Text = paymentModel.PaymentBudgetConfirm == null ? "" : paymentModel.PaymentBudgetConfirm.Value.ToString("#,##0.00");
            lblContent.Text = paymentModel.PaymentContent;
            lblDate.Text = paymentModel.PaymentPreDate.ToString("yyyy-MM-dd");

            if (string.IsNullOrEmpty(paymentModel.InvoiceTitle))
            {
                ESP.Finance.Entity.CustomerTmpInfo customModel = ESP.Finance.BusinessLogic.CustomerTmpManager.GetModel(projectModel.CustomerID.Value);

                lblInvoiceTitle.Text = customModel.NameCN1;
            }
            else
            {
                lblInvoiceTitle.Text = paymentModel.InvoiceTitle;
            }

            if (paymentModel.PaymentFactDate != null)
                txtPaymentFactDate.Text = paymentModel.PaymentFactDate.Value.ToString("yyyy-MM-dd");

            if (paymentModel.BillDate != null)
            {
                txtBillDate.Text = paymentModel.BillDate.Value.ToString("yyyy-MM-dd");
                ddlPaymentType.SelectedValue = "2";
            }
            if (paymentModel.EstReturnDate != null)
                txtEstReturnDate.Text = paymentModel.EstReturnDate.Value.ToString("yyyy-MM-dd");

            txtPaymentFee.Text = paymentModel.PaymentFee.ToString("#,##0.00");
            txtPaymentFactForiegn.Text = paymentModel.PaymentFactForiegn.ToString("#,##0.00");
            if (string.IsNullOrEmpty(paymentModel.PaymentFactForiegnUnit))
                ddlPaymentFactForiegnUnit.SelectedValue = paymentModel.PaymentFactForiegnUnit;
            txtUSDDiffer.Text = paymentModel.USDDiffer.ToString("#,##0.00");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            paymentModel = ESP.Finance.BusinessLogic.PaymentManager.GetModel(paymentId);

            if (!string.IsNullOrEmpty(txtPaymentFactDate.Text))
                paymentModel.PaymentFactDate = DateTime.Parse(txtPaymentFactDate.Text);
            else
                paymentModel.PaymentFactDate = null;

            if (ddlPaymentType.SelectedValue == "2")
            {
                if (!string.IsNullOrEmpty(txtBillDate.Text))
                    paymentModel.BillDate = DateTime.Parse(txtBillDate.Text);
                else
                {
                    Page.ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('请输入汇票到期日!');", true);
                    return;
                }
            }
            else
            {
                paymentModel.BillDate = null;
            }

            if (!string.IsNullOrEmpty(txtEstReturnDate.Text))
                paymentModel.EstReturnDate = DateTime.Parse(txtEstReturnDate.Text);
            else
                paymentModel.EstReturnDate = null;

            if (!string.IsNullOrEmpty(txtPaymentFee.Text))
                paymentModel.PaymentFee = decimal.Parse(txtPaymentFee.Text);
            else
                paymentModel.PaymentFee = 0;

            if (!string.IsNullOrEmpty(txtPaymentFactForiegn.Text))
                paymentModel.PaymentFactForiegn = decimal.Parse(txtPaymentFactForiegn.Text);
            else
                paymentModel.PaymentFactForiegn = 0;

            paymentModel.PaymentFactForiegnUnit = ddlPaymentFactForiegnUnit.SelectedValue;

            if (!string.IsNullOrEmpty(txtUSDDiffer.Text))
                paymentModel.USDDiffer = decimal.Parse(txtUSDDiffer.Text);

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



        protected void btnClose_Click(object sender, EventArgs e)
        {

        }

        protected void ddlPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPaymentType.SelectedValue == "2")
                this.lblPaymentTypeDesc.Visible = true;
            else
                this.lblPaymentTypeDesc.Visible = false;
        }
    }
}
