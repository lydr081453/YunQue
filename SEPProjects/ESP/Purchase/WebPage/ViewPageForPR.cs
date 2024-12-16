using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.WebPage
{
    public class ViewPageForPR : ESP.Web.UI.PageBase
    {
        protected override void OnLoad(EventArgs e)
        {
            int dataTypeId = (int)ESP.Purchase.Common.State.DataType.PR;
            int dataId = int.Parse(Request[ESP.Purchase.Common.RequestName.GeneralID]);
            int dimissionCount = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.GetDimissionDetail(Convert.ToInt32(CurrentUser.SysID), dataId, "PR单");
            ESP.Purchase.Entity.GeneralInfo genModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(dataId);

            if (genModel != null && (int.Parse(CurrentUser.SysID) == genModel.requestor || int.Parse(CurrentUser.SysID) == genModel.goods_receiver || int.Parse(CurrentUser.SysID) == genModel.appendReceiver))
            {

            }
            else
            {
                if (!ESP.Purchase.BusinessLogic.DataPermissionManager.CheckUserViewPermission(dataTypeId, dataId, int.Parse(CurrentUser.SysID)))
                {
                    if (dimissionCount == 0)
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('你没有访问该数据的权限！');window.location.href='/Purchase/Default.aspx';", true);
                }
            }
            base.OnLoad(e);
        }
    }
}
