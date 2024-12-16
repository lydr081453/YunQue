using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InvoiceInfo_NewInvoice : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CustomerInfo.CurrentUser = CurrentUser;
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("InvoiceList.aspx");
    }
        
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ESP.Finance.Entity.InvoiceInfo invoice = new ESP.Finance.Entity.InvoiceInfo();
        CustomerInfo.setCustomerInfo();
        ProjectInfo.setBranchInfo();
        ESP.Finance.Entity.CustomerInfo customer = CustomerInfo.CustomerModel;
        ESP.Finance.Entity.BranchInfo branch = ProjectInfo.BranchModel;
        if (branch != null)
        {
            invoice.BranchCode = branch.BranchCode;
            invoice.BranchID = branch.BranchID;
            invoice.BranchName = branch.BranchName;
        }
        invoice.CreateDate=DateTime.Now;
        invoice.CreatorEmployeeName = CurrentUser.Name ;
        invoice.CreatorID = Convert.ToInt32( CurrentUser.SysID);
        invoice.CreatorUserCode = CurrentUser.ID;
        invoice.CreatorUserName = CurrentUser.ITCode;
        if (customer != null)
        {
            invoice.CustomerID = customer.CustomerID;
            invoice.CustomerName = customer.FullNameCN;
            invoice.CustomerShortName = customer.ShortCN; 
            invoice.CustomerTitle = customer.InvoiceTitle;
        }

        if (Prepareinfo_txtApplicant.Text.Trim().Length > 0)
        {
            invoice.ReceiverEmployeeName = Prepareinfo_txtApplicant.Text.Trim();
            invoice.ReceiverUserCode = Prepareinfo_hidApplicantUserID.Value;
            invoice.ReceiverUserID = Convert.ToInt32(Prepareinfo_hidApplicantID.Value);
            invoice.ReceiverUserName = Prepareinfo_hidApplicantUserCode.Value;
        }

        invoice.InvoiceAmounts = Convert.ToDecimal( txtInvoiceAmounts.Text);
        invoice.InvoiceCode = txtInvoiceCode.Text;
        
        invoice.InvoiceStatus=(int)ESP.Finance.Utility.InvoiceStatus.Used;
        if (txtUSDDiffer.Text.Trim().Length > 0)
        {
            invoice.USDDiffer = Convert.ToDecimal(txtUSDDiffer.Text);
        }
        invoice.Remark = txtRemark.Text.Length > 200 ? txtRemark.Text.Substring(0, 200) : txtRemark.Text;
        int ret = ESP.Finance.BusinessLogic.InvoiceManager.Add(invoice);
        if (ret > 0)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('添加成功!');window.location='InvoiceList.aspx'", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('添加失败!');", true);
        }
    }
}
