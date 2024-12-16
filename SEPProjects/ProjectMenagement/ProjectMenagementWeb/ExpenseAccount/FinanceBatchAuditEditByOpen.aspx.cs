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

namespace FinanceWeb.ExpenseAccount
{

    public partial class FinanceBatchAuditEditByOpen : ESP.Web.UI.PageBase
    {
        string BeiJingBranch = string.Empty;
        ESP.Finance.SqlDataAccess.WorkFlowDataContext dataContext = new ESP.Finance.SqlDataAccess.WorkFlowDataContext();
        ESP.Finance.Entity.ExpenseAccountExtendsInfo model = null;
        ESP.Finance.Entity.ReturnInfo returnModel = null;
        ESP.Finance.SqlDataAccess.WorkItem workitem = null;
        bool isChongxiao = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            BeiJingBranch = System.Configuration.ConfigurationManager.AppSettings["FinanceAuditBJBranch"];
            
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["workitemid"]))
                {
                    workitem = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowModel(int.Parse(Request["workitemid"]));
                    model = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkItemModel(workitem.EntityId);
                    returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(workitem.EntityId);

                    if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff || returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountWaitReceiving)
                    {
                        isChongxiao= true;
                    }

                    radioInvoice.SelectedValue = model.IsInvoice == null ? "0" : model.IsInvoice.Value.ToString();

                    if (model.ReturnType.Value == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountWaitReceiving || model.ReturnType.Value == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff)
                    {
                        diffTr.Visible = true;
                        ESP.Finance.Entity.ExpenseAccountExtendsInfo parentModel = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkItemModel(model.ParentID.Value);
                        hylPrint.NavigateUrl = "DisplayByOpen.aspx?id=" + model.ParentID;
                        hylPrint.Target = "_blank";
                        hylPrint.Text = parentModel.ReturnCode;
                        labReturnFactFee.Text = parentModel.PreFee.Value.ToString("0.00");
                        labDifferenceFee.Text = (parentModel.PreFee.Value - model.PreFee.Value).ToString("0.00");

                    }
                    else
                    {
                        diffTr.Visible = false;
                    }

                    if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic)
                    {
                        this.panInvoice.Visible = true;
                    }
                }

                BindInfo();

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
                        labSuggestion.Text += log.AuditorEmployeeName + "(" + emp.FullNameEN + ")" + ";&nbsp;&nbsp;&nbsp;" + log.Suggestion + ";&nbsp;&nbsp;&nbsp;" + ((model.RequestorID.Value == log.AuditorUserID.Value && log.AuditType.Value == 0) ? "提交" : ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Names[log.AuditeStatus.Value]) + ";&nbsp;&nbsp;&nbsp;" + log.AuditeDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>";
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
            Page.ClientScript.RegisterStartupScript(typeof(string), "", "window.close();", true);
            return;
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
                ESP.Workflow.WorkItemManager.CloseWorkItem(workitem.WorkItemId, assigneeID, "Rejected", null, new Action<object>(ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus), prarms);
                //ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.AddLogList(model.ReturnID);
                try
                {
                    ESP.Finance.BusinessLogic.ExpenseAuditerListManager.DeleteByReturnID(model.ReturnID);
                }
                catch { }
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核驳回操作成功！');window.location='MajorExpenseAccountList.aspx';", true);
                return;
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核驳回操作失败，请重试！');window.location='MajorExpenseAccountList.aspx';", true);
                return;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (panInvoice.Visible == true)
            {
                workitem = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowModel(int.Parse(Request["workitemid"]));
                returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(workitem.EntityId);
                returnModel.IsInvoice = int.Parse(this.radioInvoice.SelectedItem.Value);
            }
            ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);

            Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存成功！');window.close();", true);

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
                    Label labCity = (Label)e.Row.FindControl("labCity");
                    Label labBankName = (Label)e.Row.FindControl("labBankName");
                    Label labBankAccountNo = (Label)e.Row.FindControl("labBankAccountNo");
                    labRecipient.Text = detail.Recipient;
                    labCity.Text = detail.City;
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
                    e.Row.Cells[9].Visible = true;
                }
                else
                {
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;
                    e.Row.Cells[8].Visible = false;
                    e.Row.Cells[9].Visible = false;
                }

                if (workitem.WebPage.IndexOf("step=fa") > 0 || workitem.WebPage.IndexOf("step=fm") > 0 || workitem.WebPage.IndexOf("step=f1") > 0 || workitem.WebPage.IndexOf("step=f2") > 0 || workitem.WebPage.IndexOf("step=f3") > 0)
                {
                    e.Row.Cells[10].Visible = true;
                }
                else
                {
                    e.Row.Cells[10].Visible = false;
                }
            }

        }

        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "faEdit")
            {
                string detailid = e.CommandArgument.ToString();
                Response.Redirect("FinanceBatchAuditDetailEditByOpen.aspx?detailid=" + detailid + "&workitemid=" + workitem.WorkItemId);
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
                    if (isChongxiao)
                        return (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete;
                    else
                        return (int)ESP.Finance.Utility.PaymentStatus.FinanceLevel2;
                }
                else if (workitem.WebPage.IndexOf("step=f3") > 0)
                {
                    if (BeiJingBranch.IndexOf(model.BranchCode.ToLower()) >= 0)
                        return (int)ESP.Finance.Utility.PaymentStatus.FinanceLevel3;
                    else
                        return (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete;
                }
                else if (workitem.WebPage.IndexOf("step=f4") > 0)
                {
                    if (model.ReturnStatus == 32 || (model.ReturnStatus == 31 && model.IsFixCheque == true))
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
        #endregion
    }
}
