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
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_ItemList : ESP.Web.UI.PageBase
{
    int supplierId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["supplierId"]))
        {
            supplierId = int.Parse(Request["supplierId"]);
        }
        if (!IsPostBack)
        {
            ddlproductType.DataSource = TypeManager.GetAllList();
            ddlproductType.DataTextField = "typename";
            ddlproductType.DataValueField = "typeid";
            ddlproductType.DataBind();
            ddlproductType.Items.Insert(0, new ListItem("请选择", ""));
            ListBind();
        }

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
        if (txtproductClass.Text.Trim() != "")
        {
            Terms += " and a.productClass like '%'+@productClass+'%'";
            parms.Add(new SqlParameter("@productClass", txtproductClass.Text.Trim()));
        }
        if (ddlproductType.SelectedValue != "")
        {
            Terms += " and a.productType=@productType";
            parms.Add(new SqlParameter("@productType", ddlproductType.SelectedValue));
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

    protected void gvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            string id = e.CommandArgument.ToString();
            add(int.Parse(id));
        }
    }

    private void add(int id)
    {
        ProductInfo model = ProductManager.GetModel(id);
        Response.Write("<script>opener.document.all.ctl00_ContentPlaceHolder1_item_No.value='" + model.productName + "';</script>");
        Response.Write("<script>opener.document.all.ctl00_ContentPlaceHolder1_desctiprtion.value='" + model.productDes + "';</script>");
        Response.Write("<script>opener.document.all.ctl00_ContentPlaceHolder1_uom.value='" + model.productUnit + "';</script>");
        Response.Write("<script>opener.document.all.ctl00_ContentPlaceHolder1_hidSupplier.value='" + model.supplierId + "';</script>");
        Response.Write("<script>opener.document.all.ctl00_ContentPlaceHolder1_hiditemNo.value='" + model.productName + "';</script>");
        Response.Write(@"<script>opener.setTypeValue('" + model.productType + "');window.close();</script>");
    }

    protected void gvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (supplierId > 0)
            {
                ProductInfo model = (ProductInfo)e.Row.DataItem;
                if (model.supplierId != supplierId)
                {
                    ((Button)e.Row.FindControl("btnS")).Enabled = false;
                }
            }
        }
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
