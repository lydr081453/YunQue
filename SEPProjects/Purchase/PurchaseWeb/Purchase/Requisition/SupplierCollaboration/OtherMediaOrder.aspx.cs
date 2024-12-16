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

namespace PurchaseWeb.Purchase.Requisition.SupplierCollaboration
{
    public partial class OtherMediaOrder : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request[ESP.Purchase.Common.RequestName.GeneralID]))
                {
                    if (!string.IsNullOrEmpty(Request["OrderID"]))
                        this.hidOrderID.Value = Request["OrderID"];
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
                ProjectInfo pro = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(info.Project_id);
                this.lblProNO.Text = pro.ProjectCode;

                this.lblCusName.Text = pro.Customer.FullNameCN;
            }
        }

        protected void btnRefsh_Click(object sender, EventArgs e)
        {
            BindDetails();
        }

        private decimal total = 0;
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
                order.price = total;
                order.total = total;
                order.quantity = 1;
                order.Item_No = "第三方媒体采购";
                IList<OtherMediumInProductInfo> adList = OtherMediumInProductsManager.GetInfoList("");
                if (adList != null && adList.Count > 0)
                    order.producttype = adList[0].ProductID;
                OrderInfoManager.Update(order, UserInfo.UserID, UserInfo.Username);
            }
            ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('保存成功！');window.location.href='../AddRequisitionStep6.aspx?GeneralID="+Request[RequestName.GeneralID]+"'", true);
        }

        private void SaveDetail(RepeaterItem item)
        { 
            OtherMediumForOrderInfo info = new OtherMediumForOrderInfo();

            
            HiddenField hidID = (HiddenField)item.FindControl("hidID");
            if (!string.IsNullOrEmpty(hidID.Value))
            {
                info = OtherMediumForOrderManager.GetModel(Convert.ToInt32(hidID.Value));
            }
            Label lblMediaName = (Label)item.FindControl("lblMediaName");
            TextBox txtTitle = (TextBox)item.FindControl("txtTitle");
            TextBox txtOfSpace = (TextBox)item.FindControl("txtOfSpace");
            TextBox txtStartDate = (TextBox)item.FindControl("txtStartDate");
            TextBox txtWordCount = (TextBox)item.FindControl("txtWordCount");
            TextBox txtPicSize = (TextBox)item.FindControl("txtPicSize");
            TextBox txtSpaceSize = (TextBox)item.FindControl("txtSpaceSize");
            RadioButton rdoYes = (RadioButton)item.FindControl("rdoYes");
            TextBox txtColor = (TextBox)item.FindControl("txtColor");
            TextBox txtPrice = (TextBox)item.FindControl("txtPrice");
            TextBox txtUnit = (TextBox)item.FindControl("txtUnit");
            TextBox txtAmount = (TextBox)item.FindControl("txtAmount");
            TextBox txtDiscount = (TextBox)item.FindControl("txtDiscount");
            TextBox txtOtherFee = (TextBox)item.FindControl("txtOtherFee");
            TextBox txtDescription = (TextBox)item.FindControl("txtDescription");

            HiddenField hidMediaID = (HiddenField)item.FindControl("hidMediaID");
            info.MediaName = lblMediaName.Text;
            info.OrderID = Convert.ToInt32(this.hidOrderID.Value);
            info.MediaID = Convert.ToInt32(hidMediaID.Value);
            //info.CustomerName = ;
            info.Title = txtTitle.Text;
            info.OfSpace = txtOfSpace.Text;
            info.StartDate = Convert.ToDateTime(txtStartDate.Text);

            decimal WordsCount = 0;
            info.WordsCount = WordsCount;
            if (txtWordCount.Text != string.Empty && decimal.TryParse(txtWordCount.Text, out WordsCount))
            {
                info.WordsCount = Convert.ToDecimal(txtWordCount.Text);
            }

            info.PicSize = txtPicSize.Text;
            info.LayoutSize = txtSpaceSize.Text;
            info.Color = txtColor.Text;
            info.Price = txtPrice.Text;
            info.Unit = txtUnit.Text;
            //info.Amount = txtAmount.Text;

            decimal discount =100;
            info.Discount = discount.ToString();
            if (txtDiscount.Text != string.Empty && decimal.TryParse(txtDiscount.Text, out discount))
            { 
                info.Discount = txtDiscount.Text;
            }
            info.CreatedDate = DateTime.Now;
            info.CreatedUserID = UserInfo.UserID;
            info.ModifiedDate = DateTime.Now;
            info.ModifiedUserID = UserInfo.UserID;
            info.OtherFees = txtOtherFee.Text;
            info.IsAccessories = rdoYes.Checked;

            info.Description = txtDescription.Text;

            total += Convert.ToDecimal(info.WordsCount) * Convert.ToDecimal(info.Price) * Convert.ToDecimal(info.Discount) / 100;
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
                OtherMediumForOrderManager.Update(info);
            }
            else
            {
                OtherMediumForOrderManager.Add(info);
            }

        }

        protected void rptItems_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "deleteItem")
            { 
                int id = Convert.ToInt32(e.CommandArgument);
                OtherMediumForOrderManager.Delete(id);
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
                DataSet det = OtherMediumForOrderManager.GetList(" OrderID=" + this.hidOrderID.Value);

                this.rptItems.DataSource = det.Tables[0];
            }
            else
                this.rptItems.DataSource = null;
            this.rptItems.DataBind();
        }

        protected void rptItems_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hidYN = (HiddenField)e.Item.FindControl("hidYN");
                RadioButton rdoYes = (RadioButton)e.Item.FindControl("rdoYes");
                RadioButton rdoNo = (RadioButton)e.Item.FindControl("rdoNo");

                if (Convert.ToBoolean(hidYN.Value))
                {
                    rdoYes.Checked = true;
                }
                else
                    rdoNo.Checked = true;

                TextBox txtDiscount = (TextBox)e.Item.FindControl("txtDiscount");
                if (txtDiscount.Text != string.Empty)
                    txtDiscount.Text = "100.00";

            }
        }

    }
}