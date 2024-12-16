using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
using System.Data.SqlClient;
using ESP.Finance.Utility;
using WorkFlowModel;
using WorkFlowImpl;

public partial class Purchase_ManagerAuditList : ESP.Web.UI.PageBase
{
    WorkFlowImpl.WorkItemData workitemdata = new WorkFlowImpl.WorkItemData();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IList<ESP.Finance.Entity.ReturnInfo> list = Search();
            SearchHist(list);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        IList<ESP.Finance.Entity.ReturnInfo> list = Search();
        SearchHist(list);
    }

    protected void btnSearchAll_Click(object sender, EventArgs e)
    {
        this.txtKey.Text = string.Empty;
        this.ddlStatus.SelectedIndex = 0;
        this.txtBeginDate.Text = string.Empty;
        this.txtEndDate.Text = string.Empty;
        IList<ESP.Finance.Entity.ReturnInfo> list = Search();
        SearchHist(list);
    }

    private List<ESP.Finance.Entity.ReturnInfo> Search()
    {
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
        List<WorkFlowModel.WorkItemData> list1 = workitemdata.getProcessDataList(CurrentUser.SysID, "PN付款");
        List<ESP.Finance.Entity.ReturnInfo> list = new List<ReturnInfo>();
        List<WorkFlowModel.WorkItemData> list2 = new List<WorkFlowModel.WorkItemData>();
        List<WorkFlowModel.WorkItemData> listDelegate = null;
        foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
        {
            listDelegate = workitemdata.getProcessDataList(model.UserID.ToString(), "PN付款");
            foreach (WorkFlowModel.WorkItemData o in listDelegate)
            {
                list2.Add(o);
            }
        }
        foreach (WorkFlowModel.WorkItemData o in list1)
        {
            ESP.Finance.Entity.ReturnInfo model = (ESP.Finance.Entity.ReturnInfo)o.ItemData;
            model.WorkItemID = o.WorkItemID;
            model.InstanceID = o.InstanceID;
            model.WorkItemName = o.WorkItemName;
            model.ProcessID = o.ProcessID;

            list.Add(model);
        }
        if (list2 != null && list2.Count > 0)
        {
            foreach (WorkFlowModel.WorkItemData o in list2)
            {
                ESP.Finance.Entity.ReturnInfo model = (ESP.Finance.Entity.ReturnInfo)o.ItemData;
                model.WorkItemID = o.WorkItemID;
                model.WorkItemName = o.WorkItemName;
                model.ProcessID = o.ProcessID;
                model.InstanceID = o.InstanceID;
                list.Add(model);
            }
        }
        IList<ESP.Finance.Entity.ReturnInfo> privateReturnList = SearchPrivateOrder();
        foreach (ESP.Finance.Entity.ReturnInfo m in privateReturnList)
        {
            list.Add(m);
        }
        //var tmplist = list.OrderBy(N => N.PreBeginDate);
        var tmplist = from item in list
                      where item.ReturnCode.Contains(this.txtKey.Text) ||
                                item.ProjectCode.Contains(this.txtKey.Text) ||
                                item.RequestEmployeeName.Contains(this.txtKey.Text) ||
                                item.PreFee.ToString().Contains(this.txtKey.Text) || (item.PRNo!=null && item.PRNo.Contains(this.txtKey.Text))
                      orderby item.PreBeginDate
                      select item;
        this.gvG.DataSource = tmplist.ToList();
        this.gvG.DataBind();
        return tmplist.ToList();
    }

    private IList<ESP.Finance.Entity.ReturnInfo> SearchPrivateOrder()
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
        if (!string.IsNullOrEmpty(DelegateUsers))
            term = " (ReturnStatus=@status1) AND ReturnType=@ReturnType AND (PaymentUserID=@sysID or PaymentUserID in(" + DelegateUsers + "))";
        else
            term = " (ReturnStatus=@status1 ) AND ReturnType=@ReturnType AND PaymentUserID=@sysID";

        SqlParameter p1 = new SqlParameter("@status1", System.Data.SqlDbType.Int, 4);
        p1.SqlValue = (int)PaymentStatus.Submit;
        paramlist.Add(p1);
        SqlParameter p3 = new SqlParameter("@ReturnType", System.Data.SqlDbType.Int, 4);
        p3.SqlValue = (int)ESP.Purchase.Common.PRTYpe.PrivatePR;
        paramlist.Add(p3);
        SqlParameter p5 = new SqlParameter("@sysID", System.Data.SqlDbType.Int, 4);
        p5.SqlValue = CurrentUser.SysID;
        paramlist.Add(p5);

        IList<ESP.Finance.Entity.ReturnInfo> returnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(term, paramlist);
        return returnList;
    }

    private void SearchHist(IList<ESP.Finance.Entity.ReturnInfo> returnList)
    {
        IList<ESP.Finance.Entity.ReturnInfo> list;
        List<SqlParameter> paramlist = new List<SqlParameter>();
        string term = string.Empty;
        string auditingReturnIds = string.Empty;
        string auditingSelected = string.Empty;
        string DelegateUsers = string.Empty;
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
        foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
        {
            DelegateUsers += model.UserID.ToString() + ",";
        }
        DelegateUsers = DelegateUsers.TrimEnd(',');

        foreach (ESP.Finance.Entity.ReturnInfo model in returnList)
        {
            auditingReturnIds += model.ReturnID.ToString() + ",";
        }
        auditingReturnIds = auditingReturnIds.TrimEnd(',');
        if (!string.IsNullOrEmpty(DelegateUsers))
        {
            DelegateUsers += "," + CurrentUser.SysID;
        }
        else
            DelegateUsers = CurrentUser.SysID;
        
        if (!string.IsNullOrEmpty(auditingReturnIds))
        {
            auditingSelected = " and returnid not in(" + auditingReturnIds + ")";
        }
        term = " (returnid in(select distinct formid from f_auditlog  where auditorsysid in(" + DelegateUsers + ") and formtype=@formtype)" + auditingSelected + ")";
        SqlParameter p2 = new SqlParameter("@formtype", SqlDbType.Int, 4);
        p2.Value = (int)ESP.Finance.Utility.FormType.Return;
        paramlist.Add(p2);
        if (!string.IsNullOrEmpty(term))
        {
            if (this.txtKey.Text.Trim() != string.Empty)
            {
                term += "  and (PRNO like '%'+@prno+'%' or ProjectCode like '%'+@prno+'%' or returncode like '%'+@prno+'%' or prid like '%'+@prno+'%' or prefee like  '%'+@prno+'%'  or prefee  like '%'+@prno+'%' or RequestEmployeeName  like '%'+@prno+'%' or SupplierName  like '%'+@prno+'%' )";
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
            list = ESP.Finance.BusinessLogic.ReturnManager.GetList(term, paramlist);
            var tmplist = list.OrderBy(N => N.LastUpdateDateTime);
            this.gvHist.DataSource = tmplist.ToList();
            this.gvHist.DataBind();
        }
    }

    protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvG.PageIndex = e.NewPageIndex;
        Search();
    }

    protected void gvHist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHist.PageIndex = e.NewPageIndex;
        IList<ESP.Finance.Entity.ReturnInfo> list = Search();
        SearchHist(list);
    }

    protected void gvHist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ESP.Finance.Entity.ReturnInfo model = (ESP.Finance.Entity.ReturnInfo)e.Row.DataItem;
            Label labExpectPaymentPrice = (Label)e.Row.FindControl("labExpectPaymentPrice");
            if (labExpectPaymentPrice != null && labExpectPaymentPrice.Text != string.Empty)
                labExpectPaymentPrice.Text = Convert.ToDecimal(labExpectPaymentPrice.Text).ToString("#,##0.00");
            Label lblPR = (Label)e.Row.FindControl("lblPR");
            if (model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.MediaPR && model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
                lblPR.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + model.PRID.ToString() + "'style='cursor: hand' target='_blank'>" + model.PRNo + "</a>";
            else
                lblPR.Text = model.PRNo;
            Label lblSupplier = (Label)e.Row.FindControl("lblSupplier");
            lblSupplier.Text = model.SupplierName;
            LinkButton lnkAttach = (LinkButton)e.Row.FindControl("lnkAttach");
            if (lnkAttach != null)
            {
                if (!string.IsNullOrEmpty(model.MediaOrderIDs) && (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA))
                {
                    lnkAttach.Text = "<img src='/images/PrintDefault.gif' title='导出Excel' border='0'>";
                }
                else
                {
                    lnkAttach.Text = "";
                }
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
            //3000以下对私的单子有附件显示
            if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
            {
                lblAttach.Visible = true;
                lblAttach.Text = "<a href='" + ESP.Finance.Configuration.ConfigurationManager.PurchaseServer + "Purchase\\Requisition\\Print\\RequisitionPrint.aspx?id=" + model.PRID.ToString() + "&viewButton=no&Action=ViewOldPr' style='cursor: hand' target='_blank'> <img title='附件' src='/images/PrintDefault.gif' border='0px;' /></a>";
            }
            Label lblName = (Label)e.Row.FindControl("lblName");

            lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(model.RequestorID.Value) + "');");

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
    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ESP.Finance.Entity.ReturnInfo model = (ESP.Finance.Entity.ReturnInfo)e.Row.DataItem;
            Label labExpectPaymentPrice = (Label)e.Row.FindControl("labExpectPaymentPrice");
            if (labExpectPaymentPrice != null && labExpectPaymentPrice.Text != string.Empty)
                labExpectPaymentPrice.Text = Convert.ToDecimal(labExpectPaymentPrice.Text).ToString("#,##0.00");

            HyperLink hylAudit = (HyperLink)e.Row.FindControl("hylAudit");
            if (model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PN_ForeGift)
            {
                hylAudit.NavigateUrl = "PaymentEdit.aspx?" + RequestName.ReturnID + "=" + gvG.DataKeys[e.Row.RowIndex].Value.ToString();
            }
            else
            {
                hylAudit.NavigateUrl = "/ForeGift/operationAudit.aspx?" + RequestName.ReturnID + "=" + gvG.DataKeys[e.Row.RowIndex].Value.ToString();
            }
            Label lblPR = (Label)e.Row.FindControl("lblPR");
            if (model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.MediaPR && model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
                lblPR.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + model.PRID.ToString() + "'style='cursor: hand' target='_blank'>" + model.PRNo + "</a>";
            else
                lblPR.Text = model.PRNo;
            Label lblSupplier = (Label)e.Row.FindControl("lblSupplier");
            lblSupplier.Text = model.SupplierName;
            LinkButton lnkAttach = (LinkButton)e.Row.FindControl("lnkAttach");
            if (lnkAttach != null)
            {
                if (!string.IsNullOrEmpty(model.MediaOrderIDs) && (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA))
                {
                    lnkAttach.Text = "<img src='/images/PrintDefault.gif' title='导出Excel' border='0'>";
                }
                else
                {
                    lnkAttach.Text = "";
                }
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
            //3000以下对私的单子有附件显示
            if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
            {
                lblAttach.Visible = true;
                lblAttach.Text = "<a href='" + ESP.Finance.Configuration.ConfigurationManager.PurchaseServer + "Purchase\\Requisition\\Print\\RequisitionPrint.aspx?id=" + model.PRID.ToString() + "&viewButton=no&Action=ViewOldPr' style='cursor: hand' target='_blank'> <img title='附件' src='/images/PrintDefault.gif' border='0px;' /></a>";
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
}
