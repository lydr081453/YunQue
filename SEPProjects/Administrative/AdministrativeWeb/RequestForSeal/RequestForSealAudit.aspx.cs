using ESP.Administrative.BusinessLogic;
using ESP.Finance.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdministrativeWeb.RequestForSeal
{
    public partial class RequestForSealAudit : ESP.Web.UI.PageBase
    {
        private int RfsId = 0;
        RequestForSealManager manager = new RequestForSealManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["RfsId"]))
            {
                RfsId = int.Parse(Request["RfsId"]);
            }

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("RequestForSealAuditList.aspx");
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            var model = manager.GetModel(RfsId);
            int retvalue = manager.Audit(model, CurrentUser, (int)AuditHistoryStatus.PassAuditing, txtAudit.Text);
            if (retvalue > 0)
            {
                ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('用印申请审批成功!');window.location.href='RequestForSealAuditList.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            var model = manager.GetModel(RfsId);
            int retvalue = manager.Audit(model, CurrentUser, (int)AuditHistoryStatus.TerminateAuditing, txtAudit.Text);
            if (retvalue > 0)
            {
                ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('用印申请审批驳回!');window.location.href='RequestForSealAuditList.aspx';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            }
        }

        //private string GetAuditLog(int batchId)
        //{
        //    System.Text.StringBuilder log = new System.Text.StringBuilder();
        //    IList<ESP.Finance.Entity.AuditLogInfo> auditList = ESP.Finance.BusinessLogic.AuditLogManager.GetRebateRegistrationList(batchId);

        //    foreach (var l in auditList)
        //    {
        //        string austatus = string.Empty;
        //        if (l.AuditStatus == (int)AuditHistoryStatus.PassAuditing)
        //        {
        //            austatus = "审批通过";
        //        }
        //        else if (l.AuditStatus == (int)AuditHistoryStatus.TerminateAuditing)
        //        {
        //            austatus = "审批驳回";
        //        }

        //        log.AppendFormat("{0:yyyy/MM/dd hh:mm:ss}", l.AuditDate).Append(" ")
        //            .Append(l.AuditorEmployeeName).Append("(").Append(l.AuditorUserName).Append(") ")
        //            .Append(austatus).Append(" ")
        //            .Append(l.Suggestion).Append("<br/>");
        //    }

        //    return log.ToString();
        //}

    }
}