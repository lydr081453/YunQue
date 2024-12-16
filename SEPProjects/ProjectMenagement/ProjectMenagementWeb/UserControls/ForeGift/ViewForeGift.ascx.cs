using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_ForeGift_ViewForeGift : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void BindInfo(ESP.Finance.Entity.ReturnInfo returnModel)
    {
        lblPRNo.Text = returnModel.PRNo;
        lblProjectCode.Text = returnModel.ProjectCode;
        lblApplicant.Text = returnModel.RequestEmployeeName;
        lblSupplierName.Text = returnModel.SupplierName;
        lblSupplierBank.Text = returnModel.SupplierBankName;
        lblSupplierAccount.Text = returnModel.SupplierBankAccount;
        lblForegift.Text = returnModel.PreFee.Value.ToString("#,##0.00");//押金
        lblReturnCode.Text = returnModel.ReturnCode;
        lblReturnContent.Text = returnModel.ReturnContent;
        lblInceptDate.Text = returnModel.ReturnPreDate == null ? "" : returnModel.ReturnPreDate.Value.ToString("yyyy-MM-dd");
        lblStatus.Text = ESP.Finance.Utility.ReturnPaymentType.ReturnStatusString(returnModel.ReturnStatus.Value,0,returnModel.IsDiscount);
        lblBeginDate.Text = returnModel.PreBeginDate == null ? "" : returnModel.PreBeginDate.Value.ToString("yyyy-MM-dd");
        lblendDate.Text = returnModel.PreEndDate == null ? "" : returnModel.PreEndDate.Value.ToString("yyyy-MM-dd");//预计归还时间
        lblPaymentType.Text = returnModel.PaymentTypeName;
    }
}