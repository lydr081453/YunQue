using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Supplier.BusinessLogic;
using ESP.Supplier.DataAccess;
using ESP.Supplier.Entity;

namespace SupplierWeb.ShowDlg
{
    public partial class PropertyList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            dlList.DataSource = (new SC_PropertyDataProvider()).GetAllLists();
            dlList.DataBind();
            ClientScript.RegisterStartupScript(typeof(string), "", "page_init();", true);
        }

        protected void dlList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                SC_Property property = e.Item.DataItem as SC_Property;
                if (null != property)
                {
                    Label labContent = e.Item.FindControl("labContent3") as Label;
                    if (null != labContent)
                    {
                        labContent.Text = "<input id=" + property.PropertyId.ToString() +
                                              " type='radio' name='propertylist' value='rtoArea' onclick=\"javascript:setvalue('" +
                                              property.PropertyId.ToString() + "','" + property.PropertyDes + "');\" /><label for='" +
                                              property.PropertyId.ToString() +
                                              "'>" + property.PropertyDes + "</label>";

                    }
                }
            }

        }
    }
}
