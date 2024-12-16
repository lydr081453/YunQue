using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Utility;
public partial class project_ProjectPrint : System.Web.UI.Page
{
    int projectid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Response.ContentEncoding = System.Text.Encoding.UTF8;

        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
        {
            projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
        }
        if (!IsPostBack)
        {
            InitPageContent();
        }
    }

    void InitPageContent()
    {
        if (projectid == 0) return;

        decimal VATParam1 = 1;
        ESP.Finance.Entity.ProjectInfo project = ESP.Finance.BusinessLogic.ProjectManager.GetModel(projectid);
        ESP.Finance.Entity.TaxRateInfo rateModel = null;
        ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(project.BranchCode);
        imgLogo.ImageUrl = "/images/" + branchModel.Logo;

        if (project.ContractTaxID != null && project.ContractTaxID.Value != 0)
        {
            rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(project.ContractTaxID.Value);
            VATParam1 = rateModel.VATParam1;
        }
        if (project == null) return;


        //基本信息
        labPrjCode.Text = project.ProjectCode;
        labBDPrjCode.Text = project.BDProjectCode;
       // labIsFromjoint.Text = project.IsFromJoint ? "是" : "否";
        //所有部门级联字符串拼接
        List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
        depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(project.GroupID.Value, depList);
        string groupnames = string.Empty;
        foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
        {
            groupnames += dept.DepartmentName + "-";
        }
        this.labPrjGroup.Text = groupnames.Substring(0, groupnames.Length - 1);
        lblSerialCode.Text = "流水号:" + project.SerialCode;
        labConStatus.Text = project.ContractStatusName;
        labBizType.Text = project.BusinessTypeName;
        labPrjType.Text = project.ProjectTypeName;
        lblCycle.Text = project.PayCycle;
        lblInvoice.Text = project.IsNeedInvoice == 1 ? "是" : "否";

        //项目名称
        labPrjName.Text = project.BusinessDescription;
        decimal totalD = project.TotalAmount == null ? 0 : project.TotalAmount.Value;
        decimal taxD = project.ContractTax == null ? 0 : project.ContractTax.Value;
        decimal taxfee = 0;
        if (project.IsCalculateByVAT == 1)
            taxfee = ESP.Finance.BusinessLogic.CheckerManager.GetTaxByVAT(project, rateModel);
        else
            taxfee = ESP.Finance.BusinessLogic.CheckerManager.GetTax(project, rateModel);

        this.lblTaxFee.Text = taxfee.ToString("#,##0.00");

        
        if (project.IsCalculateByVAT == 1)
        {
            this.lblTotalNoVAT.Text = (project.TotalAmount.Value / VATParam1).ToString("#,##0.00");
            this.labServiceFee.Text = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFeeByVAT(project, rateModel).ToString("#,##0.00");
        }
        else
            this.labServiceFee.Text = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFee(project, rateModel).ToString("#,##0.00");

        if (project.TotalAmount == null || project.TotalAmount == 0)
        {
            lblProfileRate.Text = string.Empty;
        }
        else
        {
            lblProfileRate.Text = (Convert.ToDecimal(this.labServiceFee.Text) / Convert.ToDecimal(project.TotalAmount) * 100).ToString("#,##0.00");
        }
        IList<ProjectScheduleInfo> list = ESP.Finance.BusinessLogic.ProjectScheduleManager.GetList(" ProjectID =" + project.ProjectId);
        string projectSchedule = string.Empty;
        foreach (ProjectScheduleInfo schedule in list)
        {
            projectSchedule += schedule.YearValue.ToString() + "年" + schedule.monthValue.ToString() + "月:" + schedule.MonthPercent.Value.ToString("#,##0.00") + "%;";
            if (labServiceFee.Text != string.Empty && labServiceFee.Text != "0.00")
            {
                decimal contractTax = project.ContractTax == null ? 0 : project.ContractTax.Value;
                projectSchedule += "Fee : " + schedule.Fee.Value.ToString("#,##0.00") + "(" + (schedule.Fee.Value / (1 - contractTax / 100)).ToString("#,##0.00") + ") ;";
            }
        }
        lblPercent.Text = projectSchedule;

        //项目信息
        if (project.BeginDate != null)
            labBizStartDate.Text = Convert.ToDateTime(project.BeginDate).ToString("yyyy-MM-dd");

        if (project.EndDate != null)
            labBizFinishDate.Text = Convert.ToDateTime(project.EndDate).ToString("yyyy-MM-dd");

        if (project.ContractServiceFee != null)
            labServiceFee.Text = project.ContractServiceFee.Value.ToString("#,##0.00");
        //if (project.ContractTax != null)
        //    labTaxRate.Text = project.ContractTax.Value.ToString("#,##0.00");

        if (project.TotalAmount != null)
            labTotalAmount.Text = project.TotalAmount.Value.ToString("#,##0.00");



        //客户信息
        if (project.Customer != null)
        {
            labCustShortEn.Text = project.Customer.ShortEN;
            labCustomerCName.Text = project.Customer.FullNameCN;
            labCustomerEName.Text = project.Customer.FullNameEN;
            labInvoiceTitle.Text = project.Customer.InvoiceTitle;
            labLinkerName.Text = project.Customer.ContactName + "/" + project.Customer.ContactPosition;
            labLinkerPhone.Text = project.Customer.ContactMobile + "/" + project.Customer.ContactFax;
            labLinkerEmail.Text = project.Customer.ContactEmail;
            labCustWebSite.Text = project.Customer.Website;
            labCustAddress.Text = project.Customer.Address1 + " " + project.Customer.Address2 + "(" + project.Customer.PostCode + ")";
            labCustArea.Text = project.Customer.AreaName;
            labCustIndustry.Text = project.Customer.IndustryName;
        }

        //项目组成员
        if (project.Members != null && project.Members.Count > 0)
        {
            string formater = @"<tr>
                        <td height='25' align='center'>
                            {0}
                        </td>
                        <td align='center'>
                            {1}
                        </td>
                        <td align='center'>
                            {2}
                        </td>
                        <td align='center'>
                            {3}
                        </td>
                        <td align='center'>
                            {4}
                        </td>
                    </tr>";

            string mem_content = string.Empty;
            foreach (ESP.Finance.Entity.ProjectMemberInfo mem in project.Members)
            {
                if (mem != null)
                {
                    string realname = string.IsNullOrEmpty(mem.MemberEmployeeName) ? "&nbsp;" : mem.MemberEmployeeName;
                    string code = string.IsNullOrEmpty(mem.MemberCode) ? "&nbsp;" : mem.MemberCode;
                    string username = string.IsNullOrEmpty(mem.MemberUserName) ? "&nbsp;" : mem.MemberUserName;
                    string mail = string.IsNullOrEmpty(mem.MemberEmail) ? "&nbsp;" : mem.MemberEmail;
                    string phone = string.IsNullOrEmpty(mem.MemberPhone) ? "&nbsp;" : mem.MemberPhone;

                    if (phone[phone.Length - 1] == '-')
                        phone = phone.Substring(0, phone.Length - 1);
                    mem_content += string.Format(formater, realname, code, username, mail, phone);
                }
            }
            ltPrjMem.Text = mem_content;
        }

        //支持方信息
        if (project.Supporters != null && project.Supporters.Count > 0)
        {
            string formater = @"<tr>
                        <td height='25' align='center'>
                            {0}
                        </td>
                        <td  align='center'>
                           {1}
                        </td>
                        <td align='right'>
                            {2}
                        </td>
                        <td  align='right'>
                            {3}
                        </td>
                        <td  align='right'>
                            {4}
                        </td>
                        <td  align='center'>
                            {5}
                        </td>
                    </tr>";
            string supporter_content = string.Empty;
            decimal supporterCostTotal = 0;
            decimal supporterTaxTotal = 0;
            foreach (ESP.Finance.Entity.SupporterInfo sup in project.Supporters)
            {
                if (sup != null)
                {
                    string groupname = string.IsNullOrEmpty(sup.GroupName) ? "&nbsp;" : sup.GroupName;
                    string leader = string.IsNullOrEmpty(sup.LeaderEmployeeName) ? "&nbsp;" : sup.LeaderEmployeeName;
                    string amount = sup.BudgetAllocation == null ? "&nbsp;" : sup.BudgetAllocation.Value.ToString("#,##0.00");
                    string type = sup.IncomeType;
                    decimal suptax=0;
                    string NoVAT = "";
                    string taxVAT = "";

                    if (project.IsCalculateByVAT == 1 && (project.ContractTaxID != null && project.ContractTaxID.Value != 0))
                    {
                        suptax = CheckerManager.GetSupporterTaxByVAT(sup, project, rateModel);
                        NoVAT = (sup.BudgetAllocation.Value / rateModel.VATParam1).ToString("#,##0.00");
                        taxVAT = suptax.ToString("#,##0.00");

                        supporterTaxTotal +=suptax;
                    }
                    
                    if (sup.BudgetAllocation != null)
                        supporterCostTotal += Convert.ToDecimal(sup.BudgetAllocation);

                    supporter_content += string.Format(formater, groupname, leader, amount,NoVAT,taxVAT, type);

                   
                }
            }
            this.lblSupporterTot.Text = supporterCostTotal.ToString("#,##0.00");
            this.lblSupporterTax.Text = supporterTaxTotal.ToString("#,##0.00");
            ltSupporters.Text = supporter_content;
        }


        //付款通知信息
        if (project.Payments != null && project.Payments.Count > 0)
        {
            string formater = @"<tr>
                        <td height='25' align='center'>
                            {0}
                        </td>
                        <td align='center'>
                            {1}
                        </td>
                        <td align='right'>
                            {2}
                        </td>
                        <td align='center'>
                            {3}
                        </td>
                    </tr>";
            string pay_content = string.Empty;
            foreach (ESP.Finance.Entity.PaymentInfo payment in project.Payments)
            {
                if (payment != null)
                {
                    string date = "&nbsp;";
                    try
                    {
                        date = payment.PaymentPreDate.ToString("yyyy-MM-dd");
                    }
                    catch { }
                    string content = string.IsNullOrEmpty(payment.PaymentContent) ? "&nbsp;" : payment.PaymentContent;
                    string amount = payment.PaymentBudget == null ? "&nbsp;" : payment.PaymentBudget.Value.ToString("#,##0.00");
                    string remark = string.IsNullOrEmpty(payment.Remark) ? "&nbsp;" : payment.Remark;
                    pay_content += string.Format(formater, date, content, amount, remark);
                }
            }
            ltPayment.Text = pay_content;
        }


        //成本明细
        decimal costTotal = 0;
        string Costformater = @"<tr>
                        <td height='25' align='center'>
                           {0}
                        </td>
                        <td align='center'>
                            {1}
                        </td>
                        <td align='left' colspan='2'>
                            {2}
                        </td>
                    </tr>";
        string contract_content = string.Empty;

        if (project.CostDetails != null && project.CostDetails.Count > 0)
        {
            foreach (ESP.Finance.Entity.ContractCostInfo cost in project.CostDetails)
            {
                if (cost != null)
                {

                    string costdes = string.IsNullOrEmpty(cost.Description) ? "&nbsp;" : cost.Description;
                    string amount = cost.Cost == null ? "&nbsp;" : cost.Cost.Value.ToString("#,##0.00");
                    string remark = string.IsNullOrEmpty(cost.Remark) ? "&nbsp;" : cost.Remark;
                    contract_content += string.Format(Costformater, costdes, amount, remark);

                    costTotal += Convert.ToDecimal(cost.Cost);
                }
            }
            ltContractDetail.Text = contract_content;

        }
        if (project.Status != (int)ESP.Finance.Utility.Status.Saved && project.Status != (int)ESP.Finance.Utility.Status.Submit)
        {
            //IList<ESP.Finance.Entity.AuditHistoryInfo> auditlist = ESP.Finance.BusinessLogic.AuditHistoryManager.GetList(" projectid=" + Request[ESP.Finance.Utility.RequestName.ProjectID]);
            string logstr = string.Empty;
            string auditStatus = string.Empty;
            string auditDate = string.Empty;

            //foreach (ESP.Finance.Entity.AuditHistoryInfo au in auditlist)
            //{
            //    if (au.AuditStatus == (int)AuditHistoryStatus.PassAuditing)
            //    {
            //        auditStatus = "审批通过";
            //        auditDate = au.AuditDate == null ? "" : au.AuditDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
            //        logstr += au.AuditorEmployeeName + auditStatus + "[" + auditDate + "] " + au.Suggestion + "<br/>";
            //    }
            //    else if (au.AuditStatus == (int)AuditHistoryStatus.TerminateAuditing)
            //    {
            //        auditStatus = "审批驳回";
            //        auditDate = au.AuditDate == null ? "" : au.AuditDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
            //        logstr += au.AuditorEmployeeName + auditStatus + "[" + auditDate + "] " + au.Suggestion + "<br/>";
            //    }

            //}
            IList<AuditLogInfo> auditLogList = AuditLogManager.GetProjectList(project.ProjectId);
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
                else if (log.AuditStatus == (int)ESP.Finance.Utility.AuditHistoryStatus.CommitChangedProject)
                {
                    logstr += log.AuditorEmployeeName + log.Suggestion + "[" + auditDate + "]<br />";
                }
            }
            this.lblLog.Text = logstr;
        }
        else
        {
            this.lblLog.Visible = false;
        }
        IList<ProjectExpenseInfo> expenseList = ESP.Finance.BusinessLogic.ProjectExpenseManager.GetList("ProjectID=" + projectid.ToString());
        if (expenseList != null && expenseList.Count > 0)
        {
            string formaterEx = @"<tr>
                        <td height='25' align='center'>
                           {0}
                        </td>
                        <td align='center'>
                            {1}
                        </td>
                        <td align='left' colspan='2'>
                            {2}
                        </td>
                    </tr>";
            string contract_Expense = string.Empty;
            foreach (ProjectExpenseInfo exp in expenseList)
            {
                string costdes = string.IsNullOrEmpty(exp.Description) ? "&nbsp;" : exp.Description;
                string amount = exp.Expense == null ? "&nbsp;" : exp.Expense.Value.ToString("#,##0.00");
                string remark = string.IsNullOrEmpty(exp.Remark) ? "&nbsp;" : exp.Remark;
                contract_Expense += string.Format(formaterEx, costdes, amount, remark);
                costTotal += Convert.ToDecimal(exp.Expense);
            }
            ltContractDetail.Text += contract_Expense;
        }
        if (costTotal > 0)
            this.lblCostTot.Text = costTotal.ToString("#,##0.00");

        string strWhere = " projectId = @projectId and status=@status";
        List<System.Data.SqlClient.SqlParameter> parms = new List<System.Data.SqlClient.SqlParameter>();
        parms.Add(new System.Data.SqlClient.SqlParameter("@projectId", projectid));
        parms.Add(new System.Data.SqlClient.SqlParameter("@status", ESP.Finance.Utility.ApplyForInvioceStatus.Status.Audited));
        repContract.DataSource = ESP.Finance.BusinessLogic.ApplyForInvioceManager.GetList(strWhere, parms).OrderByDescending(x => x.CreateDate).ToList();
        repContract.DataBind();
    }
}
