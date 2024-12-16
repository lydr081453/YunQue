using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Data.SqlClient;
using System.Data;

public partial class Purchase_PurchaseAduit : ESP.Web.UI.PageBase
{
    int returnId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ReturnID]))
        {
            returnId = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]);
        }
        if (!IsPostBack)
        {
            BindInfo();
        }
    }

    private void BindInfo()
    {
        //   string DelegateUsers = ",";
        //IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
        //foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
        //{
        //    DelegateUsers += model.UserID.ToString() + ",";
        //}
        ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
        ESP.Purchase.Entity.GeneralInfo generalModel = null;
        if (returnModel != null && returnModel.PRID != null)
        {
            generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(returnModel.PRID.Value);
            TopMessage.Model = generalModel;

            // ESP.Purchase.Entity.OrderInfo orderModel = ESP.Purchase.BusinessLogic.OrderInfoManager.GetModelByGeneralID(generalModel.id);
            ESP.Purchase.Entity.PaymentPeriodInfo paymentPeriod = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetModelByPN(returnModel.ReturnID);


            if (generalModel.PRType == 6 && generalModel.HaveInvoice == false && paymentPeriod.TaxTypes != 0)
            {
                double total = ESP.Purchase.BusinessLogic.OrderInfoManager.TaxCalculator(double.Parse(returnModel.PreFee.Value.ToString()), 2);
                lblTaxDesc.Text = "个税金额:" + (double.Parse(returnModel.PreFee.Value.ToString()) - total).ToString() + ";     税后支付金额:" + total.ToString();
            }

        }

        //if (ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorName"].ToString().ToLower() == CurrentUser.ITCode.ToLower() && returnModel.ReturnStatus != (int)PaymentStatus.PurchaseMajor2 && DelegateUsers.IndexOf(","+returnModel.PaymentUserID.Value.ToString()+",")<0)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('您不是该PN单当前审核人，请等待" + returnModel.PaymentEmployeeName + "进行审核。');window.location='PurchaseList.aspx'", true);
        //    return;
        //}

        lblApplicant.Text = returnModel.RequestEmployeeName;
        lblApplicant.Attributes["onclick"] = "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(returnModel.RequestorID.Value) + "');";
        lblBeginDate.Text = returnModel.PreBeginDate == null ? "" : returnModel.PreBeginDate.Value.ToString("yyyy-MM-dd");
        // lblEndDate.Text = returnModel.PreEndDate == null ? "" : returnModel.PreEndDate.Value.ToString("yyyy-MM-dd");
        lblInceptDate.Text = returnModel.ReturnPreDate == null ? "" : returnModel.ReturnPreDate.Value.ToString("yyyy-MM-dd");
        lblInceptPrice.Text = returnModel.PreFee == null ? "" : returnModel.PreFee.Value.ToString("#,##0.00");
        lblPeriodType.Text = returnModel.PaymentTypeName;
        lblPayRemark.Text = returnModel.ReturnContent;
        lblPRNo.Text = returnModel.PRNo;
        lblProjectCode.Text = returnModel.ProjectCode;
        lblReturnCode.Text = returnModel.ReturnCode;
        lblStatus.Text = ESP.Finance.Utility.ReturnPaymentType.ReturnStatusString(returnModel.ReturnStatus.Value, 0, returnModel.IsDiscount);
        if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.CommonPR || returnModel.ReturnType == null)
            lblPRNo.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + returnModel.PRID.ToString() + "'style='cursor: hand' target='_blank'>" + returnModel.PRNo + "</a>";
        else
            lblPRNo.Text = returnModel.PRNo;
        ESP.Finance.Entity.ReturnGeneralInfoListViewInfo vmodel = ESP.Finance.BusinessLogic.ReturnGeneralInfoListViewManager.GetModel(returnModel.ReturnID);
        //从重汇列表获取供应商信息
        IList<ESP.Finance.Entity.BankCancelInfo> cancelList = ESP.Finance.BusinessLogic.BankCancelManager.GetList(" ReturnID=" + returnModel.ReturnID.ToString() + " and (ordertype is null or ordertype=1 )");
        if (cancelList != null && cancelList.Count > 0)
        {
            this.lblSupplierName.Text = cancelList[cancelList.Count - 1].OldBankAccountName;
            this.lblSupplierBank.Text = cancelList[cancelList.Count - 1].NewBankName;
            this.lblSupplierAccount.Text = cancelList[cancelList.Count - 1].NewBankAccount;
        }
        else if (vmodel != null)
        {
            this.lblSupplierName.Text = vmodel.Account_name;
            this.lblSupplierBank.Text = vmodel.Account_bank;
            this.lblSupplierAccount.Text = vmodel.Account_number;
        }
        this.labAuditLog.Text = this.GetAuditLog(returnModel.ReturnID);
    }

    private string GetAuditLog(int Rid)
    {
        ReturnInfo ReturnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(Rid);
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

    protected void btnYes_Click(object sender, EventArgs e)
    {
        if (!ValidAudited() && ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorName"].ToString().ToLower() != CurrentUser.ITCode.ToLower())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }
        int currentAuditType = 0;
        int nextAuditType = 0;
        ESP.Framework.Entity.EmployeeInfo nextAuditor = null;
        ReturnInfo ReturnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
        bool isLast = false;
        if (ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorName"].ToString().ToLower() == CurrentUser.ITCode.ToLower())//daivd.duan专用
        {
            currentAuditType = (int)ESP.Finance.Utility.auditorType.purchase_major2;

            ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(ReturnModel.ProjectCode.Substring(0, 1));
            int FirstFinanceID = branchModel.FirstFinanceID;
            //增加部门判断,N的不同部门对应不同的第一级财务审批人
            ESP.Finance.Entity.BranchDeptInfo branchdept = ESP.Finance.BusinessLogic.BranchDeptManager.GetModel(branchModel.BranchID, ReturnModel.DepartmentID.Value);
            if (branchdept != null)
                FirstFinanceID = branchdept.FianceFirstAuditorID;

            nextAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(FirstFinanceID);
            ReturnModel.ReturnStatus = (int)PaymentStatus.MajorAudit;
            nextAuditType = (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
            isLast = true;
        }
        else
        {
            if (ReturnModel.ReturnStatus == (int)PaymentStatus.PurchaseFirst)//初审人
            {
                currentAuditType = (int)ESP.Finance.Utility.auditorType.purchase_first;

                nextAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorId"]));
                ReturnModel.ReturnStatus = (int)PaymentStatus.PurchaseMajor1;
                nextAuditType = (int)ESP.Finance.Utility.auditorType.purchase_major2;

            }
            //else
            //{
            //    currentAuditType = (int)ESP.Finance.Utility.auditorType.purchase_major2;
            //    nextAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorId"]));
               
            //        ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(ReturnModel.ProjectCode.Substring(0, 1));
            //        int FirstFinanceID = branchModel.FirstFinanceID;
            //        //增加部门判断,N的不同部门对应不同的第一级财务审批人
            //        ESP.Finance.Entity.BranchDeptInfo branchdept = ESP.Finance.BusinessLogic.BranchDeptManager.GetModel(branchModel.BranchID, ReturnModel.DepartmentID.Value);
            //        if (branchdept != null)
            //            FirstFinanceID = branchdept.FianceFirstAuditorID;

            //        nextAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(FirstFinanceID);
            //        ReturnModel.ReturnStatus = (int)PaymentStatus.MajorAudit;
            //        isLast = true;
            //        nextAuditType = (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
                
            //}
            else// if (ReturnModel.ReturnStatus == (int)PaymentStatus.PurchaseMajor2)//二级总监
            {
                currentAuditType = (int)ESP.Finance.Utility.auditorType.purchase_major2;

                ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(ReturnModel.ProjectCode.Substring(0, 1));
                int FirstFinanceID = branchModel.FirstFinanceID;
                //增加部门判断,N的不同部门对应不同的第一级财务审批人
                ESP.Finance.Entity.BranchDeptInfo branchdept = ESP.Finance.BusinessLogic.BranchDeptManager.GetModel(branchModel.BranchID, ReturnModel.DepartmentID.Value);
                if (branchdept != null)
                    FirstFinanceID = branchdept.FianceFirstAuditorID;

                nextAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(FirstFinanceID);
                ReturnModel.ReturnStatus = (int)PaymentStatus.MajorAudit;
                nextAuditType = (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
                isLast = true;
            }
        }

        ReturnModel.PaymentUserID = nextAuditor.UserID;
        ReturnModel.PaymentCode = nextAuditor.Code;
        ReturnModel.PaymentEmployeeName = nextAuditor.FullNameCN;
        ReturnModel.PaymentUserName = nextAuditor.FullNameEN;
        if (ESP.Finance.BusinessLogic.ReturnManager.Update(ReturnModel) == UpdateResult.Succeed)
        {
            SaveHistory(ReturnModel, (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing, nextAuditor.UserID, currentAuditType, nextAuditType);
            string exMail = string.Empty;
            try
            {
                //发送邮件
                ESP.Finance.Utility.SendMailHelper.SendMailReturnForPurchaseAduit(true, isLast, ReturnModel.ReturnID, ReturnModel.ReturnCode, CurrentUser.Name, nextAuditor.FullNameCN, ESP.Framework.BusinessLogic.EmployeeManager.Get(ReturnModel.RequestorID.Value).Email, nextAuditor.Email);
            }
            catch
            {
                exMail = "(邮件发送失败!)";
            }
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('审批通过！"+exMail+"');window.location='PurchaseList.aspx'", true);
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('审批失败！');", true);
        }
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {

        if (!ValidAudited() && ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorName"].ToString().ToLower() != CurrentUser.ITCode.ToLower())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
            return;
        }

        ReturnInfo ReturnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
        int currentAuditType = 0;
        if (ReturnModel.ReturnStatus == (int)PaymentStatus.PurchaseFirst)//初审人
            currentAuditType = (int)ESP.Finance.Utility.auditorType.purchase_first;
        else
            currentAuditType = (int)ESP.Finance.Utility.auditorType.purchase_major2;

        ReturnModel.ReturnStatus = (int)PaymentStatus.Save;
        ReturnModel.PaymentUserID = 0;
        ReturnModel.PaymentCode = "";
        ReturnModel.PaymentEmployeeName = "";
        ReturnModel.PaymentUserName = "";
        if (ESP.Finance.BusinessLogic.ReturnManager.Update(ReturnModel, true) == UpdateResult.Succeed)
        {
            SaveHistory(ReturnModel, (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing, 0, currentAuditType, 0);
            //发送邮件
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('审批驳回！');window.location='PurchaseList.aspx'", true);
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('审批失败！');", true);
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PurchaseList.aspx");
    }

    private void SaveHistory(ReturnInfo returnModel, int Status, int nextAuditor, int currentAudtiType, int nextAuditType)
    {
        //查询审批历史中是否存在当前审批人
        List<SqlParameter> paramlist = new List<SqlParameter>();
        string term = string.Empty;
        string DelegateUsers = string.Empty;
        string DelegateUserNames = string.Empty;
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
        foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
        {
            DelegateUsers += model.UserID.ToString() + ",";
            DelegateUserNames += new ESP.Compatible.Employee(model.UserID).Name;
        }
        DelegateUsers = DelegateUsers.TrimEnd(',');
        DelegateUserNames = DelegateUserNames.TrimEnd(',');

        if (!string.IsNullOrEmpty(DelegateUsers))
            term = " AuditType=@AuditType and (AuditorUserID=@AuditorUserID or AuditorUserID in(" + DelegateUsers + ")) and returnID=@ReturnID ";
        else
            term = " AuditType=@AuditType and AuditorUserID=@AuditorUserID and returnID=@ReturnID ";

        SqlParameter p1 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
        p1.Value = currentAudtiType;
        paramlist.Add(p1);
        SqlParameter p2 = new SqlParameter("@AuditorUserID", SqlDbType.Int, 4);
        p2.Value = CurrentUser.SysID;
        paramlist.Add(p2);
        SqlParameter p3 = new SqlParameter("@ReturnID", SqlDbType.Int, 4);
        p3.Value = returnModel.ReturnID;
        paramlist.Add(p3);
        IList<ESP.Finance.Entity.ReturnAuditHistInfo> auditList = ESP.Finance.BusinessLogic.ReturnAuditHistManager.GetList(term, paramlist);
        ReturnAuditHistInfo auditHist;
        if (auditList.Count == 0)
        {//如果不存在新建一个历史记录
            auditHist = new ReturnAuditHistInfo();
            auditHist.ReturnID = returnModel.ReturnID;
            auditHist.AuditorUserID = int.Parse(CurrentUser.SysID);
            auditHist.AuditeDate = DateTime.Now;
            auditHist.AuditeStatus = Status;
            if (!string.IsNullOrEmpty(DelegateUsers) && CurrentUser.Name != DelegateUserNames)
                auditHist.Suggestion = txtRemark.Text.Trim() + "[" + CurrentUser.Name + "为" + DelegateUserNames + "的代理人]";
            else
                auditHist.Suggestion = txtRemark.Text.Trim();
            auditHist.AuditorUserCode = CurrentUser.ID;
            auditHist.AuditorUserName = CurrentUser.ITCode;
            auditHist.AuditorEmployeeName = CurrentUser.Name;
            auditHist.AuditType = currentAudtiType;
            ESP.Finance.BusinessLogic.ReturnAuditHistManager.Add(auditHist);
            if (ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorName"].ToString().ToLower() == CurrentUser.ITCode.ToLower())//daivd.duan专用
            {
                //删除采购部人员未审核信息
                ESP.Finance.BusinessLogic.ReturnAuditHistManager.DeleteNotAudit(returnModel.ReturnID);
            }
        }
        else
        {//否则更新审批历史
            auditHist = auditList[0];
            auditHist.AuditorUserCode = CurrentUser.ID;
            auditHist.AuditorUserName = CurrentUser.ITCode;
            auditHist.AuditorEmployeeName = CurrentUser.Name;
            auditHist.AuditeDate = DateTime.Now;
            auditHist.AuditeStatus = Status;
            if (!string.IsNullOrEmpty(DelegateUsers) && CurrentUser.Name != auditList[0].AuditorEmployeeName)
                auditHist.Suggestion = txtRemark.Text.Trim() + "[" + CurrentUser.Name + "为" + auditList[0].AuditorEmployeeName + "的代理人]";
            else
                auditHist.Suggestion = txtRemark.Text.Trim();
            ESP.Finance.BusinessLogic.ReturnAuditHistManager.Update(auditHist);
        }

        term = " AuditType=@AuditType and AuditorUserID=@AuditorUserID and returnID=@ReturnID ";
        paramlist.Clear();
        p1 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
        p1.Value = nextAuditType;
        paramlist.Add(p1);
        p2 = new SqlParameter("@AuditorUserID", SqlDbType.Int, 4);
        p2.Value = nextAuditor;
        paramlist.Add(p2);
        paramlist.Add(p3);
        if (nextAuditor != 0 && returnModel.ReturnStatus != (int)ESP.Finance.Utility.PaymentStatus.Save)
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
                NextAuditHist.AuditType = nextAuditType;
                ESP.Finance.BusinessLogic.ReturnAuditHistManager.Add(NextAuditHist);
            }
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
        term = " returnid not in(select returnid from f_pnbatchrelation) ";
        if (!string.IsNullOrEmpty(DelegateUsers))
            term += " and (ReturnStatus=@status1 or  ReturnStatus=@status2 ) AND (PaymentUserID=@sysID or PaymentUserID in(" + DelegateUsers + "))";
        else
            term += " and (ReturnStatus=@status1 or  ReturnStatus=@status2 ) AND PaymentUserID=@sysID";
        SqlParameter p3 = new SqlParameter("@status2", System.Data.SqlDbType.Int, 4);
        p3.SqlValue = (int)PaymentStatus.PurchaseFirst;
        paramlist.Add(p3);
        SqlParameter p1 = new SqlParameter("@status1", System.Data.SqlDbType.Int, 4);
        p1.SqlValue = (int)PaymentStatus.PurchaseMajor1;
        paramlist.Add(p1);

        SqlParameter p6 = new SqlParameter("@sysID", System.Data.SqlDbType.Int, 4);
        p6.SqlValue = CurrentUser.SysID;
        paramlist.Add(p6);
        IList<ReturnInfo> returnlist = ESP.Finance.BusinessLogic.ReturnManager.GetList(term, paramlist);
        foreach (ReturnInfo pro in returnlist)
        {
            if (pro.ReturnID == returnId)
            {
                return true;
            }
        }
        return false;
    }
}
