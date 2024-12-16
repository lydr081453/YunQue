using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_PaymentPeriodList : ESP.Web.UI.PageBase
{
    string recipientIds = "";
    decimal dynamicPercent = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["recipientIds"]))
            recipientIds = Request["recipientIds"];
        ListBind();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
    }

    private void ListBind()
    {
        DataTable dt = PaymentPeriodManager.GetPaymentPeriodAllList(int.Parse(Request[RequestName.GeneralID]));
        gvPayment.DataSource = dt;
        gvPayment.DataBind();
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        int paymentPeriodId = int.Parse(Request["radPeriod"]);
        PaymentPeriodInfo paymentPeriod = PaymentPeriodManager.GetModel(paymentPeriodId);

        //收货单列表
        List<RecipientInfo> recipientList = RecipientManager.getModelList(" and id in (" + recipientIds + ")", new List<SqlParameter>());
        decimal recipientPrice = 0; //收货金额
        foreach(RecipientInfo model in recipientList){
            recipientPrice += model.RecipientAmount;
        }
        paymentPeriod.inceptDate = DateTime.Now;
        paymentPeriod.Status = State.PaymentStatus_commit;

        ////金额检查
        if (gvPayment.Rows.Count == 1 && recipientPrice > paymentPeriod.expectPaymentPrice)//如果所选收货账期为最后一个收货账期，收货金额不能大于账期金额
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('收货金额不能大于账期金额！');", true);
        }
        if (recipientPrice > paymentPeriod.expectPaymentPrice) //如果所选收货金额大于账期金额，只收账期金额
        {
            paymentPeriod.inceptPrice = paymentPeriod.expectPaymentPrice;

        }
        else //如果所选收货金额小于账期金额，只收收货金额
        {
            paymentPeriod.inceptPrice = recipientPrice;
        }

        //更新付款账期和收货单
        if (PaymentPeriodManager.UpdatePaymentPeriodAndRecipient(paymentPeriod, recipientIds))
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "opener.location.reload();alert('支付成功！此次支付金额为：" + paymentPeriod.inceptPrice.ToString("#,##0.####") + "');window.close();", true);
        else
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('支付失败！');", true);
    }

    protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dv = (DataRowView)e.Row.DataItem;

            Literal LitOverplusPrice = (Literal)e.Row.FindControl("LitOverplusPrice");
            dynamicPercent += decimal.Parse(dv["expectPaymentPercent"].ToString());
        }
    }
    public string GetExpectPaymentPrice(decimal expectPaymentPrice)
    {
        return expectPaymentPrice.ToString("#,##0.00");
    }
}
