using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ESP.Administrative.Entity;
using ESP.Administrative.Common;
using System.Data.SqlClient;
using ESP.Administrative.DataAccess;
using ESP.Framework.Entity;
using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Entity;

namespace ESP.Administrative.BusinessLogic
{
    /// <summary>
    /// 员工考勤基础信息类
    /// </summary>
    public class UserAttBasicInfoManager
    {
        private readonly UserAttBasicDataProvider dal = new UserAttBasicDataProvider();

        public UserAttBasicInfoManager()
        { }
        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 保存用户卡号信息
        /// </summary>
        /// <param name="model">用户卡号信息</param>
        public int Add(ESP.Administrative.Entity.UserAttBasicInfo model)
        {
            if (model != null)
            {
                model.Deleted = false;
                model.Sort = 0;
                model.CreateTime = DateTime.Now;
                model.UpdateTime = DateTime.Now;
            }
            return dal.Add(model);
        }

        public int AddAttBasicAndCommuterTime(UserAttBasicInfo model)
        {
            // 年假业务实现类
            ALAndRLManager alandrlManager = new ALAndRLManager();
            // 用户上下班业务实现类
            CommuterTimeManager commuterTimeManager = new CommuterTimeManager();
            // 考勤审批人设置业务类
            OperationAuditManageManager operationAuditManager = new OperationAuditManageManager();

            #region 用户基本信息和部门信息
            // 用户编号
            int userid = model.Userid;
            // 用户职位信息
            ESP.Framework.Entity.EmployeePositionInfo employeePositionModel = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(userid)[0];
            // 用户的上级部门编号
            int parentId = ESP.Framework.BusinessLogic.DepartmentManager.Get(employeePositionModel.DepartmentID).ParentID;
            // 用户所在的分公司编号
            int componentId = ESP.Framework.BusinessLogic.DepartmentManager.Get(parentId).ParentID;
            #endregion

            // 用户当年的年假信息对象
            ALAndRLInfo alandrlModel = alandrlManager.GetALAndRLModel(model.Userid, DateTime.Now.Year, (int)AAndRLeaveType.AnnualType);
            int operatorId = ESP.Framework.BusinessLogic.UserManager.GetCurrentUserID();
            // 获得用户审批人用户信息对象
            ESP.Administrative.Entity.OperationAuditManageInfo operationAuditModel = operationAuditManager.GetOperationAuditModelByUserID(model.Userid);

            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //#region 更新用户的年假信息
                    //double annualleave = this.GetAnnualLeave(model.Userid, DateTime.Now.Year, model);
                    //if (alandrlModel != null)
                    //{
                    //    alandrlModel.LeaveNumber = (decimal)annualleave;
                    //    alandrlModel.UpdateTime = DateTime.Now;
                    //    alandrlModel.OperatorID = operatorId;
                    //    alandrlManager.Update(alandrlModel, conn, trans);
                    //}
                    //else
                    //{
                    //    ALAndRLInfo newAlandrlModel = new ALAndRLInfo();
                    //    newAlandrlModel.CreateTime = DateTime.Now;
                    //    newAlandrlModel.Deleted = false;
                    //    newAlandrlModel.EmployeeName = model.EmployeeName;
                    //    newAlandrlModel.LeaveMonth = 12;
                    //    newAlandrlModel.LeaveNumber = (decimal)annualleave;
                    //    newAlandrlModel.LeaveType = (int)AAndRLeaveType.AnnualType;
                    //    newAlandrlModel.LeaveYear = DateTime.Now.Year;
                    //    newAlandrlModel.OperatorDept = 0;
                    //    newAlandrlModel.OperatorID = operatorId;
                    //    newAlandrlModel.RemainingNumber = (decimal)annualleave;
                    //    newAlandrlModel.UpdateTime = DateTime.Now;
                    //    newAlandrlModel.UserCode = model.UserCode;
                    //    newAlandrlModel.UserID = model.Userid;
                    //    newAlandrlModel.UserName = model.UserName;
                    //    newAlandrlModel.ValidTo = new DateTime(DateTime.Now.Year, 12, 31);
                    //    alandrlManager.Add(newAlandrlModel, conn, trans);
                    //}
                    //#endregion

                    #region 更新用户的上下班时间
                    List<CommuterTimeInfo> commuterTimeList = commuterTimeManager.GetCommuterTimeByUserId(userid);
                    TimeSpan gospan = TimeSpan.Parse(model.GoWorkTime);
                    TimeSpan offspan = TimeSpan.Parse(model.OffWorkTime);
                    if (commuterTimeList != null && commuterTimeList.Count > 0)
                    {

                        // 这个值用来标记用户是否操作过上下班时间
                        int flag = 0;
                        foreach (CommuterTimeInfo commuterTimeModel in commuterTimeList)
                        {
                            TimeSpan goWorkTimeSpan = commuterTimeModel.GoWorkTime.TimeOfDay;
                            TimeSpan offWorkTimeSpan = commuterTimeModel.OffWorkTime.TimeOfDay;
                            if (goWorkTimeSpan == gospan && offWorkTimeSpan == offspan)
                            {
                                flag = 1;
                            }
                        }
                        if (flag == 0)
                        {
                            CommuterTimeDataProvider commuterTimeDal = new CommuterTimeDataProvider();
                            // 创建用户上下班时间信息
                            ESP.Administrative.Entity.CommuterTimeInfo commuterTimeModel = new CommuterTimeInfo();
                            commuterTimeModel.AttendanceType = model.AttendanceType;
                            commuterTimeModel.BeginTime = model.WorkTimeBeginDate;
                            commuterTimeModel.CreateTime = DateTime.Now;
                            commuterTimeModel.Deleted = false;
                            commuterTimeModel.GoWorkTime = new DateTime(1900, 01, 01).Add(gospan);
                            commuterTimeModel.OffWorkTime = new DateTime(1900, 01, 01).Add(offspan);
                            commuterTimeModel.OperatorDept = 0;
                            commuterTimeModel.OperatorID = operatorId;
                            commuterTimeModel.Sort = 0;
                            commuterTimeModel.UpdateTime = DateTime.Now;
                            commuterTimeModel.UserCode = model.UserCode;
                            commuterTimeModel.UserID = model.Userid;
                            commuterTimeModel.UserName = model.EmployeeName;

                            commuterTimeDal.Add(commuterTimeModel, conn, trans);
                            foreach (CommuterTimeInfo commuterTimeModelTemp in commuterTimeList)
                            {
                                if (commuterTimeModelTemp.EndTime == null || commuterTimeModelTemp.EndTime.ToString() == "")
                                {
                                    commuterTimeModelTemp.EndTime = model.WorkTimeBeginDate.Value.AddDays(-1);
                                    commuterTimeDal.Update(commuterTimeModelTemp, conn, trans);
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        CommuterTimeDataProvider commuterTimeDal = new CommuterTimeDataProvider();
                        // 创建用户上下班时间信息
                        ESP.Administrative.Entity.CommuterTimeInfo commuterTimeModel = new CommuterTimeInfo();
                        commuterTimeModel.AttendanceType = model.AttendanceType;
                        commuterTimeModel.BeginTime = DateTime.Now.Date;
                        commuterTimeModel.CreateTime = DateTime.Now;
                        commuterTimeModel.Deleted = false;
                        commuterTimeModel.GoWorkTime = new DateTime(1900, 01, 01).Add(gospan);
                        commuterTimeModel.OffWorkTime = new DateTime(1900, 01, 01).Add(offspan);
                        commuterTimeModel.OperatorDept = 0;
                        commuterTimeModel.OperatorID = operatorId;
                        commuterTimeModel.Sort = 0;
                        commuterTimeModel.UpdateTime = DateTime.Now;
                        commuterTimeModel.UserCode = model.UserCode;
                        commuterTimeModel.UserID = model.Userid;
                        commuterTimeModel.UserName = model.EmployeeName;
                        commuterTimeDal.Add(commuterTimeModel, conn, trans);
                    }

                    #endregion

                    #region 更新用户的考勤基本信息
                    int modelid = dal.Add(model, conn, trans);
                    #endregion

                    trans.Commit();
                    return modelid;
                }
                catch (Exception)
                {
                    trans.Rollback();
                }
                return 0;
            }
        }

        /// <summary>
        /// 保存用户卡号信息
        /// </summary>
        /// <param name="model">用户卡号信息</param>
        public int Add(ESP.Administrative.Entity.UserAttBasicInfo model, CardStoreInfo cardStoreModel)
        {
            int id = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    if (model != null)
                    {
                        model.Deleted = false;
                        model.Sort = 0;
                        model.CreateTime = DateTime.Now;
                        model.UpdateTime = DateTime.Now;
                    }
                    if (cardStoreModel != null)
                    {
                        cardStoreModel.State = (int)CardStoreState.Used;
                        cardStoreModel.UpdateTime = DateTime.Now;
                        cardStoreModel.OperatorID = ESP.Framework.BusinessLogic.UserManager.GetCurrentUserID();
                        CardStoreDataProvider cardStoreDal = new CardStoreDataProvider();
                        cardStoreDal.Update(cardStoreModel, conn, trans);
                    }
                    id = dal.Add(model, conn, trans);

                    // 获得停卡任务信息
                    WaitingTaskInfo waitingTaskModel = this.GetWaitingTaskModel(model, TaskType.EnableCard);
                    WaitingTaskDataProvider waitingTaskDal = new WaitingTaskDataProvider();
                    waitingTaskDal.Add(waitingTaskModel, conn, trans);

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    id = 0;
                    ESP.Logging.Logger.Add(ex.Message, "行政系统", ESP.Logging.LogLevel.Error, ex);
                }
            }
            return id;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ESP.Administrative.Entity.UserAttBasicInfo model)
        {
            dal.Update(model);
        }
        /// <summary>
        /// 更新多条数据
        /// </summary>
        public void Update(string newUserCode, int userId)
        {
            dal.Update(newUserCode, userId);
        }
        /// <summary>
        /// 更改用户考勤的基本信息
        /// </summary>
        /// <param name="model">用户考勤基本信息对象</param>
        /// <param name="approveUserid">用户考勤审批人ID</param>
        public void UpdateAttBasicAndCommuterTime(UserAttBasicInfo model)
        {
            // 年假业务实现类
            ALAndRLManager alandrlManager = new ALAndRLManager();
            // 用户上下班业务实现类
            CommuterTimeManager commuterTimeManager = new CommuterTimeManager();
            // 考勤审批人设置业务类
            OperationAuditManageManager operationAuditManager = new OperationAuditManageManager();

            #region 用户基本信息和部门信息
            // 用户编号
            int userid = model.Userid;
            // 用户职位信息
            ESP.Framework.Entity.EmployeePositionInfo employeePositionModel = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(userid)[0];
            // 用户的上级部门编号
            int parentId = ESP.Framework.BusinessLogic.DepartmentManager.Get(employeePositionModel.DepartmentID).ParentID;
            // 用户所在的分公司编号
            int componentId = ESP.Framework.BusinessLogic.DepartmentManager.Get(parentId).ParentID;
            #endregion

            // 用户当年的年假信息对象
            ALAndRLInfo alandrlModel = alandrlManager.GetALAndRLModel(model.Userid, DateTime.Now.Year, (int)AAndRLeaveType.AnnualType);
            int operatorId = ESP.Framework.BusinessLogic.UserManager.GetCurrentUserID();
            // 获得用户审批人用户信息对象
            ESP.Administrative.Entity.OperationAuditManageInfo operationAuditModel = operationAuditManager.GetOperationAuditModelByUserID(model.Userid);

            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //#region 更新用户的年假信息
                    //double annualleave = this.GetAnnualLeave(model.Userid, DateTime.Now.Year, model);
                    //if (alandrlModel != null)
                    //{
                    //    alandrlModel.LeaveNumber = (decimal)annualleave;
                    //    alandrlModel.UpdateTime = DateTime.Now;
                    //    alandrlModel.OperatorID = operatorId;
                    //    alandrlManager.Update(alandrlModel, conn, trans);
                    //}
                    //else
                    //{
                    //    ALAndRLInfo newAlandrlModel = new ALAndRLInfo();
                    //    newAlandrlModel.CreateTime = DateTime.Now;
                    //    newAlandrlModel.Deleted = false;
                    //    newAlandrlModel.EmployeeName = model.EmployeeName;
                    //    newAlandrlModel.LeaveMonth = 12;
                    //    newAlandrlModel.LeaveNumber = (decimal)annualleave;
                    //    newAlandrlModel.LeaveType = (int)AAndRLeaveType.AnnualType;
                    //    newAlandrlModel.LeaveYear = DateTime.Now.Year;
                    //    newAlandrlModel.OperatorDept = 0;
                    //    newAlandrlModel.OperatorID = operatorId;
                    //    newAlandrlModel.RemainingNumber = (decimal)annualleave;
                    //    newAlandrlModel.UpdateTime = DateTime.Now;
                    //    newAlandrlModel.UserCode = model.UserCode;
                    //    newAlandrlModel.UserID = model.Userid;
                    //    newAlandrlModel.UserName = model.UserName;
                    //    newAlandrlModel.ValidTo = new DateTime(DateTime.Now.Year, 12, 31);
                    //    alandrlManager.Add(newAlandrlModel, conn, trans);
                    //}
                    //#endregion

                    #region 更新用户的上下班时间
                    List<CommuterTimeInfo> commuterTimeList = commuterTimeManager.GetCommuterTimeByUserId(userid);
                    if (commuterTimeList != null && commuterTimeList.Count > 0)
                    {
                        TimeSpan gospan = TimeSpan.Parse(model.GoWorkTime);
                        TimeSpan offspan = TimeSpan.Parse(model.OffWorkTime);
                        // 这个值用来标记用户是否操作过上下班时间
                        int flag = 0;
                        foreach (CommuterTimeInfo commuterTimeModel in commuterTimeList)
                        {
                            if (commuterTimeModel.EndTime == null || commuterTimeModel.EndTime.ToString() == "")
                            {
                                TimeSpan goWorkTimeSpan = commuterTimeModel.GoWorkTime.TimeOfDay;
                                TimeSpan offWorkTimeSpan = commuterTimeModel.OffWorkTime.TimeOfDay;
                                if (goWorkTimeSpan == gospan && offWorkTimeSpan == offspan)
                                {
                                    flag = 1;
                                }
                            }
                        }
                        if (flag == 0)
                        {
                            CommuterTimeDataProvider commuterTimeDal = new CommuterTimeDataProvider();
                            // 创建用户上下班时间信息
                            ESP.Administrative.Entity.CommuterTimeInfo commuterTimeModel = new CommuterTimeInfo();
                            commuterTimeModel.AttendanceType = model.AttendanceType;
                            commuterTimeModel.BeginTime = model.WorkTimeBeginDate;
                            commuterTimeModel.CreateTime = DateTime.Now;
                            commuterTimeModel.Deleted = false;
                            commuterTimeModel.GoWorkTime = new DateTime(1900, 01, 01).Add(gospan);
                            commuterTimeModel.OffWorkTime = new DateTime(1900, 01, 01).Add(offspan);
                            commuterTimeModel.OperatorDept = 0;
                            commuterTimeModel.OperatorID = operatorId;
                            commuterTimeModel.Sort = 0;
                            commuterTimeModel.UpdateTime = DateTime.Now;
                            commuterTimeModel.UserCode = model.UserCode;
                            commuterTimeModel.UserID = model.Userid;
                            commuterTimeModel.UserName = model.EmployeeName;

                            foreach (CommuterTimeInfo commuterTimeModelTemp in commuterTimeList)
                            {
                                if (commuterTimeModelTemp.EndTime == null || commuterTimeModelTemp.EndTime.ToString() == "")
                                {
                                    commuterTimeModelTemp.EndTime = model.WorkTimeBeginDate.Value.AddDays(-1);
                                    commuterTimeDal.Update(commuterTimeModelTemp, conn, trans);

                                    commuterTimeModel.WorkingDays_OverTime1 = commuterTimeModelTemp.WorkingDays_OverTime1;
                                    commuterTimeModel.WorkingDays_OverTime2 = commuterTimeModelTemp.WorkingDays_OverTime2;
                                    commuterTimeModel.LateGoWorkTime_OverTime1 = commuterTimeModelTemp.LateGoWorkTime_OverTime1;
                                    break;
                                }
                            }

                            commuterTimeDal.Add(commuterTimeModel, conn, trans);
                        }
                    }
                    #endregion

                    #region 更新用户的考勤基本信息
                    dal.Update(model, conn, trans);
                    #endregion

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.Message, "行政系统", ESP.Logging.LogLevel.Error, ex);
                }
            }
        }

        public int UpdateUserCommuteType(int userid, int attendanceType)
        {
            return dal.UpdateUserCommuteType(userid, attendanceType);
        }

        public int UpdateUserCommuteType(int userid, int attendanceType,SqlTransaction trans)
        {
            return dal.UpdateUserCommuteType(userid, attendanceType,trans );
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">用户考勤基本信息</param>
        /// <param name="cardStoreModel">门卡信息对象</param>
        /// <param name="flag">标识门卡是否归入卡库并可在用，1归入卡库并可用，2归入卡库并不可用</param>
        public void Update(ESP.Administrative.Entity.UserAttBasicInfo model, CardStoreInfo cardStoreModel, int flag)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // 更新考勤基本信息对象
                    dal.Update(model, conn, trans);
                    CardStoreDataProvider cardStoreDal = new CardStoreDataProvider();
                    // 更新卡库中门卡信息
                    if (cardStoreModel != null && cardStoreModel.State == (int)CardStoreState.Used)
                    {
                        cardStoreModel.State = flag == 1 ? (int)CardStoreState.NotUsed : (int)CardStoreState.BlankOut;
                        cardStoreModel.UpdateTime = DateTime.Now;
                        cardStoreModel.OperatorID = ESP.Framework.BusinessLogic.UserManager.GetCurrentUserID();
                        cardStoreDal.Update(cardStoreModel, conn, trans);
                    }
                    else   // 如果卡库中不存在该条门卡信息，就将门卡信息插入到卡库中
                    {
                        cardStoreModel = new CardStoreInfo();
                        cardStoreModel.CardNo = int.Parse(model.CardNo);
                        cardStoreModel.CreateTime = DateTime.Now;
                        cardStoreModel.Deleted = false;
                        cardStoreModel.OperatorDept = 0;
                        cardStoreModel.OperatorID = ESP.Framework.BusinessLogic.UserManager.GetCurrentUserID();
                        cardStoreModel.sort = 0;
                        cardStoreModel.State = flag == 1 ? (int)CardStoreState.NotUsed : (int)CardStoreState.BlankOut;
                        cardStoreModel.UpdateTime = DateTime.Now;
                        // 设置地域信息
                        cardStoreModel.AreaID = new UserAttBasicInfoManager().GetRootDepartmentID(ESP.Framework.BusinessLogic.UserManager.GetCurrentUserID()).DepartmentID;
                        cardStoreDal.Add(cardStoreModel, conn, trans);
                    }

                    // 获取停卡任务信息
                    WaitingTaskInfo waitingTaskModel = this.GetWaitingTaskModel(model, TaskType.UnEnableCard);
                    WaitingTaskDataProvider waitingTaskDal = new WaitingTaskDataProvider();
                    waitingTaskDal.Add(waitingTaskModel, conn, trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.Message, "行政系统", ESP.Logging.LogLevel.Error, ex);
                }
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            dal.Delete(ID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Administrative.Entity.UserAttBasicInfo GetModel(int ID)
        {
            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Administrative.Entity.UserAttBasicInfo GetModel(string strWhere)
        {
            return dal.GetModel(strWhere);
        }

        /// <summary>
        /// 通过用户编号获得对象实体
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <returns>返回一个用户卡号信息的对象实体</returns>
        public ESP.Administrative.Entity.UserAttBasicInfo GetModelByUserid(int userid)
        {
            string strwhere = " userid={0} and deleted=0";
            if (userid != 0)
            {
                strwhere = string.Format(strwhere, userid);
            }
            DataSet ds = dal.GetList(strwhere);
            ESP.Administrative.Entity.UserAttBasicInfo usercardinfo = new ESP.Administrative.Entity.UserAttBasicInfo();
            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                usercardinfo.PopupData(ds.Tables[0].Rows[0]);
            }
            return usercardinfo;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, List<SqlParameter> parameter)
        {
            return dal.GetList(strWhere, parameter);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return dal.GetList("");
        }

        /// <summary>
        /// 启动入职后自动关联考勤
        /// </summary>
        /// <param name="UserID">入职人员ID</param>
        /// <param name="UserCode">入职人员员工编号</param>
        /// <param name="UserName">入职人员系统登录用户名</param>
        /// <param name="EmployeeName">入职人员真实姓名</param>
        /// <returns>关联成功返回门卡号，否则返回null</returns>
        public string AssociateAttendance(int UserID, string UserCode, string UserName, string EmployeeName)
        {
            // 启动入职后自动加入用户上下班时间信息
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    ESP.HumanResource.Entity.EmployeeJobInfo employeeJobInfo = ESP.HumanResource.BusinessLogic.EmployeeJobManager.getModelBySysId(UserID);
                    UserAttBasicInfo model = new UserAttBasicInfo();
                    model.Userid = UserID;
                    model.UserCode = UserCode;
                    model.UserName = UserName;
                    model.EmployeeName = EmployeeName;

                    int areaId = this.GetRootDepartmentID(UserID).DepartmentID;

                    // 当前登录用户
                    int currentUserID = ESP.Framework.BusinessLogic.UserManager.GetCurrentUserID();


                        model.GoWorkTime = Constants.BJDEFAULTGOWORKTIME;
                        model.OffWorkTime = Constants.BJDEFAULTOFFWORKTIME;


                    model.AttendanceType = (int)AttendanceType.Normal;
                    model.AnnualLeaveBase = (int)AnnualLeaveBase.Normal;
                    model.Deleted = false;
                    model.CreateTime = DateTime.Now;
                    model.UpdateTime = DateTime.Now;
                    model.OperateorID = currentUserID;
                    model.OperateorDept = 0;
                    model.Sort = 0;
                    model.CardState = 0;
                    model.CardState = (int)CardUseState.Enable;
                    model.CardEnableTime = DateTime.Now;
                    model.AreaID = areaId;
                    model.WorkTimeBeginDate = employeeJobInfo.joinDate.Date;
                    int id = dal.Add(model, conn, trans);

                    #region 创建用户上下班时间信息
                    ESP.Administrative.Entity.CommuterTimeInfo commuterTimeModel = new CommuterTimeInfo();
                    commuterTimeModel.AttendanceType = Status.UserBasicAttendanceType_Normal;
                    commuterTimeModel.BeginTime = employeeJobInfo.joinDate.Date;
                    commuterTimeModel.CreateTime = DateTime.Now;
                    commuterTimeModel.Deleted = false;
                    if (areaId == 19 || areaId == 18)  // 如果是北京或者广州默认上班时间是9:30--18:30
                    {
                        commuterTimeModel.GoWorkTime = Status.BJDefaultGoWorkTime;
                        commuterTimeModel.OffWorkTime = Status.BJDefaultOffWorkTime;
                        commuterTimeModel.WorkingDays_OverTime1 = 2.5f;
                        commuterTimeModel.WorkingDays_OverTime2 = 5.5f;
                        commuterTimeModel.LateGoWorkTime_OverTime1 = 1;
                    }
                    else
                    {
                        commuterTimeModel.GoWorkTime = Status.DefaultGoWorkTime;
                        commuterTimeModel.OffWorkTime = Status.DefaultOffWorkTime;
                        commuterTimeModel.WorkingDays_OverTime1 = 3;
                        commuterTimeModel.WorkingDays_OverTime2 = 6;
                        commuterTimeModel.LateGoWorkTime_OverTime1 = 1;
                    }
                    commuterTimeModel.OperatorDept = 0;
                    commuterTimeModel.OperatorID = currentUserID;
                    commuterTimeModel.Sort = 0;
                    commuterTimeModel.UpdateTime = DateTime.Now;
                    commuterTimeModel.UserCode = UserCode;
                    commuterTimeModel.UserID = UserID;
                    commuterTimeModel.UserName = EmployeeName;
                    CommuterTimeDataProvider commuterTimeDal = new CommuterTimeDataProvider();
                    commuterTimeDal.Add(commuterTimeModel, conn, trans);
                    #endregion

                    trans.Commit();
                    if (id > 0)
                        return model.CardNo;
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.Message, "行政系统", ESP.Logging.LogLevel.Error, ex);
                    return null;
                }
            }
        }

        /// <summary>
        /// 通过用户编号获得用户的考勤基本信息对象
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public UserAttBasicInfo GetEnableCardByUserid(int userid)
        {
            string strwhere = " userid={0} and deleted=0 and CardState=" + (int)CardUseState.Enable;
            if (userid != 0)
            {
                strwhere = string.Format(strwhere, userid);
            }
            DataSet ds = dal.GetList(strwhere);
            ESP.Administrative.Entity.UserAttBasicInfo usercardinfo = new ESP.Administrative.Entity.UserAttBasicInfo();
            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                usercardinfo.PopupData(ds.Tables[0].Rows[0]);
            }
            return usercardinfo;
        }

        /// <summary>
        /// 更换门卡信息
        /// </summary>
        /// <param name="oldCardInfo">原门卡信息</param>
        /// <param name="newCardInfo">更换后的门卡信息</param>
        /// <returns>如果更新成功返回true，否则返回false</returns>
        public bool ExChangeCardNo(UserAttBasicInfo oldCardInfo, UserAttBasicInfo newCardInfo, CardStoreInfo cardStoreModel)
        {
            bool b = false;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                CardStoreDataProvider cardStoreManager = new CardStoreDataProvider();
                // 修改老门卡信息库中该门卡信息， 标识该卡已经作废
                CardStoreInfo oldCardStorModel = cardStoreManager.GetModelByCardNo(oldCardInfo.CardNo);
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // 更新老的门卡信息
                    dal.Update(oldCardInfo, conn, trans);
                    // 插入新的门卡信息
                    dal.Add(newCardInfo, conn, trans);

                    // 修改新的门卡信息库中该门卡信息，标识该卡已经使用
                    if (cardStoreModel != null)
                    {
                        cardStoreModel.State = (int)CardStoreState.Used;
                        cardStoreModel.UpdateTime = DateTime.Now;
                        cardStoreModel.OperatorID = ESP.Framework.BusinessLogic.UserManager.GetCurrentUserID();
                        cardStoreManager.Update(cardStoreModel, conn, trans);
                    }
                    if (oldCardStorModel != null)
                    {
                        oldCardStorModel.State = (int)CardStoreState.BlankOut;
                        oldCardStorModel.UpdateTime = DateTime.Now;
                        oldCardStorModel.OperatorID = ESP.Framework.BusinessLogic.UserManager.GetCurrentUserID();
                        cardStoreManager.Update(oldCardStorModel, conn, trans);
                    }
                    //WaitingTaskDataProvider waitingTaskDal = new WaitingTaskDataProvider();
                    //// 获得开卡任务对象
                    //WaitingTaskInfo waitingTaskModel = this.GetWaitingTaskModel(oldCardInfo, TaskType.UnEnableCard);
                    //// 获得停卡任务对象
                    //WaitingTaskInfo waitingTaskModel2 = this.GetWaitingTaskModel(newCardInfo, TaskType.EnableCard);

                    //waitingTaskDal.Add(waitingTaskModel, conn, trans);
                    //waitingTaskDal.Add(waitingTaskModel2, conn, trans);
                    trans.Commit();
                    b = true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    b = false;
                    ESP.Logging.Logger.Add(ex.Message, "行政系统", ESP.Logging.LogLevel.Error, ex);
                }
            }
            return b;
        }

        /// <summary>
        /// 获得用户所在公司的ID，如北京的用户登录获得总部的部门ID
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <returns>返回部门编号</returns>
        public ESP.Framework.Entity.DepartmentInfo GetRootDepartmentID(int UserID)
        {
            ESP.Framework.Entity.DepartmentInfo model = new ESP.Framework.Entity.DepartmentInfo();
            // 获得当前登录用户的人员部门信息
            IList<ESP.Framework.Entity.EmployeePositionInfo> list = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(UserID);

            if (list != null && list.Count > 0)
            {
                ESP.Framework.Entity.EmployeePositionInfo empPosInfo = list[0];
                List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
                ESP.Framework.Entity.DepartmentInfo departmentInfo = ESP.Framework.BusinessLogic.DepartmentManager.Get(empPosInfo.DepartmentID);
                if (departmentInfo != null && departmentInfo.DepartmentLevel == 3)
                {
                    // 添加当前用户上级部门信息
                    if (!string.IsNullOrEmpty(departmentInfo.Description))
                    {
                        model = ESP.Framework.BusinessLogic.DepartmentManager.Get(int.Parse(departmentInfo.Description));
                    }
                                      
                }
            }
            return model;
        }

        /// <summary>
        /// 获得用户所在部门的编号
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <returns>返回部门编号</returns>
        public int GetUserDepartmentId(int UserID)
        {
            IList<ESP.Framework.Entity.EmployeePositionInfo> list = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(UserID);
            if (list != null && list.Count > 0)
            {
                return list[0].DepartmentID;
            }
            return 0;
        }

        /// <summary>
        /// 获得门卡任务对象
        /// </summary>
        /// <param name="model"></param>
        /// <param name="taskType"></param>
        /// <returns></returns>
        public WaitingTaskInfo GetWaitingTaskModel(UserAttBasicInfo model, TaskType taskType)
        {
            // 停卡任务
            WaitingTaskInfo waitingTaskModel = new WaitingTaskInfo();
            waitingTaskModel.CardNo = int.Parse(model.CardNo);
            waitingTaskModel.CreateTime = DateTime.Now;
            waitingTaskModel.Deleted = false;
            waitingTaskModel.OperatorDept = 0;
            waitingTaskModel.OperatorID = ESP.Framework.BusinessLogic.UserManager.GetCurrentUserID();
            waitingTaskModel.Sort = 0;
            waitingTaskModel.TaskType = (int)taskType;  // 启用门卡
            waitingTaskModel.UpdateTime = DateTime.Now;
            waitingTaskModel.UserCode = model.UserCode;
            waitingTaskModel.UserName = model.EmployeeName;

            //230重庆分公司 ,19总部
            int departmentID = this.GetRootDepartmentID(model.Userid).DepartmentID;
            if (departmentID == (int)AreaID.HeadOffic)
            {
                waitingTaskModel.AreaID = 1;
            }
            else if (departmentID == (int)AreaID.ChongqingOffic)
            {
                waitingTaskModel.AreaID = 2;
            }
            else
            {
                waitingTaskModel.AreaID = 1;
            }
            return waitingTaskModel;
        }

        /// <summary>
        /// 获得用户使用的门卡历史信息
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <returns>返回用户使用的门卡历史信息集合</returns>
        public List<UserAttBasicInfo> GetUserHistoryCard(int userID)
        {
            return dal.GetUserHistoryCard(userID);
        }

        /// <summary>
        /// 获得考勤人员信息
        /// </summary>
        /// <returns>返回考勤人员信息</returns>
        public DataSet GetAttendanceUserInfo(int UserID)
        {
            string strUserIds = this.GetStatUserIDs(UserID);
            return dal.GetAttendanceUserInfo(strUserIds);
        }

        /// <summary>
        /// 获得用户有权查看的人员用户信息集合
        /// </summary>
        /// <param name="UserID">用户编号</param>
        public string GetStatUserIDs(int UserID)
        {
            DataCodeManager dataCodeManager = new DataCodeManager();
            //// 获得考勤系统各地的考勤管理员编号
            //DataCodeInfo datamodel = dataCodeManager.GetDataCodeByType("BeijingAttendanceAdmin")[0];
            ////string[] datacode = datamodel.Code.Split(new char[] { ',' });
            //DataCodeInfo datamodelshanghai = dataCodeManager.GetDataCodeByType("ShanghaiAttendanceAdmin")[0];
            //string[] datacodeshanghai = datamodelshanghai.Code.Split(new char[] { ',' });
            //DataCodeInfo datamodelguangzhou = dataCodeManager.GetDataCodeByType("GuangzhouAttendanceAdmin")[0];
            //string[] datacodeguangzhou = datamodelguangzhou.Code.Split(new char[] { ',' });
            //// 获得团队HRAdmin和考勤统计员的编号
            //DataCodeInfo HRAdminIdModel = dataCodeManager.GetDataCodeByType("HRAdminIDs")[0];
            //string HRAdminIds = HRAdminIdModel.Code;
            //DataCodeInfo StatisticianIDModel = dataCodeManager.GetDataCodeByType("StatisticianIDs")[0];
            //string StatisticianIds = StatisticianIDModel.Code;
            // 集团CEO查看所有统计信息
            DataCodeInfo CEOIdModel = dataCodeManager.GetDataCodeByType("CEOSeeAllStatisticIDs")[0];
            string CEOSeeAllStatisticIDs = CEOIdModel.Code;
            //DataCodeInfo CreatvieHR = dataCodeManager.GetDataCodeByType("CreativeHR")[0];
            //string creatviehrids = CreatvieHR.Code;

            //DataCodeInfo CreatvieDeptId = dataCodeManager.GetDataCodeByType("CreativeDeptId")[0];
            //string creatviedeptid = CreatvieDeptId.Code;

            OperationAuditManageManager operationAuditManager = new OperationAuditManageManager();
            //MonthStatManager monthStatManager = new MonthStatManager();
            IList<EmployeeInfo> list = new List<EmployeeInfo>();
            if (CEOSeeAllStatisticIDs.IndexOf(UserID.ToString()) != -1)
            {
                list = operationAuditManager.GetAllList();
            }

            //// 判断用户是否是团队HRAdmin
            //else if (HRAdminIds.IndexOf(UserID.ToString()) != -1)
            //{
            //   // int approveUserId = UserID;
            //    // 杨帆（echo.yang/13416）的考勤统计信息梁薇（lucy.liang/13444）协同审批
            //    //if (UserID == 13444)
            //    //{
            //    //    approveUserId = 13416;
            //    //}
            //    list = operationAuditManager.GetHRAdminSubordinates(UserID);
            //}
            //// 判断用户是否是团队考勤统计员
            //else if (StatisticianIds.IndexOf(UserID.ToString()) != -1)
            //{
            //    list = operationAuditManager.GetStatisticianSubordinates(UserID);
            //}
            //else if (creatviehrids.IndexOf(UserID.ToString()) != -1)
            //{
            //    list = operationAuditManager.GetCreativeUsers(creatviedeptid);
            //}
            //// 普通员工和各个部门小组的TearmLeader
            else
            {
                list = operationAuditManager.GetAllSubordinates(UserID);
            }

            // 拼接用户ID字符串
            StringBuilder strUserIds = new StringBuilder();
            strUserIds.Append(UserID + ",");
            if (list != null && list.Count > 0)
            {
                foreach (EmployeeInfo employeeModel in list)
                {
                    if (employeeModel != null)
                        strUserIds.Append(employeeModel.UserID + ",");
                }
            }

            string userids = strUserIds.ToString();
            if (userids.EndsWith(","))
            {
                userids = userids.TrimEnd(new char[] { ',' });
            }
            return userids;
        }

        /// <summary>
        /// 获得统计人员信息
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <returns>返回统计人员信息集合</returns>
        public IList<EmployeeInfo> GetStatUsers(int UserID, out string userids)
        {
            userids = "";
            DataCodeManager dataCodeManager = new DataCodeManager();
            // 获得考勤系统各地的考勤管理员编号
            DataCodeInfo datamodel = dataCodeManager.GetDataCodeByType("BeijingAttendanceAdmin")[0];
            string[] datacode = datamodel.Code.Split(new char[] { ',' });
            DataCodeInfo datamodelchongqing = dataCodeManager.GetDataCodeByType("ChongQingAttendanceAdmin")[0];
            string[] datacodechongqing = datamodelchongqing.Code.Split(new char[] { ',' });
          
            // 获得团队HRAdmin和考勤统计员的编号
            DataCodeInfo HRAdminIdModel = dataCodeManager.GetDataCodeByType("HRAdminIDs")[0];
            string HRAdminIds = HRAdminIdModel.Code;
            DataCodeInfo StatisticianIDModel = dataCodeManager.GetDataCodeByType("StatisticianIDs")[0];
            string StatisticianIds = StatisticianIDModel.Code;
            // 集团CEO查看所有统计信息
            DataCodeInfo CEOIdModel = dataCodeManager.GetDataCodeByType("CEOSeeAllStatisticIDs")[0];
            string CEOSeeAllStatisticIDs = CEOIdModel.Code;

            OperationAuditManageManager operationAuditManager = new OperationAuditManageManager();
            MonthStatManager monthStatManager = new MonthStatManager();
            IList<EmployeeInfo> list = new List<EmployeeInfo>();
            if (CEOSeeAllStatisticIDs.IndexOf(UserID.ToString()) != -1)
            {
                list = operationAuditManager.GetAllList();
            }
            // 判断用户是否是系统管理员
            else if (UserID.ToString() == datacode[0] || UserID.ToString() == datacodechongqing[0]
                || (datacode.Length > 3 && UserID.ToString() == datacode[2]))
            {
                if (UserID.ToString() == datacode[0] || (datacode.Length > 3 && UserID.ToString() == datacode[2]))
                {
                    list = operationAuditManager.GetADAdminInfos((int)AreaID.HeadOffic);
                }
                else if (UserID.ToString() == datacodechongqing[0] || (datacodechongqing.Length > 3 && UserID.ToString() == datacodechongqing[2]))
                {
                    list = operationAuditManager.GetADAdminInfos((int)AreaID.ChongqingOffic);
                }
            }
            // 判断用户是否是团队HRAdmin
            else if (HRAdminIds.IndexOf(UserID.ToString()) != -1)
            {
                list = operationAuditManager.GetHRAdminSubordinates(UserID);
            }
            // 判断用户是否是团队考勤统计员
            else if (StatisticianIds.IndexOf(UserID.ToString()) != -1)
            {
                list = operationAuditManager.GetStatisticianSubordinates(UserID);
            }
            // 普通员工和各个部门小组的TearmLeader
            else
            {
                list = operationAuditManager.GetAllSubordinates(UserID);
            }

            // 拼接用户ID字符串
            StringBuilder strUserIds = new StringBuilder();
            strUserIds.Append(UserID + ",");
            if (list != null && list.Count > 0)
            {
                foreach (EmployeeInfo employeeModel in list)
                {
                    strUserIds.Append(employeeModel.UserID + ",");
                }
            }

            userids = strUserIds.ToString();
            if (userids.EndsWith(","))
            {
                userids = userids.TrimEnd(new char[] { ',' });
            }
            return list;
        }

        /// <summary>
        /// 获得统计人员考勤基本信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public Dictionary<int, DataRow> GetUserAttBasicInfos(int UserID)
        {
            string userids = this.GetStatUserIDs(UserID);
            return dal.GetUserAttBasicInfos(" and u.userid in (" + userids + ")");
        }

        /// <summary>
        /// 获得门卡信息
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        public DataSet GetCardNoInfos(string strWhere, List<SqlParameter> parameters)
        {
            return dal.GetCardNoInfos(strWhere, parameters);
        }

        /// <summary>
        /// 获得所有人员考勤基本信息（用户信息，考勤信息，部门信息，职位信息等）
        /// </summary>
        /// <returns>返回一个考勤人员信息集合</returns>
        public Dictionary<int, DataRow> GetAllUserAttBasicInfos()
        {
            return dal.GetUserAttBasicInfos("");
        }

        /// <summary>
        /// 获得综合查询信息
        /// </summary>
        /// <param name="UserID">用户编号，获得用户有权限查看的人信息ID</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="parameterList">查询参数</param>
        /// <returns>返回综合信息查询结果</returns>
        public DataSet GetIntegratedQueryUserInfo(int UserID, string strWhere, List<SqlParameter> parameterList)
        {
            string userids = this.GetStatUserIDs(UserID);
            strWhere += " AND u.UserId IN (" + userids + ")";
            return dal.GetIntegratedQueryUserInfo(strWhere, parameterList);
        }

        public DataSet GetIntegratedQueryUserInfoUpdate(int userid, string strWhere, List<SqlParameter> parameterList)
        {

            DataCodeManager dataCodeManager = new DataCodeManager();

            // 集团CEO查看所有统计信息
            DataCodeInfo CEOIdModel = dataCodeManager.GetDataCodeByType("CEOSeeAllStatisticIDs")[0];
            string CEOSeeAllStatisticIDs = CEOIdModel.Code;

            if (CEOSeeAllStatisticIDs.IndexOf(userid.ToString()) != -1)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT u.*, p.*, d.level1id, d.level2id, d.level3id, d.level1+'-'+d.level2+'-'+d.level3 AS Department FROM ad_userattbasicinfo u ");
                strSql.Append(" LEFT JOIN SEP_Employees y ON u.userid=y.userid ");
                strSql.Append(" LEFT JOIN SEP_EmployeesInPositions e ON u.userid=e.userid ");
                strSql.Append(" LEFT JOIN SEP_DepartmentPositions p ON e.DepartmentPositionID=p.DepartmentPositionID ");
                strSql.Append(" LEFT JOIN V_Department d ON e.departmentid = d.level3Id ");
                strSql.Append(" WHERE 1=1 ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" " + strWhere);
                }

                strSql.Append(" ORDER BY u.UserCode ");

                return DbHelperSQL.Query(strSql.ToString(), parameterList.ToArray());
            }
            else
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT u.*, p.*, d.level1id, d.level2id, d.level3id, d.level1+'-'+d.level2+'-'+d.level3 AS Department FROM ad_userattbasicinfo u ");
                strSql.Append(" LEFT JOIN SEP_Employees y ON u.userid=y.userid ");
                strSql.Append(" LEFT JOIN SEP_EmployeesInPositions e ON u.userid=e.userid ");
                strSql.Append(" LEFT JOIN SEP_DepartmentPositions p ON e.DepartmentPositionID=p.DepartmentPositionID ");
                strSql.Append(" LEFT JOIN V_Department d ON e.departmentid = d.level3Id ");
                strSql.Append(" LEFT JOIN ad_operationauditmanage ad ON ad.userid =y.userid ");
                strSql.Append(" WHERE u.deleted=0 and (ad.teamleaderid =" + userid + " or ad.hradminId =" + userid + " or ad.StatisticianID =" + userid + " or ad.ManagerID=" + userid + ")");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" " + strWhere);
                }

                strSql.Append(" ORDER BY u.UserCode ");

                return DbHelperSQL.Query(strSql.ToString(), parameterList.ToArray());
            }
        }

        /// <summary>
        /// 计算用户的年假信息
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <param name="year">年份</param>
        /// <param name="userAttBasicModel">用户考勤基础信息对象</param>
        /// <returns>返回用户年度年假总数</returns>
        public double GetAnnualLeave(int UserID, int year, UserAttBasicInfo userAttBasicModel)
        {
            // 年假天数
            double totalAnnualDays = 0;
            if (userAttBasicModel != null)
            {
                DateTime calDateTime = new DateTime(year, 1, 1);
                // 获得用户的入职信息
                EmployeeJobInfo jobinfo = EmployeeJobManager.getModelBySysId(UserID);
                if (jobinfo != null)
                {
                    // 入职时间
                    DateTime EntryTime = jobinfo.joinDate;
                    if (EntryTime != null && EntryTime.Year <= year)
                    {
                        // 如果是当年入职，当年入职没有奖励年假
                        if (EntryTime.Year == year)
                        {
                            // 判断当前年份是否
                            int OneYearDays = 365;
                            if (DateTime.IsLeapYear(year))
                            {
                                OneYearDays = 366;
                            }
                            DateTime endDay = new DateTime(year, 12, 31);
                            TimeSpan span = endDay - EntryTime;
                            totalAnnualDays = span.TotalDays / OneYearDays * userAttBasicModel.AnnualLeaveBase;
                            int tempdays = (int)totalAnnualDays;
                            if ((tempdays + 0.5) >= totalAnnualDays)
                                totalAnnualDays = tempdays;
                            else
                                totalAnnualDays = tempdays + 0.5;
                        }
                        else     // 服务年限满三年以上的才有奖励年限
                        {
                            // 计算用户的服务年限
                            int serviceAge = Utility.GetServiceAge(calDateTime, EntryTime);
                            // 如果计算出的年假天数大于最大的年假天数，则将年假天数设为最大年假天数
                            totalAnnualDays = serviceAge + userAttBasicModel.AnnualLeaveBase;
                            if (totalAnnualDays > Status.MaxAnnualLeaveDay)
                            {
                                totalAnnualDays = Status.MaxAnnualLeaveDay;
                            }
                        }
                    }
                }
            }
            return totalAnnualDays;
        }

        /// <summary>
        /// 获得最后一条考勤基础信息对象
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <returns>返回一个用户考勤基础信息对象</returns>
        public UserAttBasicInfo GetLastUserAttBasicInfo(int userid)
        {
            return dal.GetLastUserAttBasicInfo(userid);
        }
        #endregion 成员方法
    }
}