using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;

public partial class ForeGift_financeAudit : ESP.Web.UI.PageBase
{
    int returnId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(ForeGift_financeAudit));
        this.ddlPaymentType.Attributes.Add("onChange", "selectPaymentType(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");
        this.ddlBank.Attributes.Add("onChange", "selectBank(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ReturnID]))
        {
            returnId = int.Parse(Request[ESP.Finance.Utility.RequestName.ReturnID]);
        }
        if (!IsPostBack)
        {
            this.lblPayCode.Style["display"] = "none";
            this.txtPayCode.Style["display"] = "none";
            BindInfo();
        }
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetBackUrl());
    }

    private void BindInfo()
    {
        ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
        this.lblLog.Text = this.GetAuditLog(returnId);
        ViewForeGift.BindInfo(returnModel);

        if (returnModel.BankID != null)
            this.hidBankID.Value = returnModel.BankID.ToString() + "," + returnModel.BankName;
        this.lblAccount.Text = returnModel.BankAccount;
        this.lblAccountName.Text = returnModel.BankAccountName;
        this.lblBankAddress.Text = returnModel.BankAddress;
        radioInvoice.SelectedValue = returnModel.IsInvoice == null ? "-1" : returnModel.IsInvoice.Value.ToString();
        if (returnModel.PaymentTypeID != null)
        {
            this.hidPaymentTypeID.Value = returnModel.PaymentTypeID.Value.ToString() + "," + returnModel.PaymentTypeName;
            this.txtPayCode.Text = returnModel.PaymentTypeCode;
        }
        this.txtFactFee.Text = returnModel.FactFee == null ? "" : returnModel.FactFee.Value.ToString("#,##0.00");

        if (returnModel.ReturnStatus == (int)PaymentStatus.MajorAudit)
        {
            this.txtFactFee.Text = returnModel.PreFee == null ? "" : returnModel.PreFee.Value.ToString("#,##0.00");
        }

        this.lblSupplierName.Text = returnModel.SupplierName;
        this.txtSupplierAccount.Text = returnModel.SupplierBankAccount;
        this.txtSupplierBank.Text = returnModel.SupplierBankName;

        if (returnModel.ReturnPreDate != null)
            txtReturnPreDate.Text = Convert.ToDateTime(returnModel.ReturnPreDate).ToString("yyyy-MM-dd");


        if (returnModel.ReturnStatus == (int)PaymentStatus.MajorAudit)
        {
            trNext.Visible = true;
        }
        else if (returnModel.ReturnStatus == (int)PaymentStatus.FinanceLevel3)
        {
            trNext.Visible = true;
            txtReturnPreDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtReturnPreDate.ReadOnly = true;
        }
        else
        {
            trNext.Visible = false;
            txtReturnPreDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtReturnPreDate.ReadOnly = true;
            this.txtFactFee.ReadOnly = false;
        }
    }

    private string GetAuditLog(int Rid)
    {
        ReturnInfo ReturnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(Rid);
        if (ReturnModel != null)
        {
            if (ReturnModel.PRID != null && ReturnModel.PRID.Value != 0)
                TopMessage.Model = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(ReturnModel.PRID.Value);
            if ((ReturnModel.PRID == null || ReturnModel.PRID.Value == 0) && ReturnModel.ProjectID != null && ReturnModel.ProjectID.Value != 0)
                TopMessage.ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(ReturnModel.ProjectID.Value);
        }
        IList<ESP.Finance.Entity.AuditLogInfo> histList = ESP.Finance.BusinessLogic.AuditLogManager.GetReturnList(ReturnModel.ReturnID);
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!ValidAudited())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ReturnID]))
        {
            ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]));
            if (this.radioInvoice.SelectedIndex >= 0)
                returnModel.IsInvoice = Convert.ToInt32(this.radioInvoice.SelectedValue);
            if (!string.IsNullOrEmpty(txtFactFee.Text.Trim()))
                returnModel.FactFee = Convert.ToDecimal(txtFactFee.Text.Trim());

            if (!string.IsNullOrEmpty(txtReturnPreDate.Text.Trim()))
                returnModel.ReturnPreDate = Convert.ToDateTime(txtReturnPreDate.Text);
            setModelInfo(returnModel);

            ReturnManager.Update(returnModel);

        }
    }


    private bool ValidAudited()
    {
        List<SqlParameter> paramlist = new List<SqlParameter>();
        string term = string.Empty;
        string DelegateUsers = string.Empty;
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
        foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
        {
            DelegateUsers += model.UserID.ToString() + ",";
        }
        DelegateUsers = DelegateUsers.TrimEnd(',');
        if (!string.IsNullOrEmpty(DelegateUsers))
            term = " (ReturnStatus=@status1 or  ReturnStatus=@status2 or ReturnStatus=@status3 or ReturnStatus=@status4 or ReturnStatus=@status5) AND (PaymentUserID=@sysID or PaymentUserID in(" + DelegateUsers + "))";
        else
            term = " (ReturnStatus=@status1 or  ReturnStatus=@status2 or ReturnStatus=@status3 or ReturnStatus=@status4 or ReturnStatus=@status5) AND PaymentUserID=@sysID";
        SqlParameter p3 = new SqlParameter("@status3", System.Data.SqlDbType.Int, 4);
        p3.SqlValue = (int)PaymentStatus.MajorAudit;
        paramlist.Add(p3);
        SqlParameter p1 = new SqlParameter("@status1", System.Data.SqlDbType.Int, 4);
        p1.SqlValue = (int)PaymentStatus.FinanceLevel1;
        paramlist.Add(p1);
        SqlParameter p2 = new SqlParameter("@status2", System.Data.SqlDbType.Int, 4);
        p2.SqlValue = (int)PaymentStatus.FinanceLevel2;
        paramlist.Add(p2);
        SqlParameter p4 = new SqlParameter("@status4", System.Data.SqlDbType.Int, 4);
        p4.SqlValue = (int)PaymentStatus.FinanceLevel3;
        paramlist.Add(p4);
        SqlParameter p5 = new SqlParameter("@status5", System.Data.SqlDbType.Int, 4);
        p5.SqlValue = (int)PaymentStatus.WaitReceiving;
        paramlist.Add(p5);

        SqlParameter p6 = new SqlParameter("@sysID", System.Data.SqlDbType.Int, 4);
        p6.SqlValue = CurrentUser.SysID;
        paramlist.Add(p6);
        IList<ReturnInfo> returnlist = ESP.Finance.BusinessLogic.ReturnManager.GetList(term, paramlist);
        foreach (ReturnInfo pro in returnlist)
        {
            if (pro.ReturnID.ToString() == Request[ESP.Finance.Utility.RequestName.ReturnID])
            {
                return true;
            }
        }
        return false;
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        if (!ValidAudited())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ReturnID]))
        {
            ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]));
            if (returnModel != null)
            {
                if (this.radioInvoice.SelectedIndex >= 0)
                    returnModel.IsInvoice = Convert.ToInt32(this.radioInvoice.SelectedValue);

                if (this.txtFactFee.Text == string.Empty)
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('实际付款金额必填!');", true);
                    return;
                }
                returnModel.FactFee = Convert.ToDecimal(txtFactFee.Text.Trim());
                if (this.txtReturnPreDate.Text == string.Empty)
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('预计付款时间必填!');", true);
                    return;
                }

                returnModel.ReturnPreDate = Convert.ToDateTime(txtReturnPreDate.Text);
                setModelInfo(returnModel);

                if (returnModel.ReturnStatus == (int)PaymentStatus.FinanceLevel1)
                {
                    //当在财务总监最终付款时，如果付款类型是现金借款的话，PN状态变成待报销
                        returnModel.ReturnStatus = (int)PaymentStatus.WaitReceiving;
                    //当在财务总监最终付款时，如果是押金或抵押金付款，PN状态变成待报销
                    if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ForeGift || returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_KillForeGift)
                        returnModel.ReturnStatus = (int)PaymentStatus.WaitReceiving;
                    else
                        returnModel.ReturnStatus = (int)PaymentStatus.FinanceComplete;
                }
                else if (returnModel.ReturnStatus == (int)PaymentStatus.WaitReceiving)
                {
                    returnModel.ReturnStatus = (int)PaymentStatus.FinanceComplete;
                }
                else if (returnModel.ReturnStatus == (int)PaymentStatus.FinanceLevel3)
                {
                    returnModel.ReturnStatus = (int)PaymentStatus.FinanceLevel1;
                }
                else if (returnModel.ReturnStatus == (int)PaymentStatus.MajorAudit)
                {
                    if (returnModel.PreFee >= 100000)
                    {
                        returnModel.ReturnStatus = (int)PaymentStatus.FinanceLevel3;
                    }
                    else
                    {
                        returnModel.ReturnStatus = (int)PaymentStatus.FinanceLevel1;
                    }
                }


                if (returnModel.ReturnStatus != (int)PaymentStatus.FinanceComplete && returnModel.ReturnStatus != (int)PaymentStatus.WaitReceiving)
                {
                    if (this.txtNextAuditor.Text == string.Empty || this.hidNextAuditor.Value == string.Empty)
                    {
                        ClientScript.RegisterStartupScript(typeof(string), "", "alert('下一级审核人必填!');", true);
                        return;
                    }
                    returnModel.PaymentEmployeeName = this.txtNextAuditor.Text;
                    returnModel.PaymentUserID = Convert.ToInt32(this.hidNextAuditor.Value);

                }
                int nextAuditorID = 0;
                if (!string.IsNullOrEmpty(hidNextAuditor.Value) && hidNextAuditor.Value != "0")
                    nextAuditorID = Convert.ToInt32(hidNextAuditor.Value);
                UpdateResult result = ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
                SaveHistory(returnModel, (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing, nextAuditorID);

                if (returnModel.ReturnStatus == (int)PaymentStatus.FinanceComplete)
                {
                    ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(returnModel.RequestorID.Value);
                    try
                    {
                        SendMailHelper.SendMailReturnComplete(returnModel, returnModel.RequestUserName, emp.Email);
                        ClientScript.RegisterStartupScript(typeof(string), "", "alert('审批成功!');", true);
                    }
                    catch (Exception ex)
                    {
                        ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + ex.Message + "');", true);
                    }
                }
                else
                {
                    if (result == UpdateResult.Succeed)
                    {
                        string exMail = string.Empty;
                        if (returnModel.PaymentUserID != null)
                        {
                            ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(returnModel.PaymentUserID.Value);
                            try
                            {
                                SendMailHelper.SendMailReturnFinanceOK(returnModel, CurrentUser.Name, emp.FullNameCN, emp.Email);
                            }
                            catch (Exception ex)
                            {
                                exMail = ex.Message;
                            }
                        }
                        if (exMail != string.Empty)
                        { 
                            exMail = "(邮件发送失败!)"; 
                        }
                        ClientScript.RegisterStartupScript(typeof(string), "", "alert('审批成功!"+exMail+"');", true);
                    }
                    else
                        ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + result.ToString() + "!');", true);
                }
                Response.Redirect(GetBackUrl());

            }
        }
    }

    private void setModelInfo(ReturnInfo returns)
    {
        if (!string.IsNullOrEmpty(this.hidBankID.Value))
        {
            string[] strs = this.hidBankID.Value.Split(',');
            ESP.Finance.Entity.BankInfo model = ESP.Finance.BusinessLogic.BankManager.GetModel(Convert.ToInt32(strs[0]));
            returns.BankID = model.BankID;
            returns.BankName = model.BankName;
            returns.BranchCode = model.BranchCode;
            returns.BranchID = model.BranchID;
            returns.BranchName = model.BranchName;
            returns.DBCode = model.DBCode;
            returns.DBManager = model.DBManager;
            returns.BankAccount = model.BankAccount;
            returns.BankAccountName = model.BankAccountName;
            returns.BankAddress = model.Address;
        }
        if (!string.IsNullOrEmpty(this.hidPaymentTypeID.Value))
        {
            string[] strs = this.hidPaymentTypeID.Value.Split(',');
            returns.PaymentTypeID = Convert.ToInt32(strs[0]);
            returns.PaymentTypeName = strs[1];
            returns.PaymentTypeCode = this.txtPayCode.Text;
        }
        if (this.radioInvoice.SelectedIndex >= 0)
            returns.IsInvoice = Convert.ToInt32(this.radioInvoice.SelectedValue);

        returns.SupplierBankName = txtSupplierBank.Text;
        returns.SupplierBankAccount = txtSupplierAccount.Text;
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        if (!ValidAudited())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ReturnID]))
        {
            ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]));
            returnModel.ReturnStatus = (int)PaymentStatus.Save;
            UpdateResult result = ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
            int nextAuditorID = 0;
            if (!string.IsNullOrEmpty(hidNextAuditor.Value) && hidNextAuditor.Value != "0")
                nextAuditorID = Convert.ToInt32(hidNextAuditor.Value);
            SaveHistory(returnModel, (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing, nextAuditorID);
            if (result == UpdateResult.Succeed)
            {
                ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(returnModel.RequestorID.Value);
                string exMail = string.Empty;
                try
                {
                    SendMailHelper.SendMailReturnReject(returnModel, CurrentUser.Name, returnModel.RequestUserName, emp.Email);
                }
                catch (Exception ex)
                {
                    exMail = ex.Message;
                }
                if (exMail != string.Empty)
                {
                    exMail = "(邮件发送失败!)";
                }
                //在此处增加给申请人fa送邮件的通知
                //if(returnModel.ReturnType!=ESP.Finance.Utility.)
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('已驳回成功!"+exMail+"');", true);
            }
            else
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + result.ToString() + "!');", true);
        }
        Response.Redirect(GetBackUrl());
    }

    protected void btnNoFinance_Click(object sender, EventArgs e)
    {
        if (!ValidAudited())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ReturnID]))
        {
            ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]));
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(returnModel.ProjectCode.Substring(0, 1)).FirstFinanceID);
            returnModel.PaymentUserID = Convert.ToInt32(emp.SysID);
            returnModel.PaymentUserName = emp.ITCode;
            returnModel.PaymentCode = emp.ID;
            returnModel.PaymentEmployeeName = emp.Name;
            returnModel.ReturnStatus = (int)PaymentStatus.MajorAudit;
            UpdateResult result = ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
            int nextAuditorID = 0;
            if (!string.IsNullOrEmpty(hidNextAuditor.Value) && hidNextAuditor.Value != "0")
                nextAuditorID = Convert.ToInt32(hidNextAuditor.Value);
            SaveHistory(returnModel, (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing, nextAuditorID);
            if (result == UpdateResult.Succeed)
            {
                string exMail = string.Empty;
                try
                {
                    SendMailHelper.SendMailReturnReject(returnModel, CurrentUser.Name, emp.Name, emp.EMail);
                }
                catch
                {
                    exMail = "(邮件发送失败!)";
                }
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('已驳回成功!"+exMail+"');", true);
            }
            else
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + result.ToString() + "!');", true);
        }
        Response.Redirect(GetBackUrl());
    }

    private void SaveHistory(ReturnInfo returnModel, int Status, int nextAuditor)
    {
        string term = " AuditType=@AuditType and AuditorUserID=@AuditorUserID and returnID=@ReturnID ";
        List<SqlParameter> paramlist = new List<SqlParameter>();
        SqlParameter p1 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
        p1.Value = (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
        paramlist.Add(p1);
        SqlParameter p2 = new SqlParameter("@AuditorUserID", SqlDbType.Int, 4);
        p2.Value = nextAuditor;
        paramlist.Add(p2);
        SqlParameter p3 = new SqlParameter("@ReturnID", SqlDbType.Int, 4);
        p3.Value = returnModel.ReturnID;
        paramlist.Add(p3);
        IList<ESP.Finance.Entity.ReturnAuditHistInfo> auditList;
        if (nextAuditor != 0 && returnModel.ReturnStatus != (int)ESP.Finance.Utility.PaymentStatus.MajorAudit && returnModel.ReturnStatus != (int)ESP.Finance.Utility.PaymentStatus.Save)
        {
            auditList = ESP.Finance.BusinessLogic.ReturnAuditHistManager.GetList(term, paramlist);
            if (auditList.Count == 0)
            {
                ReturnAuditHistInfo NextAuditHist = new ReturnAuditHistInfo();
                ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(nextAuditor);
                NextAuditHist.ReturnID = returnModel.ReturnID;
                NextAuditHist.AuditorUserID = emp.UserID;
                NextAuditHist.AuditorUserName = emp.Username;
                NextAuditHist.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                NextAuditHist.AuditorUserCode = emp.Code;
                NextAuditHist.AuditorEmployeeName = emp.FullNameCN;
                NextAuditHist.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
                ESP.Finance.BusinessLogic.ReturnAuditHistManager.Add(NextAuditHist);
            }
        }
        //查询审批历史中是否存在当前审批人
        paramlist.Clear();
        paramlist.Add(p1);
        p2 = new SqlParameter("@AuditorUserID", SqlDbType.Int, 4);
        p2.Value = CurrentUser.SysID;
        paramlist.Add(p2);
        paramlist.Add(p3);
        auditList = ESP.Finance.BusinessLogic.ReturnAuditHistManager.GetList(term, paramlist);
        ReturnAuditHistInfo auditHist;
        if (auditList.Count == 0)
        {//如果不存在新建一个历史记录
            auditHist = new ReturnAuditHistInfo();
            auditHist.ReturnID = returnModel.ReturnID;
            auditHist.AuditorUserID = int.Parse(CurrentUser.SysID);
            auditHist.AuditeDate = DateTime.Now;
            auditHist.AuditeStatus = Status;
            auditHist.Suggestion = txtRemark.Text.Trim();
            auditHist.AuditorUserCode = CurrentUser.ID;
            auditHist.AuditorUserName = CurrentUser.ITCode;
            auditHist.AuditorEmployeeName = CurrentUser.Name;
            auditHist.AuditType = ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
            ESP.Finance.BusinessLogic.ReturnAuditHistManager.Add(auditHist);
        }
        else
        {//否则更新审批历史
            auditHist = auditList[0];
            auditHist.AuditorUserCode = CurrentUser.ID;
            auditHist.AuditorUserName = CurrentUser.ITCode;
            auditHist.AuditorEmployeeName = CurrentUser.Name;
            auditHist.AuditeDate = DateTime.Now;
            auditHist.AuditeStatus = Status;
            auditHist.Suggestion = txtRemark.Text.Trim();
            ESP.Finance.BusinessLogic.ReturnAuditHistManager.Update(auditHist);
        }

        if (returnModel.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.MajorAudit)
        {//驳回到财务第一级
            term = " AuditType=@AuditType and ReturnID=@ReturnID";
            paramlist.Clear();
            p1 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
            p1.Value = (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
            paramlist.Add(p1);
            p2 = new SqlParameter("@ReturnID", SqlDbType.Int, 4);
            p2.Value = returnModel.ReturnID;
            paramlist.Add(p2);
            auditList = ESP.Finance.BusinessLogic.ReturnAuditHistManager.GetList(term, paramlist);
            foreach (ESP.Finance.Entity.ReturnAuditHistInfo model in auditList)
            {
                model.Suggestion = string.Empty;
                model.AuditeStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
                ESP.Finance.BusinessLogic.ReturnAuditHistManager.Update(model);
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
        ReturnInfo model = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnid);
        string branchcode = string.Empty;
        if (model != null && !string.IsNullOrEmpty(model.ProjectCode))
        {
            branchcode = model.ProjectCode.Substring(0, 1);
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
        list.Add(model.IsNeedBank.ToString());
        list.Add(model.Tag.ToString());
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
        return "/Purchase/" + (string.IsNullOrEmpty(Request["BackUrl"]) ? "PurchasePaymentList.aspx" : Request["BackUrl"]);
    }
}
