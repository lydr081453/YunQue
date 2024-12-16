using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ESP.Administrative.Common;
using ESP.Administrative.DataAccess;
using ESP.Administrative.Entity;
using System.Data.SqlClient;

namespace ESP.Administrative.BusinessLogic
{
    public class SingleOvertimeManager
    {
        #region 变量定义
        /// <summary>
        /// OT数据信息对象
        /// </summary>
        private readonly SingleOvertimeDataProvider dal = new SingleOvertimeDataProvider();
        /// <summary>
        /// OT时间数据信息对象
        /// </summary>
        private readonly OverDateTimeDataProvider overDateTimeDal = new OverDateTimeDataProvider();
        /// <summary>
        /// 考勤业务操作对象
        /// </summary>
        private readonly AttendanceManager attendanceManager = new AttendanceManager();
        #endregion

        public SingleOvertimeManager()
        { }

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">OT单数据对象</param>
        /// <param name="overDateTimeInfoList">OT时间信息集合</param>
        /// <returns>返回新添加的OT的ID值</returns>
        public int Add(SingleOvertimeInfo model, List<OverDateTimeInfo> overDateTimeInfoList)
        {
            if (model != null && overDateTimeInfoList != null && overDateTimeInfoList.Count > 0)
            {
                using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    try
                    {
                        // 保存OT单信息
                        int modelId = dal.Add(model);

                        // 循环保存OT时间信息
                        foreach (OverDateTimeInfo overDateTime in overDateTimeInfoList)
                        {
                            overDateTime.SingleOvertimeID = modelId;
                            overDateTimeDal.Add(overDateTime);
                        }
                        trans.Commit();
                        return modelId;
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SingleOvertimeInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 通过OT单ID获得OT时间信息集合
        /// </summary>
        /// <param name="singleID">OT单ID</param>
        /// <returns>返回一个OT单时间信息集合</returns>
        public List<OverDateTimeInfo> GetOverDateTimeInfoBySingleId(int singleID)
        {
            List<OverDateTimeInfo> list = new List<OverDateTimeInfo>();
            DataSet ds = overDateTimeDal.GetList(" singleovertimeid=" + singleID + " order by OverTimeDateTime desc ");
            if (ds != null && ds.Tables != null && ds.Tables[0] != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    OverDateTimeInfo overDateTimeInfo = new OverDateTimeInfo();
                    overDateTimeInfo.PopupData(dr);
                    list.Add(overDateTimeInfo);
                }
            }
            return list;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">OT单数据对象</param>
        /// <param name="overDateTimeInfoList">OT时间信息集合</param>
        public void Update(SingleOvertimeInfo model, List<OverDateTimeInfo> overDateTimeInfoList)
        {
            if (model != null && overDateTimeInfoList != null && overDateTimeInfoList.Count > 0)
            {
                using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    try
                    {
                        // 修改OT单信息
                        dal.Update(model);

                        // 循环修改OT时间信息
                        foreach (OverDateTimeInfo overDateTime in overDateTimeInfoList)
                        {
                            overDateTime.SingleOvertimeID = model.ID;
                            if (overDateTime.ID > 0)
                                overDateTimeDal.Update(overDateTime);
                            else
                                overDateTimeDal.Add(overDateTime);
                        }
                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                    }
                }
            }
        }

        /// <summary>
        /// 更新数据集合
        /// </summary>
        /// <param name="model">OT单数据对象集合</param>        
        public void Update(List<SingleOvertimeInfo> list)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    if (list.Count > 0)
                    {
                        foreach (SingleOvertimeInfo info in list)
                        {
                            dal.Update(info);
                        }
                        trans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add(ex.ToString());
                    trans.Rollback();
                }
            }
        }

        /// <summary>
        /// 修改OT单信息
        /// </summary>
        /// <param name="model">OT单信息对象</param>
        public void Update(SingleOvertimeInfo model, SqlConnection conn, SqlTransaction trans)
        {
            dal.Update(model, trans);
        }

        /// <summary>
        /// 修改OT单信息
        /// </summary>
        /// <param name="model">OT单信息对象</param>
        public void Update(SingleOvertimeInfo model)
        {
            dal.Update(model);
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
        public SingleOvertimeInfo GetModel(int ID)
        {
            return dal.GetModel(ID);
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
        public DataSet GetAllList()
        {
            return dal.GetList("");
        }

        /// <summary>
        /// 获得用户某天的OT单记录
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <param name="date">日期</param>
        /// <param name="isApprove">是否是审批通过的，true/false</param>
        /// <returns>返回一个请假单类表</returns>
        public DataSet GetList(int userid, string date, bool isApprove)
        {
            StringBuilder strbuild = new StringBuilder();
            strbuild.Append(" s.UserID={0} and ");
            strbuild.Append(" ((replace(CONVERT(char(10),o.overtimedatetime,111),'/','-') + ' ' + o.begintime between '{1} 00:00:00' and '{1} 23:59:59') ");
            strbuild.Append(" or (replace(CONVERT(char(10),o.overtimedatetime,111),'/','-') + ' ' + o.EndTime between '{1} 00:00:00' and '{1} 23:59:59') ");
            strbuild.Append(@" or '{1} 00:00:00' between replace(CONVERT(char(10),o.overtimedatetime,111),'/','-') + ' ' + o.begintime  
                             and replace(CONVERT(char(10),o.overtimedatetime,111),'/','-') + ' ' + o.EndTime)");
            if (isApprove)
                strbuild.Append("and s.State={2}");
            else
                strbuild.Append("and s.State<>{2}");

            string strwhere = string.Format(strbuild.ToString(), userid, date, Status.OverTimeState_Passed);
            return dal.GetSingleOverTimeInfo(strwhere);
        }

        /// <summary>
        /// 通过ID获得OT单对象
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DataSet GetSingleModel(int ID)
        {
            DataSet ds = dal.GetSingleOverTimeInfo(" o.id=" + ID);
            return ds;
        }

        /// <summary>
        /// 返回OT时间信息
        /// </summary>
        /// <param name="ID">OT时间信息ID</param>
        /// <returns>返回OT时间信息对象</returns>
        public OverDateTimeInfo GetOverDateTimeInfo(int ID)
        {
            return overDateTimeDal.GetModel(ID);
        }

        /// <summary>
        /// 获得OT单数据信息
        /// </summary>
        /// <param name="strwhere">查询条件</param>
        /// <returns>查询结果数据对象</returns>
        public DataSet GetSingleOverTimeInfo(string strwhere)
        {
            return dal.GetSingleOverTimeInfo(strwhere);
        }

        /// <summary>
        /// 将OT单信息关联到相对应的考勤记录信息中
        /// </summary>
        /// <param name="singleid">OT单ID</param>
        public void RelationAttendance(int singleid)
        {
            if (singleid > 0)
            {
                // 获得OT单对象
                SingleOvertimeInfo singleModel = this.GetModel(singleid);
                DataSet ds = overDateTimeDal.GetList(" singleovertimeid=" + singleid + " and deleted ='false'");
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    OverDateTimeInfo overdatetimeModel = new OverDateTimeInfo();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        overdatetimeModel.PopupData(dr);
                        // 判断OT日期是否小于当前的日期
                        if (overdatetimeModel.OverTimeDateTime.Date < DateTime.Now.Date)
                        {
                            AttendanceInfo attendanceModel =
                                attendanceManager.GetModel(singleModel.UserID, overdatetimeModel.OverTimeDateTime);
                            if (attendanceModel != null)
                            {
                                attendanceModel.Singleovertimeid = overdatetimeModel.ID;
                                attendanceModel.OverTimeHours = overdatetimeModel.OverTimeHours;
                                attendanceModel.IsOverTime = true;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获得指定日期的员工OT单
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="OvertimeDateTime"></param>
        /// <param name="isApprove">是否是审批通过的，true/false</param>
        /// <returns></returns>
        public List<SingleOvertimeInfo> GetSingleOvertimeList(int UserID, DateTime OvertimeDateTime, bool isApprove)
        {
            StringBuilder strbuild = new StringBuilder();
            strbuild.Append(" userid={0}");
            strbuild.Append(" and (begintime >= '{1}' and begintime <='{2}') and (endtime >= '{1}' and endtime <= '{2}') and Deleted='false'");
            if (isApprove)
                strbuild.Append(" and Approvestate='" + Status.OverTimeState_Passed + "'");
            else
                strbuild.Append(" and Approvestate<>'" + Status.OverTimeState_Passed + "'");
            DataSet ds = new SingleOvertimeDataProvider().GetList(string.Format(strbuild.ToString(), UserID, OvertimeDateTime.ToString("yyyy-MM-dd ") + Status.OffWorkTimePoint, OvertimeDateTime.AddDays(1).ToString("yyyy-MM-dd ") + Status.OffWorkTimePoint));
            List<SingleOvertimeInfo> list = new List<SingleOvertimeInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                SingleOvertimeInfo model = null;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SingleOvertimeInfo();
                    model.PopupData(ds.Tables[0].Rows[n]);
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得指定日期的员工OT单(供报销处使用)
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="OvertimeDateTime"></param>
        /// <param name="isApprove">是否是审批通过的，true/false</param>
        /// <returns></returns>
        public bool GetSingleOvertimeListExpense(int UserID, DateTime OvertimeDateTime, bool isApprove)
        {
            //StringBuilder strbuild = new StringBuilder();
            //strbuild.Append(" userid={0}");
            //strbuild.Append(" and (begintime >= '{1}' and begintime <='{2}') and Deleted='false'");
            //if (isApprove)
            //    strbuild.Append(" and Approvestate='" + Status.OverTimeState_Passed + "'");
            //else
            //    strbuild.Append(" and Approvestate<>'" + Status.OverTimeState_Passed + "'");
            //DataSet ds = new SingleOvertimeDataProvider().GetList(string.Format(strbuild.ToString(), UserID, OvertimeDateTime.ToString("yyyy-MM-dd ") + Status.OffWorkTimePoint, OvertimeDateTime.AddDays(1).ToString("yyyy-MM-dd ") + Status.OffWorkTimePoint));
            //List<SingleOvertimeInfo> list = new List<SingleOvertimeInfo>();
            //int rowsCount = ds.Tables[0].Rows.Count;
            //if (rowsCount > 0)
            //{
            //    SingleOvertimeInfo model = null;
            //    for (int n = 0; n < rowsCount; n++)
            //    {
            //        model = new SingleOvertimeInfo();
            //        model.PopupData(ds.Tables[0].Rows[n]);
            //        list.Add(model);
            //    }
            //}
            //ESP.Administrative.Entity.TimeSheetCommitInfo commitModel = ESP.Administrative.BusinessLogic.TimeSheetCommitManager.GetModel(UserID, OvertimeDateTime.ToString("yyyy-MM-dd"));
            //if (commitModel == null)
                return true;
            //else
            //{
            //    decimal worktime = ESP.Administrative.BusinessLogic.TimeSheetCommitManager.GetUserSumTimeSheetHours(commitModel);
            //    decimal overworkhours = decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["WorkTimeForDay"]);
            //    decimal overworkholihours = decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["WorkTimeForholiDay"]);
            //    ESP.Administrative.Entity.HolidaysInfo holi = (new ESP.Administrative.BusinessLogic.HolidaysInfoManager()).GetHolideysInfoByDatetime(commitModel.CurrentDate.Value);
            //    if (holi != null)
            //    {
            //        if (worktime >= overworkholihours)
            //        {
            //            return true;
            //        }
            //        else
            //        {
            //            return false;
            //        }
            //    }
            //    else
            //    {
            //        if (worktime >= overworkhours)
            //            return true;
            //        else
            //            return false;
            //    }
            //}
        }

        /// <summary>
        /// 获得指定日期的员工OT单
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <param name="OvertimeDateTime">要获取的OT日期</param>
        /// <param name="isApprove">是否是审批通过的，true/false</param>
        /// <returns>获得OT单信息集合</returns>
        public List<SingleOvertimeInfo> GetSingleOvertimeList(int UserID, DateTime OvertimeDateTime)
        {
            StringBuilder strbuild = new StringBuilder();
            strbuild.Append(" userid={0}");
            strbuild.Append(" and (CAST((Convert(Char(10),'{1}',120)) AS DATETIME) between CAST((Convert(Char(10),begintime,120)) AS DATETIME) and CAST((Convert(Char(10),EndTime,120)) AS DATETIME)) and Deleted='false'");
            DataSet ds = new SingleOvertimeDataProvider().GetList(string.Format(strbuild.ToString(),
                UserID, OvertimeDateTime.ToString("yyyy-MM-dd HH:mm:ss")));
            List<SingleOvertimeInfo> list = new List<SingleOvertimeInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                SingleOvertimeInfo model = null;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SingleOvertimeInfo();
                    model.PopupData(ds.Tables[0].Rows[n]);
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得有效的OT单
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <param name="CutOffDate">截止日期</param>
        public List<SingleOvertimeInfo> GetEffectiveSingleOverTime(int UserID, DateTime CutOffDate)
        {
            StringBuilder strbuild = new StringBuilder();
            strbuild.Append(" approvestate={0} and state<>{1} and beginTime between '{2}' and '{3}' and deleted=0 and UserID={4} order by apptime asc");
            DataSet ds = this.GetList(string.Format(strbuild.ToString(), Status.OverTimeState_Passed,
                Status.SingleOverTimeUserState_Used, CutOffDate.AddMonths(-1), CutOffDate, UserID));
            List<SingleOvertimeInfo> list = new List<SingleOvertimeInfo>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SingleOvertimeInfo singleOverTime = new SingleOvertimeInfo();
                    singleOverTime.PopupData(dr);
                    list.Add(singleOverTime);
                }
            }
            return list;
        }


        /// <summary>
        /// 获得有效的OT单
        /// </summary>
        /// <param name="UserIDs">用户编号(字符串，逗号分隔)</param>
        /// <param name="CutOnDate">起始日期</param>
        /// <param name="CutOffDate">截止日期</param>
        public IList<SingleOvertimeInfo> GetEffectiveSingleOverTime(string UserIDs, DateTime CutOnDate, DateTime CutOffDate,string username ,string usercode , int overtimetype)
        {
            StringBuilder strbuild = new StringBuilder();
            strbuild.Append(" approvestate={0} and (beginTime between '{1}' and '{2}') and deleted=0 and UserID in ({3})");
            if (!string.IsNullOrEmpty(username))
            {
                strbuild.Append(" and employeename like '%"+username+"%'");
            }
            if (!string.IsNullOrEmpty(usercode))
            {
                strbuild.Append(" and usercode like '%" + usercode + "%'");
            }
            if (overtimetype == -1)
            {
                strbuild.Append(" and CreateTime < BeginTime");
            }
            else if (overtimetype == 1)
            {
                strbuild.Append(" and CreateTime > BeginTime");
            }
            strbuild.Append("  order by apptime asc");
            DataSet ds = this.GetList(string.Format(strbuild.ToString(), Status.OverTimeState_Passed, CutOnDate, CutOffDate, UserIDs));
            IList<SingleOvertimeInfo> list = new List<SingleOvertimeInfo>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SingleOvertimeInfo singleOverTime = new SingleOvertimeInfo();
                    singleOverTime.PopupData(dr);
                    list.Add(singleOverTime);
                }
            }
            return list;
        }

        /// <summary>
        /// 截止到当前调休日期位置，倒休剩余有效小时数
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <param name="offtuneDateTime">调休日期</param>
        /// <returns></returns>
        public int GetRemainingHours(int UserID, DateTime offtuneDateTime)
        {
            int remaining = 0;
            StringBuilder strbuild = new StringBuilder();
            strbuild.Append(" approvestate={0} and state<>{1} and beginTime between '{2}' and '{3}' and deleted=0 and UserID={4}");
            DataSet ds = this.GetList(string.Format(strbuild.ToString(), Status.OverTimeState_Passed,
                Status.SingleOverTimeUserState_Used, offtuneDateTime.Date.AddMonths(-1), offtuneDateTime.Date, UserID));
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    remaining += int.Parse(dr["RemainingHours"].ToString());
                }
            }
            return remaining;
        }

        /// <summary>
        /// 获得一个星期内要失效的OT小时数
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <param name="offtuneDateTime">调休小时数</param>
        /// <returns></returns>
        public int InvalidWithinAWeek(int UserID, DateTime offtuneDateTime)
        {
            int remaining = 0;
            StringBuilder strbuild = new StringBuilder();
            strbuild.Append(" approvestate={0} and state<>{1} and beginTime between '{2}' and '{3}' and deleted=0 and UserID={4} ");
            DataSet ds = this.GetList(string.Format(strbuild.ToString(), Status.OverTimeState_Passed,
                            Status.SingleOverTimeUserState_Used, offtuneDateTime.Date.AddMonths(-1), offtuneDateTime.Date.AddMonths(-1).AddDays(7), UserID));
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    remaining += int.Parse(dr["RemainingHours"].ToString());
                }
            }
            return remaining;
        }

        /// <summary>
        /// 获得用户某月的OT信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>返回一个OT信息集合</returns>
        public IList<SingleOvertimeInfo> GetModelListByMonth(int userId, int year, int month)
        {
            return dal.GetModelListByMonth(userId, year, month);
        }

        /// <summary>
        /// 获取OT统计信息
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <returns></returns>
        public DataSet GetModelList(int userId, int year, int month, int day, string strwhere, List<SqlParameter> parameterList)
        {
            UserAttBasicInfoManager userAttBasicManager = new UserAttBasicInfoManager();
            string userids = userAttBasicManager.GetStatUserIDs(userId);
            strwhere += " AND s.userid IN (" + userids + ")";
            return dal.GetModelList(year, month, day, strwhere, parameterList);
        }

        /// <summary>
        /// 获得统计OT信息，前一天的OT可以抵消第二天的迟到或者旷工半天
        /// </summary>
        /// <param name="year">年月</param>
        /// <param name="month">月份</param>
        /// <param name="day">日期</param>
        /// <param name="isApproved">是否已经审批通过，true表示审批通过，false表示审批未通过</param>
        /// <returns>返回统计的OT信息集合</returns>
        public Dictionary<int, List<SingleOvertimeInfo>> GetStatSingleOvertimeInfos(int year, int month, int day)
        {
            return dal.GetStatSingleOvertimeInfos(year, month, day);
        }

        /// <summary>
        /// 获得用户OT信息
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>返回一个OT信息集合</returns>
        public Dictionary<int, List<SingleOvertimeInfo>> GetModelListByMonth(int year, int month)
        {
            return dal.GetModelListByMonth(year, month);
        }


        public List<SingleOvertimeInfo> GetSingleOvertimeByDepid(int depid)
        {
            return dal.GetSingleOvertimeByDepid(depid);
        }
        #endregion  成员方法
    }
}