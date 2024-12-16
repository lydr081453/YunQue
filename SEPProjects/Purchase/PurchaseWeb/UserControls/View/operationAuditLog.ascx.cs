using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

public partial class UserControls_View_operationAuditLog : System.Web.UI.UserControl
{
    int generalId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
        {
            generalId = int.Parse(Request[RequestName.GeneralID]);
            bindInfo();
        }
    }

    private void bindInfo()
    {
        //业务审核日志
        IList<AuditLogInfo> oploglist = AuditLogManager.GetModelListByGID(generalId);
        string strOplog = string.Empty;
        foreach (AuditLogInfo log in oploglist)
        {
            strOplog += log.auditUserName + State.operationAudit_statusName[log.auditType] + " " + log.remarkDate + " " + log.remark + "<br/>";
        }
        litLog.Text = strOplog;
    }
}
