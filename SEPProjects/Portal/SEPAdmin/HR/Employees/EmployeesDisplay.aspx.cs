/*
 * EmployeesDisplay.aspx
 * IT部门、财务部门对员工的简单查看页
 * 以入职日期排序
 * 分当月、全部选项
 * 
 */

using System;
using System.Collections.Generic;
using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Entity;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ESP.Compatible;

namespace SEPAdmin.HR.Employees
{
    public partial class EmployeesDisplay : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListBind();
            }
        }

        private void ListBind()
        {
            string strWhere = " and (a.status = 1 or a.status = 3) ";
            if (txtITCode.Text.Trim() != "")
            {
                strWhere += string.Format(" and (a.code like '%{0}%')", txtITCode.Text.Trim());
            }
            if (txtuserName.Text.Trim() != "")
            {
                strWhere += string.Format(" and (b.lastnamecn+b.firstnamecn like '%{0}%'  or b.username like '%{0}%' )", txtuserName.Text.Trim());
            }

            if (drpJoinDate.SelectedItem.Value == "2")
            {
                strWhere += string.Format(" and(j.joinDate between '{0}' and '{1}')", DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-01", DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + (new DateTime(DateTime.Today.Year, DateTime.Today.AddMonths(1).Month, 1)).AddDays(-1).Day.ToString());
            }

            List<ESP.HumanResource.Entity.EmployeeBaseInfo> list = EmployeesInPositionsManager.GetUserModelAllList(UserInfo, strWhere);

            gvList.DataSource = list;
            gvList.DataBind();
            if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("012", this.ModuleInfo.ModuleID, this.UserID))
            {
                gvList.Columns[5].Visible = false;
            }
            else if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("013", this.ModuleInfo.ModuleID, this.UserID))
            {
                gvList.Columns[5].Visible = true;
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('你没有访问该数据的权限！');window.location.href='/hr/default.aspx';", true);
            }

            this.ddlCurrentPage2.Items.Clear();
            for (int i = 1; i <= this.gvList.PageCount; i++)
            {
                this.ddlCurrentPage2.Items.Add(i.ToString());
            }
            if (this.gvList.PageCount > 0)
            {
                this.ddlCurrentPage2.SelectedIndex = this.gvList.PageIndex;
            }
            if (gvList.PageCount > 1)
            {
                PageBottom.Visible = true;
                PageTop.Visible = true;
            }
            else
            {
                PageBottom.Visible = false;
                PageTop.Visible = false;
            }
            if (list.Count > 0)
            {
                tabTop.Visible = true;
                tabBottom.Visible = true;
            }
            else
            {
                tabTop.Visible = false;
                tabBottom.Visible = false;
            }

            labAllNum.Text = labAllNumT.Text = list.Count.ToString();
            labPageCount.Text = labPageCountT.Text = (gvList.PageIndex + 1).ToString() + "/" + gvList.PageCount.ToString();
            if (gvList.PageCount > 0)
            {
                if (gvList.PageIndex + 1 == gvList.PageCount)
                    disButton("last");
                else if (gvList.PageIndex == 0)
                    disButton("first");
                else
                    disButton("");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListBind();
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.HumanResource.Entity.EmployeeBaseInfo model = (ESP.HumanResource.Entity.EmployeeBaseInfo)e.Row.DataItem;

                Repeater rep = (Repeater)e.Row.FindControl("repJob");
                List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> list = (List<ESP.HumanResource.Entity.EmployeesInPositionsInfo>)gvList.DataKeys[e.Row.RowIndex].Values[2];
                if (list.Count > 0 && list != null)
                {
                    rep.DataSource = list;
                    rep.DataBind();
                }
            }
        }

        #region 分页设置
        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            ListBind();
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            Paging(gvList.PageCount);
        }
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            Paging(0);
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            Paging((gvList.PageIndex + 1) > gvList.PageCount ? gvList.PageCount : (gvList.PageIndex + 1));
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Paging((gvList.PageIndex - 1) < 0 ? 0 : (gvList.PageIndex - 1));
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        private void Paging(int pageIndex)
        {
            GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
            gvList_PageIndexChanging(new object(), e);
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvList.PageIndex = this.ddlCurrentPage2.SelectedIndex;
            ListBind();
        }

        /// <summary>
        /// 分页按钮的显示设置
        /// </summary>
        /// <param name="page"></param>
        private void disButton(string page)
        {
            switch (page)
            {
                case "first":
                    btnFirst.Enabled = false;
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                    btnFirst2.Enabled = false;
                    btnPrevious2.Enabled = false;
                    btnNext2.Enabled = true;
                    btnLast2.Enabled = true;
                    break;
                case "last":
                    btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;

                    btnFirst2.Enabled = true;
                    btnPrevious2.Enabled = true;
                    btnNext2.Enabled = false;
                    btnLast2.Enabled = false;
                    break;
                default:
                    btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                    btnFirst2.Enabled = true;
                    btnPrevious2.Enabled = true;
                    btnNext2.Enabled = true;
                    btnLast2.Enabled = true;
                    break;
            }
        }
        #endregion

        private static int IsStringInArray(string s, IList<ESP.Framework.Entity.DepartmentInfo> array)
        {
            if (array == null || s == null)
                return 0;

            for (int i = 0; i < array.Count; i++)
            {
                if (string.Compare(s, array[i].DepartmentName, true) == 0)
                {
                    return array[i].DepartmentID;
                }
            }
            return 0;
        }
    }
}
