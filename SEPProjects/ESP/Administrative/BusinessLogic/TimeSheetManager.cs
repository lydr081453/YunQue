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

namespace ESP.Administrative.BusinessLogic
{
    public class TimeSheetManager
    {
        public readonly static TimeSheetDataProvider Provider = new TimeSheetDataProvider();
        public readonly static MonthStatManager monthStatManager = new MonthStatManager();
        public readonly static MonthStatDataProvider monthStatProvider = new MonthStatDataProvider();
        public readonly static OverWorkRecordsDataProvicer overWorkRecordsDataProvicer = new OverWorkRecordsDataProvicer();
        public readonly static ALAndRLManager alManager = new ALAndRLManager();


        private static int offTuneId = (int)TimeSheetCategoryIds.OffTuneId;//调休ID
        private static int incentiveId = (int)TimeSheetCategoryIds.IncentiveId;//奖励假ID
        private static int annualLeaveId = (int)TimeSheetCategoryIds.AnnualLeaveId; //年假ID

        public static int Add(TimeSheetInfo model)
        {
            return Provider.Add(model);
        }

        public static int Add(TimeSheetInfo model, out string errorMsg)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int returnId = Provider.Add(model, trans);
                    model.Id = returnId;
                    CheckTimeSheetInfo(model, trans);
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
        public static void CheckTimeSheetInfo(TimeSheetInfo model, SqlTransaction trans)
        {
            CheckTimeSheetInfo(model, 0,0, trans);
        }
        /// <summary>
        /// 插入调休记录
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        public static void CheckTimeSheetInfo(TimeSheetInfo model,decimal totalHours,decimal sjHours, SqlTransaction trans)
        {
            
            if (model.CategoryId == offTuneId)
            {
                ESP.Administrative.Entity.UserAttBasicInfo basicModel = (new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager()).GetModelByUserid(model.UserId);
                if (basicModel.AttendanceType == 3)
                {
                    throw new Exception("您对应的职务级别不在调休设置范围！");
                }
                else
                {
                    if (totalHours % 4 != 0)
                    {
                        throw new Exception("您申请的调休时间不符合要求（半天4小时，一天8小时）！");
                    }
                    string terms = " userId=" + model.UserId + " and status=" + (int)Common.TimeSheetCommitStatus.Passed + " and auditdate >= '" + DateTime.Now.AddMonths(-1).AddDays(-1).ToString("yyyy-MM-dd") + "' and OverWorkRemain > 0 ";
                    List<TimeSheetCommitInfo> overWorkList = new TimeSheetCommitDataProvider().GetList(terms, trans).OrderBy(x => x.CurrentDate.Value).ToList();
                    //if (totalHours != 0 && totalHours >8)
                    //{
                    //    totalHours = totalHours == 0 ? model.Hours : totalHours;
                    //    if (totalHours > (overWorkList.Sum(x => x.OverWorkRemain) + (totalHours - 8 - sjHours)))
                    //        throw new Exception("您申请的调休时间（" + (totalHours == 0 ? model.Hours : totalHours) + "小时）大于剩余调休时间（" + overWorkList.Sum(x => x.OverWorkRemain).ToString("0.##") + "小时）！");
                    //}
                    //else
                    //{
                    //    if (model.Hours > (overWorkList.Sum(x => x.OverWorkRemain)))
                    //        throw new Exception("您申请的调休时间（" + model.Hours + "小时）大于剩余调休时间（" + overWorkList.Sum(x => x.OverWorkRemain).ToString("0.##") + "小时）！");
                    //}
                    decimal offTuneHours = model.Hours;
                    foreach (TimeSheetCommitInfo t in overWorkList)
                    {
                        if (offTuneHours == 0)
                            break;
                        //插入新的调休记录
                        OverWorkRecordsInfo rInfo = new OverWorkRecordsInfo();
                        rInfo.OverWorkId = t.Id;
                        rInfo.TakeOffId = model.Id;
                        rInfo.Hours = offTuneHours >= t.OverWorkRemain ? t.OverWorkRemain : offTuneHours;
                        rInfo.Type = ESP.Administrative.Common.OverWorkRecords_Types.OffTune;
                        overWorkRecordsDataProvicer.Add(rInfo, trans);

                        offTuneHours = offTuneHours - rInfo.Hours;
                    }
                }
            }
            else if (model.CategoryId == (int)TimeSheetCategoryIds.LateExclude)
            {
                if (model.Hours != 4 && model.Hours != 1)
                {
                    throw new Exception("晚到申请时间不符合要求！");
                }
            }
            else if (model.CategoryId == annualLeaveId)
            {
                //年假
                if (model.Hours != 4 && model.Hours != 8)
                {
                    throw new Exception("年假申请时间最少为半天(4小时)，1天为8小时！");
                }
                //ALAndRLInfo al1Info = alManager.GetALAndRLModel(model.UserId, DateTime.Now.Year, (int)AAndRLeaveType.AnnualType, trans);
                //if (al1Info != null && (al1Info.RemainingNumber * 8) >= (totalHours == 0 ? model.Hours : totalHours))
                //{
                    //if (sjHours == 0)
                    //{
                    //    decimal remain = (al1Info.RemainingNumber * 8 - (totalHours == 0 ? model.Hours : totalHours)) / 8;
                    //    al1Info.RemainingNumber = decimal.Parse(Math.Floor(remain).ToString() + "." + ((al1Info.RemainingNumber * 8 - (totalHours == 0 ? model.Hours : totalHours)) % 8 >= 4 ? "5" : "0"));
                    //    alManager.Update(al1Info, trans.Connection, trans);
                    //}

                    //OverWorkRecordsInfo rInfo = new OverWorkRecordsInfo();
                    //rInfo.OverWorkId = al1Info.ID;
                    //rInfo.TakeOffId = model.Id;
                    //rInfo.Hours = model.Hours;
                    //rInfo.Type = ESP.Administrative.Common.OverWorkRecords_Types.AnnualLeave;
                    //overWorkRecordsDataProvicer.Add(rInfo, trans);
                //}
                //else
                //{
                //    throw new Exception("您申请的年假时间（" + (totalHours == 0 ? model.Hours : totalHours) + "小时）大于剩余年假时间（" + (al1Info == null ? "0" : (al1Info.RemainingNumber * 8).ToString("0.##")) + "小时）！");
                //}
            }
            else if (model.CategoryId == incentiveId)
            {
                //奖励假
                //ALAndRLInfo al2Info = alManager.GetALAndRLModel(model.UserId, DateTime.Now.Year, (int)AAndRLeaveType.RewardType, trans);
                //if (al2Info != null && (al2Info.RemainingNumber * 8) == (totalHours == 0 ? model.Hours : totalHours))
                //{
                    //if (sjHours == 0)
                    //{
                    //    al2Info.RemainingNumber = 0;
                    //    alManager.Update(al2Info, trans.Connection, trans);
                    //}

                    //OverWorkRecordsInfo rInfo = new OverWorkRecordsInfo();
                    //rInfo.OverWorkId = al2Info.ID;
                    //rInfo.TakeOffId = model.Id;
                    //rInfo.Hours = model.Hours;
                    //rInfo.Type = ESP.Administrative.Common.OverWorkRecords_Types.Incentive;
                    //overWorkRecordsDataProvicer.Add(rInfo, trans);
                //}
                //else
                //{
                //    throw new Exception("您申请的福利假时间（" + (totalHours == 0 ? model.Hours : totalHours) + "小时）不等于剩余奖励假时间（" + (al2Info == null ? "0" : (al2Info.RemainingNumber * 8).ToString("0.##")) + "小时）！");
                //}
            }
        }

        public static bool  Update(TimeSheetInfo model, out string errorMsg)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    RollBackOrDelInfo(model.Id, trans);
                    Provider.Update(model, trans);
                    CheckTimeSheetInfo(model, trans);
                    trans.Commit();
                    errorMsg = "";
                    return true;
                }
                catch(Exception ex)
                {
                    trans.Rollback();
                    errorMsg = ex.Message;
                    return false;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tsModel"></param>
        /// <returns></returns>
        public static bool UpdateChecked(TimeSheetInfo tsModel)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //Provider.Update(tsModel, trans);

                    TimeSheetCommitInfo model = TimeSheetCommitManager.GetModel(tsModel.CommitId);
                    MonthStatInfo statInfo = monthStatManager.GetNewMonthStatInfo(model.CurrentDate.Value, model.UserId.Value, model.UserCode, model.UserName,trans);
                    if (statInfo.ID != 0)
                        monthStatProvider.Update(statInfo, trans);
                    else
                        monthStatProvider.Add(statInfo, trans);
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
        /// 还原剩余年假、福利假 或删除调休记录
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="trans"></param>
        public static void RollBackOrDelInfo(int TakeOffId, SqlTransaction trans)
        {
            TimeSheetInfo tsModel = TimeSheetManager.GetModel(TakeOffId);
            using (System.IO.StreamWriter log = new System.IO.StreamWriter(HttpRuntime.AppDomainAppPath + "\\timesheetlog.txt", true))
            {
                log.WriteLine("tsModel.CategoryId = " + tsModel.CategoryId);
                log.Flush();
            }
            if (tsModel.CategoryId == (int)TimeSheetCategoryIds.AnnualLeaveId || tsModel.CategoryId == (int)TimeSheetCategoryIds.IncentiveId)
            {
                //年假、福利假
                TimeSheetCommitInfo tcModel = TimeSheetCommitManager.GetModel(tsModel.CommitId,trans);
                int leaveType = (tsModel.CategoryId == (int)TimeSheetCategoryIds.AnnualLeaveId) ? (int)AAndRLeaveType.AnnualType : (int)AAndRLeaveType.RewardType;
                ALAndRLInfo alInfo = alManager.GetALAndRLModel(tsModel.UserId, tcModel.CurrentDate.Value.Year, leaveType,trans);

                List<OverWorkRecordsInfo> recList = overWorkRecordsDataProvicer.GetList(tsModel.Id,trans);

                if (recList != null && recList.Count > 0)
                {
                    //还原剩余年假、福利假
                    decimal remain = (alInfo.RemainingNumber * 8 + recList[0].Hours) / 8;
                    if (leaveType == (int)AAndRLeaveType.RewardType)
                        alInfo.RemainingNumber = alInfo.LeaveNumber;
                    else
                        alInfo.RemainingNumber = decimal.Parse(Math.Floor(remain).ToString() + "." + ((alInfo.RemainingNumber * 8 + recList[0].Hours) % 8 >= 4 ? "5" : "0"));
                    alInfo.UpdateTime = DateTime.Now;
                    alManager.Update(alInfo, trans.Connection, trans);
                }

            }
            //else if (tsModel.CategoryId == (int)TimeSheetCategoryIds.OffTuneId)
            //{
                //删除该timesheet对应的调休记录
                overWorkRecordsDataProvicer.Delete(tsModel.Id, trans);
            //}
        }

        public static bool Delete(int Id)
        {
            //return Provider.Delete(Id);
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    RollBackOrDelInfo(Id, trans);

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

        public static TimeSheetInfo GetModel(int Id)
        {
            return Provider.GetModel(Id);
        }

        public static List<TimeSheetInfo> GetList(string strWhere)
        {
            return Provider.GetList(strWhere);
        }

        public static List<TimeSheetInfo> GetList(string strWhere,SqlTransaction trans)
        {
            return Provider.GetList(strWhere,trans);
        }

        public static List<TimeSheetInfo> GetList(int Top, string strWhere, string filedOrder)
        {
            return Provider.GetList(Top, strWhere, filedOrder);
        }

        public static int GetRecordCount(string strWhere)
        {
            return Provider.GetRecordCount(strWhere);
        }

        public static List<TimeSheetInfo> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return Provider. GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

    }
}
