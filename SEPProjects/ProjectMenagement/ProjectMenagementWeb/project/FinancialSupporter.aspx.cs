using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using ESP.Finance.Entity;
public partial class project_FinancialSupporter : ESP.Finance.WebPage.ViewPageForSupporter
{

    string query = string.Empty;
    int supporterid = 0;
    ESP.Finance.Entity.SupporterInfo supporterModel;
    ESP.Finance.Entity.ProjectInfo projectModel;
    ESP.Finance.Entity.BranchInfo branchModel;

    protected void Page_Load(object sender, EventArgs e)
    {
        query = Request.Url.Query;
        supporterid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]);
        supporterModel = ESP.Finance.BusinessLogic.SupporterManager.GetModel(supporterid);
        projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(supporterModel.ProjectID);
        branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModel(projectModel.BranchID.Value);

        if (!IsPostBack)
        {
            if (!ValidateConfirm())
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
                Response.Redirect(GetBackUrl());
            }

            this.lblLog.Text = this.GetAuditLog(supporterid);

            TopMessage.ProjectModel = projectModel;
        }
    }

    private string GetUser()
    {
        string user = "," + ESP.Finance.BusinessLogic.BranchManager.GetProjectAccounters() + ",";
        string retuser = user;
        string[] users = user.Split(',');
        for (int i = 0; i < users.Length; i++)
        {
            if (!string.IsNullOrEmpty(users[i]))
            {
                ESP.Framework.Entity.AuditBackUpInfo model = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(Convert.ToInt32(users[i]));
                if (model != null)
                {
                    retuser += model.BackupUserID.ToString() + ",";
                }
            }
        }

        ESP.Finance.Entity.BranchProjectInfo branchProject = ESP.Finance.BusinessLogic.BranchProjectManager.GetModel(branchModel.BranchID, supporterModel.GroupID.Value);

        if (branchProject != null)
        {
            retuser += branchProject.AuditorID + ",";
        }

        return user;
    }

    private bool ValidateConfirm()
    {
        string term = string.Empty;
        List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
        paramlist.Clear();
        IList<ESP.Finance.Entity.BranchInfo> branchList = ESP.Finance.BusinessLogic.BranchManager.GetList(" ProjectAccounter=" + CurrentUser.SysID);
        string BranchCodes = string.Empty;
        IList<ESP.Framework.Entity.AuditBackUpInfo> Delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));

        IList<ESP.Finance.Entity.BranchInfo> DelegateList = null;
        foreach (ESP.Framework.Entity.AuditBackUpInfo model in Delegates)
        {
            DelegateList = ESP.Finance.BusinessLogic.BranchManager.GetList(" ProjectAccounter=" + model.UserID.ToString());
            foreach (ESP.Finance.Entity.BranchInfo bmodel in DelegateList)
            {
                branchList.Add(bmodel);
            }

            var branchProjectList = ESP.Finance.BusinessLogic.BranchProjectManager.GetList(model.UserID);

            foreach (var b in branchProjectList)
            {
                ESP.Finance.Entity.BranchInfo branch = ESP.Finance.BusinessLogic.BranchManager.GetModel(b.BranchId);
                branchList.Add(branch);
            }
        }

        var branchProjectList2 = ESP.Finance.BusinessLogic.BranchProjectManager.GetList(CurrentUserID);

        foreach (var b in branchProjectList2)
        {
            ESP.Finance.Entity.BranchInfo branch = ESP.Finance.BusinessLogic.BranchManager.GetModel(b.BranchId);
            branchList.Add(branch);
        }

        if (branchList != null && branchList.Count > 0)//PR公关北京上海广州--任媛
        {
             term = "  (Status=@Status or Status=@Status2) and ((";

            foreach (ESP.Finance.Entity.BranchInfo model in branchList)
            {
                term += "ProjectCode like '" + model.BranchCode + "%' or ";
            }
            term = term.Substring(0, term.Length - 3) + ")";

            //if (cuslist != null && cuslist.Count > 0)
            //{
            //    term += " or (ProjectCode like '" + cuslist[0].BranchCode + "-" + cuslist[0].CustomerCode + "-%')";
            //}
            term += ")";

            SqlParameter p1 = new SqlParameter("@Status", SqlDbType.Int, 4);
            p1.SqlValue = (int)ESP.Finance.Utility.Status.BizAuditComplete;
            paramlist.Add(p1);
            SqlParameter p3 = new SqlParameter("@Status2", SqlDbType.Int, 4);
            p3.SqlValue = (int)ESP.Finance.Utility.Status.FinanceAuditing;
            paramlist.Add(p3);

        }

        IList<SupporterInfo> listResult = ESP.Finance.BusinessLogic.SupporterManager.GetList(term, paramlist);
        foreach (SupporterInfo pro in listResult)
        {
            if (pro.SupportID.ToString() == Request[ESP.Finance.Utility.RequestName.SupportID])
            {
                return true;
            }
        }
        return false;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (!ValidateConfirm())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }
        query = query.AddParam(RequestName.Operate, "FinancialAudit");
        Response.Redirect("SupporterAuditEdit.aspx?" + query);
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        if (!ValidateConfirm())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }

        ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModel(projectModel.BranchID.Value);
        ESP.Framework.Entity.AuditBackUpInfo ProjectDelegate = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(branchModel.ProjectAccounter);
        ESP.Framework.Entity.AuditBackUpInfo FinalDelegate = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(branchModel.FinalAccounter);


        ESP.Finance.Entity.BranchProjectInfo branchProject = ESP.Finance.BusinessLogic.BranchProjectManager.GetModel(branchModel.BranchID, supporterModel.GroupID.Value);


        if ((CurrentUserID == branchModel.ProjectAccounter || CurrentUserID == branchProject.AuditorID || (ProjectDelegate != null && ProjectDelegate.BackupUserID == CurrentUserID)))//任媛戴琼角色的审批
        {
            supporterModel.Status = (int)Status.FinanceAuditComplete;
            ESP.Finance.BusinessLogic.SupporterManager.Update(supporterModel,true);//审批完成
            SetAuditHistory(supporterModel, ESP.Finance.Utility.auditorType.operationAudit_Type_Financial2, (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing);

            string exMail = string.Empty;
            try
            {
                //给负责人发送邮件通知
                SendMailHelper.SendMailSupporterComplete(supporterModel, supporterModel.ApplicantEmployeeName, getEmployeeEmailBySysUserId(Convert.ToInt32(supporterModel.ApplicantUserID)));
                //给项目负责人发送邮件通知
                SendMailHelper.SendMailSupporterComplete(supporterModel, projectModel.ApplicantEmployeeName, getEmployeeEmailBySysUserId(Convert.ToInt32(projectModel.ApplicantUserID)));
            }
            catch
            {
                exMail = "(邮件发送失败!)";            
            }
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='" + GetBackUrl() + "';alert('支持方财务审核成功！"+exMail+"');", true);

        }

    }


    protected void btnTip_Click(object sender, EventArgs e)
    {
        supporterid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]);
        ESP.Finance.Entity.AuditLogInfo audit = new ESP.Finance.Entity.AuditLogInfo();
        audit.FormID = supporterid;
        audit.Suggestion = this.txtAuditRemark.Text;
        audit.AuditDate = DateTime.Now;
        audit.AuditorSysID = int.Parse(CurrentUser.SysID);
        audit.AuditorUserCode = CurrentUser.ID;
        audit.AuditorEmployeeName = CurrentUser.Name;
        audit.AuditorUserName = CurrentUser.ITCode;
        audit.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.Tip;
        audit.FormType = (int)ESP.Finance.Utility.FormType.Supporter;
        int ret = ESP.Finance.BusinessLogic.AuditLogManager.Add(audit);
        if (ret > 0)
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='" + GetBackUrl() + "';alert('留言保存成功！');", true);

    }

    private void SetAuditHistory(ESP.Finance.Entity.SupporterInfo SupporterModel, int audittype, int auditstatus)
    {
        string term = string.Format(" SupporterID={0} and audittype={1} and auditStatus={2}", SupporterModel.SupportID, audittype, (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing);
        List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
        System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@projectid", System.Data.SqlDbType.Int, 4);
        p1.SqlValue = SupporterModel.SupportID;
        paramlist.Add(p1);
        IList<ESP.Finance.Entity.SupporterAuditHistInfo> auditlist = ESP.Finance.BusinessLogic.SupporterAuditHistManager.GetList(term);
        var tempList = auditlist.OrderBy(N => N.SquenceLevel).ToList();
        if (tempList != null && tempList.Count > 0)
        {
            ESP.Finance.Entity.SupporterAuditHistInfo audit = tempList[0];
            if (audit.AuditorUserID.Value != Convert.ToInt32(CurrentUser.SysID))
            {
                audit.Suggestion = this.txtAuditRemark.Text + "[" + CurrentUser.Name + "为" + audit.AuditorEmployeeName + "的代理人]";
            }
            else
            {
                audit.Suggestion = this.txtAuditRemark.Text;
            }
            audit.SupporterID = supporterModel.SupportID;
            audit.AuditStatus = auditstatus;
            audit.AuditDate = DateTime.Now;
            ESP.Finance.BusinessLogic.SupporterAuditHistManager.Update(audit);
        }
    }
    protected void btnTerminate_Click(object sender, EventArgs e)
    {
        if (!ValidateConfirm())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }

        //审批驳回
        supporterid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]);
        supporterModel = ESP.Finance.BusinessLogic.SupporterManager.GetModel(supporterid);
        ESP.Finance.Entity.ProjectInfo projectmodel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(supporterModel.ProjectID);

        ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModel(projectmodel.BranchID.Value);
        ESP.Framework.Entity.AuditBackUpInfo ProjectDelegate = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(branchModel.ProjectAccounter);
        ESP.Framework.Entity.AuditBackUpInfo FinalDelegate = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(branchModel.FinalAccounter);
        string exMail = string.Empty;

        if (CurrentUserID == branchModel.ProjectAccounter || (ProjectDelegate != null && ProjectDelegate.BackupUserID == CurrentUserID))//任媛戴琼角色的审批
        {
            supporterModel.Status = (int)Status.FinanceReject;
            ESP.Finance.BusinessLogic.SupporterManager.Update(supporterModel);//驳回
            SetAuditHistory(supporterModel, ESP.Finance.Utility.auditorType.operationAudit_Type_Financial2, (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing);
            try
            {
                //给支持方创建人和负责人发出驳回通知
                SendMailHelper.SendSupporterMailAuditFinancialReject(supporterModel, CurrentUserName, getEmployeeEmailBySysUserId(Convert.ToInt32(supporterModel.ApplicantUserID)));
                //给项目负责人发送邮件通知
                SendMailHelper.SendMailSupporterComplete(supporterModel, projectmodel.ApplicantEmployeeName, getEmployeeEmailBySysUserId(Convert.ToInt32(projectmodel.ApplicantUserID)));
            }
            catch
            {
                exMail = "(邮件发送失败!)";            
            }
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='" + GetBackUrl() + "';alert('支持方申请驳回完成！"+exMail+"');", true);

        }
        else if (CurrentUserID == branchModel.FinalAccounter || (FinalDelegate != null && FinalDelegate.BackupUserID == CurrentUserID))//由eddy审批
        {
            supporterModel.Status = (int)Status.FinanceReject;
            ESP.Finance.BusinessLogic.SupporterManager.Update(supporterModel);//驳回
            SetAuditHistory(supporterModel, ESP.Finance.Utility.auditorType.operationAudit_Type_Financial2, (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing);
            try
            {
                //给支持方创建人和负责人发出驳回通知
                SendMailHelper.SendSupporterMailAuditFinancialReject(supporterModel, CurrentUserName, getEmployeeEmailBySysUserId(Convert.ToInt32(supporterModel.ApplicantUserID)));
                //给项目负责人发送邮件通知
                SendMailHelper.SendMailSupporterComplete(supporterModel, projectmodel.ApplicantEmployeeName, getEmployeeEmailBySysUserId(Convert.ToInt32(projectmodel.ApplicantUserID)));
            }
            catch
            {
                exMail = "(邮件发送失败!)";
            }
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='" + GetBackUrl() + "';alert('支持方申请驳回完成！"+exMail+"');", true);

        }
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetBackUrl());
    }

    private string getEmployeeEmailBySysUserId(int SysUserId)
    {
        return new ESP.Compatible.Employee(SysUserId).EMail;
    }


    private string GetAuditLog(int sid)
    {
        supporterModel = ESP.Finance.BusinessLogic.SupporterManager.GetModel(sid);
        IList<ESP.Finance.Entity.AuditLogInfo> histList = ESP.Finance.BusinessLogic.AuditLogManager.GetSupporterList(supporterModel.SupportID);
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
            string auditdate = model.AuditDate == null ? "" : model.AuditDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
            loginfo += model.AuditorEmployeeName + "(" + model.AuditorUserName + ")" + austatus + "[" + auditdate + "]" + model.Suggestion + "<br/>";

        }
        return loginfo;
    }

    private string GetBackUrl()
    {
        return string.IsNullOrEmpty(Request["BackUrl"]) ? "/project/OperationAuditList.aspx" : Request["BackUrl"];
    }
}
