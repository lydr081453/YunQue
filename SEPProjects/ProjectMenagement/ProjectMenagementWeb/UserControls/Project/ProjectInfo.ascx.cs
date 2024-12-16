using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;

public partial class UserControls_Project_ProjectInfo : System.Web.UI.UserControl
{
    protected bool isRecharge = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        txtBeginDate.Attributes.Add("onclick", "setDate(this);");
        if (!IsPostBack)
            BindProject();
        BindContracts();

    }

    protected ProjectInfo project;

    public ESP.Compatible.Employee CurrentUser { get; set; }
    protected void txtEndDate_OnTextChanged(object sender, EventArgs e)
    {
    }

    protected void btnEditTaxRate_Click(object sender, EventArgs e)
    {
        // ClickEditTaxRate();
    }

    private void BindContracts()
    {
        project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));

        string term = " Status is null";
        List<SqlParameter> pm = new List<SqlParameter>();

        IList<ContractInfo> contractlist = ESP.Finance.BusinessLogic.ContractManager.GetListByProject(Convert.ToInt32(project.ProjectId), term, pm);

        this.gvContracts.DataSource = contractlist;
        this.gvContracts.DataBind();
        if (this.gvContracts.Rows.Count == 0)
        {
            this.trGrid.Visible = false;
            this.trNoRecord.Visible = true;
        }
        else
        {
            this.trGrid.Visible = true;
            this.trNoRecord.Visible = false;
        }
      
        //BindTotal(amountlist);
    }

    //private void BindTotal(IList<ContractInfo> list)
    //{
    //    this.trTotal.Visible = true;
    //    this.lblTotal.Text = "总计:";
    //    decimal total = 0;
    //    foreach (ContractInfo contract in list)
    //    {
    //        total += Convert.ToDecimal(contract.TotalAmounts);
    //    }
    //    if (total == 0)
    //    {
    //        this.trTotal.Visible = false;
    //    }
    //    this.lblTotal.Text = "总计:" + total.ToString("#,##0.00");

    //}

    protected void btnRet_Click(object sender, EventArgs e)
    {
        project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
        ESP.Finance.Entity.TaxRateInfo rateModel = null;


        if (project != null)
        {
            int branchID = 0;
            if (this.hidBranchID.Value != string.Empty)
                branchID = Convert.ToInt32(this.hidBranchID.Value);
            if (project.ContractStatusName == ProjectType.BDProject || project.CustomerCode.ToLower() == ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN.ToLower())//如果是BD项目，不计算税金
            {
                this.ddlTaxRate.Items.Clear();
                lblTaxFee.Text = string.Empty;
            }
            else
            {
                if (project.ContractStatusName != ProjectType.BDProject && project.CustomerCode.ToLower() != ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN.ToLower())//如果不是BD项目，进行赋值
                {
                    IList<ESP.Finance.Entity.TaxRateInfo> ratelist = ESP.Finance.BusinessLogic.TaxRateManager.GetList(branchID, null, null);
                    this.ddlTaxRate.DataSource = ratelist;
                    this.ddlTaxRate.DataTextField = "Remark";
                    this.ddlTaxRate.DataValueField = "TaxRateID";
                    this.ddlTaxRate.DataBind();
                    if (project.ContractTaxID != null && project.ContractTaxID != 0)
                    {
                        rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(project.ContractTaxID.Value);
                        try
                        {
                            this.ddlTaxRate.SelectedValue = rateModel.TaxRateID.ToString();
                        }
                        catch
                        {
                            this.ddlTaxRate.SelectedIndex = 0;
                            rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(int.Parse(this.ddlTaxRate.SelectedValue));
                            if (rateModel != null)
                            {
                                this.ddlTaxRate.SelectedValue = rateModel.TaxRateID.ToString();
                                if (project.TotalAmount != null && project.TotalAmount != 0)
                                {
                                    if (project.IsCalculateByVAT == 1)
                                    {
                                        lblServiceFee.Text = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFeeByVAT(project, rateModel).ToString("#,##0.00");
                                        lblTaxFee.Text = ESP.Finance.BusinessLogic.CheckerManager.GetTaxByVAT(project, rateModel).ToString("#,##0.00");
                                    }
                                    else
                                    {
                                        lblServiceFee.Text = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFee(project, rateModel).ToString("#,##0.00");
                                        lblTaxFee.Text = ESP.Finance.BusinessLogic.CheckerManager.GetTax(project, rateModel).ToString("#,##0.00");
                                    }
                                    lblProfileRate.Text = (Convert.ToDecimal(this.lblServiceFee.Text) / Convert.ToDecimal(project.TotalAmount) * 100).ToString("#,##0.00");
                                }
                            }
                        }
                    }
                    this.lblTaxFee.Text = "";
                }
            }
        }
    }

    protected void btnContract_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.txtBeginDate.Text.Trim()) && !string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
        {
            try
            {
                SaveProjectInfo();
                project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
                //decimal contractTotal = ESP.Finance.BusinessLogic.ContractManager.GetTotalAmountByProject(project.ProjectId, 0);
                //if (project.TotalAmount != contractTotal && (project.Status == (int)Status.FinanceAuditComplete || project.Status == (int)Status.ProjectPreClose))
                //{
                //    ESP.Finance.BusinessLogic.ProjectManager.ChangeStatus(project, Status.Saved);
                //}
                if (project != null)
                {
                    if ((project.Status == (int)Status.FinanceAuditComplete || project.Status == (int)Status.ProjectPreClose) 
                        && (project.ContractStatusID == Convert.ToInt32(ESP.Finance.Configuration.ConfigurationManager.CAStatus)
                        || project.ContractStatusID == Convert.ToInt32(ESP.Finance.Configuration.ConfigurationManager.FCAStatus))
                        )
                        ESP.Finance.BusinessLogic.ProjectManager.ChangeCheckContractStatus(project.ProjectId, ProjectCheckContract.ContractUpdate);
                    else if (project.Status == (int)Status.Waiting)
                    {
                        ESP.Finance.BusinessLogic.ProjectManager.ChangeStatus(project, Status.BizAuditComplete);
                    }
                }
            }
            catch
            { }
        }
        BindContracts();
    }

    private void BindProject()
    {
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
        {
            project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));

            ESP.Finance.Entity.TaxRateInfo rateModel = null;


            if (project != null)
            {
                if (project.TotalAmount != null)
                {
                    this.txtTotalAmount.Text = Convert.ToDecimal(project.TotalAmount).ToString("#,##0.00");

                }
                //如果已经给了项目号只有财务能调整项目结束日期
                IList<ESP.Finance.Entity.ProjectHistInfo> histList = ESP.Finance.BusinessLogic.ProjectHistManager.GetListByProject(project.ProjectId, null, null);
                string strUsers = this.GetUser(project);
                if (histList.Count > 0 && strUsers.IndexOf("," + CurrentUser.SysID + ",") < 0)
                    this.txtEndDate.Enabled = false;
                this.txtEndDate.Text = project.EndDate == null ? DateTime.Now.ToString("yyyy-MM-dd") : project.EndDate.Value.ToString("yyyy-MM-dd");
                this.txtBeginDate.Text = project.BeginDate == null ? DateTime.Now.ToString("yyyy-MM-dd") : project.BeginDate.Value.ToString("yyyy-MM-dd");
                this.txtBranchName.Text = project.BranchName;
                this.hidBranchCode.Value = project.BranchCode;
                this.hidBranchID.Value = project.BranchID == null ? "0" : project.BranchID.Value.ToString();
                this.ddlTaxRate.Items.Clear();

                int branchID = 0;
                if (project.BranchID != null)
                    branchID = Convert.ToInt32(project.BranchID);
                if (project.ContractStatusName != ProjectType.BDProject && project.CustomerCode.ToLower() != ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN.ToLower())//如果不是BD项目，进行赋值
                {
                    IList<ESP.Finance.Entity.TaxRateInfo> ratelist = ESP.Finance.BusinessLogic.TaxRateManager.GetList(branchID, null, null);
                    this.ddlTaxRate.DataSource = ratelist;
                    this.ddlTaxRate.DataTextField = "Remark";
                    this.ddlTaxRate.DataValueField = "TaxRateID";
                    this.ddlTaxRate.DataBind();
                    ddlTaxRate.Items.Insert(0, new ListItem("请选择...", "-1"));
                    if (project.ContractTaxID != null && project.ContractTaxID != 0)
                    {
                        rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(project.ContractTaxID.Value);
                        this.ddlTaxRate.SelectedValue = project.ContractTaxID.ToString();
                        // this.lblRateRemark.Text = rateModel.TaxRate.Value.ToString("#,##0.00");
                    }

                    decimal taxfee = 0;
                    if (rateModel != null)
                    {
                        if (project.IsCalculateByVAT == 1)
                        {
                            this.lblTotalNoVAT.Text = Convert.ToDecimal(project.TotalAmount / rateModel.VATParam1).ToString("#,##0.00");
                            taxfee = CheckerManager.GetTaxByVAT(project, rateModel);
                        }
                        else
                        {
                            taxfee = ESP.Finance.BusinessLogic.CheckerManager.GetTax(project, rateModel);
                        }
                        this.lblTaxFee.Text = taxfee.ToString("#,##0.00");
                    }
                }
                if (project.ContractTaxID == null || project.ContractTaxID == 0)
                    this.lblServiceFee.Text = "0";
                else
                {
                    if (project.IsCalculateByVAT == 1)
                        this.lblServiceFee.Text = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFeeByVAT(project, rateModel).ToString("#,##0.00");
                    else
                        this.lblServiceFee.Text = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFee(project, rateModel).ToString("#,##0.00");
                }
                if (project.TotalAmount > 0)
                {
                    lblProfileRate.Text = (Convert.ToDecimal(this.lblServiceFee.Text) / Convert.ToDecimal(project.TotalAmount) * 100).ToString("#,##0.00");
                }


                //supporter info
                IList<SupporterInfo> supporterlist = ESP.Finance.BusinessLogic.SupporterManager.GetList(" projectid =" + project.ProjectId.ToString());
                decimal supporterTotal = 0;
                decimal supporterTaxTotal = 0;
                foreach (SupporterInfo sup in supporterlist)
                {
                    supporterTotal += sup.BudgetAllocation.Value;
                    supporterTaxTotal += CheckerManager.GetSupporterTaxByVAT(sup, project, rateModel);
                }

                this.lblTotalSupporter.Text = supporterTotal.ToString("#,##0.00");
                this.lblTaxSupporter.Text = supporterTaxTotal.ToString("#,##0.00");

                if (project.isRecharge)
                {
                    txtCustomerRebateRate.Text = (project.CustomerRebateRate.Value * 100).ToString("#,##0.00");
                    hidMediaId.Value = project.MediaId.ToString();
                    txtMediaName.Text = project.MediaName;
                    txtRechargeAmount.Text = project.Recharge.Value.ToString("#,##0.00");
                    txtSupplierCostRate.Text = ((project.MediaCostRate ?? 0) * 100).ToString("#,##0.00");
                    labCustomerRebateRatePrice.Text = project.CustomerRebate.Value.ToString("#,##0.00");
                    labAccountsReceivable.Text = project.AccountsReceivable.Value.ToString("#,##0.00");
                    labSupplierCost.Text = project.MediaCost.Value.ToString("#,##0.00");
                }
                IList<ProjectMediaInfo> pmList = ESP.Finance.BusinessLogic.ProjectMediaManager.GetList(" projectId=" + project.ProjectId);
                gvMedia.DataSource = pmList;
                gvMedia.DataBind();
            }
        }
    }

    private void CreateMainContract(ProjectInfo projectinfo, string fileName)
    {
        ContractInfo contract = new ContractInfo();
        contract.ProjectID = projectinfo.ProjectId;
        contract.projectCode = projectinfo.ProjectCode;
        contract.Attachment = fileName;
        contract.Del = true;
        List<ContractInfo> contractlist = (List<ContractInfo>)ESP.Finance.BusinessLogic.ContractManager.GetListByProject(projectinfo.ProjectId, null, null);
        if (contractlist == null || contractlist.Count == 0)
            ESP.Finance.BusinessLogic.ContractManager.Add(contract);
    }

    public ProjectInfo GetProject()
    {
        project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));

        //项目主信息
        if (this.txtBeginDate.Text != string.Empty)
            project.BeginDate = Convert.ToDateTime(this.txtBeginDate.Text);
        if (this.txtEndDate.Text != string.Empty)
            project.EndDate = Convert.ToDateTime(this.txtEndDate.Text);
        project.BranchID = Convert.ToInt32(this.hidBranchID.Value);
        project.BranchName = this.txtBranchName.Text;
        project.BranchCode = this.hidBranchCode.Value;
        try
        {
            project.TotalAmount = Convert.ToDecimal(this.txtTotalAmount.Text);
        }
        catch
        {
            project.TotalAmount = 0;
        }
        if (project.ContractStatusName != ProjectType.BDProject && project.CustomerCode.ToLower() != ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN.ToLower())
        {
            if (this.ddlTaxRate.SelectedItem.Value != "-1")
                project.ContractTaxID = Convert.ToInt32(this.ddlTaxRate.SelectedItem.Value);
        }
        if (project.isRecharge)
        {
            project.MediaId = int.Parse(hidMediaId.Value);
            project.MediaCostRate = decimal.Parse(txtSupplierCostRate.Text) / 100;
            project.CustomerRebateRate = decimal.Parse(txtCustomerRebateRate.Text) / 100;
        }

        return project;
    }

    public ProjectInfo GetProject(ProjectInfo project)
    {
        //项目主信息
        if (this.txtBeginDate.Text != string.Empty)
            project.BeginDate = Convert.ToDateTime(this.txtBeginDate.Text);
        if (this.txtEndDate.Text != string.Empty)
            project.EndDate = Convert.ToDateTime(this.txtEndDate.Text);
        project.BranchID = Convert.ToInt32(this.hidBranchID.Value);
        project.BranchName = this.txtBranchName.Text;
        project.BranchCode = this.hidBranchCode.Value;
        if (project.isRecharge)
        {
            project.MediaId = int.Parse(hidMediaId.Value);
            project.MediaCostRate = decimal.Parse(txtSupplierCostRate.Text) / 100;
            project.CustomerRebateRate = decimal.Parse(txtCustomerRebateRate.Text) / 100;
        }
        try
        {
            project.TotalAmount = Convert.ToDecimal(this.txtTotalAmount.Text);
        }
        catch
        {
            project.TotalAmount = 0;
        }
        if (project.ContractStatusName != ProjectType.BDProject && project.CustomerCode.ToLower() != ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN.ToLower())
        {
            if (this.ddlTaxRate.SelectedItem.Value != "-1")
                project.ContractTaxID = Convert.ToInt32(this.ddlTaxRate.SelectedItem.Value);
        }
        return project;
    }

    protected void gvContracts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ContractInfo item = (ContractInfo)e.Row.DataItem;
            project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
            //Label lblAmount = (Label)e.Row.FindControl("lblAmount");
            Label lblNo = (Label)e.Row.FindControl("lblNo");
            lblNo.Text = (e.Row.RowIndex + 1).ToString();
            //lblAmount.Text = item.TotalAmounts.ToString("#,##0.00");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkDelete.Visible = item.Del;
            if (!item.Del)
            {
                e.Row.Cells[7].Controls.Clear();
                e.Row.Cells[8].Controls.Clear();
            }
            else
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
                }
            }

            if (item.ContractID == null || item.ContractID.Value == 0)
            {
                lnkDelete.Visible = false;
            }

            HyperLink lnkDownLoad = (HyperLink)e.Row.FindControl("lnkDownLoad");
            if (lnkDownLoad != null)
            {
                lnkDownLoad.ToolTip = "下载附件：" + item.Attachment;
                lnkDownLoad.NavigateUrl = ESP.Finance.Configuration.ConfigurationManager.ContractPath + item.Attachment;
                if (item.ContractID == null || item.ContractID.Value == 0)
                {
                    lnkDownLoad.Visible = false;
                }
            }
            Label lblOldContract = (Label)e.Row.FindControl("lblOldContract");
            if (lblOldContract != null && item.ParentID != null)
            {
                ContractInfo oldContract = ESP.Finance.BusinessLogic.ContractManager.GetModel(Convert.ToInt32(item.ParentID));
                if (oldContract != null)
                    lblOldContract.Text = oldContract.Description;
                if (item.ContractID == null || item.ContractID.Value == 0)
                {
                    lblOldContract.Visible = false;
                }
            }
            Label lblUsable = (Label)e.Row.FindControl("lblUsable");
            if (item.Usable != null && Convert.ToBoolean(!item.Usable))
            {
                lblUsable.Text = "否";
            }
        }
    }

    protected void gvContracts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int contractid = int.Parse(e.CommandArgument.ToString());
            ESP.Finance.Entity.ContractInfo c = ESP.Finance.BusinessLogic.ContractManager.GetModel(contractid);
            if (c.Usable != null && c.Usable == false)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('该文件已经被替换，无法删除！');", true);
            }
            else
            {
                if (c.Del != null && c.Del == true)
                    ESP.Finance.BusinessLogic.ContractManager.Delete(contractid);
                BindContracts();
            }
        }

    }

    private void BindTaxRateRemark()
    {
        project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));

        if (this.ddlTaxRate.Items.Count >= 0 && this.ddlTaxRate.SelectedIndex > -1)
        {
            if (this.ddlTaxRate.SelectedValue != string.Empty)
            {

                TaxRateInfo rate = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(Convert.ToInt32(this.ddlTaxRate.SelectedValue));
                if (rate != null)
                {
                    //lblRateRemark.Text = rate.TaxRate == null ? "0.00" : rate.TaxRate.Value.ToString("#,##0.00");
                    if (project.TotalAmount != null && project.TotalAmount != 0)
                    {
                        if (project.IsCalculateByVAT == 1)
                        {
                            lblServiceFee.Text = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFeeByVAT(project, rate).ToString("#,##0.00");
                            lblTaxFee.Text = ESP.Finance.BusinessLogic.CheckerManager.GetTaxByVAT(project, rate).ToString("#,##0.00");
                        }
                        else
                        {
                            lblServiceFee.Text = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFee(project, rate).ToString("#,##0.00");
                            lblTaxFee.Text = ESP.Finance.BusinessLogic.CheckerManager.GetTax(project, rate).ToString("#,##0.00");
                        }
                        lblProfileRate.Text = (Convert.ToDecimal(this.lblServiceFee.Text) / Convert.ToDecimal(project.TotalAmount) * 100).ToString("#,##0.00");

                        project.ContractTaxID = rate.TaxRateID;
                        project.ContractTax = rate.TaxRate;
                    }
                }


            }


        }
        else
        {
            lblServiceFee.Text = "";
            lblTaxFee.Text = "";
            lblProfileRate.Text = "";
            project.ContractTaxID = -1;
            project.ContractTax = 0;
        }
        ESP.Finance.BusinessLogic.ProjectManager.Update(project);
    }

    protected void ddlTaxRate_SelectedIndexChanged(object sender, EventArgs e)
    { BindTaxRateRemark(); }

    private void SaveProjectInfo()
    {
        project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));

        DateTime endDate = project.EndDate == null ? DateTime.Now : project.EndDate.Value;

        if (this.txtBeginDate.Text != string.Empty)
            project.BeginDate = Convert.ToDateTime(this.txtBeginDate.Text);
        if (this.txtEndDate.Text != string.Empty)
            project.EndDate = Convert.ToDateTime(this.txtEndDate.Text);

        //P项目修改结束日期，同步更新回款确认收入
        if (!(string.IsNullOrEmpty(project.ProjectCode)) && project.ProjectTypeName == ESP.Finance.Utility.ProjectType.ShortTerm && (endDate.Year!=project.EndDate.Value.Year || endDate.Month!=project.EndDate.Value.Month))
        {
            ESP.Finance.BusinessLogic.PaymentManager.UpdatePaymentConfirmMonth(project.ProjectId, project.EndDate.Value.Year, project.EndDate.Value.Month);
        }

        project.BranchID = Convert.ToInt32(this.hidBranchID.Value);
        project.BranchName = this.txtBranchName.Text;
        project.BranchCode = this.hidBranchCode.Value;
        try
        {
            if (project.TotalAmount != null && project.TotalAmount.Value != Convert.ToDecimal(this.txtTotalAmount.Text))
            {
                if (project.Status == (int)Status.FinanceAuditComplete || project.Status == (int)Status.ProjectPreClose)
                    project.Status = (int)Status.Saved;
            }
            project.TotalAmount = Convert.ToDecimal(this.txtTotalAmount.Text);
        }
        catch
        {
            project.TotalAmount = 0;
        }

        TaxRateInfo rateModel = null;

        if (project.ContractStatusName != ProjectType.BDProject && project.CustomerCode.ToLower() != ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN.ToLower())
        {

            if (this.ddlTaxRate.SelectedItem.Value != "-1")
                project.ContractTaxID = Convert.ToInt32(this.ddlTaxRate.SelectedItem.Value);
            rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(project.ContractTaxID.Value);
        }

        if (project.isRecharge)
        {
            project.CustomerRebateRate = decimal.Parse(txtCustomerRebateRate.Text) / 100;

            project.MediaId = int.Parse(hidMediaId.Value);
            project.MediaCostRate = decimal.Parse(txtSupplierCostRate.Text) / 100;
            ESP.Finance.BusinessLogic.ProjectManager.UpdateAndSaveRecharge(project,decimal.Parse(txtRechargeAmount.Text.Trim()));
        }
        else
        {
            ESP.Finance.BusinessLogic.ProjectManager.Update(project);
        }
        
        if (project.IsCalculateByVAT == 1)
        {
            lblServiceFee.Text = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFeeByVAT(project, rateModel).ToString("#,##0.00");
            lblTaxFee.Text = ESP.Finance.BusinessLogic.CheckerManager.GetTaxByVAT(project, rateModel).ToString("#,##0.00");
        }
        else
        {
            lblServiceFee.Text = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFee(project, rateModel).ToString("#,##0.00");
            lblTaxFee.Text = ESP.Finance.BusinessLogic.CheckerManager.GetTax(project, rateModel).ToString("#,##0.00");
        }
        if (project.TotalAmount > 0)
        {
            lblProfileRate.Text = (Convert.ToDecimal(this.lblServiceFee.Text) / Convert.ToDecimal(project.TotalAmount) * 100).ToString("#,##0.00");
        }
    }

    protected void btnAddContracts_Click(object sender, EventArgs e)
    {
        SaveProjectInfo();
    }

    private string GetUser(ESP.Finance.Entity.ProjectInfo projectModel)
    {
        string user = "," + ESP.Finance.BusinessLogic.BranchManager.GetProjectAccounters() + "," + ESP.Finance.BusinessLogic.BranchManager.GetFinalAccounters() + ",";
        string retuser = user;
        string[] users = user.Split(',');
        for (int i = 0; i < users.Length; i++)
        {
            if (!string.IsNullOrEmpty(users[i]))
            {
                ESP.Framework.Entity.AuditBackUpInfo model = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(Convert.ToInt32(users[i]));
                if (model != null)
                {
                    retuser += model.BackupUserID.ToString() + ",";
                }
            }
        }

        if (projectModel.BranchID != null)
        {
            ESP.Finance.Entity.BranchProjectInfo branchProject = ESP.Finance.BusinessLogic.BranchProjectManager.GetModel(projectModel.BranchID.Value, projectModel.GroupID.Value);

            if (branchProject != null)
            {
                retuser += branchProject.AuditorID + ",";
            }
        }

        return retuser;
    }
}