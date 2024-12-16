using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using ESP.Finance.Utility;

namespace FinanceWeb.Edit
{
    public partial class NotifyTabEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !this.GridProject.CausedCallback)
            {
                Search();
            }
        }

        private void Search()
        {
            string term = string.Empty;
            List<SqlParameter> paramlist = new List<SqlParameter>();
            term = " ProjectID in(select ProjectID from F_Project where Status=@Status and ApplicantUserID=@UserID) ";
            SqlParameter p = new SqlParameter("@Status", SqlDbType.Int, 4);
            p.SqlValue = (int)Status.FinanceAuditComplete;
            paramlist.Add(p);
            SqlParameter p1 = new SqlParameter("@UserID", SqlDbType.Int, 4);
            p1.SqlValue = Convert.ToInt32(CurrentUser.SysID);
            paramlist.Add(p1);

            term += " and (paymentstatus = 0 and (paymentExtensionStatus is null or paymentExtensionStatus=0 or paymentExtensionStatus=3)) and PaymentPreDate< '" + DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd") + "'";

            if (!string.IsNullOrEmpty(term))
            {
                if (!string.IsNullOrEmpty(this.txtKey.Text))
                {
                    term += " and (PaymentCode like '%'+@Key+'%' or ProjectCode like '%'+@Key+'%' or PaymentContent like '%'+@Key+'%' )";
                    SqlParameter p2 = new SqlParameter("@Key", SqlDbType.NVarChar, 50);
                    p2.SqlValue = this.txtKey.Text.Trim();
                    paramlist.Add(p2);
                }
                this.GridProject.DataSource = ESP.Finance.BusinessLogic.PaymentManager.GetList(term, paramlist);
                this.GridProject.DataBind();
            }
        }

        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);
            GridProject.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(GridProject_NeedRebind);
            GridProject.PageIndexChanged += new ComponentArt.Web.UI.Grid.PageIndexChangedEventHandler(GridProject_PageIndexChanged);

        }

        void GridProject_NeedRebind(object sender, EventArgs e)
        {
            Search();
        }

        void GridProject_PageIndexChanged(object sender, ComponentArt.Web.UI.GridPageIndexChangedEventArgs e)
        {
            GridProject.CurrentPageIndex = e.NewIndex;
        }
        protected void GridProject_ItemDataBound(object sender, ComponentArt.Web.UI.GridItemDataBoundEventArgs e)
        {
            ESP.Finance.Entity.PaymentInfo paymentModel = (ESP.Finance.Entity.PaymentInfo)e.DataItem;
            ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(paymentModel.ProjectID);
            switch (paymentModel.PaymentStatus)
            {
                case (int)ReturnStatus.Save:
                    e.Item["PaymentStatusName"] = "已保存";
                    if (paymentModel.PaymentExtensionStatus == 1)
                        e.Item["PaymentStatusName"] = "待总监审批";
                    else if (paymentModel.PaymentExtensionStatus == 2)
                        e.Item["PaymentStatusName"] = "待财务审批";
                    else if (paymentModel.PaymentExtensionStatus == 3)
                        e.Item["PaymentStatusName"] = "已保存";

                    break;
                case (int)ReturnStatus.Submit:
                    e.Item["PaymentStatusName"] = "已提交";
                    break;
                case (int)ReturnStatus.MajorCommit:
                    e.Item["PaymentStatusName"] = "总监已确认";
                    break;
                case (int)ReturnStatus.FinancialHold:
                    e.Item["PaymentStatusName"] = "财务已挂账";
                    break;
                case (int)ReturnStatus.FinancialOver:
                    e.Item["PaymentStatusName"] = "已付款";
                    break;
            }

            e.Item["Print"] = "  <a href='/Return/NotificationPrint.aspx?" + ESP.Finance.Utility.RequestName.PaymentID + "=" + paymentModel.PaymentID.ToString() + "'" +
                                                            " target=\"_blank\"><img title=\"付款通知打印预览\" src=\"/images/PrintDefault.gif\" border=\"0px;\" /></a>";
            e.Item["Edit"] = "<a href='/Return/PaymentNotifyEdit.aspx?" + RequestName.PaymentID + "=" + paymentModel.PaymentID + "'><img src='/images/edit.gif' border='0px' /></a>";
            e.Item["Extension"] = "<a href='/Return/PaymentNotifyExtension.aspx?" + RequestName.PaymentID + "=" + paymentModel.PaymentID + "'><img src='/images/edit.gif' border='0px' /></a>";
            e.Item["ApplicantEmployeeName"] = "<a onclick=\"ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(projectModel.ApplicantUserID) + "');\" >" + projectModel.ApplicantEmployeeName + "</a>";
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Search();
        }
    }
}
