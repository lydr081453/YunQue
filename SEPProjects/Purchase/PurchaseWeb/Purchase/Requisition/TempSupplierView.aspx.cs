using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PurchaseWeb.Purchase.Requisition
{
    public partial class TempSupplierView : ESP.Web.UI.PageBase
    {
        int supplierId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["supplierId"]))
            {
                supplierId = int.Parse(Request["supplierId"]);
            }
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["isback"]) && Request["isback"] != "0")
                {
                    this.btnBack.Visible = false;
                    //SiteMapPath1
                    Page.Master.FindControl("SiteMapPath1").Visible = false;
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("SupplierInfoList.aspx");
        }
    }
}
