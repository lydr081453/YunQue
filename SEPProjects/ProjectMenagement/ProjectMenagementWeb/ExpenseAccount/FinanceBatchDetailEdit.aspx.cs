/************************************************************************\
 * 报销单显示页面
 *      
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
using ESP.Finance.BusinessLogic;

namespace FinanceWeb.ExpenseAccount
{
    public partial class FinanceBatchDetailEdit : ESP.Web.UI.PageBase
    {
        string BeiJingBranch = string.Empty;
        ESP.Finance.SqlDataAccess.WorkFlowDataContext dataContext = new ESP.Finance.SqlDataAccess.WorkFlowDataContext();
        ESP.Finance.Entity.ExpenseAccountExtendsInfo model = null;
        ESP.Finance.SqlDataAccess.WorkItem workitem = null;
        int batchid = 0;
        int workitemid = 0;
        bool isChongxiao = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["id"]) && !string.IsNullOrEmpty(Request["id"]) && !string.IsNullOrEmpty(Request["id"]))
            {
                model = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkItemModel(Convert.ToInt32(Request["id"]));
                batchid = Convert.ToInt32(Request["batchid"]);
                workitemid = Convert.ToInt32(Request["workitemid"]);
                workitem = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowModel(workitemid);
                isChongxiao = getChongxiao(batchid);
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
            }
            BeiJingBranch = System.Configuration.ConfigurationManager.AppSettings["FinanceAuditBJBranch"];
            if (!IsPostBack)
            {
                BindInfo();
            }
        }

        private bool getChongxiao(int bid)
        {
            IList<ESP.Finance.Entity.ReturnInfo> returnlist = ESP.Finance.BusinessLogic.PNBatchManager.GetReturnList(bid);

            if (returnlist != null && returnlist.Count > 0)
            {
                if (returnlist[0].ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff || returnlist[0].ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountWaitReceiving)
                {
                    return true;
                }
            }
            return false;
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
                try
                {
                    //删除批次关联
                    ESP.Finance.BusinessLogic.PNBatchRelationManager.Delete(batchid, model.ReturnID);
                    ESP.Finance.Entity.PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchid);
                    batchModel.Amounts = PNBatchManager.GetTotalAmounts(batchModel);
                    PNBatchManager.Update(batchModel);
                }
                catch { }
                //ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.AddLogList(model.ReturnID);
                try
                {
                    //删除审核人
                    ESP.Finance.BusinessLogic.ExpenseAuditerListManager.DeleteByReturnID(model.ReturnID);
                }
                catch { }
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核驳回操作成功！');window.location='FinanceBatchAuditEdit.aspx?batchid=" + batchid + "';", true);
                return;
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核驳回操作失败，请重试！');", true);
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
                    logModel.Suggestion += "(" + CurrentUser.Name + " 代理 " + emp.Name + ")";
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
                try
                {
                    //删除批次关联
                    ESP.Finance.BusinessLogic.PNBatchRelationManager.Delete(batchid, model.ReturnID);
                    ESP.Finance.Entity.PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchid);
                    batchModel.Amounts = PNBatchManager.GetTotalAmounts(batchModel);
                    PNBatchManager.Update(batchModel);
                }
                catch { }
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核驳回操作成功！');window.location='FinanceBatchAuditEdit.aspx?batchid=" + batchid + "';", true);
                return;
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核驳回操作失败，请重试！');", true);
                return;
            }
        }

        protected void BindInfo()
        {

            if (model != null)
            {
                if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff)
                {
                    gvG.Visible = false;
                    gvRecipient.Visible = true;
                    bindRecipient();
                }
                else
                {
                    gvG.Visible = true;
                    gvRecipient.Visible = false;
                    BindList();
                }

            

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
                if (model.ReturnType.Value == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff)
                {
                    labPreFee.Text = model.PreFee.Value.ToString("#,##0.00");
                }
                else
                    labPreFee.Text = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(model.ReturnID).ToString();


                labDepartment.Text = model.DepartmentName;

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

        private void bindRecipient()
        {
            List<SqlParameter> parms = new List<SqlParameter>();
            string strWhere = "and a.id in(" + model.RecipientIds + ")";

            DataSet ds = ESP.Purchase.BusinessLogic.RecipientManager.GetRecipientList(strWhere, parms);
            gvRecipient.DataSource = ds;
            gvRecipient.DataBind();
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("FinanceBatchAuditEdit.aspx?batchid=" + batchid);
        }


        protected void gvRecipient_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
        }


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
                if (detail.ExpenseType.Value == 33)
                {
                    labExpenseTypeName.Text = expenseTypeName + "(";
                    labExpenseTypeName.Text += detail.MealFeeMorning != null && detail.MealFeeMorning == 1 ? "早餐 " : "";
                    labExpenseTypeName.Text += detail.MealFeeNoon != null && detail.MealFeeNoon == 1 ? "午餐 " : "";
                    labExpenseTypeName.Text += detail.MealFeeNight != null && detail.MealFeeNight == 1 ? "晚餐" : "";
                    labExpenseTypeName.Text += ")";
                }
                else if (detail.ExpenseType.Value == 31)
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

                HyperLink hylEdit = (HyperLink)e.Row.FindControl("hylEdit");
                hylEdit.NavigateUrl = "FinanceBatchAuditDetailEdit.aspx?id=" + model.ReturnID + "&batchid=" + batchid + "&workitemid=" + workitemid + "&detailid=" + detail.ID;
                hylEdit.ImageUrl = "/images/edit.gif";
            }
        }

        protected void gvG_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
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
            }
        }

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
