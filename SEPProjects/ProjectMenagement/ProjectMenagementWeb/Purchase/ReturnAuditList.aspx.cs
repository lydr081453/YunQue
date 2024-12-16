using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using System.IO;
using ESP.Framework.BusinessLogic;

namespace FinanceWeb.Purchase
{
    public partial class ReturnAuditList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ReturnAuditList));
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

            var data = this.AuditTab.Returns;

            var keyword = txtKey.Text.Trim();
            var branchCode = txtBranchCode.Text.Trim();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(x => ((x.PRNo != null && x.PRNo.Contains(keyword))
                    || (x.ProjectCode != null && x.ProjectCode.Contains(keyword))
                    || (x.ReturnCode != null && x.ReturnCode.Contains(keyword))
                    || (x.PRID.ToString() == keyword)
                    || (x.PreFee.ToString() == keyword)
                    || (x.RequestEmployeeName != null && x.RequestEmployeeName.Contains(keyword))
                    || (x.SupplierName != null && x.SupplierName.Contains(keyword))
                    ) && (x.ReturnStatus != 140)
                    ).ToList();
            }
            if (!string.IsNullOrEmpty(branchCode))
            {
                data = data.Where(x => x.ProjectCode != null && x.ProjectCode.StartsWith(branchCode) && x.ReturnStatus!=140).OrderBy(x=>x.ReturnStatus).ToList();
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
            ESP.Finance.Entity.ReturnInfo returnModel = (ESP.Finance.Entity.ReturnInfo)e.DataItem;
            if (returnModel.IsInvoice != null)
            {
                if (returnModel.IsInvoice == 1)
                    e.Item["IsInvoice"] = "已开";
                else if (returnModel.IsInvoice == 0)
                    e.Item["IsInvoice"] = "未开";
                else
                    e.Item["IsInvoice"] = "无需发票";
            }
            e.Item["ReturnStatus"] = ReturnPaymentType.ReturnStatusString(returnModel.ReturnStatus ?? 0,0,returnModel.IsDiscount);
            //媒体稿费的单子
            if (!string.IsNullOrEmpty(returnModel.MediaOrderIDs) && (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA))
            {
                e.Item["Attach"] = "<a href='" + ESP.Finance.Configuration.ConfigurationManager.PurchaseServer + "Purchase\\Requisition\\Print\\MediaPrint.aspx?OrderID=" + returnModel.MediaOrderIDs + "' style='cursor: hand' target='_blank'> <img title='附件' src='/images/PrintDefault.gif' border='0px;'></img></a>";
                e.Item["Attach"] += "<a href='Print\\MediaUnPayment.aspx?OrderID=" + returnModel.MediaOrderIDs + "' style='cursor: hand' target='_blank'> <img title='未付款记者浏览' src='/images/PrintDefault.gif' border='0px;' ></img></a>";
            }
            else
            {
                e.Item["Attach"] = "";
            }
            //3000以下对私的单子有附件显示
            if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
            {
                e.Item["Attach"] = "<a href='" + ESP.Finance.Configuration.ConfigurationManager.PurchaseServer + "Purchase\\Requisition\\Print\\RequisitionPrint.aspx?id=" + returnModel.PRID.ToString() + "&viewButton=no&Action=ViewOldPr' style='cursor: hand' target='_blank'> <img title='附件' src='/images/PrintDefault.gif' border='0px;'></img></a>";
            }
            if (!string.IsNullOrEmpty(returnModel.MediaOrderIDs) && (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA))
            {
                e.Item["Export"] = "<img src='/images/PrintDefault.gif' style='cursor:pointer'  onclick=\"window.open('/Dialogs/ExportFile.aspx?returnID=" + returnModel.ReturnID + "&commandName=Export');\" title='导出所有记者' border='0' />";
                e.Item["Export"] += "&nbsp;<img src='/images/PrintDefault.gif' style='cursor:pointer'  onclick=\"window.open('/Dialogs/ExportFile.aspx?returnID=" + returnModel.ReturnID + "&commandName=Journalist');\" title='导出未付款记者' border='0' />";
            }
            else
            {
                e.Item["Export"] = "";
            }
            if (returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.MediaPR && returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
                e.Item["PRNO"] = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + returnModel.PRID.ToString() + "' style='cursor: hand' target='_blank'>" + returnModel.PRNo + "</a>";
            else
                e.Item["PRNO"] = returnModel.PRNo;
            if (returnModel.NeedPurchaseAudit == true)
            {
                e.Item["Print"] = "";
            }
            else
            {
                e.Item["Print"] = "<a target='_blank' href='Print/PaymantPrint.aspx?" + RequestName.ReturnID + "=" + returnModel.ReturnID + "'><img src='/images/Icon_Output.gif' border='0' ></img></a>";
            }
            e.Item["AuditStatus"] = "<a href='/project/ProjectWorkFlow.aspx?Type=return&FlowID=" + returnModel.ReturnID + "' target='_blank'><img src='/images/AuditStatus.gif' border='0px;' title='审批状态'></img></a>";
            e.Item["RequestEmployeeName"] = "<a onclick=\"ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(returnModel.RequestorID.Value) + "');\">" + e.Item["RequestEmployeeName"] + "</a>";
            //if (returnModel.ReturnStatus.Value == (int)PaymentStatus.FinanceComplete)
            //{
            //    e.Item["Audit"] = "";
            //}
            //else
            //{
            //    if (returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PN_ForeGift)
            //    {
            //        if (returnModel.ReturnStatus == (int)PaymentStatus.FinanceWaitCheck && returnModel.PaymentTypeID == 1)
            //        {
            //            hylAudit.NavigateUrl = "CashPNLink.aspx?" + RequestName.ReturnID + "=" + gvG.DataKeys[e.Row.RowIndex].Value.ToString();
            //        }
            //        else
            //        {
            //            hylAudit.NavigateUrl = "FinancialAudit.aspx?" + RequestName.ReturnID + "=" + returnModel.ReturnID.ToString();
            //        }
            //    }
            //    else
            //    {
            //        hylAudit.NavigateUrl = "/ForeGift/financeAudit.aspx?" + RequestName.ReturnID + "=" + gvG.DataKeys[e.Row.RowIndex].Value.ToString();
            //    }
            //}
            if (returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PN_ForeGift)
            {
                if (returnModel.ReturnStatus < 100)
                {
                    e.Item["Audit"] = "<a href='" + string.Format(ESP.Finance.Utility.Common.PaymentBizUrl, returnModel.ReturnID) + "&BackUrl=ReturnAuditList.aspx'><img src='/images/Audit.gif' border='0px;' title='审批'></img></a>";

                }
                else
                {
                    e.Item["Audit"] = "<a href='" + string.Format(ESP.Finance.Utility.Common.PaymentFinanceUrl, returnModel.ReturnID) + "&BackUrl=ReturnAuditList.aspx'><img src='/images/Audit.gif' border='0px;' title='审批'></img></a>";
                }
            }
            else
            {
                if (returnModel.ReturnStatus < 100)
                {
                    e.Item["Audit"] = "<a href='" + string.Format(ESP.Finance.Utility.Common.ForgiftBizUrl, returnModel.ReturnID) + "&BackUrl=ReturnAuditList.aspx'><img src='/images/Audit.gif' border='0px;' title='审批'></img></a>";

                }
                else
                {
                    e.Item["Audit"] = "<a href='" + string.Format(ESP.Finance.Utility.Common.ForgiftFinanceUrl, returnModel.ReturnID) + "&BackUrl=ReturnAuditList.aspx'><img src='/images/Audit.gif' border='0px;' title='审批'></img></a>";

                }
            }
        }


        private List<int> GetDelegateUser()
        {
            var currentUserId = UserManager.GetCurrentUserID();
            List<int> users = new List<int>();
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegateList = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(currentUserId);
            users.AddRange(delegateList.Select(x=>x.UserID));
            users.Add(currentUserId);
            return users;
        }
    }
}
