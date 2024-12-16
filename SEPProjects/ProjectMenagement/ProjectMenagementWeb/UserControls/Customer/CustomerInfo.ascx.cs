using System;
using System.Drawing;
using System.Collections.Generic;
using ExtExtenders;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Compatible;

public partial class UserControls_Customer_CustomerInfo : System.Web.UI.UserControl
{
    private int _customerid;

    public int Customer_Id
    {
        get { return _customerid; }
        set { _customerid = value; }
    }

    private ESP.Finance.Entity.CustomerInfo customerinfo;

    public ESP.Finance.Entity.CustomerInfo CustomerModel
    {
        get { return customerinfo; }
        set { customerinfo = value; }
    }
    private Employee emp = null;
    public Employee CurrentUser
    {
        get { return emp; }
        set { emp = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitCustomerInfo();
         
        }
        
    }

    protected string GetDept
    {
        get
        {
            int [] depts = CurrentUser.GetDepartmentIDs();
            if (depts != null && depts.Length > 0)
                return depts[0].ToString();
            else
                return null;
        }
    }
    
    public void InitCustomerInfo()
    {
        if (_customerid > 0)//
        {
            customerinfo = ESP.Finance.BusinessLogic.CustomerManager.GetModel(_customerid);
        }
        else
        {
            return;
        }
        if (customerinfo != null)
        {

            this.hidCustomerTmpID.Value = customerinfo.CustomerID.ToString();
            hidCustomerID.Value = customerinfo.CustomerID.ToString();
            this.txtAddress1.Text = customerinfo.Address1;
            this.txtAddress2.Text = customerinfo.Address2;
            this.txtArea.Text = customerinfo.AreaName;
            this.hidAreaCode.Value = customerinfo.AreaCode;
            this.hidAreaID.Value = customerinfo.AreaID.ToString();
            this.txtCN1.Text = customerinfo.NameCN1;
            this.txtCN2.Text = customerinfo.NameCN2;
            this.txtContact.Text = customerinfo.ContactName;
            this.txtContactEmail.Text = customerinfo.ContactEmail;
            this.txtContactFax.Text = customerinfo.ContactFax;
            this.txtContactMobile.Text = customerinfo.ContactMobile;
            this.txtContactPosition.Text = customerinfo.ContactPosition;
            this.txtEN1.Text = customerinfo.NameEN1;
            this.txtEN2.Text = customerinfo.NameEN2;
            this.txtIndustry.Text = customerinfo.IndustryName;
            this.hidIndustryCode.Value = customerinfo.IndustryCode;
            this.hidIndustryID.Value = customerinfo.IndustryID.ToString();
            this.txtPostCode.Text = customerinfo.PostCode;
            this.txtShortCN.Text = customerinfo.ShortCN;
            txtShortEN.Text = customerinfo.ShortEN;
            txtTitle.Text = customerinfo.InvoiceTitle;
            txtWebSite.Text = customerinfo.Website;
            this.hidCustomerCode.Value = customerinfo.CustomerCode;
        }
    }

    public void setCustomerInfo()
    {
  
        if (_customerid > 0)//
        {
            customerinfo = ESP.Finance.BusinessLogic.CustomerManager.GetModel(_customerid);
        }
        if (customerinfo == null)
            customerinfo = new ESP.Finance.Entity.CustomerInfo();
        customerinfo.Address1 = this.txtAddress1.Text;
        customerinfo.Address2 = this.txtAddress2.Text;
        customerinfo.AreaName = this.txtArea.Text.Trim();
         customerinfo.AreaCode = this.hidAreaCode.Value;
         if (!string.IsNullOrEmpty(this.hidAreaID.Value))
         {
             customerinfo.AreaID = Convert.ToInt32(this.hidAreaID.Value);
         }
         customerinfo.NameCN1 = this.txtCN1.Text.Trim();
         customerinfo.NameCN2 = this.txtCN2.Text.Trim();
         customerinfo.ContactName = this.txtContact.Text.Trim();
         customerinfo.ContactEmail = this.txtContactEmail.Text.Trim();
         customerinfo.ContactFax=this.txtContactFax.Text ;
         customerinfo.ContactMobile=this.txtContactMobile.Text ;
         customerinfo.ContactPosition=this.txtContactPosition.Text ;
         customerinfo.NameEN1 = this.txtEN1.Text.Trim();
         customerinfo.NameEN2 = this.txtEN2.Text.Trim();
         customerinfo.IndustryName=this.txtIndustry.Text ;
         customerinfo.IndustryCode = this.hidIndustryCode.Value;
         if (!string.IsNullOrEmpty(this.hidIndustryID.Value))
         {
             customerinfo.IndustryID = Convert.ToInt32(this.hidIndustryID.Value);
         }
         if (!string.IsNullOrEmpty(this.hidCustomerID.Value))
         {
             customerinfo.CustomerID = Convert.ToInt32(this.hidCustomerID.Value);
         }

         customerinfo.PostCode = this.txtPostCode.Text;
         customerinfo.ShortCN = this.txtShortCN.Text.Trim();
         customerinfo.ShortEN = txtShortEN.Text.Trim();
         customerinfo.InvoiceTitle = txtTitle.Text.Trim();
         customerinfo.Website=txtWebSite.Text ;
         customerinfo.CustomerCode = this.hidCustomerCode.Value;
        
    }
}
