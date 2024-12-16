using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_ConfirmRecipient : System.Web.UI.Page
{
    int id = 0;
    decimal recamount = 0;
    int reccount = 0;
    string moneytype = "";
    int recipientId;

    protected void Page_Load(object sender, EventArgs e)
    {
        logoPo.ImageUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["PortalSite"] + "/images/rec.gif";
        if (!IsPostBack)
        {
            ESP.ConfigCommon.SymmetricCrypto crypto = new ESP.ConfigCommon.SymmetricCrypto();
            try
            {
                string urlPara = crypto.DecrypString(Request["Content"].ToString());
                string[] req = urlPara.Split(';');
                if (req.Length > 0)
                {
                    id = int.Parse(req[0]);
                    recamount = decimal.Parse(req[1]);
                    reccount = req.Length > 2 ? int.Parse(req[2]) : 0;
                    recipientId = req.Length > 2 ? int.Parse(req[3]) : 0;
                    initPrintPage();
                }
            }
            catch (Exception ex)
            {
                //
            }
        }
    }

    private void initPrintPage()
    {
        GeneralInfo generalInfo = new GeneralInfo();
        generalInfo = GeneralInfoManager.GetModel(id);

        RecipientInfo recInfo = RecipientManager.GetModel(recipientId);

        ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(generalInfo.project_code.Substring(0, 1));

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
        //业务组别
        lab_requestor_group.Text = generalInfo.ReceiverGroup;
        //服务内容
        labproject_descripttion.Text = generalInfo.thirdParty_materielDesc;
        //服务日期
        labintend_receipt_date.Text = DateTime.Today.ToString("yyyy-MM-dd");

        decimal totalprice = 0;

        foreach (OrderInfo o in orderList)
        {
            totalprice += o.total;
        }
        //总价
        lab_moneytype.Text = recamount.ToString("#,##0.00");

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

        int currentIsConfirm = recInfo.IsConfirm;
        //if (recInfo.IsConfirm != State.recipentConfirm_Supplier && recInfo.IsConfirm != State.recipentConfirm_Payment && recInfo.IsConfirm != State.recipentConfirm_PaymentCommit)
        //{
        if (RecipientManager.updateConfirm(recipientId, generalInfo, reccount, true, Request, 0))
        {
            labConfirm.Text = "收货已确认！";
            if (currentIsConfirm != State.recipentConfirm_Supplier && currentIsConfirm != State.recipentConfirm_PaymentCommit && currentIsConfirm != State.recipentConfirm_PaymentCommit)
                try
                {
                    ESP.Purchase.BusinessLogic.SendMailHelper.SendPORecConfim(generalInfo, generalInfo.orderid, State.getEmployeeEmailBySysUserId(generalInfo.requestor));
                }
                catch { }
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('收货确认失败！');", true);
        }
        //}
        //else
        //{
        //    labConfirm.Text = "收货已确认！";
        //}
    }
}
