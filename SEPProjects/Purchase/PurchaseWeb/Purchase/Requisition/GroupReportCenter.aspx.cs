using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using ESP.Purchase.BusinessLogic;

public partial class Purchase_Requisition_GroupReportCenter : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtBegin.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd").Remove(8) + "25";
            txtEnd.Text = DateTime.Now.ToString("yyyy-MM-dd").Remove(8) + "25";
            ListBind();
        }
    }

    private void ListBind()
    {
        DataTable dt = new DataTable();
        string strWhere = "";
        List<SqlParameter> parms = new List<SqlParameter>();

        strWhere += string.Format(" a.Status not in ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11})", State.requisition_save, State.requisition_commit, State.requisition_return, State.order_return, State.requisition_submit, State.requisition_ok, State.requisition_temporary_commit, State.requisition_operationAduit, State.order_mediaAuditWait, State.order_ADAuditWait, State.requisition_MediaFAOperated, State.requisition_del);  //State.order_ok, State.order_sended, State.order_confirm, State.requisition_recipiented, State.order_mediaAuditYes);
        strWhere += string.Format(" and a.prtype not in ({0},{1})", (int)PRTYpe.PR_MediaFA, (int)PRTYpe.PR_PriFA);
        if (txtBegin.Text.Trim() != "")
        {
            strWhere += " and order_audittime >=CONVERT(datetime , @begin, 120 )";
            parms.Add(new SqlParameter("@begin", txtBegin.Text.Trim()));
        }
        if (txtEnd.Text.Trim() != "")
        {
            strWhere += " and order_audittime <= dateadd(d,1,CONVERT(datetime , @end, 120 ))";
            parms.Add(new SqlParameter("@end", txtEnd.Text.Trim()));
        }
        if (txtSupplierName.Text.Trim() != "")
        {
            strWhere += " and supplier_name like '%'+@suppliername+'%'";
            parms.Add(new SqlParameter("@suppliername", txtSupplierName.Text.Trim()));
        }
        if (txtProductType.Text.Trim() != "")
        {
            strWhere += @" and a.id in (select distinct a.general_id from t_orderinfo as a 
                        inner join t_type as b on b.typeid=a.producttype
                        where b.typelevel=3 and b.typename like '%'+@typename+'%')";
            parms.Add(new SqlParameter("@typename", txtProductType.Text.Trim()));
        }
        if (txtID.Text.Trim() != "")
        {
            int totalgno = 0;
            bool res = int.TryParse(txtID.Text, out totalgno);
            if (res)
            {
                strWhere += " and a.id = @pid";
                parms.Add(new SqlParameter("@pid", txtID.Text.Trim()));
            }
        }
        if (txtPr.Text.Trim() != "")
        {
            strWhere += " and prNo like '%'+@prNo+'%'";
            parms.Add(new SqlParameter("@prNo", txtPr.Text.Trim()));
        }

        List<ESP.Purchase.Entity.GeneralInfo> list = GeneralInfoManager.GetStatusList(" and" + strWhere, parms);
        gvG.DataSource = list;
        gvG.DataBind();

        dt = GeneralInfoManager.GetList(strWhere,parms);
        Session["dt"] = dt;
        Session["date"] = txtBegin.Text + " 至 " + txtEnd.Text;

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
        if (dt.Rows.Count > 0)
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

    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ESP.Purchase.Entity.GeneralInfo model = (ESP.Purchase.Entity.GeneralInfo)e.Row.DataItem;
            //对私3000以上和媒体3000以上，不显示初审人
            if (model.PRType == (int)ESP.Purchase.Common.PRTYpe.MPPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.ADPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA || model.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
            {
                e.Row.Cells[7].Controls.Clear();
                e.Row.Cells[6].Controls.Clear();
                e.Row.Cells[7].Text = "";
                e.Row.Cells[6].Text = "";
            }
            //如果是对私的PR单且是媒体合作和媒体采买，初审人也屏蔽掉
            int typeoperationflow = OrderInfoManager.getTypeOperationFlow(model.id);
            if (model.PRType == (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
            {
                if (typeoperationflow == State.typeoperationflow_MP)
                {
                    e.Row.Cells[6].Controls.Clear();
                    e.Row.Cells[7].Controls.Clear();
                    e.Row.Cells[6].Text = "";
                    e.Row.Cells[7].Text = "";
                }
            }
            Label labState = ((Label)e.Row.FindControl("labState"));
            labState.Text = State.requistionOrorder_state[int.Parse(labState.Text)].ToString();

            Repeater repProductType = (Repeater)e.Row.FindControl("repProductType");
            Repeater repProduct = (Repeater)e.Row.FindControl("repProduct");
            List<ESP.Purchase.Entity.OrderInfo> list = OrderInfoManager.GetListByGeneralId(model.id);
            string mtype = "";
            if (gvG.DataKeys[e.Row.RowIndex].Values[1].ToString() == "美元")
                mtype = "＄";
            else
                mtype = "￥";

            for (int i = 0; i < list.Count; i++)
            {
                list[i].moneytype = mtype + list[i].total.ToString("#,##0.####");

            }
            repProduct.DataSource = list;
            repProduct.DataBind();

            repProductType.DataSource = list;
            repProductType.DataBind();

            Label labRequisitionflow = (Label)e.Row.FindControl("labRequisitionflow");
            if (null != labRequisitionflow && labRequisitionflow.Text != "")
                labRequisitionflow.Text = State.requisitionflow_state[int.Parse(labRequisitionflow.Text)];
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

    protected void btnDailyPurchase_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dt"];
        FileHelper.ExportDailyPurchase(dt.Select(), Session["date"].ToString(), Server.MapPath("~"), Response);
        GC.Collect();
    }

    protected void btnCusAsk_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dt"];
        FileHelper.ExportCusAsk(dt.Select("cusAsk='是'"), Session["date"].ToString(), Server.MapPath("~"), Response);
        GC.Collect();
    }

    protected void btnAfterwards_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dt"];
        FileHelper.ExportAfterwards(dt.Select("afterwardsname='是'"), Session["date"].ToString(), Server.MapPath("~"), Response);
        GC.Collect();
    }

    protected void btnEmBuy_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dt"];
        FileHelper.ExportEmBuy(dt.Select("EmBuy='是'"), Session["date"].ToString(), Server.MapPath("~"), Response);
        GC.Collect();
    }

    protected void btnCostReport_Click(object sender, EventArgs e)
    {
        FileHelper.ExprotCostReport((DataTable)Session["dt"], "", Server.MapPath("~"), Response);
    }
}
