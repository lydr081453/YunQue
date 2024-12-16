using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.WebSite.Account
{
    public partial class Settings : ESP.Web.UI.PageBase
    {
        protected string UserIcon
        {
            get
            {
                if (UserID > 0)
                {
                    ESP.Framework.Entity.EmployeeInfo info = ESP.Framework.BusinessLogic.EmployeeManager.Get(UserID);
                    if (info != null && info.Photo.Trim().Length > 0)
                    {
                        return Portal.Common.Global.USER_ICON_FOLDER + info.Photo;
                    }
                }
                return "";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                imgUserIcon.Src = UserIcon;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Request.Files[0].ContentLength > 0)
            {
                ESP.Framework.Entity.EmployeeInfo info = ESP.Framework.BusinessLogic.EmployeeManager.Get(UserID);
                info.Photo = Portal.Common.ImageHelper.SaveIcon(Request.Files[0].InputStream, UserID, "") + ".jpg";
                ESP.Framework.BusinessLogic.EmployeeManager.Update(info);

                imgUserIcon.Src = UserIcon;
            }
        }
    }
}
