using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_LogList : ESP.Web.UI.PageBase
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
        string strwhere = "";
        if (CurrentUser.ITCode.ToLower() != ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorName"].ToLower())
            strwhere = string.Format(@"  and (b.requestor={0} or b.first_assessor={0}  or filiale_auditor={0})", CurrentUserID);
        if (txtPrNo.Text.Trim() != "")
        {
            strwhere += " and prNo like '%'+@prNo+'%'";
            parms.Add(new SqlParameter("@prNo", txtPrNo.Text.Trim()));
        }
        if (txtGlideNo.Text.Trim() != "")
        {
            int totalgno = 0;
            bool res = int.TryParse(txtGlideNo.Text, out totalgno);
            if (res)
            {
                strwhere += " and a.GId = @id";
                parms.Add(new SqlParameter("@id", txtGlideNo.Text.TrimStart('0')));
            }
        }

        List<LogInfo> list = LogManager.GetLoglist(strwhere,parms);

        DataTable dt = new DataTable();
        DataColumn dc1 = new DataColumn("glideno");
        DataColumn dc2 = new DataColumn("prNo");
        DataColumn dc3 = new DataColumn("Des");
        dt.Columns.Add(dc1);
        dt.Columns.Add(dc2);
        dt.Columns.Add(dc3);

        string glideno = "";
        string prNo = "";
        string Des = "";
        DataRow dr = null;
        for (int i = 0; i < list.Count; i++) {
            if (glideno == "")
            {
                dr = dt.NewRow();
            }
            glideno = list[i].glideno;
            prNo = list[i].PrNo;
            Des += list[i].Des + "；";

            if (i == (list.Count-1) || ( i < (list.Count-1) && list[i].Gid != list[i + 1].Gid))
            {
                DataTable auditLogs = AuditLogManager.GetauditLog(list[i].Gid, 2);
                if (auditLogs != null)
                {
                    foreach (DataRow auditLog in auditLogs.Rows)
                    {
                        Des += auditLog["auditUserName"] + "业务" + State.operationAudit_statusName[int.Parse(auditLog["auditType"].ToString())] + " " + auditLog["remarkDate"].ToString() + "；";
                    }
                }

                dr["glideno"] = glideno;
                dr["prNo"] = prNo;
                dr["Des"] = Des;
                dt.Rows.Add(dr);
                glideno = "";
                prNo = "";
                Des = "";
            }
        }

        gvL.DataSource = dt;
        gvL.DataBind();

        if (gvL.PageCount > 1)
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

        labAllNum.Text = labAllNumT.Text = dt.Rows.Count.ToString();
        labPageCount.Text = labPageCountT.Text = (gvL.PageIndex + 1).ToString() + "/" + gvL.PageCount.ToString();
        if (gvL.PageCount > 0)
        {
            if (gvL.PageIndex + 1 == gvL.PageCount)
                disButton("last");
            else if (gvL.PageIndex == 0)
                disButton("first");
            else
                disButton("");
        }

    }

    protected void gvL_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvL.PageIndex = e.NewPageIndex;
        ListBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ListBind();
    }

    protected void btnLast_Click(object sender, EventArgs e)
    {
        Paging(gvL.PageCount);
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        Paging(0);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Paging((gvL.PageIndex + 1) > gvL.PageCount ? gvL.PageCount : (gvL.PageIndex + 1));
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Paging((gvL.PageIndex - 1) < 0 ? 0 : (gvL.PageIndex - 1));
    }

    /// <summary>
    /// 翻页
    /// </summary>
    /// <param name="pageIndex">页码</param>
    private void Paging(int pageIndex)
    {
        GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
        gvL_PageIndexChanging(new object(), e);
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
