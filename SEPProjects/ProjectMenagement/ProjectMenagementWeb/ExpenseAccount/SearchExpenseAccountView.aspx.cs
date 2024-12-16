/************************************************************************\
 * 报销单显示页面(报销单查询模块中的)
 *      
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
    public partial class SearchExpenseAccountView : ESP.Web.UI.PageBase
    {
        ESP.Finance.Entity.ExpenseAccountExtendsInfo model = null;
      

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["id"]))
            {

                model = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkItemModel(Convert.ToInt32(Request["id"]));
                BindInfo();
            }

        }


        protected void BindInfo()
        {

            if (model != null)
            {
                BindList();

                labRequestUserName.Text = model.RequestEmployeeName;
                labRequestUserCode.Text = model.RequestUserCode;
                labRequestDate.Text = model.RequestDate.Value.ToString("yyyy-MM-dd");

                ESP.Finance.Entity.ProjectInfo project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(model.ProjectID.Value);

                txtproject_code1.Text = model.ProjectCode;
                //如果是GM项目，不需要FA审批
                if (model.ProjectCode.IndexOf("GM*") >= 0 || model.ProjectCode.IndexOf("*GM") >= 0 || model.ProjectCode.IndexOf(ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN) >= 0)
                {
                    txtproject_descripttion.Text = "";
                }
                else
                {
                    txtproject_descripttion.Text = project.BusinessDescription;
                }

                labMemo.Text = model.ReturnContent;

                labPreFee.Text = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(model.ReturnID).ToString();

                labDepartment.Text = model.DepartmentName;

                if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccount)
                    labReturnType.Text = "报销单";
                else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCashBorrow)
                    labReturnType.Text = "现金借款单";
                else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic)
                    labReturnType.Text = "支票/电汇付款单";
                else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountBusinessCard)
                    labReturnType.Text = "商务卡报销单";
                else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff)
                    labReturnType.Text = "PR现金借款冲销";
                

                List<ESP.Finance.Entity.ExpenseAuditDetailInfo> auditLogs = ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.GetList(" and ExpenseAuditID = " + model.ReturnID);
                foreach (ESP.Finance.Entity.ExpenseAuditDetailInfo log in auditLogs)
                {
                    ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(log.AuditorUserID.Value);
                    if (emp != null)
                    labSuggestion.Text += log.AuditorEmployeeName+"("+emp.FullNameEN+")" + ";&nbsp;&nbsp;&nbsp;" + log.Suggestion + ";&nbsp;&nbsp;&nbsp;" + ((model.RequestorID.Value == log.AuditorUserID.Value && log.AuditType.Value == 0) ? "提交" : ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Names[log.AuditeStatus.Value]) + ";&nbsp;&nbsp;&nbsp;" + log.AuditeDate.Value.ToString("yyyy-MM-dd") + "<br/>";
                }

            }

        }

        protected void BindList()
        {
            List<ESP.Finance.Entity.ExpenseAccountDetailInfo> list = null;
            if (model != null)
            {
                list = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(" and ReturnID = " + model.ReturnID);
            }
            else
            {
                list = new List<ESP.Finance.Entity.ExpenseAccountDetailInfo>();
            }

            gvG.DataSource = list;
            gvG.DataBind();
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("SearchExpenseAccountList.aspx");
        }

        

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex; 
            BindList();
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.ExpenseAccountDetailInfo detail = (ESP.Finance.Entity.ExpenseAccountDetailInfo)e.Row.DataItem;
                ESP.Purchase.Entity.TypeInfo projectType = ESP.Purchase.BusinessLogic.TypeManager.GetModel(detail.CostDetailID.Value);

                Label labCostDetailName = (Label)e.Row.FindControl("labCostDetailName");
                if (projectType != null && !string.IsNullOrEmpty(projectType.typename))
                    labCostDetailName.Text = projectType.typename;
                else
                    labCostDetailName.Text = "OOP";

                Label labExpenseTypeName = (Label)e.Row.FindControl("labExpenseTypeName");
                string expenseTypeName = ESP.Finance.BusinessLogic.ExpenseTypeManager.GetModel(detail.ExpenseType.Value).ExpenseType;
                if (detail.ExpenseType.Value == 33)
                {
                    labExpenseTypeName.Text = expenseTypeName + "(";
                    labExpenseTypeName.Text += detail.MealFeeMorning != null && detail.MealFeeMorning == 1 ? "早餐 " : "";
                    labExpenseTypeName.Text += detail.MealFeeNoon != null && detail.MealFeeNoon == 1 ? "午餐 " : "";
                    labExpenseTypeName.Text += detail.MealFeeNight != null && detail.MealFeeNight == 1 ? "晚餐" : "";
                    labExpenseTypeName.Text += ")";
                }
                else if (detail.ExpenseType.Value == 31)
                {
                    labExpenseTypeName.Text = expenseTypeName + "(" + detail.PhoneYear + "年" + detail.PhoneMonth + "月)";
                }
                else
                {
                    labExpenseTypeName.Text = expenseTypeName;
                }
            }
        }

        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modify")
            {
                string detailid = e.CommandArgument.ToString();
                Response.Redirect("SearchExpenseAccountDetailView.aspx?id=" + model.ReturnID + "&detailid=" + detailid);
            }
        }
    }
}
