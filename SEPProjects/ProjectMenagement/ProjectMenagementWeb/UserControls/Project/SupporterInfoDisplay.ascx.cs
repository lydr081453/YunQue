using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
using System.Text;
using ESP.Finance.Utility;
using System.Linq;

public partial class UserControls_Project_SupporterInfoDisplay : System.Web.UI.UserControl
{

    public bool DontBindOnLoad { get; set; }
    public SupporterInfo Supporter { get; set; }

    public IList<SupporterCostInfo> list { get; set; }
    public IList<SupporterExpenseInfo> expenselist { get; set; }
    public Dictionary<int, decimal> CostMappings { get; set; }
    public Dictionary<int, decimal> ExpenseMappings { get; set; }
    public decimal TraficFee { get; set; }
    public List<CostRecordInfo> ExpenseRecords;
    public List<CostRecordInfo> PRRecords;
    public IList<ReturnInfo> ReturnList; 
    IList<ESP.Framework.Entity.DepartmentInfo> Departments;
    ProjectInfo Project { get; set; }

    #region Bind Data
    private string GetDepartmentFullName(int departmentId)
    {
        List<string> groupnames = new List<string>(3);
        var dep = this.Departments.Where(x => x.DepartmentID == departmentId).FirstOrDefault();
        while (dep != null)
        {
            groupnames.Add(dep.DepartmentName);
            dep = this.Departments.Where(x => x.DepartmentID == dep.ParentID).FirstOrDefault();
        }
        return string.Join("-", groupnames.ToArray());
    }

    private void BindProject2()
    {
        this.lblPrjCode.Text = this.Project.ProjectCode;

        this.lblGroup.Text = GetDepartmentFullName(this.Project.GroupID ?? 0);
        this.lblSupportGroup.Text = GetDepartmentFullName(this.Supporter.GroupID ?? 0);


        this.lblServiceDescription.Text = this.Project.BusinessDescription;
        this.lblServiceType.Text = this.Supporter.ServiceType;
        this.lblBudgetAllocation.Text = "0.00";
        if (this.Supporter.BudgetAllocation != null)
            this.lblBudgetAllocation.Text = this.Supporter.BudgetAllocation.Value.ToString("#,##0.00");

        var cost = list.Sum(x => x.Amounts);
        var expense = expenselist.Sum(x => x.Expense) ?? 0;
        this.txtIncomeFee.Text = ESP.Finance.BusinessLogic.CheckerManager.GetSupporterOddAmount(Supporter.SupportID).ToString("#,##0.00");


        //VAT
        this.lblTaxFee.Text = "";
        this.lblTotalNoVAT.Text = "";

        decimal taxfee = 0;
        ESP.Finance.Entity.TaxRateInfo rateModel = null;
        if (Project.ContractTaxID != null && Project.ContractTaxID.Value != 0)
        {
            rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(Project.ContractTaxID.Value);
        }
        if (rateModel != null)
        {
            if (Project.IsCalculateByVAT == 1)
            {
                this.lblTotalNoVAT.Text = Convert.ToDecimal(Supporter.BudgetAllocation / rateModel.VATParam1).ToString("#,##0.00");
                taxfee = CheckerManager.GetSupporterTaxByVAT(Supporter, Project, rateModel);
            }

            this.lblTaxFee.Text = taxfee.ToString("#,##0.00");
        }
        //end VAT

        if (this.Supporter.BilledTax != null)
            this.txtBilledTax.Text = Convert.ToDecimal(this.Supporter.BilledTax).ToString("#,##0.00");

        if (this.Supporter.BizBeginDate != null)
            this.txtBeginDate.Text = Convert.ToDateTime(this.Supporter.BizBeginDate).ToString("yyyy-MM-dd");
        if (this.Supporter.BizEndDate != null)
            this.txtEndDate.Text = Convert.ToDateTime(this.Supporter.BizEndDate).ToString("yyyy-MM-dd");
        this.lblLeaderEmployeeName.Text = this.Supporter.LeaderEmployeeName;
        this.lblLeaderEmployeeName.Attributes.Add("onclick", "javascript:showUserInfoAsync('" + this.Supporter.LeaderUserID.Value + "');");
    }

    private void BindCosts2()
    {
        this.gvCost.DataSource = list;
        this.gvCost.DataBind();

        this.gvExpense.DataSource = expenselist;
        this.gvExpense.DataBind();

        this.trTotalCost.Visible = true;
        this.lblTotalCost.Text = "总计:";
        decimal total = 0;
        foreach (SupporterCostInfo item in list)
        {
            total += item.Amounts;
        }
        foreach (SupporterExpenseInfo eitem in expenselist)
        {
            total += eitem.Expense ?? 0;
        }
        if (total == 0)
        {
            this.trTotalCost.Visible = false;
        }
        this.lblTotal.Text = total.ToString("#,##0.00");
        this.lblTotalCost.Text = "总计:" + total.ToString("#,##0.00");
    }

    private void BindMember2()
    {
        IList<SupportMemberInfo> members = ESP.Finance.BusinessLogic.SupportMemberManager.GetList(Supporter.SupportID, null);
        this.gvMember.DataSource = members;
        this.gvMember.DataBind();
    }

    private void BindSupporterSchedule2()
    {
        if (this.Supporter.IncomeType != "Cost")
        {
            IList<SupporterScheduleInfo> list = ESP.Finance.BusinessLogic.SupporterScheduleManager.GetList(this.Supporter.SupportID, null);
            this.gvPercent.DataSource = list;
            this.gvPercent.DataBind();

            decimal totalPercent = 0;
            decimal totalFee = 0;
            foreach (SupporterScheduleInfo model in list)
            {
                totalPercent += model.MonthPercent == null ? 0 : model.MonthPercent.Value;
                totalFee += model.Fee == null ? 0 : model.Fee.Value;
            }
            this.lblTotalPercent.Text = totalPercent.ToString("#,##0.00");
            this.lblTotalFee.Text = totalFee.ToString("#,##0.00");
        }
    }

    public void BindData()
    {
        Departments = ESP.Framework.BusinessLogic.DepartmentManager.GetAll();
        this.Project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Supporter.ProjectID);
        list = ESP.Finance.BusinessLogic.SupporterCostManager.GetList(Supporter.SupportID, null);
        expenselist = ESP.Finance.BusinessLogic.SupporterExpenseManager.GetList(Supporter.SupportID, null);

        gvCost.RowDataBound -= new GridViewRowEventHandler(this.gvCost_RowDataBound);
        gvCost.RowDataBound += new GridViewRowEventHandler(this.gvCost_RowDataBound2);

        gvExpense.RowDataBound -= new GridViewRowEventHandler(this.gvExpense_RowDataBound);
        gvExpense.RowDataBound += new GridViewRowEventHandler(this.gvExpense_RowDataBound2);

        gvPercent.RowDataBound -= new GridViewRowEventHandler(this.gvPercent_RowDataBound);
        gvPercent.RowDataBound += new GridViewRowEventHandler(this.gvPercent_RowDataBound2);


        gvMember.RowDataBound -= new GridViewRowEventHandler(this.gvMember_RowDataBound);
        gvMember.RowDataBound += new GridViewRowEventHandler(this.gvMember_RowDataBound2);

        BindProject2();
        BindCosts2();
        BindMember2();
        BindSupporterSchedule2();

        lblUsedCost.Text = (CostMappings.Sum(x => x.Value)+ExpenseMappings.Sum(x=>x.Value)+TraficFee).ToString("#,##0.00");
        decimal trafficPaid = ReturnList.Where(x => x.ReturnType == 20 && (x.ReturnStatus==140 || x.ReturnStatus== 136)).Sum(x => (x.FactFee ?? (x.PreFee ?? 0)));
        lblPaid.Text = (PRRecords.Sum(x => x.PaidAmount) + ExpenseRecords.Sum(x => x.PaidAmount) + trafficPaid).ToString("#,##0.00"); ;
    }
    #endregion

    #region Event Handlers

    protected void gvMember_RowDataBound2(object sender, GridViewRowEventArgs e)
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
            lblGroupName.Text = GetDepartmentFullName(memberInfo.GroupID ?? 0);

            Label lblName = (Label)e.Row.FindControl("lblName");
            lblName.Attributes.Add("onclick", "javascript:showUserInfoAsync('" + memberInfo.MemberUserID + "');");
        }
    }

    protected void gvExpense_RowDataBound2(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SupporterExpenseInfo item = (SupporterExpenseInfo)e.Row.DataItem;
            Label lblno = (Label)e.Row.FindControl("lblNo");
            lblno.Text = (e.Row.RowIndex + 1).ToString();

            Label lblExpense = (Label)e.Row.FindControl("lblExpense");
            lblExpense.Text = (item.Expense ?? 0).ToString("#,##0.00");


            decimal usedCost;
            if (item.Description == "OOP")
            {
                ExpenseMappings.TryGetValue(0, out usedCost);
            }
            else
            {
                usedCost = TraficFee;
            }
            Label lblUsedCost = (Label)e.Row.FindControl("lblUsedCost");
            lblUsedCost.Text = usedCost.ToString("#,##0.00");
        }
    }

    protected void gvCost_RowDataBound2(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SupporterCostInfo item = (SupporterCostInfo)e.Row.DataItem;

            Label lblno = (Label)e.Row.FindControl("lblNo");
            lblno.Text = (e.Row.RowIndex + 1).ToString();

            Label lblCost = (Label)e.Row.FindControl("lblCost");
            lblCost.Text = item.Amounts.ToString("#,##0.00");


            decimal used = 0, used2 = 0;
            CostMappings.TryGetValue(item.CostTypeID.Value, out used);
            ExpenseMappings.TryGetValue(item.CostTypeID.Value, out used2);

            Label lblUsedCost = (Label)e.Row.FindControl("lblUsedCost");
            lblUsedCost.Text = (used + used2).ToString("#,##0.00");
        }
    }

    protected void gvPercent_RowDataBound2(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ESP.Finance.Entity.SupporterScheduleInfo schedule = (ESP.Finance.Entity.SupporterScheduleInfo)e.Row.DataItem;
            Label lblFeeTax = (Label)e.Row.FindControl("lblFeeTax");
            Label lblno = (Label)e.Row.FindControl("lblNo");
            lblno.Text = (e.Row.RowIndex + 1).ToString();
            if (schedule.Fee == null || Project == null || Project.ContractTax == null)
                lblFeeTax.Text = "0.00";
            else
                lblFeeTax.Text = (schedule.Fee.Value / (1 - Project.ContractTax.Value / 100)).ToString("#,##0.00");
        }
    }

    #endregion


    #region unoptimized Old Code
    protected void Page_Load(object sender, EventArgs e)
    {
        if (DontBindOnLoad)
            return;

        if (!IsPostBack)
        {
            ProjectInfo project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
            if (project != null)
            {
                BindProject(project);
                BindCosts();
                BindMember();
                BindSupporterSchedule(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]));
            }
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
        this.lblTotalPercent.Text = totalPercent.ToString("#,##0.00");
        this.lblTotalFee.Text = totalFee.ToString("#,##0.00");
    }

    private void BindSupporterSchedule(int supportID)
    {
        ESP.Finance.Entity.SupporterInfo supporterModel = ESP.Finance.BusinessLogic.SupporterManager.GetModel(supportID);

        IList<SupporterScheduleInfo> list = ESP.Finance.BusinessLogic.SupporterScheduleManager.GetList(" SupporterID =" + supportID.ToString());
        if (supporterModel.IncomeType != "Cost")
        {
            this.gvPercent.DataSource = list;
            this.gvPercent.DataBind();
            bindTotalPercent(list);
        }
    }

    #region Member
    private void BindMember()
    {
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.SupportID]))
        {
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
            System.Data.SqlClient.SqlParameter p = new System.Data.SqlClient.SqlParameter("@Supportid", Request[ESP.Finance.Utility.RequestName.SupportID]);
            paramlist.Add(p);
            IList<SupportMemberInfo> members = ESP.Finance.BusinessLogic.SupportMemberManager.GetList(" SupportID=@Supportid ", paramlist);
            this.gvMember.DataSource = members;
            this.gvMember.DataBind();
        }
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

    #endregion

    #region Cost
    private void BindCosts()
    {
        StringBuilder condition = new StringBuilder();
        condition.Append(" 1=1 ");
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.SupportID]))
            condition.Append(" AND SupportId = " + Request[ESP.Finance.Utility.RequestName.SupportID]);
        IList<SupporterCostInfo> list = ESP.Finance.BusinessLogic.SupporterCostManager.GetList(condition.ToString());

        this.gvCost.DataSource = list;
        this.gvCost.DataBind();


        StringBuilder conditionEx = new StringBuilder();
        conditionEx.Append(" 1=1 ");
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.SupportID]))
            conditionEx.Append(" AND SupporterId = " + Request[ESP.Finance.Utility.RequestName.SupportID]);
        IList<SupporterExpenseInfo> expenselist = ESP.Finance.BusinessLogic.SupporterExpenseManager.GetList(conditionEx.ToString());
        this.gvExpense.DataSource = expenselist;
        this.gvExpense.DataBind();

        BindTotal(list, expenselist);
    }

    protected void gvExpense_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int memberid = int.Parse(e.CommandArgument.ToString());
            ESP.Finance.BusinessLogic.SupporterExpenseManager.Delete(memberid);
            BindCosts();
        }

    }

    protected void gvCost_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int memberid = int.Parse(e.CommandArgument.ToString());
            ESP.Finance.BusinessLogic.SupporterCostManager.Delete(memberid);

            BindCosts();
        }

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
        this.lblTotalCost.Text = "总计:"+ total.ToString("#,##0.00");
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
    #endregion

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
                lblFeeTax.Text = (schedule.Fee.Value / (1 - projectModel.ContractTax.Value /100)).ToString("#,##0.00");
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
            ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
            if (supporter != null)
            {
                depList.Clear();
                depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(supporter.GroupID.Value, depList);
                groupname = string.Empty;
                
                foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
                {
                    groupname += dept.DepartmentName + "-";
                }
                this.lblSupportGroup.Text = groupname.Substring(0, groupname.Length - 1);

                this.lblServiceDescription.Text = projectModel.BusinessDescription;
                this.lblServiceType.Text = supporter.ServiceType;
                this.lblBudgetAllocation.Text = "0.00";
                if (supporter.BudgetAllocation != null)
                    this.lblBudgetAllocation.Text = Convert.ToDecimal(supporter.BudgetAllocation).ToString("#,##0.00");

                    this.txtIncomeFee.Text = ESP.Finance.BusinessLogic.CheckerManager.GetSupporterOddAmount(supporter.SupportID).ToString("#,##0.00");

                    //VAT
                    this.lblTaxFee.Text = "";
                    this.lblTotalNoVAT.Text = "";

                    decimal taxfee = 0;
                    ESP.Finance.Entity.TaxRateInfo rateModel = null;
                    if (projectModel.ContractTaxID != null && projectModel.ContractTaxID.Value != 0)
                    {
                        rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(projectModel.ContractTaxID.Value);
                    }
                    if (rateModel != null)
                    {
                        if (projectModel.IsCalculateByVAT == 1)
                        {
                            this.lblTotalNoVAT.Text = Convert.ToDecimal(supporter.BudgetAllocation / rateModel.VATParam1).ToString("0.00");
                            taxfee = CheckerManager.GetSupporterTaxByVAT(supporter, projectModel, rateModel);
                        }

                        this.lblTaxFee.Text = taxfee.ToString("#,##0.00");
                    }
                    //end VAT


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

    #endregion
}