using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

public partial class project_SupporterList : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Search();
        }
    }

    protected void gridList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridList.PageIndex = e.NewPageIndex;
        Search();
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
    private void Search()
    {
        string term = string.Empty;
        List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();

        string users = ","+ESP.Finance.BusinessLogic.BranchManager.GetContractAccounters() + "," + ESP.Finance.BusinessLogic.BranchManager.GetFinalAccounters()+","+ESP.Finance.BusinessLogic.BranchManager.GetProjectAccounters()+",";
        string Branchs = string.Empty;
        IList<ESP.Finance.Entity.BranchInfo> branchList = ESP.Finance.BusinessLogic.BranchManager.GetList(" OtherFinancialUsers like '%," + CurrentUser.SysID + ",%'");
        if (branchList != null && branchList.Count > 0)
        {
            foreach (ESP.Finance.Entity.BranchInfo b in branchList)
            {
                Branchs += b.BranchID.ToString() + ",";
            }
        }
        Branchs = Branchs.TrimEnd(',');

        term = " status in('"+((int)Status.Saved).ToString()+"','"+((int)Status.BizReject).ToString()+"','"+((int)Status.FinanceReject)+"')";
        if (users.IndexOf(","+CurrentUser.SysID+",") >= 0)
        {
            term += "  and projectid in (select projectid from f_project where status=" + (int)ESP.Finance.Utility.Status.FinanceAuditComplete + ")";
        }
        else if (!string.IsNullOrEmpty(Branchs))
        {
            term += " and projectid in(select projectid from f_project where branchid in(" + Branchs + "))";
        }
        else
        {
            term += " and (leaderuserid=" + CurrentUser.SysID + " or applicantuserid=" + CurrentUser.SysID + " or supportid in(select supportid from F_Supportmember where MemberUserID=" + CurrentUser.SysID + ")) and  projectid in (select projectid from f_project where status=" + (int)ESP.Finance.Utility.Status.FinanceAuditComplete + ")";
            
        }
        this.gridList.DataSource = ESP.Finance.BusinessLogic.SupporterManager.GetList(term);
        this.gridList.DataBind();
    }

    protected void gridList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;

            Label lblBudgetAllocation = (Label)e.Row.FindControl("lblBudgetAllocation");
            if(lblBudgetAllocation != null && !string.IsNullOrEmpty(lblBudgetAllocation.Text))
            {
                lblBudgetAllocation.Text = Convert.ToDecimal(lblBudgetAllocation.Text).ToString("#,##0.00");
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ESP.Finance.Entity.SupporterInfo supporterModel = (ESP.Finance.Entity.SupporterInfo)e.Row.DataItem;
            ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(supporterModel.ProjectID);

            Label lblName = (Label)e.Row.FindControl("lblName");
            lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(supporterModel.LeaderUserID.Value) + "');");

            HyperLink hylEdit = (HyperLink)e.Row.FindControl("hylEdit");
            hylEdit.NavigateUrl = "SupporterEdit.aspx?" + ESP.Finance.Utility.RequestName.ProjectID + "=" + supporterModel.ProjectID + "&" + ESP.Finance.Utility.RequestName.SupportID + "=" + supporterModel.SupportID;
            if (projectModel.InUse != (int)ESP.Finance.Utility.ProjectInUse.Use)
                hylEdit.Visible = false;
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
}