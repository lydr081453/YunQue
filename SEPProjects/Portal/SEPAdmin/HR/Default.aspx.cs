using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.WebPages;

namespace SEPAdmin.HR
{
    public partial class Default : DefaultPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListBind();
                ListBind2();
            }
        }
        #region 转正提醒
        private void ListBind()
        {
            string strWhere = " and a.status = 1 and regularStatus=0  ";
            strWhere += string.Format(" and (a.ProbationDate<='{0}') ", DateTime.Now.AddMonths(1).ToString());
           
            List<ESP.HumanResource.Entity.EmployeeBaseInfo> list = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetUserModelList(UserInfo, strWhere);
            gvList.DataSource = list;
            gvList.DataBind();

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

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                int id = int.Parse(e.CommandArgument.ToString());

                ListBind();
            }
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!string.IsNullOrEmpty(gvList.DataKeys[e.Row.RowIndex].Values[1].ToString()))
                {
                    if (gvList.DataKeys[e.Row.RowIndex].Values[1].ToString() == "1")
                    {
                        e.Row.Cells[5].Text = "试用期员工";
                        // e.Row.Cells[6].Text = string.Format("<a href='PassedCheckInEdit.aspx?sysId={0}'><img src='../../images/edit.gif' border='0px;'></a>", gvList.DataKeys[e.Row.RowIndex].Values[0].ToString());                       
                    }
                    else
                    {
                        e.Row.Cells[5].Text = "正式员工";
                        // e.Row.Cells[6].Text = "";                       
                    }
                    Repeater rep = (Repeater)e.Row.FindControl("repJob");
                    List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> list = (List<ESP.HumanResource.Entity.EmployeesInPositionsInfo>)gvList.DataKeys[e.Row.RowIndex].Values[2];
                    if (list.Count > 0 && list != null)
                    {
                        rep.DataSource = list;
                        rep.DataBind();
                    }
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
        #endregion

        #region 合同提醒
        private void ListBind2()
        {
            string strWhere = " and (a.status = 1 or a.status = 3) ";

            strWhere += string.Format(" and datediff(mm, cast('{1}' as smalldatetime), {2} ) >= 0 and datediff(mm, cast('{0}' as smalldatetime), {2} ) <= 0", DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"), "c.contractEndDate");

            // List<ESP.HumanResource.Entity.EmployeeBaseInfo> list = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModelList(strWhere);
            List<ESP.HumanResource.Entity.EmployeeBaseInfo> list = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetUserModelList(UserInfo, strWhere);
            gvList2.DataSource = list;
            gvList2.DataBind();

            if (gvList2.PageCount > 1)
            {
                PageBottom2.Visible = true;
                PageTop2.Visible = true;
            }
            else
            {
                PageBottom2.Visible = false;
                PageTop2.Visible = false;
            }
            if (list.Count > 0)
            {
                tabTop2.Visible = true;
                tabBottom2.Visible = true;
            }
            else
            {
                tabTop2.Visible = false;
                tabBottom2.Visible = false;
            }

            labAllNum2.Text = labAllNumT2.Text = list.Count.ToString();
            labPageCount2.Text = labPageCountT2.Text = (gvList2.PageIndex + 1).ToString() + "/" + gvList2.PageCount.ToString();
            if (gvList2.PageCount > 0)
            {
                if (gvList2.PageIndex + 1 == gvList2.PageCount)
                    disButton2("last");
                else if (gvList2.PageIndex == 0)
                    disButton2("first");
                else
                    disButton2("");
            }
        }

        protected void gvList2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                int id = int.Parse(e.CommandArgument.ToString());

                ListBind2();
            }
        }

        protected void gvList2_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!string.IsNullOrEmpty(gvList2.DataKeys[e.Row.RowIndex].Values[1].ToString()))
                {
                    if (gvList2.DataKeys[e.Row.RowIndex].Values[1].ToString() == "1")
                    {
                        e.Row.Cells[4].Text = "试用期员工";
                        // e.Row.Cells[6].Text = string.Format("<a href='PassedCheckInEdit.aspx?sysId={0}'><img src='../../images/edit.gif' border='0px;'></a>", gvList2.DataKeys[e.Row.RowIndex].Values[0].ToString());
                    }
                    else
                    {
                        e.Row.Cells[4].Text = "正式员工";
                        // e.Row.Cells[6].Text = "";
                    }

                    Repeater rep = (Repeater)e.Row.FindControl("repJob2");
                    List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> list = (List<ESP.HumanResource.Entity.EmployeesInPositionsInfo>)gvList2.DataKeys[e.Row.RowIndex].Values[2];
                    if (list.Count > 0 && list != null)
                    {
                        rep.DataSource = list;
                        rep.DataBind();
                    }
                }
            }
        }

        #region 分页设置
        protected void gvList2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList2.PageIndex = e.NewPageIndex;
            ListBind2();
        }

        protected void btnLast2_Click(object sender, EventArgs e)
        {
            Paging2(gvList2.PageCount);
        }
        protected void btnFirst2_Click(object sender, EventArgs e)
        {
            Paging2(0);
        }
        protected void btnNext2_Click(object sender, EventArgs e)
        {
            Paging2((gvList2.PageIndex + 1) > gvList2.PageCount ? gvList2.PageCount : (gvList2.PageIndex + 1));
        }
        protected void btnPrevious2_Click(object sender, EventArgs e)
        {
            Paging2((gvList2.PageIndex - 1) < 0 ? 0 : (gvList2.PageIndex - 1));
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        private void Paging2(int pageIndex)
        {
            GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
            gvList2_PageIndexChanging(new object(), e);
        }

        /// <summary>
        /// 分页按钮的显示设置
        /// </summary>
        /// <param name="page"></param>
        private void disButton2(string page)
        {
            switch (page)
            {
                case "first":
                    btnFirst3.Enabled = false;
                    btnPrevious3.Enabled = false;
                    btnNext3.Enabled = true;
                    btnLast3.Enabled = true;

                    btnFirst4.Enabled = false;
                    btnPrevious4.Enabled = false;
                    btnNext4.Enabled = true;
                    btnLast4.Enabled = true;
                    break;
                case "last":
                    btnFirst3.Enabled = true;
                    btnPrevious3.Enabled = true;
                    btnNext3.Enabled = false;
                    btnLast3.Enabled = false;

                    btnFirst4.Enabled = true;
                    btnPrevious4.Enabled = true;
                    btnNext4.Enabled = false;
                    btnLast4.Enabled = false;
                    break;
                default:
                    btnFirst3.Enabled = true;
                    btnPrevious3.Enabled = true;
                    btnNext3.Enabled = true;
                    btnLast3.Enabled = true;

                    btnFirst4.Enabled = true;
                    btnPrevious4.Enabled = true;
                    btnNext4.Enabled = true;
                    btnLast4.Enabled = true;
                    break;
            }
        }

        #endregion
        #endregion
    }
}
