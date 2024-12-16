using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;
using ESP.HumanResource.Entity;
using ESP.HumanResource.BusinessLogic;
namespace SEPAdmin.HR.Auxiliary
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
            //IList<EmployeeInfo> employees = null;
            List<EmployeesInPositionsInfo> list = null;
            if (!string.IsNullOrEmpty(txtUserName.Text.Trim()) || !string.IsNullOrEmpty(txtPositionName.Text.Trim()))
            {
                string strSql = " (1 = 1) ";
                if (!string.IsNullOrEmpty(txtUserName.Text.Trim()))
                {
                    strSql += string.Format(" and b.lastNameCn+b.firstNameCn like '%{0}%'", txtUserName.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtPositionName.Text.Trim()))
                {
                    strSql += string.Format(" and d.DepartmentPositionName like '%{0}%'", txtPositionName.Text.Trim());
                }
                // employees = EmployeeManager.Search(txtUserName.Text.Trim());
                list = EmployeesInPositionsManager.GetModelList(strSql);
            }
            else
            {
               // employees = EmployeeManager.GetAll();
                list = EmployeesInPositionsManager.GetModelList("");
            }
            
            rptUserList.DataSource = list;
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
