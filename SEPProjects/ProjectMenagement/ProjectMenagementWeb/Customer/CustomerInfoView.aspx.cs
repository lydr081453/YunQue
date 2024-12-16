using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using ESP.Finance.Entity;

public partial class Customer_CustomerInfoView : System.Web.UI.Page
{
    public int customerId = 0;
    private IList<ESP.Finance.Entity.CustomerAttachInfo> attlist;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.CustomerID]))
        {
            customerId = int.Parse(Request[ESP.Finance.Utility.RequestName.CustomerID]);

        }
        if (!IsPostBack)
        {
            BindInfo();
        }
    }

    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ESP.Finance.Entity.CustomerAttachInfo attchModel =(ESP.Finance.Entity.CustomerAttachInfo)e.Row.DataItem;
            Label labDown = (Label)e.Row.FindControl("labDown");
            labDown.Text = attlist[e.Row.RowIndex].Attachment == null ? "" : "<a target='_blank' href='/Dialogs/CustomerFileDownload.aspx?" + ESP.Finance.Utility.RequestName.CustomerAttachID + "=" + attchModel.AttachID.ToString() + "'><img src='/images/ico_04.gif' border='0' /></a>";
        }
    }

    private void bindFrame(int cid)
    {
        attlist = ESP.Finance.BusinessLogic.CustomerAttachManager.GetList(" customerid=" + cid + "");
        this.gvG.DataSource = attlist;
        this.gvG.DataBind();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomerList.aspx");
    }

    private void BindInfo()
    {
        if (customerId == 0) return;
        CustomerInfo model = ESP.Finance.BusinessLogic.CustomerManager.GetModel(customerId);
        bindFrame(customerId);
        if (model != null)
        {
            txtCustomerCode.Text = model.CustomerCode;
            txtAddressCode.Text = model.AddressCode;
            txtNameCN1.Text = model.NameCN1;
            txtNameEN1.Text = model.NameEN1;
            txtNameCN2.Text = model.NameCN2;
            txtNameEN2.Text = model.NameEN2;
            txtShortEN.Text = model.ShortEN;
            txtAddress1.Text = model.Address1;
            txtAddress2.Text = model.Address2;
            txtContactName.Text = model.ContactName;
            this.lblInvoiceTitle.Text = model.InvoiceTitle;
            lblArea.Text = model.AreaName;
            lblIndustry.Text = model.IndustryName;

            //if(model.DefaultTaxRate != null)
            //    txtTaxRate.Text = model.DefaultTaxRate.ToString() + "%";

            txtAppCompany.Text = model.AppCompany;
            txtContactTel.Text = model.ContactTel;
            this.txtContactFax.Text = model.ContactFax;
            txtContactEmail.Text = model.ContactEmail;
            this.txtContactWebsite.Text = model.Website;
            this.txtPostCode.Text = model.PostCode;
            this.txtA0.Text = model.AO;
        }
    }
}
