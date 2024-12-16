﻿using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using ESP.Compatible;

public partial class Purchase_Requisition_AuditRequistion : ESP.Web.UI.PageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TypeBind();
            ListBind();
        }
    }

    /// <summary>
    /// 绑定申请单流向
    /// </summary>
    private void TypeBind()
    {
        ddlRequisitionflow.Items.Add(new ListItem("全部", "-1"));
        ddlRequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toO], State.requisitionflow_toO.ToString()));
        ddlRequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toR], State.requisitionflow_toR.ToString()));
        ddlRequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toC], State.requisitionflow_toC.ToString()));
    }

    /// <summary>
    /// 绑定列表
    /// </summary>
    private void ListBind()
    {
        string DelegateUsers = string.Empty;
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
        foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
        {
            DelegateUsers += model.UserID.ToString() + ",";
        }
        DelegateUsers = DelegateUsers.TrimEnd(',');

        List<SqlParameter> parms = new List<SqlParameter>();
        string term = string.Empty;

        if (!string.IsNullOrEmpty(DelegateUsers))
        {
            term += " and ((status = @status and purchaseAuditor=@purchaseAuditor or purchaseAuditor in(" + DelegateUsers + ")) or (status = @status2 and RCAuditor=@RCAuditor or RCAuditor in(" + DelegateUsers + ")))";
        }
        else
            term += " and ((status = @status and purchaseAuditor=@purchaseAuditor) or (status = @status2 and RCAuditor=@RCAuditor))";

        parms.Add(new SqlParameter("@status", State.order_commit));
        parms.Add(new SqlParameter("@status2", State.requisition_RiskControl));
        parms.Add(new SqlParameter("@purchaseAuditor", CurrentUser.SysID));
        parms.Add(new SqlParameter("@RCAuditor", CurrentUser.SysID));

        if (txtpurchaseAuditorName.Text.Trim() != "")
        {
            term += " and purchaseAuditorName like '%'+@purchaseAuditorName+'%'";
            parms.Add(new SqlParameter("@purchaseAuditorName", txtpurchaseAuditorName.Text.Trim()));
        }

        if (txtLogs.Text.Trim() != "")
        {
            term += " and (a.id in (select gid from t_log where des like '%'+@logs+'%') or a.id in (select gid from t_auditlog where auditusername like '%'+@logs+'%'))";
            parms.Add(new SqlParameter("@logs", txtLogs.Text.Trim()));
        }

        ////只能看到采购审批人是自己的单子。
        //term += " and purchaseAuditor=@purchaseAuditor";

        //parms.Add(new SqlParameter("@purchaseAuditor", CurrentUser.SysID));

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
        if (txtAudit.Text.Trim() != "")
        {
            term += " and first_assessorname like '%'+@first_assessorname+'%'";
            parms.Add(new SqlParameter("@first_assessorname", txtAudit.Text.Trim()));
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

        if (txtsupplier.Text.Trim() != "")
        {
            term += " and supplier_name like '%'+@suppliername+'%'";
            parms.Add(new SqlParameter("@suppliername", txtsupplier.Text.Trim()));
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
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Repeater repProductType = (Repeater)e.Row.FindControl("repProductType");
            Repeater repProduct = (Repeater)e.Row.FindControl("repProduct");

            List<OrderInfo> list = OrderInfoManager.GetListByGeneralId(((GeneralInfo)e.Row.DataItem).id);
            string mtype = string.Empty;
            if (null != gvG.DataKeys[e.Row.RowIndex] && gvG.DataKeys[e.Row.RowIndex].Values[1].ToString() == "美元")
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
            {
                labRequisitionflow.Text = State.requisitionflow_state[int.Parse(labRequisitionflow.Text)];
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GeneralInfo model = (GeneralInfo)e.Row.DataItem;
            Label labFiliAudi = ((Label)e.Row.FindControl("labFiliAudi"));
            if (null != labFiliAudi)
            {
                if (!string.IsNullOrEmpty(labFiliAudi.Text))
                {
                    Employee em = new Employee(int.Parse(labFiliAudi.Text));
                    labFiliAudi.Text = em.Name;
                }
            }

            //对私3000以上和媒体3000以上，不显示初审人
            if (model.PRType == (int)ESP.Purchase.Common.PRTYpe.MPPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.ADPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA || model.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
            {
                e.Row.Cells[7].Controls.Clear();
                e.Row.Cells[6].Controls.Clear();
                e.Row.Cells[7].Text = "";
                e.Row.Cells[6].Text = "";
            }
            //如果是对私的PR单且是媒体合作和媒体采买，初审人也屏蔽掉
            int typeoperationflow = OrderInfoManager.getTypeOperationFlow(((GeneralInfo)e.Row.DataItem).id);
            if (model.PRType == (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
            {
                if (typeoperationflow == State.typeoperationflow_MP)
                {
                    e.Row.Cells[7].Controls.Clear();
                    e.Row.Cells[6].Controls.Clear();
                    e.Row.Cells[7].Text = "";
                    e.Row.Cells[6].Text = "";
                }
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ListBind();
    }

    #region 分页
    protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvG.PageIndex = e.NewPageIndex;
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
    #endregion

}
