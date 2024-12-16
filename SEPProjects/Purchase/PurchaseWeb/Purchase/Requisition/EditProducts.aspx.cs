using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using System.IO;
using System.Collections;

public partial class Purchase_EditProducts : ESP.Purchase.WebPage.EditPageForPR
{
    int generalid = 0;
    static int SupplierId = 0;
    string query = string.Empty;
    static bool isFPrduct = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
        {
            generalid = int.Parse(Request[RequestName.GeneralID]);
        }
        btnPriceAtt.Attributes["onclick"] = "showPriceAtt('" + generalid.ToString() + "');return false;";
        btnPriceAtt1.Attributes["onclick"] = "showPriceAtt1('" + generalid.ToString() + "');return false;";
        query = Request.Url.Query;

        if (!IsPostBack)
        {
            projectId = 0;
            level1Types.Clear();
            level2Types.Clear();

            Tab2.Visible = false;
            SupplierId = 0;
            isFPrduct = false;

            InitProductTypeItems();

            //设置非目录信息
            PanelInfoBind();

            GeneralInfo general = GeneralInfoManager.GetModel(generalid);
            //设置货币类型
            labMoneyType.Text = labMoneyType1.Text = general.moneytype;
            //如果为3000以上生成的单子，预计收货时间默认为当前日期
            if (general.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA || general.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA)
            {
                intend_receipt_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
                intend_receipt_date1.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
        manager1.RegisterPostBackControl(btnSave1);
        manager1.RegisterPostBackControl(btnSave2);
    }
    static List<string> level1Types = new List<string>();
    static List<string> level2Types = new List<string>();
    static int projectId = 0;
    #region Panel1
    /// <summary>
    /// 设置显示类型
    /// </summary>
    private void InitProductTypeItems()
    {
        List<OrderInfo> models = OrderInfoManager.GetListByGeneralId(generalid);

        //设置项目号下所包含的一级物料和二级物料
        GeneralInfo generalInfo = GeneralInfoManager.GetModel(generalid);
        projectId = generalInfo.Project_id;
        if (generalInfo.Project_id > 0)
        {
            //改申请单可以选择的一级物料和二级物料
            DataTable typeDt = new DataTable();
            if (generalInfo.thirdParty_materielID != "")
                typeDt = TypeManager.GetList(" and typeid in (" + generalInfo.thirdParty_materielID + ")").Tables[0];
            foreach (DataRow dr in typeDt.Rows)
            {
                if (!level1Types.Contains(dr["parentId"].ToString()))
                    level1Types.Add(dr["parentId"].ToString());
                if (!level2Types.Contains(dr["typeid"].ToString()))
                    level2Types.Add(dr["typeid"].ToString());
            }
        }



        List<TypeInfo> TopTypeList = getType1List(models); //一级物料类别
        if (models != null && models.Count > 0 && models[0].supplierId > 0)
        {
            //如果pr单已存在采购物品，并且为协议供应商，则获得该协议供应商存在的物料类别
            List<int> typeIdList = ProductManager.getSupplierProductTypeIdList(SupplierId);
            if (typeIdList.Count > 0)
            {
                if (typeIdList.Count < 2)
                {
                    //如果协议供应商只有存在一个物料类别，则直接显示非目录物品添加页面
                    setItemList(typeIdList[0], false);
                    setSetp(2);
                    Tab2.Visible = true;
                }
            }
            else
            {
                setSetp(2);
                Tab2.Visible = true;
                TabContainer1.ActiveTabIndex = 1;
                PanelInfoBind();
            }
        }
        if (TopTypeList == null)
        {
            Tab2.Visible = true;
            TabContainer1.ActiveTabIndex = 1;
            isFPrduct = true;
        }
        rep1.DataSource = TopTypeList;
        rep1.DataBind();
    }

    /// <summary>
    /// 获得一级物料类别
    /// </summary>
    /// <param name="orderInfoList"></param>
    /// <returns></returns>
    private List<TypeInfo> getType1List(List<OrderInfo> orderInfoList)
    {
        List<TypeInfo> TopTypeList = null;
        if (orderInfoList == null || orderInfoList.Count == 0)
        {
            //没有采购物品时，获得所有可用的一级物料类别
            TopTypeList = TypeManager.GetListByParentId(0);
        }
        else if (orderInfoList != null && orderInfoList.Count > 0 && orderInfoList[0].supplierId > 0)
        {
            //如果为协议供应商，则获得包含物料类别所属的一级物料类别
            SupplierId = orderInfoList[0].supplierId;
            TopTypeList = TypeManager.getLevel1ListBySupplierId(SupplierId);
        }
        return TopTypeList;
    }

    /// <summary>
    /// 获得二级物料类别
    /// </summary>
    /// <param name="type1Id">一级物料类别ID</param>
    /// <returns></returns>
    private List<TypeInfo> getType2List(int type1Id)
    {
        List<TypeInfo> type2List = null;
        if (SupplierId == 0)
        {
            //获得一级物料类别下的二级类别
            type2List = TypeManager.GetListByParentId(type1Id);
        }
        else
        {
            //获得协议供应商的二级类别
            type2List = TypeManager.getLevel2ListBySupplierIdAndLevel1ID(SupplierId, type1Id);
        }
        if (hidViewType2.Value != "")
        {
            for (int i = (type2List.Count - 1); i >= 0; i--)
            {
                //移除不需显示的二级物料类别
                if (type2List[i].typeid.ToString() != hidViewType2.Value)
                    type2List.RemoveAt(i);
            }
        }
        return type2List;
    }

    /// <summary>
    /// 一级物料类别列表 ItemDataBound事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rep1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            TypeInfo model = (TypeInfo)e.Item.DataItem;
            Label lab = (Label)e.Item.FindControl("lab");
            lab.Text = model.typename;
            DataList rep2 = (DataList)e.Item.FindControl("rep2");
            rep2.DataSource = getType2List(model.typeid);
            rep2.DataBind();
        }
    }

    /// <summary>
    /// 二级物料类别 ItemDataBound事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rep2_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            TypeInfo model = (TypeInfo)e.Item.DataItem;
            Label lab = (Label)e.Item.FindControl("lab");
            lab.Text = model.typename;
            DataList dg3 = (DataList)e.Item.FindControl("dg3");
            if (SupplierId == 0)
            {
                //根据二级物料的ID，获得三级物料
                dg3.DataSource = TypeManager.GetListByParentId(model.typeid);
            }
            else
            {
                //获得协议供应商的三级物料
                dg3.DataSource = TypeManager.getLevel3ListBySupplierIdAndLevel2ID(SupplierId, model.typeid);
            }
            dg3.DataBind();
        }
    }

    /// <summary>
    /// 物料类别列表 ItemDataBound事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dg3_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            TypeInfo model = (TypeInfo)e.Item.DataItem;
            LinkButton lnk = (LinkButton)e.Item.FindControl("lnk");
            if (projectId > 0 && !level2Types.Contains(model.parentId.ToString()))
                lnk.Enabled = false;//选择的项目号，物料类别如不存在项目号中，则屏蔽掉，不能选择。
            if (model.status == State.typestatus_mediaUp3000)
            {
                GeneralInfo general = GeneralInfoManager.GetModel(generalid);
                if (general.PRType != (int)PRTYpe.PR_MediaFA && general.PRType != (int)PRTYpe.PR_PriFA)
                    lnk.Visible = false; //只有由3000以上媒介生成的pr单，才显示状态为typestatus_mediaUp3000的物料类别
                else
                {
                    lnk.Enabled = true;
                    lnk.Visible = true;
                }
            }
            lnk.Text = model.typename;
        }
    }

    /// <summary>
    /// 物料类别列表 ItemCommand事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dg3_ItemCommand(object sender, DataListCommandEventArgs e)
    {
        if (e.CommandName == "SET")
        {
            string id = e.CommandArgument.ToString();
            hidCurrentTypeId.Value = id;
            TypeInfo model = TypeManager.GetModel(int.Parse(id));
            if (null != model)
            {
                if (model.operationflow == State.typeoperationflow_Media)
                {
                    Response.Redirect("WrittingFeeApplicant.aspx" + query);
                }
                else if (model.operationflow == State.typeoperationflow_OtheMedia)
                {
                    Response.Redirect("SupplierCollaboration/OtherMediaOrder.aspx" + query);
                }
                //else if (model.operationflow == State.typeoperationflow_Advertisement)
                //{
                //    Response.Redirect("Advertisement/AdvertisementOrder.aspx" + query);
                //}
                else
                {
                    setItemList(model.typeid, false);

                    setItemListForSupply(model.typeid, false);
                    setSetp(2);
                    //Tab2.Visible = true;
                    hidViewType1.Value = TypeManager.GetModel(model.parentId).parentId.ToString();
                    hidViewType2.Value = model.parentId.ToString();
                }
            }
        }
    }

    /// <summary>
    /// Handles the Click event of the btnSearch1 control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSearch1_Click(object sender, EventArgs e)
    {
        if (hidType2.Value.Trim() == "")
            return;
        setItemList(int.Parse(hidType2.Value == "" ? "0" : hidType2.Value), true);
    }

    private void setItemListForSupply(int typeid, bool isTypeClick)
    { }

    /// <summary>
    /// 设置目录物品列表
    /// </summary>
    /// <param name="typeid"></param>
    /// <param name="isTypeClick">是否为dgType调用</param>
    private void setItemList(int typeid, bool isTypeClick)
    {
        hidType2.Value = typeid.ToString();
        List<SqlParameter> parms = new List<SqlParameter>();
        string Terms = " and a.productType=@productType and IsShow=1 and c.status<>" + State.typestatus_block;
        parms.Add(new SqlParameter("@productType", typeid));
        if (txtSupplierNameS.Text.Trim() != "")
        {
            Terms += " and supplierName like '%'+@suppliername+'%'";
            parms.Add(new SqlParameter("@suppliername", txtSupplierNameS.Text.Trim()));
        }
        if (txtProductNameS.Text.Trim() != "")
        {
            Terms += " and a.productName like '%'+@productname+'%'";
            parms.Add(new SqlParameter("@productname", txtProductNameS.Text.Trim()));
        }
        //Terms += " and b.supplier_type = " + (int)State.supplier_type.agreement;
        if (SupplierId != 0)
        {
            Terms += " and b.id=" + SupplierId;
        }

        List<ProductInfo> list = ProductManager.getModelList(Terms, parms);
        gvItem.DataSource = list;
        gvItem.DataBind();

        //SetDorpDownList(typeid, true);

        //string Terms1 = " and a.productType=@productType";
        //List<SqlParameter> parms1 = new List<SqlParameter>();
        //parms1.Add(new SqlParameter("@productType", typeid));
        //List<ProductInfo> list1 = ProductManager.getModelList(Terms1, parms1);
        //setSupplierList(list1);
        setSupplierList(typeid);

        //if (isTypeClick)
        //{
        //    if (list.Count == 0 && list1.Count == 0)
        //    {
        //        TabContainer1.ActiveTabIndex = 1;
        //    }
        //}
        //else
        //{
        //    if (list.Count == 0 && list1.Count == 0)
        //    {
        //        TabContainer1.ActiveTabIndex = 1;
        //        hidABC.Value = "abc";
        //    }
        //}

        List<TypeInfo> typeList = new List<TypeInfo>();
        typeList.Add(TypeManager.GetModel(typeid));
        setThreeTypeList(typeList);

    }
    /*
    /// <summary>
    /// 设置下拉框的值
    /// </summary>
    /// <param name="typeid">第三级的物料类别ID</param>
    /// <param name="isBindSelectedValue">是否设置selectedValue</param>
    private void SetDorpDownList(int typeid, bool isBindSelectedValue)
    {
        TypeInfo type = TypeManager.GetModel(typeid);
        List<TypeInfo> listtypel33 = TypeManager.GetListByTypeId(typeid);
        if (listtypel33.Count > 0)
        {
            ddlType3.DataSource = listtypel33;
            ddlType3.DataTextField = "typename";
            ddlType3.DataValueField = "typeid";
            ddlType3.DataBind();
            ddlType3.SelectedValue = typeid.ToString();
        }

        List<TypeInfo> listtypel22 = TypeManager.GetListByTypeId(type.parentId);
        if (listtypel22.Count > 0)
        {
            ddlType2.DataSource = listtypel22;
            ddlType2.DataTextField = "typename";
            ddlType2.DataValueField = "typeid";
            ddlType2.DataBind();
            ddlType2.SelectedValue = type.parentId.ToString();
        }

        TypeInfo type1 = TypeManager.GetModel(type.parentId);
        if (isBindSelectedValue)
        {
            List<TypeInfo> list = TypeManager.GetListByParentId(0);
            if (list.Count > 0)
            {
                ddlType1.DataSource = list;
                ddlType1.DataTextField = "typename";
                ddlType1.DataValueField = "typeid";
                ddlType1.DataBind();
            }
            ddlType1.Items.Insert(0, new ListItem("请选择", "-1"));
        }
        if (type1 != null)
        {
            ddlType1.SelectedValue = type1.parentId.ToString();

            if (SupplierId == 0)
            {
                ddlType1.Enabled = false;
                ddlType2.Enabled = false;
                ddlType3.Enabled = false;
            }
            else
            {
                List<TypeInfo> listtypel1 = TypeManager.getLevel1ListBySupplierId(SupplierId);
                if (listtypel1.Count > 0)
                {
                    ddlType1.DataSource = listtypel1;
                    ddlType1.DataValueField = "typeid";
                    ddlType1.DataTextField = "typename";
                    ddlType1.DataBind();
                }
                ddlType1.Items.Insert(0, new ListItem("请选择", "-1"));
                ddlType2.Items.Clear();
                ddlType2.Items.Insert(0, new ListItem("请选择", "-1"));
                ddlType3.Items.Clear();
                ddlType3.Items.Insert(0, new ListItem("请选择", "-1"));

                if (isBindSelectedValue)
                {
                    List<TypeInfo> listtypel3 = TypeManager.getLevel3ListBySupplierIdAndLevel2ID(SupplierId, type.parentId);
                    if (listtypel3.Count > 0)
                    {
                        ddlType3.DataSource = listtypel3;
                        ddlType3.DataTextField = "typename";
                        ddlType3.DataValueField = "typeid";
                        ddlType3.DataBind();
                        ddlType3.SelectedValue = typeid.ToString();
                    }
                    List<TypeInfo> listtypel2 = TypeManager.getLevel2ListBySupplierIdAndLevel1ID(SupplierId, int.Parse(ddlType1.SelectedValue));
                    if (listtypel2.Count > 0)
                    {
                        ddlType2.DataSource = listtypel2;
                        ddlType2.DataTextField = "typename";
                        ddlType2.DataValueField = "typeid";
                        ddlType2.DataBind();
                        ddlType2.SelectedValue = type.parentId.ToString();
                    }
                }
            }
        }
        removeDropDownListItems(ddlType1, 1);
        removeDropDownListItems(ddlType2, 2);
    }

    /// <summary>
    /// 删除项目不包含的物料类别
    /// </summary>
    /// <param name="ddl"></param>
    /// <param name="level"></param>
    private void removeDropDownListItems(DropDownList ddl, int level)
    {
        if (projectId > 0)
        {
            for (int i = 0; i < ddl.Items.Count; i++)
            {
                ListItem li = ddl.Items[i];
                if (level == 1)
                {
                    if ((!level1Types.Contains(li.Value) && li.Value != "-1") || (li.Value == "-1" && ddl.Items.Count == 2))
                    {
                        ddl.Items.Remove(li);
                        if (ddl.Items.Count == 1 && ddl.Items[0].Value != "-1")
                        {
                            SetTypeLevel2();
                        }
                        removeDropDownListItems(ddl, level);
                    }
                }
                else if (level == 2)
                {
                    if ((!level2Types.Contains(li.Value) && li.Value != "-1") || (li.Value == "-1" && ddl.Items.Count == 2))
                    {
                        ddl.Items.Remove(li);
                        if (ddl.Items.Count == 1 && ddl.Items[0].Value != "-1")
                        {
                            setTypeLevel3();
                        }
                        else if (ddl.Items.Count > 1)
                        {
                            removeDropDownListItems(ddl, level);
                        }
                        else if (ddl.Items.Count == 0)
                        {
                            removeDropDownListItems(ddlType3, 3);
                        }
                    }
                }
                else if (level == 3)
                {
                    if (li.Value == "-1" || ddlType2.Items.Count == 0)
                    {
                        ddl.Items.Remove(li);
                    }
                    if (ddl.Items.Count == 0)
                        ddlType3.Items.Add(new ListItem("", "-1"));
                }
            }
        }
    }
    */

    /// <summary>
    /// 检索按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtKey.Text.Trim() == "")
        {
            ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('请输入检索关键字！');", true);
            return;
        }
        List<ProductInfo> list = ProductManager.getListByLike(txtKey.Text.Trim(), true);
        //List<ProductInfo> list1 = ProductManager.getListByLike(txtKey.Text.Trim(), false);
        List<TypeInfo> typeList = TypeManager.GetTypeListByLike(txtKey.Text.Trim(), true);
        if (list.Count == 0 && typeList.Count == 0)
        {
            ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('检索内容不存在，请输入其他检索关键字！');", true);
            return;
        }
        gvItem.DataSource = list;
        gvItem.DataBind();
        hidType2.Value = "";
        setSetp(2);
        //setSupplierList(list1);
        setThreeTypeList(typeList);

        if (list.Count == 0 && typeList.Count > 0)
        {
            Table gvTable = ((Table)gvItem.Controls[0]);
            Label tmpLabel = (Label)(gvTable.Rows[0].FindControl("labMsg"));
            tmpLabel.Text = "暂无目录物品，请点击物料类别，添加非目录物品。";
        }
    }

    private void setThreeTypeList(List<TypeInfo> list)
    {
        dgType.DataSource = list;
        dgType.DataBind();
    }

    private void setSupplierList(int typeid)
    {
        //string supplierIds = "";
        GeneralInfo general = GeneralInfoManager.GetModel(generalid);
        //DataTable typeDt = TypeManager.GetList(" and parentid in (" + (general.thirdParty_materielID == "" ? "-1" : general.thirdParty_materielID) + ")").Tables[0];//获得项目号下所有第三级物料类别
        //foreach (ProductInfo model in list)
        //{
        //    if (typeDt.Rows.Count > 0 && typeDt.Select(" typeid=" + model.productType).Length == 0)//如果是选择的项目号，绑定供应商时，排除不包含项目号下物料类别的供应商
        //        break;
        //    supplierIds += model.supplierId + ",";
        //}
        //if (supplierIds != "")
        //{
            //supplierIds = supplierIds.Remove(supplierIds.Length - 1);
        string Terms = "";// " and c.id in (" + supplierIds + " ) ";//and Supplier_type=" + ((int)State.supplier_type.agreement).ToString();
            List<SqlParameter> parms = new List<SqlParameter>();
            if (txtSupplierNameS.Text.Trim() != "")
            {
                Terms += " and c.supplier_name like '%'+@suppliername+'%'";
                parms.Add(new SqlParameter("@suppliername", txtSupplierNameS.Text.Trim()));
            }
            if (SupplierId != 0)
            {
                Terms += " and c.id=" + SupplierId;
            }
            DataTable supplierList = null;// SupplierManager.getLinkSupplySupplierList(Terms, typeid, parms);
            gvSupplier.DataSource = supplierList;
            gvSupplier.DataBind();
            if (supplierList.Rows.Count > 0)
                Tab2.Visible = false;
            //else
            //    Tab2.Visible = true;
            //pal4.Visible = true;
        //}
        //else
        //{
        //    gvSupplier.DataSource = new DataTable();
        //    gvSupplier.DataBind();
        //    //Tab2.Visible = true;
        //}
    }

    /// <summary>
    /// 目录物品列表 RowDataBound事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ProductInfo model = (ProductInfo)e.Row.DataItem;
            Button btnS = (Button)e.Row.FindControl("btnS");
            if ((SupplierId != 0 && model.supplierId != 0 && model.supplierId != SupplierId) || isFPrduct)
            {
                btnS.Enabled = false;
            }
            TypeInfo type = TypeManager.GetModel(model.productType);
            if (projectId > 0 && !level2Types.Contains(type.parentId.ToString()))
                btnS.Enabled = false;
            if (type.status == (int)State.typestatus_mediaUp3000)
            {
                btnS.Enabled = true;
            }
        }
    }

    /// <summary>
    /// 目录物品列表 RowCommand事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            string id = e.CommandArgument.ToString();
            ESP.Purchase.Entity.TypeInfo typeModel = ESP.Purchase.BusinessLogic.TypeManager.GetModel(int.Parse(hidType2.Value));
            if (typeModel.operationflow == (int)State.typeoperationflow_Advertisement)
            {
                ProductInfo model = ProductManager.GetModel(int.Parse(id));
                Response.Redirect("Advertisement/AdvertisementOrder.aspx?GeneralID=" + Request[RequestName.GeneralID] + "&SupplierId=" + model.supplierId + "&pageUrl=AddRequisitionStep6.aspx");
            }
            else
            {
                setProductItem(int.Parse(id));
                setSetp(3);
            }
        }
    }

    /// <summary>
    /// 检索物料类别 ItemCommand事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dgType_ItemCommand(object sender, DataListCommandEventArgs e)
    {
        if (e.CommandName == "SET")
        {
            string id = e.CommandArgument.ToString();
            hidCurrentTypeId.Value = id;
            TypeInfo model = TypeManager.GetModel(int.Parse(id));
            setItemList(model.typeid, true);
            setSetp(2);
            //Tab2.Visible = true;
        }
    }

    protected void dgType_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton lnk = (LinkButton)e.Item.FindControl("lnk");
            TypeInfo type = TypeManager.GetModel(int.Parse(lnk.CommandArgument.ToString()));
            if (projectId > 0 && !level2Types.Contains(type.parentId.ToString()))
                lnk.Enabled = false;
        }
    }

    /// <summary>
    /// 供应商列表 RowDataBound事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSupplier_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dv = (DataRowView)e.Row.DataItem;
            if ((SupplierId != 0 && int.Parse(dv["id"] == DBNull.Value ? "0" : dv["id"].ToString()) != SupplierId) || isFPrduct)
            {
                Button btnS = (Button)e.Row.FindControl("btnS");
                btnS.Enabled = false;
            }
        }
    }

    /// <summary>
    /// 供应商列表 RowCommand事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSupplier_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            string supplierId = e.CommandArgument.ToString().Split('-')[0];
            string supplyId = e.CommandArgument.ToString().Split('-')[1]; 
            ESP.Purchase.Entity.TypeInfo typeModel = ESP.Purchase.BusinessLogic.TypeManager.GetModel(int.Parse(hidCurrentTypeId.Value));
            if (typeModel.operationflow == (int)State.typeoperationflow_Advertisement)
            {

                Response.Redirect("Advertisement/AdvertisementOrder.aspx?GeneralID=" + Request[RequestName.GeneralID] + "&SupplierId=" + supplierId + "&pageUrl=AddRequisitionStep6.aspx");
            }
            else
            {
                if (supplierId != "")
                    SelectedSupplier(int.Parse(supplierId));
                else
                {
                    //供应链平台存在，但未和采购系统关联的供应商，弹出新建供应商页
                    SetTypeDropDownList();
                    setSupplyInfo();
                    Tab2.Visible = true;
                    TabContainer1.ActiveTabIndex = 1;
                    hidABC.Value = supplyId;
                    txtSupplierName.Text = hidSupplierName.Value = new ESP.Supplier.BusinessLogic.SC_SupplierManager().GetModel(int.Parse(supplyId)).supplier_name;
                    palSupplier.Visible = true;
                }
            }
        }
    }

    private void SelectedSupplier(int supplierId)
    {
        //List<ProductInfo> list = ProductManager.getModelList(" and supplierid=" + id + " and isshow=1 and  c.status<>" + State.typestatus_block, new List<SqlParameter>());// getListBySupplierId(int.Parse(id), "");
        //gvItem.DataSource = list;
        //gvItem.DataBind();
        //if (list.Count == 0)
        //{
        TabContainer1.ActiveTabIndex = 1;
        //}
        Tab2.Visible = true;

        /*
        //设置物料类别下拉框
        ddlType1.DataSource = TypeManager.getLevel1ListBySupplierId(supplierId);
        ddlType1.DataValueField = "typeid";
        ddlType1.DataTextField = "typename";
        ddlType1.DataBind();

        ddlType1.Items.Insert(0, new ListItem("请选择", "-1"));
        ddlType2.Items.Clear();
        ddlType2.Items.Insert(0, new ListItem("请选择", "-1"));
        ddlType3.Items.Clear();
        ddlType3.Items.Insert(0, new ListItem("请选择", "-1"));


        //如果只有一个，三级都给默认绑定
        if (ddlType1.Items.Count == 2)
        {
            ddlType1.SelectedIndex = 1;

            ddlType2.DataSource = TypeManager.getLevel2ListBySupplierIdAndLevel1ID(supplierId, int.Parse(ddlType1.SelectedValue));
            ddlType2.DataTextField = "typename";
            ddlType2.DataValueField = "typeid";
            ddlType2.DataBind();
            ddlType2.Items.Insert(0, new ListItem("请选择", "-1"));

            if (ddlType2.Items.Count == 2)
            {
                ddlType2.SelectedIndex = 1;
                ddlType3.DataSource = TypeManager.getLevel3ListBySupplierIdAndLevel2ID(supplierId, int.Parse(ddlType2.SelectedValue));
                ddlType3.DataTextField = "typename";
                ddlType3.DataValueField = "typeid";
                ddlType3.DataBind();

            }
        }
        ddlType1.Enabled = true;
        ddlType2.Enabled = true;
        ddlType3.Enabled = true;
        */

        SupplierInfo model = SupplierManager.GetModel(supplierId);
        txtSupplierName.Text = model.supplier_name;
        hidSupplierName.Value = model.supplier_name;
        hidSupplierId.Value = model.id.ToString();
        hidSupplierId1.Value = model.id.ToString();
        btnSelect.Visible = false;
        hidABC.Value = "";
        palSupplier.Visible = false;
        SetTypeDropDownList();

        //removeDropDownListItems(ddlType1, 1);
        //removeDropDownListItems(ddlType2, 2);
    }

    protected void rbl_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void radOperationType_SelectIndexChanged(object sender, EventArgs e)
    {
        palGR.Visible = radOperationType.SelectedValue == State.OperationTypePri.ToString();
    }

    private void setSupplyInfo()
    {
        radioBind();
        radOperationType.Items.Add(new ListItem(State.OperationTypeShow[State.OperationTypePub], State.OperationTypePub.ToString()));
        radOperationType.Items.Add(new ListItem(State.OperationTypeShow[State.OperationTypePri], State.OperationTypePri.ToString()));
        radOperationType.SelectedIndex = 0;
    }

    /// <summary>
    /// 将数据源绑定到被调用的服务器控件及其所有子控件。
    /// </summary>
    public void radioBind()
    {
        rblrequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toO], State.requisitionflow_toO.ToString()));
        rblrequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toR], State.requisitionflow_toR.ToString()));
        rblrequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toC], State.requisitionflow_toC.ToString()));
        rblrequisitionflow.SelectedValue = State.requisitionflow_toR.ToString();
    }

    public int SaveSupplySupplier()
    {
        if (hidABC.Value != "")
        {
            int supplyId = int.Parse(hidABC.Value);
            GeneralInfo general = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(generalid);
            ESP.Purchase.Entity.SupplierInfo newSupplier = new ESP.Purchase.Entity.SupplierInfo();
            ESP.Supplier.Entity.SC_Supplier supply = new ESP.Supplier.BusinessLogic.SC_SupplierManager().GetModel(supplyId);

            general.supplier_name = newSupplier.supplier_name = supply.supplier_name;
            newSupplier.contact_address = supply.contact_address;
            if (palGR.Visible)
                general.supplier_address = txtCardNum.Text.Trim();
            else
                general.supplier_address = newSupplier.contact_address;
            List<ESP.Supplier.Entity.SC_LinkMan> linkers = new ESP.Supplier.BusinessLogic.SC_LinkManManager().GetListBySupplierId(supply.id);
            string linker = "";
            if (linkers != null && linkers.Count > 0)
            {
                linker = linkers[0].Name;
            }   
            general.supplier_linkman = newSupplier.contact_name = linker;
            general.supplier_phone = newSupplier.contact_tel = getTel(supply.contact_Tel);
            general.Supplier_cellphone = newSupplier.contact_mobile = supply.contact_Mobile;
            general.supplier_fax = newSupplier.contact_fax = getTel(supply.contact_fax);
            general.supplier_email = newSupplier.contact_email = supply.contact_Email;
            general.account_bank = newSupplier.account_bank = txtaccountBank.Text.Trim();
            general.account_name = newSupplier.account_name = txtaccountName.Text.Trim();
            general.account_number = newSupplier.account_number = txtaccountNum.Text.Trim();
            newSupplier.supplier_source = "采购部推荐";
            if (null != radOperationType.SelectedValue && "" != radOperationType.SelectedValue)
            {
                general.OperationType = int.Parse(radOperationType.SelectedValue);
            }
            else
            {
                general.OperationType = 0;
            }
            if (palGR.Visible)
                general.HaveInvoice = chkHaveInvoice.Checked;
            else
                general.HaveInvoice = false;
            general.Requisitionflow = !string.IsNullOrEmpty(rblrequisitionflow.SelectedValue) ? int.Parse(rblrequisitionflow.SelectedValue) : 0;
            hidABC.Value = "";
            //创建新的供应商并和供应链关联
            return ESP.Purchase.BusinessLogic.SupplierManager.insertSupplierAndLinkSupply(general,newSupplier,supplyId);
        }
        return 0;
    }

    private string getTel(string tel)
    {
        //if (tel.Length < 6)
        //    return "";
        //tel = tel.Replace("－", "-").Replace("+", "").Replace("*", "").Replace("(", "").Replace(")", "").Replace("（", "").Replace("）", "");
        //string regTel = @"(^(\d{2,4}[-_－—]?)?\d{3,8}([-_－—]?\d{3,8})?([-_－—]?\d{1,7})?$)|(^0?1[35]\d{9}$)";
        tel = tel.Replace("_", "-").Replace("－", "-").Replace("—", "-");
        if (System.Text.RegularExpressions.Regex.IsMatch(tel, @"^(\d{7,8})|\d{11}"))
            return "--" + tel + "-";
        else if (System.Text.RegularExpressions.Regex.IsMatch(tel, @"^((\d{7,8})-\d{3,4})$"))
            return "--" + tel;
        else if (System.Text.RegularExpressions.Regex.IsMatch(tel, @"^((\d{2,3}-\d{7,8})-\d{3,4})$"))
            return "-" + tel;
        else if (System.Text.RegularExpressions.Regex.IsMatch(tel, @"^((\d{2,3}-\d{2,3}-\d{7,8})-\d{3,4})$"))
            return tel;
        else if (System.Text.RegularExpressions.Regex.IsMatch(tel, @"^((\d{2,3}-\d{2,3}-\d{7,8}))$"))
            return tel + "-";
        else if (System.Text.RegularExpressions.Regex.IsMatch(tel, @"^((\d{2,3}-\d{7,8}))$"))
            return "-" + tel + "-";
        return "---";
    }

    protected void btnSup_Click(object sender, EventArgs e)
    {
        Button btnSup = (Button)sender;
        string id = btnSup.CommandArgument.ToString();
        SelectedSupplier(int.Parse(id));
    }

    /// <summary>
    /// 设置目录物品信息
    /// </summary>
    /// <param name="id"></param>
    private void setProductItem(int id)
    {
        ProductInfo model = ProductManager.GetModel(id);
        TypeInfo typeModel = TypeManager.GetModel(model.productType);
        TypeInfo typeModel2 = TypeManager.GetModel(typeModel.parentId);
        TypeInfo typeModel1 = TypeManager.GetModel(typeModel2.parentId);
        labProductType.Text = typeModel1.typename + " - " + typeModel2.typename + " - " + typeModel.typename;

        hidProductTypeId.Value = model.productType.ToString();
        labName.Text = model.productName;
        labPrice.Text = model.ProductPrice.ToString("#,##0.00");
        labUnit.Text = model.productUnit;
        labSupplierName.Text = model.supplierName;
        hidSupplierId.Value = model.supplierId.ToString();

        GeneralInfo general = GeneralInfoManager.GetModel(generalid);
    }

    /// <summary>
    /// 添加非目录物品
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnF_Click(object sender, EventArgs e)
    {
        TabContainer1.ActiveTab = Tab2;
        if (hidType2.Value != "")
        {
            TypeInfo type = TypeManager.GetModel(int.Parse(hidType2.Value));
            ddlType1.SelectedValue = type.parentId.ToString();

            ddlType2.DataSource = TypeManager.GetListByParentId(int.Parse(ddlType1.SelectedValue));
            ddlType2.DataTextField = "typename";
            ddlType2.DataValueField = "typeid";
            ddlType2.DataBind();
            ddlType2.Items.Insert(0, new ListItem("请选择", "-1"));
            ddlType2.SelectedValue = hidType2.Value;

            ddlType1.Enabled = false;
            ddlType2.Enabled = false;
        }
    }


    /// <summary>
    /// 目录物品保存按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave1_Click(object sender, EventArgs e)
    {
        if (SaveX())
            Response.Redirect(Request["pageUrl"] + "?" + RequestName.GeneralID + "=" + Request[RequestName.GeneralID]); ;
    }

    private bool SaveX()
    {
        OrderInfo model = new OrderInfo();
        model.general_id = generalid;
        model.Item_No = labName.Text;
        model.desctiprtion = desctiprtion.Text.Trim();
        model.intend_receipt_date = intend_receipt_date.Text.Trim();
        if (Eintend_receipt_date.Text.Trim() != "")
            model.intend_receipt_date += "#" + Eintend_receipt_date.Text.Trim();
        model.price = model.oldPrice = decimal.Parse(labPrice.Text);
        model.uom = labUnit.Text;
        model.quantity = model.oldQuantity = decimal.Parse(quantity.Text);

        model.total = model.price * model.quantity;
        model.producttype = int.Parse(hidProductTypeId.Value);
        model.supplierId = int.Parse(hidSupplierId.Value);
        model.supplierName = labSupplierName.Text;
        model.productAttribute = (int)State.productAttribute.ml;
        
            for (int i = 0; i < Request.Files.Count; i++)
            {
                if (Request.Files.Keys[i] == "filBJ2" && Request.Files[i].FileName != "")
                {
                    System.Web.HttpPostedFile postFile = Request.Files[i];
                    string fileName = "wuliao_" + generalid + "_" + DateTime.Now.Ticks.ToString();
                    model.upfile += FileHelper.upFile(fileName, ESP.Purchase.Common.ServiceURL.UpFilePath, postFile) + "#";
                }
                System.Threading.Thread.Sleep(100);
            }

            //添加寻价附件
            //if (this.txtPriceAtt.Text != "") 
            if (this.hidNames.Value != "")
            {
                //string[] sArray = this.txtPriceAtt.Text.Split(';');
                string[] sArray = this.hidNames.Value.Split(';');
                foreach (string i in sArray)
                {

                    string mapPath = ESP.Purchase.Common.ServiceURL.UpFilePath;
                    string fileName = i;
                    string savePath = mapPath + "upFile\\" + fileName;
                    string filePath = ESP.Configuration.ConfigurationManager.SafeAppSettings["supplyNewsFilePath"];
                    filePath = filePath + fileName;

                    File.Copy(filePath, savePath, true);
                    model.upfile += "upFile\\" + fileName + "#";
                    System.Threading.Thread.Sleep(100);
                }
            }

            if (!string.IsNullOrEmpty(model.upfile))
            {
                model.upfile = model.upfile.TrimEnd('#');
            }
            else 
            {
                model.upfile = "";
            }
               

            

            int orderId = OrderInfoManager.addByTrans(model);

            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对T_OrderInfo表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, orderId, "添加采购物品"), "采购物品");
            return true;
      
    }

    /// <summary>
    /// 返回按钮(返回编辑页)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        //setSetp(1);
        Response.Redirect(Request["pageUrl"] + "?" + RequestName.GeneralID + "=" + generalid);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnP3_Click(object sender, EventArgs e)
    {
        //if (hidViewType1.Value != "")
        //{
        //    List<OrderInfo> models = OrderInfoManager.GetListByGeneralId(generalid);

        //    List<TypeInfo> listType = getType1List(models);
        //    for (int i = (listType.Count - 1); i >= 0; i--)
        //    {
        //        if (listType[i].typeid.ToString() != hidViewType1.Value)
        //            listType.RemoveAt(i);
        //    }
        //    rep1.DataSource = listType;
        //    rep1.DataBind();
        //}
        //TabContainer1.ActiveTabIndex = 0;
        //setSetp(1);
    }

    /// <summary>
    /// 返回按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBack1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request["pageUrl"] + "?" + RequestName.GeneralID + "=" + generalid);
    }

    /// <summary>
    /// 返回按钮（返回单子编辑页）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBack2_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request["pageUrl"] + "?" + RequestName.GeneralID + "=" + generalid);
    }

    protected void btnBack3_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request["pageUrl"] + "?" + RequestName.GeneralID + "=" + generalid);
    }

    /// <summary>
    /// 设置显示内容
    /// </summary>
    /// <param name="stepIndex"></param>
    private void setSetp(int stepIndex)
    {
        switch (stepIndex)
        {
            case 1:
                pal1.Visible = true;
                pal2.Visible = false;
                pal3.Visible = false;
                break;
            case 2:
                pal1.Visible = false;
                pal2.Visible = true;
                pal3.Visible = false;
                break;
            case 3:
                pal1.Visible = false;
                pal2.Visible = false;
                pal3.Visible = true;
                break;
        }
    }
    #endregion

    #region Panel2
    /// <summary>
    /// 非目录物品保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave2_Click(object sender, EventArgs e)
    {
        if (SaveF())
            Response.Redirect(Request["pageUrl"] + "?" + RequestName.GeneralID + "=" + Request[RequestName.GeneralID]);
    }

    private bool SaveF()
    {
        OrderInfo model = new OrderInfo();
        GeneralInfo generalModel = GeneralInfoManager.GetModel(generalid);
        model.general_id = generalid;
        model.Item_No = txtProductName.Text.Trim();
        model.desctiprtion = desctiprtion1.Text.Trim();
        model.intend_receipt_date = intend_receipt_date1.Text.Trim();
        if (Eintend_receipt_date1.Text.Trim() != "")
            model.intend_receipt_date += "#" + Eintend_receipt_date1.Text.Trim();
        model.price = model.oldPrice = decimal.Parse(txtPrice.Text);
        model.uom = txtUnit1.Text;
        model.quantity = model.oldQuantity = decimal.Parse(quantity1.Text);

        model.total = model.price * model.quantity;
        model.producttype = int.Parse(ddlType3.SelectedValue);
        model.supplierId = int.Parse(hidSupplierId1.Value == "" ? "0" : hidSupplierId1.Value);
        model.supplierName = txtSupplierName.Text == "" ? hidSupplierName.Value : txtSupplierName.Text;
        model.productAttribute = (int)State.productAttribute.fml;
        //if (GeneralInfoManager.contrastPrice(generalid, model.total, 0))
        //{
            for (int i = 0; i < Request.Files.Count; i++)
            {
                if (Request.Files.Keys[i] == "filBJ1" && Request.Files[i].FileName != "")
                {
                    System.Web.HttpPostedFile postFile = Request.Files[i];
                    string fileName = "wuliao_" + generalid + "_" + DateTime.Now.Ticks.ToString();
                    model.upfile += FileHelper.upFile(fileName, ESP.Purchase.Common.ServiceURL.UpFilePath, postFile) + "#";
                }
                System.Threading.Thread.Sleep(100);
            }
            //添加寻价附件
            //if (this.txtPriceAtt.Text != "") 
            if (this.hidNames1.Value != "")
            {
                //string[] sArray = this.txtPriceAtt.Text.Split(';');
                string[] sArray = this.hidNames1.Value.Split(';');
                foreach (string i in sArray)
                {

                    string mapPath = ESP.Purchase.Common.ServiceURL.UpFilePath;
                    string fileName = i;
                    string savePath = mapPath + "upFile\\" + fileName;
                    string filePath = ESP.Configuration.ConfigurationManager.SafeAppSettings["supplyNewsFilePath"];
                    filePath = filePath + fileName;

                    File.Copy(filePath, savePath, true);
                    model.upfile += "upFile\\" + fileName + "#";
                    System.Threading.Thread.Sleep(100);
                }
            }

            if (!string.IsNullOrEmpty(model.upfile))
            {
                model.upfile = model.upfile.TrimEnd('#');
            }
            else
            {
                model.upfile = "";
            }
                
            int orderId = 0;
            if (hidSupplierId1.Value == "")
            {
                model.supplierId = SaveSupplySupplier(); 
                orderId = OrderInfoManager.Add(model, int.Parse(CurrentUser.SysID), CurrentUser.Name);
            }
            else
            {
                orderId = OrderInfoManager.addByTrans(model);
            }
            
            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对T_OrderInfo表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, orderId, "添加采购物品"), "采购物品");
            return true;

    }

    private void PanelInfoBind()
    {
        //List<TypeInfo> list = TypeManager.GetListByParentId(0);
        //ddlType1.DataSource = list;
        //ddlType1.DataTextField = "typename";
        //ddlType1.DataValueField = "typeid";
        //ddlType1.DataBind();
        //ddlType1.Items.Insert(0, new ListItem("请选择", "-1"));

        List<OrderInfo> models = OrderInfoManager.GetListByGeneralId(generalid);
        if (null != models && models.Count > 0 && models[0].supplierId != 0)
        {
            hidCurrentTypeId.Value = models[0].producttype.ToString();
            SetTypeDropDownList();

            btnSelect.Visible = false;
            hidABC.Value = "";
            txtSupplierName.Enabled = false;
            txtSupplierName.Text = models[0].supplierName;
            hidSupplierId1.Value = models[0].supplierId.ToString();
        }
        //else if (null != models && models.Count > 0 && models[0].supplierId == 0 && models[0].supplierName != "")
        //{
        //    btnSelect.Visible = false;
        //    hidABC.Value = "";
        //    txtSupplierName.Enabled = false;
        //    txtSupplierName.Text = models[0].supplierName;
        //    hidSupplierId1.Value = "";
        //}
        //if (null != models && models.Count > 0)
        //{
        //    SetDorpDownList(models[0].producttype, false);
        //}
        //removeDropDownListItems(ddlType1, 1);
        //removeDropDownListItems(ddlType2, 2);
    }

    /*
    protected void ddlType1_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetTypeLevel2();
    }

    private void SetTypeLevel2()
    {
        if (hidSupplierId1.Value != "")
        {
            ddlType2.Items.Clear();
            ddlType3.Items.Clear();
            //ddlType2.SelectedIndex = 0;
            List<TypeInfo> listtp2 = TypeManager.getLevel2ListBySupplierIdAndLevel1ID(int.Parse(hidSupplierId1.Value), int.Parse(ddlType1.SelectedValue));
            if (listtp2.Count > 0)
            {
                ddlType2.DataSource = listtp2;
                ddlType2.DataTextField = "typename";
                ddlType2.DataValueField = "typeid";
                ddlType2.SelectedIndex = -1;
                ddlType2.DataBind();
            }
            ddlType2.Items.Insert(0, new ListItem("请选择", "-1"));
            ddlType2.SelectedIndex = 0;
            ddlType3.Items.Insert(0, new ListItem("请选择", "-1"));
            removeDropDownListItems(ddlType2, 2);
        }
    }

    protected void ddlType2_SelectedIndexChanged(object sender, EventArgs e)
    {
        setTypeLevel3();
    }

    private void setTypeLevel3()
    {
        if (hidSupplierId1.Value != "")
        {
            try
            {
                ddlType3.DataSource = TypeManager.getLevel3ListBySupplierIdAndLevel2ID(int.Parse(hidSupplierId1.Value), int.Parse(ddlType2.SelectedValue));
                ddlType3.DataTextField = "typename";
                ddlType3.DataValueField = "typeid";
                ddlType3.DataBind();
            }
            catch { }
            removeDropDownListItems(ddlType3, 3);
        }
    }
    */

    /// <summary>
    /// 根据第三级TypeId绑定三级列表
    /// </summary>
    /// <param name="level3Id"></param>
    private void SetTypeDropDownList()
    {
        ddlType1.Items.Clear();
        ddlType2.Items.Clear();
        ddlType3.Items.Clear();
        int level3Id = int.Parse(hidCurrentTypeId.Value);
        TypeInfo level3Model = TypeManager.GetModel(level3Id);
        ddlType3.Items.Add(new ListItem(level3Model.typename, level3Model.typeid.ToString()));

        TypeInfo level2Model = TypeManager.GetModel(level3Model.parentId);
        ddlType2.Items.Add(new ListItem(level2Model.typename, level2Model.typeid.ToString()));

        TypeInfo level1Model = TypeManager.GetModel(level2Model.parentId);
        ddlType1.Items.Add(new ListItem(level1Model.typename, level1Model.typeid.ToString()));
    }

    #endregion

    protected void btnP2_Click(object sender, EventArgs e)
    {
        TabContainer1.ActiveTabIndex = 0;
        setSetp(2);
    }

    protected void btnP1_Click(object sender, EventArgs e)
    {
        TabContainer1.ActiveTabIndex = 0;
        setSetp(2);
    }

    protected void btnNext1_Click(object sender, EventArgs e)
    {
        if (SaveX())
            Response.Redirect("EditProducts.aspx?" + RequestName.GeneralID + "=" + generalid + "&pageUrl=" + Request["pageUrl"]);
    }

    protected void btnNext2_Click(object sender, EventArgs e)
    {
        if (SaveF())
            Response.Redirect("EditProducts.aspx?" + RequestName.GeneralID + "=" + generalid + "&pageUrl=" + Request["pageUrl"]);
    }

    private void BindSupplySuppliersByType(int typeid)
    {
        IList<ESPAndSupplyTypeRelationInfo> listRelation = ESPAndSupplyTypeRelationManager.GetList(" and ESPTypeId=" + typeid);
        if (listRelation != null && listRelation.Count > 0)
        {
            string supplytypeids = string.Empty;
            foreach (ESPAndSupplyTypeRelationInfo relation in listRelation)
            {
                supplytypeids += relation.SupplyTypeId + ",";
            }
            //把supplytypeid变成一个字符串
            supplytypeids = supplytypeids.Substring(0, supplytypeids.Length - 1);

            SqlParameter[] parms = null;
            IList<ESP.Supplier.Entity.SC_Supplier> listSuppliers = new ESP.Supplier.BusinessLogic.SC_SupplierManager().GetList(" AND 1=1", parms);
        }
    }
}