using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.UserControls.Project
{
    public partial class AuditTip : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public int DataId { get; set; }
        public ESP.Finance.Utility.FormType DataType { get; set; }
        public string Message { get; set; }
        public ESP.Compatible.Employee CurrentUser { get; set; }
        protected void btnTip_Click(object sender, EventArgs e)
        {
            switch ((int)DataType)
            {
                //项目号
                case (int)ESP.Finance.Utility.FormType.Project:
                //支持方
                case (int)ESP.Finance.Utility.FormType.Supporter:
                //PN
                case (int)ESP.Finance.Utility.FormType.Payment:
                    ESP.Finance.Entity.AuditLogInfo audit = new ESP.Finance.Entity.AuditLogInfo();
                    audit.FormID = DataId;
                    audit.Suggestion = Message;
                    audit.AuditDate = DateTime.Now;
                    audit.AuditorSysID = int.Parse(CurrentUser.SysID);
                    audit.AuditorUserCode = CurrentUser.ID;
                    audit.AuditorEmployeeName = CurrentUser.Name;
                    audit.AuditorUserName = CurrentUser.ITCode;
                    audit.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.Tip;
                    audit.FormType = (int)DataType;
                    ESP.Finance.BusinessLogic.AuditLogManager.Add(audit);
                    break;
                //报销
                case (int)ESP.Finance.Utility.FormType.ExpenseAccount:
                    ESP.Finance.Entity.ExpenseAuditDetailInfo oop = new ESP.Finance.Entity.ExpenseAuditDetailInfo();
                    oop.AuditeDate = DateTime.Now;
                    oop.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.Tip;
                    oop.AuditorUserID = int.Parse(CurrentUser.SysID);
                    oop.AuditorUserCode = CurrentUser.ID;
                    oop.AuditorEmployeeName = CurrentUser.Name;
                    oop.AuditorUserName = CurrentUser.ITCode;
                    oop.AuditType = (int)DataType;
                    oop.ExpenseAuditID = DataId;
                    oop.Suggestion = Message;
                    ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.Add(oop);

                    break;
                //PR
                case (int)ESP.Finance.Utility.FormType.PR:
                    ESP.Purchase.Entity.AuditLogInfo log = new ESP.Purchase.Entity.AuditLogInfo();
                    log.auditType = (int)ESP.Finance.Utility.AuditHistoryStatus.Tip;
                    log.auditUserId = int.Parse(CurrentUser.SysID);
                    log.auditUserName = CurrentUser.Name;
                    log.gid = DataId;
                    log.IpAddress = Request.UserHostAddress;
                    log.prNo = "";
                    log.remark = Message;
                    log.remarkDate = DateTime.Now;
                    ESP.Purchase.BusinessLogic.AuditLogManager.Add(log, this.Request);
                    break;
            }
        }
    }
}