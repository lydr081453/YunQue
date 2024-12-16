using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Entity;

namespace SEPAdmin.Management.UserManagement
{
    public partial class PositionList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListBind();
            }
        }

        private void ListBind()
        {
            string terms = "";
            List<SqlParameter> parms = new List<SqlParameter>();

            if (txtName.Text.Trim() != "")
            {
                terms += " and PositionName like '%'+@key+'%' or LevelName like '%'+@key+'%'";
                parms.Add(new SqlParameter("@key", txtName.Text.Trim()));
            }

            gvList.DataSource = PositionBaseManager.GetList(terms, parms);
            gvList.DataBind();
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            ListBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListBind();
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            int Id = int.Parse(((ImageButton)sender).CommandArgument.ToString());
            PositionBaseManager.Delete(Id);
            ListBind();
        }
    }
}
