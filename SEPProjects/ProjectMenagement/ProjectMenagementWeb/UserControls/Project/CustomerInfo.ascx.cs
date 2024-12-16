using System;
using System.Drawing;
using System.Collections.Generic;
using ExtExtenders;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Compatible;
using ESP.Finance.Utility;
using ESP.Finance.Entity;
public partial class UserControls_Project_CustomerInfo : System.Web.UI.UserControl
{
    private int _customerid;

    public int Customer_Id
    {
        get { return _customerid; }
        set { _customerid = value; }
    }

    private ESP.Finance.Entity.CustomerTmpInfo customerinfo;

    public ESP.Finance.Entity.CustomerTmpInfo CustomerModel
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
            int[] depts = CurrentUser.GetDepartmentIDs();
            if (depts != null && depts.Length > 0)
                return depts[0].ToString();
            else
                return null;
        }
    }
    protected ESP.Finance.Entity.ProjectInfo projectModel;
    protected IList<ESP.Finance.Entity.CustomerAttachInfo> attlist = new List<ESP.Finance.Entity.CustomerAttachInfo>();
    public string CustomerAttachID
    {
        get{
            if (hidCustomerAttachID.Value == "")
                return null;
            else
                return hidCustomerAttachID.Value;
        
        }
    }


    private string GetBranches()
    {
        string branches = ",";
        string strwhere = string.Format(" otherFinancialUsers like '%,{0},%'", CurrentUser.SysID);

        IList<ESP.Finance.Entity.BranchInfo> branchlist = ESP.Finance.BusinessLogic.BranchManager.GetList(strwhere);

        if (branchlist != null && branchlist.Count > 0)
        {
            foreach (ESP.Finance.Entity.BranchInfo branch in branchlist)
            {
                branches += branch.BranchCode.ToString() + ",";
            }

            return branches;
        }
        else
        {
            return string.Empty;
        }
    }


    public void InitCustomerInfo()
    {
        int count = 0;
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
        {
            projectModel =ESP.Finance.BusinessLogic.ProjectManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
            customerinfo = projectModel.Customer;
            hidContractStatusID.Value = projectModel.ContractStatusID.ToString();
            hidCustomerAttachID.Value = projectModel.CustomerAttachID == null ? "" : projectModel.CustomerAttachID.ToString();
            if(projectModel.ContractStatusID == int.Parse(ESP.Finance.Configuration.ConfigurationManager.FCAStatus))
            {
                CustomValidator1.Enabled = true;
                TrAttach.Visible = true;
            }
            
             string branches = this.GetBranches();


            //已经有项目号且不是财务人员，不能编辑
             if (!string.IsNullOrEmpty(projectModel.ProjectCode) && string.IsNullOrEmpty(branches))
            {
                txtShortEN.Enabled = false;
                txtCN1.Enabled = false;
                //txtCN2.Enabled = false;
                txtTitle.Enabled = false;
            }
        }
        else if (_customerid > 0)//
        {
            customerinfo = ESP.Finance.BusinessLogic.CustomerTmpManager.GetModel(_customerid);
        }
        else
        {
            return;
        }
        if (customerinfo != null)
        {
            ESP.Finance.Entity.CustomerInfo customer = ESP.Finance.BusinessLogic.CustomerManager.GetModel(customerinfo.CustomerID);
            if (customer != null)
            {
                if (customerinfo.Address1 != customer.Address1)
                {
                    this.txtAddress1.ForeColor = Color.Red;
                    count++;
                }
                //if (customerinfo.Address2 != customer.Address2)
                //{
                //    this.txtAddress2.ForeColor = Color.Red;
                //    count++;
                //}
                if (customerinfo.AreaName != customer.AreaName)
                {
                    this.txtArea.ForeColor = Color.Red;
                    count++;
                }
                if (customerinfo.ContactName != customer.ContactName)
                {
                    this.txtContact.ForeColor = Color.Red;
                    count++;
                }
                if (customerinfo.ContactFax != customer.ContactFax)
                {
                    this.txtContactFax.ForeColor = Color.Red;
                    count++;
                }
                if (customerinfo.ContactMobile != customer.ContactMobile)
                {
                    this.txtContactMobile.ForeColor = Color.Red;
                    count++;
                }
                if (customerinfo.ContactPosition != customer.ContactPosition)
                {
                    this.txtContactPosition.ForeColor = Color.Red;
                    count++;
                }
                if (customerinfo.ContactEmail != customer.ContactEmail)
                {
                    this.txtContactEmail.ForeColor = Color.Red;
                    count++;
                }
                if (customerinfo.IndustryName != customer.IndustryName)
                {
                    this.txtIndustry.ForeColor = Color.Red;
                    count++;
                }
                if (customerinfo.InvoiceTitle != customer.InvoiceTitle)
                {
                    this.txtTitle.ForeColor = Color.Red;
                    count++;
                }
                if (customerinfo.NameCN1 != customer.NameCN1)
                {
                    this.txtCN1.ForeColor = Color.Red;
                    count++;
                }
                //if (customerinfo.NameCN2 != customer.NameCN2)
                //{
                //    this.txtCN2.ForeColor = Color.Red;
                //    count++;
                //}
                //if (customerinfo.NameEN1 != customer.NameEN1)
                //{
                //    this.txtEN1.ForeColor = Color.Red;
                //    count++;
                //}
                //if (customerinfo.NameEN2 != customer.NameEN2)
                //{
                //    this.txtEN2.ForeColor = Color.Red;
                //    count++;
                //}
                //if (customerinfo.PostCode != customer.PostCode)
                //{
                //    this.txtPostCode.ForeColor = Color.Red;
                //    count++;
                //}
                //if (customerinfo.ShortCN != customer.ShortCN)
                //{
                //    this.txtShortCN.ForeColor = Color.Red;
                //    count++;
                //}
                if (customerinfo.ShortEN != customer.ShortEN)
                {
                    this.txtShortEN.ForeColor = Color.Red;
                    count++;
                }
                if (customerinfo.Website != customer.Website)
                {
                    this.txtWebSite.ForeColor = Color.Red;
                    count++;
                }
                if (count == 0)
                {
                    this.divCustomer.Visible = false;
                }
                txtShortEN.Enabled = false;
            }
          
            this.hidCustomerTmpID.Value = customerinfo.CustomerTmpID.ToString();
            hidCustomerID.Value = customerinfo.CustomerID.ToString();
            bindFrame();
            
            this.txtAddress1.Text = customerinfo.Address1;
            //this.txtAddress2.Text = customerinfo.Address2;
            this.txtArea.Text = customerinfo.AreaName;
            this.hidAreaCode.Value = customerinfo.AreaCode;
            this.hidAreaID.Value = customerinfo.AreaID.ToString();
            this.txtCN1.Text = customerinfo.NameCN1;
            //this.txtCN2.Text = customerinfo.NameCN2;
            this.txtContact.Text = customerinfo.ContactName;
            this.txtContactEmail.Text = customerinfo.ContactEmail;
            this.txtContactFax.Text = customerinfo.ContactFax;
            this.txtContactMobile.Text = customerinfo.ContactMobile;
            this.txtContactPosition.Text = customerinfo.ContactPosition;
            //this.txtEN1.Text = customerinfo.NameEN1;
            //this.txtEN2.Text = customerinfo.NameEN2;
            this.txtIndustry.Text = customerinfo.IndustryName;
            this.hidIndustryCode.Value = customerinfo.IndustryCode;
            this.hidIndustryID.Value = customerinfo.IndustryID.ToString();
            //this.txtPostCode.Text = customerinfo.PostCode;
            //this.txtShortCN.Text = customerinfo.ShortCN;
            txtShortEN.Text = hidShortEN.Value = customerinfo.ShortEN;
            txtTitle.Text = customerinfo.InvoiceTitle;
            txtWebSite.Text = customerinfo.Website;
            this.hidCustomerCode.Value = customerinfo.CustomerCode;
            //this.hidShortEN.Value = customerinfo.ShortEN;只有弹出页选客户给它赋值
        }
        else
        { this.divCustomer.Visible = false; }
    }

    public void setCustomerInfo()
    {
        ESP.Finance.Entity.CustomerInfo originalModel = null;
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
        {
            customerinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID])).Customer;
   
            if (customerinfo != null && customerinfo.CustomerID != 0)
            {
                originalModel = ESP.Finance.BusinessLogic.CustomerManager.GetModel(customerinfo.CustomerID);
            }

            if (originalModel==null && !string.IsNullOrEmpty(this.hidCustomerID.Value))
            {
                originalModel = ESP.Finance.BusinessLogic.CustomerManager.GetModel(int.Parse(this.hidCustomerID.Value));
            }

        }
        else if (_customerid > 0)//
        {
            customerinfo = ESP.Finance.BusinessLogic.CustomerTmpManager.GetModel(_customerid);
        }
        if (customerinfo == null)
            customerinfo = new ESP.Finance.Entity.CustomerTmpInfo();
         customerinfo.Address1=this.txtAddress1.Text ;
         //customerinfo.Address2 = this.txtAddress2.Text;
         customerinfo.AreaName = this.txtArea.Text;
         customerinfo.AreaCode = this.hidAreaCode.Value;
         if (!string.IsNullOrEmpty(this.hidAreaID.Value))
         {
             customerinfo.AreaID = Convert.ToInt32(this.hidAreaID.Value);
         }
         customerinfo.NameCN1 = this.txtCN1.Text.Trim();
         //customerinfo.NameCN2 = this.txtCN2.Text.Trim();
         customerinfo.ContactName = this.txtContact.Text.Trim();
         customerinfo.ContactEmail = this.txtContactEmail.Text.Trim();
         customerinfo.ContactFax = this.txtContactFax.Text.Trim();
         customerinfo.ContactMobile = this.txtContactMobile.Text.Trim();
         customerinfo.ContactPosition = this.txtContactPosition.Text.Trim();
         //customerinfo.NameEN1 = this.txtEN1.Text.Trim();
         //customerinfo.NameEN2 = this.txtEN2.Text.Trim();
         customerinfo.IndustryName = this.txtIndustry.Text.Trim();
         customerinfo.IndustryCode = this.hidIndustryCode.Value;
         if (!string.IsNullOrEmpty(this.hidIndustryID.Value))
         {
             customerinfo.IndustryID = Convert.ToInt32(this.hidIndustryID.Value);
         }
         if (!string.IsNullOrEmpty(this.hidCustomerID.Value))
         {
             customerinfo.CustomerID = Convert.ToInt32(this.hidCustomerID.Value);
         }
         if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
         {
             customerinfo.ProjectID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
         }
         //customerinfo.PostCode = this.txtPostCode.Text.Trim();
         //customerinfo.ShortCN = this.txtShortCN.Text.Trim();
         //if (originalModel != null)
         //    customerinfo.ShortEN = originalModel.ShortEN;
         //else
         if (!string.IsNullOrEmpty(hidShortEN.Value))
             customerinfo.ShortEN = hidShortEN.Value;
         else
             customerinfo.ShortEN = txtShortEN.Text.Trim();

         customerinfo.InvoiceTitle = txtTitle.Text.Trim();
         customerinfo.Website=txtWebSite.Text ;
         customerinfo.CustomerCode = this.hidCustomerCode.Value;
        
    }

    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label labDown = (Label)e.Row.FindControl("labDown");
            string attachId = gvG.DataKeys[e.Row.RowIndex].Value.ToString();
            labDown.Text ="<a target='_blank' href='/Dialogs/CustomerFileDownload.aspx?" + RequestName.CustomerAttachID + "=" + attachId + " '><img src='/images/ico_04.gif' border='0' /></a>";

            CustomerAttachInfo model = (CustomerAttachInfo)e.Row.DataItem;
            if(projectModel == null)
                projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
            if (model.ProjectId != projectModel.ProjectId || !string.IsNullOrEmpty(projectModel.ProjectCode))
            {
                e.Row.Cells[6].Text = "";
            }
        }
    }

    protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int attchId = int.Parse(e.CommandArgument.ToString());
            if (ESP.Finance.BusinessLogic.CustomerAttachManager.Delete(attchId) == ESP.Finance.Utility.DeleteResult.Succeed)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('删除成功！');", true);
                bindFrame();
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('删除失败！');", true);
            }
        }
    }

    private void bindFrame()
    {
        int cid = int.Parse(hidCustomerID.Value);
        string term = " customerid=" + cid;
        term += " and ((projectid=" + Request[ESP.Finance.Utility.RequestName.ProjectID] + " and status=" + (int)ESP.Finance.Utility.Common.CustomerAttachStatus.Saved + ")";
        term += " or status=" + (int)ESP.Finance.Utility.Common.CustomerAttachStatus.Used + ")";
        attlist = ESP.Finance.BusinessLogic.CustomerAttachManager.GetList(term);
        TrAttach.Visible = attlist.Count > 0 || int.Parse(hidContractStatusID.Value) == int.Parse(ESP.Finance.Configuration.ConfigurationManager.FCAStatus);
        if (attlist.Count == 0)
            hidCustomerAttachID.Value = "";
        this.gvG.DataSource = attlist;
        this.gvG.DataBind();
        //if (string.IsNullOrEmpty(txtShortEN.Text))
        //{
            txtShortEN.Text = hidShortEN.Value;
            txtShortEN.Enabled = false;
        //}
    }

    protected void btnBindFrame_Click(object sender, EventArgs e)
    {
        bindFrame();
    }
}
