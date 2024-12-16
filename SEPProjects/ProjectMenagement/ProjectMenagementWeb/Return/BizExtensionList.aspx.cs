using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

public partial class Return_BizExtensionList : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !this.grComplete.CausedCallback)
        {
            ListBind();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ListBind();
    }

    private void ListBind()
    {
        this.AuditTab.BindData();

        string term = string.Empty;
        string DelegateUsers = string.Empty;
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
        foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
        {
            DelegateUsers += model.UserID.ToString() + ",";
        }
        DelegateUsers = DelegateUsers.TrimEnd(',');
        term = "  PaymentExtensionStatus=" + (int)PaymentExtensionStatus.Save + " OR  PaymentExtensionStatus=" + (int)PaymentExtensionStatus.PrepareAudit;
        
        if (!string.IsNullOrEmpty(term))
        {
            if (!string.IsNullOrEmpty(this.txtKey.Text))
            {
                term += " and (PaymentCode like '%'+@Key+'%' or ProjectCode like '%'+@Key+'%' or PaymentContent like '%'+@Key+'%' )";
                SqlParameter p4 = new SqlParameter("@Key", SqlDbType.NVarChar, 50);
                p4.SqlValue = this.txtKey.Text.Trim();
            }
            IList<ESP.Finance.Entity.PaymentInfo> paymentList = ESP.Finance.BusinessLogic.PaymentManager.GetList(term, null);

            grComplete.DataSource = paymentList;
            grComplete.DataBind();
        }




        //this.AuditTab.BindData();

        //var list = this.AuditTab.Payments;
        //var keyword = this.txtKey.Text.Trim();

        //list = list.Where(x => (x.PaymentCode != null && x.PaymentCode.Contains(keyword))
        //    || (x.ProjectCode != null && x.ProjectCode.Contains(keyword))
        //    || (x.PaymentContent != null && x.PaymentContent.Contains(keyword))
        //    && x.PaymentExtensionStatus == (int)PaymentExtensionStatus.Save
        //    ).ToList();

        //grComplete.DataSource = list;
        //grComplete.DataBind();
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        grComplete.ItemDataBound += new ComponentArt.Web.UI.Grid.ItemDataBoundEventHandler(grComplete_ItemDataBound);
        grComplete.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(grComplete_NeedRebind);
        grComplete.PageIndexChanged += new ComponentArt.Web.UI.Grid.PageIndexChangedEventHandler(grComplete_PageIndexChanged);

    }

    void grComplete_NeedRebind(object sender, EventArgs e)
    {
        ListBind();
    }

    void grComplete_PageIndexChanged(object sender, ComponentArt.Web.UI.GridPageIndexChangedEventArgs e)
    {
        grComplete.CurrentPageIndex = e.NewIndex;
    }

    void grComplete_ItemDataBound(object sender, ComponentArt.Web.UI.GridItemDataBoundEventArgs e)
    {
        ESP.Finance.Entity.PaymentInfo paymentModel = (ESP.Finance.Entity.PaymentInfo)e.DataItem;
        ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(paymentModel.ProjectID);
        //e.Item["Responser"] = "<a onclick='ShowMsg(\"" + ESP.Web.UI.PageBase.GetUserInfo(projectModel.ApplicantUserID) + "\");'>" + projectModel.ApplicantEmployeeName + "</a>";
        e.Item["Responser"] = projectModel.ApplicantEmployeeName;
        switch (paymentModel.PaymentStatus)
        {
            case (int)ReturnStatus.Save:
                e.Item["PaymentStatusName"] = "已保存";
                break;
            case (int)ReturnStatus.Submit:
                e.Item["PaymentStatusName"] = "已提交";
                break;
            case (int)ReturnStatus.MajorCommit:
                e.Item["PaymentStatusName"] = "总监确认";
                break;
            case (int)ReturnStatus.FinancialHold:
                e.Item["PaymentStatusName"] = "已挂账";
                break;
            case (int)ReturnStatus.FinancialOver:
                e.Item["PaymentStatusName"] = "已付款";
                break;
            default:
                e.Item["PaymentStatusName"] = "";
                break;
        }
        e.Item["Print"] = "<a href='/Return/NotificationPrint.aspx?" + ESP.Finance.Utility.RequestName.PaymentID + "=" + paymentModel.PaymentID + "' target='_blank'><img title=' 付款通知打印预览' src='/images/PrintDefault.gif' border='0px;' /></a>";
        e.Item["AuditStatus"] = "<a href='/project/ProjectWorkFlow.aspx?Type=payment&FlowID=" + paymentModel.PaymentID + "' target='_blank'><img src='/images/AuditStatus.gif' border='0px;' title='审批状态'/></a>";
        if (paymentModel.PaymentStatus == 1)
        {
            e.Item["Audit"] = "<a href='" + string.Format(ESP.Finance.Utility.Common.NotifyBizUrl, paymentModel.PaymentID) + "&BackUrl=BizAuditList.aspx'><img src='/images/Audit.gif' border='0px;' title='审批' /></a>"; ;
            e.Item["InvoiceSign"] = "";
        }
        else if (paymentModel.PaymentStatus == 2)
        {
            e.Item["Audit"] = "<a href='" + string.Format(ESP.Finance.Utility.Common.NotifyFinanceUrl, paymentModel.PaymentID) + "&BackUrl=BizAuditList.aspx'><img src='/images/Audit.gif' border='0px;' title='审批' /></a>"; ;
            e.Item["InvoiceSign"] = "";
        }
        else if (paymentModel.PaymentStatus > 2)
        {
            e.Item["Audit"] = "";
            e.Item["InvoiceSign"] = "<a href='" + string.Format(ESP.Finance.Utility.Common.NotifyFinanceUrl, paymentModel.PaymentID) + "&BackUrl=BizAuditList.aspx'><img src='/images/Audit.gif' border='0px;' title='审批' /></a>"; ;
        }
    }

    //private string GetDelegateUser()
    //{
    //    string users = string.Empty;
    //    IList<ESP.Framework.Entity.AuditBackUpInfo> delegateList = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(int.Parse(CurrentUser.SysID));
    //    foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegateList)
    //    {
    //        users += model.UserID.ToString() + ",";
    //    }
    //    users += CurrentUser.SysID;
    //    return users;
    //}
}








