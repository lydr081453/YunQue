using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ESP.Finance.Utility;
using ESP.Finance.Entity;
namespace FinanceWeb.Purchase
{
    public partial class FinanceReporterList : ESP.Web.UI.PageBase
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
                term = " ReturnStatus=@status1 and ReturnType=@ReturnType";
            SqlParameter p1 = new SqlParameter("@status1", System.Data.SqlDbType.Int, 4);
            p1.SqlValue = (int)PaymentStatus.FinanceComplete;
            paramlist.Add(p1);
            SqlParameter p2 = new SqlParameter("@ReturnType", System.Data.SqlDbType.Int, 4);
            p2.SqlValue = (int)ESP.Purchase.Common.PRTYpe.MediaPR;
            paramlist.Add(p2);
           
            if (!string.IsNullOrEmpty(term))
            {
                if (this.txtKey.Text.Trim() != string.Empty)
                {
                    term += "  and (PRNO like '%'+@prno+'%' or ProjectCode like '%'+@prno+'%' or returncode like '%'+@prno+'%' or prid like '%'+@prno+'%'  or prefee like '%'+@prno+'%')";
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

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            Search();
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.ReturnInfo model = (ReturnInfo)e.Row.DataItem;
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
                    lblStatusName.Text = ReturnPaymentType.ReturnStatusString(Convert.ToInt32(hidStatusNameID.Value),0,model.IsDiscount);
                LinkButton lnkAttach = (LinkButton)e.Row.FindControl("lnkAttach");
                if (!string.IsNullOrEmpty(model.MediaOrderIDs) && (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA))
                {
                    lnkAttach.Text = "<img src='/images/PrintDefault.gif' title='导出Excel' border='0'>";
                }
                else
                {
                    lnkAttach.Text = "";
                }

                Label lblAttach = (Label)e.Row.FindControl("lblAttach");
                if (!string.IsNullOrEmpty(model.MediaOrderIDs) && (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA))
                {
                    lblAttach.Text = "<a href='" + ESP.Finance.Configuration.ConfigurationManager.PurchaseServer + "Purchase\\Requisition\\Print\\MediaPrint.aspx?OrderID=" + model.MediaOrderIDs + "'style='cursor: hand' target='_blank'> <img title='附件' src='/images/PrintDefault.gif' border='0px;' /></a>";
                }
                else
                {
                    lblAttach.Text = "";
                }
                //3000以下对私的单子有附件显示
                if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
                {
                    lblAttach.Visible = true;
                    lblAttach.Text = "<a href='" + ESP.Finance.Configuration.ConfigurationManager.PurchaseServer + "Purchase\\Requisition\\Print\\RequisitionPrint.aspx?id=" + model.PRID.ToString() + "&viewButton=no&Action=ViewOldPr' style='cursor: hand' target='_blank'> <img title='附件' src='/images/PrintDefault.gif' border='0px;' /></a>";
                }
                HyperLink hylAudit = (HyperLink)e.Row.FindControl("hylAudit");
                hylAudit.NavigateUrl = "FinanceReporterEdit.aspx?" + ESP.Finance.Utility.RequestName.ReturnID +"="+ model.ReturnID.ToString() ;
               
                HyperLink hylPrint = (HyperLink)e.Row.FindControl("hylPrint");
                hylPrint.Target = "_blank";
                hylPrint.NavigateUrl = "Print/PaymantPrint.aspx?" + RequestName.ReturnID + "=" + model.ReturnID.ToString();

                if (model.NeedPurchaseAudit == true)
                {
                    hylPrint.Visible = false;
                }

                Label lblName = (Label)e.Row.FindControl("lblName");
                lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(model.RequestorID.Value) + "');");
                
            }
        }

        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            this.txtKey.Text = string.Empty;
            this.txtBeginDate.Text = string.Empty;
            this.txtEndDate.Text = string.Empty;
            Search();
        }

        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
    
    }
}
