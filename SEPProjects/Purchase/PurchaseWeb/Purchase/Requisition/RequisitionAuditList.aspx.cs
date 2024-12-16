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
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_RequisitionAuditList : ESP.Web.UI.PageBase
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
        List<TypeInfo> list = TypeManager.GetListByParentId(0);
        ddltype.DataSource = list;
        ddltype.DataTextField = "typename";
        ddltype.DataValueField = "typeid";
        ddltype.DataBind();
        ddltype.Items.Insert(0, new ListItem("请选择...", "-1"));
        ddltype1.Items.Insert(0, new ListItem("请选择...", "-1"));
        ddltype2.Items.Insert(0, new ListItem("请选择...", "-1"));
    }

    private void ListBind()
    {
        List<SqlParameter> parms = new List<SqlParameter>();

        string term = "";

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
        string strType = string.Empty;

        if (ddltype.SelectedValue != "-1")
        {
            strType = "select typeid from t_type where parentid in(select a.typeid from t_type a inner join t_type b on a.parentid=b.typeid where a.parentid=" + ddltype.SelectedValue + ")";

            if (hidtype1.Value != "" && hidtype1.Value != "-1")
            {
                strType = hidtype1.Value;
                if (hidtype2.Value != "" && hidtype2.Value != "-1")
                    strType = hidtype2.Value;
            }
        }
        if (strType != string.Empty && hidtype2.Value != "" && hidtype2.Value != "-1")
        {
            term += " and a.id in (select distinct general_id from t_orderinfo where producttype in (" + strType + "))";            
        }
        else if (strType != string.Empty && hidtype1.Value != "" && hidtype1.Value != "-1")
        {
            term += " and a.id in (select distinct general_id from t_orderinfo where producttypelv2 in (" + strType + "))";
        }
        else if (strType != string.Empty)
        {
            term += " and a.id in (select distinct general_id from t_orderinfo where producttype in (" + strType + "))";
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
        int id = int.Parse(gvG.Rows[int.Parse(e.CommandArgument.ToString())].Cells[0].Text.Trim());
        if (e.CommandName == "Export")
        {
            ExportToOrderInfoExcel(id);
        }
        if (e.CommandName == "ExportRequisition")
        {
            ExportToGeneralInfoExcel(id);
        }
    }
    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label labState = ((Label)e.Row.FindControl("labState"));
            labState.Text = State.requistionOrorder_state[int.Parse(labState.Text)].ToString();
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
