using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_RequisitionSaveList : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ListBind();
        }
    }

    private void ListBind()
    {
        List<SqlParameter> parms = new List<SqlParameter>();
        string term = " and (status = @status or status = @status1) and PRType not in (@Media,@Private) and requestor=@requestor";
        parms.Add(new SqlParameter("@status", State.requisition_save));
        parms.Add(new SqlParameter("@status1", State.requisition_return));
        parms.Add(new SqlParameter("@requestor", CurrentUser.SysID));
        parms.Add(new SqlParameter("@Media", (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA));
        parms.Add(new SqlParameter("@Private", (int)ESP.Purchase.Common.PRTYpe.PR_PriFA));

        if (txtProjectNo.Text.Trim() != "")
        {
            term += " and project_code like '%'+@projectcode+'%'";
            parms.Add(new SqlParameter("@projectcode", txtProjectNo.Text.Trim()));
        }
        if (txtItemNo.Text.Trim() != "")
        {
            term += " and a.id in (select distinct general_id from t_orderinfo where item_no like '%'+@itemno+'%')";
            parms.Add(new SqlParameter("@itemno", txtItemNo.Text.Trim()));
        }
        if (txtBegin.Text.Trim() != "")
        {
            term += " and app_date >=CONVERT(datetime , @begin, 120 )";
            parms.Add(new SqlParameter("@begin", txtBegin.Text.Trim()));
        }
        if (txtEnd.Text.Trim() != "")
        {
            term += " and app_date <= dateadd(d,1,CONVERT(datetime , @end, 120 ))";
            parms.Add(new SqlParameter("@end", txtEnd.Text.Trim()));
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
        int id = int.Parse(e.CommandArgument.ToString());
        GeneralInfo generalinfo = GeneralInfoManager.GetModel(id);

        if (e.CommandName == "Del")
        {
            //GeneralInfoManager.DeletePrAll(id);
            GeneralInfoManager.LogicDelete(id);
            //ESP.Purchase.BusinessLogic.ShunyaXiaoMiManager.DeleteByGid(id);
            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo,T_MediaOrder,T_OrderInfo表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, id, "删除申请单相关数据"), "申请单");
            ListBind();
        }
        if (e.CommandName == "Export")
        {
            ExportToGeneralInfoExcel(id);
        }
    }

    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GeneralInfo model = (GeneralInfo)e.Row.DataItem;
            System.Web.UI.WebControls.HyperLink hylEdit = (System.Web.UI.WebControls.HyperLink)e.Row.FindControl("hylEdit");
            if (null != hylEdit)
            {
                switch (e.Row.Cells[1].Text.ToString())
                {
                    case "1":
                        hylEdit.NavigateUrl = string.Format(State.addstatus_1, e.Row.Cells[0].Text.ToString());
                        break;
                    case "2":
                        hylEdit.NavigateUrl = string.Format(State.addstatus_2, e.Row.Cells[0].Text.ToString());
                        break;
                    case "3":
                        hylEdit.NavigateUrl = string.Format(State.addstatus_3, e.Row.Cells[0].Text.ToString());
                        break;
                    case "4":
                        hylEdit.NavigateUrl = string.Format(State.addstatus_4, e.Row.Cells[0].Text.ToString());
                        break;
                    case "5":
                        hylEdit.NavigateUrl = string.Format(State.addstatus_5, e.Row.Cells[0].Text.ToString());
                        break;
                    case "6":
                        hylEdit.NavigateUrl = string.Format(State.addstatus_6, e.Row.Cells[0].Text.ToString());
                        break;
                    default:
                        hylEdit.NavigateUrl = string.Format(State.addstatus_7, e.Row.Cells[0].Text.ToString());
                        break;
                }
            }

            //对私3000以上和媒体3000以上，不显示初审人
            if (model.PRType == (int)ESP.Purchase.Common.PRTYpe.MPPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.ADPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA || model.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
            {
                e.Row.Cells[10].Controls.Clear();
                e.Row.Cells[11].Controls.Clear();
                e.Row.Cells[10].Text = "";
                e.Row.Cells[11].Text = "";
            }
            //如果是对私的PR单且是媒体合作和媒体采买，初审人也屏蔽掉
            int typeoperationflow = OrderInfoManager.getTypeOperationFlow(((GeneralInfo)e.Row.DataItem).id);
            if (model.PRType == (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
            {
                if (typeoperationflow == State.typeoperationflow_MP)
                {
                    e.Row.Cells[10].Controls.Clear();
                    e.Row.Cells[11].Controls.Clear();
                    e.Row.Cells[10].Text = "";
                    e.Row.Cells[11].Text = "";
                }
            }

        }
    }

    protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvG.PageIndex = e.NewPageIndex;
        ListBind();
    }

    protected void SearchBtn_Click(object sender, EventArgs e)
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
        Paging((gvG.PageIndex + 2) >= gvG.PageCount ? gvG.PageCount : (gvG.PageIndex + 1));
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Paging((gvG.PageIndex - 1) < 1 ? 0 : (gvG.PageIndex - 1));
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

    protected void ExportToGeneralInfoExcel(int id)
    {
        FileHelper.ToGeneralInfoExcel(id, Server.MapPath("~"), Response);
        GC.Collect();
    }

    protected void NewUserUrl_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddRequisitionStep2.aspx");
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