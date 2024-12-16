using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using System.IO;
using System.Data.SqlClient;
using ESP.Finance.Utility;
using System.Data;

namespace FinanceWeb.project
{
    public partial class OperationAuditList : ESP.Web.UI.PageBase
    {
        //WorkFlowImpl.WorkItemData workitemdata = new WorkFlowImpl.WorkItemData();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //GetWorkFlowListP();
                //GetWorkFlowListS();

                //IList<ESP.Finance.Entity.ReturnInfo> list = Search();
                //SearchHist(list);
                grComplete.DataSource = ESP.Finance.BusinessLogic.ProjectManager.GetTaskItems(GetDelegateUser());
                grComplete.DataBind();
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
                users += model.UserID.ToString() + ",";
            }
            users += CurrentUser.SysID;
            return users;
        }

        protected string getUrl(string FormType, string FormID,string Url)
        {
            bool isFinances = false;
            
            if (FormType == "PR现金借款冲销" || FormType == "第三方报销单" || FormType == "借款冲销单" || FormType == "商务卡报销单" || FormType == "报销单" || FormType == "支票/电汇付款单" || FormType == "现金借款单")
            {
                DataSet ds = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetMajorAuditList(" and r.returnId=" + FormID.ToString() + " and wa.assigneeid=" + CurrentUser.SysID, "");
                if (!string.IsNullOrEmpty(Url) && (Url.IndexOf("step=fa") > 0 || Url.IndexOf("step=f1") > 0 || Url.IndexOf("step=f2") > 0 || Url.IndexOf("step=f3") > 0))
                {
                    isFinances = true;
                }
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count != 0)
                {
                    if (ds.Tables[0].Rows[0]["workMonth"].ToString().Trim() != "1.本次处理" && isFinances)
                    {
                        return "";

                    }
                }
               
            }
            return string.Format("<a href='..{0}&BackUrl=/project/OperationAuditList.aspx'><img src='/images/Audit.gif' border='0' /></a>",Url);
        }
    }
}
