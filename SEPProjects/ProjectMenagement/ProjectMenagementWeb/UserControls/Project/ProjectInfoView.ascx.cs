using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Text;
using System.Linq;

public partial class UserControls_Project_ProjectInfoView : System.Web.UI.UserControl
{
    protected int ContractCount = 0;//待阅览附件的数量
    public int IsViewer { get; set; }
    public bool DontBindOnLoad { get; set; }
    protected ProjectInfo project = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !DontBindOnLoad)
        {
            BindProject();
        }
    }

    private void BindProject()
    {
        int projectId;
        if (int.TryParse(Request[ESP.Finance.Utility.RequestName.ProjectID], out projectId) && projectId > 0)
            project = ESP.Finance.BusinessLogic.ProjectManager.GetModel(projectId);

        if (project == null)
            return;

        // project.Contracts = ESP.Finance.BusinessLogic.ContractManager.GetListByProject(project.ProjectId, null, null);

        BindProject(project);

    }

    public void BindProject(ProjectInfo project)
    {
        if (project.TotalAmount != null)
            this.txtTotalAmount.Text = Convert.ToDecimal(project.TotalAmount).ToString("#,##0.00");
        if (project.EndDate != null)
            this.txtEndDate.Text = Convert.ToDateTime(project.EndDate).ToString("yyyy-MM-dd");
        if (project.BeginDate != null)
            this.txtBeginDate.Text = Convert.ToDateTime(project.BeginDate).ToString("yyyy-MM-dd");
        // this.txtPercent.Text = project.OtherRequest;
        this.txtBranchName.Text = project.BranchName;
        // this.txtPercent.Text = project.OtherRequest;
        //if (project.ContractTax != null)
        //    this.txtTaxRate.Text = project.ContractTax.ToString();
        decimal taxfee = 0;
        ESP.Finance.Entity.TaxRateInfo rateModel = null;
        if (project.ContractTaxID != null && project.ContractTaxID != 0)
        {
            rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(project.ContractTaxID.Value);
            txtTaxRate.Text = rateModel.Remark;
        
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

        if (project.IsCalculateByVAT == 1)
            this.lblServiceFee.Text = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFeeByVAT(project, rateModel).ToString("#,##0.00");
        else
            this.lblServiceFee.Text = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFee(project, rateModel).ToString("#,##0.00");
        if (project.TotalAmount > 0)
        {
            lblProfileRate.Text = (Convert.ToDecimal(this.lblServiceFee.Text) / Convert.ToDecimal(project.TotalAmount) * 100).ToString("#,##0.00");
        }

        if (IsViewer == 0)
        {
            IList<ContractInfo> contractlist = project.Contracts.Where(x=>x.Status == null).ToList();

            if (contractlist.Count == 0)
            {
                this.trGrid.Visible = false;
                this.trNoRecord.Visible = true;
            }
            else
            {
                this.trGrid.Visible = true;
                this.trNoRecord.Visible = false;

                this.gvContracts.DataSource = contractlist;
                this.gvContracts.DataBind();
            }
            //BindTotal(contractlist.Where(x => x.Usable == true).ToList());
        }
        if (project.isRecharge)
        {
            txtCustomerRebateRate.Text = (project.CustomerRebateRate.Value * 100).ToString("0.0") + "%"; ;
            hidMediaId.Value = project.MediaId.ToString();
            txtMediaName.Text = project.MediaName;
            txtRechargeAmount.Text = project.Recharge.Value.ToString("#,##0.00");
            txtSupplierCostRate.Text = (project.MediaCostRate.Value * 100).ToString("0.0") + "%"; ;
            labCustomerRebateRatePrice.Text = project.CustomerRebate.Value.ToString("#,##0.00");
            labAccountsReceivable.Text = project.AccountsReceivable.Value.ToString("#,##0.00");
            labSupplierCost.Text = project.MediaCost.Value.ToString("#,##0.00");
        }
        hidRecharge.Value = project.isRecharge ? "true" : "false";

        BindCostTotal();
        BindSupporterTotal(project,rateModel);

        IList<ProjectMediaInfo> pmList = ESP.Finance.BusinessLogic.ProjectMediaManager.GetList(" projectId=" + project.ProjectId);
        gvMedia.DataSource = pmList;
        gvMedia.DataBind();
    }

    protected void gvContracts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ContractInfo item = (ContractInfo)e.Row.DataItem;
            Label lblAmount = (Label)e.Row.FindControl("lblAmount");
            Label lblNo = (Label)e.Row.FindControl("lblNo");
            lblNo.Text = (e.Row.RowIndex + 1).ToString();
            //lblAmount.Text = item.TotalAmounts.ToString("#,##0.00");

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
            if (item.Del)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }


    //private void BindTotal(IList<ContractInfo> list)
    //{
    //    this.trTotal.Visible = true;
    //    this.lblTotal.Text = "总计:";
    //    decimal total = 0;
    //    foreach (ContractInfo item in list)
    //    {
    //        total += Convert.ToDecimal(item.TotalAmounts);
    //    }
    //    if (total == 0)
    //    {
    //        this.trTotal.Visible = false;
    //    }
    //    this.lblTotal.Text = "总计:" + total.ToString("#,##0.00");
    //}

    private void BindCostTotal()
    {
        StringBuilder condition = new StringBuilder();
        condition.Append(" 1=1 ");
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
            condition.Append(" AND ProjectID = " + Request[ESP.Finance.Utility.RequestName.ProjectID]);
        IList<ContractCostInfo> list = ESP.Finance.BusinessLogic.ContractCostManager.GetList(condition.ToString());

        IList<ProjectExpenseInfo> expenselist = ESP.Finance.BusinessLogic.ProjectExpenseManager.GetList(condition.ToString());

        decimal total = 0;
        foreach (ContractCostInfo item in list)
        {
            total += Convert.ToDecimal(item.Cost);
        }
        foreach (ProjectExpenseInfo eitem in expenselist)
        {
            total += Convert.ToDecimal(eitem.Expense);
        }
        this.lblCostTot.Text = "总计:" + total.ToString("#,##0.00");
    }

    private void BindSupporterTotal(ProjectInfo projectModel,TaxRateInfo rateModel)
    {
        IList<SupporterInfo> supporterlist = ESP.Finance.BusinessLogic.SupporterManager.GetListByProject(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]), null, null);
        decimal supporterTotal = 0;
        decimal supporterTaxTotal = 0;
        foreach (SupporterInfo sup in supporterlist)
        {
            supporterTotal += sup.BudgetAllocation.Value;
            supporterTaxTotal += CheckerManager.GetSupporterTaxByVAT(sup, projectModel, rateModel);
        }

        this.lblTotalSupporter.Text = supporterTotal.ToString("#,##0.00");
        this.lblTaxSupporter.Text = supporterTaxTotal.ToString("#,##0.00");
    }

}