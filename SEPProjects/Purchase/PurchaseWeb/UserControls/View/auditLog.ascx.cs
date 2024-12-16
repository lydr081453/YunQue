using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;

public partial class UserControls_View_auditLog : System.Web.UI.UserControl
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
        DataTable oploglist = AuditLogManager.GetauditLog(generalId,(int)State.operationAudit_status.No);
        string strOplog = string.Empty;
        foreach (DataRow dr in oploglist.Rows)
        {
            strOplog += "原" + dr["prno"].ToString() + "由" + dr["auditusername"].ToString() + "于" + dr["remarkDate"].ToString() + "驳回.驳回意见:" + dr["remark"].ToString() + "<br />";
        }

        litLog.Text = strOplog;
    }
}
