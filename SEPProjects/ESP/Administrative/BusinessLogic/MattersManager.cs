using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.Entity;
using ESP.Administrative.DataAccess;
using ESP.Administrative.Common;
using System.Data;
using System.Data.SqlClient;
using ESP.Framework.Entity;
using System.Net.Mail;
using System.Collections;

namespace ESP.Administrative.BusinessLogic
{
    public class MattersManager
    {
        private MattersDataProvider dal = new MattersDataProvider();
        public MattersManager()
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
        public int Add(MattersInfo model)
        {
            int returnValue = 0;
            ALAndRLManager alandrlManager = new ALAndRLManager();
            ALAndRLDataProvider alandrlDal = new ALAndRLDataProvider();
            // 获得年假信息
            ALAndRLInfo alandrlModel = null;
            ALAndRLInfo rewardModel = null;

            if (model.MatterType == Status.MattersType_Annual_Last)
            {
                alandrlModel = alandrlManager.GetALAndRLModel(model.UserID, model.BeginTime.Year - 1, (int)AAndRLeaveType.AnnualType);
                rewardModel = alandrlManager.GetALAndRLModel(model.UserID, model.BeginTime.Year - 1, (int)AAndRLeaveType.RewardType);
            }
            else
            {
                alandrlModel = alandrlManager.GetALAndRLModel(model.UserID, model.BeginTime.Year, (int)AAndRLeaveType.AnnualType);
                rewardModel = alandrlManager.GetALAndRLModel(model.UserID, model.BeginTime.Year, (int)AAndRLeaveType.RewardType);
            }

            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    if (model.MatterType == Status.MattersType_Annual || model.MatterType == Status.MattersType_Annual_Last)
                    {
                        alandrlModel.RemainingNumber -= (model.AnnualHours / Status.WorkingHours);
                        alandrlDal.Update(alandrlModel, conn, trans);

                        if (rewardModel != null)
                        {
                            rewardModel.RemainingNumber -= (model.AwardHours / Status.WorkingHours);
                        }
                        alandrlDal.Update(rewardModel, conn, trans);
                    }

                    returnValue = dal.Add(model, conn, trans);

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(MattersInfo model, ApproveLogInfo approvelog)
        {
            int returnValue = 0;
            ALAndRLManager alandrlManager = new ALAndRLManager();
            ALAndRLDataProvider alandrlDal = new ALAndRLDataProvider();
            // 获得年假信息
            ALAndRLInfo alandrlModel = null;
            ALAndRLInfo incentiveModel = null;

            if (model.MatterType == Status.MattersType_Annual_Last)
            {
                alandrlModel = alandrlManager.GetALAndRLModel(model.UserID, model.BeginTime.Year - 1, (int)AAndRLeaveType.AnnualType);
                incentiveModel = alandrlManager.GetALAndRLModel(model.UserID, model.BeginTime.Year - 1, (int)AAndRLeaveType.RewardType);
            }
            else
            {
                alandrlModel = alandrlManager.GetALAndRLModel(model.UserID, model.BeginTime.Year, (int)AAndRLeaveType.AnnualType);
                incentiveModel = alandrlManager.GetALAndRLModel(model.UserID, model.BeginTime.Year, (int)AAndRLeaveType.RewardType);
            }

            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // 更新年假剩余信息
                    if (model.MatterType == Status.MattersType_Annual || model.MatterType == Status.MattersType_Annual_Last)
                    {
                        alandrlModel.RemainingNumber -= (model.AnnualHours / Status.WorkingHours);
                        alandrlDal.Update(alandrlModel, conn, trans);

                        if (incentiveModel != null)
                        {
                            incentiveModel.RemainingNumber -= (model.AwardHours / Status.WorkingHours);
                            alandrlDal.Update(incentiveModel, conn, trans);
                        }

                    }

                    returnValue = dal.Add(model, conn, trans);

                    model.ID = returnValue;

                    if (approvelog != null)
                    {
                        approvelog.ApproveDateID = returnValue;
                        int approveid = new ApproveLogManager().Add(approvelog, conn, trans);
                        approvelog.ID = approveid;
                    }
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    returnValue = 0;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(MattersInfo model)
        {
            int returnValue = 0;
            ALAndRLManager alandrlManager = new ALAndRLManager();
            ALAndRLDataProvider alandrlDal = new ALAndRLDataProvider();
            // 获得年假信息

            ALAndRLInfo alandrlModel = null;
            ALAndRLInfo incentiveModel = null;

            if (model.MatterType == Status.MattersType_Annual_Last)
            {
                alandrlModel = alandrlManager.GetALAndRLModel(model.UserID, model.BeginTime.Year - 1, (int)AAndRLeaveType.AnnualType);
                incentiveModel = alandrlManager.GetALAndRLModel(model.UserID, model.BeginTime.Year - 1, (int)AAndRLeaveType.RewardType);
            }
            else
            {
                alandrlModel = alandrlManager.GetALAndRLModel(model.UserID, model.BeginTime.Year, (int)AAndRLeaveType.AnnualType);
                incentiveModel = alandrlManager.GetALAndRLModel(model.UserID, model.BeginTime.Year, (int)AAndRLeaveType.RewardType);
            }

            MattersInfo origMatter = GetModel(model.ID);
            decimal annualtime = origMatter.AnnualHours;
            decimal awardtime = origMatter.AwardHours;
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // 剩余年假修改
                    if (model.MatterType == Status.MattersType_Annual || model.MatterType == Status.MattersType_Annual_Last)
                    {
                        alandrlModel.RemainingNumber -= ((model.AnnualHours - annualtime) / Status.WorkingHours);
                        alandrlDal.Update(alandrlModel, conn, trans);

                        if (incentiveModel != null)
                        {
                            incentiveModel.RemainingNumber -= ((model.AwardHours - awardtime) / Status.WorkingHours);
                            alandrlDal.Update(incentiveModel, conn, trans);
                        }

                    }
                    dal.Update(model, conn, trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(MattersInfo model, SqlConnection conn, SqlTransaction trans)
        {
            dal.Update(model, conn, trans);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(MattersInfo model, LogInfo logModel)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    if (model.MatterType == Status.MattersType_OffTune)
                    {
                        returnValue = this.UpdateOffTune(model);
                    }
                    else
                    {
                        returnValue = dal.Update(model, conn, trans);
                    }

                    new LogManager().Add(logModel.Content);
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    returnValue = 0;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 更新一组数据
        /// </summary>
        public int Update(List<MattersInfo> list)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (MattersInfo model in list)
                    {
                        returnValue = dal.Update(model, conn, trans);
                    }
                    ESP.Logging.Logger.Add("Update More MattersInfo.", "AdiministrativeWeb", ESP.Logging.LogLevel.Information);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    returnValue = 0;
                    ESP.Logging.Logger.Add(ex.ToString(), "AdiministrativeWeb", ESP.Logging.LogLevel.Error, ex);
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(MattersInfo model, ApproveLogInfo approvelog)
        {
            int returnValue = 0;
            ALAndRLManager alandrlManager = new ALAndRLManager();
            ALAndRLDataProvider alandrlDal = new ALAndRLDataProvider();
            // 获得年假信息
            ALAndRLInfo alandrlModel = null;
            ALAndRLInfo incentiveModel = null;

            if (model.MatterType == Status.MattersType_Annual_Last)
            {
                alandrlModel = alandrlManager.GetALAndRLModel(model.UserID, model.BeginTime.Year - 1, (int)AAndRLeaveType.AnnualType);
                incentiveModel = alandrlManager.GetALAndRLModel(model.UserID, model.BeginTime.Year - 1, (int)AAndRLeaveType.RewardType);
            }
            else
            {
                alandrlModel = alandrlManager.GetALAndRLModel(model.UserID, model.BeginTime.Year, (int)AAndRLeaveType.AnnualType);
                incentiveModel = alandrlManager.GetALAndRLModel(model.UserID, model.BeginTime.Year , (int)AAndRLeaveType.RewardType);
            }

            // 原来保存的时候年假总数
            MattersInfo origMatter = GetModel(model.ID);
            decimal annualtime = origMatter.AnnualHours;
            decimal awardtime = origMatter.AwardHours;

            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // 更新年假信息
                    if (model.MatterType == Status.MattersType_Annual || model.MatterType == Status.MattersType_Annual_Last)
                    {
                        alandrlModel.RemainingNumber -= ((model.AnnualHours - annualtime) / Status.WorkingHours);
                        alandrlDal.Update(alandrlModel, conn, trans);
                        
                       
                        if (incentiveModel != null)
                        {
                            incentiveModel.RemainingNumber -= ((model.AwardHours - awardtime) / Status.WorkingHours);
                            alandrlDal.Update(incentiveModel, conn, trans);
                        }

                    }

                    returnValue = dal.Update(model, conn, trans);

                    if (approvelog != null)
                    {
                        approvelog.ApproveDateID = model.ID;
                        int approveid = new ApproveLogManager().Add(approvelog, conn, trans);
                        approvelog.ID = approveid;
                    }
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    returnValue = 0;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            dal.Delete(ID);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(MattersInfo model)
        {
            int returnValue = 0;
            ALAndRLManager alandrlManager = new ALAndRLManager();
            // 获得年假信息
            ALAndRLInfo alandrlModel = null;
            ALAndRLInfo rewardModel = null;

            if (model.MatterType == Status.MattersType_Annual_Last)
            {
                alandrlModel = alandrlManager.GetALAndRLModel(model.UserID, model.BeginTime.Year - 1, (int)AAndRLeaveType.AnnualType);
                rewardModel = alandrlManager.GetALAndRLModel(model.UserID, model.BeginTime.Year - 1, (int)AAndRLeaveType.RewardType);
            }
            else
            {
                alandrlModel = alandrlManager.GetALAndRLModel(model.UserID, model.BeginTime.Year, (int)AAndRLeaveType.AnnualType);
                rewardModel = alandrlManager.GetALAndRLModel(model.UserID, model.BeginTime.Year, (int)AAndRLeaveType.RewardType);
            }

            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    if (model.MatterType == Status.MattersType_Annual || model.MatterType == Status.MattersType_Annual_Last)
                    {
                        alandrlModel.RemainingNumber += (model.AnnualHours / Status.WorkingHours);
                        new ALAndRLDataProvider().Update(alandrlModel, conn, trans);
                        if (rewardModel != null)
                        {
                            rewardModel.RemainingNumber += (model.AwardHours / Status.WorkingHours);
                            new ALAndRLDataProvider().Update(rewardModel, conn, trans);
                        }
                    }
                    dal.Update(model, conn, trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MattersInfo GetModel(int ID)
        {
            return dal.GetModel(ID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetMatterInfosList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return dal.GetMatterInfosList("");
        }

        /// <summary>
        /// 获得指定日期的员工事由
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <param name="MattersDateTime">要获取事由的日期</param>
        /// <returns>返回事由信息集合</returns>
        public List<MattersInfo> GetMattersList(int UserID, DateTime MattersDateTime)
        {
            DataSet ds = dal.GetMatterInfosList(" userid=" + UserID +
                " and ('" + MattersDateTime.ToString("yyyy-MM-dd") + "' between Convert(Char(10),BeginTime,120) and Convert(Char(10),EndTime,120)) " +
                " and Deleted='false'");
            List<MattersInfo> list = new List<MattersInfo>();

            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                MattersInfo model = null;

                for (int n = 0; n < rowsCount; n++)
                {
                    model = new MattersInfo();
                    model.PopupData(ds.Tables[0].Rows[n]);
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得一个用户所有的事由信息
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <returns>返回考勤事由集合</returns>
        public List<MattersInfo> GetMattersList(int UserID)
        {
            // 获得用户所有的考勤事由
            DataSet ds = dal.GetList(" Userid=" + UserID + " and deleted='false'");
            List<MattersInfo> list = new List<MattersInfo>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null
                && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MattersInfo model = new MattersInfo();
                    model.PopupData(dr);
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得用户某种类型的事由
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <param name="MatterType">事由类型</param>
        /// <returns>返回一个事由类型的集合</returns>
        public List<MattersInfo> GetMattersList(int UserID, int MatterType)
        {
            DataSet ds = dal.GetList(" deleted='false' and mattertype=" + MatterType + " and UserID=" + UserID);
            List<MattersInfo> list = new List<MattersInfo>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null
                && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MattersInfo model = new MattersInfo();
                    model.PopupData(dr);

                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得事由和OT的所有集合
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public DataSet GetMattersViewList(string sqlStr)
        {
            //List<MattersExtendInfo> list = 
            return dal.GetMattersViewList(sqlStr);
            //return list;
        }

        /// <summary>
        /// 获得指定ID的事由
        /// </summary>
        /// <param name="ID">编号</param>        
        /// <returns>返回一个事由类型的集合</returns>
        public List<MattersInfo> GetMattersList(string stringWhere)
        {
            DataSet ds = dal.GetMatterInfosList(" ID in (" + stringWhere + ")");
            List<MattersInfo> list = new List<MattersInfo>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null
                && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MattersInfo model = new MattersInfo();
                    model.PopupData(dr);

                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得考勤事由信息集合
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public DataSet GetMatterInfosList(string strWhere)
        {
            return dal.GetMatterInfosList(strWhere);
        }

        /// <summary>
        /// 判断事由类型的时间有没有重叠的情况
        /// </summary>
        /// <returns>如果存在重叠的情况返回true，否则返回false</returns>
        public bool CheckIsOverLap(int UserID, DateTime beginTime, DateTime endTime, int modelId)
        {
            bool b = false;
            List<MattersInfo> list = this.GetMattersList(UserID);
            if (list != null && list.Count > 0)
            {
                foreach (MattersInfo matterModel in list)
                {
                    if (matterModel.ID == modelId)
                        continue;
                    // 事由开始时间
                    DateTime matterBeginTime = matterModel.BeginTime;
                    // 事由结束时间
                    DateTime matterEndTime = matterModel.EndTime;
                    if (endTime > matterBeginTime && beginTime < matterEndTime)
                    {
                        b = true;
                    }

                    //if (matterBeginTime < beginTime && beginTime < matterEndTime)
                    //{
                    //    b = true;    
                    //}
                    //if (matterBeginTime < endTime && endTime < matterEndTime)
                    //{
                    //    b = true;
                    //}
                    //if (beginTime < matterBeginTime && matterBeginTime < endTime)
                    //{
                    //    b = true;
                    //}
                    //if (beginTime < matterEndTime && matterEndTime < endTime)
                    //{
                    //    b = true;
                    //}
                }
            }
            return b;
        }

        /// <summary>
        /// 获得考勤事由对象
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>返回考勤事由集合对象</returns>
        public IList<MattersInfo> GetModelListByMonth(int userId, int year, int month)
        {
            return dal.GetModelListByMonth(userId, year, month);
        }

        /// <summary>
        /// 修改调休单信息
        /// </summary>
        /// <param name="model">调休单对象</param>
        public int UpdateOffTune(MattersInfo model)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    if (model != null)
                    {
                        dal.Update(model, conn, trans);
                        this.CalOverTimeRest(model, model.ID, conn, trans);
                        return model.ID;
                    }
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                }
            }
            return model.ID;
        }

        /// <summary>
        /// 添加调休单信息
        /// </summary>
        /// <param name="model">调休单对象</param>
        public int AddOffTune(MattersInfo model)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int modelID = 0;
                    if (model != null)
                    {
                        modelID = dal.Add(model, conn, trans);
                        this.CalOverTimeRest(model, modelID, conn, trans);
                    }
                    trans.Commit();
                    return modelID;
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// 计算调休的时间信息
        /// </summary>
        /// <param name="model"></param>
        public void CalOverTimeRest(MattersInfo model, int modelID, SqlConnection conn, SqlTransaction trans)
        {
            SingleOvertimeManager singleOverTimeManager = new SingleOvertimeManager();
            OverTimeRestManager overTimeRestManager = new OverTimeRestManager();
            overTimeRestManager.DeleteOffTuneInfos(model.ID);

            List<SingleOvertimeInfo> list = singleOverTimeManager.GetEffectiveSingleOverTime(model.UserID, model.BeginTime);
            if (list != null && list.Count > 0)
            {
                decimal totalHours = model.TotalHours;
                decimal useingHours = 0;
                foreach (SingleOvertimeInfo single in list)
                {
                    if (single.Remaininghours > 0)
                    {
                        useingHours += single.Remaininghours;

                        if (useingHours >= totalHours)
                        {
                            decimal temphours = totalHours - (useingHours - single.Remaininghours);
                            decimal tempRemaining = single.Remaininghours - temphours;

                            OverTimeRestInfo overTimeRestModel = new OverTimeRestInfo();
                            overTimeRestModel.MatterID = modelID;
                            overTimeRestModel.OverTimeID = single.ID;
                            overTimeRestModel.Useovertimehours = (int)temphours;
                            overTimeRestManager.Add(overTimeRestModel);

                            single.Remaininghours = (int)tempRemaining;
                            single.State = Status.SingleOverTimeUserState_PartUse;
                            singleOverTimeManager.Update(single, conn, trans);

                            break;
                        }
                        else
                        {
                            OverTimeRestInfo overTimeRestModel = new OverTimeRestInfo();
                            overTimeRestModel.MatterID = modelID;
                            overTimeRestModel.OverTimeID = single.ID;
                            overTimeRestModel.Useovertimehours = single.Remaininghours;
                            overTimeRestManager.Add(overTimeRestModel);

                            single.Remaininghours = 0;
                            single.State = Status.SingleOverTimeUserState_Used;
                            singleOverTimeManager.Update(single, conn, trans);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 删除调休单事由类型
        /// </summary>
        /// <param name="mattersModel"></param>
        public void DeleteOffTuneInfo(MattersInfo mattersModel)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    SingleOvertimeManager singleOverTimeManager = new SingleOvertimeManager();
                    OverTimeRestManager overTimeRestManager = new OverTimeRestManager();
                    DataSet ds = overTimeRestManager.GetList(" MatterID=" + mattersModel.ID);
                    if (ds != null && ds.Tables != null && ds.Tables.Count > 0
                        && ds.Tables[0] != null && ds.Tables[0].Rows != null)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            SingleOvertimeInfo singleModel = singleOverTimeManager.GetModel(((int)dr["OverTimeID"]));
                            singleModel.Remaininghours += (int)dr["UseOverTimeHours"];
                            singleModel.State = Status.SingleOverTimeUserState_PartUse;
                            singleOverTimeManager.Update(singleModel, conn, trans);

                            overTimeRestManager.Delete((int)dr["ID"]);
                        }
                    }
                    Update(mattersModel, conn, trans);
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                }
            }
        }

        /// <summary>
        /// 获得考勤统计数据
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>返回考勤统计对象</returns>
        public Dictionary<int, List<MattersInfo>> GetStatModelList(int year, int month, int day)
        {
            return dal.GetStatModelList(year, month, day);
        }

        /// <summary>
        /// 批量驳回考勤事由
        /// </summary>
        /// <param name="userID">审批人ID</param>
        /// <param name="list">被驳回事由审批记录ID</param>
        /// <returns>驳回成功返回true,否则返回false</returns>
        public bool BatchOverruleMatter(int userID, List<int> list, string authority, out Dictionary<int, string> sendMailDic)
        {
            sendMailDic = new Dictionary<int, string>();
            bool b = false;
            // 审批记录业务类
            ApproveLogManager appManager = new ApproveLogManager();
            // 事由业务操作类
            MattersManager matterManager = new MattersManager();
            // OT业务操作类
            SingleOvertimeManager singleManager = new SingleOvertimeManager();
            UserInfo userInfo = ESP.Framework.BusinessLogic.UserManager.Get(userID);
            if (list != null && list.Count > 0)
            {
                using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    try
                    {
                        foreach (int id in list)
                        {
                            ApproveLogInfo appModel = appManager.GetModel(id);

                            // 请假，调休，其他，外出，出差
                            if (appModel.ApproveType == (int)Status.MattersSingle.MattersSingle_Leave
                                || appModel.ApproveType == (int)Status.MattersSingle.MattersSingle_OffTune
                                || appModel.ApproveType == (int)Status.MattersSingle.MattersSingle_Other
                                || appModel.ApproveType == (int)Status.MattersSingle.MattersSingle_Out
                                || appModel.ApproveType == (int)Status.MattersSingle.MattersSingle_Travel)
                            {
                                MattersInfo mattersModel = matterManager.GetModel(appModel.ApproveDateID);
                                if (mattersModel != null)
                                {
                                    mattersModel.UpdateTime = DateTime.Now;
                                    mattersModel.OperateorID = userID;
                                    mattersModel.MatterState = Status.MattersState_Overrule;
                                    mattersModel.Approveid = userID;
                                    mattersModel.Approvedesc += DateTime.Now.ToString("yyyy-MM-dd HH:mm") + userInfo.FullNameCN + "驳回。";
                                    matterManager.Update(mattersModel, conn, trans);

                                    sendMailDic.Add(mattersModel.ID, "matter,1," + mattersModel.UserID + "," + mattersModel.UserID + "");
                                }
                            }
                            // OT
                            else if (appModel.ApproveType == (int)Status.MattersSingle.MattersSingle_OverTime)
                            {
                                SingleOvertimeInfo singleModel = singleManager.GetModel(appModel.ApproveDateID);
                                singleModel.UpdateTime = DateTime.Now;
                                singleModel.OperateorID = userID;
                                singleModel.Approvestate = Status.OverTimeState_Overrule;
                                singleModel.ApproveID = userID;
                                singleModel.ApproveRemark += DateTime.Now.ToString("yyyy-MM-dd HH:mm") + userInfo.FullNameCN + "驳回。";
                                singleManager.Update(singleModel, conn, trans);

                                sendMailDic.Add(singleModel.ID, "single,1," + singleModel.UserID + "," + singleModel.UserID + "");
                            }

                            appModel.ApproveState = 2;   // 审批驳回
                            appModel.UpdateTime = DateTime.Now;
                            appModel.OperateorID = userID;
                            appModel.Approveremark = DateTime.Now.ToString("yyyy-MM-dd HH:mm") + userInfo.FullNameCN + "驳回";
                            appManager.Update(appModel, conn, trans);
                            ESP.Logging.Logger.Add(userInfo.FullNameCN + "(" + userInfo.UserID + ")驳回了,考勤事由编号是（" + appModel.ApproveDateID + "）",
                               "考勤系统事由驳回", ESP.Logging.LogLevel.Information);
                        }
                        trans.Commit();
                        b = true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        ESP.Logging.Logger.Add(ex.Message + "：" + ex.StackTrace,
                               "考勤系统事由驳回", ESP.Logging.LogLevel.Error);
                        b = false;
                    }
                }
            }
            return b;
        }

        /// <summary>
        /// 批量审批通过考勤事由
        /// </summary>
        /// <param name="userID">审批人ID</param>
        /// <param name="list">被审批通过的事由审批记录ID</param>
        /// <param name="sendMailDic">发邮件内容信息，key表示事由的ID值，value内容格式(matter/single,1/2,AcceptUserId,matterid/singleid)(考勤事由/OT事由,审批通过/等待下级审批,接受人id,事由申请人id)</param>
        /// <returns>审批通过成功返回true,否则返回false</returns>
        public bool BatchAuditMatter(int userID, List<int> list, out Dictionary<int, string> sendMailDic)
        {
            sendMailDic = new Dictionary<int, string>();
            bool b = false;
            // 审批记录业务类
            ApproveLogManager appManager = new ApproveLogManager();
            // 事由业务操作类
            MattersManager matterManager = new MattersManager();
            // OT业务操作类
            SingleOvertimeManager singleManager = new SingleOvertimeManager();
            UserInfo userInfo = ESP.Framework.BusinessLogic.UserManager.Get(userID);
            if (list != null && list.Count > 0)
            {
                using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    try
                    {
                        foreach (int id in list)
                        {
                            ApproveLogInfo appModel = appManager.GetModel(id);

                            // 请假，调休，其他，外出，出差
                            if (appModel.ApproveType == (int)Status.MattersSingle.MattersSingle_Leave
                                || appModel.ApproveType == (int)Status.MattersSingle.MattersSingle_OffTune
                                || appModel.ApproveType == (int)Status.MattersSingle.MattersSingle_Other
                                || appModel.ApproveType == (int)Status.MattersSingle.MattersSingle_Out
                                || appModel.ApproveType == (int)Status.MattersSingle.MattersSingle_Travel
                                || appModel.ApproveType == (int)Status.MattersSingle.MattersSingle_OTLate)
                            {
                                MattersInfo mattersModel = matterManager.GetModel(appModel.ApproveDateID);
                                if (mattersModel != null)
                                {
                                    if (mattersModel.MatterState == Status.MattersState_WaitHR)
                                    {
                                        mattersModel.UpdateTime = DateTime.Now;
                                        mattersModel.OperateorID = userID;
                                        mattersModel.MatterState = Status.MattersState_WaitDirector;
                                        mattersModel.Approveid = userID;
                                        mattersModel.Approvedesc += DateTime.Now.ToString("yyyy-MM-dd HH:mm") + userInfo.FullNameCN + "审批通过。";
                                        matterManager.Update(mattersModel, conn, trans);

                                        #region 审批记录信息
                                        ApproveLogInfo applog = new ApproveLogInfo();
                                        applog.ApproveDateID = mattersModel.ID;
                                        applog.ApproveState = 0;

                                        ESP.Administrative.BusinessLogic.OperationAuditManageManager operationAuditManager = new ESP.Administrative.BusinessLogic.OperationAuditManageManager();
                                        ESP.Administrative.Entity.OperationAuditManageInfo opearmodel = operationAuditManager.GetOperationAuditModelByUserID(mattersModel.UserID);
                                        if (opearmodel != null)
                                        {
                                            applog.ApproveID = opearmodel.TeamLeaderID;
                                            applog.ApproveName = opearmodel.TeamLeaderName;
                                        }
                                        applog.ApproveType = (int)Status.MattersSingle.MattersSingle_Leave;
                                        applog.ApproveUpUserID = userID;
                                        applog.CreateTime = DateTime.Now;
                                        applog.Deleted = false;
                                        applog.IsLastApprove = 1;
                                        applog.OperateorID = mattersModel.UserID;
                                        applog.Sort = 0;
                                        applog.UpdateTime = DateTime.Now;
                                        appManager.Add(applog, conn, trans);
                                        #endregion

                                        sendMailDic.Add(mattersModel.ID, "matter,2," + applog.ApproveID + "," + mattersModel.UserID + "");

                                    }
                                    else if (mattersModel.MatterState == Status.MattersState_WaitDirector)
                                    {
                                        mattersModel.UpdateTime = DateTime.Now;
                                        mattersModel.OperateorID = userID;
                                        mattersModel.MatterState = Status.MattersState_Passed;    // 请假单审批通过
                                        mattersModel.Approveid = userID;
                                        mattersModel.Approvedesc += DateTime.Now.ToString("yyyy-MM-dd HH:mm") + userInfo.FullNameCN + "审批通过。";
                                        matterManager.Update(mattersModel, conn, trans);

                                        sendMailDic.Add(mattersModel.ID, "matter,1," + mattersModel.UserID + "," + mattersModel.UserID + "");
                                    }
                                }
                            }
                            // OT
                            else if (appModel.ApproveType == (int)Status.MattersSingle.MattersSingle_OverTime)
                            {
                                SingleOvertimeInfo singleModel = singleManager.GetModel(appModel.ApproveDateID);
                                singleModel.UpdateTime = DateTime.Now;
                                singleModel.OperateorID = userID;
                                singleModel.Approvestate = Status.OverTimeState_Passed;    // OT单审批通过
                                singleModel.ApproveID = userID;
                                singleModel.ApproveRemark += DateTime.Now.ToString("yyyy-MM-dd HH:mm") + userInfo.FullNameCN + "审批通过。";
                                singleManager.Update(singleModel, conn, trans);

                                sendMailDic.Add(singleModel.ID, "single,1," + singleModel.UserID + "," + singleModel.UserID + "");
                            }
                            appModel.ApproveState = 1;    // 审批通过
                            appModel.UpdateTime = DateTime.Now;
                            appModel.OperateorID = userID;
                            appModel.Approveremark = DateTime.Now.ToString("yyyy-MM-dd HH:mm") + userInfo.FullNameCN + "审批通过。";
                            appManager.Update(appModel, conn, trans);
                            ESP.Logging.Logger.Add(userInfo.FullNameCN + "(" + userInfo.UserID + ")审批通过了一个考勤事由,考勤事由编号是（" + appModel.ApproveDateID + "）",
                              "考勤系统事由审批", ESP.Logging.LogLevel.Information);
                        }

                        trans.Commit();
                        b = true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        ESP.Logging.Logger.Add(ex.Message + "：" + ex.StackTrace,
                               "考勤系统事由审批", ESP.Logging.LogLevel.Error);
                        b = false;
                    }
                }
            }
            return b;
        }
        #endregion  成员方法
    }
}
