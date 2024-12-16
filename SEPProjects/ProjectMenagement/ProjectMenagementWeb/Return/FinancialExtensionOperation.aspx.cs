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
public partial class Return_FinancialExtensionOperation : ESP.Web.UI.PageBase
{
    int PaymentID = 0;
    ESP.Finance.Entity.PaymentInfo PaymentModel = null;
    ESP.Finance.Entity.ProjectInfo ProjectModel = null;

    int _projectID = 0;

    public int ProjectID
    {
        get { return _projectID; }
        set { _projectID = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        PaymentID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.PaymentID]);
        PaymentModel = ESP.Finance.BusinessLogic.PaymentManager.GetModel(PaymentID);
        this._projectID = PaymentModel.ProjectID;
        ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(PaymentModel.ProjectID);

        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.PaymentID]))
            {


                InitPage(ProjectModel, PaymentModel);
                this.lblLog.Text = this.GetAuditLog();
            }

        }
    }

    private void InitPage(ESP.Finance.Entity.ProjectInfo pmodel, ESP.Finance.Entity.PaymentInfo model)
    {
        if (pmodel.Customer != null)
        {
            txtCustomerCode.Text = pmodel.Customer.CustomerCode;
            txtAddressCode.Text = pmodel.Customer.AddressCode;
            txtNameCN1.Text = pmodel.Customer.NameCN1 + pmodel.Customer.NameCN2;
            txtNameEN1.Text = pmodel.Customer.NameEN1 + pmodel.Customer.NameEN2;
            txtShortEN.Text = pmodel.Customer.ShortEN;
            txtAddress1.Text = pmodel.Customer.Address1 + pmodel.Customer.Address2;
            txtContactName.Text = pmodel.Customer.ContactName;
            txtInvoiceTitle.Text = pmodel.Customer.InvoiceTitle;
            txtContactTel.Text = pmodel.Customer.ContactTel;
            txtContactEmail.Text = pmodel.Customer.ContactEmail;
            this.txtContactWebsite.Text = pmodel.Customer.Website;
        }
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
        string paymenttypeid = model.PaymentTypeID == null ? "" : model.PaymentTypeID.Value.ToString();
        this.txtRemark.Text = model.Remark;
        txtFactDate.Text = model.PaymentFactDate == null ? "" : model.PaymentFactDate.Value.ToString("yyyy-MM-dd");

        //原付款日期
        IList<PaymentExtensionInfo> extensioninfos = PaymentExtensionManager.GetList(" PaymentId=" + model.PaymentID);
        if (extensioninfos != null && extensioninfos.Count > 0)
            this.txtExtensionDate.Text = extensioninfos[extensioninfos.Count - 1].PaidDate.ToShortDateString();

    }

    private bool ValidAudited()
    {
        string term = string.Empty;
        List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
        string DelegateUsers = string.Empty;
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
        foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
        {
            DelegateUsers += model.UserID.ToString() + ",";
        }
        DelegateUsers = DelegateUsers.TrimEnd(',');

        if (!string.IsNullOrEmpty(DelegateUsers))
            term = " PaymentExtensionStatus in(1,2) and PaymentID in (select PaymentID from F_PaymentAuditHist where (AuditorUserID=@AuditorUserID or AuditorUserID in(" + DelegateUsers + ")))";
        else
            term = " PaymentExtensionStatus in(1,2) and PaymentID in (select PaymentID from F_PaymentAuditHist where AuditorUserID=@AuditorUserID)";

        SqlParameter p2 = new SqlParameter("@AuditorUserID", SqlDbType.Int, 4);
        p2.SqlValue = Convert.ToInt32(CurrentUser.SysID);
        paramlist.Add(p2);

        IList<PaymentInfo> paymentList = ESP.Finance.BusinessLogic.PaymentManager.GetList(term, paramlist);
        if (paymentList.Where(x => x.PaymentID == int.Parse(Request[ESP.Finance.Utility.RequestName.PaymentID])).Count() > 0)
        {
            return true;
        }
        return false;
    }

    protected void btnPass_Click(object sender, EventArgs e)
    {
        if (!ValidAudited())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }
        if (AuditOperation((int)AuditHistoryStatus.PassAuditing) == 1)
        {
            ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('付款通知审批成功!');window.location.href='" + GetBackUrl() + "';", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('付款通知审批失败!');", true);
        }
    }

    private void SetPaymentModel(ESP.Finance.Entity.PaymentInfo PModel)
    {

    }
    private int AuditOperation(int AuditStatus)
    {
        SqlParameter p3 = new SqlParameter("@AuditType", SqlDbType.Int, 4);

        if (PaymentModel.PaymentExtensionStatus == (int)PaymentExtensionStatus.Save)
        {
            PaymentModel.PaymentExtensionStatus = (int)PaymentExtensionStatus.PrepareAudit;  //变成总监已审批
            p3.SqlValue = (int)auditorType.operationAudit_Type_ZJSP;
        }
        else if (PaymentModel.PaymentExtensionStatus == (int)PaymentExtensionStatus.PrepareAudit)
        {
            p3.SqlValue = (int)auditorType.operationAudit_Type_Financial;
            PaymentModel.PaymentExtensionStatus = (int)PaymentExtensionStatus.FinanceAudit;  //变成财务已审批
            PaymentModel.PaymentPreDate = Convert.ToDateTime(this.txtExtensionDate.Text);
            PaymentModel.PaymentFactDate = Convert.ToDateTime(this.txtExtensionDate.Text);
        }
        UpdateResult result = ESP.Finance.BusinessLogic.PaymentManager.Update(PaymentModel);


        string term = string.Empty;
        List<SqlParameter> paramlist = new List<SqlParameter>();
        string DelegateUsers = string.Empty;
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
        foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
        {
            DelegateUsers += model.UserID.ToString() + ",";
        }
        DelegateUsers = DelegateUsers.TrimEnd(',');
        if (result == UpdateResult.Succeed)
        {
            if (!string.IsNullOrEmpty(DelegateUsers))
                term = " PaymentID=@PaymentID and (AuditorUserID=@AuditorUserID or AuditorUserID in (" + DelegateUsers + ")) and AuditType=@AuditType and AuditeStatus=@AuditeStatus";
            else
                term = " PaymentID=@PaymentID and AuditorUserID=@AuditorUserID and AuditType=@AuditType and AuditeStatus=@AuditeStatus";
            SqlParameter p1 = new SqlParameter("@PaymentID", SqlDbType.Int, 4);
            p1.SqlValue = Request[ESP.Finance.Utility.RequestName.PaymentID];
            paramlist.Add(p1);
            SqlParameter p2 = new SqlParameter("@AuditorUserID", SqlDbType.Int, 4);
            p2.SqlValue = CurrentUser.SysID;
            paramlist.Add(p2);
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
                auditmodel.AuditeDate = DateTime.Now;
                auditmodel.AuditeStatus = AuditStatus;
                ESP.Finance.BusinessLogic.PaymentAuditHistManager.Update(auditmodel);
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
        if (AuditOperation((int)AuditHistoryStatus.TerminateAuditing) == 1)
        {
            PaymentModel.PaymentExtensionStatus = 0;
            PaymentManager.Update(PaymentModel);
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

    private void BindTotal(IList<InvoiceDetailInfo> list)
    {
        this.trTotal.Visible = true;
        this.lblTotal.Text = "总计:";
        decimal total = 0;
        foreach (InvoiceDetailInfo detail in list)
        {
            total += detail.Amounts == null ? 0 : detail.Amounts.Value;
        }
        if (total == 0)
        {
            this.trTotal.Visible = false;
        }
        this.lblTotal.Text += total.ToString("#,##0.00");

        this.lblBlance.Text = "剩余:";
        decimal blance = 0;
        blance = PaymentModel.PaymentBudget == null ? 0 : PaymentModel.PaymentBudget.Value - total;
        this.lblBlance.Text += blance.ToString("#,##0.00");
    }


    private string GetBackUrl()
    {
        return string.IsNullOrEmpty(Request["BackUrl"]) ? "/project/FinancialAuditOperation.aspx" : Request["BackUrl"];
    }
}

