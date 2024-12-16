using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;

namespace SEPAdmin
{
    public partial class RoleBrowse : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Search();
            }
        }
        protected void btnDel_Click(object sender, EventArgs e)
        {
            GridViewRow rw = (GridViewRow)((ImageButton)sender).Parent.Parent;
            HyperLink hypRoleID = (HyperLink)rw.FindControl("hypRoleID");
            if (hypRoleID != null && !string.IsNullOrEmpty(hypRoleID.Text))
            {
                RoleManager.Delete(Convert.ToInt32(hypRoleID.Text));
                Search();
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow rw = (GridViewRow)((ImageButton)sender).Parent.Parent;
            HyperLink hypRoleID = (HyperLink)rw.FindControl("hypRoleID");
            if (hypRoleID != null && !string.IsNullOrEmpty(hypRoleID.Text))
            {
                Response.Redirect("RoleForm.aspx?id=" + (hypRoleID.Text));
            }
        }

        protected void btnUser_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnHomePage_Click(object sender, EventArgs e)
        {
            GridViewRow rw = (GridViewRow)((ImageButton)sender).Parent.Parent;
            HyperLink hypRoleID = (HyperLink)rw.FindControl("hypRoleID");
            if (hypRoleID != null && !string.IsNullOrEmpty(hypRoleID.Text))
            {
                Response.Redirect("/Management/PermissionManagement/RolePermissionModify.aspx?id=" + (hypRoleID.Text));
            }
        }

        private void Search()
        {
            IList<RoleInfo> roleInfos = RoleManager.GetAll();
            //roleInfos = RoleController.LoadProperties(roleInfos);
            this.gvView.DataSource = roleInfos;
            this.gvView.DataBind();
        }

        protected void gvView_RowDataBound(object sender, GridViewRowEventArgs e)
        {            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink link = (HyperLink)e.Row.FindControl("hypRoleID");
                link.NavigateUrl = "RoleForm.aspx?id=" + gvView.DataKeys[e.Row.RowIndex].Values[0].ToString();
                link.Text = gvView.DataKeys[e.Row.RowIndex].Values[0].ToString();
            }
        }

        protected string GetUsersLink(RoleInfo role)
        {
            return VirtualPathUtility.ToAbsolute("~/Management/ModelManagement/RoleUserForm.aspx") + "?id=" + role.RoleID;
        }
    }
}
