using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Common;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace AdministrativeWeb.Statistic
{
    public partial class OverTimeSearch : ESP.Web.UI.PageBase
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

        /// <summary>
        /// 绑定OT人员信息
        /// </summary>
        protected void BindInfo()
        {
            StringBuilder strBuilder = new StringBuilder();
            List<SqlParameter> paramlist = new List<SqlParameter>();
            // 年份
            int year = int.Parse(drpYear.SelectedValue);
            // 月份
            int month = int.Parse(drpMonth.SelectedValue);
            // 日期
            int day = 0;
            // 用户姓名
            if (!string.IsNullOrEmpty(txtUserName.Text))
            {
                strBuilder.Append(" AND s.EmployeeName like '%'+@EmployeeName+'%' ");
                paramlist.Add(new SqlParameter("@EmployeeName", txtUserName.Text.Trim()));
            }
            // 员工编号
            if (!string.IsNullOrEmpty(txtUserCode.Text))
            {
                strBuilder.Append(" AND s.UserCode like '%'+@UserCode+'%' ");
                paramlist.Add(new SqlParameter("@UserCode", txtUserCode.Text.Trim()));
            }
            // 时间
            if (PickerFrom1.SelectedDate != null && PickerFrom1.SelectedDate != DateTime.MinValue)
            {
                year = PickerFrom1.SelectedDate.Year;
                month = PickerFrom1.SelectedDate.Month;
                day = PickerFrom1.SelectedDate.Day;
            }
            // 状态
            if (drpState.SelectedValue != "0")
            {
                strBuilder.Append(" AND s.ApproveState=@ApproveState ");
                paramlist.Add(new SqlParameter("@ApproveState", drpState.SelectedValue));
            }
            SingleOvertimeManager singleOvertimeManager = new SingleOvertimeManager();
            DataSet ds = singleOvertimeManager.GetModelList(UserID, year, month, day, strBuilder.ToString(), paramlist);
            Grid1.DataSource = ds;
            Grid1.DataBind();
        }

        /// <summary>
        /// 查询OT统计信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            BindInfo();
        }

        #region 页面调用的方法
        /// <summary>
        /// 获得审批状态信息
        /// </summary>
        /// <param name="approveState">审批状态</param>
        /// <returns>返回审批状态信息</returns>
        public string GetApproveState(string approveState)
        {
            if (approveState == Status.OverTimeState_NotSubmit.ToString())
                return "未提交";
            else if (approveState == Status.OverTimeState_WaitDirector.ToString())
                return "等待TeamLeader审批";
            else if (approveState == Status.OverTimeState_Passed.ToString())
                return "审批通过";
            else
                return "未提交";
        }

        /// <summary>
        /// 获得审批人信息
        /// </summary>
        /// <param name="approveState">审批状态</param>
        /// <param name="approveName">审批人姓名</param>
        /// <returns>返回审批人姓名，如果是未提交状态则返回一个空</returns>
        public string GetApproveName(string approveState, string approveName)
        {
            if (approveState == Status.OverTimeState_NotSubmit.ToString())
                return "";
            else 
                return approveName;
        }
        #endregion
    }
}
