using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.Common;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Entity;
using System.Data;

public partial class Purchase_Requisition_EditPayment : ESP.Web.UI.PageBase
{

    int paymentId = 0;
    int gid = 0;
    private string clientId = "ctl00_ContentPlaceHolder1_paymentInfo_";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["paymentId"]))
            paymentId = int.Parse(Request["paymentId"]);
        if (!string.IsNullOrEmpty(Request[ESP.Purchase.Common.RequestName.GeneralID]))
            gid = int.Parse(Request[ESP.Purchase.Common.RequestName.GeneralID]);
        if (!IsPostBack)
        {
            InitInfo();
        }
    }


    private void InitInfo()
    {
        DropDownListBind();
        GeneralInfo general = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(gid);
        
        OrderInfo orderModel = ESP.Purchase.BusinessLogic.OrderInfoManager.GetModelByGeneralID(gid);
        if (general.PRType==6 && general.HaveInvoice==false)
        {
            trTax.Style["display"] = "block";
        }

        PaymentPeriodInfo model = PaymentPeriodManager.GetModel(paymentId);
        if (model != null)
        {
            btnNext.Visible = false;
            drpDateType.SelectedValue = model.dateType.ToString();
            drpPeriodDatumPoint.SelectedValue = model.periodDatumPoint.ToString();
            drpPeriodType.SelectedValue = model.periodType.ToString();
            txtPeriodDay.Text = model.periodDay.ToString();
            txtBegin.Text = model.beginDate.ToString("yyyy-MM-dd");
            txtExpectPaymentPercent.Text = model.expectPaymentPercent.ToString();
            txtExpectPaymentPrice.Text = model.expectPaymentPrice.ToString("#,##0.0000");
            hidPaymentPeriodId.Value = model.id.ToString();
            txtPeriodRemark.Text = model.periodRemark;

            if (general.PRType == 6 && general.HaveInvoice == false)
            {
                rdList.SelectedValue = model.TaxTypes.ToString();
            }
        }
        else
        {
            List<PaymentPeriodInfo> modelList = PaymentPeriodManager.GetModelList(" gid=" + gid);
            decimal SYPrice = getTotalPrice();
            decimal SYPercent = 100;
            foreach (PaymentPeriodInfo m in modelList)
            {
                SYPrice = SYPrice - m.expectPaymentPrice;
                SYPercent = SYPercent - m.expectPaymentPercent;
            }
            txtExpectPaymentPercent.Text = SYPercent.ToString();
            txtExpectPaymentPrice.Text = SYPrice.ToString("#,##0.####");


        }


    }

    public decimal getTotalPrice()
    {
        DataSet ds = OrderInfoManager.GetList(" general_id=" + gid);

        decimal totalprice = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            totalprice += decimal.Parse(ds.Tables[0].Rows[i]["total"].ToString());
        }
        return totalprice;
    }

    protected void drpPeriodType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpPeriodType.SelectedValue == "0")
        {
            drpPeriodDatumPoint.SelectedValue = "0";
        }
        if (drpPeriodType.SelectedValue == "1")
        {
            drpPeriodDatumPoint.SelectedValue = "1";
        }
    }

    private void DropDownListBind()
    {

        drpPeriodType.Items.Clear();
        for (int i = 0; i < State.Period_PeriodType.Length; i++)
        {
            drpPeriodType.Items.Insert(i, new ListItem(State.Period_PeriodType[i], i.ToString()));
        }
        drpPeriodType.DataBind();

        drpPeriodDatumPoint.Items.Clear();
        for (int i = 0; i < State.Period_PeriodDatumPoint.Length; i++)
        {
            drpPeriodDatumPoint.Items.Insert(i, new ListItem(State.Period_PeriodDatumPoint[i], i.ToString()));
        }
        drpPeriodDatumPoint.DataBind();

        drpDateType.Items.Clear();
        for (int i = 0; i < State.Period_DateType.Length; i++)
        {
            drpDateType.Items.Insert(i, new ListItem(State.Period_DateType[i], i.ToString()));
        }
        drpDateType.DataBind();
        drpDateType.SelectedValue = "1";
    }

    private void SaveModel()
    {
        GeneralInfo gmodel = GeneralInfoManager.GetModel(gid);
        PaymentPeriodInfo model = new PaymentPeriodInfo();
        OrderInfo orderModel = ESP.Purchase.BusinessLogic.OrderInfoManager.GetModelByGeneralID(gid);
        
        if (paymentId > 0)
            model = PaymentPeriodManager.GetModel(paymentId);
        model.gid = gid;
        model.periodType = int.Parse(drpPeriodType.SelectedValue);
        model.periodDatumPoint = int.Parse(drpPeriodDatumPoint.SelectedValue);
        model.dateType = int.Parse(drpDateType.SelectedValue);
        if (!string.IsNullOrEmpty(txtPeriodDay.Text.Trim()))
        {
            model.periodDay = txtPeriodDay.Text.Trim();
        }
        else
        {
            model.periodDay = "90";
        }
        if (txtBegin.Text.Trim() != "")
            model.beginDate = DateTime.Parse(txtBegin.Text.Trim());
        model.endDate = model.beginDate;
        model.periodRemark = txtPeriodRemark.Text.Trim();
        model.expectPaymentPercent = decimal.Parse(txtExpectPaymentPercent.Text.Trim());
        if (!string.IsNullOrEmpty(txtExpectPaymentPrice.Text.Trim()))
        {
            model.expectPaymentPrice = decimal.Parse(txtExpectPaymentPrice.Text.Trim());
        }
        model.Status = State.PaymentStatus_save;

        if (gmodel.PRType==6 && gmodel.HaveInvoice==false)//兼职类个人
        {
            if (rdList.SelectedItem == null)
            {
                txtMsg.Text = "请选择税前/税后";
                return;
            }
            model.TaxTypes = int.Parse(rdList.SelectedValue);
        }

      
        DateTime dt1 = new DateTime(model.beginDate.Year, model.beginDate.Month, 1);
        DateTime dt2 = dt1.AddMonths(1);

        List<PaymentPeriodInfo> paymentlist = null;
        if(model.id==0)
            paymentlist=ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetModelList(" gid=" + gmodel.id + " and (begindate between '" + dt1.ToString("yyyy-MM-dd") + "' and '" + dt2.ToString("yyyy-MM-dd") + "')");
        else
            paymentlist = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetModelList(" id!=" + model.id +" and gid=" + gmodel.id + " and (begindate between '" + dt1.ToString("yyyy-MM-dd") + "' and '" + dt2.ToString("yyyy-MM-dd") + "')");

        decimal totalOneMonth = 0;
        if (paymentlist != null && paymentlist.Count > 0)
        {
            foreach (PaymentPeriodInfo p in paymentlist)
            {
                totalOneMonth += p.expectPaymentPrice;
            }
        }
        if (gmodel.PRType == 6 && gmodel.HaveInvoice == false)//兼职类个人
        {
            if (model.TaxTypes == 2)//税后
            {
                model.expectPaymentPrice = decimal.Parse(OrderInfoManager.TaxCalculator(double.Parse(model.expectPaymentPrice.ToString()), 2).ToString());
                totalOneMonth += model.expectPaymentPrice;
            }
            else
            {
                totalOneMonth += model.expectPaymentPrice;
            }

            if (totalOneMonth >= 20000)
            {
                txtMsg.Text ="税前金额："+totalOneMonth.ToString()+ ". 一个月内预计支付金额不能超过20000元(含)";
                return;
            }

        }


        if (model.id > 0)
        {
            if (model.periodType == (int)State.PeriodType.prepay)
            {
                PaymentPeriodInfo tempModel = PaymentPeriodManager.GetModel(model.id);
                gmodel.sow4 = (decimal.Parse(gmodel.sow4) - tempModel.expectPaymentPrice + model.expectPaymentPrice).ToString();
            }
            PaymentPeriodManager.Update(model, gmodel);
        }
        else
        {
            if (model.periodType == (int)State.PeriodType.prepay)
            {
                gmodel.sow4 = (decimal.Parse(gmodel.sow4) + model.expectPaymentPrice).ToString();
            }
            PaymentPeriodManager.Add(model, gmodel);
        }
        Response.Write("<script>opener.__doPostBack('" + clientId.Replace('_', '$') + "btnBind','');window.close();</script>");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveModel();
        
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        SaveModel();
        //Response.Write("<script>opener.__doPostBack('" + clientId.Replace('_', '$') + "btnBind','');</script>");
        InitInfo();
    }
}