using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Supplier.BusinessLogic;

namespace PurchaseWeb.Purchase.Requisition
{
    public partial class SupplierUsers : ESP.Web.UI.PageBase
    {
        int supplierId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["supplierId"]))
                supplierId = int.Parse(Request["supplierId"]);

            if (!IsPostBack)
            {
                string terms = " supplierId=" + supplierId + " and ( isdel is null or IsDel = 0) and (iseffective is null or IsEffective=1)";
                var list = SC_SupplierSubsidiaryUsersManager.GetList(terms);
                gvList.DataSource = list;
                gvList.DataBind();
            }
        }
    }
}
