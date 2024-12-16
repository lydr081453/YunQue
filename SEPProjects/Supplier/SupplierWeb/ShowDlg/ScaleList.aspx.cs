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
    public partial class ScaleList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            dlList.DataSource = (new SC_ScaleDataProvider()).GetAllLists();
            dlList.DataBind();
            ClientScript.RegisterStartupScript(typeof(string), "", "page_init();", true);
        }

        protected void dlList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                SC_Scale scale = e.Item.DataItem as SC_Scale;
                if (null != scale)
                {
                    Label labContent = e.Item.FindControl("labContent4") as Label;
                    if (null != labContent)
                    {
                        labContent.Text = "<input id=" + scale.ScaleId.ToString() +
                                              " type='radio' name='scalelist' value='rtoArea' onclick=\"javascript:setvalue('" +
                                              scale.ScaleId.ToString() + "','" + scale.ScaleDes + "');\" /><label for='" +
                                              scale.ScaleId.ToString() +
                                              "'>" + scale.ScaleDes + "</label>";

                    }
                }
            }

        }
    }
}
