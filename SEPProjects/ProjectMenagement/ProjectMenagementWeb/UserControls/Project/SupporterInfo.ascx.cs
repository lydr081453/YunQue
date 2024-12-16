using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
using System.Text;
using ESP.Finance.Utility;

public partial class UserControls_Project_SupporterInfo : System.Web.UI.UserControl
{
    ProjectInfo project = null;
    SupporterInfo supporter = null;
    protected void Page_Load(object sender, EventArgs e)
    {
         project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
         supporter = ESP.Finance.BusinessLogic.SupporterManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]));

        if (!IsPostBack)
        {
            if (project != null)
            {
                BindProject(project);
                BindCosts();
                BindMember();
            }
        }
    }

    public ESP.Finance.Entity.SupporterInfo GetSupporterToSave()
    {
        
        if (supporter != null)
        {
            supporter.ApplicantDate = DateTime.Now;
            supporter.ApplicantEmployeeName = supporter.LeaderEmployeeName;
            decimal fee = 0;
            if (this.txtIncomeFee.Text != string.Empty)
            { fee = Convert.ToDecimal(this.txtIncomeFee.Text); }
            supporter.IncomeFee = fee;
            decimal tax = 0;
            if (this.txtBilledTax.Text != string.Empty)
            { tax = Convert.ToDecimal(this.txtBilledTax.Text); }
            supporter.BilledTax = tax;
            //supporter.CompletedPercent = this.txtOtherRequest.Text;

            if (this.txtBeginDate.Text != string.Empty)
                supporter.BizBeginDate = Convert.ToDateTime(this.txtBeginDate.Text);
            if (this.txtEndDate.Text != string.Empty)
                supporter.BizEndDate = Convert.ToDateTime(this.txtEndDate.Text);
           // SaveSchedule(supporter);
        }
        if (this.hidLeaderID.Value != this.hidOldLeaderID.Value)
        {
            supporter.LeaderUserID = Convert.ToInt32(this.hidLeaderID.Value);
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(supporter.LeaderUserID.Value);
            supporter.LeaderEmployeeName = emp.Name;
            supporter.Status = (int)ESP.Finance.Utility.Status.Saved;
        }
        return supporter;
    }
    protected void btnPercent_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.SupportID]))
        {
           IList<SupporterScheduleInfo> scheduleList = ESP.Finance.BusinessLogic.SupporterScheduleManager.GetList(" SupporterID=" + Request[ESP.Finance.Utility.RequestName.SupportID]);
           if (supporter.IncomeType != "Cost")
           {
               this.gvPercent.DataSource = scheduleList;
               this.gvPercent.DataBind();
               bindTotalPercent(scheduleList);
           }
           if (this.gvPercent.Rows.Count == 0)
           {
               this.trGrid.Visible = false;
               this.trNoRecord.Visible = true;
           }
           else
           {
               this.trGrid.Visible = true;
               this.trNoRecord.Visible = false;
           }
        }
       
    }
    protected string GetGroupID
    {
        get
        {
            string groupID = "0";
            if (supporter != null)
                groupID = supporter.GroupID.ToString();

            return groupID;
        }
    }

    protected void btnMember_Click(object sender, EventArgs e)
    {
        BindMember();
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
            Label lblName = (Label)e.Row.FindControl("lblName");
            lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(memberInfo.MemberUserID.Value) + "');");
        }
    }

    protected void gvMember_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int memberid = int.Parse(e.CommandArgument.ToString());
            ESP.Finance.BusinessLogic.SupportMemberManager.Delete(memberid);
            BindMember();
        }

    }
    #endregion

    protected void btnCost_Click(object sender, EventArgs e)
    {
        supporter.BizBeginDate = Convert.ToDateTime(this.lblBeginDate.Text);
        supporter.BizEndDate = Convert.ToDateTime(this.lblEndDate.Text);
        ESP.Finance.BusinessLogic.SupporterManager.Update(supporter);
        BindCosts();
    }

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
        this.lblTotalCost.Text ="总计:"+ total.ToString("#,##0.00");
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
            //ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
            //ESP.Finance.Entity.SupporterInfo supporterModel = ESP.Finance.BusinessLogic.SupporterManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]));
            //lblUsedCost.Text = ESP.Finance.BusinessLogic.CheckerManager.得到某一成本报销使用总额(projectModel.ProjectCode, supporterModel.GroupID.Value, 0).ToString("#,##0.00");

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
            //ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
            //ESP.Finance.Entity.SupporterInfo supporterModel = ESP.Finance.BusinessLogic.SupporterManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]));
            //lblUsedCost.Text = (ESP.Finance.BusinessLogic.CheckerManager.得到某一成本PR使用总额(projectModel.ProjectCode, supporterModel.GroupID.Value, item.CostTypeID.Value) +
            //                                ESP.Finance.BusinessLogic.CheckerManager.得到某一成本报销使用总额(projectModel.ProjectCode, supporterModel.GroupID.Value, item.CostTypeID.Value)).ToString("#,##0.00");

        }
    }
    protected void gvPercent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
            ESP.Finance.Entity.SupporterScheduleInfo schedule= (ESP.Finance.Entity.SupporterScheduleInfo)e.Row.DataItem;
            Label lblno = (Label)e.Row.FindControl("lblNo");
            Label lblFeeTax = (Label)e.Row.FindControl("lblFeeTax");
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
            IList<SupporterScheduleInfo> scheduleList = ESP.Finance.BusinessLogic.SupporterScheduleManager.GetList(" SupporterID=" + supporter.SupportID.ToString());
            depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(supporter.GroupID.Value, depList);
            groupname = string.Empty;
            foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
            {
                groupname += dept.DepartmentName + "-";
            }
            this.lblSupportGroup.Text = groupname.Substring(0, groupname.Length - 1);

            this.hidGroupID.Value = supporter.GroupID.Value.ToString();
            this.hidLeaderID.Value = supporter.LeaderUserID.Value.ToString();
            hidOldLeaderID.Value = supporter.LeaderUserID.Value.ToString();
            if (supporter != null)
            {
                if (supporter.IncomeType != "Cost")
                {
                    this.btnAddPercent.Enabled = true;
                    this.gvPercent.DataSource = scheduleList;
                    this.gvPercent.DataBind();
                }
                else
                {
                    this.btnAddPercent.Enabled = false;
                }
                if (supporter.IncomeType == "Fee")
                {
                    this.btnAddCost.Enabled = false;
                }
                if (gvPercent.Rows.Count > 0)
                {
                    this.trNoRecord.Visible = false;
                    this.trGrid.Visible = true;
                }
                else
                {
                    this.trNoRecord.Visible = true;
                    this.trGrid.Visible = false;
                }
                bindTotalPercent(scheduleList);
                this.lblServiceDescription.Text = project.BusinessDescription;
                this.lblServiceType.Text = supporter.ServiceType;
                this.lblBudgetAllocation.Text = "0.00";
                this.hidBeginDate.Value = project.BeginDate == null ? "" : project.BeginDate.Value.ToString("yyyy-MM-dd");
                this.hidEndDate.Value = project.EndDate == null ? "" : project.EndDate.Value.ToString("yyyy-MM-dd");
                this.lblBeginDate.Text = project.BeginDate == null ? "" : project.BeginDate.Value.ToString("yyyy-MM-dd");
                this.lblEndDate.Text = project.EndDate == null ? "" : project.EndDate.Value.ToString("yyyy-MM-dd");

                //VAT
                this.lblTaxFee.Text = "";
                this.lblTotalNoVAT.Text = "";

                decimal taxfee = 0;
                ESP.Finance.Entity.TaxRateInfo rateModel=null;
                if (project.ContractTaxID != null && project.ContractTaxID.Value != 0)
                {
                    rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(project.ContractTaxID.Value);
                }
                if (rateModel != null)
                {
                    if (project.IsCalculateByVAT == 1)
                    {
                        this.lblTotalNoVAT.Text = Convert.ToDecimal(supporter.BudgetAllocation / rateModel.VATParam1).ToString("0.00");
                        taxfee = CheckerManager.GetSupporterTaxByVAT(supporter,project, rateModel);
                    }
                    
                    this.lblTaxFee.Text = taxfee.ToString("#,##0.00");
                }
                //end VAT

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
            }
        }
    }

    private void bindTotalPercent(IList<SupporterScheduleInfo> scheduleList)
    {
        decimal totalPercent = 0;
        decimal totalFee = 0;
        foreach (SupporterScheduleInfo model in scheduleList)
        {
            totalPercent += model.MonthPercent==null?0:model.MonthPercent.Value;
            totalFee += model.Fee==null?0:model.Fee.Value;
        }
        this.lblTotalPercent.Text = totalPercent.ToString("#,##0.00");
        this.lblTotalFee.Text = totalFee.ToString("#,##0.00");
    }

    protected void btnAddPercent_Click(object sender, EventArgs e)
    {
        supporter.BizBeginDate = Convert.ToDateTime(this.lblBeginDate.Text);
        supporter.BizEndDate = Convert.ToDateTime(this.lblEndDate.Text);

        ESP.Finance.BusinessLogic.SupporterManager.Update(supporter);
    }
}