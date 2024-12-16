using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.Entity;
using ESP.Purchase.BusinessLogic;
using System.Data;
using System.Web.UI.HtmlControls;
using ESP.Finance.Entity;
using ESP.Purchase.Common;

namespace PurchaseWeb.Purchase.Requisition.Advertisement
{
    public partial class AdvertisementOrder : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request[ESP.Purchase.Common.RequestName.GeneralID]))
                {
                    if (!string.IsNullOrEmpty(Request["OrderID"]))
                        this.hidOrderID.Value = Request["OrderID"];
                    if (!string.IsNullOrEmpty(Request["SupplierId"]))
                        this.hidSupplierId.Value = Request["SupplierId"];
                    else
                        this.hidSupplierId.Value = "0";
                    BindPrInfo(Convert.ToInt32(Request[ESP.Purchase.Common.RequestName.GeneralID]));
                }
                BindDetails();
            }
        }

        private void BindPrInfo(int generalID)
        {
            GeneralInfo info = GeneralInfoManager.GetModel(generalID);
            if (info != null)
            {
                if (info.Project_id == 0)
                {
                    this.lblProNO.Text = info.project_code;
                    this.lblCusName.Text = "";
                }
                else
                {
                    ProjectInfo pro = ESP.Finance.BusinessLogic.ProjectManager.GetModel(info.Project_id);
                    this.lblProNO.Text = pro.ProjectCode;
                    this.lblCusName.Text = pro.Customer.FullNameCN;
                }
            }
        }

        protected void btnRefsh_Click(object sender, EventArgs e)
        {
            BindDetails();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.rptItems.Items.Count <= 0)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('请先创建明细条目！');", true);
                return;
            }
            foreach (RepeaterItem item in this.rptItems.Items)
            {
                SaveDetail(item);
            }
            OrderInfo order = OrderInfoManager.GetModel(Convert.ToInt32(this.hidOrderID.Value));
            if (order != null)
            {
                order.Item_No = "线上广告";
                order.desctiprtion = "广告投放";

                DataSet forOrder = AdvertisementForOrderManager.GetList(" OrderID=" + this.hidOrderID.Value);
                order.price = 0;
                if (forOrder.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in forOrder.Tables[0].Rows)
                    {
                        order.price += Convert.ToDecimal(dr["Total"]);
                    }
                }
                order.quantity = 1;
                order.total = order.price;
                order.intend_receipt_date = this.txtStartDate.Text;
                order.upfile = "Purchase/Requisition/Print/AdvertisementPrint2.aspx?OrderID=" + order.id.ToString();

                IList<AdvertisementInfo> adList = AdvertisementManager.GetInfoList("");
                if (adList != null && adList.Count > 0)
                    order.producttype = adList[0].ProductTypeID;
                OrderInfoManager.Update(order, UserInfo.UserID, UserInfo.Username);
            }
            if (string.IsNullOrEmpty(Request["pageUrl"]) || Request["pageUrl"].Trim().ToLower() != "majordomoaudit.aspx")
                ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('保存成功！');window.location.href='../AddRequisitionStep6.aspx?GeneralID=" + Request[RequestName.GeneralID] + "'", true);
            else
                ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('保存成功！');window.location.href='../MajordomoAudit.aspx?backUrl='AuditRequistion.aspx'&" + RequestName.GeneralID + "=" + Request[RequestName.GeneralID] + "'", true);
        }

        private void SaveDetail(RepeaterItem item)
        {
            AdvertisementForOrderInfo info = new AdvertisementForOrderInfo();


            HiddenField hidID = (HiddenField)item.FindControl("hidID");
            if (!string.IsNullOrEmpty(hidID.Value))
            {
                info = AdvertisementForOrderManager.GetModel(Convert.ToInt32(hidID.Value));
            }
            Label lblMediaName = (Label)item.FindControl("lblMediaName");
            Label lblType = (Label)item.FindControl("lblType");
            TextBox txtExemplar = (TextBox)item.FindControl("txtExemplar");
            TextBox txtPriceTotal = (TextBox)item.FindControl("txtPriceTotal");
            TextBox txtDiscount = (TextBox)item.FindControl("txtDiscount");
            Label txtTotal = (Label)item.FindControl("txtTotal");
            TextBox txtDistributionPercent = (TextBox)item.FindControl("txtDistributionPercent");
            Label txtDistributionPrice = (Label)item.FindControl("txtDistributionPrice");
            TextBox txtReturnPoint = (TextBox)item.FindControl("txtReturnPoint");
            TextBox txtAccountPeriod = (TextBox)item.FindControl("txtAccountPeriod");


            HiddenField hidAdvertisementDetailsID = (HiddenField)item.FindControl("hidAdvertisementDetailsID");
            info.MediaName = lblMediaName.Text;
            info.OrderID = Convert.ToInt32(this.hidOrderID.Value);
            info.MediaType = lblType.Text;

            info.AdvertisementDetailsID = Convert.ToInt32(hidAdvertisementDetailsID.Value);
            info.AdvertisementExemplar = txtExemplar.Text;

            decimal PriceTotal = 0;
            info.PriceTotal = PriceTotal;
            if (txtPriceTotal.Text != string.Empty && decimal.TryParse(txtPriceTotal.Text.Replace(",", ""), out PriceTotal))
                info.PriceTotal = Convert.ToDecimal(txtPriceTotal.Text.Replace(",", ""));

            info.Discount = 100-Convert.ToDecimal(txtDiscount.Text);
            info.Total = Convert.ToDecimal(txtTotal.Text.Replace(",", ""));

            decimal DPercent = 0;
            if (txtDistributionPercent.Text != string.Empty && decimal.TryParse(txtDistributionPercent.Text, out DPercent))
                info.DistributionPercent = Convert.ToDecimal(txtDistributionPercent.Text);

            decimal DPrice = 0;
            if (txtDistributionPrice.Text != string.Empty && decimal.TryParse(txtDistributionPrice.Text.Replace(",", ""), out DPrice))
                info.DistributionPrice = Convert.ToDecimal(txtDistributionPrice.Text.Replace(",", ""));

            decimal ReturnPoint = 0;
            if (txtReturnPoint.Text != string.Empty && decimal.TryParse(txtReturnPoint.Text, out ReturnPoint))
                info.ReturnPoint = Convert.ToDecimal(txtReturnPoint.Text);
            info.AccountPeriod = txtAccountPeriod.Text;
            info.IsDel = false;
            //info.CustomerName = ;



            info.CreatedDate = DateTime.Now;
            info.CreatedUserID = UserInfo.UserID;
            info.ModifiedDate = DateTime.Now;
            info.ModifiedUserID = UserInfo.UserID;

            if (!string.IsNullOrEmpty(hidID.Value))
            {
                info.ID = Convert.ToInt32(hidID.Value);
                //DataSet ds = OtherMediumForOrderManager.GetList("OrderID="+info.ID);
                //    decimal price = 0;
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    foreach (DataRow dr in ds.Tables[0].Rows)
                //    {
                //        price += Convert.ToDecimal(dr["Price"] == null ? "0" : dr["Price"]) * Convert.ToDecimal(dr["Amount"] == null ? "1" : dr["Amount"]);
                //    }
                //}
                AdvertisementForOrderManager.Update(info);
            }

        }

        protected void rptItems_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "deleteItem")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                AdvertisementForOrderManager.Delete(id);

                //foreach (RepeaterItem item in this.rptItems.Items)
                //{
                //    SaveDetail(item);
                //}

                BindDetails();
                return;
            }
            if (e.CommandName == "Show")
            {
                HtmlGenericControl divDetails = (HtmlGenericControl)e.Item.FindControl("divDetails");
                HiddenField hidIsShow = (HiddenField)e.Item.FindControl("hidIsShow");
                if (divDetails != null)
                {
                    if (hidIsShow.Value == "false")
                    {
                        divDetails.Visible = true;
                        hidIsShow.Value = "true";
                    }
                    else
                    {
                        divDetails.Visible = false;
                        hidIsShow.Value = "false";
                    }
                }
            }
        }

        private void BindDetails()
        {

            if (!string.IsNullOrEmpty(this.hidOrderID.Value))
            {
                OrderInfo order = OrderInfoManager.GetModel(Convert.ToInt32(this.hidOrderID.Value));
                if (order != null)
                    this.txtStartDate.Text = order.intend_receipt_date;
                DataSet det = AdvertisementForOrderManager.GetList(" OrderID=" + this.hidOrderID.Value);

                this.rptItems.DataSource = det.Tables[0];
            }
            else
                this.rptItems.DataSource = null;
            this.rptItems.DataBind();
        }

        protected void rptItems_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;
                TextBox txtDiscount = (TextBox)e.Item.FindControl("txtDiscount");
                if (txtDiscount != null)
                {
                    txtDiscount.Text = (100 - Convert.ToDecimal(dr["Discount"])).ToString();
                }
                //Label lblType = (Label)e.Item.FindControl("lblType");
                //HiddenField hidAdvertisementDetailsID = (HiddenField)e.Item.FindControl("hidAdvertisementDetailsID");
                //if (hidAdvertisementDetailsID != null && lblType != null)
                //{
                //    ;
                //}
            }
        }

        protected void txtPriceTotal_TextChanged(object sender, EventArgs e)
        {
            RepeaterItem rw = (RepeaterItem)((TextBox)sender).Parent;

            TextBox txtReturnPoint = (TextBox)rw.FindControl("txtReturnPoint");

            TextBox txtPriceTotal = (TextBox)rw.FindControl("txtPriceTotal");
            TextBox txtDiscount = (TextBox)rw.FindControl("txtDiscount");
            Label txtTotal = (Label)rw.FindControl("txtTotal");
            TextBox txtDistributionPercent = (TextBox)rw.FindControl("txtDistributionPercent");
            Label txtDistributionPrice = (Label)rw.FindControl("txtDistributionPrice");
            if (txtPriceTotal != null)
            {
                if (txtPriceTotal.Text != string.Empty && txtDiscount.Text != string.Empty)
                {
                    txtTotal.Text = (Convert.ToDecimal(txtPriceTotal.Text) * (100 - Convert.ToDecimal(txtDiscount.Text)) * (100 - Convert.ToDecimal(txtReturnPoint.Text)) / 10000).ToString("#,##0.00");
                    txtDistributionPrice.Text = (Convert.ToDecimal(txtPriceTotal.Text) * (100 - Convert.ToDecimal(txtDiscount.Text)) * Convert.ToDecimal(txtDistributionPercent.Text) / 10000).ToString("#,##0.00");
                }
            }
        }
        protected void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            RepeaterItem rw = (RepeaterItem)((TextBox)sender).Parent;

            TextBox txtDiscount = (TextBox)rw.FindControl("txtDiscount");

            TextBox txtReturnPoint = (TextBox)rw.FindControl("txtReturnPoint");

            Label lblDiscount = (Label)rw.FindControl("lblDiscount");
            HiddenField hidAdvertisementDetailsID = (HiddenField)rw.FindControl("hidAdvertisementDetailsID");
            if (txtDiscount != null)
            {
                AdvertisementDetailsInfo info = AdvertisementDetailsManager.GetModel(Convert.ToInt32(hidAdvertisementDetailsID.Value));
                if (info != null && info.Discount.ToString("0.00") != txtDiscount.Text)
                {
                    lblDiscount.Text = "原折扣" + (100-info.Discount).ToString() + "% Off,当前" + (Convert.ToDecimal(txtDiscount.Text)).ToString() + "% Off";
                    lblDiscount.Visible = true;
                }
                else
                    lblDiscount.Visible = false;
            }

            TextBox txtPriceTotal = (TextBox)rw.FindControl("txtPriceTotal");
            Label txtTotal = (Label)rw.FindControl("txtTotal");
            TextBox txtDistributionPercent = (TextBox)rw.FindControl("txtDistributionPercent");
            Label txtDistributionPrice = (Label)rw.FindControl("txtDistributionPrice");
            if (txtPriceTotal != null)
            {
                if (txtPriceTotal.Text != string.Empty && txtDiscount.Text != string.Empty)
                {
                    txtTotal.Text = (Convert.ToDecimal(txtPriceTotal.Text) * (100 - Convert.ToDecimal(txtDiscount.Text)) * (100 - Convert.ToDecimal(txtReturnPoint.Text)) / 10000).ToString("#,##0.00");
                    txtDistributionPrice.Text = (Convert.ToDecimal(txtPriceTotal.Text) * (100 - Convert.ToDecimal(txtDiscount.Text)) * Convert.ToDecimal(txtDistributionPercent.Text) / 10000).ToString("#,##0.00");
                }
            }
        }

        protected void txtDistributionPercent_TextChanged(object sender, EventArgs e)
        {
            RepeaterItem rw = (RepeaterItem)((TextBox)sender).Parent.Parent;

            TextBox txtDistributionPercent = (TextBox)rw.FindControl("txtDistributionPercent");
            Label lblDistributionPercent = (Label)rw.FindControl("lblDistributionPercent");
            HiddenField hidAdvertisementDetailsID = (HiddenField)rw.FindControl("hidAdvertisementDetailsID");
            Label txtTotal = (Label)rw.FindControl("txtTotal");
            if (txtDistributionPercent != null)
            {
                AdvertisementDetailsInfo info = AdvertisementDetailsManager.GetModel(Convert.ToInt32(hidAdvertisementDetailsID.Value));
                if (info != null && info.DistributionPercent.ToString("0.00") != txtDistributionPercent.Text)
                {
                    lblDistributionPercent.Text = "原有配送百分比为 " + info.DistributionPercent.ToString() + "%";
                    lblDistributionPercent.Visible = true;
                }
                else
                    lblDistributionPercent.Visible = false;

                TextBox txtPriceTotal = (TextBox)rw.FindControl("txtPriceTotal");
                TextBox txtDiscount = (TextBox)rw.FindControl("txtDiscount");
                Label txtDistributionPrice = (Label)rw.FindControl("txtDistributionPrice");
                decimal to = 0;
                if (txtPriceTotal != null && decimal.TryParse(txtPriceTotal.Text, out to))
                {
                    txtDistributionPrice.Text = (Convert.ToDecimal(txtPriceTotal.Text) * (100 - Convert.ToDecimal(txtDiscount.Text)) * Convert.ToDecimal(txtDistributionPercent.Text) / 10000).ToString("#,##0.00");
                }
            }
        }

        protected void txtReturnPoint_TextChanged(object sender, EventArgs e)
        {
            RepeaterItem rw = (RepeaterItem)((TextBox)sender).Parent.Parent;

            TextBox txtDiscount = (TextBox)rw.FindControl("txtDiscount");

            TextBox txtReturnPoint = (TextBox)rw.FindControl("txtReturnPoint");
            Label lblReturnPoint = (Label)rw.FindControl("lblReturnPoint");
            HiddenField hidAdvertisementDetailsID = (HiddenField)rw.FindControl("hidAdvertisementDetailsID");
            if (txtReturnPoint != null)
            {
                AdvertisementDetailsInfo info = AdvertisementDetailsManager.GetModel(Convert.ToInt32(hidAdvertisementDetailsID.Value));
                if (info != null && info.ReturnPoint.ToString("0.00") != txtReturnPoint.Text)
                {
                    lblReturnPoint.Text = "原返点为 " + info.ReturnPoint.ToString() + "%";
                    lblReturnPoint.Visible = true;
                }
                else
                    lblReturnPoint.Visible = false;
            }


            TextBox txtPriceTotal = (TextBox)rw.FindControl("txtPriceTotal");
            Label txtTotal = (Label)rw.FindControl("txtTotal");
            TextBox txtDistributionPercent = (TextBox)rw.FindControl("txtDistributionPercent");
            Label txtDistributionPrice = (Label)rw.FindControl("txtDistributionPrice");
            if (txtPriceTotal != null)
            {
                if (txtPriceTotal.Text != string.Empty && txtDiscount.Text != string.Empty)
                {
                    txtTotal.Text = (Convert.ToDecimal(txtPriceTotal.Text) * (100 - Convert.ToDecimal(txtDiscount.Text)) * (100 - Convert.ToDecimal(txtReturnPoint.Text)) / 10000).ToString("#,##0.00");
                    txtDistributionPrice.Text = (Convert.ToDecimal(txtPriceTotal.Text) * (100 - Convert.ToDecimal(txtDiscount.Text)) * Convert.ToDecimal(txtDistributionPercent.Text) / 10000).ToString("#,##0.00");
                }
            }
        }

        protected void txtAccountPeriod_TextChanged(object sender, EventArgs e)
        {
            RepeaterItem rw = (RepeaterItem)((TextBox)sender).Parent.Parent;

            TextBox txtAccountPeriod = (TextBox)rw.FindControl("txtAccountPeriod");
            Label lblAccountPeriod = (Label)rw.FindControl("lblAccountPeriod");
            HiddenField hidAdvertisementDetailsID = (HiddenField)rw.FindControl("hidAdvertisementDetailsID");
            if (txtAccountPeriod != null)
            {
                AdvertisementDetailsInfo info = AdvertisementDetailsManager.GetModel(Convert.ToInt32(hidAdvertisementDetailsID.Value));
                if (info != null && info.AccountPeriod != txtAccountPeriod.Text)
                {
                    lblAccountPeriod.Text = "原有账期为单月 " + info.AccountPeriod.ToString() + "天";
                    lblAccountPeriod.Visible = true;
                }
                else
                    lblAccountPeriod.Visible = false;
            }
        }
    }
}