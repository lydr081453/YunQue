using ESP.Finance.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdministrativeWeb.RequestForSeal
{
    public partial class Ctl_AuditLog : System.Web.UI.UserControl
    {
        private int RfsId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["RfsId"]))
            {
                RfsId = int.Parse(Request["RfsId"]);
            }

            if (!IsPostBack)
            {
                labLog.Text = GetAuditLog();
                LogTable.Visible = labLog.Text.Length > 0;
            }
        }

        private string GetAuditLog()
        {
            System.Text.StringBuilder log = new System.Text.StringBuilder();
            IList<ESP.Finance.Entity.AuditLogInfo> auditList = ESP.Finance.BusinessLogic.AuditLogManager.GetRequestForSealList(RfsId);

            foreach (var l in auditList)
            {
                string austatus = string.Empty;
                if (l.AuditStatus == (int)AuditHistoryStatus.PassAuditing)
                {
                    austatus = "审批通过";
                }
                else if (l.AuditStatus == (int)AuditHistoryStatus.TerminateAuditing)
                {
                    austatus = "审批驳回";
                }

                log.AppendFormat("{0:yyyy/MM/dd hh:mm:ss}", l.AuditDate).Append(" ")
                    .Append(l.AuditorEmployeeName).Append("(").Append(l.AuditorUserName).Append(") ")
                    .Append(austatus).Append(" ")
                    .Append(l.Suggestion).Append("<br/>");
            }

            return log.ToString();
        }
    }
}