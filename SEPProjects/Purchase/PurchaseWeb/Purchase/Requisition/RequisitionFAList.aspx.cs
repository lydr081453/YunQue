using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using System.Data;

namespace PurchaseWeb.Purchase.Requisition
{
    public partial class RequisitionFAList : ESP.Web.UI.PageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListBind();
                ListBindPr();
            }
        }

        private void ListBind()
        {
            Session["exportTable1"] = null;
            List<SqlParameter> parms = new List<SqlParameter>();
            string term = " and a.PRType =@Media and a.requestor=@requestor ";
            parms.Add(new SqlParameter("@requestor", CurrentUser.SysID));
            parms.Add(new SqlParameter("@Media", (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA));

            if (txtBegin.Text.Trim() != "")
            {
                term += " and a.app_date >=CONVERT(datetime , @begin, 120 )";
                parms.Add(new SqlParameter("@begin", txtBegin.Text.Trim()));
            }
            if (txtEnd.Text.Trim() != "")
            {
                term += " and a.app_date <= dateadd(d,1,CONVERT(datetime , @end, 120 ))";
                parms.Add(new SqlParameter("@end", txtEnd.Text.Trim()));
            }
            if (txtKey.Text.Trim() != "")
            {
                int result = 0;
                term += string.Format(" and (d.prno like '%'+@keys+'%' or a.supplier_name like '%'+@keys+'%' or a.prno like '%'+@keys+'%' or a.project_code like '%'+@keys+'%' {0})", int.TryParse(txtKey.Text.Trim(), out result) ? " or oldprid=@keys or a.id=@keys " : "");
                parms.Add(new SqlParameter("@keys", txtKey.Text.Trim()));
            }
            if (txtBegin.Text.Trim() == "" && txtEnd.Text.Trim() == "" && txtKey.Text.Trim() == "")
            {
                term += " and a.app_date>@appdate ";
                parms.Add(new SqlParameter("@appdate", DateTime.Now.AddMonths(-3).ToString()));
            }
            DataTable list = GeneralInfoManager.GetTableByNewMedia(term, parms);
            Session["exportTable1"] = list;
            gvG.DataSource = list;
            gvG.DataBind();
        }

        private void ListBindPr()
        {
            Session["exportTable2"] = null;
            List<SqlParameter> parms = new List<SqlParameter>();
            string term = " and a.PRType =@Private and a.requestor=@requestor";
            parms.Add(new SqlParameter("@requestor", CurrentUser.SysID));
            parms.Add(new SqlParameter("@Private", (int)ESP.Purchase.Common.PRTYpe.PR_PriFA));
           

            if (txtBegin.Text.Trim() != "")
            {
                term += " and a.app_date >=CONVERT(datetime , @begin, 120 )";
                parms.Add(new SqlParameter("@begin", txtBegin.Text.Trim()));
            }
            if (txtEnd.Text.Trim() != "")
            {
                term += " and a.app_date <= dateadd(d,1,CONVERT(datetime , @end, 120 ))";
                parms.Add(new SqlParameter("@end", txtEnd.Text.Trim()));
            }
            if (txtKey.Text.Trim() != "")
            {
                int result = 0;
                term += string.Format(" and (d.prno like '%'+@keys+'%' or a.supplier_name like '%'+@keys+'%' or a.prno like '%'+@keys+'%' or a.project_code like '%'+@keys+'%' {0})", int.TryParse(txtKey.Text.Trim(), out result) ? " or oldprid=@keys or a.id=@keys " : "");
                parms.Add(new SqlParameter("@keys", txtKey.Text.Trim()));
            }
            if (txtBegin.Text.Trim() == "" && txtEnd.Text.Trim() == "" && txtKey.Text.Trim() == "")
            {
                term += " and a.app_date>@appdate ";
                parms.Add(new SqlParameter("@appdate", DateTime.Now.AddMonths(-3).ToString()));
            }
            DataTable list = GeneralInfoManager.GetTableByNewMedia(term, parms);
            Session["exportTable2"] = list;
            gvPR.DataSource = list;
            gvPR.DataBind();
    
        }

        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            //合并媒介和个人数据
            DataTable exportTable1 = (DataTable)Session["exportTable1"];
            DataTable exportTable2 = (DataTable)Session["exportTable2"];
            DataTable newTable = new DataTable();
            newTable = exportTable1.Clone();
            foreach (DataRow dr in exportTable1.Rows)
            {
                newTable.Rows.Add(dr.ItemArray);
            }
            foreach (DataRow dr in exportTable2.Rows)
            {
                newTable.Rows.Add(dr.ItemArray);
            }
            FileHelper.ExportCollateInfoByNewPR(newTable, Server.MapPath("~"), Response);
            GC.Collect();
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
                DataRowView dv = (DataRowView)e.Row.DataItem;
                System.Web.UI.WebControls.HyperLink hylEdit = (System.Web.UI.WebControls.HyperLink)e.Row.FindControl("hylEdit");
                if (null != hylEdit)
                {
                    hylEdit.NavigateUrl = string.Format(State.addstatus_Back, e.Row.Cells[0].Text.ToString(),"RequisitionFAList.aspx");
                    if (dv["status"].ToString() != State.requisition_save.ToString() && dv["status"].ToString() != State.requisition_return.ToString())
                    {
                        hylEdit.Visible = false;
                    }
                }
                Label lblName = (Label)e.Row.FindControl("lblName");
               
                //get original PR Model
                ESP.Purchase.Entity.MediaPREditHisInfo RelationModel =ESP.Purchase.BusinessLogic.MediaPREditHisManager.GetModelByNewPRID(int.Parse(dv["id"].ToString()));
                if (RelationModel != null)
                {
                    ESP.Purchase.Entity.GeneralInfo OriginalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(RelationModel.OldPRId.Value);
                    lblName.Text = OriginalModel.requestorname;
                    lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(OriginalModel.requestor) + "');");

                    HyperLink hylPrint = (HyperLink)e.Row.FindControl("hylPrint");
                    ESP.Purchase.Entity.PrintLogInfo printModel = ESP.Purchase.BusinessLogic.PrintLogManager.GetModelByFormID(RelationModel.OldPRId.Value);
                    hylPrint.Attributes["onclick"] = "javascript:openPrint('" + RelationModel.OldPRId.Value + "');window.location='RequisitionFAList.aspx';";
                    if (printModel == null)//未打印
                    {
                        hylPrint.ToolTip = "未打印";
                        hylPrint.ImageUrl = "~/images/printno.gif";
                    }
                    else//已打印
                    {
                        hylPrint.ToolTip = "已打印" + (printModel.PrintCount == null ? 0 : printModel.PrintCount.Value) + "次";
                        hylPrint.ImageUrl = "~/images/printed.gif";
                    }
                }

                //获取新PR单生成的付款申请
                Repeater repReturnStatus = (Repeater)e.Row.FindControl("repReturnStatus");
                IList<ESP.Finance.Entity.ReturnInfo> returnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(" prid=" + dv["id"].ToString());
                repReturnStatus.DataSource = returnList;
                repReturnStatus.DataBind();
            }
        }

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            ListBind();
        }
        protected void gvPR_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPR.PageIndex = e.NewPageIndex;
            ListBindPr();
        }
        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            ListBind();
            ListBindPr();
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            Paging(gvG.PageCount);
        }
        protected void btnPRLast_Click(object sender, EventArgs e)
        {
            PagingPR(gvPR.PageCount);
        }
        protected void btnPRFirst_Click(object sender, EventArgs e)
        {
            PagingPR(0);
        }
        protected void btnPRNext_Click(object sender, EventArgs e)
        {
            PagingPR((gvPR.PageIndex + 2) >= gvPR.PageCount ? gvPR.PageCount : (gvPR.PageIndex + 1));
        }
        protected void btnPRPrevious_Click(object sender, EventArgs e)
        {
            PagingPR((gvPR.PageIndex - 1) < 1 ? 0 : (gvPR.PageIndex - 1));
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
        private void PagingPR(int pageIndex)
        {
            GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
            gvPR_PageIndexChanging(new object(), e);
        }

        private void disButton(string page)
        {
            //switch (page)
            //{
            //    case "first":
            //        btnFirst.Enabled = false;
            //        btnPrevious.Enabled = false;
            //        btnNext.Enabled = true;
            //        btnLast.Enabled = true;

            //        btnFirst2.Enabled = false;
            //        btnPrevious2.Enabled = false;
            //        btnNext2.Enabled = true;
            //        btnLast2.Enabled = true;
            //        break;
            //    case "last":
            //        btnFirst.Enabled = true;
            //        btnPrevious.Enabled = true;
            //        btnNext.Enabled = false;
            //        btnLast.Enabled = false;

            //        btnFirst2.Enabled = true;
            //        btnPrevious2.Enabled = true;
            //        btnNext2.Enabled = false;
            //        btnLast2.Enabled = false;
            //        break;
            //    default:
            //        btnFirst.Enabled = true;
            //        btnPrevious.Enabled = true;
            //        btnNext.Enabled = true;
            //        btnLast.Enabled = true;

            //        btnFirst2.Enabled = true;
            //        btnPrevious2.Enabled = true;
            //        btnNext2.Enabled = true;
            //        btnLast2.Enabled = true;
            //        break;
            //}
        }

        private void disButtonPR(string page)
        {
            //switch (page)
            //{
            //    case "first":
            //        btnPRFirst.Enabled = false;
            //        btnPRPrevious.Enabled = false;
            //        btnPRNext.Enabled = true;
            //        btnPRLast.Enabled = true;

            //        btnPRFirst2.Enabled = false;
            //        btnPRPrevious2.Enabled = false;
            //        btnPRNext2.Enabled = true;
            //        btnPRLast2.Enabled = true;
            //        break;
            //    case "last":
            //        btnPRFirst.Enabled = true;
            //        btnPRPrevious.Enabled = true;
            //        btnPRNext.Enabled = false;
            //        btnPRLast.Enabled = false;

            //        btnPRFirst2.Enabled = true;
            //        btnPRPrevious2.Enabled = true;
            //        btnPRNext2.Enabled = false;
            //        btnPRLast2.Enabled = false;
            //        break;
            //    default:
            //        btnPRFirst.Enabled = true;
            //        btnPRPrevious.Enabled = true;
            //        btnPRNext.Enabled = true;
            //        btnPRLast.Enabled = true;

            //        btnPRFirst2.Enabled = true;
            //        btnPRPrevious2.Enabled = true;
            //        btnPRNext2.Enabled = true;
            //        btnPRLast2.Enabled = true;
            //        break;
            //}
        }
    }
}
