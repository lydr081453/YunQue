using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ESP.Finance.Utility;
namespace FinanceWeb.Refund
{
    public partial class RefundPrint : ESP.Web.UI.PageBase
    {
        //int num = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.lblTitle.Text = "退款申请单";
            }
            if (!string.IsNullOrEmpty(Request["viewButton"]) && Request["viewButton"] == "no")
            {
                btnClose.Visible = false;
                btnPrint.Visible = false;
            }
            initPrintPage();
        }

        private void initPrintPage()
        {
            int rid = int.Parse(Request[ESP.Finance.Utility.RequestName.RefundID].ToString());

            ESP.Finance.Entity.RefundInfo RefundModel = ESP.Finance.BusinessLogic.RefundManager.GetModel(rid);

            ESP.Purchase.Entity.GeneralInfo generalModel = null;

            if (RefundModel != null)
            {
                if (RefundModel.PRID != 0)
                {
                    generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(RefundModel.PRID);
                }
            }

            logoImg.ImageUrl = "/images/xingyan.png";
            lblPRNo.Text = RefundModel.PRNO;
            labProjectCode.Text = RefundModel.ProjectCode;

            lblCommitDate.Text = RefundModel.RequestDate == null ? "" : RefundModel.RequestDate.ToString("yyyy-MM-dd");
            lblPreDate.Text = RefundModel.RefundDate == null ? "" : RefundModel.RefundDate.ToString("yyyy-MM-dd");
            labReturnFactDate.Text = RefundModel.RefundDate.ToString("yyyy-MM-dd");

            ESP.Compatible.Employee req = new ESP.Compatible.Employee(RefundModel.RequestorID);
            ESP.Finance.Entity.DepartmentViewInfo deptView = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(RefundModel.DeptId);
            lblPN.Text = RefundModel.RefundCode;
            labRequestorUserName.Text = RefundModel.RequestEmployeeName;
            labRequestorID.Text = req.ID.ToString();
            labDepartment.Text = deptView.level1 + "-" + deptView.level2 + "-" + deptView.level3;
            lblRemark.Text = RefundModel.Remark;

            labPreFee.Text = RefundModel.Amounts.ToString("#,##0.00");
            lab_TotalPrice.Text = RefundModel.Amounts.ToString("#,##0.00");

            labAccountName.Text = generalModel.account_name;
            labAccountBankName.Text = generalModel.account_bank;
            labAccountBankNo.Text = generalModel.account_number;

            string rethist = string.Empty;
            string auditDate = string.Empty;


            IList<ESP.Finance.Entity.AuditLogInfo> histList = ESP.Finance.BusinessLogic.AuditLogManager.GetRefundList(RefundModel.Id);

            foreach (ESP.Finance.Entity.AuditLogInfo item in histList)
            {
                auditDate = item.AuditDate == null ? "" : item.AuditDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                if (item.AuditStatus == (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing)
                {
                    item.Suggestion = "审批通过:" + item.Suggestion;
                }
                else if (item.AuditStatus == (int)ESP.Finance.Utility.AuditHistoryStatus.TerminateAuditing)
                {
                    item.Suggestion = "审批驳回:" + item.Suggestion;
                }
                else
                {
                    item.Suggestion = item.Suggestion;
                }
                rethist += "审批人:  " + item.AuditorEmployeeName + "(" + item.AuditorUserName + ")" + "  [" + auditDate + "]  " + item.Suggestion + "<br/>";
            }
            this.lblAuditList.Text = rethist;
        }


    }
}