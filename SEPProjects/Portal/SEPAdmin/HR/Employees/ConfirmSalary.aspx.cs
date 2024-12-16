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

namespace SEPAdmin.HR.Employees
{
    public partial class ConfirmSalary : ESP.Web.UI.PageBase
    {
        private ESP.HumanResource.Entity.EmployeeBaseInfo emp;
        protected void Page_Load(object sender, EventArgs e)
        {
            emp = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(UserID);
            this.lblHome.Visible = false;
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

            ddlYear.SelectedValue = (year - 1).ToString();

            ListItem m = new ListItem();
            m.Text = "13月";
            m.Value = "13";
            ddlMonth.Items.Add(m);

            ddlMonth.SelectedValue = "13";

        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            this.lblError.Text = "";
            this.lblHome.Visible = false;
            ESP.HumanResource.BusinessLogic.SalaryConfirmManager manager = new SalaryConfirmManager();

            bool isExist = manager.Exists(UserID, int.Parse(ddlYear.SelectedValue), int.Parse(ddlMonth.SelectedValue));
            if (isExist)
            {
                this.lblError.Style.Add("color", "red");
                this.lblError.Text = "您已经提交过薪资发放方式，无法重复操作！";
                this.lblHome.Visible = true;
                return;
            }
            ESP.HumanResource.Entity.SalaryConfirmInfo confirm = new SalaryConfirmInfo();
            confirm.UserId = UserID;
            confirm.ConfirmTime = DateTime.Now;
            confirm.SalaryType = int.Parse(this.ddlType.SelectedValue);
            confirm.SalaryYear = DateTime.Now.Year - 1;
            confirm.SalaryMonth = int.Parse(ddlMonth.SelectedValue);
            confirm.IPAddress = HttpContext.Current.Request.UserHostAddress;
            int ret = manager.Add(confirm);
            if (ret >= 1)
            {
                this.lblError.Style.Add("color", "green");
                this.lblError.Text = "薪资发放方式提交成功！";
                this.lblHome.Visible = true;
            }
            else
            {
                this.lblError.Style.Add("color", "red");
                this.lblError.Text = "薪资发放方式提交失败，请联系管理员！";
                return;
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            this.lblError.Text = "";
            this.lblHome.Visible = false;

            string pwd = this.GetRandom();
            int ret = ESP.Finance.BusinessLogic.SalaryManager.UpdatePassword(UserID, int.Parse(ddlYear.SelectedValue), int.Parse(ddlMonth.SelectedValue), pwd);
            if (ret == 1)
            {
                SendMail(pwd);
                this.lblError.Style.Add("color", "green");
                this.lblError.Text = "验证码已经发送到您的公司邮箱，请使用验证码进行检索！";
            }
            else
            {
                this.lblError.Style.Add("color", "red");
                this.lblError.Text = "未找到月度薪资信息，请稍后再试！";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.lblError.Text = "";
            this.lblHome.Visible = false;
            this.lblSalary.Text = string.Empty;
            this.lblInsurance.Text = string.Empty;
            ESP.Finance.Entity.SalaryInfo salaryModel = ESP.Finance.BusinessLogic.SalaryManager.GetModel(UserID, int.Parse(ddlYear.SelectedValue), int.Parse(ddlMonth.SelectedValue));

            if (salaryModel != null && (!string.IsNullOrEmpty(salaryModel.EmailPassword)) && salaryModel.EmailPassword == this.txtPwd.Text.Trim())
            {
                if (salaryModel.EmailSendTime.AddMinutes(5) >= DateTime.Now)
                {
                    this.lblSalary.Text = ESP.Salary.Utility.DESEncrypt.Decode(salaryModel.SalaryTotal);
                    this.lblInsurance.Text = ESP.Salary.Utility.DESEncrypt.Decode(salaryModel.InsuranceTotal);
                }
                else
                {
                    this.lblError.Style.Add("color", "red");
                    this.lblError.Text = "验证码已经失效，请重新发送验证码！";
                    return;
                }
            }
            else
            {
                this.lblError.Style.Add("color", "red");
                this.lblError.Text = "验证码有误，请检查！";
                return;
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
            string body = "<html><body><p>您于" + DateTime.Now.ToString() + "查询个人月度薪资信息，验证码将在5分钟后失效。您的验证码是：</p><font style=\"color:red;font-size:xx-large;\">" + pwd + "</font></body></html>";
            SendMailHelper.SendMail("年度13薪发放方式登记", recipientAddress, body, null);
        }
 
    }
}