using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.WebPage
{
    public class ViewPageForReturn : ESP.Web.UI.PageBase
    {
        protected override void OnLoad(EventArgs e)
        {
            int dataTypeId = (int)ESP.Purchase.Common.State.DataType.Return;
            int dataId = int.Parse(Request[ESP.Finance.Utility.RequestName.ReturnID]);
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(dataId);
           // int dimissionCount =  ESP.HumanResource.BusinessLogic.DimissionDetailsManager.GetDimissionDetail(Convert.ToInt32(CurrentUser.SysID), dataId);
            bool isFinance = false;
            if (returnModel.ProjectID != null && returnModel.ProjectID.Value != 0)
            {
                ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(returnModel.ProjectID.Value);
                if (projectModel.BranchID != null && projectModel.BranchID.Value != 0)
                {
                    ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModel(projectModel.BranchID.Value);
                    if (branchModel != null)
                    {
                        if (branchModel.OtherFinancialUsers.IndexOf("," + CurrentUser.SysID + ",") >= 0)
                        {
                            isFinance = true;
                        }
                    }
                }
            }
            if (!ESP.Purchase.BusinessLogic.DataPermissionManager.CheckUserViewPermission(dataTypeId, dataId, int.Parse(CurrentUser.SysID)) && !isFinance)
            {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('你没有访问该数据的权限！');window.parent.location.href='/Default.aspx';", true);
            }
            base.OnLoad(e);
        }
    }
}
