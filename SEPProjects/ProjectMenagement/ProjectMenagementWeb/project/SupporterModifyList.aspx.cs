using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using ESP.Finance.BusinessLogic;

public partial class project_SupporterModifyList : ESP.Web.UI.PageBase
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
            term = " Status=@Status";
            paramlist.Add(new SqlParameter("@Status", ESP.Finance.Utility.Status.FinanceAuditComplete));
        }
        else
        {
            term = " Status=@Status and (SupportID in(select SupportID from F_SupportMember where memberuserid=@memberuserid) or supportid in(select supporterid from F_SupporterAuditHist where auditoruserid = " + CurrentUser.SysID + "))";
            paramlist.Add(new SqlParameter("@Status", ESP.Finance.Utility.Status.FinanceAuditComplete));
            paramlist.Add(new SqlParameter("@memberuserid", CurrentUserID));
        }

        if (term != string.Empty)
        {
            if (!string.IsNullOrEmpty(this.txtKey.Text.Trim()))
            {
                term += " and (projectcode like '%'+@projectcode+'%' or GroupName like '%'+@GroupName+'%' or ServiceType like '%'+ @ServiceType +'%')";
                paramlist.Add(new SqlParameter("@projectcode", this.txtKey.Text.Trim()));
                paramlist.Add(new SqlParameter("@GroupName", this.txtKey.Text.Trim()));
                paramlist.Add(new SqlParameter("@ServiceType", this.txtKey.Text.Trim()));

            }
            this.gvG.DataSource = ESP.Finance.BusinessLogic.SupporterManager.GetList(term, paramlist);
            this.gvG.DataBind();
        }
    }

    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            System.Web.UI.WebControls.HyperLink hylEdit = (System.Web.UI.WebControls.HyperLink)e.Row.FindControl("hylEdit");
            if (hylEdit != null)
            {
                hylEdit.NavigateUrl = string.Format("SupporterModify.aspx?" + RequestName.SupportID + "={0}" + "&" + RequestName.ProjectID + "={1}" + "&" + RequestName.BackUrl + "=SupporterModifyList.aspx", e.Row.Cells[0].Text.ToString(), e.Row.Cells[1].Text.ToString());
            }
            ESP.Finance.Entity.SupporterInfo supporterModel = (ESP.Finance.Entity.SupporterInfo)e.Row.DataItem;
            ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(int.Parse(e.Row.Cells[1].Text.ToString()));
            if (projectModel != null && projectModel.InUse != (int)ESP.Finance.Utility.ProjectInUse.Use)
                hylEdit.Visible = false;
            Label labState = ((Label)e.Row.FindControl("labState"));
            labState.Text = State.SetState(int.Parse(labState.Text));
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