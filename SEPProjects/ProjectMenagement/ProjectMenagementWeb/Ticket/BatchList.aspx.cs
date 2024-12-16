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


namespace FinanceWeb.Ticket
{
    public partial class BatchList : ESP.Web.UI.PageBase
    {
        private IList<ESP.Finance.Entity.ReturnInfo> returnlist = null;
        private IList<ESP.Finance.Entity.ExpenseAccountDetailInfo> detaillist = null;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                IList<ESP.Finance.Entity.BranchInfo> blist = ESP.Finance.BusinessLogic.BranchManager.GetList(null, null);
                ddlBranch.DataSource = blist;
                ddlBranch.DataTextField = "BranchCode";
                ddlBranch.DataValueField = "BranchID";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, new ListItem("请选择", "0"));
                bindBatch();

            }
        }

        private void bindBatch()
        {
            IList<ESP.Finance.Entity.PNBatchInfo> list = null;
            string term = string.Empty;
            List<SqlParameter> paramlist = new List<SqlParameter>();

            term = " BatchType=3 and (CreatorId=@CreatorId or batchid in(select formid from f_auditlog where formtype =6 and auditorsysid =@CreatorId ))";
            SqlParameter p = new SqlParameter("@CreatorId", System.Data.SqlDbType.Int, 4);
            p.SqlValue = CurrentUser.SysID;
            paramlist.Add(p);

            if (!string.IsNullOrEmpty(term))
            {
                if (this.txtKey.Text.Trim() != string.Empty)
                {
                    term += "  and ( amounts like '%'+@prno+'%' or batchcode like '%'+@prno+'%' or  suppliername like '%'+@prno+'%')";
                    SqlParameter sp1 = new SqlParameter("@prno", System.Data.SqlDbType.NVarChar, 50);
                    sp1.SqlValue = this.txtKey.Text.Trim();
                    paramlist.Add(sp1);

                }
                if (this.ddlBranch.SelectedIndex != 0)
                {
                    term += " and Branchcode = @BranchCode";
                    System.Data.SqlClient.SqlParameter pBrach = new System.Data.SqlClient.SqlParameter("@BranchCode", System.Data.SqlDbType.NVarChar, 50);
                    pBrach.SqlValue = this.ddlBranch.SelectedItem.Text;
                    paramlist.Add(pBrach);
                }
                if (!string.IsNullOrEmpty(this.txtSupplier.Text))
                {
                    term += " and SupplierName like '%'+@SupplierName+'%' ";
                    System.Data.SqlClient.SqlParameter psup = new System.Data.SqlClient.SqlParameter("@SupplierName", System.Data.SqlDbType.NVarChar, 50);
                    psup.SqlValue = this.txtSupplier.Text;
                    paramlist.Add(psup);
                }

            }
            list = ESP.Finance.BusinessLogic.PNBatchManager.GetList(term, paramlist);
            var tmpList = list.OrderByDescending(x => x.CreateDate).ToList();
            this.gdBatch.DataSource = tmpList;
            this.gdBatch.DataBind();
        }


        protected void gdBatch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ESP.Finance.Entity.PNBatchInfo batchModel = (ESP.Finance.Entity.PNBatchInfo)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hylEdit = (HyperLink)e.Row.FindControl("hylEdit");
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
                HyperLink hylPrint = (HyperLink)e.Row.FindControl("hylPrint");
                Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                Label lblReturn = (Label)e.Row.FindControl("lblReturn");
                Label lblAmounts = (Label)e.Row.FindControl("lblAmounts");
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");

                lblTotal.Text = batchModel.Amounts.Value.ToString("#,##0.00");
                lblReturn.Text = batchModel.TicketReturnPoint.ToString("#,##0.00");
                lblAmounts.Text = (batchModel.Amounts.Value - batchModel.TicketReturnPoint).ToString("#,##0.00");

                if (lblStatus != null)
                {
                    lblStatus.Text = ReturnPaymentType.ReturnStatusString(batchModel.Status.Value, 0, null);
                }
                if (hylPrint != null)
                {
                    hylPrint.NavigateUrl = "/ExpenseAccount/Print/TicketBatchPrint.aspx?" + ESP.Finance.Utility.RequestName.BatchID + "=" + batchModel.BatchID.ToString();
                    hylPrint.Target = "_blank";
                }
                if (batchModel.Status == (int)PaymentStatus.Save)
                {
                    hylEdit.NavigateUrl = "/ExpenseAccount/BatchTicketEdit.aspx?BatchId=" + batchModel.BatchID.ToString();
                }
                else
                {
                    hylEdit.Visible = false;
                    lnkDelete.Visible = false;
                }

            }
        }

        protected void gdBatch_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }


        protected void gdBatch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdBatch.PageIndex = e.NewPageIndex;
            bindBatch();
        }

        protected void gdBatch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int batchid = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "Delete")
            {
                ESP.Finance.BusinessLogic.PNBatchManager.Delete(batchid);
                bindBatch();
            }
            else if (e.CommandName == "Export")
            {

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindBatch();
        }

        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            this.txtKey.Text = string.Empty;
            this.ddlBranch.SelectedIndex = 0;
            bindBatch();
        }

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            int batchId = int.Parse(((LinkButton)sender).CommandArgument.ToString());
            ESP.Finance.BusinessLogic.ReturnManager.ExportDeptSalayForFinance(batchId,  this.Response);
        }

        protected void lnkExportHR_Click(object sender, EventArgs e)
        {
            int batchId = int.Parse(((LinkButton)sender).CommandArgument.ToString());
            ESP.Finance.BusinessLogic.ReturnManager.ExportDeptSalayForHR(batchId, this.Response);
        }

    }
}
