using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.project
{
    public partial class PrInfoEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
                BindPr(0);
                
            
        }
        protected void btnPr_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hidId.Value))
            {
                //update general info
                ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(Convert.ToInt32(this.hidId.Value));
                generalModel.sow = this.txtSow.Text;
                generalModel.order_overrule = this.txtPOOverrule.Text;
                generalModel.requisition_overrule = this.txtPrOverrule.Text;
                generalModel.contrastRemark = this.txtContrastRemark.Text;
                generalModel.sow2 = this.txtSow2.Text;
                generalModel.sow3 = this.txtSow3.Text;
                generalModel.thirdParty_materielID = ddlType2.SelectedValue;
                generalModel.thirdParty_materielDesc = ddlType2.SelectedItem.Text;

                ESP.Purchase.BusinessLogic.GeneralInfoManager.Update(generalModel);
                //update order info
                string businessContent = "";

                for (int i = 0; i < this.gvMaterial.Rows.Count; i++)
                {
                    int orderid = Convert.ToInt32(gvMaterial.Rows[i].Cells[0].Text.ToString());
                    ESP.Purchase.Entity.OrderInfo ordermodel = ESP.Purchase.BusinessLogic.OrderInfoManager.GetModel(orderid);
                    TextBox txtItemNo = (TextBox)gvMaterial.Rows[i].FindControl("txtItemNo");
                    
                    TextBox txtDesc = (TextBox)gvMaterial.Rows[i].FindControl("txtDesc");
                    ordermodel.Item_No = txtItemNo.Text;
                    ordermodel.desctiprtion = txtDesc.Text;

                    businessContent = generalModel.project_descripttion+":" + txtItemNo.Text;
                    DropDownList ddlType3 = (DropDownList)gvMaterial.Rows[i].FindControl("ddlType3");

                    ordermodel.producttype = int.Parse(ddlType3.SelectedValue);


                    FileUpload FileUpML = (FileUpload)gvMaterial.Rows[i].FindControl("FileUpML");
                    string oldFileUrl = ordermodel.upfile;

                    if (FileUpML.FileName != string.Empty)
                    {
                        oldFileUrl = ordermodel.upfile;
                        string fileName = "wuliao_" + Guid.NewGuid().ToString() + "_" + FileUpML.FileName;
                        string urlHost = System.Configuration.ConfigurationManager.AppSettings["UpFilePath"] + @"upfile\";
                        FileUpML.SaveAs(urlHost + fileName);
                        ordermodel.upfile = @"upfile\" + fileName;
                    }

                    ESP.Purchase.BusinessLogic.OrderInfoManager.Update(ordermodel, 0, "");

                    if(oldFileUrl!=ordermodel.upfile)
                    {
                        ESP.Purchase.Entity.LogInfo fileLog = new ESP.Purchase.Entity.LogInfo();
                        fileLog.Gid = generalModel.id;
                        fileLog.Des = CurrentUser.Name + "更新报价单信息 [" + oldFileUrl + "]";
                        fileLog.LogUserId = int.Parse(CurrentUser.SysID);
                        fileLog.Status = 1;
                        fileLog.LogMedifiedTeme = DateTime.Now;
                        fileLog.IpAddress = Request.UserHostAddress;
                        ESP.Purchase.BusinessLogic.LogManager.AddLog(fileLog, Request);
                    }
                }

               

                //update recipient
                for (int i = 0; i < this.gvRecipient.Rows.Count; i++)
                {
                    int rid = Convert.ToInt32(this.gvRecipient.Rows[i].Cells[0].Text.ToString());
                    ESP.Purchase.Entity.RecipientInfo rmodel = ESP.Purchase.BusinessLogic.RecipientManager.GetModel(rid);
                    TextBox txtNote = (TextBox)this.gvRecipient.Rows[i].FindControl("txtNote");
                    TextBox txtDes = (TextBox)this.gvRecipient.Rows[i].FindControl("txtDes");
                    TextBox txtAppraiseRemark = (TextBox)this.gvRecipient.Rows[i].FindControl("txtAppraiseRemark");
                    rmodel.Note = txtNote.Text; ;
                    rmodel.AppraiseRemark = txtAppraiseRemark.Text;
                    rmodel.Des = txtDes.Text;
                    ESP.Purchase.BusinessLogic.RecipientManager.Update(rmodel);
                }
                
                var returnlist = ESP.Finance.BusinessLogic.ReturnManager.GetList(" prid = " + generalModel.id);
                foreach (var returnModel in returnlist)
                {
                    returnModel.ReturnContent = businessContent;

                    ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
                }

                for (int i = 0; i < this.gvPeriod.Rows.Count; i++)
                {
                    int id = Convert.ToInt32(this.gvPeriod.Rows[i].Cells[0].Text.ToString());
                    ESP.Purchase.Entity.PaymentPeriodInfo pmodel = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetModel(id);

                    TextBox txtRemark = (TextBox)this.gvPeriod.Rows[i].FindControl("txtRemark");
                    TextBox txtPaymentDate = (TextBox)this.gvPeriod.Rows[i].FindControl("txtPaymentDate");

                    pmodel.periodRemark = txtRemark.Text;
                    pmodel.beginDate = DateTime.Parse(txtPaymentDate.Text);
                    ESP.Purchase.BusinessLogic.PaymentPeriodManager.Update(pmodel);

                    if (pmodel.ReturnId != 0)
                    {
                        ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(pmodel.ReturnId);
                        if (returnModel != null)
                        {
                            returnModel.ReturnContent = pmodel.periodRemark;
                            returnModel.PreBeginDate = pmodel.beginDate;
                            ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
                        }
                    }
                }

               

                    //update audit log
                    for (int i = 0; i < this.gvAuditLog.Rows.Count; i++)
                    {
                        int logid = Convert.ToInt32(this.gvAuditLog.Rows[i].Cells[0].Text.ToString());
                        ESP.Purchase.Entity.AuditLogInfo logModel = ESP.Purchase.BusinessLogic.AuditLogManager.GetModel(logid);
                        TextBox txtLog = (TextBox)this.gvAuditLog.Rows[i].FindControl("txtLog");
                        logModel.remark = txtLog.Text;
                        ESP.Purchase.BusinessLogic.AuditLogManager.Update(logModel);
                    }

                //update log
                for (int i = 0; i < this.gvLog.Rows.Count; i++)
                {
                    int logid = Convert.ToInt32(this.gvLog.Rows[i].Cells[0].Text.ToString());
                    ESP.Purchase.Entity.LogInfo logModel = ESP.Purchase.BusinessLogic.LogManager.GetModel(logid);
                    TextBox txtLog = (TextBox)gvLog.Rows[i].FindControl("txtLog");
                    logModel.Des = txtLog.Text;
                    ESP.Purchase.BusinessLogic.LogManager.Update(logModel);
                }

                //update recipient log 
                for (int i = 0; i < this.gvRecipientLog.Rows.Count; i++)
                {
                    int logid = Convert.ToInt32(this.gvRecipientLog.Rows[i].Cells[0].Text.ToString());
                    ESP.Purchase.Entity.RecipientLogInfo logModel = ESP.Purchase.BusinessLogic.RecipientLogManager.GetModel(logid);
                    TextBox txtDes = (TextBox)gvRecipientLog.Rows[i].FindControl("txtDes");
                    logModel.Des = txtDes.Text;
                    ESP.Purchase.BusinessLogic.RecipientLogManager.Update(logModel);
                }
                ESP.Purchase.Entity.LogInfo log = new ESP.Purchase.Entity.LogInfo();
                log.Gid = generalModel.id;
                log.Des = CurrentUser.Name + "修改PR信息 [" + DateTime.Now.ToString() + "]";
                log.LogUserId = int.Parse(CurrentUser.SysID);
                log.Status = 1;
                log.LogMedifiedTeme = DateTime.Now;
                log.IpAddress = Request.UserHostAddress;
                ESP.Purchase.BusinessLogic.LogManager.AddLog(log, Request);
                BindPr(generalModel.id);
            }
        }

        private void BindPr(int id)
        {
            //
            string rid = string.Empty;
            ESP.Purchase.Entity.GeneralInfo generalModel = null;
            if (id == 0)
            {
                string sql = string.Empty;

                if (!string.IsNullOrEmpty(txtKeyword.Text))
                {
                    sql = " id=" + txtKeyword.Text;
                }
                else if (!string.IsNullOrEmpty(txtPrNo.Text))
                {
                    sql = " prno like '%" + this.txtPrNo.Text + "%'";
                }
                if (!string.IsNullOrEmpty(sql))
                {
                    try
                    {
                        generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModelList(sql, new List<System.Data.SqlClient.SqlParameter>())[0];
                    }
                    catch
                    { }
                }
            }
            else
                generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(id);

            if (generalModel == null)
                return;
            this.panel1.Visible = true;
            this.hidId.Value = generalModel.id.ToString();
            this.lblPrId.Text = generalModel.id.ToString();
            this.lblPrno.Text = generalModel.PrNo;
            this.lblProjectCode.Text = generalModel.project_code;
            this.lblRequestor.Text = generalModel.requestorname;
            this.lblSupplier.Text = generalModel.supplier_name;
            this.txtSow.Text = generalModel.sow;
            this.txtSow3.Text = generalModel.sow3;
            this.txtSow2.Text = generalModel.sow2;
            this.txtContrastRemark.Text = generalModel.contrastRemark;
            this.txtPrOverrule.Text = generalModel.requisition_overrule;
            this.txtPOOverrule.Text = generalModel.order_overrule;

            var costList = ESP.Finance.BusinessLogic.ContractCostManager.GetListByProject(generalModel.Project_id, "", null);
            ddlType2.DataSource = costList;
            ddlType2.DataValueField = "CostTypeID";
            ddlType2.DataTextField = "Description";
            ddlType2.DataBind();
            ddlType2.SelectedValue = generalModel.thirdParty_materielID;

            //
            List<ESP.Purchase.Entity.OrderInfo> orderList = ESP.Purchase.BusinessLogic.OrderInfoManager.GetListByGeneralId(generalModel.id);
            this.gvMaterial.DataSource = orderList;
            this.gvMaterial.DataBind();


            //
            List<ESP.Purchase.Entity.RecipientInfo> recipients = ESP.Purchase.BusinessLogic.RecipientManager.getModelList(" and gid=" + generalModel.id.ToString(), new List<System.Data.SqlClient.SqlParameter>());
            this.gvRecipient.DataSource = recipients;
            this.gvRecipient.DataBind();

            //
            IList<ESP.Purchase.Entity.AuditLogInfo> auditlogs = ESP.Purchase.BusinessLogic.AuditLogManager.GetModelListByGID(generalModel.id);
            this.gvAuditLog.DataSource = auditlogs;
            this.gvAuditLog.DataBind();


            //
            List<ESP.Purchase.Entity.LogInfo> logs = ESP.Purchase.BusinessLogic.LogManager.GetLoglistByGId(generalModel.id);
            this.gvLog.DataSource = logs;
            this.gvLog.DataBind();

            List<ESP.Purchase.Entity.PaymentPeriodInfo> periodlist = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetModelList( " gid ="+generalModel.id.ToString());
            this.gvPeriod.DataSource = periodlist;
            this.gvPeriod.DataBind();

            //
            foreach (ESP.Purchase.Entity.RecipientInfo r in recipients)
            {
                rid += r.Id.ToString() + ",";
            }
            rid = rid.TrimEnd(',');
            if (!string.IsNullOrEmpty(rid))
            {
                List<ESP.Purchase.Entity.RecipientLogInfo> recipientlogs = ESP.Purchase.BusinessLogic.RecipientLogManager.GetLoglist(" and rid in(" + rid + ")", new List<System.Data.SqlClient.SqlParameter>());
                this.gvRecipientLog.DataSource = recipientlogs;
                this.gvRecipientLog.DataBind();

            }


        }




        protected void gvMaterial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Purchase.Entity.OrderInfo model = (ESP.Purchase.Entity.OrderInfo)e.Row.DataItem;
                ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(model.general_id);
               
                TextBox txtItemNo = (TextBox)e.Row.FindControl("txtItemNo");
                txtItemNo.Text = model.Item_No;
                TextBox txtDesc = (TextBox)e.Row.FindControl("txtDesc");
                txtDesc.Text = model.desctiprtion;
                Label lblPrice = (Label)e.Row.FindControl("lblPrice");
                lblPrice.Text = model.price.ToString("#,##0.00");
                Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                lblTotal.Text = model.total.ToString("#,##0.00");
                
                Label labDown = (Label)e.Row.FindControl("labDown");

                string[] links = model.upfile.TrimEnd('#').Split('#');
                labDown.Text = "";
                for (int i = 0; i < links.Length; i++)
                {
                    string PurchaseHeader = ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"];
                   
                    if (links[i].Trim() != "")
                    {
                        if (links[i].Trim().IndexOf(".aspx") > 0)
                            labDown.Text += "<a target='_blank' href='/" + links[i].Trim() + "'><img src='/images/ico_04.gif' border='0' /></a>&nbsp;";
                        else
                            labDown.Text += "<a target='_blank' href='" + PurchaseHeader + "Purchase/Requisition/UpfileDownload.aspx?OrderId=" + model.id.ToString() + "&Index=" + i.ToString() + "'><img src='/images/ico_04.gif' border='0' /></a>&nbsp;";
                    }
                }

                DropDownList ddlType3 = (DropDownList)e.Row.FindControl("ddlType3");

                var type3List = ESP.Purchase.BusinessLogic.TypeManager.GetListByParentId(int.Parse(generalModel.thirdParty_materielID));
                ddlType3.DataSource = type3List;
                ddlType3.DataValueField = "TypeID";
                ddlType3.DataTextField = "TypeName";
                ddlType3.DataBind();
                ddlType3.SelectedValue = model.producttype.ToString();
            }
        }


        protected void gvRecipient_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Purchase.Entity.RecipientInfo model = (ESP.Purchase.Entity.RecipientInfo)e.Row.DataItem;
                TextBox txtNote = (TextBox)e.Row.FindControl("txtNote");
                txtNote.Text = model.Note;
                TextBox txtAppraiseRemark = (TextBox)e.Row.FindControl("txtAppraiseRemark");
                txtAppraiseRemark.Text = model.AppraiseRemark;
                TextBox txtDes = (TextBox)e.Row.FindControl("txtDes");
                txtDes.Text = model.Des;
            }
        }

        protected void gvAuditLog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Purchase.Entity.AuditLogInfo model = (ESP.Purchase.Entity.AuditLogInfo)e.Row.DataItem;
                TextBox txtLog = (TextBox)e.Row.FindControl("txtLog");
                txtLog.Text = model.remark;
            }
        }

        protected void gvLog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Purchase.Entity.LogInfo model = (ESP.Purchase.Entity.LogInfo)e.Row.DataItem;
                TextBox txtLog = (TextBox)e.Row.FindControl("txtLog");
                txtLog.Text = model.Des;
            }
        }

        protected void gvRecipientLog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Purchase.Entity.RecipientLogInfo model = (ESP.Purchase.Entity.RecipientLogInfo)e.Row.DataItem;
                TextBox txtDes = (TextBox)e.Row.FindControl("txtDes");
                txtDes.Text = model.Des;
            }
        }

        protected void gvPeriod_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Purchase.Entity.PaymentPeriodInfo model = (ESP.Purchase.Entity.PaymentPeriodInfo)e.Row.DataItem;
                TextBox txtRemark = (TextBox)e.Row.FindControl("txtRemark");
                TextBox txtPaymentDate = (TextBox)e.Row.FindControl("txtPaymentDate");
                txtRemark.Text = model.periodRemark;
                txtPaymentDate.Text = model.beginDate.ToString("yyyy-MM-dd");
            }
        }

        protected void ddlType2_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.gvMaterial.Rows.Count; i++)
            {
                DropDownList ddlType3 = (DropDownList)gvMaterial.Rows[i].FindControl("ddlType3");
                var type3List = ESP.Purchase.BusinessLogic.TypeManager.GetListByParentId(int.Parse(ddlType2.SelectedValue));
                ddlType3.DataSource = type3List;
                ddlType3.DataValueField = "TypeID";
                ddlType3.DataTextField = "TypeName";
                ddlType3.DataBind();
            }

        }


    }
}
