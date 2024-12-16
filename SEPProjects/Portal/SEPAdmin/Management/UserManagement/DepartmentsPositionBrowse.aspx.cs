using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;

namespace SEPAdmin.Management.UserManagement
{
    public partial class DepartmentsPositionBrowse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            IList<DepartmentPositionInfo> list = DepartmentPositionManager.GetByDepartment(0);
            gvView.DataSource = list;
            gvView.DataBind();

            hypDepPost.NavigateUrl = "DepartmentPositionForm.aspx?backurl=DepartmentsPositionBrowse";
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            GridViewRow rw = (GridViewRow)((ImageButton)sender).Parent.Parent;
            HyperLink hypDepID = (HyperLink)rw.FindControl("hypDepID");
            if (hypDepID != null && !string.IsNullOrEmpty(hypDepID.Text))
            {
                DepartmentPositionManager.Delete(Convert.ToInt32(hypDepID.Text));
                Bind();
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow rw = (GridViewRow)((ImageButton)sender).Parent.Parent;
            HyperLink hypDepID = (HyperLink)rw.FindControl("hypDepID");
            if (hypDepID != null && !string.IsNullOrEmpty(hypDepID.Text))
            {
                Response.Redirect("DepartmentPositionForm.aspx?depposid=" + (hypDepID.Text) + "&backurl=DepartmentsPositionBrowse");
            }
        }

        protected void gvView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink link = (HyperLink)e.Row.FindControl("hypDepID");
                link.NavigateUrl = "DepartmentPositionForm.aspx?depid=" + gvView.DataKeys[e.Row.RowIndex].Values[0].ToString() + "&backurl=DepartmentsPositionBrowse";
                link.Text = gvView.DataKeys[e.Row.RowIndex].Values[0].ToString();
            }
        }
    }
}
