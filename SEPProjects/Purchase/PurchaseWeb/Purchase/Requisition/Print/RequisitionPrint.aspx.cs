using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using System.Data;
using ESP.Purchase.Entity;
using System.Data.SqlClient;

public partial class Purchase_Requisition_Print_RequisitionPrint : ESP.Web.UI.PageBase
{
    string id = "";
    int num = 1;
    int numP = 1;
    string moneytype = "";
    int listCount = 0;
    private decimal dynamicPercent = 0;
    decimal dynamicTotalPrice = 0;

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                id = Request["id"].ToString();
                List<GeneralInfo> list = GeneralInfoManager.GetStatusList(" and a.id in (" + id + ")");
                listCount = list.Count;
                repList.DataSource = list;
                repList.DataBind();
            }
        }
        if (!string.IsNullOrEmpty(Request["viewButton"]) && Request["viewButton"] == "no")
        {
            btnClose.Visible = false;
            btnPrint.Visible = false;
        }
    }

    public void repItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex == 0)
        {
            num = 1;
        }
        Label number = (Label)(e.Item.FindControl("labNum"));
        number.Text = num.ToString();
        num++;

        Label repMoneytype = (Label)(e.Item.FindControl("lab_rep_moneytype"));
        repMoneytype.Text = moneytype;
    }

    public void repPeriod_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex == 0)
        {
            numP = 1;
            dynamicPercent = 0;
            //dynamicTotalPrice = 0;
        }
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label number = (Label)(e.Item.FindControl("labNum"));
            number.Text = numP.ToString();
            numP++;

            DataRowView dv = (DataRowView)e.Item.DataItem;

          //  Literal LitOverplusPrice = (Literal)e.Item.FindControl("LitOverplusPrice");

           // dynamicPercent += decimal.Parse(dv["expectPaymentPercent"].ToString());
         //   LitOverplusPrice.Text = (dynamicTotalPrice - dynamicTotalPrice * (dynamicPercent / 100)).ToString("#,##0.00");
        }
    }

    public string GetExpectPaymentPrice(decimal expectPaymentPrice)
    {
        return expectPaymentPrice.ToString("#,##0.00");
    }

    protected void repList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GeneralInfo generalInfo = (GeneralInfo)e.Item.DataItem;
            ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(generalInfo.project_code.Substring(0, 1));
            Image imgLogo = (Image)e.Item.FindControl("imgLogo");
            imgLogo.ImageUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["PortalSite"] + "/images/" + branchModel.Logo;
            //item列表项
            List<OrderInfo> orderList = OrderInfoManager.GetOrderList(" and general_id =" + generalInfo.id);
            List<ESP.Purchase.Entity.PaymentPeriodInfo> paymentList = PaymentPeriodManager.GetModelList(" gid=" + generalInfo.id);
            string PNS = string.Empty;
            moneytype = generalInfo.moneytype;
            Repeater repItem = (Repeater)e.Item.FindControl("repItem");
            repItem.DataSource = orderList;
            repItem.DataBind();

            Label labglideNo = (Label)e.Item.FindControl("labglideNo");
            Label lblPN = (Label)e.Item.FindControl("lblPN");
            Label lab_AppendUser = (Label)e.Item.FindControl("lab_AppendUser");
            Label laboldprinfo = (Label)e.Item.FindControl("laboldprinfo");
            Label lblYaJin = (Label)e.Item.FindControl("lblYaJin");
            lblYaJin.Text = generalInfo.Foregift == null ? "" : generalInfo.Foregift.ToString("#,##0.00");

            if (!string.IsNullOrEmpty(Request["Action"]) && Request["Action"].ToString() == "ViewOldPr")
            {
                laboldprinfo.Text = "<br>姓名: " + generalInfo.supplier_name + "<br>身份证:" + generalInfo.supplier_address + "<br>帐号:" + generalInfo.account_number + "<br>银行:" + generalInfo.account_bank + "<br>名称：" + generalInfo.account_name;
            }
            lab_AppendUser.Text = generalInfo.appendReceiverName + "(" + generalInfo.appendReceiverInfo + ")";
            labglideNo.Text = generalInfo.id.ToString("0000000");
            //PN number
            foreach (ESP.Purchase.Entity.PaymentPeriodInfo p in paymentList)
            {
                PNS += p.ReturnCode + ",";
            }
            if (!string.IsNullOrEmpty(PNS))
                lblPN.Text = "付款单号：" + PNS.TrimEnd(',') + "<br/>";
            ESP.Purchase.Entity.MediaPREditHisInfo relationModel = ESP.Purchase.BusinessLogic.MediaPREditHisManager.GetModelByOldPRID(generalInfo.id);
            if (relationModel != null && relationModel.NewPRId != null)
            {
                ESP.Purchase.Entity.GeneralInfo newGeneral = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(relationModel.NewPRId.Value);
                lblPN.Text += "新PR流水：" + newGeneral.id.ToString() + "<br/>";
                lblPN.Text += "新PR单号：" + newGeneral.PrNo;
            }
            //申请单号
            Label labPrno = (Label)e.Item.FindControl("labPrno");
            labPrno.Text = generalInfo.PrNo;

            Label labLX = (Label)e.Item.FindControl("labLX");
            labLX.Text = "Pr → " + State.requisitionflow_state[generalInfo.Requisitionflow].ToString();

            //日志
            List<LogInfo> loglist = LogManager.GetLoglistWithFinance(generalInfo.id);
            string strlog = string.Empty;
            foreach (LogInfo log in loglist)
            {
                strlog += log.Des + "<br/>";
            }
            Label lablog = (Label)e.Item.FindControl("lablog");
            lablog.Text = strlog;

            //业务审核日志
            IList<AuditLogInfo> oploglist = AuditLogManager.GetModelListByGID(generalInfo.id);
            string strOplog = string.Empty;
            foreach (AuditLogInfo log in oploglist)
            {
                ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(log.auditUserId);
                strOplog += log.auditUserName + "(" + emp.FullNameEN + ")" + State.operationAudit_statusName[log.auditType] + " " + log.remark + " " + log.remarkDate + "<br/>";
            }
            Label labAuditLog = (Label)e.Item.FindControl("labAuditLog");
            labAuditLog.Text = strOplog;

            Label labOverrule = (Label)e.Item.FindControl("labOverrule");
            labOverrule.Text = generalInfo.requisition_overrule;

            //供应商名称
            Label lab_supplier_name = (Label)e.Item.FindControl("lab_supplier_name");

            lab_supplier_name.Text = generalInfo.supplier_name;

            //地址
            Label lab_supplier_address = (Label)e.Item.FindControl("lab_supplier_address");
            lab_supplier_address.Text = generalInfo.supplier_address;

            Label lab_supplier_address_title = (Label)e.Item.FindControl("lab_supplier_address_title");
            Label lblXiaoMiOrder_Title = (Label)e.Item.FindControl("lblXiaoMiOrder_Title");
            Label lblXiaoMiOrder = (Label)e.Item.FindControl("lblXiaoMiOrder");

            lab_supplier_address_title.Text = "地址";
            //联系人
            Label lab_supplier_linkman = (Label)e.Item.FindControl("lab_supplier_linkman");
            lab_supplier_linkman.Text = generalInfo.supplier_linkman;
            //联系电话
            Label lab_supplier_phone = (Label)e.Item.FindControl("lab_supplier_phone");
            lab_supplier_phone.Text = generalInfo.Supplier_cellphone;
            //传真 
            Label lab_supplier_fax = (Label)e.Item.FindControl("lab_supplier_fax");
            lab_supplier_fax.Text = generalInfo.supplier_fax;
            //电子邮件
            Label lab_supplier_email = (Label)e.Item.FindControl("lab_supplier_email");
            lab_supplier_email.Text = generalInfo.supplier_email;
            //供应商来源
            Label lab_source = (Label)e.Item.FindControl("lab_source");
            lab_source.Text = generalInfo.source;

            //送货至地址
            Label lab_ship_address = (Label)e.Item.FindControl("lab_ship_address");
            lab_ship_address.Text = generalInfo.ship_address;
            //送货至邮件
            if (generalInfo.goods_receiver > 0)
            {
                ESP.Compatible.Employee receiver = new ESP.Compatible.Employee(generalInfo.goods_receiver);
            }

            //采购方
            ESP.Compatible.Employee buyer = null;

            string[] projectNo = generalInfo.project_code.Split('-');
            if (projectNo.Length > 0 && !string.IsNullOrEmpty(projectNo[0]))
            {
                Label lab_DepartmentName = (Label)e.Item.FindControl("lab_DepartmentName");
                Label lab_buyer_Address = (Label)e.Item.FindControl("lab_buyer_Address");
                lab_DepartmentName.Text = State.NameHT[projectNo[0].ToString()] == null ? "项目号有误" : State.NameHT[projectNo[0].ToString()].ToString();
                lab_buyer_Address.Text = State.AddressHT[projectNo[0].ToString()] == null ? "项目号有误" : State.AddressHT[projectNo[0].ToString()].ToString();
            }

            if (/*generalInfo.Filiale_Auditor != null && */generalInfo.Filiale_Auditor != 0)
            {
                buyer = new ESP.Compatible.Employee(generalInfo.Filiale_Auditor);
            }
            else
            {
                buyer = new ESP.Compatible.Employee(generalInfo.first_assessor);
            }

            if (buyer != null)
            {
                Label lab_buyer_contect_Name = (Label)e.Item.FindControl("lab_buyer_contect_Name");
                Label lab_buyer_Telephone = (Label)e.Item.FindControl("lab_buyer_Telephone");
                Label lab_buyer_EMail = (Label)e.Item.FindControl("lab_buyer_EMail");
                lab_buyer_contect_Name.Text = buyer.Name;
                lab_buyer_Telephone.Text = buyer.Mobile;
                lab_buyer_EMail.Text = buyer.EMail;
            }

            decimal totalprice = 0;
            foreach (OrderInfo o in orderList)
            {
                totalprice += o.total;
            }
            dynamicTotalPrice = totalprice;
            //总价
            Label lab_moneytype = (Label)e.Item.FindControl("lab_moneytype");
            lab_moneytype.Text = (generalInfo.moneytype == "美元" ? "＄" : "￥") + decimal.Round(totalprice, 4).ToString("#,##0.####"); //totalprice.ToString("#.##0.00");


            //工作需求描述
            Label lab_sow = (Label)e.Item.FindControl("lab_sow");
            lab_sow.Text = generalInfo.sow;

            //备注
            Label lab_sow3 = (Label)e.Item.FindControl("lab_sow3");
            lab_sow3.Text = generalInfo.sow3;

            //申请人
            Label lab_requestorname = (Label)e.Item.FindControl("lab_requestorname");
            lab_requestorname.Text = generalInfo.requestorname;
            //申请日期
            Label lab_app_date = (Label)e.Item.FindControl("lab_app_date");
            lab_app_date.Text = generalInfo.app_date.ToString("yyyy-MM-dd");
            //联络
            Label lab_requestor_info = (Label)e.Item.FindControl("lab_requestor_info");
            lab_requestor_info.Text = generalInfo.requestor_info;
            //业务组别
            Label lab_requestor_group = (Label)e.Item.FindControl("lab_requestor_group");
            lab_requestor_group.Text = generalInfo.requestor_group;
            //使用人
            Label lab_endusername = (Label)e.Item.FindControl("lab_endusername");
            lab_endusername.Text = generalInfo.endusername;
            //联络
            Label lab_enduser_info = (Label)e.Item.FindControl("lab_enduser_info");
            lab_enduser_info.Text = generalInfo.enduser_info;
            //收货人
            Label lab_receivername2 = (Label)e.Item.FindControl("lab_receivername2");
            lab_receivername2.Text = generalInfo.receivername;
            //联络
            Label lab_receiver_info2 = (Label)e.Item.FindControl("lab_receiver_info2");
            lab_receiver_info2.Text = generalInfo.receiver_info;
            //项目号
            Label lab_project_code = (Label)e.Item.FindControl("lab_project_code");
            lab_project_code.Text = generalInfo.project_code;
            //项目号内容描述 
            Label lab_project_descripttion = (Label)e.Item.FindControl("lab_project_descripttion");
            lab_project_descripttion.Text = generalInfo.project_descripttion + State.oldFlagNames[generalInfo.oldFlag == false ? 0 : 1];
            //第三方采购成本预算 
            Label lab_buggeted = (Label)e.Item.FindControl("lab_buggeted");
            lab_buggeted.Text = generalInfo.buggeted.ToString("#,##0.0");
            //收货人其他联络方式: 
            Label lab_OtherInfo = (Label)e.Item.FindControl("lab_OtherInfo");
            lab_OtherInfo.Text = generalInfo.receiver_Otherinfo;

            if (listCount > 1 && e.Item.ItemIndex != (listCount - 1))
            {
                Label labafter = (Label)e.Item.FindControl("labafter");
                labafter.Text = "<p style=\"page-break-after:always\">&nbsp;</p>";
            }

            //账期列表项
            DataTable dt = PaymentPeriodManager.GetPaymentPeriodList(generalInfo.id);
            Repeater repPeriod = (Repeater)e.Item.FindControl("repPeriod");
            repPeriod.DataSource = dt;
            repPeriod.DataBind();

            Label labInvoiceType = (Label)e.Item.FindControl("labInvoiceType");
            labInvoiceType.Text = generalInfo.InvoiceType;
            Label labTaxRate = (Label)e.Item.FindControl("labTaxRate");
            labTaxRate.Text = generalInfo.TaxRate == null ? "" : generalInfo.TaxRate.ToString();
        }
    }
}
