using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.Entity;
using ESP.Administrative.BusinessLogic;
using ESP.Framework.Entity;
using ESP.Administrative.Common;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Text;
using ESP.Framework.BusinessLogic;
using ComponentArt.Web.UI;
using System.Net.Mail;

namespace AdministrativeWeb.Statistic
{
    public partial class LateInfoSearch : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !Grid1.CausedCallback)
            {  
                if (!string.IsNullOrEmpty(Request["userid"]) && !string.IsNullOrEmpty(Request["flag"]) && Request["flag"] == "1")
                {
                    this.SendRemindMail(int.Parse(Request["userid"]));
                }
                drpDate();
                LoadDepartmentInfo();
                Grid1.DataSource = BindInfo();
                Grid1.DataBind();
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

        #region 变量定义 注释掉了
        ///// <summary>
        ///// 用户信息
        ///// </summary>
        //IList<ESP.Framework.Entity.EmployeeInfo> employeesList = null;

        ///// <summary>
        ///// 用户打卡记录信息
        ///// </summary>
        //Dictionary<int, Dictionary<long, DateTime>> clockInDic = null;

        ///// <summary>
        ///// OT单信息
        ///// </summary>
        //Dictionary<int, List<SingleOvertimeInfo>> singleDic = null;

        ///// <summary>
        ///// 考勤事由信息
        ///// </summary>
        //Dictionary<int, List<MattersInfo>> mattersDic = null;

        ///// <summary>
        ///// 考勤基础信息集合
        ///// </summary>
        //Dictionary<int, DataRow> userAttBasicDic = null;

        ///// <summary>
        ///// 考勤业务操作对象
        ///// </summary>
        //AttendanceManager attMan = new AttendanceManager();

        ///// <summary>
        ///// 节假日信息集合
        ///// </summary>
        //HashSet<int> holidays = null;
        //Dictionary<int, ESP.HumanResource.Entity.EmployeeJobInfo> employeeJobDic = null;
        #endregion

        /// <summary>
        /// 绑定迟到人员信息
        /// </summary>
        protected DataSet BindInfo()
        {
            // 查询参数集合
            List<SqlParameter> parameterList = new List<SqlParameter>();
            // 查询字符串
            StringBuilder strBuilder = new StringBuilder();

            Year = int.Parse(drpYear.SelectedValue);
            Month = int.Parse(drpMonth.SelectedValue);
            strBuilder.Append(" AND AttendanceYear=@Year AND AttendanceMonth=@Month ");
            parameterList.Add(new SqlParameter("Year", Year));
            parameterList.Add(new SqlParameter("Month", Month));

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
            if (!string.IsNullOrEmpty(txtPositions.Text))
            {
                strBuilder.Append(" AND Position LIKE '%'+@Position+'%' ");
                parameterList.Add(new SqlParameter("@Position", txtPositions.Text.Trim()));
            }
            // 分公司
            if (!string.IsNullOrEmpty(cbCompany.SelectedValue))
            {
                strBuilder.Append(" AND level1id=@level1id ");
                SqlParameter p = new SqlParameter("@level1id", SqlDbType.NVarChar);
                p.Value = cbCompany.SelectedValue;
                parameterList.Add(p);
            }
            // 团队
            if (!string.IsNullOrEmpty(cbDepartment1.SelectedValue))
            {
                strBuilder.Append(" AND level2id=@level2id ");
                SqlParameter p = new SqlParameter("@level2id", SqlDbType.NVarChar);
                p.Value = cbDepartment1.SelectedValue;
                parameterList.Add(p);
            }
            // 部门
            if (!string.IsNullOrEmpty(cbDepartment2.SelectedValue))
            {
                strBuilder.Append(" AND level3id=@level3id ");
                SqlParameter p = new SqlParameter("@level3id", SqlDbType.NVarChar);
                p.Value = cbDepartment2.SelectedValue;
                parameterList.Add(p);
            }

            AttendanceStatisticManager attendanceStatistic = new AttendanceStatisticManager();
            return attendanceStatistic.GetAttendanceStatisticInfo(UserID, strBuilder.ToString(), parameterList);

            #region 注释内容
            //string userids = "";
            //// 获得人员用户信息
            //UserAttBasicInfoManager userAttBasicManager = new UserAttBasicInfoManager();
            //employeesList = userAttBasicManager.GetStatUsers(UserID, out userids);



            //// 获得用户打卡记录信息
            //ClockInManager clockInManager = new ClockInManager();
            //clockInDic = clockInManager.GetClockInTimes(year, month, 0);

            //// 获得用户的OT记录信息
            //SingleOvertimeManager singleManage = new SingleOvertimeManager();
            //singleDic = singleManage.GetStatSingleOvertimeInfos(year, month, 0);

            //// 获得用户考勤事由信息
            //MattersManager mattersManager = new MattersManager();
            //mattersDic = mattersManager.GetStatModelList(year, month, 0);

            //// 获得用户当月考勤记录信息
            //Dictionary<int, List<SingleOvertimeInfo>> monthSingleDic = singleManage.GetModelListByMonth(year, month);

            //// 获得用户考勤基本信息
            //userAttBasicDic = userAttBasicManager.GetUserAttBasicInfos(UserID);

            //// 获得节假日信息
            //holidays = new HolidaysInfoManager().GetHolidayListByMonth(year, month);

            //// 获得入离职信息
            //employeeJobDic = attMan.GetEmployeeJobInfo(" sysid in (" + userids + ") ");

            //DataTable dt = new DataTable();
            //dt.Columns.Add(new DataColumn("UserID"));        // 用户编号
            //dt.Columns.Add(new DataColumn("UserName"));      // 用户姓名
            //dt.Columns.Add(new DataColumn("UserCode"));      // 员工编号
            //dt.Columns.Add(new DataColumn("Department"));    // 部门信息
            //dt.Columns.Add(new DataColumn("Position"));      // 职位
            //dt.Columns.Add(new DataColumn("AttMonth"));      // 月份
            //dt.Columns.Add(new DataColumn("LateCount1"));      // 迟到30分钟内
            //dt.Columns.Add(new DataColumn("LateCount2"));      // 迟到30分钟以上
            //dt.Columns.Add(new DataColumn("AbsentCount1"));      // 旷工半天
            //dt.Columns.Add(new DataColumn("AbsentCount2"));      // 旷工一天
            //dt.Columns.Add(new DataColumn("AbsentCount3"));      // 打卡记录不全
            //dt.Columns.Add(new DataColumn("LeaveEarly"));      // 早退
            //dt.Columns.Add(new DataColumn("OverTimeCount"));      // OT次数
            
            //DateTime d5 = DateTime.Now;
            
            //foreach (ESP.Framework.Entity.EmployeeInfo employeeModel in employeesList)
            //{
            //    if (((!string.IsNullOrEmpty(userName) && employeeModel.FullNameCN.IndexOf(userName) != -1) || string.IsNullOrEmpty(userName))
            //               && ((!string.IsNullOrEmpty(userCode) && employeeModel.Code.IndexOf(userCode) != -1) || string.IsNullOrEmpty(userCode)))
            //    {
            //        int userid = employeeModel.UserID;
            //        DataRow userAttBasicDr = null;
            //        if (userAttBasicDic.ContainsKey(userid))
            //        {
            //            userAttBasicDr = userAttBasicDic[userid];
            //        }

            //        if (userAttBasicDr != null)
            //        {
            //            ESP.HumanResource.Entity.EmployeeJobInfo employeeJobModel = employeeJobDic[userid];

            //            // OT事由信息集合
            //            List<SingleOvertimeInfo> singleList;
            //            if (!singleDic.TryGetValue(userid, out singleList))
            //            {
            //                singleList = new List<SingleOvertimeInfo>();
            //            }

            //            // 考勤事由信息集合
            //            List<MattersInfo> mattersList;
            //            if (!mattersDic.TryGetValue(userid, out mattersList))
            //            {
            //                mattersList = new List<MattersInfo>();
            //            }
            //            UserAttBasicInfo userAttBasicModel = new UserAttBasicInfo();
            //            userAttBasicModel.PopupData(userAttBasicDr);

            //            // 当月OT事由信息集合
            //            List<SingleOvertimeInfo> monthSingleList;
            //            if (!monthSingleDic.TryGetValue(userid, out monthSingleList))
            //            {
            //                monthSingleList = new List<SingleOvertimeInfo>();
            //            }

            //            DataRow dr = dt.NewRow();
            //            dr[0] = userAttBasicModel.Userid;
            //            dr[1] = userAttBasicModel.EmployeeName;
            //            dr[2] = userAttBasicModel.UserCode;
            //            dr[3] = userAttBasicDr["Department"].ToString();
            //            dr[4] = userAttBasicDr["DepartmentPositionName"].ToString();
            //            dr[5] = year + "-" + month;
            //            dr[12] = monthSingleList.Count;

            //            Dictionary<long, DateTime> clockInDictionary;
            //            if (!clockInDic.TryGetValue(userid, out clockInDictionary))
            //            {
            //                clockInDictionary = new Dictionary<long, DateTime>();
            //            }
                        
            //            attMan.GetMonthStat(userAttBasicModel, year, month, clockInDictionary, singleList, mattersList, employeeJobModel, ref dr, holidays);
            //            dt.Rows.Add(dr);
            //        }
            //    }
            //}
            //ESP.Logging.Logger.Add("计算考勤统计信息花费时间：" + (DateTime.Now - d5).TotalSeconds);
            #endregion
        }

        /// <summary>
        /// 查询OT统计信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            Grid1.DataSource = BindInfo();
            Grid1.DataBind();
        }

        /// <summary>
        /// 获得考勤月份信息
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        protected string GetAttendanceTime(string year, string month)
        {
            return year + "-" + month;
        }

        /// <summary>
        /// 获得消息提示的URL
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <returns></returns>
        protected string GetRemindUrl(string userid, string year, string month)
        {
            return "<a href='LateInfoSearch.aspx?userid=" + userid + "&flag=1&year=" + year + "&month=" + month + "'><img src='../images/Icon_Sendmail.gif' /></a>";
        }

        /// <summary>
        /// 导出考勤统计信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, ImageClickEventArgs e)
        {
            DataSet ds = BindInfo();
            FileHelper.ExprotAttendanceStat(ds, Server.MapPath("~"), Response);
        }

        /// <summary>
        /// 发送提示邮件
        /// </summary>
        /// <param name="userid">用户编号</param>
        private void SendRemindMail(int userid)
        {
            string year = Request["year"];
            string month = Request["month"];
            string url = "http://" + Request.Url.Authority + "/MailTemplate/RemindMail.aspx?userid=" + userid + "&senderid=" + UserID + "&year=" + year + "&month=" + month;
            string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
            body += "<br><br><a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "'>" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "</a>";
            MailAddress[] recipientAddress = { new MailAddress(new ESP.Compatible.Employee(userid).EMail) };

            ESP.Mail.MailManager.Send(UserInfo.FullNameCN + "提醒您该做日常考勤了", body, true, null, recipientAddress, null, null, null);
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('邮件发送成功！');", true);
        }

        public int Year
        {
            get { return (int)this.ViewState["Year"]; }
            set { this.ViewState["Year"] = value; }
        }

        public int Month
        {
            get { return (int)this.ViewState["Month"]; }
            set { this.ViewState["Month"] = value; }
        }

        #region 部门信息加载
        /// <summary>
        /// 加载一级部门
        /// </summary>
        public void LoadDepartmentInfo()
        {
            IList<DepartmentInfo> deptlist = DepartmentManager.GetAll();
            if (deptlist != null && deptlist.Count > 0)
            {
                foreach (DepartmentInfo dept in deptlist)
                {
                    if (dept.DepartmentLevel == 1)
                    {
                        ComboBoxItem item = new ComboBoxItem();
                        item.Text = dept.DepartmentName;
                        item.Value = dept.DepartmentID.ToString();
                        cbCompany.Items.Add(item);
                    }
                }
            }
        }

        /// <summary>
        /// 根据用户选择的一级部门加载相应的二级部门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cbCompany.SelectedValue))
                {
                    int companyId = int.Parse(cbCompany.SelectedValue);
                    IList<DepartmentInfo> childList = DepartmentManager.GetChildren(companyId);
                    if (childList != null && childList.Count > 0)
                    {
                        cbDepartment1.Items.Clear();
                        cbDepartment1.SelectedItem = null;
                        cbDepartment1.Text = "";
                        cbDepartment2.Items.Clear();
                        cbDepartment2.SelectedItem = null;
                        cbDepartment2.Text = "";
                        foreach (DepartmentInfo dept in childList)
                        {
                            ComboBoxItem item = new ComboBoxItem();
                            item.Text = dept.DepartmentName;
                            item.Value = dept.DepartmentID.ToString();
                            cbDepartment1.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add(ex.Message, "", ESP.Logging.LogLevel.Error, ex, "");
            }
        }

        /// <summary>
        /// 根据用户选择的二级部门加载相应的三级部门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbDepartment1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cbDepartment1.SelectedValue))
                {
                    int departmentId = int.Parse(cbDepartment1.SelectedValue);
                    IList<DepartmentInfo> childList = DepartmentManager.GetChildren(departmentId);
                    if (childList != null && childList.Count > 0)
                    {
                        cbDepartment2.Items.Clear();
                        cbDepartment2.SelectedItem = null;
                        cbDepartment2.Text = "";
                        foreach (DepartmentInfo dept in childList)
                        {
                            ComboBoxItem item = new ComboBoxItem();
                            item.Text = dept.DepartmentName;
                            item.Value = dept.DepartmentID.ToString();
                            cbDepartment2.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add(ex.Message, "", ESP.Logging.LogLevel.Error, ex, "");
            }
        }
        #endregion 
    }
}
