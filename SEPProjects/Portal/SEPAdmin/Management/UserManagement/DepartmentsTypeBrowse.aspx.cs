using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;

namespace SEPAdmin.UserManagement
{
    public partial class DepartmentsTypeBrowse : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { BindList(); }
        }

        private void BindList()
        {
            IList<DepartmentTypeInfo> list = DepartmentTypeManager.GetAll();
            gvView.DataSource = list;
            gvView.DataBind();
        }
        private void Search()
        { 
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        { Search(); }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow rw = (GridViewRow)((ImageButton)sender).Parent.Parent;
            HiddenField hdnID = (HiddenField)rw.FindControl("hdnID");
            if (hdnID != null && !string.IsNullOrEmpty(hdnID.Value))
            {
                Response.Redirect("DepartmentsTypeForm.aspx?id="+hdnID.Value);
            }
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            GridViewRow rw = (GridViewRow)((ImageButton)sender).Parent.Parent;
            HiddenField hdnID = (HiddenField)rw.FindControl("hdnID");
            if (hdnID != null && !string.IsNullOrEmpty(hdnID.Value))
            {
                DepartmentTypeManager.Delete(Convert.ToInt32(hdnID.Value));
                BindList();
            }
        }

        protected void gvView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lab = (Label)e.Row.FindControl("lblStatus");
                if (((DepartmentTypeInfo)e.Row.DataItem).Status == 0)
                    lab.Text = "是";
                else
                    lab.Text = "否";

            }
        }
    }
}