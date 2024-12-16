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
    public partial class AreaList : System.Web.UI.Page
    { 
        protected void Page_Load(object sender, EventArgs e)             
        {
            dlList.DataSource = (new SC_AreaDataProvider()).GetAllLists();
            dlList.DataBind();
            ClientScript.RegisterStartupScript(typeof(string), "", "page_init();", true);
        }

        protected void dlList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                SC_Area area = e.Item.DataItem as SC_Area;
                if (null != area)
                {
                    Label labContent = e.Item.FindControl("labContent") as Label;
                    if(null != labContent)
                    {
                        labContent.Text = "<input id=" + area.AreaId.ToString() +
                                              " type='radio' name='arealist' value='rtoArea' onclick=\"javascript:setvalue('" +
                                              area.AreaId.ToString() + "','" + area.AreaDes + "');\" /><label for='" +
                                              area.AreaId.ToString() +
                                              "'>" + area.AreaDes + "</label>";
                        
                    }
                }
            }

        }
    }
}
