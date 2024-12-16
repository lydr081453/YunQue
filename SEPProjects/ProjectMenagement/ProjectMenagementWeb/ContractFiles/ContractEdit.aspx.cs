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
    public partial class ContractEdit : ESP.Web.UI.PageBase
    {
        private int projectid = 0;
        public string AuditorName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
                {
                    projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
                    ProjectInfo projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModel(projectid);
                    this.PrepareDisplay.InitProjectInfo(projectinfo);

                    BranchInfo branch = ESP.Finance.BusinessLogic.BranchManager.GetModel(projectinfo.BranchID.Value);
                    ESP.Framework.Entity.UserInfo contractUser = ESP.Framework.BusinessLogic.UserManager.Get(branch.ContractAuditor);
                    AuditorName = contractUser.LastNameCN + contractUser.FirstNameCN;
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

        protected void Bind_ContractList()
        {

            gvContract.DataSource = ESP.Finance.BusinessLogic.ContractManager.GetListByProject(projectid, "", new List<System.Data.SqlClient.SqlParameter>()).OrderByDescending(x => x.CreateDate).ToList();
            gvContract.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            string contractIds = Request["chkContractId"];
            ESP.Finance.BusinessLogic.ContractManager.UpdateContractStatus(contractIds, ESP.Finance.Utility.ContractStatus.Status.Auditing);

            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
            {
                try
                {
                    projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
                    ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
                    ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModel(projectModel.BranchID.Value);
                    ESP.HumanResource.Entity.EmployeeBaseInfo emp = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(branchModel.ContractAuditor);
                    SendMailHelper.SendMailCommitEvidence(projectModel, emp.Email);
                }
                catch
                { }
            }

            Response.Redirect("/contractfiles/ContractEdit.aspx?ProjectID=" + Request["ProjectID"] + "&backurl=" + Request["backurl"]);
        }

        protected void gvContract_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                int contractId = int.Parse(e.CommandArgument.ToString());
                ESP.Finance.BusinessLogic.ContractManager.Delete(contractId);
                Response.Redirect("/contractfiles/ContractEdit.aspx?ProjectID=" + Request["ProjectID"] + "&backurl=" + Request["backurl"]);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request["backurl"]);
        }
    }
}