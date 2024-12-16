using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

public partial class project_SupporterPrint : System.Web.UI.Page
{
    int supporterid=0;
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (!IsPostBack)
        {
            string schedule=string.Empty;
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.SupportID]))
            {
                supporterid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]);
                ESP.Finance.Entity.SupporterInfo support = ESP.Finance.BusinessLogic.SupporterManager.GetModel(supporterid);
                ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(support.ProjectID);
                ESP.Finance.Entity.TaxRateInfo rateModel =null;

                if(projectModel.ContractTaxID!=null && projectModel.ContractTaxID.Value!=0)
                {
                   rateModel=ESP.Finance.BusinessLogic.TaxRateManager.GetModel(projectModel.ContractTaxID.Value);
                }
                IList<ESP.Finance.Entity.SupporterScheduleInfo> scheduleList = ESP.Finance.BusinessLogic.SupporterScheduleManager.GetList(" SupporterID=" + Request[ESP.Finance.Utility.RequestName.SupportID]);
                foreach (ESP.Finance.Entity.SupporterScheduleInfo s in scheduleList)
                {
                    string percent = s.MonthPercent == null ? "" : s.MonthPercent.Value.ToString("#,##0.00");
                    decimal fee=s.Fee==null?0:s.Fee.Value;
                    decimal contractTax = projectModel.ContractTax == null ? 0 : projectModel.ContractTax.Value;
                    schedule += s.YearValue.ToString() + "年" + s.monthValue.ToString() + "月:" + percent + "%" + fee.ToString("#,##0.00") + "(" + (fee / (1 - contractTax / 100)).ToString("#,##0.00") + ")<br/>";
                }
                this.lblAppDate.Text = support.ApplicantDate==null ?"":support.ApplicantDate.Value.ToString("yyyy-MM-dd");
                this.lblBizDesc.Text = projectModel.BusinessDescription;
                this.lblBudget.Text = support.BudgetAllocation == null ? "" : support.BudgetAllocation.Value.ToString("#,##0.00");
                this.lblFee.Text = ESP.Finance.BusinessLogic.CheckerManager.GetSupporterOddAmount(support.SupportID).ToString("#,##0.00");
                //所有部门级联字符串拼接
                List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
                depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(support.GroupID.Value, depList);
                string groupname = string.Empty;
                foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
                {
                    groupname += dept.DepartmentName + "-";
                }
                this.lblGroupName.Text = groupname.Substring(0, groupname.Length - 1);
                this.lblLeader.Text = support.LeaderEmployeeName;
                this.lblPercent.Text = schedule;
                this.lblProjectCode.Text = support.ProjectCode;
                this.lblServiceType.Text = support.ServiceType;
                this.lblTax.Text = support.BilledTax == null ? "" : support.BilledTax.Value.ToString("#,##0.00");

                if(rateModel!=null)
                {
                    this.lblBudgetNoVAT.Text = (support.BudgetAllocation.Value / rateModel.VATParam1).ToString("#,##0.00");
                    this.lblTaxVAT.Text = ESP.Finance.BusinessLogic.CheckerManager.GetSupporterTaxByVAT(support, projectModel, rateModel).ToString("#,##0.00");
                }
                //支持方成员
                string term = " SupportID=@SupportID";
                List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
                System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@SupportID", System.Data.SqlDbType.Int, 4);
                p1.SqlValue = supporterid;
                paramlist.Add(p1);
                IList<ESP.Finance.Entity.SupportMemberInfo> memberlist = ESP.Finance.BusinessLogic.SupportMemberManager.GetList(term,paramlist);
                IList<ESP.Finance.Entity.SupporterCostInfo> costlist = ESP.Finance.BusinessLogic.SupporterCostManager.GetList(term, paramlist);
                string termExpense = " SupporterID=@SupportID";
                IList<ESP.Finance.Entity.SupporterExpenseInfo> expenselist = ESP.Finance.BusinessLogic.SupporterExpenseManager.GetList(termExpense, paramlist);


                if (memberlist != null && memberlist.Count > 0)
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
                    foreach (ESP.Finance.Entity.SupportMemberInfo mem in memberlist)
                    {
                        if (mem != null)
                        {
                            string realname = string.IsNullOrEmpty(mem.MemberEmployeeName) ? "&nbsp;" : mem.MemberEmployeeName;
                            string code = string.IsNullOrEmpty(mem.MemberCode) ? "&nbsp;" : mem.MemberCode;
                            string username = string.IsNullOrEmpty(mem.MemberUserName) ? "&nbsp;" : mem.MemberUserName;
                            string mail = string.IsNullOrEmpty(mem.MemberEmail) ? "&nbsp;" : mem.MemberEmail;
                            string phone = string.IsNullOrEmpty(mem.MemberPhone) ? "&nbsp;" : mem.MemberPhone;

                            phone = StringHelper.FormatPhoneLastChar(phone);
                            mem_content += string.Format(formater, realname, code, username, mail, phone);
                        }
                    }
                    ltPrjMem.Text = mem_content;
                }
              
                //成本明细
                decimal totalCost = 0;
                if (costlist != null && costlist.Count > 0)
                {
                    string formater = @"<tr>
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
                    foreach (ESP.Finance.Entity.SupporterCostInfo cost in costlist)
                    {
                        if (cost != null)
                        {

                            string costdes = string.IsNullOrEmpty(cost.Description) ? "&nbsp;" : cost.Description;
                            // TODO: *** amount = "" if costs.Amounts == 0.0M ?
                            string amount = /*cost.Amounts == null ? "&nbsp;" : */cost.Amounts.ToString("#,##0.00");
                            string remark = string.IsNullOrEmpty(cost.Remark) ? "&nbsp;" : cost.Remark;
                            contract_content += string.Format(formater, costdes, amount, remark);
                            /*if (cost.Amounts != null)*/
                                totalCost += cost.Amounts;
                        }
                    }
                    ltContractDetail.Text = contract_content;

                }

                if (expenselist != null && expenselist.Count > 0)
                {
                    string formater = @"<tr>
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
                    string contract_Ex = string.Empty;
                    foreach (ESP.Finance.Entity.SupporterExpenseInfo expense in expenselist)
                    {
                        string costdes = string.IsNullOrEmpty(expense.Description) ? "&nbsp;" : expense.Description;
                        string amount = expense.Expense == null ? "&nbsp;" : Convert.ToDecimal(expense.Expense).ToString("#,##0.00");
                        string remark = string.IsNullOrEmpty(expense.Remark) ? "&nbsp;" : expense.Remark;
                        contract_Ex += string.Format(formater, costdes, amount, remark);
                        if (expense.Expense != null)
                            totalCost += Convert.ToDecimal(expense.Expense);
                    }
                    ltContractDetail.Text += contract_Ex;
                }
                if (totalCost > 0)
                { 
                    string formater = @"<tr>
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
                    string contract_Total = string.Empty;
                    contract_Total += string.Format(formater, "<B>合计:</ B>","<B>"+ totalCost.ToString("#,##0.00") + "</ B>", "&nbsp");
                    ltContractDetail.Text += contract_Total;
                }


                IList<ESP.Finance.Entity.SupporterAuditHistInfo> auditList = ESP.Finance.BusinessLogic.SupporterAuditHistManager.GetList("SupporterID="+Request[ESP.Finance.Utility.RequestName.SupportID]);
                string logstr = string.Empty;
                string auditStatus=string.Empty;
                string auditDate = string.Empty;

                foreach (ESP.Finance.Entity.SupporterAuditHistInfo au in auditList)
                {
                    if (au.AuditStatus == (int)AuditHistoryStatus.PassAuditing)
                    {
                        auditStatus = "审批通过";
                        auditDate = au.AuditDate == null ? "" : au.AuditDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        logstr += au.AuditorEmployeeName +auditStatus + "[" + auditDate + "] " + au.Suggestion + "<br/>";
                    }
                    else if (au.AuditStatus == (int)AuditHistoryStatus.TerminateAuditing)
                    {
                        auditStatus = "审批驳回";
                        auditDate = au.AuditDate == null ? "" : au.AuditDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        
                        logstr += au.AuditorEmployeeName+"("+au.AuditorUserName+")" + auditStatus + "[" + auditDate + "] " + au.Suggestion + "<br/>";
                    }
                }

                this.lblLog.Text = logstr;
            }
        }
    }
}
