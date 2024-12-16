using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.Utility;

public partial class Customer_CustomerDiffer : ESP.Web.UI.PageBase
{
    public int customerId = 0;
    private IList<ESP.Finance.Entity.CustomerAttachInfo> attlist;

  
    protected void Page_Load(object sender, EventArgs e)
    {
        string struser = string.Empty;
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.CustomerID]))
            {
                customerId = int.Parse(Request[ESP.Finance.Utility.RequestName.CustomerID]);

            }
            IList<ESP.Finance.Entity.BranchInfo> branchlist = ESP.Finance.BusinessLogic.BranchManager.GetList(null,null);
            struser = ",";
            foreach (ESP.Finance.Entity.BranchInfo model in branchlist)
            {
                struser += model.ProjectAccounter + ",";
            }
            struser += ESP.Finance.BusinessLogic.BranchManager.GetContractAccounters() + "," + ESP.Finance.BusinessLogic.BranchManager.GetFinalAccounters() + ",";
            if (struser.IndexOf("," + CurrentUser.SysID + ",") >= 0)
            {
                this.btnSubmit.Visible = true;
            }
            else
            {
                this.btnSubmit.Visible = false;
            }
            BindInfo();
            bindFrame(customerId);
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
            Label labDown = (Label)e.Row.FindControl("labDown");
            labDown.Text = attlist[e.Row.RowIndex].Attachment == null ? "" : "<a target='_blank' href='/Dialogs/CustomerFileDownload.aspx?" + RequestName.CustomerAttachID + "=" + attlist[e.Row.RowIndex].AttachID.ToString() + " '><img src='/images/ico_04.gif' border='0' /></a>";
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
        ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), "window.close();", true);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ESP.Finance.Entity.CustomerTmpInfo customer = ESP.Finance.BusinessLogic.CustomerTmpManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.CustomerID]));
         ESP.Finance.Entity.CustomerInfo c=null ;
        if (customer.CustomerID != 0)
        {
           c= ESP.Finance.BusinessLogic.CustomerManager.GetModel(customer.CustomerID);
        }
        else
        { c = new CustomerInfo(); }

        c.Address1 = customer.Address1;
        c.Address2 = customer.Address2;
        c.AreaID = customer.AreaID;
        c.AreaCode = customer.AreaCode;
        c.AreaName = customer.AreaName;
        c.ContactName = customer.ContactName;
        c.ContactFax = customer.ContactFax;
        c.ContactMobile = customer.ContactMobile;
        c.ContactPosition = customer.ContactPosition;
        c.ContactEmail = customer.ContactEmail;
        c.IndustryID = customer.IndustryID;
        c.IndustryCode = customer.IndustryCode;
        c.IndustryName = customer.IndustryName;
        c.InvoiceTitle = customer.InvoiceTitle;
        c.NameCN1 = customer.NameCN1;
        c.NameCN2 = customer.NameCN2;
        c.NameEN1 = customer.NameEN1;
        c.NameEN2 = customer.NameEN2;
        c.PostCode = customer.PostCode;
        c.ShortCN = customer.ShortCN;
        c.ShortEN = customer.ShortEN;
        c.Website = customer.Website;
        UpdateResult result=UpdateResult.Failed;
        if (customer.CustomerID == 0)
        {
            customer.CustomerID=ESP.Finance.BusinessLogic.CustomerManager.Add(c);
            ESP.Finance.BusinessLogic.CustomerTmpManager.Update(customer);
            result = UpdateResult.Succeed;
        }
        else
        {
            result = ESP.Finance.BusinessLogic.CustomerManager.Update(c);
        }
        if (result == UpdateResult.Succeed)
            ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), "alert('更新客户成功.');window.close();", true);
        else
        {
            ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), "alert('"+result.ToString()+"');", true);
        }
    }

    private void BindInfo()
    {
        ESP.Finance.Entity.CustomerTmpInfo customer = ESP.Finance.BusinessLogic.CustomerTmpManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.CustomerID]));
        if (customer != null)
        {
            ESP.Finance.Entity.CustomerInfo c = ESP.Finance.BusinessLogic.CustomerManager.GetModel(customer.CustomerID);
            if (c != null)
            {
                if (c.Address1 != customer.Address1)
                {
                    this.lblAddress1D.Text = customer.Address1 == "" ? "空数据" : customer.Address1;
                    this.lblAddress1D.Visible = true;
                }
                //if (c.Address2 != customer.Address2)
                //{
                //    this.lblAddress2D.Text = customer.Address2 == "" ? "空数据" : customer.Address2;
                //    this.lblAddress2D.Visible = true;
                //}
                if (c.AreaName != customer.AreaName)
                {
                    this.lblAreaD.Text = customer.AreaName == "" ? "空数据" : customer.AreaName;
                    this.lblAreaD.Visible = true;
                }
                if (c.ContactName != customer.ContactName)
                {
                    this.lblContactD.Text = customer.ContactName == "" ? "空数据" : customer.ContactName;
                    this.lblContactD.Visible = true;
                }
                if (c.ContactFax != customer.ContactFax)
                {
                    this.lblContactFaxD.Text = customer.ContactFax == "" ? "空数据" : customer.ContactFax;
                    this.lblContactFaxD.Visible = true;
                }
                if (c.ContactMobile != customer.ContactMobile)
                {
                    this.lblContactMobileD.Text = customer.ContactMobile == "" ? "空数据" : customer.ContactMobile;
                    this.lblContactMobileD.Visible = true;
                }
                if (c.ContactPosition != customer.ContactPosition)
                {
                    this.lblContactPositionD.Text = customer.ContactPosition == "" ? "空数据" : customer.ContactPosition;
                    this.lblContactPositionD.Visible = true;
                }
                if (c.ContactEmail != customer.ContactEmail)
                {
                    this.lblEmailD.Text = customer.ContactEmail == "" ? "空数据" : customer.ContactEmail;
                    this.lblEmailD.Visible = true;
                }
                if (c.IndustryName != customer.IndustryName)
                {
                    this.lblIndustryD.Text = customer.IndustryName == "" ? "空数据" : customer.IndustryName;
                    this.lblIndustryD.Visible = true;
                }
                if (c.InvoiceTitle != customer.InvoiceTitle)
                {
                    this.lblInvoiceTitleD.Text = customer.InvoiceTitle == "" ? "空数据" : customer.InvoiceTitle;
                    this.lblInvoiceTitleD.Visible = true;
                }
                if (c.NameCN1 != customer.NameCN1)
                {
                    this.lblNameCN1D.Text = customer.NameCN1 == "" ? "空数据" : customer.NameCN1;
                    this.lblNameCN1D.Visible = true;
                }
                //if (c.NameCN2 != customer.NameCN2)
                //{
                //    this.lblNameCN2D.Text = customer.NameCN2 == "" ? "空数据" : customer.NameCN2;
                //    this.lblNameCN2D.Visible = true;
                //}
                //if (c.NameEN1 != customer.NameEN1)
                //{
                //    this.lblNameEN1D.Text = customer.NameEN1 == "" ? "空数据" : customer.NameEN1;
                //    this.lblNameEN1D.Visible = true;
                //}
                //if (c.NameEN2 != customer.NameEN2)
                //{
                //    this.lblNameEN2D.Text = customer.NameEN2 == "" ? "空数据" : customer.NameEN2;
                //    this.lblNameEN2D.Visible = true;
                //}
                //if (c.PostCode != customer.PostCode)
                //{
                //    this.lblPostCodeD.Text = customer.PostCode == "" ? "空数据" : customer.PostCode;
                //    this.lblPostCodeD.Visible = true;
                //}
                //if (c.ShortCN != customer.ShortCN)
                //{
                //    this.lblShortCND.Text = customer.ShortCN == "" ? "空数据" : customer.ShortCN;
                //    this.lblShortCND.Visible = true;
                //}
                if (c.ShortEN != customer.ShortEN)
                {
                    this.lblShortEND.Text = customer.ShortEN == "" ? "空数据" : customer.ShortEN;
                    this.lblShortEND.Visible = true;
                }
                if (c.Website != customer.Website)
                {
                    this.lblWebSiteD.Text = customer.Website == "" ? "空数据" : customer.Website;
                    this.lblWebSiteD.Visible = true;
                }

                // 正式客户信息
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
            else
            {
                this.lblAddress1D.Text = customer.Address1;
                this.lblAddress1D.Visible = true;
                //this.lblAddress2D.Text = customer.Address2;
                //this.lblAddress2D.Visible = true;
          
                this.lblAreaD.Text = customer.AreaName;
                this.lblAreaD.Visible = true;
          
                this.lblContactD.Text = customer.ContactName;
                this.lblContactD.Visible = true;
           
                this.lblContactFaxD.Text = customer.ContactFax;
                this.lblContactFaxD.Visible = true;
          
                this.lblContactMobileD.Text = customer.ContactMobile;
                this.lblContactMobileD.Visible = true;
          
                this.lblContactPositionD.Text = customer.ContactPosition;
                this.lblContactPositionD.Visible = true;
          
                this.lblEmailD.Text = customer.ContactEmail ;
                this.lblEmailD.Visible = true;
            
                this.lblIndustryD.Text = customer.IndustryName;
                this.lblIndustryD.Visible = true;
            
                this.lblInvoiceTitleD.Text = customer.InvoiceTitle;
                this.lblInvoiceTitleD.Visible = true;
            
                this.lblNameCN1D.Text = customer.NameCN1;
                this.lblNameCN1D.Visible = true;
            
                //this.lblNameCN2D.Text = customer.NameCN2;
                //this.lblNameCN2D.Visible = true;
           
                //this.lblNameEN1D.Text = customer.NameEN1;
                //this.lblNameEN1D.Visible = true;
            
                //this.lblNameEN2D.Text = customer.NameEN2;
                //this.lblNameEN2D.Visible = true;
            
                //this.lblPostCodeD.Text = customer.PostCode;
                //this.lblPostCodeD.Visible = true;
            
                //this.lblShortCND.Text = customer.ShortCN;
                //this.lblShortCND.Visible = true;
            
                this.lblShortEND.Text =  customer.ShortEN;
                this.lblShortEND.Visible = true;
           
                this.lblWebSiteD.Text = customer.Website;
                this.lblWebSiteD.Visible = true;
            }
        }
    }

}
