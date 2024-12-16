using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Entity;

public partial class project_ProjectAuditedModify : ESP.Finance.WebPage.EditPageForProject
{
    string query = string.Empty;
    int projectid = 0;
    //int customerid = 0;
    string bakurl = string.Empty;
    ESP.Finance.Entity.ProjectInfo projectmodel;


    private ESP.Finance.Entity.ProjectHistInfo getLastestProjectHist(ESP.Finance.Entity.ProjectInfo pmodel)
    {
        List<System.Data.SqlClient.SqlParameter> paramList = null;
        string term;
        IList<ESP.Finance.Entity.ProjectHistInfo> projectList = null;

        paramList = new List<SqlParameter>();
        int ProjectID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
        term = " ProjectID=@ProjectID";
        System.Data.SqlClient.SqlParameter p2 = new System.Data.SqlClient.SqlParameter("@ProjectID", System.Data.SqlDbType.Int, 4);
        p2.SqlValue = pmodel.ProjectId;
        paramList.Add(p2);
        projectList = ESP.Finance.BusinessLogic.ProjectHistManager.GetList(term, paramList);
        if (projectList == null || projectList.Count == 0)
            return null;
        else
        {
            var tmplist = projectList.OrderByDescending(N => N.SubmitDate).ToList();
            return tmplist[0];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        query = Request.Url.Query;
        Server.ScriptTimeout = 600;
        ProjectMember.CurrentUser = CurrentUser;
        this.CustomerInfo.CurrentUser = CurrentUser;
        this.PaymentInfo.CurrentUser = CurrentUser;
        this.ProjectInfo.CurrentUser = CurrentUser;
        AjaxPro.Utility.RegisterTypeForAjax(typeof(project_ProjectAuditedModify));
        this.ddlBank.Attributes.Add("onchange", "selectBank(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");

        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
            {
                projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
                projectmodel = ESP.Finance.BusinessLogic.ProjectManager.GetModel(projectid);

                hidBranchId.Value = projectmodel.BranchID.ToString();

                TopMessage.ProjectModel = projectmodel;
                ViewState["ProjectModel"] = projectmodel;
                this.PaymentInfo.ProjectInfo = projectmodel;
                this.PaymentInfo.InitProjectInfo();
                if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.Operate]))
                {
                    this.hidOddTotalAmount.Value = projectmodel.TotalAmount == null ? "0.00" : projectmodel.TotalAmount.Value.ToString("0.00");
                    this.hidOddCost.Value = (projectmodel.TotalAmount.Value - ESP.Finance.BusinessLogic.CheckerManager.GetProjectOddAmount(projectmodel.ProjectId)).ToString("0.00");
                    this.hidContractStatus.Value = projectmodel.ContractStatusID == null ? "" : projectmodel.ContractStatusID.Value.ToString();
                    hidRechargeAmount.Value = projectmodel.Recharge == null ? "0.00" : projectmodel.Recharge.Value.ToString("0.00");
                }
                else
                {
                    this.hidOddTotalAmount.Value = "0.00";
                    this.hidOddCost.Value = "0.00";
                    hidRechargeAmount.Value = "0.00";
                }


                if (projectmodel.BankId != 0)
                {
                    ESP.Finance.Entity.BankInfo bankModel = ESP.Finance.BusinessLogic.BankManager.GetModel(projectmodel.BankId);
                    this.hidBankID.Value = bankModel.BankID.ToString();

                    this.lblAccount.Text = bankModel.BankAccount;
                    this.lblAccountName.Text = bankModel.BankAccountName;
                    this.lblBankAddress.Text = bankModel.Address;
                }

            }
            //40%
            //decimal taxfee = 0;
            //decimal servicefee = 0;
            //decimal profilerate = 0;
            //ESP.Finance.Entity.TaxRateInfo rateModel = null;
            //if (projectmodel.ContractTaxID != null && projectmodel.ContractTaxID.Value != 0)
            //    rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(projectmodel.ContractTaxID.Value);

            //if (projectmodel.ContractTax != null)
            //{
            //    if (projectmodel.IsCalculateByVAT == 1)
            //    {
            //        taxfee = ESP.Finance.BusinessLogic.CheckerManager.GetTaxByVAT(projectmodel, rateModel);
            //    }
            //    else
            //    {
            //        taxfee = ESP.Finance.BusinessLogic.CheckerManager.GetTax(projectmodel, rateModel);
            //    }
            //}
            //if (projectmodel.IsCalculateByVAT == 1)
            //{
            //    servicefee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFeeByVAT(projectmodel, rateModel);
            //}
            //else
            //{
            //    servicefee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFee(projectmodel, rateModel);
            //}
            //if (projectmodel.TotalAmount > 0)
            //{
            //    profilerate = (servicefee / Convert.ToDecimal(projectmodel.TotalAmount) * 100);
            //}
            //if (profilerate < 40 && projectmodel.ContractStatusName != ESP.Finance.Utility.ProjectType.BDProject)
            //{
            //    this.lblTip.Text = "项目毛利率为" + profilerate.ToString("#,##0.00") + "%，低于40%，请说明立项原因。";
            //    this.txtReason.Text = projectmodel.ProfileReason;
            //}
            //else
            //{
            //    this.tabReason.Visible = false;
            //}

            if (!CurrentUserRight())
            {
                ddlBank.Enabled = false;
            }

        }
    }

    [AjaxPro.AjaxMethod]
    public static List<List<string>> GetBanks(int branchid)
    {
        List<List<string>> retlists = new List<List<string>>();
        List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
        paramlist.Add(new System.Data.SqlClient.SqlParameter("@branchid", branchid));
        IList<BankInfo> paylist = ESP.Finance.BusinessLogic.BankManager.GetList(" branchid=@branchid", paramlist);
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

    private bool CurrentUserRight()
    {
        bool curRight = false;
        string sql = string.Format(" (FirstFinanceID={0} or PaymentAccounter ={0} or ProjectAccounter={0})", CurrentUserID);

        IList<BranchInfo> branchList = ESP.Finance.BusinessLogic.BranchManager.GetList(sql);

        string financeIds = System.Configuration.ConfigurationManager.AppSettings["FinanceIds"];

        if ((branchList != null && branchList.Count > 0) || (financeIds.IndexOf("," + CurrentUserID + ",")) >= 0)
            curRight = true;

        return curRight;
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        string backUrl = string.Empty;
        if (!string.IsNullOrEmpty(Request[RequestName.BackUrl]))
        {
            backUrl = Request[RequestName.BackUrl];
        }
        else
        {
            backUrl = "/project/ProjectAuditedModifyList.aspx";
        }
        Response.Redirect(backUrl);
    }

    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    SaveProjectInfo();
    //}

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        string backUrl = string.Empty;
        if (!string.IsNullOrEmpty(Request[RequestName.BackUrl]))
        {
            backUrl = Request[RequestName.BackUrl];
        }
        else
        {
            backUrl = "/project/ProjectAuditedModifyList.aspx";
        }
        int ret = SaveProjectInfo();
        switch (ret)
        {
            case 0://failed
                break;
            case 1://更新内容，不触发审批流程
                //  ESP.Logging.Logger.Add(string.Format("{0}对F_Project表中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, Request[RequestName.ProjectID], "变更项目号申请单不触发审批流程"), "变更项目号申请单");
                ClientScript.RegisterStartupScript(typeof(string), "", "window.location='" + backUrl + "';alert('项目变更成功！');", true);
                break;
            case 2://更新内容，触发审批流程
                // ESP.Logging.Logger.Add(string.Format("{0}对F_Project表中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, Request[RequestName.ProjectID], "变更项目号申请单触发审批流程"), "变更项目号申请单");
                query = query.AddParam(RequestName.BackUrl, backUrl);
                Response.Redirect("NewSetAuditor.aspx?" + query);
                break;
        }
    }

    private ESP.Finance.Entity.ProjectHistInfo getProjectHistModel(int projectId)
    {
        var paramList = new List<SqlParameter>();
        string term = " ProjectID=@ProjectID and Status=@Status";
        System.Data.SqlClient.SqlParameter p2 = new System.Data.SqlClient.SqlParameter("@ProjectID", System.Data.SqlDbType.Int, 4);
        p2.SqlValue = projectId;
        paramList.Add(p2);
        System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@Status", System.Data.SqlDbType.Int, 4);
        p1.SqlValue = (int)ESP.Finance.Utility.Status.FinanceAuditComplete;
        paramList.Add(p1);
        var projectList = ESP.Finance.BusinessLogic.ProjectHistManager.GetList(term, paramList);
        var tmp = projectList.OrderByDescending(N => N.SubmitDate).ToList().FirstOrDefault();
        return tmp;
    }

    private int SaveProjectInfo()
    {
        int ret = 0;
        int responserDiffer = 0;
        int customerid = 0;
        decimal totalPercent = 0;
        decimal totalFee = 0;



        projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
        if (ViewState["ProjectModel"] != null)
            projectmodel = (ESP.Finance.Entity.ProjectInfo)ViewState["ProjectModel"];
        decimal? oldRechargeAmount = projectmodel.Recharge; //记录原始充值金额
        int OldContractStatusID = projectmodel.ContractStatusID ?? 0;

        List<SqlParameter> paramlist = new List<SqlParameter>();
        //获取prepareinfo内容
        this.PrepareInfo.setProjectModel();
        if (projectmodel.ApplicantUserID != PrepareInfo.ProjectModel.ApplicantUserID)
        {
            responserDiffer = 1;
            ESP.Finance.Entity.AuditHistoryInfo responser = new ESP.Finance.Entity.AuditHistoryInfo();
            responser.AuditorUserID = PrepareInfo.ProjectModel.ApplicantUserID;
            responser.AuditorUserCode = PrepareInfo.ProjectModel.ApplicantCode;
            responser.AuditorEmployeeName = PrepareInfo.ProjectModel.ApplicantEmployeeName;
            responser.AuditorUserName = PrepareInfo.ProjectModel.ApplicantUserName;
            responser.AuditStatus = (int)AuditHistoryStatus.UnAuditing;
            responser.AuditType = auditorType.operationAudit_Type_YS;
            responser.ProjectID = PrepareInfo.ProjectModel.ProjectId;
            string term = " ProjectID=@ProjectID";
            SqlParameter p1 = new SqlParameter("@ProjectID", SqlDbType.Int, 4);
            p1.Value = PrepareInfo.ProjectModel.ProjectId;
            paramlist.Add(p1);
            IList<ESP.Finance.Entity.AuditHistoryInfo> list = ESP.Finance.BusinessLogic.AuditHistoryManager.GetList(term, paramlist);
            foreach (ESP.Finance.Entity.AuditHistoryInfo model in list)
            {
                ESP.Finance.BusinessLogic.AuditHistoryManager.Delete(model.AuditID);
            }
            ESP.Finance.BusinessLogic.AuditHistoryManager.Add(responser);
        }
        //更新准备信息
        projectmodel = PrepareInfo.setProjectModel(projectmodel);
        projectmodel.ProfileReason = this.txtReason.Text;
        //更新customer
        this.CustomerInfo.setCustomerInfo();
        if (projectmodel.Customer == null)
        {
            customerid = ESP.Finance.BusinessLogic.CustomerTmpManager.Add(CustomerInfo.CustomerModel);
        }
        else
        {
            ESP.Finance.BusinessLogic.CustomerTmpManager.Update(CustomerInfo.CustomerModel);
            customerid = CustomerInfo.CustomerModel.CustomerTmpID;
        }
        projectmodel.CustomerID = customerid;
        //付款信息
        projectmodel.PayCycle = this.PaymentInfo.PayCycle;
        projectmodel.IsNeedInvoice = this.PaymentInfo.Is3rdInvoice == true ? 1 : 0;
        //更新项目结束日期
        projectmodel = this.ProjectInfo.GetProject(projectmodel);

        projectmodel.PayCycle = StringHelper.SubString(this.PaymentInfo.PayCycle, 500);
        projectmodel.IsNeedInvoice = this.PaymentInfo.Is3rdInvoice == true ? 1 : 0;
        projectmodel.CustomerRemark = StringHelper.SubString(this.PaymentInfo.CustomerRemark, 200);
        projectmodel.SubmitDate = DateTime.Now;
        paramlist.Clear();

        if (projectmodel.ContractStatusName != ProjectType.BDProject)
        {
            string condition = " projectID =@projectID AND usable=1";

            paramlist.Add(new System.Data.SqlClient.SqlParameter("@projectID", projectmodel.ProjectId.ToString()));

            IList<ContractInfo> listcontract = ESP.Finance.BusinessLogic.ContractManager.GetList(condition, paramlist);
            if (listcontract == null || listcontract.Count == 0)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('请完整添加合同信息!');", true);
                return 0;
            }
            if (projectmodel.ContractTaxID == null || projectmodel.ContractTaxID == -1)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('请选择合同税率!');", true);
                return 0;
            }

            string term = " Status is null and ContractType is null";
            IList<ContractInfo> contractlist = ESP.Finance.BusinessLogic.ContractManager.GetListByProject(Convert.ToInt32(projectmodel.ProjectId), term, new List<System.Data.SqlClient.SqlParameter>());
            var oldContractList = projectmodel.Contracts.Where(x => x.Status == null && x.ContractType==null).ToList();
            if (contractlist.Count() != oldContractList.Count())
            {
                if (projectmodel.Status == (int)Status.Waiting)
                {
                    projectmodel.Status = (int)Status.BizAuditComplete;
                    ret = 1;
                }
                else
                {
                    projectmodel.Status = (int)Status.Saved;
                    ret = 2;
                }
            }
            //从非合同类型 变更为 合同类型
            if ((OldContractStatusID != 1 && OldContractStatusID != 5) && (projectmodel.ContractStatusID == 1 || projectmodel.ContractStatusID == 5))
            {
                projectmodel.Status = (int)Status.Saved;
                ret = 2;
            }

            IList<PaymentInfo> listPayment = ESP.Finance.BusinessLogic.PaymentManager.GetList(" ProjectID = " + projectmodel.ProjectId.ToString());
            decimal totalPay = 0;

            foreach (PaymentInfo payment in listPayment)
            {
                totalPay += Convert.ToDecimal(payment.PaymentBudget);
            }
            if (!projectmodel.isRecharge)
            {
                if (projectmodel.TotalAmount.Value != totalPay)
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('付款通知总额与项目合同总金额不符');", true);
                    return -1;
                }
            }
            else
            {
                if (Convert.ToDecimal(projectmodel.AccountsReceivable) != totalPay)
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('付款通知总额与应收金额不符');", true);
                    return -1;
                }
            }

        }
        IList<ESP.Finance.Entity.ProjectScheduleInfo> ProjectSchedules = ESP.Finance.BusinessLogic.ProjectScheduleManager.GetList("ProjectID=" + projectmodel.ProjectId.ToString());

        foreach (ProjectScheduleInfo model in ProjectSchedules)
        {
            totalPercent += model.MonthPercent.Value;
            totalFee += model.Fee == null ? 0 : model.Fee.Value;
        }
        decimal servicefee = 0;
        if (projectmodel.IsCalculateByVAT == 1)
            servicefee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFeeByVAT(projectmodel, null);
        else
            servicefee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFee(projectmodel, null);

        if (totalFee != servicefee)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('预计完工百分比输入有误.');", true);
            return -1;
        }
        ESP.Finance.Entity.ProjectHistInfo projectHistModel = this.getLastestProjectHist(projectmodel);
        ProjectInfo oldProject = projectHistModel.ProjectModel.ObjectDeserialize<ProjectInfo>();
        //合同检查
        IList<ESP.Finance.Entity.ContractInfo> contractList = ESP.Finance.BusinessLogic.ContractManager.GetList(" projectID=" + projectmodel.ProjectId.ToString() + " and isdelay=1");

        if (contractList.Count > 0 || projectmodel.TotalAmount.Value.ToString("0.00") != this.hidOddTotalAmount.Value 
            || (hidContractStatus.Value != "1" && projectmodel.ContractStatusID != null && projectmodel.ContractStatusID == 1) 
            || projectmodel.Status == (int)Status.Saved || responserDiffer == 1
            || (projectmodel.Recharge == null ? "0.00" : projectmodel.Recharge.Value.ToString("0.00")) != this.hidRechargeAmount.Value)
        {
            projectmodel.Status = (int)Status.Saved;
            ret = 2;
        }
        else if (projectmodel.Status == (int)Status.FinanceAuditComplete)
        {
            projectmodel.Status = (int)Status.FinanceAuditComplete;
            ret = 1;
        }
        else if (projectmodel.Status == (int)Status.Waiting)
        {
            projectmodel.Status = (int)Status.BizAuditComplete;
            ret = 1;
        }
        else
        {
            ret = 1;
        }

        if (oldProject != null)
        {
            //成本校验是否有变化
            //旧版本：this.hidOddCost.Value != (Convert.ToDecimal(this.hidOddTotalAmount.Value) - ESP.Finance.BusinessLogic.CheckerManager.GetProjectOddAmount(projectmodel.ProjectId)).ToString("0.00")
            decimal newCost = ContractCostManager.GetTotalAmountByProject(projectmodel.ProjectId);
            newCost += ProjectExpenseManager.GetTotalExpense(projectmodel.ProjectId);
            newCost += SupporterManager.GetTotalAmountByProject(projectmodel.ProjectId);
            //历史总成本
            decimal oldCost = oldProject.CostDetails.Sum(x => x.Cost.Value);
            oldCost += oldProject.Expenses.Sum(x => x.Expense.Value);
            oldCost += oldProject.Supporters.Sum(x => x.BudgetAllocation.Value);

            if (newCost != oldCost)
            {
                projectmodel.Status = (int)Status.Saved;
                ret = 2;
            }
        }
        else
        {
            if (this.hidOddCost.Value != (Convert.ToDecimal(this.hidOddTotalAmount.Value) - ESP.Finance.BusinessLogic.CheckerManager.GetProjectOddAmount(projectmodel.ProjectId)).ToString("0.00"))
            {
                projectmodel.Status = (int)Status.Saved;
                ret = 2;
            }
        }


        if (!string.IsNullOrEmpty(hidBankID.Value))
        {
            projectmodel.BankId = int.Parse(hidBankID.Value);
        }

        UpdateResult result = ESP.Finance.BusinessLogic.ProjectManager.Update(projectmodel);
        if (result == UpdateResult.Succeed)
        {
            if (CurrentUserRight())
            {
                ESP.Finance.BusinessLogic.PaymentManager.UpdatePaymentBankInfo(projectmodel.ProjectId, projectmodel.BankId);
                //同步项目结束日期
                if (!(string.IsNullOrEmpty(projectmodel.ProjectCode)) && projectmodel.ProjectTypeName == ESP.Finance.Utility.ProjectType.ShortTerm)
                {
                    ESP.Finance.BusinessLogic.PaymentManager.UpdatePaymentConfirmMonth(projectmodel.ProjectId, projectmodel.EndDate.Value.Year, projectmodel.EndDate.Value.Month);
                }
            }
            return ret;
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + result.ToString() + "');", true);
            return 0;
        }
    }



}