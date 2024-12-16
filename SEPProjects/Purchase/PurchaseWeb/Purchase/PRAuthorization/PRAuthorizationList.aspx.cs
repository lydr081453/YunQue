using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace PurchaseWeb.Purchase.PRAuthorization
{
    public partial class PRAuthorizationList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("PRAuthorizationEdit.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListBind();
        }


        private void ListBind()
        {
            string terms = "";
            List<SqlParameter> parms = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(txtKey.Text.Trim()))
            {
                terms += " and (createusername like '%"+txtKey.Text.Trim()+"%' or username like '%"+txtKey.Text.Trim()+"%')";
            }
            if (radStatus.SelectedValue != "-1")
            {
                terms += " and status=" + radStatus.SelectedValue;
            }
            gvList.DataSource = ESP.Purchase.BusinessLogic.PRAuthorizationManager.GetList(terms, parms);
            gvList.DataBind();
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            ListBind();
        }
    }
}
