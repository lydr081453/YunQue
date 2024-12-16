using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using System.Data.SqlClient;
using ESP.Finance.BusinessLogic;

public partial class project_SupporterCompleteList : ESP.Web.UI.PageBase
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
        string users = GetUser();
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
        if (users.IndexOf("," + CurrentUserID.ToString() + ",") >= 0)
        {
            term = " Status=@Status";
            SqlParameter p = new SqlParameter("@Status", System.Data.SqlDbType.Int, 4);
            p.Value = (int)ESP.Finance.Utility.Status.FinanceAuditComplete;
            paramlist.Add(p);
        }
        else if (!string.IsNullOrEmpty(Branchs))
        {
            term = " Status=@Status and projectid in(select projectid from f_project where branchid in("+Branchs+"))";
            SqlParameter p = new SqlParameter("@Status", System.Data.SqlDbType.Int, 4);
            p.Value = (int)ESP.Finance.Utility.Status.FinanceAuditComplete;
            paramlist.Add(p);
        }
        else
        {
            term = " Status=@Status and (SupportID in(select SupportID from F_SupportMember where memberuserid=@memberuserid) or supportid in(select supporterid from F_SupporterAuditHist where auditoruserid = @memberuserid) or LeaderUserID=@memberuserid or applicantUserID=@memberuserid {0})";
            IList<ESP.Finance.Entity.PrjUserRelationInfo> relationList = ESP.Finance.BusinessLogic.PrjUserRelationManager.GetUsableList(CurrentUserID);
            string strBranch = " or (";
            foreach (ESP.Finance.Entity.PrjUserRelationInfo relationModel in relationList)
            {
                strBranch += "projectcode like '" + relationModel.BranchCode + "%' or ";
            }
            if (relationList != null && relationList.Count != 0)
            {
                strBranch = strBranch.Substring(0, strBranch.Length - 4) + " )";
                term = string.Format(term, strBranch);
            }
            else
            {
                term = string.Format(term, "");
            }
            SqlParameter p1 = new SqlParameter("@Status", System.Data.SqlDbType.Int, 4);
            p1.Value = (int)ESP.Finance.Utility.Status.FinanceAuditComplete;
            SqlParameter p2 = new SqlParameter("@memberuserid", System.Data.SqlDbType.Int, 4);
            p2.Value = CurrentUserID;
            paramlist.Add(p1);
            paramlist.Add(p2);
        }

        if (term != string.Empty)
        {
            if (!string.IsNullOrEmpty(this.txtKey.Text.Trim()))
            {
                term += " and (supportercode like '%'+@key+'%' or projectcode like '%'+@key+'%' or GroupName like '%'+@key+'%' or ServiceType like '%'+ @key +'%')";
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
    private string GetUser()
    {
        string user = "," + ESP.Finance.BusinessLogic.BranchManager.GetProjectAccounters() + ",";
        user += ESP.Finance.BusinessLogic.BranchManager.GetContractAccounters() + "," + ESP.Finance.BusinessLogic.BranchManager.GetFinalAccounters() + ","+ESP.Finance.BusinessLogic.BranchManager.GetProjectAccounters()+",";
        return user;
    }

    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string users = GetUser();
        ESP.Finance.Entity.SupporterInfo supporterModel = (ESP.Finance.Entity.SupporterInfo)e.Row.DataItem;
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(supporterModel.ProjectID);
            Label labState = ((Label)e.Row.FindControl("labState"));
            labState.Text = State.SetState(int.Parse(labState.Text));
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
            HyperLink hylEdit = (HyperLink)e.Row.FindControl("hylEdit");
            if (users.IndexOf("," + CurrentUserID.ToString() + ",") < 0)
            {
                hylEdit.Visible = false;
            }
            hylEdit.NavigateUrl = "SupporterModify.aspx?" + ESP.Finance.Utility.RequestName.ProjectID + "=" + supporterModel.ProjectID + "&" + ESP.Finance.Utility.RequestName.SupportID + "=" + supporterModel.SupportID+"&"+RequestName.BackUrl+"=SupporterCompleteList.aspx";
            if (projectModel.InUse != (int)ESP.Finance.Utility.ProjectInUse.Use)
                hylEdit.Visible = false;
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