using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.IO;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using ESP.Compatible;

//using Microsoft.Office.Interop.Excel;

public partial class Purchase_FilialeAuditList : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //TypeBind();
            ListBind();
        }
    }

    private void ListBind()
    {
        List<SqlParameter> parms = new List<SqlParameter>();
        string term = " and (status =@status or (status=@status1 and Filiale_Auditor is not null and Filiale_Auditor <>0 )) and prtype <> @prtype";
        parms.Add(new SqlParameter("@status", State.requisition_temporary_commit));
        parms.Add(new SqlParameter("@status1", State.order_return));
        parms.Add(new SqlParameter("@prtype", (int)PRTYpe.ADPR));
        if (ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorName"].ToString().ToLower() != CurrentUser.ITCode.ToLower() && ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorId2"].ToString().Trim() != CurrentUser.SysID)
        {
            term += " and (Filiale_Auditor=@filiale";
            parms.Add(new SqlParameter("@filiale", CurrentUser.SysID));
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
            string uids = "";
            foreach (ESP.Framework.Entity.AuditBackUpInfo backup in delegates)
            {
                uids += backup.UserID + ",";
            }
            if (uids != "")
            {
                term += string.Format(" or Filiale_Auditor in ({0}) ", uids.TrimEnd(','));
            }
            term += ")";
        }
        if (txtPrNo.Text.Trim() != "")
        {
            term += " and prNo like '%'+@prNo+'%'";
            parms.Add(new SqlParameter("@prNo", txtPrNo.Text.Trim()));
        }
        if (txtrequestor.Text.Trim() != "")
        {
            term += " and requestorname like '%'+@requestor+'%'";
            parms.Add(new SqlParameter("@requestor", txtrequestor.Text.Trim()));
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
            GeneralInfo model = (GeneralInfo)e.Row.DataItem;
            Repeater repProductType = (Repeater)e.Row.FindControl("repProductType");
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

            repProductType.DataSource = list;
            repProductType.DataBind();

            Label labFiliAudi = ((Label)e.Row.FindControl("labFiliAudi"));
            if (null != labFiliAudi)
            {
                if(!string.IsNullOrEmpty(labFiliAudi.Text))
                {

                    ESP.Compatible.Employee em = new Employee(int.Parse(labFiliAudi.Text));
                    labFiliAudi.Text = em.Name;
                }
            }
            //对私3000以上和媒体3000以上，不显示初审人
            if (model.PRType == (int)ESP.Purchase.Common.PRTYpe.MPPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.ADPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA || model.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
            {
                e.Row.Cells[7].Controls.Clear();
                e.Row.Cells[8].Controls.Clear();
                e.Row.Cells[7].Text = "";
                e.Row.Cells[8].Text = "";
            }
            //如果是对私的PR单且是媒体合作和媒体采买，初审人也屏蔽掉
            int typeoperationflow = OrderInfoManager.getTypeOperationFlow(((GeneralInfo)e.Row.DataItem).id);
            if (model.PRType == (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
            {
                if (typeoperationflow == State.typeoperationflow_MP)
                {
                    e.Row.Cells[7].Controls.Clear();
                    e.Row.Cells[8].Controls.Clear();
                    e.Row.Cells[7].Text = "";
                    e.Row.Cells[8].Text = "";
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
