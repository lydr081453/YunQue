using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkFlowModel;
using WorkFlowImpl;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

public partial class Project_AuditList : ESP.Web.UI.PageBase
{

    WorkFlowImpl.WorkItemData workitemdata = new WorkFlowImpl.WorkItemData();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ListBind();
        }
    }


    protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvG.PageIndex = e.NewPageIndex;
        ListBind();

    }

    private int[] GetUsers()
    {
        var currentUserId = ESP.Framework.BusinessLogic.UserManager.GetCurrentUserID();
        List<int> users = new List<int>();
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegateList = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(currentUserId);
        users.AddRange(delegateList.Select(x => x.UserID));
        users.Add(currentUserId);
        return users.ToArray();
    }

    private void ListBind()
    {
        int[] userIds = GetUsers();

        IList<ESP.Finance.Entity.ProjectInfo> Projects  = ESP.Finance.BusinessLogic.ProjectManager.GetWaitAuditList(userIds);

        gvG.DataSource = Projects; 
        gvG.DataBind();
    }

    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ESP.Finance.Entity.ProjectInfo projectmodel = (ESP.Finance.Entity.ProjectInfo)e.Row.DataItem;
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;

            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblName = (Label)e.Row.FindControl("lblName");
            lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(projectmodel.ApplicantUserID) + "');");

            HyperLink hylAudit = (HyperLink)e.Row.FindControl("hylAudit");
            hylAudit.NavigateUrl = "AuditOperation.aspx?" + ESP.Finance.Utility.RequestName.ProjectID + "=" + projectmodel.ProjectId;
            ESP.Finance.Entity.ProjectInfo currentProject = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectmodel.ProjectId);
            if (currentProject != null)
            {
                if (currentProject.InUse != (int)ESP.Finance.Utility.ProjectInUse.Use)
                {
                    hylAudit.Visible = false;
                }
                Literal litUse = (Literal)e.Row.FindControl("litUse");
                litUse.Text = ESP.Finance.Utility.Common.ProjectInUse_Names[currentProject.InUse];
            }
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
}
