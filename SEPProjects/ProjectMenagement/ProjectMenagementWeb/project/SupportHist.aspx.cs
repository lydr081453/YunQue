using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Utility;
using System.Text;

namespace FinanceWeb.project
{
    public partial class SupportHist : ESP.Web.UI.PageBase
    {
        IList<ESP.Finance.Entity.SupportHistoryInfo> SupporterList = null;
        string term = string.Empty;
        List<System.Data.SqlClient.SqlParameter> paramList = null;
        private string SequenceState = "Sequence";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SupporterList = getSupporterList();
                if (SupporterList != null && SupporterList.Count != 0)
                {
                    try
                    {
                        InitPageContent(SupporterList[0].HistoryData.ObjectDeserialize<SupporterInfo>());
                        ViewState[SequenceState] = 0;
                    }
                    catch
                    { }
                    this.lblRepresent.Text = "当前是第" + (Convert.ToInt32(ViewState[SequenceState]) + 1).ToString() + "条记录 / 共" + SupporterList.Count.ToString() + "条记录";
                }
            }
        }

        protected void btnFirst_Click(object sender, EventArgs e)
        {
            if (SupporterList == null)
            {
                SupporterList = getSupporterList();
            }

            if (SupporterList != null && SupporterList.Count != 0)
            {
                try
                {
                    this.InitPageContent(SupporterList[0].HistoryData.ObjectDeserialize<ESP.Finance.Entity.SupporterInfo>());
                    ViewState[SequenceState] = 0;
                }
                catch
                { }
                this.lblRepresent.Text = "当前是第" + (Convert.ToInt32(ViewState[SequenceState]) + 1).ToString() + "条记录 / 共" + SupporterList.Count.ToString() + "条记录";
            }
        }

        protected void btnPret_Click(object sender, EventArgs e)
        {
            if (SupporterList == null)
            {
                SupporterList = getSupporterList();
            }
            if (SupporterList != null && SupporterList.Count != 0)
            {
                int seq = Convert.ToInt32(ViewState[SequenceState]) - 1;
                if (seq - 1 < 0)
                    seq = 0;
                try
                {
                    InitPageContent(SupporterList[seq].HistoryData.ObjectDeserialize<SupporterInfo>());
                    ViewState[SequenceState] = seq;
                }
                catch
                { }
                this.lblRepresent.Text = "当前是第" + (Convert.ToInt32(ViewState[SequenceState]) + 1).ToString() + "条记录 / 共" + SupporterList.Count.ToString() + "条记录";

            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (SupporterList == null)
            {
                SupporterList = getSupporterList();
            }

            if (SupporterList != null && SupporterList.Count != 0)
            {
                int seq = Convert.ToInt32(ViewState[SequenceState]) + 1;
                if (seq + 1 >= SupporterList.Count)
                    seq = SupporterList.Count - 1;
                try
                {
                    InitPageContent(SupporterList[seq].HistoryData.ObjectDeserialize<SupporterInfo>());
                    ViewState[SequenceState] = seq;
                }
                catch
                { }
                this.lblRepresent.Text = "当前是第" + (Convert.ToInt32(ViewState[SequenceState]) + 1).ToString() + "条记录 / 共" + SupporterList.Count.ToString() + "条记录";

            }
        }

        private void InitPageContent(SupporterInfo HistModel)
        {
            if (HistModel != null)
            {
                ESP.Finance.Entity.ProjectInfo project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(HistModel.ProjectID);
                BindProject(project);
                BindCosts(HistModel.SupporterCosts, HistModel.SupporterExpenses);
                BindMember(HistModel.SupporterMembers);
                BindSupporterSchedule(HistModel);
            }
        }

        private void BindCosts(IList<ESP.Finance.Entity.SupporterCostInfo> costs,IList<ESP.Finance.Entity.SupporterExpenseInfo> expenses)
        {
             this.gvCost.DataSource = costs;
            this.gvCost.DataBind();

            this.gvExpense.DataSource = expenses;
            this.gvExpense.DataBind();

            BindTotal(costs, expenses);
        }

        private void BindTotal(IList<SupporterCostInfo> list, IList<SupporterExpenseInfo> elist)
        {
            this.trTotalCost.Visible = true;
            this.lblTotalCost.Text = "总计:";
            decimal total = 0;
            foreach (SupporterCostInfo item in list)
            {
                total += Convert.ToDecimal(item.Amounts);
            }
            foreach (SupporterExpenseInfo eitem in elist)
            {
                total += Convert.ToDecimal(eitem.Expense);
            }
            if (total == 0)
            {
                this.trTotalCost.Visible = false;
            }
            this.lblTotal.Text = total.ToString("#,##0.00");
            this.lblTotalCost.Text = "总计:" + total.ToString("#,##0.00");
        }

        protected void gvExpense_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SupporterExpenseInfo item = (SupporterExpenseInfo)e.Row.DataItem;
                Label lblno = (Label)e.Row.FindControl("lblNo");
                lblno.Text = (e.Row.RowIndex + 1).ToString();
                Label lblExpense = (Label)e.Row.FindControl("lblExpense");
                Label lblUsedCost = (Label)e.Row.FindControl("lblUsedCost");
                if (lblExpense != null && item.Expense != null)
                {
                    lblExpense.Text = Convert.ToDecimal(item.Expense).ToString("#,##0.00");
                }
               
            }
        }

        protected void gvCost_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SupporterCostInfo item = (SupporterCostInfo)e.Row.DataItem;
                Label lblno = (Label)e.Row.FindControl("lblNo");
                lblno.Text = (e.Row.RowIndex + 1).ToString();
                Label lblCost = (Label)e.Row.FindControl("lblCost");
                Label lblUsedCost = (Label)e.Row.FindControl("lblUsedCost");
                if (lblCost != null)
                {
                    lblCost.Text = item.Amounts.ToString("#,##0.00");
                }
               
            }
        }

        protected void gvPercent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
                ESP.Finance.Entity.SupporterScheduleInfo schedule = (ESP.Finance.Entity.SupporterScheduleInfo)e.Row.DataItem;
                Label lblFeeTax = (Label)e.Row.FindControl("lblFeeTax");
                Label lblno = (Label)e.Row.FindControl("lblNo");
                lblno.Text = (e.Row.RowIndex + 1).ToString();
                if (schedule.Fee == null || projectModel == null || projectModel.ContractTax == null)
                    lblFeeTax.Text = "0.00";
                else
                    lblFeeTax.Text = (schedule.Fee.Value / (1 - projectModel.ContractTax.Value / 100)).ToString("#,##0.00");
            }
        }

        private void BindProject(ProjectInfo project)
        {
            this.lblPrjCode.Text = project.ProjectCode;
            //所有部门级联字符串拼接
            List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
            depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(project.GroupID.Value, depList);
            string groupname = string.Empty;
            foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
            {
                groupname += dept.DepartmentName + "-";
            }
            this.lblGroup.Text = groupname.Substring(0, groupname.Length - 1);

            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.SupportID]))
            {
                SupporterInfo supporter = ESP.Finance.BusinessLogic.SupporterManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]));
                //ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
                if (supporter != null)
                {
                    this.lblServiceDescription.Text = project.BusinessDescription;
                    this.lblServiceType.Text = supporter.ServiceType;
                    this.lblBudgetAllocation.Text = "0.00";
                    if (supporter.BudgetAllocation != null)
                        this.lblBudgetAllocation.Text = Convert.ToDecimal(supporter.BudgetAllocation).ToString("#,##0.00");

                    this.txtIncomeFee.Text = ESP.Finance.BusinessLogic.CheckerManager.GetSupporterOddAmount(supporter.SupportID).ToString("#,##0.00");

                    if (supporter.BilledTax != null)
                        this.txtBilledTax.Text = Convert.ToDecimal(supporter.BilledTax).ToString("#,##0.00");

                    if (supporter.BizBeginDate != null)
                        this.txtBeginDate.Text = Convert.ToDateTime(supporter.BizBeginDate).ToString("yyyy-MM-dd");
                    if (supporter.BizEndDate != null)
                        this.txtEndDate.Text = Convert.ToDateTime(supporter.BizEndDate).ToString("yyyy-MM-dd");
                    this.lblLeaderEmployeeName.Text = supporter.LeaderEmployeeName;
                    this.lblLeaderEmployeeName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(supporter.LeaderUserID.Value) + "');");
                }
            }
        }

        private void BindMember(IList<SupportMemberInfo> members)
        {
            this.gvMember.DataSource = members;
                this.gvMember.DataBind();
        }

        protected void gvMember_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ESP.Finance.Entity.SupportMemberInfo memberInfo = (ESP.Finance.Entity.SupportMemberInfo)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                Label lblMemberPhone = (Label)e.Row.FindControl("lblMemberPhone");
                if (lblMemberPhone != null && !string.IsNullOrEmpty(lblMemberPhone.Text))
                {
                    lblMemberPhone.Text = StringHelper.FormatPhoneLastChar(lblMemberPhone.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //所有部门级联字符串拼接
                Label lblGroupName = (Label)e.Row.FindControl("lblGroupName");
                List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
                depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(memberInfo.GroupID.Value, depList);
                string groupname = string.Empty;
                foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
                {
                    groupname += dept.DepartmentName + "-";
                }
                if (!string.IsNullOrEmpty(groupname))
                    lblGroupName.Text = groupname.Substring(0, groupname.Length - 1);

                Label lblName = (Label)e.Row.FindControl("lblName");
                lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(memberInfo.MemberUserID.Value) + "');");
            }
        }



        private void bindTotalPercent(IList<SupporterScheduleInfo> scheduleList)
        {
            decimal totalPercent = 0;
            decimal totalFee = 0;
            foreach (SupporterScheduleInfo model in scheduleList)
            {
                totalPercent += model.MonthPercent == null ? 0 : model.MonthPercent.Value;
                totalFee += model.Fee == null ? 0 : model.Fee.Value;
            }
            this.lblTotalPercent.Text = totalPercent.ToString("0.00");
            this.lblTotalFee.Text = totalFee.ToString("#,##0.00");
        }

        private void BindSupporterSchedule(SupporterInfo supporterModel)
        {
            if (supporterModel.IncomeType != "Cost")
            {
                this.gvPercent.DataSource = supporterModel.SupporterSchedules;
                this.gvPercent.DataBind();
                bindTotalPercent(supporterModel.SupporterSchedules);
            }
        }



        private IList<ESP.Finance.Entity.SupportHistoryInfo> getSupporterList()
        {
            paramList = new List<SqlParameter>();
            int supportId = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]);
            SupporterList = ESP.Finance.BusinessLogic.SupportHistoryManager.GetListBySupport(supportId);
            var tmplist = SupporterList.OrderByDescending(N => N.CommitDate).ToList();
            return tmplist;
        }


        protected void btnLast_Click(object sender, EventArgs e)
        {
            if (SupporterList == null)
            {
                SupporterList = getSupporterList();
            }
            if (SupporterList != null && SupporterList.Count != 0)
            {
                int seq = SupporterList.Count - 1;
                try
                {
                    InitPageContent(SupporterList[seq].HistoryData.ObjectDeserialize<SupporterInfo>());
                    ViewState[SequenceState] = seq;
                }
                catch
                { }
                this.lblRepresent.Text = "当前是第" + (Convert.ToInt32(ViewState[SequenceState]) + 1).ToString() + "条记录 / 共" + SupporterList.Count.ToString() + "条记录";

            }
        }
    }
}
