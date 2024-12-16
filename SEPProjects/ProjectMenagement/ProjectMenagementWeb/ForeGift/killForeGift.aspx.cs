using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ESP.Finance.Utility;

public partial class ForeGift_killForeGift : ESP.Web.UI.PageBase
{
    int returnId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ReturnID]))
        {
            returnId = int.Parse(Request[ESP.Finance.Utility.RequestName.ReturnID]);
        }
        if (!IsPostBack)
        {
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
            ViewForeGift.BindInfo(returnModel);
            lblLog.Text= GetAuditLog(returnModel);
            // listBind(returnModel);
            // logBind(returnModel);
        }
    }

    /// <summary>
    /// 绑定抵押金付款申请列表
    /// </summary>
    private void listBind(ESP.Finance.Entity.ReturnInfo returnModel)
    {
        if (returnModel == null)
            returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
        string terms = " projectCode=@projectCode and supplierName=@supplierName and returnType=@returnType and returnStatus=@returnStatus";
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@projectCode", returnModel.ProjectCode));
        parms.Add(new SqlParameter("@supplierName", returnModel.SupplierName));
        parms.Add(new SqlParameter("@returnType", (int)ESP.Purchase.Common.PRTYpe.PN_KillForeGift));
        parms.Add(new SqlParameter("@returnStatus", (int)ESP.Finance.Utility.PaymentStatus.WaitReceiving));
        IList<ESP.Finance.Entity.ReturnInfo> returnList = ESP.Finance.BusinessLogic.ReturnManager.GetList(terms, parms);
        gvG.DataSource = returnList;
        gvG.DataBind();
    }

    /// <summary>
    /// 绑定已抵押金列表
    /// </summary>
    private void logBind(ESP.Finance.Entity.ReturnInfo returnModel)
    {
        //if (returnModel == null)
        //    returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
        //string terms = " and foregiftReturnId=@returnId";
        //List<SqlParameter> parms = new List<SqlParameter>();
        //parms.Add(new SqlParameter("@returnId", returnModel.ReturnID));
        //gvLog.DataSource = ESP.Finance.BusinessLogic.ForeGiftLinkManager.GetKillList(terms,parms);
        //gvLog.DataBind();
    }

    private string GetAuditLog(ESP.Finance.Entity.ReturnInfo ReturnModel)
    {
        int prId = ReturnModel.PRID ?? 0;
        ESP.Purchase.Entity.GeneralInfo pr = prId > 0 ? ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(prId) : null;
        IList<ESP.Purchase.Entity.AuditLogInfo> oploglist = null;

        if (pr != null)
        {
            if (pr.ValueLevel == 1)
            {
                oploglist = ESP.Purchase.BusinessLogic.AuditLogManager.GetModelListByGID(pr.id);
            }
        }

        IList<ESP.Finance.Entity.AuditLogInfo> histList = ESP.Finance.BusinessLogic.AuditLogManager.GetReturnList(ReturnModel.ReturnID);

        System.Text.StringBuilder log = new System.Text.StringBuilder();

        if (oploglist != null && oploglist.Count > 0)
        {
            foreach (var l in oploglist)
            {
                log.AppendFormat("{0:yyyy/MM/dd hh:mm:ss}", l.remarkDate).Append(" ")
                    .Append(l.auditUserName)
                    .Append(ESP.Purchase.Common.State.operationAudit_statusName[l.auditType]).Append(" ")
                    .Append(l.remark).Append("<br/>");
            }
        }

        foreach (var l in histList)
        {
            string austatus = string.Empty;
            if (l.AuditStatus == (int)AuditHistoryStatus.PassAuditing)
            {
                austatus = "审批通过";
            }
            else if (l.AuditStatus == (int)AuditHistoryStatus.TerminateAuditing)
            {
                austatus = "审批驳回";
            }

            log.AppendFormat("{0:yyyy/MM/dd hh:mm:ss}", l.AuditDate).Append(" ")
                .Append(l.AuditorEmployeeName).Append("(").Append(l.AuditorUserName).Append(") ")
                .Append(austatus).Append(" ")
                .Append(l.Suggestion).Append("<br/>");
        }

        return log.ToString();
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        int status = 136;
        ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
        ESP.Finance.Entity.ForeGiftLinkInfo foregiftModel = new ESP.Finance.Entity.ForeGiftLinkInfo();
        if (returnModel.ReturnStatus == 136)
            status = 137;
        else if (returnModel.ReturnStatus == 137)
        {
            status = 138;
        }
        else if (returnModel.ReturnStatus == 138)
        {
            status = 140;
        }
        //存在抵消押金的PN
        if (txtPrice.Text.Trim() != "")
        {
            //线下抵消押金
            
            foregiftModel.foregiftPrId = returnModel.PRID.Value;
            foregiftModel.foregiftReturnId = returnModel.ReturnID;
            foregiftModel.killforegiftPrId = 0;
            foregiftModel.killforegiftReturnId = 0;
            foregiftModel.linker = int.Parse(CurrentUser.SysID);
            foregiftModel.linkDate = DateTime.Now;
            foregiftModel.killPrice = decimal.Parse(txtPrice.Text.Trim());

        }
        if (foregiftModel.killPrice == 0 || foregiftModel.killPrice != returnModel.PreFee.Value)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('抵消金额不等于押金金额，不能进行抵消押金！');", true);
        }
        else
        {
            if (ESP.Finance.BusinessLogic.ReturnManager.updataSatatusAndAddKillForegift(returnModel, foregiftModel, status))
            {
                ESP.Finance.Entity.AuditLogInfo audit = new ESP.Finance.Entity.AuditLogInfo();
                audit.FormID = returnModel.ReturnID;
                audit.Suggestion = this.txtKillForegiftRemark.Text;
                audit.AuditDate = DateTime.Now;
                audit.AuditorSysID = int.Parse(CurrentUser.SysID);
                audit.AuditorUserCode = CurrentUser.ID;
                audit.AuditorEmployeeName = CurrentUser.Name;
                audit.AuditorUserName = CurrentUser.ITCode;
                audit.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing;
                audit.FormType = (int)ESP.Finance.Utility.FormType.Return;
                int ret = ESP.Finance.BusinessLogic.AuditLogManager.Add(audit);

                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('已成功抵消押金！');window.location='/Edit/ReturnTabEdit.aspx';", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('抵消押金失败！');", true);
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Edit/ReturnTabEdit.aspx");
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
            ESP.Finance.Entity.ReturnInfo model = (ESP.Finance.Entity.ReturnInfo)e.Row.DataItem;

            Label labExpectPaymentPrice = (Label)e.Row.FindControl("labExpectPaymentPrice");
            if (labExpectPaymentPrice != null && labExpectPaymentPrice.Text != string.Empty)
                labExpectPaymentPrice.Text = Convert.ToDecimal(labExpectPaymentPrice.Text).ToString("#,##0.00");
            Label lblPR = (Label)e.Row.FindControl("lblPR");
            lblPR.Text = model.PRNo;
            Label lblBeginDate = (Label)e.Row.FindControl("lblBeginDate");
            if (lblBeginDate != null && lblBeginDate.Text != string.Empty)
                lblBeginDate.Text = Convert.ToDateTime(lblBeginDate.Text).ToString("yyyy-MM-dd");
            Label lblEndDate = (Label)e.Row.FindControl("lblEndDate");
            if (lblEndDate != null && lblEndDate.Text != string.Empty)
                lblEndDate.Text = Convert.ToDateTime(lblEndDate.Text).ToString("yyyy-MM-dd");

            Label lblStatusName = (Label)e.Row.FindControl("lblStatusName");
            HiddenField hidStatusNameID = (HiddenField)e.Row.FindControl("hidStatusNameID");
            if (lblStatusName != null && hidStatusNameID != null && hidStatusNameID.Value != string.Empty)
                lblStatusName.Text = ReturnPaymentType.ReturnStatusString(Convert.ToInt32(hidStatusNameID.Value), 0, model.IsDiscount);


            Label lblName = (Label)e.Row.FindControl("lblName");
            lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(model.RequestorID.Value) + "');");
        }
    }

    protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvG.PageIndex = e.NewPageIndex;
        listBind(null);
    }

    protected void gvLog_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //gvLog.PageIndex = e.NewPageIndex;
        //logBind(null);
    }
}
