using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_AddRequisitionStep7 : ESP.Purchase.WebPage.EditPageForPR
{
    int generalid = 0;

    string query = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
        {
            generalid = int.Parse(Request[RequestName.GeneralID]);
            ItemListBind(" general_id = " + generalid);
        }
        if (!IsPostBack)
        {
            BindInfo();
            //如果申请单为协议供应商删除付款账期信息
            GeneralInfo g = GeneralInfoManager.GetModel(generalid);
            //框架合同列表
            if (!string.IsNullOrEmpty(g.FCPrIds.Trim(',')))
            {
                DataTable FCList = GeneralInfoManager.GetFCPrList(null, g.FCPrIds.Trim(','));
                GvFCPr.DataSource = FCList;
                GvFCPr.DataBind();
                FCTable.Visible = FCList.Rows.Count > 0;
            }

            if ((g.status == State.requisition_save || g.status == State.requisition_commit || g.status == State.requisition_temporary_commit) && g.source == "协议供应商")
            {
                int periodsCount = PaymentPeriodManager.GetList(" gid = " + g.id).Tables[0].Rows.Count;
                if (periodsCount == 0)
                {
                    int supplierPeriod = 0;
                    //获得协议供应商的账期
                    List<OrderInfo> orderList = OrderInfoManager.GetListByGeneralId(g.id);
                    if (orderList != null && orderList.Count > 0)
                    {
                        foreach (OrderInfo order in orderList)
                        {
                            if (order.supplierId > 0)
                            {
                                SupplierInfo supplier = SupplierManager.GetModel(order.supplierId);
                                try
                                {
                                    supplierPeriod = int.Parse(supplier.business_paytime);
                                }
                                catch { }
                                break;
                            }
                        }
                    }

                    PaymentPeriodInfo paymentPeriod = new PaymentPeriodInfo();
                    paymentPeriod.gid = g.id;
                    paymentPeriod.expectPaymentPrice = getTotalPrice(" general_id = " + g.id);
                    paymentPeriod.expectPaymentPercent = 100;
                    paymentPeriod.periodDay = State.supplierpayment[supplierPeriod].ToString();
                    paymentPeriod.beginDate = DateTime.Parse(DateTime.Now.AddMonths(2 + supplierPeriod).ToString("yyyy-MM") + "-01").AddDays(14);
                    //paymentPeriod.endDate = DateTime.Parse(DateTime.Now.AddMonths(3 + supplierPeriod).ToString("yyyy-MM") + "-01").AddDays(-1);
                    paymentPeriod.periodType = (int)State.PeriodType.period;
                    paymentPeriod.Status = State.PaymentStatus_save;
                    PaymentPeriodManager.Add(paymentPeriod);
                }
            }
            //未提交、驳回，采购驳回状态
            //如果为框架合同，生成预付
            //如果仅押金无付款
            if ((g.status == State.requisition_save || g.status == State.requisition_return || g.status == State.order_return) && (g.Requisitionflow == ESP.Purchase.Common.State.requisitionflow_toFC || (g.Foregift > 0 && g.totalprice == 0)))
            {
                int periodsCount = PaymentPeriodManager.GetList(" gid = " + g.id).Tables[0].Rows.Count;
                if (periodsCount == 0)
                {
                    PaymentPeriodInfo paymentPeriod = new PaymentPeriodInfo();
                    paymentPeriod.gid = g.id;
                    paymentPeriod.expectPaymentPrice = 0;
                    paymentPeriod.expectPaymentPercent = 100;
                    paymentPeriod.periodDay = "0";
                    paymentPeriod.beginDate = DateTime.Now;
                    paymentPeriod.periodType = (int)State.PeriodType.prepay;
                    paymentPeriod.Status = State.PaymentStatus_save;
                    PaymentPeriodManager.Add(paymentPeriod);
                }
            }
        }
        query = Request.Url.Query;
    }

    private void ItemListBind(string term)
    {
        DataSet ds = OrderInfoManager.GetList(term);
        gdvItem.DataSource = ds.Tables[0];
        gdvItem.DataBind();
        DgRecordCount.Text = ds.Tables[0].Rows.Count.ToString();
        if (ds.Tables[0].Rows.Count == 0)
        {
            labTotalPrice.Text = "0";
        }
        else
        {
            decimal totalprice = 0;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                totalprice += decimal.Parse(ds.Tables[0].Rows[i]["total"].ToString());
                labTotalPrice.Text = totalprice.ToString("#,##0.####");
                paymentInfo.TotalPrice = totalprice;
            }
        }
    }

    public void BindInfo()
    {
        if (generalid > 0)
        {
            GeneralInfo g = GeneralInfoManager.GetModel(generalid);
            if (null != g)
            {
                GenericInfo.Model = g;
                GenericInfo.BindInfo();

                projectInfo.Model = g;
                projectInfo.BindInfo();

                supplierInfo.Model = g;
                supplierInfo.BindInfo();

                paymentInfo.Model = g;
                //paymentInfo.BindInfo();
                if (g.source == "" || g.source == "协议供应商")
                {
                    paymentInfo.IsShowCommand = false;
                    paymentInfo.IsShowGridView = false;
                }
                if (g.PRType != (int)ESP.Purchase.Common.PRTYpe.MediaPR && g.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA && g.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_PriFA)
                {
                    if (g.ValueLevel == 1)
                        lblValueLevel.Text = "5000元(含)以下，不需要采购部审批！";
                    else
                        lblValueLevel.Text = "<font color='red'>5000元以上，需要采购部审批！";
                }
                labNote.Text = g.sow3.Trim();
            }
        }
    }

    public int SaveInfo(GeneralInfo g)
    {
        string strvalue = "";
        g.Addstatus = 6;

        try
        {
            paymentInfo.AddPayment(false);
            paymentInfo.NotShow();
        }
        catch { }

        if (generalid == 0)
        {
            return GeneralInfoDataProvider.Add(g);
        }
        else
        {
            try
            {
                GeneralInfoDataProvider.Update(g);
                //如果申请单为协议供应商删除付款账期信息
                if ((g.status == State.requisition_save || g.status == State.requisition_commit || g.status == State.requisition_temporary_commit) && g.source == "协议供应商")
                {
                    int periodsCount = PaymentPeriodManager.GetList(" gid = " + g.id).Tables[0].Rows.Count;
                    if (periodsCount == 0)
                    {
                        int supplierPeriod = 0;
                        //获得协议供应商的账期
                        List<OrderInfo> orderList = OrderInfoManager.GetListByGeneralId(g.id);
                        if (orderList != null && orderList.Count > 0)
                        {
                            foreach (OrderInfo order in orderList)
                            {
                                if (order.supplierId > 0)
                                {
                                    SupplierInfo supplier = SupplierManager.GetModel(order.supplierId);
                                    try
                                    {
                                        supplierPeriod = int.Parse(supplier.business_paytime);
                                    }
                                    catch { }
                                    break;
                                }
                            }
                        }

                        PaymentPeriodInfo paymentPeriod = new PaymentPeriodInfo();
                        paymentPeriod.gid = g.id;
                        paymentPeriod.expectPaymentPrice = getTotalPrice(" general_id = " + g.id);
                        paymentPeriod.expectPaymentPercent = 100;
                        paymentPeriod.periodDay = State.supplierpayment[supplierPeriod].ToString();
                        paymentPeriod.beginDate = DateTime.Parse(DateTime.Now.AddMonths(2 + supplierPeriod).ToString("yyyy-MM") + "-01").AddDays(14);
                        //paymentPeriod.endDate = DateTime.Parse(DateTime.Now.AddMonths(3 + supplierPeriod).ToString("yyyy-MM") + "-01").AddDays(-1);
                        paymentPeriod.periodType = (int)State.PeriodType.period;
                        paymentPeriod.Status = State.PaymentStatus_save;
                        PaymentPeriodManager.Add(paymentPeriod);
                    }
                }
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }

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

    protected void btnSave_Click(object sender, EventArgs e)
    {

        GeneralInfo g = GeneralInfoManager.GetModel(generalid);
        if (checkPayPrice(g) != "")
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + checkPayPrice(g) + "');", true);
            return;
        }
        paymentInfo.Model = g;
        string Msg = paymentInfo.SetModel(g);
        if (Msg != "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + Msg + "');", true);
            return;
        }
        if (SaveInfo(g) != 0)
        {
            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, generalid.ToString(), "创建采购申请单第四步保存并返回列表页"), "创建采购申请单");

            ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据保存成功!');window.location='RequisitionSaveList.aspx'", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据保存失败!');", true);
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        GeneralInfo g = GeneralInfoManager.GetModel(generalid);
        OrderInfo orderModel = OrderInfoManager.GetModelByGeneralID(generalid);

        if (checkPayPrice(g) != "")
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + checkPayPrice(g) + "');", true);
            return;
        }
        paymentInfo.AddPayment(false);
        paymentInfo.DynamicPercent = 0;
        paymentInfo.ListBind();
        //System.Web.UI.HtmlControls.HtmlInputHidden hitTotalPercent = (System.Web.UI.HtmlControls.HtmlInputHidden)paymentInfo.FindControl("hitTotalPercent");//付款账期预计支付百分比
        //if (hitTotalPercent != null && hitTotalPercent.Value != "")
        //{
        //    if (decimal.Parse(hitTotalPercent.Value) != 100)
        //    {
        //        ClientScript.RegisterStartupScript(typeof(string), "", "alert('付款账期预计支付百分比应为100%');", true);
        //        return;
        //    }
        //}
        decimal totalPayment = paymentInfo.GetPaymentPrice();

        //付款账期金额必须等于采购物品总额
        if (totalPayment != getTotalPrice(" general_id = " + g.id))
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('付款账期总金额必须等于采购物品总金额！');", true);
            return;
        }

        paymentInfo.Model = g;
        string Msg = paymentInfo.SetModel(g);
        if (Msg != "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + Msg + "');", true);
            return;
        }
        if (SaveInfo(g) != 0)
        {
            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, generalid.ToString(), "创建采购申请单第四步跳转第五步"), "创建采购申请单");

            query = query.AddParam(RequestName.GeneralID, generalid);
            query = query.Replace("focus=gvPayment&show=True", "");
            if (query.Length > 0 && query.Substring(0, 1).Equals("&"))
            {
                query = query.Substring(1, query.Length - 1);
            }
            Response.Redirect("AddRequisitionStep5.aspx?" + query);
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据提交失败!');", true);
        }
    }

    protected void btnPre_Click(object sender, EventArgs e)
    {
        GeneralInfo g = GeneralInfoManager.GetModel(generalid);
        if (checkPayPrice(g) != "")
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + checkPayPrice(g) + "');", true);
            return;
        }
        g.Addstatus = 4;
        try
        {
            GeneralInfoDataProvider.Update(g);

            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, generalid.ToString(), "创建采购申请单第四步跳转第三步"), "创建采购申请单");
        }
        catch (Exception ex)
        {
            //
        }
        finally
        {
            query = query.AddParam(RequestName.GeneralID, generalid);
            query = query.Replace("focus=gvPayment&show=True", "");
            if (query.Length > 0 && query.Substring(0, 1).Equals("&"))
            {
                query = query.Substring(1, query.Length - 1);
            }
            Response.Redirect("AddRequisitionStep6.aspx?" + query);
        }
    }

    int num = 1;
    protected void gdvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ;
            //OrderInfo ordermodel = OrderInfoManager.GetModelByGeneralID(Convert.ToInt32(Request[RequestName.GeneralID]));

            DataRowView dr = (DataRowView)e.Row.DataItem;
            OrderInfo ordermodel = OrderInfoManager.GetModel(Convert.ToInt32(dr["id"].ToString()));
            if (ordermodel == null)
            {
                ordermodel = e.Row.DataItem as OrderInfo;
            }
            if (!string.IsNullOrEmpty(Request[RequestName.BillID]) || (ordermodel != null && ordermodel.BillID > 0))//针对媒介对私的报销单
            {
                e.Row.Cells[0].Text = num.ToString();
                num++;
                Label labDown = (Label)e.Row.FindControl("labDown");
                string[] links = ordermodel.upfile.TrimEnd('#').Split('#');
                //string[] links = dr["upfile"].ToString().TrimEnd('#').Split('#');
                labDown.Text = "";
                for (int i = 0; i < links.Length; i++)
                {
                    if (links[i].Trim() != "")
                    {
                        if (links[i].Trim().IndexOf(".aspx") > 0)
                            labDown.Text += "<a target='_blank' href='/" + links[i].Trim() + "'><img src='/images/ico_04.gif' border='0' /></a>&nbsp;";
                        else
                            labDown.Text += "<a target='_blank' href='/Purchase/Requisition/UpfileDownload.aspx?OrderId=" + dr["id"].ToString() + "&Index=" + i.ToString() + "'><img src='/images/ico_04.gif' border='0' /></a>&nbsp;";
                    }
                }
                e.Row.Cells[2].Text = ordermodel.producttypename;
            }
            else
            {
                e.Row.Cells[0].Text = num.ToString();
                num++;
                Label labDown = (Label)e.Row.FindControl("labDown");
                string[] links = ordermodel.upfile.TrimEnd('#').Split('#');
                labDown.Text = "";
                for (int i = 0; i < links.Length; i++)
                {
                    if (links[i].Trim() != "")
                    {
                        if (links[i].Trim().IndexOf(".aspx") > 0)
                            labDown.Text += "<a target='_blank' href='/" + links[i].Trim() + "'><img src='/images/ico_04.gif' border='0' /></a>&nbsp;";
                        else
                            labDown.Text += "<a target='_blank' href='/Purchase/Requisition/UpfileDownload.aspx?OrderId=" + dr["id"].ToString() + "&Index=" + i.ToString() + "'><img src='/images/ico_04.gif' border='0' /></a>&nbsp;";
                    }
                }

                int productTypeID = int.Parse(dr["productType"].ToString());
                TypeInfo ty = TypeManager.GetModel(productTypeID);

                e.Row.Cells[2].Text = ty == null ? "" : TypeManager.GetModel(productTypeID).typename;
            }
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    protected string checkPayPrice(GeneralInfo model)
    {
        return "";
    }
}