using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Compatible;
using ESP.Finance.Utility;
using ESP.Finance.Entity;

public partial class UserControls_Project_ProjectSupporter : System.Web.UI.UserControl
{
    int projectid = 0;
    ESP.Finance.Entity.ProjectInfo projectinfo;
    public ESP.Finance.Entity.ProjectInfo ProjectInfo
    {
        get { if (projectinfo == null)projectinfo = new ESP.Finance.Entity.ProjectInfo(); return projectinfo; }
        set { projectinfo = value; }
    }

    public void InitProjectInfo()
    {
        projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
        projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModel(projectid);
        this.hidTotalCost.Value = ESP.Finance.BusinessLogic.SupporterManager.GetTotalAmountByProject(projectid).ToString();
        this.gvSupporter.DataSource = projectinfo.Supporters;
        this.gvSupporter.DataBind();

        if (this.gvSupporter.Rows.Count > 0)
        {
            this.trGrid.Visible = true;
            this.trNoRecord.Visible = false;
        }
        else
        {
            this.trGrid.Visible = false;
            this.trNoRecord.Visible = true;
        }

        BindTotal(projectinfo.Supporters);

    }

    protected void btnRet_Click(object sender, EventArgs e)
    {
        projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
        projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
        if ((projectinfo.Status == (int)ESP.Finance.Utility.Status.FinanceAuditComplete || projectinfo.Status == (int)ESP.Finance.Utility.Status.ProjectPreClose) && Convert.ToDecimal(this.hidTotalCost.Value) != ESP.Finance.BusinessLogic.SupporterManager.GetTotalAmountByProject(projectid))
        {
            projectinfo.Status = (int)ESP.Finance.Utility.Status.Saved;
            ESP.Finance.BusinessLogic.ProjectManager.Update(projectinfo);
        }
        InitProjectInfo();
    }

    private void BindTotal(IList<SupporterInfo> list)
    {
        this.trTotal.Visible = true;
        this.lblTotal.Text = "总计:";
        decimal total = 0;
        foreach (SupporterInfo item in list)
        {
            if(item.BudgetAllocation != null)
                total += (decimal)item.BudgetAllocation;
        }
        if (total == 0)
        {
            this.trTotal.Visible = false;
        }
        this.lblTotal.Text ="总计:"+ total.ToString("#,##0.00");
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
                SupporterInfo item = (SupporterInfo)e.Row.DataItem;
                ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(item.ProjectID);
                ESP.Finance.Entity.TaxRateInfo rateModel = null;
                if (projectModel.ContractTaxID != null && projectModel.ContractTaxID.Value != 0)
                    rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(projectModel.ContractTaxID.Value);

                if (!string.IsNullOrEmpty(projectModel.ProjectCode))
                {
                    e.Row.Cells[12].Visible = false;
                }
                Label lblNo = (Label)e.Row.FindControl("lblNo");
                lblNo.Text = (e.Row.RowIndex + 1).ToString();
                Label lblBudgetAllocation = (Label)e.Row.FindControl("lblBudgetAllocation");
                Label lblBudgetNoVAT = (Label)e.Row.FindControl("lblBudgetNoVAT");
                Label lblTaxVAT = (Label)e.Row.FindControl("lblTaxVAT");

                if (lblBudgetAllocation != null && item.BudgetAllocation != null)
                {
                    lblBudgetAllocation.Text = ((decimal)item.BudgetAllocation).ToString("#,##0.00");
                    if (rateModel != null && projectModel.IsCalculateByVAT==1)
                    {
                        lblBudgetNoVAT.Text = (item.BudgetAllocation.Value / rateModel.VATParam1).ToString("#,##0.00");
                        lblTaxVAT.Text = ESP.Finance.BusinessLogic.CheckerManager.GetSupporterTaxByVAT(item, projectModel, rateModel).ToString("#,##0.00");
                    }
                }
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
                if (lnkDelete != null)
                {
                    if (item.SupportID == 0)
                    {
                        lnkDelete.Visible = false;
                    }
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblName = (Label)e.Row.FindControl("lblName");
            lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(Convert.ToInt32(e.Row.Cells[4].Text)) + "');");
       
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            InitProjectInfo();
        }
    }

    protected void gvSupporter_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int supporterid = int.Parse(e.CommandArgument.ToString());
            ESP.Finance.BusinessLogic.SupporterManager.Delete(supporterid);
            InitProjectInfo();
        }
    }
}
