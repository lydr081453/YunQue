using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using System.IO;
using ESP.Framework.BusinessLogic;

namespace FinanceWeb.Refund
{
    public partial class RefundAuditList : ESP.Web.UI.PageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //AjaxPro.Utility.RegisterTypeForAjax(typeof(RefundAuditList));
            if (!IsPostBack && !grComplete.CausedCallback)
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

            var data = this.AuditTab.Refunds;

            var keyword = txtKey.Text.Trim();
            var branchCode = txtBranchCode.Text.Trim();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(x => ((x.PRNO != null && x.PRNO.Contains(keyword))
                    || (x.ProjectCode != null && x.ProjectCode.Contains(keyword))
                    || (x.RefundCode != null && x.RefundCode.Contains(keyword))
                    || (x.PRID.ToString() == keyword)
                    || (x.Amounts.ToString() == keyword)
                    || (x.RequestEmployeeName != null && x.RequestEmployeeName.Contains(keyword))
                    || (x.SupplierName != null && x.SupplierName.Contains(keyword))
                    ) && (x.Status != 140)
                    ).ToList();
            }
            if (!string.IsNullOrEmpty(branchCode))
            {
                data = data.Where(x => x.ProjectCode != null && x.ProjectCode.StartsWith(branchCode) && x.Status != 140).OrderBy(x => x.Status).ToList();
            }

            grComplete.DataSource = data;
            grComplete.DataBind();
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
            ESP.Finance.Entity.RefundInfo refundModel = (ESP.Finance.Entity.RefundInfo)e.DataItem;

            e.Item["StatusName"] = ReturnPaymentType.ReturnStatusString(refundModel.Status, 0, false);

            e.Item["Print"] = "<a target='_blank' href='RefundPrint.aspx?" + RequestName.RefundID + "=" + refundModel.Id + "'><img src='/images/Icon_Output.gif' border='0' ></img></a>";

            e.Item["AuditStatus"] = "<a href='/project/ProjectWorkFlow.aspx?Type=refund&FlowID=" + refundModel.Id + "' target='_blank'><img src='/images/AuditStatus.gif' border='0px;' title='审批状态'></img></a>";
            e.Item["RequestEmployeeName"] = "<a onclick=\"ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(refundModel.RequestorID) + "');\">" + e.Item["RequestEmployeeName"] + "</a>";

            e.Item["Audit"] = "<a href='" + string.Format(ESP.Finance.Utility.Common.RefundAuditUrl, refundModel.Id) + "&BackUrl=RefundAuditList.aspx'><img src='/images/Audit.gif' border='0px;' title='审批'></img></a>";
        }


        private List<int> GetDelegateUser()
        {
            var currentUserId = UserManager.GetCurrentUserID();
            List<int> users = new List<int>();
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegateList = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(currentUserId);
            users.AddRange(delegateList.Select(x => x.UserID));
            users.Add(currentUserId);
            return users;
        }

    }
}