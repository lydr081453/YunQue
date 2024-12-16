using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_ConfirmOrder : System.Web.UI.Page
{
    int id = 0;
    int num = 1;
    int num2 = 1;
    string moneytype = "";

    int numP = 1;
    private decimal dynamicPercent = 0;
    decimal dynamicTotalPrice = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            logoPo.ImageUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["PortalSite"] + "/images/po_.gif";
            logoPo2.ImageUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["PortalSite"] + "/images/po_ (2).gif";
            if (!string.IsNullOrEmpty(Request["content"]))
            {
                ESP.ConfigCommon.SymmetricCrypto crypto = new ESP.ConfigCommon.SymmetricCrypto();

                try
                {
                    string urlPara = crypto.DecrypString(Request["content"].ToString());
                    id = int.Parse(urlPara);
                    GeneralInfo generalInfo = GeneralInfoManager.GetModel(id);
                    ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(generalInfo.project_code.Substring(0,1));

                    logoImg.ImageUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["PortalSite"] + "/images/" + branchModel.Logo;

                    //item列表项
                    List<OrderInfo> orderList = OrderInfoManager.GetOrderList(" and general_id =" + generalInfo.id);

                    //历史订单列表
                    List<GeneralInfo> hisList = GeneralInfoManager.GetStatusList(" and a.id != " + generalInfo.id + " and a.supplier_name = '" + generalInfo.supplier_name + "' and a.status = " + State.order_confirm.ToString());

                    bindInfo(generalInfo, orderList, hisList);

                    if (generalInfo.status == State.order_sended)
                    {
                        ESP.ITIL.BusinessLogic.申请单业务设置.供应商确认订单(ref generalInfo);
                        GeneralInfoManager.Update(generalInfo);
                        LogInfo log = new LogInfo();
                        log.Gid = generalInfo.id;
                        log.LogMedifiedTeme = DateTime.Now;
                        log.LogUserId = 0;
                        log.Des = string.Format(State.log_confrim, generalInfo.supplier_name, DateTime.Now.ToString());
                        LogManager.AddLog(log, Request);

                        string auditEmail = State.getEmployeeEmailBySysUserId(generalInfo.Filiale_Auditor == 0 ? generalInfo.first_assessor : generalInfo.Filiale_Auditor);
                        string ret = ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPOConfirm(generalInfo, generalInfo.orderid, State.getEmployeeEmailBySysUserId(generalInfo.requestor), State.getEmployeeEmailBySysUserId(generalInfo.goods_receiver), auditEmail);
                    }
                    labConfirm.Text = "订单已确认!";
                }
                catch
                {
                    //
                }
            }
        }
    }

    /// <summary>
    /// Gets the expect payment price.
    /// </summary>
    /// <param name="expectPaymentPrice">The expect payment price.</param>
    /// <returns></returns>
    public string GetExpectPaymentPrice(decimal expectPaymentPrice)
    {
        return expectPaymentPrice.ToString("#,##0.00");
    }

    /// <summary>
    /// Binds the info.
    /// </summary>
    /// <param name="generalInfo">The general info.</param>
    /// <param name="orderList">The order list.</param>
    /// <param name="hisList">His list.</param>
    private void bindInfo(GeneralInfo generalInfo, List<OrderInfo> orderList, List<GeneralInfo> hisList)
    {
        moneytype = generalInfo.moneytype;

        if (orderList.Count == 0)
        {
            orderList = new List<OrderInfo>();
            orderList.Add(new OrderInfo());
        }

        repItem.DataSource = orderList;
        repItem.DataBind();

        repHis.DataSource = hisList;
        repHis.DataBind();

        //订单号
        labOrderNo.Text = generalInfo.orderid;

        //供应商名称
        lab_supplier_name.Text = generalInfo.supplier_name;
        //地址
        lab_supplier_address.Text = generalInfo.supplier_address;
        //联系人
        lab_supplier_linkman.Text = generalInfo.supplier_linkman;
        //联系电话
        lab_supplier_phone.Text = generalInfo.supplier_phone;
        //传真 
        lab_supplier_fax.Text = generalInfo.supplier_fax;
        //电子邮件
        lab_supplier_email.Text = generalInfo.supplier_email;

        //供应商来源
        //lab_source.Text = generalInfo.source;

        //采购方
        ESP.Compatible.Employee buyer = null;

        string[] projectNo = generalInfo.project_code.Split('-');
        if (projectNo.Length > 0 && !string.IsNullOrEmpty(projectNo[0]))
        {
            lab_DepartmentName.Text = State.NameHT[projectNo[0].ToString()] == null ? "项目号有误" : State.NameHT[projectNo[0].ToString()].ToString();
            lab_buyer_Address.Text = State.AddressHT[projectNo[0].ToString()] == null ? "项目号有误" : State.AddressHT[projectNo[0].ToString()].ToString();
        }

        if (generalInfo.Filiale_Auditor != 0)
        {
            buyer = new ESP.Compatible.Employee(generalInfo.Filiale_Auditor);
        }
        else
        {
            buyer = new ESP.Compatible.Employee(generalInfo.first_assessor);
        }

        if (buyer != null)
        {
            lab_buyer_contect_Name.Text = buyer.Name;
            lab_buyer_Telephone.Text = buyer.Telephone;
            lab_buyer_EMail.Text = buyer.EMail;
        }

        //送货至
        lab_ship_address.Text = generalInfo.ship_address;

        decimal totalprice = 0;
        foreach (OrderInfo o in orderList)
        {
            totalprice += o.total;
        }
        //总价
        lab_moneytype.Text = (generalInfo.moneytype == "美元" ? "＄" : "￥") + decimal.Round(totalprice, 2).ToString();

        dynamicTotalPrice = totalprice;

        //工作需求描述
        lab_sow.Text = generalInfo.sow;

        //备注
        lab_sow3.Text = generalInfo.sow3;

        //账期列表项
        DataTable dt = PaymentPeriodManager.GetPaymentPeriodList(generalInfo.id);

        repPeriod.DataSource = dt;
        repPeriod.DataBind();
    }

    /// <summary>
    /// Handles the ItemDataBound event of the repPeriod control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterItemEventArgs"/> instance containing the event data.</param>
    public void repPeriod_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label number = (Label)(e.Item.FindControl("labNum"));
            number.Text = numP.ToString();
            numP++;

            DataRowView dv = (DataRowView)e.Item.DataItem;

            Literal LitOverplusPrice = (Literal)e.Item.FindControl("LitOverplusPrice");

            dynamicPercent += decimal.Parse(dv["expectPaymentPercent"].ToString());
            LitOverplusPrice.Text = (dynamicTotalPrice - dynamicTotalPrice * (dynamicPercent / 100)).ToString("#,##0.00");
        }
    }

    /// <summary>
    /// Handles the ItemDataBound event of the repItem control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterItemEventArgs"/> instance containing the event data.</param>
    public void repItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label number = (Label)(e.Item.FindControl("labNum"));
        number.Text = num.ToString();
        num++;

        Label repMoneytype = (Label)(e.Item.FindControl("lab_rep_moneytype"));
        repMoneytype.Text = moneytype;
    }

    /// <summary>
    /// Handles the ItemDataBound event of the repHis control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterItemEventArgs"/> instance containing the event data.</param>
    public void repHis_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label number = (Label)(e.Item.FindControl("labNum"));
        number.Text = num2.ToString();
        num2++;

        Label repMoneytype = (Label)(e.Item.FindControl("lab_rep_moneytype"));
        repMoneytype.Text = moneytype;

        GeneralInfo gi = (e.Item.DataItem as GeneralInfo);

        ESP.Compatible.Employee buyer = new ESP.Compatible.Employee();
        if (/*gi.Filiale_Auditor != null && */gi.Filiale_Auditor != 0)
        {
            buyer = new ESP.Compatible.Employee(gi.Filiale_Auditor);
        }
        else
        {
            buyer = new ESP.Compatible.Employee(gi.first_assessor);
        }
        if (buyer != null)
        {
            Label lab_buyer_name = (Label)(e.Item.FindControl("lab_buyer_name"));
            lab_buyer_name.Text = buyer.Name;

            Label lab_buyer_email = (Label)(e.Item.FindControl("lab_buyer_email"));
            lab_buyer_email.Text = buyer.EMail;
        }

        Label lab_statusname = (Label)(e.Item.FindControl("lab_statusname"));
        lab_statusname.Text = State.requistionOrorder_state[gi.status];
    }
}
