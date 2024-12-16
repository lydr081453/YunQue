/************************************************************************\
 * 报销单明细显示页面(查询功能中的)
 *      
 *
\************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.ExpenseAccount
{
    public partial class SearchExpenseAccountDetailView : System.Web.UI.Page
    {
        ESP.Finance.Entity.ReturnInfo model = null;
        ESP.Finance.Entity.ExpenseAccountDetailInfo detail = null;
        ESP.Finance.Entity.ProjectInfo project = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            BindInfo();
            ShowPan();
        }

        protected void BindInfo()
        {
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                model = ESP.Finance.BusinessLogic.ReturnManager.GetModel(Convert.ToInt32(Request["id"]));
                project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(model.ProjectID.Value);
                labRequestUserName.Text = model.RequestEmployeeName;
                labRequestUserCode.Text = model.RequestUserCode;
                labRequestDate.Text = model.RequestDate.Value.ToString("yyyy-MM-dd");
                labProjectCode.Text = model.ProjectCode;
                //如果是GM项目，不需要FA审批
                if (model.ProjectCode.IndexOf("GM*") >= 0 || model.ProjectCode.IndexOf("*GM") >= 0 || model.ProjectCode.IndexOf(ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN) >= 0)
                {
                    labProjectName.Text = "";
                }
                else
                {
                    labProjectName.Text = project.BusinessDescription;
                }

                labGroup.Text = model.DepartmentName;



                labPreFee.Text = model.PreFee.Value.ToString("0.00");

                labMemo.Text = model.ReturnContent;

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

                if (!string.IsNullOrEmpty(Request["detailid"]))
                {
                    detail = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetModel(Convert.ToInt32(Request["detailid"]));

                    labExpenseDate.Text = detail.ExpenseDate.Value.ToString("yyyy-MM-dd");
                    labExpenseType.Text = ESP.Finance.BusinessLogic.ExpenseTypeManager.GetModel(detail.ExpenseType.Value).ExpenseType;
                    labExpenseDesc.Text = detail.ExpenseDesc;
                    labExpenseMoney.Text = detail.ExpenseMoney.Value.ToString();

                    ESP.Purchase.Entity.TypeInfo projectType = ESP.Purchase.BusinessLogic.TypeManager.GetModel(detail.CostDetailID.Value);
                    if (projectType != null && !string.IsNullOrEmpty(projectType.typename))
                        labProjectType.Text = projectType.typename;
                    else
                        labProjectType.Text = "OOP";


                   

                    if (detail.ExpenseType == 2)
                    {
                        if (detail.MealFeeMorning != null && detail.MealFeeMorning == 1)
                        {
                            chkMealFee1.Checked = true;
                        }
                        else
                        {
                            chkMealFee1.Checked = false;
                        }

                        if (detail.MealFeeNoon != null && detail.MealFeeNoon == 1)
                        {
                            chkMealFee2.Checked = true;
                        }
                        else
                        {
                            chkMealFee2.Checked = false;
                        }

                        if (detail.MealFeeNight != null && detail.MealFeeNight == 1)
                        {
                            chkMealFee3.Checked = true;
                        }
                        else
                        {
                            chkMealFee3.Checked = false;
                        }
                    }
                    else if (detail.ExpenseType == 7)
                    {
                        labYear.Text = detail.PhoneYear.Value.ToString();
                        labMonth.Text = detail.PhoneMonth.Value.ToString();
                    }
                }

            }
        }

        protected void ShowPan()
        {
            if (detail.ExpenseType == 2)
            {
                panMealFee.Visible = true;
                panPhone.Visible = false;
            }
            else if (detail.ExpenseType == 7)
            {
                panMealFee.Visible = false;
                panPhone.Visible = true;
            }
            else
            {
                panMealFee.Visible = false;
                panPhone.Visible = false;
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("SearchExpenseAccountView.aspx?id=" + Request["id"]);
        }
    }
}
