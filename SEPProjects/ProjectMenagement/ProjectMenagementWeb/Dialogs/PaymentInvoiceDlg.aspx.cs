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

public partial class Dialogs_PaymentInvoiceDlg : System.Web.UI.Page
{

    //private string clientId = "ctl00_ContentPlaceHolder1_PaymentInfo_";
    private ESP.Finance.Entity.ProjectInfo project_info;
    private ESP.Finance.Entity.PaymentInfo payment_info;
    private static string term = string.Empty;
    private static List<SqlParameter> paramlist = null;
    private IList<ESP.Finance.Entity.InvoiceDetailInfo> invoiceDetaillist;
    string script = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        project_info = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
        payment_info = ESP.Finance.BusinessLogic.PaymentManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.PaymentID]));

        if (!IsPostBack)
        {
            decimal paid = 0;


            term = string.Empty;
            paramlist = null;
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.PaymentID]))
            {
                term = "  CustomerID=@CustomerID and BranchID=@BranchID and InvoiceStatus=@InvoiceStatus ";
                paramlist = new List<SqlParameter>();
                paramlist.Add(new SqlParameter("@CustomerID", project_info.Customer == null ? 0 : project_info.Customer.CustomerID));
                paramlist.Add(new SqlParameter("@BranchID", payment_info.BranchID == null ? 0 : payment_info.BranchID.Value));
                paramlist.Add(new SqlParameter("@InvoiceStatus",(int)ESP.Finance.Utility.InvoiceStatus.Used));
            }

            invoiceDetaillist = ESP.Finance.BusinessLogic.InvoiceDetailManager.GetListByPayment(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.PaymentID]), null, null);
            foreach (ESP.Finance.Entity.InvoiceDetailInfo model in invoiceDetaillist)
            {
                paid += model.Amounts == null ? 0 : model.Amounts.Value;
            }

            this.hidTotal.Value = (payment_info.PaymentBudget-paid).ToString();

            this.lblTotalAmount.Text = (payment_info.PaymentBudget - paid).Value.ToString("#,##0.00");
                IList<ESP.Finance.Entity.InvoiceInfo> invoiceList = ESP.Finance.BusinessLogic.InvoiceManager.GetList(term, paramlist);
                gvInvoice.DataSource = invoiceList;
                this.gvInvoice.DataBind();
            this.divInvoice.Style["display"] = "none";

            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.InvoiceDetailID]))
            {
                ESP.Finance.Entity.InvoiceDetailInfo invoiceDetail = ESP.Finance.BusinessLogic.InvoiceDetailManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.InvoiceDetailID]));
                this.txtAmount.Text = invoiceDetail.Amounts == null ? "" : invoiceDetail.Amounts.Value.ToString("#,##0.00");
                this.txtInvoiceNo.Text = invoiceDetail.InvoiceNo;
                this.txtDes.Text = invoiceDetail.Remark;
                txtDiffer.Text = invoiceDetail.USDDiffer == null ? string.Empty : invoiceDetail.USDDiffer.Value.ToString("#,##0.00");
                this.hidTotal.Value = Convert.ToDecimal(payment_info.PaymentBudget - paid + invoiceDetail.Amounts).ToString();
                this.lblInvoiceBalance.Text = ESP.Finance.BusinessLogic.CheckerManager.GetInvoiceOddAmount(invoiceDetail.InvoiceID.Value).ToString("#,##0.00");
            }

            
            this.gvInvoiceDetail.DataSource = invoiceDetaillist;
            this.gvInvoiceDetail.DataBind();
            BindTotal(invoiceDetaillist);
        }

    }


    private void BindTotal(IList<ESP.Finance.Entity.InvoiceDetailInfo> list)
    {
        this.trTotal.Visible = true;
        this.lblTotal.Text = "总计:";
        decimal total = 0;
        foreach (ESP.Finance.Entity.InvoiceDetailInfo invDetail in list)
        {
            total += Convert.ToDecimal(invDetail.Amounts);
        }
        if (total == 0)
        {
            this.trTotal.Visible = false;
        }
        this.lblTotal.Text += total.ToString("#,##0.00");

        this.lblBlance.Text = "剩余:";
        decimal blance = 0;
        if(project_info==null)
            project_info = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
        blance = Convert.ToDecimal(project_info.TotalAmount) - total;
        this.lblBlance.Text += blance.ToString("#,##0.00");
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        BindInvoice();
    }
    protected void btnClean_Click(object sender, EventArgs e)
    {
        term = string.Empty;
        paramlist = null;
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.PaymentID]))
        {
            term = "  CustomerID=@CustomerID and BranchID=@BranchID and InvoiceStatus=@InvoiceStatus ";
            paramlist = new List<SqlParameter>();
            paramlist.Add(new SqlParameter("@CustomerID", project_info.Customer==null?0:project_info.Customer.CustomerID));
            paramlist.Add(new SqlParameter("@BranchID", payment_info.BranchID==null?0:payment_info.BranchID.Value));
            paramlist.Add(new SqlParameter("@InvoiceStatus", (int)ESP.Finance.Utility.InvoiceStatus.Used));
        }
        IList<ESP.Finance.Entity.InvoiceInfo> invoiceList = ESP.Finance.BusinessLogic.InvoiceManager.GetList(term, paramlist);
        gvInvoice.DataSource = invoiceList;
        this.gvInvoice.DataBind();
    }

    private void BindInvoice()
    {
        //term = " InvoiceCode like '%'+@InvoiceCode+'%'";
        //paramlist = new List<SqlParameter>();
        //paramlist.Add(new SqlParameter("@InvoiceCode", this.txtInvoiceNo.Text.Trim()));

        term = " 1=1 ";
        paramlist = new List<SqlParameter>();

        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.PaymentID]))
        {
            term += " and CustomerID=@CustomerID and BranchID=@BranchID and InvoiceStatus=@InvoiceStatus ";
            paramlist.Add(new SqlParameter("@CustomerID", project_info.Customer == null ? 0 : project_info.Customer.CustomerID));
            paramlist.Add(new SqlParameter("@BranchID", payment_info.BranchID == null ? 0 : payment_info.BranchID.Value));
            paramlist.Add(new SqlParameter("@InvoiceStatus", (int)ESP.Finance.Utility.InvoiceStatus.Used));
        }

        IList<ESP.Finance.Entity.InvoiceInfo> invoiceList = ESP.Finance.BusinessLogic.InvoiceManager.GetList(term, paramlist);
        gvInvoice.DataSource = invoiceList;
        this.gvInvoice.DataBind();
    }

    protected void btnNewInvoiceDetail_Click(object sender, EventArgs e)
    {
        int retvalue=0;
        if (project_info == null)
        {
            project_info = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
        }

        ESP.Finance.Entity.InvoiceDetailInfo invoicedetail = null;

        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.InvoiceDetailID]))
        {
            invoicedetail = ESP.Finance.BusinessLogic.InvoiceDetailManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.InvoiceDetailID]));
        }
        else
        {
            invoicedetail = new ESP.Finance.Entity.InvoiceDetailInfo();
        }
        invoicedetail.CreateDate = DateTime.Now;

        if (project_info != null)
        {
            invoicedetail.ProjectCode = project_info.ProjectCode;
            invoicedetail.ProjectID = project_info.ProjectId;


            invoicedetail.ResponseCode = project_info.ApplicantCode;
            invoicedetail.ResponseEmployeeName = project_info.ApplicantEmployeeName;
            invoicedetail.ResponseUserID = project_info.ApplicantUserID;
            invoicedetail.ResponseUserName = project_info.ApplicantUserName;
        }

        invoicedetail.Description = txtDes.Text;
        invoicedetail.Amounts = Convert.ToDecimal(txtAmount.Text.Replace(",", ""));
        invoicedetail.InvoiceID = string.IsNullOrEmpty(hidInvoiceID.Value)?0: Convert.ToInt32(hidInvoiceID.Value);
        invoicedetail.InvoiceNo = txtInvoiceNo.Text;

        invoicedetail.PaymentCode = payment_info.PaymentCode;
        invoicedetail.PaymentID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.PaymentID]);
        invoicedetail.Remark = string.Empty;
        if (string.IsNullOrEmpty(txtDiffer.Text))
            invoicedetail.USDDiffer = 0;
        else
           invoicedetail.USDDiffer = Convert.ToDecimal(txtDiffer.Text.Replace(",", ""));

        invoicedetail.Description = StringHelper.SubString(this.txtDes.Text.Trim(), 200);

        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.InvoiceDetailID]))
        {
            ESP.Finance.BusinessLogic.InvoiceDetailManager.Update(invoicedetail);
        }
        else
        {
            retvalue=ESP.Finance.BusinessLogic.InvoiceDetailManager.Add(invoicedetail);
            
            if ( retvalue> 0)
            {
                InitProjectInfo();
            }
            else if(retvalue==-1)
            {
                Page.ClientScript.RegisterStartupScript(typeof(string),new Guid().ToString(),"alert('超出发票可用金额!');",true);
            }
            else if (retvalue == -2)
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('超出付款通知金额!');", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('保存时出现未知错误!');", true);
            }
        }
      
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        //string script = string.Format(" opener.location='{0}?{1}={2}';window.close();", Request[ESP.Finance.Utility.RequestName.BackUrl], RequestName.ProjectID, Request[ESP.Finance.Utility.RequestName.ProjectID]);
        //ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), script, true);
        //ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), "window.close();", true);

        script = @"var uniqueId = 'ctl00$ContentPlaceHolder1$';
opener.__doPostBack(uniqueId + 'btnRet', '');
window.close(); ";

        ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), script, true);
    }

    protected void btnSelect_OnClick(object sender, EventArgs e)
    {
        this.divInvoice.Style["display"] = "block";
        BindInvoice();
    }



    protected void gvInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
        }
    }

    protected void gvInvoiceDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInvoiceDetail.PageIndex = e.NewPageIndex;
    }

    protected void gvInvoice_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvInvoice.PageIndex = e.NewPageIndex;
        BindInvoice();
    }

    protected void gvInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            string pid = gvInvoice.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString();
            ESP.Finance.Entity.InvoiceInfo invoice = ESP.Finance.BusinessLogic.InvoiceManager.GetModel(int.Parse(pid));
            this.hidInvoiceID.Value = pid;
            this.txtInvoiceNo.Text = invoice.InvoiceCode;
            this.divInvoice.Style["display"] = "none";
            this.lblInvoiceBalance.Text = ESP.Finance.BusinessLogic.CheckerManager.GetInvoiceOddAmount(Convert.ToInt32(pid)).ToString("#,##0.00") ;
        }
    }

    protected void gvInvoiceDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int invoiceDetailID = int.Parse(e.CommandArgument.ToString());
            ESP.Finance.BusinessLogic.InvoiceDetailManager.Delete(invoiceDetailID);
            InitProjectInfo();
            
        }

    }

    public void InitProjectInfo()
    {
        if (project_info == null)
        {
            project_info = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));

        }

        invoiceDetaillist = ESP.Finance.BusinessLogic.InvoiceDetailManager.GetListByPayment(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.PaymentID]),null,null);
        this.gvInvoiceDetail.DataSource = invoiceDetaillist;
        this.gvInvoiceDetail.DataBind();
        BindTotal(invoiceDetaillist);
        decimal paid = 0;
        foreach (ESP.Finance.Entity.InvoiceDetailInfo model in invoiceDetaillist)
        {
            paid += model.Amounts == null ? 0 : model.Amounts.Value;
        }
        this.hidTotal.Value = (payment_info.PaymentBudget - paid).ToString();

        this.lblTotalAmount.Text = (payment_info.PaymentBudget - paid).Value.ToString("#,##0.00");

        this.txtInvoiceNo.Text = string.Empty;
        this.txtAmount.Text = string.Empty;
        this.txtDiffer.Text = string.Empty;
        this.txtDes.Text = string.Empty;
        this.hidInvoiceID.Value = "0";
        this.hidTotal.Value = string.Empty;
    }

    protected void gvInvoiceDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblNo = (Label)e.Row.FindControl("lblNo");
            lblNo.Text = (e.Row.RowIndex + 1).ToString();
            ESP.Finance.Entity.InvoiceDetailInfo item = (ESP.Finance.Entity.InvoiceDetailInfo)e.Row.DataItem;
            Label lblPaymentBudget = (Label)e.Row.FindControl("lblPaymentBudget");
            if (lblPaymentBudget != null && item.Amounts != null)
            {
                lblPaymentBudget.Text = Convert.ToDecimal(item.Amounts).ToString("#,##0.00");
            }

            Label lblDiffer = (Label)e.Row.FindControl("lblDiffer");
            if (lblDiffer != null && item.Amounts != null)
            {
                lblDiffer.Text = Convert.ToDecimal(item.USDDiffer==null?0:item.USDDiffer.Value).ToString("#,##0.00");
            }
        }
    }
}

