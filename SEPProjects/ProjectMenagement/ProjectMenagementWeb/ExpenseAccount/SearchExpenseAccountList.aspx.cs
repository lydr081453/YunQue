/************************************************************************\
 * 报销单查询模块  查询出当前登录人所有下级员工及本人的报销单
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
    public partial class SearchExpenseAccountList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindHistList();
            }
        }

        private void bindHistList()
        {
            string whereStr = "";

            whereStr += string.Format(" and r.returnid in(select distinct entityid from wf_WorkItems where status ={0}  and operatorid in ({1})) ", (int)ESP.Workflow.WorkItemStatus.Done, GetDelegateUser());

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

            /*
            //获得当前登录用户的所有下级员工
            ESP.Administrative.BusinessLogic.OperationAuditManageManager operationManager = new ESP.Administrative.BusinessLogic.OperationAuditManageManager();
            List<ESP.Framework.Entity.EmployeeInfo> empList = operationManager.GetAllSubordinates(int.Parse(CurrentUser.SysID));
            string empIDs = CurrentUser.SysID + ",";
            foreach (ESP.Framework.Entity.EmployeeInfo emp in empList)
            {
                if (emp != null)
                {
                    empIDs += emp.UserID.ToString() + ",";
                }
            }

            //查询所有下级员工包括自己的报销单
            if (!string.IsNullOrEmpty(empIDs))
            {
                whereStr += string.Format(" and r.RequestorID in ({0}) ", empIDs.TrimEnd(','));
            }
            */
            DataSet ds = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetAlreadyAuditDetailList(whereStr);

            GridHist.DataSource = ds.Tables[0];
            GridHist.DataBind();

            GridHist.GroupBy = "RequestEmployeeName ASC";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindHistList();
        }

        protected string getPrintUrl(string returnID, string returnType)
        {
            var sum = "";
            if (Convert.ToInt32(returnType) != (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty)
            {
                sum = "<a href='Print/ExpensePrint.aspx?expenseID=" + returnID + "' target='_blank'><img src='/images/Icon_Output.gif' /></a>";
            }
            else
            {
                sum = "<a href='Print/ThirdPartyPrint.aspx?expenseID=" + returnID + "' target='_blank'><img src='/images/Icon_Output.gif' /></a>";
            }
            return sum;
        }

        protected string getCostDetailName(string costDetailID)
        {
            ESP.Purchase.Entity.TypeInfo projectType = ESP.Purchase.BusinessLogic.TypeManager.GetModel(Convert.ToInt32(costDetailID));

            if (projectType != null && !string.IsNullOrEmpty(projectType.typename))
                return projectType.typename;
            else
                return "OOP";
        }

        protected string getExpenseTypeName(string expenseType,string detailid)
        {
            ESP.Finance.Entity.ExpenseAccountDetailInfo detail = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetModel(Convert.ToInt32(detailid));
            ESP.Finance.Entity.ExpenseTypeInfo type = ESP.Finance.BusinessLogic.ExpenseTypeManager.GetModel(Convert.ToInt32(expenseType));
            string expenseTypeName = "";

            if (type != null)
                expenseTypeName = type.ExpenseType;
            else
                expenseTypeName = "";

            string name = "";
            if (Convert.ToInt32(expenseType) == 33)
            {
                name = expenseTypeName + "(";
                name += detail.MealFeeMorning != null && detail.MealFeeMorning == 1 ? "早餐 " : "";
                name += detail.MealFeeNoon != null && detail.MealFeeNoon == 1 ? "午餐 " : "";
                name += detail.MealFeeNight != null && detail.MealFeeNight == 1 ? "晚餐" : "";
                name += ")";
            }
            else if (Convert.ToInt32(expenseType) == 31)
            {
                name = expenseTypeName + "(" + detail.PhoneYear + "年" + detail.PhoneMonth + "月)";
            }
            else
            {
                name = expenseTypeName;
            }
            return name;
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

        /*
        private void bindList()
        {
            string conditionStr = string.Empty;
            conditionStr += string.Format(" ReturnType in ({0},{1},{2},{3},{3}) ", (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccount, (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic, (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCashBorrow, (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountBusinessCard, (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff);
            if (!string.IsNullOrEmpty(txtKey.Text.Trim()))
            {
                conditionStr += string.Format(" and ProjectCode + ReturnCode +  RequestEmployeeName +  CAST(PreFee AS nvarchar)  like '%{0}%' ", txtKey.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtBeginDate.Text.Trim()))
            {
                conditionStr += string.Format(" and '{0}' <= Convert(Char(10),RequestDate,120) ", txtBeginDate.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtEndDate.Text.Trim()))
            {
                conditionStr += string.Format(" and '{0}' >= Convert(Char(10),RequestDate,120) ", txtEndDate.Text.Trim());
            }
            if (!ddlStatus.SelectedValue.Equals("-1"))
            { 
                
            }

            //获得当前登录用户的所有下级员工
            ESP.Administrative.BusinessLogic.OperationAuditManageManager operationManager = new ESP.Administrative.BusinessLogic.OperationAuditManageManager();
            List<ESP.Framework.Entity.EmployeeInfo> empList = operationManager.GetAllSubordinates(int.Parse(CurrentUser.SysID));
            string empIDs = CurrentUser.SysID + ",";
            foreach (ESP.Framework.Entity.EmployeeInfo emp in empList)
            {
                if (emp != null)
                {
                    empIDs += emp.UserID.ToString() + ",";
                }
            }

            //查询所有下级员工包括自己的报销单
            if (!string.IsNullOrEmpty(empIDs))
            {
                conditionStr += string.Format(" and RequestorID in ({0}) ", empIDs.TrimEnd(','));
            }

            List<ESP.Finance.Entity.ReturnInfo> list = (List<ESP.Finance.Entity.ReturnInfo>)ESP.Finance.BusinessLogic.ReturnManager.GetList(conditionStr);
            this.gvG.DataSource = list;
            this.gvG.DataBind();
        }
        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindList();
        }

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            bindList();
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.ReturnInfo model = (ESP.Finance.Entity.ReturnInfo)e.Row.DataItem;
                ESP.Finance.Entity.ProjectInfo project = ESP.Finance.BusinessLogic.ProjectManager.GetModel(model.ProjectID.Value);

                Label lblReturnType = (Label)e.Row.FindControl("lblReturnType");

                if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccount)
                    lblReturnType.Text = "报销单";
                else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCashBorrow)
                    lblReturnType.Text = "现金借款单";
                else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic)
                    lblReturnType.Text = "支票/电汇付款单";
                else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountBusinessCard)
                    lblReturnType.Text = "商务卡报销单";
                else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff)
                    lblReturnType.Text = "PR现金借款冲销";

                Label labProjectName = (Label)e.Row.FindControl("labProjectName");
                if (project != null)
                    labProjectName.Text = project.BusinessDescription;

                HyperLink hylEdit = (HyperLink)e.Row.FindControl("hylEdit");
                hylEdit.NavigateUrl = "SearchExpenseAccountView.aspx?id=" + model.ReturnID;

                HyperLink hylPrint = (HyperLink)e.Row.FindControl("hylPrint");
                if (hylPrint != null)
                {
                    hylPrint.Target = "_blank";
                    hylPrint.NavigateUrl = "Print/ExpensePrint.aspx?expenseID=" + model.ReturnID;
                }
                if (model.ReturnStatus >= (int)PaymentStatus.Submit)
                {
                    hylPrint.Visible = true;
                }
                else
                {
                    hylPrint.Visible = false;
                }
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                lblStatus.Text = ESP.Finance.Utility.ExpenseStatusName.GetExpenseStatusName(model.ReturnStatus.Value);

                Label labRequestUserName = (Label)e.Row.FindControl("labRequestUserName");
                labRequestUserName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(model.RequestorID.Value) + "');");
                
            }
        }

        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            
            if (e.CommandName == "Del")
            {
                
            }
        }
        */

    }
}
