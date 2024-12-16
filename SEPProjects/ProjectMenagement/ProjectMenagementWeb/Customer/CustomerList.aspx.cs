using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using System.Text;

public partial class Customer_CustomerList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Search();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }
    protected void gvCustomers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gvCustomers.PageIndex = e.NewPageIndex;
        Search();
    }
    protected void btnSearchAll_Click(object sender, EventArgs e)
    {
        this.txtCustomerNameCN.Text = string.Empty;
        this.txtShortNameEN.Text = string.Empty;
        Search();
    }

    private void Search()
    {
        StringBuilder condition = new StringBuilder();

        condition.Append(" 1=1 ");
        List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
        if (this.txtCustomerNameCN.Text.Trim() != string.Empty)
        {
            condition.Append(" AND NameCN1 LIKE('%" + this.txtCustomerNameCN.Text.Trim() + "%')");
        }
        if (this.txtShortNameEN.Text.Trim() != string.Empty)
        {
            condition.Append(" AND ShortEN LIKE('%" + this.txtShortNameEN.Text.Trim() + "%')");
        }
        IList<CustomerInfo> list = ESP.Finance.BusinessLogic.CustomerManager.GetList(condition.ToString(),null);

        gvCustomers.DataSource = list;
        gvCustomers.DataBind();
    }

    #region 翻页
    protected void btnLast_Click(object sender, EventArgs e)
    {
        Paging(gvCustomers.PageCount);
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        Paging(0);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Paging((gvCustomers.PageIndex + 1) > gvCustomers.PageCount ? gvCustomers.PageCount : (gvCustomers.PageIndex + 1));
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Paging((gvCustomers.PageIndex - 1) < 0 ? 0 : (gvCustomers.PageIndex - 1));
    }

    /// <summary>
    /// 翻页
    /// </summary>
    /// <param name="pageIndex">页码</param>
    private void Paging(int pageIndex)
    {
        GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
        gvCustomer_PageIndexChanging(new object(), e);
    }

    protected void gvCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCustomers.PageIndex = e.NewPageIndex;
        Search();
    }
    #endregion 翻页

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Label labPhone = (Label)e.Row.FindControl("labPhone");
            //if (labPhone != null && labPhone.Text != string.Empty)
            //{
            //    if (labPhone.Text[labPhone.Text.Length - 1] == '-')
            //    {
            //        labPhone.Text = labPhone.Text.Substring(0, labPhone.Text.Length - 1);
            //    }
            //}
            //Label labFax = (Label)e.Row.FindControl("labFax");
            //if (labFax != null && labFax.Text != string.Empty)
            //{
            //    if (labFax.Text[labFax.Text.Length - 1] == '-')
            //    {
            //        labFax.Text = labFax.Text.Substring(0, labFax.Text.Length - 1);
            //    }
            //}
        }

        //if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        //{
        //    e.Row.Cells[10].Visible = false;
        //}
    }

    //private void disButton(string page)
    //{
    //    switch (page)
    //    {
    //        case "first":
    //            btnFirst.Enabled = false;
    //            btnPrevious.Enabled = false;
    //            btnNext.Enabled = true;
    //            btnLast.Enabled = true;

    //            btnFirst2.Enabled = false;
    //            btnPrevious2.Enabled = false;
    //            btnNext2.Enabled = true;
    //            btnLast2.Enabled = true;
    //            break;
    //        case "last":
    //            btnFirst.Enabled = true;
    //            btnPrevious.Enabled = true;
    //            btnNext.Enabled = false;
    //            btnLast.Enabled = false;

    //            btnFirst2.Enabled = true;
    //            btnPrevious2.Enabled = true;
    //            btnNext2.Enabled = false;
    //            btnLast2.Enabled = false;
    //            break;
    //        default:
    //            btnFirst.Enabled = true;
    //            btnPrevious.Enabled = true;
    //            btnNext.Enabled = true;
    //            btnLast.Enabled = true;

    //            btnFirst2.Enabled = true;
    //            btnPrevious2.Enabled = true;
    //            btnNext2.Enabled = true;
    //            btnLast2.Enabled = true;
    //            break;
    //    }
    //}
}
