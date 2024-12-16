using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

public partial class Reports_InvoiceSigning : ESP.Web.UI.PageBase
{
    string term = string.Empty;
    List<SqlParameter> paramlist = null;
    private static IList<ESP.Finance.Entity.InvoiceDetailReporterInfo> InvoiceList = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Reports_ProjectSigning));
        this.ddlBranch.Attributes.Add("onChange", "selectBranch(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");
       
        if (!IsPostBack)
        {
            Search();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }
    protected void btnSearchAll_Click(object sender, EventArgs e)
    {
        this.txtKey.Text = "";
        this.txtEndDate.Text = "";
        this.txtBeginDate.Text = "";
        Search();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string title=string.Empty;
        if (!string.IsNullOrEmpty(this.hidBranchName.Value))
        {
            title = this.hidBranchName.Value;
        }
        try
        {
           ESP.Finance.BusinessLogic.InvoiceDetailReportManager.GetInvoiceReport(InvoiceList, title, this.txtBeginDate.Text, this.txtEndDate.Text,Response);
           GC.Collect();
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('"+ex.Message+"');", true);
        }
    }

    private void Search()
    {
        term = string.Empty;
        paramlist = new List<SqlParameter>();
        if (!string.IsNullOrEmpty(this.txtKey.Text))
        {
            term += " (a.InvoiceNo like '%'+@InvoiceCode+'%' or payment.ProjectCode like '%'+@InvoiceCode+'%' or a.PaymentCode like '%'+@InvoiceCode+'%' or invoice.CustomerShortName like '%'+@InvoiceCode+'%' or a.Description like '%'+@InvoiceCode+'%') and ";
            SqlParameter p1 = new SqlParameter("@InvoiceCode", SqlDbType.NVarChar, 50);
            p1.SqlValue = this.txtKey.Text;
            paramlist.Add(p1);
        }
        if (!string.IsNullOrEmpty(this.txtBeginDate.Text.Trim()) && !string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
        {
            term += " (a.CreateDate between @BeginDate and @EndDate) and ";
            SqlParameter p2 = new SqlParameter("@BeginDate", SqlDbType.DateTime, 8);
            p2.SqlValue = this.txtBeginDate.Text;
            paramlist.Add(p2);
            SqlParameter p3 = new SqlParameter("@EndDate", SqlDbType.DateTime, 8);
            p3.SqlValue = this.txtEndDate.Text;
            paramlist.Add(p3);
        }
        if (!string.IsNullOrEmpty(this.hidBranchID.Value) && this.hidBranchID.Value != "-1")
        {
            term += " invoice.BranchID =@BranchID and ";
            SqlParameter p4 = new SqlParameter("@BranchID", SqlDbType.Int, 4);
            p4.Value = this.hidBranchID.Value;
            paramlist.Add(p4);
        }
        if (!string.IsNullOrEmpty(term))
            term = term.Substring(0, term.Length - 4);

        InvoiceList = ESP.Finance.BusinessLogic.InvoiceDetailReportManager.GetList(term, paramlist);
        this.gvG.DataSource = InvoiceList;
        this.gvG.DataBind();
    }

    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ESP.Finance.Entity.InvoiceDetailReporterInfo model = (ESP.Finance.Entity.InvoiceDetailReporterInfo)e.Row.DataItem;
            Label lblRemark = (Label)e.Row.FindControl("lblRemark");
            Label lblDesc = (Label)e.Row.FindControl("lblDesc");
            Label lblFee= (Label)e.Row.FindControl("lblFee");
            Label lblDiffer = (Label)e.Row.FindControl("lblDiffer");
            if (lblFee != null)
            {
                lblFee.Text = model.Amounts == null ? "0.00" : model.Amounts.Value.ToString("#,##0.00");
            }
            if (lblDiffer != null)
            {
                lblDiffer.Text = model.USDDiffer == null ? "0.00" : model.USDDiffer.Value.ToString("#,##0.00");
            }
            if (lblRemark != null)
            {
                if (model.Remark != null)
                {
                    if (model.Remark.Length > 10)
                        lblRemark.Text = model.Remark.Substring(0, 10) + "...";
                    else
                        lblRemark.Text = model.Remark;
                }
            }

            //if (lblDesc != null)
            //{
            //    if (model.Description != null)
            //    {
            //        if (model.Description.Length > 10)
            //            lblDesc.Text = model.Description.Substring(0, 10) + "...";
            //        else
            //            lblDesc.Text = model.Description;
            //    }
            //}
        }
    }

    protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvG.PageIndex = e.NewPageIndex;
        Search();
    }
    [AjaxPro.AjaxMethod]
    public static List<List<string>> GetBranchList()
    {
        IList<ESP.Finance.Entity.BranchInfo> blist = ESP.Finance.BusinessLogic.BranchManager.GetList(null, null);
        List<List<string>> list = new List<List<string>>();
        List<string> item = null;
        foreach (ESP.Finance.Entity.BranchInfo branch in blist)
        {
            item = new List<string>();
            item.Add(branch.BranchID.ToString());
            item.Add(branch.BranchName);
            list.Add(item);
        }
        List<string> c = new List<string>();
        c.Add("-1");
        c.Add("请选择...");
        list.Insert(0, c);
        return list;
    }
}
