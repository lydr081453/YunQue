using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.Monitor
{
    public partial class CostMonitor : ESP.Web.UI.PageBase
    {
        private DataTable dt = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hidGroupID.Value = CurrentUser.GetDepartmentIDs()[0].ToString();
                hidUserId.Value = "0";
                Search();
            }
        }


        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            Search();
        }


        private void Search()
        {
            dt = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetCostMonitor(int.Parse(hidUserId.Value), txtSupplier.Text, txtProject.Text, txtBeginDate.Text, txtEndDate.Text);
            this.gvG.DataSource = dt;
            this.gvG.DataBind();
        }
        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
          
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                Label labPrtype = (Label)e.Row.FindControl("labPrtype");
                Label labState = (Label)e.Row.FindControl("labState");
                Label lblPrint = (Label)e.Row.FindControl("lblPrint");
                Label lblPrno = (Label)e.Row.FindControl("lblPrno");
                labPrtype.Text = ESP.Purchase.Common.State.requistionOrorder_state[int.Parse(row["status"].ToString())];
                int prytpe =int.Parse(row["prtype"].ToString());
                switch(prytpe)
                {
                    case 0:
                        labState.Text = "对公申请";
                        break;
                    case 1: labState.Text = "稿费申请";
                        break;
                    case 6:
                        labState.Text = "对私申请";
                        break;
                    case 7:
                        labState.Text = "媒体合作";
                        break;
                    case 98:
                        labState.Text = "广告采买";
                        break;
                    default:
                        labState.Text = "对公申请";
                        break;
                }
                lblPrint.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\Print\\RequisitionPrint.aspx?ID=" + row["Id"].ToString() + "'style='cursor: hand' target='_blank'> <img title='打印预览' src='/images/ProjectPrint.gif' border='0px;' /></a>";
                lblPrno.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + row["Id"].ToString() + "'style='cursor: hand' target='_blank'>" + row["PrNo"].ToString() + "</a>"; ;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }
        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            this.txtEndDate.Text = string.Empty;
            this.txtBeginDate.Text = string.Empty;
            this.txtUser.Text = string.Empty;
            this.hidUserId.Value = "0";
            this.txtProject.Text = string.Empty;
            this.txtSupplier.Text = string.Empty;
            Search();
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            dt = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetCostMonitor(int.Parse(hidUserId.Value), txtSupplier.Text, txtProject.Text, txtBeginDate.Text, txtEndDate.Text);
            ESP.Finance.BusinessLogic.ProjectManager.ExportCostMonitor(dt, Response);
        }

    }
}
