using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
public partial class BankInfo_BankEdit : ESP.Web.UI.PageBase
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
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.BankID]))
        {
            bankId = int.Parse(Request[ESP.Finance.Utility.RequestName.BankID]);
        }
        if (bankId > 0)
        {
            btnBranch.Visible = false;
            ESP.Finance.Entity.BankInfo model = ESP.Finance.BusinessLogic.BankManager.GetModel(bankId);
            if (model != null)
            {
                ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModel(model.BranchID);
                if (branchModel != null) //分公司信息
                {
                    txtBranchCode.Value = branchModel.BranchCode;
                    labBranchCode.Text = branchModel.BranchCode;
                    hidBranchID.Value = branchModel.BranchID.ToString();
                    txtBranchName.Value = branchModel.BranchName;
                    labBranchName.Text = branchModel.BranchName;
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.BankID]))
        {
            bankId = int.Parse(Request[ESP.Finance.Utility.RequestName.BankID]);
        }
        if (bankId > 0)
        {
            if (ESP.Finance.Utility.UpdateResult.Succeed == ESP.Finance.BusinessLogic.BankManager.Update(getBankModel()))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存成功！');window.location='BankList.aspx';", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存失败！');", true);
            }
        }
        else
        {
            if (ESP.Finance.BusinessLogic.BankManager.Add(getBankModel()) > 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存成功！');window.location='BankList.aspx';", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存失败！');", true);
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("BankList.aspx");
    }

    private ESP.Finance.Entity.BankInfo getBankModel()
    {
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.BankID]))
        {
            bankId = int.Parse(Request[ESP.Finance.Utility.RequestName.BankID]);
        }
        ESP.Finance.Entity.BankInfo model = null;
        if (bankId > 0)
        {
            model = ESP.Finance.BusinessLogic.BankManager.GetModel(bankId);
        }
        else
        {
            model=new ESP.Finance.Entity.BankInfo();
        }
        model.BranchID = int.Parse(hidBranchID.Value);
        model.BranchName = txtBranchName.Value.Trim();
        model.BranchCode = txtBranchCode.Value.Trim();

        model.DBCode = txtDBCode.Text.Trim();
        model.DBManager = txtDBManager.Text.Trim();
        model.BankName = txtBankName.Text.Trim();
        model.BankAccountName = txtBankAccountName.Text.Trim();
        model.BankAccount = txtBankAccount.Text.Trim();
        model.Address = txtAddress.Text.Trim();
        model.PhoneNo = txtPhoneNo.Text.Trim();
        model.ExchangeNo = txtExchangeNo.Text.Trim();
        model.RequestPhone = txtRequestPhone.Text.Trim();
        return model;
    }
}
