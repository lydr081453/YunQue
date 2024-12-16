using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_Print_OrderPrint : ESP.Web.UI.PageBase
{
    int id = 0;
    int num = 1;
    int numP = 1;
    string moneytype = "";
    private decimal dynamicPercent = 0;
    decimal dynamicTotalPrice = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
       
        logoPo.ImageUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["PortalSite"] + "/images/po_.gif";
        logoPo2.ImageUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["PortalSite"] + "/images/po_ (2).gif";
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                id = int.Parse(Request["id"].ToString());

                //添加操作日志
                ESP.Logging.Logger.Add(string.Format("查看/打印 PR流水号为 {0} PO单", id.ToString()));

                if (id > 0)
                    initPrintPage();
            }

            if (!string.IsNullOrEmpty(Request["mail"]))
            {
                btnPrint.Visible = false;
                btnClose.Visible = false;
                linkConfirmUrl.Visible = true;

                spanlog.Visible = false;
                lablog.Visible = false;

                if (Request["mail"] != "changedPrNo")//变更prno时给供应商发信，不需要确认功能
                {
                    ESP.ConfigCommon.SymmetricCrypto crypto = new ESP.ConfigCommon.SymmetricCrypto();
                    linkConfirmUrl.Text = "点击确认订单";
                    linkConfirmUrl.Target = "_blank";
                    string url = HttpContext.Current.Request.Url.ToString();
                    string no_http = url.Substring(url.IndexOf("//") + 2);
                    string host_url = "http://" + no_http.Substring(0, no_http.IndexOf("/") + 1) + "Purchase/Requisition/";
                    linkConfirmUrl.NavigateUrl = host_url + "ConfirmOrder.aspx?content=" + System.Web.HttpUtility.UrlEncode(crypto.EncrypString(Request["id"].ToString()));
                }
            }
            else
            {
                linkConfirmUrl.Visible = false;
            }
            if (!string.IsNullOrEmpty(Request["showBottom"]) && Request["showBottom"] == "false")
                palBottom.Visible = false;
        }
    }

    public void repItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label number = (Label)(e.Item.FindControl("labNum"));
        number.Text = num.ToString();
        num++;

        Label repMoneytype = (Label)(e.Item.FindControl("lab_rep_moneytype"));
        repMoneytype.Text = moneytype;
    }

    public void repPeriod_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //{
        //    Label number = (Label)(e.Item.FindControl("labNum"));
        //    number.Text = numP.ToString();
        //    numP++;
        //}
        if (e.Item.ItemIndex == 0)
        {
            numP = 1;
            dynamicPercent = 0;
            dynamicTotalPrice = 0;
        }
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label number = (Label)(e.Item.FindControl("labNum"));
            number.Text = numP.ToString();
            numP++;

            DataRowView dv = (DataRowView)e.Item.DataItem;

            Literal LitOverplusPrice = (Literal)e.Item.FindControl("LitOverplusPrice");

            dynamicPercent += decimal.Parse(dv["expectPaymentPercent"].ToString());
            // LitOverplusPrice.Text = (dynamicTotalPrice - dynamicTotalPrice * (dynamicPercent / 100)).ToString("#,##0.00");
        }
    }

    public string GetExpectPaymentPrice(decimal expectPaymentPrice)
    {
        return expectPaymentPrice.ToString("#,##0.00");
    }

    private void initPrintPage()
    {
        GeneralInfo generalInfo = new GeneralInfo();
        generalInfo = GeneralInfoManager.GetModel(id);
        ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(generalInfo.project_code.Substring(0,1));

        logoImg.ImageUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["PortalSite"] + "/images/" + branchModel.Logo;

        //item列表项
        List<OrderInfo> orderList = OrderInfoManager.GetOrderList(" and general_id =" + generalInfo.id);

        moneytype = generalInfo.moneytype;

        if (orderList.Count == 0)
        {
            orderList = new List<OrderInfo>();
            orderList.Add(new OrderInfo());
        }

        repItem.DataSource = orderList;
        repItem.DataBind();

        //订单号
        labOrderNo.Text = generalInfo.orderid;

        //供应商名称
        lab_supplier_name.Text = generalInfo.supplier_name;
        //地址
        lab_supplier_address.Text = generalInfo.supplier_address;
        //联系人
        lab_supplier_linkman.Text = generalInfo.supplier_linkman;
        //联系电话
        lab_supplier_phone.Text = generalInfo.Supplier_cellphone;
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

        buyer = new ESP.Compatible.Employee(generalInfo.enduser);

        if (buyer != null)
        {
            string department = GetLevel1DeparmentID(int.Parse(buyer.SysID));
           if (department == State.filialeName_CQID)
            {
                labbuyerFax.Text = State.fax_CQ; //采购方传真
                lab_buyer_Telephone.Text = buyer.Mobile;
            }
            else
            {
                labbuyerFax.Text = State.fax_ZB; //采购方传真
                lab_buyer_Telephone.Text = buyer.Mobile;
            }
            lab_buyer_contect_Name.Text = buyer.Name;
            lab_buyer_EMail.Text = buyer.EMail;
        }

        //送货至
        lab_ship_address.Text = generalInfo.ship_address;

        decimal totalprice = 0;
        foreach (OrderInfo o in orderList)
        {
            totalprice += o.total;
        }
        dynamicTotalPrice = totalprice;
        //总价
        lab_moneytype.Text = (generalInfo.moneytype == "美元" ? "＄" : "￥") + decimal.Round(totalprice, 2).ToString("#,##0.####"); //totalprice.ToString("#.##0.00");


        //工作需求描述
        //lab_sow.Text = generalInfo.sow;

        //备注
        //lab_sow3.Text = generalInfo.sow3;

        //申请人
        lab_requestorname.Text = generalInfo.requestorname;
        //申请日期
        lab_app_date.Text = generalInfo.app_date.ToString("yyyy-MM-dd");
        //联络
        lab_requestor_info.Text = generalInfo.requestor_info;
        //业务组别
        lab_requestor_group.Text = generalInfo.requestor_group;
        //使用人
        lab_endusername.Text = generalInfo.endusername;
        //联络
        lab_enduser_info.Text = generalInfo.enduser_info;
        //收货人
        lab_receivername2.Text = generalInfo.receivername;
        //联络
        lab_receiver_info2.Text = generalInfo.receiver_info;
        //项目号
        lab_project_code.Text = generalInfo.project_code;
        //项目号内容描述 
        lab_project_descripttion.Text = generalInfo.project_descripttion;
        //第三方采购成本预算 
        //lab_buggeted.Text = generalInfo.buggeted.ToString();
        //收货人其他联络方式: 
        //lab_OtherInfo.Text = generalInfo.receiver_Otherinfo;
        //其他
        // lab_others.Text = generalInfo.others;
        this.lab_receiver_group.Text = generalInfo.ReceiverGroup;

        this.lab_enduser_group.Text = generalInfo.enduser_group;

        //框架协议号码
        lab_fa_no.Text = generalInfo.fa_no;
        //申请单号
        lab_PrNo.Text = generalInfo.PrNo;

       // if (generalInfo.first_assessor > 0)
        //{
          //  ESP.Compatible.Employee assessor = new ESP.Compatible.Employee(generalInfo.first_assessor);
           // lab_first_assessor.Text = assessor.Name;
           // lab_first_assessor_info.Text = ESP.Purchase.Common.Common.FormatPhoneNo(assessor.Telephone, 4);
        //}
        //else
        //{
          //  lab_first_assessor.Text = generalInfo.first_assessorname;
       // }

        //账期列表项
        DataTable dt = PaymentPeriodManager.GetPaymentPeriodList(generalInfo.id);

        repPeriod.DataSource = dt;
        repPeriod.DataBind();
    }

    private string GetLevel1DeparmentID(int sysId)
    {
        IList<ESP.Compatible.Department> dtdep = ESP.Compatible.Employee.GetDepartments(sysId);
        //string nodename = "";
        if (dtdep.Count > 0)
        {
            //string level = dtdep[0].Level.ToString();
            //if (level == "1")
            //{
            //    nodename = dtdep[0].NodeName;
            //}
            //else if (level == "2")
            //{
            //    ESP.Compatible.Department dep = ESP.Compatible.DepartmentManager.GetDepartmentByPK(dtdep[0].UniqID);
            //    nodename = dep.Parent.DepartmentName;

            //}
            //else if (level == "3")
            //{
            //  ESP.Compatible.Department dep = ESP.Compatible.DepartmentManager.GetDepartmentByPK(dtdep[0].UniqID);
            //  ESP.Compatible.Department dep2 = ESP.Compatible.DepartmentManager.GetDepartmentByPK(dep.Parent.UniqID);
            // nodename = dep2.Parent.DepartmentName;

            //}
            return dtdep[0].Description;
        }
        else
            return "";
       
    }
}
