using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.ExpenseAccount
{
    public partial class FinanceBatchAuditDetailEditByOpen : ESP.Web.UI.PageBase
    {
        ESP.Finance.Entity.ReturnInfo model = null;
        ESP.Finance.Entity.ExpenseAccountDetailInfo detail = null;
        ESP.Finance.SqlDataAccess.WorkItem workitem = null;
        ESP.Finance.Entity.ProjectInfo project = null;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["workitemid"]) && !string.IsNullOrEmpty(Request["detailid"]))
            {
                workitem = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowModel(int.Parse(Request["workitemid"]));
                model = ESP.Finance.BusinessLogic.ReturnManager.GetModel(workitem.EntityId);
                project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(model.ProjectID.Value);
                detail = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetModel(Convert.ToInt32(Request["detailid"]));
            }

            if (!IsPostBack)
            {
                if (workitem != null && model != null  && detail != null)
                {
                    BindInfo();
                    ShowPan();
                }
            }
        }

        protected void BindInfo()
        {

            labPNcode.Text = model.ReturnCode;
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
            else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty)
                labReturnType.Text = "第三方报销单";
            else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountWaitReceiving)
                labReturnType.Text = "借款冲销单";

            labPreFee.Text = model.PreFee.Value.ToString("0.00");
            
            //labMemo.Text = model.ReturnContent;



            labExpenseDate.Text = detail.ExpenseDate.Value.ToString("yyyy-MM-dd");
            labExpenseType.Text = ESP.Finance.BusinessLogic.ExpenseTypeManager.GetModel(detail.ExpenseType.Value).ExpenseType;
            //labExpenseDesc.Text = detail.ExpenseDesc;
            txtExpenseDesc.Text = detail.ExpenseDesc;
            labExpenseTypeNumber.Text = detail.ExpenseTypeNumber.Value.ToString("0");

            labExpenseMoney.Text = detail.ExpenseMoney.Value.ToString("0.00");
            txtExpenseMoney.Value = Convert.ToDouble(detail.ExpenseMoney.Value);

            ESP.Purchase.Entity.TypeInfo projectType = ESP.Purchase.BusinessLogic.TypeManager.GetModel(detail.CostDetailID.Value);
            if (projectType != null && !string.IsNullOrEmpty(projectType.typename))
                labProjectType.Text = projectType.typename;
            else
                labProjectType.Text = "OOP";


            

            if (detail.ExpenseType == 33)
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
            else if (detail.ExpenseType == 31)
            {
                labYear.Text = detail.PhoneYear.Value.ToString();
                labMonth.Text = detail.PhoneMonth.Value.ToString();
            }


            if (workitem.WebPage.IndexOf("step=fa") > 0 || workitem.WebPage.IndexOf("step=f1") > 0 || workitem.WebPage.IndexOf("step=f2") > 0 || workitem.WebPage.IndexOf("step=f3") > 0)
            {
                labExpenseMoney.Visible = false;
                txtExpenseMoney.Visible = true;

                btnSave.Visible = true;
            }
            else
            {
                labExpenseMoney.Visible = true;
                txtExpenseMoney.Visible = false;

                btnSave.Visible = false;
            }

        }

        protected void ShowPan()
        {
            if (detail.ExpenseType == 33)
            {
                panMealFee.Visible = true;
                panPhone.Visible = false;
            }
            else if (detail.ExpenseType == 31)
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            decimal totalMoney = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(model.ReturnID, detail.ID);
            bool isF3 = false;

            try
            {
                if (workitem.WebPage.IndexOf("step=f3") > 0)
                {
                    isF3 = true;
                }
                if ((totalMoney + Convert.ToDecimal(txtExpenseMoney.Value.Value)) > model.PreFee.Value && !isF3)
                {
                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('修改后报销总额不能大于修改前报销总额！');", true);
                    return;
                }
                else
                {
                    ESP.Finance.Entity.ExpenseAccountDetailHisInfo his = new ESP.Finance.Entity.ExpenseAccountDetailHisInfo();
                    his.ExpenseAccountDetailID = detail.ID;
                    his.ReturnID = model.ReturnID;
                    his.ModifyUserID = Convert.ToInt32(CurrentUser.SysID);
                    his.ModifyUserName = CurrentUser.Name;
                    his.ModifyDateTime = DateTime.Now;
                    his.ExpenseDescOld = detail.ExpenseDesc;
                    his.ExpenseMoneyOld = detail.ExpenseMoney;
                    his.CurrentPreFeeOld = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(model.ReturnID);

                    detail.ExpenseMoney = Convert.ToDecimal(txtExpenseMoney.Value.Value);
                    detail.ExpenseDesc = txtExpenseDesc.Text.Trim();
                    //ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.Update(detail);

                    //model.PreFee = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(model.ReturnID);
                    //ESP.Finance.BusinessLogic.ReturnManager.Update(model);


                    if (ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.UpdateDetailByFinance(model, detail, his))
                    {
                        string clientId = "ctl00_ContentPlaceHolder1_";
                        Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('修改成功！');window.location='FinanceBatchAuditEditByOpen.aspx?workitemid=" + Request["workitemid"] + "';opener.__doPostBack('" + clientId.Replace('_', '$') + "btnReLoad','');", true);
                        return;
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('修改失败，请重试！');window.location='FinanceBatchAuditEditByOpen.aspx?workitemid=" + Request["workitemid"] + "';", true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('修改失败，请重试！');window.location='FinanceBatchAuditEditByOpen.aspx?workitemid=" + Request["workitemid"] + "';", true);
                return;
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("FinanceBatchAuditEditByOpen.aspx?workitemid=" + Request["workitemid"]);
        }
    }
}
