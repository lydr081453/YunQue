using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.BusinessLogic;
using ESP.Framework.Entity;

namespace SEPAdmin.Management.UserManagement
{
    public partial class UserRolesForm : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["userid"]))
            {
                try
                {
                    ListBind(int.Parse(Request["userid"]));
                }
                catch { }
            }
        }

        private void ListBind(int userid)
        {
            IList<RoleInfo> list = RoleManager.GetUserRoles(userid);
            if (null != list && list.Count > 0)
            {
                labUserID.Text = userid.ToString();
                EmployeeInfo emp = EmployeeManager.Get(userid);
                if (null != emp)
                    labUserName.Text = emp.Username;

                gvView.DataSource = list;
                gvView.DataBind();
                
            }
        }

        /// <summary>
        /// 返回按钮的Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserBrowse.aspx");
        }
    }
}
