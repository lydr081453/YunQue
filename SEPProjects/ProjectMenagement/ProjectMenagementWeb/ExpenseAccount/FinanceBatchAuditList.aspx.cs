/************************************************************************\
 * 审核列表页
 * 
 * 根据登录人不同角色，列出所有此人待审核的报销单
 *       
 *
\************************************************************************/
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using System.Data;

namespace FinanceWeb.ExpenseAccount
{
    public partial class FinanceBatchAuditList : ESP.Web.UI.PageBase
    {
        ESP.Finance.Entity.ExpenseAccountExtendsInfo model = null;
        ESP.Finance.SqlDataAccess.WorkItem workitem = null;
        bool isFinances = false;
        bool isF1 = false;
        bool isF2orF3 = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Server.ScriptTimeout = 600;
                bindList();
                bindAuditedList(0);
            }
        }

        /// <summary>
        /// 更具代理人获取其代理的所有人
        /// </summary>
        /// <returns></returns>
        private string GetDelegateUser()
        {
            string users = string.Empty;
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegateList = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegateList)
            {
                users += model.UserID.ToString()+",";
            }
            users += CurrentUser.SysID;
            return users;
        }

        private void bindList()
        {
            string whereStr = "";

            whereStr += string.Format(" and PaymentUserID in ({0}) and BatchType = 2 and Status not in({1},{2},{3}) ", GetDelegateUser(), (int)PaymentStatus.FAAudit, (int)PaymentStatus.FinanceComplete, (int)PaymentStatus.WaitReceiving);

            if (!string.IsNullOrEmpty(txtKey.Text.Trim()))
            {
                whereStr += string.Format(" and  (BatchCode like '%{0}%' or PurchaseBatchCode like '%{0}%') ", txtKey.Text.Trim());
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


            GridNoNeed.DataSource = dt;
            GridNoNeed.DataBind();

            
            
        }

        private void bindAuditedList(int flag)
        {
            string whereStr = "";
            if (CurrentUser.SysID.Trim() == System.Configuration.ConfigurationManager.AppSettings["EddyBinID"].Trim() )
            {
                whereStr += string.Format(" and BatchType = 2 and Status ={0} ", (int)PaymentStatus.FinanceComplete);
            }
            else
            {
                whereStr += string.Format(" and (PaymentUserID in ({0}) or creatorid={1} or d.otherfinancialusers like '%,{1},%') and BatchType = 2 and Status in({2},{3},{4},{5}) ", GetDelegateUser(), CurrentUser.SysID, (int)PaymentStatus.FinanceComplete, (int)PaymentStatus.FinanceLevel1, (int)PaymentStatus.FinanceLevel2, (int)PaymentStatus.FinanceLevel3);
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
            if (flag == 0)
            {
                whereStr += string.Format(" and a.createdate >'{0}'",DateTime.Now.AddMonths(-6).ToString("yyyy-MM-dd"));
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
            bindList();
            bindAuditedList(1);
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

        protected string getUrl2(string batchid)
        {
           return  "<a target='_blank' href='FinanceBatchAuditView.aspx?batchid=" + batchid + "'><img src='images/Audit.gif' /></a>";
           
        }

        protected string getFinanceUrl(string batchid)
        {
            return "<a target='_blank' href='FinanceBatchAuditView.aspx?batchid=" + batchid + "'><img src='images/output.gif' /></a>";
        }
    }
}
