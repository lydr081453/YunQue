using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections.Generic;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_OrderCommitList : ESP.Web.UI.PageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TypeBind();
            ListBind();
        }
    }

    private void TypeBind()
    {
        ddlRequisitionflow.Items.Add(new ListItem("全部", "-1"));

        ddlRequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toO], State.requisitionflow_toO.ToString()));
        ddlRequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toR], State.requisitionflow_toR.ToString()));
        ddlRequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toC], State.requisitionflow_toC.ToString()));
    }

    private void ListBind()
    {
        List<SqlParameter> parms = new List<SqlParameter>();

        string term = string.Empty;

            term = " and (status = @status ) ";
            parms.Add(new SqlParameter("@status", State.order_commit));

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
        if (txtProjectCode.Text.Trim() != "")
        {
            term += " and project_code like '%'+@projectcode+'%'";
            parms.Add(new SqlParameter("@projectcode", txtProjectCode.Text.Trim()));
        }
        if (txtRequestor.Text.Trim() != "")
        {
            term += " and requestorname like '%'+@requestorname+'%'";
            parms.Add(new SqlParameter("@requestorname", txtRequestor.Text.Trim()));
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
        if (e.CommandName == "Return")
        {
            string gid = e.CommandArgument.ToString();

            GeneralInfo g = GeneralInfoManager.GetModel(int.Parse(gid));
            string alertMsg = "";
            if (g.InUse == (int)State.PRInUse.Use)
            {
                string logMsg = "";
                if (null != g)
                {
                    if (CurrentUser.SysID == g.first_assessor.ToString()) 
                    {
                        logMsg = string.Format(State.log_requisition_cancelJT, CurrentUserName, DateTime.Now.ToString());
                        alertMsg = string.Format(State.log_requisition_cancelJT, "", "");
                    }
                    if (CurrentUser.SysID == g.Filiale_Auditor.ToString()) 
                    {
                        logMsg = string.Format(State.log_requisition_cancelFGS, CurrentUserName + "(" + CurrentUser.ITCode + ")", DateTime.Now.ToString());
                        alertMsg = string.Format(State.log_requisition_cancelFGS, "", "");
                    }
                    ESP.ITIL.BusinessLogic.申请单业务设置.物料审核人撤销PR(CurrentUser, ref g);
                    GeneralInfoManager.Update(g);
                    //记录操作日志
                    ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, g.id, "撤销集团已审核的申请单"), "编辑申请单");
                    LogInfo log = new LogInfo();

                    log.Gid = g.id;
                    log.LogMedifiedTeme = DateTime.Now;
                    log.LogUserId = CurrentUserID;

                    log.Des = logMsg;
                    LogManager.AddLog(log, Request);
                }
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + alertMsg + "！');window.location='OrderCommitList.aspx'", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + ESP.Purchase.Common.State.DisabledMessageForPR + "');window.location='OrderCommitList.aspx'", true);
            }
        }
    }
    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GeneralInfo model = (GeneralInfo)e.Row.DataItem;
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
            repProduct.DataSource = list;
            repProduct.DataBind();

            Label labRequisitionflow = (Label)e.Row.FindControl("labRequisitionflow");
            if (null != labRequisitionflow && labRequisitionflow.Text != "")
            {
                labRequisitionflow.Text = State.requisitionflow_state[int.Parse(labRequisitionflow.Text)];
            }
            LinkButton btnCancel = (LinkButton)e.Row.FindControl("btnCancel");
            if (CurrentUser.SysID != model.first_assessor.ToString() && CurrentUser.SysID != model.Filiale_Auditor.ToString())
            {
                btnCancel.Visible = false;
            }
            if (model.InUse != (int)State.PRInUse.Use)
                btnCancel.Visible = false;
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
}
