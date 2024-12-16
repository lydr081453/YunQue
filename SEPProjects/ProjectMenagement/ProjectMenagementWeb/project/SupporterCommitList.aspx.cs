using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using System.Data.SqlClient;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Entity;

public partial class project_SupporterCommitList : ESP.Web.UI.PageBase
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

    private string GetUser()
    {
        string user = "," + ESP.Finance.BusinessLogic.BranchManager.GetProjectAccounters() + ",";
        user += ESP.Finance.BusinessLogic.BranchManager.GetContractAccounters() + "," + ESP.Finance.BusinessLogic.BranchManager.GetFinalAccounters() + ","+ESP.Finance.BusinessLogic.BranchManager.GetProjectAccounters()+",";
        return user;
    }

    private void Search()
    {
        term = string.Empty;
        paramlist.Clear();
        string users = GetUser();// ConfigurationManager.ContractAdmin + "," + ConfigurationManager.FinancialAdmin1 + "," + ConfigurationManager.FinancialAdmin2 + "," + ESP.Finance.BusinessLogic.BranchManager.GetFinalAccounters();

        if (users.IndexOf("," + CurrentUserID.ToString() + ",") >= 0)
        {
            term = "  Status not in(" + (int)Status.Saved + "," + (int)Status.FinanceAuditComplete + ")";
        }
        else
        {
            term = "  Status not in (" + (int)Status.Saved + "," + (int)Status.FinanceAuditComplete + ") and (SupportID in(select SupportID from F_SupportMember where MemberUserID=@memberuserid) or applicantUserID=" + CurrentUser.SysID + " or leaderuserID=" + CurrentUser.SysID + " or supportid in(select supporterid from F_SupporterAuditHist where auditoruserid = " + CurrentUser.SysID + "))";
            SqlParameter p1 = new SqlParameter("@memberuserid", System.Data.SqlDbType.Int, 4);
            p1.Value = CurrentUserID;
            paramlist.Add(p1);
        }

        if (term != string.Empty)
        {
            if (!string.IsNullOrEmpty(this.txtKey.Text.Trim()))
            {
                term += " and (supportercode like '%'+@key+'%' or ApplicantEmployeeName like '%'+@key+'%' or projectcode like '%'+@key+'%' or GroupName like '%'+@key+'%' or ServiceType like '%'+@key+'%' or ServiceDescription like '%'+@key+'%')";
                SqlParameter p3 = new SqlParameter("@key", System.Data.SqlDbType.NVarChar, 50);
                p3.Value = this.txtKey.Text.Trim();
                paramlist.Add(p3);
            }
            this.gvG.DataSource = ESP.Finance.BusinessLogic.SupporterManager.GetList(term, paramlist);
            this.gvG.DataBind();
        }
    }

    protected void lbNewProject_Click(object sender, EventArgs e)
    {
        Response.Redirect("SupporterEdit.aspx");
    }

    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ESP.Finance.Entity.SupporterInfo supporterModel = (ESP.Finance.Entity.SupporterInfo)e.Row.DataItem;
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            SupporterInfo support = (SupporterInfo)e.Row.DataItem;
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label labState = ((Label)e.Row.FindControl("labState"));
            labState.Text = State.SetState(int.Parse(labState.Text));

            Label labDate = ((Label)e.Row.FindControl("labDate"));
            if (labDate != null && labDate.Text != string.Empty)
                labDate.Text = Convert.ToDateTime(labDate.Text).ToString("yyyy-MM-dd");
            Label lblName = (Label)e.Row.FindControl("lblName");
            lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(supporterModel.LeaderUserID.Value) + "');");
            //所有部门级联字符串拼接
            Label lblGroupName = (Label)e.Row.FindControl("lblGroupName");
            List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
            depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(supporterModel.GroupID.Value, depList);
            string groupname = string.Empty;
            foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
            {
                groupname += dept.DepartmentName + "-";
            }
            if (!string.IsNullOrEmpty(groupname))
            lblGroupName.Text = groupname.Substring(0, groupname.Length - 1);
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