using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using System.Data.SqlClient;
using ESP.Finance.BusinessLogic;

public partial class Monitor_SupporterMonitorList : ESP.Web.UI.PageBase
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
        term = string.Empty;
        paramlist.Clear();
        term = " Status=@Status and (supportercode like '%'+@key+'%' or projectcode like '%'+@key+'%' or GroupName like '%'+@key+'%' or ServiceType like '%'+ @key +'%')";
        SqlParameter p = new SqlParameter("@Status", System.Data.SqlDbType.Int, 4);
        p.Value = (int)ESP.Finance.Utility.Status.FinanceAuditComplete;
        paramlist.Add(p);
        SqlParameter p2= new SqlParameter("@key",System.Data.SqlDbType.NVarChar,50);
        p2.Value= this.txtKey.Text.Trim();
        paramlist.Add(p2);
        this.gvG.DataSource = ESP.Finance.BusinessLogic.SupporterManager.GetList(term, paramlist);
        this.gvG.DataBind();
    }

    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ESP.Finance.Entity.SupporterInfo supporterModel = (ESP.Finance.Entity.SupporterInfo)e.Row.DataItem;
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label labState = ((Label)e.Row.FindControl("labState"));
            labState.Text = State.SetState(int.Parse(labState.Text));
                Label lblName = (Label)e.Row.FindControl("lblName");
                lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(supporterModel.LeaderUserID.Value) + "');");

            
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

