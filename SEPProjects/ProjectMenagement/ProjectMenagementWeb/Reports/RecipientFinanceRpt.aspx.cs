using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ESP.Finance.BusinessLogic;
using System.Collections.Generic;
using ESP.Finance.Utility;

namespace FinanceWeb.Reports
{
    public partial class RecipientFinanceRpt : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ddlBranch.Attributes.Add("onChange", "selectBranch(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");
           
            BindBranch();
            binddata();
        }

        private DataTable  binddata()
        {
            string where = string.Empty;
            if ((!string.IsNullOrEmpty(this.txtBeginDate.Text)) && (!string.IsNullOrEmpty(this.txtEndDate.Text)))
            {
                where = " and (commitdate between '" + this.txtBeginDate.Text.Trim() + "' and '" + txtEndDate.Text.Trim() + " 23:59:59')";
            }
            if (!string.IsNullOrEmpty(this.hidBranchCode.Value))
            {
                where += " and projectcode like '" + this.hidBranchCode.Value + "%'";
            }
            if (!string.IsNullOrEmpty(txtKey.Text))
            {
                where += " and (projectcode like '%" + txtKey.Text + "%' or suppliername like '%" + txtKey.Text + "%')";
            }

            return ReturnManager.GetRecipientReport(where);
          
        }

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            gvG.DataSource = binddata();
            gvG.DataBind();
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Data.DataRowView dr = (System.Data.DataRowView)e.Row.DataItem;
                string flg = e.Row.Cells[13].Text;
                Label lblStatus = ((Label)e.Row.FindControl("lblStatus"));
                int status = Convert.ToInt32(e.Row.Cells[14].Text);
                if (flg == "GR")
                {
                    lblStatus.Text = ESP.Purchase.Common.State.recipientConfirm_Names[status];
                }
                else
                {
                    lblStatus.Text = ReturnPaymentType.ReturnStatusString(status, 0, false);
                }
               
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = binddata();
                ReturnManager.ExportRecipientReport(dt,this.Response);
                GC.Collect();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvG.DataSource = binddata();
            gvG.DataBind();
        }

        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            this.txtBeginDate.Text = "";
            this.txtEndDate.Text = "";
            this.ddlBranch.SelectedIndex = 0;
            binddata();
        }

        private void BindBranch()
        {
            IList<ESP.Finance.Entity.BranchInfo> blist = ESP.Finance.BusinessLogic.BranchManager.GetList(" otherfinancialusers like '%,"+CurrentUser.SysID+",%'", null);
            this.ddlBranch.DataSource = blist;
            this.ddlBranch.DataTextField = "BranchCode";
            this.ddlBranch.DataValueField = "BranchID";
            this.ddlBranch.DataBind();

            ListItem list = new ListItem();
            list.Value = "-1";
            list.Text = "请选择...";
            ddlBranch.Items.Insert(0, list);
        }

        protected void btnExportPaid_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBeginDate.Text) || string.IsNullOrEmpty(txtEndDate.Text))
                {
                    Page.ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('请输入区间日期');", true);
                    return;
                }

                IList<ESP.Finance.Entity.ReturnInfo > returnlist = ReturnManager.GetPaidPNReport(CurrentUserID, DateTime.Parse(this.txtBeginDate.Text), DateTime.Parse(this.txtEndDate.Text));
                ReturnManager.ExportPaidPN(returnlist, this.Response);
                GC.Collect();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('" + ex.Message + "');", true);
            }
          
        }

    }
}
