using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections.Generic;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_ShowRequisitionDetail : ESP.Purchase.WebPage.ViewPageForPR
{
    private int generalid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (string.IsNullOrEmpty(Request.QueryString[RequestName.GeneralID]))
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('请选择基础数据！');window.location='frmAddGeneral.aspx'", true);
        }
        else
        {
            Session[RequestName.GeneralID] = Request.QueryString[RequestName.GeneralID];
            productInfo.CurrentUserId = CurrentUserID;
            generalid = int.Parse(Request[RequestName.GeneralID]);
           
        }

        if (!IsPostBack)
        {
           // productInfo.ItemListBind(" general_id = " + Session[RequestName.GeneralID]);
            BindInfo();
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportToGeneralInfoExcel();
    }

    public void BindInfo()
    {
        GeneralInfo g = GeneralInfoManager.GetModel(int.Parse(Session[RequestName.GeneralID].ToString()));
        if (null != g)
        {
            GenericInfo.Model = g;
            GenericInfo.BindInfo();
            projectInfo.Model = g;
            projectInfo.BindInfo();
            supplierInfo.Model = g;
            supplierInfo.BindInfo();
            RequirementDescInfo.BindInfo(g);

            paymentInfo.Model = g;
            paymentInfo.BindInfo();

            productInfo.Model = g;
           
            productInfo.BindInfo();
        }

        paymentInfo.TotalPrice = productInfo.TotalPrice;
    }

    protected void ExportToGeneralInfoExcel()
    {
        FileHelper.ToGeneralInfoExcel(generalid, Server.MapPath("~"), Response);
        GC.Collect();
    }

    protected void BackUrl_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[RequestName.BackUrl]))
        {
            Response.Redirect(Request[RequestName.BackUrl]);
        }
        else
        {
            if (string.IsNullOrEmpty(Request["pageUrl"]))
                Response.Redirect("RequisitionSaveList.aspx");
            else
                Response.Redirect(Request["pageUrl"].ToString());
        }
    }
}