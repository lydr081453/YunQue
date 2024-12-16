using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.BusinessLogic;
using ESP.Framework.Entity;

namespace SEPAdmin.WebSiteManagement
{
    public partial class WebSiteList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IList<WebSiteInfo> list = WebSiteManager.GetAll();
                gvWebSites.DataSource = list;
                gvWebSites.DataBind();
            }

        }

        protected void EditItem_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "EditItem")
            {
                int webSiteID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("WebSiteEdit.aspx?id=" + webSiteID);
            }
        }

        //protected string GetUsername(int userid)
        //{
        //    UserInfo u = UserController.Get(userid);
        //    return u.Username;
        //}

        public bool IsEditable(WebSiteInfo webSiteInfo)
        {
            return true;
        }
    }
}
