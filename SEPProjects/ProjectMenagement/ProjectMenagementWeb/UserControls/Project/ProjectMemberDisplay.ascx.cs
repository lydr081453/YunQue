using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

public partial class UserControls_Project_ProjectMemberDisplay : System.Web.UI.UserControl
{
    public bool DontBindOnLoad { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !DontBindOnLoad)
        {
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
            {
                int projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
                ESP.Finance.Entity.ProjectInfo projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModel(projectid);
                if (projectinfo.Members != null && projectinfo.Members.Count == 0)
                {
                    ESP.Finance.Entity.ProjectMemberInfo model = new ESP.Finance.Entity.ProjectMemberInfo();
                    projectinfo.Members.Add(model);
                }
                this.gvMember.DataSource = projectinfo.Members;
                this.gvMember.DataBind();
            }
        }
    }
    public void InitProjectMember(ESP.Finance.Entity.ProjectInfo projectModel)
    {
        this.gvMember.DataSource = projectModel.Members;
        this.gvMember.DataBind();
    }
    protected void gvMember_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ESP.Finance.Entity.ProjectMemberInfo member = (ESP.Finance.Entity.ProjectMemberInfo)e.Row.DataItem;
            Label lblNo = (Label)e.Row.FindControl("lblNo");
            lblNo.Text = (e.Row.RowIndex + 1).ToString();
            Label lblPhone = (Label)e.Row.FindControl("lblPhone");
            Label lblName = (Label)e.Row.FindControl("lblName");
            //lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(Convert.ToInt32(e.Row.Cells[1].Text)) + "');");
            lblName.Attributes.Add("onclick", "javascript:showUserInfoAsync(" + (member.MemberUserID ?? 0) + ");");
            if (lblPhone != null)
                lblPhone.Text = StringHelper.FormatPhoneLastChar(lblPhone.Text);
            //所有部门级联字符串拼接
            Label lblGroupName = (Label)e.Row.FindControl("lblGroupName");
            List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
            depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(member.GroupID.Value, depList);
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
