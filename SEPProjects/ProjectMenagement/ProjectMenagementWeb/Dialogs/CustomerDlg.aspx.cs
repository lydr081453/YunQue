using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.BusinessLogic;
using ExtExtenders;
using ESP.Finance.Utility;

public partial class Dialogs_CustomerDlg : System.Web.UI.Page
{
    private string clientId = "ctl00_ContentPlaceHolder1_CustomerInfo_";
    private static string  term = string.Empty;
    private static List<SqlParameter> paramlist = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          
            IList<ESP.Finance.Entity.CustomerInfo> cms = ESP.Finance.BusinessLogic.CustomerManager.GetList(term, paramlist);
            gvCustomer.DataSource = cms;
            this.gvCustomer.DataBind();
        }
         
    }
    protected void gvCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            string cid = gvCustomer.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString();
            add(cid);
        }
    }


    protected void gvCustomer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
            //e.Row.Cells[10].Visible = false;
            //e.Row.Cells[13].Visible = false;
        }
    }

    protected void gvCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvCustomer.PageIndex = e.NewPageIndex;
        search();
    }

    private void add(string customerid)
    {
        
        ESP.Finance.Entity.CustomerInfo customer = ESP.Finance.BusinessLogic.CustomerManager.GetModel(int.Parse(customerid));
        string strscript = "";
        strscript += "opener.document.getElementById('" + clientId + "hidCustomerID').value= '" + customer.CustomerID + "';";
        strscript += "opener.document.getElementById('" + clientId + "hidShortEN').value= '" + customer.ShortEN + "';";
        strscript += "opener.document.getElementById('" + clientId + "txtShortEN').value= '" + customer.ShortEN + "';";
        strscript += "opener.document.getElementById('" + clientId + "txtShortEN').disabled=true;";
        //strscript += "opener.document.getElementById('" + clientId + "txtShortCN').value= '" + customer.ShortCN + "';";
        strscript += "opener.document.getElementById('" + clientId + "txtCN1').value= '" + customer.NameCN1 + "';";
        //strscript += "opener.document.getElementById('" + clientId + "txtCN2').value= '" + customer.NameCN2 + "';";
        strscript += "opener.document.getElementById('" + clientId + "txtTitle').value= '" + customer.InvoiceTitle + "';";
        //strscript += "opener.document.getElementById('" + clientId + "txtEN1').value= '" + customer.NameEN1 + "';";
        //strscript += "opener.document.getElementById('" + clientId + "txtEN2').value= '" + customer.NameEN2 + "';";
        strscript += "opener.document.getElementById('" + clientId + "txtAddress1').value= '" + customer.Address1 + "';";
        //strscript += "opener.document.getElementById('" + clientId + "txtAddress2').value= '" + customer.Address2 + "';";
        //strscript += "opener.document.getElementById('" + clientId + "txtPostCode').value= '" + customer.PostCode + "';";
        strscript += "opener.document.getElementById('" + clientId + "txtWebSite').value= '" + customer.Website + "';";
        strscript += "opener.document.getElementById('" + clientId + "hidAreaID').value= '" + customer.AreaID + "';";
        strscript += "opener.document.getElementById('" + clientId + "hidAreaCode').value= '" + customer.AreaCode + "';";
        strscript += "opener.document.getElementById('" + clientId + "txtArea').value= '" + customer.AreaName + "';";
        strscript += "opener.document.getElementById('" + clientId + "hidCustomerCode').value= '" + customer.CustomerCode + "';";
        strscript += "opener.document.getElementById('" + clientId + "hidIndustryID').value= '" + customer.IndustryID + "';";
        strscript += "opener.document.getElementById('" + clientId + "hidIndustryCode').value= '" + customer.IndustryCode + "';";
        strscript += "opener.document.getElementById('" + clientId + "txtIndustry').value= '" + customer.IndustryName + "';";
        //contact information
        strscript += "opener.document.getElementById('" + clientId + "txtContact').value= '" + customer.ContactName + "';";
        strscript += "opener.document.getElementById('" + clientId + "txtContactPosition').value= '" + customer.ContactPosition + "';";
        strscript += "opener.document.getElementById('" + clientId + "txtContactMobile').value= '" + customer.ContactMobile + "';";
        strscript += "opener.document.getElementById('" + clientId + "txtContactFax').value= '" + customer.ContactFax + "';";
        strscript += "opener.document.getElementById('" + clientId + "txtContactEmail').value= '" + customer.ContactEmail + "';";
        strscript += "opener.document.getElementById('" + clientId + "hidRebateRate').value= '" + customer.RebateRate + "';";
        if (!string.IsNullOrEmpty(Request["s"]) && Request["s"] == "fc")
            strscript += "opener.bindFrame();";
        strscript += "window.close(); ";
        ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), strscript, true);
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        ESP.Finance.Entity.ProjectInfo projectmodel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
        int cid = projectmodel.CustomerID == null ? 0 : projectmodel.CustomerID.Value;
        if(cid>0)
        ESP.Finance.BusinessLogic.CustomerTmpManager.Delete(cid);

        string strscript = "";
        strscript += "opener.document.getElementById('" + clientId + "hidCustomerID').value= '';";
        strscript += "opener.document.getElementById('" + clientId + "hidShortEN').value= '';";
        strscript += "opener.document.getElementById('" + clientId + "txtShortEN').value= '';";
        strscript += "opener.document.getElementById('" + clientId + "txtShortEN').disabled=false;";
        //strscript += "opener.document.getElementById('" + clientId + "txtShortCN').value= '';";
        strscript += "opener.document.getElementById('" + clientId + "txtCN1').value= '';";
        //strscript += "opener.document.getElementById('" + clientId + "txtCN2').value= '';";
        strscript += "opener.document.getElementById('" + clientId + "txtTitle').value= '';";
        //strscript += "opener.document.getElementById('" + clientId + "txtEN1').value= '';";
        //strscript += "opener.document.getElementById('" + clientId + "txtEN2').value= '';";
        strscript += "opener.document.getElementById('" + clientId + "txtAddress1').value= '';";
        //strscript += "opener.document.getElementById('" + clientId + "txtAddress2').value= '';";
        //strscript += "opener.document.getElementById('" + clientId + "txtPostCode').value= '';";
        strscript += "opener.document.getElementById('" + clientId + "txtWebSite').value= '';";
        strscript += "opener.document.getElementById('" + clientId + "hidAreaID').value= '';";
        strscript += "opener.document.getElementById('" + clientId + "hidAreaCode').value= '';";
        strscript += "opener.document.getElementById('" + clientId + "txtArea').value= '';";
        strscript += "opener.document.getElementById('" + clientId + "hidCustomerCode').value= '';";
        strscript += "opener.document.getElementById('" + clientId + "hidIndustryID').value= '';";
        strscript += "opener.document.getElementById('" + clientId + "hidIndustryCode').value= '';";
        strscript += "opener.document.getElementById('" + clientId + "txtIndustry').value= '';";
        strscript += "opener.document.getElementById('" + clientId + "txtContact').value= '';";
        strscript += "opener.document.getElementById('" + clientId + "txtContactPosition').value= '';";
        strscript += "opener.document.getElementById('" + clientId + "txtContactMobile').value= '';";
        strscript += "opener.document.getElementById('" + clientId + "txtContactFax').value= '';";
        strscript += "opener.document.getElementById('" + clientId + "txtContactEmail').value= '';";
        strscript += "opener.document.getElementById('" + clientId + "hidRebateRate').value= '';";
        strscript += "window.close(); ";

        ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), strscript, true);
         }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        search();
    }

    private void search()
    {
        term = "  CustomerCode like '%'+@CustomerCode+'%' or ShortEN like '%'+@ShortEN+'%' or NameCN1 like '%'+@NameCN1+'%' or  NameEN1 like '%'+@NameEN1+'%' or IndustryCode like '%'+@IndustryCode+'%' or  IndustryName like '%'+@IndustryName+'%' or AreaCode  like '%'+@AreaCode+'%' or AreaName like '%'+@AreaName+'%' ";
        paramlist = new List<SqlParameter>();
        paramlist.Add(new SqlParameter("@CustomerCode", this.txtCode.Text.Trim()));
        paramlist.Add(new SqlParameter("@ShortEN", this.txtCode.Text.Trim()));
        paramlist.Add(new SqlParameter("@NameCN1", this.txtCode.Text.Trim()));
        paramlist.Add(new SqlParameter("@NameEN1", this.txtCode.Text.Trim()));
        paramlist.Add(new SqlParameter("@IndustryCode", this.txtCode.Text.Trim()));
        paramlist.Add(new SqlParameter("@IndustryName", this.txtCode.Text.Trim()));
        paramlist.Add(new SqlParameter("@AreaCode", this.txtCode.Text.Trim()));
        paramlist.Add(new SqlParameter("@AreaName", this.txtCode.Text.Trim()));

        IList<ESP.Finance.Entity.CustomerInfo> cms = ESP.Finance.BusinessLogic.CustomerManager.GetList(term, paramlist);
        gvCustomer.DataSource = cms;
        this.gvCustomer.DataBind();
    }
    protected void btnClean_Click(object sender, EventArgs e)
    {
        term = string.Empty;
        paramlist = null;
        IList<ESP.Finance.Entity.CustomerInfo> cms = ESP.Finance.BusinessLogic.CustomerManager.GetList(term, paramlist);
        gvCustomer.DataSource = cms;
        this.gvCustomer.DataBind();
    }
}
