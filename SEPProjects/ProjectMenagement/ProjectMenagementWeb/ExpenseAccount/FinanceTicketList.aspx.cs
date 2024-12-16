using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

namespace FinanceWeb.ExpenseAccount
{
    public partial class FinanceTicketList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindList();
            }
        }


        private int[] GetUsers()
        {
            List<int> users = new List<int>();
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegateList = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(CurrentUserID);
            users.AddRange(delegateList.Select(x => x.UserID));
            users.Add(CurrentUserID);
            return users.ToArray();
        }

        private string GetDelegateUser(int[] userIds)
        {
            if (userIds == null || userIds.Length == 0)
                return null;

            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.Append(CurrentUserID);
            for (var i = 0; i < userIds.Length; i++)
            {
                s.Append(",").Append(userIds[i]);
            }

            return s.ToString();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindList();
        }

        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            bindList();
        }

        private void bindList()
        {
            int[] userIds = GetUsers();
            string userIdsString = GetDelegateUser(userIds);

            string str = string.Format(" paymentuserid in({0}) and batchtype=3 and status in(109,110,120,130)", userIdsString);
            if (!string.IsNullOrEmpty(txtKey.Text))
            {
                str += string.Format(" and (batchcode like '%{0}%'  or purchasebatchcode like '%{0}%' or suppliername like '%{0}%')", txtKey.Text);
            }
            IList<ESP.Finance.Entity.PNBatchInfo> batchList = ESP.Finance.BusinessLogic.PNBatchManager.GetList(str, new List<System.Data.SqlClient.SqlParameter>());
            this.gdBatch.DataSource = batchList;
            this.gdBatch.DataBind();
        }

        protected void gdBatch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ESP.Finance.Entity.PNBatchInfo batchModel = (ESP.Finance.Entity.PNBatchInfo)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hylEdit = (HyperLink)e.Row.FindControl("hylEdit");
                Label lblAmounts = (Label)e.Row.FindControl("lblAmounts");
                Label lblReAmounts = (Label)e.Row.FindControl("lblReAmounts");
                Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                HyperLink hylPrint = (HyperLink)e.Row.FindControl("hylPrint");

                if (hylPrint != null)
                {
                    hylPrint.NavigateUrl = "/ExpenseAccount/Print/TicketBatchPrint.aspx?" + ESP.Finance.Utility.RequestName.BatchID + "=" + batchModel.BatchID.ToString();
                    hylPrint.Target = "_blank";
                }

                if (lblAmounts != null)
                    lblAmounts.Text = batchModel.Amounts == null ? "0":batchModel.Amounts.Value.ToString("#,##0.00");
                lblReAmounts.Text = batchModel.TicketReturnPoint.ToString("#,##0.00");
                lblTotal.Text = (batchModel.Amounts.Value - batchModel.TicketReturnPoint).ToString("#,##0.00");
                if (lblStatus != null)
                {
                    lblStatus.Text = ReturnPaymentType.ReturnStatusString(batchModel.Status.Value, 0, null);
                }

                    hylEdit.NavigateUrl = "FinanceTicketAudit.aspx?BatchId=" + batchModel.BatchID.ToString();
               
            }
        }


        protected void gdBatch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdBatch.PageIndex = e.NewPageIndex;
            bindList();
        }


    }
}
