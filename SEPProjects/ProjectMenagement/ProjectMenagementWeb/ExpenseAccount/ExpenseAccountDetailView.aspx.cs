using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.ExpenseAccount
{
    public partial class ExpenseAccountDetailView : ESP.Web.UI.PageBase
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



            txtExpenseDate.Text = detail.ExpenseDate.Value.ToString("yyyy-MM-dd");
            labExpenseType.Text = ESP.Finance.BusinessLogic.ExpenseTypeManager.GetModel(detail.ExpenseType.Value).ExpenseType;
            //labExpenseDesc.Text = detail.ExpenseDesc;
            txtExpenseDesc.Text = detail.ExpenseDesc;
            labExpenseTypeNumber.Text = detail.ExpenseTypeNumber.Value.ToString("0");

            labExpenseMoney.Text = detail.ExpenseMoney.Value.ToString("0.00");
            txtMoney.Text = detail.ExpenseMoney.Value.ToString("0.00");

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
                ddlPhoneInvoice.SelectedValue = detail.PhoneInvoice.ToString();
                txtInvoiceNo.Text = detail.PhoneInvoiceNo;
            }


            if (workitem.WebPage.IndexOf("step=fa") > 0 || workitem.WebPage.IndexOf("step=f1") > 0 || workitem.WebPage.IndexOf("step=f2") > 0 || workitem.WebPage.IndexOf("step=f3") > 0 || workitem.WebPage.IndexOf("step=f4") > 0 )
            {
                labExpenseMoney.Visible = false;
                txtMoney.Visible = true;

                btnSave.Visible = true;
            }
            else
            {
                labExpenseMoney.Visible = true;
                txtMoney.Visible = false;

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
            //媒体预付申请到财务审批时，可以添加金额为负数的报销明细冲抵原金额
            if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia && Request["faadd"] == "1")
            {
                detail.ID = 0;
                detail.ExpenseMoney = Convert.ToDecimal(txtMoney.Text);
                detail.ExpenseDesc = txtExpenseDesc.Text.Trim();
                detail.ExpenseDate = DateTime.Parse(txtExpenseDate.Text);

                try
                {
                    if (ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.Add(detail)>0)
                    {
                        string requesturl = string.Format("&workitemid={0}&BackUrl={1}", Request["workitemid"], Request["BackUrl"]);
                        Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('添加成功！');window.location='" + workitem.WebPage + requesturl + "';", true);
                        return;
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('添加失败，请重试！');", true);
                        return;
                    }
                }
                catch
                {
                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('添加失败，请重试！');", true);
                    return;
                }

            }
            else
            {
                try
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

                    detail.ExpenseMoney = Convert.ToDecimal(txtMoney.Text);
                    detail.ExpenseDesc = txtExpenseDesc.Text.Trim();
                    detail.ExpenseDate = DateTime.Parse(txtExpenseDate.Text);
                    if (detail.ExpenseType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic)
                    {
                        detail.PhoneInvoice = int.Parse(ddlPhoneInvoice.SelectedValue);
                        detail.PhoneInvoiceNo = txtInvoiceNo.Text;


                        int invoiceCount = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.CheckPhoneInvoiceNo(detail.ID, detail.PhoneInvoiceNo);

                        if (detail.PhoneInvoice == 2)
                        {
                            if (string.IsNullOrEmpty(txtInvoiceNo.Text) || txtInvoiceNo.Text.Trim().Length != 8)
                            {
                                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('请输入正确的手机电子发票号！');", true);
                                return;
                            }

                            if (invoiceCount > 0)
                            {
                                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('您输入的手机电子发票号已经使用过！');", true);
                                return;
                            }

                        }
                    }

                    if (ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.UpdateDetailByFinance(model, detail, his))
                    {
                        string requesturl = string.Format("&workitemid={0}&BackUrl={1}", Request["workitemid"], Request["BackUrl"]);
                        Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('修改成功！');window.location='" + workitem.WebPage + requesturl + "';", true);
                        return;
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('修改失败，请重试！');", true);
                        return;
                    }

                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('修改失败，请重试！');", true);
                    return;
                }
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            string requesturl = string.Format("&workitemid={0}&BackUrl={1}", Request["workitemid"], Request["BackUrl"]);

            Response.Redirect(workitem.WebPage + requesturl);
        }
    }
}
