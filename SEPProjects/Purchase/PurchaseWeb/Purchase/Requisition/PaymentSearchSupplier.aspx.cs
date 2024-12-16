using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_PaymentSearchSupplier : ESP.Web.UI.PageBase
{
    private string clientId = "ctl00_ContentPlaceHolder1_";
    int productType = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        #region AjaxProRegister
        AjaxPro.Utility.RegisterTypeForAjax(typeof(TypeDataProvider));
        #endregion
        if (!string.IsNullOrEmpty(Request["source"]))
        {
        }
        if(!string.IsNullOrEmpty(Request["productType"]))
        {
            productType = int.Parse(Request["productType"]);
        }
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["name"]))
            {
                txtSupplierName.Text = Request["name"].ToString();
            }
        }
        listBind();
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

    private void listBind()
    {
        btnClean.Visible = false;
        if (!string.IsNullOrEmpty(txtSupplierName.Text))
        {
            btnClean.Visible = true;
        }
        string strWhere = "";
        List<SqlParameter> parms = new List<SqlParameter>();
        if (txtSupplierName.Text.Trim() != "")
        {
            strWhere += " and supplier_name like '%'+@supplier_name+'%'";
            parms.Add(new SqlParameter("@supplier_name", txtSupplierName.Text.Trim()));
        }

        List<SupplierInfo> list = null;
        if (productType > 0)
        {
            list = SupplierManager.getSupplierListByProductTypeId(productType, strWhere, parms);
        }
        else
        {
            list = SupplierManager.getModelList(strWhere, parms);
        }
        if (null != list)
        {
            gv.DataSource = list;
            gv.DataBind();
        }
    }

    protected void btnClean_Click(object sender, EventArgs e)
    {
        txtSupplierName.Text = "";
        List<SupplierInfo> list = SupplierManager.getModelList("", new List<SqlParameter>());

        if (null != list)
        {
            gv.DataSource = list;
            gv.DataBind();
        }
    }

    protected void add(int id)
    {
        SupplierInfo model = SupplierManager.GetModel(id);
        if (null != model)
        {
            if (!string.IsNullOrEmpty(Request["source"]) && Request["source"] != "product")
            {
                Response.Write("<script>opener.document.all.ctl00_ContentPlaceHolder1_txtSupplier.value='" + model.supplier_name + "';</script>");
                Response.Write("<script>opener.document.all.ctl00_ContentPlaceHolder1_hidSupplierId.value='" + model.id + "';</script>");
                Response.Write("<script>window.close();</script>");
                return;
            }
            else if(!string.IsNullOrEmpty(Request["source"]) && Request["source"] == "product")
            {
                Response.Write("<script>opener.document.all.ctl00_ContentPlaceHolder1_TabContainer1_Tab2_txtSupplierName.value='" + model.supplier_name + "';</script>");
                Response.Write("<script>opener.document.all.ctl00_ContentPlaceHolder1_TabContainer1_Tab2_hidSupplierName.value='" + model.supplier_name + "';</script>");
                Response.Write("<script>opener.document.all.ctl00_ContentPlaceHolder1_TabContainer1_Tab2_hidSupplierId1.value='" + model.id + "';</script>");
                List<List<string>> items = ProductManager.GetTypeListBySupplierId(model.id, "");
                Response.Write("<script>window.opener.clearType();</script>");
                foreach (List<string> item in items)
                {
                    Response.Write("<script>window.opener.setType('"+item[0]+"','"+item[1]+"');</script>");
                }
                if (productType > 0)
                {
                    Response.Write("<script>window.opener.setType2('" + productType + "');</script>");
                }
                if (items.Count > 0 && items[0].Count > 0)
                {
                    TypeInfo typeModel = TypeManager.GetModel(int.Parse(items[0][0].ToString()));
                    if (typeModel != null)
                        Response.Write("<script>window.opener.setType1('" + typeModel.parentId + "');</script>");
                }
                Response.Write("<script>window.close();</script>");
                
                return;
            }
            Response.Write("<script>opener.document.all." + clientId + "txtsupplier_name.value= '" + model.supplier_name + "'</script>");
            Response.Write(@"<script>window.close();</script>");
        }
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            string id = gv.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString();
            add(int.Parse(id));
        }
    }

    protected void btnX_Click(object sender, EventArgs e)
    {
        Response.Write("<script>opener.document.all." + clientId + "txtsupplier_name.style.display='block';</script>");
        Response.Write("<script>opener.document.all." + clientId + "txtsupplier_name.value= ''</script>");  
        Response.Write(@"<script>window.close();</script>");
    }
}
