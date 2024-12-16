using ESP.Finance.BusinessLogic;
using ESP.Finance.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.DeadLine
{
    public partial class DeadLineEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
            {
                DateTime currentdt = Convert.ToDateTime(this.txtEndDate.Text.Trim());
                DateTime currentdt2 = Convert.ToDateTime(this.txtEndDate2.Text.Trim());
                DateTime projectdt = Convert.ToDateTime(this.txtProjectDate.Text.Trim());

                int faDate = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ExpenseAuditDate"]);
                string term = " (DeadLineYear=@DeadLineYear and DeadLineMonth=@DeadLineMonth)";
                List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
                System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@DeadLineYear", System.Data.SqlDbType.Int, 4);
                p1.Value = currentdt.Year;
                paramlist.Add(p1);
                System.Data.SqlClient.SqlParameter p2 = new System.Data.SqlClient.SqlParameter("@DeadLineMonth", System.Data.SqlDbType.Int, 4);
                p2.Value = int.Parse(ddlMonth.SelectedValue);
                paramlist.Add(p2);


                IList<ESP.Finance.Entity.DeadLineInfo> deadlineList = ESP.Finance.BusinessLogic.DeadLineManager.GetList(term, paramlist);
                if (deadlineList.Count > 1)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('该月结帐日已经存在,无法完成操作!');", true);
                }
                else
                {
                    ESP.Finance.Entity.DeadLineInfo deadline = new ESP.Finance.Entity.DeadLineInfo();

                    deadline.CreateUserID = CurrentUserID;
                    deadline.CreateUserCode = CurrentUser.ITCode;
                    deadline.CreateUserName = CurrentUserCode;
                    deadline.CreateUserEmpName = CurrentUserName;

                    deadline.DeadLine = currentdt;
                    deadline.DeadLineYear = currentdt.Year;
                    deadline.DeadLineMonth = int.Parse(ddlMonth.SelectedValue);
                    deadline.DeadLineDay = 1;

                    deadline.ExpenseDeadLine = DateTime.Parse(txtExpDeadLine.Text);
                    deadline.ExpenseCommitDeadLine = currentdt;
                    DateTime auditDL = DateTime.Parse(txtAuditDeadLine.Text);
                    deadline.ExpenseAuditDeadLine = new DateTime(auditDL.Year, auditDL.Month, auditDL.Day, 23, 59, 59);

                    deadline.ProjectDeadLine = new DateTime(projectdt.Year, projectdt.Month, projectdt.Day, 23, 59, 59);
                    deadline.ProjectDeadLineYear = projectdt.Year;
                    deadline.ProjectDeadLineMonth = projectdt.Month;
                    deadline.ProjectDeadLineDay = projectdt.Day;


                    deadline.DeadLine2 = currentdt2;
                    deadline.DeadLineYear2 = currentdt2.Year;
                    deadline.DeadLineMonth2 = currentdt2.Month;
                    deadline.DeadLineDay2 = currentdt2.Day;
                    deadline.ExpenseDeadLine2 = DateTime.Parse(txtExpDeadLine2.Text);
                    deadline.ExpenseCommitDeadLine2 = currentdt2;
                    DateTime auditDL2 = DateTime.Parse(txtAuditDeadLine2.Text);
                    deadline.ExpenseAuditDeadLine2 = new DateTime(auditDL2.Year, auditDL2.Month, auditDL2.Day, 23, 59, 59);
                    deadline.SalaryDate = Convert.ToDateTime(this.txtSalary.Text.Trim());

                    int ret =  ESP.Finance.BusinessLogic.DeadLineManager.Add(deadline);

                    if (ret > 0)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('该月结帐日添加成功!');window.location='DeadLineList.aspx'", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('结帐日添加操作失败!');", true);
                    }
                }
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DeadLineList.aspx");
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
            {
                DateTime currentdt = Convert.ToDateTime(this.txtEndDate.Text.Trim());
                DateTime currentdt2 = Convert.ToDateTime(this.txtEndDate2.Text.Trim());



                int faDate = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ExpenseAuditDate"]);

                txtExpDeadLine.Text = ESP.Finance.Utilities.AddWorkDays.addWorkDays(currentdt, -5).ToString("yyyy-MM-dd");
                DateTime dtaudit = ESP.Finance.Utilities.AddWorkDays.addWorkDays(currentdt, faDate);
                var holidays = (new ESP.Administrative.BusinessLogic.HolidaysInfoManager()).GetOneYearHolidays(dtaudit.Year);

                while (holidays.Contains(dtaudit))
                {
                    dtaudit = dtaudit.AddDays(1);
                }

                txtAuditDeadLine.Text = new DateTime(dtaudit.Year, dtaudit.Month, dtaudit.Day).ToString("yyyy-MM-dd"); ;

                txtExpDeadLine2.Text = ESP.Finance.Utilities.AddWorkDays.addWorkDays(currentdt2, -5).ToString("yyyy-MM-dd");
                DateTime dtaudit2 = ESP.Finance.Utilities.AddWorkDays.addWorkDays(currentdt2, faDate);
                while (holidays.Contains(dtaudit2))
                {
                    dtaudit2 = dtaudit.AddDays(1);
                }
                txtAuditDeadLine2.Text = new DateTime(dtaudit2.Year, dtaudit2.Month, dtaudit2.Day).ToString("yyyy-MM-dd");

            }
        }
    }
}
