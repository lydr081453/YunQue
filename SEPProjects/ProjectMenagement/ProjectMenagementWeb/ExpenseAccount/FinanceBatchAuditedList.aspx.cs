using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ESP.Finance.Utility;
using System.Collections.Generic;

namespace FinanceWeb.ExpenseAccount
{
    public partial class FinanceBatchAuditedList : ESP.Web.UI.PageBase
    {
        ESP.Finance.Entity.ExpenseAccountExtendsInfo model = null;
        ESP.Finance.SqlDataAccess.WorkItem workitem = null;
        bool isFinances = false;
        bool isF1 = false;
        bool isF2orF3 = false;

        private string GetDelegateUser()
        {
            string users = string.Empty;
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegateList = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegateList)
            {
                users += model.UserID.ToString() + ",";
            }
            users += CurrentUser.SysID;
            return users;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Server.ScriptTimeout = 600;
                bindAuditedList();
            }
        }

        private void bindAuditedList()
        {
            string whereStr = "";
            if (CurrentUser.SysID.Trim() == System.Configuration.ConfigurationManager.AppSettings["EddyBinID"].Trim())
            {
                whereStr += string.Format(" and BatchType = 2 and Status in({0},{1}) ", (int)PaymentStatus.FinanceComplete, (int)PaymentStatus.WaitReceiving);
            }
            else
            {
                whereStr += string.Format(" and PaymentUserID in ({0}) and BatchType = 2 and Status in({1},{2}) ", GetDelegateUser(), (int)PaymentStatus.FinanceComplete, (int)PaymentStatus.WaitReceiving);
            }
            if (!string.IsNullOrEmpty(txtKey.Text.Trim()))
            {
                whereStr += string.Format(" and  (BatchCode like '%{0}%' or PurchaseBatchCode like '%{0}%')", txtKey.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtBeginDate.Text.Trim()))
            {
                whereStr += string.Format(" and datediff(dd, cast('{0}' as datetime), CreateDate ) >= 0 ", txtBeginDate.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtEndDate.Text.Trim()))
            {
                whereStr += string.Format(" and datediff(dd, cast('{0}' as datetime), CreateDate ) <= 0 ", txtBeginDate.Text.Trim());
            }


            DataTable dt = ESP.Finance.BusinessLogic.PNBatchManager.GetBatchByExpenseAccount(whereStr);

            //foreach (DataRow dr in dt.Rows)
            //{

            //}


            gdAudited.DataSource = dt;
            gdAudited.DataBind();



        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindAuditedList();
        }

        protected string getEmpName(string createrID)
        {
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(Convert.ToInt32(createrID));
            if (emp != null)
            {
                return emp.Name;
            }
            return "";
        }

        protected string getUrl(string batchid)
        {
            ESP.Finance.Entity.PNBatchInfo batchInfo = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(Convert.ToInt32(batchid));
            var sum = "";
            if (batchInfo.Status == (int)PaymentStatus.FinanceComplete)
            {
                sum = "财务审批通过";
            }
            else
            {
                sum = "<a href='FinanceBatchAuditEdit.aspx?batchid=" + batchid + "'><img src='images/Audit.gif' /></a>";
            }

            return sum;
        }
    }
}
