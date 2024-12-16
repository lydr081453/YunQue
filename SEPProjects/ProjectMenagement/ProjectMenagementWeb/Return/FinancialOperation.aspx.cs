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
using AjaxPro;
public partial class Return_FinancialOperation : ESP.Web.UI.PageBase
{
    int PaymentID = 0;
    ESP.Finance.Entity.PaymentInfo PaymentModel = null;
    ESP.Finance.Entity.ProjectInfo ProjectModel = null;

    private IList<ESP.Finance.Entity.InvoiceDetailInfo> invoiceDetailList;

    int _projectID = 0;

    public int ProjectID
    {
        get { return _projectID; }
        set { _projectID = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Return_FinancialOperation));
        this.ddlPaymentType.Attributes.Add("onChange", "selectPaymentType(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");
        this.ddlBank.Attributes.Add("onChange", "selectBank(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");
        
        PaymentID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.PaymentID]);
        PaymentModel = ESP.Finance.BusinessLogic.PaymentManager.GetModel(PaymentID);

        this._projectID = PaymentModel.ProjectID;
        ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(PaymentModel.ProjectID);

        if (!string.IsNullOrEmpty(Request["Invoice"]) && Request["Invoice"].ToString().Trim() == "1")
        {
            btnPass.Visible = false;
            btnCancel.Visible = false;
        }

        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.PaymentID]))
            {
                InitPage(ProjectModel, PaymentModel);

                InitInvoiceDetailInfo();//add by chaochao
                this.lblLog.Text = GetAuditLog();
            }

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
        this.txtFactDate.Text = model.PaymentFactDate == null ? "" : model.PaymentFactDate.Value.ToString("yyyy-MM-dd");
        this.txtFactAmount.Text = model.PaymentFee.ToString("#,##0.00");
        string paymenttypeid = model.PaymentTypeID == null ? "" : model.PaymentTypeID.Value.ToString();
        this.txtRemark.Text = model.Remark;
        if (model.BankID != null)
            this.hidBankID.Value = model.BankID.ToString() + "," + model.BankName;
        this.lblAccount.Text = model.BankAccount;
        this.lblAccountName.Text = model.BankAccountName;
        this.lblBankAddress.Text = model.BankAddress;
        if (model.PaymentTypeID != null)
        {
            this.hidPaymentTypeID.Value = model.PaymentTypeID.Value.ToString() + "," + model.PaymentTypeName;
            this.txtPayCode.Text = model.PaymentTypeCode;
            ESP.Finance.Entity.PaymentTypeInfo typemodel = ESP.Finance.BusinessLogic.PaymentTypeManager.GetModel(model.PaymentTypeID.Value);
            if (typemodel.IsNeedCode == true)
            {
                this.lblPayCode.Style["display"] = "block";
                this.txtPayCode.Style["display"] = "block";
            }
            else
            {
                this.lblPayCode.Style["display"] = "none";
                this.txtPayCode.Style["display"] = "none";
            }
        }
        //if (!string.IsNullOrEmpty(model.InvoiceNo))
        //{
        //    this.txtInvoiceNo.Text = model.InvoiceNo;
        //    this.ddlInvoice.SelectedIndex = 2;
        //}
        this.txtCreditCode.Text = model.CreditCode;
        //this.txtDiffer.Text = model.USDDiffer.ToString("#,##0.00");

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
            term = " PaymentStatus=@PaymentStatus and PaymentID in (select PaymentID from F_PaymentAuditHist where (AuditorUserID=@AuditorUserID or AuditorUserID in(" + DelegateUsers + ")) and AuditType=@AuditType)";
        else
            term = " PaymentStatus=@PaymentStatus and PaymentID in (select PaymentID from F_PaymentAuditHist where AuditorUserID=@AuditorUserID and AuditType=@AuditType)";
        SqlParameter p1 = new SqlParameter("@PaymentStatus", SqlDbType.Int, 4);
        p1.SqlValue = (int)ReturnStatus.MajorCommit;
        paramlist.Add(p1);
        SqlParameter p2 = new SqlParameter("@AuditorUserID", SqlDbType.Int, 4);
        p2.SqlValue = Convert.ToInt32(CurrentUser.SysID);
        paramlist.Add(p2);
        SqlParameter p3 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
        p3.SqlValue = (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
        paramlist.Add(p3);
        IList<PaymentInfo> paymentList = ESP.Finance.BusinessLogic.PaymentManager.GetList(term, paramlist);
        foreach (PaymentInfo pro in paymentList)
        {
            if (pro.PaymentID.ToString() == Request[ESP.Finance.Utility.RequestName.PaymentID])
            {
                return true;
            }
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
        if (!string.IsNullOrEmpty(this.hidBankID.Value))
        {
            string[] strs = this.hidBankID.Value.Split(',');
            ESP.Finance.Entity.BankInfo model = ESP.Finance.BusinessLogic.BankManager.GetModel(Convert.ToInt32(strs[0]));
            PModel.BankID = model.BankID;
            PModel.BankName = model.BankName;
            PModel.BranchCode = model.BranchCode;
            PModel.BranchID = model.BranchID;
            PModel.BranchName = model.BranchName;
            PModel.DBCode = model.DBCode;
            PModel.DBManager = model.DBManager;
            PModel.BankAccount = model.BankAccount;
            PModel.BankAccountName = model.BankAccountName;
            PModel.BankAddress = model.Address;
        }
        if (!string.IsNullOrEmpty(this.hidPaymentTypeID.Value))
        {
            string[] strs = this.hidPaymentTypeID.Value.Split(',');
            PModel.PaymentTypeID = Convert.ToInt32(strs[0]);
            PModel.PaymentTypeName = strs[1];
            PModel.PaymentTypeCode = this.txtPayCode.Text;
        }
        PModel.InvoiceDate = DateTime.Now;
        PModel.Remark = this.txtRemark.Text;
        PModel.CreditCode = this.txtCreditCode.Text;

    }
    private int AuditOperation(int AuditStatus)
    {
        int paymentStatus = 0;
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.PaymentID]))
        {
            PaymentID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.PaymentID]);
            PaymentModel = ESP.Finance.BusinessLogic.PaymentManager.GetModel(PaymentID);
            ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(PaymentModel.ProjectID);
            if (ESP.Finance.BusinessLogic.CheckerManager.GetPaymentOddAmount(PaymentID) == 0)
                paymentStatus = (int)ReturnStatus.FinancialOver;
            else
            {
                paymentStatus = (int)ReturnStatus.FinancialHold;
            }
        }
        else
        {
            return 0;
        }
        string term = string.Empty;
        List<SqlParameter> paramlist = new List<SqlParameter>();
        string DelegateUsers = string.Empty;
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
        foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
        {
            DelegateUsers += model.UserID.ToString() + ",";
        }
        DelegateUsers = DelegateUsers.TrimEnd(',');

        SetPaymentModel(PaymentModel);
        PaymentModel.PaymentStatus = paymentStatus;
        UpdateResult result = ESP.Finance.BusinessLogic.PaymentManager.Update(PaymentModel);

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
            SqlParameter p3 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
            p3.SqlValue = (int)auditorType.operationAudit_Type_Financial;
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (!ValidAudited())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }
        if (AuditOperation((int)AuditHistoryStatus.TerminateAuditing) == 1)
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

    protected void btnRet_Click(object sender, EventArgs e)
    {
        InitInvoiceDetailInfo();
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

    public void InitInvoiceDetailInfo()
    {

        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.PaymentID]))
        {
            if (PaymentModel == null)
            {
                PaymentID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.PaymentID]);
                PaymentModel = ESP.Finance.BusinessLogic.PaymentManager.GetModel(PaymentID);
                this._projectID = PaymentModel.ProjectID;
                if (ProjectModel == null)
                {
                    ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(PaymentModel.ProjectID);
                }
            }
            invoiceDetailList = ESP.Finance.BusinessLogic.InvoiceDetailManager.GetListByPayment(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.PaymentID]), null, null);
            if (invoiceDetailList != null && invoiceDetailList.Count == 0)
            {
                InvoiceDetailInfo model = new InvoiceDetailInfo();
                invoiceDetailList.Add(model);
            }
            this.gvInvoiceDetail.DataSource = invoiceDetailList;
            this.gvInvoiceDetail.DataBind();
            BindTotal(invoiceDetailList);
        }

    }

    protected void gvInvoiceDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInvoiceDetail.PageIndex = e.NewPageIndex;
    }

    protected void gvInvoiceDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int invoicedetail = int.Parse(e.CommandArgument.ToString());
                ESP.Finance.BusinessLogic.InvoiceDetailManager.Delete(invoicedetail);

            }
            InitInvoiceDetailInfo();
        }

    }

    protected void gvInvoiceDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblNo = (Label)e.Row.FindControl("lblNo");
            lblNo.Text = (e.Row.RowIndex + 1).ToString();
            ESP.Finance.Entity.InvoiceDetailInfo item = (ESP.Finance.Entity.InvoiceDetailInfo)e.Row.DataItem;
            Label lblPaymentBudget = (Label)e.Row.FindControl("lblPaymentBudget");
            if (lblPaymentBudget != null && item.Amounts != null)
            {
                lblPaymentBudget.Text = Convert.ToDecimal(item.Amounts).ToString("#,##0.00");
            }

            Label lblDiffer = (Label)e.Row.FindControl("lblDiffer");
            if (lblDiffer != null && item.Amounts != null)
            {
                lblDiffer.Text = Convert.ToDecimal(item.USDDiffer == null ? 0 : item.USDDiffer.Value).ToString("#,##0.00");
            }

            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            if (lnkDelete != null)
            {
                if (item.InvoiceDetailID == 0)
                {
                    lnkDelete.Visible = false;
                }
            }
        }
    }

    [AjaxPro.AjaxMethod]
    public static List<List<string>> GetPayments()
    {
        List<List<string>> retlists = new List<List<string>>();
        IList<PaymentTypeInfo> paylist = ESP.Finance.BusinessLogic.PaymentTypeManager.GetList(null, null);
        List<string> first = new List<string>();
        first.Add("-1");
        first.Add("请选择..");
        retlists.Add(first);
        foreach (PaymentTypeInfo item in paylist)
        {
            List<string> i = new List<string>();
            i.Add(item.PaymentTypeID.ToString());
            i.Add(item.PaymentTypeName);
            retlists.Add(i);
        }

        return retlists;
    }

    [AjaxPro.AjaxMethod]
    public static List<List<string>> GetBanks(int returnid)
    {
        PaymentInfo model = ESP.Finance.BusinessLogic.PaymentManager.GetModel(returnid);
        string branchcode = string.Empty;
        if (model != null && !string.IsNullOrEmpty(model.ProjectCode))
        {
            branchcode = model.ProjectCode.Substring(0, 1);
        }
        else
        {
            ProjectInfo pmodel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(model.ProjectID);
            branchcode = pmodel.BranchCode;
        }

        List<List<string>> retlists = new List<List<string>>();
        List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
        paramlist.Add(new System.Data.SqlClient.SqlParameter("@branchcode", branchcode));
        IList<BankInfo> paylist = ESP.Finance.BusinessLogic.BankManager.GetList(" branchcode=@branchcode", paramlist);
        List<string> first = new List<string>();
        first.Add("-1");
        first.Add("请选择..");
        retlists.Add(first);
        foreach (BankInfo item in paylist)
        {
            List<string> i = new List<string>();
            i.Add(item.BankID.ToString());
            i.Add(item.BankName);
            retlists.Add(i);
        }

        return retlists;
    }

    [AjaxPro.AjaxMethod]
    public static List<string> GetPaymentTypeModel(int paymentTypeID)
    {
        List<string> list = new List<string>();
        ESP.Finance.Entity.PaymentTypeInfo model = ESP.Finance.BusinessLogic.PaymentTypeManager.GetModel(paymentTypeID);
        list.Add(model.PaymentTypeID.ToString());
        list.Add(model.PaymentTypeName);
        list.Add(model.IsNeedCode.ToString());

        return list;
    }

    [AjaxPro.AjaxMethod]
    public static List<string> GetBankModel(int bankID)
    {
        List<string> list = new List<string>();
        ESP.Finance.Entity.BankInfo bankmodel = ESP.Finance.BusinessLogic.BankManager.GetModel(bankID);
        list.Add(bankmodel.BankID.ToString());
        list.Add(bankmodel.BankName);
        list.Add(bankmodel.BankAccount);
        list.Add(bankmodel.BankAccountName);
        list.Add(bankmodel.Address);

        return list;
    }

    private string GetBackUrl()
    {
        return string.IsNullOrEmpty(Request["BackUrl"]) ? "/project/FinancialAuditOperation.aspx" : Request["BackUrl"];
    }
}

