using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Entity;

public partial class AdvertisementPrint : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ESP.Logging.Logger.Add(string.Format("查看/打印 查询条件为 {0} 广告费用明细", Request["OrderID"].ToString()));

        if (!string.IsNullOrEmpty(Request["Type"]))//如果Type有值，说明该单据尚未被拆分(3000),orderid的值是orderinfo 的ID
        {
            lblNewPRID.Visible = false;
            lblNewPRNo.Visible = false;
            lblNewFee.Visible = false;
            OrderInfo orderInfo = OrderInfoManager.GetModel(Convert.ToInt32(Request["OrderID"]));
            Total = orderInfo.total;
            GeneralInfo generalInfo = GeneralInfoManager.GetModel(orderInfo.general_id);
            IList<ESP.Finance.Entity.ReturnInfo> returnModels = ESP.Finance.BusinessLogic.ReturnManager.GetList(" prid=" + generalInfo.id.ToString());
            IList<AdvertisementForOrderInfo> orderList = AdvertisementForOrderManager.GetInfoList("OrderID=" + Request["OrderID"]);
            listCount = orderList.Count;
            repItem.DataSource = orderList;
            repItem.DataBind();
            litTotal.Text = "<tr><td colspan='9' height='20px' align='right' class='f12pxGgray_right'><b>合计：" + Total.ToString("#,##0.####") + "</b></td></tr>";

            lab_buggeted.Text = generalInfo.buggeted.ToString("#,##0.00");
            lab_project_code.Text = generalInfo.project_code;
            lab_project_descripttion.Text = generalInfo.project_descripttion;
            lab_requestorContacts.Text = generalInfo.requestor_info;
            lab_requestorDate.Text = generalInfo.requisition_committime.ToString("yyyy-MM-dd");
            lab_requestorGroup.Text = generalInfo.requestor_group;
            lab_requestorName.Text = generalInfo.requestorname;
            labPrno.Text = generalInfo.PrNo;

            if (returnModels != null && returnModels.Count > 0)
                lblPN.Text = returnModels[0].ReturnCode;
            //日志
            string strHist = string.Empty;
            List<OperationAuditLogInfo> auditHists = OperationAuditLogManager.GetLoglistByGId(generalInfo.id);
            List<LogInfo> loglist = LogManager.GetLoglistWithFinance(generalInfo.id);
            foreach (LogInfo log in loglist)
            {
                strHist += log.Des + "<br/>";
            }

            foreach (OperationAuditLogInfo item in auditHists)
            {
                ESP.Framework.Entity.EmployeeInfo emp=ESP.Framework.BusinessLogic.EmployeeManager.Get(item.auditorId);
                strHist += "审批人:" + item.auditorName+"("+emp.FullNameEN+")" + " [" + Convert.ToDateTime(item.auditTime).ToString("yyyy-MM-dd") + "]" + "   " + item.auditRemark + "<br/>";
            }
            lblAuditHist.Text = strHist;
        }
        else//Request的orderid传入值为MediaOrderID的字符串，则先从MediaOrder表获取，该单据已被拆分
        {
            if (!string.IsNullOrEmpty(Request["OrderID"]))
            {
                string orderids = Request["OrderID"].TrimEnd(',');
                List<MediaOrderInfo> orderList = MediaOrderManager.GetModelList("MeidaOrderID in(" + orderids + ")");
                if (orderList != null && orderList.Count > 0)
                {
                    OrderInfo orderInfo = OrderInfoManager.GetModel(orderList[0].OrderID.Value);
                    GeneralInfo generalInfo = GeneralInfoManager.GetModel(orderInfo.general_id);
                    MediaPREditHisInfo relationModel = MediaPREditHisManager.GetModelByOldPRID(generalInfo.id);
                    GeneralInfo NewGeneralInfo = null;
                    if (relationModel!=null && relationModel.NewPRId != null && relationModel.NewPRId != 0)
                    {
                        NewGeneralInfo = GeneralInfoManager.GetModel(relationModel.NewPRId.Value);
                        lblNewPRID.Text = NewGeneralInfo.id.ToString();
                        lblNewPRNo.Text = NewGeneralInfo.PrNo;
                        lblNewFee.Text = NewGeneralInfo.totalprice.ToString("#,##0.00");
                    }
                    IList<ESP.Finance.Entity.ReturnInfo> returnModels = ESP.Finance.BusinessLogic.ReturnManager.GetList(" prid=" + generalInfo.id.ToString());
                    listCount = orderList.Count;
                    repItem.DataSource = orderList;
                    repItem.DataBind();
                    litTotal.Text = "<tr><td colspan='9' height='20px' align='right' class='f12pxGgray_right'><b>合计：" + Total.ToString("#,##0.####") + "</b></td></tr>";

                    lab_buggeted.Text = generalInfo.buggeted.ToString("#,##0.00");
                    lab_project_code.Text = generalInfo.project_code;
                    lab_project_descripttion.Text = generalInfo.project_descripttion;
                    lab_requestorContacts.Text = generalInfo.requestor_info;
                    lab_requestorDate.Text = generalInfo.requisition_committime.ToString("yyyy-MM-dd");
                    lab_requestorGroup.Text = generalInfo.requestor_group;
                    lab_requestorName.Text = generalInfo.requestorname;
                    labPrno.Text = generalInfo.PrNo;
                    if (returnModels != null && returnModels.Count > 0)
                        lblPN.Text = returnModels[0].ReturnCode;
                    //日志
                    string strHist = string.Empty;
                    List<OperationAuditLogInfo> auditHists = OperationAuditLogManager.GetLoglistByGId(generalInfo.id);
                    List<LogInfo> loglist = LogManager.GetLoglistWithFinance(generalInfo.id);
                    foreach (LogInfo log in loglist)
                    {
                        strHist += log.Des + "<br/>";
                    }

                    foreach (OperationAuditLogInfo item in auditHists)
                    {
                        ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(item.auditorId);
                        strHist += "审批人:" + item.auditorName + "(" + emp.FullNameEN + ")" + " [" + Convert.ToDateTime(item.auditTime).ToString("yyyy-MM-dd") + "]" + "   " + item.auditRemark + "\n";
                    }
                    lblAuditHist.Text = strHist;
                }
            }
        }
    }

    private decimal getMediaAvgPrice(int mediaID)
    {
        decimal avgPrice = 0;
        List<MediaOrderInfo> medialist = MediaOrderManager.GetModelList("mediaid=" + mediaID.ToString());
        if (medialist == null || medialist.Count == 0)
            return 0;
        else
        {
            int totalWordLength = 0;
            decimal totalAmount = 0;
            foreach (MediaOrderInfo item in medialist)
            {
                totalWordLength += item.WordLength == null ? 0 : item.WordLength.Value;
                totalAmount += item.TotalAmount == null ? 0 : item.TotalAmount.Value;
            }
            if (totalWordLength == 0)
                return 0;
            else
                avgPrice = totalAmount / totalWordLength;
        }
        return avgPrice;
    }

    string preReceiverName = "";
    string preBankaccountName = "";
    decimal subTotal = 0;
    static decimal Total = 0;
    static int listCount = 0;
    public void repItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
        //    MediaOrderInfo item = (MediaOrderInfo)e.Item.DataItem;
            Label number = (Label)(e.Item.FindControl("labNum"));
            number.Text = (e.Item.ItemIndex + 1).ToString();
        //    Label lblReleaseDate = (Label)e.Item.FindControl("lblReleaseDate");
        //    Label lblAvgPrice = (Label)e.Item.FindControl("lblAvgPrice");
        //    lblAvgPrice.Text = getMediaAvgPrice(item.MediaID.Value).ToString("#,##0.00");
        //    Label lblUnitPrice = (Label)e.Item.FindControl("lblUnitPrice");
        //    decimal projectAvgPrice = 0;
        //    lblReleaseDate.Text = item.ReleaseDate == null ? "" : item.ReleaseDate.Value.ToString("yyyy-MM-dd");
        //    if (item.WordLength == null || item.WordLength == 0)
        //    {
        //        projectAvgPrice = 0;
        //    }
        //    else
        //    {
        //        projectAvgPrice = item.TotalAmount.Value / (item.WordLength.Value);
        //    }
        //    lblUnitPrice.Text = projectAvgPrice.ToString("#,##0.00");

        //    if (e.Item.ItemIndex != 0)
        //    {
        //        if (item.ReceiverName.Replace(" ", "") != preReceiverName || item.BankAccountName.Replace(" ", "") != preBankaccountName)
        //        {
        //            Literal litSubTotal = (Literal)e.Item.FindControl("litSubTotal");
        //            litSubTotal.Text = "<tr><td colspan='9' align='right' height='20px' class='f12pxGgray_right'><b>小计：" + subTotal.ToString("#,##0.####") + "</b></td></tr>";
        //            subTotal = item.TotalAmount.Value;
        //        }
        //        else
        //        {
        //            subTotal += item.TotalAmount.Value;
        //        }
        //    }
        //    else
        //    {
        //        Total = 0;
        //        subTotal = item.TotalAmount.Value;
        //    }
        //    if (e.Item.ItemIndex == (listCount - 1))
        //    {
        //        Literal litEndSubTotal = (Literal)e.Item.FindControl("litEndSubTotal");
        //        litEndSubTotal.Text = "<tr><td colspan='9' align='right' height='20px' class='f12pxGgray_right'><b>小计：" + subTotal.ToString("#,##0.####") + "</b></td></tr>";
        //    }
        //    preReceiverName = item.ReceiverName.Replace(" ", "");
        //    preBankaccountName = item.BankAccountName.Replace(" ", "");
        //    Total += item.TotalAmount.Value;
        }
    }
}
