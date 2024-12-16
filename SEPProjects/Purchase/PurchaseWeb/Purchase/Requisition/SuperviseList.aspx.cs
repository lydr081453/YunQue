using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_SuperviseList : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string[] state = State.requistionOrorderValue1_state;
            string[] statevalue = State.requistionOrorderValue_state;
            for (int i = 0; i < state.Length; i++)
            {
                ddlState.Items.Add(new ListItem(state[i].ToString(), statevalue[i].ToString()));
            }
            ddlState.Items.Insert(0, new ListItem("请选择", "-1"));

            ddlRequisitionflow.Items.Add(new ListItem("全部", "-1"));
            ddlRequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toO], State.requisitionflow_toO.ToString()));
            ddlRequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toR], State.requisitionflow_toR.ToString()));
            ddlRequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toC], State.requisitionflow_toC.ToString()));

            //对公/对私
            ddlOperationType.Items.Add(new ListItem("全部", "-1"));
            ddlOperationType.Items.Add(new ListItem(State.OperationTypeShow[State.OperationTypePub], State.OperationTypePub.ToString()));
            ddlOperationType.Items.Add(new ListItem(State.OperationTypeShow[State.OperationTypePri], State.OperationTypePri.ToString()));

            //付款状态
            ddlPaymentStatus.Items.Add(new ListItem("全部", "-1"));
            ddlPaymentStatus.Items.Add(new ListItem(State.PaymentStatus_Names[(int)State.PaymentStatus.Not], ((int)State.PaymentStatus.Not).ToString()));
            ddlPaymentStatus.Items.Add(new ListItem(State.PaymentStatus_Names[(int)State.PaymentStatus.Create], ((int)State.PaymentStatus.Create).ToString()));
            ddlPaymentStatus.Items.Add(new ListItem(State.PaymentStatus_Names[(int)State.PaymentStatus.Complete], ((int)State.PaymentStatus.Complete).ToString()));

            ListBind();
        }
    }

    private void ListBind()
    {
        List<SqlParameter> parms = new List<SqlParameter>();

        string term ="";
        term += " and status>@status";
        parms.Add(new SqlParameter("@status", State.requisition_save));
        if (ddlState.SelectedValue != "-1")
        {
            term += " and status=@status2";
            parms.Add(new SqlParameter("@status2", ddlState.SelectedValue));
        }
        if (ddlOperationType.SelectedValue != "-1")
        {
            if (int.Parse(ddlOperationType.SelectedValue) == (int)State.OperationTypePub)
                term += " and (operationType=@operationType or operationType is null)";
            else
                term += " and operationType=@operationType";
            parms.Add(new SqlParameter("@operationType", ddlOperationType.SelectedValue));
        }
        if (ddlPaymentStatus.SelectedValue != "-1") 
        {
            if (int.Parse(ddlPaymentStatus.SelectedValue) == (int)State.PaymentStatus.Not)
            {
                term += string.Format(" and a.id not in (select gid from t_paymentperiod where status<>{0} and status<>{1} and status<>{2})", State.PaymentStatus_wait, State.PaymentStatus_commit, State.PaymentStatus_over);
            }
            else if (int.Parse(ddlPaymentStatus.SelectedValue) == (int)State.PaymentStatus.Create)
            {
                term += string.Format(" and a.id in (select gid from t_paymentperiod where status={0})", State.PaymentStatus_commit);
            }
            else if (int.Parse(ddlPaymentStatus.SelectedValue) == (int)State.PaymentStatus.Complete)
            {
                term += string.Format(" and a.id in (select gid from t_paymentperiod where status={0})", State.PaymentStatus_over);
            }
        }

        if (ddlRequisitionflow.SelectedValue != "-1")
        {
            term += " and requisitionflow = @requisitionflow ";
            parms.Add(new SqlParameter("@requisitionflow", ddlRequisitionflow.SelectedValue));
        }

        if (txtPrNo.Text.Trim() != "")
        {
            term += " and prNo like '%'+@prNo+'%'";
            parms.Add(new SqlParameter("@prNo", txtPrNo.Text.Trim()));
        }
        if (txtPC.Text.Trim() != "")
        {
            term += " and project_code like '%'+@project_code+'%'";
            parms.Add(new SqlParameter("@project_code", txtPC.Text.Trim()));
        }
        if (txtGlideNo.Text.Trim() != "")
        {
            int totalgno = 0;
            bool res = int.TryParse(txtGlideNo.Text, out totalgno);
            if (res)
            {
                term += " and a.id = @id";
                parms.Add(new SqlParameter("@id", txtGlideNo.Text.TrimStart('0')));
            }
        }
        if (txtsupplierName.Text.Trim() != "")
        {
            term += " and supplier_name like '%'+@suppliername+'%'";
            parms.Add(new SqlParameter("@suppliername", txtsupplierName.Text.Trim()));
        }
        if (txtAudit.Text.Trim() != "")
        {
            term += " and first_assessorname like '%'+@first_assessorname+'%'";
            parms.Add(new SqlParameter("@first_assessorname", txtAudit.Text.Trim()));
        }
        if (txtRequestor.Text.Trim() != "")
        {
            term += " and requestorname like '%'+@requestorname+'%'";
            parms.Add(new SqlParameter("@requestorname", txtRequestor.Text.Trim()));
        }
        if (txtTotalMin.Text.Trim() != "")
        {
            int totalmin = 0;
            bool res = int.TryParse(txtTotalMin.Text, out totalmin);
            if (res)
            {
                term += " and b.totalprice >=@totalmin";
                parms.Add(new SqlParameter("@totalmin", txtTotalMin.Text.Trim()));
            }

        }

        if (txtTotalMax.Text.Trim() != "")
        {
            int totalmax = 0;
            bool res = int.TryParse(txtTotalMax.Text, out totalmax);
            if (res)
            {
                term += " and b.totalprice <= @totalmax";
                parms.Add(new SqlParameter("@totalmax", txtTotalMax.Text.Trim()));
            }
        }
        if (txtBegin.Text.Trim() != "")
        {
            term += " and order_audittime >=CONVERT(datetime , @begin, 120 )";
            parms.Add(new SqlParameter("@begin", txtBegin.Text.Trim()));
        }
        if (txtEnd.Text.Trim() != "")
        {
            term += " and order_audittime <= dateadd(d,1,CONVERT(datetime , @end, 120 ))";
            parms.Add(new SqlParameter("@end", txtEnd.Text.Trim()));
        }
        if (txtFiliale_AuditName.Text.Trim() != "")
        {
            term += " and Filiale_AuditName like '%'+@Filiale_AuditName+'%'";
            parms.Add(new SqlParameter("@Filiale_AuditName", txtFiliale_AuditName.Text.Trim()));
        }

        List<GeneralInfo> list = GeneralInfoManager.GetStatusList(term, parms);
        gvG.DataSource = list;
        gvG.DataBind();

        if (gvG.PageCount > 1)
        {
            PageBottom.Visible = true;
            PageTop.Visible = true;
        }
        else
        {
            PageBottom.Visible = false;
            PageTop.Visible = false;
        }
        if (list.Count > 0)
        {
            tabTop.Visible = true;
            tabBottom.Visible = true;
        }
        else
        {
            tabTop.Visible = false;
            tabBottom.Visible = false;
        }

        labAllNum.Text = labAllNumT.Text = list.Count.ToString();
        labPageCount.Text = labPageCountT.Text = (gvG.PageIndex + 1).ToString() + "/" + gvG.PageCount.ToString();
        if (gvG.PageCount > 0)
        {
            if (gvG.PageIndex + 1 == gvG.PageCount)
                disButton("last");
            else if (gvG.PageIndex == 0)
                disButton("first");
            else
                disButton("");
        }
    }

    protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ESP.Purchase.Entity.GeneralInfo model = (ESP.Purchase.Entity.GeneralInfo)e.Row.DataItem;
            Label labState = ((Label)e.Row.FindControl("labState"));
            labState.Text = State.requistionOrorder_state[int.Parse(labState.Text)].ToString();

            Label labRequisitionflow = (Label)e.Row.FindControl("labRequisitionflow");
            if (null != labRequisitionflow && labRequisitionflow.Text != "")
            {
                labRequisitionflow.Text = State.requisitionflow_state[int.Parse(labRequisitionflow.Text)];
            }
            HyperLink hypView = (HyperLink)e.Row.FindControl("hypView");
            hypView.NavigateUrl = "OrderDetail.aspx?" + RequestName.GeneralID + "=" + e.Row.Cells[0].Text.ToString() + "&backUrl=SuperviseList.aspx";
            Repeater repProduct = (Repeater)e.Row.FindControl("repProduct");
            List<OrderInfo> list = OrderInfoManager.GetListByGeneralId(((GeneralInfo)e.Row.DataItem).id);
            string mtype = "";
            if (gvG.DataKeys[e.Row.RowIndex].Values[1].ToString() == "美元")
                mtype = "＄";
            else
                mtype = "￥";

            for (int i = 0; i < list.Count; i++)
            {
                list[i].moneytype = mtype + list[i].total.ToString("#,##0.####");
            }
            //对私3000以上和媒体3000以上，不显示初审人
            if (model.PRType == (int)ESP.Purchase.Common.PRTYpe.MPPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.ADPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA || model.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
            {
                e.Row.Cells[5].Controls.Clear();
                e.Row.Cells[6].Controls.Clear();
                e.Row.Cells[5].Text = "";
                e.Row.Cells[6].Text = "";
            }
            //如果是对私的PR单且是媒体合作和媒体采买，初审人也屏蔽掉
            int typeoperationflow = OrderInfoManager.getTypeOperationFlow(((GeneralInfo)e.Row.DataItem).id);
            if (model.PRType == (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
            {
                if (typeoperationflow == State.typeoperationflow_MP)
                {
                    e.Row.Cells[5].Controls.Clear();
                    e.Row.Cells[6].Controls.Clear();
                    e.Row.Cells[5].Text = "";
                    e.Row.Cells[6].Text = "";
                }
            }
            repProduct.DataSource = list;
            repProduct.DataBind();
        }
    }

    protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvG.PageIndex = e.NewPageIndex;
        ListBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ListBind();
    }

    protected void btnLast_Click(object sender, EventArgs e)
    {
        Paging(gvG.PageCount);
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        Paging(0);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Paging((gvG.PageIndex + 1) > gvG.PageCount ? gvG.PageCount : (gvG.PageIndex + 1));
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Paging((gvG.PageIndex - 1) < 0 ? 0 : (gvG.PageIndex - 1));
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

    protected void ExportToOrderInfoExcel(int id)
    {
        FileHelper.ToOrderInfoExcel(id, Server.MapPath("~"), Response);
        GC.Collect();
    }

    protected void ExportToGeneralInfoExcel(int id)
    {
        FileHelper.ToGeneralInfoExcel(id, Server.MapPath("~"), Response);
        GC.Collect();
    }
    protected void ExportRequisition(int id)
    {
        FileHelper.ExportRequisition(id, Server.MapPath("~"), Response);
        GC.Collect();
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
}
