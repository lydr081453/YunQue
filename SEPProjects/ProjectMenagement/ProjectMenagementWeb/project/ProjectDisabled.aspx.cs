using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using ESP.Finance.BusinessLogic;
using System.Data.SqlClient;

public partial class project_ProjectDisabled : ESP.Web.UI.PageBase
{
    string term = string.Empty;
    List<SqlParameter> paramlist = new List<SqlParameter>();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Search();
        }
    }


    protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvG.PageIndex = e.NewPageIndex;
        Search();
    }

    private void Search()
    {
        term = " and Status=" + (int)ESP.Finance.Utility.Status.FinanceAuditComplete;

        paramlist.Clear();
        if (term != string.Empty)
        {
            if (!string.IsNullOrEmpty(this.txtKey.Text.Trim()))
            {
                term += " and (serialcode like '%'+@serialcode+'%' or projectcode like '%'+@projectcode+'%' or GroupName like '%'+@GroupName+'%' or BranchName like '%'+@BranchName+'%' or BusinessTypeName like '%'+@BusinessTypeName+'%')";
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@serialcode", this.txtKey.Text.Trim()));
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@projectcode", this.txtKey.Text.Trim()));
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@GroupName", this.txtKey.Text.Trim()));
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@BranchName", this.txtKey.Text.Trim()));
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@BusinessTypeName", this.txtKey.Text.Trim()));

            }
            this.gvG.DataSource = ESP.Finance.BusinessLogic.ProjectManager.GetList(term, paramlist);
            this.gvG.DataBind();
        }
    }

    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ESP.Finance.Entity.ProjectInfo projectmodel = (ESP.Finance.Entity.ProjectInfo)e.Row.DataItem;

        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label labState = ((Label)e.Row.FindControl("labState"));
            labState.Text = State.SetState(int.Parse(labState.Text));
            Label lblName = (Label)e.Row.FindControl("lblName");
            lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(projectmodel.ApplicantUserID) + "');");

        }
    }

    protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Disabled")
        {
            int projectId = int.Parse(e.CommandArgument.ToString());
            if (ESP.Finance.BusinessLogic.ProjectManager.ProjectDisabled(projectId, Request))
            {
                Search();
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('暂停项目成功！');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('暂停项目失败！');", true);
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }
    protected void btnSearchAll_Click(object sender, EventArgs e)
    {
        this.txtKey.Text = string.Empty;
        Search();
    }
}
