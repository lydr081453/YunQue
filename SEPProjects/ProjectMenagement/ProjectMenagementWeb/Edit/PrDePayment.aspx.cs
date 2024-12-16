using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

namespace FinanceWeb.Edit
{
    public partial class PrDePayment : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindList();
            }
        }

        private void bindList()
        {
           // string delegateusers = string.Empty;
            //IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
            //foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
            //{
            //    delegateusers += model.UserID.ToString() + ",";
            //}
            //delegateusers = delegateusers.TrimEnd(',');
            //string Branchs = string.Empty;
            //IList<ESP.Finance.Entity.BranchInfo> branchList = ESP.Finance.BusinessLogic.BranchManager.GetList(" OtherFinancialUsers like '%," + CurrentUser.SysID + ",%'");
            //if (branchList != null && branchList.Count > 0)
            //{
            //    foreach (ESP.Finance.Entity.BranchInfo b in branchList)
            //    {
            //        Branchs += b.BranchID.ToString() + ",";
            //    }
            //}
            //Branchs = Branchs.TrimEnd(',');

            string term = string.Empty;
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
            //if (string.IsNullOrEmpty(Branchs))
            //{
                //if (!string.IsNullOrEmpty(delegateusers))
                //{
                   // term = " (returnID in(select returnid from F_ReturnAuditHist where AuditorUserID=@currentUserId  or AuditorUserID in(" + delegateusers + ")) or (RequestorID=@currentUserId or RequestorID in(" + delegateusers + "))) ";
                   // term = " (RequestorID=@currentUserId or RequestorID in(" + delegateusers + ")) ";
                //}
                //else
                //{
                  //  term = " (returnID in(select returnid from F_ReturnAuditHist where AuditorUserID=@currentUserId) or (RequestorID=@currentUserId)) ";
                    term = "  (RequestorID=@currentUserId) ";
               // }

                System.Data.SqlClient.SqlParameter puserid = new System.Data.SqlClient.SqlParameter("@currentUserId", System.Data.SqlDbType.Int, 4);
                puserid.SqlValue = int.Parse(CurrentUser.SysID);
                paramlist.Add(puserid);
                term += " and (returnType=@returnType or returnType=@returnType3) and ReturnStatus=@ReturnStatus";
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@returnType", ESP.Purchase.Common.PRTYpe.CommonPR));
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@returnType3", ESP.Purchase.Common.PRTYpe.MPPR));
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@ReturnStatus", ESP.Finance.Utility.PaymentStatus.WaitReceiving));
            //}
            //else
            //{
            //    term = "(projectid in(select projectid from f_project where branchid in(" + Branchs + ")))";
            //}
            if (this.txtKey.Text.Trim() != string.Empty)
            {
                term += "  and (prno like '%'+@prno+'%' or projectcode like '%'+@prno+'%' or returncode like '%'+@prno+'%' or prid like '%'+@prno+'%' or prefee  like '%'+@prno+'%' or RequestEmployeeName  like '%'+@prno+'%' or SupplierName  like '%'+@prno+'%' )";
                System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@prno", System.Data.SqlDbType.NVarChar, 50);
                p1.SqlValue = this.txtKey.Text.Trim();
                paramlist.Add(p1);
            }
            IList<ESP.Finance.Entity.ReturnInfo> returnlist = ESP.Finance.BusinessLogic.ReturnManager.GetList(term, paramlist);
            this.gvG.DataSource = returnlist;
            this.gvG.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindList();
        }
        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            this.txtKey.Text = string.Empty;
            bindList();
        }

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            bindList();
        }
        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.ReturnInfo model = (ESP.Finance.Entity.ReturnInfo)e.Row.DataItem;
                IList<ESP.Finance.Entity.PNBatchRelationInfo> relationList = ESP.Finance.BusinessLogic.PNBatchRelationManager.GetList(" ReturnID=" + model.ReturnID.ToString(), null);

                //冲销，连接个人报销冲销界面
                HyperLink hylDePayment = (HyperLink)e.Row.FindControl("hylDePayment");
                if (model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.WaitReceiving && (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MPPR || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.CommonPR || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PrivatePR))
                {
                    hylDePayment.Visible = true;
                    hylDePayment.NavigateUrl = "/Purchase/ReturnDePayment.aspx?" + RequestName.ReturnID + "=" + model.ReturnID.ToString();
                }
                else
                { hylDePayment.Visible = false; }

                Label labExpectPaymentPrice = (Label)e.Row.FindControl("labExpectPaymentPrice");
                if (labExpectPaymentPrice != null && labExpectPaymentPrice.Text != string.Empty)
                    labExpectPaymentPrice.Text = Convert.ToDecimal(labExpectPaymentPrice.Text).ToString("#,##0.00");
                Label lblPR = (Label)e.Row.FindControl("lblPR");
                if (model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.MediaPR && model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
                    lblPR.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + model.PRID.ToString() + "'style='cursor: hand' target='_blank'>" + model.PRNo + "</a>";
                else
                    lblPR.Text = model.PRNo;
                Label lblBeginDate = (Label)e.Row.FindControl("lblBeginDate");
                if (lblBeginDate != null && lblBeginDate.Text != string.Empty)
                    lblBeginDate.Text = Convert.ToDateTime(lblBeginDate.Text).ToString("yyyy-MM-dd");
                Label lblEndDate = (Label)e.Row.FindControl("lblEndDate");
                if (lblEndDate != null && lblEndDate.Text != string.Empty)
                    lblEndDate.Text = Convert.ToDateTime(lblEndDate.Text).ToString("yyyy-MM-dd");

                Label lblStatusName = (Label)e.Row.FindControl("lblStatusName");
                HiddenField hidStatusNameID = (HiddenField)e.Row.FindControl("hidStatusNameID");
                if (lblStatusName != null && hidStatusNameID != null && hidStatusNameID.Value != string.Empty)
                    lblStatusName.Text = ReturnPaymentType.ReturnStatusString(Convert.ToInt32(hidStatusNameID.Value),0,model.IsDiscount);
                LinkButton lnkAttach = (LinkButton)e.Row.FindControl("lnkAttach");
                Label lblSupplier = (Label)e.Row.FindControl("lblSupplier");
                lblSupplier.Text = model.SupplierName;
                Label lblName = (Label)e.Row.FindControl("lblName");
                lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(model.RequestorID.Value) + "');");

                HyperLink hylPrint = (HyperLink)e.Row.FindControl("hylPrint");
                if (hylPrint != null)
                {
                    hylPrint.Target = "_blank";
                    hylPrint.NavigateUrl = "/Purchase/Print/PaymantPrint.aspx?" + RequestName.ReturnID + "=" + e.Row.Cells[0].Text;
                }
                if (relationList != null && relationList.Count > 0)
                {
                    hylPrint.Visible = false;
                }
            }
        }

    }
}
