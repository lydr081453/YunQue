using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;

public partial class project_ProjectDisplay : ESP.Finance.WebPage.ViewPageForProject
{
    //private void Log(int id) { Log(id, ""); }
    //private void Log(int id, string s)
    //{
    //    using (System.IO.StreamWriter log = new System.IO.StreamWriter(HttpRuntime.AppDomainAppPath + "\\timelog.txt", true))
    //    {
    //        log.WriteLine("{0:00} {1:hh:mm:ss:ffffff} {2}", id, DateTime.Now, s);
    //        log.Flush();
    //    }
    //}

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        ProjectInfoView.DontBindOnLoad = true;
        PaymentDisplay.DontBindOnLoad = true;
        CustomerDisplay.DontBindOnLoad = true;
        ProjectSupporterDisplay.DontBindOnLoad = true;
        ProjectMemberDisplay.DontBindOnLoad = true;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack || string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
            return;

        ProjectInfo projectModel = null;
        BankInfo bankModel = null;
        int projectid;
        if (int.TryParse(Request[ESP.Finance.Utility.RequestName.ProjectID], out projectid) && projectid > 0)
        {
            projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
            if (projectModel.BankId != 0)
            { 
                bankModel = ESP.Finance.BusinessLogic.BankManager.GetModel(projectModel.BankId); 
            }
        }

        if (projectModel == null)
        {
            Response.End();
            return;
        }

        projectModel.Members = ProjectMemberManager.GetListByProject(projectid, null, null);//成员列表
        projectModel.Supporters = SupporterManager.GetListByProject(projectid, null, null);//支持方列表
        projectModel.Contracts = ContractManager.GetListByProject(projectid, null, null);//合同列表
        projectModel.Payments = PaymentManager.GetListByProject(projectid, null, null);//支付通知列表
        projectModel.CostDetails = ContractCostManager.GetListByProject(projectid, null, null);//成本明细列表
        //model.Hists = ProjectHistManager.GetListByProject(ProjectId, null, null);//历史信息
        projectModel.ProjectSchedules = ProjectScheduleManager.GetListByProject(projectid);//各月完百分比
        projectModel.Expenses = ProjectExpenseManager.GetListByProject(projectid);//OOP
        int customerid = projectModel.CustomerID ?? 0;//客户
        if (customerid > 0)
        {
            projectModel.Customer = CustomerTmpManager.GetModel(customerid);
        }

        //TopMessage.ProjectModel = projectModel;
        this.PaymentDisplay.CurrentUserIDReport = CurrentUserID;
        this.PaymentDisplay.InitProjectInfo(projectModel);

        this.ProjectSupporterDisplay.InitProjectInfo(projectModel);
        this.ProjectInfoView.BindProject(projectModel);
        this.CustomerDisplay.BindCustomerInfo(projectModel.Customer);
        this.ProjectMemberDisplay.InitProjectMember(projectModel);
        string auditDate = string.Empty;
        string auditStatus = string.Empty;
        string logstr = string.Empty;
        IList<AuditLogInfo> auditLogList = AuditLogManager.GetProjectList(projectModel.ProjectId);
        foreach (AuditLogInfo log in auditLogList)
        {
            auditDate = log.AuditDate == null ? "" : log.AuditDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
            if (log.AuditStatus == (int)AuditHistoryStatus.PassAuditing)
            {
                auditStatus = "审批通过";
                logstr += log.AuditorEmployeeName + auditStatus + "[" + auditDate + "] " + log.Suggestion + "<br/>";
            }
            else if (log.AuditStatus == (int)AuditHistoryStatus.TerminateAuditing)
            {
                auditStatus = "审批驳回";
                logstr += log.AuditorEmployeeName + auditStatus + "[" + auditDate + "] " + log.Suggestion + "<br/>";
            }
            else if (log.AuditStatus == (int)AuditHistoryStatus.RequestorCancel)
            {
                logstr += log.AuditorEmployeeName + log.Suggestion + "[" + auditDate + "]<br />";
            }
            else if (log.AuditStatus == (int)AuditHistoryStatus.WaitingContract)
            {
                logstr += log.AuditorEmployeeName + log.Suggestion + "[" + auditDate + "]<br />";
            }
            else if (log.AuditStatus == (int)ESP.Finance.Utility.AuditHistoryStatus.CommitChangedProject)
            {
                logstr += log.AuditorEmployeeName + log.Suggestion + "[" + auditDate + "]<br />";
            }
        }

        if (bankModel != null)
        {
            this.lblBankName.Text = bankModel.BankName;
            this.lblAccount.Text = bankModel.BankAccount;
            this.lblAccountName.Text = bankModel.BankAccountName;
            this.lblBankAddress.Text = bankModel.Address;
        }
        this.lblLog.Text = logstr;
        this.lblReason.Text = projectModel.ProfileReason;

        ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(projectModel.BranchCode);
        ESP.Framework.Entity.OperationAuditManageInfo operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(projectModel.GroupID.Value);
        string users = string.Empty;
        if (branchModel != null && branchModel.BranchCode != null)
        {
            users = "," + branchModel.FirstFinanceID.ToString() + "," + branchModel.FinalAccounter.ToString() + "," + branchModel.PaymentAccounter.ToString() + "," + branchModel.OtherFinancialUsers + "," + branchModel.ContractAccounter.ToString() + "," + branchModel.ProjectAccounter.ToString() + ",";
            users += ESP.Configuration.ConfigurationManager.SafeAppSettings["CostView"].ToString().Trim() + ",";
        }
        else
            users = ",";
        if (operationModel != null)
        {
            users += operationModel.DirectorId.ToString() + "," + operationModel.ManagerId.ToString() + "," + operationModel.FAId.ToString() + "," + operationModel.CEOId.ToString() + ",";
        }

        if ((!ESP.Purchase.BusinessLogic.DataPermissionManager.isMaxPermissionUser(int.Parse(CurrentUser.SysID))) && users.IndexOf("," + CurrentUser.SysID + ",") < 0 && Convert.ToInt32(CurrentUser.SysID) != projectModel.CreatorID && Convert.ToInt32(CurrentUser.SysID) != projectModel.ApplicantUserID)
        {
            ProjectInfoView.IsViewer = 1;
            ProjectSupporterDisplay.Visible = false;
            PaymentDisplay.Visible = false;
            lblLog.Visible = false;
        }
        if (!IsPostBack)
        {
            Bind_ContractList(projectModel);
            Bind_ApplyForInvioceList(projectModel);
            bind_Consumption(projectModel.ProjectId);
            Bind_RebateRegistrationList(projectModel.ProjectId);
        }
    }

    protected void bind_Consumption(int projectId)
    {

        var consumptionList = ESP.Finance.BusinessLogic.ConsumptionManager.GetCostList(" a.projectId=" + projectId);
        this.lblConsumptionTotal.Text = consumptionList.Sum(x=>x.Amount).ToString("#,##0.00");
        gvConsumption.DataSource = consumptionList;
        gvConsumption.DataBind();
    }
    protected void Bind_ContractList(ProjectInfo projectModel)
    {
        string strWhere = " status=@status";
        List<System.Data.SqlClient.SqlParameter> sqlParams = new List<System.Data.SqlClient.SqlParameter>();
        sqlParams.Add( new System.Data.SqlClient.SqlParameter( "@status", (int)ESP.Finance.Utility.ContractStatus.Status.Audited));
        gvContract.DataSource = ESP.Finance.BusinessLogic.ContractManager.GetListByProject(projectModel.ProjectId, strWhere, sqlParams).OrderByDescending(x => x.CreateDate).ToList();
        gvContract.DataBind();
        
    }

    private void Bind_RebateRegistrationList(int projectId)
    {
        string strWhere = " a.status=@status and a.projectId=@projectId";
        List<System.Data.SqlClient.SqlParameter> sqlParams = new List<System.Data.SqlClient.SqlParameter>();
        sqlParams.Add(new System.Data.SqlClient.SqlParameter("@status", (int)ESP.Finance.Utility.Common.RebateRegistration_Status.Audited));
        sqlParams.Add(new System.Data.SqlClient.SqlParameter("@projectId",projectId ));
        List<RebateRegistrationInfo> list =  ESP.Finance.BusinessLogic.RebateRegistrationManager.GetList(strWhere,sqlParams).OrderByDescending(x=>x.Id).ToList();
        gvRebateRegistration.DataSource = list;
        gvRebateRegistration.DataBind();

        labRebateRegistrationTotal.Text = list.Sum(x => x.RebateAmount).ToString("#,##0.00");
    }

    protected void Bind_ApplyForInvioceList(ProjectInfo projectModel)
    {
        string strWhere = " status=@status and projectId = @projectId";
        List<System.Data.SqlClient.SqlParameter> parms = new List<System.Data.SqlClient.SqlParameter>();
        parms.Add(new System.Data.SqlClient.SqlParameter("@projectId", projectModel.ProjectId));
        parms.Add(new System.Data.SqlClient.SqlParameter("@status", ESP.Finance.Utility.ApplyForInvioceStatus.Status.Audited));
        gvApplyForInvioce.DataSource = ESP.Finance.BusinessLogic.ApplyForInvioceManager.GetList(strWhere, parms).OrderByDescending(x => x.CreateDate).ToList();
        gvApplyForInvioce.DataBind();
    }

    protected void gvConsumption_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
        }
    }

}
