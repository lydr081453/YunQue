﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.WebPage
{
    public class EditPageForProject : ESP.Web.UI.PageBase
    {
        protected override void OnLoad(EventArgs e)
        {
            int dataTypeId = (int)ESP.Purchase.Common.State.DataType.Project;
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
            {
                int dataId = int.Parse(Request[ESP.Finance.Utility.RequestName.ProjectID]);
                if (!ESP.Purchase.BusinessLogic.DataPermissionManager.CheckUserEditPermission(dataTypeId, dataId, int.Parse(CurrentUser.SysID)))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('你没有访问该数据的权限！');window.parent.location.href='/Default.aspx';", true);
                }
            }
            base.OnLoad(e);
        }
    }
}