using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InvoiceInfo_InvoiceList : System.Web.UI.Page
{
    string term = string.Empty;
    List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Search();
        }
    }

    private void Search()
    {
        term = string.Empty;
        paramlist.Clear();
        if (!string.IsNullOrEmpty(this.txtKey.Text.Trim()))
        {
            term += "  (InvoiceCode like '%'+@InvoiceCode+'%' ) and ";
            SqlParameter p = new SqlParameter("@InvoiceCode", SqlDbType.NVarChar, 50);
            p.SqlValue = this.txtKey.Text.Trim();
            paramlist.Add(p);
        }
        if (this.ddlStatus.SelectedIndex != 0)
        {
            term += " InvoiceStatus=@InvoiceStatus and ";
            SqlParameter p2 = new SqlParameter("@InvoiceStatus", SqlDbType.Int, 4);
            p2.SqlValue = this.ddlStatus.SelectedValue;
            paramlist.Add(p2);
        }

        if (term != string.Empty)
            term = term.Substring(0, term.Length - 4);
        this.gvInvoice.DataSource = ESP.Finance.BusinessLogic.InvoiceManager.GetList(term, paramlist);
        this.gvInvoice.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }

    protected void lbNewInvoice_Click(object sender, EventArgs e)
    {
        Response.Redirect("NewInvoice.aspx");
    }

    protected void btnSearchAll_Click(object sender, EventArgs e)
    {
        this.txtKey.Text = string.Empty;
        Search();
    }

    protected void gvInvoice_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInvoice.PageIndex = e.NewPageIndex;
        Search();
    }


    protected void gvInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            System.Web.UI.WebControls.DropDownList ddlStatus = (System.Web.UI.WebControls.DropDownList)e.Row.FindControl("ddlStatus");
            if (ddlStatus != null)
            {
                ddlStatus.SelectedValue = e.Row.Cells[1].Text;
            }
        }
    }

    protected void gvInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Save")
        {
            int invoiceid = int.Parse(e.CommandArgument.ToString());
            ESP.Finance.Entity.InvoiceInfo model = ESP.Finance.BusinessLogic.InvoiceManager.GetModel(invoiceid);
            foreach (GridViewRow gvr in gvInvoice.Rows)
            {
                if (gvr.Cells[0].Text == invoiceid.ToString())
                {
                    DropDownList ddlStatus = (DropDownList)gvr.FindControl("ddlStatus");
                    if (ddlStatus != null)
                    {
                        model.InvoiceStatus = ddlStatus.SelectedIndex;
                    }
                }
            }

            ESP.Finance.Utility.UpdateResult result = ESP.Finance.BusinessLogic.InvoiceManager.Update(model);
            if (result == ESP.Finance.Utility.UpdateResult.Succeed)
            {
                Search();
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + result.ToString() + "!');", true);
            }
        }
    }
}
