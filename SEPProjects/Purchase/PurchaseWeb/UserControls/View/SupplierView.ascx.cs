using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Entity;

public partial class UserControls_View_SupplierView : System.Web.UI.UserControl
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
            BindInfo();
            ListBind();
            TabContainer1.ActiveTabIndex = 1;
        }
    }

    private void BindInfo()
    {
        SupplierInfo model = SupplierManager.GetModel(supplierId);

        #region 基础信息
        labSupplierName.Text = model.supplier_name;
        labsupplier_area.Text = model.supplier_area;
        labsupplier_industry.Text = model.supplier_industry;
        labsupplier_scale.Text = model.supplier_scale;
        labsupplier_principal.Text = model.supplier_principal;
        labsupplier_builttime.Text = model.supplier_builttime;
        hpysupplier_website.Text = model.supplier_website;
        hpysupplier_website.NavigateUrl = model.supplier_website;
        labsupplier_source.Text = model.supplier_source;
        labfa_no.Text = model.supplier_frameNO;
        #endregion

        #region 联系信息
        labLinker.Text = model.contact_name;
        labCellPhone.Text = model.contact_mobile;
        labTelPhone.Text = model.contact_tel;
        labFax.Text = model.contact_fax;
        labEmail.Text = model.contact_email;
        labAddress.Text = model.contact_address;
        #endregion

        #region 产品服务信息
        labservice_content.Text = model.service_content;
        labservice_area.Text = model.service_area;
        labservice_workamount.Text = model.service_workamount;
        labservice_customization.Text = model.service_customization;
        labservice_ohter.Text = model.service_ohter;
        labservice_forshunya.Text = model.service_forshunya;
        #endregion

        #region 商务条款
        labbbusiness_price.Text = model.business_price == "" ? "暂无" : "<a target='_blank' href='../../" + model.business_price + "'><img src='/images/ico_04.gif' border='0' /></a>";
        labbusiness_paytime.Text = model.business_paytime;
        labbusiness_prepay.Text = model.business_prepay;
        #endregion

        #region 评估信息
        labevaluation_department.Text = model.evaluation_department;
        labevaluation_level.Text = model.evaluation_level;
        labevaluation_feedback.Text = model.evaluation_feedback;
        labevaluation_note.Text = model.evaluation_note;
        #endregion

        #region 帐户信息
        labaccount_name.Text = model.account_name;
        labaccount_bank.Text = model.account_bank;
        labaccount_number.Text = model.account_number;
        #endregion

        #region 产品图片
        List<ImageInfo> lsImage = ImageManager.GetListBySupplierID(supplierId);
        dlimage.DataSource = lsImage;
        dlimage.DataBind();
        #endregion
    }

    private void ListBind()
    {
        List<ProductInfo> list = ProductManager.getListBySupplierId(supplierId, "");
        gvProduct.DataSource = list;
        gvProduct.DataBind();

        List<ImageInfo> lsImage = ImageManager.GetListBySupplierID(supplierId);
        dlimage.DataSource = lsImage;
        dlimage.DataBind();

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
