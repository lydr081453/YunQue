using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Utility;

namespace FinanceWeb.project
{
    public partial class PaymentPrint : System.Web.UI.Page
    {
        int paymentId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Response.ContentEncoding = System.Text.Encoding.UTF8;

            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.PaymentID]))
            {
                paymentId = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.PaymentID]);
            }
            if (!IsPostBack)
            {
                InitPageContent();
            }
        }


        void InitPageContent()
        {
            if (paymentId == 0) return;


            ESP.Finance.Entity.PaymentInfo paymentModel = ESP.Finance.BusinessLogic.PaymentManager.GetModel(paymentId);
            ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(paymentModel.ProjectID);
            ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModel(projectModel.BranchID.Value );
            IList<ESP.Finance.Entity.PaymentDetailInfo> detailList = ESP.Finance.BusinessLogic.PaymentDetailManager.GetList(" paymentid="+ paymentModel.PaymentID.ToString());

            int bankId = 0;
            if (projectModel.BankId != 0)
            {
                bankId = projectModel.BankId;
            }
            if (paymentModel.BankID != null && paymentModel.BankID.Value != 0)
            {
                bankId = paymentModel.BankID.Value;
            }
            ESP.Finance.Entity.BankInfo bankModel = ESP.Finance.BusinessLogic.BankManager.GetModel(bankId);
           

            //基本信息
            lblInvoiceTitle.Text = paymentModel.InvoiceTitle;
            lblPaymentCode.Text = paymentModel.PaymentCode;
            lblPaymentPreDate.Text = paymentModel.PaymentPreDate.ToString("yyyy-MM-dd");

            lblSign.Text = branchModel.BranchName;

            if (bankModel != null)
            {
                lblBankTitle.Text = bankModel.BankAccountName;
                lblBankName.Text = bankModel.BankName;
                lblBankAccount.Text = bankModel.BankAccount;
                lblBankAddress.Text = bankModel.Address;
            }
            if (detailList == null)
            {
                detailList = new List<ESP.Finance.Entity.PaymentDetailInfo>();
            }
            if (detailList.Count == 0)
            {
                ESP.Finance.Entity.PaymentDetailInfo item = new PaymentDetailInfo();
                item.PaymentContent = paymentModel.PaymentContent;
                item.PaymentPreAmount = paymentModel.PaymentBudget.Value;
                detailList.Add(item);
            }
            //付款通知信息
                string formater = @"<tr>
                        <td height='25' align='left'>
                            {0}
                        </td>
                        <td align='right'>
                            {1}
                        </td>
                    </tr>";
                string pay_content = string.Empty;
                foreach (ESP.Finance.Entity.PaymentDetailInfo item in detailList)
                {
                    if (item != null)
                    {
                        string content = string.IsNullOrEmpty(item.PaymentContent) ? "&nbsp;" : item.PaymentContent;
                        string amount =  item.PaymentPreAmount.ToString("#,##0.00");

                        pay_content += string.Format(formater, content, amount);
                    }
                }
                ltPayment.Text = pay_content;

                lblTotalDetail.Text = detailList.Sum(x => x.PaymentPreAmount).ToString("#,##0.00");
        }
    }
}