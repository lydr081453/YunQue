using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
public partial class Monitor_ProjectMonitorList : ESP.Web.UI.PageBase
{
    string term = string.Empty;
    List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Monitor_ProjectMonitorList));
        this.ddlBranch.Attributes.Add("onChange", "selectBranch(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");
       
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

            term = " and (projectCode is not null and projectCode <>'')";
           
            if (!string.IsNullOrEmpty(this.txtKey.Text.Trim()))
            {
                term += " and (serialcode like '%'+@serialcode+'%' or projectcode like '%'+@projectcode+'%' or GroupName like '%'+@GroupName+'%' or BranchName like '%'+@BranchName+'%' or BusinessTypeName like '%'+@BusinessTypeName+'%')";
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@serialcode", this.txtKey.Text.Trim()));
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@projectcode", this.txtKey.Text.Trim()));
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@GroupName", this.txtKey.Text.Trim()));
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@BranchName", this.txtKey.Text.Trim()));
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@BusinessTypeName", this.txtKey.Text.Trim()));

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
            if (!string.IsNullOrEmpty(this.hidBranchID.Value) && this.hidBranchID.Value != "-1")
            {
                term += " and BranchID =@BranchID";
                SqlParameter p4 = new SqlParameter("@BranchID", SqlDbType.Int, 4);
                p4.Value = this.hidBranchID.Value;
                paramlist.Add(p4);
            }
            this.gvG.DataSource = ESP.Finance.BusinessLogic.ProjectManager.GetList(term, paramlist);
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
            Label lblName = (Label)e.Row.FindControl("lblName");
            lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(projectmodel.ApplicantUserID) + "');");
      
            Label labState = ((Label)e.Row.FindControl("labState"));
            labState.Text = State.SetState(int.Parse(labState.Text));
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
    [AjaxPro.AjaxMethod]
    public static List<List<string>> GetBranchList()
    {
        IList<ESP.Finance.Entity.BranchInfo> blist = ESP.Finance.BusinessLogic.BranchManager.GetList(null, null);
        List<List<string>> list = new List<List<string>>();
        List<string> item = null;
        foreach (ESP.Finance.Entity.BranchInfo branch in blist)
        {
            item = new List<string>();
            item.Add(branch.BranchID.ToString());
            item.Add(branch.BranchName);
            list.Add(item);
        }
        List<string> c = new List<string>();
        c.Add("-1");
        c.Add("请选择...");
        list.Insert(0, c);
        return list;
    }
}
