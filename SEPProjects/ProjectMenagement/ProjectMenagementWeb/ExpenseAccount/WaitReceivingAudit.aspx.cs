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
using System.Data.SqlClient;
using System.Data;

namespace FinanceWeb.ExpenseAccount
{

    public partial class WaitReceivingAudit : ESP.Web.UI.PageBase
    {
        ESP.Finance.SqlDataAccess.WorkFlowDataContext dataContext = new ESP.Finance.SqlDataAccess.WorkFlowDataContext();
        ESP.Finance.Entity.ExpenseAccountExtendsInfo model = null;
        ESP.Finance.SqlDataAccess.WorkItem workitem = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["workitemid"]))
            {
                workitem = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowModel(int.Parse(Request["workitemid"]));
                model = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkItemModel(workitem.EntityId);
                

                ESP.Finance.Entity.ExpenseAccountExtendsInfo parentModel = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkItemModel(model.ParentID.Value);
                hylPrint.NavigateUrl = "DisplayByOpen.aspx?id=" + model.ParentID;
                hylPrint.Target = "_blank";
                hylPrint.Text = parentModel.ReturnCode;
                labReturnFactFee.Text = parentModel.PreFee.Value.ToString("0.00");

                labDifferenceFee.Text = (model.PreFee.Value- parentModel.PreFee.Value).ToString("0.00");
            }
            if (!IsPostBack)
            {
                BindInfo();
                if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff)
                {
                    bindData(model);
                }
                IsFinanceAudit();
            }
        }


        protected void IsFinanceAudit()
        {
            if (workitem!=null && !string.IsNullOrEmpty(workitem.WebPage))
            {
                if (workitem.WebPage.IndexOf("step=f1") > 0 )
                {
                    btnAudit.Visible = false;
                    trNext.Visible = true;
                    btnUnAuditF.Visible = false;
                    
                }
                else if (workitem.WebPage.IndexOf("step=f2") > 0)
                {
                    btnAuditF.Visible = false;
                    trNext.Visible = false;
                    
                }
                else if (workitem.WebPage.IndexOf("step=f3") > 0)
                {
                    btnAuditF.Visible = false;
                    trNext.Visible = false;
                }
                else
                {
                    btnAuditF.Visible = false;
                    btnUnAuditF.Visible = false;
                    trNext.Visible = false;
                }
            }
        }

        private void bindData(ESP.Finance.Entity.ExpenseAccountExtendsInfo returnModel)
        {
            List<SqlParameter> parms = new List<SqlParameter>();
            string strWhere = "and a.id in("+returnModel.RecipientIds+")";

            DataSet ds = ESP.Purchase.BusinessLogic.RecipientManager.GetRecipientList(strWhere, parms);
            gvRecipient.DataSource = ds;
            gvRecipient.DataBind();
        }

        protected void gvRecipient_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
        }

        protected void BindInfo()
        {

            if (model != null && workitem != null)
            {
                BindList();

                labRequestUserName.Text = model.RequestEmployeeName;
                labRequestUserCode.Text = model.RequestUserCode;
                labRequestDate.Text = model.RequestDate.Value.ToString("yyyy-MM-dd");

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

                //labMemo.Text = model.ReturnContent;
                if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff)
                    labPreFee.Text = model.PreFee.Value.ToString("#,##0.00");
                else
                labPreFee.Text = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(model.ReturnID).ToString();

                labDepartment.Text = model.DepartmentName;

                if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccount)
                    labReturnType.Text = "报销单";
                else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCashBorrow)
                    labReturnType.Text = "现金借款单";
                else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic)
                    labReturnType.Text = "支票/电汇付款单";
                else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountBusinessCard)
                    labReturnType.Text = "商务卡报销单";
                else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff)
                    labReturnType.Text = "PR现金借款冲销";
                else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty)
                    labReturnType.Text = "第三方报销单";
                else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountWaitReceiving)
                    labReturnType.Text = "借款冲销单";


                List<ESP.Finance.Entity.ExpenseAuditDetailInfo> auditLogs = ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.GetList(" and ExpenseAuditID = " + model.ReturnID);
                foreach (ESP.Finance.Entity.ExpenseAuditDetailInfo log in auditLogs)
                {
                    ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(log.AuditorUserID.Value);
                    if (emp != null)
                    labSuggestion.Text += log.AuditorEmployeeName+"("+emp.FullNameEN+")" + ";&nbsp;&nbsp;&nbsp;" + log.Suggestion + ";&nbsp;&nbsp;&nbsp;" + ((model.RequestorID.Value == log.AuditorUserID.Value && log.AuditType.Value == 0) ? "提交" : ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Names[log.AuditeStatus.Value]) + ";&nbsp;&nbsp;&nbsp;" + log.AuditeDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>";
                }

                
                if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff)
                {
                    gvG.Visible = false;
                    gvRecipient.Visible = true;
                }
                else
                {
                    gvG.Visible = true;
                    gvRecipient.Visible = false;
                }
               
                
            }

        }

        protected void BindList()
        {
            List<ESP.Finance.Entity.ExpenseAccountDetailInfo> list = null;
            if (model!=null)
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
            return string.IsNullOrEmpty(Request["BackUrl"]) ? "/ExpenseAccount/ExpenseAccountAuditList.aspx" : Request["BackUrl"];
        }

        //protected void btnEditFA_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("ExpenseAccountEditFA.aspx?workitemid=" + workitem.WorkItemId);
        //}

        protected void btnAudit_Click(object sender, EventArgs e)
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
                logModel.AuditeStatus = (int)ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Passed;
                logModel.Suggestion = txtSuggestion.Text.Trim();

                int assigneeID = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetAssigneeIdByWorkItemID(workitem.WorkItemId, dataContext);
                if (assigneeID != Convert.ToInt32(CurrentUser.SysID))
                {
                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(assigneeID);
                    logModel.Suggestion += "(" + CurrentUser.Name+"("+CurrentUser.ITCode+")" + " 代理 " + emp.Name + ")";
                }

                //设置工作流委托方法的参数
                Dictionary<string, object> prarms = new Dictionary<string, object>() 
                { 
                    { "EntID", model.ReturnID } ,
                    { "ReturnStatus",  GetStatus()} ,
                    { "LogModel", logModel }
                };

                if (workitem != null && !string.IsNullOrEmpty(workitem.WebPage) && workitem.WebPage.IndexOf("step=f3") > 0 )
                {
                    
                    //prarms.Add("FactFee", txtFactFee.Value.Value);
                    if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff)
                    {
                        prarms.Add("FactFee", model.PreFee);
                    }
                    else
                    {
                        prarms.Add("FactFee", ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(model.ReturnID));
                    }
                    
                    
                }

                //decimal ooptotal = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalOOPByReturnID(model.ReturnID);
                //decimal costtotal = model.PreFee.Value - ooptotal;
                //if (ESP.Finance.BusinessLogic.ExpenseAccountManager.ValidateMoney(model,costtotal,ooptotal))
                //{
                    //调用工作流
                    ESP.Workflow.WorkItemManager.CloseWorkItem(workitem.WorkItemId, assigneeID, "Approved", null, new Action<object>(ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus), prarms);

                    //如果是业务审批最后一级审批通过，则记录时间
                    ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowLastAuditPassTime(model.ReturnID, new ESP.Compatible.Employee(UserInfo.UserID));
                    if (returnModel != null)
                    {
                        ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel); 
                    }

                    //如果审批结束，则改变单据状态为审批结束
                    if (ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowInstanceStatus(workitem.WorkflowInstanceId) == (int)ESP.Workflow.WorkflowProcessStatus.Closed)
                    {
                        ESP.Finance.BusinessLogic.ExpenseAccountManager.RecivingOver(model);
                    }
                //}
                //else
                //{
                //    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('项目成本金额不足，无法审核通过，请检查！');window.location='WaitReceivingAuditList.aspx';", true);
                //    return;
                //}

                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核通过操作成功！');window.location='" + GetBackUrl() + "';", true);
                return;
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核通过操作失败，请重试！');", true);
                return;
            }
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

                int assigneeID = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetAssigneeIdByWorkItemID(workitem.WorkItemId, dataContext);
                if (assigneeID != Convert.ToInt32(CurrentUser.SysID))
                {
                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(assigneeID);
                    logModel.Suggestion += "(" + CurrentUser.Name+"("+CurrentUser.ITCode+")" + " 代理 " + emp.Name + ")";
                }

                //设置工作流委托方法的参数
                Dictionary<string, object> prarms = new Dictionary<string, object>() 
                { 
                    { "EntID", model.ReturnID } ,
                    { "ReturnStatus", (int)ESP.Finance.Utility.PaymentStatus.ConfirmReceiving } ,
                    { "LogModel", logModel }
                };
                //调用工作流
                ESP.Workflow.WorkItemManager.CloseWorkItem(workitem.WorkItemId, assigneeID, "Rejected", null, new Action<object>(ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus), prarms);
                //ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.AddLogList(model.ReturnID);
                try
                {
                    ESP.Finance.BusinessLogic.ExpenseAuditerListManager.DeleteByReturnID(model.ReturnID);
                }
                catch { }
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核驳回操作成功！');window.location='" + GetBackUrl() + "';", true);
                return;
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核驳回操作失败，请重试！');", true);
                return;
            }
        }

        protected void btnAuditF_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidNextAuditor.Value))
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('请选择财务下级审批人！');", true);
                return;
            }
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
                logModel.AuditeStatus = (int)ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Passed;
                logModel.Suggestion = txtSuggestion.Text.Trim();

                int assigneeID = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetAssigneeIdByWorkItemID(workitem.WorkItemId, dataContext);
                if (assigneeID != Convert.ToInt32(CurrentUser.SysID))
                {
                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(assigneeID);
                    logModel.Suggestion += "(" + CurrentUser.Name+"("+CurrentUser.ITCode+")" + " 代理 " + emp.Name + ")";
                }

                //设置工作流委托方法的参数
                Dictionary<string, object> prarms = new Dictionary<string, object>() 
                { 
                    { "EntID", model.ReturnID } ,
                    { "ReturnStatus",  GetStatus()} ,
                    { "LogModel", logModel }
                };

                //设置财务下级审批人参数
                List<int> nextAuditerList = new List<int>();
                nextAuditerList.Add(Convert.ToInt32(hidNextAuditor.Value));
                Dictionary<string, object> nextAuditer = null; 

                if (workitem!=null && !string.IsNullOrEmpty(workitem.WebPage) && workitem.WebPage.IndexOf("step=f1")>0)
                {
                    nextAuditer = new Dictionary<string, object>() 
                    { 
                        { "Finance2Id" ,  nextAuditerList.ToArray() }
                    };
                }

                //if (workitem != null && !string.IsNullOrEmpty(workitem.WebPage) && workitem.WebPage.IndexOf("step=f2") > 0)
                //{
                //    nextAuditer = new Dictionary<string, object>() 
                //    { 
                //        { "Finance3Id" ,  nextAuditerList.ToArray() }
                //    };
                //}

                //decimal ooptotal = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalOOPByReturnID(model.ReturnID);
                //decimal costtotal = model.PreFee.Value - ooptotal;
                //if (ESP.Finance.BusinessLogic.ExpenseAccountManager.ValidateMoney(model,costtotal,ooptotal))
                //{
                    //调用工作流
                    ESP.Workflow.WorkItemManager.CloseWorkItem(workitem.WorkItemId, assigneeID, "Approved", nextAuditer, new Action<object>(ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus), prarms);

                //}
                //else
                //{
                //    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('项目成本金额不足，无法审核通过，请检查！');window.location='WaitReceivingAuditList.aspx';", true);
                //    return;
                //}

                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核通过操作成功！');window.location='" + GetBackUrl() + "';", true);
                return;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return;
            }
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
                    logModel.Suggestion += "(" + CurrentUser.Name+"("+CurrentUser.ITCode+")" + " 代理 " + emp.Name + ")";
                }

                //设置工作流委托方法的参数
                Dictionary<string, object> prarms = new Dictionary<string, object>() 
                { 
                    { "EntID", model.ReturnID } ,
                    { "ReturnStatus", (int)ESP.Finance.Utility.PaymentStatus.FAAudit } ,
                    { "LogModel", logModel }
                };
                //调用工作流
                ESP.Workflow.WorkItemManager.CloseWorkItem(workitem.WorkItemId, assigneeID, "Returned", null, new Action<object>(ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus), prarms);

                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核驳回操作成功！');window.location='" + GetBackUrl() + "';", true);
                return;
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核驳回操作失败，请重试！');", true);
                return;
            }
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
                    labExpenseTypeName.Text = expenseTypeName + "(" + detail.PhoneYear + "年" + detail.PhoneMonth + "月)";
                }
                else
                {
                    labExpenseTypeName.Text = expenseTypeName;
                }

                if (model.ReturnType.Value == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty)
                {
                    Label labRecipient = (Label)e.Row.FindControl("labRecipient");
                    Label labBankName = (Label)e.Row.FindControl("labBankName");
                    Label labBankAccountNo = (Label)e.Row.FindControl("labBankAccountNo");
                    labRecipient.Text = detail.Recipient;
                    labBankName.Text = detail.BankName;
                    labBankAccountNo.Text = detail.BankAccountNo;
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
                if (model != null && model.ReturnType.Value == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty)
                {
                    e.Row.Cells[6].Visible = true;
                    e.Row.Cells[7].Visible = true;
                    e.Row.Cells[8].Visible = true;
                }
                else
                {
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;
                    e.Row.Cells[8].Visible = false;
                }

                if (workitem.WebPage.IndexOf("step=fa") > 0 || workitem.WebPage.IndexOf("step=fm") > 0 || workitem.WebPage.IndexOf("step=f1") > 0 || workitem.WebPage.IndexOf("step=f2") > 0 || workitem.WebPage.IndexOf("step=f3") > 0)
                {
                    e.Row.Cells[9].Visible = true;
                }
                else
                {
                    e.Row.Cells[9].Visible = false;
                }
            } 
            
        }

       protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modify")
            {
                string detailid = e.CommandArgument.ToString();
                Response.Redirect("ExpenseAccountDetailView.aspx"+Request.Url.Query+"&detailid=" + detailid );
            }
            if (e.CommandName == "faEdit")
            {
                string detailid = e.CommandArgument.ToString();
                Response.Redirect("ExpenseAccountDetailView.aspx"+Request.Url.Query+"&detailid=" + detailid);
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
                    return (int)ESP.Finance.Utility.PaymentStatus.FinanceLevel1;
                }
                else if (workitem.WebPage.IndexOf("step=f2") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete;
                }
                else if (workitem.WebPage.IndexOf("step=f3") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete;
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
        #endregion

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string sum = string.Empty;
            if (Convert.ToInt32(model.ReturnType) != (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty && Convert.ToInt32(model.ReturnType) != (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic)
            {
                sum = "Print/ExpensePrint.aspx?expenseID=" + model.ReturnID;
            }
            else
            {
                sum = "Print/ThirdPartyPrint.aspx?expenseID=" + model.ReturnID;
            }
            Page.ClientScript.RegisterStartupScript(typeof(string), "", "window.open('" + sum + "');", true);
        }
    }
}
