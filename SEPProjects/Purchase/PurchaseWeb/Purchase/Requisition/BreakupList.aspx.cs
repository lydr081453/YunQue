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
using System.IO;
using System.Threading;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_BreakupList : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlStatus.Items.Insert(0, new ListItem("全部","全部"));
            ddlStatus.Items.Insert(1, new ListItem(State.recipient_WS, State.recipient_WS));
            ddlStatus.Items.Insert(2, new ListItem(State.recipient_SHZ, State.recipient_SHZ));
            ddlStatus.Items.Insert(3, new ListItem(State.recipient_YSW, State.recipient_YSW));
            ListBind();
        }
    }


    private void ListBind()
    {
        List<SqlParameter> parms = new List<SqlParameter>();
        string term = string.Empty;

        if (ddlStatus.SelectedValue == "全部")
        {
            term += " and status in (" + State.requisition_recipiented + "," + State.order_confirm + ")";
        }
        else
        {
            term += " and status = @status ";
            if (ddlStatus.SelectedValue == State.recipient_YSW)
            {
                parms.Add(new SqlParameter("@status", State.requisition_recipiented));
            }
            else if (ddlStatus.SelectedValue == State.recipient_WS)
            {
                parms.Add(new SqlParameter("@status", State.order_confirm));
                term += " and a.id not in (select gid from t_recipient)";
            }
            else
            {
                parms.Add(new SqlParameter("@status", State.order_confirm));
                term += " and a.id in (select gid from t_recipient)";
            }
        }


        if (ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorName"].ToString().ToLower() != CurrentUser.ITCode.ToLower())
        {
            term += " and (requestor = @requestor or enduser = @requestor or goods_receiver = @requestor or first_assessor =@requestor or Filiale_Auditor=@requestor)";
            parms.Add(new SqlParameter("@requestor", CurrentUser.SysID));
        }
        if (txtsupplierName.Text.Trim() != "")
        {
            term += " and supplier_name like '%'+@suppliername+'%'";
            parms.Add(new SqlParameter("@suppliername", txtsupplierName.Text.Trim()));
        }

        if (txtPrNo.Text.Trim() != "")
        {
            term += " and prNo like '%'+@prNo+'%'";
            parms.Add(new SqlParameter("@prNo", txtPrNo.Text.Trim()));
        }
        if (txtProjectCode.Text.Trim() != "")
        {
            term += " and project_code like '%'+@projectcode+'%'";
            parms.Add(new SqlParameter("@projectcode", txtProjectCode.Text.Trim()));
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
        if (txtOrderNo.Text.Trim() != "")
        {
            term += " and orderid like '%'+@orderid+'%'";
            parms.Add(new SqlParameter("@orderid", txtOrderNo.Text.Trim()));
        }
        if (txtAudit.Text.Trim() != "")
        {
            term += " and first_assessorname like '%'+@first_assessorname+'%'";
            parms.Add(new SqlParameter("@first_assessorname", txtAudit.Text.Trim()));
        }
        if (txtgoods_receiver.Text.Trim() != "")
        {
            term += " and receivername like '%'+@receivername+'%'";
            parms.Add(new SqlParameter("@receivername", txtgoods_receiver.Text.Trim()));
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
        //int id = int.Parse(gvG.Rows[int.Parse(e.CommandArgument.ToString())].Cells[0].Text.Trim());
        //if (e.CommandName == "Del")
        //{
        //    GeneralInfoManager.Delete(id, CurrentUserID, CurrentUserName);
        //    ListBind();
        //}
        //if (e.CommandName == "Export")
        //{
        //    ExportToOrderInfoExcel(id);
        //}
        //if (e.CommandName == "ExportRequisition")
        //{
        //    ExportToGeneralInfoExcel(id);
        //}
    }

    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Repeater repProduct = (Repeater) e.Row.FindControl("repProduct");
            List<OrderInfo> list =
                OrderInfoManager.GetListByGeneralId(((GeneralInfo) e.Row.DataItem).id);
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


            Panel printpo = (Panel) e.Row.FindControl("printpo"); // 打印订单
            if (string.IsNullOrEmpty((((GeneralInfo) e.Row.DataItem)).orderid))
            {
                if (printpo != null)
                    printpo.Visible = false;
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

    //protected void ExportToOrderInfoExcel(int id)
    //{
    //    FileHelper.ToOrderInfoExcel(id, Server.MapPath("~"), Response);
    //    GC.Collect();
    //}

    //protected void ExportToGeneralInfoExcel(int id)
    //{
    //    FileHelper.ToGeneralInfoExcel(id, Server.MapPath("~"), Response);
    //    GC.Collect();
    //}
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
