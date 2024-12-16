using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.Reports
{
    public partial class ProxyReturnList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Search();
                BindLog();
            }
        }

        private void BindLog()
        {
            IList<ESP.Finance.Entity.AuditLogInfo> logList = ESP.Finance.BusinessLogic.AuditLogManager.GetProxyPNList(0);
            foreach (ESP.Finance.Entity.AuditLogInfo log in logList)
            {
                lblLog.Text += log.Suggestion + " 导出日期[" + log.AuditDate.Value.ToString() + "]<br/>";
            }
            lblLog.Style["display"] = "block";
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }
        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            this.txtEndDate.Text = "";
            this.txtBeginDate.Text = "";
            Search();
        }
        private void Search()
        {
            string term = string.Empty;
            if (!string.IsNullOrEmpty(this.txtBeginDate.Text) && !string.IsNullOrEmpty(this.txtEndDate.Text))
            {
                term = " createdate >='" + this.txtBeginDate.Text + "' and createdate<='" + this.txtEndDate.Text + "'";
            }
            System.Data.DataTable dt = ESP.Finance.BusinessLogic.ReturnManager.GetProxyReturnList(term);
            this.gvG.DataSource = dt;
            this.gvG.DataBind();
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            string term = string.Empty;
            if (!string.IsNullOrEmpty(this.txtBeginDate.Text) && !string.IsNullOrEmpty(this.txtEndDate.Text))
            {
                term = " createdate >='" + this.txtBeginDate.Text + "' and createdate<='" + this.txtEndDate.Text + "'";
            }
            System.Data.DataTable dt = ESP.Finance.BusinessLogic.ReturnManager.GetProxyReturnList(term);
            try
            {
                ESP.Finance.BusinessLogic.ReturnManager.ExportProxyReturnList(Convert.ToInt32(CurrentUser.SysID), dt, Response);
                GC.Collect();

                ESP.Finance.Entity.AuditLogInfo log = new ESP.Finance.Entity.AuditLogInfo();
                log.AuditDate = DateTime.Now;
                log.AuditorEmployeeName = CurrentUser.Name;
                log.AuditorSysID = Convert.ToInt32(CurrentUser.SysID);
                log.AuditorUserCode = CurrentUser.ID;
                log.AuditorUserName = CurrentUser.ITCode;
                log.AuditStatus = 1;
                log.FormID = 0;
                log.FormType = (int)ESP.Finance.Utility.FormType.ProxyPnReport;
                log.Suggestion = CurrentUser.Name + " 导出数据[" + this.txtBeginDate.Text + "-" + this.txtEndDate.Text + "]";
                ESP.Finance.BusinessLogic.AuditLogManager.Add(log);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('" + ex.Message + "');", true);
            }
        }
        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Data.DataRowView dr = (System.Data.DataRowView)e.Row.DataItem;
                Label lblDate = (Label)e.Row.FindControl("lblDate");
                if (dr["prebegindate"] != System.DBNull.Value)
                    lblDate.Text = Convert.ToDateTime(dr["prebegindate"]).ToString("yyyy-MM-dd");

                Label lblFee = (Label)e.Row.FindControl("lblFee");
                lblFee.Text = Convert.ToDecimal(dr["prefee"]).ToString("#,##0.00");
            }
        }
        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            Search();
        }
    }
}
