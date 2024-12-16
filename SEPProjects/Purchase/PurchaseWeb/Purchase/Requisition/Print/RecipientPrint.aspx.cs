using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_Print_RecipientPrint : ESP.Web.UI.PageBase
{
    int id = 0;
    string moneytype = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        logoPo.ImageUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["PortalSite"] + "/images/rec.gif";
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                id = int.Parse(Request["id"].ToString());
                if (id > 0)
                    initPrintPage();
            }
            ESP.Logging.Logger.Add(string.Format("查看/打印 流水号为 {0} PR单",id.ToString()));

            if (!string.IsNullOrEmpty(Request["mail"]))
            {
                ESP.ConfigCommon.SymmetricCrypto crypto = new ESP.ConfigCommon.SymmetricCrypto();
                linkConfirmUrl.Text = "点击确认收货";
                linkConfirmUrl.Target = "_blank";
                string url = HttpContext.Current.Request.Url.ToString();
                string no_http = url.Substring(url.IndexOf("//") + 2);
                string host_url = "http://" + no_http.Substring(0, no_http.IndexOf("/") + 1) + "Purchase/Requisition/";
                linkConfirmUrl.NavigateUrl = host_url + "ConfirmRecipient.aspx?content=" + System.Web.HttpUtility.UrlEncode(crypto.EncrypString(Request["id"].ToString() + ";" + Request["rec"].ToString() + ";" + Request["reccount"].ToString() + ";" + Request["recipientId"].ToString()));
            }
            else
            {
                linkConfirmUrl.Visible = false;
            }
        }
    }

    private void initPrintPage()
    {
        GeneralInfo generalInfo = new GeneralInfo();
        generalInfo = GeneralInfoManager.GetModel(id);
        ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(generalInfo.project_code.Substring(0, 1));

        logoImg.ImageUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["PortalSite"] + "/images/" + branchModel.Logo;

        RecipientInfo recInfo = RecipientManager.GetModel(int.Parse(Request["recipientId"].ToString()));
        //如果是实发金额收货，增加提示
        if (recInfo.Status == ESP.Purchase.Common.State.recipientstatus_Unsure)
        {
            lblLastNotify.Text = "此次实发金额收货为本采购订单最后一次收货，最终金额以本次收货金额以及历史已完成收货实际金额为准（如有）";
        }
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

        //供应商名称
        lab_supplier_name.Text = generalInfo.supplier_name; 
        //订单号
        labOrderNo.Text = generalInfo.orderid;             
        //服务项目
        labproject_name.Text = generalInfo.project_descripttion;        
        //项目号
        labproject_code.Text = generalInfo.project_code;
        //负责人
        lab_endusername.Text = generalInfo.receivername;
        //附加收货人
        lab_AppendUser.Text = generalInfo.appendReceiverName;
        //业务组别
        lab_requestor_group.Text = generalInfo.ReceiverGroup;
       //服务内容
        labproject_descripttion.Text = generalInfo.thirdParty_materielDesc;
        //服务日期
        labintend_receipt_date.Text = DateTime.Today.ToString("yyyy-MM-dd");
        //历史收货
        List<RecipientInfo> recipientList = RecipientManager.getModelList(" and gid=" + generalInfo.id.ToString(), new List<SqlParameter>());
        decimal histtotal = 0;
        foreach (RecipientInfo recipientModel in recipientList)
        {
            if (recInfo.Id != recipientModel.Id && recipientModel.RecipientDate<recInfo.RecipientDate)
            {
                if (labRecipientLog.Text.Trim() != "")
                    labRecipientLog.Text += "<br />";
                labRecipientLog.Text += recipientModel.RecipientName + "于" + recipientModel.RecipientDate + "收货" + recipientModel.RecipientAmount.ToString("#,##0.####");
                histtotal += recipientModel.RecipientAmount;
            }
        }
        labHistTotal.Text = histtotal.ToString("#,##0.####");
        decimal totalprice = 0;
        
        foreach (OrderInfo o in orderList)
        {
            totalprice += o.total;
        }
        //总价
        if (!string.IsNullOrEmpty(Request["rec"]))
            lab_moneytype.Text = decimal.Parse(Request["rec"].ToString()).ToString("#,##0.####");

        //收货明细
        //单价
        labSinglePrice.Text = recInfo.SinglePrice;
        //数量
        labNum.Text = recInfo.Num;
        //内容
        labDes.Text = recInfo.Des;
        //备注
        labNote.Text = recInfo.Note;
        //收货单号
        labRecipientNo.Text = recInfo.RecipientNo;
    }
}
