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

namespace PurchaseWeb.Purchase.Requisition.SupplierCollaboration
{
    public partial class OtherMediaDetailAddOrEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindType();
                BindDetailInfo();
            }
        }

        private void BindType()
        {
            DataSet ds = ManuscriptTypeManager.GetAllList();
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.ddlType.DataSource = ds.Tables[0];
                this.ddlType.DataTextField = "TypeName";
                this.ddlType.DataValueField = "ID";
                this.ddlType.DataBind();
            }
        }
        
        private void BindDetailInfo()
        {
            if (!string.IsNullOrEmpty(Request["DetailID"]))
            {
                OtherMediumInProductDetailsInfo info = OtherMediumInProductsDetailsManager.GetModel(Convert.ToInt32(Request["DetailID"]));
                if (info != null)
                {
                    this.txtArea.Text = info.Area;
                    this.txtDesc.Text = info.Description;
                    this.txtDiscount.Text = info.Discount;
                    this.txtHopePrice.Text = info.HopePrice;
                    this.txtLayout.Text = info.Layout;
                    this.txtNewsPrice.Text = info.NewsPrice;
                    this.txtShunYaDesc.Text = info.ShunYaDescription;
                    this.txtTitlePrice.Text = info.TitlePrice;
                    this.ddlType.SelectedValue = info.ManuscriptType.ToString();
                    this.txtUnit.Text = info.Unit;

                    if (info.IsHavePic)
                        this.rdoHavePicY.Checked = true;
                    else
                        this.rdoHavePicN.Checked = true; 

                }
            }
            else
            {
                this.rdoHavePicN.Checked = true; 
            }
        }

        private void Save()
        {
            OtherMediumInProductDetailsInfo info = new OtherMediumInProductDetailsInfo();
            info.CreatedDate = DateTime.Now;
            info.CreatedUserID = UserInfo.UserID;
            if (!string.IsNullOrEmpty(Request["DetailID"]))
            {
                info = OtherMediumInProductsDetailsManager.GetModel(Convert.ToInt32(Request["DetailID"]));
            }
            info.MediaProductID = Convert.ToInt32(Request["MediaProductID"]);
            info.ModifiedDate = DateTime.Now;
            info.ModifiedUserID = UserInfo.UserID;
            info.Area = this.txtArea.Text;
            info.Description = this.txtDesc.Text;
            info.Discount = this.txtDiscount.Text;
            info.HopePrice = this.txtHopePrice.Text;
            info.Layout = this.txtLayout.Text;
            info.NewsPrice = this.txtNewsPrice.Text;
            info.ShunYaDescription = this.txtShunYaDesc.Text;
            info.TitlePrice = this.txtTitlePrice.Text;
            info.ManuscriptType = Convert.ToInt32(this.ddlType.SelectedItem.Value);
            info.Unit = this.txtUnit.Text;
            info.IsHavePic = this.rdoHavePicY.Checked;

            if (!string.IsNullOrEmpty(Request["DetailID"]))
            {
                OtherMediumInProductsDetailsManager.Update(info);
            }
            else
            {
                OtherMediumInProductsDetailsManager.Add(info);
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