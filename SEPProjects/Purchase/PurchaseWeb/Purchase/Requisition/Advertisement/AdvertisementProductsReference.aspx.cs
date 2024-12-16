using System;
using System.Linq;
using System.Collections;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using System.Data;
using System.Web.UI;

namespace PurchaseWeb.Purchase.Requisition.Advertisement
{
    public partial class AdvertisementProductsReference : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindType();
                ListBind();
            }
        }

        private void BindType()
        {
            DataSet ds = AdvertisementTypeManager.GetList(" IsDel='False'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.dllType.Items.Add(new ListItem("--全部--","-1"));
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    this.dllType.Items.Add(new ListItem(dr["TypeName"].ToString(), dr["ID"].ToString()));
                }

            }
        }

        /// <summary>
        /// 绑定列表
        /// </summary>
        private void ListBind()
        {
            string strType = string.Empty;
            if (this.dllType.SelectedItem.Value != "-1")
            {
                strType = " And MediaTypeID=" + this.dllType.SelectedItem.Value;
            }
            DataSet dsMedia = AdvertisementManager.GetList(" IsDel='0' AND MediaName Like('%" + this.txtName.Text.Trim() + "%') " + strType , "MediaName");
            if (dsMedia.Tables[0].Rows.Count > 0)
                this.gvG.DataSource = dsMedia.Tables[0];
            else
                this.gvG.DataSource = null;
            this.gvG.DataBind();
            //string[] ids = new string[dsMedia.Tables[0].Rows.Count];
            //if (dsMedia.Tables[0].Rows.Count > 0)
            //{
            //    for (int i = 0; i < dsMedia.Tables[0].Rows.Count; i++)
            //    {
            //        ids[i] = dsMedia.Tables[0].Rows[i]["ID"].ToString();
            //    }
            //}
            //else
            //{
            //    this.gvG.DataSource = null;
            //    this.gvG.DataBind();
            //    return;
            //}


            //List<SqlParameter> parms = new List<SqlParameter>();
            //string term = string.Empty;
            //term = " 1=1 ";

            //if (ids.Length > 0)
            //{
            //    term += " AND AdvertisementID in(";
            //    for (int j = 0; j < ids.Length; j++)
            //    {
            //        term += ids[j];
            //        if (j != ids.Length - 1)
            //        {
            //            term += ",";
            //        }
            //    }
            //    term += ")";
            //}

            //DataSet list = AdvertisementDetailsManager.GetList(term);
            //gvG.DataSource = list.Tables[0];
            //gvG.DataBind();

            //if (gvG.PageCount > 1)
            //{
            //    PageBottom.Visible = true;
            //    PageTop.Visible = true;
            //}
            //else
            //{
            //    PageBottom.Visible = false;
            //    PageTop.Visible = false;
            //}
            //if (list.Tables[0].Rows.Count > 0)
            //{
            //    tabTop.Visible = true;
            //    tabBottom.Visible = true;
            //}
            //else
            //{
            //    tabTop.Visible = false;
            //    tabBottom.Visible = false;
            //}

            //labAllNum.Text = labAllNumT.Text = list.Tables[0].Rows.Count.ToString();
            //labPageCount.Text = labPageCountT.Text = (gvG.PageIndex + 1).ToString() + "/" + gvG.PageCount.ToString();
            //if (gvG.PageCount > 0)
            //{
            //    if (gvG.PageIndex + 1 == gvG.PageCount)
            //        disButton("last");
            //    else if (gvG.PageIndex == 0)
            //        disButton("first");
            //    else
            //        disButton("");
            //}

        }

        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Show")
            { 
                GridView gvDetails = (GridView)((Control)e.CommandSource).Parent.FindControl("gvDetails");
                LinkButton lkShow = (LinkButton)((Control)e.CommandSource).Parent.FindControl("lkShow");
                HiddenField hidIsShow = (HiddenField)((Control)e.CommandSource).Parent.FindControl("hidIsShow");
                if (gvDetails != null)
                {
                    if (hidIsShow.Value == "false")
                    {
                        gvDetails.Visible = true;
                        hidIsShow.Value = "true";
                        lkShow.Text = "显示/隐藏";
                    }
                    else
                    {
                        gvDetails.Visible = false;
                        hidIsShow.Value = "false";
                        lkShow.Text = "显示/隐藏";
                    }
                }
            }
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hidMediaID = (HiddenField)e.Row.FindControl("hidID");
                //if (hidMediaID != null && !string.IsNullOrEmpty(hidMediaID.Value))
                //{
                //    AdvertisementInfo info = AdvertisementManager.GetModel(Convert.ToInt32(hidMediaID.Value));
                //    if (info != null)
                //    {
                //        lblMediaName.Text = info.MediaName;
                //    }
                //}

                Label lblMediaType = (Label)e.Row.FindControl("lblMediaType");
                AdvertisementTypeInfo type = AdvertisementTypeManager.GetModel(Convert.ToInt32(lblMediaType.Text));
                if (type != null)
                    lblMediaType.Text = type.TypeName;

                GridView gvDetails = (GridView)e.Row.FindControl("gvDetails");
                if (gvDetails != null)
                {
                    DataSet dsDetail = AdvertisementDetailsManager.GetList(" AdvertisementID=" + hidMediaID.Value);
                    if (dsDetail.Tables[0].Rows.Count > 0)
                        gvDetails.DataSource = dsDetail.Tables[0];
                    else
                        gvDetails.DataSource = null;
                    gvDetails.DataBind();
                }
            }
        }

        private void SaveOrderDetail(int orderID, int detailID)
        {
            AdvertisementForOrderInfo info = new AdvertisementForOrderInfo();
            info.OrderID = orderID;

            AdvertisementDetailsInfo detailInfo = AdvertisementDetailsManager.GetModel(detailID);

            AdvertisementInfo proInfo = AdvertisementManager.GetModel(detailInfo.AdvertisementID);

            info.MediaName = proInfo.MediaName;
            info.CreatedUserID = UserInfo.UserID;
            info.ModifiedUserID = UserInfo.UserID;
            info.AdvertisementDetailsID = detailInfo.ID;
            info.AdvertisementID = proInfo.ID;
            info.Discount = detailInfo.Discount;
            info.DistributionPercent = detailInfo.DistributionPercent;
            info.ReturnPoint = detailInfo.ReturnPoint;
            info.AccountPeriod = detailInfo.AccountPeriod;
            info.MediaType = AdvertisementTypeManager.GetModel(proInfo.MediaTypeID).TypeName;

            //待完善


            AdvertisementForOrderManager.Add(info);
        }

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            ListBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListBind();
        }

        #region 分页
        protected void btnLast_Click(object sender, EventArgs e)
        {
            Paging(gvG.PageCount);
        }
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            Paging(0);
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            Paging((gvG.PageIndex + 1) > gvG.PageCount ? gvG.PageCount : (gvG.PageIndex + 1));
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Paging((gvG.PageIndex - 1) < 0 ? 0 : (gvG.PageIndex - 1));
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        private void Paging(int pageIndex)
        {
            GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
            gvG_PageIndexChanging(new object(), e);
        }

        private void disButton(string page)
        {
            switch (page)
            {
                case "first":
                    btnFirst.Enabled = false;
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                    btnFirst2.Enabled = false;
                    btnPrevious2.Enabled = false;
                    btnNext2.Enabled = true;
                    btnLast2.Enabled = true;
                    break;
                case "last":
                    btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;

                    btnFirst2.Enabled = true;
                    btnPrevious2.Enabled = true;
                    btnNext2.Enabled = false;
                    btnLast2.Enabled = false;
                    break;
                default:
                    btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                    btnFirst2.Enabled = true;
                    btnPrevious2.Enabled = true;
                    btnNext2.Enabled = true;
                    btnLast2.Enabled = true;
                    break;
            }
        }

        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int orderID = 0;
            int generalID = Convert.ToInt32(Request[RequestName.GeneralID]);
            if (!string.IsNullOrEmpty(Request["OrderID"]) && Request["OrderID"] != "0")
            {
                orderID = Convert.ToInt32(Request["OrderID"]);
            }
            else
            {
                int supplierId = 0;
                ESP.Purchase.Entity.SupplierInfo supplierModel = null;
                if (!string.IsNullOrEmpty(Request["SupplierId"]) && Request["SupplierId"].Trim()!="0")
                {
                    supplierId = Convert.ToInt32(Request["SupplierId"]);
                    supplierModel = ESP.Purchase.BusinessLogic.SupplierManager.GetModel(supplierId);
                }
                GeneralInfo gen = GeneralInfoManager.GetModel(Convert.ToInt32(Request[RequestName.GeneralID]));

                OrderInfo order = new OrderInfo();
                if (supplierModel != null)
                {
                    order.supplierId = supplierModel.id;
                    order.supplierName = supplierModel.supplier_name;

                    gen.supplier_name = supplierModel.supplier_name;
                    gen.supplier_phone = supplierModel.contact_tel;
                    gen.supplier_address = supplierModel.contact_address;
                    gen.Supplier_cellphone = supplierModel.contact_mobile;
                    gen.supplier_email = supplierModel.contact_email;
                    gen.supplier_fax = supplierModel.contact_fax;
                    gen.supplier_linkman = supplierModel.contact_name;
                    gen.account_bank = supplierModel.account_bank;
                    gen.account_name = supplierModel.account_name;
                    gen.account_number = supplierModel.account_number;
                    gen.source = supplierModel.supplier_source;
                    gen.fa_no = supplierModel.supplier_frameNO;
                }
                else
                {
                    order.supplierId = 0;
                    order.supplierName = gen.supplier_name;
                }
                order.general_id = Convert.ToInt32(Request[RequestName.GeneralID]);
                order.producttype = Convert.ToInt32(Request[RequestName.ProductType]);
                
                orderID = OrderInfoManager.Add(order, UserInfo.UserID, UserInfo.Username);//还需要更多信息

                IList<AdvertisementInfo> adList = AdvertisementManager.GetInfoList("");
                if(adList != null && adList.Count > 0)
                    order.producttype = adList[0].ProductTypeID;
                if (gen != null)
                {
                    gen.PRType = (int)PRTYpe.PR_OtherAdvertisement;
                    GeneralInfoManager.Update(gen);
                }
            }
            foreach (GridViewRow dr in this.gvG.Rows)
            {
                GridView gvDetails = (GridView)dr.FindControl("gvDetails");
                foreach (GridViewRow drDetail in gvDetails.Rows)
                {
                    CheckBox ckSel = (CheckBox)drDetail.FindControl("ckSel");
                    if (ckSel != null && ckSel.Checked)
                    {
                        HiddenField hidDetailID = (HiddenField)drDetail.FindControl("hidDetailID");
                        if (hidDetailID != null && !string.IsNullOrEmpty(hidDetailID.Value))
                        {
                            SaveOrderDetail(orderID, Convert.ToInt32(hidDetailID.Value));

                        }
                    }
                }
            }

            string clientId = "ctl00_ContentPlaceHolder1_";
            string clientId1 = "ctl00$ContentPlaceHolder1$";
            Response.Write("<script>opener.document.all." + clientId + "hidOrderID.value= '" + orderID + "'</script>");
            Response.Write("<script>opener.__doPostBack('" + clientId1 + "btnRefsh','');</script>");
            Response.Write(@"<script>window.close();</script>");
        }
    }
}