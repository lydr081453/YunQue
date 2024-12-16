using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_Print_supplierPrint : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["supplier"] != null)
            {
                List<SupplierInfo> list = (List<SupplierInfo>)Session["supplier"];
                repSupplier.DataSource = list;
                repSupplier.DataBind();
            }
        }
    }
}
