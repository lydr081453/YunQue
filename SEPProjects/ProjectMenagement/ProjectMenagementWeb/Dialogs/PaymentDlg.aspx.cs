using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.BusinessLogic;
using ExtExtenders;
using ESP.Finance.Utility;

public partial class Dialogs_PaymentDlg : ESP.Web.UI.PageBase
{

    //private string clientId = "ctl00_ContentPlaceHolder1_PaymentInfo_";
    private ESP.Finance.Entity.ProjectInfo projectinfo;
    private static string term = string.Empty;
    private static List<SqlParameter> paramlist = null;
    private List<ESP.Finance.Entity.PaymentInfo> paymentlist;
    string script = string.Empty;


    private string GetBranches()
    {
        string branches = ",";
        string strwhere = string.Format(" otherFinancialUsers like '%,{0},%'", CurrentUser.SysID);

        IList<ESP.Finance.Entity.BranchInfo> branchlist = ESP.Finance.BusinessLogic.BranchManager.GetList(strwhere);

        if (branchlist != null && branchlist.Count > 0)
        {
            foreach (ESP.Finance.Entity.BranchInfo branch in branchlist)
            {
                branches += branch.BranchCode.ToString() + ",";
            }

            return branches;
        }
        else
        {
            return string.Empty;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            decimal paid = 0;
            projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
            IList<ESP.Finance.Entity.PaymentInfo> paylist = ESP.Finance.BusinessLogic.PaymentManager.GetListByProject(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]), null, null);
            foreach (ESP.Finance.Entity.PaymentInfo model in paylist)
            {
                paid += model.PaymentBudget == null ? 0 : model.PaymentBudget.Value;
            }
            if (!projectinfo.isRecharge)
            {
                this.hidTotal.Value = (projectinfo.TotalAmount - paid).ToString();
                this.lblTotalAmount.Text = (projectinfo.TotalAmount - paid).Value.ToString("#,##0.00");
            }
            else
            {
                this.hidTotal.Value = (projectinfo.AccountsReceivable - paid).ToString();
                this.lblTotalAmount.Text = (projectinfo.AccountsReceivable - paid).Value.ToString("#,##0.00");
            }
            IList<ESP.Finance.Entity.PaymentContentInfo> cms = ESP.Finance.BusinessLogic.PaymentContentManager.GetList(term, paramlist);
            gvPaymentContent.DataSource = cms;
            this.gvPaymentContent.DataBind();
            this.divPayment.Style["display"] = "none";

            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.PaymentID]) && Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.PaymentID]) != 0)
            {
                ESP.Finance.Entity.PaymentInfo payment = ESP.Finance.BusinessLogic.PaymentManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.PaymentID]));
                this.txtContent.Text = payment.PaymentBudget == null ? "" : payment.PaymentBudget.Value.ToString("0.00");
                this.txtpaymentContent.Text = payment.PaymentContent;
                this.txtRemark.Text = payment.Remark;
                this.dpDailyStartTime.Text = payment.PaymentPreDate.ToString("yyyy-MM-dd");
                this.hidTotal.Value = Convert.ToDecimal(projectinfo.TotalAmount - paid + payment.PaymentBudget).ToString();

                string branches = this.GetBranches();


                //已经有项目号且不是财务人员，不能编辑
                if (!string.IsNullOrEmpty(payment.PaymentCode) && string.IsNullOrEmpty(branches))
                {
                    dpDailyStartTime.Enabled = false;
                }
            }
            else
            {
                txtpaymentContent.Text = projectinfo.BusinessDescription;
            }

            List<System.Data.SqlClient.SqlParameter> plist = new List<System.Data.SqlClient.SqlParameter>();
            System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@projectid", projectinfo.ProjectId);
            plist.Add(p1);
            paymentlist = (List<ESP.Finance.Entity.PaymentInfo>)ESP.Finance.BusinessLogic.PaymentManager.GetList(" projectid=@projectid", plist);
            this.gvPayment.DataSource = paymentlist;
            this.gvPayment.DataBind();
            BindTotal(paymentlist);
        }

    }


    private void BindTotal(List<ESP.Finance.Entity.PaymentInfo> list)
    {
        this.trTotal.Visible = true;
        this.lblTotal.Text = "总计:";
        decimal total = 0;
        foreach (ESP.Finance.Entity.PaymentInfo payment in list)
        {
            total += Convert.ToDecimal(payment.PaymentBudget);
        }
        if (total == 0)
        {
            this.trTotal.Visible = false;
        }
        this.lblTotal.Text += total.ToString("#,##0.00");

        this.lblBlance.Text = "剩余:";
        decimal blance = 0;
        if (projectinfo == null)
            projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
        if (!projectinfo.isRecharge)
            blance = Convert.ToDecimal(projectinfo.TotalAmount) - total;
        else
            blance = Convert.ToDecimal(projectinfo.AccountsReceivable) - total;
        this.lblBlance.Text += blance.ToString("#,##0.00");
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        BindPayment();
    }
    protected void btnClean_Click(object sender, EventArgs e)
    {
        term = string.Empty;
        paramlist = null;
        IList<ESP.Finance.Entity.PaymentContentInfo> cms = ESP.Finance.BusinessLogic.PaymentContentManager.GetList(term, paramlist);
        gvPaymentContent.DataSource = cms;
        this.gvPaymentContent.DataBind();
    }

    private void BindPayment()
    {
        term = "  PaymentContent like '%'+@PaymentContent+'%'";
        paramlist = new List<SqlParameter>();
        paramlist.Add(new SqlParameter("@PaymentContent", this.txtpaymentContent.Text.Trim()));

        IList<ESP.Finance.Entity.PaymentContentInfo> cms = ESP.Finance.BusinessLogic.PaymentContentManager.GetList(term, paramlist);
        gvPaymentContent.DataSource = cms;
        this.gvPaymentContent.DataBind();
    }

    protected void btnAutoComplete_Click(object sender, EventArgs e)
    {
        if (projectinfo == null)
        {
            projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));

        }
        int differ = 0;
        decimal totalAmount = projectinfo.TotalAmount.Value;
        DateTime bDate = projectinfo.BeginDate.Value;
        DateTime eDate = projectinfo.EndDate.Value;
        ESP.Finance.Entity.DeadLineInfo deadline = ESP.Finance.BusinessLogic.DeadLineManager.GetCurrentMonthModel();
        if (deadline.DeadLine >= eDate)
            eDate = projectinfo.EndDate.Value.AddMonths(2);
        differ = (eDate.Year - bDate.Year) * 12 + (eDate.Month - bDate.Month) + 1;
        List<ESP.Finance.Entity.PaymentInfo> pList = new List<ESP.Finance.Entity.PaymentInfo>();
        for (int i = 0; i < differ; i++)
        {
            ESP.Finance.Entity.PaymentInfo payment = new ESP.Finance.Entity.PaymentInfo();
            payment.ProjectID = projectinfo.ProjectId;
            payment.ProjectCode = projectinfo.ProjectCode;
            payment.BranchID = projectinfo.BranchID;
            payment.BranchCode = projectinfo.BranchCode;
            payment.BranchName = projectinfo.BranchName;
            if (i == differ - 1)
            {
                payment.PaymentBudget = totalAmount;
                totalAmount = 0;
            }
            else
            {
                payment.PaymentBudget = System.Math.Round(Convert.ToDecimal(projectinfo.TotalAmount / differ), 2);
                totalAmount = totalAmount - System.Math.Round(Convert.ToDecimal(projectinfo.TotalAmount / differ), 2);
            }
            payment.PaymentPreDate = new DateTime(bDate.Year, bDate.Month, 5);
            payment.PaymentContent = bDate.Year.ToString() + "年" + bDate.Month.ToString() + "月付款通知";
            bDate = bDate.AddMonths(1);
            payment.Remark = "系统生成";
            pList.Add(payment);
        }
        if (ESP.Finance.BusinessLogic.PaymentManager.Add(pList) > 0)
        {
            InitProjectInfo();
            sendMail(projectinfo.ProjectId);
        }
    }

    protected void btnNewPayment_Click(object sender, EventArgs e)
    {
        if (projectinfo == null)
        {
            projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));

        }
        ESP.Finance.Entity.PaymentInfo payment = null;

        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.PaymentID]) && Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.PaymentID]) != 0)
        {
            payment = ESP.Finance.BusinessLogic.PaymentManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.PaymentID]));
        }
        else
        {
            payment = new ESP.Finance.Entity.PaymentInfo();
        }

        payment.ProjectID = projectinfo.ProjectId;
        payment.ProjectCode = projectinfo.ProjectCode;
        payment.BranchID = projectinfo.BranchID;
        payment.BranchCode = projectinfo.BranchCode;
        payment.BranchName = projectinfo.BranchName;
        payment.PaymentBudget = Convert.ToDecimal(this.txtContent.Text);
        payment.PaymentPreDate = Convert.ToDateTime(this.dpDailyStartTime.Text);
        payment.PaymentContent = this.txtpaymentContent.Text;

        payment.Remark = StringHelper.SubString(this.txtRemark.Text.Trim(), 500);

        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.PaymentID]) && Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.PaymentID]) != 0)
        {
            ESP.Finance.BusinessLogic.PaymentManager.Update(payment);
            sendMail(payment.ProjectID);
        }
        else
        {
            if (ESP.Finance.BusinessLogic.PaymentManager.Add(payment) > 0)
            {
                sendMail(payment.ProjectID);
                if (this.hidPaymentContentID.Value == "")
                {
                    ESP.Finance.Entity.PaymentContentInfo content = new ESP.Finance.Entity.PaymentContentInfo();
                    content.paymentContent = this.txtpaymentContent.Text;
                    ESP.Finance.BusinessLogic.PaymentContentManager.Add(content);
                }
                else
                {
                    ESP.Finance.Entity.PaymentContentInfo content = ESP.Finance.BusinessLogic.PaymentContentManager.GetModel(Convert.ToInt32(this.hidPaymentContentID.Value));
                    content.paymentContent = this.txtpaymentContent.Text;
                    ESP.Finance.BusinessLogic.PaymentContentManager.Update(content);
                }
            }
        }
        //backurl=/project/ProjectEdit.aspx&
        //Operate=BizAudit
        Response.Redirect("/Dialogs/PaymentDlg.aspx?" + RequestName.ProjectID + "=" + payment.ProjectID.ToString() + "&" + RequestName.BackUrl + "=" + Request[RequestName.BackUrl] + "&" + RequestName.Operate + "=" + Request[RequestName.Operate]);
    }

    private void sendMail(int projectID)
    {
        ESP.Finance.Entity.ProjectInfo model = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectID);
        if (!string.IsNullOrEmpty(model.ProjectCode) && !string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.Operate]))
        {
            ESP.Finance.Entity.BranchInfo branchModel  = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(model.BranchCode);
            ESP.Finance.Entity.BranchDeptInfo deptModel = ESP.Finance.BusinessLogic.BranchDeptManager.GetModel(branchModel.BranchID, model.GroupID.Value);
            ESP.Compatible.Employee emp = null;
            if (deptModel != null)
            {
                emp = new ESP.Compatible.Employee(deptModel.FianceFirstAuditorID);
            }
            else
            {
                emp = new ESP.Compatible.Employee(branchModel.ProjectAccounter);
            }
            if (emp != null )
            {
                try
                {
                    SendMailHelper.SendMailPaymentNotify(model, model.ApplicantEmployeeName, emp.Name, emp.EMail);
                }
                catch { }
            }
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        script = @"var uniqueId = 'ctl00$ContentPlaceHolder1$PaymentInfo$';
opener.btnRetClick();
window.close(); ";

        ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), script, true);
    }

    protected void btnSelect_OnClick(object sender, EventArgs e)
    {
        this.divPayment.Style["display"] = "block";
        BindPayment();
    }

    protected void gvPaymentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            string pid = gvPaymentContent.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString();
            ESP.Finance.Entity.PaymentContentInfo content = ESP.Finance.BusinessLogic.PaymentContentManager.GetModel(int.Parse(pid));
            this.hidPaymentContentID.Value = pid;
            this.txtpaymentContent.Text = content.paymentContent;
            this.divPayment.Style["display"] = "none";
        }
        if (e.CommandName == "Del")
        {
            int paymentContentid = int.Parse(e.CommandArgument.ToString());
            ESP.Finance.BusinessLogic.PaymentContentManager.Delete(paymentContentid);
            BindPayment();
        }
    }

    protected void gvPaymentContent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
        }

    }

    protected void gvPaymentContent_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvPaymentContent.PageIndex = e.NewPageIndex;
        BindPayment();
    }

    protected void gvPayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvPayment.PageIndex = e.NewPageIndex;
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
        if (e.CommandName == "Edit")
        {
            int paymentid = int.Parse(e.CommandArgument.ToString());
            ESP.Finance.Entity.PaymentInfo p = ESP.Finance.BusinessLogic.PaymentManager.GetModel(paymentid);
            Response.Redirect("/Dialogs/PaymentDlg.aspx?" + RequestName.ProjectID + "=" + p.ProjectID.ToString() + "&" + RequestName.PaymentID + "=" + p.PaymentID.ToString());

        }

    }

    public void InitProjectInfo()
    {
        if (projectinfo == null)
        {
            projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));

        }
        List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
        System.Data.SqlClient.SqlParameter p = new System.Data.SqlClient.SqlParameter("@projectid", Request[ESP.Finance.Utility.RequestName.ProjectID]);
        paramlist.Add(p);
        paymentlist = (List<ESP.Finance.Entity.PaymentInfo>)ESP.Finance.BusinessLogic.PaymentManager.GetList(" projectid=@projectid", paramlist);
        this.gvPayment.DataSource = paymentlist;
        this.gvPayment.DataBind();
        BindTotal(paymentlist);
        decimal paid = 0;
        foreach (ESP.Finance.Entity.PaymentInfo model in paymentlist)
        {
            paid += model.PaymentBudget == null ? 0 : model.PaymentBudget.Value;
        }

        if (!projectinfo.isRecharge)
        {
            this.hidTotal.Value = (projectinfo.TotalAmount - paid).ToString();
            this.lblTotalAmount.Text = (projectinfo.TotalAmount - paid).Value.ToString("#,##0.00");
        }
        else {
            this.hidTotal.Value = (projectinfo.AccountsReceivable - paid).ToString();
            this.lblTotalAmount.Text = (projectinfo.AccountsReceivable - paid).Value.ToString("#,##0.00");
        }

        this.txtContent.Text = string.Empty;
        this.txtpaymentContent.Text = string.Empty;
        this.txtRemark.Text = string.Empty;
        this.hidPaymentContentID.Value = string.Empty;
        this.hidTotal.Value = string.Empty;
        this.dpDailyStartTime.Text = string.Empty;
    }
    private string GetUser()
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
        return retuser;
    }
    protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ESP.Finance.Entity.DeadLineInfo deadLine = ESP.Finance.BusinessLogic.DeadLineManager.GetCurrentMonthModel();

            Label lblNo = (Label)e.Row.FindControl("lblNo");
            lblNo.Text = (e.Row.RowIndex + 1).ToString();
            ESP.Finance.Entity.PaymentInfo item = (ESP.Finance.Entity.PaymentInfo)e.Row.DataItem;
            Label lblPaymentBudget = (Label)e.Row.FindControl("lblPaymentBudget");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            if (item.PaymentStatus != (int)ReturnStatus.Save)
            {
                lnkDelete.Visible = false;
            }
            string users = "," + GetUser() + ",";
            if (lblPaymentBudget != null && item.PaymentBudget != null)
            {
                lblPaymentBudget.Text = Convert.ToDecimal(item.PaymentBudget).ToString("#,##0.00");
            }
            if (item.PaymentStatus != (int)ESP.Finance.Utility.ReturnStatus.Save && users.IndexOf("," + CurrentUser.SysID + ",") < 0)
            {
                e.Row.Cells[5].Text = "&nbsp;";
                e.Row.Cells[6].Text = "&nbsp;";
            }

            if (!string.IsNullOrEmpty(item.PaymentCode))
            {
                e.Row.Cells[6].Text = "&nbsp;";
            }
        }
    }
}

