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

public partial class Dialogs_IndustryDlg : System.Web.UI.Page
{
    
    private string clientId = "ctl00_ContentPlaceHolder1_CustomerInfo_";
    private static string term = string.Empty;
    private static List<SqlParameter> paramlist = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IList<ESP.Finance.Entity.CustomerIndustryInfo> cms = ESP.Finance.BusinessLogic.CustomerIndustryManager.GetList(term, paramlist);
            gvIndustry.DataSource = cms;
            this.gvIndustry.DataBind();
        }
         
    }

    protected void gvIndustry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            string cid = gvIndustry.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString();
            add(cid);
        }
    }
    protected void gvIndustry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
        }
    }

    protected void gvIndustry_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvIndustry.PageIndex = e.NewPageIndex;
        search();
    }

    private void add(string industryid)
    {
        ESP.Finance.Entity.CustomerIndustryInfo industry = ESP.Finance.BusinessLogic.CustomerIndustryManager.GetModel(int.Parse(industryid));
        if (!string.IsNullOrEmpty(Request["clientid"]))
        {
            clientId = Request["clientid"];
        }


        string strscript = "opener.document.getElementById('" + clientId + "hidIndustryID').value= '" + industry.IndustryID + "';";
        strscript += "opener.document.getElementById('" + clientId + "txtIndustry').value= '" + industry.CategoryName + "';";
        strscript += "opener.document.getElementById('" + clientId + "hidIndustryCode').value= '" + industry.IndustryCode + "';";
        strscript += "window.close(); ";
        ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), strscript, true);

    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        search();
    }

    private void search()
    {
        term = " IndustryCode like '%'+@IndustryCode+'%' or CategoryName like '%'+@CategoryName+'%' or Description like '%'+@Description+'%'";
        paramlist = new List<SqlParameter>();
        paramlist.Add(new SqlParameter("@IndustryCode", this.txtCode.Text.Trim()));
        paramlist.Add(new SqlParameter("@CategoryName", this.txtCode.Text.Trim()));
        paramlist.Add(new SqlParameter("@Description", this.txtCode.Text.Trim()));

        IList<ESP.Finance.Entity.CustomerIndustryInfo> cms = ESP.Finance.BusinessLogic.CustomerIndustryManager.GetList(term, paramlist);
        gvIndustry.DataSource = cms;
        this.gvIndustry.DataBind();
    }

    protected void btnClean_Click(object sender, EventArgs e)
    {
        term = string.Empty;
        paramlist = null;
        IList<ESP.Finance.Entity.CustomerIndustryInfo> cms = ESP.Finance.BusinessLogic.CustomerIndustryManager.GetList(term, paramlist);
        gvIndustry.DataSource = cms;
        this.gvIndustry.DataBind();
    }
}

