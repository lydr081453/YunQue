using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using ESP.Finance.BusinessLogic;
using ESP.HumanResource.Entity;
using ESP.Framework.Entity;
public partial class project_ProjectWorkFlow : System.Web.UI.Page
{
    string type = string.Empty;
    int FlowID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = string.Empty;
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["Type"]))
            {
                type = Request["Type"];
            }
            if (!string.IsNullOrEmpty(Request["FlowID"]))
            {
                FlowID = Convert.ToInt32(Request["FlowID"]);
            }
            switch (type)
            {
                case "project":
                    this.litBiz.Text = GetBizScript(FlowID, 1);
                    this.litContract.Text = GetBizScript(FlowID, 2);
                    this.litFinance.Text = GetBizScript(FlowID, 3);
                    this.litControl.Text = GetBizScript(FlowID, 4);
                    break;
                case "supporter":
                    this.litBiz.Text = GetSupporterScript(FlowID, 1);
                    this.litContract.Text = GetSupporterScript(FlowID, 2);
                    this.litFinance.Text = GetSupporterScript(FlowID, 3);
                    this.litControl.Text = GetSupporterScript(FlowID, 4);
                    break;
                case "payment":
                    this.litBiz.Text = GetPaymentScript(FlowID, 1);
                    this.litContract.Text = GetPaymentScript(FlowID, 2);
                    this.litFinance.Text = GetPaymentScript(FlowID, 3);
                    this.litControl.Text = GetPaymentScript(FlowID, 4);
                    break;
                case "return":
                    this.litBiz.Text = GetReturnScript(FlowID, 1);
                    this.litContract.Text = GetReturnScript(FlowID, 2);
                    this.litFinance.Text = GetReturnScript(FlowID, 3);
                    this.litControl.Text = GetReturnScript(FlowID, 4);
                    break;
                case "oop":
                    this.litBiz.Text = GetOOPScript(FlowID, 1);
                    this.litContract.Text = "";
                    this.litFinance.Text = GetOOPScript(FlowID, 2);
                    this.litControl.Text = "";
                    break;
                case "consumption":
                    this.litBiz.Text = GetConsumptionScript(FlowID, 1,(int)ESP.Finance.Utility.FormType.Consumption);
                    this.litContract.Text = "";
                    this.litFinance.Text = GetConsumptionScript(FlowID, 3, (int)ESP.Finance.Utility.FormType.Consumption);
                    this.litControl.Text = "";
                    break;
                case "RebateRegistration":
                    this.litBiz.Text = GetConsumptionScript(FlowID, 1, (int)ESP.Finance.Utility.FormType.RebateRegistration);
                    this.litContract.Text = "";
                    this.litFinance.Text = GetConsumptionScript(FlowID, 3, (int)ESP.Finance.Utility.FormType.RebateRegistration);
                    this.litControl.Text = "";
                    break;
                case "refund":
                    this.litBiz.Text = GetRefundScript(FlowID, 1);
                    this.litContract.Text = "";
                    this.litFinance.Text = GetRefundScript(FlowID, 3);
                    this.litControl.Text = "";
                    break;
                case "requestForSeal":
                    this.litBiz.Text = GetConsumptionScript(FlowID, 1, (int)ESP.Finance.Utility.FormType.RequestForSeal);
                    this.litContract.Text = "";
                    this.litFinance.Text = "";
                    this.litControl.Text = "";
                    break;
                    
            }
        }
    }

    private string GetOOPScript(int id, int level)
    {
        string script = string.Empty;
        DataTable dt = ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.GetWorkflow(id ,level);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int status = 0;
            if (dt.Rows[i]["auditeStatus"] != DBNull.Value && !string.IsNullOrEmpty(dt.Rows[i]["auditeStatus"].ToString()))
            {
                status = int.Parse(dt.Rows[i]["auditeStatus"].ToString());
            }
            if (status == 2)//报销审批通过
                status = 1;
            else if (status > 2)
                status = 2;
            else
                status = 0;
            script += "<tr><td width='50%' height='40' align='right'><img style='cursor:hand' width='24' height='24' hspace='5' src='" + GetImage(status) + "' onclick='ShowMsg(\"" + getUserInfo(int.Parse(dt.Rows[i]["auditer"].ToString()), "") + "\");'/></td><td width='50%' align='left'>" + dt.Rows[i]["auditerName"].ToString() + this.GetDelegateUser(int.Parse(dt.Rows[i]["auditer"].ToString())) + "</td>";
        }
        script += "</tr> <tr><td align='right' bgcolor='#FFFFFF'></td> <td align='left' bgcolor='#FFFFFF'></td></tr>";
        return script;
    }
    private string GetPaymentScript(int id, int level)
    {
        List<SqlParameter> paramlist = new List<SqlParameter>();
        string term = string.Empty;
        string script = string.Empty;

        SqlParameter p = new SqlParameter("@PaymentID", SqlDbType.Int, 4);
        p.SqlValue = FlowID;
        paramlist.Add(p);

        switch (level)
        {
            case 1:
                term = " PaymentID=@PaymentID and AuditType<@AuditType ";
                SqlParameter p1 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
                p1.SqlValue = (int)auditorType.operationAudit_Type_Contract;
                paramlist.Add(p1);
                break;
            case 2:
                term = " PaymentID=@PaymentID and AuditType=@AuditType ";
                SqlParameter p2 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
                p2.SqlValue = (int)auditorType.operationAudit_Type_Contract;
                paramlist.Add(p2);
                break;
            case 3:
                term = " PaymentID=@PaymentID and (AuditType between @AuditType1 and @AuditType2) ";
                SqlParameter p3 = new SqlParameter("@AuditType1", SqlDbType.Int, 4);
                p3.SqlValue = (int)auditorType.operationAudit_Type_Financial;
                paramlist.Add(p3);
                SqlParameter p4 = new SqlParameter("@AuditType2", SqlDbType.Int, 4);
                p4.SqlValue = (int)auditorType.operationAudit_Type_Financial3;
                paramlist.Add(p4);
                break;
            case 4:
                term = " PaymentID=@PaymentID and AuditType=@AuditType ";
                SqlParameter p5 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
                p5.SqlValue = (int)auditorType.operationAudit_Type_RiskControl;
                paramlist.Add(p5);
                break;
        }

        IList<PaymentAuditHistInfo> auditlist = ESP.Finance.BusinessLogic.PaymentAuditHistManager.GetList(term, paramlist);
        foreach (PaymentAuditHistInfo model in auditlist)
        {
            script += "<tr><td width='50%' height='40' align='right'><img style='cursor:hand' width='24' height='24' hspace='5' src='" + GetImage(model.AuditeStatus.Value) + "' onclick='ShowMsg(\"" + getUserInfo(model.AuditorUserID.Value, model.Suggestion) + "\");'/></td><td width='50%' align='left'>" + model.AuditorEmployeeName + this.GetDelegateUser(model.AuditorUserID.Value) + "</td>";
        }
        script += "</tr> <tr><td align='right' bgcolor='#FFFFFF'></td> <td align='left' bgcolor='#FFFFFF'></td></tr>";
        return script;
    }

    private string GetRefundScript(int id, int level)
    {
        List<SqlParameter> paramlist = new List<SqlParameter>();
        string term = string.Empty;
        string script = string.Empty;

        SqlParameter p = new SqlParameter("@modelId", SqlDbType.Int, 4);
        p.SqlValue = FlowID;
        paramlist.Add(p);

        SqlParameter pModelType = new SqlParameter("@modelType", SqlDbType.Int, 4);
        pModelType.SqlValue = (int)ESP.Finance.Utility.FormType.Refund;
        paramlist.Add(pModelType);

        switch (level)
        {
            case 1:
                term = " modelId=@modelId and AuditType<@AuditType and modelType=@modelType ";
                SqlParameter p1 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
                p1.SqlValue = (int)auditorType.operationAudit_Type_Contract;
                paramlist.Add(p1);
                break;
            case 2:
                term = " modelId=@modelId and AuditType=@AuditType and modelType=@modelType ";
                SqlParameter p2 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
                p2.SqlValue = (int)auditorType.operationAudit_Type_Contract;
                paramlist.Add(p2);
                break;
            case 3:
                term = " modelId=@modelId and (AuditType between @AuditType1 and @AuditType2)  and modelType=@modelType ";
                SqlParameter p3 = new SqlParameter("@AuditType1", SqlDbType.Int, 4);
                p3.SqlValue = (int)auditorType.operationAudit_Type_Financial;
                paramlist.Add(p3);
                SqlParameter p4 = new SqlParameter("@AuditType2", SqlDbType.Int, 4);
                p4.SqlValue = (int)auditorType.operationAudit_Type_Financial3;
                paramlist.Add(p4);
                break;
            case 4:
                term = " modelId=@modelId and AuditType=@AuditType and modelType=@modelType ";
                SqlParameter p5 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
                p5.SqlValue = (int)auditorType.operationAudit_Type_RiskControl;
                paramlist.Add(p5);
                break;
        }

        IList<WorkFlowInfo> auditlist = ESP.Finance.BusinessLogic.WorkFlowManager.GetList(term, paramlist);
        foreach (WorkFlowInfo model in auditlist)
        {
            script += "<tr><td width='50%' height='40' align='right'><img style='cursor:hand' width='24' height='24' hspace='5' src='" + GetImage(model.AuditStatus) + "' onclick='ShowMsg(\"" + getUserInfo(model.AuditorUserID, model.Suggestion) + "\");'/></td><td width='50%' align='left'>" + model.AuditorEmployeeName + this.GetDelegateUser(model.AuditorUserID) + "</td>";
        }
        script += "</tr> <tr><td align='right' bgcolor='#FFFFFF'></td> <td align='left' bgcolor='#FFFFFF'></td></tr>";
        return script;
    }

    private string GetReturnScript(int id, int level)
    {
        List<SqlParameter> paramlist = new List<SqlParameter>();
        string term = string.Empty;
        string script = string.Empty;

        SqlParameter p = new SqlParameter("@ReturnID", SqlDbType.Int, 4);
        p.SqlValue = FlowID;
        paramlist.Add(p);

        switch (level)
        {
            case 1:
                term = " ReturnID=@ReturnID and AuditType<@AuditType ";
                SqlParameter p1 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
                p1.SqlValue = (int)auditorType.operationAudit_Type_Contract;
                paramlist.Add(p1);
                break;
            case 2:
                term = " ReturnID=@ReturnID and AuditType=@AuditType ";
                SqlParameter p2 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
                p2.SqlValue = (int)auditorType.operationAudit_Type_Contract;
                paramlist.Add(p2);
                break;
            case 3:
                term = " ReturnID=@ReturnID and (AuditType between @AuditType1 and @AuditType2)";
                SqlParameter p3 = new SqlParameter("@AuditType1", SqlDbType.Int, 4);
                p3.SqlValue = (int)auditorType.operationAudit_Type_Financial;
                paramlist.Add(p3);
                SqlParameter p4 = new SqlParameter("@AuditType2", SqlDbType.Int, 4);
                p4.SqlValue = (int)auditorType.operationAudit_Type_Financial3;
                paramlist.Add(p4);
                break;
            case 4:
                term = " ReturnID=@ReturnID and AuditType=@AuditType ";
                SqlParameter p5 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
                p5.SqlValue = (int)auditorType.operationAudit_Type_RiskControl;
                paramlist.Add(p5);
                break;
        }

        IList<ReturnAuditHistInfo> auditlist = ESP.Finance.BusinessLogic.ReturnAuditHistManager.GetList(term, paramlist);
        foreach (ReturnAuditHistInfo model in auditlist)
        {
            script += "<tr><td width='50%' height='40' align='right'><img style='cursor:hand' width='24' height='24' hspace='5' src='" + GetImage(model.AuditeStatus.Value) + "' onclick='ShowMsg(\"" + getUserInfo(model.AuditorUserID.Value, model.Suggestion) + "\");'/></td><td width='50%' align='left'>" + model.AuditorEmployeeName + this.GetDelegateUser(model.AuditorUserID.Value) + "</td>";
        }
        script += "</tr> <tr><td align='right' bgcolor='#FFFFFF'></td> <td align='left' bgcolor='#FFFFFF'></td></tr>";
        return script;
    }


    private string GetSupporterScript(int id, int level)
    {
        List<SqlParameter> paramlist = new List<SqlParameter>();
        string term = string.Empty;
        string script = string.Empty;

        SqlParameter p = new SqlParameter("@SupporterID", SqlDbType.Int, 4);
        p.SqlValue = FlowID;
        paramlist.Add(p);

        switch (level)
        {
            case 1:
                term = " SupporterID=@SupporterID and AuditType<@AuditType ";
                SqlParameter p1 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
                p1.SqlValue = (int)auditorType.operationAudit_Type_Contract;
                paramlist.Add(p1);
                break;
            case 2:
                term = " SupporterID=@SupporterID and AuditType=@AuditType ";
                SqlParameter p2 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
                p2.SqlValue = (int)auditorType.operationAudit_Type_Contract;
                paramlist.Add(p2);
                break;
            case 3:
                term = " SupporterID=@SupporterID and (AuditType between @AuditType1 and @AuditType2)";
                SqlParameter p3 = new SqlParameter("@AuditType1", SqlDbType.Int, 4);
                p3.SqlValue = (int)auditorType.operationAudit_Type_Financial;
                paramlist.Add(p3);
                SqlParameter p4 = new SqlParameter("@AuditType2", SqlDbType.Int, 4);
                p4.SqlValue = (int)auditorType.operationAudit_Type_Financial3;
                paramlist.Add(p4);
                break;
            case 4:
                term = " SupporterID=@SupporterID and AuditType=@AuditType ";
                SqlParameter p5 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
                p5.SqlValue = (int)auditorType.operationAudit_Type_RiskControl;
                paramlist.Add(p5);
                break;
        }



        IList<SupporterAuditHistInfo> auditlist = ESP.Finance.BusinessLogic.SupporterAuditHistManager.GetList(term, paramlist);
        foreach (SupporterAuditHistInfo model in auditlist)
        {
            script += "<tr><td width='50%' height='40' align='right'><img style='cursor:hand' width='24' height='24' hspace='5' src='" + GetImage(model.AuditStatus.Value) + "' onclick='ShowMsg(\"" + getUserInfo(model.AuditorUserID.Value, model.Suggestion) + "\");'/></td><td width='50%' align='left'>" + model.AuditorEmployeeName + this.GetDelegateUser(model.AuditorUserID.Value) + "</td>";
        }
        script += "</tr> <tr><td align='right' bgcolor='#FFFFFF'></td> <td align='left' bgcolor='#FFFFFF'></td></tr>";
        return script;
    }
    private string GetBizScript(int id, int level)
    {
        List<SqlParameter> paramlist = new List<SqlParameter>();
        string term = string.Empty;
        string script = string.Empty;

        SqlParameter p = new SqlParameter("@ProjectID", SqlDbType.Int, 4);
        p.SqlValue = FlowID;
        paramlist.Add(p);

        switch (level)
        {
            case 1:
                term = " ProjectID=@ProjectID and AuditType<@AuditType ";
                SqlParameter p1 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
                p1.SqlValue = (int)auditorType.operationAudit_Type_Contract;
                paramlist.Add(p1);
                break;
            case 2:
                term = " ProjectID=@ProjectID and AuditType=@AuditType ";
                SqlParameter p2 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
                p2.SqlValue = (int)auditorType.operationAudit_Type_Contract;
                paramlist.Add(p2);
                break;
            case 3:
                term = " ProjectID=@ProjectID and (AuditType between @AuditType1 and @AuditType2)";
                SqlParameter p3 = new SqlParameter("@AuditType1", SqlDbType.Int, 4);
                p3.SqlValue = (int)auditorType.operationAudit_Type_Financial;
                paramlist.Add(p3);
                SqlParameter p4 = new SqlParameter("@AuditType2", SqlDbType.Int, 4);
                p4.SqlValue = (int)auditorType.operationAudit_Type_Financial3;
                paramlist.Add(p4);
                break;
            case 4:
                term = " ProjectID=@ProjectID and AuditType=@AuditType ";
                SqlParameter p5 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
                p5.SqlValue = (int)auditorType.operationAudit_Type_RiskControl;
                paramlist.Add(p5);
                break;
        }



        IList<AuditHistoryInfo> auditlist = ESP.Finance.BusinessLogic.AuditHistoryManager.GetList(term, paramlist);
        foreach (AuditHistoryInfo model in auditlist)
        {
            string suggestions = string.Empty;
            IList<ESP.Finance.Entity.AuditLogInfo> logList = null;

            if (!string.IsNullOrEmpty(Request["Type"]))
            {
                type = Request["Type"];
            }
            switch (type)
            {
                case "project":
                    logList = ESP.Finance.BusinessLogic.AuditLogManager.GetProjectList(model.ProjectID.Value).Where(N => N.AuditorSysID == model.AuditorUserID).ToList();
                    break;
                case "supporter":
                    logList = ESP.Finance.BusinessLogic.AuditLogManager.GetSupporterList(model.ProjectID.Value).Where(N => N.AuditorSysID == model.AuditorUserID).ToList();
                    break;
                case "payment":
                    logList = ESP.Finance.BusinessLogic.AuditLogManager.GetPaymentList(model.ProjectID.Value).Where(N => N.AuditorSysID == model.AuditorUserID).ToList();
                    break;
                case "return":
                    logList = ESP.Finance.BusinessLogic.AuditLogManager.GetReturnList(model.ProjectID.Value).Where(N => N.AuditorSysID == model.AuditorUserID).ToList();
                    break;
            }
            if (logList != null && logList.Count > 0)
            {
                foreach (ESP.Finance.Entity.AuditLogInfo log in logList)
                {
                    suggestions += log.Suggestion + "<br/>";
                }
            }
            else
            {
                suggestions = model.Suggestion;
            }
            script += "<tr><td width='50%' height='40' align='right'><img style='cursor:hand' width='24' height='24' hspace='5' src='" + GetImage(model.AuditStatus.Value) + "' onclick='ShowMsg(\"" + getUserInfo(model.AuditorUserID.Value, suggestions) + "\");'/></td><td width='50%' align='left'>" + model.AuditorEmployeeName + this.GetDelegateUser(model.AuditorUserID.Value) + "</td>";
        }
        script += "</tr> <tr><td align='right' bgcolor='#FFFFFF'></td> <td align='left' bgcolor='#FFFFFF'></td></tr>";
        return script;
    }

    private string GetConsumptionScript(int id, int level,int formType)
    {
        List<SqlParameter> paramlist = new List<SqlParameter>();
        string term = string.Empty;
        string script = string.Empty;

        SqlParameter p = new SqlParameter("@BatchId", SqlDbType.Int, 4);
        p.SqlValue = FlowID;
        paramlist.Add(p);

        SqlParameter p2 = new SqlParameter("@FormType", SqlDbType.Int, 4);
        p2.SqlValue = formType;
        paramlist.Add(p2);

        switch (level)
        {
            case 1:
                term = " BatchId=@BatchId and FormType=@FormType and AuditType<@AuditType ";
                SqlParameter p1 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
                p1.SqlValue = (int)auditorType.operationAudit_Type_Financial;
                paramlist.Add(p1);
                break;
            case 3:
                term = " BatchId=@BatchId and FormType=@FormType and AuditType>=@AuditType ";
                SqlParameter p3 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
                p3.SqlValue = (int)auditorType.operationAudit_Type_Financial;
                paramlist.Add(p3);
                break;
        }

        IList<ConsumptionAuditInfo> auditlist = ESP.Finance.BusinessLogic.ConsumptionAuditManager.GetList(term, paramlist);
        foreach (ConsumptionAuditInfo model in auditlist)
        {
            string suggestions = string.Empty;
            IList<ESP.Finance.Entity.AuditLogInfo> logList = null;

 
            logList = ESP.Finance.BusinessLogic.AuditLogManager.GetConsumptionList(model.BatchID).Where(N => N.AuditorSysID == model.AuditorUserID).ToList();
            
            if (logList != null && logList.Count > 0)
            {
                foreach (ESP.Finance.Entity.AuditLogInfo log in logList)
                {
                    suggestions += log.Suggestion + "<br/>";
                }
            }
            else
            {
                suggestions = model.Suggestion;
            }
            script += "<tr><td width='50%' height='40' align='right'><img style='cursor:hand' width='24' height='24' hspace='5' src='" + GetImage(model.AuditStatus.Value) + "' onclick='ShowMsg(\"" + getUserInfo(model.AuditorUserID, suggestions) + "\");'/></td><td width='50%' align='left'>" + model.AuditorEmployeeName + this.GetDelegateUser(model.AuditorUserID) + "</td>";
        }
        script += "</tr> <tr><td align='right' bgcolor='#FFFFFF'></td> <td align='left' bgcolor='#FFFFFF'></td></tr>";
        return script;
    }


    private string getUserInfo(int userid, string auditdesc)
    {
        string script = string.Empty;
        EmployeeBaseInfo emp = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(userid);
        IList<EmployeePositionInfo> ep = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(userid);
        ESP.Finance.Entity.DepartmentViewInfo dept = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(ep[0].DepartmentID);

        //2009年6月10日修改，处理audit description的html显示问题，为了适应javascript输出
        if (auditdesc != null && auditdesc.Trim().Length > 0)
        {
            auditdesc = auditdesc.Trim();
            auditdesc = HttpUtility.HtmlEncode(auditdesc);
            auditdesc = auditdesc.Replace("\r\n", "<br/>");
        }
        else
        {
            auditdesc = "尚未审批或未留下任何审批信息";
        }

        script += "<ul><li>员工姓名：" + emp.FullNameCN + "[" + emp.Code + "]</li>";
        script += "<li>员工帐号：" + emp.Username + "</li>";
        script += "<li>移动电话：" + emp.MobilePhone + "</li>";
        script += "<li>电子邮箱：" + emp.InternalEmail + "</li>";
        script += "<li>所属部门：" + dept.level1 + "-" + dept.level2 + "-" + dept.level3 + "</li>";
        script += "<li>工作地点：" + emp.WorkCity + "</li>";
        script += "<li>审批信息：" + auditdesc + "</li></ul>";
        return script;
    }
    private string GetImage(int AuditStatus)
    {
        string ret = string.Empty;
        switch (AuditStatus)
        {
            case (int)AuditHistoryStatus.UnAuditing:
                ret = "/images/WF_Waiting.gif' alt='待审批";
                break;
            case (int)AuditHistoryStatus.TerminateAuditing:
                ret = "/images/WF_Reject.gif' alt='审批驳回,请重新编辑提交";
                break;
            case (int)AuditHistoryStatus.PassAuditing:
                ret = "/images/WF_Pass.gif' alt='审批通过";
                break;
            case (int)AuditHistoryStatus.WaitingContract:
                ret = "/images/WF_Contract.gif' alt='等待合同,请在项目变更中重新提交合同附件.";
                break;
        }
        return ret;
    }

    private string GetDelegateUser(int userid)
    {
        ESP.Framework.Entity.AuditBackUpInfo delegateModel = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(userid);
        string delegateuser = string.Empty;
        if (delegateModel != null)
        {
            delegateuser = "[代理人:" + new ESP.Compatible.Employee(delegateModel.BackupUserID).Name + "]";
        }
        return delegateuser;
    }
}
