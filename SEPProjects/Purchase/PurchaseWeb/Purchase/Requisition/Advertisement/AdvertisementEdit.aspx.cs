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
    public partial class AdvertisementEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindType();
                if (!string.IsNullOrEmpty(Request["AdvertisementID"]))
                {
                    BindDetails();
                }
            }
        }

        private void BindType()
        {
            DataSet ds = AdvertisementTypeManager.GetAllList();
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.dllType.DataSource = ds.Tables[0];
                this.dllType.DataTextField = "TypeName";
                this.dllType.DataValueField = "ID";
                this.dllType.DataBind();
            }
        }

        protected void btnRefsh_Click(object sender, EventArgs e)
        {
            BindDetails();
        }

        private void BindDetails()
        {
            AdvertisementInfo info = AdvertisementManager.GetModel(Convert.ToInt32(Request["AdvertisementID"]));
            this.txtMediaName.Text = info.MediaName;
            this.dllType.SelectedValue = info.MediaTypeID.ToString();
            this.txtThirdParty.Text = TypeManager.GetModel(info.ProductTypeID).typename;
            hidtypeIds.Value = info.ProductTypeID.ToString();

            DataSet ds = AdvertisementDetailsManager.GetList("AdvertisementID=" + Request["AdvertisementID"]);
            this.rptItems.DataSource = ds.Tables[0];
            this.rptItems.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AdvertisementInfo info = new AdvertisementInfo();
            info.CreatedDate = DateTime.Now;
            info.CreatedUserID = UserInfo.UserID;
            if (!string.IsNullOrEmpty(Request["AdvertisementID"]))
            {
                info = AdvertisementManager.GetModel(Convert.ToInt32(Request["AdvertisementID"]));
            }
            info.MediaName = this.txtMediaName.Text;
            info.ModifiedUserID = UserInfo.UserID;
            info.ModifiedDate = DateTime.Now;

            info.MediaTypeID = Convert.ToInt32(this.dllType.SelectedItem.Value);
            //待填
            info.ProductTypeID = Convert.ToInt32(this.hidtypeIds.Value);

            if (!string.IsNullOrEmpty(Request["AdvertisementID"]))
            {
                AdvertisementManager.Update(info);
            }
            else
            {
                AdvertisementManager.Add(info);
            }
            ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('保存成功！');window.location.href='AdvertisementList.aspx'", true);

        }

        protected void rptItems_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "deleteItem")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                AdvertisementDetailsManager.Delete(id);
                BindDetails();
                return;
            }
        }

        protected void rptItems_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            //{
            //    Label txtManuscriptType = (Label)e.Item.FindControl("txtManuscriptType");
            //    if (txtManuscriptType != null && txtManuscriptType.Text != string.Empty)
            //    {
            //        AdvertisementTypeInfo info = AdvertisementTypeManager.GetModel(Convert.ToInt32(txtManuscriptType.Text));
            //        if (info != null)
            //        {
            //            txtManuscriptType.Text = info.TypeName;
            //        }
            //    }
            //}
        }
    }
}