using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Entity;
using ESP.Purchase.Common;

public partial class UserControls_View_paymentInfo : System.Web.UI.UserControl
{
    private decimal dynamicPercent = 0;
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

    private decimal totalPrice;
    public decimal TotalPrice
    {
        get { return totalPrice; }
        set { totalPrice = value; hidTotalPrice.Value = totalPrice.ToString(); }
    }

    string pageName = "";
    decimal paymentPrice = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ListBind();
        pageName = Request.Path.Split('/')[3];
    }

    private void ListBind()
    {
        usedPrice = 0;
        if (model != null)
        {
            this.lblPeriodType.Text = State.PeriodType4CreatePNDesc[model.PeriodType];
        }
        DataTable dt = PaymentPeriodManager.GetPaymentPeriodList(int.Parse(Request[RequestName.GeneralID]));
        gvPayment.DataSource = dt;
        gvPayment.DataBind();
    }

    public void BindInfo()
    {
    }

    int num = 1;
    public static decimal usedPrice = 0;
    protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dv = (DataRowView)e.Row.DataItem;

            Literal litNo = (Literal)e.Row.FindControl("litNo");
            litNo.Text = num.ToString();
            num++;

            Literal LitOverplusPrice = (Literal)e.Row.FindControl("LitOverplusPrice");
            decimal totalprice = decimal.Parse(hidTotalPrice.Value);
            dynamicPercent += decimal.Parse(dv["expectPaymentPercent"].ToString());
            usedPrice += decimal.Parse(dv["expectpaymentprice"].ToString());
            LitOverplusPrice.Text = (totalprice - usedPrice).ToString("#,##0.####"); 
        }
    }

    public string GetExpectPaymentPrice(decimal expectPaymentPrice)
    {
        return expectPaymentPrice.ToString("#,##0.00");
    }
}
