using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace FinanceWeb.UserControls.Project
{
    public partial class AuditTab : System.Web.UI.UserControl
    {
        //private int tabIndex = 0;
        //public int TabIndex
        //{
        //    get { return tabIndex; }
        //    set { tabIndex = value; }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        public int TabIndex { get; set; }

        public IList<ESP.Finance.Entity.ProjectInfo> Projects { get; private set; }
        public IList<ESP.Finance.Entity.SupporterInfo> Supporter { get; private set; }
        public IList<ESP.Finance.Entity.ReturnInfo> Returns { get; private set; }
        public IList<ESP.Finance.Entity.PaymentInfo> Payments { get; private set; }
        public IList<ESP.Finance.Entity.ProjectInfo> Contracts { get; private set; }
        public IList<ESP.Finance.Entity.ProjectInfo> ApplyForInvioces { get; private set; }
        public IList<ESP.Finance.Entity.PNBatchInfo> Consumptions { get; private set; }
        public IList<ESP.Finance.Entity.PNBatchInfo> RebateRegistrations { get; private set; }
        public IList<ESP.Finance.Entity.RefundInfo> Refunds { get; private set; }
        public DataSet Expenses { get; private set; }

        public void BindData()
        {
            int[] userIds = GetUsers();
            string userIdsString = GetDelegateUser(userIds);

            Projects = ESP.Finance.BusinessLogic.ProjectManager.GetWaitAuditList(userIds);
            Supporter = ESP.Finance.BusinessLogic.SupporterManager.GetWaitAuditList(userIds);
            Returns = ESP.Finance.BusinessLogic.ReturnManager.GetWaitAuditList(userIds);
            Payments = ESP.Finance.BusinessLogic.PaymentManager.GetWaitAuditList(userIds);
            Consumptions = ESP.Finance.BusinessLogic.PNBatchManager.GetWaitAuditList(userIds,4);
            RebateRegistrations = ESP.Finance.BusinessLogic.PNBatchManager.GetWaitAuditList(userIds,5);
            Expenses = GetExpenseAccount(userIdsString);
            Refunds = ESP.Finance.BusinessLogic.RefundManager.GetWaitAuditList(userIds);

            SetTab(Table2, Tab2, TabIndex == 2, Projects.Count, "/project/ProjectAuditList.aspx");//
            SetTab(Table3, Tab3, TabIndex == 3, Supporter.Count, "/project/SupportAuditList.aspx");//
            SetTab(Table4, Tab4, TabIndex == 4, Returns.Count, "/Purchase/ReturnAuditList.aspx");//
            SetTab(Table5, Tab5, TabIndex == 5, Payments.Count, "/Return/BizAuditList.aspx");//
            SetTab(Table6, Tab6, TabIndex == 6, Expenses.Tables[0].Rows.Count, "/ExpenseAccount/ExpenseAccountAuditList.aspx");
            
            //证据链审核人
            
            if (ESP.Finance.BusinessLogic.BranchManager.GetList("contractAuditor in("+userIdsString+")").Count > 0)
            {
                Contracts = ESP.Finance.BusinessLogic.ProjectManager.GetContractAuditingProjectList();
                SetTab(Table7, Tab7, TabIndex == 7, Contracts.Count, "/ContractFiles/ContractAuditList.aspx");

                ApplyForInvioces = ESP.Finance.BusinessLogic.ProjectManager.GetApplyForInvioceAuditingProjectList();
                SetTab(Table8, Tab8, TabIndex == 8, ApplyForInvioces.Count, "/ApplyForInvioce/ApplyForInvioceAuditList.aspx");
            }
            else
            {
                Table7.Visible = false;
                Table8.Visible = false;
            }
            //消耗导入审批
            SetTab(Table9, Tab9, TabIndex == 9, Consumptions.Count, "/Consumption/ConsumptionAuditList.aspx");
            //返点导入审批
            SetTab(Table10, Tab10, TabIndex == 10, RebateRegistrations.Count, "/RebateRegistration/RebateRegistrationAuditList.aspx");
            //退款申请
            SetTab(Table11, Tab11, TabIndex == 11, Refunds.Count, "/Refund/RefundAuditList.aspx");
        }

        private int[] GetUsers()
        {
            var currentUserId = ESP.Framework.BusinessLogic.UserManager.GetCurrentUserID();
            List<int> users = new List<int>();
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegateList = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(currentUserId);
            users.AddRange(delegateList.Select(x => x.UserID));
            users.Add(currentUserId);
            return users.ToArray();
        }

        private void SetTab(System.Web.UI.HtmlControls.HtmlTable table, HyperLink link, bool current, int count, string url)
        {
            if (current)
            {
                table.Attributes["class"] = "button_on";
            }
            else
            {
                table.Attributes["class"] = "button_over";
                table.Attributes.Add("onmouseover", "changeClass(this);");
                table.Attributes.Add("onmouseout", "changeClass2(this);");
            }
            if (count >= 0)
                link.Text += "<font color='red'>(" + count + ")</font>";
            link.NavigateUrl = url;
        }

        /// <summary>
        /// 更具代理人获取其代理的所有人
        /// </summary>
        /// <returns></returns>
        private string GetDelegateUser(int[] userIds)
        {
            if (userIds == null || userIds.Length == 0)
                return null;

            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.Append(userIds[0]);
            for (var i = 1; i < userIds.Length; i++)
            {
                s.Append(",").Append(userIds[i]);
            }

            return s.ToString();
        }

        public DataSet GetExpenseAccount(string userIdsString)
        {
            string whereStr = "";

            whereStr += string.Format(" and wa.assigneeid in ({0}) and wi.status = {1} ", userIdsString, (int)ESP.Workflow.WorkItemStatus.Open);

            whereStr += " and r.returnid not in(SELECT  a.ReturnID FROM F_PNBatchRelation as a inner join F_PNBatch as b on a.batchid=b.batchid where b.batchtype=2) ";

            DataSet ds = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetMajorAuditList(whereStr, "");
            return ds;
        }
    }
}