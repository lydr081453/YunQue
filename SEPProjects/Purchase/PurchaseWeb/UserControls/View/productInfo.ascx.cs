using System;
using System.Data;
using System.Web.UI.WebControls;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Entity;
using ESP.Purchase.Common;
using System.Collections.Generic;

public partial class UserControls_View_productInfo : System.Web.UI.UserControl
{
    private GeneralInfo model;
    public GeneralInfo Model
    {
        set { model = value; }
        get { return model; }
    }

    public int CurrentUserId { get; set; }

    private decimal totalPrice;
    public decimal TotalPrice
    {
        get { return totalPrice; }
        set { totalPrice = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void ItemListBindData(string term)
    {
        IList<OrderInfo> ds = OrderInfoManager.GetInfoList(term);

        gdvItem.DataSource = ds;
        gdvItem.DataBind();
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
            }
            labTotalPrice.Text = totalprice.ToString("#,##0.####");
            totalPrice = decimal.Parse(totalprice.ToString("#,##0.####"));
        }

    }

    int num = 1;
    protected void gdvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            OrderInfo dr = (OrderInfo)e.Row.DataItem;

            ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(Convert.ToInt32(Request[RequestName.GeneralID]));

            ESP.Framework.Entity.OperationAuditManageInfo operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(generalModel.Departmentid);

            ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(CurrentUserId);
            string mobile = emp.MobilePhone.Replace("-", "").Trim();

            Label lblItemNo = (Label)e.Row.FindControl("lblItemNo");
            Label lblDesctiprtion = (Label)e.Row.FindControl("lblDesctiprtion");
            Label lblType = (Label)e.Row.FindControl("lblType");
            Label labDown = (Label)e.Row.FindControl("labDown");
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");

            TypeInfo ty = TypeManager.GetModel(dr.producttype);

            lblDesctiprtion.Text = dr.desctiprtion;
            txtthirdParty_materielDesc.Text = generalModel.thirdParty_materielDesc;

            lblItemNo.Text = dr.Item_No;
            lblType.Text = ty.typename;


            e.Row.Cells[0].Text = num.ToString();
            num++;
            string[] links = dr.upfile.TrimEnd('#').Split('#');
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

            if (dr.total == 0)
            {
                btnDelete.Visible = true;
            }



        }
    }

    public void BindInfo()
    {
        ItemListBindData(" general_id = " + model.id);
        //框架合同列表
        if (!string.IsNullOrEmpty(model.FCPrIds.Trim(',')))
        {
            DataTable FCList = GeneralInfoManager.GetFCPrList(null, model.FCPrIds.Trim(','));
            GvFCPr.DataSource = FCList;
            GvFCPr.DataBind();
            FCTable.Visible = FCList.Rows.Count > 0;
        }
        // labNote.Text = Model.sow3.Trim();
        // txtthirdParty_materielDesc.Text = Model.thirdParty_materielDesc;
        txtbuggeted.Text = Model.buggeted.ToString("#,##0.00");
        labforegift.Text = Model.Foregift.ToString("#,##0.#####");
        if (Model.PRType != (int)ESP.Purchase.Common.PRTYpe.MediaPR && Model.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA && Model.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_PriFA)
        {
            if (Model.ValueLevel == 1)
                lblValueLevel.Text = "5000元(含)以下，不需要采购部审批！";
            else
                lblValueLevel.Text = "<font color='red'>5000元以上，需要采购部审批！";
        }
        labInvoiceType.Text = model.InvoiceType;
        labTaxRate.Text = model.TaxRate == null ? "" : model.TaxRate.ToString();

        if (Model.PRType == 6 && Model.HaveInvoice == false)
        {
            double totalTax = 0;
            double total = 0;
            List<PaymentPeriodInfo> periodlist = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetModelList(" gid=" + Model.id);
            if (periodlist != null && periodlist.Count > 0)
            {
                foreach (PaymentPeriodInfo p in periodlist)
                {
                    if (p.TaxTypes == 2)
                    {
                        total += double.Parse(p.expectPaymentPrice.ToString());
                    }
                    else
                    {
                        total += double.Parse(p.expectPaymentPrice.ToString()) - ESP.Purchase.BusinessLogic.OrderInfoManager.TaxCalculator(Convert.ToDouble(p.expectPaymentPrice), 1);
                    }
                    totalTax += ESP.Purchase.BusinessLogic.OrderInfoManager.TaxCalculator(Convert.ToDouble(p.expectPaymentPrice), 1);

                }
                txtMsg.Text = "该PR单类型为对私无发票, 税后支付总额为:" + total.ToString("#,##.00") + "; 税金" + totalTax.ToString("#,##.00");
            }
        }

    }
    
    protected void gdvItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        OrderInfoManager.Delete(int.Parse(gdvItem.DataKeys[e.RowIndex].Values[0].ToString()), CurrentUserId, CurrentUserId.ToString(), model);

        ItemListBindData(" general_id = " + model.id);

    }

    public void ValidProfit(ESP.Finance.Entity.ProjectInfo projectModel)
    {
    }
    public decimal getTotalPrice()
    {
        return decimal.Parse(labTotalPrice.Text);
    }

    public int getItemRowCount()
    {
        return gdvItem.Rows.Count;
    }
}
