using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Purchase_KillTraffic : ESP.Web.UI.PageBase
{
    int returnId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ReturnID]))
            returnId = int.Parse(Request[ESP.Finance.Utility.RequestName.ReturnID]);
        if (!IsPostBack)
        {
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
        returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete;
        decimal inputMoney = decimal.Parse(txtPrice.Text.Trim());
        returnModel.FactFee = inputMoney;
        if (inputMoney == 0)
        {
            returnModel.FactFee = 0;
            returnModel.PreFee = 0;
        }
        ESP.Finance.Utility.UpdateResult result = ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
        if (result == ESP.Finance.Utility.UpdateResult.Succeed)
        {
            ESP.Finance.Entity.AuditLogInfo logModel = new ESP.Finance.Entity.AuditLogInfo();
            logModel.AuditDate = DateTime.Now;
            logModel.AuditorEmployeeName = CurrentUser.Name;
            logModel.AuditorSysID = Convert.ToInt32(CurrentUser.SysID);
            logModel.AuditorUserCode = CurrentUser.ID;
            logModel.AuditorUserName = CurrentUser.ITCode;
            logModel.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing;
            logModel.FormID = returnModel.ReturnID;
            logModel.FormType = (int)ESP.Finance.Utility.FormType.Return;
            logModel.Suggestion ="销账处理,"+ this.txtRemark.Text.Trim();
            ESP.Finance.BusinessLogic.AuditLogManager.Add(logModel);
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('Traffic Fee销账成功!');window.location='/Edit/ReturnTabEdit.aspx';", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('Traffic Fee销账失败!');", true);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Edit/ReturnTabEdit.aspx");
    }
}
