using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Supplier.DataAccess;
using ESP.Supplier.Entity;

namespace SupplierWeb.ShowDlg
{
    public partial class TypeList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            dlList.DataSource = (new SC_TypeDataProvider()).GetAllL2Lists();
            dlList.DataBind();
            ClientScript.RegisterStartupScript(typeof(string), "", "page_init();", true);
        }

        protected void dlList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                SC_Type type = e.Item.DataItem as SC_Type;
                if (null != type)
                {
                    Label labContent = e.Item.FindControl("labContent") as Label;
                    if (null != labContent)
                    {
                        labContent.Text = "<input id=" + type.Id.ToString() +
                                              " type='checkbox' name='typelist' value='rtoArea' onclick=\"javascript:setvalue('" +
                                              type.Id.ToString() + "','" + type.TypeName + "');\" /><label for='" +
                                              type.Id.ToString() +
                                              "'>" + type.TypeName + "</label>";

                    }
                }
            }

        }
    }
}
