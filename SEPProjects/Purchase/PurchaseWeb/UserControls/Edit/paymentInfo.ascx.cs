using System;
using System.Web.UI.WebControls;
using System.Data;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

public partial class UserControls_Edit_paymentInfo : System.Web.UI.UserControl
{
    private decimal totalPercent = 0;
    private decimal dynamicPercent = 0;
    public decimal DynamicPercent
    {
        set { dynamicPercent = 0; }
    }

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

    private bool isShowCommand = true;
    /// <summary>
    /// 是否显示编辑、删除、添加
    /// </summary>
    public bool IsShowCommand
    {
        set { 
            isShowCommand = value;
        }
        get { return isShowCommand; }
    }

    private bool isShowGridView;
    /// <summary>
    /// 是否显示付款账期列表
    /// </summary>
    public bool IsShowGridView
    {
        set
        {
            isShowCommand = value;
        }
        get { return isShowGridView; }
    }

    string pageName = "";
    decimal paymentPrice = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        #region AjaxProRegister
        AjaxPro.Utility.RegisterTypeForAjax(typeof(PaymentPeriodManager));
        #endregion
        pageName = Request.Path.Split('/')[3];
        

        if (!IsPostBack)
        {
            ListBind(true);
            //DropDownListBind();
            if (!string.IsNullOrEmpty(Request["focus"]))
            {
                gvPayment.Focus();
            }

            //if (!string.IsNullOrEmpty(Request["show"]) && Request["show"].ToString() == "True")
            //{
            //    gvPayment.Focus();
            //    Show();
            //}
        }
    }

    //private void DropDownListBind()
    //{

    //    drpPeriodType.Items.Clear();
    //    for (int i = 0; i < State.Period_PeriodType.Length; i++)
    //    {
    //        drpPeriodType.Items.Insert(i, new ListItem(State.Period_PeriodType[i], i.ToString()));
    //    }
    //    drpPeriodType.DataBind();

    //    drpPeriodDatumPoint.Items.Clear();
    //    for (int i = 0; i < State.Period_PeriodDatumPoint.Length; i++)
    //    {
    //        drpPeriodDatumPoint.Items.Insert(i, new ListItem(State.Period_PeriodDatumPoint[i], i.ToString()));
    //    }
    //    drpPeriodDatumPoint.DataBind();

    //    drpDateType.Items.Clear();
    //    for (int i = 0; i < State.Period_DateType.Length; i++)
    //    { 
    //        drpDateType.Items.Insert(i,new ListItem(State.Period_DateType[i],i.ToString()));
    //    }
    //    drpDateType.DataBind();
    //    drpDateType.SelectedValue = "1";
    //}

    public decimal getTotalPrice(string term)
    {
        DataSet ds = OrderInfoManager.GetList(term);

        decimal totalprice = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            totalprice += decimal.Parse(ds.Tables[0].Rows[i]["total"].ToString());
        }
        return totalprice;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isLoad">是否是从Page_Load调用，是否计算剩余百分比</param>
    public void ListBind(bool isLoad)
    {
        GeneralInfo gmodel = GeneralInfoManager.GetModel(int.Parse(Request[RequestName.GeneralID]));
        num = 1;
        totalPercent = 0;
        usedPrice = 0;
        DataTable dt = PaymentPeriodManager.GetPaymentPeriodList(int.Parse(Request[RequestName.GeneralID]));
        /*if (!IsPostBack)此功能已在3000以上处理时操作
        {
            //如果为3000以上生成的单子，预计收货时间默认为当前日期
            if (dt.Rows.Count == 0 && getTotalPrice(" general_id = " + gmodel.id) > 0 && (gmodel.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA || gmodel.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA))
            {
                //自动添加付款帐期,标准条款,当前日期
                PaymentPeriodInfo paymentPeriod = new PaymentPeriodInfo();
                paymentPeriod.gid = gmodel.id;
                paymentPeriod.expectPaymentPrice = getTotalPrice(" general_id = " + gmodel.id);
                paymentPeriod.expectPaymentPercent = 100;
                paymentPeriod.periodDay = State.supplierpayment[0].ToString();
                paymentPeriod.beginDate = DateTime.Now;
                //paymentPeriod.endDate = DateTime.Parse(DateTime.Now.AddMonths(3 + 0).ToString("yyyy-MM") + "-01").AddDays(-1);
                paymentPeriod.periodType = (int)State.PeriodType.period;
                paymentPeriod.Status = State.PaymentStatus_save;
                PaymentPeriodManager.Add(paymentPeriod);
                dt = PaymentPeriodManager.GetPaymentPeriodList(int.Parse(Request[RequestName.GeneralID]));
            }
        }*/
        gvPayment.DataSource = dt;
        gvPayment.DataBind();
        foreach (DataRow dr in dt.Rows)
        {
            paymentPrice += decimal.Parse(dr["expectPaymentPrice"].ToString());
            totalPercent += decimal.Parse(dr["expectPaymentPercent"].ToString());
        }
        hitTotalPercent.Value = totalPercent.ToString();
        if (isLoad)
        {
            txtExpectPaymentPercent.Text = (100 - totalPercent).ToString();
        }
        if (paymentPrice == getTotalPrice(" general_id = " + gmodel.id))
        {
            btnShow.Enabled = false;
        }
        else
        {
            btnShow.Enabled = true;
        }

        if (!string.IsNullOrEmpty(gmodel.source) && gmodel.source != "协议供应商")
        {
            if (dt.Rows.Count == 0)
            {
                Show();
            }
        }
    }

    public void ListBind()
    {
        ListBind(false);
    }

    public string SetModel(GeneralInfo m)
    {
        string returnMsg = "";
        //更新付款账期表
        PaymentPeriodInfo paymentPeriod = new PaymentPeriodInfo();
        paymentPeriod.gid = int.Parse(Request[RequestName.GeneralID]);
        //paymentPeriod.periodPrice = decimal.Parse(txtPay.Text.Trim());
        if (txtBegin.Text.Trim() != "")
            paymentPeriod.beginDate = DateTime.Parse(txtBegin.Text.Trim());
        //if (txtEnd.Text.Trim() != "")
        //    paymentPeriod.endDate = DateTime.Parse(txtEnd.Text.Trim());
        paymentPeriod.endDate = paymentPeriod.beginDate;
        paymentPeriod.periodType = (int)State.PeriodType.prepay;
        //if ((m.PRType == (int)PRTYpe.MediaPR || m.PRType == (int)PRTYpe.PrivatePR) && m.HaveInvoice==false && gvPayment.Rows.Count > 1)
        //    returnMsg = "媒介或对私申请单不可以存在多个付款帐期！";
        return returnMsg;
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
            LitOverplusPrice.Text = (totalprice - usedPrice).ToString("#,##0.####"); //(totalprice - totalprice * (dynamicPercent/100)).ToString("#,##0.0000"); ;

            LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
            lnkEdit.OnClientClick = "openEdit('" + dv["id"].ToString() + "');return false;";
        }
    }

    protected void gdvItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int paymentPeriodId = int.Parse(gvPayment.DataKeys[e.RowIndex].Value.ToString());

        PaymentPeriodInfo model = PaymentPeriodManager.GetModel(paymentPeriodId);
        GeneralInfo gmodel = GeneralInfoManager.GetModel(model.gid);

        if (model.periodType == (int)State.PeriodType.prepay)
        {
            gmodel.sow4 = (decimal.Parse(gmodel.sow4) - model.expectPaymentPrice).ToString() ;
        }

        PaymentPeriodManager.Delete(paymentPeriodId,gmodel);

        dynamicPercent = 0;
        gvPayment.Focus();
        ListBind(true);
    }

    protected void gdvItem_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int paymentPeriodId = int.Parse(gvPayment.DataKeys[e.NewEditIndex].Value.ToString());
        PaymentPeriodInfo model = PaymentPeriodManager.GetModel(paymentPeriodId);
        drpDateType.SelectedValue = model.dateType.ToString();
        drpPeriodDatumPoint.SelectedValue = model.periodDatumPoint.ToString();
        drpPeriodType.SelectedValue = model.periodType.ToString();
        txtPeriodDay.Text = model.periodDay.ToString();
        txtBegin.Text = model.beginDate.ToString("yyyy-MM-dd");
        //txtEnd.Text = model.endDate.ToString("yyyy-MM-dd") == "1900-01-01" ? "" : model.endDate.ToString("yyyy-MM-dd");
        txtExpectPaymentPercent.Text = model.expectPaymentPercent.ToString();
        txtExpectPaymentPrice.Text = model.expectPaymentPrice.ToString("#,##0.0000");
        hidPaymentPeriodId.Value = model.id.ToString();
        txtPeriodRemark.Text = model.periodRemark;
        hidCurrentPercent.Value = model.expectPaymentPercent.ToString();

        //Edit.Visible = true;
        //btnAddPayment.Visible = true;
        //btnNotShow.Visible = true;
        //btnShow.Visible = false;
        dynamicPercent = 0;
        gvPayment.Focus();
        ListBind(false);
    }

    protected void btnAddPayment_Click(object sender, EventArgs e)
    {
        //Response.Redirect("PaymentPeriodEdit.aspx?type=Add&" + RequestName.GeneralID + "=" + Request[RequestName.GeneralID] + "&pageUrl=" + pageName);
        AddPayment(false);
        Response.Redirect(pageName + "?" + RequestName.GeneralID + "=" + Request[RequestName.GeneralID] + "&focus=gvPayment&show=False");
    }

    public void AddPayment(bool isShow)
    {
        if (Edit.Visible == true)
        {
            GeneralInfo gmodel = GeneralInfoManager.GetModel(int.Parse(Request[RequestName.GeneralID]));
            PaymentPeriodInfo model = new PaymentPeriodInfo();
            model.gid = int.Parse(Request[RequestName.GeneralID]);
            model.periodType = int.Parse(drpPeriodType.SelectedValue);
            model.periodDatumPoint = int.Parse(drpPeriodDatumPoint.SelectedValue);
            model.dateType = int.Parse(drpDateType.SelectedValue);
            if (!string.IsNullOrEmpty(txtPeriodDay.Text.Trim()))
            {
                model.periodDay = txtPeriodDay.Text.Trim();
            }
            else
            {
                model.periodDay = "45-60";
            }
            if (txtBegin.Text.Trim() != "")
                model.beginDate = DateTime.Parse(txtBegin.Text.Trim());
            //if (txtEnd.Text.Trim() != "")
            //    model.endDate = DateTime.Parse(txtEnd.Text.Trim());
            model.endDate = model.beginDate;

            model.periodRemark = txtPeriodRemark.Text.Trim();
            model.expectPaymentPercent = decimal.Parse(txtExpectPaymentPercent.Text.Trim());            
            if (!string.IsNullOrEmpty(txtExpectPaymentPrice.Text.Trim()))
            {
                model.expectPaymentPrice = decimal.Parse(txtExpectPaymentPrice.Text.Trim());
            }
            model.Status = State.PaymentStatus_save;
            if (!string.IsNullOrEmpty(hidPaymentPeriodId.Value))
            {
                model.id = int.Parse(hidPaymentPeriodId.Value);
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

            Edit.Visible = false;
           // btnAddPayment.Visible = false;

            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='AddRequisitionStep7.aspx?" + RequestName.GeneralID + "=" + Request[RequestName.GeneralID] + "';alert('添加成功！');", true);
            
        }
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
        gvPayment.Focus();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (Edit.Visible == false)
        {
            Show();
        }
        else {
            AddPayment(true);
            Show();
            Response.Redirect(pageName + "?" + RequestName.GeneralID + "=" + Request[RequestName.GeneralID] + "&focus=gvPayment&show=True");
        }
    }

    protected void btnBind_Click(object sender, EventArgs e)
    {
        ListBind();
        gvPayment.Focus();
    }

    protected void Show()
    {
        //Edit.Visible = true;
        //btnAddPayment.Visible = true;
        //btnNotShow.Visible = true;

        //btnShow.Visible = false;
        gvPayment.Focus();
    }

    public void NotShow()
    {
        Edit.Visible = false;
        //btnAddPayment.Visible = false;
        btnNotShow.Visible = false;

        //btnShow.Visible = true;
        gvPayment.Focus();
    }

    protected void btnNotShow_Click(object sender, EventArgs e)
    {
        NotShow();
    }

    /// <summary>
    /// 获得预付款+付款账期的总金额
    /// </summary>
    /// <returns></returns>
    public decimal GetPaymentPrice()
    {
        return paymentPrice;
    }

    /// <summary>
    /// 获得预付款
    /// </summary>
    /// <returns></returns>
    public decimal GetPayPrice()
    {
        //return decimal.Parse(txtPay.Text.Trim());
        return 0;
    }

    public string GetExpectPaymentPrice(decimal expectPaymentPrice)
    {
        return expectPaymentPrice.ToString("#,##0.00");
    }

}
