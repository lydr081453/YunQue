using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SupplierWeb.UserPage
{
    public partial class MainPage : System.Web.UI.Page
    {
        private int _supplierId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null && int.Parse(Session["user"].ToString()) > 0)
            {
                _supplierId = int.Parse(Session["user"].ToString());
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
    }
}
