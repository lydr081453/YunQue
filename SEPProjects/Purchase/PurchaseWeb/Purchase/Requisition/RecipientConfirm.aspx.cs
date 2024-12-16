using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

namespace PurchaseWeb.Purchase.Requisition
{
    public partial class RecipientConfirm : ESP.Web.UI.PageBase
    {

        int recipientid = 0;
        int generalid = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["RecipientId"]))
            {
                recipientid = int.Parse(Request["RecipientId"]);
            }
            if (!string.IsNullOrEmpty(Request["GeneralID"]))
            {
                generalid = int.Parse(Request["GeneralID"]);
            }
            if (!IsPostBack)
            {
                RecipientInfo RModel = RecipientManager.GetModel(recipientid);
                GeneralInfo g = GeneralInfoManager.GetModel(RModel.Gid);
                generalid = g.id;
                //  BindRecipientType(g);
                ItemListBind(" general_id = " + g.id, g);
                BindInfo(g);
                BindgvFPList(g);

                txtAmount.Text = RModel.RecipientAmount.ToString("#,##0.00");
                txtNote.Text = RModel.Des;
                //txtSinglePrice.Text = RModel.SinglePrice;
                //txtCount.Text = RModel.Num.ToString();
                if (!string.IsNullOrEmpty(RModel.FileUrl))
                {
                    hpFile.ToolTip = "下载附件：" + RModel.FileUrl;
                    hpFile.ImageUrl = "/images/ico_04.gif";
                    this.hpFile.NavigateUrl = "/Purchase/Requisition/UpfileDownload.aspx?RecipientId=" + RModel.Id.ToString();
                }
                else
                {
                    hpFile.Visible = false;
                }
            }
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

        private void BindgvFPList(GeneralInfo g)
        {
            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@Gid", generalid));
            List<RecipientInfo> items = RecipientManager.getModelList(" and Gid=@Gid", parms);
            gvFP.DataSource = items;
            gvFP.DataBind();

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ESP.Framework.Entity.AuditBackUpInfo auditBackUp = ESP.Framework.BusinessLogic.AuditBackUpManager.GetLayOffModelByUserID(int.Parse(CurrentUser.SysID));
            RecipientInfo RModel = RecipientManager.GetModel(recipientid);
            decimal confirmAmount = Decimal.Parse(txtAmount.Text);
            if (confirmAmount > RModel.RecipientAmount * Decimal.Parse("1.1"))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('附加收货金额已经超出收货金额10%！');", true);
                return;
            }

            if (this.fileupContract.FileName != string.Empty)
            {
                string fileName = "rp_" + Guid.NewGuid().ToString() + "_" + this.fileupContract.FileName;
                string urlHost = System.Configuration.ConfigurationManager.AppSettings["UpFilePath"] + @"upfile\";
                this.fileupContract.SaveAs(urlHost + fileName);

                RModel.FileUrl = @"upfile\" + fileName;
            }

            RModel.RecipientAmount = confirmAmount;
            RModel.Note = txtNote.Text;
           // RModel.SinglePrice = txtSinglePrice.Text;
            //RModel.Num = txtCount.Text;
            RModel.IsConfirm = State.recipentConfirm_Emp2;

            RecipientManager.UpdateConfirm(RModel);


                //记录附加收货人确认日志
                LogInfo log = new LogInfo();
                log.Gid = RModel.Gid;
                log.LogMedifiedTeme = DateTime.Now;
                log.LogUserId = CurrentUserID;
                log.Des = CurrentUserName + "(" + CurrentUser.ITCode + ")" + "于" + DateTime.Now.ToString() + "确认收货";
                
            if (auditBackUp != null)
                    log.Des = ESP.Framework.BusinessLogic.EmployeeManager.Get(auditBackUp.BackupUserID).FullNameCN + "代替" + log.Des;
                LogManager.AddLog(log, Request);

                RecipientLogInfo Rlog = new RecipientLogInfo();
                Rlog.Rid = RModel.Id;
                Rlog.LogMedifiedTeme = DateTime.Now;
                Rlog.LogUserId = CurrentUserID;
                Rlog.Des = CurrentUserName + "(" + CurrentUser.ITCode + ")" + "于" + DateTime.Now.ToString() + "确认收货";
                if (auditBackUp != null)
                    Rlog.Des = ESP.Framework.BusinessLogic.EmployeeManager.Get(auditBackUp.BackupUserID).FullNameCN + "代替" + Rlog.Des;
                RecipientLogManager.AddLog(Rlog, Request);

                hidSupplierEmail.Value = "";
                string exMail =  string.Empty;
                try
                {
                    SendMail(RModel.Id);
                }
                catch
                {
                    exMail = "(邮件发送失败!)";
                }
            ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('确认成功，请等待供应商确认收货单！"+exMail+"');", true);
            

            Response.Redirect("HandConfirmRecipient.aspx");
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("HandConfirmRecipient.aspx");
        }

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
}