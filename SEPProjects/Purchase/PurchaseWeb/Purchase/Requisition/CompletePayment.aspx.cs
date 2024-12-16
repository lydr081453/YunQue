using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using ESP.Purchase.Common;
using ESP.Purchase.BusinessLogic;


public partial class Purchase_Requisition_CompletePayment : ESP.Web.UI.PageBase
{
    static bool isCQStockDeparment = false;
    static bool isZBStockDeparment = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlType.Items.Add(new ListItem(State.OperationTypeShow[State.OperationTypePub], State.OperationTypePub.ToString()));
            ddlType.Items.Add(new ListItem(State.OperationTypeShow[State.OperationTypePri], State.OperationTypePri.ToString()));
            ddlType.Items.Insert(0, new ListItem("全部", "-1"));

            bindInfo();
        }
    }

    /// <summary>
    /// 绑定列表信息
    /// </summary>
    public void bindInfo()
    {
        isCQStockDeparment = false;
        isZBStockDeparment = false;
        SetDeparmentValue();

        List<SqlParameter> parms = new List<SqlParameter>();

        string strWhere = string.Format(" and (a.status = {0} or a.status = {1} ) ", State.PaymentStatus_commit, State.PaymentStatus_over);


        if (!string.IsNullOrEmpty(txtGeneralId.Text.Trim()))
        {
            int totalgno = 0;
            bool res = int.TryParse(txtGeneralId.Text, out totalgno);
            if (res)
            {
                strWhere += string.Format(" and b.id = '{0}' ", txtGeneralId.Text.Trim());
            }
        }
        if (ddlType.SelectedValue != "-1")
        {
            strWhere += " and b.operationType=" + ddlType.SelectedValue;
        }

        if (!string.IsNullOrEmpty(txtPrno.Text.Trim()))
        {
            strWhere += string.Format(" and b.prno = '{0}' ", txtPrno.Text.Trim());
        }
        if (txtAudit.Text.Trim() != "")
        {
            strWhere += " and b.first_assessorname like '%'+@first_assessorname+'%'";
            parms.Add(new SqlParameter("@first_assessorname", txtAudit.Text.Trim()));
        }
        if (!isCQStockDeparment  && !isZBStockDeparment)
        {
            strWhere += " and b.requestor=" + CurrentUser.SysID;
        }
        if (txtPN.Text.Trim() != "")
        {
            strWhere += " and a.returncode like '%'+@pnno+'%'";
            parms.Add(new SqlParameter("@pnno", txtPN.Text.Trim()));
        }

        DataSet ds = PaymentPeriodManager.GetGeneralPaymentList(strWhere,parms);
        DataView dv = ds.Tables[0].DefaultView;
        dv.Sort = "requisition_committime desc";
        gvSupplier.DataSource = dv;
        gvSupplier.DataBind();

        if (gvSupplier.PageCount > 1)
        {
            PageBottom.Visible = true;
            PageTop.Visible = true;
        }
        else
        {
            PageBottom.Visible = false;
            PageTop.Visible = false;
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            tabTop.Visible = true;
            tabBottom.Visible = true;
        }
        else
        {
            tabTop.Visible = false;
            tabBottom.Visible = false;
        }
        labAllNum.Text = labAllNumT.Text = ds.Tables[0].Rows.Count.ToString();
        labPageCount.Text = labPageCountT.Text = (gvSupplier.PageIndex + 1).ToString() + "/" + gvSupplier.PageCount.ToString();
        if (gvSupplier.PageCount > 0)
        {
            if (gvSupplier.PageIndex + 1 == gvSupplier.PageCount)
                disButton("last");
            else if (gvSupplier.PageIndex == 0)
                disButton("first");
            else
                disButton("");
        }
    }

    /// <summary>
    /// 检查当前登陆人是否为采购部的人员
    /// </summary>
    private bool checkIsStockDeparmentUser(string dep)
    {
        int[] deparments = CurrentUser.GetDepartmentIDs();
        foreach (int uniqID in deparments)
        {
            if (dep == "CQ")
            {
                if (uniqID == int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["CQStockDeparmentUniqID"].ToString()))
                    return true;
            }
            else
            {
                if (uniqID == int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["StockDeparmentUniqID"].ToString()))
                    return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    private void SetDeparmentValue()
    {
        if (checkIsStockDeparmentUser("CQ"))
            isCQStockDeparment = true;
        if (checkIsStockDeparmentUser("ZB"))
            isZBStockDeparment = true;
    }

    protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSupplier.PageIndex = e.NewPageIndex;
        bindInfo();
    }

    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        bindInfo();
    }

    protected void btnLast_Click(object sender, EventArgs e)
    {
        Paging(gvSupplier.PageCount);
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        Paging(0);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Paging((gvSupplier.PageIndex + 1) > gvSupplier.PageCount ? gvSupplier.PageCount : (gvSupplier.PageIndex + 1));
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Paging((gvSupplier.PageIndex - 1) < 0 ? 0 : (gvSupplier.PageIndex - 1));
    }

    /// <summary>
    /// 翻页
    /// </summary>
    /// <param name="pageIndex">页码</param>
    private void Paging(int pageIndex)
    {
        GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
        gvG_PageIndexChanging(new object(), e);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindInfo();
    }
    private void disButton(string page)
    {
        switch (page)
        {
            case "first":
                btnFirst.Enabled = false;
                btnPrevious.Enabled = false;
                btnNext.Enabled = true;
                btnLast.Enabled = true;

                btnFirst2.Enabled = false;
                btnPrevious2.Enabled = false;
                btnNext2.Enabled = true;
                btnLast2.Enabled = true;
                break;
            case "last":
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                btnNext.Enabled = false;
                btnLast.Enabled = false;

                btnFirst2.Enabled = true;
                btnPrevious2.Enabled = true;
                btnNext2.Enabled = false;
                btnLast2.Enabled = false;
                break;
            default:
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                btnNext.Enabled = true;
                btnLast.Enabled = true;

                btnFirst2.Enabled = true;
                btnPrevious2.Enabled = true;
                btnNext2.Enabled = true;
                btnLast2.Enabled = true;
                break;
        }
    }

    protected void gvSupplier_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Literal litPNNO = (Literal)e.Row.FindControl("litPNNO");
            Literal litStatus = (Literal)e.Row.FindControl("litStatus");

            DataRowView dv = (DataRowView)e.Row.DataItem;
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(int.Parse(dv["returnID"] == DBNull.Value ? "0" : dv["returnID"].ToString()));
            if (returnModel != null)
            {
                litPNNO.Text = returnModel.ReturnCode;
                litStatus.Text = ESP.Finance.Utility.ReturnPaymentType.ReturnStatusString(returnModel.ReturnStatus.Value, 0, returnModel.IsDiscount);
            }
        }
    }

    protected void gvSupplier_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    public string GetFormatPrice(decimal price)
    {
        return price.ToString("#,##0.00");
    }
}
