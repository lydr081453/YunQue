using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

public partial class project_ProjectCommitList : ESP.Web.UI.PageBase
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



    protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvG.PageIndex = e.NewPageIndex;
        Search();
    }
    private string GetUser()
    {
        string user = "," + ESP.Finance.BusinessLogic.BranchManager.GetProjectAccounters() + ",";
        user += ESP.Finance.BusinessLogic.BranchManager.GetContractAccounters() + "," + ESP.Finance.BusinessLogic.BranchManager.GetFinalAccounters() + ",";
        return user;
    }

    private void Search()
    {
        term = string.Empty;
        paramlist.Clear();
        string users = GetUser();

        if (users.IndexOf(","+CurrentUserID.ToString()+",") >= 0)
        {
            term = " and Status not in(" + (int)Status.Saved + "," + (int)Status.FinanceAuditComplete + ")";
        }
        else
        {
            term = " and Status not in (" + (int)Status.Saved + "," + (int)Status.FinanceAuditComplete + ") and (projectid in(select projectid from f_projectmember where memberuserid=@memberuserid) or projectid in(select distinct projectid from f_audithistory where auditoruserid=@memberuserid) or creatorid=@memberuserid or applicantUserID=@memberuserid)";
            System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@memberuserid",System.Data.SqlDbType.Int,4);
            p1.Value = CurrentUserID;
            paramlist.Add(p1);
        }

        if (term != string.Empty)
        {
            if (!string.IsNullOrEmpty(this.txtKey.Text.Trim()))
            {
                string customerIDs = string.Empty;
                IList<ESP.Finance.Entity.CustomerTmpInfo> customerList = ESP.Finance.BusinessLogic.CustomerTmpManager.GetList(" NameCN1 like '%" + this.txtKey.Text.Trim() + "%' or  NameEN1 like '%" + this.txtKey.Text.Trim() + "%' ");
                foreach (ESP.Finance.Entity.CustomerTmpInfo cusModel in customerList)
                {
                    customerIDs += cusModel.CustomerTmpID.ToString() + ",";
                }
                customerIDs = customerIDs.TrimEnd(',');
                term += " and (serialcode like '%'+@serialcode+'%' or BusinessDescription like '%'+@serialcode+'%' or projectcode like '%'+@serialcode+'%' or GroupName like '%'+@serialcode+'%' or BranchName like '%'+@serialcode+'%' or BusinessTypeName like '%'+@serialcode+'%' ";
                if (!string.IsNullOrEmpty(customerIDs))
                    term += " or CustomerID in(" + customerIDs + "))";
                else
                    term += ")";
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@serialcode", this.txtKey.Text.Trim()));
            }
            this.gvG.DataSource = ESP.Finance.BusinessLogic.ProjectManager.GetList(term, paramlist);
            this.gvG.DataBind();
        }
    }

    protected void lbNewProject_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProjectStep1.aspx");
    }

    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ESP.Finance.Entity.ProjectInfo projectmodel = (ESP.Finance.Entity.ProjectInfo)e.Row.DataItem;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label labState = ((Label)e.Row.FindControl("labState"));
            labState.Text = State.SetState(int.Parse(labState.Text));
            Label lblName = (Label)e.Row.FindControl("lblName");
            lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(projectmodel.ApplicantUserID) + "');");
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

            if (projectmodel.Status == (int)ESP.Finance.Utility.Status.Submit)
            {
                //提交状态才显示撤销按钮
                ImageButton ImgCancel = (ImageButton)e.Row.FindControl("ImgCancel");
                ImgCancel.Visible = true;
            }
        }
    }

    protected void ImgCancel_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton ImgCancel = (ImageButton)sender;
        int projectId = int.Parse(ImgCancel.CommandArgument.ToString());
        ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectId);
        if (projectModel.Status == (int)ESP.Finance.Utility.Status.Submit)
        {

            if (ESP.Finance.BusinessLogic.ProjectManager.CancelProject(projectModel,CurrentUser))
            {
                //删除工作流数据
                WorkFlowDAO.ProcessInstanceDao dao = new WorkFlowDAO.ProcessInstanceDao();
                dao.TerminateProcess(projectModel.ProcessID ?? 0, projectModel.InstanceID ?? 0);
            }
            Search();
        }
        else
        {
            string Msg = "";
            IList<ESP.Finance.Entity.AuditHistoryInfo> auditHist = ESP.Finance.BusinessLogic.AuditHistoryManager.GetList(" projectid=" + projectModel.ProjectId + " and auditStatus=" + (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing);
            if (auditHist != null && auditHist.Count > 0)
            {
                auditHist = auditHist.OrderByDescending(N => N.AuditID).ToList();
                Msg = "项目号已通过"+auditHist[0].AuditorEmployeeName+"审核。";
            }
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('"+ Msg + "不能进行撤销操作！" +"');", true);
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
