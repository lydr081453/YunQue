using System;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_ProductView : ESP.Web.UI.PageBase
{
    int productId = 0;
    int supplierId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["productId"]))
        {
            productId = int.Parse(Request["productId"]);
        }
        if (!string.IsNullOrEmpty(Request["sid"]))
            supplierId = int.Parse(Request["sid"]);
        if (!string.IsNullOrEmpty(Request["supplierId"]))
            supplierId = int.Parse(Request["supplierId"]);
        if (!IsPostBack)
        {
            BindInfo();
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["backUrl"]))
            Response.Redirect(Request["backUrl"] + "?supplierId=" + supplierId);
        else
            Response.Redirect("ProductList.aspx");
    }

    private void BindInfo()
    {
        if (productId == 0) return;
        ProductInfo model = ProductManager.GetModel(productId);
        labSupplier.Text = model.supplierName;
        labProductName.Text = model.productName;
        labproductDes.Text = model.productDes;
        labproductUnit.Text = model.productUnit;
        labproductClass.Text = model.productClass;
    }
}
