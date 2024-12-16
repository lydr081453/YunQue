using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;
using ESP.HumanResource.Entity;
using ESP.HumanResource.BusinessLogic;

namespace SEPAdmin.HR.Employees
{
    public partial class DimissionEmployeeDlg : System.Web.UI.Page
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
            List<EmployeesInPositionsInfo> list = null;
            string strSql = " e.status=5 ";

            if (!string.IsNullOrEmpty(txtUserName.Text.Trim()) || !string.IsNullOrEmpty(txtPositionName.Text.Trim()) )
            {
               
                if (!string.IsNullOrEmpty(txtUserName.Text.Trim()))
                {
                    strSql += string.Format(" and (b.lastNameCn+b.firstNameCn like '%{0}%' or b.username like '%{0}%' or e.code like '%{0}%')", txtUserName.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtPositionName.Text.Trim()))
                {
                    strSql += string.Format(" and d.DepartmentPositionName like '%{0}%'", txtPositionName.Text.Trim());
                }

                list = EmployeesInPositionsManager.GetModelList(strSql);
            }
            else
            {
                list = EmployeesInPositionsManager.GetModelList(strSql);
            }

            gvList.DataSource = list;
            gvList.DataBind();
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