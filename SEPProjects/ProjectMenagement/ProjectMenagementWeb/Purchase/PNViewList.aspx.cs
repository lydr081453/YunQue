using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using ESP.Finance.Utility;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Entity;

namespace FinanceWeb.Purchase
{
    public partial class PNViewList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Search();
            }
        }

        private void Search()
        {
            List<SqlParameter> paramlist = new List<SqlParameter>();
            string term = string.Empty;
            term += " and (b.first_assessor=@auditor or b.Filiale_Auditor=@auditor";
            paramlist.Add(new SqlParameter("@auditor", CurrentUser.SysID));
            if (GetLevel1DeparmentID(int.Parse(CurrentUser.SysID)) != ESP.Purchase.Common.State.filialeName_CQ)
                term += " or d.supplier_area='" + ESP.Purchase.Common.State.filialeName_CQ.Substring(0, 2) + "')";
            else if (GetLevel1DeparmentID(int.Parse(CurrentUser.SysID)) != "总部")
                term += " or d.supplier_area=" + "'北京')";
            term += " and a.returnStatus>=" + (int)PaymentStatus.PurchaseFirst;

            if (this.txtKey.Text.Trim() != string.Empty)
            {
                term += "  and (a.PRNO like '%'+@prno+'%' or ProjectCode like '%'+@prno+'%' or returncode like '%'+@prno+'%' or prid like '%'+@prno+'%'  or prefee like '%'+@prno+'%')";
                SqlParameter sp1 = new SqlParameter("@prno", System.Data.SqlDbType.NVarChar, 50);
                sp1.SqlValue = this.txtKey.Text.Trim();
                paramlist.Add(sp1);

            }
            if (this.ddlStatus.SelectedIndex != 0)
            {
                term += " and returnStatus=@returnStatus";
                System.Data.SqlClient.SqlParameter sp2 = new System.Data.SqlClient.SqlParameter("@returnStatus", System.Data.SqlDbType.Int, 4);
                sp2.SqlValue = this.ddlStatus.SelectedValue;
                paramlist.Add(sp2);
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

            this.gvG.DataSource = ReturnManager.GetPNTableForPurchasePN(term, paramlist);
            this.gvG.DataBind();
        }

        private string GetLevel1DeparmentID(int sysId)
        {
            IList<ESP.Compatible.Department> dtdep = ESP.Compatible.Employee.GetDepartments(sysId);
            string nodename = "";
            if (dtdep.Count > 0)
            {
                string level = dtdep[0].Level.ToString();
                if (level == "1")
                {
                    nodename = dtdep[0].NodeName;
                }
                else if (level == "2")
                {
                    ESP.Compatible.Department dep = ESP.Compatible.DepartmentManager.GetDepartmentByPK(dtdep[0].UniqID);
                    nodename = dep.Parent.DepartmentName;

                }
                else if (level == "3")
                {
                    ESP.Compatible.Department dep = ESP.Compatible.DepartmentManager.GetDepartmentByPK(dtdep[0].UniqID);
                    ESP.Compatible.Department dep2 = ESP.Compatible.DepartmentManager.GetDepartmentByPK(dep.Parent.UniqID);
                    nodename = dep2.Parent.DepartmentName;

                }
            }
            return nodename;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            this.txtKey.Text = string.Empty;
            this.ddlStatus.SelectedIndex = 0;
            this.txtBeginDate.Text = string.Empty;
            this.txtEndDate.Text = string.Empty;
            Search();
        }

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            Search();
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //ESP.Finance.Entity.ReturnInfo model = (ReturnInfo)e.Row.DataItem;
                DataRowView dv = (DataRowView)e.Row.DataItem;
                Label labExpectPaymentPrice = (Label)e.Row.FindControl("labExpectPaymentPrice");
                if (labExpectPaymentPrice != null && labExpectPaymentPrice.Text != string.Empty)
                    labExpectPaymentPrice.Text = Convert.ToDecimal(labExpectPaymentPrice.Text).ToString("#,##0.00");
                Label lblInvoice = (Label)e.Row.FindControl("lblInvoice");
                if (lblInvoice != null)
                    if (dv["IsInvoice"] != DBNull.Value)
                    {
                        if (int.Parse(dv["IsInvoice"].ToString()) == 1)
                            lblInvoice.Text = "已开";
                        else if (int.Parse(dv["IsInvoice"].ToString()) == 0)
                            lblInvoice.Text = "未开";
                        else
                            lblInvoice.Text = "无需发票";
                    }

                Label lblPR = (Label)e.Row.FindControl("lblPR");
                if (int.Parse(dv["ReturnType"].ToString()) != (int)ESP.Purchase.Common.PRTYpe.MediaPR && int.Parse(dv["ReturnType"].ToString()) != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
                    lblPR.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + dv["PRID"].ToString() + "'style='cursor: hand' target='_blank'>" + dv["PRNo"].ToString() + "</a>";
                else
                    lblPR.Text = dv["PRNo"].ToString();
                Label lblStatusName = (Label)e.Row.FindControl("lblStatusName");
                HiddenField hidStatusNameID = (HiddenField)e.Row.FindControl("hidStatusNameID");
                if (lblStatusName != null && hidStatusNameID != null && hidStatusNameID.Value != string.Empty)
                    lblStatusName.Text = ReturnPaymentType.ReturnStatusString(Convert.ToInt32(hidStatusNameID.Value),0,null);
                Label lblName = (Label)e.Row.FindControl("lblName");
                lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(int.Parse(dv["RequestorID"].ToString())) + "');");
            }
        }
    }
}
