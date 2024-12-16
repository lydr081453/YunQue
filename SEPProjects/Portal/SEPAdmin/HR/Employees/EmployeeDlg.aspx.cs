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
    public partial class EmployeeDlg : System.Web.UI.Page
    {
        int isHR = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["iskaoqin"]))
                {
                    hidKaoqin.Value = Request["iskaoqin"].ToString();
                }
                if (!string.IsNullOrEmpty(Request["isHR"]))
                {
                    hidIsHR.Value = Request["isHR"].ToString();
                }
                if (!string.IsNullOrEmpty(Request["istrans"]))
                {
                    hidIsTrans.Value = Request["istrans"].ToString();
                }

                int deptid = string.IsNullOrEmpty(Request["deptid"]) ? 0 : int.Parse(Request["deptid"]);

                BindList(deptid);
            }
        }

        private void BindList(int deptid )
        {
            List<EmployeesInPositionsInfo> list = null;
            if (!string.IsNullOrEmpty(txtUserName.Text.Trim()) || !string.IsNullOrEmpty(txtPositionName.Text.Trim()) || deptid!=0)
            {
                string strSql = " e.status in(1,3) ";
                if (!string.IsNullOrEmpty(txtUserName.Text.Trim()))
                {
                    strSql += string.Format(" and (b.lastNameCn+b.firstNameCn like '%{0}%' or b.username like '%{0}%' or e.code like '%{0}%')", txtUserName.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtPositionName.Text.Trim()))
                {
                    strSql += string.Format(" and d.DepartmentPositionName like '%{0}%'", txtPositionName.Text.Trim());
                }
                if (deptid!=0)
                {
                    strSql += " and c.level3id=" + deptid;
                }

                list = EmployeesInPositionsManager.GetModelList(strSql);
            }
            else
            {
                list = EmployeesInPositionsManager.GetModelList("");
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
                BindList(0);
            }
            catch { }
        }
    }
}
