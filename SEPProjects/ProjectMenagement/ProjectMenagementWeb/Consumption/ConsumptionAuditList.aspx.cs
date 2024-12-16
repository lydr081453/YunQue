using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

namespace FinanceWeb.Consumption
{
    public partial class ConsumptionAuditList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !this.grComplete.CausedCallback)
            {
                ListBind();
            }
        }

        private void ListBind()
        {
            this.AuditTab.BindData();
            var data = this.AuditTab.Consumptions;

            var keyword = txtKey.Text.Trim();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(x =>
                    (x.PurchaseBatchCode != null && x.PurchaseBatchCode.Contains(keyword))
                    || (x.Description != null && x.Description.Contains(keyword))
                    || (x.Creator != null && x.Creator.Contains(keyword))
                    || (x.Amounts != null && x.Amounts.ToString().Contains(keyword))
                    || (x.PaymentEmployeeName != null && x.PaymentEmployeeName.Contains(keyword))
                ).ToList();
            }

            grComplete.DataSource = data; 
            grComplete.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListBind();
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
            e.Item["Status"] = ReturnPaymentType.ReturnStatusString(int.Parse(e.Item["Status"].ToString()), 0, false);

            e.Item["Creator"] = "<a onclick=\"ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(int.Parse(e.Item["CreatorID"].ToString())) + "');\">" + e.Item["Creator"] + "</a>";
            e.Item["AuditStatus"] = "<a href='/project/ProjectWorkFlow.aspx?Type=consumption&FlowID=" + e.Item["BatchID"] + "' target='_blank'><img src='/images/AuditStatus.gif' border='0px;' title='审批状态'/></a>";
            e.Item["AuditLink"] = "<a href='" + string.Format("ConsumptionAudit.aspx?BatchId={0}", e.Item["BatchID"]) + "&BackUrl=ConsumptionAuditList.aspx'><img src='/images/Audit.gif' border='0px' /></a>";
            e.Item["PrintLink"] = "<a target='_blank' href='" + string.Format("ConsumptionPrint.aspx?BatchId={0}", e.Item["BatchID"]) + "'><img src='/images/PrintDefault.gif' border='0px' /></a>";

            if (e.Item["PeriodID"]!=null && e.Item["PeriodID"].ToString() == "1")
            {
                e.Item["Adj"] = "调整";
            }
            else
                e.Item["Adj"] = "导入";
        }

        private string GetDelegateUser()
        {
            string users = string.Empty;
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegateList = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(int.Parse(CurrentUser.SysID));
            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegateList)
            {
                users += model.UserID.ToString() + ",";
            }
            users += CurrentUser.SysID;
            return users;
        }

    }
}