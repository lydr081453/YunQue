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
    public partial class SalaryView : ESP.Web.UI.PageBase
    {
        private ESP.HumanResource.Entity.EmployeeBaseInfo emp;
        protected void Page_Load(object sender, EventArgs e)
        {
            emp = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(UserID);
            if (!IsPostBack)
            {
                BindDDL();
                this.lblEmail.Text = emp.InternalEmail;
                lblNameCN.Text = emp.FullNameCN + "(" + emp.Code + ")";

            }
        }

        private void BindDDL()
        {
            int year = DateTime.Now.Year;

            ddlYear.Items.Add(new ListItem((year - 1).ToString(), (year - 1).ToString()));
            ddlYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            ddlYear.SelectedValue = year.ToString();
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string pwd = this.GetRandom();
            int ret = ESP.Finance.BusinessLogic.SalaryManager.UpdatePassword(UserID, int.Parse(ddlYear.SelectedValue), 0, pwd);
            if (ret >= 1)
            {
                SendMail(pwd);
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('验证码已经发送到您的公司邮箱，请使用验证码进行检索！');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('未找到薪资信息，请稍后再试！');", true);
                return;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            search();
        }

        private void search()
        {
            this.gvG.DataSource = null;
            this.gvG.DataBind();

            if (string.IsNullOrEmpty(txtPwd.Text))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请输入验证码！');", true);
                return;
            }
            string strwhere = string.Empty;
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();

            strwhere += " ((salaryYear=@salaryYear and salarymonth in(1,2,3,4,5,6,7,8,9,10,11,12)) or (salaryYear=@salaryYearLast and salarymonth=13)) and userid=@userid ";

            paramlist.Add(new System.Data.SqlClient.SqlParameter("@salaryYear", this.ddlYear.SelectedValue));
            paramlist.Add(new System.Data.SqlClient.SqlParameter("@salaryYearLast", int.Parse(this.ddlYear.SelectedValue) - 1));
            paramlist.Add(new System.Data.SqlClient.SqlParameter("@userid", CurrentUserID));

            var salaryList = ESP.Finance.BusinessLogic.SalaryManager.GetList(strwhere, paramlist);
            if (salaryList != null && salaryList.Count > 0)
            {
                if (salaryList[0] != null && (!string.IsNullOrEmpty(salaryList[0].EmailPassword)) && salaryList[0].EmailPassword == this.txtPwd.Text.Trim())
                {
                    if (salaryList[0].EmailSendTime.AddMinutes(5) >= DateTime.Now)
                    {
                        this.gvG.DataSource = salaryList;
                        this.gvG.DataBind();
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

        private string GetRandom()
        {
            Random random = new Random();
            string str = string.Empty;

            char[] ch = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            for (int i = 0; i < 6; i++)
            {
                char num = ch[random.Next(ch.Length)];
                str += num;
            }
            return str;
        }

        private void SendMail(string pwd)
        {
            string recipientAddress = emp.InternalEmail;
            string body = "<html><body><p>您于" + DateTime.Now.ToString() + "查询个人薪资信息，验证码将在5分钟后失效。您的验证码是：</p><font style=\"color:red;font-size:xx-large;\">" + pwd + "</font></body></html>";
            SendMailHelper.SendMail("个人薪资信息", recipientAddress, body, null);
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ESP.Finance.Entity.SalaryInfo model = (ESP.Finance.Entity.SalaryInfo)e.Row.DataItem;
               if (e.Row.RowType == DataControlRowType.DataRow)
               {
                   Label lblDate = ((Label)e.Row.FindControl("lblDate"));
                   Label lblSalaryBased = ((Label)e.Row.FindControl("lblSalaryBased"));
                   Label lblSalaryPerformance = ((Label)e.Row.FindControl("lblSalaryPerformance"));
                   Label lblKaoqinTotal = ((Label)e.Row.FindControl("lblKaoqinTotal"));
                   
                   Label lblIncome = ((Label)e.Row.FindControl("lblIncome"));
                   Label lblInsuranceTotal = ((Label)e.Row.FindControl("lblInsuranceTotal"));

                   Label lblSalaryPretax = ((Label)e.Row.FindControl("lblSalaryPretax"));

                   Label lblTaxedCut = ((Label)e.Row.FindControl("lblTaxedCut"));
                    Label lblTax3 = ((Label)e.Row.FindControl("lblTax3"));
                   Label lblSalaryPaid = ((Label)e.Row.FindControl("lblSalaryPaid"));

                   lblDate.Text = model.SalaryYear + "年" + model.SalaryMonth + "月";
                   lblSalaryBased.Text =  string.Format("{0:N2}", decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(model.SalaryBased)));
                   lblSalaryPerformance.Text =  string.Format("{0:N2}", decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(model.SalaryPerformance)));
                   lblKaoqinTotal.Text =  string.Format("{0:N2}", decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(model.KaoqinTotal)));

                   lblIncome.Text =  string.Format("{0:N2}", decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(model.Income)));
                   lblInsuranceTotal.Text =  string.Format("{0:N2}", decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(model.InsuranceTotal)));
                   lblSalaryPretax.Text = string.Format("{0:N2}", decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(model.SalaryPretax)));
                   lblTaxedCut.Text =  string.Format("{0:N2}", decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(model.TaxedCut)));

                   lblTax3.Text = string.Format("{0:N2}", decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(model.Tax3)));
                   
                   lblSalaryPaid.Text = string.Format("{0:N2}", decimal.Parse(ESP.Salary.Utility.DESEncrypt.Decode(model.SalaryPaid)));


               }
        }
  
    }
}