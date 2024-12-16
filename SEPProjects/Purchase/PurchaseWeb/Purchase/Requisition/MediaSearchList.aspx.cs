using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using ESP.Purchase.BusinessLogic;
using System.Collections;

namespace PurchaseWeb.Purchase.Requisition
{
    public partial class MediaSearchList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListBind();
            }
        }

        /// <summary>
        /// 绑定列表
        /// </summary>
        private void ListBind()
        {
            List<SqlParameter> parms = new List<SqlParameter>();
            string term = string.Empty;
            term = " a.status not in (@status1,@status2,@status3) and a.prtype=@prtype";
            parms.Add(new SqlParameter("@status1", State.requisition_del));
            parms.Add(new SqlParameter("@status2", State.requisition_save));
            parms.Add(new SqlParameter("@status3", State.requisition_return));
            parms.Add(new SqlParameter("@prtype", (int)PRTYpe.MediaPR));
            if (!string.IsNullOrEmpty(txtProjectCode.Text.Trim()))
            {
                term += " and a.project_Code like '%'+@projectCode+'%'";
                parms.Add(new SqlParameter("@projectCode", txtProjectCode.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtMediaName.Text.Trim()))
            {
                term += " and c.MediaName like '%'+@mediaName+'%'";
                parms.Add(new SqlParameter("@mediaName", txtMediaName.Text.Trim()));
            }
            if(!string.IsNullOrEmpty(txtReportorName.Text.Trim()))
            {
                term += " and c.ReporterName like '%'+@ReporterName+'%'";
                parms.Add(new SqlParameter("@ReporterName", txtReportorName.Text.Trim()));
            }
            System.Data.DataSet ds = MediaOrderManager.GetListByGID(term, parms);
            gvG.DataSource = ds;
            gvG.DataBind();

            if (gvG.PageCount > 1)
            {
                PageBottom.Visible = true;
                PageTop.Visible = true;
            }
            else
            {
                PageBottom.Visible = false;
                PageTop.Visible = false;
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                tabTop.Visible = true;
                tabBottom.Visible = true;
            }
            else
            {
                tabTop.Visible = false;
                tabBottom.Visible = false;
            }

            labAllNum.Text = labAllNumT.Text = ds.Tables[0].Rows.Count.ToString();
            labPageCount.Text = labPageCountT.Text = (gvG.PageIndex + 1).ToString() + "/" + gvG.PageCount.ToString();
            if (gvG.PageCount > 0)
            {
                if (gvG.PageIndex + 1 == gvG.PageCount)
                    disButton("last");
                else if (gvG.PageIndex == 0)
                    disButton("first");
                else
                    disButton("");
            }

        }

        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }


        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
         
        }

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            ListBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListBind();
        }

        #region 分页
        protected void btnLast_Click(object sender, EventArgs e)
        {
            Paging(gvG.PageCount);
        }
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            Paging(0);
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            Paging((gvG.PageIndex + 1) > gvG.PageCount ? gvG.PageCount : (gvG.PageIndex + 1));
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Paging((gvG.PageIndex - 1) < 0 ? 0 : (gvG.PageIndex - 1));
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        private void Paging(int pageIndex)
        {
            GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
            gvG_PageIndexChanging(new object(), e);
        }

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
