using System;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;
using System.Web.UI.WebControls;

public partial class Purchase_Requisition_prMediaView : ESP.Purchase.WebPage.ViewPageForPR
{
    int generalid = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
        {
            generalid = int.Parse(Request[RequestName.GeneralID]);
            productInfo.ItemListBind(" general_id = " + generalid);
            productInfo.CurrentUser = CurrentUser;
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

    public void BindInfo()
    {
        GeneralInfo g = GeneralInfoManager.GetModel(generalid);
        if (null != g)
        {
            genericInfo.Model = g;
            genericInfo.BindInfo();

            projectInfo.PurchaseAuditor = UserID;
            projectInfo.Model = g;
            projectInfo.BindInfo();

            if (g.status == State.requisition_commit || g.status == State.order_return)
            {
                TextBox txtbuggeted = (TextBox)projectInfo.FindControl("txtbuggeted");
                txtbuggeted.Enabled = false;
            }

            supplierInfo.Model = g;
            supplierInfo.BindInfo();

            supplerInfoV.Model = g;
            supplerInfoV.BindInfo();

            if (g.PRType == (int)PRTYpe.MediaPR)
            {
                supplerInfoV.Visible = true;
                supplierInfo.Visible = false;
            }

            productInfo.Model = g;
            productInfo.BindInfo();

            if (g.Project_id != 0)
            {
                ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModel(g.Project_id);
                productInfo.ValidProfit(projectModel);
            }

            paymentInfo.Model = g;
            paymentInfo.IsShowCommand = false;
            if (g.source == "协议供应商")
                paymentInfo.IsShowGridView = false;

            RequirementDescInfo.BindInfo(g);

            labdownContrast.Text = g.contrastFile == "" ? "" : "<a target='_blank' href='/Purchase/Requisition/PrFileDownLoad.aspx?GeneralId=" + g.id.ToString() + "&Index=0&Type=Contrast'><img src='/images/ico_04.gif' border='0' /></a>";
            if (g.contrastFile.Trim() == "")
                chkContrast.Visible = false;
            labdownConsult.Text = g.consultFile == "" ? "" : "<a target='_blank' href='/Purchase/Requisition/PrFileDownLoad.aspx?GeneralId=" + g.id.ToString() + "&Index=0&Type=Consult'><img src='/images/ico_04.gif' border='0' /></a>";
            if (g.consultFile.Trim() == "")
                chkConsult.Visible = false;

            txtorderid.Text = g.orderid;
            if (!string.IsNullOrEmpty(g.type))
                ddlType.SelectedValue = g.type;
            else
                ddlType.SelectedValue = "谈判类";
            txtcontrast.Text = g.contrast;
            txtconsult.Text = g.consult;
            txtfirst_assessor.Text = g.first_assessorname;
            hidfirst_assessor.Value = g.first_assessor + "-" + g.first_assessorname;
            radafterwards.SelectedValue = g.afterwardsname;
            if (g.afterwardsname == "是")
            {
                divafterwards.Attributes.Add("style", "display:block");
                txtafterwardsReason.Text = g.afterwardsReason;
            }
            else
            {
                divafterwards.Attributes.Add("style", "display:none");
            }
            radEmBuy.SelectedValue = g.EmBuy;
            if (g.EmBuy == "是")
            {
                divEmBuy.Attributes.Add("style", "display:block");
                txtEmBuyReason.Text = g.EmBuyReason;
            }
            else
            {
                divEmBuy.Attributes.Add("style", "display:none");
            }
            txtContractNo.Text = g.ContractNo;
            if (g.CusAsk == "是")
            {
                CusAskYes.Checked = true;
                CusAskNo.Checked = false;
                txtCusName.Text = g.CusName;
                txtCusAskYesReason.Text = g.CusAskYesReason;
                divCusAskYes.Attributes.Add("style", "display: block");
            }
            else
            {

                CusAskNo.Checked = true;
                CusAskYes.Checked = false;
                divCusAskYes.Attributes.Add("style", "display: none");
            }
            //txtoverrule.Text = g.order_overrule;
            laboverrulehis.Text = g.order_overrule;
            txtothers.Text = g.others;
            txtorderid.Text = g.orderid;
            if (g.isMajordomoUndo)
                hidisMajordomoUndo.Value = "yes";
        }
    }

    public GeneralInfo GetModel(GeneralInfo g)
    {
        g.id = generalid;

        genericInfo.Model = g;
        g = genericInfo.setModelInfo();

        projectInfo.Model = g;

        supplierInfo.Model = g;
        g = supplierInfo.setModelInfo();

        RequirementDescInfo.setModelInfo(g);

        //比价附件
        if (chkContrast.Checked)
            g.contrastFile = "";
        if (upContrast.PostedFile.FileName != "")
        {
            string ContrastFileName = g.contrastFile == "" ? ("BJ_" + g.id + "_" + DateTime.Now.Ticks.ToString()) : g.contrastFile.Split('\\')[1].ToString().Split('.')[0].ToString();
            g.contrastFile = FileHelper.upFile(ContrastFileName, ESP.Purchase.Common.ServiceURL.UpFilePath, upContrast);

        }
        //议价附件
        if (chkConsult.Checked)
            g.consultFile = "";
        if (upConsult.PostedFile.FileName != "")
        {
            string ConsultFileName = g.consultFile == "" ? ("YJ_" + g.id + "_" + DateTime.Now.Ticks.ToString()) : g.consultFile.Split('\\')[1].ToString().Split('.')[0].ToString();
            g.consultFile = FileHelper.upFile(ConsultFileName, ESP.Purchase.Common.ServiceURL.UpFilePath, upConsult);

        }

        g.type = ddlType.SelectedItem.Text;
        g.contrast = txtcontrast.Text.Trim() == "" ? "0" : txtcontrast.Text.Trim();
        g.consult = txtconsult.Text.Trim() == "" ? "0" : txtconsult.Text.Trim();
        g.first_assessor = hidfirst_assessor.Value.Split('-')[1].Equals(txtfirst_assessor.Text.Trim()) == false ? 0 : int.Parse(hidfirst_assessor.Value.Split('-')[0]);
        g.first_assessorname = txtfirst_assessor.Text.Trim() == "" ? hidfirst_assessor.Value.Split('-')[1] : txtfirst_assessor.Text.Trim();
        g.afterwardsname = radafterwards.SelectedValue;
        g.afterwardsReason = g.afterwardsname == "是" ? txtafterwardsReason.Text.Trim() : "";
        g.EmBuy = radEmBuy.SelectedValue;
        g.EmBuyReason = g.EmBuy == "是" ? txtEmBuyReason.Text.Trim() : "";
        g.ContractNo = txtContractNo.Text.Trim();
        if (!string.IsNullOrEmpty(this.txtoverrule.Text.Trim()))
            g.order_overrule = this.txtoverrule.Text.Trim();
        if (CusAskYes.Checked)
        {
            g.CusAsk = CusAskYes.Value;
            g.CusName = txtCusName.Text.Trim();
            g.CusAskYesReason = txtCusAskYesReason.Text.Trim();
        }
        else
        {
            g.CusAsk = CusAskNo.Value;
            g.CusName = "";
            g.CusAskYesReason = "";
        }
        g.others = txtothers.Text.Trim();
        productInfo.Model = g;
        g = productInfo.setModelInfo();

        return g;
    }

    protected bool checkTotalPrice(GeneralInfo model)
    {
        if (productInfo.getTotalPrice() > model.totalprice)
        {
            return false;
        }
        return true;
    }

    protected string checkPayPrice(GeneralInfo model)
    {
        return "";
    }

    private bool auditCheck(GeneralInfo g)
    {
        if (!ESP.Purchase.BusinessLogic.GeneralInfoManager.CheckStatusForAudit(g, new int[] { State.order_commit,State.order_mediaAuditWait, State.order_ADAuditWait }))
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('本条记录已经过您的审核或您没有权限审核');window.location.href='/Purchase/Default.aspx';", true);
            return false;
        }
        if (productInfo.getItemRowCount() < 1)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('请添加采购物品!');", true);
            return false;
        }
        //if (!GeneralInfoManager.contrastPrice(generalid, 0, 0))
        //{
        //    ClientScript.RegisterStartupScript(typeof(string), "", "alert('采购物品总金额已经超过第三方采购成本预算！');", true);
        //    return false;
        //}

        if (!checkTotalPrice(g))
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('采购物品的总价不能大于修改前，请修改后再审核！');", true);
            return false;
        }
        if (checkPayPrice(g) != "")
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + checkPayPrice(g) + "');", true);
            return false;
        }
        return true;
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        GeneralInfo g = GeneralInfoManager.GetModel(generalid);
        g = GetModel(g);
        paymentInfo.Model = g;
        string Msg = paymentInfo.SetModel(g);
        if (Msg != "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + Msg + "');", true);
            return;
        }
        if (!auditCheck(g))
            return;
        try
        {
            paymentInfo.AddPayment(false);
            paymentInfo.NotShow();
        }
        catch { }
        GeneralInfoManager.Update(g);

        //记录操作日志
        ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, g.id, "保存"), "采购总监审批");
        ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据保存成功!');window.location='MajordomoAudit.aspx?" + RequestName.GeneralID + "=" + generalid + "&backUrl=" + Request["backUrl"] + "';", true);
    }

    protected void btn_Click(Object sender, EventArgs e)
    {
        Response.Redirect(Request["backUrl"]);
    }



    protected void btnTip_Click(object sender, EventArgs e)
    {
        GeneralInfo generalInfo = GeneralInfoManager.GetModel(generalid);
        var log = ESP.Purchase.BusinessLogic.AuditLogManager.getNewAuditLog((int)ESP.Purchase.Common.State.operationAudit_status.Message, generalInfo.PrNo, generalInfo.id, txtoverrule.Text, CurrentUser, UserID);

        int ret = AuditLogManager.Add(log, Request);

        if (ret > 0)
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('留言保存成功！');", true);

    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        GeneralInfo g = GeneralInfoManager.GetModel(generalid);
        RequirementDescInfo.setModelInfo(g);
        if (!ESP.Purchase.BusinessLogic.GeneralInfoManager.CheckStatusForAudit(g, new int[] { State.order_mediaAuditWait }))
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('本条记录已经过您的审核或您没有权限审核');window.location.href='/Purchase/Default.aspx';", true);
            return;
        }
        string Msg1 = ESP.ITIL.BusinessLogic.申请单业务设置.媒介审批通过(CurrentUser, ref g);
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
        g.prMediaRemark = txtoverrule.Text.Trim();


        try
        {
            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, g.id, "媒介审批通过"), "媒介审批");
            LogInfo log = new LogInfo();
            log.Gid = g.id;
            log.LogMedifiedTeme = DateTime.Now;
            log.LogUserId = CurrentUserID;
            log.Des = string.Format(State.log_prMedia_commit, CurrentUserName + "(" + CurrentUser.ITCode + ")", DateTime.Now.ToString());
            if (GeneralInfoManager.UpdateAndAddLog(g, log, ESP.Purchase.BusinessLogic.AuditLogManager.getNewAuditLog(true, g.PrNo, g.id, txtoverrule.Text, CurrentUser), Request, ESP.Purchase.Common.State.PR_CostRecordsType.PR单媒介审批通过))
            {
                string ret = ESP.Purchase.BusinessLogic.SendMailHelper.SendMailForMediaAudit(g, g.PrNo, State.getEmployeeEmailBySysUserId(g.requestor), CurrentUser.Name, true);
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + g.orderid + "审批通过成功!');window.location='prMediaAuditList.aspx'", true);
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('审批驳回失败!');", true);
        }
    }

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

    protected void btnNo_Click(object sender, EventArgs e)
    {
        GeneralInfo g = GeneralInfoManager.GetModel(generalid);
        RequirementDescInfo.setModelInfo(g);
        if (!ESP.Purchase.BusinessLogic.GeneralInfoManager.CheckStatusForAudit(g, new int[] { State.order_mediaAuditWait }))
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('本条记录已经过您的审核或您没有权限审核');window.location.href='/Purchase/Default.aspx';", true);
            return;
        }
        g.prMediaRemark = txtoverrule.Text.Trim();
        ESP.ITIL.BusinessLogic.申请单业务设置.媒介审批驳回(CurrentUser, ref g);


        //$$$$$ PR单媒介审批驳回 插入log信息
#if debug
                System.Diagnostics.Debug.WriteLine("PR单媒介审批驳回");
                Trace.Write("PR单媒介审批驳回");
#endif


        try
        {
            //GeneralInfoDataProvider.Update(g);
            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, g.id, "媒介审批驳回"), "媒介审批");
            LogInfo log = new LogInfo();
            log.Gid = g.id;
            log.LogMedifiedTeme = DateTime.Now;
            log.LogUserId = CurrentUserID;
            log.Des = string.Format(State.log_prMedia_return, CurrentUserName + "(" + CurrentUser.ITCode + ")", DateTime.Now.ToString());
            //LogManager.AddLog(log, Request);
            if (GeneralInfoManager.UpdateAndAddLog(g, log, ESP.Purchase.BusinessLogic.AuditLogManager.getNewAuditLog(false, g.PrNo, g.id, txtoverrule.Text, CurrentUser), Request, ESP.Purchase.Common.State.PR_CostRecordsType.PR单媒介审批驳回))
            {
                string ret = ESP.Purchase.BusinessLogic.SendMailHelper.SendMailForMediaAudit(g, g.PrNo, State.getEmployeeEmailBySysUserId(g.requestor), CurrentUser.Name, true);
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('审批驳回成功!');window.location='prMediaAuditList.aspx'", true);
            }

        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('审批驳回失败!');", true);
        }
    }
}
