using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Threading;
using System.Collections.Generic;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using System.IO;

public partial class UserControls_Edit_productInfo : System.Web.UI.UserControl
{
    int generalid = 0;
    string pageName = "";
    private GeneralInfo model;
    public GeneralInfo Model
    {
        set { model = value; }
        get { return model; }
    }

    private ESP.Compatible.Employee currentUser;
    public ESP.Compatible.Employee CurrentUser
    {
        get { return currentUser; }
        set { currentUser = value; }
    }

    private bool hideDelCommand = false;
    public bool HideDelCommand
    {
        get { return hideDelCommand; }
        set { hideDelCommand = value; }
    }

    private bool hideAllCommand = false;
    public bool HideAllCommand
    {
        get { return hideAllCommand; }
        set { hideAllCommand = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        generalid = int.Parse(Request[RequestName.GeneralID]);
        pageName = Request.Path.Split('/')[3];
        this.ddlValueLevel.Attributes.Add("onchange ", "changeColor()"); 
        ddlValueLevel.SelectedIndex = 2;
    }

    /// <summary>
    /// Binds the info.
    /// </summary>
    public void BindInfo()
    {
        txtNote.Text = Model.sow3.Trim();
        txtThirdParty.Text = Model.thirdParty_materielDesc;
        hidtypeIds.Value = Model.thirdParty_materielID;
        txtbugget.Text = Model.buggeted.ToString("#,##0.00");
        txtforegift.Text = model.Foregift.ToString("#,##0.####");
        ddlInvoiceType.SelectedValue = model.InvoiceType;
        if (model.TaxRate != null)
            ddlTaxRate.SelectedValue = model.TaxRate.Value.ToString();
        string str = string.Empty;
        string str2 = string.Empty;

        IList<ESP.Purchase.Entity.RiskConsultationInfo> risk = ESP.Purchase.BusinessLogic.RiskConsultationManager.GetList(model.id);
        if (risk != null && risk.Count > 0)
        {
            if (risk[0].Total <= 11)
            {
                str = "<img src='/images/re-07.jpg' width='211' height='23' />";
            }
            else if (risk[0].Total >= 17)
            {
                str = "<img src='/images/re-09.jpg' width='211' height='23' />"; ;
                str2 = "<img src='/images/re-10.jpg' width='14' height='12' / style='margin:0 5px -2px 20px;'>建议您向采购部咨询";
            }
            else 
            {
                str = "<img src='/images/re-08.jpg' width='211' height='23' />";
            }
            this.lblResultTitle.Text = "风险评估结果为：";
            this.lblResult.Text = str;
            this.lblResult2.Text = str2;
        }
        this.ddlValueLevel.SelectedValue = model.ValueLevel.ToString();

        if (ESP.Purchase.BusinessLogic.OrderInfoManager.getTotalPriceByGID(model.id) > 5000 || model.PRType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA || model.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA)
        {
            this.ddlValueLevel.SelectedValue = "2";
            this.ddlValueLevel.Enabled = false;
            this.tb5000.Style["display"] = "none";
            this.lblResultTitle.Visible = false;
            this.lblResult.Visible = false;
            this.lblResult2.Visible = false;
        }

        if (Model.Project_id == 0)
            btnSel.Visible = false;

        hidDepartmentId.Value = Model.Departmentid.ToString();
        hidProjectId.Value = Model.Project_id.ToString();
        if (Model.Project_id > 0)
        {
            txtThirdParty.Attributes["onfocus"] = "javascript:this.blur();";
        }

        if ((Model.status == State.requisition_save || Model.status == State.requisition_return) && Model.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA || Model.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA)
        {
            palSup.Visible = true;
            List<SupplierInfo> suplierList = ESP.Purchase.BusinessLogic.SupplierManager.getModelList(" and payment_tax is not null and payment_tax <> 0", new List<System.Data.SqlClient.SqlParameter>());
            ddlSup.DataSource = suplierList;
            ddlSup.DataTextField = "supplier_name";
            ddlSup.DataValueField = "id";
            ddlSup.DataBind();

            OrderInfo orderModel = ESP.Purchase.BusinessLogic.OrderInfoManager.GetModelByGeneralID(Model.id);
            if (orderModel != null)
                ddlSup.SelectedValue = orderModel.supplierId.ToString();
            else
                ddlSup.Items.Insert(0, new ListItem("请选择...", "0"));


        }

        if (Model.PRType == 6 && Model.HaveInvoice == false)
        {
            double totalTax = 0;
            List<PaymentPeriodInfo> periodlist = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetModelList(" gid=" + Model.id);
            if (periodlist != null && periodlist.Count > 0)
            {
                foreach (PaymentPeriodInfo p in periodlist)
                {
                    if (p.TaxTypes == 2)
                    {
                        totalTax += ESP.Purchase.BusinessLogic.OrderInfoManager.TaxCalculator(Convert.ToDouble(p.expectPaymentPrice), 1);
                    }
                }
                txtMsg.Text = "该PR单类型为对私无发票, 税前支付总额为:" + Model.totalprice.ToString("#,##.00") + "; 税金" + totalTax.ToString("#,##.00");
            }
        }

    }

    public void ValidProfit(ESP.Finance.Entity.ProjectInfo projectModel)
    {
        //40%
        //decimal taxfee = 0;
        //decimal servicefee = 0;
        //decimal profilerate = 0;
        //if (projectModel.ContractTax != null)
        //{
        //    ESP.Finance.Entity.TaxRateInfo rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(projectModel.ContractTaxID.Value);

        //    taxfee = ESP.Finance.BusinessLogic.CheckerManager.GetTax(projectModel, rateModel);

        //    servicefee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFee(projectModel, rateModel);
        //}
        
        //if (projectModel.TotalAmount > 0)
        //{
        //    profilerate = (servicefee / Convert.ToDecimal(projectModel.TotalAmount) * 100);
        //}
        //if (profilerate < 40 && projectModel.ContractStatusName != ESP.Finance.Utility.ProjectType.BDProject)
        //{
        //    this.tbNotify.Visible = true;
        //}
        //else
        //{
        //    this.tbNotify.Visible = false;
        //}
    }
  

    /// <summary>
    /// Sets the model info.
    /// </summary>
    /// <returns></returns>
    public GeneralInfo setModelInfo()
    {
        string strvalue = "";
        if (txtNote.Text.ToString().Length > 81)
        {
            strvalue = txtNote.Text.ToString().Substring(0, 80);
            Model.sow3 = strvalue;
        }
        else
        {
            Model.sow3 = txtNote.Text.ToString();
        }
        if (!string.IsNullOrEmpty(Request["chkcontrastFile"]))
        {
            string[] contrastFiles = Request["chkcontrastFile"].Split(',');
            foreach (string filePath in contrastFiles)
            {
                Model.contrastUpFiles = Model.contrastUpFiles.Replace(filePath + "#", "");
            }
        }
        Model.thirdParty_materielDesc = txtThirdParty.Text.Trim();
        Model.thirdParty_materielID = hidtypeIds.Value;
        Model.buggeted = decimal.Parse(txtbugget.Text.Trim());
        Model.Foregift = decimal.Parse(txtforegift.Text.Trim());
        model.ValueLevel = int.Parse(ddlValueLevel.SelectedValue);
        model.InvoiceType = ddlInvoiceType.SelectedValue;
        model.TaxRate = int.Parse(ddlTaxRate.SelectedValue);
        model.FCPrIds = hidFCPrIds.Value;
        if (model.ValueLevel == 0)
        {
            this.tb5000.Style["background-color"] = "Red";
        }
        for (int i = 0; i < Request.Files.Count; i++)
        {
            if (Request.Files.Keys[i] == "filBJ" && Request.Files[i].FileName != "")
            {
                HttpPostedFile postFile = Request.Files[i];
                string fileName = "contrast_" + Model.id + "_" + DateTime.Now.Ticks.ToString();
                Model.contrastUpFiles += FileHelper.upFile(fileName,ESP.Purchase.Common.ServiceURL.UpFilePath, postFile) + "#";
            }
            Thread.Sleep(100);
        }
        if (Model.OperationType == 1 && Model.HaveInvoice == false)
        {
            Model.PRType = 6;
        }
        return Model;
    }

    int num = 1;
    /// <summary>
    /// Handles the RowDataBound event of the gdvItem control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
    protected void gdvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            OrderInfo ordermodel = OrderInfoManager.GetModel(Convert.ToInt32(Request["orderid"]));
            OrderInfo dr = (OrderInfo)e.Row.DataItem;

            Label labDown = (Label)e.Row.FindControl("labDown");

            if (ordermodel == null)
            {
                ordermodel = e.Row.DataItem as OrderInfo;
            }

            e.Row.Cells[0].Text = num.ToString();
            num++;
            if (!string.IsNullOrEmpty(ordermodel.upfile))
            {
                string[] links = ordermodel.upfile.Split('#');
                labDown.Text = "";
                for (int i = 0; i < links.Length; i++)
                {
                    if (links[i].Trim() != "")
                    {
                        if (links[i].Trim().IndexOf(".aspx") > 0)
                            labDown.Text += "<a target='_blank' href='/" + links[i].Trim() + "'><img src='/images/ico_04.gif' border='0' /></a>&nbsp;";
                        else
                            labDown.Text += "<a target='_blank' href='/Purchase/Requisition/UpfileDownload.aspx?OrderId=" + dr.id.ToString() + "&Index=" + i.ToString() + "'><img src='/images/ico_04.gif' border='0' /></a>&nbsp;";
                    }
                }
            }
          
            int productTypeID = dr.producttype;
            TypeInfo ty = TypeManager.GetModel(productTypeID);

            e.Row.Cells[2].Text = ty == null ? "" : TypeManager.GetModel(productTypeID).typename;
        }
    }

    /// <summary>
    /// Handles the Click event of the btnAddItem control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnAddItem_Click(object sender, EventArgs e)
    {
        if (model == null)
            model = GeneralInfoManager.GetModel(Convert.ToInt32(Request[RequestName.GeneralID]));

        if (model.Project_id!=0 && string.IsNullOrEmpty(hidtypeIds.Value))
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请先选择采购物料并确认采购预算！');", true);
            return;
        }

        List<OrderInfo> orderList = OrderInfoManager.GetOrderList(" and a.general_id=" + model.id.ToString());

        model.thirdParty_materielDesc = txtThirdParty.Text.Trim();
        model.thirdParty_materielID = hidtypeIds.Value;
        model.buggeted = decimal.Parse(txtbugget.Text.Trim());
        model.Foregift = string.IsNullOrEmpty(txtforegift.Text.Trim()) ? 0 : decimal.Parse(txtforegift.Text.Trim());
        model.InvoiceType = ddlInvoiceType.SelectedValue;
        model.TaxRate = int.Parse(ddlTaxRate.SelectedValue);
        model.FCPrIds = hidFCPrIds.Value;
        GeneralInfoManager.Update(model);

        if (model.PRType == (int)PRTYpe.MediaPR)
        {
           // List<OrderInfo> orderList = OrderInfoManager.GetOrderList(" and a.general_id=" + model.id.ToString());
            string OrderID = string.Empty;
            if (orderList != null && orderList.Count > 0)
                OrderID = orderList[0].id.ToString();
            Response.Redirect("WrittingFeeApplicant.aspx?" + RequestName.GeneralID + "=" + Request[RequestName.GeneralID] + "&orderid=" + OrderID + "&pageUrl=" + pageName);
        }

        else if (model.PRType == (int)PRTYpe.PR_OtherMedia)
        {
          //  List<OrderInfo> orderList = OrderInfoManager.GetOrderList(" and a.general_id=" + model.id.ToString());
            string OrderID = string.Empty;
            if (orderList != null && orderList.Count > 0)
                OrderID = orderList[0].id.ToString();
            Response.Redirect("SupplierCollaboration/OtherMediaOrder.aspx?" + RequestName.GeneralID + "=" + generalid + "&orderid=" + OrderID + "&pageUrl=" + this.pageName);
        }
        else if (model.PRType == (int)PRTYpe.PR_OtherAdvertisement)
        {
           // List<OrderInfo> orderList = OrderInfoManager.GetOrderList(" and a.general_id=" + model.id.ToString());
            string OrderID = string.Empty;
            if (orderList != null && orderList.Count > 0)
                OrderID = orderList[0].id.ToString();
            Response.Redirect("Advertisement/AdvertisementOrder.aspx?" + RequestName.GeneralID + "=" + generalid + "&orderid=" + OrderID + "&pageUrl=" + this.pageName);
        }
        else
        {
            Response.Redirect("newEditProducts.aspx?" + RequestName.GeneralID + "=" + Request[RequestName.GeneralID] + "&pageUrl=" + pageName);
            
  
        }
    }

    /// <summary>
    /// Handles the RowDeleting event of the gdvItem control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewDeleteEventArgs"/> instance containing the event data.</param>
    protected void gdvItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GeneralInfo g = GeneralInfoManager.GetModel(generalid);
        OrderInfo order = OrderInfoManager.GetModel(int.Parse(gdvItem.DataKeys[e.RowIndex].Values[0].ToString()));
        OrderInfoManager.Delete(int.Parse(gdvItem.DataKeys[e.RowIndex].Values[0].ToString()), int.Parse(CurrentUser.SysID), CurrentUser.Name, g);

        ItemListBind(" general_id = " + generalid);
    }

    /// <summary>
    /// Handles the RowEditing event of the gdvItem control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewEditEventArgs"/> instance containing the event data.</param>
    protected void gdvItem_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Response.Redirect("ModifyProducts.aspx?" + RequestName.GeneralID + "=" + generalid + "&orderid=" + gdvItem.DataKeys[e.NewEditIndex].Value.ToString() + "&pageUrl=" + pageName);
    }

    /// <summary>
    /// Items the list bind.
    /// </summary>
    /// <param name="term">The term.</param>
    public void ItemListBind(string term)
    {
        IList<OrderInfo> ds = OrderInfoManager.GetInfoList(term);

        //IList<OrderInfo> newDS = new List<OrderInfo>();

        //foreach (OrderInfo info in ds)
        //{
           // TypeInfo type = TypeManager.GetModel(info.producttype);
        if (ds != null && ds.Count > 0)
        {
            if (model == null)
                model = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(ds[0].general_id);

            //框架合同列表
            hidFCPrIds.Value = model.FCPrIds;
            DataTable FCList = GeneralInfoManager.GetFCPrList(model,"");
            GvFCPr.DataSource = FCList;
            GvFCPr.DataBind();
            FCTable.Visible = FCList.Rows.Count > 0;
        }
          //      newDS.Add(info);
        //}

        gdvItem.DataSource = ds;
        gdvItem.DataBind();
        if (ds.Count > 0)
        {
            btnSel.Disabled = true;
        }
        else
        {
            btnSel.Disabled = false;
        }
        DgRecordCount.Text = ds.Count.ToString();
        if (ds.Count == 0)
        {
            labTotalPrice.Text = "0";
        }
        else
        {
            decimal totalprice = 0;
            for (int i = 0; i < ds.Count; i++)
            {
                totalprice += decimal.Parse(ds[i].total.ToString());
                labTotalPrice.Text = totalprice.ToString("#,##0.####");
            }
        }

        if (HideDelCommand)
            gdvItem.Columns[11].Visible = false;
        if (HideAllCommand)
        {
            btnAddItem.Visible = false;
            gdvItem.Columns[10].Visible = false;
            gdvItem.Columns[11].Visible = false;
        }

    }

    /// <summary>
    /// Gets the total price.
    /// </summary>
    /// <returns></returns>
    public decimal getTotalPrice()
    {
        return decimal.Parse(labTotalPrice.Text);
    }

    /// <summary>
    /// Gets the item row count.
    /// </summary>
    /// <returns></returns>
    public int getItemRowCount()
    {
        return gdvItem.Rows.Count;
    }

    protected void ddlSup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSup.SelectedValue != "0")
        {
            if (!ESP.Purchase.BusinessLogic.MediaOrderManager.ChangedSupplier(int.Parse(ddlSup.SelectedValue), generalid))
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更供应商失败！');", true);
            else
                Response.Redirect(Request.RawUrl);
        }
    }
}
