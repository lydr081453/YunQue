using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Entity;
using System.Data;

namespace PurchaseWeb.Purchase.Requisition.Print
{
    public partial class AdvertisementPrint2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ESP.Logging.Logger.Add(string.Format("查看/打印 查询条件为 {0} 广告费用明细", Request["OrderID"].ToString()));

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
                    ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(item.auditorId);
                    strHist += "审批人:" + item.auditorName + "(" + emp.FullNameEN + ")" + " [" + Convert.ToDateTime(item.auditTime).ToString("yyyy-MM-dd") + "]" + "   " + item.auditRemark + "<br/>";
                }
                lblAuditHist.Text = strHist;
          
            
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

        static decimal Total = 0;
        static int listCount = 0;
        public void repItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label number = (Label)(e.Item.FindControl("labNum"));
                number.Text = (e.Item.ItemIndex + 1).ToString();

                ESP.Purchase.Entity.AdvertisementForOrderInfo model = (ESP.Purchase.Entity.AdvertisementForOrderInfo)e.Item.DataItem;
                Label lblDiscount = (Label)e.Item.FindControl("lblDiscount");
                if (lblDiscount != null)
                {
                    lblDiscount.Text = (100 -model.Discount).ToString();
                }
            }
        }


    }
}

