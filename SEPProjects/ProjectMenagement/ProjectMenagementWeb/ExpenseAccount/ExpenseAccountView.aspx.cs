/************************************************************************\
 * 审批页面
 *      
 * 不同角色进入后，页面显示权限不同，根据step参数区分
 * 
 * workitemid为工作项ID
 *
\************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using ESP.Finance.Entity;
using AjaxPro;
namespace FinanceWeb.ExpenseAccount
{
    public partial class ExpenseAccountView : ESP.Web.UI.PageBase
    {
        string BeiJingBranch = string.Empty;

        ESP.Finance.SqlDataAccess.WorkFlowDataContext dataContext = new ESP.Finance.SqlDataAccess.WorkFlowDataContext();
        ESP.Finance.Entity.ExpenseAccountExtendsInfo model = null;
        ESP.Finance.SqlDataAccess.WorkItem workitem = null;

        string ReturnUrl = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            BeiJingBranch = System.Configuration.ConfigurationManager.AppSettings["FinanceAuditBJBranch"];

            if (!string.IsNullOrEmpty(Request["workitemid"]))
            {
                AjaxPro.Utility.RegisterTypeForAjax(typeof(ExpenseAccountView));
                this.ddlBank.Attributes.Add("onChange", "selectBank(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");

                workitem = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowModel(int.Parse(Request["workitemid"]));
                model = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkItemModel(workitem.EntityId);

            }

            if (!IsPostBack)
            {
                BindInfo();
                IsFinanceAudit();
            }
        }


        protected void IsFinanceAudit()
        {
            if (workitem != null && !string.IsNullOrEmpty(workitem.WebPage))
            {
                if (workitem.WebPage.IndexOf("step=f1") > 0)
                {
                    btnAudit.Visible = false;
                    trNext.Visible = true;
                    btnUnAuditF.Visible = false;
                    trF3.Visible = false;
                }
                else if (workitem.WebPage.IndexOf("step=f2") > 0)
                {
                    btnAudit.Visible = false;
                    trNext.Visible = true;
                    trF3.Visible = false;
                }
                else if (workitem.WebPage.IndexOf("step=f3") > 0)
                {
                    btnAudit.Visible = false;
                    if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia)
                        trNext.Visible = true;
                    else
                        trNext.Visible = false;
                    trF3.Visible = false;
                }
                else if (workitem.WebPage.IndexOf("step=f4") > 0)
                {
                    btnAudit.Visible = false;
                    trNext.Visible = false;
                    trF3.Visible = false;
                }
                else
                {
                    btnAuditF.Visible = false;
                    btnUnAuditF.Visible = false;
                    trNext.Visible = false;
                    trF3.Visible = false;
                }
            }
        }

        protected void BindInfo()
        {

            if (model != null && workitem != null)
            {
                BindList();

                labPNcode.Text = model.ReturnCode;

                labRequestUserName.Text = model.RequestEmployeeName;
                labRequestUserCode.Text = model.RequestUserCode;
                labRequestDate.Text = model.RequestDate.Value.ToString("yyyy-MM-dd");
                radioInvoice.SelectedValue = model.IsInvoice == null ? "0" : model.IsInvoice.Value.ToString();
                ESP.Finance.Entity.ProjectInfo project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(model.ProjectID.Value);

                txtproject_code1.Text = model.ProjectCode;
                //如果是GM项目，不需要FA审批
                if (model.ProjectCode.IndexOf("GM*") >= 0 || model.ProjectCode.IndexOf("*GM") >= 0 || model.ProjectCode.IndexOf(ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN) >= 0)
                {
                    txtproject_descripttion.Text = "";
                }
                else
                {
                    txtproject_descripttion.Text = project.BusinessDescription;
                }
                //普通报销、现金借款显示个人银行信息
                if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccount || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCashBorrow)
                {
                    this.panBank.Visible = true;
                    //绑定个人银行卡号
                    ESP.HumanResource.Entity.EmployeeBaseInfo empModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(model.RequestorID.Value);
                    lblCreatorBank.Text = empModel.SalaryBank;
                    lblCreatorAccount.Text = empModel.SalaryCardNo;
                }
                labPreFee.Text = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(model.ReturnID).ToString();

                List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
                if (model.DepartmentID != null && model.DepartmentID != 0)
                {
                    depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(model.DepartmentID.Value, depList);
                    string groupname = string.Empty;
                    foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
                    {
                        groupname += dept.DepartmentName + "-";
                    }
                    if (!string.IsNullOrEmpty(groupname))
                        labDepartment.Text = groupname.Substring(0, groupname.Length - 1);
                }



                if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccount)
                    labReturnType.Text = "报销单";
                else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCashBorrow)
                    labReturnType.Text = "现金借款单";
                else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic)
                {
                    this.panInvoice.Visible = true;
                    labReturnType.Text = "支票/电汇付款单";
                    this.trCheckNo.Visible = true;
                    this.trCheckNo1.Visible = true;
                    this.trCheckNo2.Visible = true;
                    this.txtCheckNo.Text = model.PaymentTypeCode;
                    if (model.BankID != null)
                        this.hidBankID.Value = model.BankID.ToString() + "," + model.BankName;
                    this.lblAccount.Text = model.BankAccount;
                    this.lblAccountName.Text = model.BankAccountName;
                    this.lblBankAddress.Text = model.BankAddress;
                    this.txtCheckNo.Text = model.PaymentTypeCode;
                }
                else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountBusinessCard)
                    labReturnType.Text = "商务卡报销单";
                else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff)
                    labReturnType.Text = "PR现金借款冲销";
                else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty)
                    labReturnType.Text = "第三方报销单";
                else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia)
                {
                    labReturnType.Text = "媒体预付申请";
                }

                if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccount)
                {
                    txtFactFee.Value = Convert.ToDouble(model.PreFee.Value);
                    trConfirmFee.Visible = false;
                    labFactFeeName.Text = "实际支付金额:";
                }
                else
                {
                    trConfirmFee.Visible = true;
                    labFactFeeName.Text = "冲销金额:";
                    ESP.Finance.Entity.ExpenseAccountInfo confirmModel = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetModelByReturnID(model.ReturnID);
                    if (confirmModel != null)
                    {
                        labConfirmFee.Text = confirmModel.ConfirmFee.Value.ToString("0.00");
                    }
                    else
                    {
                        labConfirmFee.Text = "0.00";
                        trConfirmFee.Visible = false;
                    }
                }

                var auditLogs = ESP.Finance.BusinessLogic.AuditLogManager.GetOOPList(model.ReturnID);
                foreach (var log in auditLogs)
                {
                     labSuggestion.Text += log.AuditorEmployeeName + "(" + log.AuditorUserName + ")" + ";&nbsp;&nbsp;&nbsp;" + log.Suggestion + ";&nbsp;&nbsp;&nbsp;" + ((model.RequestorID.Value == log.AuditorSysID.Value) ? "提交" : ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Names[log.AuditStatus.Value]) + ";&nbsp;&nbsp;&nbsp;" + log.AuditDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>";
                }

            }

        }

        protected void BindList()
        {
            List<ESP.Finance.Entity.ExpenseAccountDetailInfo> list = null;
            if (model != null)
            {
                list = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(" and ReturnID = " + model.ReturnID);
            }
            else
            {
                list = new List<ESP.Finance.Entity.ExpenseAccountDetailInfo>();
            }

            gvG.DataSource = list;
            gvG.DataBind();
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect(GetBackUrl());
        }

        private string GetBackUrl()
        {
            return string.IsNullOrEmpty(Request["BackUrl"]) ? "/project/OperationAuditList.aspx" : Request["BackUrl"];
        }

        //protected void btnEditFA_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("ExpenseAccountEditFA.aspx"+Request.Url.Query);
        //}

        protected void btnAudit_Click(object sender, EventArgs e)
        {
            //设置审核日志记录
            ESP.Finance.Entity.ExpenseAuditDetailInfo logModel = new ESP.Finance.Entity.ExpenseAuditDetailInfo();
            logModel.AuditeDate = DateTime.Now;
            logModel.AuditorEmployeeName = CurrentUser.Name;
            logModel.AuditorUserID = Convert.ToInt32(CurrentUser.SysID);
            logModel.AuditorUserCode = CurrentUser.ID;
            logModel.AuditorUserName = CurrentUser.ITCode;
            logModel.AuditType = GetStep();
            logModel.ExpenseAuditID = model.ReturnID;
            logModel.AuditeStatus = (int)ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Passed;
            logModel.Suggestion = txtSuggestion.Text.Trim();

            int assigneeID = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetAssigneeIdByWorkItemID(workitem.WorkItemId, dataContext);

            if (assigneeID != Convert.ToInt32(CurrentUser.SysID))
            {
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(assigneeID);
                logModel.Suggestion += "(" + CurrentUser.Name + " 代理 " + emp.Name + ")";
            }

            //设置工作流委托方法的参数
            Dictionary<string, object> prarms = new Dictionary<string, object>() 
                { 
                    { "EntID", model.ReturnID } ,
                    { "ReturnStatus",  GetStatus()} ,
                    { "LogModel", logModel }
                };

                //调用工作流
                ESP.Workflow.WorkItemManager.CloseWorkItem(workitem.WorkItemId, assigneeID, "Approved", null, new Action<object>(ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus), prarms);

            //如果是业务审批最后一级审批通过，则记录时间
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowLastAuditPassTime(model.ReturnID, new ESP.Compatible.Employee(UserInfo.UserID));
            //支票电汇需要支票号

            if (returnModel != null)
            {
                returnModel.PaymentTypeCode = this.txtCheckNo.Text;
                if (!string.IsNullOrEmpty(this.hidBankID.Value))
                {
                    string[] strs = this.hidBankID.Value.Split(',');
                    ESP.Finance.Entity.BankInfo bankmodel = ESP.Finance.BusinessLogic.BankManager.GetModel(Convert.ToInt32(strs[0]));
                    returnModel.BankID = bankmodel.BankID;
                    returnModel.BankName = bankmodel.BankName;
                    returnModel.BranchCode = bankmodel.BranchCode;
                    returnModel.BranchID = bankmodel.BranchID;
                    returnModel.BranchName = bankmodel.BranchName;
                    returnModel.DBCode = bankmodel.DBCode;
                    returnModel.DBManager = bankmodel.DBManager;
                    returnModel.BankAccount = bankmodel.BankAccount;
                    returnModel.BankAccountName = bankmodel.BankAccountName;
                    returnModel.BankAddress = bankmodel.Address;
                }
                if (panInvoice.Visible == true)
                {
                    returnModel.IsInvoice = int.Parse(this.radioInvoice.SelectedValue);
                }
                ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);

            }

            //如果审批结束，则改变单据状态为审批结束
            if (ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowInstanceStatus(workitem.WorkflowInstanceId) == (int)ESP.Workflow.WorkflowProcessStatus.Closed)
            {
                if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia || (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic && (model.IsFixCheque == null || model.IsFixCheque == false)) || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountBusinessCard || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccount || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty )
                {
                    ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus(model.ReturnID, (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete, null);
                }
                else//现金借款:32,PR现金借款冲销34,借款冲销单36
                {
                    //支票电汇需要支票号
                    ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus(model.ReturnID, this.txtCheckNo.Text, (int)ESP.Finance.Utility.PaymentStatus.WaitReceiving, null);
                }
            }

            Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核通过操作成功！');window.location='" + GetBackUrl() + "';", true);


        }

        protected void btnUnAudit_Click(object sender, EventArgs e)
        {
            try
            {
                //设置审核日志记录
                ESP.Finance.Entity.ExpenseAuditDetailInfo logModel = new ESP.Finance.Entity.ExpenseAuditDetailInfo();
                logModel.AuditeDate = DateTime.Now;
                logModel.AuditorEmployeeName = CurrentUser.Name;
                logModel.AuditorUserID = Convert.ToInt32(CurrentUser.SysID);
                logModel.AuditorUserCode = CurrentUser.ID;
                logModel.AuditorUserName = CurrentUser.ITCode;
                logModel.AuditType = GetStep();
                logModel.ExpenseAuditID = model.ReturnID;
                logModel.AuditeStatus = (int)ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Reject;
                logModel.Suggestion = txtSuggestion.Text.Trim();

                int returnStatus = (int)ESP.Finance.Utility.PaymentStatus.Rejected;
                if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountWaitReceiving)
                {
                    returnStatus = (int)ESP.Finance.Utility.PaymentStatus.ConfirmReceiving;
                }

                int assigneeID = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetAssigneeIdByWorkItemID(workitem.WorkItemId, dataContext);

                if (assigneeID != Convert.ToInt32(CurrentUser.SysID))
                {
                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(assigneeID);
                    logModel.Suggestion += "(" + CurrentUser.Name + " 代理 " + emp.Name + ")";
                }

                //设置工作流委托方法的参数
                Dictionary<string, object> prarms = new Dictionary<string, object>() 
                { 
                    { "EntID", model.ReturnID } ,
                    { "ReturnStatus", returnStatus } ,
                    { "LogModel", logModel }
                };
                //调用工作流
                try
                {
                    ESP.Workflow.WorkItemManager.CloseWorkItem(workitem.WorkItemId, assigneeID, "Rejected", null, new Action<object>(ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus), prarms);
                }
                catch { }
                if (workitem != null && !string.IsNullOrEmpty(workitem.WebPage) && workitem.WebPage.IndexOf("step=f4") > 0)
                {
                    ESP.Finance.BusinessLogic.ExpenseAccountManager.WorkFlowReject(workitem.WorkflowInstanceId,model.ReturnID);
                    ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.Add(logModel);

                    AuditLogInfo loginfo = new AuditLogInfo();
                    loginfo.FormID = logModel.ExpenseAuditID;
                    loginfo.FormType = (int)ESP.Finance.Utility.FormType.ExpenseAccount;
                    loginfo.AuditorSysID = logModel.AuditorUserID;
                    loginfo.AuditorUserName = logModel.AuditorUserName;
                    loginfo.AuditorUserCode = logModel.AuditorUserCode;
                    loginfo.AuditorEmployeeName = logModel.AuditorEmployeeName;
                    loginfo.Suggestion = logModel.Suggestion;
                    loginfo.AuditDate = logModel.AuditeDate;
                    loginfo.AuditStatus = logModel.AuditeStatus;

                    ESP.Finance.BusinessLogic.AuditLogManager.Add(loginfo);
                }

                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核驳回操作成功！');window.location='" + GetBackUrl() + "';", true);

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核驳回操作失败，请重试！');", true);
                return;
            }
        }

        protected void btnAuditF_Click(object sender, EventArgs e)
        {

            if (trNext.Visible == true && string.IsNullOrEmpty(hidNextAuditor.Value))
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('请选择财务下级审批人！');", true);
                return;
            }

            //设置审核日志记录
            ESP.Finance.Entity.ExpenseAuditDetailInfo logModel = new ESP.Finance.Entity.ExpenseAuditDetailInfo();
            logModel.AuditeDate = DateTime.Now;
            logModel.AuditorEmployeeName = CurrentUser.Name;
            logModel.AuditorUserID = Convert.ToInt32(CurrentUser.SysID);
            logModel.AuditorUserCode = CurrentUser.ID;
            logModel.AuditorUserName = CurrentUser.ITCode;
            logModel.AuditType = GetStep();
            logModel.ExpenseAuditID = model.ReturnID;
            logModel.AuditeStatus = (int)ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Passed;
            logModel.Suggestion = txtSuggestion.Text.Trim();

            int assigneeID = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetAssigneeIdByWorkItemID(workitem.WorkItemId, dataContext);

            if (assigneeID != Convert.ToInt32(CurrentUser.SysID))
            {
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(assigneeID);
                logModel.Suggestion += "(" + CurrentUser.Name + "(" + CurrentUser.ITCode + ")" + " 代理 " + emp.Name + ")";
            }

            //设置工作流委托方法的参数
            Dictionary<string, object> prarms = new Dictionary<string, object>() 
                { 
                    { "EntID", model.ReturnID } ,
                    { "ReturnStatus",  GetStatus()} ,
                    { "LogModel", logModel }
                };

            //设置财务下级审批人参数
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(model.ReturnID);

            List<int> nextAuditerList = new List<int>();
            if (trNext.Visible == true)
            {
                nextAuditerList.Add(Convert.ToInt32(hidNextAuditor.Value));
            }
            Dictionary<string, object> nextAuditer = null;

            if (workitem != null && !string.IsNullOrEmpty(workitem.WebPage) && workitem.WebPage.IndexOf("step=f1") > 0)
            {
                if (model.PreFee >= 100000 || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia)
                {
                    nextAuditer = new Dictionary<string, object>() 
                    { 
                        { "Finance2Id" ,  nextAuditerList.ToArray() }
                    };
                }
                else
                {
                    nextAuditer = new Dictionary<string, object>() 
                    { 
                        { "Finance3Id" ,  nextAuditerList.ToArray() }
                    };
                }
            }

            if (workitem != null && !string.IsNullOrEmpty(workitem.WebPage) && workitem.WebPage.IndexOf("step=f2") > 0)
            {
                nextAuditer = new Dictionary<string, object>() 
                    { 
                        { "Finance3Id" ,  nextAuditerList.ToArray() }
                    };

            }

            if (workitem != null && !string.IsNullOrEmpty(workitem.WebPage) && workitem.WebPage.IndexOf("step=f3") > 0)
            {
                nextAuditer = new Dictionary<string, object>() 
                    { 
                        { "Finance4Id" ,  nextAuditerList.ToArray() }
                    };

            }

            returnModel.PaymentTypeCode = this.txtCheckNo.Text;

            if (!string.IsNullOrEmpty(this.hidBankID.Value))
            {
                string[] strs = this.hidBankID.Value.Split(',');
                ESP.Finance.Entity.BankInfo bankmodel = ESP.Finance.BusinessLogic.BankManager.GetModel(Convert.ToInt32(strs[0]));
                returnModel.BankID = bankmodel.BankID;
                returnModel.BankName = bankmodel.BankName;
                returnModel.BranchCode = bankmodel.BranchCode;
                returnModel.BranchID = bankmodel.BranchID;
                returnModel.BranchName = bankmodel.BranchName;
                returnModel.DBCode = bankmodel.DBCode;
                returnModel.DBManager = bankmodel.DBManager;
                returnModel.BankAccount = bankmodel.BankAccount;
                returnModel.BankAccountName = bankmodel.BankAccountName;
                returnModel.BankAddress = bankmodel.Address;
                returnModel.LastAuditPassTime = DateTime.Now;
            }

            if (panInvoice.Visible == true)
            {
                returnModel.IsInvoice = int.Parse(this.radioInvoice.SelectedValue);
            }

            ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);

            //调用工作流
            //try
            //{
                ESP.Workflow.WorkItemManager.CloseWorkItem(workitem.WorkItemId, assigneeID, "Approved", nextAuditer, new Action<object>(ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus), prarms);
            //}
            //catch
            //{ }
            //if (workitem != null && !string.IsNullOrEmpty(workitem.WebPage) && workitem.WebPage.IndexOf("step=f4") > 0)
            //{
            //    ESP.Finance.BusinessLogic.ExpenseAccountManager.CloseWorkFlow(workitem.WorkflowInstanceId,CurrentUserID);
            //    ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.Add(logModel);
            //}
            //如果审批结束，则改变单据状态为审批结束
            if (ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowInstanceStatus(workitem.WorkflowInstanceId) == (int)ESP.Workflow.WorkflowProcessStatus.Closed)
            {
                ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus(model.ReturnID, this.txtCheckNo.Text, this.GetStatus(), null);
            }



            Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核通过操作成功！');window.location='" + GetBackUrl() + "';", true);


        }

        protected void btnUnAuditF_Click(object sender, EventArgs e)
        {
            try
            {
                //设置审核日志记录
                ESP.Finance.Entity.ExpenseAuditDetailInfo logModel = new ESP.Finance.Entity.ExpenseAuditDetailInfo();
                logModel.AuditeDate = DateTime.Now;
                logModel.AuditorEmployeeName = CurrentUser.Name;
                logModel.AuditorUserID = Convert.ToInt32(CurrentUser.SysID);
                logModel.AuditorUserCode = CurrentUser.ID;
                logModel.AuditorUserName = CurrentUser.ITCode;
                logModel.AuditType = GetStep();
                logModel.ExpenseAuditID = model.ReturnID;
                logModel.AuditeStatus = (int)ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_RejectToF1;
                logModel.Suggestion = txtSuggestion.Text.Trim();

                int assigneeID = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetAssigneeIdByWorkItemID(workitem.WorkItemId, dataContext);

                if (assigneeID != Convert.ToInt32(CurrentUser.SysID))
                {
                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(assigneeID);
                    logModel.Suggestion += "(" + CurrentUser.Name + " 代理 " + emp.Name + ")";
                }

                //设置工作流委托方法的参数
                Dictionary<string, object> prarms = new Dictionary<string, object>() 
                { 
                    { "EntID", model.ReturnID } ,
                    { "ReturnStatus", (int)ESP.Finance.Utility.PaymentStatus.CEOAudit } ,
                    { "LogModel", logModel }
                };
                //调用工作流
                try
                {
                    ESP.Workflow.WorkItemManager.CloseWorkItem(workitem.WorkItemId, assigneeID, "Returned", null, new Action<object>(ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus), prarms);
                }
                catch { }
                if (workitem != null && !string.IsNullOrEmpty(workitem.WebPage) && workitem.WebPage.IndexOf("step=f4") > 0)
                {
                    ESP.Finance.BusinessLogic.ExpenseAccountManager.WorkFlowRejectToFinance1(workitem.WorkflowInstanceId,model.ReturnID);
                    ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.Add(logModel);

                    AuditLogInfo loginfo = new AuditLogInfo();
                    loginfo.FormID = logModel.ExpenseAuditID;
                    loginfo.FormType = (int)ESP.Finance.Utility.FormType.ExpenseAccount;
                    loginfo.AuditorSysID = logModel.AuditorUserID;
                    loginfo.AuditorUserName = logModel.AuditorUserName;
                    loginfo.AuditorUserCode = logModel.AuditorUserCode;
                    loginfo.AuditorEmployeeName = logModel.AuditorEmployeeName;
                    loginfo.Suggestion = logModel.Suggestion;
                    loginfo.AuditDate = logModel.AuditeDate;
                    loginfo.AuditStatus = logModel.AuditeStatus;
                    
                    ESP.Finance.BusinessLogic.AuditLogManager.Add(loginfo);
                }
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核驳回操作成功！');window.location='" + GetBackUrl() + "';", true);

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核驳回操作失败，请重试！');", true);
                return;
            }
        }


        protected void btnTip_Click(object sender, EventArgs e)
        {
            int ret = 0;
            ESP.Finance.Entity.ExpenseAuditDetailInfo logModel = new ESP.Finance.Entity.ExpenseAuditDetailInfo();
            logModel.AuditeDate = DateTime.Now;
            logModel.AuditorEmployeeName = CurrentUser.Name;
            logModel.AuditorUserID = Convert.ToInt32(CurrentUser.SysID);
            logModel.AuditorUserCode = CurrentUser.ID;
            logModel.AuditorUserName = CurrentUser.ITCode;
            logModel.AuditType = GetStep();
            logModel.ExpenseAuditID = model.ReturnID;
            logModel.AuditeStatus = (int)ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Tip;
            logModel.Suggestion = txtSuggestion.Text.Trim();
            ret = ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.Add(logModel);
            if (ret > 0)
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('留言添加成功！');window.location='" + GetBackUrl() + "';", true);

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string sum = string.Empty;
            if (Convert.ToInt32(model.ReturnType) != (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty && Convert.ToInt32(model.ReturnType) != (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic && Convert.ToInt32(model.ReturnType) != (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia)
            {
                sum = "Print/ExpensePrint.aspx?expenseID=" + model.ReturnID;
            }
            else
            {
                sum = "Print/ThirdPartyPrint.aspx?expenseID=" + model.ReturnID;
            }
            Page.ClientScript.RegisterStartupScript(typeof(string), "", "window.open('" + sum + "');", true);
        }

        #region
        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            BindList();
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.ExpenseAccountDetailInfo detail = (ESP.Finance.Entity.ExpenseAccountDetailInfo)e.Row.DataItem;
                ESP.Purchase.Entity.TypeInfo projectType = ESP.Purchase.BusinessLogic.TypeManager.GetModel(detail.CostDetailID.Value);

                Label labCostDetailName = (Label)e.Row.FindControl("labCostDetailName");
                if (projectType != null && !string.IsNullOrEmpty(projectType.typename))
                    labCostDetailName.Text = projectType.typename;
                else
                    labCostDetailName.Text = "OOP";

                Label labExpenseTypeName = (Label)e.Row.FindControl("labExpenseTypeName");
                string expenseTypeName = ESP.Finance.BusinessLogic.ExpenseTypeManager.GetModel(detail.ExpenseType.Value).ExpenseType;
                if (detail.ExpenseType.Value == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_BusinessTrip"]))
                {
                    labExpenseTypeName.Text = expenseTypeName + "(";
                    labExpenseTypeName.Text += detail.MealFeeMorning != null && detail.MealFeeMorning == 1 ? "早餐 " : "";
                    labExpenseTypeName.Text += detail.MealFeeNoon != null && detail.MealFeeNoon == 1 ? "午餐 " : "";
                    labExpenseTypeName.Text += detail.MealFeeNight != null && detail.MealFeeNight == 1 ? "晚餐" : "";
                    labExpenseTypeName.Text += ")";
                }
                else if (detail.ExpenseType.Value == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_Cellphone"]))
                {
                    string mobileInfo = string.Empty;
                    ESP.Finance.Entity.MobileListInfo mobile = ESP.Finance.BusinessLogic.MobileListManager.GetModel(detail.Creater.Value);
                    if (mobile != null)
                    {
                        mobileInfo = "<font color='red'>从 " + mobile.EndDate.Year + "年" + mobile.EndDate.Month + "月起享受话费补贴</font>";
                    }
                    labExpenseTypeName.Text = expenseTypeName + "(" + detail.PhoneYear + "年" + detail.PhoneMonth + "月) " + mobileInfo;
                }
                else
                {
                    labExpenseTypeName.Text = expenseTypeName;
                }

                if (model.ReturnType.Value == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty || model.ReturnType.Value == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic || model.ReturnType.Value == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia)
                {
                    Label labRecipient = (Label)e.Row.FindControl("labRecipient");
                    Label labCity = (Label)e.Row.FindControl("labCity");
                    Label labBankName = (Label)e.Row.FindControl("labBankName");
                    Label labBankAccountNo = (Label)e.Row.FindControl("labBankAccountNo");
                    labRecipient.Text = detail.Recipient;
                    labCity.Text = detail.City;
                    labBankName.Text = detail.BankName;
                    labBankAccountNo.Text = detail.BankAccountNo;
                }
                else if (model.ReturnType.Value == (int)ESP.Purchase.Common.PRTYpe.PN_AirTicket)
                {
                    if (detail.TicketStatus != 0)
                    {
                        for (int i = 0; i < e.Row.Cells.Count; i++)
                        {
                            e.Row.Cells[i].ForeColor = System.Drawing.Color.Gray;
                        }
                    }
                    if (detail.TripType != 0)
                    {
                        for (int i = 0; i < e.Row.Cells.Count; i++)
                        {
                            e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
        }

        protected void gvG_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                if (workitem == null && !string.IsNullOrEmpty(Request["workitemid"]))
                {
                    workitem = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowModel(int.Parse(Request["workitemid"]));
                }
                if (model == null && workitem != null)
                {
                    model = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkItemModel(workitem.EntityId);
                }
                if (model != null && (model.ReturnType.Value == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty || model.ReturnType.Value == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic || model.ReturnType.Value == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia))
                {
                    e.Row.Cells[6].Visible = true;
                    e.Row.Cells[7].Visible = true;
                    e.Row.Cells[8].Visible = true;
                    e.Row.Cells[9].Visible = true;
                }
                else
                {
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;
                    e.Row.Cells[8].Visible = false;
                    e.Row.Cells[9].Visible = false;

                }

                if (model != null && model.ReturnType.Value == (int)ESP.Purchase.Common.PRTYpe.PN_AirTicket)
                {
                    e.Row.Cells[10].Visible = true;
                    e.Row.Cells[11].Visible = true;
                    e.Row.Cells[12].Visible = true;
                    e.Row.Cells[13].Visible = true;
                    e.Row.Cells[14].Visible = true;
                }
                else
                {
                    e.Row.Cells[10].Visible = false;
                    e.Row.Cells[11].Visible = false;
                    e.Row.Cells[12].Visible = false;
                    e.Row.Cells[13].Visible = false;
                    e.Row.Cells[14].Visible = false;
                }
                if (model != null && model.ReturnType.Value == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia)
                {
                    e.Row.Cells[15].Visible = false;
                }


                if (workitem.WebPage.IndexOf("step=fa") > 0 || workitem.WebPage.IndexOf("step=fm") > 0 || workitem.WebPage.IndexOf("step=f1") > 0 || workitem.WebPage.IndexOf("step=f2") > 0 || workitem.WebPage.IndexOf("step=f3") > 0 || workitem.WebPage.IndexOf("step=f4") > 0)
                {
                    e.Row.Cells[15].Visible = true;
                }
                else
                {
                    e.Row.Cells[15].Visible = false;
                }
            }

        }

        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string query = Request.Url.Query.RemoveParam("detailid");
            if (e.CommandName == "Modify")
            {
                string detailid = e.CommandArgument.ToString();
                Response.Redirect("ExpenseAccountDetailView.aspx?" + query + "&detailid=" + detailid);
            }
            if (e.CommandName == "faAdd")
            {
                string detailid = e.CommandArgument.ToString();

                Response.Redirect("ExpenseAccountDetailView.aspx?" + query + "&faadd=1&detailid=" + detailid);
            }
            if (e.CommandName == "faEdit")
            {
                string detailid = e.CommandArgument.ToString();

                Response.Redirect("ExpenseAccountDetailView.aspx?" + query + "&detailid=" + detailid);
            }
        }
        #endregion

        #region
        protected int GetStep()
        {
            if (workitem != null && !string.IsNullOrEmpty(workitem.WebPage))
            {
                if (workitem.WebPage.IndexOf("step=pa") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_YS;
                }
                else if (workitem.WebPage.IndexOf("step=pm") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_XMFZ;
                }
                else if (workitem.WebPage.IndexOf("step=mj") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_ZJSP;
                }
                else if (workitem.WebPage.IndexOf("step=gm") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLSP;
                }
                else if (workitem.WebPage.IndexOf("step=ceo") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_CEO;
                }
                else if (workitem.WebPage.IndexOf("step=fa") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_FA;
                }
                else if (workitem.WebPage.IndexOf("step=ra") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_ReciveFJ;
                }
                else if (workitem.WebPage.IndexOf("step=f1") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
                }
                else if (workitem.WebPage.IndexOf("step=f2") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial2;
                }
                else if (workitem.WebPage.IndexOf("step=f3") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial3;
                }
                else if (workitem.WebPage.IndexOf("step=f4") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial3;
                }
                else if (workitem.WebPage.IndexOf("step=fm") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_FinancialMajor;
                }
            }
            return 0;
        }

        protected int GetStatus()
        {
            if (workitem != null && !string.IsNullOrEmpty(workitem.WebPage))
            {
                if (workitem.WebPage.IndexOf("step=pa") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.PrepareAudit;
                }
                else if (workitem.WebPage.IndexOf("step=pm") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.ProjectManagerAudit;
                }
                else if (workitem.WebPage.IndexOf("step=mj") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.MajorAudit;
                }
                else if (workitem.WebPage.IndexOf("step=gm") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.GeneralManagerAudit;
                }
                else if (workitem.WebPage.IndexOf("step=ceo") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.CEOAudit;
                }
                else if (workitem.WebPage.IndexOf("step=fa") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.FAAudit;
                }
                else if (workitem.WebPage.IndexOf("step=f1") > 0)
                {
                    if (model.PreFee >= 100000 || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia)
                    {
                        return (int)ESP.Finance.Utility.PaymentStatus.FinanceLevel1;
                    }
                    else
                    {
                        return (int)ESP.Finance.Utility.PaymentStatus.FinanceLevel3;
                    }
                }
                else if (workitem.WebPage.IndexOf("step=f2") > 0)
                {
                    if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia)
                    {
                        return (int)ESP.Finance.Utility.PaymentStatus.FinanceLevel2;
                    }
                    else
                        return (int)ESP.Finance.Utility.PaymentStatus.FinanceLevel3;
                }
                else if (workitem.WebPage.IndexOf("step=f3") > 0)
                {
                    if (model.ReturnType == 32 || (model.ReturnType == 31 && model.IsFixCheque == true))
                    {
                        return (int)ESP.Finance.Utility.PaymentStatus.WaitReceiving;
                    }
                    else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia)
                    {
                        return (int)ESP.Finance.Utility.PaymentStatus.FinanceLevel3;
                    }
                    else
                    {
                        return (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete;
                    }

                }
                else if (workitem.WebPage.IndexOf("step=f4") > 0)
                {
                    if (model.ReturnType == 32 || (model.ReturnType == 31 && model.IsFixCheque == true))
                    {
                        return (int)ESP.Finance.Utility.PaymentStatus.WaitReceiving;
                    }
                    else
                    {
                        return (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete;
                    }
                }
                else if (workitem.WebPage.IndexOf("step=fm") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.EddyAudit;
                }
                else if (workitem.WebPage.IndexOf("step=rv") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.Recived;
                }
                else if (workitem.WebPage.IndexOf("step=ra") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.FJ_Recived;
                }
            }
            return 0;
        }

        [AjaxPro.AjaxMethod]
        public static List<List<string>> GetBanks(int returnid)
        {
            ESP.Finance.SqlDataAccess.WorkItem workitemModel = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowModel(returnid);
            ESP.Finance.Entity.ExpenseAccountExtendsInfo expenseModel = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkItemModel(workitemModel.EntityId);
            string branchcode = string.Empty;
            if (expenseModel != null && !string.IsNullOrEmpty(expenseModel.ProjectCode))
            {
                branchcode = expenseModel.ProjectCode.Substring(0, 1);
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


        #endregion
    }
}

