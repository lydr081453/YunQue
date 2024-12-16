using System;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

public partial class prADAudit : ESP.Purchase.WebPage.ViewPageForPR
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
           // productInfo.ItemListBind(" general_id = " + generalid);
           
        }
        if (!IsPostBack)
        {
            BindInfo();
        }

        if (!string.IsNullOrEmpty(Request["type"]) && Request["type"].ToString().Equals("audit"))
        {
            btnOk.Visible = true;
            btnNo.Visible = true;
        }
        if (!string.IsNullOrEmpty(Request["type"]) && Request["type"].ToString().Equals("auditR"))
        {
            btnOk.Visible = true;
            btnNo.Visible = true;
        }
        if (!string.IsNullOrEmpty(Request["vis"]) && Request["vis"] == "false")
        {
            //
        }

        btnOk.Attributes["onclick"] = "javascript:if(window.confirm('您确定要审批通过吗？')) {this.disabled=true} else {return false;}";
        btnNo.Attributes["onclick"] = "javascript:if(window.confirm('您确定要审批驳回吗？')) {this.disabled=true} else {return false;}";

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
            //txtpayment_terms.Text = g.payment_terms;
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
            if (g.fili_overrule != "")
            {
                palFili.Visible = true;
                labFili.Text = g.fili_overrule;
            }
            if (g.order_overrule != "")
            {
                palOverrule.Visible = true;
                labOverrule.Text = g.order_overrule;
            }
            if (g.requisition_overrule != "")
            {
                palOverrulP.Visible = true;
                labOverruleP.Text = g.requisition_overrule;
            }
            lablasttime.Text = g.lasttime.ToString();
            labrequisition_committime.Text = g.requisition_committime.ToString() == State.datetime_minvalue ? "" : g.requisition_committime.ToString();
            laborder_committime.Text = g.order_committime.ToString() == State.datetime_minvalue ? "" : g.order_committime.ToString();
            laborder_audittime.Text = g.order_audittime.ToString() == State.datetime_minvalue ? "" : g.order_audittime.ToString();

            labrequisitionflow.Text = State.requisitionflow_state[g.Requisitionflow];
        }
    }

    /// <summary>
    /// Handles the Click event of the btn control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btn_Click(Object sender, EventArgs e)
    {
        Response.Redirect(Request["backUrl"]);
    }

    /// <summary>
    /// Handles the Click event of the btnOk control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnOk_Click(object sender, EventArgs e)
    {
        GeneralInfo g = GeneralInfoManager.GetModel(generalid);
        RequirementDescInfo.setModelInfo(g);
        if (!ESP.Purchase.BusinessLogic.GeneralInfoManager.CheckStatusForAudit(g, new int[] { State.order_ADAuditWait }))
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('本条记录已经过您的审核或您没有权限审核');window.location.href='/Purchase/Default.aspx';", true);
            return;
        }
        g.adRemark += CurrentUserName + ":" + txtprMediaRemark.Text.Trim() + "。";
        string Msg1 = ESP.ITIL.BusinessLogic.申请单业务设置.广告媒体采买审批通过(CurrentUser, ref g);
        if (Msg1 != "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + Msg1 + "');", true);
            return;
        }
        //if (g.source == "协议供应商" && !OrderInfoManager.isHaveAttachByGID(g.id))
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请上传采购物品的报价附件！');", true);
        //    return;
        //}

        //$$$$$ PR单广告采买审批通过 插入log信息
#if debug
                System.Diagnostics.Debug.WriteLine("PR单广告采买审批通过");
                Trace.Write("PR单广告采买审批通过");
#endif
        try
        {
            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, g.id, "AD审批通过"), "AD审批");
            LogInfo log = new LogInfo();
            log.Gid = g.id;
            log.LogMedifiedTeme = DateTime.Now;
            log.LogUserId = CurrentUserID;
            log.Des = string.Format(State.log_prMedia_commit, CurrentUserName + "(" + CurrentUser.ITCode + ")", DateTime.Now.ToString());

            //ESP.Framework.Entity.EmployeeInfo adAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["ADAuditorId"]));
            //ESP.Framework.Entity.EmployeeInfo adAuditor2 = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["ADAuditorId2"]));
            ESP.Framework.Entity.EmployeeInfo nextAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(g.adAuditor);
            if (GeneralInfoManager.UpdateAndAddLog(g, log, ESP.Purchase.BusinessLogic.AuditLogManager.getNewAuditLog(true, g.PrNo, g.id, txtprMediaRemark.Text, CurrentUser), Request, ESP.Purchase.Common.State.PR_CostRecordsType.PR单广告购买审批通过))
            {
                if (CurrentUserID != g.adAuditor)
                {
                    string strauditor2email = string.Empty;
                    if (nextAuditor.Email != "")
                    {
                        strauditor2email = "," + nextAuditor.Email;
                    }
                    string ret = ESP.Purchase.BusinessLogic.SendMailHelper.SendMailForADAudit(g, g.PrNo, State.getEmployeeEmailBySysUserId(g.requestor) + strauditor2email, CurrentUser.Name, true);
                }
                else
                {
                    string ret = ESP.Purchase.BusinessLogic.SendMailHelper.SendMailForADAudit(g, g.PrNo, State.getEmployeeEmailBySysUserId(g.requestor), CurrentUser.Name, true);
                }

                ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + g.orderid + "审批通过成功!');window.location='prADAuditList.aspx'", true);
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('审批通过失败!');", true);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
        {
            generalid = int.Parse(Request[RequestName.GeneralID]);
            string url = string.Format("/Purchase/Requisition/EditOrder.aspx?{0}={1}", ESP.Purchase.Common.RequestName.GeneralID, generalid);
            Response.Redirect(url);
        }
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
        RequirementDescInfo.setModelInfo(g);
        if (!ESP.Purchase.BusinessLogic.GeneralInfoManager.CheckStatusForAudit(g, new int[] { State.order_ADAuditWait }))
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('本条记录已经过您的审核或您没有权限审核');window.location.href='/Purchase/Default.aspx';", true);
            return;
        }
        g.adRemark = txtprMediaRemark.Text.Trim();
        ESP.ITIL.BusinessLogic.申请单业务设置.广告媒体采买审批驳回(CurrentUser, ref g);

        //$$$$$ PR单广告采买审批驳回 插入log信息
        #if debug
                System.Diagnostics.Debug.WriteLine("PR单广告采买审批驳回");
                Trace.Write("PR单广告采买审批驳回");
        #endif

        try
        {
            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, g.id, "AD审批驳回"), "AD审批");
            LogInfo log = new LogInfo();
            log.Gid = g.id;
            log.LogMedifiedTeme = DateTime.Now;
            log.LogUserId = CurrentUserID;
            log.Des = string.Format(State.log_prMedia_return, CurrentUserName + "(" + CurrentUser.ITCode + ")", DateTime.Now.ToString());
            if (GeneralInfoManager.UpdateAndAddLog(g, log, ESP.Purchase.BusinessLogic.AuditLogManager.getNewAuditLog(false, g.PrNo, g.id, txtprMediaRemark.Text, CurrentUser), Request, ESP.Purchase.Common.State.PR_CostRecordsType.PR单广告购买审批驳回))
            {
                string ret = ESP.Purchase.BusinessLogic.SendMailHelper.SendMailForADAudit(g, g.PrNo, State.getEmployeeEmailBySysUserId(g.requestor), CurrentUser.Name, true);
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('审批驳回成功!');window.location='prADAuditList.aspx'", true);
            }

        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('审批驳回失败!');", true);
        }
    }
}
