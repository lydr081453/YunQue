using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;


public partial class Purchase_Requisition_RecipientDetail : ESP.Web.UI.PageBase
{
    int generalid = 0;
    static decimal YPrice = 0;
    static decimal WPrice = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
        {
            generalid = int.Parse(Request[RequestName.GeneralID]);
        }
        if (!IsPostBack)
        {
            YPrice = 0;
            WPrice = 0;
            GeneralInfo g = GeneralInfoManager.GetModel(generalid);
            BindRecipientType(g);
            ItemListBind(" general_id = " + generalid, g);
            BindInfo(g);
            BindgvFPList(g);

            //dlScore.DataSource = ESP.Purchase.BusinessLogic.ScoreContentManager.GetList("");
            //dlScore.DataBind();
        }
        SetFocus(btnCancel);
    }

    private void ItemListBind(string term, GeneralInfo g)
    {
        DataSet ds = OrderInfoManager.GetList(term);
        gdvItem.DataSource = ds.Tables[0];
        gdvItem.DataBind();
        DgRecordCount.Text = ds.Tables[0].Rows.Count.ToString();
        if (ds.Tables[0].Rows.Count == 0)
        {
            labTotalPrice.Text = "0";
        }
        else
        {
            decimal totalprice = 0;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                totalprice += decimal.Parse(ds.Tables[0].Rows[i]["total"].ToString());
                //labTotalPrice.Text = labQE.Text = totalprice.ToString("#,##0.00");
                labTotalPrice.Text = totalprice.ToString("#,##0.####");
                //labQE.Text = (totalprice - decimal.Parse(g.sow4)).ToString("#,##0.00");
                labQE.Text = (totalprice).ToString("#,##0.00");
            }
        }
    }

    public void BindInfo(GeneralInfo g)
    {
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

            paymentInfo.Model = g;
            paymentInfo.BindInfo();

            paymentInfo.Model = g;
            paymentInfo.BindInfo();
            paymentInfo.TotalPrice = decimal.Parse(labTotalPrice.Text);

            //BindBTB(g.id);

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

            labNote.Text = g.sow3.Trim();

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
            hidSupplierEmail.Value = g.supplier_email;
        }
    }

    protected void btn_Click(Object sender, EventArgs e)
    {
        Response.Redirect(Request["backUrl"] == null ? "OrderList.aspx" : Request["backUrl"].ToString());
    }

    int num = 1;
    protected void gdvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = num.ToString();
            num++;
            Label labDown = (Label)e.Row.FindControl("labDown");
            labDown.Text = labDown.Text == "" ? "" : "<a target='_blank' href='../../" + labDown.Text + "'><img src='/images/ico_04.gif' border='0' /></a>";
            DataRowView dr = (DataRowView)e.Row.DataItem;
            int productTypeID = int.Parse(dr["productType"].ToString());
            TypeInfo ty = TypeManager.GetModel(productTypeID);

            e.Row.Cells[2].Text = ty == null ? "" : TypeManager.GetModel(productTypeID).typename;
        }
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


    private void BindRecipientType(GeneralInfo generalModel)
    {
        string[] recipientType = State.recipient_state;

            for (int i = 0; i < recipientType.Length; i++)
            {
                ddlRecipientType.Items.Insert(i, new ListItem(recipientType[i].ToString(), i.ToString()));
            }
        
        ddlRecipientType.Items.Insert(0, new ListItem("请选择...", "-1"));
    }

    private void BindgvFPList(GeneralInfo g)
    {
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@Gid", generalid));
        List<RecipientInfo> items = RecipientManager.getModelList(" and Gid=@Gid", parms);
        gvFP.DataSource = items;
        gvFP.DataBind();
        decimal totalPrice = decimal.Parse(labTotalPrice.Text);
        foreach (RecipientInfo model in items)
        {
            YPrice += model.RecipientAmount;
        }
        WPrice = totalPrice - YPrice;// - decimal.Parse(g.sow4);
        labYPrice.Text = YPrice.ToString("#,##0.00");
        //labPrepay.Text = decimal.Parse(g.sow4).ToString("#,##0.00");
        labWPrice.Text = WPrice.ToString("#,##0.00");

        if (items.Count > 0)
        {
            ddlRecipientType.SelectedValue = State.recipientstatus_Part.ToString();
            litRemark.Text = "说明：分多次进行收货。";
            ddlRecipientType.Enabled = false;
            tr3.Visible = true;
            tr4.Visible = false;
            btnFP.Visible = true;
        }
    }

    protected void ddlRecipientType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRecipientType.SelectedValue == State.recipientstatus_All.ToString())
        {
            tr1.Visible = true;
            tr2.Visible = false;
            tr3.Visible = false;
            tr4.Visible = false;
            litRemark.Text = "说明：以采购物品的实际金额进行全额收货。";
            btnZF1.Visible = true;
            btnZF2.Visible = false;
            btnFP.Visible = false;
        }
        else if (ddlRecipientType.SelectedValue == State.recipientstatus_Unsure.ToString())
        {
            tr1.Visible = false;
            tr2.Visible = true;
            tr3.Visible = false;
            tr4.Visible = false;
            litRemark.Text = "说明：实发金额收货，收货金额上限不能超过总金额的10%。";
            btnZF1.Visible = false;
            btnZF2.Visible = true;
            btnFP.Visible = false;
        }
        else if (ddlRecipientType.SelectedValue == State.recipientstatus_Part.ToString())
        {
            tr1.Visible = false;
            tr2.Visible = false;
            tr3.Visible = true;
            tr4.Visible = false;
            btnZF1.Visible = false;
            btnZF2.Visible = false;
            btnFP.Visible = true;
            litRemark.Text = "说明：分多次进行收货。";
        }
        else
        {
            tr1.Visible = false;
            tr2.Visible = false;
            tr3.Visible = false;
            tr4.Visible = true;
            litRemark.Text = "";
            btnZF1.Visible = false;
            btnZF2.Visible = false;
            btnFP.Visible = false;
        }
    }
    //全额收货，直接更新PR单状态为已完成收货
    protected void btnZF1_Click(object sender, EventArgs e)
    {
        RecipientInfo model = new RecipientInfo();
        model.Gid = generalid;
        model.RecipientName = CurrentUser.Name;
        model.RecipientDate = DateTime.Now;
        model.RecipientAmount = decimal.Parse(labQE.Text);
        model.Note = txtNote1.Text;
        model.Status = State.recipientstatus_All;
        //model.account_name = txtaccountName.Text.Trim();
        //model.account_bank = txtaccountBank.Text.Trim();
        //model.account_number = txtaccountNum.Text.Trim();
        model.RecipientNo = RecipientManager.CreateRecipientNo(generalid, false);
       // model.AppraiseRemark = txtAppraise.Text.Trim();

        model.SinglePrice = txtSinglePrice1.Text.Trim();
        model.Num = txtNum1.Text.Trim();
        model.Des = txtDes1.Text.Trim();

        //if (this.fileupContract.FileName != string.Empty)
        //{
        //    string fileName = "rp_" + Guid.NewGuid().ToString() + "_" + this.fileupContract.FileName;
        //    string urlHost = System.Configuration.ConfigurationManager.AppSettings["UpFilePath"] + @"upfile\";
        //    this.fileupContract.SaveAs(urlHost + fileName);

        //    model.FileUrl = @"upfile\" + fileName;
        //}
        //else
        //{
        //    System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "btnSave1", "alert('请上传结算单附件！');", true);
        //    return;
        //}

        if (this.fileupContract.FileName != string.Empty)
        {
            string fileName = "rp_" + Guid.NewGuid().ToString() + "_" + this.fileupContract.FileName;
            string urlHost = System.Configuration.ConfigurationManager.AppSettings["UpFilePath"] + @"upfile\";
            this.fileupContract.SaveAs(urlHost + fileName);

            model.FileUrl = @"upfile\" + fileName;
        }

        Save(model, "全额");
    }

    //实发金额,直接更新PR单状态为已完成收货
    protected void btnZF2_Click(object sender, EventArgs e)
    {
        RecipientInfo model = new RecipientInfo();
        model.Gid = generalid;
        model.RecipientName = CurrentUser.Name;
        model.RecipientDate = DateTime.Now;
        model.RecipientAmount = decimal.Parse(txtFQE.Text);
        model.Note = txtNote2.Text;
        model.Status = State.recipientstatus_Unsure;
        //model.account_name = txtaccountName.Text.Trim();
        //model.account_bank = txtaccountBank.Text.Trim();
        //model.account_number = txtaccountNum.Text.Trim();
        model.RecipientNo = RecipientManager.CreateRecipientNo(generalid, false);
       // model.AppraiseRemark = txtAppraise.Text.Trim();
        model.SinglePrice = txtSinglePrice2.Text.Trim();
        model.Num = txtNum2.Text.Trim();
        model.Des = txtDes2.Text.Trim();
        //if (this.fileupContract.FileName != string.Empty)
        //{
        //    string fileName = "rp_" + Guid.NewGuid().ToString() + "_" + this.fileupContract.FileName;
        //    string urlHost = System.Configuration.ConfigurationManager.AppSettings["UpFilePath"] + @"upfile\";
        //    this.fileupContract.SaveAs(urlHost + fileName);

        //    model.FileUrl = @"upfile\" + fileName;
        //}
        //else
        //{
        //    System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "btnSave1", "alert('请上传结算单附件！');", true);
        //    return;
        //}

        if (this.fileupContract.FileName != string.Empty)
        {
            string fileName = "rp_" + Guid.NewGuid().ToString() + "_" + this.fileupContract.FileName;
            string urlHost = System.Configuration.ConfigurationManager.AppSettings["UpFilePath"] + @"upfile\";
            this.fileupContract.SaveAs(urlHost + fileName);

            model.FileUrl = @"upfile\" + fileName;
        }

        GeneralInfo g = GeneralInfoManager.GetModel(generalid);

        decimal totalPrice = decimal.Parse(labTotalPrice.Text);
        //收货金额上限10%
        //if (!( model.RecipientAmount <= (totalPrice + (totalPrice * decimal.Parse("0.1")))))
        //if ((model.RecipientAmount + decimal.Parse(g.sow4)) > (totalPrice + (totalPrice * decimal.Parse("0.1"))))
        if ((model.RecipientAmount) > (totalPrice + (totalPrice * decimal.Parse("0.1"))))
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "btnSave1", "alert('收货金额上限已超10%，请重新填写金额！');", true);
            return;
        }

        Save(model, "实发金额");
    }

    //分批收货
    protected void btnFP_Click(object sender, EventArgs e)
    {
        if (hidisLast.Value == "0")
        {//如果不是最后一次收货，更新PR单状态为收货中
            btnZF3_Click();
        }
        else
        {//如果是分批全额收货且金额相符，更新PR单状态为已完成收货
            if (decimal.Parse(txtFPQE.Text) == WPrice)
            {
                btnZF4_Click();
            }
            else
            {//如果是分批全额收货且金额不符,更新PR单状态为已完成收货
                btnZF5_Click();
            }
        }
    }

    protected void btnZF3_Click()
    {
        RecipientInfo model = new RecipientInfo();
        model.Gid = generalid;
        model.RecipientName = CurrentUser.Name;
        model.RecipientDate = DateTime.Now;
        model.RecipientAmount = decimal.Parse(txtFPQE.Text);
        model.Note = txtNote3.Text;
        model.Status = State.recipientstatus_Part;
        model.RecipientNo = RecipientManager.CreateRecipientNo(generalid, true);
        //model.AppraiseRemark = txtAppraise.Text.Trim();
        model.SinglePrice = txtSinglePrice3.Text.Trim();
        model.Num = txtNum3.Text.Trim();
        model.Des = txtDes3.Text.Trim();
        GeneralInfo generalModel = GeneralInfoManager.GetModel(generalid);
        if (generalModel.appendReceiver > 0)//有附加收货人
            model.IsConfirm = ESP.Purchase.Common.State.recipentConfirm_Emp1;//等待附加收货人进行收货确认
        else
            model.IsConfirm = ESP.Purchase.Common.State.recipentConfirm_Emp2;
        //if (generalModel.Requisitionflow == State.requisitionflow_toR || generalModel.Requisitionflow == State.requisitionflow_toC) //pr -> pr或pr -> 合同， 直接确认成功
        //    model.IsConfirm = State.recipentConfirm_Emp2;

            //if (this.fileupContract.FileName != string.Empty)
            //{
            //    string fileName = "rp_" + Guid.NewGuid().ToString() + "_" + this.fileupContract.FileName;
            //    string urlHost = System.Configuration.ConfigurationManager.AppSettings["UpFilePath"] + @"upfile\";
            //    this.fileupContract.SaveAs(urlHost + fileName);

            //    model.FileUrl = @"upfile\" + fileName;
            //}
            //else
            //{
            //    System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "btnSave1", "alert('请上传结算单附件！');", true);
            //    return;
            //}

        if (this.fileupContract.FileName != string.Empty)
        {
            string fileName = "rp_" + Guid.NewGuid().ToString() + "_" + this.fileupContract.FileName;
            string urlHost = System.Configuration.ConfigurationManager.AppSettings["UpFilePath"] + @"upfile\";
            this.fileupContract.SaveAs(urlHost + fileName);

            model.FileUrl = @"upfile\" + fileName;
        }

        generalModel.supplier_email = hidSupplierEmail.Value.Trim();
        string Msg1 = ESP.ITIL.BusinessLogic.申请单业务设置.申请单收货中(CurrentUser, ref generalModel);
        if (Msg1 != "")
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "btnSave1", "alert('" + Msg1 + "');", true);
            return;
        }
        int recipientId = RecipientManager.AddAndUpdateStatus(model, generalModel, new List<ESP.Purchase.Entity.ScoreRecordInfo>());
        //记录操作日志
        ESP.Logging.Logger.Add(string.Format("{0}对T_Recipient表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, recipientId, "添加第" + RecipientManager.getRecipientCount(generalid) + "次收货"), "收货");
        if (recipientId > 0)
        {
            recamount = model.RecipientAmount;
            SendMail(recipientId, "");

            //收货日志----begin
            LogInfo log = new LogInfo();
            log.Gid = generalid;
            log.LogMedifiedTeme = DateTime.Now;
            log.LogUserId = CurrentUserID;
            log.Des = string.Format(State.log_recipient, CurrentUserName + "(" + CurrentUser.ITCode + ")", model.RecipientNo, DateTime.Now.ToString());
            ESP.Framework.Entity.AuditBackUpInfo auditBackUp = ESP.Framework.BusinessLogic.AuditBackUpManager.GetLayOffModelByUserID(int.Parse(CurrentUser.SysID));
            if (auditBackUp != null)
                log.Des = ESP.Framework.BusinessLogic.EmployeeManager.Get(auditBackUp.BackupUserID).FullNameCN + "代替" + log.Des;
            LogManager.AddLog(log, Request);
            //addSupplyLog();
            RecipientLogInfo Rlog = new RecipientLogInfo();
            Rlog.Rid = recipientId;
            Rlog.LogMedifiedTeme = DateTime.Now;
            Rlog.LogUserId = CurrentUserID;
            Rlog.Des = string.Format(State.log_recipient, CurrentUserName + "(" + CurrentUser.ITCode + ")", model.RecipientNo, DateTime.Now.ToString());
            if (auditBackUp != null)
                Rlog.Des = ESP.Framework.BusinessLogic.EmployeeManager.Get(auditBackUp.BackupUserID).FullNameCN + "代替" + Rlog.Des;
            RecipientLogManager.AddLog(Rlog, Request);

            //收货日志----end
            if (model.IsConfirm != ESP.Purchase.Common.State.recipentConfirm_Emp1)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "btnSave1", "window.location.href='" + Request["backUrl"] + "';alert('收货确认成功！');", true);
            }
            else
            {
                 //给附加收货人进行确认
                ESP.ConfigCommon.SendMail.Send1("收货确认", new ESP.Compatible.Employee(generalModel.appendReceiver).EMail, generalModel.PrNo + "已创建收货（" + model.RecipientNo + "），您为附加收货人，请进行收货确认。", true);
                System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "btnSave1", "window.location.href='" + Request["backUrl"] + "';alert('收货确认成功！请等待附加收货人进行确认！');", true);
            }
        }
        else
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "btnSave1", "alert('收货确认失败！');", true);
        }
    }

    //剩余金额,直接更新PR单状态为已完成收货
    protected void btnZF4_Click()
    {
        RecipientInfo model = new RecipientInfo();
        model.Gid = generalid;
        model.RecipientName = CurrentUser.Name;
        model.RecipientDate = DateTime.Now;
        txtFPQE.Text = WPrice.ToString();
        model.RecipientAmount = decimal.Parse(txtFPQE.Text);
        model.Note = txtNote3.Text;
        model.Status = State.recipientstatus_All;
        model.RecipientNo = RecipientManager.CreateRecipientNo(generalid, true);
        //model.AppraiseRemark = txtAppraise.Text.Trim();
        model.SinglePrice = txtSinglePrice3.Text.Trim();
        model.Num = txtNum3.Text.Trim();
        model.Des = txtDes3.Text.Trim();

        //if (this.fileupContract.FileName != string.Empty)
        //{
        //    string fileName = "rp_" + Guid.NewGuid().ToString() + "_" + this.fileupContract.FileName;
        //    string urlHost = System.Configuration.ConfigurationManager.AppSettings["UpFilePath"] + @"upfile\";
        //    this.fileupContract.SaveAs(urlHost + fileName);

        //    model.FileUrl = @"upfile\" + fileName;
        //}
        //else
        //{
        //    System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "btnSave1", "alert('请上传结算单附件！');", true);
        //    return;
        //}

        if (this.fileupContract.FileName != string.Empty)
        {
            string fileName = "rp_" + Guid.NewGuid().ToString() + "_" + this.fileupContract.FileName;
            string urlHost = System.Configuration.ConfigurationManager.AppSettings["UpFilePath"] + @"upfile\";
            this.fileupContract.SaveAs(urlHost + fileName);

            model.FileUrl = @"upfile\" + fileName;
        }

        Save(model, "剩余金额");
    }

    //实发剩余金额,直接更新PR单状态为已完成收货
    protected void btnZF5_Click()
    {
        RecipientInfo model = new RecipientInfo();
        model.Gid = generalid;
        model.RecipientName = CurrentUser.Name;
        model.RecipientDate = DateTime.Now;
        model.RecipientAmount = decimal.Parse(txtFPQE.Text);
        model.Note = txtNote3.Text;
        model.Status = State.recipientstatus_Unsure;
        model.RecipientNo = RecipientManager.CreateRecipientNo(generalid, true);
        //model.AppraiseRemark = txtAppraise.Text.Trim();
        model.SinglePrice = txtSinglePrice3.Text.Trim();
        model.Num = txtNum3.Text.Trim();
        model.Des = txtDes3.Text.Trim();

        //if (this.fileupContract.FileName != string.Empty)
        //{
        //    string fileName = "rp_" + Guid.NewGuid().ToString() + "_" + this.fileupContract.FileName;
        //    string urlHost = System.Configuration.ConfigurationManager.AppSettings["UpFilePath"] + @"upfile\";
        //    this.fileupContract.SaveAs(urlHost + fileName);

        //    model.FileUrl = @"upfile\" + fileName;
        //}
        //else
        //{
        //    System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "btnSave1", "alert('请上传结算单附件！');", true);
        //    return;
      // }

        if (this.fileupContract.FileName != string.Empty)
        {
            string fileName = "rp_" + Guid.NewGuid().ToString() + "_" + this.fileupContract.FileName;
            string urlHost = System.Configuration.ConfigurationManager.AppSettings["UpFilePath"] + @"upfile\";
            this.fileupContract.SaveAs(urlHost + fileName);

            model.FileUrl = @"upfile\" + fileName;
        }

        decimal totalPrice = decimal.Parse(labTotalPrice.Text.Replace(",", ""));
        //if (!((model.RecipientAmount + YPrice) >= (totalPrice - (totalPrice * decimal.Parse("0.1"))) && (model.RecipientAmount + YPrice) <= (totalPrice + (totalPrice * decimal.Parse("0.1")))))
        if ((model.RecipientAmount + YPrice) > (totalPrice + (totalPrice * decimal.Parse("0.1"))))
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "btnSave1", "alert('收货金额上限已超10%，请重新填写金额！');", true);
            return;
        }

        Save(model, "实发剩余金额");
    }

    decimal recamount = 0;
    //更新PR单状态为已完成收货
    private void Save(RecipientInfo model, string mailMsg)
    {
        GeneralInfo generalModel = GeneralInfoManager.GetModel(generalid);
        if (generalModel.status == State.requisition_recipiented)
        {
            //System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "btnSave1", "window.location.href='" + Request["backUrl"] + "';alert('收货已完成,无法重复操作！');", true);
            //return;
        }
        string Msg1 = ESP.ITIL.BusinessLogic.申请单业务设置.申请单完成收货(CurrentUser, ref generalModel);
        if (Msg1 != "")
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "btnSave1", "alert('" + Msg1 + "');", true);
            return;
        }
        generalModel.supplier_email = hidSupplierEmail.Value.Trim();
        if (generalModel.appendReceiver > 0)//有附加收货人
            model.IsConfirm = ESP.Purchase.Common.State.recipentConfirm_Emp1;//等待附加收货人进行收货确认
        else
            model.IsConfirm = ESP.Purchase.Common.State.recipentConfirm_Emp2;
        int recipientId = RecipientManager.AddAndUpdateStatus(model, generalModel, new List<ESP.Purchase.Entity.ScoreRecordInfo>());
        //记录操作日志
        ESP.Logging.Logger.Add(string.Format("{0}对T_Recipient表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, recipientId, "添加" + mailMsg + "收货"), "收货");
        if (recipientId > 0)
        {
            recamount = model.RecipientAmount;
            //收货日志----begin
            LogInfo log = new LogInfo();
            log.Gid = generalid;
            log.LogMedifiedTeme = DateTime.Now;
            log.LogUserId = CurrentUserID;
            log.Des = string.Format(State.log_recipient, CurrentUserName + "(" + CurrentUser.ITCode + ")", model.RecipientNo, DateTime.Now.ToString());
            ESP.Framework.Entity.AuditBackUpInfo auditBackUp = ESP.Framework.BusinessLogic.AuditBackUpManager.GetLayOffModelByUserID(int.Parse(CurrentUser.SysID));
            if (auditBackUp != null)
                log.Des = ESP.Framework.BusinessLogic.EmployeeManager.Get(auditBackUp.BackupUserID).FullNameCN + "代替" + log.Des;

            LogManager.AddLog(log, Request);

            RecipientLogInfo Rlog = new RecipientLogInfo();
            Rlog.Rid = recipientId;
            Rlog.LogMedifiedTeme = DateTime.Now;
            Rlog.LogUserId = CurrentUserID;
            Rlog.Des = string.Format(State.log_recipient, CurrentUserName + "(" + CurrentUser.ITCode + ")", model.RecipientNo, DateTime.Now.ToString());
            if (auditBackUp != null)
                Rlog.Des = ESP.Framework.BusinessLogic.EmployeeManager.Get(auditBackUp.BackupUserID).FullNameCN + "代替" + Rlog.Des;
            RecipientLogManager.AddLog(Rlog, Request);
            //收货日志----end

            string exMail = string.Empty;
            try
            {
                SendMail(recipientId, mailMsg);
            }
            catch
            {
                exMail = "(邮件发送失败!)";
            }
            if (model.IsConfirm != ESP.Purchase.Common.State.recipentConfirm_Emp1)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "btnSave1", "window.location.href='" + Request["backUrl"] + "';alert('收货确认成功！"+exMail+"');", true);
            }
            else
            {
                exMail = string.Empty;
                try
                {
                    //给附加收货人进行确认
                    ESP.ConfigCommon.SendMail.Send1("收货确认", new ESP.Compatible.Employee(generalModel.appendReceiver).EMail, generalModel.PrNo + "已创建收货（" + model.RecipientNo + "），您为附加收货人，请进行收货确认。", true);
                }
                catch
                {
                    exMail = "(邮件发送失败!)";
                }
                System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "btnSave1", "window.location.href='" + Request["backUrl"] + "';alert('收货确认成功！请等待附加收货人进行确认！"+exMail+"');", true);
            }
        }
        else
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "btnSave1", "alert('收货确认失败！');", true);
        }
    }

    //private List<ESP.Purchase.Entity.ScoreRecordInfo> getScoreList()
    //{
    //    GeneralInfo g = GeneralInfoManager.GetModel(generalid);
    //    List<ESP.Purchase.Entity.ScoreRecordInfo> scoreList = new List<ScoreRecordInfo>();
    //    for (int i = 0; i < dlScore.Items.Count; i++)
    //    {
    //        PurchaseWeb.UserControls.Edit.Score control = (PurchaseWeb.UserControls.Edit.Score)dlScore.Items[i].FindControl("Score");
    //        scoreList.Add(control.GetRecordInfo(g));
    //    }
    //    ESP.Purchase.Entity.ScoreRecordInfo score = new ScoreRecordInfo();
    //    score.PRID = g.id;
    //    score.PRNO = g.PrNo;
    //    int supplierId = 0;
    //    string supplierName = "";
    //    ESP.Purchase.BusinessLogic.OrderInfoManager.getSupplierId(" and general_id=" + g.id, out supplierId, out supplierName);
    //    score.SupplierID = supplierId;
    //    score.SupplierName = supplierName;
    //    score.Remark = txtAppraise.Text.Trim();
    //    scoreList.Add(score);
    //    return scoreList;
    //}

    //private void addSupplyLog()
    //{
    //    OrderInfo orderModel = ESP.Purchase.BusinessLogic.OrderInfoManager.GetModelByGeneralID(generalid);
    //    if (orderModel != null && orderModel.supplierId != 0)
    //    {
            //ESPAndSupplySuppliersRelation relation = ESPAndSupplySuppliersRelationManager.GetModelByEid(orderModel.supplierId);
            //if (relation != null)
            //{
            //    FeedBackInfo feedbak = new FeedBackInfo();
            //    feedbak.supplierId = relation.SupplySupplierId;
            //    feedbak.supplierName = orderModel.supplierName;
            //    feedbak.status = 0;
            //    feedbak.creator= CurrentUser.SysID;
            //    feedbak.creatorName = CurrentUser.Name;
            //    feedbak.createTime = DateTime.Now;
            //    feedbak.ModifiedDate = DateTime.Now;
            //    feedbak.ModifiedManagerId = 0;
            //   // feedbak.feedback=this.txtAppraise.Text;
            //    for (int i = 0; i < dlScore.Items.Count; i++)
            //    {
            //        PurchaseWeb.UserControls.Edit.Score control = (PurchaseWeb.UserControls.Edit.Score)dlScore.Items[i].FindControl("Score");
            //        switch (i)
            //        { 
            //            case 0:
            //                feedbak.PriceScore = (control.GetScore()+1).ToString();
            //                break;
            //            case 1:
            //                feedbak.QualityScore = (control.GetScore()+1).ToString();
            //                break;
            //            case 2:
            //                feedbak.ServiceScore = (control.GetScore()+1).ToString();
            //                break;
            //            case 3:
            //                feedbak.TimelinessScore = (control.GetScore()+1).ToString();
            //                break;
            //        }
            //    }
            //    FeedBackManager.Add(feedbak);
            //}
      //  }
   // }

    private void SendMail(int recipientId, string mailMsg)
    {
        GeneralInfo g = GeneralInfoManager.GetModel(generalid);
        if (g.appendReceiver == 0)//2009-4-27之前的申请单，不包含附加收货人的，可以给供应商直接发信，
        {
            int reccount = RecipientManager.getRecipientCount(generalid);
            string url = Request.Url.ToString().Substring(0, Request.Url.ToString().LastIndexOf('/') + 1) + "Print/RecipientPrint.aspx?id=" + generalid + "&rec=" + recamount + "&mail=yes&reccount=" + reccount + "&recipientId=" + recipientId;
            string body = ESP.ConfigCommon.SendMail.ScreenScrapeHtml(url);

            if (mailMsg == "")
            {
                mailMsg = "第" + reccount.ToString() + "次";
            }

            string htmlFilePath = Server.MapPath("~") + "ExcelTemplate\\" + "收货单" + g.orderid + mailMsg + "收货.htm";

            FileHelper.DeleteFile(htmlFilePath);
            FileHelper.SaveFile(htmlFilePath, body);

            string ret = ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPORectoSup(g.orderid, hidSupplierEmail.Value, body, htmlFilePath, mailMsg);
        }
    }

    //protected void dlScore_ItemDataBound(object sender, DataListItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        ESP.Purchase.Entity.ScoreContentInfo model = (ESP.Purchase.Entity.ScoreContentInfo)e.Item.DataItem;
    //        PurchaseWeb.UserControls.Edit.Score scoreControl = (PurchaseWeb.UserControls.Edit.Score)e.Item.FindControl("Score");
    //        scoreControl.SetControls(model);
    //    }
    //}

    //private void BindBTB(int gid)
    //{
    //    var addedList = ESP.Purchase.BusinessLogic.ShunyaXiaoMiManager.GetInfoList(" gid=" + gid, new List<SqlParameter>());
    //    List<ShunyaXiaoMiInfo> newList = new List<ShunyaXiaoMiInfo>();

    //    foreach (var xm in addedList)
    //    {
    //        var newdata = PinTuiBaoDataLoading.TransOrderJsonDataToModel(xm, xm.XiaoMiNo);
    //        newList.Add(newdata);
    //    }

    //    this.gvPTB.DataSource = newList;
    //    this.gvPTB.DataBind();
    //}

    //protected void gvPTB_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
    //    {
    //    }

    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        ESP.Purchase.Entity.ShunyaXiaoMiInfo model = (ESP.Purchase.Entity.ShunyaXiaoMiInfo)e.Row.DataItem;

    //        Label lblOrderStatus = ((Label)e.Row.FindControl("lblOrderStatus"));
    //        Label lblSaler = ((Label)e.Row.FindControl("lblSaler"));
    //        Label lblPayByOther = ((Label)e.Row.FindControl("lblPayByOther"));

    //        lblOrderStatus.Text = State.XiaoMiOrderStatus[model.OrderStatus];

    //        lblPayByOther.Text = State.XiaoMiPayByOtherStatus[model.PayByOtherStatus];

    //        if (!string.IsNullOrEmpty(model.SalerEnterpriseName))
    //        {
    //            lblSaler.Text = model.SalerEnterpriseName;
    //        }
    //        else if (!string.IsNullOrEmpty(model.SalerRealName))
    //        {
    //            lblSaler.Text = model.SalerRealName;
    //        }
    //        else
    //        {
    //            lblSaler.Text = model.Saler;
    //        }


    //    }
    //}

}
