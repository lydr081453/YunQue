using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using ESP.Finance.BusinessLogic;
using System.Data.SqlClient;

public partial class project_ProjectAuditedModifiy : ESP.Web.UI.PageBase
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
        string users = this.GetUser();

        if (users.IndexOf("," + CurrentUserID.ToString().Trim() + ",") >= 0)
        {
            term = " and Status in(" + ((int)Status.Waiting).ToString() + "," + ((int)Status.FinanceAuditComplete).ToString() + "," + ((int)Status.ProjectPreClose).ToString() + ")";
        }
        else
        {
            term = " and Status in(" + ((int)Status.Waiting).ToString() + "," + ((int)Status.FinanceAuditComplete).ToString() + "," + ((int)Status.ProjectPreClose).ToString() + ") and (CreatorID=@memberuserid or ApplicantUserID=@memberuserid)";
            paramlist.Add(new System.Data.SqlClient.SqlParameter("@memberuserid", CurrentUserID));
        }

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
    private string GetUser()
    {
        string user = "," + ESP.Finance.BusinessLogic.BranchManager.GetProjectAccounters() + ",";
        //IList<ESP.Finance.Entity.BranchInfo> branchlist = ESP.Finance.BusinessLogic.BranchManager.GetList(null, null);
        //foreach (ESP.Finance.Entity.BranchInfo model in branchlist)
        //{
        //    user += model.ProjectAccounter.ToString().Trim() + ",";
        //}
        user += ESP.Finance.BusinessLogic.BranchManager.GetContractAccounters() + "," + ESP.Finance.BusinessLogic.BranchManager.GetFinalAccounters() + ",";
        return user;
    }

    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string users = this.GetUser();
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
            HyperLink hylEdit = (HyperLink)e.Row.FindControl("hylEdit");
            if (hylEdit != null)
            {
                if (users.IndexOf("," + CurrentUser.SysID + ",") < 0 && CurrentUser.SysID.Trim() != e.Row.Cells[3].Text.Trim() && CurrentUser.SysID.Trim() != e.Row.Cells[4].Text.Trim())
                {
                    hylEdit.Visible = false;
                }
                hylEdit.NavigateUrl = string.Format("/project/ProjectAuditedModify.aspx?{0}={1}&{2}={3}", RequestName.ProjectID, e.Row.Cells[0].Text.ToString(), RequestName.Operate, "update");
            }
            Label labState = ((Label)e.Row.FindControl("labState"));
            labState.Text = State.SetState(int.Parse(labState.Text));
            Label lblName = (Label)e.Row.FindControl("lblName");
            lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(projectmodel.ApplicantUserID) + "');");
            if (projectmodel.InUse != (int)ESP.Finance.Utility.ProjectInUse.Use)
                hylEdit.Visible = false;
            //所有部门级联字符串拼接
            Label lblGroupName = (Label)e.Row.FindControl("lblGroupName");
            List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
            depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(projectmodel.GroupID.Value, depList);
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