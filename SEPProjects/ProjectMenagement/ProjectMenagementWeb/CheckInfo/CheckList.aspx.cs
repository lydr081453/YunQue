using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CheckInfo_CheckList : ESP.Web.UI.PageBase
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
            term += "  (CheckSysCode like '%'+@CheckSysCode+'%' or CheckCode like '%'+@CheckCode+'%') and ";
            SqlParameter p = new SqlParameter("@CheckSysCode", SqlDbType.NVarChar, 50);
            p.SqlValue = this.txtKey.Text.Trim();
            paramlist.Add(p);
            SqlParameter p1 = new SqlParameter("@CheckCode", SqlDbType.NVarChar, 50);
            p1.SqlValue = this.txtKey.Text.Trim();
            paramlist.Add(p1);
        }
        if (this.ddlStatus.SelectedIndex != 0)
        {
            term += " CheckStatus=@CheckStatus and ";
            SqlParameter p2 = new SqlParameter("@CheckStatus", SqlDbType.Int, 4);
            p2.SqlValue = this.ddlStatus.SelectedValue;
            paramlist.Add(p2);
        }
         
        if(term!=string.Empty)
            term=term.Substring(0,term.Length-4);
        this.gvCheck.DataSource = ESP.Finance.BusinessLogic.CheckManager.GetList(term, paramlist);
        this.gvCheck.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }

    protected void lbNewCheck_Click(object sender, EventArgs e)
    {
        Response.Redirect("NewCheck.aspx");
    }

    protected void btnSearchAll_Click(object sender, EventArgs e)
    {
        this.txtKey.Text = string.Empty;
        Search();
    }

    protected void gvCheck_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCheck.PageIndex = e.NewPageIndex;
        Search();
    }


    protected void gvCheck_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvCheck_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Save")
        {
            int checkid = int.Parse(e.CommandArgument.ToString());
            ESP.Finance.Entity.CheckInfo model = ESP.Finance.BusinessLogic.CheckManager.GetModel(checkid);
            foreach (GridViewRow gvr in gvCheck.Rows)
            {
                if (gvr.Cells[0].Text == checkid.ToString())
                {
                    DropDownList ddlStatus = (DropDownList)gvr.FindControl("ddlStatus");
                    if (ddlStatus != null)
                    {
                        model.CheckStatus = ddlStatus.SelectedIndex;
                    }
                }
            }

           ESP.Finance.Utility.UpdateResult result =ESP.Finance.BusinessLogic.CheckManager.Update(model);
           if (result == ESP.Finance.Utility.UpdateResult.Succeed)
           {
               Search();
           }
           else
           {
               ClientScript.RegisterStartupScript(typeof(string), "", "alert('"+result.ToString()+"!');", true);
           }
        }
    }
}
