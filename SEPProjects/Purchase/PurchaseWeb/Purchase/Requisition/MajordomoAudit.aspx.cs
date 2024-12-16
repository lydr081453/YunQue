using System;
using System.Web.UI.WebControls;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using System.Collections.Generic;
using System.Collections;

public partial class Purchase_Requisition_MajordomoAudit : ESP.Purchase.WebPage.EditPageForPR
{
    int generalid = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        productInfo.CurrentUser = CurrentUser;

        if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
        {
            generalid = int.Parse(Request[RequestName.GeneralID]);
        }
        if (!IsPostBack)
        {
            DataBind();
            BindInfo();
            productInfo.HideDelCommand = true;
            productInfo.ItemListBind(" general_id = " + Request[RequestName.GeneralID]);

            paymentInfo.TotalPrice = productInfo.getTotalPrice();
        }
    }

    public void BindInfo()
    {
        GeneralInfo g = GeneralInfoManager.GetModel(generalid);
        if (null != g)
        {
            genericInfo.Model = g;
            genericInfo.BindInfo();

            projectInfo.PurchaseAuditor = CurrentUserID;
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
    /// 协议供应商： 采购物品总价不能小于预付款
    /// 非协议供应商：预付款+付款账期金额应等于采购物品总价
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    protected string checkPayPrice(GeneralInfo model)
    {
        return "";
    }

    protected void btnOk_Click(object sender, EventArgs e)
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

        if (ESP.Purchase.BusinessLogic.GeneralInfoManager.IsProjectClosed(g, true))
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('该项目号已经关闭，无法审核PR单！');", true);
            return;
        }


        if (!auditCheck(g))
            return;
        paymentInfo.AddPayment(false);
        paymentInfo.DynamicPercent = 0;
        paymentInfo.ListBind();

        string delegateStr = string.Empty;
        if (g.status == ESP.Purchase.Common.State.order_commit && CurrentUser.SysID != g.purchaseAuditor.ToString())
        {
            delegateStr = "代理" + g.purchaseAuditorName;
        }
        else if (g.status == ESP.Purchase.Common.State.requisition_RiskControl && CurrentUser.SysID != g.RCAuditor.ToString())
        {
            delegateStr = "代理" + g.RCAuditorName;
        }

        //付款账期金额必须等于采购物品总额
        if (paymentInfo.GetPaymentPrice() != productInfo.getTotalPrice())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('付款账期总金额必须等于采购物品总金额！');", true);
            return;
        }

        if (txtoverrule.Text.Trim() != string.Empty)
        {
            g.order_overrule += CurrentUser.Name + "(" + CurrentUser.ITCode + ")" + delegateStr + "审批通过：" + txtoverrule.Text + "。";
        }
        string Msg1 = string.Empty;

        if (g.RCAuditor > 0 && g.status == (int)ESP.Purchase.Common.State.requisition_RiskControl)
        {
            Msg1 = ESP.ITIL.BusinessLogic.申请单业务设置.风控审核通过(CurrentUser, ref g, hidisMajordomoUndo.Value, Page);
        }
        else
        {
            Msg1 = ESP.ITIL.BusinessLogic.申请单业务设置.采购总监审批通过(CurrentUser, ref g, hidisMajordomoUndo.Value, Page);
        }
        if (Msg1 != "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + Msg1 + "');", true);
            return;
        }

        LogInfo log = new LogInfo();
        log.Gid = g.id;
        log.LogMedifiedTeme = DateTime.Now;
        log.LogUserId = CurrentUserID;
        log.Des = string.Format(State.log_order_commit, CurrentUserName + "(" + CurrentUser.ITCode + ")" + delegateStr, DateTime.Now.ToString());

        if (GeneralInfoManager.UpdateAndAddLog(g, log, ESP.Purchase.BusinessLogic.AuditLogManager.getNewAuditLog(true, g.PrNo, generalid, delegateStr + "审批通过：" + txtoverrule.Text, CurrentUser), Request, ESP.Purchase.Common.State.PR_CostRecordsType.PR单采购总监审批通过))
        {
            string poret = string.Empty;
            string exMail = string.Empty;
            try
            {
                if (g.Requisitionflow == State.requisitionflow_toO && g.status == State.order_sended)
                {
                    SendMail(g);
                }

                string mail = State.getEmployeeEmailBySysUserId(g.Filiale_Auditor > 0 ? g.Filiale_Auditor : g.first_assessor);
                string ret = string.Empty;

                if (g.status == State.requisition_commit)//风控中心审批完成转入待物料审核状态
                {
                    ret = ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPRRiskControl(g, g.PrNo, State.getEmployeeEmailBySysUserId(g.requestor), mail, true);
                }
                else//否则采购总监审批完成
                {
                    ret = ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPRGen(g, g.PrNo, State.getEmployeeEmailBySysUserId(g.requestor), mail, true);
                }
            }
            catch
            {
                exMail = "(邮件发送失败!)";
            }
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + g.orderid + "订单审批通过!" + exMail + "');window.location='AuditRequistion.aspx'", true);

        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('审批通过失败!');", true);
        }
    }

    private bool auditCheck(GeneralInfo g)
    {
        if (!ESP.Purchase.BusinessLogic.GeneralInfoManager.CheckStatusForAudit(g, new int[] { State.order_commit, State.order_ADAuditWait, State.requisition_RiskControl }))
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('本条记录已经过您的审核或您没有权限审核');window.location.href='/Purchase/Default.aspx';", true);
            return false;
        }
        if (productInfo.getItemRowCount() < 1)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('请添加采购物品!');", true);
            return false;
        }


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

    /// <summary>
    /// 保存按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

        //$$$$$ PR单集团采购审批保存 插入log信息
#if debug
                System.Diagnostics.Debug.WriteLine("PR单集团采购审批保存");
                Trace.Write("PR单集团采购审批保存");
#endif


        //记录操作日志
        ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, g.id, "保存"), "采购总监审批");
        ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据保存成功!');window.location='MajordomoAudit.aspx?" + RequestName.GeneralID + "=" + generalid + "&backUrl=" + Request["backUrl"] + "';", true);
    }


    protected void btnTip_Click(object sender, EventArgs e)
    {
        GeneralInfo generalInfo = GeneralInfoManager.GetModel(generalid);
        var log = ESP.Purchase.BusinessLogic.AuditLogManager.getNewAuditLog((int)ESP.Purchase.Common.State.operationAudit_status.Message, generalInfo.PrNo, generalInfo.id, txtoverrule.Text, CurrentUser, UserID);

        int ret = AuditLogManager.Add(log, Request);

        if (ret > 0)
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('留言保存成功！');", true);

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

        string delegateStr = string.Empty;
        int AuditFlag = 0;

        if (g.status == ESP.Purchase.Common.State.order_commit && CurrentUser.SysID != g.purchaseAuditor.ToString())
        {
            delegateStr = "代理" + g.purchaseAuditorName;
        }
        else if (g.status == ESP.Purchase.Common.State.requisition_RiskControl && CurrentUser.SysID != g.RCAuditor.ToString())
        {
            delegateStr = "代理" + g.RCAuditorName;
            AuditFlag = 1;
        }


        if (txtoverrule.Text.Trim() != string.Empty)
        {
            g.order_overrule += CurrentUser.Name + "(" + CurrentUser.ITCode + ")" + delegateStr + "审批驳回：" + txtoverrule.Text + "。";
        }
        ESP.ITIL.BusinessLogic.申请单业务设置.采购总监审批驳回(CurrentUser, ref g);


        string orderNo = g.orderid;

        LogInfo log = new LogInfo();
        log.Gid = g.id;
        log.LogMedifiedTeme = DateTime.Now;
        log.LogUserId = CurrentUserID;
        log.Des = string.Format(State.log_order_return, CurrentUserName + "(" + CurrentUser.ITCode + ")" + delegateStr, DateTime.Now.ToString());

        if (GeneralInfoManager.UpdateAndAddLog(g, log, ESP.Purchase.BusinessLogic.AuditLogManager.getNewAuditLog(false, g.PrNo, generalid, delegateStr + "审批驳回：" + txtoverrule.Text, CurrentUser), Request, ESP.Purchase.Common.State.PR_CostRecordsType.PR单采购总监审批驳回))
        {
            string exMail = string.Empty;
            if (AuditFlag == 1)//风控中心操作驳回
            {
                ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, g.id, "风控中心审批驳回"), "风控中心审批");
                try
                {
                    string ret = ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPRRiskControl(g, g.PrNo, "", State.getEmployeeEmailBySysUserId(g.first_assessor), false);
                }
                catch
                {
                    exMail = "(邮件发送失败!)";
                }
            }
            else//采购总监驳回
            {
                //记录操作日志
                ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, g.id, "采购总监审批驳回"), "采购总监审批");

                try
                {
                    string ret = ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPRGen(g, g.PrNo, "", State.getEmployeeEmailBySysUserId(g.first_assessor), false);
                }
                catch
                {
                    exMail = "(邮件发送失败!)";
                }
            }

            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + orderNo + "订单审批驳回!" + exMail + "');window.location='AuditRequistion.aspx'", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('审批驳回失败!');", true);
        }
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request["backUrl"]);
    }


    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="generalid"></param>
    protected string SendMail(GeneralInfo generalInfo)
    {
        string htmlFilePath = "";
        string url = "";
        string body = "";
        string clause = "";
        string clause2 = "";
        try
        {

            url = Request.Url.ToString().Substring(0, Request.Url.ToString().LastIndexOf('/') + 1) + "Print/OrderPrint.aspx?id=" + generalInfo.id + "&mail=yes";
            body = ESP.ConfigCommon.SendMail.ScreenScrapeHtml(url);

            ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(generalInfo.project_code.Substring(0, 1));

            htmlFilePath = Server.MapPath("~") + "ExcelTemplate\\" + "订单" + generalInfo.orderid + ".htm";
            clause = Server.MapPath("~") + "ExcelTemplate\\" + branchModel.POTerm;
            clause2 = Server.MapPath("~") + "ExcelTemplate\\" + branchModel.POStandard;

            FileHelper.DeleteFile(htmlFilePath);
            FileHelper.SaveFile(htmlFilePath, body);
            List<OrderInfo> orders = OrderInfoManager.GetListByGeneralId(generalInfo.id);
            Hashtable attFiles = new Hashtable();
            attFiles.Add(branchModel.POTerm, clause);
            attFiles.Add(branchModel.POStandard, clause2);
            attFiles.Add("", htmlFilePath);
            //李彦娥处理产生的新PR单不需要发工作描述
            if ((generalInfo.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA && generalInfo.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_PriFA) && generalInfo.sow2.Trim() != "")
            {
                attFiles.Add("工作描述" + generalInfo.sow2.Substring(generalInfo.sow2.IndexOf(".")), ESP.Purchase.Common.ServiceURL.UpFilePath + generalInfo.sow2);
            }

            int filecount = 1;
            foreach (OrderInfo model in orders)
            {
                string[] upfiles = model.upfile.TrimEnd('#').Split('#');
                foreach (string upfile in upfiles)
                {
                    if (upfile.Trim() != "")
                    {
                        if (generalInfo.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_OtherAdvertisement)
                        {
                        }
                        else
                        {
                            attFiles.Add("采购物品报价" + filecount.ToString() + upfile.Trim().Substring(upfile.IndexOf(".")), ESP.Purchase.Common.ServiceURL.UpFilePath + upfile.Trim());
                        }
                        filecount++;
                    }
                }
            }
            string supplierEmail = generalInfo.supplier_email;

            string ret = ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPO(generalInfo, generalInfo.orderid, State.getEmployeeEmailBySysUserId(generalInfo.requestor), supplierEmail, body, attFiles);

            return "";
        }
        catch (ESP.ConfigCommon.MailException ex)
        {

            return ex.ToString();
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

}
