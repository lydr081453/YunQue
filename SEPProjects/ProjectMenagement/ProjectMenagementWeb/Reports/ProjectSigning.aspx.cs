using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using ESP.Finance.Entity;
public partial class Reports_ProjectSigning : ESP.Web.UI.PageBase
{
    string term = string.Empty;
    List<SqlParameter> paramlist = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ddlBranch.Attributes.Add("onChange", "selectBranch(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");
        if (!IsPostBack)
        {
            BindBranch();
            this.gvG.DataSource = Search();
            this.gvG.DataBind();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.gvG.DataSource = Search();
        this.gvG.DataBind();
    }

    private IList<ESP.Finance.Entity.ProjectInfo>  Search()
    {
        term = string.Empty;
        paramlist = new List<SqlParameter>();

        term = " and projectCode<>'' and TotalAmount<>0 ";

        if (!string.IsNullOrEmpty(this.txtKey.Text))
        {
            term += " and (serialcode like '%'+@key+'%' or projectcode like '%'+@key+'%' or GroupName like '%'+@key+'%' or BranchName like '%'+@key+'%' or BusinessTypeName like '%'+@key+'%')";
            SqlParameter p = new SqlParameter("@Key", SqlDbType.NVarChar, 50);
            p.Value = this.txtKey.Text.Trim();
            paramlist.Add(p);
        }
        if (!string.IsNullOrEmpty(this.txtBeginDate.Text) && !string.IsNullOrEmpty(this.txtEndDate.Text))
        {
            term += " and SubmitDate between @BeginDate and @EndDate";
            SqlParameter p2 = new SqlParameter("@BeginDate", SqlDbType.DateTime, 8);
            p2.Value = this.txtBeginDate.Text.Trim();
            paramlist.Add(p2);

            SqlParameter p3 = new SqlParameter("@EndDate", SqlDbType.DateTime, 8);
            p3.Value = Convert.ToDateTime(this.txtEndDate.Text.Trim()).ToString("yyyy-MM-dd") + " 23:59:59";
            paramlist.Add(p3);
        }
        if (this.ddlBranch.SelectedItem.Value != "-1")
        {
            term += " and BranchID =@BranchID";
            SqlParameter p4 = new SqlParameter("@BranchID", SqlDbType.Int, 4);
            p4.Value = this.ddlBranch.SelectedItem.Value;
            paramlist.Add(p4);
        }
        return  ESP.Finance.BusinessLogic.ProjectManager.GetList(term, paramlist);

    }

    protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvG.PageIndex = e.NewPageIndex;
        this.gvG.DataSource = Search();
        this.gvG.DataBind();
    }
    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ESP.Finance.Entity.ProjectInfo projectmodel = (ESP.Finance.Entity.ProjectInfo)e.Row.DataItem;

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
            lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(projectmodel.ApplicantUserID) + "');");
        }
    }
    protected void btnSearchAll_Click(object sender, EventArgs e)
    {
        this.txtEndDate.Text = "";
        this.txtBeginDate.Text = "";
        this.txtKey.Text = "";
        this.gvG.DataSource = Search();
        this.gvG.DataBind();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        DateTime beginDate = new DateTime(2024, 1, 1);
        DateTime endDate = DateTime.Now;
        if (!string.IsNullOrEmpty(this.txtEndDate.Text) && !string.IsNullOrEmpty(this.txtBeginDate.Text))
        {
            beginDate = Convert.ToDateTime(this.txtBeginDate.Text);
            endDate = Convert.ToDateTime(this.txtEndDate.Text);
        }
        else
        {
            beginDate = new DateTime(DateTime.Now.Year, 1, 1);
            endDate = new DateTime(DateTime.Now.Year, 12, 31);
        }
        string title = string.Empty;
        if (this.ddlBranch.SelectedItem.Value != "-1")
        {
            title = this.ddlBranch.SelectedItem.Text;
        }
        try
        {
            ESP.Finance.BusinessLogic.ProjectManager.GetProjectSigning(beginDate, endDate, Search(), Response);
            GC.Collect();
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('" + ex.Message + "');", true);
        }
    }


    private void BindBranch()
    {
        IList<ESP.Finance.Entity.BranchInfo> blist = ESP.Finance.BusinessLogic.BranchManager.GetList(null, null);
        this.ddlBranch.DataSource = blist;
        this.ddlBranch.DataTextField = "BranchName";
        this.ddlBranch.DataValueField = "BranchID";
        this.ddlBranch.DataBind();

        ListItem list = new ListItem();
        list.Value = "-1";
        list.Text = "请选择...";
        ddlBranch.Items.Insert(0, list);
    }
}

