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
    public partial class PrincipalList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            dlList.DataSource = (new SC_PrincipalDataProvider()).GetAllLists();
            dlList.DataBind();
            ClientScript.RegisterStartupScript(typeof(string), "", "page_init();", true);
        }

        protected void dlList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                SC_Principal principal = e.Item.DataItem as SC_Principal;
                if (null != principal)
                {
                    Label labContent = e.Item.FindControl("labContent2") as Label;
                    if (null != labContent)
                    {
                        labContent.Text = "<input id=" + principal.PrincipalId.ToString() +
                                              " type='radio' name='principallist' value='rtoArea' onclick=\"javascript:setvalue('" +
                                              principal.PrincipalId.ToString() + "','" + principal.PrincipalDes + "');\" /><label for='" +
                                              principal.PrincipalId.ToString() +
                                              "'>" + principal.PrincipalDes + "</label>";

                    }
                }
            }

        }
    }
}
