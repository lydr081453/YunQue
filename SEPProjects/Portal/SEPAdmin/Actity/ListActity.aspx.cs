using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.Actity
{
    public partial class ListActity : ESP.Web.UI.PageBase
    {
        private int pageSize = 20;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        /// <summary>
        /// 新增培训
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLink_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddActity.aspx");
        }

        private void BindData()
        {
            string sqlWhere = string.Empty;
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                sqlWhere = " where ActityTitle like '%'+'" + txtSearch.Text + "'+'%' or ActityContent like '%'+'" + txtSearch.Text + "'+'%' or Lecturer like '%'+'" + txtSearch.Text + "'+'%'";
            }
            List<ESP.HumanResource.Entity.ActityInfo> list = new ESP.HumanResource.BusinessLogic.ActityManager().GetList(sqlWhere);
            this.gvActity.DataSource = list;
            this.gvActity.DataBind();

            if (list.Count <= pageSize)
            {
                lblTotalCount.Text = list.Count.ToString();
                lblTotalCount2.Text = list.Count.ToString();
                lblPageCount.Text = 1 + "";
                lblPageCount2.Text = 1 + "";
            }
            else
            {
                if (list.Count % pageSize != 0)
                {
                    lblTotalCount.Text = list.Count.ToString();
                    lblTotalCount2.Text = list.Count.ToString();
                    lblPageCount.Text = (list.Count / pageSize + 1) + "";
                    lblPageCount2.Text = (list.Count / pageSize + 1) + "";
                }
                else
                {
                    lblTotalCount.Text = list.Count.ToString();
                    lblTotalCount2.Text = list.Count.ToString();
                    lblPageCount.Text = (list.Count / pageSize) + "";
                    lblPageCount2.Text = (list.Count / pageSize) + "";
                }
            }
        }
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();

        }

        #region 翻页
        protected void btnLast_Click(object sender, EventArgs e)
        {
            Paging(gvActity.PageCount);
        }
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            Paging(0);
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            Paging((gvActity.PageIndex + 2) >= gvActity.PageCount ? gvActity.PageCount : (gvActity.PageIndex + 1));
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Paging((gvActity.PageIndex - 1) < 1 ? 0 : (gvActity.PageIndex - 1));
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

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvActity.PageIndex = e.NewPageIndex;
            BindData();
        }
        #endregion

        protected void gvGift_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());
            //ESP.UserPoint.Entity.GiftInfo model = ESP.UserPoint.BusinessLogic.GiftManager.GetModel(id);
            ESP.HumanResource.Entity.ActityInfo actityInfo = new ESP.HumanResource.BusinessLogic.ActityManager().GetModel(id);
            if (e.CommandName == "Delete")
            {
                DeleteItem(id);
            }
            else if (e.CommandName == "Update")
            {
                Response.Redirect("AddActity.aspx?id=" + id);
            }
        }

        /// <summary>
        /// 删除活动
        /// </summary>
        /// <param name="id">活动id</param>
        private void DeleteItem(int id)
        {
            int result = new ESP.HumanResource.BusinessLogic.ActityManager().Delete(id);
            if (result == 1)
            {
                Response.Write("<script>alert('删除成功！')</script>");
                Server.Transfer("ListActity.aspx");
            }
        }

        protected void gvGift_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
            ESP.HumanResource.Entity.ActityInfo model = (ESP.HumanResource.Entity.ActityInfo)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.WebControls.Label ActityTime = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblActityTime");
                System.Web.UI.WebControls.LinkButton linkDelete = e.Row.FindControl("linkDelete") as LinkButton;
                System.Web.UI.WebControls.LinkButton linkUpdate = e.Row.FindControl("linkUpdate") as LinkButton;
                if (ActityTime != null)
                {
                    ActityTime.Text = model.ActityTime.ToString("yyyy-MM-dd HH:mm");
                }
                if (model.ActityTime <= DateTime.Now)
                {
                    if (linkDelete != null)
                    {
                        linkDelete.Visible = false;
                    }
                    if (linkUpdate != null)
                    {
                        linkUpdate.Visible = false;
                    }
                }
            }
        }
    }
}
