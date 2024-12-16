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
using ESP.Finance.BusinessLogic;
using ESP.Finance.Entity;


public partial class Dialogs_ProjectList : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            listBind();
        }

    }

    protected void gvProject_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            string pid = e.CommandArgument.ToString();
            add(pid);
        }
    }


    protected void gvProject_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ESP.Finance.Entity.ProjectInfo projectmodel = (ESP.Finance.Entity.ProjectInfo)e.Row.DataItem;
            Literal lblGroupName = (Literal)e.Row.FindControl("litGroup");
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

    protected void gvProject_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProject.PageIndex = e.NewPageIndex;
        search();
    }


    private void add(string projectId)
    {
        ProjectInfo project = ProjectManager.GetModel(int.Parse(projectId));
        //ProjectMediaInfo projectMedia = ProjectMediaManager.GetList(" and projectId="+projectId + " and endDate is null").FirstOrDefault();

        string script = "opener.setProject('"+projectId+"','"+project.ProjectCode+"');window.close();";
        ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), script, true);
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        search();
    }

    private void search()
    {
        listBind();
    }
    protected void btnClean_Click(object sender, EventArgs e)
    {
        txtCode.Text = "";
        listBind();
    }

    private void listBind()
    {
        StringBuilder strTerms = new StringBuilder();
        List<SqlParameter> parms = new List<SqlParameter>();
        string projectType =System.Web.Configuration.WebConfigurationManager.AppSettings["ProjectType_Recharge"];
        projectType = projectType.TrimStart(',');
        projectType=projectType.TrimEnd(',');
        strTerms.Append(" and projectCode <> '' and projectTypeId in(@projectTypeId)");
        parms.Add(new SqlParameter("@ProjectTypeId", projectType));
        if (txtCode.Text.Trim() != "")
        {
            strTerms.Append(" and projectCode like '%'+@projectCode+'%'");
            parms.Add(new SqlParameter("@projectCode", txtCode.Text.Trim()));
        }

        gvProject.DataSource = ProjectManager.GetList(strTerms.ToString(), parms);
        gvProject.DataBind();
    }
}
