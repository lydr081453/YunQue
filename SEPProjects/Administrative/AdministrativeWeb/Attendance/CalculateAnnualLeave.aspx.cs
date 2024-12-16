using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdministrativeWeb.Attendance
{
    public partial class CalculateAnnualLeave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            drpDate();
        }

        /// <summary>
        /// 绑定下拉列表中的日期和时间内容
        /// </summary>
        protected void drpDate()
        {
            int year = DateTime.Now.Year - 5;
            for (int i = 0; i <= 10; i++)
            {
                drpYear.Items.Insert(i, new ListItem((year + i).ToString(), (year + i).ToString()));
            }
            drpYear.SelectedValue = DateTime.Now.Year.ToString();
        }

        /// <summary>
        /// 计算用户的年假
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCalculateAnnual_Click(object sender, EventArgs e)
        {
            ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetAllList();
        }
    }
}