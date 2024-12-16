using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace PurchaseWeb.UserPointRule.view
{
    public partial class ListUserPointRule : ESP.Web.UI.PageBase
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
                sqlWhere = " and RuleName like '%'+'" + txtName.Text + "'+'%'";
            }
            //拼 查询 的 sql条件 
            IList<ESP.UserPoint.Entity.UserPointRuleInfo> list = ESP.UserPoint.BusinessLogic.UserPointRuleManager.GetList(sqlWhere);
            if (list != null && list.Count > 0)
            {
                this.gvUserPointRule.DataSource = list;
                this.gvUserPointRule.DataBind();
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


        protected void btnLink_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddUserPointRule.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindingData();
        }

         #region 翻页
        protected void btnLast_Click(object sender, EventArgs e)
        {
            Paging(gvUserPointRule.PageCount);
        }
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            Paging(0);
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            Paging((gvUserPointRule.PageIndex + 2) >= gvUserPointRule.PageCount ? gvUserPointRule.PageCount : (gvUserPointRule.PageIndex + 1));
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Paging((gvUserPointRule.PageIndex - 1) < 1 ? 0 : (gvUserPointRule.PageIndex - 1));
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
            gvUserPointRule.PageIndex = e.NewPageIndex;
            BindingData();
        }
        #endregion


        protected void gvUserPointRule_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());
            ESP.UserPoint.Entity.UserPointRuleInfo model = ESP.UserPoint.BusinessLogic.UserPointRuleManager.GetModel(id);

            if (e.CommandName == "Del")
            {
                //调用删除方法
                DeleteItem(id);
            }

        }
        protected void gvUserPointRule_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
            ESP.UserPoint.Entity.UserPointRuleInfo model = (ESP.UserPoint.Entity.UserPointRuleInfo)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //System.Web.UI.WebControls.Label lblStatus = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus");
                //System.Web.UI.WebControls.Label lblCreateTime = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblCreateTime");
                //if (lblStatus != null)
                //{
                //    if (model.State == 1)
                //        lblStatus.Text = "已审核";
                //}
                //if (lblCreateTime != null)
                //{
                //    lblCreateTime.Text = model.CreateTime.ToString("yyyy-MM-dd");
                //}
            }

        }


        private void DeleteItem(int id)
        {
            int result = ESP.UserPoint.BusinessLogic.UserPointRuleManager.Delete(id);
            if (result > 0)
            {
                Response.Write("<script>alert('删除成功！')</script>");
                Server.Transfer("ListUserPointRule.aspx");
            }
        }
       
    }
}
