using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

namespace FinanceWeb.Return
{
    public partial class BizAuditList : ESP.Web.UI.PageBase
    {
        IList<ESP.Finance.Entity.ProjectInfo> projectList = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            projectList = ESP.Finance.BusinessLogic.ProjectManager.GetList(" and status=32");
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
            //string terms = "";
            //List<System.Data.SqlClient.SqlParameter> parms = new List<System.Data.SqlClient.SqlParameter>();
            //if (txtKey.Text.Trim() != "")
            //{
            //    terms += " and (PaymentCode like '%'+@Key+'%' or ProjectCode like '%'+@Key+'%' or PaymentContent like '%'+@Key+'%' )";
            //    parms.Add(new System.Data.SqlClient.SqlParameter("@Key", this.txtKey.Text.Trim()));
            //}

            this.AuditTab.BindData();

            var keyword = this.txtKey.Text.Trim();

            var list = this.AuditTab.Payments.Where(x =>
                (x.PaymentCode != null && x.PaymentCode.Contains(keyword))
                || (x.ProjectCode != null && x.ProjectCode.Contains(keyword))
                || (x.PaymentContent != null && x.PaymentContent.Contains(keyword))
                || (x.PaymentExtensionStatus == (int)PaymentExtensionStatus.Save)
                || (x.PaymentExtensionStatus == (int)PaymentExtensionStatus.PrepareAudit)
                ).ToList();

            grComplete.DataSource = list;// ESP.Finance.BusinessLogic.PaymentManager.GetWaitAuditList(GetDelegateUser(), terms, parms);
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
            ESP.Finance.Entity.PaymentInfo paymentModel = (ESP.Finance.Entity.PaymentInfo)e.DataItem;
            ESP.Finance.Entity.ProjectInfo projectModel =projectList.Where(x=>x.ProjectId==paymentModel.ProjectID).FirstOrDefault();
            if (projectModel == null)
                return;
            e.Item["Responser"] = projectModel.ApplicantEmployeeName;
            switch (paymentModel.PaymentStatus)
            {
                case (int)ReturnStatus.Save:
                    e.Item["PaymentStatusName"] = "已保存";
                    if(paymentModel.PaymentExtensionStatus==1)
                        e.Item["PaymentStatusName"] = "待总监审批";
                    else if (paymentModel.PaymentExtensionStatus ==2)
                        e.Item["PaymentStatusName"] = "待财务审批";
                    else if (paymentModel.PaymentExtensionStatus == 3)
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
            }
            else if (paymentModel.PaymentStatus == 2)
            {
                e.Item["Audit"] = "<a href='" + string.Format(ESP.Finance.Utility.Common.NotifyFinanceUrl, paymentModel.PaymentID) + "&BackUrl=BizAuditList.aspx'><img src='/images/Audit.gif' border='0px;' title='审批' /></a>"; ;
            }
            else if (paymentModel.PaymentStatus > 2)
            {
                e.Item["Audit"] = "";
            }
            else if (paymentModel.PaymentStatus == 0)
            {
                if (paymentModel.PaymentExtensionStatus == 1 || paymentModel.PaymentExtensionStatus == 2)
                {
                    e.Item["Audit"] = "<a href='" + string.Format(ESP.Finance.Utility.Common.NotifyDelayUrl, paymentModel.PaymentID) + "&BackUrl=BizAuditList.aspx'><img src='/images/Audit.gif' border='0px;' title='审批' /></a>"; ;
                }
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
}
