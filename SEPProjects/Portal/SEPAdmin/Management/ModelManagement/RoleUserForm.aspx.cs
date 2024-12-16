using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;

namespace SEPAdmin
{
    public partial class RoleUserForm : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (RoleID <= 0)
            {
                Response.End();
                return;
            }

             if (!IsPostBack)
            {    

               BindList();
            }
        }


        protected int RoleID
        {
            get
            {
                if (ViewState["RoleID"] == null)
                {
                    int roleId = 0;
                    string qs = Request.QueryString["id"];
                    int.TryParse(qs, out roleId);
                    ViewState["RoleID"] = roleId;
                }
                return (int)ViewState["RoleID"];
            }

        }

        private void BindList()
        {
            BindDepartmentList();
            BindUserList();
        }
        private void BindDepartmentList()
        {
            IList<DepartmentInfo> listDap = RoleManager.GetDepartmentsInRole(this.RoleID);
            gvRoleDep.DataSource = listDap;
            gvRoleDep.DataBind();

        }
        private void BindUserList()
        {
            IList<UserInfo> listUser = RoleManager.GetUsersInRole(this.RoleID);
            gvRoleUser.DataSource = listUser;
            gvRoleUser.DataBind();
        }

        protected void gvRoleDep_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteUser")
            {
                GridView view = (GridView)sender;
                int depId = (int)view.DataKeys[int.Parse(e.CommandArgument.ToString())].Value;
                RoleManager.RemoveEntityFromRole(depId, this.RoleID, RoleOwnerType.Department);
                BindList();
            }
        }

        protected void gvRoleUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteUser")
            {
                GridView view = (GridView)sender;
                int userId = (int)view.DataKeys[int.Parse(e.CommandArgument.ToString())].Value;
                RoleManager.RemoveEntityFromRole(userId, this.RoleID, RoleOwnerType.User);
                BindList();
            }
        }

        protected void btnAddDepartments_Command(object sender, CommandEventArgs e)
        {
            string val = hdnNewDepartments.Value;
            string[] arr = val.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int departmentId = 0;
            foreach (string s in arr)
            {
                int.TryParse(s, out departmentId);
                if (departmentId > 0)
                {
                    RoleManager.AddDepartmentToRole(departmentId, this.RoleID);
                }
            }
            BindDepartmentList();
        }

        protected void btnAddUsers_Command(object sender, CommandEventArgs e)
        {
            string val = hdnAddUsers.Value;
            string[] arr = val.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int userId = 0;
            foreach (string s in arr)
            {
                int.TryParse(s, out userId);
                if (userId > 0)
                {
                    RoleManager.AddUserToRole(userId, this.RoleID);
                }
            }
            BindUserList();
        }
    }
}
