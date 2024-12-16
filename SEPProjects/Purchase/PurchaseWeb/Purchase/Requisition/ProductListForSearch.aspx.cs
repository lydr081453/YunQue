using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_ProductListForSearch : ESP.Web.UI.PageBase
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
            ListBind();
        }
    }

    private void ListBind()
    {
        string terms = "";
        List<SqlParameter> parms = new List<SqlParameter>();
        terms += " and supplierId = @supplierId and isShow = 1";
        parms.Add(new SqlParameter("@supplierId", supplierId));

        List<ProductInfo> productList = ProductManager.getModelList(terms, parms);
        gvItem.DataSource = productList;
        gvItem.DataBind();
    }
}
