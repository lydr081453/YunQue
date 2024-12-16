using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Utility;
public partial class UserControls_Project_ProjectPercent : System.Web.UI.UserControl
{
    int projectID = 0;
    ProjectInfo projectModel = null;
    protected int BeginYear = 0;
    protected int BeginMonth = 0;
    protected int MonthAmount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
        {
            projectID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
            ESP.Finance.Entity.DeadLineInfo deadLine = ESP.Finance.BusinessLogic.DeadLineManager.GetCurrentMonthModel();

            projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModel(projectID);
            BeginYear = projectModel.BeginDate.Value.Year;
            BeginMonth = projectModel.BeginDate.Value.Month;
            int year = projectModel.EndDate.Value.Year - projectModel.BeginDate.Value.Year;
            int month = projectModel.EndDate.Value.Month - projectModel.BeginDate.Value.Month + 1;
            //如果本月结账日>=该项目的结束日，则延长2个月的百分比
            if (deadLine.DeadLine >= projectModel.EndDate.Value)
                MonthAmount = year * 12 + month + 2;
            else
                MonthAmount = year * 12 + month;
            this.gvPercent.DataSource = projectModel.ProjectSchedules;
            BindTotal(projectModel.ProjectSchedules);
            this.gvPercent.DataBind();
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

    public void InitCustomerProject(ESP.Finance.Entity.ProjectInfo customModel)
    {
        BeginYear = customModel.BeginDate.Value.Year;
        BeginMonth = customModel.BeginDate.Value.Month;
        int year = customModel.EndDate.Value.Year - customModel.BeginDate.Value.Year;
        int month = customModel.EndDate.Value.Month - customModel.BeginDate.Value.Month + 1;
        MonthAmount = year * 12 + month + 2;
        //BindProjectSchedule(projectModel);
        this.gvPercent.DataSource = customModel.ProjectSchedules;
        BindTotal(customModel.ProjectSchedules);
        this.gvPercent.DataBind();
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

    private void BindTotal(IList<ProjectScheduleInfo> scheduleList)
    {
        decimal totalPercent = 0;
        decimal totalFee = 0;
        foreach (ProjectScheduleInfo model in scheduleList)
        {
            totalPercent += model.MonthPercent == null ? 0 : model.MonthPercent.Value;
            totalFee += model.Fee == null ? 0 : model.Fee.Value;
        }
        this.lblTotalFee.Text = totalFee.ToString("#,##0.00");
        this.lblTotalPercent.Text = totalPercent.ToString("#,##0.00") + "%";
    }
   

    protected void btnAddPercent_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
        {
            projectID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
            projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectID);
        }

    }
    protected void btnPercent_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
        {
            projectID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
            projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModel(projectID);
            this.gvPercent.DataSource = projectModel.ProjectSchedules;
            this.gvPercent.DataBind();
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

    protected void gvPercent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ESP.Finance.Entity.ProjectScheduleInfo schedule = (ESP.Finance.Entity.ProjectScheduleInfo)e.Row.DataItem;
            ESP.Finance.Entity.ProjectInfo projectModel=ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(schedule.ProjectID.Value);
            Label lblNo = (Label)e.Row.FindControl("lblNo");
            Label lblFeeTax= (Label)e.Row.FindControl("lblFeeTax");
            lblNo.Text = (e.Row.RowIndex + 1).ToString();
            decimal contractTax = projectModel.ContractTax == null ? 0 : projectModel.ContractTax.Value;
            decimal fee = schedule.Fee == null ? 0 : schedule.Fee.Value;
            lblFeeTax.Text = (fee / (1 - contractTax / 100)).ToString("#,##0.00");
        }
    }
}