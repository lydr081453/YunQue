using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.DataAccess;
using ESP.Administrative.Entity;
using System.Data;
using System.Data.SqlClient;
using ESP.Administrative.Common;
using System.Web;
using System.Net.Mail;

namespace ESP.Administrative.BusinessLogic
{
    public class TimeSheetCommitManager
    {
        public readonly static TimeSheetCommitDataProvider Provider = new TimeSheetCommitDataProvider();
        public readonly static TimeSheetDataProvider timeSheetProvider = new TimeSheetDataProvider();
       // public readonly static MonthStatManager monthStatManager = new MonthStatManager();
        public readonly static MonthStatDataProvider monthStatProvider = new MonthStatDataProvider();

        public static int Add(TimeSheetCommitInfo model)
        {
            return Provider.Add(model);
        }

        public static int AddByLongHoliday(TimeSheetCommitInfo model, decimal hours, out string errorMsg)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int returnId = Provider.Add(model, trans);

                    TimeSheetInfo timesheetModel = new TimeSheetInfo();

                    timesheetModel.CategoryId = model.CategoryId.Value;
                    timesheetModel.CategoryName = model.CategoryName;
                    timesheetModel.CommitId = returnId;
                    timesheetModel.CreateDate = model.CreateDate.Value;
                    timesheetModel.Hours = hours;
                    timesheetModel.IP = model.IP;
                    timesheetModel.SubmitDate = model.CommitDate.Value;
                    timesheetModel.TypeId = (int)ESP.Administrative.Common.TimeSheetType.Holiday;
                    timesheetModel.UserId = model.UserId.Value;
                    timesheetModel.UserName = model.UserName;
                    timesheetModel.WorkItem = model.Description;

                    int timesheetId = timeSheetProvider.Add(timesheetModel, trans);
                    timesheetModel.Id = timesheetId;
                    TimeSheetManager.CheckTimeSheetInfo(timesheetModel, trans);
                    trans.Commit();
                    errorMsg = "";
                    return returnId;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    errorMsg = ex.Message;
                    return 0;
                }
            }
        }

        public static bool UpdateByLongHoliday(TimeSheetCommitInfo model, decimal hours, out string errorMsg)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    TimeSheetInfo timesheetModel = TimeSheetManager.GetList(" commitId=" + model.Id)[0];
                    TimeSheetManager.RollBackOrDelInfo(timesheetModel.Id, trans);
                    Provider.Update(model, trans);

                    timesheetModel.CategoryId = model.CategoryId.Value;
                    timesheetModel.CategoryName = model.CategoryName;
                    timesheetModel.CommitId = model.Id;
                    timesheetModel.CreateDate = model.CreateDate.Value;
                    timesheetModel.Hours = hours;
                    timesheetModel.IP = model.IP;
                    timesheetModel.SubmitDate = model.CommitDate.Value;
                    timesheetModel.TypeId = (int)ESP.Administrative.Common.TimeSheetType.Holiday;
                    timesheetModel.UserId = model.UserId.Value;
                    timesheetModel.UserName = model.UserName;
                    timesheetModel.WorkItem = model.Description;
                    TimeSheetManager.CheckTimeSheetInfo(timesheetModel, trans);

                    trans.Commit();
                    errorMsg = "";
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    errorMsg = ex.Message;
                    return false;
                }
            }
        }

        public static bool Update(TimeSheetCommitInfo model)
        {
            return Provider.Update(model);
        }

        public static bool SubmitAndUpdateMonthStat(TimeSheetCommitInfo model)
        {
            bool retvalue = false;
            //ESP.Administrative.Entity.OperationAuditManageInfo manage = new ESP.Administrative.BusinessLogic.OperationAuditManageManager().GetOperationAuditModelByUserID(model.UserId.Value);
            
            //List<TimeSheetInfo> totalList = timeSheetProvider.GetList(" commitId=" + model.Id);
            //List<TimeSheetInfo> tList = totalList.Where(x => x.CategoryId == (int)TimeSheetCategoryIds.PrenatalCheckId || x.CategoryId == (int)TimeSheetCategoryIds.FuneralLeaveId || x.CategoryId == (int)TimeSheetCategoryIds.MarriageLeaveId || x.CategoryId == (int)TimeSheetCategoryIds.MaternityLeaveId).ToList();
            //DateTime bdate = new DateTime(model.CurrentDate.Value.Year,model.CurrentDate.Value.Month,1);
            //DateTime edate = bdate.AddMonths(1);
            //List<TimeSheetInfo> sicklist = timeSheetProvider.GetList(" categoryId=" + (int)TimeSheetCategoryIds.SickLeaveId + " and commitid in(select id from ad_timesheetcommit where status in(2,3,4) and  userid="+ model.UserId+" and (currentdate between '"+bdate.ToString("yyyy-MM-dd")+"' and '"+edate.ToString("yyyy-MM-dd")+"'))");

            //List<TimeSheetInfo> anuallist = timeSheetProvider.GetList(" (categoryId=" + (int)TimeSheetCategoryIds.AnnualLeaveId + " or categoryId=" + (int)TimeSheetCategoryIds.AffairLeaveId + ") and commitid in(select id from ad_timesheetcommit where status in(2,3,4) and  userid=" + model.UserId + " and (currentdate between '" + bdate.ToString("yyyy-MM-dd") + "' and '" + edate.ToString("yyyy-MM-dd") + "'))");

            //decimal sickhours = sicklist.Sum(x => x.Hours) + totalList.Where(x => x.CommitId == model.Id && x.CategoryId == (int)TimeSheetCategoryIds.SickLeaveId).Sum(x => x.Hours);
            //decimal eventhours = anuallist.Where(x => x.CategoryId == (int)TimeSheetCategoryIds.AffairLeaveId).Sum(x => x.Hours) + totalList.Where(x => x.CommitId == model.Id && x.CategoryId == (int)TimeSheetCategoryIds.AffairLeaveId).Sum(x => x.Hours);
            //decimal annualhours = anuallist.Where(x => x.CategoryId == (int)TimeSheetCategoryIds.AnnualLeaveId).Sum(x => x.Hours) + totalList.Where(x => x.CommitId == model.Id && x.CategoryId == (int)TimeSheetCategoryIds.AnnualLeaveId).Sum(x => x.Hours); 
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //if ((tList != null && tList.Count > 0) || sickhours>=16)
                    //{
                    //    //如有产检需HR审批
                    //    model.Status = (int)ESP.Administrative.Common.TimeSheetCommitStatus.WaitHR;
                    //}
                    Provider.Update(model, trans);
                   // MonthStatInfo statInfo = monthStatManager.GetNewMonthStatInfo(model.CurrentDate.Value, model.UserId.Value, model.UserCode, model.UserName, trans);
                    //if (statInfo.ID != 0)
                    //    monthStatProvider.Update(statInfo, trans);
                    //else
                    //{
                    //    int approvedateid = monthStatProvider.Add(statInfo, trans);
                    //    ESP.Administrative.Entity.ApproveLogInfo log = new ApproveLogInfo();
                    //    log.ApproveDateID = approvedateid;
                    //    log.ApproveID = manage.HRAdminID;
                    //    log.ApproveName = manage.HRAdminName;
                    //    log.ApproveType = 1;
                    //    log.ApproveState = 0;
                    //    log.ApproveUpUserID = 0;
                    //    log.IsLastApprove = 0;
                    //    log.Deleted = false;
                    //    log.CreateTime = DateTime.Now;
                    //    log.UpdateTime = DateTime.Now;
                    //    log.OperateorID = model.UserId.Value;
                    //    new ApproveLogDataProvider().Add(log, trans.Connection, trans);
                    //}
                    trans.Commit();
                    retvalue= true;
                }
                catch
                {
                    trans.Rollback();
                    retvalue= false;
                }
            }
            //send mail
                   
            //if (tList != null && tList.Count > 0)
            //{
            //    ESP.Administrative.Entity.OperationAuditManageInfo operation = (new ESP.Administrative.BusinessLogic.OperationAuditManageManager()).GetOperationAuditModelByUserID(model.UserId.Value);
            //    string hr = new ESP.Compatible.Employee(operation.HRAdminID).EMail;

            //    if (!string.IsNullOrEmpty(hr))
            //    {
            //        string body = "<br><br>" + model.UserName + "提交了" + model.CurrentDate.Value.ToString("yyyy-MM-dd") + "的Time Sheet(" + tList[0].CategoryName+ "),等待您的审批。";
                    
            //        MailAddress[] recipientAddress = { new MailAddress(hr) };
            //        ESP.Mail.MailManager.Send("Time Sheet审批", body, true, null, recipientAddress, null, null, null);
            //    }
            //}
            //string titles =string.Empty;
            //if (eventhours>= 16)
            //{
            //    titles="事假";
            //}
            //else if(annualhours>=16)
            //{
            //   titles="年假";
            //}
            //if(eventhours >= 16 || annualhours>=16)
            //{
            //    ESP.Administrative.Entity.OperationAuditManageInfo operation = (new ESP.Administrative.BusinessLogic.OperationAuditManageManager()).GetOperationAuditModelByUserID(model.UserId.Value);
            //    string hr = new ESP.Compatible.Employee(operation.HRAdminID).EMail;

            //    if (!string.IsNullOrEmpty(hr))
            //    {
            //        string body = "<br><br>" + model.UserName + "提交了" + model.CurrentDate.Value.ToString("yyyy-MM-dd") + "的Time Sheet(" + titles + ")。共计" + eventhours+"小时";

            //        MailAddress[] recipientAddress = { new MailAddress(hr) };
            //        ESP.Mail.MailManager.Send("Time Sheet年假、事假提醒", body, true, null, recipientAddress, null, null, null);
            //    }
            //}


            return retvalue;
        }

        /// <summary>
        /// 撤销timesheet
        /// </summary>
        /// <param name="commitId"></param>
        /// <returns></returns>
        public static bool CancelAndUpdateMonthStat(TimeSheetCommitInfo model)
        {
            ESP.Administrative.Entity.OperationAuditManageInfo manage = new ESP.Administrative.BusinessLogic.OperationAuditManageManager().GetOperationAuditModelByUserID(model.UserId.Value);

            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    model.Status = (int)TimeSheetCommitStatus.Save;
                    Provider.Update(model, trans);

                   // MonthStatInfo statInfo = monthStatManager.GetNewMonthStatInfo(model.CurrentDate.Value, model.UserId.Value, model.UserCode, model.UserName, trans);
                    //if (statInfo.ID != 0)
                    //    monthStatProvider.Update(statInfo, trans);
                    //else
                    //{
                    //    int approvedateid = monthStatProvider.Add(statInfo, trans);

                    //    ESP.Administrative.Entity.ApproveLogInfo auditlog = new ApproveLogInfo();
                    //    auditlog.ApproveDateID = approvedateid;
                    //    auditlog.ApproveID = manage.HRAdminID;
                    //    auditlog.ApproveName = manage.HRAdminName;
                    //    auditlog.ApproveType = 1;
                    //    auditlog.ApproveState = 0;
                    //    auditlog.ApproveUpUserID = 0;
                    //    auditlog.IsLastApprove = 0;
                    //    auditlog.Deleted = false;
                    //    auditlog.CreateTime = DateTime.Now;
                    //    auditlog.UpdateTime = DateTime.Now;
                    //    auditlog.OperateorID = model.UserId.Value;
                    //    new ApproveLogDataProvider().Add(auditlog, trans.Connection, trans);

                    //}
                    TimeSheetLogInfo log = new TimeSheetLogInfo();
                    log.AuditDate = DateTime.Now;
                    log.AuditorId = model.UserId.Value;
                    log.AuditorName = model.UserName;
                    log.CommitId = model.Id;
                    log.Status = model.Status.Value;
                    log.Suggestion = "撤销" + model.CurrentDate.Value.ToString("yyyy年MM月dd日") + "TimeSheet";

                    TimeSheetLogManager.Add(log, trans);

                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
            }
        }


        public static bool Delete(int Id)
        {
            return Provider.Delete(Id);
        }

        public static bool DeleteByLongHoliday(int Id)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    List<TimeSheetInfo> tsList = TimeSheetManager.GetList(" commitId=" + Id);
                    if (tsList != null && tsList.Count > 0)
                    {
                        TimeSheetInfo timesheetModel = tsList[0];
                        TimeSheetManager.RollBackOrDelInfo(timesheetModel.Id, trans);
                        timeSheetProvider.Delete(timesheetModel.Id, trans);
                    }
                    Provider.Delete(Id, trans);

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return false;
                }
            }
        }

        public static bool DeleteList(string Idlist)
        {
            return Provider.DeleteList(Idlist);
        }

        public static TimeSheetCommitInfo GetModel(int Id)
        {
            return Provider.GetModel(Id, null);
        }

        public static int ValidTimeSheetRecords(int UserId, DateTime weekbegin)
        {
            return Provider.ValidTimeSheetRecords(UserId, weekbegin);
        }

        public static TimeSheetCommitInfo GetModel(int Id, SqlTransaction trans)
        {
            return Provider.GetModel(Id, trans);
        }

        public static TimeSheetCommitInfo GetModel(int UserId, string CurrentDate)
        {
            return Provider.GetModelByUserId(UserId, CurrentDate);
        }

        public static List<TimeSheetCommitInfo> GetList(string strWhere)
        {
            return Provider.GetList(strWhere);
        }

        public static List<TimeSheetCommitInfo> GetList(int Top, string strWhere, string filedOrder)
        {
            return Provider.GetList(Top, strWhere, filedOrder);
        }

        public static int GetRecordCount(string strWhere)
        {
            return Provider.GetRecordCount(strWhere);
        }

        public static List<TimeSheetCommitInfo> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return Provider.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public static TimeSheetCommitInfo GetWorkTime(TimeSheetCommitInfo model)
        {
            return GetWorkTime(model, null);
        }

        public static TimeSheetCommitInfo GetWorkTime(TimeSheetCommitInfo model, SqlTransaction trans)
        {
            TimeSheetCommitInfo returnModel = Provider.GetModelByUserId(model.UserId ?? 0, model.CurrentDate.Value.ToString("yyyy-MM-dd"), trans);
            if (returnModel != null)
                return returnModel;

            //获取人员正常工作时间
            CommuterTimeManager ctManager = new CommuterTimeManager();
            CommuterTimeInfo ctInfo = ctManager.GetCommuterTimes(ctManager.GetCommuterTimeByUserId(model.UserId ?? 0), model.CurrentDate.Value);

            //获取节假日时间
            HolidaysInfoManager hoManager = new HolidaysInfoManager();
            HolidaysInfo hoInfo = hoManager.GetHolideysInfoByDatetime(model.CurrentDate.Value);

            if (hoInfo != null)
            {
                //判断是否为整天节假日。全天节假日GoWorkTime、OffWorkTime为NULL，不赋值
                if ((hoInfo.EndDate - hoInfo.HoliDate).Hours != 9)
                {
                    //非全天
                    if (hoInfo.HoliDate.Ticks < DateTime.Parse(hoInfo.HoliDate.ToString("yyyy-MM-dd") + " 12:30:00").Ticks)
                    {
                        //上午放假
                        model.GoWorkTime = hoInfo.EndDate;
                        model.OffWorkTime = ctInfo.OffWorkTime;
                    }
                    else
                    {
                        //下午放假
                        model.GoWorkTime = ctInfo.GoWorkTime;
                        model.OffWorkTime = hoInfo.HoliDate;
                    }
                }

            }
            else
            {
                //当前日期 + 上班起始结束时间
                model.GoWorkTime = DateTime.Parse(model.CurrentDate.Value.ToString("yyyy-MM-dd") + " " + ctInfo.GoWorkTime.ToString("HH:mm:ss"));
                model.OffWorkTime = DateTime.Parse(model.CurrentDate.Value.ToString("yyyy-MM-dd") + " " + ctInfo.OffWorkTime.ToString("HH:mm:ss"));
            }

            int newId = 0;


            if (trans == null)
                newId = Provider.Add(model);
            else
                newId = Provider.Add(model, trans);
            model.Id = newId;
            return model;
        }

        public static TimeSheetCommitInfo GetTimeSheetInfoForDay(int userId, DateTime day)
        {
            return Provider.GetTimeSheetInfoForDay(userId, day);
        }

        public static DataTable GetUserListByManagerId(int ManagerId, string BeginDate, string EndDate, string keys)
        {
            return Provider.GetUserListByManagerId(ManagerId, BeginDate, EndDate, keys);
        }

        public static DataTable GetUserListByHrAdmin(string BeginDate, string EndDate, string keys)
        {
            return Provider.GetUserListByHrAdmin(BeginDate, EndDate, keys);
        }

        public static DataTable GetWaitAuditTimeSheetList(int LeaderId, string keys)
        {
            return Provider.GetWaitAuditTimeSheetList(LeaderId, keys);
        }

        public static DataTable GetWaitHRAuditTimeSheetList(int hrId, string keys)
        {
            return Provider.GetWaitHRAuditTimeSheetList(hrId, keys);
        }

        private static void writeLog(string s)
        {
            using (System.IO.StreamWriter log = new System.IO.StreamWriter(HttpRuntime.AppDomainAppPath + "\\timelog.txt", true))
            {
                log.WriteLine(s);
                log.Flush();
            }
        }

        public static List<TimeSheetCommitInfo> GetLongHolidayList(string terms)
        {
            return Provider.GetLongHolidayList(terms);
        }

        public static bool DeleteLongHoliday(string serialNo)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //还原mothstat
                    List<TimeSheetCommitInfo> list = TimeSheetCommitManager.GetList(" serialNo = '" + serialNo + "'");
                    List<string> date = new List<string>();
                    foreach (var model in list)
                    {
                        if (!date.Contains(model.CurrentDate.Value.ToString("yyyy-MM")))
                        {
                            //MonthStatInfo statInfo = monthStatManager.GetNewMonthStatInfo(model.CurrentDate.Value, model.UserId.Value, model.UserCode, model.UserName, trans);
                            //if (statInfo.ID != 0)
                            //    monthStatProvider.Update(statInfo, trans);
                            //else
                            //{
                            //    int approvedateid = monthStatProvider.Add(statInfo, trans);
                            //}
                            date.Add(model.CurrentDate.Value.ToString("yyyy-MM"));
                        }
                       // List<TimeSheetInfo> tsList = timeSheetProvider.GetList(" commitid=" + model.Id);
                        //foreach (var t in tsList)
                        //{
                            //调休、年假、奖励假还原
                            //TimeSheetManager.RollBackOrDelInfo(t.Id, trans);
                        //}
                    }

                    //删除长假的timesheet数据
                    timeSheetProvider.DeleteLongHoliday(serialNo, trans);

                    //还原commit为正常数据
                    Provider.RollbackLongHoliday(serialNo, trans);

                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 新的长假申请
        /// </summary>
        /// <param name="model"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static string InsertLongHoliday(TimeSheetCommitInfo model, decimal totalHours)
        {

            string waithr = "";
            string serialNo = DateTime.Now.ToString("yyyyMMddhhmmss");
            DateTime bdate = new DateTime(model.BeginDate.Value.Year, model.BeginDate.Value.Month, 1);
            DateTime edate = bdate.AddMonths(1);
            List<TimeSheetInfo> sicklist = timeSheetProvider.GetList(" categoryId=" + (int)TimeSheetCategoryIds.SickLeaveId + " and commitid in(select id from ad_timesheetcommit where status in(2,3,4) and  userid=" + model.UserId + " and (currentdate between '" + bdate.ToString("yyyy-MM-dd") + "' and '" + edate.ToString("yyyy-MM-dd") + "'))");
            List<TimeSheetInfo> anuallist = timeSheetProvider.GetList(" (categoryId=" + (int)TimeSheetCategoryIds.AnnualLeaveId + " or categoryId=" + (int)TimeSheetCategoryIds.AffairLeaveId + ") and commitid in(select id from ad_timesheetcommit where status in(2,3,4) and  userid=" + model.UserId + " and (currentdate between '" + bdate.ToString("yyyy-MM-dd") + "' and '" + edate.ToString("yyyy-MM-dd") + "'))");

            decimal sickhours = sicklist.Sum(x => x.Hours);
            decimal eventhours = anuallist.Where(x => x.CategoryId == (int)TimeSheetCategoryIds.AffairLeaveId).Sum(x => x.Hours);
            decimal annualhours = anuallist.Where(x => x.CategoryId == (int)TimeSheetCategoryIds.AnnualLeaveId).Sum(x => x.Hours);


            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    decimal sjHours = totalHours;
                    for (DateTime bg = model.BeginDate.Value; bg <= model.EndDate.Value; )
                    {
                        //婚假、产假含法定假日
                        if (model.CategoryId != (int)TimeSheetCategoryIds.MaternityLeaveId && model.CategoryId != (int)TimeSheetCategoryIds.MarriageLeaveId)
                        {
                            HolidaysInfo holiday = new HolidaysInfoManager().GetHolideysInfoByDatetime(bg);
                            if (holiday != null && holiday.Type != (int)HolidayType.halfHoliday)
                            {
                                bg = bg.AddDays(1);
                                continue;
                            }
                        }

                        //生成新的commit数据
                        TimeSheetCommitInfo commitModel = Provider.GetModelByUserId(model.UserId ?? 0, bg.ToString("yyyy-MM-dd"), trans);
                        int commitId = 0;
                        if (commitModel == null)
                        {
                            //commit数据不存在时
                            commitModel = new TimeSheetCommitInfo();

                            WeekSettingInfo week = ESP.Administrative.BusinessLogic.WeekSettingManager.GetWeekByDate(bg);
                            commitModel.WeekId = week.Id;
                            commitModel.UserId = model.UserId;
                            commitModel.UserCode = model.UserCode;
                            commitModel.UserName = model.UserName;
                            commitModel.DepartmentId = model.DepartmentId;
                            commitModel.DepartmentName = model.DepartmentName;
                            commitModel.CreateDate = model.CreateDate;
                            commitModel.IP = model.IP;
                            commitModel.CurrentDate = bg;
                            commitModel.CommitType = ESP.Administrative.Common.TimeSheetCommitType.Holiday.ToString();
                            if (model.CategoryId == (int)TimeSheetCategoryIds.MaternityLeaveId || model.CategoryId == (int)TimeSheetCategoryIds.MarriageLeaveId || model.CategoryId == (int)TimeSheetCategoryIds.PrenatalCheckId || model.CategoryId == (int)TimeSheetCategoryIds.FuneralLeaveId)
                            {
                                commitModel.Status = (int)ESP.Administrative.Common.TimeSheetCommitStatus.Commit;
                                waithr = "hr";
                            }
                            else if (model.CategoryId == (int)TimeSheetCategoryIds.SickLeaveId)
                            {
                                if (model.BeginDate == model.EndDate)
                                {
                                    sickhours += totalHours;
                                }
                                else
                                sickhours += 8;
                                if (sickhours >= 16)
                                {
                                    commitModel.Status = (int)ESP.Administrative.Common.TimeSheetCommitStatus.Commit;
                                    waithr = "hr";
                                }
                                else
                                {
                                    commitModel.Status = (int)ESP.Administrative.Common.TimeSheetCommitStatus.Commit;
                                }
                            }
                            else if (model.CategoryId == (int)TimeSheetCategoryIds.AnnualLeaveId)
                            {
                                commitModel.Status = (int)ESP.Administrative.Common.TimeSheetCommitStatus.Commit;
                                if (model.BeginDate == model.EndDate)
                                {
                                    annualhours += totalHours;
                                }
                                else
                                    annualhours += 8;
                                if (annualhours >= 16)
                                    waithr = "annual";
                            }
                            else if (model.CategoryId == (int)TimeSheetCategoryIds.AffairLeaveId)
                            {
                                commitModel.Status = (int)ESP.Administrative.Common.TimeSheetCommitStatus.Commit;
                                if (model.BeginDate == model.EndDate)
                                {
                                    eventhours += totalHours;
                                }
                                else
                                     eventhours += 8;
                                if (eventhours >= 16)
                                    waithr = "affair";
                            }
                            else
                            {
                                commitModel.Status = (int)ESP.Administrative.Common.TimeSheetCommitStatus.Commit;
                            }

                            //获得上下班时间
                            DateTime goWorkTime = new DateTime(1900, 1, 1);
                            DateTime offWorkTime = new DateTime(1900, 1, 1);
                            var clockManager = new ESP.Administrative.BusinessLogic.ClockInManager();
                            clockManager.GetAttendanceTime(model.UserId ?? 0, bg, out goWorkTime, out offWorkTime);
                            if (goWorkTime.Year != 1900)
                            {
                                commitModel.CurrentGoWorkTime = goWorkTime;
                            }
                            if (offWorkTime.Year != 1900)
                            {
                                commitModel.CurrentOffWorkTime = offWorkTime;
                            }

                            commitModel.CategoryId = model.CategoryId;
                            commitModel.CategoryName = model.CategoryName;
                            commitModel.BeginDate = model.BeginDate;
                            commitModel.EndDate = model.EndDate;
                            commitModel.SerialNo = serialNo;

                            TimeSheetCommitInfo newCommitModel = GetWorkTime(commitModel, trans);
                            commitId = newCommitModel.Id;
                        }
                        else
                        {
                            commitModel.CommitType = ESP.Administrative.Common.TimeSheetCommitType.Holiday.ToString();
                            if (model.CategoryId == (int)TimeSheetCategoryIds.MaternityLeaveId || model.CategoryId == (int)TimeSheetCategoryIds.MarriageLeaveId || model.CategoryId == (int)TimeSheetCategoryIds.SickLeaveId || model.CategoryId == (int)TimeSheetCategoryIds.FuneralLeaveId)
                            {
                                commitModel.Status = (int)ESP.Administrative.Common.TimeSheetCommitStatus.Commit;
                                waithr = "hr";
                            }
                            else if (model.CategoryId == (int)TimeSheetCategoryIds.SickLeaveId)
                            {
                                if (model.BeginDate == model.EndDate)
                                {
                                    sickhours += totalHours;
                                }
                                else
                                sickhours += 8;
                                if (sickhours >= 16)
                                {
                                    commitModel.Status = (int)ESP.Administrative.Common.TimeSheetCommitStatus.Commit;
                                    waithr = "hr";
                                }
                                else
                                {
                                    commitModel.Status = (int)ESP.Administrative.Common.TimeSheetCommitStatus.Commit;
                                }
                            }
                            else if (model.CategoryId == (int)TimeSheetCategoryIds.AnnualLeaveId)
                            {
                                commitModel.Status = (int)ESP.Administrative.Common.TimeSheetCommitStatus.Commit;
                                if (model.BeginDate == model.EndDate)
                                {
                                    annualhours += totalHours;
                                }
                                else
                                annualhours += 8;
                                if (annualhours >= 16)
                                    waithr = "annual";
                            }
                            else if (model.CategoryId == (int)TimeSheetCategoryIds.AffairLeaveId)
                            {
                                commitModel.Status = (int)ESP.Administrative.Common.TimeSheetCommitStatus.Commit;
                                if (model.BeginDate == model.EndDate)
                                {
                                    eventhours += totalHours;
                                }
                                else
                                eventhours += 8;
                                if (eventhours >= 16)
                                    waithr = "affair";
                            }
                            else
                            {
                                commitModel.Status = (int)ESP.Administrative.Common.TimeSheetCommitStatus.Commit;
                            }
                            commitModel.CategoryId = model.CategoryId;
                            commitModel.CategoryName = model.CategoryName;
                            commitModel.BeginDate = model.BeginDate;
                            commitModel.EndDate = model.EndDate;
                            commitModel.SerialNo = serialNo;
                            Provider.Update(commitModel, trans);

                            commitId = commitModel.Id;
                        }


                        //生成新的timesheet数据
                        TimeSheetInfo timesheetModel = new TimeSheetInfo();
                        timesheetModel.CategoryId = model.CategoryId.Value;
                        timesheetModel.CategoryName = model.CategoryName;
                        timesheetModel.CommitId = commitId;
                        timesheetModel.CreateDate = model.CreateDate.Value;
                        if (totalHours<8)
                            timesheetModel.Hours = totalHours;
                        else
                        timesheetModel.Hours = 8;
                        timesheetModel.IP = model.IP;
                        timesheetModel.SubmitDate = model.CommitDate.Value;
                        timesheetModel.TypeId = (int)ESP.Administrative.Common.TimeSheetType.Holiday;
                        timesheetModel.UserId = model.UserId.Value;
                        timesheetModel.UserName = model.UserName;
                        timesheetModel.WorkItem = model.Description;

                        int tsId = timeSheetProvider.Add(timesheetModel, trans);
                        timesheetModel.Id = tsId;

                        if (model.BeginDate == model.EndDate)
                        {
                            sjHours=sjHours- totalHours;
                        }
                        else
                        sjHours = sjHours - 8;
                        TimeSheetManager.CheckTimeSheetInfo(timesheetModel, totalHours, sjHours, trans);

                        //MonthStatInfo statInfo = monthStatManager.GetNewMonthStatInfo(commitModel.CurrentDate.Value, commitModel.UserId.Value, commitModel.UserCode, commitModel.UserName, trans);
                        //if (statInfo.ID != 0)
                        //    monthStatProvider.Update(statInfo, trans);
                        //else
                        //{
                        //    int approvedateid = monthStatProvider.Add(statInfo, trans);
                        //}

                        bg = bg.AddDays(1);
                    }
                    trans.Commit();
                    return waithr;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return ex.Message;
                }
            }
        }

        public static bool SubmitAudit(string commitIds, string serialNos, int status, TimeSheetLogInfo log)
        {
            if (string.IsNullOrEmpty(serialNos))
            {
                serialNos = "";
            }
            if (string.IsNullOrEmpty(commitIds))
            {
                commitIds = "";
            }

            bool returnValue = false;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    if (!string.IsNullOrEmpty(serialNos))
                    {
                        List<TimeSheetCommitInfo> list = Provider.GetList(" serialno in ('" + serialNos.Replace(",", "','") + "')");
                        foreach (var v in list)
                        {
                            commitIds += "," + v.Id;
                        }
                    }
                    foreach (string id in commitIds.Trim(',').Split(','))
                    {
                        TimeSheetCommitInfo model = GetModel(int.Parse(id));
                        if (model.Status != (int)ESP.Administrative.Common.TimeSheetCommitStatus.Commit && model.Status != (int)ESP.Administrative.Common.TimeSheetCommitStatus.WaitHR) //如果不是提交状态，不做操作
                            continue;
                        model.Status = status;
                        if (status == (int)Common.TimeSheetCommitStatus.Passed)
                        {
                            if (model.CommitType != ESP.Administrative.Common.TimeSheetCommitType.Holiday.ToString())
                            {

                                HolidaysInfo holiday = new HolidaysInfoManager().GetHolideysInfoByDatetime(model.CurrentDate.Value);
                                if (holiday != null && holiday.Type != (int)HolidayType.halfHoliday)
                                {
                                    //如果为公休假（周末）,OT时间 *　设置倍数　
                                    int sabbaticalLeaveBS = int.Parse(System.Configuration.ConfigurationManager.AppSettings["SabbaticalLeaveBS"].ToString()); //Configuration.ConfigurationManager.SafeAppSettings["SabbaticalLeaveBS"].ToString());
                                    if (holiday.Type == (int)HolidayType.SabbaticalLeave)
                                    {
                                        List<TimeSheetInfo> tsList = new TimeSheetDataProvider().GetList("commitId=" + id);
                                        decimal overwork = tsList.Sum(x => x.Hours) * sabbaticalLeaveBS;
                                        if (overwork > 8)
                                            model.OverWorkHours = 8;
                                        else
                                            model.OverWorkHours = overwork;
                                    }

                                }
                            }
                        }
                        model.AuditDate = DateTime.Now;
                        Provider.Update(model, trans);

                        log.Id = 0;
                        log.CommitId = int.Parse(id);
                        log.Status = status;
                        new TimeSheetLogDataProvider().Add(log, trans);

                    }

                    trans.Commit();
                    returnValue = true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 提交周TimeSheet
        /// </summary>
        public static bool SubmitWeek(int userId, string userCode, string userName, string beginDate, string endDate)
        {
            ESP.Administrative.Entity.OperationAuditManageInfo manage = new ESP.Administrative.BusinessLogic.OperationAuditManageManager().GetOperationAuditModelByUserID(userId);
            bool retval = false;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    Provider.SubmitWeek(userId, beginDate, endDate, trans);
                    //MonthStatInfo statInfo = monthStatManager.GetNewMonthStatInfo(DateTime.Parse(beginDate), userId, userCode, userName, trans);
                    //if (statInfo.ID != 0)
                    //    monthStatProvider.Update(statInfo, trans);
                    //else
                    //{

                    //    int approvedateid = monthStatProvider.Add(statInfo, trans);
                    //    ESP.Administrative.Entity.ApproveLogInfo log = new ApproveLogInfo();
                    //    log.ApproveDateID = approvedateid;
                    //    log.ApproveID = manage.HRAdminID;
                    //    log.ApproveName = manage.HRAdminName;
                    //    log.ApproveType = 1;
                    //    log.ApproveState = 0;
                    //    log.ApproveUpUserID = 0;
                    //    log.IsLastApprove = 0;
                    //    log.Deleted = false;
                    //    log.CreateTime = DateTime.Now;
                    //    log.UpdateTime = DateTime.Now;
                    //    log.OperateorID = userId;
                    //    new ApproveLogDataProvider().Add(log, trans.Connection, trans);
                    //}
                    trans.Commit();

                    retval = true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message + ex.StackTrace);

                }
            }
            //send mail
            //int status = (int)ESP.Administrative.Common.TimeSheetCommitStatus.WaitHR;
            //List<ESP.Administrative.Entity.TimeSheetCommitInfo> commitlist = ESP.Administrative.BusinessLogic.TimeSheetCommitManager.GetList(" userid =" + userId + " and (currentdate between '" + beginDate + "' and '" + endDate + "') and status=" + status);
            //if (commitlist != null && commitlist.Count > 0)
            //{
            //    string email = new ESP.Compatible.Employee(manage.HRAdminID).EMail;
            //    string body = "<br><br>" + commitlist[0].UserName + "按周提交了" + beginDate + "至" + endDate + "的Time Sheet,其中有相关事由等待您的审批。";

            //    MailAddress[] recipientAddress = { new MailAddress(email) };
            //    ESP.Mail.MailManager.Send("Time Sheet审批", body, true, null, recipientAddress, null, null, null);
            //}


            return retval;

        }

        /// <summary>
        /// 获得有效剩余调休时间
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static decimal GetUserOverWorkRemain(int UserId)
        {
            return Provider.GetUserOverWorkRemain(UserId);
        }

        public static decimal GetRecentUserOverWorkRemain(int UserId)
        {
            return Provider.GetRecentUserOverWorkRemain(UserId);
        }

        public static DataTable GetOTList(string begindate, string enddate, string userids)
        {
            return Provider.GetOTList(begindate, enddate, userids);
        }

        public static decimal GetUserSumTimeSheetHours(ESP.Administrative.Entity.TimeSheetCommitInfo commitModel)
        {
            return Provider.GetUserSumTimeSheetHours(commitModel);
        }
    }
}
