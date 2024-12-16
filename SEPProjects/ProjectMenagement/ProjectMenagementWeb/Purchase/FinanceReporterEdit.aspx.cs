using System;
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
    public partial class FinanceReporterEdit : ESP.Web.UI.PageBase
    {
        int returnId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ReturnID]))
                {
                    returnId = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]);
                }
                BindInfo();

            }
        }
        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
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

        private string GetAuditLog(int Rid)
        {
            ReturnInfo ReturnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(Rid);
            IList<ESP.Finance.Entity.AuditLogInfo> histList = ESP.Finance.BusinessLogic.AuditLogManager.GetReturnList(ReturnModel.ReturnID);
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

        private void SetSubTotal(string mediaOrderIDs)
        {
            lblTotal.Text = ESP.Purchase.BusinessLogic.MediaOrderManager.GetSubTotalByPaymentUser(mediaOrderIDs);
        }
        private void BindInfo()
        {
            List<int> tmplist = new List<int>();
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
            if (returnModel != null && returnModel.PRID != null)
                TopMessage.Model = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(returnModel.PRID.Value);
            IList<ESP.Purchase.Entity.MediaOrderInfo> mediaList = ESP.Purchase.BusinessLogic.MediaOrderManager.GetModelList(" meidaOrderID in(" + returnModel.MediaOrderIDs.TrimEnd(',') + ")");
            foreach (ESP.Purchase.Entity.MediaOrderInfo model in mediaList)
            {
                if (model.IsPayment == 1)
                {
                    tmplist.Add(model.MeidaOrderID);
                }
            }
            this.SelectedItems = tmplist;
            this.gvG.DataSource = mediaList;
            this.gvG.DataBind();
            SetSubTotal(returnModel.MediaOrderIDs.TrimEnd(','));
            lblApplicant.Text = returnModel.RequestEmployeeName;
            lblApplicant.Attributes["onclick"] = "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(returnModel.RequestorID.Value) + "');";
            lblBeginDate.Text = returnModel.PreBeginDate == null ? "" : returnModel.PreBeginDate.Value.ToString("yyyy-MM-dd");
            // lblEndDate.Text = returnModel.PreEndDate == null ? "" : returnModel.PreEndDate.Value.ToString("yyyy-MM-dd");
            lblInceptDate.Text = returnModel.ReturnPreDate == null ? "" : returnModel.ReturnPreDate.Value.ToString("yyyy-MM-dd");
            lblInceptPrice.Text = returnModel.PreFee == null ? "" : returnModel.PreFee.Value.ToString("#,##0.00");
            lblPeriodType.Text = returnModel.PaymentTypeName;
            lblPayRemark.Text = returnModel.ReturnContent;
            lblPRNo.Text = returnModel.PRNo;
            lblProjectCode.Text = returnModel.ProjectCode;
            lblReturnCode.Text = returnModel.ReturnCode;
            lblPayCode.Text = returnModel.PaymentTypeCode;
            lblStatus.Text = ESP.Finance.Utility.ReturnPaymentType.ReturnStatusString(returnModel.ReturnStatus.Value, 0, returnModel.IsDiscount);
            if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.CommonPR || returnModel.ReturnType == null)
                lblPRNo.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + returnModel.PRID.ToString() + "'style='cursor: hand' target='_blank'>" + returnModel.PRNo + "</a>";
            else
                lblPRNo.Text = returnModel.PRNo;
            ESP.Finance.Entity.ReturnGeneralInfoListViewInfo vmodel = ESP.Finance.BusinessLogic.ReturnGeneralInfoListViewManager.GetModel(returnModel.ReturnID);
            //从重汇列表获取供应商信息
            IList<ESP.Finance.Entity.BankCancelInfo> cancelList = ESP.Finance.BusinessLogic.BankCancelManager.GetList(" ReturnID=" + returnModel.ReturnID.ToString() + " and (ordertype is null or ordertype=1 )");
            if (cancelList != null && cancelList.Count > 0)
            {
                this.lblSupplierName.Text = cancelList[cancelList.Count - 1].OldBankAccountName;
                this.lblSupplierBank.Text = cancelList[cancelList.Count - 1].NewBankName;
                this.lblSupplierAccount.Text = cancelList[cancelList.Count - 1].NewBankAccount;
            }
            else if (vmodel != null)
            {
                this.lblSupplierName.Text = vmodel.Account_name;
                this.lblSupplierBank.Text = vmodel.Account_bank;
                this.lblSupplierAccount.Text = vmodel.Account_number;
            }
            lblFactFee.Text = returnModel.FactFee == null ? "" : returnModel.FactFee.Value.ToString("#,##0.00");
            radioInvoice.SelectedValue = returnModel.IsInvoice == null ? "" : returnModel.IsInvoice.Value.ToString();
            lblPreDate.Text = returnModel.ReturnPreDate == null ? "" : returnModel.ReturnPreDate.Value.ToString("yyyy-MM-dd");
            lblBankName.Text = returnModel.BankName;
            lblAccountName.Text = returnModel.BankAccountName;
            lblAccount.Text = returnModel.BankAccount;
            lblBankAddress.Text = returnModel.BankAddress;
            lblPaymentType.Text = returnModel.PaymentTypeName;
            this.lblLog.Text = this.GetAuditLog(returnModel.ReturnID);
            labDepartment.Text = returnModel.DepartmentName;
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            CollectSelected();
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ReturnID]))
            {
                returnId = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]);
            }
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);

            int ret = ESP.Purchase.BusinessLogic.MediaOrderManager.UpdateMediaIsPayment(this.SelectedItems, returnModel.MediaOrderIDs.TrimEnd(','), CurrentUserID, returnModel.PRID.Value);
            if (ret == 1)
                Response.Redirect("FinanceReporterList.aspx");
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存失败!');'", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("FinanceReporterList.aspx");
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


        /// <summary>
        /// 从当前页收集选中项的情况
        /// </summary>
        protected void CollectSelected()
        {
            if (this.SelectedItems == null)
                SelectedItems = new List<int>();
            else
                SelectedItems.Clear();
            string sMID = string.Empty;
            for (int i = 0; i < this.gvG.Rows.Count; i++)
            {
                sMID = gvG.Rows[i].Cells[1].Text.Trim();
                int MID; int.TryParse(sMID, out MID);
                CheckBox cb = this.gvG.Rows[i].FindControl("chkMedia") as CheckBox;
                if (SelectedItems.Contains(MID) && !cb.Checked)
                    SelectedItems.Remove(MID);
                if (!SelectedItems.Contains(MID) && cb.Checked)
                    SelectedItems.Add(MID);
            }
            this.SelectedItems = SelectedItems;
        }

    }
}
