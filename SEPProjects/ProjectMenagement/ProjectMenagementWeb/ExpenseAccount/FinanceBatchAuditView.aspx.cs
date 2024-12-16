/************************************************************************\
 * 审批页面
 *      
 * 不同角色进入后，页面显示权限不同，根据step参数区分
 * 
 * workitemid为工作项ID
 *
\************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using System.Data;
using System.Data.SqlClient;
using ESP.Finance.Entity;

namespace FinanceWeb.ExpenseAccount
{

    public partial class FinanceBatchAuditView : ESP.Web.UI.PageBase
    {
        decimal totalExpense = 0;
        ESP.Finance.SqlDataAccess.WorkFlowDataContext dataContext = new ESP.Finance.SqlDataAccess.WorkFlowDataContext();
        ESP.Finance.Entity.ExpenseAccountExtendsInfo model = null;
        ESP.Finance.SqlDataAccess.WorkItem workitem = null;
        ESP.Finance.Entity.PNBatchInfo batchInfo = null;
        int batchid = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 6000;
            if (!string.IsNullOrEmpty(Request["batchid"]))
            {
                batchid = Convert.ToInt32(Request["batchid"]);
                batchInfo = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchid);
            }
            if (!IsPostBack)
            {
                BindInfo();
            }
        }

        private bool getChongxiao(int bid)
        {
            IList<ESP.Finance.Entity.ReturnInfo> returnlist = ESP.Finance.BusinessLogic.PNBatchManager.GetReturnList(bid);

            if (returnlist != null && returnlist.Count > 0)
            {
                if (returnlist[0].ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff || returnlist[0].ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountWaitReceiving)
                {
                    return true;
                }
            }
            return false;
        }

        protected void BindInfo()
        {

            if (batchid > 0 && batchInfo != null)
            {
                BindList();
                txtBatchCode.Text = batchInfo.BatchCode;
                lblBatchId.Text = batchInfo.BatchID.ToString();
                lblPurchaseBatchCode.Text = batchInfo.PurchaseBatchCode;
            }

        }

        protected void BindList()
        {
            DataSet ds = new DataSet();
            string whereStr = "";

            whereStr += string.Format(" and pb.batchid = {0} ",  batchid);

            ds = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetExpenseOrderView(whereStr);

            gvG.DataSource = ds.Tables[0];
            gvG.DataBind();
            this.lblTotal.Text = totalExpense.ToString("#,##0.00");

            List<ESP.Finance.Entity.ExpenseAuditDetailInfo> loglist = ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.GetList(" and expenseauditid=" + ds.Tables[0].Rows[0]["ReturnID"].ToString() + " and audittype in(11,12,13)");
            foreach (ESP.Finance.Entity.ExpenseAuditDetailInfo info in loglist)
            {
                lblLog.Text += info.AuditorEmployeeName + "(" + info.AuditorUserName + ")" + ";&nbsp;&nbsp;&nbsp;" + info.Suggestion + ";&nbsp;&nbsp;&nbsp;" + ((info.AuditType.Value == 0) ? "提交" : ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Names[info.AuditeStatus.Value]) + ";&nbsp;&nbsp;&nbsp;" + info.AuditeDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>";
            }
        }

        /// <summary>
        /// 更具代理人获取其代理的所有人
        /// </summary>
        /// <returns></returns>

        #region
        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            BindList();
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = (DataRowView)e.Row.DataItem;

                Label lblPreFee = (Label)e.Row.FindControl("lblPreFee");
                lblPreFee.Text = dr["PreFee"].ToString();

                HyperLink hylPrint = (HyperLink)e.Row.FindControl("hylPrint");
                if (Convert.ToInt32(dr["ReturnType"]) != (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty)
                {
                    hylPrint.NavigateUrl = "Print/ExpensePrint.aspx?expenseID=" + dr["ReturnID"];
                }
                else
                {
                    hylPrint.NavigateUrl = "Print/ThirdPartyPrint.aspx?expenseID=" + dr["ReturnID"];
                }
                hylPrint.Target = "_blank";

                totalExpense += Convert.ToDecimal(dr["PreFee"]);
            }
        }


        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        #endregion

        protected void btnExport_Click(object sender, EventArgs e)
        {
           DataTable  dbList = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetDetailListForReport(batchid);
           ESP.Finance.BusinessLogic.ReturnManager.ExportOOPBatch(dbList, this.Response);
        }


    }
}
