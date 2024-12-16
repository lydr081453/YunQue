using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.Utility;

namespace FinanceWeb.ApplyForInvioce
{
    public partial class ApplyForInvioceEdit :ESP.Web.UI.PageBase
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
                Bind_List();
                this.lblLog.Text = this.GetAuditLog(projectid);
                hyNew.NavigateUrl = "NewApplyForInvioce.aspx?ProjectID=" + Request["ProjectID"] + "&backurl=" + Request["backurl"];
            }
        }

        private string GetAuditLog(int pid)
        {
            IList<ESP.Finance.Entity.AuditLogInfo> histList = ESP.Finance.BusinessLogic.AuditLogManager.GetApplyForInvioceList(pid);

            string loginfo = string.Empty;
            foreach (ESP.Finance.Entity.AuditLogInfo model in histList)
            {
                string austatus = string.Empty;
                if (Common.AuditHistoryStatus_Names.ContainsKey(model.AuditStatus.Value))
                    austatus = Common.AuditHistoryStatus_Names[model.AuditStatus.Value];
                string auditdate = model.AuditDate == null ? "" : model.AuditDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                loginfo += model.AuditorEmployeeName + "(" + model.AuditorUserName + ")" + austatus + "[" + auditdate + "]" + model.Suggestion + "<br/>";

            }

            return loginfo;
        }

        protected void Bind_List()
        {
            string strWhere = " projectId = @projectId";
            List<System.Data.SqlClient.SqlParameter> parms = new List<System.Data.SqlClient.SqlParameter>();
            parms.Add(new System.Data.SqlClient.SqlParameter("@projectId", projectid));
            gvContract.DataSource = ESP.Finance.BusinessLogic.ApplyForInvioceManager.GetList(strWhere, parms).OrderByDescending(x => x.CreateDate).ToList();
            gvContract.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            string Ids = Request["chkId"];
            ESP.Finance.BusinessLogic.ApplyForInvioceManager.UpdateStatus(Ids, ESP.Finance.Utility.ApplyForInvioceStatus.Status.Auditing);

            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
            {
                try
                {
                    projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
                    ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
                    ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModel(projectModel.BranchID.Value);
                    ESP.HumanResource.Entity.EmployeeBaseInfo emp = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(branchModel.ContractAuditor);
                    SendMailHelper.SendMailCommitApplyForInvioce(projectModel, emp.Email);
                }
                catch
                { }
            }

            Response.Redirect("/ApplyForInvioce/ApplyForInvioceEdit.aspx?ProjectID=" + Request["ProjectID"] + "&backurl=" + Request["backurl"]);
        }

        protected void gvContract_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                int contractId = int.Parse(e.CommandArgument.ToString());
                ESP.Finance.BusinessLogic.ApplyForInvioceManager.Delete(contractId);
                Response.Redirect("/ApplyForInvioce/ApplyForInvioceEdit.aspx?ProjectID=" + Request["ProjectID"] + "&backurl=" + Request["backurl"]);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request["backurl"]);
        }
    }
}