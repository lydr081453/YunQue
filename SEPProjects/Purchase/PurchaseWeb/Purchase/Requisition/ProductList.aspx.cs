using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

public partial class Purchase_ProductList : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region AjaxProRegister
        AjaxPro.Utility.RegisterTypeForAjax(typeof(TypeDataProvider));
        #endregion

        if (!IsPostBack)
        {
            TypeBind();
            ListBind();
        }

    }

    private void TypeBind()
    {
        List<TypeInfo> list = TypeManager.GetListByParentId(0);
        ddltype.DataSource = list;
        ddltype.DataTextField = "typename";
        ddltype.DataValueField = "typeid";
        ddltype.DataBind();
        ddltype.Items.Insert(0, new ListItem("请选择...", "-1"));
        ddltype1.Items.Insert(0, new ListItem("请选择...", "-1"));
        ddltype2.Items.Insert(0, new ListItem("请选择...", "-1"));
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ListBind();
    }

    private void ListBind()
    {
        string Terms = "";
        List<SqlParameter> parms = new List<SqlParameter>();

        if (txtproductName.Text.Trim() != "")
        {
            Terms += " and a.productName like '%'+@productName+'%'";
            parms.Add(new SqlParameter("@productName", txtproductName.Text.Trim()));
        }
        if (txtsupplierName.Text.Trim() != "")
        {
            Terms += " and b.supplier_name like '%'+@suppliername+'%'";
            parms.Add(new SqlParameter("@suppliername", txtsupplierName.Text.Trim()));
        }
        if (txtproductClass.Text.Trim() != "")
        {
            Terms += " and a.productClass like '%'+@productClass+'%'";
            parms.Add(new SqlParameter("@productClass", txtproductClass.Text.Trim()));
        }
        string strType = string.Empty;

        if (ddltype.SelectedValue != "-1")
        {
            strType = "select typeid from t_type where parentid in(select a.typeid from t_type a inner join t_type b on a.parentid=b.typeid where a.parentid=" + ddltype.SelectedValue + ")";

            if (hidtype1.Value != "" && hidtype1.Value != "-1")
            {
                strType = "select typeid from t_type where parentid =" + hidtype1.Value;
                if (hidtype2.Value != "" && hidtype2.Value != "-1")
                    strType = hidtype2.Value;
            }
        }
        if (strType != string.Empty)
        {
            Terms += " and a.productType in (" + strType + ")";
        }

        List<ProductInfo> list = ProductManager.getModelList(Terms, parms);
        gvProduct.DataSource = list;
        gvProduct.DataBind();

        if (gvProduct.PageCount > 1)
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
        labPageCount.Text = labPageCountT.Text = (gvProduct.PageIndex + 1).ToString() + "/" + gvProduct.PageCount.ToString();

        if (gvProduct.PageCount > 0)
        {
            if (gvProduct.PageIndex + 1 == gvProduct.PageCount)
                disButton("last");
            else if (gvProduct.PageIndex == 0)
                disButton("first");
            else
                disButton("");
        }
    }

    protected void btnLast_Click(object sender, EventArgs e)
    {
        Paging(gvProduct.PageCount);
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        Paging(0);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Paging((gvProduct.PageIndex + 1) > gvProduct.PageCount ? gvProduct.PageCount : (gvProduct.PageIndex + 1));
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Paging((gvProduct.PageIndex - 1) < 0 ? 0 : (gvProduct.PageIndex - 1));
    }

    /// <summary>
    /// 翻页
    /// </summary>
    /// <param name="pageIndex">页码</param>
    private void Paging(int pageIndex)
    {
        GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
        gvProduct_PageIndexChanging(new object(), e);
    }

    protected void gvProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProduct.PageIndex = e.NewPageIndex;
        ListBind();
    }

    protected void gvProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = int.Parse(gvProduct.DataKeys[e.RowIndex].Value.ToString());
        ProductManager.Delete(id);
        ListBind();
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
}
