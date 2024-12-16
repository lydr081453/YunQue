using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
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

public partial class Purchase_Requisition_Filiale_RequisitionEdit : ESP.Purchase.WebPage.EditPageForPR
{
    int generalid = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
        {
            generalid = int.Parse(Request[RequestName.GeneralID]);
        }
        if (!IsPostBack)
        {
            DataBind();
            BindInfo();
            if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
            {
                productInfo.HideDelCommand = true;
                productInfo.ItemListBind(" general_id = " + Request[RequestName.GeneralID]);
                paymentInfo.TotalPrice = productInfo.getTotalPrice();
            }
        }
    }

    public void BindInfo()
    {
        GeneralInfo g = GeneralInfoManager.GetModel(generalid);
        if (null != g)
        {
            this.ddlPeriodType.SelectedValue = g.PeriodType.ToString();
            genericInfo.Model = g;
            genericInfo.BindInfo();


            projectInfo.Model = g;
            projectInfo.BindInfo();

            supplierInfo.Model = g;
            supplierInfo.BindInfo();

            supplerInfoV.Model = g;
            supplerInfoV.BindInfo();

            if (g.PRType == (int)PRTYpe.MediaPR)
            {
                supplerInfoV.Visible = true;
                supplierInfo.Visible = false;
            }

            paymentInfo.Model = g;
            paymentInfo.IsShowCommand = false;
            if (g.source == "协议供应商")
                paymentInfo.IsShowGridView = false;

            RequirementDescInfo.BindInfo(g);

            productInfo.Model = g;
            productInfo.BindInfo();

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
            //分公司审核人
            if (g.Filiale_Auditor != 0)
            {
                trFiliale.Style["display"] = "block";
                txtFiliale.Text = g.Filiale_AuditName;
                hidFiliale.Value = g.Filiale_Auditor + "-" + g.Filiale_AuditName;
            }
            else
                trFiliale.Style["display"] = "none";
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
            txtoverrule.Text = g.fili_overrule.Trim();
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
            if (g.requisition_overrule != "")
            {
                palOverrule.Visible = true;
                labOverrule.Text = g.requisition_overrule;
            }
            lablasttime.Text = g.lasttime.ToString();
            if (g.isMajordomoUndo)
                hidisMajordomoUndo.Value = "yes";
        }
    }

    public GeneralInfo getModel(GeneralInfo g)
    {
        g.id = generalid;

        genericInfo.Model = g;
        g = genericInfo.setModelInfo();

        projectInfo.Model = g;
        //g = projectInfo.setModelInfo();

        supplierInfo.Model = g;
        g = supplierInfo.setModelInfo();
        productInfo.Model = g;
        g = productInfo.setModelInfo();

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
        //账期是固定还是开放式的
        g.PeriodType = Convert.ToInt32(this.ddlPeriodType.SelectedValue);
        g.type = ddlType.SelectedItem.Text;
        g.contrast = txtcontrast.Text.Trim() == "" ? "0" : txtcontrast.Text.Trim();
        g.consult = txtconsult.Text.Trim() == "" ? "0" : txtconsult.Text.Trim();
        
        g.first_assessor = hidfirst_assessor.Value.Split('-')[1].Equals(txtfirst_assessor.Text.Trim()) == false ? 0 : int.Parse(hidfirst_assessor.Value.Split('-')[0]);
        g.first_assessorname = txtfirst_assessor.Text.Trim() == "" ? hidfirst_assessor.Value.Split('-')[1] : txtfirst_assessor.Text.Trim();
       
        //分公司审核人
        if (g.Filiale_Auditor != 0)
        {
            g.Filiale_Auditor = hidFiliale.Value.Split('-')[1].Equals(txtFiliale.Text.Trim()) == false ? 0 : int.Parse(hidFiliale.Value.Split('-')[0]);
            g.Filiale_AuditName = txtFiliale.Text.Trim() == "" ? hidFiliale.Value.Split('-')[1] : txtFiliale.Text.Trim();
        }
        g.afterwardsname = radafterwards.SelectedValue;
        g.afterwardsReason = g.afterwardsname == "是" ? txtafterwardsReason.Text.Trim() : "";
        g.EmBuy = radEmBuy.SelectedValue;
        g.EmBuyReason = g.EmBuy == "是" ? txtEmBuyReason.Text.Trim() : "";
        g.ContractNo = txtContractNo.Text.Trim();

        if (txtoverrule.Text.Trim() != "")
        {
            g.fili_overrule = CurrentUser.Name + ":" + txtoverrule.Text.Trim();
        }
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
        return g;
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        GeneralInfo g = GeneralInfoManager.GetModel(generalid);
        if (!ESP.Purchase.BusinessLogic.GeneralInfoManager.CheckStatusForAudit(g, new int[] { State.requisition_temporary_commit, State.order_return }))
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('本条记录已经过您的审核或您没有权限审核');window.location.href='/Purchase/Default.aspx';", true);
            return;
        }
        if (!checkTotalPrice(g))
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('采购物品的总价不能大于修改前，请修改后再保存！');", true);
            return;
        }
        ESP.ITIL.BusinessLogic.申请单业务设置.分公司审核驳回(CurrentUser, ref g);

        //$$$$$ PR单分公司审批驳回 插入log信息
#if debug
                System.Diagnostics.Debug.WriteLine("PR单分公司审批驳回");
                Trace.Write("PR单分公司审批驳回");
#endif


        g.fili_overrule = txtoverrule.Text.Trim();
        g = getModel(g);
        paymentInfo.Model = g;
        string Msg = paymentInfo.SetModel(g);
        if (Msg != "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + Msg + "');", true);
            return;
        }
        LogInfo log = new LogInfo();
        string delegateStr = string.Empty;
        log.Gid = g.id;
        log.LogMedifiedTeme = DateTime.Now;
        log.LogUserId = CurrentUserID;
        if (CurrentUser.SysID != g.Filiale_Auditor.ToString())
        {
            delegateStr = "代理" + g.Filiale_AuditName;
        }

        log.Des = string.Format(State.log_requisition_temporary_return, CurrentUserName + "(" + CurrentUser.ITCode + ")" + delegateStr, DateTime.Now.ToString());
        if (GeneralInfoManager.UpdateAndAddLog(g, log, ESP.Purchase.BusinessLogic.AuditLogManager.getNewAuditLog(false, g.PrNo, generalid, txtoverrule.Text, CurrentUser), Request, ESP.Purchase.Common.State.PR_CostRecordsType.PR单分公司审核驳回))
        {
            string exMail = string.Empty;
            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, g.id, "分公司审核驳回"), "分公司审核");
            try
            {
                string ret = ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPRFili(g, g.PrNo, NodeName(), State.getEmployeeEmailBySysUserId(g.requestor), "", false);
            }
            catch
            {
                exMail = "(邮件发送失败!)";
            }
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据驳回成功!"+exMail+"');window.location='FilialeAuditList.aspx'", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据驳回失败!');", true);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        GeneralInfo g = GeneralInfoManager.GetModel(generalid);
        if (!ESP.Purchase.BusinessLogic.GeneralInfoManager.CheckStatusForAudit(g, new int[] { State.requisition_temporary_commit, State.order_return }))
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('本条记录已经过您的审核或您没有权限审核');window.location.href='/Purchase/Default.aspx';", true);
            return;
        }
        if (productInfo.getItemRowCount() < 1)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('请添加采购物品!');", true);
            return;
        }
        //if (!GeneralInfoManager.contrastPrice(generalid, 0, 0))
        //{
        //    ClientScript.RegisterStartupScript(typeof(string), "", "alert('采购物品总金额已经超过第三方采购成本预算！');", true);
        //    return;
        //}
        if (g.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA && g.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_PriFA)
        {
            if (ESP.Purchase.BusinessLogic.GeneralInfoManager.IsProjectClosed(g,true))
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('该项目号已经关闭，无法审核PR单！');", true);
                return;
            }
        }
        if (!checkTotalPrice(g))
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('采购物品的总价不能大于修改前，请修改后再保存！');", true);
            return;
        }
        paymentInfo.AddPayment(false);
        paymentInfo.DynamicPercent = 0;
        paymentInfo.ListBind();
        if (paymentInfo.GetPaymentPrice() != productInfo.getTotalPrice())
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('付款账期总金额必须等于采购物品总金额！');", true);
            return;
        }
        g = getModel(g);
        string Msg1 = ESP.ITIL.BusinessLogic.申请单业务设置.分公司审核通过(CurrentUser, hidNextFili.Value, txtNextFili.Text.Trim(), ref g, hidisMajordomoUndo.Value, Page);


        //$$$$$ PR单分公司审批通过 插入log信息
#if debug
                System.Diagnostics.Debug.WriteLine("PR单分公司审批通过");
                Trace.Write("PR单分公司审批通过");
#endif



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
        if (CurrentUser.SysID != g.Filiale_Auditor.ToString())
        {
            delegateStr = "代理" + g.Filiale_AuditName;
        }
        log.Des = string.Format(State.log_requisition_temporary_commit, CurrentUserName + "(" + CurrentUser.ITCode + ")" + delegateStr, DateTime.Now.ToString());
        paymentInfo.Model = g;
        string Msg = paymentInfo.SetModel(g);
        if (Msg != "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + Msg + "');", true);
            return;
        }
        if (GeneralInfoManager.UpdateAndAddLog(g, log, ESP.Purchase.BusinessLogic.AuditLogManager.getNewAuditLog(true, g.PrNo, generalid, txtoverrule.Text, CurrentUser), Request, ESP.Purchase.Common.State.PR_CostRecordsType.PR单分公司审核通过))
        {
            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, g.id, "分公司审核通过"), "分公司审核");
            if (hidNextFili.Value == "")
            {
                string exMail = string.Empty;
                try
                {
                    if (g.status == State.order_commit)
                    {
                        ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPRFili(g, g.PrNo, NodeName(), State.getEmployeeEmailBySysUserId(g.requestor), State.getEmployeeEmailBySysUserId(g.purchaseAuditor), true);
                    }
                    else if (g.status == State.requisition_commit)
                    {
                        ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPRFili(g, g.PrNo, NodeName(), State.getEmployeeEmailBySysUserId(g.requestor), State.getEmployeeEmailBySysUserId(g.first_assessor), true);
                    }
                    else
                    {
                        ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPRFili(g, g.PrNo, NodeName(), State.getEmployeeEmailBySysUserId(g.requestor), State.getEmployeeEmailBySysUserId(g.Filiale_Auditor), true);
                    }
                }
                catch
                {
                    exMail = "(邮件发送失败!)";
                }

                ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + g.PrNo + "已提交成功，请在查询中心查询审批状态。"+exMail+"');window.location='FilialeAuditList.aspx'", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('审核成功！');window.location='FilialeAuditList.aspx'", true);
            }

        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据提交失败!');", true);
        }
    }

    /// <summary>
    /// 保存按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        GeneralInfo g = GeneralInfoManager.GetModel(generalid);
        if (!ESP.Purchase.BusinessLogic.GeneralInfoManager.CheckStatusForAudit(g, new int[] { State.requisition_temporary_commit, State.order_return }))
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('本条记录已经过您的审核或您没有权限审核');window.location.href='/Purchase/Default.aspx';", true);
            return;
        }
        if (!checkTotalPrice(g))
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('采购物品的总价不能大于修改前，请修改后再保存！');", true);
            return;
        }
        //if (g.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA && g.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_PriFA)
        //{
        //    if (!GeneralInfoManager.contrastPrice(generalid, 0, 0))
        //    {
        //        ClientScript.RegisterStartupScript(typeof(string), "", "alert('采购物品总金额已经超过第三方采购成本预算！');", true);
        //        return;
        //    }
        //}
        if (g.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA && g.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_PriFA)
        {
            if (ESP.Purchase.BusinessLogic.GeneralInfoManager.IsProjectClosed(g,true))
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('该项目号已经关闭，无法审核PR单！');", true);
                return;
            }
        }
        try
        {
            paymentInfo.AddPayment(false);
            paymentInfo.NotShow();
        }
        catch { }
        try
        {
            g = getModel(g);
            paymentInfo.Model = g;
            string Msg = paymentInfo.SetModel(g);
            if (Msg != "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + Msg + "');", true);
                return;
            }
            GeneralInfoManager.Update(g);

            //$$$$$ PR单分公司审批保存 插入log信息
#if debug
                System.Diagnostics.Debug.WriteLine("PR单分公司审批保存");
                Trace.Write("PR单分公司审批保存");
#endif


            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, g.id, "保存"), "分公司审核");
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据保存成功!');window.location='Filiale_RequisitionEdit.aspx?" + RequestName.GeneralID + "=" + generalid + "'", true);
        }
        catch
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据保存失败!');", true);
        }
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

    private string NodeName()
    {
        IList<ESP.Compatible.Department> dtdep = ESP.Compatible.Employee.GetDepartments(UserID);
        string nodename = "";
        if (dtdep.Count > 0)
        {
            string level = dtdep[0].Level.ToString();
            if (level == "1")
            {
                nodename = dtdep[0].NodeName;
            }
            else if (level == "2")
            {
                ESP.Compatible.Department dep = ESP.Compatible.DepartmentManager.GetDepartmentByPK(dtdep[0].UniqID);
                nodename = dep.Parent.DepartmentName;

            }
            else if (level == "3")
            {
                ESP.Compatible.Department dep = ESP.Compatible.DepartmentManager.GetDepartmentByPK(dtdep[0].UniqID);
                ESP.Compatible.Department dep2 = ESP.Compatible.DepartmentManager.GetDepartmentByPK(dep.Parent.UniqID);
                nodename = dep2.Parent.DepartmentName;

            }
        }
        return nodename;
    }

}
