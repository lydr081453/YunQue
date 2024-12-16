using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using System.IO;
using ESP.Supplier.Entity;
using ESP.Supplier.BusinessLogic;

namespace PurchaseWeb.Purchase
{
    public partial class newEditProducts : ESP.Purchase.WebPage.EditPageForPR
    {
        int generalId = 0;
        static string thirdTypeIds = "";
        string query = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSecret.Visible = false;
            palSecretAdd.Visible = false;
            if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
            {
                generalId = int.Parse(Request[RequestName.GeneralID]);
                hidGeneralID.Value = generalId.ToString();
            }

            query = Request.Url.Query;

            if (!IsPostBack)
            {
                thirdTypeIds = "";


                TypeLevel1Bind();

                GeneralInfo general = GeneralInfoManager.GetModel(generalId);
                //设置货币类型
                labMoneyType.Text = labMoneyType1.Text = general.moneytype;
                //如果为3000以上生成的单子，预计收货时间默认为当前日期
                if (general.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA || general.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA)
                {
                    intend_receipt_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    intend_receipt_date1.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }

                if (!string.IsNullOrEmpty(Request["ptb"]) && !string.IsNullOrEmpty(Request["typeId"]))
                {
                    hidCurrentTypeId.Value = Request["typeId"].ToString();

                    TypeInfo model = TypeManager.GetModel(int.Parse(hidCurrentTypeId.Value));

                    List<OrderInfo> orderlist = OrderInfoManager.GetListByGeneralId(generalId);
                    if (isSecretType() && (orderlist.Count == 0 || orderlist[0].supplierId == 0))
                    {
                        hidCurrentSupplierId.Value = "0";
                        btnSecret.Visible = true;
                        palSecretAdd.Visible = true;
                    }
                    if (isSecretType() && orderlist.Count > 0 && orderlist[0].supplierId == 0)
                    {
                        btnSecret_Click(sender, e);
                    }
                    else
                    {
                        SupplierBind();
                        //隐藏物料类别信息，显示供应商列表
                        palType.Visible = false;
                        palSupplier.Visible = true;
                    }
                }


            }
        }

        /// <summary>
        /// 绑定一级物料信息
        /// </summary>
        private void TypeLevel1Bind()
        {
            string level1Ids = "";
            string thirdLevel1Ids = "";
            //申请单下采购物品列表
            List<OrderInfo> orders = OrderInfoManager.GetListByGeneralId(generalId);
            //申请单对象
            GeneralInfo generalInfo = GeneralInfoManager.GetModel(generalId);

            if (generalInfo.Project_id > 0)
            {
                //如果项目号大于0，获取项目号包含的二级物料
                DataTable typeDt = new DataTable();
                if (generalInfo.thirdParty_materielID != "")
                {
                    thirdTypeIds = generalInfo.thirdParty_materielID;
                    typeDt = TypeManager.GetList(" and typeid in (" + generalInfo.thirdParty_materielID + ")").Tables[0];
                }
                thirdLevel1Ids = ",";
                foreach (DataRow dr in typeDt.Rows)
                {
                    //获取项目号包含二级物料的一级物料
                    thirdLevel1Ids += dr["parentId"].ToString() + ",";
                }
            }
            if (orders != null && orders.Count > 0 && orders[0].supplierId > 0)
            {
                hidCurrentSupplierId.Value = orders[0].supplierId.ToString();
            }

            //if (orders != null && orders.Count > 0 && orders[0].supplierId > 0)
            //{
            //    hidCurrentSupplierId.Value = orders[0].supplierId.ToString();
            //    //如果已存在采购物品，获取该供应商下一级物料
            //    List<TypeInfo> level1s = TypeManager.getLevel1ListBySupplierId(orders[0].supplierId);
            //    if (level1s == null || level1s.Count == 0)
            //    {
            //        //不存在目录物品，则显示第一个采购物品的一级物料类别
            //        TypeInfo level3 = TypeManager.GetModel(orders[0].producttype);
            //        if (level3 != null)
            //        {
            //            TypeInfo level2 = TypeManager.GetModel(level3.parentId);
            //            if (level2 != null)
            //            {
            //                level1Ids += level2.parentId + ",";
            //            }
            //        }
            //    }
            //    else
            //    {
            //        foreach (TypeInfo t in level1s)
            //        {
            //            if (thirdLevel1Ids == "" || thirdLevel1Ids.Contains("," + t.typeid + ","))
            //            {
            //                level1Ids += t.typeid + ",";
            //            }
            //        }
            //    }
            //}
            //else
            //{
            level1Ids = thirdLevel1Ids;
            //}
            string terms = "";
            if (level1Ids == "")
                terms = " and parentId=0";
            else
                terms = " and typeId in (" + level1Ids.TrimStart(',').TrimEnd(',') + ")";
            if (!string.IsNullOrEmpty(txtKey.Text))
            {
                terms += @" and typeid in (select parentid from t_type where typeid in 
                            (select parentid from t_type where typelevel=3 and typename like '%" + txtKey.Text.Trim() + "%'))";
            }

            rep1.DataSource = TypeManager.GetList(terms);
            rep1.DataBind();
        }

        protected void rep1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dv = (DataRowView)e.Item.DataItem;
                Label lab = (Label)e.Item.FindControl("lab");
                lab.Text = dv["typeName"].ToString();
                DataList rep2 = (DataList)e.Item.FindControl("rep2");
                rep2.DataSource = TypeLevel2Bind(int.Parse(dv["typeid"].ToString()));
                rep2.DataBind();
                if (rep2.Items.Count < 1)
                    e.Item.Visible = false;
            }
        }

        /// <summary>
        /// 绑定二级物料信息
        /// </summary>
        private DataTable TypeLevel2Bind(int leve1Id)
        {
            string thirdLevel2Ids = thirdTypeIds;
            string leve2Ids = "";
            //if (int.Parse(hidCurrentSupplierId.Value) > 0)
            //{
            //    List<TypeInfo> level2s = TypeManager.getLevel2ListBySupplierIdAndLevel1ID(int.Parse(hidCurrentSupplierId.Value), leve1Id);
            //    if (level2s == null || level2s.Count == 0)
            //    {
            //        //申请单下采购物品列表
            //        List<OrderInfo> orders = OrderInfoManager.GetListByGeneralId(generalId);
            //        if (orders != null && orders.Count > 0)
            //        {
            //            TypeInfo level3 = TypeManager.GetModel(orders[0].producttype);
            //            if (level3 != null)
            //            {
            //                leve2Ids += level3.parentId + ",";
            //            }
            //        }
            //    }
            //    else
            //    {
            //        thirdLevel2Ids = "," + thirdLevel2Ids + ",";
            //        foreach (TypeInfo t in level2s)
            //        {
            //            if (thirdTypeIds == "" || thirdLevel2Ids.Contains("," + t.typeid + ","))
            //            {
            //                leve2Ids += t.typeid + ",";
            //            }
            //        }
            //    }
            //}
            //else
            //{
            leve2Ids = thirdLevel2Ids;
            //}
            string terms = "";
            if (leve2Ids == "")
                terms = " and parentId=" + leve1Id;
            else
                terms = " and parentId=" + leve1Id + " and typeid in (" + leve2Ids.TrimStart(',').TrimEnd(',') + ")";

            if (!string.IsNullOrEmpty(txtKey.Text))
            {
                terms += @" and typeid in
                            (select parentid from t_type where typelevel=3 and typename like '%" + txtKey.Text.Trim() + "%')";
            }
            return TypeManager.GetList(terms).Tables[0];
        }

        protected void rep2_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dv = (DataRowView)e.Item.DataItem;
                Label lab = (Label)e.Item.FindControl("lab");
                lab.Text = dv["typename"].ToString();
                DataList dg3 = (DataList)e.Item.FindControl("dg3");
                dg3.DataSource = TypeLevel3Bind(int.Parse(dv["typeid"].ToString()));
                dg3.DataBind();
                if (dg3.Items.Count < 1)
                    e.Item.Visible = false;
            }
        }

        /// <summary>
        /// 绑定三级物料
        /// </summary>
        /// <param name="level2Id"></param>
        /// <returns></returns>
        private DataTable TypeLevel3Bind(int level2Id)
        {
            string typeids = "";
            string terms = " and parentId=" + level2Id;

            if (typeids != "")
                terms = " and typeid in (" + typeids.TrimEnd(',') + ")";
            if (!string.IsNullOrEmpty(txtKey.Text))
            {
                terms += " and typename like '%" + txtKey.Text.Trim() + "%'";
            }
            return TypeManager.GetList(terms).Tables[0];
        }

        protected void dg3_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dv = (DataRowView)e.Item.DataItem;
                LinkButton lnk = (LinkButton)e.Item.FindControl("lnk");
                if (int.Parse(dv["status"].ToString()) == State.typestatus_mediaUp3000)
                {
                    GeneralInfo general = GeneralInfoManager.GetModel(generalId);
                    if (general.PRType != (int)PRTYpe.PR_MediaFA && general.PRType != (int)PRTYpe.PR_PriFA)
                        lnk.Visible = false; //只有由3000以上媒介生成的pr单，才显示状态为typestatus_mediaUp3000的物料类别
                    else
                    {
                        lnk.Enabled = true;
                        lnk.Visible = true;
                    }
                }
                lnk.Text = dv["typename"].ToString();
            }
        }

        /// <summary>
        /// 选择物料类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnk_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            hidCurrentTypeId.Value = lnk.CommandArgument.ToString();

            TypeInfo model = TypeManager.GetModel(int.Parse(hidCurrentTypeId.Value));
            if (null != model)
            {
                ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(CurrentUserID);
                string mobile = emp.MobilePhone.Replace("-", "").Trim();
                //string xiaomiMobiles = ESP.Purchase.BusinessLogic.XiaoMiGroupManager.GetMobiles(mobile);


                if (model.operationflow == State.typeoperationflow_Media)
                {
                    Response.Redirect("WrittingFeeApplicant.aspx" + query);
                }
                else if (model.operationflow == State.typeoperationflow_OtheMedia && !string.IsNullOrEmpty(mobile))
                {
                    Response.Redirect("ShunyaXiaomi/XiaoMiOrder.aspx?typeId=" + hidCurrentTypeId.Value + "&GeneralID=" + Request[RequestName.GeneralID]);
                }
                else if (model.operationflow == (int)State.typeoperationflow_Advertisement)
                {
                    Response.Redirect("Advertisement/AdvertisementOrder.aspx?GeneralID=" + Request[RequestName.GeneralID]);
                }
                else
                {
                    List<OrderInfo> orderlist = OrderInfoManager.GetListByGeneralId(generalId);
                    if (isSecretType() && (orderlist.Count == 0 || orderlist[0].supplierId == 0))
                    {
                        hidCurrentSupplierId.Value = "0";
                        btnSecret.Visible = true;
                        palSecretAdd.Visible = true;
                    }
                    if (isSecretType() && orderlist.Count > 0 && orderlist[0].supplierId == 0)
                    {
                        btnSecret_Click(sender, e);
                    }
                    else
                    {
                        SupplierBind();
                        //隐藏物料类别信息，显示供应商列表
                        palType.Visible = false;
                        palSupplier.Visible = true;
                    }
                }
            }

        }

        protected void btnSearch1_Click(object sender, EventArgs e)
        {
            TypeLevel1Bind();
        }

        #region 新修改
        public void SupplierBind()
        {
            FXYSupplierBind();
            HisSupplierBind();
        }

        public void FXYSupplierBind()
        {
            string Terms = "";
            List<SqlParameter> parms = new List<SqlParameter>();
            if (txtSupplier.Text.Trim() != "")
            {
                Terms += " and (c2.supplier_name like '%'+@suppliername+'%')";
                parms.Add(new SqlParameter("@suppliername", txtSupplier.Text.Trim()));
            }
            if (int.Parse(hidCurrentSupplierId.Value) != 0)
            {
                Terms += " and c2.id=" + int.Parse(hidCurrentSupplierId.Value);
            }

            DataTable fxyList = SupplierManager.getSupplierListOrderByFeiXieYi(Terms, parms, hidCurrentTypeId.Value);

            PagedDataSource ps = new PagedDataSource();
            ps.DataSource = fxyList.DefaultView;
            ps.AllowPaging = true;
            ps.PageSize = 10;
            int curpage = int.Parse(ViewState["currentPageIndex2"] == null ? "0" : ViewState["currentPageIndex2"].ToString());
            ViewState["currentPageIndex2"] = curpage;
            btnFP2.Visible = btnPP2.Visible = !(curpage == 0);
            btnLP2.Visible = btnNP2.Visible = !(curpage == (ps.PageCount - 1));
            litCurrentPage2.Text = (curpage + 1).ToString();
            litTotalPage2.Text = ps.PageCount.ToString();
            litCount2.Text = fxyList.Rows.Count.ToString();
            ps.CurrentPageIndex = curpage;
            ViewState["pageCount2"] = ps.PageCount.ToString();

            repFXY.DataSource = ps;
            repFXY.DataBind();
        }


        public void HisSupplierBind()
        {
            string Terms = " and c2.requestor=@requestor  ";
            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@requestor", CurrentUserID));

            if (txtSupplier.Text.Trim() != "")
            {
                Terms += " and (c2.supplier_name like '%'+@suppliername+'%')";
                parms.Add(new SqlParameter("@suppliername", txtSupplier.Text.Trim()));
            }
            if (int.Parse(hidCurrentSupplierId.Value) != 0)
            {
                Terms += " and c2.id=" + int.Parse(hidCurrentSupplierId.Value);
            }

            DataTable histList = SupplierManager.getSupplierListOrderByHist(Terms, parms, hidCurrentTypeId.Value);

            PagedDataSource ps = new PagedDataSource();
            ps.DataSource = histList.DefaultView;
            ps.AllowPaging = true;
            ps.PageSize = 10;
            int curpage = int.Parse(ViewState["currentPageIndex3"] == null ? "0" : ViewState["currentPageIndex3"].ToString());
            ViewState["currentPageIndex3"] = curpage;
            btnFP3.Visible = btnPP3.Visible = !(curpage == 0);
            btnLP3.Visible = btnNP3.Visible = !(curpage == (ps.PageCount - 1));
            litCurrentPage3.Text = (curpage + 1).ToString();
            litTotalPage3.Text = ps.PageCount.ToString();
            litCount3.Text = histList.Rows.Count.ToString();
            ps.CurrentPageIndex = curpage;
            ViewState["pageCount3"] = ps.PageCount.ToString();

            rpHis.DataSource = ps;
            rpHis.DataBind();
        }

        protected void repFXY_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
        }

        protected void repFXY_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {

        }



        protected void btnFP2_Click(object sender, EventArgs e)
        {
            ViewState["currentPageIndex2"] = 0;
            FXYSupplierBind();
        }

        protected void btnPP2_Click(object sender, EventArgs e)
        {
            ViewState["currentPageIndex2"] = int.Parse(ViewState["currentPageIndex2"].ToString()) - 1;
            FXYSupplierBind();
        }

        protected void btnNP2_Click(object sender, EventArgs e)
        {
            ViewState["currentPageIndex2"] = int.Parse(ViewState["currentPageIndex2"].ToString()) + 1;
            FXYSupplierBind();
        }

        protected void btnLP2_Click(object sender, EventArgs e)
        {
            ViewState["currentPageIndex2"] = int.Parse(ViewState["pageCount2"].ToString()) - 1;
            FXYSupplierBind();
        }
        #endregion

        /* 旧版
        private void SupplierBind()
        {
            string Terms = "";
            List<SqlParameter> parms = new List<SqlParameter>();
            if (txtSupplier.Text.Trim() != "")
            {
                Terms += " and (c.supplier_name like '%'+@suppliername+'%' or a.supplier_name like '%'+@suppliername+'%')";
                parms.Add(new SqlParameter("@suppliername", txtSupplier.Text.Trim()));
            }
            if (int.Parse(hidCurrentSupplierId.Value) != 0)
            {
                Terms += " and c.id=" + int.Parse(hidCurrentSupplierId.Value);
            }
            DataTable supplierList = SupplierManager.getLinkSupplySupplierList(Terms, int.Parse(hidCurrentTypeId.Value), parms, txtProduct.Text.Trim());
            if (int.Parse(hidCurrentSupplierId.Value) != 0 && (supplierList == null || supplierList.Rows.Count == 0))
            {
                //如果已确定供应商，并该供应商没有该物料类别，则只能添加该供应商的非目录物品
                supplierList = SupplierManager.GetList(" and id=" + hidCurrentSupplierId.Value, new List<SqlParameter>()).Tables[0];
            }
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

            repSupplier.DataSource = ps;
            repSupplier.DataBind();
        }

        protected void btnFP_Click(object sender, EventArgs e)
        {
            ViewState["currentPageIndex"] = 0;
            SupplierBind();
        }

        protected void btnPP_Click(object sender, EventArgs e)
        {
            ViewState["currentPageIndex"] = int.Parse(ViewState["currentPageIndex"].ToString()) - 1;
            SupplierBind();
        }

        protected void btnNP_Click(object sender, EventArgs e)
        {
            ViewState["currentPageIndex"] = int.Parse(ViewState["currentPageIndex"].ToString()) + 1;
            SupplierBind();
        }

        protected void btnLP_Click(object sender, EventArgs e)
        {
            ViewState["currentPageIndex"] = int.Parse(ViewState["pageCount"].ToString()) - 1;
            SupplierBind();
        }

        protected void repSupplier_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                DataRowView dv = (DataRowView)e.Item.DataItem;
                GridView gvProduct = (GridView)e.Item.FindControl("gvProduct");

                int sid = dv["id"] == DBNull.Value ? 0 : int.Parse(dv["id"].ToString());
                List<ProductInfo> products = ProductsBind(sid);
                gvProduct.DataSource = products;
                gvProduct.DataBind();

                LinkButton linkShow = (LinkButton)e.Item.FindControl("linkShow");
                if (products.Count == 0)
                    linkShow.Visible = false;
            }
        }

        protected void repSupplier_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "showproduct")
            {
                GridView gvProduct = (GridView)e.Item.FindControl("gvProduct");
                LinkButton linkShow = (LinkButton)e.Item.FindControl("linkShow");
                System.Web.UI.HtmlControls.HtmlGenericControl divProduct = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divProduct");
                if (gvProduct != null)
                {
                    if (linkShow.Text == "展开")
                    {
                        gvProduct.Visible = true;
                        linkShow.Text = "隐藏";

                        if (gvProduct.Rows.Count > 3)
                        {
                            divProduct.Style.Add("display", "block");
                            divProduct.Style.Add("overflow", "auto");
                            divProduct.Style.Add("height", "150px");
                        }
                    }
                    else
                    {
                        linkShow.Text = "展开";
                        gvProduct.Visible = false;
                        if (gvProduct.Rows.Count > 3)
                        {
                            divProduct.Style.Clear();
                            divProduct.Style.Add("display", "none");
                        }
                    }
                }
            }
        }
         */

        /// <summary>
        /// 供应商处上一步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPSuplier_Click(object sender, EventArgs e)
        {
            palSupplier.Visible = false;
            palType.Visible = true;
        }

        private List<ProductInfo> ProductsBind(int sid)
        {
            List<SqlParameter> parms = new List<SqlParameter>();
            string Terms = " and a.productType=@productType and IsShow=1 and c.status<>" + State.typestatus_block;
            parms.Add(new SqlParameter("@productType", int.Parse(hidCurrentTypeId.Value)));
            if (txtProduct.Text.Trim() != "")
            {
                Terms += " and a.productName like '%'+@productname+'%'";
                parms.Add(new SqlParameter("@productname", txtProduct.Text.Trim()));
            }
            if (sid != 0)
            {
                Terms += " and b.id=" + sid;
                return ProductManager.getModelList(Terms, parms);
            }
            return new List<ProductInfo>();
        }

        protected void btnSearch2_Click(object sender, EventArgs e)
        {
            ViewState["currentPageIndex"] = "0";
            ViewState["currentPageIndex1"] = "0";
            ViewState["currentPageIndex2"] = "0";
            SupplierBind();
        }

        /// <summary>
        /// 选择目录物品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelectML_Click(object sender, EventArgs e)
        {
            Button btnSelectML = (Button)sender;
            ESP.Purchase.Entity.TypeInfo typeModel = ESP.Purchase.BusinessLogic.TypeManager.GetModel(int.Parse(hidCurrentTypeId.Value));
            if (typeModel.operationflow == (int)State.typeoperationflow_Advertisement)
            {
                ProductInfo model = ProductManager.GetModel(int.Parse(btnSelectML.CommandArgument.ToString()));
                Response.Redirect("Advertisement/AdvertisementOrder.aspx?GeneralID=" + Request[RequestName.GeneralID] + "&SupplierId=" + model.supplierId + "&pageUrl=AddRequisitionStep6.aspx");
            }
            else
            {
                setMLInfo(int.Parse(btnSelectML.CommandArgument.ToString()));

                //显示TAB2、目录物品，隐藏Tab1、非目录物品
                Tab1.Visible = false;
                palFML.Visible = false;
                Tab2.Visible = true;
                palML.Visible = true;
            }


        }

        /// <summary>
        /// 设置目录物品信息
        /// </summary>
        /// <param name="mlId"></param>
        private void setMLInfo(int mlId)
        {
            ProductInfo model = ProductManager.GetModel(mlId);
            TypeInfo typeModel = TypeManager.GetModel(model.productType);
            TypeInfo typeModel2 = TypeManager.GetModel(typeModel.parentId);
            TypeInfo typeModel1 = TypeManager.GetModel(typeModel2.parentId);
            labProductType.Text = typeModel1.typename + " - " + typeModel2.typename + " - " + typeModel.typename;

            //hidProductTypeId.Value = model.productType.ToString();
            labName.Text = model.productName;
            labPrice.Text = model.ProductPrice.ToString("#,##0.00");
            hidMLPrice.Value = model.ProductPrice.ToString("0.##");
            labUnit.Text = model.productUnit;
            labSupplierName.Text = model.supplierName;
            hidCurrentSupplierId.Value = model.supplierId.ToString();
        }

        /// <summary>
        /// 目录物品保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave1_Click(object sender, EventArgs e)
        {
            if (SaveML())
                Response.Redirect(Request["pageUrl"] + "?" + RequestName.GeneralID + "=" + Request[RequestName.GeneralID]); ;
        }

        protected void btnNext1_Click(object sender, EventArgs e)
        {
            if (SaveML())
                Response.Redirect("newEditProducts.aspx?" + RequestName.GeneralID + "=" + generalId + "&pageUrl=" + Request["pageUrl"]);
        }

        private bool SaveML()
        {
            OrderInfo model = new OrderInfo();
            model.general_id = generalId;
            model.Item_No = labName.Text;

            model.desctiprtion = desctiprtion.Text.Trim();
            model.intend_receipt_date = intend_receipt_date.Text.Trim();
            if (Eintend_receipt_date.Text.Trim() != "")
                model.intend_receipt_date += "#" + Eintend_receipt_date.Text.Trim();
            model.price = model.oldPrice = decimal.Parse(labPrice.Text);
            model.uom = labUnit.Text;
            model.quantity = model.oldQuantity = decimal.Parse(quantity.Text);

            model.total = model.price * model.quantity;
            model.producttype = int.Parse(hidCurrentTypeId.Value);
            model.supplierId = int.Parse(hidCurrentSupplierId.Value);
            model.supplierName = labSupplierName.Text;
            model.productAttribute = (int)State.productAttribute.ml;


            if (string.IsNullOrEmpty(model.upfile))
                model.upfile = "";

            for (int i = 0; i < Request.Files.Count; i++)
            {
                if (Request.Files.Keys[i] == "filBJ2" && Request.Files[i].FileName != "")
                {
                    System.Web.HttpPostedFile postFile = Request.Files[i];
                    string fileName = "wuliao_" + generalId + "_" + DateTime.Now.Ticks.ToString();
                    model.upfile += FileHelper.upFile(fileName, ESP.Purchase.Common.ServiceURL.UpFilePath, postFile) + "#";
                }
                System.Threading.Thread.Sleep(100);
            }

            //添加寻价附件
            if (this.hidNames.Value != "")
            {
                //string[] sArray = this.txtPriceAtt.Text.Split(';');
                string[] sArray = this.hidNames.Value.Split(';');
                string[] sIds = txtIds.Text.Split(',');
                for (int i = 0; i < sArray.Length; i++)
                {
                    string mapPath = ESP.Purchase.Common.ServiceURL.UpFilePath;
                    string fileName = sArray[i];
                    //string extension = System.IO.Path.GetExtension(sArray[i]).ToLower();
                    //保存文件名以回复id开头
                    //string newfilename = sIds[i]+"_" +Guid.NewGuid().ToString() + extension;
                    //string savePath = mapPath + "upFile\\" + newfilename;
                    string savePath = mapPath + "upFile\\" + sIds[i] + "_" + fileName;
                    string filePath = ESP.Configuration.ConfigurationManager.SafeAppSettings["supplyNewsFilePath"];
                    filePath = filePath + fileName;

                    File.Copy(filePath, savePath, true);
                    //model.upfile += "upFile\\" + newfilename + "#";
                    model.upfile += "upFile\\" + sIds[i] + "_" + fileName + "#";
                    System.Threading.Thread.Sleep(100);
                }
            }

            if (!string.IsNullOrEmpty(model.upfile))
                model.upfile = model.upfile.TrimEnd('#');



            int orderId = OrderInfoManager.addByTrans(model);

            //保存order单与回复附件
            ESP.Purchase.Entity.OrderMsg omModel = new OrderMsg();
            omModel.GeneralId = generalId;
            omModel.OrderId = orderId;
            omModel.MsgReturnId = txtIds.Text;
            omModel.CreatTime = omModel.UpdateTime = DateTime.Now;
            omModel.CreatUserId = omModel.UpdateUserId = CurrentUserID;
            OrderMsgManager.Add(omModel);

            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对T_OrderInfo表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, orderId, "添加采购物品"), "采购物品");
            return true;
            //}
            //else
            //{
            //    ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('采购物品总金额已经超过第三方采购成本预算！');", true);
            //    return false;
            //}
        }

        /// <summary>
        /// 返回按钮(返回编辑页)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request["pageUrl"] + "?" + RequestName.GeneralID + "=" + generalId);
        }

        /// <summary>
        /// 非目录物品保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave2_Click(object sender, EventArgs e)
        {
            if (SaveFML())
                Response.Redirect(Request["pageUrl"] + "?" + RequestName.GeneralID + "=" + Request[RequestName.GeneralID]);
        }

        protected void btnNext2_Click(object sender, EventArgs e)
        {
            if (SaveFML())
                Response.Redirect("newEditProducts.aspx?" + RequestName.GeneralID + "=" + generalId + "&pageUrl=" + Request["pageUrl"]);
        }

        /// <summary>
        /// 采购物品上一步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnP_Click(object sender, EventArgs e)
        {
            //隐藏Tab2,采购物品，物料类型,显示Tab1,供应商
            Tab2.Visible = false;
            palML.Visible = false;
            palFML.Visible = false;
            Tab1.Visible = true;
            palType.Visible = false;
            palSupplier.Visible = true;
        }

        private bool SaveFML()
        {
            //非目录物品必须添加报价信息
            if (hidNames1.Value == "")
            {
                ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('非目录物品必须添加报价信息！')", true);
                return false;
            }
            OrderInfo model = new OrderInfo();
            GeneralInfo generalModel = GeneralInfoManager.GetModel(generalId);
            model.general_id = generalId;
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
            model.supplierId = int.Parse(hidCurrentSupplierId.Value);
            model.supplierName = txtSupplierName.Text.Trim();
            model.productAttribute = (int)State.productAttribute.fml;
            if (string.IsNullOrEmpty(model.upfile))
                model.upfile = "";

            //if (GeneralInfoManager.contrastPrice(generalId, model.total, 0))
            //{
            for (int i = 0; i < Request.Files.Count; i++)
            {
                if (Request.Files.Keys[i] == "filBJ1" && Request.Files[i].FileName != "")
                {
                    System.Web.HttpPostedFile postFile = Request.Files[i];
                    string fileName = "wuliao_" + generalId + "_" + DateTime.Now.Ticks.ToString();
                    model.upfile += FileHelper.upFile(fileName, ESP.Purchase.Common.ServiceURL.UpFilePath, postFile) + "#";
                }
                System.Threading.Thread.Sleep(100);
            }
            //添加寻价附件
            if (this.hidNames1.Value != "")
            {
                string[] sArray = this.hidNames1.Value.Split(';');
                for (int i = 0; i < sArray.Length; i++)
                {
                    string mapPath = ESP.Purchase.Common.ServiceURL.UpFilePath;
                    string fileName = sArray[i];
                    //保存文件名以回复id开头
                    string newfilename = "_" + Guid.NewGuid().ToString();
                    string savePath = mapPath + "upFile\\" + newfilename + "_" + fileName;
                    string filePath = ESP.Configuration.ConfigurationManager.SafeAppSettings["supplyNewsFilePath"];
                    filePath = filePath + fileName;

                    File.Copy(filePath, savePath, true);
                    model.upfile += "upFile\\" + newfilename + "_" + fileName + "#";
                    System.Threading.Thread.Sleep(100);
                }
            }
            if (!string.IsNullOrEmpty(model.upfile))
                model.upfile = model.upfile.TrimEnd('#');
            int orderId = 0;
            if (hidCurrentSupplierId.Value == "0")
            {

                generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(generalModel.id);
                if (palFX.Visible)
                {
                    generalModel.supplier_name = txtSupplierName.Text.Trim();

                    if (txtaccountBank.Text.Trim() == "" || txtaccountName.Text.Trim() == "" || txtaccountNum.Text.Trim() == "")
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('请填写供应商帐号信息！')", true);
                        return false;
                    }
                    generalModel.account_bank = txtaccountBank.Text.Trim();
                    generalModel.account_name = txtaccountName.Text.Trim();
                    generalModel.account_number = txtaccountNum.Text.Trim();


                    generalModel.OperationType = 0;

                    generalModel.Requisitionflow = !string.IsNullOrEmpty(rblrequisitionflow.SelectedValue) ? int.Parse(rblrequisitionflow.SelectedValue) : 0;
                    if (ddlsource.SelectedValue == "客户指定")
                    {
                        if (null != filEmailFile.PostedFile && filEmailFile.PostedFile.FileName != "")
                        {
                            string fileName = string.IsNullOrEmpty(generalModel.CusAskEmailFile) ? ("cursask_" + generalModel.id + "_" + DateTime.Now.Ticks.ToString()) : generalModel.CusAskEmailFile.Split('\\')[1].ToString().Split('.')[0].ToString();
                            generalModel.CusAskEmailFile = FileHelper.upFile(fileName, ESP.Purchase.Common.ServiceURL.UpFilePath, filEmailFile);
                        }
                    }
                    else
                    {
                        generalModel.CusAskEmailFile = "";
                    }
                    generalModel.source = ddlsource.SelectedValue;
                }
                if (palSecret.Visible)
                {
                    generalModel.supplier_name = txtSupplierName.Text.Trim();
                    generalModel.supplier_linkman = txtLinker.Text.Trim();
                    generalModel.Supplier_cellphone = txtMobile.Text.Trim();
                    generalModel.supplier_email = txtEmail.Text.Trim();
                    generalModel.supplier_address = txtAddress.Text.Trim();

                    generalModel.account_bank = txtaccountBank.Text.Trim();
                    generalModel.account_name = txtaccountName.Text.Trim();
                    generalModel.account_number = txtaccountNum.Text.Trim();
                    generalModel.supplier_phone = txtsupplier_con.Text;
                    generalModel.supplier_fax = txtsupplierfax_con.Text.Trim();

                    PRAuthorizationInfo prauthorization = PRAuthorizationManager.GetUsedModel(generalModel.requestor);
                    if (prauthorization != null)
                        generalModel.PRAuthorizationId = prauthorization.Id;
                }
                GeneralInfoManager.Update(generalModel);
                orderId = OrderInfoManager.Add(model, int.Parse(CurrentUser.SysID), CurrentUser.Name);
            }
            else
            {
                if (palFX.Visible)
                {
                    if (txtaccountBank.Text.Trim() == "" || txtaccountName.Text.Trim() == "" || txtaccountNum.Text.Trim() == "")
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('请填写供应商帐号信息！')", true);
                        return false;
                    }
                    generalModel.supplier_name = txtSupplierName.Text.Trim();

                    if (palSecret.Visible)
                    {
                        generalModel.supplier_name = txtSupplierName.Text.Trim();
                        generalModel.supplier_linkman = txtLinker.Text.Trim();
                        generalModel.Supplier_cellphone = txtMobile.Text.Trim();
                        generalModel.supplier_email = txtEmail.Text.Trim();
                        generalModel.supplier_address = txtAddress.Text.Trim();

                        generalModel.account_bank = txtaccountBank.Text.Trim();
                        generalModel.account_name = txtaccountName.Text.Trim();
                        generalModel.account_number = txtaccountNum.Text.Trim();

                        generalModel.supplier_phone = txtsupplier_con.Text;
                        generalModel.supplier_fax = txtsupplierfax_con.Text.Trim();

                        PRAuthorizationInfo prauthorization = PRAuthorizationManager.GetUsedModel(generalModel.requestor);
                        if (prauthorization != null)
                            generalModel.PRAuthorizationId = prauthorization.Id;
                    }
                    else
                    {
                        generalModel.account_bank = txtaccountBank.Text.Trim();
                        generalModel.account_name = txtaccountName.Text.Trim();
                        generalModel.account_number = txtaccountNum.Text.Trim();

                        ESP.Purchase.Entity.SupplierInfo supplier = ESP.Purchase.BusinessLogic.SupplierManager.GetModel(model.supplierId);
                        if (supplier != null)
                        {
                            generalModel.supplier_linkman = supplier.contact_name;
                            generalModel.supplier_phone = supplier.contact_tel;
                            generalModel.Supplier_cellphone = supplier.contact_mobile;
                            generalModel.supplier_fax = supplier.contact_fax;
                            generalModel.supplier_email = supplier.contact_email;
                            generalModel.fa_no = supplier.supplier_frameNO;
                            generalModel.source = ddlsource.SelectedValue;
                            generalModel.supplier_address = supplier.contact_address;
                        }
                    }
                    generalModel.OperationType = 0;

                    generalModel.Requisitionflow = !string.IsNullOrEmpty(rblrequisitionflow.SelectedValue) ? int.Parse(rblrequisitionflow.SelectedValue) : 0;
                    if (ddlsource.SelectedValue == "客户指定")
                    {
                        if (null != filEmailFile.PostedFile && filEmailFile.PostedFile.FileName != "")
                        {
                            string fileName = string.IsNullOrEmpty(generalModel.CusAskEmailFile) ? ("cursask_" + generalModel.id + "_" + DateTime.Now.Ticks.ToString()) : generalModel.CusAskEmailFile.Split('\\')[1].ToString().Split('.')[0].ToString();
                            generalModel.CusAskEmailFile = FileHelper.upFile(fileName, ESP.Purchase.Common.ServiceURL.UpFilePath, filEmailFile);
                        }
                    }
                    else
                    {
                        generalModel.CusAskEmailFile = "";
                    }
                    generalModel.source = ddlsource.SelectedValue;
                    GeneralInfoManager.Update(generalModel);

                }
                orderId = OrderInfoManager.addByTrans(model);
            }

            //保存order单与回复附件
            ESP.Purchase.Entity.OrderMsg omModel = new OrderMsg();
            omModel.GeneralId = generalId;
            omModel.OrderId = orderId;
            omModel.MsgReturnId = txtIds.Text;
            omModel.CreatTime = DateTime.Now;
            omModel.UpdateTime = DateTime.Now;
            omModel.CreatUserId = CurrentUserID;
            OrderMsgManager.Add(omModel);

            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对T_OrderInfo表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, orderId, "添加采购物品"), "采购物品");
            return true;
            //}
            //else
            //{
            //    ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('采购物品总金额已经超过第三方采购成本预算！')", true);
            //    return false;
            //}
        }

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

        protected void rbl_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void setSupplyInfo()
        {
            radioBind();
        }

        /// <summary>
        /// 将数据源绑定到被调用的服务器控件及其所有子控件。
        /// </summary>
        public void radioBind()
        {
            rblrequisitionflow.Items.Clear();
            rblrequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toO], State.requisitionflow_toO.ToString()));
            rblrequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toC], State.requisitionflow_toC.ToString()));
            rblrequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toFC], State.requisitionflow_toFC.ToString()));
            rblrequisitionflow.SelectedValue = State.requisitionflow_toO.ToString();
        }


        private string getTel(string tel)
        {
            try
            {
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
            catch
            {
                return "---";
            }
        }

        /// <summary>
        /// 选择供应商
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelectSupplier_Click(object sender, EventArgs e)
        {
            Button btnSelectSupplier = (Button)sender;
            string supplierId = btnSelectSupplier.CommandArgument.ToString().Split('-')[0];

            bool isMLProduct = false;
            ESP.Purchase.Entity.TypeInfo typeModel = ESP.Purchase.BusinessLogic.TypeManager.GetModel(int.Parse(hidCurrentTypeId.Value));
            if (!string.IsNullOrEmpty(supplierId))
            {
                IList<ESP.Purchase.Entity.ProductInfo> productList = ESP.Purchase.BusinessLogic.ProductManager.getListBySupplierId(Convert.ToInt32(supplierId), null);
                foreach (ESP.Purchase.Entity.ProductInfo p in productList)
                {
                    if (typeModel.typeid == p.id)
                    {
                        isMLProduct = true;
                    }
                }
            }
            if (typeModel.operationflow == (int)State.typeoperationflow_Advertisement)
            {

                Response.Redirect("Advertisement/AdvertisementOrder.aspx?GeneralID=" + Request[RequestName.GeneralID] + "&SupplierId=" + supplierId + "&pageUrl=AddRequisitionStep6.aspx");
            }
            else
            {
                SetTypeDropDownList();
                setSupplyInfo();
                if (supplierId != "")
                {
                    //选择采购平台存在的供应商
                    SupplierInfo model = SupplierManager.GetModel(int.Parse(supplierId));
                    txtSupplierName.Text = model.supplier_name;
                    hidCurrentSupplierId.Value = supplierId.ToString(); ;

                    if (model.supplier_type != (int)State.supplier_type.agreement || (model.supplier_type == (int)State.supplier_type.agreement && isMLProduct == false))
                    {
                        palFX.Visible = true;
                        GeneralInfo generalModel = GeneralInfoManager.GetModel(generalId);
                        if (generalModel.source == "客户指定")
                        {
                            divEmail.Style["display"] = "block";
                            labEmailFile.Text = generalModel.CusAskEmailFile == "" ? "" : "<a target='_blank' href='/Purchase/Requisition/PrFileDownLoad.aspx?GeneralId=" + generalModel.id.ToString() + "&Index=0&Type=CusAskEmailFile'><img src='/images/ico_04.gif' border='0' /></a>";
                        }
                        try
                        {
                            ddlsource.SelectedValue = generalModel.source;
                        }
                        catch { }

                        if (generalModel.account_bank != "")
                        {
                            txtaccountBank.Text = generalModel.account_bank;
                            txtaccountName.Text = generalModel.account_name;
                            txtaccountNum.Text = generalModel.account_number;
                        }
                        else
                        {
                            txtaccountBank.Text = model.account_bank;
                            txtaccountName.Text = model.account_name;
                            txtaccountNum.Text = model.account_number;
                        }
                        rblrequisitionflow.SelectedValue = generalModel.Requisitionflow.ToString();
                    }
                    else
                    {
                        palFX.Visible = false;

                    }
                }
                else
                {

                    palFX.Visible = true;
                }

                //显示TAB2、非目录物品，隐藏Tab1、目录物品
                Tab1.Visible = false;
                palML.Visible = false;
                Tab2.Visible = true;
                palFML.Visible = true;
                palSecret.Visible = false;
                txtSupplierName.Enabled = false;
            }
        }


        protected void btnSelectSupplierHist_Click(object sender, EventArgs e)
        {

            Button btnSelectSupplierHis = (Button)sender;
            string gid = btnSelectSupplierHis.CommandArgument.ToString();

            ESP.Purchase.Entity.TypeInfo typeModel = ESP.Purchase.BusinessLogic.TypeManager.GetModel(int.Parse(hidCurrentTypeId.Value));

            SetTypeDropDownList();
            setSupplyInfo();

            //从PR单里选择供应商
            GeneralInfo gsmodel = GeneralInfoManager.GetModel(int.Parse(gid));//待复制供应商信息的PR单
            OrderInfo orderModel = OrderInfoManager.GetModelByGeneralID(gsmodel.id);//待复制物料信息
            txtSupplierName.Text = gsmodel.supplier_name;
            if (orderModel != null)
                hidCurrentSupplierId.Value = orderModel.supplierId.ToString();


            palFX.Visible = true;
            GeneralInfo generalModel = GeneralInfoManager.GetModel(generalId);//当前待编辑的PR单

            try
            {
                ddlsource.SelectedValue = generalModel.source;
            }
            catch { }

            if (generalModel.account_bank != "")
            {
                txtaccountBank.Text = generalModel.account_bank;
                txtaccountName.Text = generalModel.account_name;
                txtaccountNum.Text = generalModel.account_number;
            }
            else
            {
                txtaccountBank.Text = gsmodel.account_bank;
                txtaccountName.Text = gsmodel.account_name;
                txtaccountNum.Text = gsmodel.account_number;
            }

            txtLinker.Text = gsmodel.supplier_linkman;
            txtAddress.Text = gsmodel.supplier_address;
            txtsupplier_con.Text = gsmodel.supplier_phone;
            txtSupplierName.Text = gsmodel.supplier_name;
            txtsupplierfax_con.Text = gsmodel.supplier_fax;
            txtMobile.Text = gsmodel.Supplier_cellphone;
            txtEmail.Text = gsmodel.supplier_email;
            rblrequisitionflow.SelectedValue = generalModel.Requisitionflow.ToString();

            //显示TAB2、非目录物品，隐藏Tab1、目录物品
            Tab1.Visible = false;
            palML.Visible = false;
            Tab2.Visible = true;
            palFML.Visible = true;
            palSecret.Visible = true;
            txtSupplierName.Enabled = false;

        }

        /// <summary>
        /// 是否为私密物料类型
        /// </summary>
        /// <returns></returns>
        private bool isSecretType()
        {
            //GeneralInfo gmodel = GeneralInfoManager.GetModel(generalId);
            ////MediaAuthorization
            //List<ESP.Purchase.Entity.PRAuthorizationInfo> authlist = PRAuthorizationManager.GetList(" and userid=" + gmodel.requestor.ToString() + " and status=1", new List<SqlParameter>());
            //string typeids = string.Empty;
            //bool ishavetypeids = false;
            //bool ishavemediaids = false;
            //foreach (PRAuthorizationInfo auth in authlist)
            //{
            //    typeids += auth.TypeId.TrimEnd(',');
            //}
            //typeids += ",";
            //int typeid = int.Parse(hidCurrentTypeId.Value);
            //TypeInfo currentType = TypeManager.GetModel(typeid);
            //if (!string.IsNullOrEmpty(hidCurrentTypeId.Value))
            //{
            //    string mediaids = ESP.Configuration.ConfigurationManager.SafeAppSettings["MediaAuthorization"];

            //    if (typeids.Contains("," + currentType.parentId.ToString() + ","))
            //    {
            //        ishavetypeids = true;
            //    }

            //    if (mediaids.Contains("," + currentType.parentId.ToString() + ","))
            //    {
            //        ishavemediaids = true;
            //    }
            //}

            //if (ishavetypeids)
            //{
            //    return true;
            //}
            //else
            //{
            //    if (ishavemediaids)
            //    {
            return true;
            //    }
            //    else
            //        return false;
            //}
        }

        protected void btnSecret_Click(object sender, EventArgs e)
        {
            palSecret.Visible = true;
            txtSupplierName.Enabled = true;
            SetTypeDropDownList();
            setSupplyInfo();
            GeneralInfo gmodel = GeneralInfoManager.GetModel(generalId);
            if (gmodel != null)
            {
                txtSupplierName.Text = gmodel.supplier_name;
                txtAddress.Text = gmodel.supplier_address;
                txtLinker.Text = gmodel.supplier_linkman;
                txtEmail.Text = gmodel.supplier_email;
                //if (gmodel.supplier_phone != null && gmodel.supplier_phone.Split('-').Length == 4)
                //{
                txtsupplier_con.Text = gmodel.supplier_phone;//.Split('-')[0];
                //txtsupplier_area.Text = gmodel.supplier_phone.Split('-')[1];
                //txtsupplier_phone.Text = gmodel.supplier_phone.Split('-')[2];
                //txtsupplier_ext.Text = gmodel.supplier_phone.Split('-')[3];
                //}
                //if (gmodel.supplier_fax != null && gmodel.supplier_fax.Split('-').Length == 4)
                //{
                txtsupplierfax_con.Text = gmodel.supplier_fax;//.Split('-')[0];
                //txtsupplierfax_area.Text = gmodel.supplier_fax.Split('-')[1];
                //txtsupplierfax_phone.Text = gmodel.supplier_fax.Split('-')[2];
                //txtsupplierfax_ext.Text = gmodel.supplier_fax.Split('-')[3];
                //}

                txtaccountBank.Text = gmodel.account_bank;
                txtaccountName.Text = gmodel.account_name;
                txtaccountNum.Text = gmodel.account_number;
            }


            //显示TAB2、非目录物品，隐藏Tab1、目录物品
            Tab1.Visible = false;
            palML.Visible = false;
            Tab2.Visible = true;
            palFML.Visible = true;
            palFX.Visible = true;
        }

        protected string ViewSupplierPriceLevel(object supplierPrickLevel)
        {
            string type = "暂无";
            if (supplierPrickLevel != null)
            {
                switch (supplierPrickLevel.ToString())
                {
                    case "3":
                        type = "<font color=\"red\"><b>高</b></font>";
                        break;
                    case "2":
                        type = "<font color=\"blue\"><b>中</b></font>";
                        break;
                    case "1":
                        type = "低";
                        break;
                }
            }
            return type;
        }

        protected void btnFP3_Click(object sender, EventArgs e)
        {
            ViewState["currentPageIndex3"] = 0;
            HisSupplierBind();
        }

        protected void btnPP3_Click(object sender, EventArgs e)
        {
            ViewState["currentPageIndex3"] = int.Parse(ViewState["currentPageIndex3"].ToString()) - 1;
            HisSupplierBind();
        }

        protected void btnNP3_Click(object sender, EventArgs e)
        {
            ViewState["currentPageIndex3"] = int.Parse(ViewState["currentPageIndex3"].ToString()) + 1;
            HisSupplierBind();
        }

        protected void btnLP3_Click(object sender, EventArgs e)
        {
            ViewState["currentPageIndex3"] = int.Parse(ViewState["pageCount3"].ToString()) - 1;
            HisSupplierBind();
        }



    }
}
