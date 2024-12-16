using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
public partial class Return_NotificationPrint : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        int PaymentID = 0;
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.PaymentID]))
            {
                PaymentID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.PaymentID]);
                ESP.Finance.Entity.PaymentInfo model = ESP.Finance.BusinessLogic.PaymentManager.GetModel(PaymentID);
                ESP.Finance.Entity.ProjectInfo ProjectModel=ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(model.ProjectID);
                //customer info title left 
                if (ProjectModel.Customer != null)
                {
                    this.lblMobile.Text = ProjectModel.Customer.ContactMobile;
                    this.lblCompany.Text = ProjectModel.Customer.NameCN1;
                    this.lblPostCode.Text = ProjectModel.Customer.PostCode;
                    this.lblTel.Text = ProjectModel.Customer.ContactTel;
                    this.lblDept.Text = ProjectModel.Customer.ContactPosition;
                }
                //date and paymentcode title right
                if (model != null)
                {
                    this.lblPaymentCode.Text = model.PaymentCode;
                    this.lblDate.Text = model.PaymentFactDate == null ? "" : model.PaymentFactDate.Value.ToString("yyyy-MM-dd");
                    //bank info bottom
                    this.lblBankName.Text = model.BankName;
                    this.lblBranchName.Text = model.BranchName;
                    this.lblBankAccount.Text = model.BankAccount;
                    this.lblBankAddress.Text = model.BankAddress;
                    this.lblAccountName.Text = model.BankAccountName;
                    //table content
                    this.lblPaymentAmount.Text = model.PaymentFee.ToString("#,##0.00");
                    this.lblPaymentDesc.Text = model.PaymentContent;
                    this.lblProjectCode.Text = model.ProjectCode;
                    this.lblTotalAmount.Text = model.PaymentFee.ToString("#,##0.00");
                }
                if(ProjectModel!=null)
                {
                   this.lblProjectName.Text = ProjectModel.BusinessDescription;
                }
            }
        }
    }
}
