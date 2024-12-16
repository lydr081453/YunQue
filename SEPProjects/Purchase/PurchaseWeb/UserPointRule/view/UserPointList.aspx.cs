using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PurchaseWeb.UserPointRule.view
{
    public partial class UserPointList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindList();
            }
        }

        private void bindList()
        {
            string strwhere = string.Empty;
            if (!string.IsNullOrEmpty(txtKeyword.Text))
            {
                strwhere = " and userid in(select userid from sep_users where username like '%" + txtKeyword.Text + "%' or (lastnamecn+firstnamecn) like '%" + txtKeyword.Text + "%')";
            }
            if (!string.IsNullOrEmpty(txtPointBegin.Text) && !string.IsNullOrEmpty(txtPointEnd.Text))
            {
                strwhere += " and (sp between " + txtPointBegin.Text + " and " + txtPointEnd.Text + ")";
            }

            if (ddlasc.SelectedValue == "1")
                strwhere += " order by sp desc";
            else
                strwhere += " order by sp asc";
            IList<ESP.UserPoint.Entity.UserPointInfo> userlist = ESP.UserPoint.BusinessLogic.UserPointManager.GetList(strwhere);
            this.gvG.DataSource = userlist;
            this.gvG.DataBind();

            labAllNum.Text = labAllNumT.Text = userlist.Count.ToString();
            labPageCount.Text = labPageCountT.Text = (gvG.PageIndex + 1).ToString() + "/" + gvG.PageCount.ToString();
            labAllNumT.Text = labAllNum.Text;
            labPageCountT.Text = labPageCount.Text;
        }


        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.UserPoint.Entity.UserPointInfo userpoint = (ESP.UserPoint.Entity.UserPointInfo)e.Row.DataItem;
                ESP.Framework.Entity.UserInfo userModel = ESP.Framework.BusinessLogic.UserManager.Get(userpoint.UserID);
                IList<ESP.Framework.Entity.EmployeePositionInfo> positions = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(userModel.UserID);
                ESP.Finance.Entity.DepartmentViewInfo dept = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(positions[0].DepartmentID);

                Label lblUserName = (Label)e.Row.FindControl("lblUserName");
                Label lblEmail = (Label)e.Row.FindControl("lblEmail");
                Label lblDepartment = (Label)e.Row.FindControl("lblDepartment");

                lblUserName.Text = userModel.LastNameCN + userModel.FirstNameCN;
                lblEmail.Text = userModel.Email;
                lblDepartment.Text = dept.level1 + "-" + dept.level2 + "-" + dept.level3;
            }
        }

        #region "Page"
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

        private void Paging(int pageIndex)
        {
            GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
            gvG_PageIndexChanging(new object(), e);
        }


        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            bindList();
        }

        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindList();
        }

        //protected void link_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("../../Gift/view/ExchangeManager.aspx");
        //}
    }
}
