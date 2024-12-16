using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
using System.Data.SqlClient;
using ESP.Finance.Utility;
using System.Data;
using System.IO;

public partial class Purchase_PurchaseList : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Search();
            SearchHist();
        }
    }

    private void Search()
    {
        List<SqlParameter> paramlist = new List<SqlParameter>();
        string term = string.Empty;
        string DelegateUsers = string.Empty;
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
        foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
        {
            DelegateUsers += model.UserID.ToString() + ",";
        }
        DelegateUsers = DelegateUsers.TrimEnd(',');
        term = " returnid not in(select returnid from f_pnbatchrelation) ";
        if (!string.IsNullOrEmpty(DelegateUsers))
            term += " and (ReturnStatus=@status1 or  ReturnStatus=@status2 )";
        else
            term += " and (ReturnStatus=@status1 or  ReturnStatus=@status2  )";
        SqlParameter p3 = new SqlParameter("@status2", System.Data.SqlDbType.Int, 4);
        p3.SqlValue = (int)PaymentStatus.PurchaseFirst;
        paramlist.Add(p3);
        SqlParameter p1 = new SqlParameter("@status1", System.Data.SqlDbType.Int, 4);
        p1.SqlValue = (int)PaymentStatus.PurchaseMajor1;
        paramlist.Add(p1);

        if (ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorName"].ToString().ToLower() != CurrentUser.ITCode.ToLower())
        {
            if (!string.IsNullOrEmpty(DelegateUsers))
                term += " AND (PaymentUserID=@sysID or PaymentUserID in(" + DelegateUsers + "))";
            else
                term += " AND PaymentUserID=@sysID";

            SqlParameter p6 = new SqlParameter("@sysID", System.Data.SqlDbType.Int, 4);
            p6.SqlValue = CurrentUser.SysID;
            paramlist.Add(p6);
        }

        if (!string.IsNullOrEmpty(term))
        {
            if (this.txtKey.Text.Trim() != string.Empty)
            {
                term += "  and (PRNO like '%'+@prno+'%' or ProjectCode like '%'+@prno+'%' or returncode like '%'+@prno+'%' or prid like '%'+@prno+'%'  or prefee like '%'+@prno+'%')";
                SqlParameter sp1 = new SqlParameter("@prno", System.Data.SqlDbType.NVarChar, 50);
                sp1.SqlValue = this.txtKey.Text.Trim();
                paramlist.Add(sp1);

            }
            if (this.ddlStatus.SelectedIndex != 0)
            {
                term += " and returnStatus=@returnStatus";
                System.Data.SqlClient.SqlParameter sp2 = new System.Data.SqlClient.SqlParameter("@returnStatus", System.Data.SqlDbType.Int, 4);
                sp2.SqlValue = this.ddlStatus.SelectedValue;
                paramlist.Add(sp2);
            }
            if (!string.IsNullOrEmpty(txtBeginDate.Text.Trim()) && !string.IsNullOrEmpty(txtEndDate.Text.Trim()))
            {
                if (Convert.ToDateTime(txtBeginDate.Text) <= Convert.ToDateTime(txtEndDate.Text))
                {
                    term += " and LastUpdateDateTime between @beginDate and @endDate";
                    System.Data.SqlClient.SqlParameter sp3 = new System.Data.SqlClient.SqlParameter("@beginDate", System.Data.SqlDbType.DateTime, 8);
                    sp3.SqlValue = this.txtBeginDate.Text;
                    paramlist.Add(sp3);
                    System.Data.SqlClient.SqlParameter sp4 = new System.Data.SqlClient.SqlParameter("@endDate", System.Data.SqlDbType.DateTime, 8);
                    sp4.SqlValue = this.txtEndDate.Text;
                    paramlist.Add(sp4);

                }
            }
            IList<ESP.Finance.Entity.ReturnInfo> returnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(term, paramlist);
            var tmplist = returnList.OrderBy(N => N.PreBeginDate);
            IList<ReturnInfo> returnlist = tmplist.ToList();
            this.gvG.DataSource = returnlist;
            this.gvG.DataBind();
        }
    }


    private void SearchHist()
    {
        IList<ESP.Finance.Entity.ReturnInfo> list;
        List<SqlParameter> paramlist = new List<SqlParameter>();
        string term = string.Empty;
        string auditTypes = auditorType.purchase_first + "," + auditorType.purchase_major2;
        term = " returnid in(select returnid from F_ReturnauditHist where AuditorUserID=@AuditorUserID and AuditeStatus!=@AuditeStatus and auditType in (" + auditTypes + "))";
        SqlParameter p1 = new SqlParameter("@AuditorUserID", SqlDbType.Int, 4);
        p1.Value = CurrentUser.SysID;
        paramlist.Add(p1);
        SqlParameter p2 = new SqlParameter("@AuditeStatus", SqlDbType.Int, 4);
        p2.Value = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
        paramlist.Add(p2);
        if (!string.IsNullOrEmpty(term))
        {
            if (this.txtKey.Text.Trim() != string.Empty)
            {
                term += "  and (PRNO like '%'+@prno+'%' or ProjectCode like '%'+@prno+'%' or returncode like '%'+@prno+'%' or prid like '%'+@prno+'%'  or prefee like '%'+@prno+'%')";
                SqlParameter sp1 = new SqlParameter("@prno", System.Data.SqlDbType.NVarChar, 50);
                sp1.SqlValue = this.txtKey.Text.Trim();
                paramlist.Add(sp1);

            }
            if (this.ddlStatus.SelectedIndex != 0)
            {
                term += " and returnStatus=@returnStatus";
                System.Data.SqlClient.SqlParameter sp2 = new System.Data.SqlClient.SqlParameter("@returnStatus", System.Data.SqlDbType.Int, 4);
                sp2.SqlValue = this.ddlStatus.SelectedValue;
                paramlist.Add(sp2);
            }
            if (!string.IsNullOrEmpty(txtBeginDate.Text.Trim()) && !string.IsNullOrEmpty(txtEndDate.Text.Trim()))
            {
                if (Convert.ToDateTime(txtBeginDate.Text) <= Convert.ToDateTime(txtEndDate.Text))
                {
                    term += " and LastUpdateDateTime between @beginDate and @endDate";
                    System.Data.SqlClient.SqlParameter sp3 = new System.Data.SqlClient.SqlParameter("@beginDate", System.Data.SqlDbType.DateTime, 8);
                    sp3.SqlValue = this.txtBeginDate.Text;
                    paramlist.Add(sp3);
                    System.Data.SqlClient.SqlParameter sp4 = new System.Data.SqlClient.SqlParameter("@endDate", System.Data.SqlDbType.DateTime, 8);
                    sp4.SqlValue = this.txtEndDate.Text;
                    paramlist.Add(sp4);

                }
            }
        }
        list = ESP.Finance.BusinessLogic.ReturnManager.GetList(term, paramlist);
        this.gvHist.DataSource = list;
        this.gvHist.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
        SearchHist();
    }

    protected void btnSearchAll_Click(object sender, EventArgs e)
    {
        this.txtKey.Text = string.Empty;
        this.ddlStatus.SelectedIndex = 0;
        this.txtBeginDate.Text = string.Empty;
        this.txtEndDate.Text = string.Empty;
        Search();
        SearchHist();
    }

    protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvG.PageIndex = e.NewPageIndex;
        Search();
    }

    protected void gvHist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHist.PageIndex = e.NewPageIndex;
        SearchHist();
    }

    protected void gvHist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ESP.Finance.Entity.ReturnInfo model = (ReturnInfo)e.Row.DataItem;
            IList<ESP.Finance.Entity.PNBatchRelationInfo> relationList = ESP.Finance.BusinessLogic.PNBatchRelationManager.GetList(" ReturnID=" + model.ReturnID.ToString(), null);

            Label labExpectPaymentPrice = (Label)e.Row.FindControl("labExpectPaymentPrice");
            if (labExpectPaymentPrice != null && labExpectPaymentPrice.Text != string.Empty)
                labExpectPaymentPrice.Text = Convert.ToDecimal(labExpectPaymentPrice.Text).ToString("#,##0.00");
            Label lblPR = (Label)e.Row.FindControl("lblPR");
            if (model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.MediaPR && model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
                lblPR.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + model.PRID.ToString() + "'style='cursor: hand' target='_blank'>" + model.PRNo + "</a>";
            else
                lblPR.Text = model.PRNo;
            Label lblInvoice = (Label)e.Row.FindControl("lblInvoice");
            if (lblInvoice != null && lblInvoice.Text != string.Empty)
                if (model.IsInvoice != null)
                {
                    if (model.IsInvoice.Value == 1)
                        lblInvoice.Text = "已开";
                    else if (model.IsInvoice.Value == 0)
                        lblInvoice.Text = "未开";
                    else
                        lblInvoice.Text = "无需发票";
                }

            Label lblStatusName = (Label)e.Row.FindControl("lblStatusName");
            HiddenField hidStatusNameID = (HiddenField)e.Row.FindControl("hidStatusNameID");
            if (lblStatusName != null && hidStatusNameID != null && hidStatusNameID.Value != string.Empty)
                lblStatusName.Text = ReturnPaymentType.ReturnStatusString(Convert.ToInt32(hidStatusNameID.Value), 0, model.IsDiscount);
            LinkButton lnkAttach = (LinkButton)e.Row.FindControl("lnkAttach");
            if (!string.IsNullOrEmpty(model.MediaOrderIDs) && (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA))
            {
                lnkAttach.Text = "<img src='/images/PrintDefault.gif' title='导出Excel' border='0'>";
            }
            else
            {
                lnkAttach.Text = "";
            }
            Label lblAttach = (Label)e.Row.FindControl("lblAttach");
            if (!string.IsNullOrEmpty(model.MediaOrderIDs) && (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA))
            {
                lblAttach.Text = "<a href='" + ESP.Finance.Configuration.ConfigurationManager.PurchaseServer + "Purchase\\Requisition\\Print\\MediaPrint.aspx?OrderID=" + model.MediaOrderIDs + "'style='cursor: hand' target='_blank'> <img title='附件' src='/images/PrintDefault.gif' border='0px;' /></a>";
            }
            else
            {
                lblAttach.Text = "";
            }

            HyperLink hylPrint = (HyperLink)e.Row.FindControl("hylPrint");
            hylPrint.Target = "_blank";
            hylPrint.NavigateUrl = "Print/PaymantPrint.aspx?" + RequestName.ReturnID + "=" + model.ReturnID.ToString();
            if (relationList != null && relationList.Count > 0)
            {
                hylPrint.Visible = false;
            }

            if (model.NeedPurchaseAudit == true)
            {
                hylPrint.Visible = false;
            }
            Label lblName = (Label)e.Row.FindControl("lblName");
            lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(model.RequestorID.Value) + "');");
        }
    }

    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ESP.Finance.Entity.ReturnInfo model = (ReturnInfo)e.Row.DataItem;
            IList<ESP.Finance.Entity.PNBatchRelationInfo> relationList = ESP.Finance.BusinessLogic.PNBatchRelationManager.GetList(" ReturnID=" + model.ReturnID.ToString(), null);

            Label labExpectPaymentPrice = (Label)e.Row.FindControl("labExpectPaymentPrice");
            if (labExpectPaymentPrice != null && labExpectPaymentPrice.Text != string.Empty)
                labExpectPaymentPrice.Text = Convert.ToDecimal(labExpectPaymentPrice.Text).ToString("#,##0.00");
            Label lblInvoice = (Label)e.Row.FindControl("lblInvoice");
            if (lblInvoice != null)
                if (model.IsInvoice != null)
                {
                    if (model.IsInvoice.Value == 1)
                        lblInvoice.Text = "已开";
                    else if (model.IsInvoice.Value == 0)
                        lblInvoice.Text = "未开";
                    else
                        lblInvoice.Text = "无需发票";
                }

            Label lblPR = (Label)e.Row.FindControl("lblPR");
            if (model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.MediaPR && model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
                lblPR.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + model.PRID.ToString() + "'style='cursor: hand' target='_blank'>" + model.PRNo + "</a>";
            else
                lblPR.Text = model.PRNo;
            Label lblStatusName = (Label)e.Row.FindControl("lblStatusName");
            HiddenField hidStatusNameID = (HiddenField)e.Row.FindControl("hidStatusNameID");
            if (lblStatusName != null && hidStatusNameID != null && hidStatusNameID.Value != string.Empty)
                lblStatusName.Text = ReturnPaymentType.ReturnStatusString(Convert.ToInt32(hidStatusNameID.Value), 0, model.IsDiscount);
            LinkButton lnkAttach = (LinkButton)e.Row.FindControl("lnkAttach");
            if (!string.IsNullOrEmpty(model.MediaOrderIDs) && (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA))
            {
                lnkAttach.Text = "<img src='/images/PrintDefault.gif' title='导出Excel' border='0'>";
            }
            else
            {
                lnkAttach.Text = "";
            }
            Label lblAttach = (Label)e.Row.FindControl("lblAttach");
            if (!string.IsNullOrEmpty(model.MediaOrderIDs) && (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA))
            {
                lblAttach.Text = "<a href='" + ESP.Finance.Configuration.ConfigurationManager.PurchaseServer + "Purchase\\Requisition\\Print\\MediaPrint.aspx?OrderID=" + model.MediaOrderIDs + "'style='cursor: hand' target='_blank'> <img title='附件' src='/images/PrintDefault.gif' border='0px;' /></a>";
            }
            else
            {
                lblAttach.Text = "";
            }
            HyperLink hylAudit = (HyperLink)e.Row.FindControl("hylAudit");
            hylAudit.NavigateUrl = "PurchaseAduit.aspx?" + RequestName.ReturnID + "=" + model.ReturnID;
            HyperLink hylPrint = (HyperLink)e.Row.FindControl("hylPrint");
            hylPrint.Target = "_blank";
            hylPrint.NavigateUrl = "Print/PaymantPrint.aspx?" + RequestName.ReturnID + "=" + model.ReturnID.ToString();
            if (relationList != null && relationList.Count > 0)
            {
                hylPrint.Visible = false;
            }
            Label lblName = (Label)e.Row.FindControl("lblName");
            lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(model.RequestorID.Value) + "');");
            //如果申请单暂停，不能编辑付款申请
            if (model.PRID != null && model.PRID != 0 && ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(model.PRID.Value).InUse != (int)ESP.Purchase.Common.State.PRInUse.Use)
            {
                hylAudit.Visible = false;
            }
        }
    }

    protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Export")
        {
            int returnID = int.Parse(e.CommandArgument.ToString());
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnID);
            if (!string.IsNullOrEmpty(returnModel.MediaOrderIDs))
            {
                string filename;
                string serverpath = Server.MapPath(ESP.Finance.Configuration.ConfigurationManager.MediaOrderPath);
                ESP.Finance.BusinessLogic.ReturnManager.ExportMediaOrderExcel(CurrentUserID, returnModel, serverpath, out filename,false);
                if (!string.IsNullOrEmpty(filename))
                {
                    outExcel(serverpath + filename, filename, true);
                }
            }
            else
            {
                return;
            }
        }
    }
    protected void gvHist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Export")
        {
            int returnID = int.Parse(e.CommandArgument.ToString());
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnID);
            if (!string.IsNullOrEmpty(returnModel.MediaOrderIDs))
            {
                string filename;
                string serverpath = Server.MapPath(ESP.Finance.Configuration.ConfigurationManager.MediaOrderPath);
                ESP.Finance.BusinessLogic.ReturnManager.ExportMediaOrderExcel(CurrentUserID, returnModel, serverpath, out filename, false);
                if (!string.IsNullOrEmpty(filename))
                {
                    outExcel(serverpath + filename, filename, true);
                }
            }
            else
            {
                return;
            }
        }
    }


    private void outExcel(string pathandname, string filename, bool isDelete)
    {
        if (!File.Exists(pathandname))
            return;
        FileStream fin = File.OpenRead(pathandname);
        Response.AddHeader("Content-Disposition", "attachment;   filename=" + filename);
        Response.AddHeader("Connection", "Close");
        Response.AddHeader("Content-Transfer-Encoding", "binary");
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Length", fin.Length.ToString());

        byte[] buf = new byte[1024];
        while (true)
        {
            int length = fin.Read(buf, 0, buf.Length);
            if (length > 0)
                Response.OutputStream.Write(buf, 0, length);
            if (length < buf.Length)
                break;
        }
        fin.Close();
        Response.Flush();
        Response.Close();
        if (isDelete)
        {
            FileInfo finfo = new FileInfo(pathandname);
            finfo.Delete();
        }
    }
}