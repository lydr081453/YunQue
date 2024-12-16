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
    public partial class ApplyForInvioceAudit : ESP.Finance.WebPage.EditPageForProject
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
                Bind_List();
                this.lblLog.Text = this.GetAuditLog(projectid);
            }
        }

        private string GetAuditLog(int pid)
        {
            IList<ESP.Finance.Entity.AuditLogInfo> histList = ESP.Finance.BusinessLogic.AuditLogManager.GetApplyForInvioceList(pid);

            string loginfo = string.Empty;
            foreach (ESP.Finance.Entity.AuditLogInfo model in histList)
            {
                string austatus = string.Empty;
                if(Common.AuditHistoryStatus_Names.ContainsKey(model.AuditStatus.Value))
                    austatus = Common.AuditHistoryStatus_Names[model.AuditStatus.Value];

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
            audit.FormType = (int)ESP.Finance.Utility.FormType.ApplyForInvioce;
            return ESP.Finance.BusinessLogic.AuditLogManager.Add(audit);
        }

        protected void Bind_List()
        {
            string strWhere = " status!=@status and projectId=@projectId";
            List<System.Data.SqlClient.SqlParameter> parms = new List<System.Data.SqlClient.SqlParameter>();
            parms.Add(new System.Data.SqlClient.SqlParameter("@projectId", projectid));
            parms.Add(new System.Data.SqlClient.SqlParameter("@status", ESP.Finance.Utility.ApplyForInvioceStatus.Status.Wait_Submit));
            gvList.DataSource = ESP.Finance.BusinessLogic.ApplyForInvioceManager.GetList(strWhere, parms).OrderByDescending(x => x.CreateDate).ToList();
            gvList.DataBind();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request["backurl"]);
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            string ids = Request["chkId"];
            ESP.Finance.BusinessLogic.ApplyForInvioceManager.UpdateStatus(ids, ESP.Finance.Utility.ApplyForInvioceStatus.Status.Audited);
            addAuditLog(AuditHistoryStatus.ApplyForInvioceAudit_Audited);

            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
            {
                try
                {
                    projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
                    ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
                    SendMailHelper.SendMailApplyForInvioceAudit(projectModel.ProjectCode, projectModel.ApplicantUserEmail, CurrentUserName, "审核通过", txtAuditRemark.Text.Trim());
                }
                catch
                { }
            }

            Response.Redirect(Request["backurl"]);
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            string ids = Request["chkId"];
            ESP.Finance.BusinessLogic.ApplyForInvioceManager.UpdateStatus(ids, ESP.Finance.Utility.ApplyForInvioceStatus.Status.Rejected);
            addAuditLog(AuditHistoryStatus.ApplyForInvioceAudit_Rejected);

            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
            {
                try
                {
                    projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
                    ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
                    SendMailHelper.SendMailApplyForInvioceAudit(projectModel.ProjectCode, projectModel.ApplicantUserEmail, CurrentUserName,"审核驳回",txtAuditRemark.Text.Trim());
                }
                catch
                { }
            }

            Response.Redirect(Request["backurl"]);
        }
    }
}