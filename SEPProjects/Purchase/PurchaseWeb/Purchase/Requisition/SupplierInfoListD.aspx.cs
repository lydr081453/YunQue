using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_SupplierInfoListD : ESP.Web.UI.PageBase
{
    /// <summary>
    /// 页面装载
    /// </summary>
    /// <param name="sender">发送者</param>
    /// <param name="e">事件对象</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        #region AjaxProRegister
        AjaxPro.Utility.RegisterTypeForAjax(typeof(TypeDataProvider));
        #endregion

        if (!IsPostBack)
        {
            ListBind();
        }
    }

    /// <summary>
    /// Binds the tree.
    /// </summary>
    /// <param name="nds">The NDS.</param>
    /// <param name="parentId">The parent id.</param>
    /// <param name="items">The items.</param>
    void BindTree(TreeNodeCollection nds, int parentId, List<TypeInfo> items)
    {
        TreeNode tn = null;
        foreach (TypeInfo model in items)
        {
            tn = new TreeNode(model.typename);
            nds.Add(tn);
            BindTree(tn.ChildNodes, model.typeid, TypeManager.GetListByParentId(model.typeid));
        }
    }

    /// <summary>
    /// Handles the Click event of the btnSearch control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ListBind();
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
        Terms += " and c2.supplier_type = " + (int)State.supplier_type.agreement;
        DataTable dt= SupplierManager.getSupplierListOrderByXieYi(Terms, parms, txtType.Text);

        gvSupplier.DataSource = dt;
        gvSupplier.DataBind();
        Session["supplier"] = dt;
    }

    /// <summary>
    /// Handles the RowDeleting event of the gvSupplier control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewDeleteEventArgs"/> instance containing the event data.</param>
    //protected void gvSupplier_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    int id = int.Parse(gvSupplier.DataKeys[e.RowIndex].Value.ToString());
    //    SupplierManager.Delete(id);
    //    ListBind();
    //}

    /// <summary>
    /// Handles the RowDataBound event of the gv control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[9].Visible = false;
        }
    }

    /// <summary>
    /// Handles the Click event of the btnBack control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Default.aspx");
    }
}