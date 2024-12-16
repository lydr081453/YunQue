using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.Entity;
using ESP.Purchase.BusinessLogic;

namespace FinanceWeb.Purchase.Print
{
    public partial class MediaUnPayment : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
               if (!string.IsNullOrEmpty(Request["OrderID"]))
                {
                    string orderids = Request["OrderID"].TrimEnd(',');
                    List<MediaOrderInfo> orderList = MediaOrderManager.GetModelList("MeidaOrderID in(" + orderids + ") and (ispayment=0 or ispayment is null)");
                    if (orderList != null && orderList.Count > 0)
                    {
                        OrderInfo orderInfo = OrderInfoManager.GetModel(orderList[0].OrderID.Value);
                        GeneralInfo generalInfo = GeneralInfoManager.GetModel(orderInfo.general_id);
                        MediaPREditHisInfo relationModel = MediaPREditHisManager.GetModelByOldPRID(generalInfo.id);
                        GeneralInfo NewGeneralInfo = null;
                        if (relationModel != null && relationModel.NewPRId != null && relationModel.NewPRId != 0)
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
                        litTotal.Text = "<tr><td colspan='9' height='20px' align='right' class='f12pxGgray_right'><b>合计：" + Total.ToString("#,##0.00") + "</b></td></tr>";

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
                        List<LogInfo> loglist = LogManager.GetLoglistByGId(generalInfo.id);
                        foreach (LogInfo log in loglist)
                        {
                            strHist += log.Des + "<br/>";
                        }

                        foreach (OperationAuditLogInfo item in auditHists)
                        {
                            strHist += "审批人:" + item.auditorName + " [" + Convert.ToDateTime(item.auditTime).ToString("yyyy-MM-dd") + "]" + "   " + item.auditRemark + "\n";
                        }
                        lblAuditHist.Text = strHist;
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
                MediaOrderInfo item = (MediaOrderInfo)e.Item.DataItem;
                Label number = (Label)(e.Item.FindControl("labNum"));
                number.Text = (e.Item.ItemIndex + 1).ToString();
                Label lblReleaseDate = (Label)e.Item.FindControl("lblReleaseDate");
                Label lblAvgPrice = (Label)e.Item.FindControl("lblAvgPrice");
                lblAvgPrice.Text = getMediaAvgPrice(item.MediaID.Value).ToString("#,##0.00");
                Label lblUnitPrice = (Label)e.Item.FindControl("lblUnitPrice");
                decimal projectAvgPrice = 0;
                lblReleaseDate.Text = item.ReleaseDate == null ? "" : item.ReleaseDate.Value.ToString("yyyy-MM-dd");
                if (item.WordLength == null || item.WordLength == 0)
                {
                    projectAvgPrice = 0;
                }
                else
                {
                    projectAvgPrice = item.TotalAmount.Value / (item.WordLength.Value);
                }
                lblUnitPrice.Text = projectAvgPrice.ToString("#,##0.00");

                if (e.Item.ItemIndex != 0)
                {
                    if (item.ReceiverName.Replace(" ", "") != preReceiverName || item.BankAccountName.Replace(" ", "") != preBankaccountName)
                    {
                        Literal litSubTotal = (Literal)e.Item.FindControl("litSubTotal");
                        litSubTotal.Text = "<tr><td colspan='9' align='right' height='20px' class='f12pxGgray_right'><b>小计：" + subTotal.ToString("#,##0.00") + "</b></td></tr>";
                        subTotal = item.TotalAmount.Value;
                    }
                    else
                    {
                        subTotal += item.TotalAmount.Value;
                    }
                }
                else
                {
                    Total = 0;
                    subTotal = item.TotalAmount.Value;
                }
                if (e.Item.ItemIndex == (listCount - 1))
                {
                    Literal litEndSubTotal = (Literal)e.Item.FindControl("litEndSubTotal");
                    litEndSubTotal.Text = "<tr><td colspan='9' align='right' height='20px' class='f12pxGgray_right'><b>小计：" + subTotal.ToString("#,##0.00") + "</b></td></tr>";
                }
                preReceiverName = item.ReceiverName.Replace(" ", "");
                preBankaccountName = item.BankAccountName.Replace(" ", "");
                Total += item.TotalAmount.Value;
            }
        }
    }
}
