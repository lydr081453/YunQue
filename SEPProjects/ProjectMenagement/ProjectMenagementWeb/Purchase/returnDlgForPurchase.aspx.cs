using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using ESP.Finance.Utility;
using ESP.Finance.Entity;

public partial class Purchase_returnDlgForPurchase : ESP.Web.UI.PageBase
{
    int batchId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[RequestName.BatchID]))
            batchId = int.Parse(Request[RequestName.BatchID]);
        if (!IsPostBack)
        {
            Search();
        }
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
            term = " and (ReturnStatus=@status1 or  ReturnStatus=@status2 ) AND (a.PaymentUserID=@sysID or a.PaymentUserID in(" + DelegateUsers + "))";
        else
            term = " and (ReturnStatus=@status1 or  ReturnStatus=@status2 ) AND a.PaymentUserID=@sysID";

        SqlParameter p1 = new SqlParameter("@status1", System.Data.SqlDbType.Int, 4);
        p1.SqlValue = (int)PaymentStatus.PurchaseFirst;
        paramlist.Add(p1);
        SqlParameter p2 = new SqlParameter("@status2", System.Data.SqlDbType.Int, 4);
        p2.SqlValue = (int)PaymentStatus.PurchaseMajor1;
        paramlist.Add(p2);
        SqlParameter p6 = new SqlParameter("@sysID", System.Data.SqlDbType.Int, 4);
        p6.SqlValue = CurrentUser.SysID;
        paramlist.Add(p6);
        if (!string.IsNullOrEmpty(term))
        {
            if (this.txtKey.Text.Trim() != string.Empty)
            {
                term += "  and (a.PRNO like '%'+@prno+'%' or ProjectCode like '%'+@prno+'%' or returncode like '%'+@prno+'%' or prid like '%'+@prno+'%'  or prefee like '%'+@prno+'%' or a.SupplierName  like '%'+@prno+'%' )";
                SqlParameter sp1 = new SqlParameter("@prno", System.Data.SqlDbType.NVarChar, 50);
                sp1.SqlValue = this.txtKey.Text.Trim();
                paramlist.Add(sp1);

            }
            if (txtSupplierName.Text.Trim() != string.Empty)
            {
                term += " and a.SupplierName  like '%'+@supplierName+'%'";
                SqlParameter supp = new SqlParameter("@supplierName", SqlDbType.NVarChar, 50);
                supp.SqlValue = txtSupplierName.Text.Trim();
                paramlist.Add(supp);
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
            PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchId);
            if (batchModel.Status == (int)PaymentStatus.PurchaseFirst)
            {
                term += " and b.first_assessor=" + CurrentUser.SysID + " and returnstatus=" + (int)PaymentStatus.PurchaseFirst;
            }
            else if(batchModel.Status == (int)PaymentStatus.PurchaseMajor1)
            {
                term += " and returnstatus=" + (int)PaymentStatus.PurchaseMajor1;
            }

            term += " and returnid not in(select returnid from f_pnbatchrelation)";
            term += " and projectcode like ''+@BranchCode+'%' ";
            System.Data.SqlClient.SqlParameter pBrach = new System.Data.SqlClient.SqlParameter("@BranchCode", System.Data.SqlDbType.NVarChar, 50);
            pBrach.SqlValue = batchModel.BranchCode;
            paramlist.Add(pBrach);
            term += " and PaymentTypeID=@PaymentTypeID";
            System.Data.SqlClient.SqlParameter pPaymentTypeID = new System.Data.SqlClient.SqlParameter("@PaymentTypeID", System.Data.SqlDbType.Int, 4);
            pPaymentTypeID.SqlValue = batchModel.PaymentTypeID.Value;
            paramlist.Add(pPaymentTypeID);
            DataTable returnList = ESP.Finance.BusinessLogic.ReturnManager.GetPNTableLinkPR(term, paramlist);
            DataView viewList = returnList.DefaultView;
            viewList.Sort = "PreBeginDate DESC";
            this.gvG.DataSource = viewList;
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
        try
        {
            if (!string.IsNullOrEmpty(Request["chkItem"]))
            {
                string[] itemIds = Request["chkItem"].Split(',');
                ESP.Finance.BusinessLogic.PNBatchManager.AddItems(batchId, itemIds);
                string script = @"opener.location='BatchPurchase.aspx?" + RequestName.BatchID + "=" + batchId + "';";
                ScriptManager.RegisterStartupScript(this, Page.GetType(), Guid.NewGuid().ToString(), "alert('添加成功!');" + script + "window.close();", true);
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + ex.Message + "');", true);
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
            DataRowView dv = (DataRowView)e.Row.DataItem;
            Label labExpectPaymentPrice = (Label)e.Row.FindControl("labExpectPaymentPrice");
            if (labExpectPaymentPrice != null && labExpectPaymentPrice.Text != string.Empty)
                labExpectPaymentPrice.Text = Convert.ToDecimal(labExpectPaymentPrice.Text).ToString("#,##0.00");
            Label lblPR = (Label)e.Row.FindControl("lblPR");
            if (int.Parse(dv["ReturnType"].ToString()) != (int)ESP.Purchase.Common.PRTYpe.MediaPR && int.Parse(dv["ReturnType"].ToString()) != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
                lblPR.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + dv["PRid"].ToString() + "'style='cursor: hand' target='_blank'>" + dv["PRNo"].ToString() + "</a>";
            else
                lblPR.Text = dv["PRNo"].ToString();
            Label lblStatusName = (Label)e.Row.FindControl("lblStatusName");
            HiddenField hidStatusNameID = (HiddenField)e.Row.FindControl("hidStatusNameID");
            if (lblStatusName != null && hidStatusNameID != null && hidStatusNameID.Value != string.Empty)
                lblStatusName.Text = ReturnPaymentType.ReturnStatusString(Convert.ToInt32(hidStatusNameID.Value),0,null);
        }
    }

}
