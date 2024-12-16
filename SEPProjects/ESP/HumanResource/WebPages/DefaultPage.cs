using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.WebPages
{
    public class DefaultPage : ESP.Web.UI.PageBase
    {
        protected override void OnLoad(EventArgs e)
        {
            string url = "";
            if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("014", this.ModuleInfo.ModuleID, this.UserID))  // 判断当前登录用户是否拥有普通用户权限
            {
                url = "/DefaultWorkSpace.aspx";
            }

            if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("002", this.ModuleInfo.ModuleID, this.UserID) ||
                ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("003", this.ModuleInfo.ModuleID, this.UserID) ||
                ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("005", this.ModuleInfo.ModuleID, this.UserID) ||
                ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("006", this.ModuleInfo.ModuleID, this.UserID))
            {
                url = "/HR/Employees/EmployeesAllList.aspx";
            }
            else if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("012", this.ModuleInfo.ModuleID, this.UserID) ||
                ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("013", this.ModuleInfo.ModuleID, this.UserID))
            {
                url = "/HR/Employees/EmployeesDisplay.aspx";
            }
            else if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("001", this.ModuleInfo.ModuleID, this.UserID) ||
                ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("004", this.ModuleInfo.ModuleID, this.UserID))
            {
                url = "";               
            }

            if (!string.IsNullOrEmpty(url))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='" + url + "';", true);
            }
            
            base.OnLoad(e);
        }
    }
}
