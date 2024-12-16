using System;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_OrderDetail : ESP.Purchase.WebPage.ViewPageForPR
{
    int generalid = 0;

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
        {
            generalid = int.Parse(Request[RequestName.GeneralID]);
            productInfo.CurrentUserId = CurrentUserID;
            //productInfo.ItemListBind(" general_id = " + generalid);
           
        }
        BindInfo();

        tabOverrule.Visible = false;
        if (!string.IsNullOrEmpty(Request["type"]) && Request["type"].ToString().Equals("audit"))
        {
            btnOk.Visible = true;
            btnNo.Visible = true;
            projectInfo.IsEditPage = true;
            tabOverrule.Visible = true;
        }
        if (!string.IsNullOrEmpty(Request["type"]) && Request["type"].ToString().Equals("auditR"))
        {
            btnOk.Visible = true;
            btnNo.Visible = true;
            projectInfo.IsEditPage = true;
            tabOverrule.Visible = true;            
        }
        if (!string.IsNullOrEmpty(Request["vis"]) && Request["vis"] == "false")
        {
            //
        }

        btnOk.Attributes["onclick"] = "javascript:if(Page_ClientValidate()) {if(window.confirm('您确定要审批通过吗？')) {this.disabled=true} else {return false;}}";
        btnNo.Attributes["onclick"] = "javascript:if(Page_ClientValidate()) {if(window.confirm('您确定要审批驳回吗？')) {this.disabled=true} else {return false;}}";  

    }

    /// <summary>
    /// Binds the info.
    /// </summary>
    public void BindInfo()
    {
        GeneralInfo g = GeneralInfoManager.GetModel(generalid);
        if (null != g)
        {

            GenericInfo.Model = g;
            GenericInfo.BindInfo();

            projectInfo.Model = g;
           
            projectInfo.BindInfo();

            supplierInfo.Model = g;
            supplierInfo.BindInfo();

            RequirementDescInfo.BindInfo(g);
            labdownContrast.Text = g.contrastFile == "" ? "" : "<a target='_blank' href='/Purchase/Requisition/PrFileDownLoad.aspx?GeneralId=" + g.id.ToString() + "&Index=0&Type=Contrast'><img src='/images/ico_04.gif' border='0' /></a>";
            labdownConsult.Text = g.consultFile == "" ? "" : "<a target='_blank' href='/Purchase/Requisition/PrFileDownLoad.aspx?GeneralId=" + g.id.ToString() + "&Index=0&Type=Consult'><img src='/images/ico_04.gif' border='0' /></a>";
            txtorderid.Text = g.orderid;
            txttype.Text = g.type;
            txtcontrast.Text = g.contrast;
            txtconsult.Text = g.consult;

            if (g.PRType != (int)ESP.Purchase.Common.PRTYpe.MediaPR && g.PRType != (int)ESP.Purchase.Common.PRTYpe.MPPR && g.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA && g.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_PriFA && g.PRType != (int)ESP.Purchase.Common.PRTYpe.ADPR)
            {
                ddlfirst_assessor.Text = g.first_assessorname;
                ddlfirst_assessor.Attributes["onclick"] = "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(g.first_assessor) + "');";
            }
            
            labafterwards.Text = g.afterwardsname;
            if (g.afterwardsname == "是")
                labafterwardsReason.Text = "理由：" + g.afterwardsReason;
            labEmBuy.Text = g.EmBuy;
            if (g.EmBuy == "是")
                labEmBuyReason.Text = "理由：" + g.EmBuyReason;
            labCusAsk.Text = g.CusAsk;
            if (g.CusAsk == "是")
            {
                labCusName.Text = "客户名称：" + g.CusName;
                labCusAskYesReason.Text = "理  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;由：" + g.CusAskYesReason;
            }

            txtothers.Text = g.others;
            labContractNo.Text = g.ContractNo;

            productInfo.Model = g;
            productInfo.BindInfo();
            paymentInfo.Model = g;
            paymentInfo.BindInfo();
            paymentInfo.TotalPrice = productInfo.TotalPrice;

            if(g.PRType == (int)PRTYpe.MediaPR && g.status == State.order_mediaAuditYes)
            {
                tabMedia.Visible = true;
                litprMediaRemark.Text = g.prMediaRemark;
            }
        }
    }

    /// <summary>
    /// Handles the Click event of the btn control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btn_Click(Object sender, EventArgs e)
    {
        Response.Redirect(Request["backUrl"] == null ? "OrderList.aspx" : Request["backUrl"].ToString());
    }

    /// <summary>
    /// Handles the Click event of the btnOk control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnOk_Click(object sender, EventArgs e)
    {
        GeneralInfo g = GeneralInfoManager.GetModel(generalid);
        g.status = State.order_ok;
        g.order_overrule = txtoverrule.Text;
        string exMail = string.Empty;
        try
        {
            if (g.Requisitionflow == State.requisitionflow_toO)
            {
                g.order_committime = DateTime.Now;
                g.orderid = g.PrNo.Replace("PR", "PO");
            }
            g.order_audittime = DateTime.Now;
            GeneralInfoDataProvider.Update(g);
            LogInfo log = new LogInfo();
            log.Gid = g.id;
            log.LogMedifiedTeme = DateTime.Now;
            log.LogUserId = CurrentUserID;

            log.Des = string.Format(State.log_order_commit, CurrentUserName + "(" + CurrentUser.ITCode + ")", DateTime.Now.ToString());
            LogManager.AddLog(log, Request);
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核通过失败!');", true);
        }
        try
        {
            string mail = State.getEmployeeEmailBySysUserId(g.Filiale_Auditor > 0 ? g.Filiale_Auditor : g.first_assessor);

            string ret = ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPRGen(g, g.PrNo, State.getEmployeeEmailBySysUserId(g.requestor), mail, true);
          
        }
        catch 
        {
            exMail = "(邮件发送失败!)";
        }
        ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + g.orderid + "订单审核通过!"+exMail+"');window.location='AuditRequistion.aspx'", true);
    }

    /// <summary>
    /// Handles the Click event of the btnAudit control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnAudit_Click(object sender, EventArgs e)
    {
        if (hidState.Value == "ok")
        {
            btnOk_Click(sender, e);
        }
        else if (hidState.Value == "no")
        {
            btnNo_Click(sender, e);
        }
    }

    /// <summary>
    /// Handles the Click event of the btnNo control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnNo_Click(object sender, EventArgs e)
    {
        GeneralInfo g = GeneralInfoManager.GetModel(generalid);
        g.order_overrule = txtoverrule.Text;
        g.status = State.order_return;
        string orderNo = g.orderid;
        g.orderid = "";
        string exMail = string.Empty;
        try
        {
            GeneralInfoDataProvider.Update(g);

            LogInfo log = new LogInfo();
            log.Gid = g.id;
            log.LogMedifiedTeme = DateTime.Now;
            log.LogUserId = CurrentUserID;
            log.Des = string.Format(State.log_order_return, CurrentUserName + "(" + CurrentUser.ITCode + ")", DateTime.Now.ToString());
            LogManager.AddLog(log, Request);
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核驳回失败!');", true);
        }
        try
        {
            string ret = ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPRGen(g, g.PrNo, "", State.getEmployeeEmailBySysUserId(g.first_assessor), false);
        }
        catch
        {
            exMail = "(邮件发送失败!)";
        }
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + orderNo + "订单审核驳回!"+exMail+"');window.location='AuditRequistion.aspx'", true);

    }

    protected void btnOrderExport_Click(object sender, EventArgs e)
    {
        ExportToOrderInfoExcel(generalid);
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportToGeneralInfoExcel(generalid);

    }

    protected void ExportToOrderInfoExcel(int id)
    {
        FileHelper.ToOrderInfoExcel(id, Server.MapPath("~"), Response);
        GC.Collect();
    }

    protected void ExportToGeneralInfoExcel(int id)
    {
        FileHelper.ToGeneralInfoExcel(id, Server.MapPath("~"), Response);
        GC.Collect();
    }


}