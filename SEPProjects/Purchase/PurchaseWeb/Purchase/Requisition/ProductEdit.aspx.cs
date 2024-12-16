using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_ProductEdit : ESP.Web.UI.PageBase
{
    int productId = 0;
    int supplierId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        #region AjaxProRegister
        AjaxPro.Utility.RegisterTypeForAjax(typeof(ESP.Purchase.DataAccess.TypeDataProvider));
        #endregion

        if (!string.IsNullOrEmpty(Request["productId"]))
        {
            productId = int.Parse(Request["productId"]);
        }
        if (!string.IsNullOrEmpty(Request["sid"]))
        {
            supplierId = int.Parse(Request["sid"]);
            SupplierInfo ms = SupplierManager.GetModel(supplierId);
            txtSupplier.Text = ms.supplier_name.ToString();
        }
        if (!IsPostBack)
        {
            //BindTree(treeView.Nodes, 0, TypeBaseHelperManager.GetListByParentId(0));
            List<TypeInfo> list = TypeManager.GetListByParentId(0);
            ddltype.DataSource = list;
            ddltype.DataTextField = "typename";
            ddltype.DataValueField = "typeid";
            ddltype.DataBind();
            ddltype.Items.Insert(0, new ListItem("请选择...", "-1"));
            ddltype1.Items.Insert(0, new ListItem("请选择...", "-1"));
            ddltype2.Items.Insert(0, new ListItem("请选择...", "-1"));
            BindInfo();
        }
        if (Request["backUrl"] == "ProposedSupplierEdit.aspx" || Request["backUrl"] == "TempSupplierEdit.aspx")
        {
            rdlShow.Enabled = false;
            rdlShow.SelectedValue = "0";
        }
    }

    void BindTree(TreeNodeCollection nds, int parentId, List<TypeInfo> items)
    {
        TreeNode tn = null;
        foreach (TypeInfo model in items)
        {
            tn = new TreeNode(model.typename);
            nds.Add(tn);
            BindTree(tn.ChildNodes, model.typeid, TypeManager.GetListByParentId(model.typeid));
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if(supplierId >0)
            Response.Redirect(Request["backUrl"]+"?supplierId="+supplierId.ToString());
        else
            Response.Redirect("ProductList.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ProductInfo model = null;
        if (productId == 0)
        {
            model = new ProductInfo();
            productId = ProductManager.Add(getModel(model));
            if (productId > 0)
            {
                //记录操作日志
                ESP.Logging.Logger.Add(string.Format("{0}对T_Product表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, productId, "添加目录物品"), "添加");
                if (supplierId > 0)
                    ClientScript.RegisterStartupScript(typeof(string), "", "window.location='" + Request["backUrl"] + "?supplierId=" + supplierId.ToString() + "';alert('保存成功！');", true);
                else
                    ClientScript.RegisterStartupScript(typeof(string), "", "window.location='ProductList.aspx';alert('保存成功！');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存失败！');", true);
            }
        }
        else
        {
            model = ProductManager.GetModel(productId);
            if (ProductManager.Update(getModel(model)) > 0)
            {
                //记录操作日志
                ESP.Logging.Logger.Add(string.Format("{0}对T_Product表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, productId, "编辑目录物品"), "编辑");
                if (supplierId > 0)
                    ClientScript.RegisterStartupScript(typeof(string), "", "window.location='"+Request["backUrl"]+"?supplierId=" + supplierId.ToString() + "';alert('保存成功！');", true);
                else
                    ClientScript.RegisterStartupScript(typeof(string), "", "window.location='ProductList.aspx';alert('保存成功！');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存失败！');", true);
            }
        }
    }

    private ProductInfo getModel(ProductInfo model)
    {
        //model.supplierId = int.Parse(hidSupplierId.Value);
        model.supplierId = supplierId;
        model.productName = txtProductName.Text.Trim();
        model.productType = int.Parse(hidtype2.Value);
        if (txtproductDes.Text.Length > 1000)
            model.productDes = txtproductDes.Text.Substring(0, 1000);
        else
            model.productDes = txtproductDes.Text.Trim();
        model.productUnit = txtproductUnit.Text.Trim();
        model.productClass = txtproductClass.Text.Trim();
        if (txtproductPrice.Text != "")
            model.ProductPrice = decimal.Parse(txtproductPrice.Text.ToString());
        else
            model.ProductPrice = 0;
        model.IsShow = int.Parse(rdlShow.SelectedValue);
        return model;
    }

    private void BindInfo()
    {
        if (productId == 0) return;
        ProductInfo model = ProductManager.GetModel(productId);
        //hidSupplierId.Value = model.supplierId.ToString();
        txtSupplier.Text = model.supplierName;
        txtProductName.Text = model.productName;
        txtproductDes.Text = model.productDes;
        txtproductUnit.Text = model.productUnit;
        //hidproductType.Value = model.productType.ToString();
        txtproductClass.Text = model.productClass;
        txtproductPrice.Text = model.ProductPrice.ToString();
        rdlShow.SelectedValue = model.IsShow.ToString();

        hidtype2.Value = model.productType.ToString();
        TypeInfo l2 = TypeManager.GetModel(model.productType);
        TypeInfo l1 = TypeManager.GetModel(l2.parentId);
        hidtype1.Value = l1.typeid.ToString();

        List<TypeInfo> items = TypeManager.GetListByParentId(State.RootType);
        ddltype.DataSource = items;
        ddltype.DataTextField = "typename";
        ddltype.DataValueField = "typeid";
        ddltype.DataBind();
        ddltype.SelectedValue = l1.parentId.ToString();
        hidtype.Value = l1.parentId.ToString();
    }
}
