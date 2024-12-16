using System;
using System.Collections.Generic;
using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Entity;

namespace SEPAdmin.HR.Employees
{
    public partial class UserList3 : ESP.Web.UI.PageBase
    {
        private int DepId = 0;
        private int isProject = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(Request["depid"]))
            {
                DepId = int.Parse(Request["depid"]);
            }

            if (!string.IsNullOrEmpty(Request["IsProject"]))
            {
                isProject = int.Parse(Request["IsProject"]);
            }



                BindList(DepId);
                  }

        private void BindList(int depid)
        {
            string deptids = "31,32,43,254,260,71,87,233,240";
            string deptid = string.Empty;
            if (deptids.IndexOf(depid.ToString()) >= 0)
            {
                deptid = deptids;
            }
            else
            {
                deptid = depid.ToString();
            }

            string strSql = " (1 = 1) and e.status in (1,3)";

            List<EmployeesInPositionsInfo> list = null;
            if (!string.IsNullOrEmpty(txtUserCode.Text.Trim()) || !string.IsNullOrEmpty(txtUserName.Text.Trim()) || !string.IsNullOrEmpty(txtPositionName.Text.Trim()))
            {
               
                if (depid != 0)
                {
                    strSql += string.Format(" and c.level3Id in({0})", deptid);
                }
                if (!string.IsNullOrEmpty(txtUserCode.Text.Trim()))
                {
                    strSql += string.Format(" and e.Code like '%{0}%'", txtUserCode.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtUserName.Text.Trim()))
                {
                    strSql += string.Format(" and (b.lastNameCn+b.firstNameCn like '%{0}%' or b.username like '%{0}%')", txtUserName.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtPositionName.Text.Trim()))
                {
                    strSql += string.Format(" and d.DepartmentPositionName like '%{0}%'", txtPositionName.Text.Trim());
                }
                if (isProject != 0)
                {
                    strSql += " and d.PositionLevel<=9 ";
                }
                list = EmployeesInPositionsManager.GetModelList(strSql);
            }
            else
            {
               // strSql = " (1 = 1)  and e.status not in (5, 6) ";
                if (depid != 0)
                { 
                strSql+=" and c.level3Id in(" + deptid+")";
                }
                if (isProject != 0)
                {
                    strSql += " and d.PositionLevel<=9 ";
                }
                list = EmployeesInPositionsManager.GetModelList(strSql);
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
                BindList(0);
            }
            catch { }
        }
    }
}