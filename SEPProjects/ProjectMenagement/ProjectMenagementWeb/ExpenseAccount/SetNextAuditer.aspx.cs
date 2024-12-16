/************************************************************************\
 * 审批页面
 *      
 * 不同角色进入后，页面显示权限不同，根据step参数区分
 * 
 * workitemid为工作项ID
 *
\************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

namespace FinanceWeb.ExpenseAccount
{
    public partial class SetNextAuditer : ESP.Web.UI.PageBase
    {
        List<ESP.Finance.Entity.ExpenseAccountBatchAudit> auditList = null;
        string ReturnUrl = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["ReturnUrl"]))
            {
                ReturnUrl = Request["ReturnUrl"].ToString();
            }
            else
            { 
                ReturnUrl = "MajorExpenseAccountList.aspx";
            }
            auditList = (List<ESP.Finance.Entity.ExpenseAccountBatchAudit>)Session["AuditListByBatch"];
            if (auditList != null)
            {
                initForm();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "window.location='" + ReturnUrl + "';", true);
                return;
            }
        }

        protected void initForm()
        {
            foreach (ESP.Finance.Entity.ExpenseAccountBatchAudit auditInfo in auditList)
            {
                if (auditInfo.Workitem != null && !string.IsNullOrEmpty(auditInfo.Workitem.WebPage) && (auditInfo.Workitem.WebPage.IndexOf("step=f3") > 0 || auditInfo.Workitem.WebPage.IndexOf("step=f2") > 0))
                {
                    trNext.Visible = false;
                }
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Session.Remove("AuditListByBatch");
            Response.Redirect(ReturnUrl);
        }

        protected void btnAudit_Click(object sender, EventArgs e)
        {
            if ( auditList != null && auditList.Count > 0)
            {
                //设置财务下级审批人参数
                List<int> nextAuditerList = new List<int>();
                if(hidNextAuditor != null && !string.IsNullOrEmpty(hidNextAuditor.Value))
                    nextAuditerList.Add(Convert.ToInt32(hidNextAuditor.Value));
                string batchNo = txtBatchNo.Text.Trim();

                bool isSuccess = false;

                if ("MajorExpenseAccountList.aspx".Equals(ReturnUrl))
                {
                    isSuccess = ESP.Finance.BusinessLogic.ExpenseAccountManager.BatchAuditExpense(auditList, nextAuditerList, batchNo);
                }
                else
                {
                    isSuccess = ESP.Finance.BusinessLogic.ExpenseAccountManager.BatchAuditReceiving(auditList, nextAuditerList, batchNo);
                }

                if (isSuccess)
                {
                    Session.Remove("AuditListByBatch");
                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('批量审核通过操作成功！');window.location='" + ReturnUrl + "';", true);
                    return;
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('批量审核操作失败，请重试！');window.location='" + ReturnUrl + "';", true);
                    return;
                }
            }
        }

        
    }
}
