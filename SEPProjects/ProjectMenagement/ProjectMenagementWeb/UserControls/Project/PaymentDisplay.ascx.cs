using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
public partial class UserControls_Project_PaymentDisplay : System.Web.UI.UserControl
{
    public bool DontBindOnLoad { get; set; }
    public int CurrentUserIDReport { get; set; }
    //int projectid = 0;
    ProjectInfo projectinfo;
    private ESP.Finance.Entity.BranchInfo branchModel;

    public ProjectInfo ProjectInfo
    {
        get { if (projectinfo == null)projectinfo = new ESP.Finance.Entity.ProjectInfo(); return projectinfo; }
        set { projectinfo = value; }
    }

    private List<ESP.Finance.Entity.PaymentInfo> paymentlist;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack && !DontBindOnLoad)
        {
            InitProjectInfo();
        }
    }
    private void BindTotal(List<PaymentInfo> list)
    {
        this.trTotal.Visible = true;
        this.lblTotal.Text = "总计:";
        decimal total = 0;
        foreach (PaymentInfo payment in list)
        {
            total += Convert.ToDecimal(payment.PaymentBudget);
        }
        if (total == 0)
        {
            this.trTotal.Visible = false;
        }
        this.lblTotal.Text = "总计:" + total.ToString("#,##0.00");
        this.lblBlance.Text = "剩余:";
        decimal blance = 0;
        if(!projectinfo.isRecharge)
            blance = Convert.ToDecimal(projectinfo.TotalAmount) - total;
        else
            blance = Convert.ToDecimal(projectinfo.AccountsReceivable) - total;
        this.lblBlance.Text = "剩余:" + blance.ToString("#,##0.00");
    }
    public void InitProjectInfo()
    {
        projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
        branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(projectinfo.BranchCode);

        if (branchModel == null && projectinfo.BranchID != 0)
            branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModel(projectinfo.BranchID.Value);

        List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
        System.Data.SqlClient.SqlParameter p = new System.Data.SqlClient.SqlParameter("@projectid", projectinfo.ProjectId);
        paramlist.Add(p);
        paymentlist = (List<ESP.Finance.Entity.PaymentInfo>)ESP.Finance.BusinessLogic.PaymentManager.GetList(" projectid=@projectid", paramlist);
        this.gvPayment.DataSource = paymentlist;
        this.gvPayment.DataBind();
        if (this.gvPayment.Rows.Count > 0)
        {
            this.trGrid.Visible = true;
            this.trNoRecord.Visible = false;
        }
        else
        {
            this.trGrid.Visible = false; ;
            this.trNoRecord.Visible = true;
        }
        gvPercent.DataSource = projectinfo.ProjectSchedules;
        gvPercent.DataBind();
        if (gvPercent.Rows.Count > 0)
        {
            this.trPercentNoRecord.Visible = false;
            this.trPercent.Visible = true;
        }
        else
        {
            this.trPercent.Visible = false;
            this.trPercentNoRecord.Visible = true;
        }
        BindTotal(paymentlist);
        BindTotalPercent(projectinfo.ProjectSchedules);
        this.lblPayCycle.Text = projectinfo.PayCycle;
        if (Convert.ToBoolean(projectinfo.IsNeedInvoice))
            this.lbl3rdInvoice.Text = "是";
        else
            this.lbl3rdInvoice.Text = "否";
        this.lblCustomerRemark.Text = projectinfo.CustomerRemark;
        //this.lbl3rdInvoice.Text = Convert.ToBoolean(projectinfo.IsNeedInvoice).ToString();
    }

    public void InitProjectInfo(ProjectInfo customerModel)
    {
        branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(customerModel.BranchCode);

        if (branchModel == null && customerModel.BranchID != null && customerModel.BranchID != 0)
        {
            branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModel(customerModel.BranchID.Value);
        }

        this.gvPayment.DataSource = customerModel.Payments;
        this.gvPayment.DataBind();
        if (this.gvPayment.Rows.Count > 0)
        {
            this.trGrid.Visible = true;
            this.trNoRecord.Visible = false;
        }
        else
        {
            this.trGrid.Visible = false; ;
            this.trNoRecord.Visible = true;
        }
        gvPercent.DataSource = customerModel.ProjectSchedules;
        gvPercent.DataBind();
        if (gvPercent.Rows.Count > 0)
        {
            this.trPercentNoRecord.Visible = false;
            this.trPercent.Visible = true;
        }
        else
        {
            this.trPercent.Visible = false;
            this.trPercentNoRecord.Visible = true;
        }
        this.trTotal.Visible = true;
        this.lblTotal.Text = "总计:";
        decimal total = 0;
        foreach (PaymentInfo payment in customerModel.Payments)
        {
            total += payment.PaymentBudget ?? 0;
        }
        if (total == 0)
        {
            this.trTotal.Visible = false;
        }
        this.lblTotal.Text = "总计:" + total.ToString("#,##0.00");
        this.lblBlance.Text = "剩余:";
        decimal blance = 0;
        if(!customerModel.isRecharge)
            blance = (customerModel.TotalAmount ?? 0) - total;
        else
            blance = (customerModel.AccountsReceivable ?? 0) - total;
        this.lblBlance.Text = "剩余:" + blance.ToString("#,##0.00");

        BindTotalPercent(customerModel.ProjectSchedules);
        this.lblPayCycle.Text = customerModel.PayCycle;
        if (Convert.ToBoolean(customerModel.IsNeedInvoice))
            this.lbl3rdInvoice.Text = "是";
        else
            this.lbl3rdInvoice.Text = "否";
        this.lblCustomerRemark.Text = customerModel.CustomerRemark;
    }

    private void BindTotalPercent(IList<ProjectScheduleInfo> scheduleList)
    {
        decimal totalPercent = 0;
        decimal totalFee = 0;
        foreach (ProjectScheduleInfo model in scheduleList)
        {
            totalPercent += model.MonthPercent ?? 0;
            totalFee += model.Fee ?? 0;
        }
        this.lblTotalFee.Text = "总计:" + totalFee.ToString("#,##0.00");
        this.lblTotalPercent.Text = "总计:" + totalPercent.ToString("0.00") + "%";
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

          

            int[] userIds = GetUsers();
            string userIdsString = GetDelegateUser(userIds);

            
            if (!string.IsNullOrEmpty(userIdsString))
            {
                if (userIdsString.IndexOf(branchModel.ProjectAccounter.ToString()) < 0 && branchModel.OtherFinancialUsers.IndexOf("," + CurrentUserIDReport + ",") < 0 && userIdsString.IndexOf(branchModel.FinalAccounter.ToString()) < 0 && userIdsString.IndexOf(branchModel.PaymentAccounter.ToString()) < 0)
                {
                    e.Row.Cells[5].Text = "&nbsp;";//编辑
                    e.Row.Cells[6].Text = "&nbsp;";//明细
                    e.Row.Cells[7].Text = "&nbsp;";//回款
                    e.Row.Cells[8].Text = "&nbsp;";//导出
                }
            }
            else
            {   //当前登录人不是会计不能编辑打印
                if (!(CurrentUserIDReport == branchModel.ProjectAccounter || branchModel.OtherFinancialUsers.IndexOf("," + CurrentUserIDReport + ",") >= 0 || CurrentUserIDReport == branchModel.FinalAccounter || CurrentUserIDReport == branchModel.PaymentAccounter))
                {

                    e.Row.Cells[5].Text = "&nbsp;";//编辑
                    e.Row.Cells[6].Text = "&nbsp;";//明细
                    e.Row.Cells[7].Text = "&nbsp;";//回款
                    e.Row.Cells[8].Text = "&nbsp;";//导出
                }
            }           
        }
    }

 
    private string GetDelegateUser(int[] userIds)
    {
        if (userIds == null || userIds.Length == 0)
            return null;

        System.Text.StringBuilder s = new System.Text.StringBuilder();
        s.Append(userIds[0]);
        for (var i = 1; i < userIds.Length; i++)
        {
            s.Append(",").Append(userIds[i]);
        }

        return s.ToString();
    }

    private int[] GetUsers()
    {
        List<int> users = new List<int>();
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegateList = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(CurrentUserIDReport);
        users.AddRange(delegateList.Select(x => x.UserID));
        users.Add(CurrentUserIDReport);
        return users.ToArray();
    }

    protected void gvPercent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblNo = (Label)e.Row.FindControl("lblNo");
            lblNo.Text = (e.Row.RowIndex + 1).ToString();
        }
    }

    protected void gvPayment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Export")
        {
            int paymentId = int.Parse(e.CommandArgument.ToString());
            ESP.Finance.BusinessLogic.PaymentManager.ExportPaymentDetail(paymentId, this.Response);
        }
    }
}
