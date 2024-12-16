/************************************************************************\
 * 报销单确认收货页
 *      
 * 如果是现场购买或现金借款报销单，有申请人需要确认收货的流程，则需要在此页操作
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
    public partial class ConfirmExpenseAccountView : ESP.Web.UI.PageBase
    {
        string BeiJingBranch = string.Empty;
        bool isChongxiao = false;
        ESP.Finance.SqlDataAccess.WorkFlowDataContext dataContext = new ESP.Finance.SqlDataAccess.WorkFlowDataContext();
        ESP.Finance.Entity.ExpenseAccountExtendsInfo model = null;
        ESP.Finance.SqlDataAccess.WorkItem workitem = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            BeiJingBranch = System.Configuration.ConfigurationManager.AppSettings["FinanceAuditBJBranch"];
            if (!string.IsNullOrEmpty(Request["workitemid"]))
            {
                workitem = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowModel(int.Parse(Request["workitemid"]));
                model = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkItemModel(workitem.EntityId);
                isChongxiao = getChongxiao();
            }
            if (!IsPostBack)
            {
                BindInfo();
            }
        }


        private bool getChongxiao()
        {

            if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountWaitReceiving)
                {
                    return true;
                }
           
            return false;
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

                labPreFee.Text = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(model.ReturnID).ToString();


                labDepartment.Text = model.DepartmentName;

                int[] depts = new ESP.Compatible.Employee(int.Parse(CurrentUser.SysID)).GetDepartmentIDs();
                hidGroupID.Value = depts[0].ToString();
                hidApplicantID.Value = CurrentUser.SysID;


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

                txtConfirmFee.Value = Convert.ToDouble(model.PreFee.Value);

                List<ESP.Finance.Entity.ExpenseAuditDetailInfo> auditLogs = ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.GetList(" and ExpenseAuditID = " + model.ReturnID);
                foreach (ESP.Finance.Entity.ExpenseAuditDetailInfo log in auditLogs)
                {
                    ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(log.AuditorUserID.Value);
                    //txtSuggestion.Text += log.AuditorEmployeeName + ";  " + log.Suggestion + ";  " + (model.RequestorID.Value == log.AuditorUserID.Value ? "提交" : ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Names[log.AuditeStatus.Value]) + ";  " + log.AuditeDate.Value.ToString("yyyy-MM-dd") + "\r\n";
                    if (emp != null)
                    labSuggestion.Text += log.AuditorEmployeeName+"("+emp.FullNameEN+")" + ";&nbsp;&nbsp;&nbsp;" + log.Suggestion + ";&nbsp;&nbsp;&nbsp;" + ((model.RequestorID.Value == log.AuditorUserID.Value && log.AuditType.Value == 0) ? "提交" : ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Names[log.AuditeStatus.Value]) + ";&nbsp;&nbsp;&nbsp;" + log.AuditeDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>";
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
            Response.Redirect("CashExpenseAccountList.aspx?ReturnType=" + model.ReturnType);
        }

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
                logModel.AuditType = (int)PaymentStatus.ConfirmReceiving;
                logModel.ExpenseAuditID = model.ReturnID;
                logModel.AuditeStatus = (int)ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Passed;
                logModel.Suggestion = "收货";

                ESP.Finance.Entity.ExpenseAccountInfo recive = new ESP.Finance.Entity.ExpenseAccountInfo();
                recive.ReturnID = model.ReturnID;
                recive.ConfirmFee = Convert.ToDecimal(txtConfirmFee.Value.Value);

                //设置附加收货人
                List<int> receiveAffirmantId = new List<int>();
                if (!string.IsNullOrEmpty(hidApplicantID.Value))
                {

                    receiveAffirmantId.Add(Convert.ToInt32(hidApplicantID.Value));
                }

                Dictionary<string, object> nextAuditer = new Dictionary<string, object>() 
                { 
                    { "ReceiveAffirmantId" ,  receiveAffirmantId.ToArray() }
                };

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
                    { "LogModel", logModel },
                    { "ReciveModel" , recive}
                };

                //调用工作流
                ESP.Workflow.WorkItemManager.CloseWorkItem(workitem.WorkItemId, assigneeID, "Approved", nextAuditer, new Action<object>(ESP.Finance.BusinessLogic.ExpenseAccountManager.UpdateStatus), prarms);

                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('确认收货成功！');window.location='CashExpenseAccountList.aspx?ReturnType=" + model.ReturnType + "';", true);
                return;
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('确认收货失败，请重试！');window.location='CashExpenseAccountList.aspx?ReturnType=" + model.ReturnType + "';", true);
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
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[7].Visible = false;
                ESP.Finance.Entity.ExpenseAccountDetailInfo detail = (ESP.Finance.Entity.ExpenseAccountDetailInfo)e.Row.DataItem;
                ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(detail.ReturnID.Value);
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
                    string mobileInfo = string.Empty;
                    ESP.Finance.Entity.MobileListInfo mobile = ESP.Finance.BusinessLogic.MobileListManager.GetModel(detail.Creater.Value);
                    if (mobile != null)
                    {
                        mobileInfo = "<font color='red'>3G体验计划</font>";
                    }
                    labExpenseTypeName.Text = expenseTypeName + "(" + detail.PhoneYear + "年" + detail.PhoneMonth + "月) " + mobileInfo;
                }
                else if (returnModel.ReturnType.Value == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic)
                {
                    e.Row.Cells[4].Visible = true;
                    e.Row.Cells[5].Visible = true;
                    e.Row.Cells[6].Visible = true;
                    e.Row.Cells[7].Visible = true;
                    Label labRecipient = (Label)e.Row.FindControl("labRecipient");
                    Label labCity = (Label)e.Row.FindControl("labCity");
                    Label labBankName = (Label)e.Row.FindControl("labBankName");
                    Label labBankAccountNo = (Label)e.Row.FindControl("labBankAccountNo");
                    labRecipient.Text = detail.Recipient;
                    labCity.Text = detail.City;
                    labBankName.Text = detail.BankName;
                    labBankAccountNo.Text = detail.BankAccountNo;
                }
                else
                {
                    labExpenseTypeName.Text = expenseTypeName;
                }
            }
        }

        #endregion

        #region

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
            }
            return 0;
        }
        #endregion
    }
}
