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

namespace PurchaseWeb.Purchase.Requisition.Advertisement
{
    public partial class AdvertisementDetailAddOrEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDetailInfo();
            }
        }
        
        private void BindDetailInfo()
        {
            if (!string.IsNullOrEmpty(Request["DetailID"]))
            {
                AdvertisementDetailsInfo info = AdvertisementDetailsManager.GetModel(Convert.ToInt32(Request["DetailID"]));
                if (info != null)
                {
                    this.txtReturn.Text = info.ReturnPoint.ToString();
                    this.txtPeriod.Text = info.AccountPeriod;
                    this.txtDesc.Text = info.DiscountDescription;
                    this.txtDiscount.Text = info.Discount.ToString();

                    this.txtMin.Text = info.DistributionMin.ToString();
                    this.txtMax.Text = info.DistributionMax.ToString();
                    this.txtPercent.Text = info.DistributionPercent.ToString();
                    this.txtDistributionDescription.Text = info.DistributionDescription.ToString();

                }
            }
        }

        private void Save()
        {
            AdvertisementDetailsInfo info = new AdvertisementDetailsInfo();
            info.CreatedDate = DateTime.Now;
            info.CreatedUserID = UserInfo.UserID;
            if (!string.IsNullOrEmpty(Request["DetailID"]))
            {
                info = AdvertisementDetailsManager.GetModel(Convert.ToInt32(Request["DetailID"]));
            }
            info.AdvertisementID = Convert.ToInt32(Request["AdvertisementID"]);
            info.ModifiedDate = DateTime.Now;
            info.ModifiedUserID = UserInfo.UserID;

            info.ReturnPoint = Convert.ToDecimal(this.txtReturn.Text);
            info.AccountPeriod = this.txtPeriod.Text;
            info.DiscountDescription = this.txtDesc.Text;
            info.Discount = Convert.ToDecimal(this.txtDiscount.Text);
            info.DistributionDescription = this.txtDistributionDescription.Text;

            info.DistributionMin = Convert.ToDecimal(this.txtMin.Text);
            info.DistributionMax = Convert.ToDecimal(this.txtMax.Text);
            info.DistributionPercent = Convert.ToDecimal(this.txtPercent.Text);

            if (!string.IsNullOrEmpty(Request["DetailID"]))
            {
                AdvertisementDetailsManager.Update(info);
            }
            else
            {
                AdvertisementDetailsManager.Add(info);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save();
            string clientId1 = "ctl00$ContentPlaceHolder1$";
            Response.Write("<script>opener.__doPostBack('" + clientId1 + "btnRefsh','');</script>");
            Response.Write(@"<script>window.close();</script>");
        }
    }
}