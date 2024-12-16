using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
public partial class UserControls_Project_ProjectContractCostView : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCosts();
        }
    }

    public void BindCosts(ESP.Finance.Entity.ProjectInfo customModel)
    {
        this.gvCost.DataSource = customModel.CostDetails;
        this.gvCost.DataBind();
        this.gvExpense.DataSource = customModel.Expenses;
        this.gvExpense.DataBind();
        if (this.gvCost.Rows.Count == 0 && this.gvExpense.Rows.Count == 0)
        {
            this.trGrid.Visible = false;
            this.trNoRecord.Visible = true;
        }
        else
        {
            this.trGrid.Visible = true;
            this.trNoRecord.Visible = false;
        }
        BindTotal(customModel.CostDetails, customModel.Expenses);
    }
    private void BindCosts()
    {
        StringBuilder condition = new StringBuilder();
        condition.Append(" 1=1 ");
        if(!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
            condition.Append(" AND ProjectID = " + Request[ESP.Finance.Utility.RequestName.ProjectID]);
        IList<ContractCostInfo> list = ESP.Finance.BusinessLogic.ContractCostManager.GetList(condition.ToString());
        this.gvCost.DataSource = list;
        this.gvCost.DataBind();
        IList<ProjectExpenseInfo> expenselist = ESP.Finance.BusinessLogic.ProjectExpenseManager.GetList(condition.ToString());
        this.gvExpense.DataSource = expenselist;
        this.gvExpense.DataBind();
        if (this.gvCost.Rows.Count == 0 && this.gvExpense.Rows.Count == 0)
        {
            this.trGrid.Visible = false;
            this.trNoRecord.Visible = true;
        }
        else
        {
            this.trGrid.Visible = true;
            this.trNoRecord.Visible = false;
        }

        BindTotal(list, expenselist);
    }
    private void BindTotal(IList<ContractCostInfo> list,IList<ProjectExpenseInfo> expenselist)
    {
        this.trTotal.Visible = true;
        this.lblTotal.Text = "总计:";
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
            this.trTotal.Visible = false;
        }
        this.lblTotal.Text ="总计:"+ total.ToString("#,##0.00");
    }

    protected void gvCost_RowDataBound(object sender, GridViewRowEventArgs e)
    { 
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
            Label lblno = (Label)e.Row.FindControl("lblNo");
            lblno.Text = (e.Row.RowIndex + 1).ToString();
            ContractCostInfo item = (ContractCostInfo)e.Row.DataItem;
            Label lblCost = (Label)e.Row.FindControl("lblCost");
            Label lblUsedCost = (Label)e.Row.FindControl("lblUsedCost");
            if (lblCost != null && item.Cost != null)
            {
                lblCost.Text = Convert.ToDecimal(item.Cost).ToString("#,##0.00");
            }
            //if (projectModel.Status >= 12)
            //{
            //    lblUsedCost.Text = (ESP.Finance.BusinessLogic.CheckerManager.得到某一成本PR使用总额(projectModel.ProjectCode, projectModel.GroupID.Value, item.CostTypeID.Value) +
            //        ESP.Finance.BusinessLogic.CheckerManager.得到某一成本报销使用总额(projectModel.ProjectCode, projectModel.GroupID.Value, item.CostTypeID.Value)).ToString("#,##0.00");
            //}
        }
    }
    protected void gvExpense_RowDataBound(object sender, GridViewRowEventArgs e)
    { 
       // ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
        //if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        //{
        //    if (projectModel.Status < 12)
        //    {
        //        e.Row.Cells[3].Visible = false;
        //    }
        //}
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblno = (Label)e.Row.FindControl("lblNo");
            lblno.Text = (e.Row.RowIndex + 1).ToString();
            ProjectExpenseInfo item = (ProjectExpenseInfo)e.Row.DataItem;
            Label lblExpense = (Label)e.Row.FindControl("lblExpense");
            Label lblUsedCost = (Label)e.Row.FindControl("lblUsedCost");
            if (lblExpense != null && item.Expense != null)
            {
                lblExpense.Text = Convert.ToDecimal(item.Expense).ToString("#,##0.00");
            }
            //if (projectModel.Status >= 12)
            //{
            //    if (item.Description == "OOP")
            //        lblUsedCost.Text = ESP.Finance.BusinessLogic.CheckerManager.得到某一成本报销使用总额(projectModel.ProjectCode, projectModel.GroupID.Value, 0).ToString("#,##0.00");
            //    else
            //        lblUsedCost.Text = ESP.Finance.BusinessLogic.CheckerManager.得到TrafficFee使用总额(projectModel.ProjectCode, projectModel.GroupID.Value).ToString("#,##0.00");
            //}
        }
    }
    
}