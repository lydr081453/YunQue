using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using ESP.Administrative.Entity;
using System.Text;
using ESP.Administrative.Common;
using ESP.Administrative.BusinessLogic;
using System.Collections;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;
using System.Data;

namespace AdministrativeWeb.Attendance
{
    public partial class IntegratedQueryView : ESP.Web.UI.PageBase
    {
        private string lowerUserIds;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 通过用户ID获得用户信息
                if (Request["ApplicantID"] != null)
                {
                    string applicantID = Request["ApplicantID"];
                    SelectUserID = applicantID;

                    upUserInfo.Visible = true;
                    // 默认选择当前日期
                    cldAttendance.SelectedDate = DateTime.Now;
                    BindCalender(cldAttendance.SelectedDate.Year, cldAttendance.SelectedDate.Month);

                    // 设置各月考勤的状态标题
                    SetTitleString();
                    SetMonthStat();
                }
                lowerUserIds = new UserAttBasicInfoManager().GetStatUserIDs(UserID);

                DataCodeManager dataCodeManager = new DataCodeManager();
                List<DataCodeInfo> dataCodeList = dataCodeManager.GetDataCodeByType("HR" + UserID);
                if (dataCodeList != null && dataCodeList.Count > 0)
                {
                    DataCodeInfo dataCodeModel = dataCodeList[0];
                    if (dataCodeModel != null)
                    {
                        lowerUserIds = lowerUserIds + "," + new UserAttBasicInfoManager().GetStatUserIDs(int.Parse(dataCodeModel.Code));
                    }
                }             
                GetDepartmentInfo();
            }
        }

        /// <summary>
        /// 被选择用户的ID值
        /// </summary>
        public string SelectUserID
        {
            get
            {
                return this.ViewState["SelectUserID"] as string;
            }
            set
            {
                this.ViewState["SelectUserID"] = value;
            }
        }

        /// <summary>
        /// 绑定部门节点信息
        /// </summary>
        /// <param name="depinfo">部门信息</param>
        /// <param name="parentNode">节点树的根节点</param>
        /// <param name="Id"></param>
        /// <param name="n"></param>
        /// <param name="treenode"></param>
        /// <param name="isP"></param>
        /// <param name="isHerf"></param>
        void BindTree(IList<ESP.Framework.Entity.DepartmentInfo> depinfo,
            ComponentArt.Web.UI.TreeViewNode parentNode, int Id, ref int n, string treenode, bool isP, bool isHerf)
        {
            ComponentArt.Web.UI.TreeViewNode tn = null;
            // 部门下面的人员列表
            DataSet ds = null;
            IList<ESP.HumanResource.Entity.EmployeeBaseInfo> employeeList = null;
            int departmentId = 0;
            foreach (ESP.Framework.Entity.DepartmentInfo info in depinfo)
            {
                if (info.DepartmentName.IndexOf("作废") == -1)
                {
                    if (info.ParentID == Id)
                    {
                        if (info.DepartmentLevel == 3)
                        {
                            ds = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetList(info.DepartmentID);
                            departmentId = info.DepartmentID;
                            tn = new ComponentArt.Web.UI.TreeViewNode();
                            tn.Text = info.DepartmentName;
                            tn.Value = info.DepartmentID.ToString();
                            tn.ImageUrl = "/images/treeview/dept.gif";
                        }
                        else if (info.DepartmentLevel == 2)
                        {
                            tn = new ComponentArt.Web.UI.TreeViewNode();
                            tn.Text = info.DepartmentName;
                            tn.Value = info.DepartmentID.ToString();
                            tn.ImageUrl = "/images/treeview/dept.gif";
                        }
                        else
                        {
                            tn = new ComponentArt.Web.UI.TreeViewNode();
                            tn.Text = info.DepartmentName;
                            tn.Value = info.DepartmentID.ToString();
                            tn.ImageUrl = "/images/treeview/corp.gif";
                        }

                        n++;
                        tn.ToolTip = info.DepartmentName;
                        parentNode.Nodes.Add(tn);

                        if (info.DepartmentLevel == 3 && ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && departmentId == info.DepartmentID)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                if (lowerUserIds.IndexOf(dr["userid"].ToString()) == -1)
                                {
                                    continue;
                                }
                                ComponentArt.Web.UI.TreeViewNode tnemployee = new ComponentArt.Web.UI.TreeViewNode();
                                tnemployee.Text = dr["userName"].ToString();

                                tnemployee.Value = dr["userid"].ToString();
                                tnemployee.ImageUrl = "/images/treeview/user.gif";
                                tnemployee.NavigateUrl = "IntegratedQueryView.aspx?ApplicantID=" + dr["userid"].ToString();

                                n++;
                                tnemployee.ToolTip = dr["userName"].ToString();
                                tn.Nodes.Add(tnemployee);
                            }
                            //foreach (ESP.HumanResource.Entity.EmployeeBaseInfo employeeinfo in employeeList)
                            //{
                            //    if (employeeinfo.Status != ESP.HumanResource.Common.Status.Dimission)
                            //    {
                            //        ComponentArt.Web.UI.TreeViewNode tnemployee = new ComponentArt.Web.UI.TreeViewNode();
                            //        tnemployee.Text = employeeinfo.FullNameCN;
                            //        tnemployee.Value = employeeinfo.UserID.ToString();
                            //        tnemployee.ImageUrl = "/images/treeview/user.gif";
                            //        tnemployee.NavigateUrl = "IntegratedQueryList.aspx?ApplicantID=" + employeeinfo.UserID;

                            //        n++;
                            //        tnemployee.ToolTip = employeeinfo.FullNameCN;

                            //        tn.Nodes.Add(tnemployee);
                            //    }
                            //}
                            //employeeList = null;
                            ds = null;
                            departmentId = 0;
                        }

                        BindTree(depinfo, tn, info.DepartmentID, ref n, treenode, isP, isHerf);
                    }
                }
            }
        }

        /// <summary>
        /// 获得当天用户所能产看的部门信息
        /// </summary>
        public void GetDepartmentInfo()
        {
            DataCodeInfo datamodel = new DataCodeManager().GetDataCodeByType("BeijingAttendanceAdmin")[0];
            string[] datacode = datamodel.Code.Split(new char[] { ',' });
            DataCodeInfo datamodelshanghai = new DataCodeManager().GetDataCodeByType("ShanghaiAttendanceAdmin")[0];
            string[] datacodeshanghai = datamodelshanghai.Code.Split(new char[] { ',' });
            DataCodeInfo datamodelguangzhou = new DataCodeManager().GetDataCodeByType("GuangzhouAttendanceAdmin")[0];
            string[] datacodeguangzhou = datamodelguangzhou.Code.Split(new char[] { ',' });

            DataCodeInfo datamodelAll = new DataCodeManager().GetDataCodeByType("CEOSeeAllStatisticIDs")[0];
           

            // 获得团队HRAdmin和考勤统计员的编号
            DataCodeInfo HRAdminIdModel = new DataCodeManager().GetDataCodeByType("HRAdminIDs")[0];
            string HRAdminIds = HRAdminIdModel.Code;
            DataCodeInfo StatisticianIDModel = new DataCodeManager().GetDataCodeByType("StatisticianIDs")[0];
            string StatisticianIds = StatisticianIDModel.Code;

            // 局部变量：保存部门信息包括1,2,3级部门信息
            IList<ESP.Framework.Entity.DepartmentInfo> depinfo = new List<ESP.Framework.Entity.DepartmentInfo>();
            ESP.Administrative.BusinessLogic.OperationAuditManageManager operationAuditManager = new ESP.Administrative.BusinessLogic.OperationAuditManageManager();
            if (datamodelAll.Code.IndexOf(UserID.ToString()) >= 0)
            {
                IList<ESP.Framework.Entity.DepartmentInfo> list = null;
                list = ESP.Framework.BusinessLogic.DepartmentManager.GetChildrenRecursion(0);

                if (list != null && list.Count > 0)
                {
                    Dictionary<int, ESP.Framework.Entity.DepartmentInfo> dic = new Dictionary<int, ESP.Framework.Entity.DepartmentInfo>();

                    foreach (ESP.Framework.Entity.DepartmentInfo departmentinfo in list)
                    {
                        // 判断这个部门是否已经在Dictionary中了
                        if (!dic.ContainsKey(departmentinfo.DepartmentID))
                            dic.Add(departmentinfo.DepartmentID, departmentinfo);
                    }

                    // 将Dictionary中的值保存到一个List的集合中
                    Dictionary<int, ESP.Framework.Entity.DepartmentInfo>.ValueCollection valueColl = dic.Values;
                    foreach (ESP.Framework.Entity.DepartmentInfo departmentInfo in valueColl)
                    {
                        depinfo.Add(departmentInfo);
                    }
                }
            }
            // 各个地区系统管理员，可以查看各个地区所有员工的考勤信息
            else if (UserID.ToString() == datacode[0] || UserID.ToString() == datacodeguangzhou[0] || UserID.ToString() == datacodeshanghai[0]
               || (datacode.Length > 3 && UserID.ToString() == datacode[2]))
            {
                IList<ESP.Framework.Entity.DepartmentInfo> list = null;
                ESP.Framework.Entity.DepartmentInfo depart = new DepartmentInfo();
                if (UserID.ToString() == datacode[0] || (datacode.Length > 3 && UserID.ToString() == datacode[2]))
                {
                    list = ESP.Framework.BusinessLogic.DepartmentManager.GetChildrenRecursion((int)AreaID.HeadOffic);
                    depart = ESP.Framework.BusinessLogic.DepartmentManager.Get((int)AreaID.HeadOffic);
                }
                else if (UserID.ToString() == datacodeguangzhou[0])
                {
                    list = ESP.Framework.BusinessLogic.DepartmentManager.GetChildrenRecursion((int)AreaID.HeadOffic);
                    depart = ESP.Framework.BusinessLogic.DepartmentManager.Get((int)AreaID.HeadOffic);
                }
                else if (UserID.ToString() == datacodeshanghai[0])
                {
                    list = ESP.Framework.BusinessLogic.DepartmentManager.GetChildrenRecursion((int)AreaID.HeadOffic);
                    depart = ESP.Framework.BusinessLogic.DepartmentManager.Get((int)AreaID.HeadOffic);
                }

                if (list != null && list.Count > 0)
                {
                    Dictionary<int, ESP.Framework.Entity.DepartmentInfo> dic = new Dictionary<int, ESP.Framework.Entity.DepartmentInfo>();
                    
                    // 判断这个部门是否已经在Dictionary中了
                    if (!dic.ContainsKey(depart.DepartmentID))
                        dic.Add(depart.DepartmentID, depart);

                    foreach (ESP.Framework.Entity.DepartmentInfo departmentinfo in list)
                    {
                        // 判断这个部门是否已经在Dictionary中了
                        if (!dic.ContainsKey(departmentinfo.DepartmentID))
                            dic.Add(departmentinfo.DepartmentID, departmentinfo);
                    }

                    // 将Dictionary中的值保存到一个List的集合中
                    Dictionary<int, ESP.Framework.Entity.DepartmentInfo>.ValueCollection valueColl = dic.Values;
                    foreach (ESP.Framework.Entity.DepartmentInfo departmentInfo in valueColl)
                    {
                        depinfo.Add(departmentInfo);
                    }
                }
            }
            else if (HRAdminIds.IndexOf(UserID.ToString()) != -1)
            {
                List<ESP.Framework.Entity.EmployeeInfo> operationAuditList = operationAuditManager.GetHRAdminSubordinates(UserID);

                DataCodeManager dataCodeManager = new DataCodeManager();
                List<DataCodeInfo> dataCodeList = dataCodeManager.GetDataCodeByType("HR" + UserID);
                if (dataCodeList != null && dataCodeList.Count > 0)
                {
                    DataCodeInfo dataCodeModel = dataCodeList[0];
                    if (dataCodeModel != null)
                    {
                        List<ESP.Framework.Entity.EmployeeInfo> tempList = operationAuditManager.GetHRAdminSubordinates(int.Parse(dataCodeModel.Code));
                        operationAuditList.AddRange(tempList);
                    }
                }

                Dictionary<int, ESP.Framework.Entity.DepartmentInfo> dic = new Dictionary<int, ESP.Framework.Entity.DepartmentInfo>();
                if (operationAuditList != null && operationAuditList.Count > 0)
                {
                    foreach (ESP.Framework.Entity.EmployeeInfo operationAuditModel in operationAuditList)
                    {
                        if (operationAuditModel != null)
                        {
                            List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
                            //int depid = new UserAttBasicInfoManager().GetUserDepartmentId(operationAuditModel.UserID);
                            int depid = 0;
                            IList<ESP.Framework.Entity.EmployeePositionInfo> employeePositionInfoList = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(operationAuditModel.UserID);
                            if (employeePositionInfoList != null && employeePositionInfoList.Count > 0)
                            {
                                foreach (ESP.Framework.Entity.EmployeePositionInfo employeePositionModel in employeePositionInfoList)
                                {
                                    depid = employeePositionModel.DepartmentID;

                                    ESP.Framework.Entity.DepartmentInfo departmentInfo = ESP.Framework.BusinessLogic.DepartmentManager.Get(depid);
                                    if (departmentInfo != null && departmentInfo.DepartmentLevel == 3)
                                    {
                                        // 添加当前用户上级部门信息
                                        depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(depid, depList);
                                        foreach (ESP.Framework.Entity.DepartmentInfo dm in depList)
                                        {
                                            // 判断这个部门是否已经在Dictionary中了
                                            if (!dic.ContainsKey(dm.DepartmentID))
                                                dic.Add(dm.DepartmentID, dm);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                // 将Dictionary中的值保存到一个List的集合中
                Dictionary<int, ESP.Framework.Entity.DepartmentInfo>.ValueCollection valueColl = dic.Values;
                foreach (ESP.Framework.Entity.DepartmentInfo departmentInfo in valueColl)
                {
                    depinfo.Add(departmentInfo);
                }
            }
            else if (StatisticianIds.IndexOf(UserID.ToString()) != -1)
            {
                List<ESP.Framework.Entity.EmployeeInfo> operationAuditList = operationAuditManager.GetStatisticianSubordinates(UserID);
                Dictionary<int, ESP.Framework.Entity.DepartmentInfo> dic = new Dictionary<int, ESP.Framework.Entity.DepartmentInfo>();
                if (operationAuditList != null && operationAuditList.Count > 0)
                {
                    foreach (ESP.Framework.Entity.EmployeeInfo operationAuditModel in operationAuditList)
                    {
                        if (operationAuditModel != null && operationAuditModel.UserID != 0)
                        {
                            List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
                            int depid = new UserAttBasicInfoManager().GetUserDepartmentId(operationAuditModel.UserID);
                            ESP.Framework.Entity.DepartmentInfo departmentInfo = ESP.Framework.BusinessLogic.DepartmentManager.Get(depid);
                            if (departmentInfo != null && departmentInfo.DepartmentLevel == 3)
                            {
                                // 添加当前用户上级部门信息
                                depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(depid, depList);
                                foreach (ESP.Framework.Entity.DepartmentInfo dm in depList)
                                {
                                    // 判断这个部门是否已经在Dictionary中了
                                    if (!dic.ContainsKey(dm.DepartmentID))
                                        dic.Add(dm.DepartmentID, dm);
                                }
                            }
                        }
                    }
                }
                // 将Dictionary中的值保存到一个List的集合中
                Dictionary<int, ESP.Framework.Entity.DepartmentInfo>.ValueCollection valueColl = dic.Values;
                foreach (ESP.Framework.Entity.DepartmentInfo departmentInfo in valueColl)
                {
                    depinfo.Add(departmentInfo);
                }
            }
            else
            {
                // 获得当前登录用户的人员部门信息
                IList<ESP.Framework.Entity.EmployeePositionInfo> list = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(UserID);

                // 获得用
                List<ESP.Administrative.Entity.OperationAuditManageInfo> operationAuditList = operationAuditManager.GetModelList(" TeamLeaderID=" + UserID);
                if (list != null && list.Count > 0)
                {
                    Dictionary<int, ESP.Framework.Entity.DepartmentInfo> dic = new Dictionary<int, ESP.Framework.Entity.DepartmentInfo>();
                    foreach (ESP.Framework.Entity.EmployeePositionInfo empPosInfo in list)
                    {
                        List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
                        ESP.Framework.Entity.DepartmentInfo departmentInfo = ESP.Framework.BusinessLogic.DepartmentManager.Get(empPosInfo.DepartmentID);
                        if (departmentInfo != null && departmentInfo.DepartmentLevel == 3)
                        {
                            // 添加当前用户上级部门信息
                            depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(empPosInfo.DepartmentID, depList);
                            foreach (ESP.Framework.Entity.DepartmentInfo dm in depList)
                            {
                                // 判断这个部门是否已经在Dictionary中了
                                if (!dic.ContainsKey(dm.DepartmentID))
                                    dic.Add(dm.DepartmentID, dm);
                            }
                        }
                    }

                    if (operationAuditList != null && operationAuditList.Count > 0)
                    {
                        foreach (ESP.Administrative.Entity.OperationAuditManageInfo operationAuditModel in operationAuditList)
                        {
                            List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
                            int depid = new UserAttBasicInfoManager().GetUserDepartmentId(operationAuditModel.UserID);
                            ESP.Framework.Entity.DepartmentInfo departmentInfo = ESP.Framework.BusinessLogic.DepartmentManager.Get(depid);
                            if (departmentInfo != null && departmentInfo.DepartmentLevel == 3)
                            {
                                // 添加当前用户上级部门信息
                                depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(depid, depList);
                                foreach (ESP.Framework.Entity.DepartmentInfo dm in depList)
                                {
                                    // 判断这个部门是否已经在Dictionary中了
                                    if (!dic.ContainsKey(dm.DepartmentID))
                                        dic.Add(dm.DepartmentID, dm);
                                }
                            }
                        }
                    }

                    // 将Dictionary中的值保存到一个List的集合中
                    Dictionary<int, ESP.Framework.Entity.DepartmentInfo>.ValueCollection valueColl = dic.Values;
                    foreach (ESP.Framework.Entity.DepartmentInfo departmentInfo in valueColl)
                    {
                        depinfo.Add(departmentInfo);
                    }
                }
            }

            // 创建一个人员节点数的根节点
            ComponentArt.Web.UI.TreeViewNode rootNode = new ComponentArt.Web.UI.TreeViewNode();
            rootNode.Text = "星言云汇人力结构图";
            rootNode.Expanded = true;
            rootNode.ImageUrl = "/images/treeview/root.gif";
            userTreeView.Nodes.Add(rootNode);
            int n = 0;
            // 获得部门信息包括1,2,3级部门信息
            string str = "";
            bool isP = false;
            bool isHerf = false;
            // 开始绑定人员节点数
            depinfo = depinfo.OrderBy(c => c.DepartmentName).ToList<ESP.Framework.Entity.DepartmentInfo>();
            BindTree(depinfo, rootNode, 0, ref  n, str, isP, isHerf);
        }

        /// <summary>
        /// 绑定考勤统计日期值
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public string DataBindGetDay(ComponentArt.Web.UI.CalendarDay day)
        {
            return day.Date.Day.ToString();
        }

        #region 考勤事由变量定义
        /// <summary>
        /// 考勤业务对象类
        /// </summary>
        private ESP.Administrative.BusinessLogic.AttendanceManager attMan =
            new ESP.Administrative.BusinessLogic.AttendanceManager();
        /// <summary>
        /// OT单业务对象
        /// </summary>
        private SingleOvertimeManager singleOverTime = new SingleOvertimeManager();
        /// <summary>
        /// 考勤时间记录信息业务对象
        /// </summary>
        private ClockInManager clockInManager = new ClockInManager();
        /// <summary>
        /// 用户考勤基本信息业务对象
        /// </summary>
        private UserAttBasicInfoManager userBasicManager = new UserAttBasicInfoManager();
        /// <summary>
        /// 事由业务对象
        /// </summary>
        private MattersManager mattersManager = new MattersManager();
        /// <summary>
        /// 每月考勤业务对象类
        /// </summary>
        private MonthStatManager monthManager = new MonthStatManager();
        /// <summary>
        /// 用户上下班时间信息集合
        /// </summary>
        private CommuterTimeManager commuterTimeManager = new CommuterTimeManager();
        /// <summary>
        /// 打卡记录集合
        /// </summary>
        Hashtable clockInTimes = null;
        /// <summary>
        /// 考勤事由集合
        /// </summary>
        IList<MattersInfo> matters = null;
        /// <summary>
        /// OT单信息集合
        /// </summary>
        IList<SingleOvertimeInfo> overtimes = null;
        /// <summary>
        /// 节假日信息集合
        /// </summary>
        HashSet<int> holidays = null;
        /// <summary>
        /// 当前用户基本信息
        /// </summary>
        UserAttBasicInfo userBasicModel = null;
        /// <summary>
        /// 用户提示信息
        /// </summary>
        private string Tips = "";
        /// <summary>
        /// 员工入职信息
        /// </summary>
        ESP.HumanResource.Entity.EmployeeJobInfo employeeJobModel = null;
        /// <summary>
        /// 员工离职信息
        /// </summary>
        ESP.HumanResource.Entity.DimissionInfo dimissionModel = null;
        /// <summary>
        /// 用户上下班时间信息集合
        /// </summary>
        List<CommuterTimeInfo> commuterTimeList = null;
        #endregion

        /// <summary>
        /// 绑定考勤信息日历
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        public void BindCalender(int year, int month)
        {
            clockInTimes = clockInManager.GetClockInTimesOfMonth(year, month, int.Parse(SelectUserID));
            holidays = new HolidaysInfoManager().GetHolidayListByMonth(year, month);
            matters = mattersManager.GetModelListByMonth(int.Parse(SelectUserID), year, month);
            overtimes = new SingleOvertimeManager().GetModelListByMonth(int.Parse(SelectUserID), year, month);
            userBasicModel = userBasicManager.GetModelByUserid(int.Parse(SelectUserID));
            employeeJobModel = ESP.HumanResource.BusinessLogic.EmployeeJobManager.getModelBySysId(int.Parse(SelectUserID));
            dimissionModel = ESP.HumanResource.BusinessLogic.DimissionManager.GetModelByUserID(int.Parse(SelectUserID));
            commuterTimeList = commuterTimeManager.GetCommuterTimeByUserId(int.Parse(SelectUserID));

            int days = DateTime.DaysInMonth(year, month);

            for (int i = 1; i <= days; i++)
            {
                ComponentArt.Web.UI.CalendarDay day = new ComponentArt.Web.UI.CalendarDay();
                day.Date = new DateTime(year, month, i);
                day.TemplateId = "DefaultTemplate";
                if (holidays.Contains(i))
                    day.CssClass = "holidayday";
                else
                    day.CssClass = "day";
                cldAttendance.CustomDays.Add(day);
            }
        }

        /// <summary>
        /// 绑定上班时间
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public string DataBindGetClockIn(ComponentArt.Web.UI.CalendarDay day)
        {
            StringBuilder builder = new StringBuilder();
            if (employeeJobModel != null && employeeJobModel.joinDate.Date <= day.Date)
            {
                object obj = clockInTimes[(long)day.Date.Day];
                if (obj == null)
                    return "";
                string color = Status.normal;
                ArrayList list = (ArrayList)obj;
                if (list != null && list.Count > 0)
                {
                    DateTime clockIn = ((DateTime)list[0]);
                    string titleStr = "";
                    if (holidays.Contains(day.Date.Day))
                    {
                        color = Status.weekend;
                    }
                    else
                    {
                        CommuterTimeInfo commuterModel = commuterTimeManager.GetCommuterTimes(commuterTimeList, day.Date);
                        if (clockIn.Date == day.Date
                            && clockIn.TimeOfDay >= commuterModel.GoWorkTime.TimeOfDay.Add(new TimeSpan(0, Status.GoWorkTime_BufferMinute, 0)))
                        {
                            color = Status.improper;
                        }
                    }
                    if (list.Count > 1 && list[1] != null && !string.IsNullOrEmpty(list[1].ToString()))
                    {
                        color = Status.operatorTime;
                        if (list.Count > 2 && list[2] != null && !string.IsNullOrEmpty(list[2].ToString()))
                        {
                            titleStr = list[1].ToString() + "录入，备注：" + list[2].ToString();
                        }
                    }
                    builder.Append("<span style=\"color:").Append(color).Append("\" title='" + titleStr + "'>").Append(clockIn.ToString("HH:mm")).Append("</span>");
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// 绑定下班时间，如果是当天则不显示下班时间
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public string DataBindGetClockOut(ComponentArt.Web.UI.CalendarDay day)
        {
            // 获得上下班时间信息
            CommuterTimeInfo commuterModel = commuterTimeManager.GetCommuterTimes(commuterTimeList, day.Date);
            if (employeeJobModel != null && employeeJobModel.joinDate.Date <= day.Date)
            {
                // 如果绑定的是当前日期的考勤信息，不显示下班时间
                if (day.Date == DateTime.Now.Date)
                {
                    return "";
                }
                else
                {
                    StringBuilder builder = new StringBuilder();
                    object obj = clockInTimes[(long)-day.Date.Day];
                    if (obj == null)
                        return "";

                    string color = Status.normal;
                    ArrayList list = (ArrayList)obj;
                    if (list != null && list.Count > 0)
                    {
                        DateTime clockOut = ((DateTime)list[0]);
                        string titleStr = "";
                        if (holidays.Contains(day.Date.Day))
                        {
                            color = Status.weekend;
                        }
                        else
                        {
                            // 判断时间是否正常，如果不正常则显示相对应的颜色
                            if (clockOut.Date == day.Date &&
                                clockOut.TimeOfDay < commuterModel.OffWorkTime.TimeOfDay)
                            {
                                color = Status.improper;
                            }
                            // 计算工作日OT超过几点后有效
                            TimeSpan span = commuterModel.OffWorkTime.AddHours(commuterModel.WorkingDays_OverTime1).TimeOfDay;
                            if (clockOut.Date > day.Date
                                || (clockOut.Date == day.Date
                                && clockOut.TimeOfDay > span))
                            {
                                color = Status.overNine;
                            }
                        }
                        if (list.Count > 1 && list[1] != null && !string.IsNullOrEmpty(list[1].ToString()))
                        {
                            color = Status.operatorTime;
                            if (list.Count > 2 && list[2] != null && !string.IsNullOrEmpty(list[2].ToString()))
                            {
                                titleStr = list[1].ToString() + "录入，备注：" + list[2].ToString();
                            }
                        }
                        builder.Append("<span style=\"color:").Append(color).Append("\" title='" + titleStr + "'>").Append(clockOut.ToString("HH:mm")).Append("</span>");
                        return builder.ToString();
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// 判断显示考勤统计图表内容
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public string DataBindGetIconsHtml(ComponentArt.Web.UI.CalendarDay day)
        {
            // 获得上下班时间信息
            CommuterTimeInfo commuterModel = commuterTimeManager.GetCommuterTimes(commuterTimeList, day.Date);

            StringBuilder builder = new StringBuilder();
            DateTime today = day.Date;
            DateTime tomorrow = today.AddDays(1);
            if (employeeJobModel != null && employeeJobModel.joinDate.Date <= today 
                && (dimissionModel == null || (dimissionModel != null && today <= dimissionModel.dimissionDate.Date)))
            {
                ArrayList clockinList = (ArrayList)clockInTimes[(long)day.Date.Day];
                ArrayList clockoutList = (ArrayList)clockInTimes[(long)-day.Date.Day];
                // 上班时间
                DateTime clockIn = new DateTime(1900, 1, 1);
                if (clockinList != null && clockinList.Count > 0)
                {
                    clockIn = (DateTime)clockinList[0];
                }
                // 下班时间
                DateTime clockOut = new DateTime(1900, 1, 1);
                if (clockoutList != null && clockoutList.Count > 0)
                {
                    clockOut = (DateTime)clockoutList[0];
                }
                #region 判断人员考勤的类型，如果是考勤是正常（普通员工）的就计算考勤情况
                if (commuterModel.AttendanceType == Status.UserBasicAttendanceType_Normal)
                {
                    #region 判断显示当天的OT信息
                    //foreach (SingleOvertimeInfo info in overtimes)
                    //{
                    //    if ((info.BeginTime >= today && info.BeginTime < tomorrow))
                    //    {
                    //        int imgType = 0;
                    //        switch (info.Approvestate)
                    //        {
                    //            case Status.OverTimeState_NotSubmit:
                    //            case Status.OverTimeState_Cancel:
                    //            case Status.OverTimeState_Overrule:
                    //                imgType = 1;
                    //                break;
                    //            case Status.OverTimeState_Passed:
                    //                imgType = 3;
                    //                break;
                    //            case Status.OverTimeState_WaitDirector:
                    //            case Status.OverTimeState_WaitHR:
                    //                imgType = 2;
                    //                break;
                    //            default:
                    //                imgType = 1;
                    //                break;
                    //        }

                    //        if (imgType == 1)
                    //        {
                    //            string url = string.Format(Status.defaultUrl, "", info.ID, 1);
                    //        }
                    //        string imgUrl = "/images/calendar/" + imgType + "jiaban.jpg";
                    //        string title;
                    //        switch (imgType)
                    //        {
                    //            case 2:
                    //                title = "OT 审批中 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                    //                    + "  " + info.OverTimeCause;
                    //                break;
                    //            case 3:
                    //                title = "OT 已审批 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                    //                    + "  " + info.OverTimeCause;
                    //                break;
                    //            case 1:
                    //            default:
                    //                title = "OT 未提交 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                    //                    + "  " + info.OverTimeCause;
                    //                break;
                    //        }

                    //        builder.Append("<img alt=\"OT\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");

                    //        if (imgType == 1)
                    //        {
                    //        }
                    //    }
                    //}
                    #endregion

                    #region 根据上下班时间计算考勤事由信息
                    // 是否迟到
                    bool isLate = false;
                    // 是否上午旷工
                    bool isAMAbsent = false;
                    // 是否下午旷工
                    bool isPMAbsent = false;
                    // 是否早退
                    bool isLeaveEarly = false;
                    // 考勤异常时间数
                    TimeSpan abnormityTime = new TimeSpan(0);
                    attMan.CalDefaultMatters(int.Parse(SelectUserID), today, clockIn, clockOut,
                        out isLate, out isAMAbsent, out isPMAbsent, out isLeaveEarly, out abnormityTime, commuterModel);
                    long abnormityTicks = abnormityTime.Ticks;
                    #endregion

                    #region 计算前一天的OT信息，判断是否去除当天的迟到和矿工
                    // 获得前一天的OT单信息，并且是审批通过的OT单
                    //List<SingleOvertimeInfo> beforeDaySingleList = singleOverTime.GetSingleOvertimeList(int.Parse(SelectUserID), today.AddDays(-1), true);
                    //if (beforeDaySingleList != null && beforeDaySingleList.Count > 0)
                    //{
                    //    foreach (SingleOvertimeInfo single in beforeDaySingleList)
                    //    {
                    //        if (single != null)
                    //        {
                    //            HolidaysInfo holidayModel = new HolidaysInfoManager().GetHolideysInfoByDatetime(today.AddDays(-1));
                    //            // 判断用户的OT开始时间是否大于用户的下班时间
                    //            if (single.BeginTime >= single.BeginTime.Date.Add(commuterModel.OffWorkTime.TimeOfDay)
                    //                || ((holidays.Contains(today.AddDays(-1).Day) || holidayModel != null)
                    //                    && single.EndTime > single.BeginTime.Date.Add(commuterModel.OffWorkTime.TimeOfDay)))
                    //            {
                    //                decimal overTimeHours = 0;
                    //                if (!holidays.Contains(today.AddDays(-1).Day) && holidayModel == null)
                    //                {
                    //                    overTimeHours = single.OverTimeHours;
                    //                }
                    //                else
                    //                {
                    //                    overTimeHours = (decimal)(single.EndTime - single.BeginTime.Date.Add(commuterModel.OffWorkTime.TimeOfDay)).TotalHours;
                    //                }
                    //                // 用户OT小时数大于6小时
                    //                if (overTimeHours >= (decimal)commuterModel.WorkingDays_OverTime2)
                    //                {
                    //                    TimeSpan tempSpan = new TimeSpan(Status.AMWorkingHours, 0, 0);
                    //                    abnormityTicks -= tempSpan.Ticks;
                    //                    if (isAMAbsent)
                    //                    {
                    //                        isAMAbsent = false;
                    //                    }
                    //                    else if (isLate)
                    //                    {
                    //                        isLate = false;
                    //                    }
                    //                    else if (isPMAbsent)
                    //                    {
                    //                        TimeSpan timeDifference = TimeSpan.FromTicks(abnormityTicks);
                    //                        if (timeDifference.TotalHours > Status.NumberOfHours_Absent)
                    //                        {
                    //                            isPMAbsent = true;
                    //                        }
                    //                        else if (timeDifference.TotalHours > Status.NumberOfHours_Late)
                    //                        {
                    //                            isAMAbsent = true;
                    //                        }
                    //                        else
                    //                        {
                    //                            isLate = true;
                    //                        }
                    //                        //DateTime goDateTime = DateTime.Parse(today.ToString("yyyy-MM-dd ") + userBasicModel.GoWorkTime);
                    //                        //DateTime pmGoDateTime = goDateTime.AddHours(Status.GoWorkTime_AMGapPM);
                    //                        //TimeSpan span = clockIn - pmGoDateTime;
                    //                        //// 判断用户超出时间是否是在迟到的时间范围内
                    //                        //if (TimeSpan.FromTicks(abnormityTicks).TotalMinutes > 0)
                    //                        //{
                    //                        //    isLate = true;
                    //                        //}
                    //                        //// 判断用户超出时间是否是在旷工的时间范围内
                    //                        //else if (span.TotalHours < Status.NumberOfHours_Absent)
                    //                        //{
                    //                        //    isAMAbsent = true;
                    //                        //}
                    //                        //// 否则的话用户超出的时间已经超过了旷工半天的时间了
                    //                        //else
                    //                        //{
                    //                        //    isPMAbsent = true;
                    //                        //}
                    //                    }
                    //                }
                    //                else if (overTimeHours >= (decimal)commuterModel.WorkingDays_OverTime1)
                    //                {
                    //                    //TimeSpan tempSpan = new TimeSpan(Status.LateGoWorkTime_OverTime1, 0, 0);
                    //                    DateTime tempSpan = new DateTime().AddHours(commuterModel.LateGoWorkTime_OverTime1);
                    //                    abnormityTicks -= tempSpan.Ticks;
                    //                    if (isLate)
                    //                    {
                    //                        isLate = false;
                    //                    }
                    //                    else if (isAMAbsent)
                    //                    {
                    //                        TimeSpan timeDifference = TimeSpan.FromTicks(abnormityTicks);
                    //                        if (timeDifference.TotalHours > Status.NumberOfHours_Absent)
                    //                        {
                    //                            isPMAbsent = true;
                    //                        }
                    //                        else if (timeDifference.TotalHours > Status.NumberOfHours_Late)
                    //                        {
                    //                            isAMAbsent = true;
                    //                        }
                    //                        else
                    //                        {
                    //                            isLate = true;
                    //                        }
                    //                        //DateTime goDateTime = DateTime.Parse(today.ToString("yyyy-MM-dd ") + userBasicModel.GoWorkTime);
                    //                        //DateTime pmGoDateTime = goDateTime.AddHours(Status.NumberOfHours_Late);
                    //                        //TimeSpan span = clockIn - pmGoDateTime;
                    //                        //// 判断用户超出时间是否是在迟到的时间范围内
                    //                        //if (span.TotalHours < Status.NumberOfHours_Late)
                    //                        //{
                    //                        //    isLate = true;
                    //                        //}
                    //                        //// 判断用户超出时间是否是在旷工的时间范围内
                    //                        //else if (span.TotalHours < Status.NumberOfHours_Absent)
                    //                        //{
                    //                        //    isAMAbsent = true;
                    //                        //}
                    //                        //// 否则的话用户超出的时间已经超过了旷工半天的时间了
                    //                        //else
                    //                        //{
                    //                        //    isPMAbsent = true;
                    //                        //}
                    //                    }
                    //                    else if (isPMAbsent)
                    //                    {
                    //                        TimeSpan timeDifference = TimeSpan.FromTicks(abnormityTicks);
                    //                        if (timeDifference.TotalHours > Status.NumberOfHours_Absent)
                    //                        {
                    //                            isPMAbsent = true;
                    //                        }
                    //                        else if (timeDifference.TotalHours > Status.NumberOfHours_Late)
                    //                        {
                    //                            isAMAbsent = true;
                    //                        }
                    //                        else
                    //                        {
                    //                            isLate = true;
                    //                        }
                    //                        //DateTime goDateTime = DateTime.Parse(today.ToString("yyyy-MM-dd ") + userBasicModel.GoWorkTime);
                    //                        //DateTime pmGoDateTime = goDateTime.AddHours(Status.GoWorkTime_AMGapPM);
                    //                        //TimeSpan span = clockIn - pmGoDateTime;
                    //                        //// 判断用户超出时间是否是在迟到的时间范围内
                    //                        //if (span.TotalHours < Status.NumberOfHours_Late)
                    //                        //{
                    //                        //    isLate = true;
                    //                        //}
                    //                        //// 判断用户超出时间是否是在旷工的时间范围内
                    //                        //else if (span.TotalHours < Status.NumberOfHours_Absent)
                    //                        //{
                    //                        //    isAMAbsent = true;
                    //                        //}
                    //                        //// 否则的话用户超出的时间已经超过了旷工半天的时间了
                    //                        //else
                    //                        //{
                    //                        //    isPMAbsent = true;
                    //                        //}
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    #endregion

                    #region 判断显示当天的考勤事由信息
                    foreach (MattersInfo info in matters)
                    {
                        if ((info.BeginTime >= today && info.BeginTime < tomorrow)
                            || (info.EndTime > today && info.EndTime <= tomorrow)
                            || (info.BeginTime <= today && info.EndTime >= tomorrow))
                        {
                            attMan.CalAttendance(ref isLate, ref isAMAbsent, ref isPMAbsent, ref isLeaveEarly, info, commuterModel, clockIn, clockOut, today, ref abnormityTicks);
                            int imgType = 0;
                            switch (info.MatterState)
                            {
                                case 1:
                                case 5:
                                case 6:
                                    imgType = 1;
                                    break;
                                case 2:
                                    imgType = 3;
                                    break;
                                case 3:
                                case 4:
                                    imgType = 2;
                                    break;
                                default:
                                    imgType = 1;
                                    break;
                            }

                            string matterType;
                            switch (info.MatterType)
                            {
                                case 1:
                                    matterType = "病假";
                                    break;

                                case 2:
                                    matterType = "事假";
                                    break;

                                case 3:
                                    matterType = "年假";
                                    break;

                                case 4:
                                    matterType = "婚假";
                                    break;

                                case 5:
                                    matterType = "产假";
                                    break;

                                case 6:
                                    matterType = "丧假";
                                    break;

                                case 7:
                                    matterType = "出差";
                                    break;

                                case 8:
                                    matterType = "外出";
                                    break;

                                case 9:
                                    matterType = "调休";
                                    break;
                                case 10:
                                    matterType = "其它";
                                    break;
                                case 11:
                                    matterType = "产检";
                                    break;
                                case 12:
                                    matterType = "福利假";
                                    break;
                                case 14:
                                    matterType = "陪产假";
                                    break;
                                case 15:
                                    matterType = "年假(补)";
                                    break;
                                default:
                                    continue;
                            }

                            string imagename;
                            int t = 0;
                            imagename = "qingjia";

                            if (info.MatterType == 8)
                            {
                                t = 2;
                                imagename = "waichu";
                            }
                            if (info.MatterType == 7)
                            {
                                t = 3;
                                imagename = "chuchai";
                            }
                            if (info.MatterType == 9)
                            {
                                t = 4;
                                imagename = "tiaoxiu";
                            }
                            if (info.MatterType == 10)
                            {
                                t = 5;
                                imagename = "qita";
                            }

                            if (!holidays.Contains(today.Day))
                            {
                                if (imgType == 1)
                                {
                                    string url = string.Format(Status.defaultUrl, "", info.ID, t);
                                }
                                string imgUrl = "/images/calendar/" + imgType + imagename + ".jpg";
                                string title;
                                switch (imgType)
                                {
                                    case 2:
                                        title = matterType + " 审批中 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                    case 3:
                                        title = matterType + " 已审批 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                    case 1:
                                    default:
                                        title = matterType + " 未提交 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                }

                                builder.Append("<img alt=\"OT\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");

                                if (imgType == 1)
                                {
                                }
                            }
                            else if (info.MatterType == 4 || info.MatterType == 5 || info.MatterType == 6 || info.MatterType == 7 || info.MatterType == 14)
                            {
                                if (imgType == 1)
                                {
                                    string url = string.Format(Status.defaultUrl, "", info.ID, t);
                                }
                                string imgUrl = "/images/calendar/" + imgType + imagename + ".jpg";
                                string title;
                                switch (imgType)
                                {
                                    case 2:
                                        title = matterType + " 审批中 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                    case 3:
                                        title = matterType + " 已审批 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                    case 1:
                                    default:
                                        title = matterType + " 未提交 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                }

                                builder.Append("<img alt=\"OT\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");

                                if (imgType == 1)
                                {
                                }
                            }
                        }
                    }
                    #endregion

                    #region 判断用户是否是第一天入职
                    if (employeeJobModel.joinDate.Date == today && clockOut.ToString("yyyy-MM-dd") != Status.EmptyTime)
                    {
                        isLate = false;
                        isAMAbsent = false;
                        isPMAbsent = false;
                        abnormityTicks = 0;
                    }
                    #endregion

                    TimeSpan remainTime = TimeSpan.FromTicks(abnormityTicks);
                    #region 判断显示考勤默认事由信息
                    if (!holidays.Contains(today.Day))
                    {
                        if (isLate && remainTime.TotalMinutes >= Status.GoWorkTime_BufferMinute && remainTime.TotalHours <= Status.NumberOfHours_Late)
                        {
                            string title = "迟到";
                            string imgUrl = "/images/calendar/3chidao.jpg";
                            builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                        }
                        else if (remainTime.TotalHours > Status.NumberOfHours_Late && remainTime.TotalHours <= Status.NumberOfHours_Absent)
                        {
                            if (isLate && isLeaveEarly)
                            {
                                string title = "迟到";
                                string imgUrl = "/images/calendar/3chidao.jpg";
                                builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");

                                string title2 = "早退";
                                string imgUrl2 = "/images/calendar/3zaotui.jpg";
                                builder.Append("<img alt=\"").Append(Server.HtmlEncode(title2)).Append("\" title=\"").Append(Server.HtmlEncode(title2)).Append("\" src=\"").Append(imgUrl2).Append("\"/>");
                            }
                            else if (isLate)
                            {
                                string title = "迟到";
                                string imgUrl = "/images/calendar/3chidao.jpg";
                                builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                            }
                            else if (isLeaveEarly)
                            {
                                string title2 = "早退";
                                string imgUrl2 = "/images/calendar/3zaotui.jpg";
                                builder.Append("<img alt=\"").Append(Server.HtmlEncode(title2)).Append("\" title=\"").Append(Server.HtmlEncode(title2)).Append("\" src=\"").Append(imgUrl2).Append("\"/>");
                            }
                            else
                            {
                                string title = "旷工半天";
                                string imgUrl = "/images/calendar/3kuanggong.jpg";
                                builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                            }
                        }
                        else if (remainTime.TotalHours > Status.NumberOfHours_Absent)
                        {
                            if (isLate)
                            {
                                string title = "迟到";
                                string imgUrl = "/images/calendar/3chidao.jpg";
                                builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");

                                string title2 = "旷工半天";
                                string imgUrl2 = "/images/calendar/3kuanggong.jpg";
                                builder.Append("<img alt=\"").Append(Server.HtmlEncode(title2)).Append("\" title=\"").Append(Server.HtmlEncode(title2)).Append("\" src=\"").Append(imgUrl2).Append("\"/>");
                            }
                            else if (isLeaveEarly)
                            {
                                string title2 = "早退";
                                string imgUrl2 = "/images/calendar/3zaotui.jpg";
                                builder.Append("<img alt=\"").Append(Server.HtmlEncode(title2)).Append("\" title=\"").Append(Server.HtmlEncode(title2)).Append("\" src=\"").Append(imgUrl2).Append("\"/>");

                                string title = "旷工半天";
                                string imgUrl = "/images/calendar/3kuanggong.jpg";
                                builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                            }
                            else
                            {
                                string title = "旷工一天";
                                string imgUrl = "/images/calendar/3kuanggong.jpg";
                                builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                            }
                        }
                        else if (isLeaveEarly && remainTime.TotalMinutes >= 1 && remainTime.TotalHours <= Status.NumberOfHours_Late)
                        {
                            string title = "早退";
                            string imgUrl = "/images/calendar/3zaotui.jpg";
                            builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                        }
                        else if (remainTime.TotalMinutes >= 1 && remainTime.TotalHours <= Status.NumberOfHours_Late)
                        {
                            if (clockIn > today.Date.Add(commuterModel.GoWorkTime.TimeOfDay) && (clockIn - today.Date.Add(commuterModel.GoWorkTime.TimeOfDay)).TotalMinutes >= Status.GoWorkTime_BufferMinute)
                            {
                                string title = "迟到";
                                string imgUrl = "/images/calendar/3chidao.jpg";
                                builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                            }
                            else if (clockOut < today.Date.Add(commuterModel.OffWorkTime.TimeOfDay) && today.Date < DateTime.Now.Date && (clockOut - today.Date.Add(commuterModel.OffWorkTime.TimeOfDay)).TotalMinutes >= 1)
                            {
                                string title = "早退";
                                string imgUrl = "/images/calendar/3zaotui.jpg";
                                builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                            }
                        }

                        //if (isLate)
                        //{
                        //    string title = "迟到";
                        //    string imgUrl = "/images/calendar/3chidao.jpg";
                        //    builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                        //}
                        //else if (isAMAbsent)
                        //{
                        //    string title = "旷工半天";
                        //    string imgUrl = "/images/calendar/3kuanggong.jpg";
                        //    builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                        //}
                        //else if (isPMAbsent)
                        //{
                        //    string title = "旷工一天";
                        //    string imgUrl = "/images/calendar/3kuanggong.jpg";
                        //    builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                        //}
                        //if (isLeaveEarly)
                        //{
                        //    string title = "早退";
                        //    string imgUrl = "/images/calendar/3zaotui.jpg";
                        //    builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                        //}
                    }
                    #endregion
                }
                #endregion
                else if (commuterModel.AttendanceType == Status.UserBasicAttendanceType_Special)
                {
                    #region 判断显示当天的OT信息
                    //foreach (SingleOvertimeInfo info in overtimes)
                    //{
                    //    if ((info.BeginTime >= today && info.BeginTime < tomorrow))
                    //    {
                    //        int imgType = 0;
                    //        switch (info.Approvestate)
                    //        {
                    //            case Status.OverTimeState_NotSubmit:
                    //            case Status.OverTimeState_Cancel:
                    //            case Status.OverTimeState_Overrule:
                    //                imgType = 1;
                    //                break;
                    //            case Status.OverTimeState_Passed:
                    //                imgType = 3;
                    //                break;
                    //            case Status.OverTimeState_WaitDirector:
                    //            case Status.OverTimeState_WaitHR:
                    //                imgType = 2;
                    //                break;
                    //            default:
                    //                imgType = 1;
                    //                break;
                    //        }

                    //        if (imgType == 1)
                    //        {
                    //            string url = string.Format(Status.defaultUrl, "", info.ID, 1);
                    //        }
                    //        string imgUrl = "/images/calendar/" + imgType + "jiaban.jpg";
                    //        string title;
                    //        switch (imgType)
                    //        {
                    //            case 2:
                    //                title = "OT 审批中 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                    //                    + "  " + info.OverTimeCause;
                    //                break;
                    //            case 3:
                    //                title = "OT 已审批 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                    //                    + "  " + info.OverTimeCause;
                    //                break;
                    //            case 1:
                    //            default:
                    //                title = "OT 未提交 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                    //                    + "  " + info.OverTimeCause;
                    //                break;
                    //        }

                    //        builder.Append("<img alt=\"OT\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");

                    //        if (imgType == 1)
                    //        {
                    //        }
                    //    }
                    //}
                    #endregion

                    #region 根据上下班时间计算考勤事由信息
                    // 是否迟到
                    bool isLate = false;
                    // 是否上午旷工
                    bool isAMAbsent = false;
                    // 是否下午旷工
                    bool isPMAbsent = false;
                    // 是否早退
                    bool isLeaveEarly = false;
                    TimeSpan span = new TimeSpan();

                    if (clockIn >= new DateTime(clockIn.Year, clockIn.Month, clockIn.Day, commuterModel.OffWorkTime.Hour, commuterModel.OffWorkTime.Minute, commuterModel.OffWorkTime.Second))
                    {
                        clockIn = DateTime.Parse(Status.EmptyTime);
                        clockOut = DateTime.Parse(Status.EmptyTime);
                    }

                    if (clockIn.ToString("yyyy-MM-dd") == Status.EmptyTime || clockOut.ToString("yyyy-MM-dd") == Status.EmptyTime || clockIn == clockOut)
                    {
                        if (day.Date < DateTime.Now.Date)
                        {
                            isPMAbsent = true;
                            TimeSpan workingHours = commuterModel.OffWorkTime - commuterModel.GoWorkTime;
                            int hours = workingHours.Hours;
                            int minutes = workingHours.Minutes;
                            int seconds = workingHours.Seconds;
                            if (workingHours.TotalHours > Status.WorkingHours)
                            {
                                hours = Status.WorkingHours;
                                minutes = 0;
                                seconds = 0;
                            }

                            span = span.Add(new TimeSpan(hours, minutes, seconds));
                        }
                    }
                    else
                    {
                        if (day.Date < DateTime.Now.Date)
                        {
                            TimeSpan workingHours = commuterModel.OffWorkTime - commuterModel.GoWorkTime;
                            int hours = workingHours.Hours;
                            int minutes = workingHours.Minutes;
                            int seconds = workingHours.Seconds;

                            DateTime currentOffTime = new DateTime(clockIn.Year, clockIn.Month, clockIn.Day, commuterModel.OffWorkTime.Hour, commuterModel.OffWorkTime.Minute, commuterModel.OffWorkTime.Second);
                            DateTime currentGoTime = new DateTime(clockIn.Year, clockIn.Month, clockIn.Day, commuterModel.GoWorkTime.Hour, commuterModel.GoWorkTime.Minute, commuterModel.GoWorkTime.Second);

                            if (clockIn > currentGoTime)
                                currentGoTime = clockIn;

                            if (clockOut < currentOffTime)
                            {
                                currentOffTime = clockOut;
                            }

                            TimeSpan workingHours2 = currentOffTime - currentGoTime;

                            int hours2 = workingHours2.Hours;
                            int minutes2 = workingHours2.Minutes;
                            int seconds2 = workingHours2.Seconds;
                            span = span.Add(new TimeSpan(hours - hours2, minutes - minutes2, seconds - seconds2));
                        }
                    }

                    TimeSpan abnormityTime = new TimeSpan(0);

                    attMan.CalDefaultMatters(UserID, today, clockIn, clockOut,
                       out isLate, out isAMAbsent, out isPMAbsent, out isLeaveEarly, out abnormityTime, commuterModel);

                    long abnormityTicks = span.Ticks;
                    #endregion

                    #region 计算前一天的OT信息，判断是否去除当天的迟到和矿工
                    // 获得前一天的OT单信息，并且是审批通过的OT单
                    //List<SingleOvertimeInfo> beforeDaySingleList = singleOverTime.GetSingleOvertimeList(int.Parse(SelectUserID), today.AddDays(-1), true);
                    //if (beforeDaySingleList != null && beforeDaySingleList.Count > 0)
                    //{
                    //    foreach (SingleOvertimeInfo single in beforeDaySingleList)
                    //    {
                    //        if (single != null)
                    //        {
                    //            // 判断用户的OT开始时间是否大于用户的下班时间
                    //            if (single.BeginTime >= single.BeginTime.Date.Add(commuterModel.OffWorkTime.TimeOfDay)
                    //                || ((holidays.Contains(today.AddDays(-1).Day) || new HolidaysInfoManager().GetHolideysInfoByDatetime(today.AddDays(-1)) != null)
                    //                    && single.EndTime > single.BeginTime.Date.Add(commuterModel.OffWorkTime.TimeOfDay)))
                    //            {
                    //                decimal overTimeHours = 0;
                    //                if (!holidays.Contains(today.AddDays(-1).Day))
                    //                {
                    //                    overTimeHours = single.OverTimeHours;
                    //                }
                    //                else
                    //                {
                    //                    overTimeHours = (decimal)(single.EndTime - single.BeginTime.Date.Add(commuterModel.OffWorkTime.TimeOfDay)).TotalHours;
                    //                }
                    //                // 用户OT小时数大于6小时
                    //                if (overTimeHours >= (decimal)commuterModel.WorkingDays_OverTime2)
                    //                {
                    //                    TimeSpan tempSpan = new TimeSpan(Status.AMWorkingHours, 0, 0);
                    //                    abnormityTicks -= tempSpan.Ticks;
                    //                    if (isAMAbsent)
                    //                    {
                    //                        isAMAbsent = false;
                    //                    }
                    //                    else if (isLate)
                    //                    {
                    //                        isLate = false;
                    //                    }
                    //                    else if (isPMAbsent)
                    //                    {
                    //                        TimeSpan timeDifference = TimeSpan.FromTicks(abnormityTicks);
                    //                        if (timeDifference.TotalHours > Status.NumberOfHours_Absent)
                    //                        {
                    //                            isPMAbsent = true;
                    //                        }
                    //                        else if (timeDifference.TotalHours > Status.NumberOfHours_Late)
                    //                        {
                    //                            isAMAbsent = true;
                    //                        }
                    //                        else
                    //                        {
                    //                            isLate = true;
                    //                        }
                    //                        //DateTime goDateTime = DateTime.Parse(today.ToString("yyyy-MM-dd ") + userBasicModel.GoWorkTime);
                    //                        //DateTime pmGoDateTime = goDateTime.AddHours(Status.GoWorkTime_AMGapPM);
                    //                        //TimeSpan span = clockIn - pmGoDateTime;
                    //                        //// 判断用户超出时间是否是在迟到的时间范围内
                    //                        //if (TimeSpan.FromTicks(abnormityTicks).TotalMinutes > 0)
                    //                        //{
                    //                        //    isLate = true;
                    //                        //}
                    //                        //// 判断用户超出时间是否是在旷工的时间范围内
                    //                        //else if (span.TotalHours < Status.NumberOfHours_Absent)
                    //                        //{
                    //                        //    isAMAbsent = true;
                    //                        //}
                    //                        //// 否则的话用户超出的时间已经超过了旷工半天的时间了
                    //                        //else
                    //                        //{
                    //                        //    isPMAbsent = true;
                    //                        //}
                    //                    }
                    //                }
                    //                else if (overTimeHours >= (decimal)commuterModel.WorkingDays_OverTime1)
                    //                {
                    //                    //TimeSpan tempSpan = new TimeSpan(Status.LateGoWorkTime_OverTime1, 0, 0);
                    //                    DateTime tempSpan = new DateTime().AddHours(commuterModel.LateGoWorkTime_OverTime1);
                    //                    abnormityTicks -= tempSpan.Ticks;
                    //                    if (isLate)
                    //                    {
                    //                        isLate = false;
                    //                    }
                    //                    else if (isAMAbsent)
                    //                    {
                    //                        TimeSpan timeDifference = TimeSpan.FromTicks(abnormityTicks);
                    //                        if (timeDifference.TotalHours > Status.NumberOfHours_Absent)
                    //                        {
                    //                            isPMAbsent = true;
                    //                        }
                    //                        else if (timeDifference.TotalHours > Status.NumberOfHours_Late)
                    //                        {
                    //                            isAMAbsent = true;
                    //                        }
                    //                        else
                    //                        {
                    //                            isLate = true;
                    //                        }
                    //                        //DateTime goDateTime = DateTime.Parse(today.ToString("yyyy-MM-dd ") + userBasicModel.GoWorkTime);
                    //                        //DateTime pmGoDateTime = goDateTime.AddHours(Status.NumberOfHours_Late);
                    //                        //TimeSpan span = clockIn - pmGoDateTime;
                    //                        //// 判断用户超出时间是否是在迟到的时间范围内
                    //                        //if (span.TotalHours < Status.NumberOfHours_Late)
                    //                        //{
                    //                        //    isLate = true;
                    //                        //}
                    //                        //// 判断用户超出时间是否是在旷工的时间范围内
                    //                        //else if (span.TotalHours < Status.NumberOfHours_Absent)
                    //                        //{
                    //                        //    isAMAbsent = true;
                    //                        //}
                    //                        //// 否则的话用户超出的时间已经超过了旷工半天的时间了
                    //                        //else
                    //                        //{
                    //                        //    isPMAbsent = true;
                    //                        //}
                    //                    }
                    //                    else if (isPMAbsent)
                    //                    {
                    //                        TimeSpan timeDifference = TimeSpan.FromTicks(abnormityTicks);
                    //                        if (timeDifference.TotalHours > Status.NumberOfHours_Absent)
                    //                        {
                    //                            isPMAbsent = true;
                    //                        }
                    //                        else if (timeDifference.TotalHours > Status.NumberOfHours_Late)
                    //                        {
                    //                            isAMAbsent = true;
                    //                        }
                    //                        else
                    //                        {
                    //                            isLate = true;
                    //                        }
                    //                        //DateTime goDateTime = DateTime.Parse(today.ToString("yyyy-MM-dd ") + userBasicModel.GoWorkTime);
                    //                        //DateTime pmGoDateTime = goDateTime.AddHours(Status.GoWorkTime_AMGapPM);
                    //                        //TimeSpan span = clockIn - pmGoDateTime;
                    //                        //// 判断用户超出时间是否是在迟到的时间范围内
                    //                        //if (span.TotalHours < Status.NumberOfHours_Late)
                    //                        //{
                    //                        //    isLate = true;
                    //                        //}
                    //                        //// 判断用户超出时间是否是在旷工的时间范围内
                    //                        //else if (span.TotalHours < Status.NumberOfHours_Absent)
                    //                        //{
                    //                        //    isAMAbsent = true;
                    //                        //}
                    //                        //// 否则的话用户超出的时间已经超过了旷工半天的时间了
                    //                        //else
                    //                        //{
                    //                        //    isPMAbsent = true;
                    //                        //}
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    #endregion

                    #region 判断显示当天的考勤事由信息
                    foreach (MattersInfo info in matters)
                    {
                        if ((info.BeginTime >= today && info.BeginTime < tomorrow)
                            || (info.EndTime > today && info.EndTime <= tomorrow)
                            || (info.BeginTime <= today && info.EndTime >= tomorrow))
                        {
                            attMan.CalAttendance(ref isLate, ref isAMAbsent, ref isPMAbsent, ref isLeaveEarly, info, commuterModel, clockIn, clockOut, today, ref abnormityTicks);
                            int imgType = 0;
                            switch (info.MatterState)
                            {
                                case 1:
                                case 5:
                                case 6:
                                    imgType = 1;
                                    break;
                                case 2:
                                    imgType = 3;
                                    break;
                                case 3:
                                case 4:
                                    imgType = 2;
                                    break;
                                default:
                                    imgType = 1;
                                    break;
                            }

                            string matterType;
                            switch (info.MatterType)
                            {
                                case 1:
                                    matterType = "病假";
                                    break;

                                case 2:
                                    matterType = "事假";
                                    break;

                                case 3:
                                    matterType = "年假";
                                    break;

                                case 4:
                                    matterType = "婚假";
                                    break;

                                case 5:
                                    matterType = "产假";
                                    break;

                                case 6:
                                    matterType = "丧假";
                                    break;

                                case 7:
                                    matterType = "出差";
                                    break;

                                case 8:
                                    matterType = "外出";
                                    break;

                                case 9:
                                    matterType = "调休";
                                    break;
                                case 10:
                                    matterType = "其它";
                                    break;
                                case 11:
                                    matterType = "产检";
                                    break;
                                case 12:
                                    matterType = "福利假";
                                    break;
                                case 14:
                                    matterType = "陪产假";
                                    break;
                                case 15:
                                    matterType = "年假(补)";
                                    break;
                                default:
                                    continue;
                            }

                            string imagename;
                            int t = 0;
                            imagename = "qingjia";

                            if (info.MatterType == 8)
                            {
                                t = 2;
                                imagename = "waichu";
                            }
                            if (info.MatterType == 7)
                            {
                                t = 3;
                                imagename = "chuchai";
                            }
                            if (info.MatterType == 9)
                            {
                                t = 4;
                                imagename = "tiaoxiu";
                            }
                            if (info.MatterType == 10)
                            {
                                t = 5;
                                imagename = "qita";
                            }

                            if (!holidays.Contains(today.Day))
                            {
                                if (imgType == 1)
                                {
                                    string url = string.Format(Status.defaultUrl, "", info.ID, t);
                                }
                                string imgUrl = "/images/calendar/" + imgType + imagename + ".jpg";
                                string title;
                                switch (imgType)
                                {
                                    case 2:
                                        title = matterType + " 审批中 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                    case 3:
                                        title = matterType + " 已审批 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                    case 1:
                                    default:
                                        title = matterType + " 未提交 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                }

                                builder.Append("<img alt=\"OT\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");

                                if (imgType == 1)
                                {
                                }
                            }
                            else if (info.MatterType == 4 || info.MatterType == 5 || info.MatterType == 6 || info.MatterType == 7 || info.MatterType == 14)
                            {
                                if (imgType == 1)
                                {
                                    string url = string.Format(Status.defaultUrl, "", info.ID, t);
                                }
                                string imgUrl = "/images/calendar/" + imgType + imagename + ".jpg";
                                string title;
                                switch (imgType)
                                {
                                    case 2:
                                        title = matterType + " 审批中 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                    case 3:
                                        title = matterType + " 已审批 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                    case 1:
                                    default:
                                        title = matterType + " 未提交 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                }

                                builder.Append("<img alt=\"OT\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");

                                if (imgType == 1)
                                {
                                }
                            }
                        }
                    }
                    #endregion

                    #region 判断考勤默认事由情况
                    TimeSpan remainTime = TimeSpan.FromTicks(abnormityTicks);
                    if (!holidays.Contains(today.Day))
                    {
                        if (remainTime.TotalMinutes >= Status.GoWorkTime_BufferMinute && remainTime.TotalHours <= Status.NumberOfHours_Late)
                        {
                            string title = "迟到";
                            string imgUrl = "/images/calendar/3chidao.jpg";
                            builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                        }
                        else if (remainTime.TotalHours > Status.NumberOfHours_Late && remainTime.TotalHours <= Status.NumberOfHours_Absent)
                        {
                            string title = "旷工半天";
                            string imgUrl = "/images/calendar/3kuanggong.jpg";
                            builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                        }
                        else if (remainTime.TotalHours > Status.NumberOfHours_Absent)
                        {
                            string title = "旷工一天";
                            string imgUrl = "/images/calendar/3kuanggong.jpg";
                            builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                        }
                        else if (isLeaveEarly && remainTime.TotalMinutes >= 1 && remainTime.TotalHours <= Status.NumberOfHours_Late)
                        {
                            string title = "早退";
                            string imgUrl = "/images/calendar/3zaotui.jpg";
                            builder.Append("<img alt=\"").Append(Server.HtmlEncode(title)).Append("\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");
                        }
                    }
                    #endregion
                }
                else if (commuterModel.AttendanceType == Status.UserBasicAttendanceType_NotStat)
                {
                    #region 判断显示当天的OT信息
                    //foreach (SingleOvertimeInfo info in overtimes)
                    //{
                    //    if ((info.BeginTime >= today && info.BeginTime < tomorrow))
                    //    {
                    //        int imgType = 0;
                    //        switch (info.Approvestate)
                    //        {
                    //            case Status.OverTimeState_NotSubmit:
                    //            case Status.OverTimeState_Cancel:
                    //            case Status.OverTimeState_Overrule:
                    //                imgType = 1;
                    //                break;
                    //            case Status.OverTimeState_Passed:
                    //                imgType = 3;
                    //                break;
                    //            case Status.OverTimeState_WaitDirector:
                    //            case Status.OverTimeState_WaitHR:
                    //                imgType = 2;
                    //                break;
                    //            default:
                    //                imgType = 1;
                    //                break;
                    //        }

                    //        if (imgType == 1)
                    //        {
                    //            string url = string.Format(Status.defaultUrl, "", info.ID, 1);
                    //        }
                    //        string imgUrl = "/images/calendar/" + imgType + "jiaban.jpg";
                    //        string title;
                    //        switch (imgType)
                    //        {
                    //            case 2:
                    //                title = "OT 审批中 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                    //                    + "  " + info.OverTimeCause;
                    //                break;
                    //            case 3:
                    //                title = "OT 已审批 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                    //                    + "  " + info.OverTimeCause;
                    //                break;
                    //            case 1:
                    //            default:
                    //                title = "OT 未提交 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                    //                    + "  " + info.OverTimeCause;
                    //                break;
                    //        }

                    //        builder.Append("<img alt=\"OT\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");

                    //        if (imgType == 1)
                    //        {
                    //        }
                    //    }
                    //}
                    #endregion

                    #region 根据上下班时间计算考勤事由信息
                    // 是否迟到
                    bool isLate = false;
                    // 是否上午旷工
                    bool isAMAbsent = false;
                    // 是否下午旷工
                    bool isPMAbsent = false;
                    // 是否早退
                    bool isLeaveEarly = false;
                    TimeSpan span = new TimeSpan();
                    if ((clockIn.ToString("yyyy-MM-dd") == Status.EmptyTime || clockOut.ToString("yyyy-MM-dd") == Status.EmptyTime && day.Date < DateTime.Now.Date) || (clockIn == clockOut))
                    {
                        isPMAbsent = true;
                        span = span.Add(new TimeSpan(Status.WorkingHours, 0, 0));
                    }
                    long abnormityTicks = span.Ticks;
                    #endregion

                    #region 判断显示当天的考勤事由信息
                    foreach (MattersInfo info in matters)
                    {
                        if ((info.BeginTime >= today && info.BeginTime < tomorrow)
                            || (info.EndTime > today && info.EndTime <= tomorrow)
                            || (info.BeginTime <= today && info.EndTime >= tomorrow))
                        {
                            attMan.CalAttendance(ref isLate, ref isAMAbsent, ref isPMAbsent, ref isLeaveEarly, info, commuterModel, clockIn, clockOut, today, ref abnormityTicks);
                            int imgType = 0;
                            switch (info.MatterState)
                            {
                                case 1:
                                case 5:
                                case 6:
                                    imgType = 1;
                                    break;
                                case 2:
                                    imgType = 3;
                                    break;
                                case 3:
                                case 4:
                                    imgType = 2;
                                    break;
                                default:
                                    imgType = 1;
                                    break;
                            }

                            string matterType;
                            switch (info.MatterType)
                            {
                                case 1:
                                    matterType = "病假";
                                    break;

                                case 2:
                                    matterType = "事假";
                                    break;

                                case 3:
                                    matterType = "年假";
                                    break;

                                case 4:
                                    matterType = "婚假";
                                    break;

                                case 5:
                                    matterType = "产假";
                                    break;

                                case 6:
                                    matterType = "丧假";
                                    break;

                                case 7:
                                    matterType = "出差";
                                    break;

                                case 8:
                                    matterType = "外出";
                                    break;

                                case 9:
                                    matterType = "调休";
                                    break;
                                case 10:
                                    matterType = "其它";
                                    break;
                                case 11:
                                    matterType = "产检";
                                    break;
                                case 12:
                                    matterType = "福利假";
                                    break;
                                case 14:
                                    matterType = "陪产假";
                                    break;
                                default:
                                    continue;
                            }

                            string imagename;
                            int t = 0;
                            imagename = "qingjia";

                            if (info.MatterType == 8)
                            {
                                t = 2;
                                imagename = "waichu";
                            }
                            if (info.MatterType == 7)
                            {
                                t = 3;
                                imagename = "chuchai";
                            }
                            if (info.MatterType == 9)
                            {
                                t = 4;
                                imagename = "tiaoxiu";
                            }
                            if (info.MatterType == 10)
                            {
                                t = 5;
                                imagename = "qita";
                            }

                            if (!holidays.Contains(today.Day))
                            {
                                if (imgType == 1)
                                {
                                    string url = string.Format(Status.defaultUrl, "", info.ID, t);
                                }
                                string imgUrl = "/images/calendar/" + imgType + imagename + ".jpg";
                                string title;
                                switch (imgType)
                                {
                                    case 2:
                                        title = matterType + " 审批中 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                    case 3:
                                        title = matterType + " 已审批 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                    case 1:
                                    default:
                                        title = matterType + " 未提交 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                }

                                builder.Append("<img alt=\"OT\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");

                                if (imgType == 1)
                                {
                                }
                            }
                            else if (info.MatterType == 4 || info.MatterType == 5 || info.MatterType == 6 || info.MatterType == 7 || info.MatterType == 14)
                            {
                                if (imgType == 1)
                                {
                                    string url = string.Format(Status.defaultUrl, "", info.ID, t);
                                }
                                string imgUrl = "/images/calendar/" + imgType + imagename + ".jpg";
                                string title;
                                switch (imgType)
                                {
                                    case 2:
                                        title = matterType + " 审批中 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                    case 3:
                                        title = matterType + " 已审批 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                    case 1:
                                    default:
                                        title = matterType + " 未提交 ：" + info.BeginTime.ToString("yyyy-MM-dd HH:mm") + "--" + info.EndTime.ToString("yyyy-MM-dd HH:mm")
                                        + "  " + info.MatterContent;
                                        break;
                                }

                                builder.Append("<img alt=\"OT\" title=\"").Append(Server.HtmlEncode(title)).Append("\" src=\"").Append(imgUrl).Append("\"/>");

                                if (imgType == 1)
                                {
                                }
                            }
                        }
                    }
                    #endregion
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// 可视月份改变的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cldAttendance_VisibleDateChanged(object sender, EventArgs e)
        {
            int year = cldAttendance.VisibleDate.Year;
            int month = cldAttendance.VisibleDate.Month;

            int day = cldAttendance.SelectedDate.Day;
            int total = DateTime.DaysInMonth(year, month);
            if (day > total)
                day = total;
            cldAttendance.SelectedDate = new DateTime(year, month, day);

            BindCalender(year, month);

            // 设置各月考勤的状态标题
            SetTitleString();
            SetMonthStat();
        }

        /// <summary>
        /// 设置各月考勤考勤记录的标题和审批状态
        /// </summary>
        public void SetTitleString()
        {
            labApproveDesc.Text = "";
            // 判断用户提示信息是否为空
            if (!string.IsNullOrEmpty(Tips))
            {
                cldAttendance.TitleDateFormat = Tips;
            }
            else
            {
                MonthStatInfo monthStatInfo =
                monthManager.GetMonthStatInfoApprove(int.Parse(SelectUserID), cldAttendance.SelectedDate.Year, cldAttendance.SelectedDate.Month);
                string title = cldAttendance.MonthNames[cldAttendance.SelectedDate.Month - 1]
                        + " " + cldAttendance.SelectedDate.Year + "   {0}";
                if (monthStatInfo != null)
                {
                    if (monthStatInfo.State == Status.MonthStatAppState_NoSubmit)
                    {
                        title = string.Format(title, "未提交");
                    }
                    else if (monthStatInfo.State == Status.MonthStatAppState_WaitDirector)
                    {
                        title = string.Format(title, "等待总监审批");
                    }
                    else if (monthStatInfo.State == Status.MonthStatAppState_WaitStatisticians)
                    {
                        title = string.Format(title, "等待考勤统计员审批");
                    }
                    else if (monthStatInfo.State == Status.MonthStatAppState_Passed)
                    {
                        title = string.Format(title, "审批通过");
                    }
                    else if (monthStatInfo.State == Status.MonthStatAppState_WaitADAdmin)
                    {
                        title = string.Format(title, "等待考勤管理员确认");
                    }
                    else if (monthStatInfo.State == Status.MonthStatAppState_WaitHRAdmin)
                    {
                        title = string.Format(title, "等待团队HR审批");
                    }
                    else if (monthStatInfo.State == Status.MonthStatAppState_WaitManager)
                    {
                        title = string.Format(title, "等待团队总经理审批");
                    }
                    else if (monthStatInfo.State == Status.MonthStatAppState_Overrule)
                    {
                        title = string.Format(title, "审批驳回");
                    }

                    if (!string.IsNullOrEmpty(monthStatInfo.ApproveRemark))
                    {
                        labApproveDesc.Text = "审批记录：<br/>" + monthStatInfo.ApproveRemark.Replace("\r\n", "<br />");
                    }
                }
                else
                {
                    title = string.Format(title, "未提交");
                }
                cldAttendance.TitleDateFormat = title;
            }
        }

        /// <summary>
        /// 设置当前用户所选择的月份的考情统计信息
        /// </summary>
        protected void SetMonthStat()
        {
            UserAttBasicInfo userBasicModel = userBasicManager.GetModelByUserid(int.Parse(SelectUserID));
            if (userBasicModel != null)
            {
                // 判断人员考勤的类型，如果是考勤是特殊的就不计算考勤情况
                AttendanceDataInfo attdatainfo = attMan.GetMonthStat(int.Parse(SelectUserID),
                    cldAttendance.SelectedDate.Year, cldAttendance.SelectedDate.Month, clockInTimes, employeeJobModel, dimissionModel);
                if (attdatainfo != null)
                {
                    if (attdatainfo.LateCount > 0)
                        labLate.Text = attdatainfo.LateCount + "";    // 迟到
                    else
                        labLate.Text = "";    // 迟到

                    if (attdatainfo.LeaveEarlyCount > 0)
                        labLeaveEarly.Text = attdatainfo.LeaveEarlyCount + "";   // 早退
                    else
                        labLeaveEarly.Text = "";

                    if (attdatainfo.AbsentHours > 0)
                        labAbsent.Text = string.Format("{0:F1}", attdatainfo.AbsentHours / Status.WorkingHours) + "D";    // 旷工
                    else
                        labAbsent.Text = "";

                    //if (attdatainfo.OverTimeHours > 0)
                    //    labOverTime.Text = string.Format("{0:F1}", attdatainfo.OverTimeHours) + "H";   // OT
                    //else
                    //    labOverTime.Text = "";

                    //if (attdatainfo.HolidayOverTimeHours > 0)
                    //    labHolidayOverTime.Text = string.Format("{0:F1}", attdatainfo.HolidayOverTimeHours) + "H";   // 节假日OT
                    //else
                    //    labHolidayOverTime.Text = "";

                    if (attdatainfo.EvectionHours > 0)
                        labEvection.Text = string.Format("{0:F1}", attdatainfo.EvectionHours / Status.WorkingHours) + "D";  // 出差
                    else
                        labEvection.Text = "";

                    if (attdatainfo.EgressHours > 0)
                        labEgress.Text = string.Format("{0:F3}", attdatainfo.EgressHours) + "H";   // 外出
                    else
                        labEgress.Text = "";

                    if (attdatainfo.SickLeaveHours > 0)
                        labSickLeave.Text = string.Format("{0:F3}", attdatainfo.SickLeaveHours) + "H"; // 病假
                    else
                        labSickLeave.Text = "";

                    if (attdatainfo.SickByYear > 0)
                        lblSickTotal.Text = string.Format("{0:F3}", attdatainfo.SickByYear) + "H"; // 病假Total
                    else
                        lblSickTotal.Text = "";

                    if (attdatainfo.AffiairLeaveHours > 0)
                        labAffiairLeave.Text = string.Format("{0:F3}", attdatainfo.AffiairLeaveHours) + "H";  // 事假
                    else
                        labAffiairLeave.Text = "";

                    if (attdatainfo.AffairByYear > 0)
                        labAffairTotal.Text = string.Format("{0:F3}", attdatainfo.AffairByYear) + "H";  // 事假Total
                    else
                        labAffairTotal.Text = "";


                    if (attdatainfo.AnnualLeaveHours > 0)
                        labAnnualLeave.Text = string.Format("{0:F3}", attdatainfo.AnnualLeaveHours / Status.WorkingHours) + "D";  // 年假
                    else
                        labAnnualLeave.Text = "";

                    if (attdatainfo.AnnualLeaveByYear > 0)
                        lblAnnualTotal.Text = string.Format("{0:F3}", attdatainfo.AnnualLeaveByYear / Status.WorkingHours) + "D";  // 年假
                    else
                        lblAnnualTotal.Text = "";

                    if (attdatainfo.LastAnnualHours > 0)
                        labAnnualLast.Text = string.Format("{0:F3}", attdatainfo.LastAnnualHours / Status.WorkingHours) + "D";  // 补年假
                    else
                        labAnnualLast.Text = "";

                    if (attdatainfo.FuneralLeaveHours > 0)
                        labFuneralLeave.Text = string.Format("{0:F1}", attdatainfo.FuneralLeaveHours / Status.WorkingHours) + "D";   // 丧假
                    else
                        labFuneralLeave.Text = "";

                    if (attdatainfo.MarriageLeaveHours > 0)
                        labMarriageLeave.Text = string.Format("{0:F1}", attdatainfo.MarriageLeaveHours / Status.WorkingHours) + "D";  // 婚假
                    else
                        labMarriageLeave.Text = "";

                    if (attdatainfo.MaternityLeaveHours > 0)
                        labMaternityLeave.Text = string.Format("{0:F1}", attdatainfo.MaternityLeaveHours / Status.WorkingHours) + "D";  // 产假
                    else
                        labMaternityLeave.Text = "";

                    if (attdatainfo.PrenatalCheckHours > 0)
                        labPrenatalCheck.Text = string.Format("{0:F3}", attdatainfo.PrenatalCheckHours / Status.WorkingHours) + "D";   // 产前检查
                    else
                        labPrenatalCheck.Text = "";

                    //if (attdatainfo.IncentiveHours > 0)
                    //    labIncentive.Text = string.Format("{0:F3}", attdatainfo.IncentiveHours / Status.WorkingHours) + "D";      // 奖励假
                    //else
                    //    labIncentive.Text = "";
                    //if (attdatainfo.IncentiveByYear > 0)
                    //    lblIncentiveTotal.Text = string.Format("{0:F3}", attdatainfo.IncentiveByYear / Status.WorkingHours) + "D";      // 福利假
                    //else
                    //    lblIncentiveTotal.Text = "";
                }
            }
        }

        /// <summary>
        /// 调休日期信息
        /// </summary>
        public string TipsInfo
        {
            get
            {

                // 剩余年假总数
                double remainingAnnualDays = 0;
                // 个人年假总数
                double totalAnnualLeaveDays = attMan.CalAnnualLeave(int.Parse(SelectUserID), cldAttendance.SelectedDate.Year, out remainingAnnualDays);
                // 奖励年假
                double remainingAwardDays = 0;
                double awardAnnualDays = attMan.GetAwardAnnualDays(int.Parse(SelectUserID), cldAttendance.SelectedDate.Year, out remainingAwardDays);

                ESP.HumanResource.Entity.DimissionFormInfo dimission = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModelByUserid(int.Parse(SelectUserID));


                if (dimission != null )
                {

                    double annualBase = 0;
                    double rewardBase = 0;

                    double remainAnnual = 0;
                    double canUseAnnual = 0;
                    double usedAnnual = 0;

                    double remainReward = 0;
                    double canUseReward = 0;
                    double usedReward = 0;
                    try
                    {
                        remainAnnual = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetAnnualLeaveInfo(dimission.UserId, dimission.LastDay.Value, out canUseAnnual, out usedAnnual, out annualBase);
                        remainReward = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetRewardLeaveInfo(dimission.UserId, dimission.LastDay.Value, out canUseReward, out usedReward, out rewardBase);
                    }
                    catch
                    {
                        remainAnnual = 0;
                        remainReward = 0;
                    }
                    //
                    double canUseTotal = (canUseAnnual + canUseReward);//2.5+2.5

                    int tempdays = (int)canUseTotal;
                    //if ((tempdays + 0.5) >= canUseTotal)
                    //    canUseTotal = tempdays;
                    //else
                    //    canUseTotal = tempdays + 0.5;

                    canUseTotal = tempdays;


                    double yuzhiTotal = (usedAnnual + usedReward) - canUseTotal > 0 ? (usedAnnual + usedReward) - canUseTotal : 0;//1
                    double yuzhiAnnual = 0;// yuzhiTotal - usedReward > 0 ? yuzhiTotal - usedReward : 0;//0
                    double yuzhiReward = 0;// usedReward - canUseReward > 0 ? usedReward - canUseReward : 0;//0
                    //6.5-3.5
                    if (canUseTotal >= annualBase)
                        yuzhiReward = yuzhiTotal;
                    else
                    {
                        if (yuzhiTotal <= annualBase)
                        {
                            if (usedAnnual + usedReward > annualBase)
                            {
                                yuzhiReward = usedReward;
                                yuzhiAnnual = yuzhiTotal - usedReward;
                            }
                            else
                            {
                                yuzhiAnnual = yuzhiTotal;
                                yuzhiReward = 0;
                            }
                        }
                        else
                        {
                            yuzhiReward = usedReward;
                            yuzhiAnnual = yuzhiTotal - usedReward;
                        }
                    }
                    //年假余

                    double remainTotal = remainAnnual;// +remainReward;
                    if ( remainAnnual <= 0)
                        remainTotal = 0;
                    string  nianjiayu = remainTotal < 0 ? "0" : ((int)remainTotal).ToString("#,##0.000");

                    return "年假余&nbsp;<font style=\"font-weight: bolder\">" + nianjiayu + "</font>&nbsp;天； 预支" + yuzhiTotal.ToString("#,##0.000") + "天年假&nbsp;<font style=\"font-weight: bolder; color: Red;\">其中法定假" + yuzhiAnnual.ToString("#,##0.000") + "天，福利假" + yuzhiReward.ToString("#,##0.000") + "天。</font>";
                   
                    //return "年假（剩余/总数）：" + ((int)remainAnnual).ToString() + "/" + ((int)canUseAnnual).ToString();
                }
                else
                    return "<span style='padding-right:60px;'>年假（剩余/总数）：" + (remainingAnnualDays + remainingAwardDays) + "/" + (totalAnnualLeaveDays + awardAnnualDays) + "</span>";
      
            }
        }

        /// <summary>
        /// 当前选择用户的信息
        /// </summary>
        public string CurAttendanceUserInfo
        {
            get
            {
                ESP.Framework.Entity.UserInfo userInfo = ESP.Framework.BusinessLogic.UserManager.Get(int.Parse(SelectUserID));
                ESP.Framework.Entity.EmployeeInfo employeeInfo = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(SelectUserID));
                string username = "<span style=\"color: Black; font-weight: bold; font-size: 12pt;\">" + employeeInfo.Code + "，" + userInfo.FullNameCN;
                return username + "，" + cldAttendance.SelectedDate.Month + "月份考勤信息</span><br/><span style=\"color: #B50000; font-size: 9pt;\">" + TipsInfo + "</span>";
            }
        }

        /// <summary>
        /// 导出考勤统计信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            BindCalender(cldAttendance.SelectedDate.Year, cldAttendance.SelectedDate.Month);
            AttendanceDataInfo attdatainfo = attMan.GetMonthStat(int.Parse(SelectUserID),
                           cldAttendance.SelectedDate.Year, cldAttendance.SelectedDate.Month, clockInTimes, employeeJobModel, dimissionModel);            

            FileHelper.ExportStatistics(null, Server.MapPath("~"), cldAttendance.SelectedDate.Year, cldAttendance.SelectedDate.Month, int.Parse(SelectUserID), attdatainfo,
                clockInTimes, holidays, matters, overtimes, employeeJobModel, commuterTimeList, Response, dimissionModel);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                // 保存当前选择日期的考勤记录
                string clew = "";
                int year = cldAttendance.SelectedDate.Year;
                int month = cldAttendance.SelectedDate.Month;
                BindCalender(year, month);

                int flagtype = 0;
                if (matters != null && matters.Count > 0)
                {
                    foreach (MattersInfo model in matters)
                    {
                        if (model.MatterState != Status.MattersState_Passed)
                        {
                            flagtype = 1;
                        }
                    }
                }

                if (flagtype == 1)
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('您尚有考勤事由未审批，无法提交整月考勤！');", true);
                }
                else
                {
                    if (string.IsNullOrEmpty(clew))
                    {
                        bool b = attMan.SubmintAttendance(int.Parse(SelectUserID), year, month, chkDimission.Checked);
                        if (b)
                        {
                            // 驳回自动关闭七天的提交限制
                            AttGracePeriodManager manager = new AttGracePeriodManager();
                            manager.DeleteByUserId(int.Parse(SelectUserID));

                            ClientScript.RegisterStartupScript(typeof(string), "", "alert('考勤记录提交成功，等待审批。');window.location='IntegratedQueryView.aspx?ApplicantID=" + SelectUserID + "';", true);
                        }
                        else
                            ClientScript.RegisterStartupScript(typeof(string), "", "alert('考勤记录提交失败。');window.location='IntegratedQueryView.aspx?ApplicantID=" + SelectUserID + "';", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + clew + "');window.location='AttendanceSelect.aspx';", true);
                    }
                    // 设置各月考勤的状态标题
                    SetTitleString();
                    SetMonthStat();
                }
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add(ex.Message, "提交考勤记录", ESP.Logging.LogLevel.Error, ex);
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('考勤记录提交失败。');", true);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ESP.Administrative.BusinessLogic.MonthStatManager monthManager = new MonthStatManager();
            int ret = monthManager.Delete(int.Parse(SelectUserID), cldAttendance.SelectedDate.Year, cldAttendance.SelectedDate.Month);

            if (ret > 0)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('考勤撤销完成。');window.location='IntegratedQueryView.aspx?ApplicantID=" + SelectUserID + "';", true);
            }         
        }
    }
}
