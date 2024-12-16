using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ESP.Finance.Utility;
using System.Data;
using ESP.Framework.BusinessLogic;

namespace FinanceWeb.UserControls.Project
{
    public partial class EditTab : System.Web.UI.UserControl
    {
        private int tabIndex = 0;
        public int TabIndex
        {
            get { return tabIndex; }
            set { tabIndex = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //setTabNum();
            }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            AjaxPro.Utility.RegisterTypeForAjax(typeof(EditTab));

            switch (TabIndex)
            {
                case 0:
                    tabProject.Attributes["class"] = "button_on";
                    tabOOP.Attributes["class"] = "button_over";
                    tabPayment.Attributes["class"] = "button_over";
                    tabReturn.Attributes["class"] = "button_over";
                    tabSupporter.Attributes["class"] = "button_over";
                    tabPrDepayment.Attributes["class"] = "button_over";
                    tabRefund.Attributes["class"] = "button_over";

                    tabOOP.Attributes.Add("onmouseover", "changeClass(this);");
                    tabOOP.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabPayment.Attributes.Add("onmouseover", "changeClass(this);");
                    tabPayment.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabReturn.Attributes.Add("onmouseover", "changeClass(this);");
                    tabReturn.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabSupporter.Attributes.Add("onmouseover", "changeClass(this);");
                    tabSupporter.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabPrDepayment.Attributes.Add("onmouseover", "changeClass(this);");
                    tabPrDepayment.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabRefund.Attributes.Add("onmouseover", "changeClass(this);");
                    tabRefund.Attributes.Add("onmouseout", "changeClass2(this);");
                    break;
                case 1:
                    tabProject.Attributes["class"] = "button_over";
                    tabOOP.Attributes["class"] = "button_over";
                    tabPayment.Attributes["class"] = "button_over";
                    tabReturn.Attributes["class"] = "button_over";
                    tabSupporter.Attributes["class"] = "button_on";
                    tabPrDepayment.Attributes["class"] = "button_over";
                    tabRefund.Attributes["class"] = "button_over";

                    tabOOP.Attributes.Add("onmouseover", "changeClass(this);");
                    tabOOP.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabPayment.Attributes.Add("onmouseover", "changeClass(this);");
                    tabPayment.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabReturn.Attributes.Add("onmouseover", "changeClass(this);");
                    tabReturn.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabProject.Attributes.Add("onmouseover", "changeClass(this);");
                    tabProject.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabPrDepayment.Attributes.Add("onmouseover", "changeClass(this);");
                    tabPrDepayment.Attributes.Add("onmouseout", "changeClass2(this);");
                      tabRefund.Attributes.Add("onmouseover", "changeClass(this);");
                    tabRefund.Attributes.Add("onmouseout", "changeClass2(this);");
                    break;
                case 2:
                    tabProject.Attributes["class"] = "button_over";
                    tabOOP.Attributes["class"] = "button_over";
                    tabPayment.Attributes["class"] = "button_over";
                    tabReturn.Attributes["class"] = "button_on";
                    tabSupporter.Attributes["class"] = "button_over";
                    tabPrDepayment.Attributes["class"] = "button_over";
                    tabRefund.Attributes["class"] = "button_over";

                    tabOOP.Attributes.Add("onmouseover", "changeClass(this);");
                    tabOOP.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabPayment.Attributes.Add("onmouseover", "changeClass(this);");
                    tabPayment.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabSupporter.Attributes.Add("onmouseover", "changeClass(this);");
                    tabSupporter.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabProject.Attributes.Add("onmouseover", "changeClass(this);");
                    tabProject.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabPrDepayment.Attributes.Add("onmouseover", "changeClass(this);");
                    tabPrDepayment.Attributes.Add("onmouseout", "changeClass2(this);");
                      tabRefund.Attributes.Add("onmouseover", "changeClass(this);");
                    tabRefund.Attributes.Add("onmouseout", "changeClass2(this);");
                    break;
                case 3:
                    tabProject.Attributes["class"] = "button_over";
                    tabOOP.Attributes["class"] = "button_on";
                    tabPayment.Attributes["class"] = "button_over";
                    tabReturn.Attributes["class"] = "button_over";
                    tabSupporter.Attributes["class"] = "button_over";
                    tabPrDepayment.Attributes["class"] = "button_over";
                    tabRefund.Attributes["class"] = "button_over";

                    tabReturn.Attributes.Add("onmouseover", "changeClass(this);");
                    tabReturn.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabPayment.Attributes.Add("onmouseover", "changeClass(this);");
                    tabPayment.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabSupporter.Attributes.Add("onmouseover", "changeClass(this);");
                    tabSupporter.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabProject.Attributes.Add("onmouseover", "changeClass(this);");
                    tabProject.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabPrDepayment.Attributes.Add("onmouseover", "changeClass(this);");
                    tabPrDepayment.Attributes.Add("onmouseout", "changeClass2(this);");
                      tabRefund.Attributes.Add("onmouseover", "changeClass(this);");
                    tabRefund.Attributes.Add("onmouseout", "changeClass2(this);");
                    break;
                case 4:
                    tabProject.Attributes["class"] = "button_over";
                    tabOOP.Attributes["class"] = "button_over";
                    tabPayment.Attributes["class"] = "button_on";
                    tabReturn.Attributes["class"] = "button_over";
                    tabSupporter.Attributes["class"] = "button_over";
                    tabPrDepayment.Attributes["class"] = "button_over";
                    tabRefund.Attributes["class"] = "button_over";

                    tabReturn.Attributes.Add("onmouseover", "changeClass(this);");
                    tabReturn.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabOOP.Attributes.Add("onmouseover", "changeClass(this);");
                    tabOOP.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabSupporter.Attributes.Add("onmouseover", "changeClass(this);");
                    tabSupporter.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabProject.Attributes.Add("onmouseover", "changeClass(this);");
                    tabProject.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabPrDepayment.Attributes.Add("onmouseover", "changeClass(this);");
                    tabPrDepayment.Attributes.Add("onmouseout", "changeClass2(this);");
                      tabRefund.Attributes.Add("onmouseover", "changeClass(this);");
                    tabRefund.Attributes.Add("onmouseout", "changeClass2(this);");
                    break;
                case 5:
                    tabProject.Attributes["class"] = "button_over";
                    tabOOP.Attributes["class"] = "button_over";
                    tabPayment.Attributes["class"] = "button_over";
                    tabReturn.Attributes["class"] = "button_over";
                    tabSupporter.Attributes["class"] = "button_over";
                    tabPrDepayment.Attributes["class"] = "button_on";
                    tabRefund.Attributes["class"] = "button_over";

                    tabReturn.Attributes.Add("onmouseover", "changeClass(this);");
                    tabReturn.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabOOP.Attributes.Add("onmouseover", "changeClass(this);");
                    tabOOP.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabSupporter.Attributes.Add("onmouseover", "changeClass(this);");
                    tabSupporter.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabProject.Attributes.Add("onmouseover", "changeClass(this);");
                    tabProject.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabPayment.Attributes.Add("onmouseover", "changeClass(this);");
                    tabPayment.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabRefund.Attributes.Add("onmouseover", "changeClass(this);");
                    tabRefund.Attributes.Add("onmouseout", "changeClass2(this);");
                    break;
                case 6:
                    tabRefund.Attributes["class"] = "button_on";
                    tabProject.Attributes["class"] = "button_over";
                    tabOOP.Attributes["class"] = "button_over";
                    tabPayment.Attributes["class"] = "button_over";
                    tabReturn.Attributes["class"] = "button_over";
                    tabSupporter.Attributes["class"] = "button_over";
                    tabPrDepayment.Attributes["class"] = "button_over";

                    tabReturn.Attributes.Add("onmouseover", "changeClass(this);");
                    tabReturn.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabOOP.Attributes.Add("onmouseover", "changeClass(this);");
                    tabOOP.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabSupporter.Attributes.Add("onmouseover", "changeClass(this);");
                    tabSupporter.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabProject.Attributes.Add("onmouseover", "changeClass(this);");
                    tabProject.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabPayment.Attributes.Add("onmouseover", "changeClass(this);");
                    tabPayment.Attributes.Add("onmouseout", "changeClass2(this);");
                    tabPrDepayment.Attributes.Add("onmouseover", "changeClass(this);");
                    tabPrDepayment.Attributes.Add("onmouseout", "changeClass2(this);");
                    break;
            }
        }

        #region o
        private void setTabNum()
        {
            ESP.Web.UI.PageBase ParentPage = (ESP.Web.UI.PageBase)Page;
            for (int i = 0; i < 8; i++)
            {
                switch (i)
                {
                    case 0:
                        Tab1.Text += "<font color='red'>(" + GetProjectCount(ParentPage) + ")</font>";
                        break;
                    case 1:
                        Tab2.Text += "<font color='red'>(" + GetSupporterCount(ParentPage) + ")</font>";
                        break;
                    case 2:
                        Tab3.Text += "<font color='red'>(" + GetReturnCount(ParentPage) + ")</font>";
                        break;
                    case 3:
                        Tab4.Text += "<font color='red'>(" + GetOOPCount(ParentPage) + ")</font>";
                        break;
                    case 4:
                        Tab5.Text += "<font color='red'>(" + GetNotifyCount(ParentPage) + ")</font>";
                        break;
                    case 5:
                        Tab6.Text += "<font color='red'>(" + GetPrDepaymentCount(ParentPage) + ")</font>";
                        break;
                    case 6:
                        Tab7.Text += "<font color='red'>(" + GetRefundCount(ParentPage) + ")</font>";
                        break;
                }
            }
        }

        private string GetUser()
        {
            string user = "," + ESP.Finance.BusinessLogic.BranchManager.GetProjectAccounters()
                + "," + ESP.Finance.BusinessLogic.BranchManager.GetContractAccounters()
                + "," + ESP.Finance.BusinessLogic.BranchManager.GetFinalAccounters()
                + ",";
            return user;
        }


        private int GetProjectCount(ESP.Web.UI.PageBase ParentPage)
        {
            string term = string.Empty;
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<SqlParameter>();
            string users = GetUser();

            if (users.IndexOf("," + ParentPage.CurrentUserID.ToString() + ",") >= 0)
            {
                term = " and Status in('" + (int)Status.Saved + "','" + (int)Status.BizReject + "','" + (int)Status.ContractReject + "','" + (int)Status.FinanceReject + "')";
            }
            else
            {
                term = " and Status in('" + (int)Status.Saved + "','" + (int)Status.BizReject + "','" + (int)Status.ContractReject + "','" + (int)Status.FinanceReject + "') and (creatorid=@memberuserid or applicantUserID=@memberuserid)";
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@memberuserid", ParentPage.CurrentUserID));
            }
            return ESP.Finance.BusinessLogic.ProjectManager.GetList(term, paramlist).Count;
        }

        private int GetSupporterCount(ESP.Web.UI.PageBase ParentPage)
        {
            string term = string.Empty;
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();

            string users = "," + ESP.Finance.BusinessLogic.BranchManager.GetContractAccounters() + "," + ESP.Finance.BusinessLogic.BranchManager.GetFinalAccounters() + "," + ESP.Finance.BusinessLogic.BranchManager.GetProjectAccounters() + ",";

            term = " status in('" + ((int)Status.Saved).ToString() + "','" + ((int)Status.BizReject).ToString() + "','" + ((int)Status.FinanceReject) + "')";
            if (users.IndexOf("," + ParentPage.CurrentUserID + ",") >= 0)
            {
                term += "  and projectid in (select projectid from f_project where status=" + (int)ESP.Finance.Utility.Status.FinanceAuditComplete + " or status=" + (int)ESP.Finance.Utility.Status.ProjectPreClose + ")";
            }
            else
            {
                term += " and (leaderuserid=" + ParentPage.CurrentUserID + " or applicantuserid=" + ParentPage.CurrentUserID + ") and  projectid in (select projectid from f_project where status=" + (int)ESP.Finance.Utility.Status.FinanceAuditComplete + " or status=" + (int)ESP.Finance.Utility.Status.ProjectPreClose + ")";

            }
            return ESP.Finance.BusinessLogic.SupporterManager.GetList(term, paramlist).Count;
        }

        private int GetReturnCount(ESP.Web.UI.PageBase ParentPage)
        {
            string term = string.Empty;
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();

                term = " ((returnStatus!=1 and returnid in(select returnid from f_returninvoice where status!=2 and (faid=@currentUserId or financeid=@currentUserId))) or (RequestorID=@currentUserId and returnstatus=1) ";

            term += string.Format(" or (RequestorID=@currentUserId and ((returnStatus<>{0} and (isinvoice=0 or isinvoice is null)) or returnStatus={1}))) and returntype not in(20,30,31,32,33,34,35,36,37)", (int)ESP.Finance.Utility.PaymentStatus.Save, (int)ESP.Finance.Utility.PaymentStatus.FinanceReject);

            System.Data.SqlClient.SqlParameter puserid = new System.Data.SqlClient.SqlParameter("@currentUserId", System.Data.SqlDbType.Int, 4);
            puserid.SqlValue = int.Parse(ParentPage.CurrentUser.SysID);
            paramlist.Add(puserid);

            return ESP.Finance.BusinessLogic.ReturnManager.GetList(term, paramlist).Count;
        }

        private int GetOOPCount(ESP.Web.UI.PageBase ParentPage)
        {
            string conditionStr = string.Empty;
            conditionStr += string.Format(" RequestorID = {0}", ParentPage.CurrentUserID);
            conditionStr += " and returnStatus in(0,1,136,138,-1)";
            conditionStr += " and returnType in(30,31,32,33,34,35,36,37)";
            return ESP.Finance.BusinessLogic.ReturnManager.GetList(conditionStr).Count;
        }

        private int GetNotifyCount(ESP.Web.UI.PageBase ParentPage)
        {
            string term = string.Empty;
            List<SqlParameter> paramlist = new List<SqlParameter>();
            term = " ProjectID in(select ProjectID from F_Project where Status=@Status and ApplicantUserID=@UserID) ";
            SqlParameter p = new SqlParameter("@Status", SqlDbType.Int, 4);
            p.SqlValue = (int)Status.FinanceAuditComplete;
            paramlist.Add(p);
            SqlParameter p1 = new SqlParameter("@UserID", SqlDbType.Int, 4);
            p1.SqlValue = ParentPage.CurrentUserID;
            paramlist.Add(p1);
            term += " and paymentstatus = 0 and PaymentPreDate< '" + DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd") + "'";
            return ESP.Finance.BusinessLogic.PaymentManager.GetList(term, paramlist).Count;
        }

        private int GetPrDepaymentCount(ESP.Web.UI.PageBase ParentPage)
        {
            string delegateusers = string.Empty;
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(ParentPage.CurrentUserID);
            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
            {
                delegateusers += model.UserID.ToString() + ",";
            }
            delegateusers = delegateusers.TrimEnd(',');
            string Branchs = string.Empty;
            IList<ESP.Finance.Entity.BranchInfo> branchList = ESP.Finance.BusinessLogic.BranchManager.GetList(" OtherFinancialUsers like '%," + ParentPage.CurrentUserID + ",%'");
            if (branchList != null && branchList.Count > 0)
            {
                foreach (ESP.Finance.Entity.BranchInfo b in branchList)
                {
                    Branchs += b.BranchID.ToString() + ",";
                }
            }
            Branchs = Branchs.TrimEnd(',');

            string term = string.Empty;
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
            if (string.IsNullOrEmpty(Branchs))
            {
                if (!string.IsNullOrEmpty(delegateusers))
                {
                    term = " (returnID in(select returnid from F_ReturnAuditHist where AuditorUserID=@currentUserId  or AuditorUserID in(" + delegateusers + ")) or (RequestorID=@currentUserId or RequestorID in(" + delegateusers + "))) ";
                }
                else
                {
                    term = " (returnID in(select returnid from F_ReturnAuditHist where AuditorUserID=@currentUserId) or (RequestorID=@currentUserId)) ";
                }

                System.Data.SqlClient.SqlParameter puserid = new System.Data.SqlClient.SqlParameter("@currentUserId", System.Data.SqlDbType.Int, 4);
                puserid.SqlValue = ParentPage.CurrentUserID;
                paramlist.Add(puserid);
                term += " and returnType=@returnType and ReturnStatus=@ReturnStatus";
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@returnType", ESP.Purchase.Common.PRTYpe.CommonPR));
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@ReturnStatus", ESP.Finance.Utility.PaymentStatus.WaitReceiving));
            }
            else
            {
                term = "(projectid in(select projectid from f_project where branchid in(" + Branchs + ")))";
            }
            IList<ESP.Finance.Entity.ReturnInfo> returnlist = ESP.Finance.BusinessLogic.ReturnManager.GetList(term, paramlist);
            return returnlist.Count;
        }

        private int GetRefundCount(ESP.Web.UI.PageBase ParentPage)
        {
            string term = string.Empty;
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();

            term = " (RequestorID=@currentUserId and status in(0,1,-1)) ";

            System.Data.SqlClient.SqlParameter puserid = new System.Data.SqlClient.SqlParameter("@currentUserId", System.Data.SqlDbType.Int, 4);
            puserid.SqlValue = int.Parse(ParentPage.CurrentUser.SysID);
            paramlist.Add(puserid);

            return ESP.Finance.BusinessLogic.RefundManager.GetList(term, paramlist).Count;
        }

        #endregion

        private static int[] GetUsers()
        {
            var branches = ESP.Finance.BusinessLogic.BranchManager.GetAllList();
            var list = branches.Select(x => x.ProjectAccounter)
                .Union(branches.Select(x => x.FinalAccounter))
                .Union(branches.Select(x => x.ContractAccounter));

            return list.ToArray();
        }

        [AjaxPro.AjaxMethod]
        public static object GetCounts()
        {
            int userId = UserManager.GetCurrentUserID();
            int[] managers = GetUsers();

            var project = GetProjectCount(userId, managers);
            var supporter = GetSupporterCount(userId, managers);
            var @return = GetReturnCount(userId, managers);
            var oop = GetOOPCount(userId, managers);
            var notify = GetNotifyCount(userId, managers);
            var depayment = GetPrDepaymentCount(userId, managers);
            var refund = GetRefundCount(userId, managers);
            var d = new
            {
                project = project,
                supporter = supporter,
                @return = @return,
                oop = oop,
                notify = notify,
                depayment = depayment,
                refund = refund
            };
            return d;
        }

        private static int GetProjectCount(int userId, int[] managers)
        {
            var term = " and Status in(" + (int)Status.Saved + "," + (int)Status.BizReject + "," + (int)Status.ContractReject + "," + (int)Status.FinanceReject + ") ";
            if (!managers.Contains(userId))
            {
                term += " and  (creatorid=" + userId + " or applicantUserID=" + userId + ") ";
            }
            return ESP.Finance.BusinessLogic.ProjectManager.GetList(term).Count;
        }
        private static int GetSupporterCount(int userId, int[] managers)
        {
            string term = string.Format(" status in({0:D},{1:D},{2:D}) and projectid in (select projectid from f_project where status={3:D} or status={4:D}) ",
                Status.Saved, Status.BizReject, Status.FinanceReject,
                ESP.Finance.Utility.Status.FinanceAuditComplete, ESP.Finance.Utility.Status.ProjectPreClose);

            if (!managers.Contains(userId))
            {
                term += string.Format(" and (leaderuserid={0:D} or applicantuserid={0:D}) ", userId);
            }
            return ESP.Finance.BusinessLogic.SupporterManager.GetList(term).Count;
        }
        private static int GetReturnCount(int userId, int[] managers)
        {

            string term = string.Empty;
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
          
            term = " ((returnStatus!=1 and returnid in(select returnid from f_returninvoice where status!=2 and (faid=@currentUserId or financeid=@currentUserId))) or (RequestorID=@currentUserId and returnstatus=1) ";

            term += string.Format(" or (RequestorID=@currentUserId and ((returnStatus<>{0} and (isinvoice=0 or isinvoice is null)) or returnStatus={1}))) and returntype not in(20,30,31,32,33,34,35,36,37)", (int)ESP.Finance.Utility.PaymentStatus.Save, (int)ESP.Finance.Utility.PaymentStatus.FinanceReject);

            System.Data.SqlClient.SqlParameter puserid = new System.Data.SqlClient.SqlParameter("@currentUserId", System.Data.SqlDbType.Int, 4);
            puserid.SqlValue = userId;
            paramlist.Add(puserid);

            return ESP.Finance.BusinessLogic.ReturnManager.GetList(term, paramlist).Count;
        }
        private static int GetOOPCount(int userId, int[] managers)
        {
            string conditionStr = string.Empty;
            conditionStr += string.Format(" RequestorID = {0}", userId);
            conditionStr += " and returnStatus in(0,1,136,138,-1)";
            conditionStr += " and returnType in(30,31,32,33,34,35,36,37)";
            return ESP.Finance.BusinessLogic.ReturnManager.GetList(conditionStr).Count;
        }
        private static int GetNotifyCount(int userId, int[] managers)
        {
            string term = string.Empty;
            List<SqlParameter> paramlist = new List<SqlParameter>();
            term = " ProjectID in(select ProjectID from F_Project where Status=@Status and ApplicantUserID=@UserID) ";
            SqlParameter p = new SqlParameter("@Status", SqlDbType.Int, 4);
            p.SqlValue = (int)Status.FinanceAuditComplete;
            paramlist.Add(p);
            SqlParameter p1 = new SqlParameter("@UserID", SqlDbType.Int, 4);
            p1.SqlValue = userId;
            paramlist.Add(p1);
            term += " and paymentstatus = 0 and PaymentPreDate< '" + DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd") + "'";
            return ESP.Finance.BusinessLogic.PaymentManager.GetList(term, paramlist).Count;
        }
        private static int GetPrDepaymentCount(int userId, int[] managers)
        {
            string delegateusers = string.Empty;
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(userId);
            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
            {
                delegateusers += model.UserID.ToString() + ",";
            }
            delegateusers = delegateusers.TrimEnd(',');
            string Branchs = string.Empty;
            IList<ESP.Finance.Entity.BranchInfo> branchList = ESP.Finance.BusinessLogic.BranchManager.GetList(" OtherFinancialUsers like '%," + userId + ",%'");
            if (branchList != null && branchList.Count > 0)
            {
                foreach (ESP.Finance.Entity.BranchInfo b in branchList)
                {
                    Branchs += b.BranchID.ToString() + ",";
                }
            }
            Branchs = Branchs.TrimEnd(',');

            string term = string.Empty;
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
            if (string.IsNullOrEmpty(Branchs))
            {
                if (!string.IsNullOrEmpty(delegateusers))
                {
                    term = " (returnID in(select returnid from F_ReturnAuditHist where AuditorUserID=@currentUserId  or AuditorUserID in(" + delegateusers + ")) or (RequestorID=@currentUserId or RequestorID in(" + delegateusers + "))) ";
                }
                else
                {
                    term = " (returnID in(select returnid from F_ReturnAuditHist where AuditorUserID=@currentUserId) or (RequestorID=@currentUserId)) ";
                }

                System.Data.SqlClient.SqlParameter puserid = new System.Data.SqlClient.SqlParameter("@currentUserId", System.Data.SqlDbType.Int, 4);
                puserid.SqlValue = userId;
                paramlist.Add(puserid);
                term += " and returnType=@returnType and ReturnStatus=@ReturnStatus";
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@returnType", ESP.Purchase.Common.PRTYpe.CommonPR));
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@ReturnStatus", ESP.Finance.Utility.PaymentStatus.WaitReceiving));
            }
            else
            {
                term = "(projectid in(select projectid from f_project where branchid in(" + Branchs + ")))";
            }
            var c = ESP.Finance.BusinessLogic.ReturnManager.GetCount(term, paramlist);

            return c;
        }

        private static int GetRefundCount(int userId, int[] managers)
        {

            string term = string.Empty;
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();

            term = " (RequestorID=@currentUserId and status in(0,1,-1)) ";

            System.Data.SqlClient.SqlParameter puserid = new System.Data.SqlClient.SqlParameter("@currentUserId", System.Data.SqlDbType.Int, 4);
            puserid.SqlValue = userId;
            paramlist.Add(puserid);

            return ESP.Finance.BusinessLogic.RefundManager.GetList(term, paramlist).Count;
        }
    }
}