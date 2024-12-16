using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
public partial class BankInfo_BankView : ESP.Web.UI.PageBase
{
    int bankId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.BankID]))
        {
            bankId = int.Parse(Request[ESP.Finance.Utility.RequestName.BankID]);
        }
        if (!IsPostBack)
        {
            bindInfo();
        }
    }

    /// <summary>
    /// 绑定页面信息
    /// </summary>
    private void bindInfo()
    {
        if (bankId > 0)
        {
            ESP.Finance.Entity.BankInfo model = ESP.Finance.BusinessLogic.BankManager.GetModel(bankId);
            if (model != null)
            {
                ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModel(model.BranchID);
                if (branchModel != null) //分公司信息
                {
                    txtBranchCode.Text = branchModel.BranchCode;
                    txtBranchName.Text = branchModel.BranchName;
                    labDes.Text = branchModel.Des;
                }

                txtDBCode.Text = model.DBCode;
                txtDBManager.Text = model.DBManager;
                txtBankName.Text = model.BankName;
                txtBankAccountName.Text = model.BankAccountName;
                txtBankAccount.Text = model.BankAccount;
                txtAddress.Text = model.Address;
                txtPhoneNo.Text = model.PhoneNo;
                txtExchangeNo.Text = model.ExchangeNo;
                txtRequestPhone.Text = model.RequestPhone;
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("BankList.aspx");
    }
}
