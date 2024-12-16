using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Purchase_Requisition_Print_CollatePrint : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            labText.Text = GetPrintContent();
        }
    }

    private string GetPrintContent()
    {
        int oldPRID = int.Parse(Request["oldPRID"]);
        string printContent = "";
        ESP.Purchase.Entity.MediaPREditHisInfo predidHisInfo = ESP.Purchase.BusinessLogic.MediaPREditHisManager.GetModelByOldPRID(oldPRID);

        if (predidHisInfo.NewPRId != null && predidHisInfo.NewPRId.Value != 0)
        {
            IList<ESP.Finance.Entity.ReturnInfo> paymentList = ESP.Finance.BusinessLogic.ReturnManager.GetList(" prid=" + predidHisInfo.NewPRId.Value.ToString());
            printContent += GetPrHtml(predidHisInfo.NewPRId.Value);//3000以上生成PR单
            printContent += "<p style=\"page-break-after:always\">&nbsp;</p>";
            if (paymentList != null && paymentList.Count > 0)
                foreach (ESP.Finance.Entity.ReturnInfo payment in paymentList)
                {
                    printContent += GetPnHtml(payment.ReturnID);//3000以上生成PR单生成的PN单
                }
            printContent += GetFileHtml(predidHisInfo.NewPRId.Value);
        }
        //记录打印信息
        ESP.Purchase.Entity.PrintLogInfo printLog = ESP.Purchase.BusinessLogic.PrintLogManager.GetModelByFormID(oldPRID);
        if (printLog == null)
            printLog = new ESP.Purchase.Entity.PrintLogInfo();
        printLog.FormID = oldPRID;
        printLog.FormType = 1;
        printLog.PrintCount = (printLog.PrintCount == null ? 0 : printLog.PrintCount.Value) + 1;
        if (printLog.PrintID != 0)
            ESP.Purchase.BusinessLogic.PrintLogManager.Update(printLog);
        else
            ESP.Purchase.BusinessLogic.PrintLogManager.Add(printLog);
        return printContent + "<script>window.print();</script>";
    }

    private string GetPrHtml(int generalId)
    {
        string hostUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["MainPage"].Replace("default.aspx", "");
        string url = "";
        string content = "";
        url = hostUrl + "Purchase/Requisition/Print/RequisitionPrint.aspx?id=" + generalId + "&viewButton=no";
        content += ESP.ConfigCommon.SendMail.ScreenScrapeHtml(url);
        if (ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(generalId).PRType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA)
        {
            content += "<p style=\"page-break-after:always\">&nbsp;</p>";
            url = hostUrl + "Purchase/Requisition/Print/RequisitionPrint.aspx?id=" + Request["oldPRID"] + "&viewButton=no&Action=ViewOldPr";
            content += ESP.ConfigCommon.SendMail.ScreenScrapeHtml(url);
        }
        return content;
    }

    private string GetPnHtml(int generalId)
    {
        string hostUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["MainPage"].Replace("default.aspx", "");
        string url = hostUrl + "Purchase/Requisition/Print/PaymantPrint.aspx?" + ESP.Finance.Utility.RequestName.ReturnID + "=" + generalId + "&viewButton=no";
        return ESP.ConfigCommon.SendMail.ScreenScrapeHtml(url);
    }

    private string GetFileHtml(int generalId)
    {
        ESP.Purchase.Entity.GeneralInfo model = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(generalId);
        if (model.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_PriFA)//个人单不需要打印该项
        {
            // IList<ESP.Finance.Entity.ReturnInfo> ReturnModels = ESP.Finance.BusinessLogic.ReturnManager.GetList(" prid="+model.id.ToString());
            if (!string.IsNullOrEmpty(model.NewMediaOrderIDs))
            {
                string hostUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["MainPage"].Replace("default.aspx", "");
                string url = hostUrl + "Purchase/Requisition/Print/MediaPrint.aspx?OrderID=" + model.NewMediaOrderIDs;
                try
                {
                    bottomButton.Visible = false;
                    return "<p style=\"page-break-after:always\">&nbsp;</p>" + ESP.ConfigCommon.SendMail.ScreenScrapeHtml(url);
                }
                catch { return ""; }
            }
            else
            {
                string hostUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["MainPage"].Replace("default.aspx", "");
                if (model.sow2.Trim() != "")
                {
                    string url = hostUrl + model.sow2;

                    try
                    {
                        bottomButton.Visible = false;
                        return "<p style=\"page-break-after:always\">&nbsp;</p>" + ESP.ConfigCommon.SendMail.ScreenScrapeHtml(url);
                    }
                    catch { return ""; }
                }
            }
        }
        return "";
    }
}
