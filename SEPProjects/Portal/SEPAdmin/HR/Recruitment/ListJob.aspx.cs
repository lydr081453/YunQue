using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.HR.Recruitment
{
    public partial class ListJob : ESP.Web.UI.PageBase
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
            Response.Redirect("AddJob.aspx");
        }

        private void BindData()
        {
            string sqlWhere = string.Empty;
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                sqlWhere = " where JobName like '%'+'" + txtSearch.Text + "'+'%' or WorkingPlace like '%'+'" + txtSearch.Text + "'+'%' or Responsibilities like '%'+'" + txtSearch.Text + "'+'%' or Requirements like '%'+'" + txtSearch.Text + "'+'%'";
            }
            List<ESP.HumanResource.Entity.JobInfo> list = new ESP.HumanResource.BusinessLogic.JobManager().GetList(sqlWhere);
            this.gvJob.DataSource = list;
            this.gvJob.DataBind();

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
            if (txtSearch.Text.Length <= 50)
                BindData();
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('最大50字符，搜索条件输入超限！');", true);
                txtSearch.Focus();
            }

        }

        #region 翻页
        protected void btnLast_Click(object sender, EventArgs e)
        {
            Paging(gvJob.PageCount);
        }
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            Paging(0);
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            Paging((gvJob.PageIndex + 2) >= gvJob.PageCount ? gvJob.PageCount : (gvJob.PageIndex + 1));
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Paging((gvJob.PageIndex - 1) < 1 ? 0 : (gvJob.PageIndex - 1));
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
            gvJob.PageIndex = e.NewPageIndex;
            BindData();
        }
        #endregion

        protected void gvGift_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());
            //ESP.HumanResource.Entity.JobInfo jobInfo = new ESP.HumanResource.BusinessLogic.JobManager().GetModel(id);
            if (e.CommandName == "Delete")
            {
                DeleteItem(id);
            }
            else if (e.CommandName == "Update")
            {
                Response.Redirect("AddJob.aspx?id=" + id);
            }
        }

        /// <summary>
        /// 删除活动
        /// </summary>
        /// <param name="id">活动id</param>
        private void DeleteItem(int id)
        {
            int result = new ESP.HumanResource.BusinessLogic.JobManager().Delete(id);
            if (result == 1)
            {
                Response.Write("<script>alert('删除成功！')</script>");
                Server.Transfer("ListJob.aspx");
            }
        }

        protected void gvGift_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
            ESP.HumanResource.Entity.JobInfo model = (ESP.HumanResource.Entity.JobInfo)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.WebControls.Label ActityTime = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblActityTime");
                System.Web.UI.WebControls.Label UpdateTime = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblUpdateTime");
                System.Web.UI.WebControls.Label WorkingPlace = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblWorkingPlace");
                System.Web.UI.WebControls.LinkButton linkDelete = e.Row.FindControl("linkDelete") as LinkButton;
                System.Web.UI.WebControls.LinkButton linkUpdate = e.Row.FindControl("linkUpdate") as LinkButton;
                if (ActityTime != null)
                {
                    ActityTime.Text = model.CreateTime.ToString("yyyy-MM-dd");
                }
                if (UpdateTime != null)
                    UpdateTime.Text = model.UpdateTime.ToString("yyyy-MM-dd");
                WorkingPlace.Text = model.WorkingPlace;
                //switch (model.WorkingPlace)
                //{
                //    case "0":
                //        WorkingPlace.Text = "北京";
                //        break;
                //    case "1":
                //        WorkingPlace.Text = "上海";
                //        break;
                //    case "2":
                //        WorkingPlace.Text = "深圳";
                //        break;
                //    case "3":
                //        WorkingPlace.Text = "广州";
                //        break;
                //    case "4":
                //        WorkingPlace.Text = "长沙";
                //        break;
                //    case "5":
                //        WorkingPlace.Text = "重庆";
                //        break;
                //}
            }
        }
    }
}
