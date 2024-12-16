using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.Entity;
using ESP.HumanResource.BusinessLogic;
using System.Net.Mail;
using ESP.HumanResource.Common;


namespace FinanceWeb.Return
{
    public partial class SalaryDetail : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                ESP.HumanResource.Entity.EmployeeBaseInfo emp = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(UserID);
                int year = int.Parse(Request["year"]);
                int month = int.Parse(Request["month"]);
                string pwd = Request["pwd"];

                 this.lblEmail.Text = emp.InternalEmail;
                lblNameCN.Text = emp.FullNameCN + "(" + emp.Code + ")";

                ESP.Finance.Entity.SalaryInfo salaryModel = ESP.Finance.BusinessLogic.SalaryManager.GetModel(emp.UserID, year, month);

                if (!string.IsNullOrEmpty(salaryModel.BranchCode))
                {
                    ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(salaryModel.BranchCode);
                    if (branchModel != null)
                        lblBranch.Text = branchModel.BranchName;
                }

                if (salaryModel != null && (!string.IsNullOrEmpty(salaryModel.EmailPassword)) && salaryModel.EmailPassword == pwd)
                {
                    if (salaryModel.EmailSendTime.AddMinutes(5) >= DateTime.Now)
                    {
                        lblDate.Text = salaryModel.SalaryYear.ToString() + "年" + salaryModel.SalaryMonth + "月";

                        lblSalaryBase.Text = string.Format("{0:N2}", decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(salaryModel.SalaryBased)));
                        lblSalaryPerformance.Text = string.Format("{0:N2}", decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(salaryModel.SalaryPerformance)));

                        lblAffairCut.Text = string.Format("{0:N2}", decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(salaryModel.AffairCut)));
                        lblSickCut.Text = string.Format("{0:N2}", decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(salaryModel.SickCut)));
                        lblLateCut.Text = string.Format("{0:N2}", decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(salaryModel.LateCut)));
                        lblAbsenceCut.Text = string.Format("{0:N2}", decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(salaryModel.AbsenceCut)));
                        lblClockCut.Text = string.Format("{0:N2}", decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(salaryModel.ClockCut)));
                        lblKaoqinTotal.Text = string.Format("{0:N2}", decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(salaryModel.KaoqinTotal)));

                        lblOtherCut.Text = string.Format("{0:N2}", decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(salaryModel.OtherCut)));
                        lblOtherIncome.Text = string.Format("{0:N2}", decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(salaryModel.OtherIncome)));
                        lblIncome.Text = string.Format("{0:N2}", decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(salaryModel.Income)));
                       
                        lblHouse.Text = string.Format("{0:N2}", decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(salaryModel.Housing)));
                        lblRetirement.Text = string.Format("{0:N2}", decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(salaryModel.Retirement)));
                        lblMedical.Text = string.Format("{0:N2}", decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(salaryModel.Medical)));
                        lblUnEmp.Text = string.Format("{0:N2}", decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(salaryModel.UnEmp)));
                        lblInsuranceTotal.Text =string.Format("{0:N2}",  decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(salaryModel.InsuranceTotal)));
                        
                        lblSalaryPreTax.Text = string.Format("{0:N2}", decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(salaryModel.SalaryPretax)));
                        lblTax3.Text = string.Format("{0:N2}", decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(salaryModel.Tax3)));
                        lblTaxedCut.Text = string.Format("{0:N2}", decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(salaryModel.TaxedCut)));
                        lblSalaryPaid.Text = string.Format("{0:N2}", decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(salaryModel.SalaryPaid)));
                        lblRemark.Text = salaryModel.Remark;

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('验证码已经失效，请重新发送验证码！');", true);
                        return;
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('验证码有误，请检查！');", true);
                    return;
                }
               

            }
        }
    }
}