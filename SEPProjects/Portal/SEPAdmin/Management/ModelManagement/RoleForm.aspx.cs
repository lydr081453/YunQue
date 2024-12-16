using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;

namespace SEPAdmin.ModelManagement
{
    public partial class RoleForm : ESP.Web.UI.PageBase
    {
        private RoleInfo CurrentRole
        {
            get
            {
                if (null != ViewState["CurrentRole"])
                    return (RoleInfo)ViewState["CurrentRole"];
                else
                {
                    return new RoleInfo();
                }
            }
            set { ViewState["CurrentRole"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRoleGroup();
                if (!string.IsNullOrEmpty(Request["id"]))
                {
                    BindRoleInfo(Convert.ToInt32(Request["id"]));
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save(CurrentRole);
            //ShowCompleteMessage("修改成功", "ItemBrowse.aspx");
            Response.Redirect("RoleBrowse.aspx");
        }

        private void Save(RoleInfo info)
        {
            info.RoleName = this.txtName.Text.Trim();
            info.RoleGroupID = Convert.ToInt32(this.ddlType.SelectedValue);
            info.Description = this.txtDes.Text.Trim();

            if (info.RoleID <= 0)
            {
                //info.CreatedUserID = userid;
                info.CreatedTime = DateTime.Now;
                info.Creator = this.UserID;
                //////
                RoleManager.Create( info);
            }
            else
            {
                info.LastModifiedTime = DateTime.Now;
                info.LastModifier = this.UserID;
                //////
                RoleManager.Update( info);
            }
        }

        private void BindRoleInfo(int id)
        {
            RoleInfo info = RoleManager.Get(id);
            if (info != null)
            {
                CurrentRole = info;
                this.txtName.Text = info.RoleName;
                //this.txtCreateDate.Text = info.CreatedTime.ToString("yyyy-MM-dd");

                //EmployeeInfo employee = EmployeeController.Get(info.Creator);
                //if (employee != null)
                //    this.txtCreateUser.Text = employee.FullNameCN;

                this.txtDes.Text = info.Description;

                this.ddlType.SelectedValue = info.RoleGroupID.ToString();
            }
        }

        private void BindRoleGroup()
        {
            IList<RoleGroupInfo> list = RoleGroupManager.GetAll();

            foreach (RoleGroupInfo info in list)
            {
                ListItem item = new ListItem(info.RoleGroupName, info.RoleGroupID.ToString());
                this.ddlType.Items.Add(item);
            }
        }
    }
}
