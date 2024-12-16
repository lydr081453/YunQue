using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_HandConfirmRecipient : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindInfo();
        }
    }

    /// <summary>
    /// 绑定列表信息
    /// </summary>
    public void bindInfo()
    {

        List<SqlParameter> parms = new List<SqlParameter>();
        //string strWhere = " and (a.isconfirm = 0 or a.isconfirm is null or a.isconfirm = " + State.recipentConfirm_Emp1 + " or ( b.appendReceiver is not null and b.appendReceiver>0 and a.isconfirm=" + State.recipentConfirm_Emp2 + " and (b.source='协议供应商' or b.supplier_email<>'' )))";
        string strWhere = " and (a.isconfirm=" + State.recipentConfirm_Emp1 + " or (b.requisitionflow in (" + State.requisitionflow_toO + "," + State.requisitionflow_toC + ") and a.isconfirm=" + State.recipentConfirm_Emp2 + "))";

        //strWhere += " and ( b.goods_receiver=" + CurrentUser.SysID + " or b.first_assessor = " + CurrentUser.SysID + " or b.Filiale_Auditor = " + CurrentUser.SysID + ")";
        int[] depts = CurrentUser.GetDepartmentIDs();
        bool isPurchaseDept = false;
        for (int i = 0; i < depts.Length; i++)
        {//FirstAssessorDeptID
            if ((ESP.Configuration.ConfigurationManager.SafeAppSettings["FirstAssessorDeptID"].IndexOf("," + depts[i].ToString() + ",") >= 0) || depts[i] == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["StockDeparmentUniqID"]) || depts[i] == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["CQStockDeparmentUniqID"]))
            {
                isPurchaseDept = true;
            }
        }
        if (!isPurchaseDept)
            strWhere += " and (b.requestor=" + CurrentUser.SysID + " or b.goods_receiver=" + CurrentUser.SysID + " or b.appendReceiver=" + CurrentUser.SysID + ")";
        if (!string.IsNullOrEmpty(txtsupplier_name.Text.Trim()))
        {
            strWhere += " and supplier_name like '%'+@suppliername+'%'";
            parms.Add(new SqlParameter("@suppliername", txtsupplier_name.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(txtGlideNo.Text.Trim()))
        {
            int totalgno = 0;
            bool res = int.TryParse(txtGlideNo.Text, out totalgno);
            if (res)
            {
                strWhere += " and a.Gid = @Gid";
                parms.Add(new SqlParameter("@Gid", txtGlideNo.Text.Trim()));
            }
        }
        if (!string.IsNullOrEmpty(txtPrNo.Text.Trim()))
        {
            strWhere += " and b.orderid like '%'+@orderid+'%'";
            parms.Add(new SqlParameter("@orderid", txtPrNo.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(txtRNo.Text.Trim()))
        {
            strWhere += " and a.RecipientNo like '%'+@RecipientNo+'%'";
            parms.Add(new SqlParameter("@RecipientNo", txtRNo.Text.Trim()));
        }
        //txtReceiver
        if (!string.IsNullOrEmpty(txtReceiver.Text.Trim()))
        {
            strWhere += " and (b.receivername like '%'+@receivername+'%' or b.appendReceiverName like '%'+@receivername+'%')";
            parms.Add(new SqlParameter("@receivername", txtReceiver.Text.Trim()));
        }

        DataSet ds = RecipientManager.GetRecipientList(strWhere, parms);
        gvSupplier.DataSource = ds;
        gvSupplier.DataBind();
        if (gvSupplier.PageCount > 1)
        {
            PageBottom.Visible = true;
            PageTop.Visible = true;
        }
        else
        {
            PageBottom.Visible = false;
            PageTop.Visible = false;
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            tabTop.Visible = true;
            tabBottom.Visible = true;
        }
        else
        {
            tabTop.Visible = false;
            tabBottom.Visible = false;
        }

        labAllNum.Text = labAllNumT.Text = ds.Tables[0].Rows.Count.ToString();
        labPageCount.Text = labPageCountT.Text = (gvSupplier.PageIndex + 1).ToString() + "/" + gvSupplier.PageCount.ToString();
        if (gvSupplier.PageCount > 0)
        {
            if (gvSupplier.PageIndex + 1 == gvSupplier.PageCount)
                disButton("last");
            else if (gvSupplier.PageIndex == 0)
                disButton("first");
            else
                disButton("");
        }

    }

    #region 分页
    protected void btnLast_Click(object sender, EventArgs e)
    {
        Paging(gvSupplier.PageCount);
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        Paging(0);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Paging((gvSupplier.PageIndex + 1) > gvSupplier.PageCount ? gvSupplier.PageCount : (gvSupplier.PageIndex + 1));
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Paging((gvSupplier.PageIndex - 1) < 0 ? 0 : (gvSupplier.PageIndex - 1));
    }

    /// <summary>
    /// 翻页
    /// </summary>
    /// <param name="pageIndex">页码</param>
    private void Paging(int pageIndex)
    {
        GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
        gvSupplier_PageIndexChanging(new object(), e);
    }

    protected void gvSupplier_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSupplier.PageIndex = e.NewPageIndex;
        bindInfo();
    }
    #endregion

    private void disButton(string page)
    {
        switch (page)
        {
            case "first":
                btnFirst.Enabled = false;
                btnPrevious.Enabled = false;
                btnNext.Enabled = true;
                btnLast.Enabled = true;

                btnFirst2.Enabled = false;
                btnPrevious2.Enabled = false;
                btnNext2.Enabled = true;
                btnLast2.Enabled = true;
                break;
            case "last":
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                btnNext.Enabled = false;
                btnLast.Enabled = false;

                btnFirst2.Enabled = true;
                btnPrevious2.Enabled = true;
                btnNext2.Enabled = false;
                btnLast2.Enabled = false;
                break;
            default:
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                btnNext.Enabled = true;
                btnLast.Enabled = true;

                btnFirst2.Enabled = true;
                btnPrevious2.Enabled = true;
                btnNext2.Enabled = true;
                btnLast2.Enabled = true;
                break;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindInfo();
    }

    protected void gvSupplier_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dv = (DataRowView)e.Row.DataItem;
            //LinkButton lnkHankConfirm = (LinkButton)e.Row.FindControl("lnkHankConfirm");           
            HyperLink hpConfirm = (HyperLink)e.Row.FindControl("hpConfirm");
            LinkButton lnkHankConfirm2 = (LinkButton)e.Row.FindControl("lnkHankConfirm2");
            LinkButton lnkSend = (LinkButton)e.Row.FindControl("lnkSend");
            LinkButton btnCancel = (LinkButton)e.Row.FindControl("btnCancel");
            lnkSend.CommandArgument = dv["recipientId"].ToString();
            lnkSend.OnClientClick = "var returnValue = confirmEmail('" + dv["supplier_email"] + "');if(returnValue == 2){return true;}else if (returnValue == 1){alert('供应商邮件不能为空！');return false;}else{return false;}";

            hpConfirm.NavigateUrl = "RecipientConfirm.aspx?backUrl=HandConfirmRecipient.aspx&GeneralID=" + dv["Expr1"].ToString() + "&RecipientId=" + dv["recipientId"].ToString();

            if (int.Parse(dv["inUse"] == DBNull.Value ? ((int)State.PRInUse.Use + "") : dv["inUse"].ToString()) != (int)State.PRInUse.Use)
            {
                hpConfirm.Visible = false;
                lnkSend.Visible = false;
                btnCancel.Visible = false;
            }
            //收货人
            Label labRev = (Label)e.Row.FindControl("labRev");
            labRev.Text = dv["receivername"].ToString();
            labRev.Attributes["onclick"] = "ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(int.Parse(dv["goods_receiver"].ToString())) + "');";
            //附加收货人
            Label labAppend = (Label)e.Row.FindControl("labAppend");
            labAppend.Text = dv["appendReceiverName"] == DBNull.Value ? "" : dv["appendReceiverName"].ToString();
            if (dv["appendReceiver"] != DBNull.Value && !string.IsNullOrEmpty(dv["appendReceiver"].ToString()))
                labAppend.Attributes["onclick"] = "ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(int.Parse(dv["appendReceiver"].ToString())) + "');";
            //确认状态
            Label labConfirm = (Label)e.Row.FindControl("labConfirm");
            labConfirm.Text = ESP.Purchase.Common.State.recipientConfirm_Names[int.Parse(dv["isConfirm"] == DBNull.Value ? "0" : dv["isConfirm"].ToString())];

            if (dv["appendReceiver"] != DBNull.Value && dv["appendReceiver"] != null && int.Parse(dv["appendReceiver"].ToString()) > 0)//包含附加收货人
            {
                //if ((int.Parse(CurrentUser.SysID) == int.Parse(dv["goods_receiver"].ToString()) || int.Parse(CurrentUser.SysID) == int.Parse(dv["requestor"].ToString())) && int.Parse(dv["isconfirm"].ToString()) != State.recipentConfirm_No)
                //{
                //    //当前登陆人是收货人时，不可以确认"未确认"之外的状态
                //    if (int.Parse(dv["goods_receiver"].ToString()) != int.Parse(dv["appendReceiver"].ToString()))
                //        hpConfirm.Visible = false;
                //}
                if (int.Parse(CurrentUser.SysID) == int.Parse(dv["appendReceiver"].ToString()) && int.Parse(dv["isconfirm"].ToString()) == State.recipentConfirm_Emp1)
                {
                    hpConfirm.Visible = true;
                }
                else
                {
                    //当前登陆人是收货人时，不可以确认"收货人确认"之外的状态
                    //if (int.Parse(dv["goods_receiver"].ToString()) != int.Parse(dv["appendReceiver"].ToString()))
                        hpConfirm.Visible = false;
                }
                //如果是附加收货人已确认，手动确认隐藏
                //if (int.Parse(dv["isconfirm"].ToString()) == State.recipentConfirm_Emp2)
                //{
                //    hpConfirm.Visible = false;
                //}
                if (int.Parse(dv["isconfirm"].ToString()) != State.recipentConfirm_Emp2)
                {
                    lnkSend.Visible = false;
                }
            }
            if (int.Parse(dv["isconfirm"].ToString()) == (int)ESP.Purchase.Common.State.recipentConfirm_Supplier || (Convert.ToInt32(dv["isconfirm"]) == (int)ESP.Purchase.Common.State.recipentConfirm_Emp2 && dv["source"].ToString().Trim() != "协议供应商") || PeriodRecipientManager.isJoinPeriod(int.Parse(dv["recipientId"].ToString())))
            {

            }
            else
            {
                e.Row.Cells[4].Controls.Clear();
                e.Row.Cells[4].Text = string.Empty;
            }
            ////如果收货单是未确认和收货人确认，不能进行供应商确认
            //if (dv["isconfirm"] == DBNull.Value || int.Parse(dv["isconfirm"].ToString()) == State.recipentConfirm_No || int.Parse(dv["isconfirm"].ToString()) == State.recipentConfirm_Emp1)
            //    lnkHankConfirm2.Visible = false;
            showSupplierConfirm(lnkHankConfirm2, int.Parse(dv["recipientId"].ToString()));
        }
    }

    /// <summary>
    /// 是否显示供应商确认
    /// </summary>
    /// <param name="confirmButton"></param>
    /// <param name="recipientId"></param>
    private void showSupplierConfirm(LinkButton confirmButton, int recipientId)
    {
        RecipientInfo recipientModel = RecipientManager.GetModel(recipientId);
        GeneralInfo generalModel = GeneralInfoManager.GetModel(recipientModel.Gid);
        if (recipientModel.IsConfirm == State.recipentConfirm_Emp2)
        {
            //已二级确认
            //if (generalModel.Requisitionflow == State.requisitionflow_toO || generalModel.Requisitionflow == State.requisitionflow_toC)
            //{
            //    if (generalModel.source == "协议供应商")
            //        confirmButton.Visible = false;//PR-PO,必须协议供应商确认
            //}

            confirmButton.Visible = true;
        }
        else
        {
            //未二级确认
            confirmButton.Visible = false;
        }
    }

    protected void gvSupplier_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "HandConfirm" || e.CommandName == "HandConfirm2" || e.CommandName == "SendMail" || e.CommandName == "Return")
        {
            int recipientId = int.Parse(e.CommandArgument.ToString());
            RecipientInfo model = RecipientManager.GetModel(recipientId);
            GeneralInfo generalInfo = GeneralInfoManager.GetModel(model.Gid);
            if (generalInfo.InUse == (int)State.PRInUse.Use)
            {
                if (e.CommandName == "HandConfirm")//手动确认收货单
                {
                    RecipientManager.updateConfirm(recipientId, generalInfo, 0, false, Request, int.Parse(CurrentUser.SysID));
                    //记录操作日志
                    // ESP.Logging.Logger.Add(string.Format("{0}对T_Recipient表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, recipientId, "手动确认收货单"), "收货单");
                    bindInfo();
                    if (generalInfo.appendReceiver == 0)
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('确认成功，请去付款账期进行付款申请！');", true);
                    }
                    else
                    {
                        if (int.Parse(CurrentUser.SysID) == generalInfo.appendReceiver)
                        {
                            //记录附加收货人确认日志
                            LogInfo log = new LogInfo();
                            log.Gid = model.Gid;
                            log.LogMedifiedTeme = DateTime.Now;
                            log.LogUserId = CurrentUserID;
                            log.Des = CurrentUserName + "(" + CurrentUser.ITCode + ")" + "于" + DateTime.Now.ToString() + "确认收货";
                            ESP.Framework.Entity.AuditBackUpInfo auditBackUp = ESP.Framework.BusinessLogic.AuditBackUpManager.GetLayOffModelByUserID(int.Parse(CurrentUser.SysID));
                            if (auditBackUp != null)
                                log.Des = ESP.Framework.BusinessLogic.EmployeeManager.Get(auditBackUp.BackupUserID).FullNameCN + "代替" + log.Des;
                            LogManager.AddLog(log, Request);

                            RecipientLogInfo Rlog = new RecipientLogInfo();
                            Rlog.Rid = recipientId;
                            Rlog.LogMedifiedTeme = DateTime.Now;
                            Rlog.LogUserId = CurrentUserID;
                            Rlog.Des = CurrentUserName + "(" + CurrentUser.ITCode + ")" + "于" + DateTime.Now.ToString() + "确认收货";
                            if (auditBackUp != null)
                                Rlog.Des = ESP.Framework.BusinessLogic.EmployeeManager.Get(auditBackUp.BackupUserID).FullNameCN + "代替" + Rlog.Des;
                            RecipientLogManager.AddLog(Rlog, Request);

                            hidSupplierEmail.Value = "";
                            SendMail(recipientId);
                            if (generalInfo.source != "协议供应商")
                                ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('确认成功，请去付款账期进行付款申请！');", true);
                            else
                                ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('确认成功，请等待供应商确认收货单！');", true);
                        }
                        else
                        {
                            ESP.ConfigCommon.SendMail.Send1("收货确认", new ESP.Compatible.Employee(generalInfo.appendReceiver).EMail, generalInfo.PrNo + "已创建收货（" + model.RecipientNo + "），您为附加收货人，请进行收货确认。", true);
                            ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('确认成功，请等待附加收货人确认收货单！');", true);
                        }
                    }
                }
                else if (e.CommandName == "HandConfirm2")
                {
                    //RecipientManager.updateConfirm(recipientId, generalInfo, 0, false, Request, int.Parse(CurrentUser.SysID));
                    ESP.Purchase.DataAccess.RecipientDataHelper.updateConfirm(recipientId, ESP.Purchase.Common.State.recipentConfirm_Supplier, null, null);
                    //记录附加收货人确认日志
                    LogInfo log = new LogInfo();
                    log.Gid = model.Gid;
                    log.LogMedifiedTeme = DateTime.Now;
                    log.LogUserId = CurrentUserID;
                    log.Des = CurrentUserName + "(" + CurrentUser.ITCode + ")" + "于" + DateTime.Now.ToString() + "代替供应商确认收货";
                    LogManager.AddLog(log, Request);

                    RecipientLogInfo Rlog = new RecipientLogInfo();
                    Rlog.Rid = recipientId;
                    Rlog.LogMedifiedTeme = DateTime.Now;
                    Rlog.LogUserId = CurrentUserID;
                    Rlog.Des = CurrentUserName + "(" + CurrentUser.ITCode + ")" + "于" + DateTime.Now.ToString() + "代替供应商确认收货";
                    RecipientLogManager.AddLog(Rlog, Request);
                    //记录操作日志
                    ESP.Logging.Logger.Add(string.Format("{0}对T_Recipient表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, recipientId, "供应商手动确认收货单"), "收货单");
                    bindInfo();
                    ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('确认成功，请去付款账期进行付款申请！');", true);
                }
                else if (e.CommandName == "Return")//撤销收货单
                {
                    ESP.Purchase.Entity.SupplierSendInfo sendModel = ESP.Purchase.BusinessLogic.SupplierSendManager.GetModel(recipientId, (int)State.DataType.GR);
                    if (sendModel != null)
                    {
                        //给供应商发送撤销收货通知
                        string url = Request.Url.ToString().Substring(0, Request.Url.ToString().LastIndexOf('/') + 1) + "Print/RecipientPrint.aspx?id=" + generalInfo.id + "&recipientId=" + recipientId;
                        string body = "该收货单已被撤销！</br>" + ESP.ConfigCommon.SendMail.ScreenScrapeHtml(url);
                        ESP.ConfigCommon.SendMail.Send1("收货单撤销通知", sendModel.Email, body, false, new System.Collections.Hashtable());
                        ESP.Purchase.BusinessLogic.SupplierSendManager.Delete(recipientId, (int)State.DataType.GR);
                    }
                    if (RecipientManager.Delete(recipientId, CurrentUser.Name, Request))
                    {
                        //记录操作日志
                        //收货日志----begin
                        LogInfo log = new LogInfo();
                        log.Gid = model.Gid;
                        log.LogMedifiedTeme = DateTime.Now;
                        log.LogUserId = CurrentUserID;
                        log.Des = CurrentUserName + "(" + CurrentUser.ITCode + ")" + "撤销" + model.RecipientNo + "收货单 " + DateTime.Now.ToString();//string.Format(State.log_recipient, CurrentUserName, model.RecipientNo, DateTime.Now.ToString());
                        ESP.Framework.Entity.AuditBackUpInfo auditBackUp = ESP.Framework.BusinessLogic.AuditBackUpManager.GetLayOffModelByUserID(int.Parse(CurrentUser.SysID));
                        if (auditBackUp != null)
                            log.Des = ESP.Framework.BusinessLogic.EmployeeManager.Get(auditBackUp.BackupUserID).FullNameCN + "代替" + log.Des;
                        LogManager.AddLog(log, Request);

                        RecipientLogInfo Rlog = new RecipientLogInfo();
                        Rlog.Rid = recipientId;
                        Rlog.LogMedifiedTeme = DateTime.Now;
                        Rlog.LogUserId = CurrentUserID;
                        Rlog.Des = CurrentUserName + "(" + CurrentUser.ITCode + ")" + "撤销" + model.RecipientNo + "收货单 " + DateTime.Now.ToString(); //string.Format(State.log_recipient, CurrentUserName, model.RecipientNo, DateTime.Now.ToString());
                        if (auditBackUp != null)
                            Rlog.Des = ESP.Framework.BusinessLogic.EmployeeManager.Get(auditBackUp.BackupUserID).FullNameCN + "代替" + Rlog.Des;
                        RecipientLogManager.AddLog(Rlog, Request);

                        //ESP.Logging.Logger.Add(string.Format("{0}对T_Recipient表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, recipientId, "撤销收货单"), "收货单");
                        bindInfo();
                        ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('收货单撤销成功！');", true);
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('收货单撤销失败！');", true);
                    }
                }
                else if (e.CommandName == "SendMail")//给供应商发邮件
                {
                    SendMail(recipientId);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + ESP.Purchase.Common.State.DisabledMessageForPR + "');window.location='OrderCommitList.aspx'", true);
            }
        }
    }

    private void SendMail(int recipientId)
    {
        RecipientInfo model = RecipientManager.GetModel(recipientId);
        GeneralInfo g = GeneralInfoManager.GetModel(model.Gid);
        int reccount = RecipientManager.getRecipientCount(g.id);
        string mailMsg = "";
        if (reccount == 1)
        {
            if (model.Status == State.recipientstatus_All)//一次全额
                mailMsg = "全额";
            else if (model.Status == State.recipientstatus_Unsure)//一次实发金额
                mailMsg = "实发金额";
        }
        else
        {
            if (model.Status == State.recipientstatus_All) //剩余金额
                mailMsg = "剩余金额";
            else if (model.Status == State.recipientstatus_Unsure) //实发剩余金额
                mailMsg = "实发剩余金额";
        }
        if (model.Status == State.recipientstatus_Part) //分批收货
        {
            mailMsg = "";
            List<RecipientInfo> recList = RecipientManager.getModelList(" and Gid=@Gid", new List<SqlParameter> { new SqlParameter("@Gid", model.Gid) });
            for (int i = recList.Count; i > 0; i--)
            {
                if (model.Id == recList[recList.Count - i].Id)
                    reccount = i;
            }
        }

        string url = Request.Url.ToString().Substring(0, Request.Url.ToString().LastIndexOf('/') + 1) + "Print/RecipientPrint.aspx?id=" + g.id + "&rec=" + model.RecipientAmount + "&mail=yes&reccount=" + reccount + "&recipientId=" + recipientId;
        string body = ESP.ConfigCommon.SendMail.ScreenScrapeHtml(url);

        if (mailMsg == "")
        {
            mailMsg = "第" + reccount.ToString() + "次";
        }
        // string orderid = g.orderid == "" ? g.PrNo : g.orderid;
        string guid = Guid.NewGuid().ToString();
        string htmlFilePath = Server.MapPath("~") + "ExcelTemplate\\" + "收货单" + guid + ".htm";

        FileHelper.DeleteFile(htmlFilePath);
        FileHelper.SaveFile(htmlFilePath, body);

        string ret = ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPORectoSup(g.orderid, hidSupplierEmail.Value == "" ? g.supplier_email : hidSupplierEmail.Value, body, htmlFilePath, mailMsg);
        if (string.IsNullOrEmpty(ret))
        {
            ESP.Purchase.Entity.SupplierSendInfo sendModel = new SupplierSendInfo();
            sendModel.DataId = recipientId;
            sendModel.DataType = (int)State.DataType.GR;
            sendModel.Email = hidSupplierEmail.Value == "" ? g.supplier_email : hidSupplierEmail.Value;
            ESP.Purchase.BusinessLogic.SupplierSendManager.Add(sendModel);
        }
        GC.Collect();
    }
}
