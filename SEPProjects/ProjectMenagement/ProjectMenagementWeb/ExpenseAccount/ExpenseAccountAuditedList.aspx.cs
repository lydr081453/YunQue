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
    public partial class ExpenseAccountAuditedList : ESP.Web.UI.PageBase
    {
        ESP.Finance.SqlDataAccess.WorkFlowDataContext dataContext = new ESP.Finance.SqlDataAccess.WorkFlowDataContext();
        ESP.Finance.Entity.ExpenseAccountExtendsInfo model = null;
        ESP.Finance.SqlDataAccess.WorkItem workitem = null;
        bool isFinances = false;
        bool isF1 = false;
        bool isF2orF3 = false;
        bool isFa = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ClientScript.RegisterStartupScript(typeof(ScriptManager), "AppInitialize", "", true);

            if (!IsPostBack)
            {
                bindBranch();
                bindHistList();
            }
        }

        protected void bindBranch()
        {
            //string whereStr = string.Format(" FirstFinanceID = {0} or otherfinancialusers like '%,{1},%'", CurrentUserID,CurrentUserID);
            if (isF2orF3)
            {
                //  whereStr = "";
            }
            List<ESP.Finance.Entity.BranchInfo> list = (List<ESP.Finance.Entity.BranchInfo>)ESP.Finance.BusinessLogic.BranchManager.GetList("");

            ddlBranch.Items.Clear();
            ddlBranch.Items.Insert(0, new ListItem("请选择..", "0"));

            int count = 1;
            foreach (ESP.Finance.Entity.BranchInfo branch in list)
            {
                ddlBranch.Items.Insert(count, new ListItem(branch.BranchCode, branch.BranchCode));
                count++;
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
        private void bindHistList()
        {
            this.AuditTab.BindData();

            string whereStr = "";

            whereStr += string.Format(" and r.returnid in(select distinct entityid from wf_WorkItems where status ={0} and WorkflowName != 'Charge' and WorkItemName != '收货' and r.returnType<>40  and operatorid in ({1})) ", (int)ESP.Workflow.WorkItemStatus.Done, GetDelegateUser());

            if (!string.IsNullOrEmpty(txtKey.Text.Trim()))
            {
                whereStr += string.Format(" and ( r.ProjectCode like '%{0}%' or r.ReturnCode like '%{0}%' or r.RequestEmployeeName like '%{0}%' or r.PreFee like '%{0}%' or  r.PaymentTypeCode like '%{0}%' )", txtKey.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtBeginDate.Text.Trim()))
            {
                whereStr += string.Format(" and datediff(dd, cast('{0}' as datetime), r.CommitDate ) >= 0 ", txtBeginDate.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtEndDate.Text.Trim()))
            {
                whereStr += string.Format(" and datediff(dd, cast('{0}' as datetime), r.CommitDate ) <= 0 ", txtBeginDate.Text.Trim());
            }

            if (!string.IsNullOrEmpty(ddlBranch.SelectedValue) && !"0".Equals(ddlBranch.SelectedValue))
            {
                whereStr += string.Format(" and r.BranchCode = '{0}' ", ddlBranch.SelectedValue);
            }

            if (!string.IsNullOrEmpty(ddlType.SelectedValue) && !"-1".Equals(ddlType.SelectedValue))
            {
                whereStr += string.Format(" and r.returntype = '{0}' ", ddlType.SelectedValue);
            }
            DataSet ds = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetMajorAlreadyAuditList(whereStr);

            GridHist.DataSource = ds.Tables[0];
            GridHist.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindHistList();
        }

        protected void tab1_click(object sender, EventArgs e)
        {
            Response.Redirect("ExpenseAccountAuditList.aspx");
        }


        protected string getUrl(string webpage, string workitemid, string workMonth)
        {
            var sum = "<a href='" + webpage + "&workitemid=" + workitemid + "&BackUrl=ExpenseAccountAuditList.aspx'><img src='images/Audit.gif' /></a>";
            if (workMonth != "1.本次处理" && isFinances)
            {
                return "";
            }
            return sum;
        }

        protected string getPrintUrl(string returnID, string returnType)
        {
            var sum = "";
            if (Convert.ToInt32(returnType) != (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty && Convert.ToInt32(returnType) != (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic)
            {
                sum = "<a href='Print/ExpensePrint.aspx?expenseID=" + returnID + "' target='_blank'><img src='/images/Icon_Output.gif' /></a>";
            }
            else
            {
                sum = "<a href='Print/ThirdPartyPrint.aspx?expenseID=" + returnID + "' target='_blank'><img src='/images/Icon_Output.gif' /></a>";
            }
            return sum;
        }

        protected string getIsBatchInput(string returnID, decimal preFee, string workitemid, decimal confirmFee, string returntype, string workMonth)
        {
            bool isBatchAudit = true;

            if (Convert.ToInt32(returntype) == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccount)
            {
                isBatchAudit = true;
            }
            else
            {

                if (confirmFee > 0 && preFee != confirmFee)
                {
                    isBatchAudit = false;
                }
                else
                {
                    isBatchAudit = true;
                }
            }

            if (isBatchAudit && ("1.本次处理".Equals(workMonth) || !isFinances))
            {
                return "<input type=\"checkbox\" id=\"chkAudit\" name=\"chkAudit\" value='" + workitemid + "' OnClick='CalSelected(this.checked," + preFee + "," + returnID + ");'/>";
            }
            return "";
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
