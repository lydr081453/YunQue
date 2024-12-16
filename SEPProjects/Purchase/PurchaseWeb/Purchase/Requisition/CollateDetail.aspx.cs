using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Purchase_Requisition_CollateDetail : ESP.Web.UI.PageBase
{
    int oldPRID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        oldPRID = int.Parse(Request["oldPRID"]);
        productInfo.CurrentUserId = CurrentUserID;
        if (!IsPostBack)
        {
            PageInit();
        }
    }

    private void PageInit()
    {
        ESP.Purchase.Entity.MediaPREditHisInfo predidHisInfo = ESP.Purchase.BusinessLogic.MediaPREditHisManager.GetModelByOldPRID(oldPRID);
        if (predidHisInfo != null)
        {
            initOldPR(predidHisInfo.OldPRId.Value);//原始申请单信息
            initNewPR(predidHisInfo.NewPRId != null ? predidHisInfo.NewPRId.Value : 0);//新申请单信息
            if (predidHisInfo.OldPRId != null && predidHisInfo.OldPRId.Value != 0)
            {
                down3000GV.DataSource = ESP.Finance.BusinessLogic.ReturnManager.GetList(" prid=" + predidHisInfo.OldPRId.Value);
                down3000GV.DataBind();
            }
            if (predidHisInfo.NewPRId != null && predidHisInfo.NewPRId.Value != 0)
            {
                newGV.DataSource = ESP.Finance.BusinessLogic.ReturnManager.GetList(" prid=" + predidHisInfo.NewPRId.Value);
                newGV.DataBind();
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CollateList.aspx");
    }

    private void initOldPR(int prId)
    {
        ESP.Purchase.Entity.GeneralInfo oldModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(prId);
        GenericInfo.Model = oldModel;
        GenericInfo.BindInfo();
        projectInfo.Model = oldModel;
        projectInfo.BindInfo();
        projectInfo.HideTabTitle();
        productInfo.Model = oldModel;
        
        productInfo.BindInfo();
    }

    private void initNewPR(int prId)
    {
        projectInfo1.HideTabTitle();
        ESP.Purchase.Entity.GeneralInfo newModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(prId);
        if (newModel != null)
        {
            GenericInfo1.Model = newModel;
            GenericInfo1.BindInfo();
            projectInfo1.Model = newModel;
            projectInfo1.BindInfo();
            productInfo1.Model = newModel;
            productInfo1.BindInfo();
        }
    }


    public string GetFormatPrice(decimal price)
    {
        return price.ToString("#,##0.00");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Response.Write(GetPrintContent());
    }

    private string GetPrintContent()
    {
        ESP.Purchase.Entity.MediaPREditHisInfo predidHisInfo = ESP.Purchase.BusinessLogic.MediaPREditHisManager.GetModelByOldPRID(oldPRID);
        string printContent = "<object ID='WebBrowser' WIDTH=0 HEIGHT=0 CLASSID='CLSID:8856F961-340A-11D0-A96B-00C04FD705A2'VIEWASTEXT></object>";
        printContent += "<script languge='Javascript'>WebBrowser.ExecWB(6,6); window.opener=null;window.close();</script>";
        if(predidHisInfo.NewPRId != null && predidHisInfo.NewPRId.Value != 0)
            printContent += GetPrHtml(predidHisInfo.NewPRId.Value);
        return printContent;
    }

    private string GetPrHtml(int generalId)
    {
        string hostUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["MainPage"].Replace("default.aspx", "");
        string url = hostUrl + "Purchase/Requisition/Print/RequisitionPrint.aspx?id=" + generalId + "&mail=yes";
        return ESP.ConfigCommon.SendMail.ScreenScrapeHtml(url);
    }

    protected void down3000GV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ESP.Finance.Entity.ReturnInfo model = (ESP.Finance.Entity.ReturnInfo)e.Row.DataItem;

            Label labExpectPaymentPrice = (Label)e.Row.FindControl("labExpectPaymentPrice");
            if (labExpectPaymentPrice != null && labExpectPaymentPrice.Text != string.Empty)
                labExpectPaymentPrice.Text = Convert.ToDecimal(labExpectPaymentPrice.Text).ToString("#,##0.00");
            Label lblPR = (Label)e.Row.FindControl("lblPR");
            if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.CommonPR)
                lblPR.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\ShowRequisitionDetail.aspx?GeneralID=" + model.PRID.ToString() + "'style='cursor: hand' target='_blank'>" + model.PRNo + "</a>";
            else
                lblPR.Text = model.PRNo;
            Label lblPN = (Label)e.Row.FindControl("lblPN");
            lblPN.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["FinanceServer"] + "Purchase\\ReturnDisplay.aspx?" + ESP.Finance.Utility.RequestName.ReturnID + "=" + model.ReturnID.ToString() + "'style='cursor: hand' target='_blank'>" + model.ReturnCode + "</a>";

            Label lblBeginDate = (Label)e.Row.FindControl("lblBeginDate");
            if (lblBeginDate != null && lblBeginDate.Text != string.Empty)
                lblBeginDate.Text = Convert.ToDateTime(lblBeginDate.Text).ToString("yyyy-MM-dd");
            Label lblEndDate = (Label)e.Row.FindControl("lblEndDate");
            if (lblEndDate != null && lblEndDate.Text != string.Empty)
                lblEndDate.Text = Convert.ToDateTime(lblEndDate.Text).ToString("yyyy-MM-dd");

            Label lblStatusName = (Label)e.Row.FindControl("lblStatusName");
            HiddenField hidStatusNameID = (HiddenField)e.Row.FindControl("hidStatusNameID");
            if (lblStatusName != null && hidStatusNameID != null && hidStatusNameID.Value != string.Empty)
                lblStatusName.Text = ESP.Finance.Utility.ReturnPaymentType.ReturnStatusString(Convert.ToInt32(hidStatusNameID.Value), 0, model.IsDiscount);

            Label lblName = (Label)e.Row.FindControl("lblName");
            lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(model.RequestorID.Value) + "');");

            LinkButton lnkMail = (LinkButton)e.Row.FindControl("lnkMail");
            if (model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete)
            {
                lnkMail.Visible = true;
            }
            else
            {
                lnkMail.Visible = false;
            }
        }
    }

    protected void down3000GV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Mail")
        {
            int returnID = int.Parse(e.CommandArgument.ToString());
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnID);
            try
            {
                ESP.Finance.Utility.SendMailHelper.SendMediaToOriginal(returnModel);
            }
            catch { }

        }
    }
    protected void newGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Mail")
        {
            int returnID = int.Parse(e.CommandArgument.ToString());
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnID);
            try
            {
                ESP.Finance.Utility.SendMailHelper.SendMediaToOriginal(returnModel);
            }
            catch { }
        }
    }

    protected void newGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ESP.Finance.Entity.ReturnInfo model = (ESP.Finance.Entity.ReturnInfo)e.Row.DataItem;

            Label labExpectPaymentPrice = (Label)e.Row.FindControl("labExpectPaymentPrice");
            if (labExpectPaymentPrice != null && labExpectPaymentPrice.Text != string.Empty)
                labExpectPaymentPrice.Text = Convert.ToDecimal(labExpectPaymentPrice.Text).ToString("#,##0.00");
            Label lblPR = (Label)e.Row.FindControl("lblPR");
            if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.CommonPR)
                lblPR.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\ShowRequisitionDetail.aspx?GeneralID=" + model.PRID.ToString() + "'style='cursor: hand' target='_blank'>" + model.PRNo + "</a>";
            else
                lblPR.Text = model.PRNo;
            Label lblPN = (Label)e.Row.FindControl("lblPN");
            lblPN.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["FinanceServer"] + "Purchase\\ReturnDisplay.aspx?" + ESP.Finance.Utility.RequestName.ReturnID + "=" + model.ReturnID.ToString() + "'style='cursor: hand' target='_blank'>" + model.ReturnCode + "</a>";
            Label lblBeginDate = (Label)e.Row.FindControl("lblBeginDate");
            if (lblBeginDate != null && lblBeginDate.Text != string.Empty)
                lblBeginDate.Text = Convert.ToDateTime(lblBeginDate.Text).ToString("yyyy-MM-dd");
            Label lblEndDate = (Label)e.Row.FindControl("lblEndDate");
            if (lblEndDate != null && lblEndDate.Text != string.Empty)
                lblEndDate.Text = Convert.ToDateTime(lblEndDate.Text).ToString("yyyy-MM-dd");

            Label lblStatusName = (Label)e.Row.FindControl("lblStatusName");
            HiddenField hidStatusNameID = (HiddenField)e.Row.FindControl("hidStatusNameID");
            if (lblStatusName != null && hidStatusNameID != null && hidStatusNameID.Value != string.Empty)
                lblStatusName.Text = ESP.Finance.Utility.ReturnPaymentType.ReturnStatusString(Convert.ToInt32(hidStatusNameID.Value),0, model.IsDiscount);

            Label lblName = (Label)e.Row.FindControl("lblName");
            lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(model.RequestorID.Value) + "');");

            LinkButton lnkMail = (LinkButton)e.Row.FindControl("lnkMail");
            if (model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete)
            {
                lnkMail.Visible = true;
            }
            else
            {
                lnkMail.Visible = false;
            }
        }
    }
}
