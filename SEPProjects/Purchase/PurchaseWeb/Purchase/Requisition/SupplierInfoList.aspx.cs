using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_SupplierInfoList : ESP.Web.UI.PageBase
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
            ddlStatus.Items.Add(new ListItem("全部", "-1"));
            for (int i = 0; i < ESP.Purchase.Common.State.supplierstatus.Length; i++)
            {
                ddlStatus.Items.Add(new ListItem(ESP.Purchase.Common.State.supplierstatus[i], i.ToString()));
            }
            ddlSupplierType.Items.Add(new ListItem(State.supplierType_Names[(int)State.supplier_type.agreement], ((int)State.supplier_type.agreement).ToString()));
            ddlSupplierType.Items.Add(new ListItem(State.supplierType_Names[(int)State.supplier_type.noAgreement], ((int)State.supplier_type.noAgreement).ToString()));
            ddlSupplierType.Items.Add(new ListItem(State.supplierType_Names[(int)State.supplier_type.recommend], ((int)State.supplier_type.recommend).ToString()));
            ddlSupplierType.Items.Insert(0, new ListItem("全部", "0"));
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
        TreeNode tn;
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

    private void ListBind()
    {
        ListBind(ViewState["SortExpression"] == null ? "" : ViewState["SortExpression"].ToString(), ViewState["SortDirection"] == null ? "" : ViewState["SortDirection"].ToString());
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        List<SupplierInfo> list = (List<SupplierInfo>)ViewState["SupplierList"];
        FileHelper.ExportSupplier(list, Server.MapPath("~"), Response);
        GC.Collect();
    }

    /// <summary>
    /// Lists the bind.
    /// </summary>
    private void ListBind(string sortColumn, string sortDirection)
    {
        string Terms = "";
        string typeids = "";
        int auditorId = 0;
        List<SqlParameter> parms = new List<SqlParameter>();
        if (ddlStatus.SelectedValue != "-1")
        {
            Terms += " and supplier_status=@supplier_status";
            parms.Add(new SqlParameter("@supplier_status", ddlStatus.SelectedValue));
        }
        if (txtsupplierName.Text.Trim() != "")
        {
            Terms += " and supplier_name like '%'+@suppliername+'%'";
            parms.Add(new SqlParameter("@suppliername", txtsupplierName.Text.Trim()));
        }
        if (txtArea.Text.Trim() != "")
        {
            Terms += " and supplier_area like '%'+@supplier_area+'%'";
            parms.Add(new SqlParameter("@supplier_area", txtArea.Text.Trim()));
        }
        if (txtlinkName.Text.Trim() != "")
        {
            Terms += " and contact_name like '%'+@linkName+'%'";
            parms.Add(new SqlParameter("@linkName", txtlinkName.Text.Trim()));
        }

        if (txtType.Text.Trim() != "")
        {
            typeids = txtType.Text.Trim();
        }
        if (txtAuditor.Text.Trim() != "")
        {
            IList<ESP.Framework.Entity.EmployeeInfo> empList = ESP.Framework.BusinessLogic.EmployeeManager.SearchByChineseName(txtAuditor.Text.Trim());
            if (empList != null && empList.Count > 0)
            {
                auditorId = empList[0].UserID;
            }
        }
        if (ddlSupplierType.SelectedValue != "0")
            Terms += " and supplier_type = " + ddlSupplierType.SelectedValue;

        List<SupplierInfo> list = new List<SupplierInfo>();
        if (txtType.Text.Trim() == "" && txtAuditor.Text.Trim() == "")
        {
            list = SupplierManager.getAllModelList(Terms, parms);
        }
        else if (txtType.Text.Trim() != "")
        {
            list = SupplierManager.getAllSupplierListByTypeNames(typeids, Terms, parms);
        }
        else if (txtAuditor.Text.Trim() != "")
        {
            list = SupplierManager.getSupplierListByAuditorId(auditorId, Terms, parms,true);
        }
        list.Sort(new Comparison<SupplierInfo>(delegate(SupplierInfo x, SupplierInfo y)
        {
            if (sortColumn == "supplier_name")
                if (sortDirection == "ASC")
                    return x.supplier_name.CompareTo(y.supplier_name);
                else
                    return y.supplier_name.CompareTo(x.supplier_name);
            else if (sortColumn == "supplier_area")
                if (sortDirection == "ASC")
                    return x.supplier_area.CompareTo(y.supplier_area);
                else
                    return y.supplier_area.CompareTo(x.supplier_area);
            else if (sortColumn == "contact_name")
                if (sortDirection == "ASC")
                    return x.contact_name.CompareTo(y.contact_name);
                else
                    return y.contact_name.CompareTo(x.contact_name);
            else
                if (sortDirection == "ASC")
                    return x.supplier_name.CompareTo(y.supplier_name);
                else
                    return y.supplier_name.CompareTo(x.supplier_name);
        }));
        ViewState["SupplierList"] = list;
        gvSupplier.DataSource = list;
        gvSupplier.DataBind();
        if (gvSupplier.PageCount > 1)
        {
            PageBottom.Visible = true;
            PageTop.Visible = true;
        }
        else
        {
            PageBottom.Visible = false;
            PageTop.Visible = false;
        }
        if (list.Count > 0)
        {
            tabTop.Visible = true;
            tabBottom.Visible = true;
        }
        else
        {
            tabTop.Visible = false;
            tabBottom.Visible = false;
        }

        labAllNum.Text = labAllNumT.Text = list.Count.ToString();
        labPageCount.Text = labPageCountT.Text = (gvSupplier.PageIndex + 1).ToString() + "/" + gvSupplier.PageCount.ToString();
        if (gvSupplier.PageCount > 0)
        {
            if (gvSupplier.PageIndex + 1 == gvSupplier.PageCount)
                disButton("last");
            else if (gvSupplier.PageIndex == 0)
                disButton("first");
            else
                disButton("");
        }
    }

    protected void gvSupplier_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["SortDirection"] = (ViewState["SortDirection"] == null || ViewState["SortDirection"].ToString() == "DESC") ? "ASC" : "DESC";
        ViewState["SortExpression"] = e.SortExpression;
        ListBind();
    }

    #region 分页
    protected void gvSupplier_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSupplier.PageIndex = e.NewPageIndex;
        ListBind();
    }

    protected void btnLast_Click(object sender, EventArgs e)
    {
        Paging(gvSupplier.PageCount);
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        Paging(0);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Paging((gvSupplier.PageIndex + 1) > gvSupplier.PageCount ? gvSupplier.PageCount : (gvSupplier.PageIndex + 1));
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Paging((gvSupplier.PageIndex - 1) < 0 ? 0 : (gvSupplier.PageIndex - 1));
    }

    /// <summary>
    /// 翻页
    /// </summary>
    /// <param name="pageIndex">页码</param>
    private void Paging(int pageIndex)
    {
        GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
        gvSupplier_PageIndexChanging(new object(), e);
    }

    private void disButton(string page)
    {
        switch (page)
        {
            case "first":
                btnFirst.Enabled = false;
                btnPrevious.Enabled = false;
                btnNext.Enabled = true;
                btnLast.Enabled = true;

                btnFirst2.Enabled = false;
                btnPrevious2.Enabled = false;
                btnNext2.Enabled = true;
                btnLast2.Enabled = true;
                break;
            case "last":
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                btnNext.Enabled = false;
                btnLast.Enabled = false;

                btnFirst2.Enabled = true;
                btnPrevious2.Enabled = true;
                btnNext2.Enabled = false;
                btnLast2.Enabled = false;
                break;
            default:
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                btnNext.Enabled = true;
                btnLast.Enabled = true;

                btnFirst2.Enabled = true;
                btnPrevious2.Enabled = true;
                btnNext2.Enabled = true;
                btnLast2.Enabled = true;
                break;
        }
    }

    #endregion

    /// <summary>
    /// Handles the RowDeleting event of the gvSupplier control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewDeleteEventArgs"/> instance containing the event data.</param>
    protected void gvSupplier_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = int.Parse(gvSupplier.DataKeys[e.RowIndex].Value.ToString());
        SupplierManager.Delete(id);
        ListBind();
    }

    /// <summary>
    /// Handles the RowDataBound event of the gv control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ESP.Purchase.Entity.SupplierInfo model = (ESP.Purchase.Entity.SupplierInfo)e.Row.DataItem;
            Label labPhone = (Label)e.Row.FindControl("labPhone");
            LinkButton lnkUse = (LinkButton)e.Row.FindControl("lnkUse");
            if (labPhone != null && labPhone.Text != string.Empty)
            {
                if (labPhone.Text[labPhone.Text.Length - 1] == '-')
                {
                    labPhone.Text = labPhone.Text.Substring(0, labPhone.Text.Length - 1);
                }
            }
            Label labFax = (Label)e.Row.FindControl("labFax");
            if (labFax != null && labFax.Text != string.Empty)
            {
                if (labFax.Text[labFax.Text.Length - 1] == '-')
                {
                    labFax.Text = labFax.Text.Substring(0, labFax.Text.Length - 1);
                }
            }
            if (model.supplier_status == (int)ESP.Purchase.Common.State.supplierstatus_used)
            {
                lnkUse.OnClientClick = "return confirm('您确定停用吗？');";
                lnkUse.Text = "<img src='/images/disable.gif' border='0' />";
                lnkUse.CommandName = "Delete";
            }
            else
            {
                lnkUse.OnClientClick = "return confirm('您确定启用吗？');";
                lnkUse.Text = "<img src='/images/used.gif' border='0' />";
                lnkUse.CommandName = "Use";
            }
            Literal litType = (Literal)e.Row.FindControl("litType");
            if (model.supplier_type == (int)State.supplier_type.agreement)
                litType.Text = "协议供应商";
            else if (model.supplier_type == (int)State.supplier_type.recommend)
                litType.Text = "推荐供应商";
            else
                litType.Text = "临时供应商";
        }

        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[9].Visible = false;
        }
    }

    protected void gvSupplier_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Changed")
        {
            int supplierId = int.Parse(e.CommandArgument.ToString());
            SupplierManager.ChangedTypeToRecommend(supplierId);

            ListBind();
        }
        else if (e.CommandName == "Use")
        {
            int supplierId = int.Parse(e.CommandArgument.ToString());
            ESP.Purchase.Entity.SupplierInfo model = ESP.Purchase.BusinessLogic.SupplierManager.GetModel(supplierId);
            model.supplier_status = (int)ESP.Purchase.Common.State.supplierstatus_used;
            ESP.Purchase.BusinessLogic.SupplierManager.Update(model);
            ListBind();
        }
    }

    /// <summary>
    /// Recommends the specified id.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="rem">The rem.</param>
    /// <returns></returns>
    protected string Recommend(string id, string rem)
    {
        if (id == null)
        {
            return "";
        }
        else if (rem == "0")
        {
            return "<a  style=\"cursor:hand;text-decoration:underline\" onclick=\"recommend('" + id.Trim() + "','rem')\" > 推荐</a>";
        }
        else
        {
            return "<a  style=\"cursor:hand;text-decoration:underline\" onclick=\"recommend('" + id.Trim() + "','unrem')\" > 取消推荐</a>";
        }
    }
}