using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_SupplierListForSearch : ESP.Web.UI.PageBase
{
    int productTypeId = 0;

    /// <summary>
    /// 页面装载
    /// </summary>
    /// <param name="sender">发送者</param>
    /// <param name="e">事件对象</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ListBind();
        }
         
    }

    /// <summary>
    /// Lists the bind.
    /// </summary>
    private void ListBind()
    {
        string Terms = "";
        string typeids = "";
        List<SqlParameter> parms = new List<SqlParameter>();
        if (txtsupplierName.Text.Trim() != "")
        {
            Terms += " and c2.supplier_name like '%'+@suppliername+'%'";
            parms.Add(new SqlParameter("@suppliername", txtsupplierName.Text.Trim()));
        }
        if (txtArea.Text.Trim() != "")
        {
            Terms += " and c2.supplier_area like '%'+@supplier_area+'%'";
            parms.Add(new SqlParameter("@supplier_area", txtArea.Text.Trim()));
        }
        if (txtlinkName.Text.Trim() != "")
        {
            Terms += " and c2.contact_name like '%'+@linkName+'%'";
            parms.Add(new SqlParameter("@linkName", txtlinkName.Text.Trim()));
        }
        if (txtType.Text.Trim() != "")
        {
            typeids = txtType.Text.Trim();
        }
       
        DataTable dt = SupplierManager.getSupplierListOrderByRecommend(Terms, parms, txtType.Text);

        gvSupplier.DataSource = dt;
        gvSupplier.DataBind();
       
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ListBind();
    }

    /// <summary>
    /// Handles the RowDataBound event of the gv control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[9].Visible = false;
        }
    }
}