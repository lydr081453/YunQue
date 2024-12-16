using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.WebPage
{
    public class EditPageForPR : ESP.Web.UI.PageBase
    {
        protected override void OnLoad(EventArgs e)
        {
            int dataTypeId = (int)ESP.Purchase.Common.State.DataType.PR;
            if (!string.IsNullOrEmpty(Request[ESP.Purchase.Common.RequestName.GeneralID]))
            {
                int dataId = int.Parse(Request[ESP.Purchase.Common.RequestName.GeneralID]);
                if (!ESP.Purchase.BusinessLogic.DataPermissionManager.CheckUserEditPermission(dataTypeId, dataId, int.Parse(CurrentUser.SysID)))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('你没有访问该数据的权限！');window.location.href='/Purchase/Default.aspx';", true);
                }
            }
            base.OnLoad(e);
        }
    }
}
