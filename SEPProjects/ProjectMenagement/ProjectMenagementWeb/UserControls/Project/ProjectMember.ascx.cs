using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Compatible;
using ESP.Finance.Utility;

public partial class UserControls_Project_ProjectMember : System.Web.UI.UserControl
{
    private ESP.Compatible.Employee currentUser;
    /// <summary>
    /// Gets or sets the current user.
    /// </summary>
    /// <value>The current user.</value>
    public ESP.Compatible.Employee CurrentUser
    {
        get { return currentUser; }
        set { currentUser = value; }
    }

    int projectid = 0;
    ESP.Finance.Entity.ProjectInfo projectinfo;
    public ESP.Finance.Entity.ProjectInfo ProjectInfo
    {
        get
        {
            if (projectinfo == null)
                projectinfo = new ESP.Finance.Entity.ProjectInfo();
            return projectinfo;
        }
        set { projectinfo = value; }
    }

    private void bindMember()
    {
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
        {
            projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
            if (projectinfo == null)
                projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModel(projectid);
        }
        if (projectinfo.Members != null && projectinfo.Members.Count == 0)
        {
            ESP.Finance.Entity.ProjectMemberInfo model = new ESP.Finance.Entity.ProjectMemberInfo();
            projectinfo.Members.Add(model);
        }
        this.gvMember.DataSource = projectinfo.Members;
        this.gvMember.DataBind();

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        bindMember();
    }

    protected void btnMember_Click(object sender, EventArgs e)
    {
        bindMember();
    }

    protected void gvMember_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
        ESP.Finance.Entity.ProjectInfo pmodel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);

        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblno = (Label)e.Row.FindControl("lblNo");
            if (lblno != null)
                lblno.Text = (e.Row.RowIndex + 1).ToString();

            Label lblPhone = (Label)e.Row.FindControl("lblPhone");
            if (lblPhone != null)
                lblPhone.Text = StringHelper.FormatPhoneLastChar(lblPhone.Text);
        }
        LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
        if (null != lnkDelete)
        {
            if (e.Row.Cells[1].Text.ToString() == projectinfo.ApplicantUserID.ToString())
            {
                lnkDelete.Visible = false;
            }
        }
        if (!string.IsNullOrEmpty(pmodel.ProjectCode) && pmodel.ContractStatusName != ESP.Finance.Utility.ProjectType.BDProject)
        {
            e.Row.Cells[10].Text = "";
            e.Row.Cells[10].Visible = false;
        }
    }
    protected void gvMember_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int memberid = int.Parse(e.CommandArgument.ToString());
            ESP.Finance.BusinessLogic.ProjectMemberManager.Delete(memberid);

            projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
            projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModel(projectid);
            this.gvMember.DataSource = projectinfo.Members;
            this.gvMember.DataBind();
        }

    }
}
