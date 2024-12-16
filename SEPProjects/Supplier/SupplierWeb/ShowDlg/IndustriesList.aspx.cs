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
    public partial class IndustriesList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            dlList.DataSource = (new SC_IndustriesDataProvider()).GetAllLists();
            dlList.DataBind();
            ClientScript.RegisterStartupScript(typeof(string), "", "page_init();", true);
        }

        protected void dlList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                SC_Industries industries = e.Item.DataItem as SC_Industries;
                if (null != industries)
                {
                    Label labContent = e.Item.FindControl("labContent1") as Label;
                    if (null != labContent)
                    {
                        labContent.Text = "<input id=" + industries.IndustryID.ToString() +
                                              " type='radio' name='industrieslist' value='rtoArea' onclick=\"javascript:setvalue('" +
                                              industries.IndustryID.ToString() + "','" + industries.IndustryName + "');\" /><label for='" +
                                              industries.IndustryID.ToString() +
                                              "'>" + industries.IndustryName + "</label>";

                    }
                }
            }

        }
    }
}
