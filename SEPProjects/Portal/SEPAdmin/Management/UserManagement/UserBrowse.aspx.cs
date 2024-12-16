using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;

namespace SEPAdmin.UserManagement
{
    public partial class UserBrowse : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //  SEP.BusinessControlling.EmployeeController.Create
            //IList<EmployeeInfo> employees = EmployeeController.GetAll();

            //gvView.DataSource = employees;
            //gvView.DataBind();
            ListBind();
        }

        private void ListBind()
        {
            IList<EmployeeInfo> list = null;
            if (txtSearch.Text.Trim() != "")
            {
                list = EmployeeManager.Search(int.MaxValue, 0, "", " ((u.lastnamecn+u.firstnamecn like '%'+@code+'%' OR u.username like '%'+@code+'%'))", 
                    new ESP.Framework.DataAccess.Utilities.DbDataParameter[] {
                        new ESP.Framework.DataAccess.Utilities.DbDataParameter(DbType.String, "code", txtSearch.Text.Trim()) });
            }
            else
            {
                list = EmployeeManager.GetAll(); ;
            }
            gvView.DataSource = list;
            gvView.DataBind();

            if (gvView.PageCount > 1)
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
            labPageCount.Text = labPageCountT.Text = (gvView.PageIndex + 1).ToString() + "/" + gvView.PageCount.ToString();
            if (gvView.PageCount > 0)
            {
                if (gvView.PageIndex + 1 == gvView.PageCount)
                    disButton("last");
                else if (gvView.PageIndex == 0)
                    disButton("first");
                else
                    disButton("");
            }
        }

        protected void gvView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //HyperLink link = (HyperLink)e.Row.FindControl("hypRoleID");
                //link.NavigateUrl = "UserForm.aspx?userid=" + gvView.DataKeys[e.Row.RowIndex].Values[0].ToString();
                //  link.Text = gvView.DataKeys[e.Row.RowIndex].Values[0].ToString();
            }
        }

        #region 分页设置
        protected void gvView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvView.PageIndex = e.NewPageIndex;
            ListBind();
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            Paging(gvView.PageCount);
        }
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            Paging(0);
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            Paging((gvView.PageIndex + 1) > gvView.PageCount ? gvView.PageCount : (gvView.PageIndex + 1));
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Paging((gvView.PageIndex - 1) < 0 ? 0 : (gvView.PageIndex - 1));
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        private void Paging(int pageIndex)
        {
            GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
            gvView_PageIndexChanging(new object(), e);
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
    }
}