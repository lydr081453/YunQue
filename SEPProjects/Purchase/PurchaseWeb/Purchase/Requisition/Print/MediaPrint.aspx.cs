using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_Print_MediaPrint : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ESP.Logging.Logger.Add(string.Format("查看/打印 查询条件为 {0} 媒介稿费报销单", Request["OrderID"].ToString()));

        if (!string.IsNullOrEmpty(Request["Type"]))//如果Type有值，说明该单据尚未被拆分(3000),orderid的值是orderinfo 的ID
        {
            //lblNewPRID.Visible = false;
            //lblNewPRNo.Visible = false;
            //lblNewFee.Visible = false;
            OrderInfo orderInfo = OrderInfoManager.GetModel(Convert.ToInt32(Request["OrderID"]));
            GeneralInfo generalInfo = GeneralInfoManager.GetModel(orderInfo.general_id);
            IList<ESP.Finance.Entity.ReturnInfo> returnModels = ESP.Finance.BusinessLogic.ReturnManager.GetList(" prid=" + generalInfo.id.ToString());
            List<MediaOrderInfo> orderList = MediaOrderManager.GetModelList("OrderID=" + Request["OrderID"]);
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

            //if (returnModels != null && returnModels.Count > 0)
            //    lblPN.Text = returnModels[0].ReturnCode;
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
                    if (relationModel != null && relationModel.NewPRId != null && relationModel.NewPRId != 0)
                    {
                        NewGeneralInfo = GeneralInfoManager.GetModel(relationModel.NewPRId.Value);
                        //lblNewPRID.Text = NewGeneralInfo.id.ToString();
                        //lblNewPRNo.Text = NewGeneralInfo.PrNo;
                        //lblNewFee.Text = NewGeneralInfo.totalprice.ToString("#,##0.00");
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
                    //if (returnModels != null && returnModels.Count > 0)
                    //    lblPN.Text = returnModels[0].ReturnCode;
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

            MediaOrderInfo item = (MediaOrderInfo)e.Item.DataItem;
            OrderInfo orderInfo = OrderInfoManager.GetModel(Convert.ToInt32(Request["OrderID"]));
            GeneralInfo generalModel = GeneralInfoManager.GetModel(orderInfo.general_id);

             Label lblCity = (Label)e.Item.FindControl("lblCity");
            Label lblMediaName = (Label)e.Item.FindControl("lblMediaName");
            Label lblBank = (Label)e.Item.FindControl("lblBank");
            Label lblTitle = (Label)e.Item.FindControl("lblTitle");

            lblTitle.Text = item.Subject;

            //if (item.CityName == "北京" && generalModel != null && generalModel.PRType == 1 && generalModel.id >= 20706)
            //{
                //decimal total = ESP.Purchase.BusinessLogic.MediaOrderManager.GetMediaAmount(item.OrderID.Value, " and bankaccountname='"+item.BankAccountName+"' and cityname='北京'");
                //if (item.Subject.IndexOf("北京") >= 0)
                //{
                //    lblTitle.Text = item.Subject.Replace("北京","");
                //}
            //    if (total >= 50000)
            //    {
            //        lblCity.Text = "深圳";
            //        lblMediaName.Text = "深圳特区报";
            //        lblBank.Text = "工行";
            //    }
            //    else if (total >= 45000 && total < 50000)
            //    {
            //        lblCity.Text = "上海";
            //        lblMediaName.Text = "申江服务导报";
            //        lblBank.Text = "建行";
            //    }
            //    else if (total >= 40000 && total < 45000)
            //    {
            //        lblCity.Text = "南京";
            //        lblMediaName.Text = "江苏商报";
            //        lblBank.Text = "招行";
            //    }
            //    else if (total >= 35000 && total < 40000)
            //    {
            //        lblCity.Text = "杭州";
            //        lblMediaName.Text = "浙江市场导报";
            //        lblBank.Text = "交行";
            //    }
            //    else if (total >= 30000 && total < 35000)
            //    {
            //        lblCity.Text = "成都";
            //        lblMediaName.Text = "华西都市报";
            //        lblBank.Text = "浦发";
            //    }
            //    else if (total >= 25000 && total < 30000)
            //    {
            //        lblCity.Text = "西安";
            //        lblMediaName.Text = "三秦都市报";
            //        lblBank.Text = "中行";
            //    }
            //    else if (total >= 20000 && total < 25000)
            //    {
                   
            //        if (generalModel.PrNo.IndexOf("PR1012") >= 0 || generalModel.PrNo.IndexOf("PR1101") >= 0)
            //        {
            //            lblCity.Text = "南京";
            //            lblMediaName.Text = "新民晚报";
            //            lblBank.Text = "民生银行";
                        
            //        }
            //        else if (generalModel.PrNo.IndexOf("PR1102") >= 0 || generalModel.PrNo.IndexOf("PR1103") >= 0 || generalModel.PrNo.IndexOf("PR1104") >= 0)
            //        {
            //            lblCity.Text = "南京";
            //            lblMediaName.Text = "南京晨报";
            //            lblBank.Text = "招商银行";
            //        }
            //        else if (generalModel.PrNo.IndexOf("PR1105") >= 0 || generalModel.PrNo.IndexOf("PR1106") >= 0 || generalModel.PrNo.IndexOf("PR1107") >= 0)
            //        {
            //            lblCity.Text = "上海";
            //            lblMediaName.Text = "东方早报";
            //            lblBank.Text = "中国工商银行";
            //        }
            //        else if (generalModel.PrNo.IndexOf("PR1108") >= 0 || generalModel.PrNo.IndexOf("PR1109") >= 0 || generalModel.PrNo.IndexOf("PR1110") >= 0)
            //        {
            //            lblCity.Text = "温州";
            //            lblMediaName.Text = "温州商报";
            //            lblBank.Text = "建设银行";
            //        }
            //        else
            //        {
            //            lblCity.Text = "深圳";
            //            lblMediaName.Text = "深圳商报";
            //            lblBank.Text = "交通银行";
            //        }
            //    }
            //    else if (total > 15000 && total < 20000)
            //    {
            //        lblCity.Text = "杭州";
            //        lblMediaName.Text = "杭州日报";
            //        lblBank.Text = "招行";
            //    }
            //    else
            //    {
            //        lblCity.Text = item.CityName;
            //        lblMediaName.Text = item.MediaName;
            //        lblBank.Text = item.BankName;
            //    }
            //}
            //else
            //{
                lblCity.Text = item.CityName;
                lblMediaName.Text = item.MediaName;
                lblBank.Text = item.BankName;
            //}


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
                    litSubTotal.Text = "<tr><td colspan='9' align='right' height='20px' class='f12pxGgray_right'><b>小计：" + subTotal.ToString("#,##0.####") + "</b></td></tr>";
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
                litEndSubTotal.Text = "<tr><td colspan='9' align='right' height='20px' class='f12pxGgray_right'><b>小计：" + subTotal.ToString("#,##0.####") + "</b></td></tr>";
            }
            preReceiverName = item.ReceiverName.Replace(" ", "");
            preBankaccountName = item.BankAccountName.Replace(" ", "");
            Total += item.TotalAmount.Value;
        }
    }
}
