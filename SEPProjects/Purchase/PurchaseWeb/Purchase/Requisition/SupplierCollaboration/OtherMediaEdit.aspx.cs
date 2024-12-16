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
    public partial class OtherMediaEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["MediaProductID"]))
                {
                    BindDetails();
                }
            }
        }

        protected void btnRefsh_Click(object sender, EventArgs e)
        {
            BindDetails();
        }

        private void BindDetails()
        {
            OtherMediumInProductInfo info = OtherMediumInProductsManager.GetModel(Convert.ToInt32(Request["MediaProductID"]));
            this.txtMediaName.Text = info.MediaName;
            this.txtThirdParty.Text = TypeManager.GetModel(info.ProductID).typename;
            this.hidtypeIds.Value = info.ProductID.ToString();
            DataSet ds = OtherMediumInProductsDetailsManager.GetList("MediaProductID="+Request["MediaProductID"]);
            this.rptItems.DataSource = ds.Tables[0];
            this.rptItems.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            OtherMediumInProductInfo info = new OtherMediumInProductInfo();
            info.CreatedDate = DateTime.Now;
            info.CreatedUserID = UserInfo.UserID;
            if (!string.IsNullOrEmpty(Request["MediaProductID"]))
            {
                info = OtherMediumInProductsManager.GetModel(Convert.ToInt32(Request["MediaProductID"]));
            }
            info.MediaName = this.txtMediaName.Text;
            info.ModifiedUserID = UserInfo.UserID;
            info.ModifiedDate = DateTime.Now;
            info.ProductID = Convert.ToInt32(this.hidtypeIds.Value);

            if (!string.IsNullOrEmpty(Request["MediaProductID"]))
            {
                OtherMediumInProductsManager.Update(info);
            }
            else
            {
                OtherMediumInProductsManager.Add(info);
            }

            ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('保存成功！');window.location.href='OtherMediaList.aspx'", true);
        }

        protected void rptItems_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "deleteItem")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                OtherMediumInProductsDetailsManager.Delete(id);
                BindDetails();
                return;
            }
        }

        protected void rptItems_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label txtManuscriptType = (Label)e.Item.FindControl("txtManuscriptType");
                if (txtManuscriptType != null && txtManuscriptType.Text != string.Empty)
                {
                    ManuscriptTypeInfo info = ManuscriptTypeManager.GetModel(Convert.ToInt32(txtManuscriptType.Text));
                    if (info != null)
                    {
                        txtManuscriptType.Text = info.TypeName;
                    }
                }
            }
        }
    }
}