using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ESP.Finance.Utility;

public partial class ForeGift_killForeGiftList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            listBind();
            logBind();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        listBind();
        logBind();
    }

    /// <summary>
    /// 绑定待抵押金列表
    /// </summary>
    private void listBind()
    {
        string terms = " returnType=@returnType and returnStatus=@returnStatus";
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@returnType", (int)ESP.Purchase.Common.PRTYpe.PN_ForeGift));
        parms.Add(new SqlParameter("@returnStatus", (int)ESP.Finance.Utility.PaymentStatus.WaitReceiving));
        if (txtKey.Text.Trim() != "")
        {
            terms += " and (PRNO like '%'+@prno+'%' or ProjectCode like '%'+@prno+'%' or returncode like '%'+@prno+'%' or prid like '%'+@prno+'%'  or prefee  like '%'+@prno+'%' or RequestEmployeeName  like '%'+@prno+'%' or SupplierName  like '%'+@prno+'%' )";
            SqlParameter sp1 = new SqlParameter("@prno", System.Data.SqlDbType.NVarChar, 50);
            sp1.SqlValue = this.txtKey.Text.Trim();
            parms.Add(sp1);
        }
        IList<ESP.Finance.Entity.ReturnInfo> returnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(terms, parms);
        gvList.DataSource = returnList;
        gvList.DataBind();
    }

    /// <summary>
    /// 绑定已完成抵消押金列表
    /// </summary>
    private void logBind()
    {
        string terms = " returnType=@returnType and returnStatus=@returnStatus";
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@returnType", (int)ESP.Purchase.Common.PRTYpe.PN_ForeGift));
        parms.Add(new SqlParameter("@returnStatus", (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete));
        if (txtKey.Text.Trim() != "")
        {
            terms += " and (PRNO like '%'+@prno+'%' or ProjectCode like '%'+@prno+'%' or returncode like '%'+@prno+'%' or prid like '%'+@prno+'%'  or prefee  like '%'+@prno+'%' or RequestEmployeeName  like '%'+@prno+'%' or SupplierName  like '%'+@prno+'%' )";
            SqlParameter sp1 = new SqlParameter("@prno", System.Data.SqlDbType.NVarChar, 50);
            sp1.SqlValue = this.txtKey.Text.Trim();
            parms.Add(sp1);
        }
        IList<ESP.Finance.Entity.ReturnInfo> returnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(terms, parms);
        gvLog.DataSource = returnList;
        gvLog.DataBind();
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

            Label labExpectPaymentPrice = (Label)e.Row.FindControl("labExpectPaymentPrice");
            if (labExpectPaymentPrice != null && labExpectPaymentPrice.Text != string.Empty)
                labExpectPaymentPrice.Text = Convert.ToDecimal(labExpectPaymentPrice.Text).ToString("#,##0.00");
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


            Label lblName = (Label)e.Row.FindControl("lblName");
            lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(model.RequestorID.Value) + "');");
        }
    }

    protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvList.PageIndex = e.NewPageIndex;
        listBind();
    }

    protected void gvLog_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ESP.Finance.Entity.ReturnInfo model = (ESP.Finance.Entity.ReturnInfo)e.Row.DataItem;

            Label labExpectPaymentPrice = (Label)e.Row.FindControl("labExpectPaymentPrice");
            if (labExpectPaymentPrice != null && labExpectPaymentPrice.Text != string.Empty)
                labExpectPaymentPrice.Text = Convert.ToDecimal(labExpectPaymentPrice.Text).ToString("#,##0.00");
            Label lblBeginDate = (Label)e.Row.FindControl("lblBeginDate");
            if (lblBeginDate != null && lblBeginDate.Text != string.Empty)
                lblBeginDate.Text = Convert.ToDateTime(lblBeginDate.Text).ToString("yyyy-MM-dd");
            Label lblEndDate = (Label)e.Row.FindControl("lblEndDate");
            if (lblEndDate != null && lblEndDate.Text != string.Empty)
                lblEndDate.Text = Convert.ToDateTime(lblEndDate.Text).ToString("yyyy-MM-dd");

            Label lblStatusName = (Label)e.Row.FindControl("lblStatusName");
            HiddenField hidStatusNameID = (HiddenField)e.Row.FindControl("hidStatusNameID");
            if (lblStatusName != null && hidStatusNameID != null && hidStatusNameID.Value != string.Empty)
                lblStatusName.Text = ReturnPaymentType.ReturnStatusString(Convert.ToInt32(hidStatusNameID.Value), 0, model.IsDiscount);


            Label lblName = (Label)e.Row.FindControl("lblName");
            lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(model.RequestorID.Value) + "');");
        }
    }

    protected void gvLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLog.PageIndex = e.NewPageIndex;
        logBind();
    }
}
