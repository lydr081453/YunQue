using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using ESP.Finance.Entity;
namespace FinanceWeb.Purchase
{
    public partial class BatchReturnEdit : ESP.Web.UI.PageBase
    {
        ESP.Finance.Entity.PNBatchInfo batchModel = null;
        private IList<ReturnInfo> returnList;
        int batchid = 0;
        //string HuNanBranch = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            batchid = Convert.ToInt32(Request[RequestName.BatchID]);
            batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchid);
            returnList = ESP.Finance.BusinessLogic.PNBatchManager.GetReturnList(batchid);
            //HuNanBranch = System.Configuration.ConfigurationManager.AppSettings["FinanceAuditHuNanBranch"];
            if (!IsPostBack)
            {
                trMediaHeader.Visible = false;
                trMedia.Visible = false;
                trTotal.Visible = false;
                bindInfo();
                Search();
            }
        }
        protected void ddlBank_SelectedIndexChangeed(object sender, EventArgs e)
        {
            int bankid = Convert.ToInt32(this.ddlBank.SelectedItem.Value);
            ESP.Finance.Entity.BankInfo bankModel = ESP.Finance.BusinessLogic.BankManager.GetModel(bankid);
            this.lblAccountName.Text = bankModel.BankAccountName;
            this.lblAccount.Text = bankModel.BankAccount;
            this.lblBankAddress.Text = bankModel.Address;

        }

        private void bindInfo()
        {
            this.lblBatchId.Text = batchModel.BatchID.ToString();
            this.lblPurchaseBatchCode.Text = batchModel.PurchaseBatchCode;
            List<SqlParameter> paramlist = new List<SqlParameter>();

            string term = " IsBatch=@IsBatch";
            SqlParameter p = new SqlParameter("@IsBatch", SqlDbType.Bit);
            p.Value = true;
            paramlist.Add(p);
            IList<PaymentTypeInfo> paylist = ESP.Finance.BusinessLogic.PaymentTypeManager.GetList(term, paramlist);
            ddlPaymentType.DataSource = paylist;
            ddlPaymentType.DataTextField = "PaymentTypeName";
            ddlPaymentType.DataValueField = "PaymentTypeID";
            ddlPaymentType.DataBind();
            ddlPaymentType.Items.Insert(0, new ListItem("请选择", "0"));

            paramlist.Clear();
            SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@branchcode", SqlDbType.NVarChar, 50);
            p1.Value = batchModel.BranchCode;
            paramlist.Add(p1);
            IList<BankInfo> bankist = ESP.Finance.BusinessLogic.BankManager.GetList(" branchcode=@branchcode ", paramlist);
            ddlBank.DataSource = bankist;
            ddlBank.DataTextField = "BankName";
            ddlBank.DataValueField = "BankID";
            ddlBank.DataBind();
            ddlBank.Items.Insert(0, new ListItem("请选择", "0"));
            if (batchModel.BankID != null)
                ddlBank.SelectedValue = batchModel.BankID.Value.ToString();
            if (batchModel.PaymentTypeID != null)
                ddlPaymentType.SelectedValue = batchModel.PaymentTypeID.Value.ToString();
            this.lblLog.Text = GetAuditLog(batchid);
        }

        private string GetAuditLog(int Rid)
        {
            IList<ESP.Finance.Entity.AuditLogInfo> histList = ESP.Finance.BusinessLogic.AuditLogManager.GetBatchList(Rid);
            string loginfo = string.Empty;
            foreach (ESP.Finance.Entity.AuditLogInfo model in histList)
            {
                string austatus = string.Empty;
                if (model.AuditStatus == (int)AuditHistoryStatus.PassAuditing)
                {
                    austatus = "审批通过";
                }
                else if (model.AuditStatus == (int)AuditHistoryStatus.TerminateAuditing)
                {
                    austatus = "审批驳回";
                }
                string auditdate = model.AuditDate == null ? "" : model.AuditDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                loginfo += model.AuditorEmployeeName + "(" + model.AuditorUserName + ")" + austatus + "[" + auditdate + "]" + model.Suggestion + "<br/>";

            }
            return loginfo;
        }


        private void Search()
        {
            if (batchModel != null)
            {
                this.txtBatchCode.Text = batchModel.BatchCode;
                this.lblSupplierName.Text = batchModel.SupplierName;
                this.txtSupplierAccount.Text = batchModel.SupplierBankAccount;
                this.txtSupplierBank.Text = batchModel.SupplierBankName;
                this.lblAccount.Text = batchModel.BankAccount;
                this.lblAccountName.Text = batchModel.BankAccountName;
                this.lblBankAddress.Text = batchModel.BankAddress;
                this.txtReturnPreDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.lblBranchName.Text = batchModel.BranchCode;
                if (batchModel.PaymentTypeID != null)
                    this.ddlPaymentType.SelectedValue = batchModel.PaymentTypeID.Value.ToString();
                this.radioInvoice.SelectedValue = batchModel.IsInvoice == null ? "" : batchModel.IsInvoice.Value.ToString();
                var sortReturnList = from c in returnList orderby c.ReturnID select c;
                this.gvG.DataSource = sortReturnList;
                this.gvG.DataBind();

                decimal totalAmounts = 0;
                if (returnList != null && returnList.Count > 0)
                {
                    foreach (ESP.Finance.Entity.ReturnInfo model in returnList)
                    {
                        if (model.FactFee != null && model.FactFee.Value != 0)
                            totalAmounts += model.FactFee.Value;
                        else
                            totalAmounts += model.PreFee.Value;
                    }
                }
                this.lblFactFee.Text = totalAmounts.ToString("#,##0.00");


                if (batchModel.Status == (int)PaymentStatus.MajorAudit)
                {
                    trNext.Visible = true;

                }
                else
                {
                    trNext.Visible = false;
                }


            }
        }

        protected void CollectSelected()
        {
            if (this.SelectedItems == null)
                SelectedItems = new List<int>();
            else
                SelectedItems.Clear();

            if (this.AllMediaItems == null)
                AllMediaItems = new ArrayList();
            else
                AllMediaItems.Clear();

            string sMID = string.Empty;

            for (int i = 0; i < this.gvMedia.Rows.Count; i++)
            {
                sMID = gvMedia.Rows[i].Cells[1].Text.Trim();
                int MID; int.TryParse(sMID, out MID);
                CheckBox cb = this.gvMedia.Rows[i].FindControl("chkMedia") as CheckBox;
                if (SelectedItems.Contains(MID) && !cb.Checked)
                    SelectedItems.Remove(MID);
                if (!SelectedItems.Contains(MID) && cb.Checked)
                    SelectedItems.Add(MID);
                AllMediaItems.Add(MID);
            }
            this.SelectedItems = SelectedItems;
            this.AllMediaItems = AllMediaItems;
        }

        protected List<int> SelectedItems
        {
            get
            {
                return (ViewState["mySelectedItems"] != null) ? (List<int>)ViewState["mySelectedItems"] : null;
            }
            set
            {
                ViewState["mySelectedItems"] = value;
            }
        }

        protected ArrayList AllMediaItems
        {
            get
            {
                return (ViewState["AllMediaItems"] != null) ? (ArrayList)ViewState["AllMediaItems"] : null;
            }
            set
            {
                ViewState["AllMediaItems"] = value;
            }
        }

        protected void gvMedia_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label labPaymenter = (Label)e.Row.FindControl("labPaymenter");

                ESP.Purchase.Entity.MediaOrderInfo model = (ESP.Purchase.Entity.MediaOrderInfo)e.Row.DataItem;
                if (model.PaymentUserID != null && model.PaymentUserID.Value != 0)
                {
                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(model.PaymentUserID.Value);
                    labPaymenter.Text = emp.Name;
                    labPaymenter.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(Convert.ToInt32(emp.SysID)) + "');");
                }
                CheckBox cb = e.Row.FindControl("chkMedia") as CheckBox;
                //这里的处理是为了回显之前选中的情况
                if (e.Row.RowIndex > -1 && this.SelectedItems != null)
                {
                    if (this.SelectedItems != null && model != null)
                    {
                        if (this.SelectedItems.Contains(model.MeidaOrderID))
                        {
                            cb.Checked = true;
                            if (batchModel.Status == (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete)
                                cb.Enabled = false;
                        }
                        else
                        {
                            cb.Checked = false;
                            cb.Enabled = true;
                        }
                    }
                }
            }
        }

        private void SetSubTotal(string ids)
        {
            lblTotal.Text = ESP.Purchase.BusinessLogic.MediaOrderManager.GetTotalAmountByUser(ids);
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {

            string filename = string.Empty;
            string serverpath = Server.MapPath(ESP.Finance.Configuration.ConfigurationManager.MediaOrderPath);
            ESP.Finance.BusinessLogic.ReturnManager.ExportBatchData(Convert.ToInt32(CurrentUser.SysID), batchid, serverpath, out filename);
            if (!string.IsNullOrEmpty(filename))
            {
                outExcel(serverpath + filename, filename, true);
            }

        }

        protected void btnAudit2_Click(object sender, EventArgs e)
        {
            if (!ValidAudited())
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
                return;
            }

            ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(batchModel.BranchCode);
            IList<ESP.Finance.Entity.PNBatchRelationInfo> relationList = ESP.Finance.BusinessLogic.PNBatchRelationManager.GetList(" BatchID=" + batchModel.BatchID.ToString(), null);
            ESP.Finance.Entity.ReturnInfo firstReturnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(relationList[0].ReturnID.Value);



            int FirstFinanceID = branchModel.FirstFinanceID;
            ESP.Finance.Entity.BranchDeptInfo branchdept = ESP.Finance.BusinessLogic.BranchDeptManager.GetModel(branchModel.BranchID, firstReturnModel.DepartmentID.Value);
            if (branchdept != null)
                FirstFinanceID = branchdept.FianceFirstAuditorID;

            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(FirstFinanceID);

            batchModel.PaymentUserID = Convert.ToInt32(emp.SysID);
            batchModel.PaymentUserName = emp.ITCode;
            batchModel.PaymentCode = emp.ID;
            batchModel.PaymentEmployeeName = emp.Name;
            batchModel.Status = (int)PaymentStatus.MajorAudit;
            batchModel.Description = this.txtRemark.Text.Trim();
            int result = ESP.Finance.BusinessLogic.PNBatchManager.BatchAudit(batchModel, null, "", CurrentUser, (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing, false);
            if (result == 1)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('审批驳回成功!');", true);
                Response.Redirect("BatchReturnList.aspx");
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (batchModel != null)
            {
                setModelInfo(batchModel, true);
                if (string.IsNullOrEmpty(this.txtBatchCode.Text.Trim()))
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('批次号必填!');", true);
                    return;
                }
                if (ESP.Finance.BusinessLogic.PNBatchManager.Exist(this.txtBatchCode.Text.Trim(), batchModel.BatchID) != 0)
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('批次号已经存在!');", true);
                    return;
                }
            }
            //如果是媒体稿费收集已付款的记者
            string mediaOrderIDs = string.Empty;

            CollectSelected();
            if (AllMediaItems != null && AllMediaItems.Count > 0)
            {
                for (int i = 0; i < AllMediaItems.Count; i++)
                {
                    mediaOrderIDs += AllMediaItems[i].ToString() + ",";
                }
                mediaOrderIDs = mediaOrderIDs.TrimEnd(',');
            }
            int result = ESP.Finance.BusinessLogic.PNBatchManager.BatchAudit(batchModel, SelectedItems, mediaOrderIDs, CurrentUser, (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing, true);
            Response.Redirect("BatchReturnEdit.aspx" + Request.Url.Query);
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存成功!');", true);


        }

        protected void btnAudit_Click(object sender, EventArgs e)
        {

            if (!ValidAudited())
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('本条记录已经过您的审核或您没有权限审核!');", true);
                return;
            }
            if (batchModel != null)
            {
                foreach (ReturnInfo returnModel in returnList)
                {
                    if (returnModel.PRID != null && returnModel.PRID.Value > 0)
                    {
                        string alertMessage = "";

                        if (alertMessage != "")
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + alertMessage + "');", true);
                            return;
                        }
                    }
                }

                setModelInfo(batchModel, false);

                if (batchModel.Status == (int)PaymentStatus.FinanceComplete || batchModel.Status == (int)PaymentStatus.FinanceLevel3 || (batchModel.Status == (int)PaymentStatus.FinanceLevel1))
                {
                    if (string.IsNullOrEmpty(this.txtBatchCode.Text.Trim()))
                    {
                        ClientScript.RegisterStartupScript(typeof(string), "", "alert('批次号必填!');", true);
                        return;
                    }
                    if (ESP.Finance.BusinessLogic.PNBatchManager.Exist(this.txtBatchCode.Text.Trim(), batchModel.BatchID) != 0)
                    {
                        ClientScript.RegisterStartupScript(typeof(string), "", "alert('批次号已经存在!');", true);
                        return;
                    }
                }
                if (batchModel.Status == (int)PaymentStatus.MajorAudit || batchModel.Status == (int)PaymentStatus.FinanceLevel3)
                {
                    if (this.txtNextAuditor.Text == string.Empty || this.hidNextAuditor.Value == string.Empty)
                    {
                        ClientScript.RegisterStartupScript(typeof(string), "", "alert('下一级审核人必填!');", true);
                        return;
                    }
                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(Convert.ToInt32(this.hidNextAuditor.Value));
                    batchModel.PaymentUserID = Convert.ToInt32(emp.SysID);
                    batchModel.PaymentUserName = emp.ITCode;
                    batchModel.PaymentEmployeeName = emp.Name;
                    batchModel.PaymentCode = emp.ID;

                }
                //如果是媒体稿费收集已付款的记者
                //string mediaOrderIDs = string.Empty;
                //CollectSelected();
                //if (AllMediaItems != null && AllMediaItems.Count > 0)
                //{
                //    for (int i = 0; i < AllMediaItems.Count; i++)
                //    {
                //        mediaOrderIDs += AllMediaItems[i].ToString() + ",";
                //    }
                //    mediaOrderIDs = mediaOrderIDs.TrimEnd(',');
                //}
                int result = ESP.Finance.BusinessLogic.PNBatchManager.BatchAudit(batchModel, SelectedItems, "", CurrentUser, (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing, false);
                Response.Redirect("BatchReturnList.aspx");
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核成功!');", true);

            }

        }


        protected void btnTip_Click(object sender, EventArgs e)
        {
            ESP.Finance.Entity.AuditLogInfo audit = new ESP.Finance.Entity.AuditLogInfo();
            audit.FormID = batchid;
            audit.Suggestion = this.txtRemark.Text;
            audit.AuditDate = DateTime.Now;
            audit.AuditorSysID = int.Parse(CurrentUser.SysID);
            audit.AuditorUserCode = CurrentUser.ID;
            audit.AuditorEmployeeName = CurrentUser.Name;
            audit.AuditorUserName = CurrentUser.ITCode;
            audit.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.Tip;
            audit.FormType = (int)ESP.Finance.Utility.FormType.PNBatch;
            int ret = ESP.Finance.BusinessLogic.AuditLogManager.AddBatch(audit);
            if (ret > 0)
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='BatchReturnList.aspx';alert('留言保存成功！');", true);
        }

        private bool checkIsNeedFinanceDiretorAudit(PNBatchInfo batchModel)
        {
            bool ret = false;
            IList<ESP.Finance.Entity.PNBatchRelationInfo> relationList = ESP.Finance.BusinessLogic.PNBatchRelationManager.GetList(" BatchID=" + batchModel.BatchID.ToString(), null);

            if (relationList != null && relationList.Count > 0)
            {
                ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(relationList[0].ReturnID.Value);

                if (returnModel.NeedPurchaseAudit)//协议供应商不批
                {
                    return false;
                }

                if (batchModel.Amounts >= 100000)
                {
                    ret = true;
                }

            }
            return ret;
        }


        private void setModelInfo(PNBatchInfo batchModel, bool isSave)
        {
            batchModel.Description = this.txtRemark.Text.Trim();

            if (this.radioInvoice.SelectedIndex >= 0)
                batchModel.IsInvoice = Convert.ToInt32(this.radioInvoice.SelectedValue);
            if (isSave == false)
            {
                if (!string.IsNullOrEmpty(this.hidNextAuditor.Value))
                {
                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(Convert.ToInt32(this.hidNextAuditor.Value));
                    batchModel.PaymentUserID = Convert.ToInt32(emp.SysID);
                    batchModel.PaymentUserName = emp.ITCode;
                    batchModel.PaymentEmployeeName = emp.Name;
                    batchModel.PaymentCode = emp.ID;
                }
                if (batchModel.Status == (int)PaymentStatus.FinanceLevel1)
                {
                    batchModel.Status = (int)PaymentStatus.FinanceComplete;
                }
                else if (batchModel.Status == (int)PaymentStatus.FinanceLevel3 || batchModel.Status == (int)PaymentStatus.FinanceLevel2)
                {
                    batchModel.Status = (int)PaymentStatus.FinanceLevel1;
                }
                else if (batchModel.Status == (int)PaymentStatus.MajorAudit)
                {
                    if (checkIsNeedFinanceDiretorAudit(batchModel))  // >=10w
                        batchModel.Status = (int)PaymentStatus.FinanceLevel3;
                    else
                        batchModel.Status = (int)PaymentStatus.FinanceLevel1;
                }
            }
            batchModel.BatchCode = this.txtBatchCode.Text.Trim();
            batchModel.PaymentTypeID = Convert.ToInt32(this.ddlPaymentType.SelectedValue);
            batchModel.PaymentType = this.ddlPaymentType.SelectedItem.Text;
            if (this.ddlBank.SelectedIndex > 0)
            {
                ESP.Finance.Entity.BankInfo bankModel = ESP.Finance.BusinessLogic.BankManager.GetModel(Convert.ToInt32(this.ddlBank.SelectedValue));
                batchModel.BankID = bankModel.BankID;
                batchModel.BankName = bankModel.BankName;
                batchModel.BranchCode = bankModel.BranchCode;
                batchModel.BranchID = bankModel.BranchID;
                batchModel.DBCode = bankModel.DBCode;
                batchModel.DBManager = bankModel.DBManager;
                batchModel.BankAccount = bankModel.BankAccount;
                batchModel.BankAccountName = bankModel.BankAccountName;
                batchModel.BankAddress = bankModel.Address;
            }
            batchModel.PaymentDate = Convert.ToDateTime(this.txtReturnPreDate.Text);
        }

        private bool ValidAudited()
        {
            //List<SqlParameter> paramlist = new List<SqlParameter>();
            //string term = string.Empty;
            string DelegateUsers = ",";
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
            {
                DelegateUsers += model.UserID.ToString() + ",";
            }
            if (CurrentUserID == batchModel.PaymentUserID.Value || DelegateUsers.IndexOf(batchModel.PaymentUserID.ToString().Trim()) >= 0)
            {
                return true;
            }

            return false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("BatchReturnList.aspx");
        }

        protected void lnkRefresh_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void outExcel(string pathandname, string filename, bool isDelete)
        {
            if (!File.Exists(pathandname))
                return;
            FileStream fin = File.OpenRead(pathandname);
            Response.AddHeader("Content-Disposition", "attachment;   filename=" + filename);
            Response.AddHeader("Connection", "Close");
            Response.AddHeader("Content-Transfer-Encoding", "binary");
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Length", fin.Length.ToString());

            byte[] buf = new byte[1024];
            while (true)
            {
                int length = fin.Read(buf, 0, buf.Length);
                if (length > 0)
                    Response.OutputStream.Write(buf, 0, length);
                if (length < buf.Length)
                    break;
            }
            fin.Close();
            Response.Flush();
            Response.Close();
            if (isDelete)
            {
                FileInfo finfo = new FileInfo(pathandname);
                finfo.Delete();
            }
        }

        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                int reutrnID = int.Parse(e.CommandArgument.ToString());
                ESP.Finance.Entity.ReturnInfo model = ESP.Finance.BusinessLogic.ReturnManager.GetModel(reutrnID);
                ESP.Finance.BusinessLogic.PNBatchRelationManager.DeleteByReturnID(Convert.ToInt32(Request[RequestName.BatchID]), model);
                Search();
            }

        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.ReturnInfo model = (ReturnInfo)e.Row.DataItem;
                Label lblSupplier = (Label)e.Row.FindControl("lblSupplier");
                Label labExpectPaymentPrice = (Label)e.Row.FindControl("labExpectPaymentPrice");
                lblSupplier.Text = model.SupplierName;
                if (labExpectPaymentPrice != null)
                {
                    if (model.FactFee != null && model.FactFee.Value != 0)
                    {
                        labExpectPaymentPrice.Text = model.FactFee.Value.ToString("#,##0.00");
                    }
                    else
                    {
                        labExpectPaymentPrice.Text = model.PreFee.Value.ToString("#,##0.00");
                    }
                }
                Label lblInvoice = (Label)e.Row.FindControl("lblInvoice");
                if (lblInvoice != null)
                    if (model.IsInvoice != null)
                    {
                        if (model.IsInvoice.Value == 1)
                            lblInvoice.Text = "已开";
                        else if (model.IsInvoice.Value == 0)
                            lblInvoice.Text = "未开";
                        else
                            lblInvoice.Text = "无需发票";
                    }

                Label lblPR = (Label)e.Row.FindControl("lblPR");
                if (model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.MediaPR && model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
                    lblPR.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + model.PRID.ToString() + "'style='cursor: hand' target='_blank'>" + model.PRNo + "</a>";
                else
                    lblPR.Text = model.PRNo;
                Label lblStatusName = (Label)e.Row.FindControl("lblStatusName");
                HiddenField hidStatusNameID = (HiddenField)e.Row.FindControl("hidStatusNameID");
                if (lblStatusName != null && hidStatusNameID != null && hidStatusNameID.Value != string.Empty)
                    lblStatusName.Text = ReturnPaymentType.ReturnStatusString(Convert.ToInt32(hidStatusNameID.Value), 0, model.IsDiscount);
                Label lblEdit = (Label)e.Row.FindControl("lblEdit");
                lblEdit.Text = "<a target='_blank' onclick='window.open(\"ReturnFinanceSave.aspx?" + RequestName.ReturnID + "=" + model.ReturnID + "&" + RequestName.BatchID + "=" + batchid.ToString() + "\");' style='cursor: hand' target='_blank'><img  src='/images/ProjectPrint.gif' border='0px;' /></a>";
                HyperLink hylPrint = (HyperLink)e.Row.FindControl("hylPrint");
                if (hylPrint != null)
                {
                    hylPrint.Target = "_blank";
                    hylPrint.NavigateUrl = "Print/PaymantPrint.aspx?" + RequestName.ReturnID + "=" + model.ReturnID.ToString();
                }
            }
        }

        [AjaxPro.AjaxMethod]
        public static List<List<string>> GetBranchList()
        {
            IList<ESP.Finance.Entity.BranchInfo> blist = ESP.Finance.BusinessLogic.BranchManager.GetList(null, null);
            List<List<string>> list = new List<List<string>>();
            List<string> item = null;
            foreach (ESP.Finance.Entity.BranchInfo branch in blist)
            {
                item = new List<string>();
                item.Add(branch.BranchID.ToString());
                item.Add(branch.BranchCode);
                list.Add(item);
            }
            List<string> c = new List<string>();
            c.Add("-1");
            c.Add("请选择...");
            list.Insert(0, c);
            return list;
        }

        [AjaxPro.AjaxMethod]
        public static List<List<string>> GetPayments()
        {
            List<List<string>> retlists = new List<List<string>>();
            string term = " IsBatch=@IsBatch";
            List<SqlParameter> paramlist = new List<SqlParameter>();
            SqlParameter p = new SqlParameter("@IsBatch", SqlDbType.Bit);
            p.Value = true;
            paramlist.Add(p);
            IList<PaymentTypeInfo> paylist = ESP.Finance.BusinessLogic.PaymentTypeManager.GetList(term, paramlist);
            List<string> first = new List<string>();
            first.Add("-1");
            first.Add("请选择..");
            retlists.Add(first);
            foreach (PaymentTypeInfo item in paylist)
            {
                List<string> i = new List<string>();
                i.Add(item.PaymentTypeID.ToString());
                i.Add(item.PaymentTypeName);
                retlists.Add(i);
            }

            return retlists;
        }

        [AjaxPro.AjaxMethod]
        public static List<List<string>> GetBanks(string branchcode)
        {
            List<List<string>> retlists = new List<List<string>>();
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
            SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@branchcode", SqlDbType.NVarChar, 50);
            p1.Value = branchcode;
            paramlist.Add(p1);
            IList<BankInfo> paylist = ESP.Finance.BusinessLogic.BankManager.GetList(" branchcode=@branchcode ", paramlist);
            List<string> first = new List<string>();
            first.Add("-1");
            first.Add("请选择..");
            retlists.Add(first);
            foreach (BankInfo item in paylist)
            {
                List<string> i = new List<string>();
                i.Add(item.BankID.ToString());
                i.Add(item.BankName);
                retlists.Add(i);
            }

            return retlists;
        }

        [AjaxPro.AjaxMethod]
        public static List<string> GetBankModel(int bankID)
        {
            List<string> list = new List<string>();
            ESP.Finance.Entity.BankInfo bankmodel = ESP.Finance.BusinessLogic.BankManager.GetModel(bankID);
            list.Add(bankmodel.BankID.ToString());
            list.Add(bankmodel.BankName);
            list.Add(bankmodel.BankAccount);
            list.Add(bankmodel.BankAccountName);
            list.Add(bankmodel.Address);

            return list;
        }
    }
}
