using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
using ESP.Purchase.Entity;
using ESP.Framework.BusinessLogic;

public partial class Dialogs_selectProjectList : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            ListBind();
    }

    private void ListBind()
    {
        StringBuilder strTerms = new StringBuilder();
        StringBuilder strTerms2 = new StringBuilder();
        List<SqlParameter> parms = new List<SqlParameter>();
        int[] depts = new ESP.Compatible.Employee(int.Parse(CurrentUser.SysID)).GetDepartmentIDs();

        strTerms.Append(" and status = @status");
        parms.Add(new SqlParameter("@status", ESP.Purchase.Common.State.projectstatus_ok));

        strTerms2.Append(" and status = " + ESP.Purchase.Common.State.projectstatus_ok);

        if (!OperationAuditManageManager.GetCurrentUserIsManager(CurrentUser.SysID)
            && !OperationAuditManageManager.GetCurrentUserIsDirector(CurrentUser.SysID)
            && !(ESP.Configuration.ConfigurationManager.SafeAppSettings["ProjectAdministrativeIDs"].IndexOf("," + depts[0] + ",") >= 0)
            && !(ESP.Configuration.ConfigurationManager.SafeAppSettings["SpecialDeptIDs"].IndexOf("," + depts[0] + ",") >= 0)
            )
        {
            strTerms.Append(" and memberUserID = @userId");
            parms.Add(new SqlParameter("@userId", int.Parse(CurrentUser.SysID)));
        }

        if (txtPA.Text.Trim() != "")
        {
            strTerms.Append(" and serialCode like '%'+@PA+'%'");
            parms.Add(new SqlParameter("@PA", txtPA.Text.Trim()));

            strTerms2.Append(" and serialCode like '%" + txtPA.Text.Trim() + "%'");
        }
        if (txtProjectCode.Text.Trim() != "")
        {
            strTerms.Append(" and projectCode like '%'+@projectCode+'%'");
            parms.Add(new SqlParameter("@projectCode", txtProjectCode.Text.Trim()));

            strTerms2.Append(" and projectCode like '%" + txtProjectCode.Text.Trim() + "%'");
        }

        List<ESP.Purchase.Entity.V_GetProjectList> list = null;
        //添加分公司GM项目号
        IList<ESP.Finance.Entity.BranchInfo> branchList = ESP.Finance.BusinessLogic.BranchManager.GetAllList();


        if (ESP.Configuration.ConfigurationManager.SafeAppSettings["ProjectAdministrativeIDs"].IndexOf("," + depts[0] + ",") >= 0 || ESP.Configuration.ConfigurationManager.SafeAppSettings["SpecialDeptIDs"].IndexOf("," + depts[0] + ",") >= 0)
        {
            list = ESP.Purchase.BusinessLogic.V_GetProjectList.GetList(strTerms.ToString(), parms);
            var q = (from project in list
                     select new ESP.Purchase.Entity.V_GetProjectList
                     {
                         ProjectId = project.ProjectId,
                         ProjectCode = project.ProjectCode,
                         SerialCode = project.SerialCode,
                         SubmitDate = project.SubmitDate,
                         GroupID = project.GroupID,
                         BusinessDescription = project.BusinessDescription
                     }).Distinct(new V_GetProjectListComparer());

            list = q.ToList();
        }
        else if (OperationAuditManageManager.GetCurrentUserIsManager(CurrentUser.SysID))
        {
            list = ESP.Purchase.BusinessLogic.V_GetProjectList.GetProjectListByManager(Convert.ToInt32(CurrentUser.SysID), strTerms2.ToString());

        }
        else if (OperationAuditManageManager.GetCurrentUserIsDirector(CurrentUser.SysID))
        {
            list = ESP.Purchase.BusinessLogic.V_GetProjectList.GetProjectListByDirector(Convert.ToInt32(CurrentUser.SysID), strTerms2.ToString());
        }
        else
        {
            list = ESP.Purchase.BusinessLogic.V_GetProjectList.GetList(strTerms.ToString(), parms);
        }
        int deptid = CurrentUser.GetDepartmentIDs()[0];
        if (ESP.Configuration.ConfigurationManager.SafeAppSettings["ProjectAdministrativeIDs"].IndexOf("," + deptid.ToString() + ",") >= 0)
        {
            foreach (ESP.Finance.Entity.BranchInfo branch in branchList)
            {
                ESP.Purchase.Entity.V_GetProjectList newProject = new ESP.Purchase.Entity.V_GetProjectList();
                newProject.ProjectCode = branch.BranchCode + "-" + "GM*-*-" + DateTime.Now.Year.ToString().Substring(2) + DateTime.Now.Month.ToString("00") + "001";
                newProject.SubmitDate = DateTime.Parse(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("00") + "-01");
                list.Add(newProject);
            }
        }

        if (int.Parse(CurrentUser.SysID) == 13729)
        {
            ESP.Purchase.Entity.V_GetProjectList newProject = new ESP.Purchase.Entity.V_GetProjectList();
            newProject.ProjectCode = "R-" + "GM*-*-" + DateTime.Now.Year.ToString().Substring(2) + DateTime.Now.Month.ToString("00") + "001";
            newProject.SubmitDate = DateTime.Parse(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("00") + "-01");
            list.Add(newProject);
        }

        gvProject.DataSource = list;
        gvProject.DataBind();
    }

    class V_GetProjectListComparer : IEqualityComparer<V_GetProjectList>
    {
        #region IEqualityComparer<V_GetProjectList> 成员

        public bool Equals(V_GetProjectList x, V_GetProjectList y)
        {
            return x.ProjectId == y.ProjectId
                && x.ProjectCode == y.ProjectCode
                && x.SerialCode == y.SerialCode
                && x.SubmitDate == y.SubmitDate
                && x.GroupID == y.GroupID
                && x.BusinessDescription == y.BusinessDescription;
        }

        public int GetHashCode(V_GetProjectList obj)
        {
            return obj.ProjectId.GetHashCode();
        }

        #endregion
    }


    protected void btnSelect_Click(object sender, EventArgs e)
    {
        ListBind();
    }

    protected void gvProject_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            string projectId = e.CommandArgument.ToString();
            add(projectId);
        }
    }

    protected void gvProject_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ESP.Purchase.Entity.V_GetProjectList model = (ESP.Purchase.Entity.V_GetProjectList)e.Row.DataItem;
            Literal litGroup = (Literal)e.Row.FindControl("litGroup");
            ESP.Framework.Entity.DepartmentInfo departModel = ESP.Framework.BusinessLogic.DepartmentManager.Get(model.GroupID);
            if (departModel != null)
                litGroup.Text = departModel.DepartmentName;
        }
    }


    private void add(string projectId)
    {
        int result = 0;
        string depts = "";
        if (int.TryParse(projectId, out result))
        {
            ESP.Purchase.Entity.V_GetProjectList model = ESP.Purchase.BusinessLogic.V_GetProjectList.GetModel(int.Parse(projectId));

            if (model != null)
            {
                List<ESP.Purchase.Entity.V_GetProjectGroupList> list = ESP.Purchase.BusinessLogic.V_GetProjectGroupList.GetGroupListByPid(model.ProjectId);

                foreach (ESP.Purchase.Entity.V_GetProjectGroupList group in list)
                {
                    //如果支持方是FEE，则可以选择主申请方的成本组
                    IList<ESP.Finance.Entity.SupporterInfo> supportList = ESP.Finance.BusinessLogic.SupporterManager.GetList("ProjectID=" + group.ProjectId.ToString() + " and GroupID=" + group.GroupID.ToString());
                    if (supportList != null && supportList.Count > 0)
                    {
                        if (supportList[0].IncomeType == "Fee")
                        {
                            IList<ESP.Finance.Entity.SupportMemberInfo> memberList = ESP.Finance.BusinessLogic.SupportMemberManager.GetList(" SupportID=" + supportList[0].SupportID.ToString() + " and MemberUserID=" + CurrentUser.SysID);
                            if (memberList != null && memberList.Count > 0)
                            {
                                ESP.Finance.Entity.ProjectInfo ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(supportList[0].ProjectID);
                                depts += ProjectModel.GroupID + "," + ProjectModel.GroupName + "#";
                            }
                        }
                    }

                   // if (ESP.Purchase.BusinessLogic.V_GetProjectList.MemberInProjectGroup(group.ProjectId, group.GroupID, int.Parse(CurrentUser.SysID)))
                        depts += group.GroupID + "," + group.GroupName + "#";

                }
                Response.Write("<script>window.opener.setProjectInfo('" + model.ProjectId + "','" + model.ProjectCode + "','" + model.BusinessDescription + "','" + depts.TrimEnd('#') + "');</script>");
            }
        }
        else
        {
            //如果是GM项目，自动带出自己的组
            ESP.Framework.Entity.DepartmentInfo dept = ESP.Framework.BusinessLogic.DepartmentManager.Get(CurrentUser.GetDepartmentIDs()[0]);
            depts = dept.DepartmentID.ToString() + "," + dept.DepartmentName + "#";
            Response.Write("<script>window.opener.setProjectInfo('0','" + projectId + "','','" + depts.TrimEnd('#') + "');</script>");
        }

        Response.Write(@"<script>window.close();</script>");
    }
}
