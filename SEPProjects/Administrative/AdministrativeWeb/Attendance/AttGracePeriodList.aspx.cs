using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Entity;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AdministrativeWeb.Attendance
{
    public partial class AttGracePeriodList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                drpDate();
                BindInfo();
            }
        }

        /// <summary>
        /// 绑定下拉列表中的日期和时间内容
        /// </summary>
        protected void drpDate()
        {
            int year = DateTime.Now.Year - 10;
            for (int i = 0; i <= 10; i++)
            {
                drpYear.Items.Insert(i, new ListItem((year + i).ToString(), (year + i).ToString()));
            }

            drpYear.SelectedValue = DateTime.Now.Year.ToString();

            for (int i = 1; i <= 12; i++)
            {
                drpMonth.Items.Insert(i - 1, new ListItem((i).ToString("00"), (i).ToString("00")));
            }

            if (10 < DateTime.Now.Day)
            {
                drpMonth.SelectedValue = DateTime.Now.Month.ToString("00");
            }
            else if (1 != DateTime.Now.Month)
            {
                drpMonth.SelectedValue = (DateTime.Now.Month - 1).ToString("00");
            }
            else
            {
                drpMonth.SelectedValue = (DateTime.Now.Month - 1).ToString("00");
                drpYear.SelectedValue = (DateTime.Now.Year - 1).ToString();
            }
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            BindInfo();
        }

        /// <summary>
        /// 查询当前用户所添加的打卡记录信息
        /// </summary>
        protected void BindInfo()
        {
            List<SqlParameter> parameterList = new List<SqlParameter>();
            // 查询字符串
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" OperatorID='" + UserID + "'");
            if (!string.IsNullOrEmpty(txtUserName.Text))
            {
                strBuilder.Append(" AND EmployeeName LIKE '%'+@EmployeeName+'%' ");
                parameterList.Add(new SqlParameter("@EmployeeName", txtUserName.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtUserCode.Text))
            {
                strBuilder.Append(" AND UserCode LIKE '%'+@UserCode+'%' ");
                parameterList.Add(new SqlParameter("@UserCode", txtUserCode.Text.Trim()));
            }

            int year = int.Parse(drpYear.SelectedValue);
            int month = int.Parse(drpMonth.SelectedValue);
            DateTime datetime1 = new DateTime(year, month, 1, 0, 0, 0);
            DateTime datetime2 = new DateTime(year, month, DateTime.DaysInMonth(year, month), 23, 59, 59);
            strBuilder.Append(" AND ((BeginTime BETWEEN '" + datetime1.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + datetime2.ToString("yyyy-MM-dd HH:mm:ss") + "') ");
            strBuilder.Append(" OR   (EndTime   BETWEEN '" + datetime1.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + datetime2.ToString("yyyy-MM-dd HH:mm:ss") + "')) ");
            AttGracePeriodManager manager = new AttGracePeriodManager();
            DataSet ds = manager.GetList(strBuilder.ToString());
            Grid1.DataSource = ds;
            Grid1.DataBind();
        }

        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("AttGracePeriodEdit.aspx");
        }
    }
}