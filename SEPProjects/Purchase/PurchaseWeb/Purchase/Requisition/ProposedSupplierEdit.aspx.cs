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
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_ProposedSupplierEdit : ESP.Web.UI.PageBase
{
    public int psupplierId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["psupplierId"]) || !string.IsNullOrEmpty(Request["supplierId"]))
        {
            psupplierId = int.Parse(string.IsNullOrEmpty(Request["psupplierId"]) ? Request["supplierId"] : Request["psupplierId"]);
            pnlPro.Visible = true;
        }
        else
            pnlPro.Visible = false;

        if (!IsPostBack)
        {
            ListBind();
            BindInfo();
            if (!string.IsNullOrEmpty(Request["TabIndex"]))
                TabContainer1.ActiveTabIndex = int.Parse(Request["TabIndex"]);
        }
    }

    #region 按钮事件
    //返回按钮事件
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProposedSupplierList.aspx");
    }

    //保存按钮事件
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SupplierInfo model;
        if (psupplierId == 0)
        {
            model = new SupplierInfo();
            psupplierId = SupplierManager.Add(getModel(model));
            if (psupplierId > 0)
            {
                //记录操作日志
                ESP.Logging.Logger.Add(string.Format("{0}对T_Supplier表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, psupplierId, "添加推荐供应商"), "推荐供应商");
                SaveImage();
                ClientScript.RegisterStartupScript(typeof(string), "", "window.location='ProposedSupplierEdit.aspx?psupplierId=" + psupplierId + "&TabIndex=" + TabContainer1.ActiveTabIndex + "';alert('保存成功！');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存失败！');", true);
            }
        }
        else
        {
            model = SupplierManager.GetModel(psupplierId);
            if (SupplierManager.Update(getModel(model)) > 0)
            {
                //记录操作日志
                ESP.Logging.Logger.Add(string.Format("{0}对T_Supplier表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, psupplierId, "编辑推荐供应商"), "推荐供应商");
                SaveImage();
                ClientScript.RegisterStartupScript(typeof(string), "", "window.location='ProposedSupplierEdit.aspx?psupplierId=" + model.id + "&TabIndex=" + TabContainer1.ActiveTabIndex + "';alert('保存成功！');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存失败！');", true);
            }
        }
    }
    #endregion

    private void SaveImage()
    {
        if (imageupload.HasFile)
        {
            HttpPostedFile myFile = imageupload.PostedFile;

            if (myFile.FileName != null && myFile.ContentLength > 0)//&& theFile.ContentLength <= Config.PHOTO_CONTENT_LENGTH)
            {
                string ext = "";
                for (int n = 0; n < Config.PHOTO_EXTENSIONS.Length; ++n)
                {
                    string FileExt = System.IO.Path.GetExtension(myFile.FileName).ToLower();
                    if (FileExt == Config.PHOTO_EXTENSIONS[n])
                    {
                        ext = Config.PHOTO_EXTENSIONS[n];
                        break;
                    }
                }
                if (ext.Length == 0)
                {
                    ESP.ConfigCommon.MessageBox.Show(Page, "图片格式错误，需要：GIF、JPEG或BMP文件格式！");
                    return;
                }
                else if (myFile.ContentLength > Config.PHOTO_CONTENT_LENGTH)
                {
                    ESP.ConfigCommon.MessageBox.Show(Page, "图片大小不得超过1024k！");
                    return;
                }
                else
                {
                    ImageInfo imagemodel = new ImageInfo();
                    imagemodel.supplier_id = psupplierId;
                    imagemodel.imagename = imageupload.FileName;
                    imagemodel.imageurl = ESP.ConfigCommon.ImageHelper.SavePhoto(myFile.InputStream, Config.PhotoSizeSettings.LARGESIZE, myFile.ContentLength, CurrentUser.SysID, ESP.Configuration.ConfigurationManager.SafeAppSettings["ProductImagePath"]).filename;
                    ImageManager.Add(imagemodel);

                }
            }
        }

    }

    private SupplierInfo getModel(SupplierInfo model)
    {
        #region 基础信息
        model.supplier_name = txtSupplierName.Text.Trim();
        model.supplier_area = txtsupplier_area1.Text.Trim();
        model.supplier_industry = txtsupplier_industry.Text.Trim();
        model.supplier_scale = txtsupplier_scale.Text.Trim();
        model.supplier_principal = txtsupplier_principal.Text.Trim();
        model.supplier_builttime = txtsupplier_builttime.Text.Trim();
        model.supplier_website = txtsupplier_website.Text.Trim();
        model.supplier_source = txtsupplier_source.Text != "" ? txtsupplier_source.Text.Trim() : "协议供应商";
        model.supplier_frameNO = txtfa_no.Text.Trim();
        #endregion

        #region 联系信息
        model.contact_name = txtLinker.Text.Trim();
        model.contact_mobile = txtCellPhone.Text.Trim();
        string supplier_con = txtsupplier_con.Text.Trim();
        string supplier_area = txtsupplier_area.Text.Trim();
        if (!string.IsNullOrEmpty(txtsupplier_phone.Text))
        {
            if (string.IsNullOrEmpty(supplier_con))
                supplier_con = "86";
            if (string.IsNullOrEmpty(supplier_area))
                supplier_area = "010";
        }

        string supplierfax_con = txtsupplierfax_con.Text.Trim();
        string supplierfax_area = txtsupplierfax_area.Text.Trim();
        if (!string.IsNullOrEmpty(txtsupplierfax_phone.Text))
        {
            if (string.IsNullOrEmpty(supplierfax_con))
                supplierfax_con = "86";
            if (string.IsNullOrEmpty(supplierfax_area))
                supplierfax_area = "010";
        }
        model.contact_tel = supplier_con + "-" + supplier_area + "-" + txtsupplier_phone.Text.Trim() + "-" + txtsupplier_ext.Text.Trim();
        model.contact_fax = supplierfax_con + "-" + supplierfax_area + "-" + txtsupplierfax_phone.Text.Trim() + "-" + txtsupplierfax_ext.Text.Trim();
        model.contact_email = txtEmail.Text.Trim();
        model.contact_address = txtAddress.Text.Trim();
        #endregion

        #region 产品服务信息
        model.service_content = txtservice_content.Text.Trim().Length > 1000 ? txtservice_content.Text.Trim().Substring(0, 1000) : txtservice_content.Text.Trim();
        model.service_area = txtservice_area.Text.Trim().Length > 500 ? txtservice_area.Text.Trim().Substring(0, 500) : txtservice_area.Text.Trim();
        model.service_workamount = txtservice_workamount.Text.Trim().Length > 500 ? txtservice_workamount.Text.Trim().Substring(0, 500) : txtservice_workamount.Text.Trim();
        model.service_customization = txtservice_customization.Text.Trim().Length > 500 ? txtservice_customization.Text.Trim().Substring(0, 500) : txtservice_customization.Text.Trim();
        model.service_ohter = txtservice_ohter.Text.Trim().Length > 2000 ? txtservice_ohter.Text.Trim().Substring(0, 2000) : txtservice_ohter.Text.Trim();
        model.service_forshunya = txtservice_forshunya.Text.Trim().Length > 2000 ? txtservice_forshunya.Text.Trim().Substring(0, 2000) : txtservice_forshunya.Text.Trim();
        #endregion

        #region 商务条款
        if (chkPrice.Checked)
            model.business_price = "";
        if (filbusiness_price.PostedFile.FileName != "")
        {
            string fileName = (model.business_price == "" || model.business_price == null) ? ("price_" + model.id + "_" + DateTime.Now.Ticks.ToString()) : model.business_price.Split('\\')[1].ToString().Split('.')[0].ToString();
            model.business_price = FileHelper.upFile(fileName, ESP.Purchase.Common.ServiceURL.UpFilePath, filbusiness_price);
        }
        model.business_paytime = txtbusiness_paytime.Text.Trim();
        model.business_prepay = txtbusiness_prepay.Text.Trim();
        #endregion

        #region 评估信息
        model.evaluation_department = txtevaluation_department.Text.Trim().Length > 500 ? txtevaluation_department.Text.Trim().Substring(0, 500) : txtevaluation_department.Text.Trim();
        model.evaluation_level = txtevaluation_level.Text.Trim().Length > 500 ? txtevaluation_level.Text.Trim().Substring(0, 500) : txtevaluation_level.Text.Trim();
        model.evaluation_feedback = txtevaluation_feedback.Text.Trim().Length > 2000 ? txtevaluation_feedback.Text.Trim().Substring(0, 2000) : txtevaluation_feedback.Text.Trim();
        model.evaluation_note = txtevaluation_note.Text.Trim().Length > 2000 ? txtevaluation_note.Text.Trim().Substring(0, 2000) : txtevaluation_note.Text.Trim();
        #endregion

        #region 帐户信息
        model.account_name = txtaccount_name.Text.Trim();
        model.account_bank = txtaccount_bank.Text.Trim();
        model.account_number = txtaccount_number.Text.Trim();
        #endregion

        model.supplier_type = (int)State.supplier_type.recommend;
        
        return model;
    }

    private void BindInfo()
    {
        if (psupplierId == 0) return;
        SupplierInfo model = SupplierManager.GetModel(psupplierId);

        #region 基础信息
        txtSupplierName.Text = model.supplier_name;
        txtsupplier_area1.Text = model.supplier_area;
        txtsupplier_industry.Text = model.supplier_industry;
        txtsupplier_scale.Text = model.supplier_scale;
        txtsupplier_principal.Text = model.supplier_principal;
        txtsupplier_builttime.Text = model.supplier_builttime;
        txtsupplier_website.Text = model.supplier_website;
        txtsupplier_source.Text = model.supplier_source;
        txtfa_no.Text = model.supplier_frameNO;
        #endregion

        #region 联系信息
        txtLinker.Text = model.contact_name;
        txtCellPhone.Text = model.contact_mobile;
        if (model.contact_tel.Split('-').Length == 4)
        {
            txtsupplier_con.Text = model.contact_tel.Split('-')[0];
            txtsupplier_area.Text = model.contact_tel.Split('-')[1];
            txtsupplier_phone.Text = model.contact_tel.Split('-')[2];
            txtsupplier_ext.Text = model.contact_tel.Split('-')[3];
        }
        if (model.contact_fax.Split('-').Length == 4)
        {
            txtsupplierfax_con.Text = model.contact_fax.Split('-')[0];
            txtsupplierfax_area.Text = model.contact_fax.Split('-')[1];
            txtsupplierfax_phone.Text = model.contact_fax.Split('-')[2];
            txtsupplierfax_ext.Text = model.contact_fax.Split('-')[3];
        }
        txtEmail.Text = model.contact_email;
        txtAddress.Text = model.contact_address;
        #endregion

        #region 产品服务信息
        txtservice_content.Text = model.service_content;
        txtservice_area.Text = model.service_area;
        txtservice_workamount.Text = model.service_workamount;
        txtservice_customization.Text = model.service_customization;
        txtservice_ohter.Text = model.service_ohter;
        txtservice_forshunya.Text = model.service_forshunya;
        #endregion

        #region 商务条款
        txtbusiness_paytime.Text = model.business_paytime;
        txtbusiness_prepay.Text = model.business_prepay;
        if (model.business_price.Trim() != "")
        {
            labdowPrice.Text = model.business_price.Trim() == "" ? "" : "<a target='_blank' href='../../" + model.business_price + "'><img src='/images/ico_04.gif' border='0' /></a>";
            chkPrice.Visible = true;
        }
        #endregion

        #region 评估信息
        txtevaluation_department.Text = model.evaluation_department;
        txtevaluation_level.Text = model.evaluation_level;
        txtevaluation_feedback.Text = model.evaluation_feedback;
        txtevaluation_note.Text = model.evaluation_note;
        #endregion

        #region 帐户信息
        txtaccount_name.Text = model.account_name;
        txtaccount_bank.Text = model.account_bank;
        txtaccount_number.Text = model.account_number;
        #endregion


    }
    protected void lnkaddP_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductEdit.aspx?backUrl=ProposedSupplierEdit.aspx&sid=" + psupplierId.ToString());
    }


    private void ListBind()
    {
        List<ProductInfo> list = ProductManager.getListBySupplierId(psupplierId, "");
        gvProduct.DataSource = list;
        gvProduct.DataBind();

        List<ImageInfo> lsImage = ImageManager.GetListBySupplierID(psupplierId);
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

    protected void gvProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = int.Parse(gvProduct.DataKeys[e.RowIndex].Value.ToString());
        ProductManager.Delete(id);
        //记录操作日志
        ESP.Logging.Logger.Add(string.Format("{0}对T_Product表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, id, "删除目录物品"), "目录物品");
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

    /// <summary>
    /// 批量屏蔽
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDis_Click(object sender, EventArgs e)
    {
        if (ProductManager.DisabledData(Request["chkItem"]) > 0)
        {
            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对T_Product表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, "(" + Request["chkItem"] + ")", "批量屏蔽目录物品"), "目录物品");
            ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('批量屏蔽成功！');", true);
            ListBind();
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('批量屏蔽失败！');", true);
        }
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDel_Click(object sender, EventArgs e)
    {
        if (ProductManager.Delete(Request["chkItem"]) > 0)
        {
            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对T_Product表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, "(" + Request["chkItem"] + ")", "批量删除目录物品"), "目录物品");
            ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('批量删除成功！');", true);
            ListBind();
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('批量删除失败！');", true);
        }
    }
}
