using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using System.Linq;
namespace FinanceWeb.Search
{
    public partial class NotifyTabList : ESP.Web.UI.PageBase
    {
        IList<ESP.Finance.Entity.ProjectInfo> projectList = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            projectList = ESP.Finance.BusinessLogic.ProjectManager.GetList(" and status=32");
            if (!IsPostBack && !this.GridProject.CausedCallback)
            {
                Search();
            }
        }

        protected override void OnInit(EventArgs e)
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


        private void Search()
        {
            string term = string.Empty;
            List<SqlParameter> paramlist = new List<SqlParameter>();
            term = @" PaymentID in (select PaymentID from F_PaymentAuditHist where AuditorUserID =@UserID) 
                      or ProjectID in(select ProjectID from F_Project where Status=@Status and ApplicantUserID=@UserID) 
                      or projectid in(select projectid from f_project where branchcode in(select branchcode from f_branch where paymentaccounter=@UserID) 
                      and Status=@Status) ";
            SqlParameter p = new SqlParameter("@Status", SqlDbType.Int, 4);
            p.SqlValue = (int)Status.FinanceAuditComplete;
            paramlist.Add(p);
            SqlParameter p1 = new SqlParameter("@UserID", SqlDbType.Int, 4);
            p1.SqlValue = Convert.ToInt32(CurrentUser.SysID);
            paramlist.Add(p1);

            if (this.ddlStatus.SelectedIndex != 0)
            {
                term += " and paymentstatus = @PaymentStatus ";
                SqlParameter p2 = new SqlParameter("@PaymentStatus", SqlDbType.Int, 4);
                p2.SqlValue = Convert.ToInt32(this.ddlStatus.SelectedItem.Value);
                paramlist.Add(p2);
            }
            else
            {
                term += " and paymentstatus != 0";

            }

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

        protected void GridProject_ItemDataBound(object sender, ComponentArt.Web.UI.GridItemDataBoundEventArgs e)
        {
            ESP.Finance.Entity.PaymentInfo paymentModel = (ESP.Finance.Entity.PaymentInfo)e.DataItem;
            ESP.Finance.Entity.ProjectInfo projectModel = projectList.Where(x => x.ProjectId == paymentModel.ProjectID).FirstOrDefault();
            e.Item["ProjectCode"] = projectModel.ProjectCode;
            e.Item["ApplicantEmployeeName"] = projectModel.ApplicantEmployeeName;
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

            if (paymentModel.PaymentStatus == 2 || paymentModel.PaymentStatus == 3 || paymentModel.PaymentStatus == 4)
                e.Item["InvoiceSign"] = "<a href='" + string.Format(ESP.Finance.Utility.Common.NotifyFinanceUrl, paymentModel.PaymentID) + "&Invoice=1&BackUrl=/Search/NotifyTabList.aspx'><img src='/images/Audit.gif' border='0px;' title='发票登记' /></a>"; ;


            e.Item["ViewAudit"] = "<a href=\"/project/ProjectWorkFlow.aspx?Type=payment&FlowID=" + paymentModel.PaymentID.ToString() +
                                                            "\" target=\"_blank\"><img src=\"/images/AuditStatus.gif\" border=\"0px;\" title=\"审批状态\"></a>";
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Search();
        }

    }
}
