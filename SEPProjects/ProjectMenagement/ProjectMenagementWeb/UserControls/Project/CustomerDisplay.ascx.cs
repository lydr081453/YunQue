using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
public partial class UserControls_Project_CustomerDisplay : System.Web.UI.UserControl
{
    public bool DontBindOnLoad { get; set; }
    protected ESP.Finance.Entity.ProjectInfo projectInfo;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !DontBindOnLoad)
        {
            initCustomerInfo();
        }
    }

    private void initCustomerInfo()
    {
        ESP.Finance.Entity.CustomerTmpInfo c = null;

        int proejctId;
        if (int.TryParse(Request[ESP.Finance.Utility.RequestName.ProjectID], out proejctId) && proejctId > 0)
        {
            projectInfo = ESP.Finance.BusinessLogic.ProjectManager.GetModel(proejctId);
            c = projectInfo.Customer;
        }

        BindCustomerInfo(c);
    }

    public void BindCustomerInfo(ESP.Finance.Entity.CustomerTmpInfo c)
    {
        if (c == null)
        {
            this.divCustomer.Visible = false;
            this.Visible = false;
            return;
        }
        if(projectInfo == null)
            projectInfo = ESP.Finance.BusinessLogic.ProjectManager.GetModel(int.Parse(Request[ESP.Finance.Utility.RequestName.ProjectID]));

        ESP.Finance.Entity.CustomerInfo customer = ESP.Finance.BusinessLogic.CustomerManager.GetModel(c.CustomerID);

        this.divCustomer.Visible = (customer == null) ? AllDiff() : CompareCustomer(c, customer);
        BindData(c);

        //如果是手输的客户
        if (customer == null)
        {
            TrProxyHeader.Visible = false;
            TrProxyBody.Visible = false;
        }
        else//如果是从客户库中选择的
        {
            //获取客户的协议框架内容
            IList<ESP.Finance.Entity.CustomerAttachInfo> attachList = new List<ESP.Finance.Entity.CustomerAttachInfo>();
            if(!string.IsNullOrEmpty(projectInfo.CustomerAttachID))
                attachList = ESP.Finance.BusinessLogic.CustomerAttachManager.GetList(" attachID in (" + projectInfo.CustomerAttachID.Trim(',') + ")");
            //IList<ESP.Finance.Entity.CustomerAttachInfo> attachList = ESP.Finance.BusinessLogic.CustomerAttachManager.GetList(" CustomerID=" + (projectInfo.Customer == null ? 0 : projectInfo.Customer.CustomerID));
            if (attachList != null && attachList.Count > 0)//如果有框架协议
            {
                TrProxyHeader.Visible = true;
                TrProxyBody.Visible = true;
                gvProxy.DataSource = attachList;
                gvProxy.DataBind();
            }
            else//如果没有框架协议
            {
                TrProxyHeader.Visible = false;
                TrProxyBody.Visible = false;
            }
        }
    }

    private void BindData(ESP.Finance.Entity.CustomerTmpInfo c)
    {
        this.hidCustomerID.Value = c.CustomerTmpID.ToString();
        this.lblAddress1.Text = c.Address1;
        //this.lblAddress2.Text = c.Address2;
        this.lblArea.Text = c.AreaName;
        this.lblContact.Text = c.ContactName;
        this.lblContactFax.Text = c.ContactFax;
        this.lblContactMobile.Text = c.ContactMobile;
        this.lblContactPosition.Text = c.ContactPosition;
        this.lblEmail.Text = c.ContactEmail;
        this.lblIndustry.Text = c.IndustryName;
        this.lblInvoiceTitle.Text = c.InvoiceTitle;
        this.lblNameCN1.Text = c.NameCN1;
        //this.lblNameCN2.Text = c.NameCN2;
        //this.lblNameEN1.Text = c.NameEN1;
        //this.lblNameEN2.Text = c.NameEN2;
        //this.lblPostCode.Text = c.PostCode;
        //this.lblShortCN.Text = c.ShortCN;
        this.lblShortEN.Text = c.ShortEN;
        this.lblWebSite.Text = c.Website;
    }

    private bool AllDiff()
    {
        this.lblAddress1.ForeColor = Color.Red;
        //this.lblAddress2.ForeColor = Color.Red;
        this.lblArea.ForeColor = Color.Red;
        this.lblContact.ForeColor = Color.Red;
        this.lblContactFax.ForeColor = Color.Red;
        this.lblContactMobile.ForeColor = Color.Red;
        this.lblContactPosition.ForeColor = Color.Red;
        this.lblEmail.ForeColor = Color.Red;
        this.lblIndustry.ForeColor = Color.Red;
        this.lblInvoiceTitle.ForeColor = Color.Red;
        this.lblNameCN1.ForeColor = Color.Red;
        //this.lblNameCN2.ForeColor = Color.Red;
        //this.lblNameEN1.ForeColor = Color.Red;
        //this.lblNameEN2.ForeColor = Color.Red;
        //this.lblPostCode.ForeColor = Color.Red;
        //this.lblShortCN.ForeColor = Color.Red;
        this.lblShortEN.ForeColor = Color.Red;
        this.lblWebSite.ForeColor = Color.Red;
        return true;
    }

    private bool CompareCustomer(ESP.Finance.Entity.CustomerTmpInfo c, ESP.Finance.Entity.CustomerInfo customer)
    {
        int count = 0;
        if (c.Address1 != customer.Address1)
        {
            this.lblAddress1.ForeColor = Color.Red;
            count++;
        }
        //if (c.Address2 != customer.Address2)
        //{
        //    this.lblAddress2.ForeColor = Color.Red;
        //    count++;
        //}
        if (c.AreaName != customer.AreaName)
        {
            this.lblArea.ForeColor = Color.Red;
            count++;
        }
        if (c.ContactName != customer.ContactName)
        {
            this.lblContact.ForeColor = Color.Red;
            count++;
        }
        if (c.ContactFax != customer.ContactFax)
        {
            this.lblContactFax.ForeColor = Color.Red;
            count++;
        }
        if (c.ContactMobile != customer.ContactMobile)
        {
            this.lblContactMobile.ForeColor = Color.Red;
            count++;
        }
        if (c.ContactPosition != customer.ContactPosition)
        {
            this.lblContactPosition.ForeColor = Color.Red;
            count++;
        }
        if (c.ContactEmail != customer.ContactEmail)
        {
            this.lblEmail.ForeColor = Color.Red;
            count++;
        }
        if (c.IndustryName != customer.IndustryName)
        {
            this.lblIndustry.ForeColor = Color.Red;
            count++;
        }
        if (c.InvoiceTitle != customer.InvoiceTitle)
        {
            this.lblInvoiceTitle.ForeColor = Color.Red;
            count++;
        }
        if (c.NameCN1 != customer.NameCN1)
        {
            this.lblNameCN1.ForeColor = Color.Red;
            count++;
        }
        //if (c.NameCN2 != customer.NameCN2)
        //{
        //    this.lblNameCN2.ForeColor = Color.Red;
        //    count++;
        //}
        //if (c.NameEN1 != customer.NameEN1)
        //{
        //    this.lblNameEN1.ForeColor = Color.Red;
        //    count++;
        //}
        //if (c.NameEN2 != customer.NameEN2)
        //{
        //    this.lblNameEN2.ForeColor = Color.Red;
        //    count++;
        //}
        //if (c.PostCode != customer.PostCode)
        //{
        //    this.lblPostCode.ForeColor = Color.Red;
        //    count++;
        //}
        //if (c.ShortCN != customer.ShortCN)
        //{
        //    this.lblShortCN.ForeColor = Color.Red;
        //    count++;
        //}
        if (c.ShortEN != customer.ShortEN)
        {
            this.lblShortEN.ForeColor = Color.Red;
            count++;
        }
        if (c.Website != customer.Website)
        {
            this.lblWebSite.ForeColor = Color.Red;
            count++;
        }
        return count > 0;
    }

    protected void gvProxy_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label labDown = (Label)e.Row.FindControl("labDown");
            string attachId = gvProxy.DataKeys[e.Row.RowIndex].Value.ToString();
            labDown.Text = "<a target='_blank' href='/Dialogs/CustomerFileDownload.aspx?" + RequestName.CustomerAttachID + "=" + attachId + " '><img src='/images/ico_04.gif' border='0' /></a>";

        }
    }
}
