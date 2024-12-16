using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;

namespace SEPAdmin.Management.ModelManagement
{
    public partial class UserList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindList();
            }
        }

        private void BindList()
        {
           // IList<EmployeeInfo> employees = EmployeeController.GetAll();
            IList<EmployeeInfo> employees = null;
            if(!string.IsNullOrEmpty(txtUserName.Text.Trim()))
             employees = EmployeeManager.Search(txtUserName.Text.Trim());
            else
                employees = EmployeeManager.GetAll();

            rptUserList.DataSource = employees;
            rptUserList.DataBind();
        }

        /// <summary>
        /// 检索按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindList();
            }
            catch { }
        }
    }
}
