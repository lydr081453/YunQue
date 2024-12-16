using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Utility;
public partial class UserControls_Project_PaymentInfo : System.Web.UI.UserControl
{
    //int projectid=0;
    ESP.Finance.Entity.ProjectInfo projectinfo;
    public ESP.Finance.Entity.ProjectInfo ProjectInfo
    {
        get { if (projectinfo == null)projectinfo = new ESP.Finance.Entity.ProjectInfo(); return projectinfo; }
        set { projectinfo = value; }
    }
    private List<ESP.Finance.Entity.PaymentInfo> paymentlist;
    public string PayCycle
    {
        get { return this.txtPayCycle.Text; }
        set { this.txtPayCycle.Text = value; }
    }
    public bool Is3rdInvoice
    {
        get 
        {
            if (this.chkIs3rdInvoice.Checked)
                return true;
            else
                return false;
        }
        set 
        {
            if (value)
                this.chkIs3rdInvoice.Checked = true;
            else
                this.chkNot3rdInvoice.Checked = true;
        }
    }
    public ESP.Compatible.Employee CurrentUser
    { get; set; }

    public string CustomerRemark
    {
        get { return this.txtCustomerRemark.Text; }
        set { this.txtCustomerRemark.Text = value; }
    }
     public void InitProjectInfo()
    {
        if (projectinfo == null)
        {
            projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
            this.chkNot3rdInvoice.Checked = true;
        }
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
        BindTotal(paymentlist);
        this.txtPayCycle.Text = projectinfo.PayCycle;
        this.txtCustomerRemark.Text = projectinfo.CustomerRemark;
        //this.chk3rdInvoice.Checked = Convert.ToBoolean(projectinfo.IsNeedInvoice);

        if (Convert.ToBoolean(projectinfo.IsNeedInvoice))
        {
            this.chkIs3rdInvoice.Checked = true;
        }
        else
        {
            this.chkNot3rdInvoice.Checked = true;
        }
    }
    protected void btnRet_Click(object sender, EventArgs e)
    {
        if (projectinfo == null)
        {
            projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
        
        }
        projectinfo.IsNeedInvoice = this.chkIs3rdInvoice.Checked == true ? 1 : 0;
        projectinfo.PayCycle = this.PayCycle;
        projectinfo.CustomerRemark = CustomerRemark;
        ESP.Finance.BusinessLogic.ProjectManager.Update(projectinfo);

        List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
        System.Data.SqlClient.SqlParameter p = new System.Data.SqlClient.SqlParameter("@projectid", projectinfo.ProjectId);
        paramlist.Add(p);
        paymentlist = (List<ESP.Finance.Entity.PaymentInfo>)ESP.Finance.BusinessLogic.PaymentManager.GetList(" projectid=@projectid", paramlist);
        BindTotal(paymentlist);
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
        this.lblTotal.Text ="总计:"+ total.ToString("#,##0.00");

        this.lblBlance.Text = "剩余:";
        decimal blance = 0;
        if(!projectinfo.isRecharge)
            blance = Convert.ToDecimal(projectinfo.TotalAmount) - total;
        else
            blance = Convert.ToDecimal(projectinfo.AccountsReceivable) - total;

        this.lblBlance.Text = "剩余:" + blance.ToString("#,##0.00");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
            InitProjectInfo();
    }

    protected void gvPayment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int paymentid = int.Parse(e.CommandArgument.ToString());
            ESP.Finance.BusinessLogic.PaymentManager.Delete(paymentid);
            InitProjectInfo();
        }
    
    }
    private string GetUser()
    {
        string user = "," + ESP.Finance.BusinessLogic.BranchManager.GetProjectAccounters() + ","+ESP.Finance.BusinessLogic.BranchManager.GetFinalAccounters()+",";
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
        return retuser;
    }
    protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ESP.Finance.Entity.PaymentInfo paymentModel=(ESP.Finance.Entity.PaymentInfo)e.Row.DataItem;
            ESP.Finance.Entity.DeadLineInfo deadLine = ESP.Finance.BusinessLogic.DeadLineManager.GetCurrentMonthModel();
            Label lblNo = (Label)e.Row.FindControl("lblNo");
            lblNo.Text = (e.Row.RowIndex + 1).ToString();
            PaymentInfo item = (PaymentInfo)e.Row.DataItem;
            Label lblPaymentBudget = (Label)e.Row.FindControl("lblPaymentBudget");
            if (lblPaymentBudget != null && item.PaymentBudget != null)
            {
                lblPaymentBudget.Text = Convert.ToDecimal(item.PaymentBudget).ToString("#,##0.00");
            }
            string users = "," + GetUser()+",";
            if (item.PaymentStatus != (int)ESP.Finance.Utility.ReturnStatus.Save && users.IndexOf("," + CurrentUser.SysID + ",") < 0)
            {
                e.Row.Cells[5].Text = "&nbsp;";
                e.Row.Cells[6].Text = "&nbsp;";
            }
            if (!string.IsNullOrEmpty(paymentModel.PaymentCode))
            {
                e.Row.Cells[7].Text = "&nbsp;";
            }
            
            //if (deadLine.DeadLine >=paymentModel.PaymentPreDate && users.IndexOf(","+CurrentUser.SysID+",")<0)
            //{
            //    if (!(deadLine.DeadLine.Year == paymentModel.PaymentPreDate.Year && deadLine.DeadLine.Month == paymentModel.PaymentPreDate.Month))
            //    {
            //        e.Row.Cells[5].Text = "&nbsp;";
            //        e.Row.Cells[6].Text = "&nbsp;";
            //    }
            //}
            Label lblDate = (Label)e.Row.FindControl("lblDate");
            if (lblDate != null && item.PaymentPreDate != null)
            {
                lblDate.Text = Convert.ToDateTime(item.PaymentPreDate).ToString("yyyy-MM-dd");
            }
        }
    }
}
