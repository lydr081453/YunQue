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
public partial class project_ProjectHist : ESP.Web.UI.PageBase
{
    IList<ESP.Finance.Entity.ProjectHistInfo> projectList = null;
    string term = string.Empty;
    List<System.Data.SqlClient.SqlParameter> paramList = null;
    private string SequenceState = "Sequence";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            projectList = getProjectList();
            if (projectList != null && projectList.Count != 0)
            {
                try
                {
                    InitPageContent(projectList[0].ProjectModel.ObjectDeserialize<ProjectInfo>());
                    ViewState[SequenceState] = 0;
                }
                catch
                { }
                this.lblRepresent.Text = "当前是第" + (Convert.ToInt32(ViewState[SequenceState]) + 1).ToString() + "条记录 / 共" + projectList.Count.ToString() + "条记录";
            }
        }
    }

    private IList<ESP.Finance.Entity.ProjectHistInfo> getProjectList()
    {
        paramList = new List<SqlParameter>();
        int ProjectID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
        term = " ProjectID=@ProjectID";
        System.Data.SqlClient.SqlParameter p2 = new System.Data.SqlClient.SqlParameter("@ProjectID", System.Data.SqlDbType.Int, 4);
        p2.SqlValue = ProjectID;
        paramList.Add(p2);
      
        projectList = ESP.Finance.BusinessLogic.ProjectHistManager.GetList(term, paramList);
        var tmplist = projectList.OrderByDescending(N => N.SubmitDate).ToList();
        return tmplist;
    }

    private void InitPageContent(ProjectInfo HistModel)
    {
        if (HistModel != null)
        {
            InitProjectInfo(HistModel);
            InitProjectSupporterInfo(HistModel);
            initCustomerInfo(HistModel.Customer);
            BindCosts(HistModel);
            BindCustomerProject(HistModel);
            InitPaymentInfo(HistModel);
            InitProjectMember(HistModel);
        }
    }
    private void InitProjectMember(ESP.Finance.Entity.ProjectInfo projectModel)
    {
        this.gvMember.DataSource = projectModel.Members;
        this.gvMember.DataBind();
    }


    public void InitPaymentInfo(ESP.Finance.Entity.ProjectInfo customerModel)
    {
        this.gvPayment.DataSource = customerModel.Payments;
        this.gvPayment.DataBind();
        if (this.gvPayment.Rows.Count > 0)
        {
            this.trPaymentGrid.Visible = true;
            this.trPaymentNoRecord.Visible = false;
        }
        else
        {
            this.trPaymentGrid.Visible = false; ;
            this.trPaymentNoRecord.Visible = true;
        }
        gvPercent.DataSource = customerModel.ProjectSchedules;
        gvPercent.DataBind();
        if (gvPercent.Rows.Count > 0)
        {
            this.trPercentNoRecord.Visible = false;
            this.trPercentGrid.Visible = true;
        }
        else
        {
            this.trPercentGrid.Visible = false;
            this.trPercentNoRecord.Visible = true;
        }
        this.trPercentTotal.Visible = true;
        this.lblTotalPercent.Text = "总计:";
        decimal total = 0;
        foreach (PaymentInfo payment in customerModel.Payments)
        {
            total += Convert.ToDecimal(payment.PaymentBudget);
        }
        if (total == 0)
        {
            this.trPaymentTotal.Visible = false;
        }
        this.lblPaymentTotal.Text = "总计:" + total.ToString("#,##0.00");
        this.lblPaymentBlance.Text = "剩余:";
        decimal blance = 0;
        blance = Convert.ToDecimal(customerModel.TotalAmount) - total;
        this.lblPaymentBlance.Text = "剩余:" + blance.ToString("#,##0.00");

        decimal totalPercent = 0;
        decimal totalFee = 0;
        foreach (ProjectScheduleInfo model in customerModel.ProjectSchedules)
        {
            totalPercent += model.MonthPercent == null ? 0 : model.MonthPercent.Value;
            totalFee += model.Fee == null ? 0 : model.Fee.Value;
        }
        this.lblTotalFee.Text = "总计:" + totalFee.ToString("#,##0.00");
        this.lblTotalPercent.Text = "总计:" + totalPercent.ToString("0.00") + "%";
        this.lblPayCycle.Text = customerModel.PayCycle;
        if (Convert.ToBoolean(customerModel.IsNeedInvoice))
            this.lbl3rdInvoice.Text = "是";
        else
            this.lbl3rdInvoice.Text = "否";
        this.lblCustomerRemark.Text = customerModel.CustomerRemark;
    }
    private void InitProjectSupporterInfo(ESP.Finance.Entity.ProjectInfo customModel)
    {
        this.gvSupporter.DataSource = customModel.Supporters;
        this.gvSupporter.DataBind();

        if (this.gvSupporter.Rows.Count > 0)
        {
            this.trSupporterGrid.Visible = true;
            this.trSupporterNoRecord.Visible = false;
        }
        else
        {
            this.trSupporterGrid.Visible = false; ;
            this.trSupporterNoRecord.Visible = true;
        }
        this.trSupporterTotal.Visible = true;
        this.lblSupporterTotal.Text = "总计:";
        decimal total = 0;
        foreach (SupporterInfo item in customModel.Supporters)
        {
            total += Convert.ToDecimal(item.BudgetAllocation);
        }
        if (total == 0)
        {
            this.trSupporterTotal.Visible = false;
        }
        this.lblSupporterTotal.Text = "总计:" + total.ToString("#,##0.00");
    }
    private void BindCustomerProject(ESP.Finance.Entity.ProjectInfo customModel)
    {
        if (customModel != null)
        {
            if (customModel.TotalAmount != null)
                this.txtTotalAmount.Text = Convert.ToDecimal(customModel.TotalAmount).ToString("#,##0.00");
            if (customModel.EndDate != null)
                this.txtEndDate.Text = Convert.ToDateTime(customModel.EndDate).ToString("yyyy-MM-dd");
            if (customModel.BeginDate != null)
                this.txtBeginDate.Text = Convert.ToDateTime(customModel.BeginDate).ToString("yyyy-MM-dd");
            // this.txtPercent.Text = project.OtherRequest;
            this.txtBranchName.Text = customModel.BranchName;
            // this.txtPercent.Text = project.OtherRequest;
            //if (customModel.ContractTax != null)
            //    this.txtTaxRate.Text = customModel.ContractTax.ToString();
            decimal taxfee = 0;
            ESP.Finance.Entity.TaxRateInfo rateModel = null;
            if (customModel.ContractTaxID != null && customModel.ContractTaxID != 0)
            {
                rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(customModel.ContractTaxID.Value);
                txtTaxRate.Text = rateModel.Remark;

                if (customModel.IsCalculateByVAT == 1)
                {
                    this.lblTotalNoVAT.Text = Convert.ToDecimal(customModel.TotalAmount / rateModel.VATParam1).ToString("#,##0.00");
                    taxfee = CheckerManager.GetTaxByVAT(customModel, rateModel);
                }
                else
                {
                    taxfee = ESP.Finance.BusinessLogic.CheckerManager.GetTax(customModel, rateModel);
                }
                this.lblTaxFee.Text = taxfee.ToString("#,##0.00");
            }


            if (customModel.IsCalculateByVAT == 1)
                this.lblServiceFee.Text = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFeeByVAT(customModel, rateModel).ToString("#,##0.00");
            else
                this.lblServiceFee.Text = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFee(customModel, rateModel).ToString("#,##0.00");
            if (customModel.TotalAmount > 0)
            {
                lblProfileRate.Text = (Convert.ToDecimal(this.lblServiceFee.Text) / Convert.ToDecimal(customModel.TotalAmount) * 100).ToString("#,##0.00");
            }

            decimal totalContractCost = 0;
            foreach (ESP.Finance.Entity.ContractCostInfo cost in customModel.CostDetails)
            {
                totalContractCost += cost.Cost.Value;
            }
            decimal totalSupportCost = 0;
            decimal supporterTaxTotal = 0;
            foreach (ESP.Finance.Entity.SupporterInfo supporter in customModel.Supporters)
            {
                totalSupportCost += supporter.BudgetAllocation.Value;
                supporterTaxTotal += CheckerManager.GetSupporterTaxByVAT(supporter, customModel, rateModel);
            }
            this.lblSupTotal.Text = totalSupportCost.ToString("#,##0.00");
            this.lblTaxSupporter.Text = supporterTaxTotal.ToString("#,##0.00");
            
            decimal totalPrjExpense = 0;
            foreach (ESP.Finance.Entity.ProjectExpenseInfo expense in customModel.Expenses)
            {
                totalPrjExpense += expense.Expense.Value;
            }
            this.lblCostTot.Text = (totalContractCost + totalPrjExpense).ToString("#,##0.00");

            decimal fee = 0;
            if (customModel.IsCalculateByVAT == 1)
            {
                fee = (customModel.TotalAmount.Value - totalSupportCost) / rateModel.VATParam1 - taxfee - totalContractCost - totalPrjExpense;
            }
            else
            {
               fee = customModel.TotalAmount.Value - taxfee - totalContractCost - totalSupportCost - totalPrjExpense;//change totalamount * taxRate
            }

            this.lblServiceFee.Text = fee.ToString("#,##0.00");

        }
        this.gvContracts.DataSource = customModel.Contracts;
        this.gvContracts.DataBind();
    }

    private void InitProjectInfo(ESP.Finance.Entity.ProjectInfo CustomerModel)
    {
        if (CustomerModel != null)
        {
            this.lblBDProject.Text = CustomerModel.BDProjectCode;
            this.lblUpdateTime.Text = CustomerModel.SubmitDate.Value.ToString("yyyy-MM-dd hh:mm:ss");
            this.lblBizDesc.Text = CustomerModel.BusinessDescription;
            this.lblContactStatus.Text = CustomerModel.ContractStatusName;
            //所有部门级联字符串拼接
            List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
            depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(CustomerModel.GroupID.Value, depList);
            string groupname = string.Empty;
            foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
            {
                groupname += dept.DepartmentName + "-";
            }
            this.lblGroup.Text = groupname.Substring(0, groupname.Length - 1);

            if (!string.IsNullOrEmpty(CustomerModel.ProjectCode))
            {
                this.lblProjectCode.Text = CustomerModel.ProjectCode;
            }
            this.lblApplicant.Text = CustomerModel.ApplicantEmployeeName;
            lblApplicant.Attributes["onclick"] = "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(CustomerModel.ApplicantUserID) + "');";
            this.lblSerialCode.Text = CustomerModel.SerialCode;
            this.lblProjectType.Text = CustomerModel.ProjectTypeName;
            this.lblFromJoint.Text = CustomerModel.IsFromJoint == false ? "否" : "是";
            this.lblBizType.Text = CustomerModel.BusinessTypeName;
        }
    }

    private void initCustomerInfo(ESP.Finance.Entity.CustomerTmpInfo customModel)
    {
        int count = 0;
        if (customModel != null)
        {
            ESP.Finance.Entity.CustomerInfo customer = ESP.Finance.BusinessLogic.CustomerManager.GetModel(customModel.CustomerID);
            if (customer != null)
            {
                if (customModel.Address1 != customer.Address1)
                {
                    this.lblAddress1.ForeColor = Color.Red;
                    count++;
                }
                if (customModel.Address2 != customer.Address2)
                {
                    this.lblAddress2.ForeColor = Color.Red;
                    count++;
                }
                if (customModel.AreaName != customer.AreaName)
                {
                    this.lblArea.ForeColor = Color.Red;
                    count++;
                }
                if (customModel.ContactName != customer.ContactName)
                {
                    this.lblContact.ForeColor = Color.Red;
                    count++;
                }
                if (customModel.ContactFax != customer.ContactFax)
                {
                    this.lblContactFax.ForeColor = Color.Red;
                    count++;
                }
                if (customModel.ContactMobile != customer.ContactMobile)
                {
                    this.lblContactMobile.ForeColor = Color.Red;
                    count++;
                }
                if (customModel.ContactPosition != customer.ContactPosition)
                {
                    this.lblContactPosition.ForeColor = Color.Red;
                    count++;
                }
                if (customModel.ContactEmail != customer.ContactEmail)
                {
                    this.lblEmail.ForeColor = Color.Red;
                    count++;
                }
                if (customModel.IndustryName != customer.IndustryName)
                {
                    this.lblIndustry.ForeColor = Color.Red;
                    count++;
                }
                if (customModel.InvoiceTitle != customer.InvoiceTitle)
                {
                    this.lblInvoiceTitle.ForeColor = Color.Red;
                    count++;
                }
                if (customModel.NameCN1 != customer.NameCN1)
                {
                    this.lblNameCN1.ForeColor = Color.Red;
                    count++;
                }
                if (customModel.NameCN2 != customer.NameCN2)
                {
                    this.lblNameCN2.ForeColor = Color.Red;
                    count++;
                }
                if (customModel.NameEN1 != customer.NameEN1)
                {
                    this.lblNameEN1.ForeColor = Color.Red;
                    count++;
                }
                if (customModel.NameEN2 != customer.NameEN2)
                {
                    this.lblNameEN2.ForeColor = Color.Red;
                    count++;
                }
                if (customModel.PostCode != customer.PostCode)
                {
                    this.lblPostCode.ForeColor = Color.Red;
                    count++;
                }
                if (customModel.ShortCN != customer.ShortCN)
                {
                    this.lblShortCN.ForeColor = Color.Red;
                    count++;
                }
                if (customModel.ShortEN != customer.ShortEN)
                {
                    this.lblShortEN.ForeColor = Color.Red;
                    count++;
                }
                if (customModel.Website != customer.Website)
                {
                    this.lblWebSite.ForeColor = Color.Red;
                    count++;
                }

                if (count == 0)
                {
                    this.divCustomer.Visible = false;
                }
                else
                {
                    this.divCustomer.Visible = true;
                }
            }
            else
            {
                this.lblAddress1.ForeColor = Color.Red;
                this.lblAddress2.ForeColor = Color.Red;
                this.lblArea.ForeColor = Color.Red;
                this.lblContact.ForeColor = Color.Red;
                this.lblContactFax.ForeColor = Color.Red;
                this.lblContactMobile.ForeColor = Color.Red;
                this.lblContactPosition.ForeColor = Color.Red;
                this.lblEmail.ForeColor = Color.Red;
                this.lblIndustry.ForeColor = Color.Red;
                this.lblInvoiceTitle.ForeColor = Color.Red;
                this.lblNameCN1.ForeColor = Color.Red;
                this.lblNameCN2.ForeColor = Color.Red;
                this.lblNameEN1.ForeColor = Color.Red;
                this.lblNameEN2.ForeColor = Color.Red;
                this.lblPostCode.ForeColor = Color.Red;
                this.lblShortCN.ForeColor = Color.Red;
                this.lblShortEN.ForeColor = Color.Red;
                this.lblWebSite.ForeColor = Color.Red;
                this.divCustomer.Visible = true;
            }
            this.hidCustomerID.Value = customModel.CustomerTmpID.ToString();
            this.lblAddress1.Text = customModel.Address1;
            this.lblAddress2.Text = customModel.Address2;
            this.lblArea.Text = customModel.AreaName;
            this.lblContact.Text = customModel.ContactName;
            this.lblContactFax.Text = customModel.ContactFax;
            this.lblContactMobile.Text = customModel.ContactMobile;
            this.lblContactPosition.Text = customModel.ContactPosition;
            this.lblEmail.Text = customModel.ContactEmail;
            this.lblIndustry.Text = customModel.IndustryName;
            this.lblInvoiceTitle.Text = customModel.InvoiceTitle;
            this.lblNameCN1.Text = customModel.NameCN1;
            this.lblNameCN2.Text = customModel.NameCN2;
            this.lblNameEN1.Text = customModel.NameEN1;
            this.lblNameEN2.Text = customModel.NameEN2;
            this.lblPostCode.Text = customModel.PostCode;
            this.lblShortCN.Text = customModel.ShortCN;
            this.lblShortEN.Text = customModel.ShortEN;
            this.lblWebSite.Text = customModel.Website;
        }
        else
        { this.divCustomer.Visible = false; }
    }

    private void BindCosts(ESP.Finance.Entity.ProjectInfo customModel)
    {
        this.gvCost.DataSource = customModel.CostDetails;
        this.gvCost.DataBind();
        this.gvExpense.DataSource = customModel.Expenses;
        this.gvExpense.DataBind();
        if (this.gvCost.Rows.Count == 0 && this.gvExpense.Rows.Count == 0)
        {
            this.trCostGrid.Visible = false;
            this.trCostNoRecord.Visible = true;
        }
        else
        {
            this.trCostGrid.Visible = true;
            this.trCostNoRecord.Visible = false;
        }
        BindTotal(customModel.CostDetails, customModel.Expenses);
    }

    private void BindTotal(IList<ContractCostInfo> list, IList<ProjectExpenseInfo> expenselist)
    {
        this.trCostTotal.Visible = true;
        this.lblCostTotal.Text = "总计:";
        decimal total = 0;
        foreach (ContractCostInfo item in list)
        {
            total += Convert.ToDecimal(item.Cost);
        }
        foreach (ProjectExpenseInfo eitem in expenselist)
        {
            total += Convert.ToDecimal(eitem.Expense);
        }
        if (total == 0)
        {
            this.trCostTotal.Visible = false;
        }
        this.lblCostTotal.Text = "总计:" + total.ToString("#,##0.00");
    }

    protected void gvMember_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ESP.Finance.Entity.ProjectMemberInfo member = (ESP.Finance.Entity.ProjectMemberInfo)e.Row.DataItem;
            Label lblNo = (Label)e.Row.FindControl("lblNo");
            lblNo.Text = (e.Row.RowIndex + 1).ToString();
            Label lblPhone = (Label)e.Row.FindControl("lblPhone");
            Label lblName = (Label)e.Row.FindControl("lblName");
            lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(Convert.ToInt32(e.Row.Cells[1].Text)) + "');");
            if (lblPhone != null)
                lblPhone.Text = StringHelper.FormatPhoneLastChar(lblPhone.Text);
            //所有部门级联字符串拼接
            Label lblGroupName = (Label)e.Row.FindControl("lblGroupName");
            List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
            depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(member.GroupID.Value, depList);
            string groupname = string.Empty;
            foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
            {
                groupname += dept.DepartmentName + "-";
            }
            if (!string.IsNullOrEmpty(groupname))
                lblGroupName.Text = groupname.Substring(0, groupname.Length - 1);
        }
    }
    protected void gvContracts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ContractInfo item = (ContractInfo)e.Row.DataItem;
            Label lblAmount = (Label)e.Row.FindControl("lblAmount");
            Label lblNo = (Label)e.Row.FindControl("lblNo");
            lblNo.Text = (e.Row.RowIndex + 1).ToString();
            lblAmount.Text = item.TotalAmounts.ToString("#,##0.00");

            HyperLink lnkDownLoad = (HyperLink)e.Row.FindControl("lnkDownLoad");
            if (lnkDownLoad != null)
            {
                lnkDownLoad.ToolTip = "下载附件：" + item.Attachment;
                lnkDownLoad.NavigateUrl = "~/Dialogs/ContractFileDownLoad.aspx?" + RequestName.ContractID + "=" + item.ContractID.ToString();//ESP.Finance.Configuration.ConfigurationManager.ContractPath + item.Attachment;

            }
            Label lblOldContract = (Label)e.Row.FindControl("lblOldContract");
            if (lblOldContract != null && item.ParentID != null)
            {
                ContractInfo oldContract = ESP.Finance.BusinessLogic.ContractManager.GetModel(Convert.ToInt32(item.ParentID));
                if (oldContract != null)
                    lblOldContract.Text = oldContract.Description;

            }
            Label lblUsable = (Label)e.Row.FindControl("lblUsable");
            if (item.Usable != null && Convert.ToBoolean(!item.Usable))
            {
                lblUsable.Text = "否";
            }
        }
    }
    protected void gvCost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblno = (Label)e.Row.FindControl("lblNo");
            lblno.Text = (e.Row.RowIndex + 1).ToString();
            ContractCostInfo item = (ContractCostInfo)e.Row.DataItem;
            Label lblCost = (Label)e.Row.FindControl("lblCost");
            if (lblCost != null && item.Cost != null)
            {
                lblCost.Text = Convert.ToDecimal(item.Cost).ToString("#,##0.00");
            }
        }
    }
    protected void gvExpense_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblno = (Label)e.Row.FindControl("lblNo");
            lblno.Text = (e.Row.RowIndex + 1).ToString();
            ProjectExpenseInfo item = (ProjectExpenseInfo)e.Row.DataItem;
            Label lblExpense = (Label)e.Row.FindControl("lblExpense");
            if (lblExpense != null && item.Expense != null)
            {
                lblExpense.Text = Convert.ToDecimal(item.Expense).ToString("#,##0.00");
            }
        }
    }
    protected void gvSupporter_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[4].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.SupporterInfo supporter = (ESP.Finance.Entity.SupporterInfo)e.Row.DataItem;
                Label lblNo = (Label)e.Row.FindControl("lblNo");
                lblNo.Text = (e.Row.RowIndex + 1).ToString();
                SupporterInfo item = (SupporterInfo)e.Row.DataItem;
                Label lblBudgetAllocation = (Label)e.Row.FindControl("lblBudgetAllocation");
                if (lblBudgetAllocation != null && item.BudgetAllocation != null)
                {
                    lblBudgetAllocation.Text = Convert.ToDecimal(item.BudgetAllocation).ToString("#,##0.00");
                }
                //所有部门级联字符串拼接
                Label lblGroupName = (Label)e.Row.FindControl("lblGroupName");
                List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
                depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(supporter.GroupID.Value, depList);
                string groupname = string.Empty;
                foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
                {
                    groupname += dept.DepartmentName + "-";
                }
                if (!string.IsNullOrEmpty(groupname))
                    lblGroupName.Text = groupname.Substring(0, groupname.Length - 1);
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblName = (Label)e.Row.FindControl("lblName");
            lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(Convert.ToInt32(e.Row.Cells[4].Text)) + "');");

        }
    }

    protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblNo = (Label)e.Row.FindControl("lblNo");
            lblNo.Text = (e.Row.RowIndex + 1).ToString();
            PaymentInfo item = (PaymentInfo)e.Row.DataItem;
            Label lblPaymentBudget = (Label)e.Row.FindControl("lblPaymentBudget");
            if (lblPaymentBudget != null && item.PaymentBudget != null)
            {
                lblPaymentBudget.Text = Convert.ToDecimal(item.PaymentBudget).ToString("#,##0.00");
            }

            Label lblDate = (Label)e.Row.FindControl("lblDate");
            if (lblDate != null && item.PaymentPreDate != null)
            {
                lblDate.Text = Convert.ToDateTime(item.PaymentPreDate).ToString("yyyy-MM-dd");
            }
        }
    }

    protected void gvPercent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblNo = (Label)e.Row.FindControl("lblNo");
            lblNo.Text = (e.Row.RowIndex + 1).ToString();
        }
    }

    protected void btnFirst_Click(object sender, EventArgs e)
    {
        if (projectList == null)
        {
            projectList = getProjectList();
        }

        if (projectList != null && projectList.Count != 0)
        {
            try
            {
                this.InitPageContent(projectList[0].ProjectModel.ObjectDeserialize<ProjectInfo>());
                ViewState[SequenceState] = 0;
            }
            catch
            { }
            this.lblRepresent.Text = "当前是第" + (Convert.ToInt32(ViewState[SequenceState]) + 1).ToString() + "条记录 / 共" + projectList.Count.ToString() + "条记录";
        }
    }

    protected void btnPret_Click(object sender, EventArgs e)
    {
        if (projectList == null)
        {
            projectList = getProjectList();
        }
        if (projectList != null && projectList.Count != 0)
        {
            int seq = Convert.ToInt32(ViewState[SequenceState]) - 1;
            if (seq - 1 < 0)
                seq = 0;
            try
            {
                InitPageContent(projectList[seq].ProjectModel.ObjectDeserialize<ProjectInfo>());
                ViewState[SequenceState] = seq;
            }
            catch
            { }
            this.lblRepresent.Text = "当前是第" + (Convert.ToInt32(ViewState[SequenceState]) + 1).ToString() + "条记录 / 共" + projectList.Count.ToString() + "条记录";

        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (projectList == null)
        {
            projectList = getProjectList();
        }

        if (projectList != null && projectList.Count != 0)
        {
            int seq = Convert.ToInt32(ViewState[SequenceState]) + 1;
            if (seq + 1 >= projectList.Count)
                seq = projectList.Count - 1;
            try
            {
                InitPageContent(projectList[seq].ProjectModel.ObjectDeserialize<ProjectInfo>());
                ViewState[SequenceState] = seq;
            }
            catch
            { }
            this.lblRepresent.Text = "当前是第" + (Convert.ToInt32(ViewState[SequenceState]) + 1).ToString() + "条记录 / 共" + projectList.Count.ToString() + "条记录";

        }
    }

    protected void btnLast_Click(object sender, EventArgs e)
    {
        if (projectList == null)
        {
            projectList = getProjectList();
        }
        if (projectList != null && projectList.Count != 0)
        {
            int seq = projectList.Count - 1;
            try
            {
                InitPageContent(projectList[seq].ProjectModel.ObjectDeserialize<ProjectInfo>());
                ViewState[SequenceState] = seq;
            }
            catch
            { }
            this.lblRepresent.Text = "当前是第" + (Convert.ToInt32(ViewState[SequenceState]) + 1).ToString() + "条记录 / 共" + projectList.Count.ToString() + "条记录";

        }
    }
}

