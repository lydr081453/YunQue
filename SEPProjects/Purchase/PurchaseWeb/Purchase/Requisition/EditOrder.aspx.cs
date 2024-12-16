using System;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_EditOrder : ESP.Purchase.WebPage.EditPageForPR
{
    int generalid = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
        {
            generalid = int.Parse(Request[RequestName.GeneralID]);
        }
        productInfo.CurrentUser = CurrentUser;
        if (!IsPostBack)
        {
            DataBind();
            BindInfo();
          //  productInfo.HideDelCommand = true;
            productInfo.ItemListBind(" general_id = " + Request[RequestName.GeneralID]);
            paymentInfo.TotalPrice = productInfo.getTotalPrice();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sysUserId"></param>
    /// <returns></returns>
    private bool isBackUpUser(int sysUserId)
    {
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
        foreach (ESP.Framework.Entity.AuditBackUpInfo backUp in delegates)
        {
            if (backUp.UserID == sysUserId)
                return true;
        }
        return false;
    }

    public void BindInfo()
    {
        GeneralInfo g = GeneralInfoManager.GetModel(generalid);
        this.ddlPeriodType.SelectedValue = g.PeriodType.ToString();
        if (null != g)
        {
            if (g.first_assessor.ToString() != "" && g.first_assessor.ToString() != CurrentUser.SysID.ToString() && !isBackUpUser(g.first_assessor))
            {
                //if (ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorName"].ToString().ToLower() != CurrentUser.ITCode.ToLower())
                if(g.first_assessor!=CurrentUserID)
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('您不是该申请单的初审人,无法审批该申请单!');window.location='ChangeAuditUser.aspx?" + RequestName.GeneralID + "=" + generalid.ToString() + "'", true);
                }
            }
            genericInfo.Model = g;
            genericInfo.BindInfo();

            projectInfo.PurchaseAuditor = CurrentUserID;
            projectInfo.Model = g;
            projectInfo.BindInfo();

            if (g.Project_id != 0)
            {
                ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModel(g.Project_id);
                productInfo.ValidProfit(projectModel);
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
            productInfo.CurrentUser = CurrentUser;
            productInfo.BindInfo();

            paymentInfo.Model = g;
            if(g.source == "协议供应商")
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
            txtoverrule.Text = g.requisition_overrule;
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

            txtothers.Text = g.others;
            txtorderid.Text = g.orderid;
            if (g.isMajordomoUndo)
                hidisMajordomoUndo.Value = "yes";
        }
    }

    /// <summary>
    /// Gets the model.
    /// </summary>
    /// <param name="g">The g.</param>
    /// <returns></returns>
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
            g.contrastFile = FileHelper.upFile(ContrastFileName,ESP.Purchase.Common.ServiceURL.UpFilePath, upContrast);
        }
        //议价附件
        if (chkConsult.Checked)
            g.consultFile = "";
        if (upConsult.PostedFile.FileName != "")
        {
            string ConsultFileName = g.consultFile == "" ? ("YJ_" + g.id + "_" + DateTime.Now.Ticks.ToString()) : g.consultFile.Split('\\')[1].ToString().Split('.')[0].ToString();
            g.consultFile = FileHelper.upFile(ConsultFileName, ESP.Purchase.Common.ServiceURL.UpFilePath, upConsult);
        }
        //账期是固定还是开放式的
        g.PeriodType = Convert.ToInt32(this.ddlPeriodType.SelectedValue);
        g.type = ddlType.SelectedItem.Text;
        g.contrast = txtcontrast.Text.Trim() == "" ? "0" : txtcontrast.Text.Trim();
        g.consult = txtconsult.Text.Trim() == "" ? "0" : txtconsult.Text.Trim();
        int newFirstor = hidfirst_assessor.Value.Split('-')[1].Equals(txtfirst_assessor.Text.Trim()) == false ? 0 : int.Parse(hidfirst_assessor.Value.Split('-')[0]);
        string newFirstorName = txtfirst_assessor.Text.Trim() == "" ? hidfirst_assessor.Value.Split('-')[1] : txtfirst_assessor.Text.Trim();
        if (newFirstor != g.first_assessor)
        {
            LogInfo log = new LogInfo();
            log.Gid = g.id;
            log.LogMedifiedTeme = DateTime.Now;
            log.LogUserId = CurrentUserID;
            log.Des = string.Format(State.log_changecheker, CurrentUser.Name + "(" + CurrentUser.ITCode + ")", newFirstorName,
                                    DateTime.Now.ToString());
            LogManager.AddLog(log, Request);

            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, g.id, "变更初审人"), "申请单");
            string Msg = "您提交的流水号：" + g.glideno + "  申请单号：" + g.PrNo + "  的申请单初审人变更为：" + newFirstorName;
            ESP.ConfigCommon.SendMail.Send1("变更初审人", State.getEmployeeEmailBySysUserId(g.requestor), Msg, true, "");
        }
        g.first_assessor = newFirstor;
        g.first_assessorname = newFirstorName;
        g.afterwardsname = radafterwards.SelectedValue;
        g.afterwardsReason = g.afterwardsname == "是" ? txtafterwardsReason.Text.Trim() : "";
        g.EmBuy = radEmBuy.SelectedValue;
        g.EmBuyReason = g.EmBuy == "是" ? txtEmBuyReason.Text.Trim() : "";
        g.ContractNo = txtContractNo.Text.Trim();

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

    /// <summary>
    /// 保存按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
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
        if (g.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA && g.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_PriFA)
        {
            if (ESP.Purchase.BusinessLogic.GeneralInfoManager.IsProjectClosed(g,true))
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('该项目号已经关闭，无法审核PR单！');", true);
                return;
            }
            //if (ESP.Purchase.BusinessLogic.GeneralInfoManager.IsOverProjectTotalAmount(g))
            //{
            //    ClientScript.RegisterStartupScript(typeof(string), "", "alert('采购物品总金额已经超过项目可用总成本！');", true);
            //    return;
            //}
        }
        if (txtoverrule.Text.Trim() != string.Empty)
        {
            g.requisition_overrule = txtoverrule.Text.Trim();
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

        ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据保存成功!');window.location='EditOrder.aspx?" + RequestName.GeneralID + "=" + generalid + "'", true);
    }

    /// <summary>
    /// 单子为申请单提交或订单驳回时，采购物品的总价只能比之前小
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    protected bool checkTotalPrice(GeneralInfo model)
    {
        if (productInfo.getTotalPrice() > model.totalprice)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    protected string checkPayPrice(GeneralInfo model)
    {
        return "";
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
            btnCommit_Click(sender, e);
        }
        else if (hidState.Value == "no")
        {
            btnReturn_Click(sender, e);
        }
    }

    /// <summary>
    /// Handles the Click event of the btnReturn control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnReturn_Click(object sender, EventArgs e)
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
        g.requisition_overrule = CurrentUser.Name + ":" + txtoverrule.Text.Trim();
        if (!auditCheck(g))
            return;
        ESP.ITIL.BusinessLogic.申请单业务设置.物料审核驳回(CurrentUser, ref g);
        //操作日志
        LogInfo log = new LogInfo();
        string delegateStr = string.Empty;
        log.Gid = g.id;
        log.LogMedifiedTeme = DateTime.Now;
        log.LogUserId = CurrentUserID;
        if (CurrentUser.SysID != g.first_assessor.ToString())
        {
            delegateStr = "代理" + g.first_assessorname;
        }
        log.Des = string.Format(State.log_requisition_return, CurrentUserName + "(" + CurrentUser.ITCode + ")" + delegateStr , DateTime.Now.ToString());
        string exMail = string.Empty;

        if (GeneralInfoManager.UpdateAndAddLog(g, log, ESP.Purchase.BusinessLogic.AuditLogManager.getNewAuditLog(false, g.PrNo, generalid, delegateStr + "审批驳回：" + txtoverrule.Text, CurrentUser), Request, ESP.Purchase.Common.State.PR_CostRecordsType.PR单采购初审驳回))
        {
            try
            {
                string ret = ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPRAcr(g, g.PrNo, g.first_assessorname, State.getEmployeeEmailBySysUserId(g.requestor), "", false);
            }
            catch
            {
                exMail = "(邮件发送失败!)";
            }
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据驳回成功!"+exMail+"');window.location='OrderList.aspx'", true);
        }
        else 
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据驳回失败!');", true);
        }
    }


    /// <summary>
    /// 审核检查
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    private bool auditCheck(GeneralInfo model)
    {
        if (!ESP.Purchase.BusinessLogic.GeneralInfoManager.CheckStatusForAudit(model, new int[] { State.requisition_commit, State.order_return }))
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('本条记录已经过您的审核或您没有权限审核');window.location.href='/Purchase/Default.aspx';", true);
            return false;
        }
        if (productInfo.getItemRowCount() < 1)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('请添加采购物品!');", true);
            return false;
        }

        if (!checkTotalPrice(model))
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('采购物品的总价不能大于修改前，请修改后再审核！');", true);
            return false;
        }
        if (checkPayPrice(model) != "")
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + checkPayPrice(model) + "');", true);
            return false;
        }
        return true;
    }



    protected void btnTip_Click(object sender, EventArgs e)
    {
        GeneralInfo generalInfo = GeneralInfoManager.GetModel(generalid);
        var log = ESP.Purchase.BusinessLogic.AuditLogManager.getNewAuditLog((int)ESP.Purchase.Common.State.operationAudit_status.Message, generalInfo.PrNo, generalInfo.id, txtoverrule.Text, CurrentUser, UserID);

        int ret = AuditLogManager.Add(log, Request);

        if (ret > 0)
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('留言保存成功！');", true);

    }

    /// <summary>
    /// Handles the Click event of the btnCommit control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnCommit_Click(object sender, EventArgs e)
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
        if (g.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA && g.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_PriFA)
        {
            if (ESP.Purchase.BusinessLogic.GeneralInfoManager.IsProjectClosed(g,true))
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('该项目号已经关闭，无法审核PR单！');", true);
                return;
            }
            //if (ESP.Purchase.BusinessLogic.GeneralInfoManager.IsOverProjectTotalAmount(g))
            //{
            //    ClientScript.RegisterStartupScript(typeof(string), "", "alert('采购物品总金额已经超过项目可用总成本！');", true);
            //    return;
            //}
        }
        g.requisition_overrule = CurrentUser.Name + ":" + txtoverrule.Text.Trim();
        if (!auditCheck(g))
            return;
        paymentInfo.AddPayment(false);
        paymentInfo.DynamicPercent = 0;
        paymentInfo.ListBind();

        //付款账期金额必须等于采购物品总额
        if (paymentInfo.GetPaymentPrice() != productInfo.getTotalPrice())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('付款账期总金额必须等于采购物品总金额！');", true);
            return;
        }
        string Msg1 = ESP.ITIL.BusinessLogic.申请单业务设置.物料审核通过(CurrentUser, ref g, hidisMajordomoUndo.Value, Page);
        if (Msg1 != "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + Msg1 + "');", true);
            return;
        }
        LogInfo log = new LogInfo();
        string delegateStr = string.Empty;
        log.Gid = g.id;
        log.LogMedifiedTeme = DateTime.Now;
        log.LogUserId = CurrentUserID;
        if (CurrentUser.SysID != g.first_assessor.ToString())
        {
            delegateStr = "代理"+g.first_assessorname;
        }
        log.Des = string.Format(State.log_requisition_ok, CurrentUserName + "(" + CurrentUser.ITCode + ")" + delegateStr , DateTime.Now.ToString());
        if (GeneralInfoManager.UpdateAndAddLog(g, log, ESP.Purchase.BusinessLogic.AuditLogManager.getNewAuditLog(true, g.PrNo, generalid, delegateStr + "审批通过：" + txtoverrule.Text, CurrentUser), Request, ESP.Purchase.Common.State.PR_CostRecordsType.PR单采购初审通过))
        {
            string exMail = string.Empty;
            try
            {
                string ret = ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPRAcr(g, g.PrNo, g.first_assessorname, State.getEmployeeEmailBySysUserId(g.requestor), ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorEmail"], true);
            }
            catch
            {
                exMail = "(邮件发送失败!)";
            }
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + g.PrNo + "已审核通过，正在等待申请单审批，请在查询中心查询审批状态!" + exMail + "');window.location='OrderList.aspx'", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核申请单失败!');", true);
        }
    }


    /// <summary>
    /// Handles the Click event of the BackUrl control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void BackUrl_Click(object sender, EventArgs e)
    {
        Response.Redirect("RequisitionSaveList.aspx");
    }

}
