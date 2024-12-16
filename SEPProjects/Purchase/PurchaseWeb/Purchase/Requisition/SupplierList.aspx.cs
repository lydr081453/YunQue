using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_SupplierList : ESP.Web.UI.PageBase
{
    private string clientId = "ctl00_ContentPlaceHolder1_supplierInfo_";
    int productType = 0;

    /// <summary>
    /// 页面装载
    /// </summary>
    /// <param name="sender">发送者</param>
    /// <param name="e">事件对象</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        #region AjaxProRegister
        AjaxPro.Utility.RegisterTypeForAjax(typeof(TypeDataProvider));
        #endregion

        if (!string.IsNullOrEmpty(Request["source"]))
        {
            btnX.Visible = false;
            btnX1.Visible = false;
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
            listBind();
        }
       
    }

    /// <summary>
    /// Binds the tree.
    /// </summary>
    /// <param name="nds">The NDS.</param>
    /// <param name="parentId">The parent id.</param>
    /// <param name="items">The items.</param>
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

    /// <summary>
    /// Lists the bind.
    /// </summary>
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
        strWhere += " and supplier_type = " + (int)State.supplier_type.agreement;
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        listBind();
    }

    /// <summary>
    /// Handles the Click event of the btnClean control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnClean_Click(object sender, EventArgs e)
    {
        txtSupplierName.Text = "";
        listBind();
    }

    /// <summary>
    /// Adds the specified id.
    /// </summary>
    /// <param name="id">The id.</param>
    protected void add(int id)
    {
        SupplierInfo model = SupplierManager.GetModel(id);
        GeneralInfo generalModel = null;
        if (null != model)
        {
            if (!string.IsNullOrEmpty(Request["source"]) && Request["source"] != "product")
            {
                Response.Write("<script>opener.document.all.ctl00_ContentPlaceHolder1_txtSupplier.value='" + model.supplier_name + "';</script>");
                Response.Write("<script>opener.document.all.ctl00_ContentPlaceHolder1_hidSupplierId.value='" + model.id + "';</script>");
                Response.Write("<script>window.close();</script>");
                return;
            }
            else if (!string.IsNullOrEmpty(Request["source"]) && Request["source"] == "product")
            {
                Response.Write("<script>opener.document.all.ctl00_ContentPlaceHolder1_TabContainer1_Tab2_txtSupplierName.value='" + model.supplier_name + "';</script>");
                Response.Write("<script>opener.document.all.ctl00_ContentPlaceHolder1_TabContainer1_Tab2_hidSupplierName.value='" + model.supplier_name + "';</script>");
                Response.Write("<script>opener.document.all.ctl00_ContentPlaceHolder1_TabContainer1_Tab2_hidSupplierId1.value='" + model.id + "';</script>");
                List<List<string>> items = ProductManager.GetTypeListBySupplierId(model.id, "");
                Response.Write("<script>window.opener.clearType();</script>");
                foreach (List<string> item in items)
                {
                    Response.Write("<script>window.opener.setType('" + item[0] + "','" + item[1] + "');</script>");
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

            if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
            {
                generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(int.Parse(Request[RequestName.GeneralID]));
            }
            if (generalModel != null && generalModel.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_OtherAdvertisement)
            {
                generalModel.supplier_name = model.supplier_name;
                generalModel.source = model.supplier_source;
                generalModel.supplier_linkman = model.contact_name;
                generalModel.supplier_fax = model.contact_fax;
                generalModel.supplier_address = model.contact_address;
                generalModel.Supplier_cellphone = model.contact_mobile;
                generalModel.supplier_email = model.contact_email;
                generalModel.supplier_phone = model.contact_tel;
                generalModel.account_bank = model.account_bank;
                generalModel.account_name = model.account_name;
                generalModel.account_number = model.account_number;
                generalModel.fa_no = model.supplier_frameNO;
                ESP.Purchase.BusinessLogic.GeneralInfoManager.Update(generalModel);
                List<ESP.Purchase.Entity.OrderInfo> orderlist = ESP.Purchase.BusinessLogic.OrderInfoManager.GetListByGeneralId(generalModel.id);
                foreach (OrderInfo order in orderlist)
                {
                    order.supplierId = model.id;
                    order.supplierName = model.supplier_name;
                    ESP.Purchase.BusinessLogic.OrderInfoManager.Update(order,int.Parse(CurrentUser.SysID),CurrentUser.Name);
                }
                Response.Write("<script>opener.location.reload();</script>");
                Response.Write(@"<script>window.close();</script>");
            }
            else
            {
                Response.Write("<script>opener.document.all." + clientId + "txtsource.style.display='none';</script>");
                Response.Write("<script>opener.document.all." + clientId + "txtsupplier_name.style.display='none';</script>");
                Response.Write("<script>opener.document.all." + clientId + "txtsupplier_address.style.display='none';</script>");
                Response.Write("<script>opener.document.all." + clientId + "txtsupplier_linkman.style.display='none';</script>");
                Response.Write("<script>opener.document.all." + clientId + "txtsupplier_con.style.display='none';</script>");
                Response.Write("<script>opener.document.all." + clientId + "txtsupplier_area.style.display='none';</script>");
                Response.Write("<script>opener.document.all." + clientId + "txtsupplier_phone.style.display='none';</script>");
                Response.Write("<script>opener.document.all." + clientId + "txtsupplier_ext.style.display='none';</script>");
                Response.Write("<script>opener.document.all." + clientId + "txtsupplier_cellphone.style.display='none';</script>");
                Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_con.style.display='none';</script>");
                Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_area.style.display='none';</script>");
                Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_phone.style.display='none';</script>");
                Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_ext.style.display='none';</script>");
                Response.Write("<script>opener.document.all." + clientId + "txtsupplier_email.style.display='none';</script>");
                Response.Write("<script>opener.document.all." + clientId + "ddlsource.style.display='none';</script>");

                Response.Write("<script>opener.document.all." + clientId + "labsource.style.display='block';</script>");
                Response.Write("<script>opener.document.all." + clientId + "labsupplier_name.style.display='block';</script>");
                Response.Write("<script>opener.document.all." + clientId + "labsupplier_address.style.display='block';</script>");
                Response.Write("<script>opener.document.all." + clientId + "labsupplier_linkman.style.display='block';</script>");
                Response.Write("<script>opener.document.all." + clientId + "labsupplier_phone.style.display='block';</script>");
                Response.Write("<script>opener.document.all." + clientId + "labsupplier_cellphone.style.display='block';</script>");
                Response.Write("<script>opener.document.all." + clientId + "labsupplier_fax.style.display='block';</script>");
                Response.Write("<script>opener.document.all." + clientId + "labsupplier_email.style.display='block';</script>");
                Response.Write("<script>opener.document.all." + clientId + "labfa_no.style.display='block';</script>");

                Response.Write("<script>if(document.all)opener.document.all." + clientId + "labsupplier_name.innerText= '" + model.supplier_name + "';else opener.document.all." + clientId + "labsupplier_name.textContent= '" + model.supplier_name + "';</script>");
                Response.Write("<script>if(document.all)opener.document.all." + clientId + "labsource.innerText= '" + model.supplier_source + "';else opener.document.all." + clientId + "labsource.textContent= '" + model.supplier_source + "';</script>");
                Response.Write("<script>if(document.all)opener.document.all." + clientId + "labsupplier_address.innerText= '" + model.contact_address + "';opener.document.all." + clientId + "labsupplier_address.textContent= '" + model.contact_address + "';</script>");
                Response.Write("<script>if(document.all)opener.document.all." + clientId + "labsupplier_linkman.innerText= '" + model.contact_name + "';else opener.document.all." + clientId + "labsupplier_linkman.textContent= '" + model.contact_name + "';</script>");
                Response.Write("<script>if(document.all)opener.document.all." + clientId + "labsupplier_phone.innerText= '" + model.contact_tel + "';else opener.document.all." + clientId + "labsupplier_phone.textContent= '" + model.contact_tel + "';</script>");
                Response.Write("<script>if(document.all)opener.document.all." + clientId + "labsupplier_cellphone.innerText= '" + model.contact_mobile + "'; else opener.document.all." + clientId + "labsupplier_cellphone.textContent= '" + model.contact_mobile + "';</script>");
                Response.Write("<script>if(document.all)opener.document.all." + clientId + "labsupplier_fax.innerText= '" + model.contact_fax + "'; else opener.document.all." + clientId + "labsupplier_fax.textContent= '" + model.contact_fax + "';</script>");
                Response.Write("<script>if(document.all)opener.document.all." + clientId + "labsupplier_email.innerText= '" + (model.contact_email == "" ? "无" : model.contact_email) + "'; else opener.document.all." + clientId + "labsupplier_email.textContent= '" + (model.contact_email == "" ? "无" : model.contact_email) + "';</script>");
                Response.Write("<script>if(document.all)opener.document.all." + clientId + "labfa_no.innerText= '" + model.supplier_frameNO + "'; else opener.document.all." + clientId + "labfa_no.textContent= '" + model.supplier_frameNO + "';</script>");


                Response.Write("<script>opener.document.all." + clientId + "txtsupplier_name.value= '" + model.supplier_name + "'</script>");
                Response.Write("<script>opener.document.all." + clientId + "txtsource.value= '" + model.supplier_source + "'</script>");
                Response.Write("<script>opener.document.all." + clientId + "txtsupplier_address.value= '" + model.contact_address + "'</script>");
                Response.Write("<script>opener.document.all." + clientId + "txtsupplier_linkman.value= '" + model.contact_name + "'</script>");

                Response.Write("<script>opener.document.all." + clientId + "labaccountName.value= '" + model.account_name + "'</script>");
                Response.Write("<script>opener.document.all." + clientId + "labaccountBank.value= '" + model.account_bank + "'</script>");
                Response.Write("<script>opener.document.all." + clientId + "labaccountNum.value= '" + model.account_number + "'</script>");


                string[] phonenum = model.contact_tel.Split('-');
                if (phonenum.Length == 4)
                {
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplier_con.value= '" + phonenum[0] + "'</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplier_area.value= '" + phonenum[1] + "'</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplier_phone.value= '" + phonenum[2] + "'</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplier_ext.value= '" + phonenum[3] + "'</script>");
                }
                else if (phonenum.Length == 3)
                {
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplier_con.value= ''</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplier_area.value= '" + phonenum[0] + "'</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplier_phone.value= '" + phonenum[1] + "'</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplier_ext.value= '" + phonenum[2] + "'</script>");
                }
                else if (phonenum.Length == 2)
                {
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplier_con.value= ''</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplier_area.value= ''</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplier_phone.value= '" + phonenum[0] + "'</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplier_ext.value= '" + phonenum[1] + "'</script>");
                }
                else if (phonenum.Length == 1)
                {
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplier_con.value= ''</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplier_area.value= ''</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplier_phone.value= '" + phonenum[0] + "'</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplier_ext.value= ''</script>");
                }
                else
                {
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplier_con.value= ''</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplier_area.value= ''</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplier_phone.value= ''</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplier_ext.value= ''</script>");
                }
                Response.Write("<script>opener.document.all." + clientId + "txtsupplier_cellphone.value= '" + model.contact_mobile + "'</script>");
                string[] faxnum = model.contact_fax.Split('-');
                if (faxnum.Length == 4)
                {
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_con.value= '" + faxnum[0] + "'</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_area.value= '" + faxnum[1] + "'</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_phone.value= '" + faxnum[2] + "'</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_ext.value= '" + faxnum[3] + "'</script>");
                }
                else if (faxnum.Length == 3)
                {
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_con.value= ''</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_area.value= '" + faxnum[0] + "'</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_phone.value= '" + faxnum[1] + "'</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_ext.value= '" + faxnum[2] + "'</script>");
                }
                else if (faxnum.Length == 2)
                {
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_con.value= ''</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_area.value= ''</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_phone.value= '" + faxnum[0] + "'</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_ext.value= '" + faxnum[1] + "'</script>");
                }
                else if (faxnum.Length == 1)
                {
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_con.value= ''</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_area.value= ''</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_phone.value= '" + faxnum[0] + "'</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_ext.value= ''</script>");
                }
                else
                {
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_con.value= ''</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_area.value= ''</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_phone.value= ''</script>");
                    Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_ext.value= ''</script>");
                }
                Response.Write("<script>opener.document.all." + clientId + "txtsupplier_email.value= '" + model.contact_email + "'</script>");
                Response.Write("<script>opener.document.all." + clientId + "txtfa_no.value= '" + model.supplier_frameNO + "'</script>");
                Response.Write(@"<script>window.close();</script>");
            }
        }
    }

    /// <summary>
    /// Handles the RowCommand event of the gv control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            string id = gv.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString();
            add(int.Parse(id));
        }
    }

    /// <summary>
    /// Handles the Click event of the btnX control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnX_Click(object sender, EventArgs e)
    {
        GeneralInfo generalModel = null;
        if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
        {
            generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(int.Parse(Request[RequestName.GeneralID]));
        }
        if (generalModel != null && generalModel.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_OtherAdvertisement)
        {
            string strWhere = "";
            List<SqlParameter> parms = new List<SqlParameter>();

            if (txtSupplierName.Text.Trim() != "")
            {
                strWhere += " and supplier_name like '%" + txtSupplierName.Text.Trim() + "%'";
            }

            List<ESP.Purchase.Entity.SupplierInfo> supplierlist = SupplierManager.getModelList(strWhere, new List<SqlParameter>());

            if (null != supplierlist)
            {
                gv.DataSource = supplierlist;
                gv.DataBind();
            }
        }
        else
        {
            Response.Write("<script>opener.document.all." + clientId + "txtsupplier_name.style.display='block';</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtsupplier_address.style.display='block';</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtsupplier_linkman.style.display='block';</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtsupplier_con.style.display='block';</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtsupplier_area.style.display='block';</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtsupplier_phone.style.display='block';</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtsupplier_ext.style.display='block';</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtsupplier_cellphone.style.display='block';</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_con.style.display='block';</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_area.style.display='block';</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_phone.style.display='block';</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_ext.style.display='block';</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtsupplier_email.style.display='block';</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtsource.style.display='block';</script>");
            Response.Write("<script>opener.document.all." + clientId + "ddlsource.style.display='block';</script>");

            Response.Write("<script>if(document.all)opener.document.all." + clientId + "labsupplier_name.innerText= ''; else opener.document.all." + clientId + "labsupplier_name.textContent= '';</script>");
            Response.Write("<script>if(document.all)opener.document.all." + clientId + "labsource.innerText= ''; else opener.document.all." + clientId + "labsource.textContent= '';</script>");
            Response.Write("<script>if(document.all)opener.document.all." + clientId + "labsupplier_address.innerText= ''; else opener.document.all." + clientId + "labsupplier_address.textContent= '';</script>");
            Response.Write("<script>if(document.all)opener.document.all." + clientId + "labsupplier_linkman.innerText= ''; else opener.document.all." + clientId + "labsupplier_linkman.textContent= '';</script>");
            Response.Write("<script>if(document.all)opener.document.all." + clientId + "labsupplier_phone.innerText= ''; else opener.document.all." + clientId + "labsupplier_phone.textContent= '';</script>");
            Response.Write("<script>if(document.all)opener.document.all." + clientId + "labsupplier_cellphone.innerText= ''; else opener.document.all." + clientId + "labsupplier_cellphone.textContent= '';</script>");
            Response.Write("<script>if(document.all)opener.document.all." + clientId + "labsupplier_fax.innerText= '';else opener.document.all." + clientId + "labsupplier_fax.textContent= '';</script>");
            Response.Write("<script>if(document.all)opener.document.all." + clientId + "labsupplier_email.innerText= ''; else opener.document.all." + clientId + "labsupplier_email.textContent= '';</script>");
            Response.Write("<script>if(document.all)opener.document.all." + clientId + "labfa_no.innerText= ''; else opener.document.all." + clientId + "labfa_no.textContent= '';</script>");

            Response.Write("<script>opener.document.all." + clientId + "txtsupplier_name.value= ''</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtsource.value= ''</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtsupplier_address.value= ''</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtsupplier_linkman.value= ''</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtsupplier_con.value= ''</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtsupplier_area.value= ''</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtsupplier_phone.value= ''</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtsupplier_ext.value= ''</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtsupplier_cellphone.value= ''</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_con.value= ''</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_area.value= ''</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_phone.value= ''</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtsupplierfax_ext.value= ''</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtsupplier_email.value= ''</script>");
            Response.Write("<script>opener.document.all." + clientId + "txtfa_no.value= ''</script>");

            Response.Write(@"<script>window.close();</script>");
        }
    }
}