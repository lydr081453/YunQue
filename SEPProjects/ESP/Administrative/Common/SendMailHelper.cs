using ESP.Administrative.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace ESP.Administrative.Common
{
    public class SendMailHelper
    {
        const string RequestForSealAudit = "{0}申请的用印申请已被{2}{3}。请查看审批记录。";

        public static string SendMailRequestForSealAudit(RequestForSealInfo model, int status, string CreatorEmail, string currentRoleName, string nextAuditorMail)
        {
            string ret = "";
            string msgFirstOpera = string.Empty;
            List<MailAddress> mailAddressList = new List<MailAddress>();
            mailAddressList.Add(new MailAddress(CreatorEmail));

            if (status == (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing)
            {
                msgFirstOpera = string.Format(RequestForSealAudit, model.RequestorName,currentRoleName, "审批通过");
                if (!string.IsNullOrEmpty(nextAuditorMail))
                {
                    mailAddressList.Add(new MailAddress(nextAuditorMail));
                }
            }
            else if (status == (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing)
            {
                msgFirstOpera = string.Format(RequestForSealAudit, model.RequestorName, currentRoleName, "审批驳回");
            }

            ESP.Mail.MailManager.Send("用印申请审批", msgFirstOpera, true, null, mailAddressList.ToArray(), null, null, null);

            return ret;
        }
    }
}
