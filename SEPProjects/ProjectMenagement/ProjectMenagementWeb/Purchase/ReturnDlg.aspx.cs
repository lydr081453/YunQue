using System;
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
    public partial class ReturnDlg : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Search();
            }
        }

        protected ArrayList SelectedItems
        {
            get
            {
                return (ViewState["mySelectedItems"] != null) ? (ArrayList)ViewState["mySelectedItems"] : null;
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
                SelectedItems = new ArrayList();
            else
                SelectedItems.Clear();

            for (int i = 0; i < this.gvG.Rows.Count; i++)
            {
                string id = this.gvG.Rows[i].Cells[1].Text;
                CheckBox cb = this.gvG.Rows[i].FindControl("chkReturn") as CheckBox;
                if (SelectedItems.Contains(id) && !cb.Checked)
                    SelectedItems.Remove(id);
                if (!SelectedItems.Contains(id) && cb.Checked)
                    SelectedItems.Add(id);
            }
            this.SelectedItems = SelectedItems;
        }

        private void Search()
        {
            List<SqlParameter> paramlist = new List<SqlParameter>();
            string term = string.Empty;
            string DelegateUsers = string.Empty;
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
            {
                DelegateUsers += model.UserID.ToString() + ",";
            }
            DelegateUsers = DelegateUsers.TrimEnd(',');
            if (!string.IsNullOrEmpty(DelegateUsers))
                term = " (ReturnStatus=@status1 or  ReturnStatus=@status2 or ReturnStatus=@status3 or ReturnStatus=@status4 or (ReturnStatus=@status5 and returnType not in (@PrType,@PrType2))) AND (PaymentUserID=@sysID or PaymentUserID in(" + DelegateUsers + "))";
            else
                term = " (ReturnStatus=@status1 or  ReturnStatus=@status2 or ReturnStatus=@status3 or ReturnStatus=@status4 or (ReturnStatus=@status5 and returnType not in (@PrType,@PrType2))) AND PaymentUserID=@sysID";
            SqlParameter p3 = new SqlParameter("@status3", System.Data.SqlDbType.Int, 4);
            p3.SqlValue = (int)PaymentStatus.MajorAudit;
            paramlist.Add(p3);
            SqlParameter p1 = new SqlParameter("@status1", System.Data.SqlDbType.Int, 4);
            p1.SqlValue = (int)PaymentStatus.FinanceLevel1;
            paramlist.Add(p1);
            SqlParameter p2 = new SqlParameter("@status2", System.Data.SqlDbType.Int, 4);
            p2.SqlValue = (int)PaymentStatus.FinanceLevel2;
            paramlist.Add(p2);
            SqlParameter p4 = new SqlParameter("@status4", System.Data.SqlDbType.Int, 4);
            p4.SqlValue = (int)PaymentStatus.FinanceLevel3;
            paramlist.Add(p4);
            SqlParameter p5 = new SqlParameter("@status5", System.Data.SqlDbType.Int, 4);
            p5.SqlValue = (int)PaymentStatus.WaitReceiving;
            paramlist.Add(p5);
            SqlParameter pType = new SqlParameter("@PrType", System.Data.SqlDbType.Int, 4);
            pType.SqlValue = (int)ESP.Purchase.Common.PRTYpe.PN_ForeGift;
            paramlist.Add(pType);
            SqlParameter pType2 = new SqlParameter("@PrType2", System.Data.SqlDbType.Int, 4);
            pType2.SqlValue = (int)ESP.Purchase.Common.PRTYpe.PN_KillForeGift;
            paramlist.Add(pType2);
            SqlParameter p6 = new SqlParameter("@sysID", System.Data.SqlDbType.Int, 4);
            p6.SqlValue = CurrentUser.SysID;
            paramlist.Add(p6);
            if (!string.IsNullOrEmpty(term))
            {
                if (!string.IsNullOrEmpty(term))
                {
                    if (this.txtKey.Text.Trim() != string.Empty)
                    {
                        term += "  and (PRNO like '%'+@prno+'%' or ProjectCode like '%'+@prno+'%' or returncode like '%'+@prno+'%' or prid like '%'+@prno+'%'  or prefee like '%'+@prno+'%' or SupplierName  like '%'+@prno+'%' )";
                        SqlParameter sp1 = new SqlParameter("@prno", System.Data.SqlDbType.NVarChar, 50);
                        sp1.SqlValue = this.txtKey.Text.Trim();
                        paramlist.Add(sp1);

                    }
                    if (!string.IsNullOrEmpty(txtBeginDate.Text.Trim()) && !string.IsNullOrEmpty(txtEndDate.Text.Trim()))
                    {
                        if (Convert.ToDateTime(txtBeginDate.Text) <= Convert.ToDateTime(txtEndDate.Text))
                        {
                            term += " and LastUpdateDateTime between @beginDate and @endDate";
                            System.Data.SqlClient.SqlParameter sp3 = new System.Data.SqlClient.SqlParameter("@beginDate", System.Data.SqlDbType.DateTime, 8);
                            sp3.SqlValue = this.txtBeginDate.Text;
                            paramlist.Add(sp3);
                            System.Data.SqlClient.SqlParameter sp4 = new System.Data.SqlClient.SqlParameter("@endDate", System.Data.SqlDbType.DateTime, 8);
                            sp4.SqlValue = this.txtEndDate.Text;
                            paramlist.Add(sp4);

                        }
                    }

                        term += " and projectcode like ''+@BranchCode+'%' ";
                        System.Data.SqlClient.SqlParameter pBrach = new System.Data.SqlClient.SqlParameter("@BranchCode", System.Data.SqlDbType.NVarChar, 50);
                        pBrach.SqlValue = Request[RequestName.BranchCode];
                        paramlist.Add(pBrach);
                        string paymentID = Request[RequestName.PaymentID].Trim();
                        if (paymentID == "1")
                        {
                            term += " and (PaymentTypeID=@PaymentTypeID and (returnType=@MediaPR or returnType=@PrivatePR))";
                            System.Data.SqlClient.SqlParameter pPaymentTypeID = new System.Data.SqlClient.SqlParameter("@PaymentTypeID", System.Data.SqlDbType.Int, 4);
                            pPaymentTypeID.SqlValue = Request[RequestName.PaymentID];
                            paramlist.Add(pPaymentTypeID);
                            System.Data.SqlClient.SqlParameter pMediaPR = new System.Data.SqlClient.SqlParameter("@MediaPR", System.Data.SqlDbType.Int, 4);
                            pMediaPR.SqlValue = (int)ESP.Purchase.Common.PRTYpe.MediaPR;
                            paramlist.Add(pMediaPR);
                            System.Data.SqlClient.SqlParameter pPrivatePR = new System.Data.SqlClient.SqlParameter("@PrivatePR", System.Data.SqlDbType.Int, 4);
                            pPrivatePR.SqlValue = (int)ESP.Purchase.Common.PRTYpe.PrivatePR;
                            paramlist.Add(pPrivatePR);
                        }
                        else
                        {
                            term += " and (PaymentTypeID=@PaymentTypeID)";
                            System.Data.SqlClient.SqlParameter pPaymentTypeID = new System.Data.SqlClient.SqlParameter("@PaymentTypeID", System.Data.SqlDbType.Int, 4);
                            pPaymentTypeID.SqlValue = Request[RequestName.PaymentID];
                            paramlist.Add(pPaymentTypeID);
                        }
                    if (!string.IsNullOrEmpty(Request[RequestName.BatchID]))
                    {
                        IList<ESP.Finance.Entity.ReturnInfo> batchList = ESP.Finance.BusinessLogic.PNBatchManager.GetReturnList(Convert.ToInt32(Request[RequestName.BatchID]));
                        if (batchList != null && batchList.Count > 0)
                        {
                            ArrayList tmpList = new ArrayList();
                            foreach (ESP.Finance.Entity.ReturnInfo model in batchList)
                            {
                                tmpList.Add(model.ReturnID);
                            }
                            this.SelectedItems = tmpList;
                        }
                    }
                }
                IList<ESP.Finance.Entity.ReturnInfo> returnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(term, paramlist);
                var tmplist = returnList.OrderBy(N => N.PreBeginDate);
                IList<ReturnInfo> returnlist = tmplist.ToList();
                this.gvG.DataSource = returnlist;
                this.gvG.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            this.txtKey.Text = string.Empty;
            this.txtBeginDate.Text = string.Empty;
            this.txtEndDate.Text = string.Empty;
            Search();
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            CollectSelected();
            try
            {
                DateTime PaymentDate=Convert.ToDateTime(Request[RequestName.PaymentDate]);
                int BatchID=string.IsNullOrEmpty(Request[RequestName.BatchID])?0:Convert.ToInt32(Request[RequestName.BatchID]);
                int ret = 0;// ESP.Finance.BusinessLogic.PNBatchManager.Add(BatchID, SelectedItems, Convert.ToInt32(Request[RequestName.BankID]), CurrentUser, "", PaymentDate);
                if (ret > 0)
                {
                    string script = @"opener.location='BatchReturnEdit.aspx?" + RequestName.BatchID + "=" + ret.ToString()+"&"+RequestName.Operate+"=Add"+"';";
                     ScriptManager.RegisterStartupScript(this, Page.GetType(), Guid.NewGuid().ToString(), "alert('创建成功!');" + script + "window.close();", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('创建失败!');'", true);

                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('"+ex.Message+"');", true);
            }
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.close();", true);
        }
        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.ReturnInfo model = (ReturnInfo)e.Row.DataItem;
                Label lblSupplier = (Label)e.Row.FindControl("lblSupplier");
                lblSupplier.Text = model.SupplierName;
                Label labExpectPaymentPrice = (Label)e.Row.FindControl("labExpectPaymentPrice");
                if (labExpectPaymentPrice != null && labExpectPaymentPrice.Text != string.Empty)
                    labExpectPaymentPrice.Text = Convert.ToDecimal(labExpectPaymentPrice.Text).ToString("#,##0.00");
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
                //这里的处理是为了回显之前选中的情况
                if (e.Row.RowIndex > -1 && this.SelectedItems != null)
                {
                    CheckBox cb = e.Row.FindControl("chkReturn") as CheckBox;
                    if (this.SelectedItems != null && model != null)
                    {
                        if (this.SelectedItems.Contains(model.ReturnID))
                            cb.Checked = true;
                        else
                            cb.Checked = false;
                    }
                }
            }
        }

        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {

            }
        }
      
    }
}
