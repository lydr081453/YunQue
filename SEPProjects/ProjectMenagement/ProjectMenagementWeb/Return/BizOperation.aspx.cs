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
using ESP.Finance.BusinessLogic;

public partial class Return_BizOperation : ESP.Web.UI.PageBase
{
    int PaymentID = 0;
    ESP.Finance.Entity.PaymentInfo PaymentModel = null;
    ESP.Finance.Entity.ProjectInfo ProjectModel = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        PaymentID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.PaymentID]);
        PaymentModel = ESP.Finance.BusinessLogic.PaymentManager.GetModel(PaymentID);
        ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(PaymentModel.ProjectID);

        if (!IsPostBack)
        {
            InitPage(ProjectModel, PaymentModel);
            this.lblLog.Text = GetAuditLog();

        }
    }


    private bool ValidAudited()
    {
        string term = string.Empty;
        List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
        string DelegateUsers = string.Empty;
        IList<PaymentInfo> paymentList = null;
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
        foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
        {
            DelegateUsers += model.UserID.ToString() + ",";
        }
        DelegateUsers = DelegateUsers.TrimEnd(',');
        if (!string.IsNullOrEmpty(DelegateUsers))
        {
            term = " PaymentStatus=@PaymentStatus and PaymentID in (select PaymentID from F_PaymentAuditHist where (AuditorUserID=@AuditorUserID or AuditorUserID in (" + DelegateUsers + ")) and AuditType=@AuditType)";
        }
        else
        {
            term = " PaymentStatus=@PaymentStatus and PaymentID in (select PaymentID from F_PaymentAuditHist where AuditorUserID=@AuditorUserID and AuditType=@AuditType)";
        }
        SqlParameter p1 = new SqlParameter("@PaymentStatus", SqlDbType.Int, 4);
        p1.SqlValue = (int)ReturnStatus.Submit;
        paramlist.Add(p1);
        SqlParameter p2 = new SqlParameter("@AuditorUserID", SqlDbType.Int, 4);
        p2.SqlValue = Convert.ToInt32(CurrentUser.SysID);
        paramlist.Add(p2);
        SqlParameter p3 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
        p3.SqlValue = (int)ESP.Finance.Utility.auditorType.operationAudit_Type_ZJSP;
        paramlist.Add(p3);
        if (!string.IsNullOrEmpty(term))
        {
            paymentList = ESP.Finance.BusinessLogic.PaymentManager.GetList(term, paramlist);
        }
        foreach (PaymentInfo pro in paymentList)
        {
            if (pro.PaymentID.ToString() == Request[ESP.Finance.Utility.RequestName.PaymentID])
            {
                return true;
            }
        }
        return false;
    }
    private void InitPage(ESP.Finance.Entity.ProjectInfo pmodel, ESP.Finance.Entity.PaymentInfo model)
    {
        this.lblBizType.Text = pmodel.BusinessTypeName;
        this.lblBranchCode.Text = pmodel.BranchCode;
        this.lblBranchName.Text = pmodel.BranchName;
        this.lblContractStatus.Text = pmodel.ContractStatusName;
        this.lblGroupName.Text = pmodel.GroupName;
        this.lblPaymentAmount.Text = model.PaymentBudget.Value.ToString("#,##0.00");
        this.lblPaymentCircle.Text = pmodel.OtherRequest;
        this.lblPaymentCode.Text = model.PaymentCode;
        this.lblPaymentContent.Text = model.PaymentContent;
        this.lblPaymentPreDate.Text = model.PaymentPreDate.ToString("yyyy-MM-dd");
        this.lblProjectCode.Text = pmodel.ProjectCode;
        this.lblProjectName.Text = pmodel.BusinessDescription;
        this.lblProjectType.Text = pmodel.ProjectTypeName;
        this.lblResponser.Text = pmodel.ApplicantEmployeeName;
        lblResponser.Attributes["onclick"] = "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(pmodel.ApplicantUserID) + "');";
        ESP.Compatible.Employee emp = new ESP.Compatible.Employee(pmodel.ApplicantUserID);
        this.lblResponserEmail.Text = emp.EMail;
        this.lblResponserMobile.Text = emp.Mobile;
        this.lblResponserTel.Text = emp.Telephone;
        this.lblFactDate.Text = model.PaymentFactDate == null ? "" : model.PaymentFactDate.Value.ToString("yyyy-MM-dd");
        this.lblFactFee.Text = model.PaymentFee.ToString("#,##0.00");
        string paymenttypeid = model.PaymentTypeID == null ? "" : model.PaymentTypeID.Value.ToString();
        this.lblPaymentType.Text = model.PaymentTypeName;
        this.lblRemark.Text = model.Remark;
    }

    protected void btnPass_Click(object sender, EventArgs e)
    {
        if (!ValidAudited())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }
        if (AuditOperation((int)ReturnStatus.MajorCommit, (int)AuditHistoryStatus.PassAuditing) == 1)
        {
            ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('付款通知审批成功!');window.location.href='" + GetBackUrl() + "';", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('付款通知审批失败!');", true);
        }
    }

    private int AuditOperation(int paymentStatus, int AuditStatus)
    {
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.PaymentID]))
        {
            PaymentID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.PaymentID]);
            PaymentModel = ESP.Finance.BusinessLogic.PaymentManager.GetModel(PaymentID);
            ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(PaymentModel.ProjectID);
        }
        string term = string.Empty;
        List<SqlParameter> paramlist = new List<SqlParameter>();

        PaymentModel.PaymentStatus = paymentStatus;
        UpdateResult result = ESP.Finance.BusinessLogic.PaymentManager.Update(PaymentModel);
        if (result == UpdateResult.Succeed)
        {
            term = " PaymentID=@PaymentID and AuditType=@AuditType and AuditeStatus=@AuditeStatus";
            SqlParameter p1 = new SqlParameter("@PaymentID", SqlDbType.Int, 4);
            p1.SqlValue = Request[ESP.Finance.Utility.RequestName.PaymentID];
            paramlist.Add(p1);
            //SqlParameter p2 = new SqlParameter("@AuditorUserID", SqlDbType.Int, 4);
            //p2.SqlValue = CurrentUser.SysID;
            //paramlist.Add(p2);
            SqlParameter p3 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
            p3.SqlValue = (int)auditorType.operationAudit_Type_ZJSP;
            paramlist.Add(p3);
            SqlParameter p4 = new SqlParameter("@AuditeStatus", SqlDbType.Int, 4);
            p4.SqlValue = (int)AuditHistoryStatus.UnAuditing;
            paramlist.Add(p4);
            IList<ESP.Finance.Entity.PaymentAuditHistInfo> auditList = ESP.Finance.BusinessLogic.PaymentAuditHistManager.GetList(term, paramlist);
            if (auditList != null && auditList.Count > 0)
            {
                ESP.Finance.Entity.PaymentAuditHistInfo auditmodel = auditList[0];
                if (auditmodel.AuditorUserID.Value != Convert.ToInt32(CurrentUser.SysID))
                {
                    auditmodel.Suggestion = this.txtSuggestion.Text + "[" + CurrentUser.Name + "为" + auditmodel.AuditorEmployeeName + "的代理人]";
                }
                else
                {
                    auditmodel.Suggestion = this.txtSuggestion.Text;
                }
                auditmodel.AuditeStatus = AuditStatus;
                auditmodel.AuditeDate = DateTime.Now;

                ESP.Finance.BusinessLogic.PaymentAuditHistManager.Update(auditmodel);

                //ESP.Finance.Entity.AuditLogInfo logModel = new AuditLogInfo();
                //logModel.AuditDate = DateTime.Now;
                //logModel.AuditorEmployeeName = CurrentUser.Name;
                //logModel.AuditorSysID = Convert.ToInt32(CurrentUser.SysID);
                //logModel.AuditorUserCode = CurrentUser.ID;
                //logModel.AuditorUserName = CurrentUser.ITCode;
                //logModel.AuditStatus = AuditStatus;
                //logModel.FormID = PaymentModel.PaymentID;
                //logModel.FormType = (int)ESP.Finance.Utility.FormType.Payment;
                //logModel.Suggestion = auditmodel.Suggestion;
                //ESP.Finance.BusinessLogic.AuditLogManager.Add(logModel);
            }

            return 1;
        }
        else
        {
            return 0;
        }
    }

    private string GetAuditLog()
    {
        IList<ESP.Finance.Entity.AuditLogInfo> histList = ESP.Finance.BusinessLogic.AuditLogManager.GetPaymentList(PaymentModel.PaymentID);

        System.Text.StringBuilder log = new System.Text.StringBuilder();

        foreach (var l in histList)
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (!ValidAudited())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }
        if (AuditOperation((int)ReturnStatus.Save, (int)AuditHistoryStatus.TerminateAuditing) == 1)
        {
            ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('付款通知驳回成功!');window.location.href='" + GetBackUrl() + "';", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('付款通知驳回失败!');", true);
        }
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetBackUrl());
    }

    private string GetBackUrl()
    {
        return string.IsNullOrEmpty(Request["BackUrl"]) ? "/project/OperationAuditList.aspx" : Request["BackUrl"];
    }
}
