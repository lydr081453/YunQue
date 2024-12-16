using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Excel;
using System.Text;

namespace PurchaseWeb.Gift.view
{
    public partial class ListGift : ESP.Web.UI.PageBase
    {
        private int pageSize = 20;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindingData();
            }
        }

        private void BindingData()
        {
            string sqlWhere = string.Empty;
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                sqlWhere = " and Name like '%'+'" + txtName.Text + "'+'%'";
            }
            //拼 查询 的 sql条件 
            IList<ESP.UserPoint.Entity.GiftInfo> list = ESP.UserPoint.BusinessLogic.GiftManager.GetList(sqlWhere);
            if (list != null && list.Count > 0)
            {
                this.gvGift.DataSource = list;
                this.gvGift.DataBind();
                lblTotalCount.Text = list.Count.ToString();
                lblTotalCount2.Text = list.Count.ToString();
                if (list.Count > pageSize)
                {
                    if (list.Count % pageSize == 0)
                    {
                        lblPageCount.Text = (list.Count / pageSize).ToString();
                        lblPageCount2.Text = (list.Count / pageSize).ToString();
                    }
                    else
                    {
                        lblPageCount.Text = (list.Count / pageSize + 1).ToString();
                        lblPageCount2.Text = (list.Count / pageSize + 1).ToString();
                    }
                }
                else
                {
                    lblPageCount.Text = 1 + "";
                    lblPageCount2.Text = 1 + "";
                }
            }
        }

        protected void gvGift_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());
            ESP.UserPoint.Entity.GiftInfo model = ESP.UserPoint.BusinessLogic.GiftManager.GetModel(id);

            if (e.CommandName == "Del")
            {
                //调用删除方法
                DeleteItem(id);
            }
        }
        protected void gvGift_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
            ESP.UserPoint.Entity.GiftInfo model = (ESP.UserPoint.Entity.GiftInfo)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.WebControls.Label lblStatus = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus");
                System.Web.UI.WebControls.Label lblCreateTime = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblCreateTime");
                if (lblStatus != null)
                {
                    if (model.State == 1)//linkDelete
                    {
                        lblStatus.Text = "已审核";
                        System.Web.UI.WebControls.LinkButton linkDelete = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("linkDelete");
                        linkDelete.Visible = false;
                    }
                    else if (model.State == 0)
                    {
                        lblStatus.Text = "待确认";
                    }
                    else
                    {
                        lblStatus.Text = "活动结束";
                        System.Web.UI.WebControls.LinkButton linkDelete = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("linkDelete");
                        linkDelete.Visible = false;
                    }
                }
                if (lblCreateTime != null)
                {
                    lblCreateTime.Text = model.CreateTime.ToString("yyyy-MM-dd");
                }
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindingData();
        }
        private void DeleteItem(int id)
        {
            int result = ESP.UserPoint.BusinessLogic.GiftManager.Delete(id);
            if (result == 1)
            {
                Response.Write("<script>alert('删除成功！')</script>");
                Server.Transfer("ListGift.aspx");
            }
        }

        protected void btnLink_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddGift.aspx");
        }

        #region 翻页
        protected void btnLast_Click(object sender, EventArgs e)
        {
            Paging(gvGift.PageCount);
        }
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            Paging(0);
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            Paging((gvGift.PageIndex + 2) >= gvGift.PageCount ? gvGift.PageCount : (gvGift.PageIndex + 1));
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Paging((gvGift.PageIndex - 1) < 1 ? 0 : (gvGift.PageIndex - 1));
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
            gvGift.PageIndex = e.NewPageIndex;
            BindingData();
        }
        #endregion
    }
}
