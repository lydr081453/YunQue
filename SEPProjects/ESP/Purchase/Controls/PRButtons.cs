using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.Controls
{
    public class CheckPRInputButton : System.Web.UI.HtmlControls.HtmlInputButton
    {
        protected override void OnServerClick(EventArgs e)
        {
            string returnPage = "";
            int PRID = 0;
            int ProjectID = 0;
            if (!string.IsNullOrEmpty(Page.Request[ESP.Finance.Utility.RequestName.ReturnID]))
            {
                ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(int.Parse(Page.Request[ESP.Finance.Utility.RequestName.ReturnID]));
                if (returnModel != null)
                {
                    if(returnModel.PRID != null && returnModel.PRID.Value > 0)
                        PRID = returnModel.PRID.Value;
                    if (returnModel.ProjectID != null && returnModel.ProjectID.Value > 0)
                        ProjectID = returnModel.ProjectID.Value;
                    returnPage = "/project/Default.aspx";
                }
            }
            if (!string.IsNullOrEmpty(Page.Request[Common.RequestName.GeneralID]))
            {
                PRID = int.Parse(Page.Request[Common.RequestName.GeneralID]);
                returnPage = "/Purchase/Default.aspx";
            }
            if (PRID > 0)
            {
                ESP.Purchase.Entity.GeneralInfo model = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(PRID);
                string alertMessage = "";
                if (model.InUse != (int)ESP.Purchase.Common.State.PRInUse.Use)
                {
                    if (model.InUse == (int)ESP.Purchase.Common.State.PRInUse.Disable)
                    {
                        alertMessage = Common.State.DisabledMessageForPR;
                    }
                    else if (model.InUse == (int)ESP.Purchase.Common.State.PRInUse.DisableProject)
                    {
                        alertMessage = Common.State.DisabledMessageForProject;
                    }
                }
                if (model.status == Common.State.requisition_Stop)
                {
                    alertMessage = Common.State.StopMessageForPR;
                }
                if (alertMessage != "")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + alertMessage + "');window.location.href='" + returnPage + "';", true);
                    return;
                }
            }
            if (ProjectID > 0)
            {
                ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(ProjectID);
                if (projectModel != null && projectModel.InUse != (int)ESP.Finance.Utility.ProjectInUse.Use)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('该项目号已被暂停，你不能进行任何操作！');window.location.href='" + returnPage + "';", true);
                    return;
                }
            }
            base.OnServerClick(e);
        }
    }

    public class CheckPRAspButton : System.Web.UI.WebControls.Button
    {
        protected override void OnClick(EventArgs e)
        {
            int PRID = 0;
            string returnPage = "";
            int ProjectID = 0;
            if (!string.IsNullOrEmpty(Page.Request[ESP.Finance.Utility.RequestName.ReturnID]))
            {
                ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(int.Parse(Page.Request[ESP.Finance.Utility.RequestName.ReturnID]));
                if (returnModel != null)
                {
                    if (returnModel.PRID != null && returnModel.PRID.Value > 0)
                        PRID = returnModel.PRID.Value;
                    if (returnModel.ProjectID != null && returnModel.ProjectID.Value > 0)
                        ProjectID = returnModel.ProjectID.Value;
                    returnPage = "/project/Default.aspx";
                }
            }
            if (!string.IsNullOrEmpty(Page.Request[Common.RequestName.GeneralID]))
            {
                PRID = int.Parse(Page.Request[Common.RequestName.GeneralID]);
                returnPage = "/Purchase/Default.aspx";
            }
            if (PRID > 0)
            {
                ESP.Purchase.Entity.GeneralInfo model = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(PRID);
                string alertMessage = "";
                if (model.InUse != (int)ESP.Purchase.Common.State.PRInUse.Use)
                {
                    if (model.InUse == (int)ESP.Purchase.Common.State.PRInUse.Disable)
                    {
                        alertMessage = Common.State.DisabledMessageForPR;
                    }
                    else if (model.InUse == (int)ESP.Purchase.Common.State.PRInUse.DisableProject)
                    {
                        alertMessage = Common.State.DisabledMessageForProject;
                    }
                }
                if (model.status == Common.State.requisition_Stop)
                {
                    alertMessage = Common.State.StopMessageForPR;
                }
                if (alertMessage != "")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + alertMessage + "');window.location.href='" + returnPage + "';", true);
                    return;
                }
            }
            if (ProjectID > 0)
            {
                ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(ProjectID);
                if (projectModel != null && projectModel.InUse != (int)ESP.Finance.Utility.ProjectInUse.Use)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('该项目号已被暂停，你不能进行任何操作！');window.location.href='" + returnPage + "';", true);
                    return;
                }
            }
            base.OnClick(e);
        }
    }
}
