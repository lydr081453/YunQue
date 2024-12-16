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
    public partial class TicketAuditList : ESP.Web.UI.PageBase
    {

        ESP.Finance.SqlDataAccess.WorkFlowDataContext dataContext = new ESP.Finance.SqlDataAccess.WorkFlowDataContext();
        ESP.Finance.Entity.ExpenseAccountExtendsInfo model = null;
        ESP.Finance.SqlDataAccess.WorkItem workitem = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ClientScript.RegisterStartupScript(typeof(ScriptManager), "AppInitialize", "", true);

            if (!IsPostBack)
            {
                bindList();
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

        private void bindList()
        {
            this.AuditTab.BindData();

            string whereStr = "";
            //and wi.workitemname != '财务第一级审批' and wi.workitemname != '财务第二级审批' and wi.workitemname != '财务第三级审批'
            whereStr += string.Format(" and wa.assigneeid in ({0}) and wi.status = {1} and r.returnType=40", GetDelegateUser(), (int)ESP.Workflow.WorkItemStatus.Open);

            if (!string.IsNullOrEmpty(txtKey.Text.Trim()))
            {
                whereStr += string.Format(" and ( r.ProjectCode like '%{0}%' or r.ReturnCode like '%{0}%' or r.RequestEmployeeName like '%{0}%' or r.PreFee like '%{0}%' or  r.PaymentTypeCode like '%{0}%' )", txtKey.Text.Trim());
            }
            whereStr += " and r.returnid not in(SELECT  a.ReturnID FROM F_PNBatchRelation as a inner join F_PNBatch as b on a.batchid=b.batchid where b.batchtype=2) ";

            DataSet ds = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetMajorAuditList(whereStr, "");

            if (ds.Tables[0].Rows.Count == 0)
            {
                GridNoNeed.DataSource = ds.Tables[0];
                GridNoNeed.DataBind();
            }

            string returnIds = string.Empty;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                returnIds += dr["ReturnID"].ToString() + ",";
            }

            if (!string.IsNullOrEmpty(returnIds))
            {
                IList<ESP.Finance.Entity.ExpenseAccountDetailInfo> detailList = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(" and returnID in(" + returnIds.TrimEnd(',') + ")");
                DataSet ds2 = ESP.MediaLinq.Utilities.ListToDataSet.ToDataSet(detailList);
                DataTable dt = new DataTable();
                dt = ds2.Tables[0].Copy();
                dt.TableName = "detail";
                ds.Tables.Add(dt);
                if (detailList != null && detailList.Count != 0)
                    ds.Relations.Add(ds.Tables[0].Columns["ReturnID"], ds.Tables["detail"].Columns["ReturnID"]);
                GridNoNeed.DataSource = ds.Tables[0];
                GridNoNeed.DataBind();
            }


        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindList();
        }

        protected string getReturnCode(int returnid)
        {
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnid);
            string code = returnModel.ReturnCode;
            if (returnModel.TicketNo != 0)
                code += "-" + returnModel.TicketNo.ToString();
            return code;
        }

        protected string getUrl(string webpage, string workitemid, string workMonth)
        {
            var sum = "<a href='" + webpage + "&workitemid=" + workitemid + "&BackUrl=TicketAuditList.aspx'><img src='images/Audit.gif' /></a>";
            return sum;
        }
        #region
        protected int GetStep()
        {
            if (workitem != null && !string.IsNullOrEmpty(workitem.WebPage))
            {
                if (workitem.WebPage.IndexOf("step=pa") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_YS;
                }
                else if (workitem.WebPage.IndexOf("step=pm") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_XMFZ;
                }
                else if (workitem.WebPage.IndexOf("step=mj") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_ZJSP;
                }
                else if (workitem.WebPage.IndexOf("step=gm") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLSP;
                }
                else if (workitem.WebPage.IndexOf("step=ceo") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_CEO;
                }
                else if (workitem.WebPage.IndexOf("step=fa") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_FA;
                }
                else if (workitem.WebPage.IndexOf("step=f1") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
                }
                else if (workitem.WebPage.IndexOf("step=f2") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial2;
                }
                else if (workitem.WebPage.IndexOf("step=f3") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial3;
                }
                else if (workitem.WebPage.IndexOf("step=fm") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_FinancialMajor;
                }
                else if (workitem.WebPage.IndexOf("step=ra") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.FJ_Recived;
                }
            }
            return 0;
        }

        protected int GetStatus()
        {
            if (workitem != null && !string.IsNullOrEmpty(workitem.WebPage))
            {
                if (workitem.WebPage.IndexOf("step=pa") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.PrepareAudit;
                }
                else if (workitem.WebPage.IndexOf("step=pm") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.ProjectManagerAudit;
                }
                else if (workitem.WebPage.IndexOf("step=mj") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.MajorAudit;
                }
                else if (workitem.WebPage.IndexOf("step=gm") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.GeneralManagerAudit;
                }
                else if (workitem.WebPage.IndexOf("step=ceo") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.CEOAudit;
                }
                else if (workitem.WebPage.IndexOf("step=fa") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.FAAudit;
                }
                else if (workitem.WebPage.IndexOf("step=f1") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.FinanceLevel1;
                }
                else if (workitem.WebPage.IndexOf("step=f2") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.FinanceLevel2;
                }
                else if (workitem.WebPage.IndexOf("step=f3") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete;
                }
                else if (workitem.WebPage.IndexOf("step=fm") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.EddyAudit;
                }
                else if (workitem.WebPage.IndexOf("step=rv") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.Recived;
                }
            }
            return 0;
        }
        #endregion
    }
}
