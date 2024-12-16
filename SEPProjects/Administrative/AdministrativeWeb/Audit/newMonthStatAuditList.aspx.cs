using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ESP.Administrative.Common;
using ESP.Administrative.BusinessLogic;
using ESP.HumanResource.BusinessLogic;
using System.Net.Mail;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;
using ComponentArt.Web.UI;
using System.Text;
using System.Data.SqlClient;
using ESP.Administrative.Entity;

namespace AdministrativeWeb.Audit
{
    public partial class newMonthStatAuditList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 孙军兰（kate.sun/13389）的考勤统计信息武燕（amanda.wu/13970）协同审批
                ApproveUserID = UserID.ToString();
                //if (UserID == 13970)
                //{
                //    ApproveUserID = "13389";
                //}
                // 杨帆（echo.yang/13416）的考勤统计信息梁薇（lucy.liang/13444）协同审批
                //if (UserID == 13444)
                //{
                //    ApproveUserID = "13416";
                //}
                drpDate();
                BindInfo();
                LoadDepartmentInfo();
            }
        }

        /// <summary>
        /// 考勤统计信息
        /// </summary>
        public string ApproveUserID
        {
            get
            {
                return this.ViewState["ApproveUserID"] as string;
            }
            set
            {
                this.ViewState["ApproveUserID"] = value;
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

            if (15 < DateTime.Now.Day)
            {
                drpMonth.SelectedValue = DateTime.Now.Month.ToString("00");
            }
            else if (1 != DateTime.Now.Month)
            {
                drpMonth.SelectedValue = (DateTime.Now.Month - 1).ToString("00");
            }
            else
            {
                drpMonth.SelectedValue = (DateTime.Now.AddMonths(-1)).Month.ToString("00");
                drpYear.SelectedValue = (DateTime.Now.Year - 1).ToString();
            }
        }
        
        /// <summary>
        /// 绑定待审批的考勤统计信息
        /// </summary>
        protected void BindInfo()
        {
            int status = 0;
            //if (!string.IsNullOrEmpty(Request["type"]))
            //{
                List<SqlParameter> parameterList = new List<SqlParameter>();
                // 查询字符串
                StringBuilder strBuilder = new StringBuilder();

                // 姓名
                if (!string.IsNullOrEmpty(txtUserName.Text))
                {
                    strBuilder.Append(" AND m.EmployeeName LIKE '%'+@EmployeeName+'%' ");
                    parameterList.Add(new SqlParameter("@EmployeeName", txtUserName.Text.Trim()));
                }
                // 员工编号
                if (!string.IsNullOrEmpty(txtUserCode.Text))
                {
                    strBuilder.Append(" AND m.UserCode LIKE '%'+@UserCode+'%' ");
                    parameterList.Add(new SqlParameter("@UserCode", txtUserCode.Text.Trim()));
                }
                // 分公司
                if (!string.IsNullOrEmpty(cbCompany.SelectedValue))
                {
                    strBuilder.Append(" AND d.level1id=@level1id ");
                    SqlParameter p = new SqlParameter("@level1id", SqlDbType.NVarChar);
                    p.Value = cbCompany.SelectedValue;
                    parameterList.Add(p);
                }
                // 团队
                if (!string.IsNullOrEmpty(cbDepartment1.SelectedValue))
                {
                    strBuilder.Append(" AND d.level2id=@level2id ");
                    SqlParameter p = new SqlParameter("@level2id", SqlDbType.NVarChar);
                    p.Value = cbDepartment1.SelectedValue;
                    parameterList.Add(p);
                }
                // 部门
                if (!string.IsNullOrEmpty(cbDepartment2.SelectedValue))
                {
                    strBuilder.Append(" AND d.level3id=@level3id ");
                    SqlParameter p = new SqlParameter("@level3id", SqlDbType.NVarChar);
                    p.Value = cbDepartment2.SelectedValue;
                    parameterList.Add(p);
                }

                //switch (Request["type"])
                //{
                //    case "2": status = Status.MonthStatAppState_WaitDirector; btnExport.Visible = false; break;
                //    case "3": status = Status.MonthStatAppState_WaitStatisticians; break;
                //    case "4": status = Status.MonthStatAppState_WaitHRAdmin; break;
                //    case "5": status = Status.MonthStatAppState_WaitManager; btnExport.Visible = false; break;
                //    case "6": status = Status.MonthStatAppState_WaitADAdmin; break;
                //}

                status = Status.MonthStatAppState_NoSubmit;
                hidType.Value = status.ToString();
                DataSet ds = new ApproveLogManager().GetNewApproveList(ApproveUserID, status, int.Parse(drpYear.SelectedItem.Value), int.Parse(drpMonth.SelectedItem.Value), strBuilder.ToString(), parameterList);

                Grid1.DataSource = ds.Tables[0];
                Grid1.DataBind();
            //}
        }

        /// <summary>
        /// 查询考勤统计信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            BindInfo();
        }

        /// <summary>
        /// 审批通过考勤统计信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAudit_Click(object sender, EventArgs e)
        {
            string hid = hidMatter.Value;
            string type = hidType.Value;
            if (!string.IsNullOrEmpty(hid) || !string.IsNullOrEmpty(type))
            {
                List<ESP.Administrative.Entity.MonthStatInfo> list = new MonthStatManager().GetMonthStatsList(hid);
                if (list.Count > 0)
                {
                    List<ESP.Administrative.Entity.MonthStatInfo> listmonth = new List<ESP.Administrative.Entity.MonthStatInfo>();
                    List<ESP.Administrative.Entity.ApproveLogInfo> listapprove = new List<ESP.Administrative.Entity.ApproveLogInfo>();
                    List<ESP.Administrative.Entity.ApproveLogInfo> list2 = new List<ESP.Administrative.Entity.ApproveLogInfo>();
                    ESP.Administrative.Entity.ApproveLogInfo approve = null;
                    foreach (ESP.Administrative.Entity.MonthStatInfo info in list)
                    {
                        approve = new ESP.Administrative.Entity.ApproveLogInfo();
                        ESP.Administrative.Entity.OperationAuditManageInfo manageModel
                            = new ESP.Administrative.BusinessLogic.OperationAuditManageManager().GetOperationAuditModelByUserID(info.UserID);

                        /*
                        switch (type)
                        {
                            case "2":   // 等待总监审批。
                                {
                                    info.State = Status.MonthStatAppState_WaitHRAdmin;
                                    info.ApproveID = UserInfo.UserID;
                                    info.ApproveTime = DateTime.Now;
                                    info.ApproveRemark += UserInfo.FullNameCN + "，" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "，审批通过：批量审批；\r\n";

                                    approve.ApproveID = manageModel.HRAdminID;
                                    approve.ApproveName = manageModel.HRAdminName;

                                    #region 注释代码
                                    //if (manageModel.ManagerID > 0)
                                    //{
                                    //    info.State = Status.MonthStatAppState_WaitManager;
                                    //    info.ApproveID = UserInfo.UserID;
                                    //    info.ApproveTime = DateTime.Now;
                                    //    info.ApproveRemark += UserInfo.FullNameCN + "，" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "，审批通过：批量审批；\r\n";

                                    //    approve.ApproveID = manageModel.ManagerID;
                                    //    approve.ApproveName = manageModel.ManagerName;
                                    //}
                                    //else
                                    //{
                                    //    info.State = Status.MonthStatAppState_WaitADAdmin;
                                    //    info.ManagerID = UserInfo.UserID;
                                    //    info.ManagerTime = DateTime.Now;
                                    //    info.ApproveRemark += UserInfo.FullNameCN + "，" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "，审批通过：批量审批；\r\n";

                                    //    int departmentId = new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager().GetRootDepartmentID(info.UserID).DepartmentID;
                                    //    if (departmentId == (int)AreaID.HeadOffic)
                                    //    {
                                    //        ESP.Administrative.Entity.DataCodeInfo datacodeModel = new DataCodeManager().GetDataCodeByType("BeijingAttendanceAdmin")[0];
                                    //        string[] datecode = datacodeModel.Code.Split(new char[] { ',' });
                                    //        approve.ApproveID = int.Parse(datecode[0]);
                                    //        approve.ApproveName = datecode[1];
                                    //    }
                                    //    else if (departmentId == (int)AreaID.ShanghaiOffic)
                                    //    {
                                    //        ESP.Administrative.Entity.DataCodeInfo datacodeModel = new DataCodeManager().GetDataCodeByType("ShanghaiAttendanceAdmin")[0];
                                    //        string[] datecode = datacodeModel.Code.Split(new char[] { ',' });
                                    //        approve.ApproveID = int.Parse(datecode[0]);
                                    //        approve.ApproveName = datecode[1];
                                    //    }
                                    //    else if (departmentId == (int)AreaID.GuangzhouOffic)
                                    //    {
                                    //        ESP.Administrative.Entity.DataCodeInfo datacodeModel = new DataCodeManager().GetDataCodeByType("GuangzhouAttendanceAdmin")[0];
                                    //        string[] datecode = datacodeModel.Code.Split(new char[] { ',' });
                                    //        approve.ApproveID = int.Parse(datecode[0]);
                                    //        approve.ApproveName = datecode[1];
                                    //    }
                                    //    else if (departmentId == (int)AreaID.ChengduOffic)
                                    //    {
                                    //        ESP.Administrative.Entity.DataCodeInfo datacodeModel = new DataCodeManager().GetDataCodeByType("ChengduAttendanceAdmin")[0];
                                    //        string[] datecode = datacodeModel.Code.Split(new char[] { ',' });
                                    //        approve.ApproveID = int.Parse(datecode[0]);
                                    //        approve.ApproveName = datecode[1];
                                    //    }
                                    //    else
                                    //    {
                                    //        ESP.Administrative.Entity.DataCodeInfo datacodeModel = new DataCodeManager().GetDataCodeByType("BeijingAttendanceAdmin")[0];
                                    //        string[] datecode = datacodeModel.Code.Split(new char[] { ',' });
                                    //        approve.ApproveID = int.Parse(datecode[0]);
                                    //        approve.ApproveName = datecode[1];
                                    //    }
                                    //}
                                    #endregion
                                    break;
                                }
                            case "4":  // 等待团队HRAdmin审批。
                                {
                                    info.State = Status.MonthStatAppState_Passed;
                                    info.ADAdminID = UserInfo.UserID;
                                    info.ADAdminTime = DateTime.Now;
                                    info.ApproveRemark += UserInfo.FullNameCN + "，" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "，审批通过：批量审批；\r\n";


                                    //info.State = Status.MonthStatAppState_WaitDirector;
                                    //info.HRAdminID = UserInfo.UserID;
                                    //info.HRAdminTime = DateTime.Now;
                                    //info.ApproveRemark += UserInfo.FullNameCN + "，" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "，审批通过：批量审批；\r\n";

                                    //approve.ApproveID = manageModel.TeamLeaderID;
                                    //approve.ApproveName = manageModel.TeamLeaderName;
                                    break;
                                }
                            case "5":  // 等待团队总经理审批。
                                {
                                    info.State = Status.MonthStatAppState_WaitADAdmin;
                                    info.ManagerID = UserInfo.UserID;
                                    info.ManagerTime = DateTime.Now;
                                    info.ApproveRemark += UserInfo.FullNameCN + "，" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "，审批通过：批量审批；\r\n";

                                    int departmentId = new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager().GetRootDepartmentID(info.UserID).DepartmentID;
                                    if (departmentId == (int)AreaID.HeadOffic)
                                    {
                                        ESP.Administrative.Entity.DataCodeInfo datacodeModel = new DataCodeManager().GetDataCodeByType("BeijingAttendanceAdmin")[0];
                                        string[] datecode = datacodeModel.Code.Split(new char[] { ',' });
                                        approve.ApproveID = int.Parse(datecode[0]);
                                        approve.ApproveName = datecode[1];
                                    }
                                    else if (departmentId == (int)AreaID.ShanghaiOffic)
                                    {
                                        ESP.Administrative.Entity.DataCodeInfo datacodeModel = new DataCodeManager().GetDataCodeByType("ShanghaiAttendanceAdmin")[0];
                                        string[] datecode = datacodeModel.Code.Split(new char[] { ',' });
                                        approve.ApproveID = int.Parse(datecode[0]);
                                        approve.ApproveName = datecode[1];
                                    }
                                    else if (departmentId == (int)AreaID.GuangzhouOffic)
                                    {
                                        ESP.Administrative.Entity.DataCodeInfo datacodeModel = new DataCodeManager().GetDataCodeByType("GuangzhouAttendanceAdmin")[0];
                                        string[] datecode = datacodeModel.Code.Split(new char[] { ',' });
                                        approve.ApproveID = int.Parse(datecode[0]);
                                        approve.ApproveName = datecode[1];
                                    }
                                    else if (departmentId == (int)AreaID.ChengduOffic)
                                    {
                                        ESP.Administrative.Entity.DataCodeInfo datacodeModel = new DataCodeManager().GetDataCodeByType("ChengduAttendanceAdmin")[0];
                                        string[] datecode = datacodeModel.Code.Split(new char[] { ',' });
                                        approve.ApproveID = int.Parse(datecode[0]);
                                        approve.ApproveName = datecode[1];
                                    }
                                    else
                                    {
                                        ESP.Administrative.Entity.DataCodeInfo datacodeModel = new DataCodeManager().GetDataCodeByType("BeijingAttendanceAdmin")[0];
                                        string[] datecode = datacodeModel.Code.Split(new char[] { ',' });
                                        approve.ApproveID = int.Parse(datecode[0]);
                                        approve.ApproveName = datecode[1];
                                    }
                                    break;
                                }
                            case "6":  // 等待考勤管理员审批。
                                {
                                    info.State = Status.MonthStatAppState_Passed;
                                    info.ADAdminID = UserInfo.UserID;
                                    info.ADAdminTime = DateTime.Now;
                                    info.ApproveRemark += UserInfo.FullNameCN + "，" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "，审批通过：批量审批；\r\n";

                                    break;
                                }
                        }
                        */
                        info.State = Status.MonthStatAppState_Passed;
                        info.ADAdminID = UserInfo.UserID;
                        info.ADAdminTime = DateTime.Now;
                        info.ApproveRemark += UserInfo.FullNameCN + "，" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "，审批通过：批量审批；\r\n";

                        info.UpdateTime = DateTime.Now;
                        info.OperateorID = UserInfo.UserID;
                        listmonth.Add(info);
                        /*
                        if (type != "4")
                        {
                            approve.ApproveType = (int)Status.MattersSingle.MattersSingle_Attendance;
                            approve.ApproveDateID = info.ID;
                            approve.ApproveState = 0;
                            approve.ApproveUpUserID = UserInfo.UserID;
                            approve.IsLastApprove = 0;
                            approve.Deleted = false;
                            approve.CreateTime = approve.UpdateTime = DateTime.Now;
                            approve.OperateorID = UserInfo.UserID;
                            listapprove.Add(approve);
                        }
                        */
                        //ESP.Administrative.Entity.ApproveLogInfo model = new ApproveLogManager().GetModel(int.Parse(ApproveUserID), info.ID);
                        //if (type == "4")
                        //    model.IsLastApprove = 1;
                        //model.ApproveState = 1;
                        //model.UpdateTime = DateTime.Now;
                        //model.OperateorID = UserInfo.UserID;
                        //list2.Add(model);
                    }
                    if (0 < new MonthStatManager().Update(listmonth, listapprove, list2))
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='newMonthStatAuditList.aspx?type="+type+"';alert('审批成功！');", true);
                        /*foreach (ESP.Administrative.Entity.MonthStatInfo model in listmonth)
                        {
                            string email = new ESP.Compatible.Employee(model.UserID).EMail;
                            if (!string.IsNullOrEmpty(email))
                            {
                                string body = "<br><br>" + UserInfo.FullNameCN + "审批通过了您的" + model.Year + "年" + model.Month + "月的考勤";
                                body += "<br><br><a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "'>" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "</a>";
                                MailAddress[] recipientAddress = { new MailAddress(email) };

                                ESP.Mail.MailManager.Send("考勤事由审批", body, true, null, recipientAddress, null, null, null);
                            }
                        }*/
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='newMonthStatAuditList.aspx?type=" + type + "';alert('审批失败！');", true);
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='newMonthStatAuditList.aspx?type=" + type + "';alert('审批失败！');", true);
                }
            }
        }

        /// <summary>
        /// 导出考勤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            string type = hidType.Value;
            string content = drpYear.SelectedItem.Text + "|" + drpMonth.SelectedItem.Value;
            FileHelper.Export(content, type, int.Parse(ApproveUserID), Server.MapPath("~"), Response);
        }

        /// <summary>
        /// 计算显示时间信息
        /// </summary>
        /// <param name="hours"></param>
        /// <returns></returns>
        protected string GetTimeInfo(string hours)
        {
            string timeinfo = "0";
            if (!string.IsNullOrEmpty(hours))
            {
                double hour = double.Parse(hours);
                timeinfo = (hour / Status.WorkingHours).ToString();
            }
            timeinfo = timeinfo == "0" ? "" : string.Format("{0:F3}", timeinfo);
            return timeinfo;
        }

        /// <summary>
        /// 计算显示时间信息
        /// </summary>
        /// <param name="hours"></param>
        /// <returns></returns>
        protected string GetTimeInfo2(string hours)
        {
            hours = (int)double.Parse(hours) == 0 ? "" : string.Format("{0:F3}", double.Parse(hours));
            return hours;
        }

        protected string GetAttendanceType(string type)
        {
            if (!string.IsNullOrEmpty(type) && type == ((int)AttendanceSubType.Dimission).ToString())
            {
                type = "离职提交";
            }
            else
            {
                type = "";
            }
            return type;
        }

        protected string GetWTBday(string info)
        {
            string date = info.Split('#')[0];
            int userid = int.Parse(info.Split('#')[1]);

            HashSet<int> holidays = new HolidaysInfoManager().GetHolidayListByMonth(int.Parse(date.Split('-')[0]), int.Parse(date.Split('-')[1]));

            int WtbDayNum = 0;

            DateTime begin = DateTime.Parse(date + "-01");
            DateTime end = begin.AddMonths(1).AddDays(-1);

            for (DateTime i = begin; i <= end;)
            {
             
                    //不包含commit且不是假期
                    if (!holidays.Contains(i.Day))
                        WtbDayNum++;
                
                i = i.AddDays(1);
            }
            return WtbDayNum.ToString();
        }

        protected string GetUnauditday(string info)
        {
            string date = info.Split('#')[0];
            int userid = int.Parse(info.Split('#')[1]);

            HashSet<int> holidays = new HolidaysInfoManager().GetHolidayListByMonth(int.Parse(date.Split('-')[0]), int.Parse(date.Split('-')[1]));

            int WtbDayNum = 0;

            DateTime begin = DateTime.Parse(date + "-01");
            DateTime end = begin.AddMonths(1).AddDays(-1);

            for (DateTime i = begin; i <= end; )
            {
                i = i.AddDays(1);
            }
            return WtbDayNum.ToString();
        }

        /// <summary>
        /// 审批驳回考勤统计信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOverrule_Click(object sender, EventArgs e)
        {
            string hid = hidMatter.Value;
            string type = hidType.Value;
            string userids = "";
            if (!string.IsNullOrEmpty(hid) || !string.IsNullOrEmpty(type))
            {
                List<ESP.Administrative.Entity.MonthStatInfo> list = new MonthStatManager().GetMonthStatsList(hid);
                if (list.Count > 0)
                {
                    List<ESP.Administrative.Entity.MonthStatInfo> listmonth = new List<ESP.Administrative.Entity.MonthStatInfo>();
                    List<ESP.Administrative.Entity.ApproveLogInfo> listapprove = new List<ESP.Administrative.Entity.ApproveLogInfo>();
                    List<ESP.Administrative.Entity.ApproveLogInfo> list2 = new List<ESP.Administrative.Entity.ApproveLogInfo>();
                    ESP.Administrative.BusinessLogic.ApproveLogManager approveManager = new ApproveLogManager();
                    foreach (ESP.Administrative.Entity.MonthStatInfo info in list)
                    {
                        ESP.Administrative.Entity.ApproveLogInfo model = new ApproveLogManager().GetModel(int.Parse(ApproveUserID), info.ID);
                        ESP.Administrative.Entity.OperationAuditManageInfo manageModel
                            = new ESP.Administrative.BusinessLogic.OperationAuditManageManager().GetOperationAuditModelByUserID(info.UserID);

                        switch (type)
                        {
                            case "2":
                                {
                                    info.State = Status.MonthStatAppState_Overrule;
                                    info.ApproveID = UserInfo.UserID;
                                    info.ApproveTime = DateTime.Now;
                                    info.ApproveRemark += UserInfo.FullNameCN + "，" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "，审批驳回：批量审批；\r\n";
                                        //"TeamLeader" + UserInfo.FullNameCN + "驳回了您的" + info.Year + "年" + info.Month + "月考勤；\r\n";
                                    break;
                                }
                            case "4":
                                {
                                    info.State = Status.MonthStatAppState_Overrule;
                                    info.HRAdminID = UserInfo.UserID;
                                    info.HRAdminTime = DateTime.Now;
                                    info.ApproveRemark += UserInfo.FullNameCN + "，" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "，审批驳回：批量审批；\r\n";
                                        //"团队HR/Admin" + UserInfo.FullNameCN + "驳回了您的" + info.Year + "年" + info.Month + "月考勤；\r\n";
                                    break;
                                }
                            case "5":
                                {
                                    info.State = Status.MonthStatAppState_Overrule;
                                    info.ManagerID = UserInfo.UserID;
                                    info.ManagerTime = DateTime.Now;
                                    info.ApproveRemark += UserInfo.FullNameCN + "，" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "，审批驳回：批量审批；\r\n";
                                        //"团队总经理" + UserInfo.FullNameCN + "驳回了您的" + info.Year + "年" + info.Month + "月考勤；\r\n";
                                    
                                    break;
                                }
                            case "6":
                                {
                                    info.State = Status.MonthStatAppState_Overrule;
                                    info.ADAdminID = UserInfo.UserID;
                                    info.ADAdminTime = DateTime.Now;
                                    info.ApproveRemark += UserInfo.FullNameCN + "，" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "，审批驳回：批量审批；\r\n";
                                        //"考勤管理员" + UserInfo.FullNameCN + "驳回了您的" + info.Year + "年" + info.Month + "月考勤；\r\n";
                                    break;
                                }
                        }
                        info.UpdateTime = DateTime.Now;
                        info.OperateorID = UserInfo.UserID;
                        listmonth.Add(info);

                        if (type == "6")
                            model.IsLastApprove = 1;
                        model.ApproveState = Status.ApproveState_Overrule;
                        model.UpdateTime = DateTime.Now;
                        model.OperateorID = UserInfo.UserID;
                        list2.Add(model);
                        userids += info.UserID.ToString() + ",";
                    }
                    if (0 < new MonthStatManager().Update(listmonth, listapprove, list2))
                    {
                        // 驳回自动关闭七天的提交限制
                        DataCodeManager datacodeManager = new DataCodeManager();
                        DataCodeInfo datamodel = datacodeManager.GetDataCodeByType("OpenedUserID")[0];
                        if (userids.EndsWith(","))
                        {
                            userids = userids.TrimEnd(new char[] { ',' });
                        }
                        if (!string.IsNullOrEmpty(datamodel.Code))
                        {
                            datamodel.Code += "," + userids;
                        }
                        else
                        {
                            datamodel.Code =  userids + "";
                        }
                        datacodeManager.Update(datamodel);

                        foreach (ESP.Administrative.Entity.MonthStatInfo model in listmonth)
                        {
                            string email = new ESP.Compatible.Employee(model.UserID).EMail;
                            if (!string.IsNullOrEmpty(email))
                            {
                                string body = "<br><br>" + UserInfo.FullNameCN + "驳回了您的" + model.Year + "年" + model.Month + "月的考勤";
                                body += "<br><br><a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "'>" + ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] + "</a>";
                                MailAddress[] recipientAddress = { new MailAddress(email) };

                                ESP.Mail.MailManager.Send("考勤事由审批", body, true, null, recipientAddress, null, null, null);
                            }
                        }
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='MonthStatAuditList.aspx?type="+type+"';alert('审批成功！');", true);
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='MonthStatAuditList.aspx?type=" + type + "';alert('审批失败！');", true);
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='MonthStatAuditList.aspx?type=" + type + "';alert('审批失败！');", true);
                }
            }
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
                ComboBoxItem itemt = new ComboBoxItem();
                itemt.Text = "请选择......";
                itemt.Value = "";
                cbCompany.Items.Add(itemt);
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
                    cbDepartment1.Items.Clear();
                    cbDepartment1.SelectedItem = null;
                    cbDepartment1.Text = "";
                    cbDepartment2.Items.Clear();
                    cbDepartment2.SelectedItem = null;
                    cbDepartment2.Text = "";
                    if (childList != null && childList.Count > 0)
                    {
                        ComboBoxItem itemt = new ComboBoxItem();
                        itemt.Text = "请选择......";
                        itemt.Value = "";
                        cbDepartment1.Items.Add(itemt);
                        foreach (DepartmentInfo dept in childList)
                        {
                            ComboBoxItem item = new ComboBoxItem();
                            item.Text = dept.DepartmentName;
                            item.Value = dept.DepartmentID.ToString();
                            cbDepartment1.Items.Add(item);
                        }
                    }
                }
                else
                {
                    cbDepartment1.Items.Clear();
                    cbDepartment1.SelectedItem = null;
                    cbDepartment1.Text = "";
                    cbDepartment2.Items.Clear();
                    cbDepartment2.SelectedItem = null;
                    cbDepartment2.Text = "";
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
                    cbDepartment2.Items.Clear();
                    cbDepartment2.SelectedItem = null;
                    cbDepartment2.Text = "";
                    if (childList != null && childList.Count > 0)
                    {
                        ComboBoxItem itemt = new ComboBoxItem();
                        itemt.Text = "请选择......";
                        itemt.Value = "";
                        cbDepartment2.Items.Add(itemt);
                        foreach (DepartmentInfo dept in childList)
                        {
                            ComboBoxItem item = new ComboBoxItem();
                            item.Text = dept.DepartmentName;
                            item.Value = dept.DepartmentID.ToString();
                            cbDepartment2.Items.Add(item);
                        }
                    }
                }
                else
                {
                    cbDepartment2.Items.Clear();
                    cbDepartment2.SelectedItem = null;
                    cbDepartment2.Text = "";
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
