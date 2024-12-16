using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.Utility;

namespace FinanceWeb.ContractFiles
{
    public partial class ContractAudit : ESP.Finance.WebPage.EditPageForProject
    {
        private int projectid = 0;
        private ProjectInfo projectInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
                {
                    projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
                    projectInfo = ESP.Finance.BusinessLogic.ProjectManager.GetModel(projectid);
                    this.PrepareDisplay.InitProjectInfo(projectInfo);
                }
                Bind_ContractList();
                this.lblLog.Text = this.GetAuditLog(projectid);
            }
        }

        private string GetAuditLog(int pid)
        {
            IList<ESP.Finance.Entity.AuditLogInfo> histList = ESP.Finance.BusinessLogic.AuditLogManager.GetContractList(pid);

            string loginfo = string.Empty;
            foreach (ESP.Finance.Entity.AuditLogInfo model in histList)
            {
                string austatus = string.Empty;
                if (model.AuditStatus == (int)AuditHistoryStatus.PassAuditing)
                {
                    austatus = "审批通过";
                }
                else if (model.AuditStatus == (int)AuditHistoryStatus.TerminateAuditing)
                {
                    austatus = "审批驳回";
                }
                if (model.AuditStatus == (int)AuditHistoryStatus.ContractAudit_Audited)
                {
                    austatus = "证据链-审批通过";
                }
                else if (model.AuditStatus == (int)AuditHistoryStatus.ContractAudit_Rejected)
                {
                    austatus = "证据链-审批驳回";
                }
                string auditdate = model.AuditDate == null ? "" : model.AuditDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                loginfo += model.AuditorEmployeeName + "(" + model.AuditorUserName + ")" + austatus + "[" + auditdate + "]" + model.Suggestion + "<br/>";

            }

            return loginfo;
        }

        protected void btnTip_Click(object sender, EventArgs e)
        {
            int ret = addAuditLog(AuditHistoryStatus.Tip);
            if (ret > 0)
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='" + Request["backurl"] + "';alert('留言保存成功！');", true);

        }

        private int addAuditLog(ESP.Finance.Utility.AuditHistoryStatus status)
        {
            projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
            ESP.Finance.Entity.AuditLogInfo audit = new ESP.Finance.Entity.AuditLogInfo();
            audit.FormID = projectid;
            audit.Suggestion = this.txtAuditRemark.Text;
            audit.AuditDate = DateTime.Now;
            audit.AuditorSysID = int.Parse(CurrentUser.SysID);
            audit.AuditorUserCode = CurrentUser.ID;
            audit.AuditorEmployeeName = CurrentUser.Name;
            audit.AuditorUserName = CurrentUser.ITCode;
            audit.AuditStatus = (int)status;
            audit.FormType = (int)ESP.Finance.Utility.FormType.Contract;
            return ESP.Finance.BusinessLogic.AuditLogManager.Add(audit);
        }

        protected void Bind_ContractList()
        {
            string strWhere = " status=@status and del=0";
            List<System.Data.SqlClient.SqlParameter> parms = new List<System.Data.SqlClient.SqlParameter>();
            parms.Add(new System.Data.SqlClient.SqlParameter("@status", (int)ESP.Finance.Utility.ContractStatus.Status.Auditing));
            gvContract.DataSource = ESP.Finance.BusinessLogic.ContractManager.GetListByProject(projectid, strWhere, parms).OrderByDescending(x => x.CreateDate).ToList();
            gvContract.DataBind();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["backurl"]))
                Response.Redirect("ContractAuditList.aspx");
            else
            Response.Redirect(Request["backurl"]);
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            string contractIds = Request["chkContractId"];
            ESP.Finance.BusinessLogic.ContractManager.UpdateContractStatus(contractIds, ESP.Finance.Utility.ContractStatus.Status.Audited);
            addAuditLog(AuditHistoryStatus.ContractAudit_Audited);

            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
            {
                try
                {
                    projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
                    ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
                    SendMailHelper.SendMailConfirmEvidence(projectModel.ProjectCode, projectModel.ApplicantUserEmail, CurrentUserName);
                }
                catch
                { }
            }
            if (string.IsNullOrEmpty(Request["backurl"]))
                Response.Redirect("ContractAuditList.aspx");
            else
                Response.Redirect(Request["backurl"]);
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            string contractIds = Request["chkContractId"];
            ESP.Finance.BusinessLogic.ContractManager.UpdateContractStatus(contractIds, ESP.Finance.Utility.ContractStatus.Status.Rejected);
            addAuditLog(AuditHistoryStatus.ContractAudit_Rejected);

            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
            {
                try
                {
                    projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
                    ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
                    SendMailHelper.SendMailRejectEvidence(projectModel.ProjectCode, projectModel.ApplicantUserEmail, CurrentUserName);
                }
                catch
                { }
            }
            if (string.IsNullOrEmpty(Request["backurl"]))
                Response.Redirect("ContractAuditList.aspx");
            else
                Response.Redirect(Request["backurl"]);
        }
    }
}