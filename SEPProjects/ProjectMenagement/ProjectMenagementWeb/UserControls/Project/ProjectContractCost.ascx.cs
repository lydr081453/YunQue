using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using System.Text;
using ESP.Finance.Utility;
public partial class UserControls_Project_ProjectContractCost : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCosts();
        }
    }

    protected void btnRet_Click(object sender, EventArgs e)
    {
        BindCosts();
    }

    private void BindCosts()
    {
        StringBuilder condition = new StringBuilder();
        condition.Append(" 1=1 ");
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
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

    protected void gvExpense_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int memberid = int.Parse(e.CommandArgument.ToString());
            //在此处增加采购系统验证
            ESP.Finance.BusinessLogic.ProjectExpenseManager.Delete(memberid);
            BindCosts();
            StringBuilder condition = new StringBuilder();
            condition.Append(" 1=1 ");
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
                condition.Append(" AND ProjectID = " + Request[ESP.Finance.Utility.RequestName.ProjectID]);
            IList<ContractCostInfo> list = ESP.Finance.BusinessLogic.ContractCostManager.GetList(condition.ToString());
            IList<ProjectExpenseInfo> expenselist = ESP.Finance.BusinessLogic.ProjectExpenseManager.GetList(condition.ToString());
            BindTotal(list, expenselist);
        }

    }

    protected void gvCost_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int memberid = int.Parse(e.CommandArgument.ToString());
            ESP.Finance.BusinessLogic.ContractCostManager.Delete(memberid);

            BindCosts();
            StringBuilder condition = new StringBuilder();
            condition.Append(" 1=1 ");
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
                condition.Append(" AND ProjectID = " + Request[ESP.Finance.Utility.RequestName.ProjectID]);
            IList<ContractCostInfo> list = ESP.Finance.BusinessLogic.ContractCostManager.GetList(condition.ToString());
            IList<ProjectExpenseInfo> expenselist = ESP.Finance.BusinessLogic.ProjectExpenseManager.GetList(condition.ToString());
            BindTotal(list, expenselist);
        }

    }

    private void BindTotal(IList<ContractCostInfo> list, IList<ProjectExpenseInfo> elist)
    {
        this.trTotal.Visible = true;
        this.lblTotal.Text = "总计:";
        decimal total = 0;
        foreach (ContractCostInfo item in list)
        {
            total += Convert.ToDecimal(item.Cost);
        }
        foreach (ProjectExpenseInfo eitem in elist)
        {
            total += Convert.ToDecimal(eitem.Expense);
        }
        if (total == 0)
        {
            this.trTotal.Visible = false;
        }
        this.lblTotal.Text = "总计:" + total.ToString("#,##0.00");
    }
    protected void gvExpense_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ProjectExpenseInfo item = (ProjectExpenseInfo)e.Row.DataItem;
            Label lblno = (Label)e.Row.FindControl("lblNo");
            lblno.Text = (e.Row.RowIndex + 1).ToString();
            Label lblExpense = (Label)e.Row.FindControl("lblExpense");
            Label lblUsedCost = (Label)e.Row.FindControl("lblUsedCost");
            if (lblExpense != null && item.Expense != null)
            {
                lblExpense.Text = Convert.ToDecimal(item.Expense).ToString("#,##0.00");
            }
           // ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
            //if (item.Description == "OOP")
            //    lblUsedCost.Text = ESP.Finance.BusinessLogic.CheckerManager.得到某一成本报销使用总额(projectModel.ProjectCode, projectModel.GroupID.Value, 0).ToString("#,##0.00");
            //else
            //    lblUsedCost.Text = ESP.Finance.BusinessLogic.CheckerManager.得到TrafficFee使用总额(projectModel.ProjectCode, projectModel.GroupID.Value).ToString("#,##0.00");

        }
    }

    protected void gvCost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
         if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ContractCostInfo item = (ContractCostInfo)e.Row.DataItem;
            Label lblno = (Label)e.Row.FindControl("lblNo");
            lblno.Text = (e.Row.RowIndex + 1).ToString();
            Label lblCost = (Label)e.Row.FindControl("lblCost");
            Label lblUsedCost = (Label)e.Row.FindControl("lblUsedCost");

            if (lblCost != null && item.Cost != null)
            {
                lblCost.Text = Convert.ToDecimal(item.Cost).ToString("#,##0.00");
            }

            //在此处增加采购系统验证
            //ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));

            //if (ESP.Finance.BusinessLogic.CheckerManager.PR申请是否使用某一成本项(projectModel.ProjectCode, item.CostTypeID.Value, projectModel.GroupID.Value) == true)
            //{
            //    lblCost.ForeColor = System.Drawing.Color.Gray;
            //    e.Row.Cells[1].ForeColor = System.Drawing.Color.Gray;
            //}
            //lblUsedCost.Text = (ESP.Finance.BusinessLogic.CheckerManager.得到某一成本PR使用总额(projectModel.ProjectCode, projectModel.GroupID.Value, item.CostTypeID.Value)+
            //    ESP.Finance.BusinessLogic.CheckerManager.得到某一成本报销使用总额(projectModel.ProjectCode, projectModel.GroupID.Value, item.CostTypeID.Value)).ToString("#,##0.00");
                       
        }
    }
}