using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;
using System.Data;

public partial class Purchase_Requisition_ChangedSupplier : ESP.Web.UI.PageBase
{
    private string clientId = "ctl00_ContentPlaceHolder1_supplierInfo_";
    string productTypes = "";
    int generalid = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        #region AjaxProRegister
        AjaxPro.Utility.RegisterTypeForAjax(typeof(TypeDataProvider));
        #endregion
        if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
        {
            generalid = int.Parse(Request[RequestName.GeneralID]);
        }
        if (!IsPostBack)
        {
            //绑定物料类别树
            BindTree(treeView.Nodes, 0, TypeManager.GetListByParentId(0));

            if (!string.IsNullOrEmpty(Request["name"]))
            {
                txtSupplierName.Text = Request["name"].ToString();
            }
            supplierInfo.FindControl("btn").Visible = false;
            supplierInfo.FindControl("trTitle").Visible = false;
            supplierInfo.FindControl("txtfa_no").Visible = false;

            GeneralInfo generalInfoModel = GeneralInfoManager.GetModel(generalid);
            if (generalInfoModel.source != "协议供应商")
            {
                supplierInfo.Model = generalInfoModel;
                supplierInfo.BindInfo();
            }
        }
        listBind();
        supplierInfo.viewControl("非协议供应商");
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
        /*
        //获得已添加采购物品类型ID 
        List<OrderInfo> orderList = OrderInfoManager.GetListByGeneralId(generalid);
        foreach (OrderInfo order in orderList)
        {
            productTypes += order.producttype + ",";
        }
        productTypes = productTypes.Remove(productTypes.Length - 1);


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
        strWhere += " and supplier_type = " + (int)State.supplier_type.agreement;
        List<SupplierInfo> list = null;
        List<SupplierInfo> removeList = new List<SupplierInfo>();
            list = SupplierManager.getModelList(strWhere, parms);
            foreach (SupplierInfo model in list)
            {
                List<int> supplierTypeIds = ProductManager.getSupplierProductTypeIdList(model.id);
                foreach (string typeid in productTypes.Split(','))
                {
                    if (!supplierTypeIds.Contains(int.Parse(typeid)))
                    {
                        removeList.Add(model);
                        break;
                    }
                }
            }
            foreach (SupplierInfo model in removeList)
            {
                list.Remove(model);
            }
        if (null != list)
        {
            gv.DataSource = list;
            gv.DataBind();
        }*/

        //获得已添加采购物品类型ID 
        List<OrderInfo> orderList = OrderInfoManager.GetListByGeneralId(generalid);
        int typeId = 0;
        if (orderList != null && orderList.Count > 0)
            typeId = orderList[0].producttype;

        string Terms = "";
        List<SqlParameter> parms = new List<SqlParameter>();
        if (txtSupplierName.Text.Trim() != "")
        {
            Terms += " and (c.supplier_name like '%'+@suppliername+'%' or a.supplier_name like '%'+@suppliername+'%')";
            parms.Add(new SqlParameter("@suppliername", txtSupplierName.Text.Trim()));
        }
        DataTable supplierList = SupplierManager.getLinkSupplySupplierList(Terms, typeId, parms, "");
        PagedDataSource ps = new PagedDataSource();
        ps.DataSource = supplierList.DefaultView;
        ps.AllowPaging = true;
        ps.PageSize = 10;
        int curpage = int.Parse(ViewState["currentPageIndex"] == null ? "0" : ViewState["currentPageIndex"].ToString());
        ViewState["currentPageIndex"] = curpage;
        btnFP1.Visible = btnPP1.Visible = btnFP2.Visible = btnPP2.Visible = !(curpage == 0);
        btnLP1.Visible = btnNP1.Visible = btnLP2.Visible = btnNP2.Visible = !(curpage == (ps.PageCount - 1));
        litCurrentPage1.Text = litCurrentPage2.Text = (curpage + 1).ToString();
        litTotalPage1.Text = litTotalPage2.Text = ps.PageCount.ToString();
        litCount1.Text = litCount2.Text = supplierList.Rows.Count.ToString();
        ps.CurrentPageIndex = curpage;
        ViewState["pageCount"] = ps.PageCount.ToString();

        gv.DataSource = ps;
        gv.DataBind();
    }

    protected void btnFP_Click(object sender, EventArgs e)
    {
        ViewState["currentPageIndex"] = 0;
        listBind();
    }

    protected void btnPP_Click(object sender, EventArgs e)
    {
        ViewState["currentPageIndex"] = int.Parse(ViewState["currentPageIndex"].ToString()) - 1;
        listBind();
    }

    protected void btnNP_Click(object sender, EventArgs e)
    {
        ViewState["currentPageIndex"] = int.Parse(ViewState["currentPageIndex"].ToString()) + 1;
        listBind();
    }

    protected void btnLP_Click(object sender, EventArgs e)
    {
        ViewState["currentPageIndex"] = int.Parse(ViewState["pageCount"].ToString()) - 1;
        listBind();
    }

    protected void btnClean_Click(object sender, EventArgs e)
    {
        txtSupplierName.Text = "";
        List<SupplierInfo> list = SupplierManager.getModelList("", new List<SqlParameter>());

        //绑定物料类别树
        BindTree(treeView.Nodes, 0, TypeManager.GetListByParentId(0));

        if (null != list)
        {
            gv.DataSource = list;
            gv.DataBind();
        }
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        Button btnSelectSupplier = (Button)sender;
        string supplierId = btnSelectSupplier.CommandArgument.ToString().Split('-')[0];
        string supplyId = btnSelectSupplier.CommandArgument.ToString().Split('-')[1];
        if (supplierId != "")
        {
            //选择采购平台存在的供应商
            add(int.Parse(supplierId));
        }
        else
        {
            ESP.Supplier.Entity.SC_Supplier s = new ESP.Supplier.BusinessLogic.SC_SupplierManager().GetModel(int.Parse(supplyId));
            ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "window.opener.setSName2('" + s.supplier_name + "','" + s.id + "');window.close();", true);
            return;
        }
    }

    protected void add(int id)
    {
        SupplierInfo model = SupplierManager.GetModel(id);
        if (null != model)
        {
            ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "window.opener.setSName1('" + model.supplier_name + "','" + model.id + "');window.close();", true);
            return;
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
        supplierInfo.Visible = true;
        btnSave.Visible = true;
        SetFocus(btnSave);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DropDownList ddlsource = (DropDownList)supplierInfo.FindControl("ddlsource");
        Label labEmailFile = (Label)supplierInfo.FindControl("labEmailFile");
        if (ddlsource.SelectedValue == "客户指定" && labEmailFile.Text == "" && Request.Files[0].FileName == "")
        {
            ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('请上传客户指定邮件附件！');", true);
            return;
        }

        GeneralInfo general = GeneralInfoManager.GetModel(generalid);
        supplierInfo.Model = general;
        supplierInfo.setModelInfo();
        GeneralInfoManager.Update(supplierInfo.Model);
        //记录操作日志
        ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, general.id, "编辑供应商"), "申请单");
        ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "window.opener.setSName('" + supplierInfo.Model.supplier_name + "');window.close();", true);
    }
}
