using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ESP.HumanResource.Common;
using System.Web;

namespace ESP.HumanResource.BusinessLogic
{
    public class DimissionFormManager
    {
        private static readonly ESP.HumanResource.DataAccess.DimissionFormProvider dal = new ESP.HumanResource.DataAccess.DimissionFormProvider();
        public DimissionFormManager()
        { }

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(int DimissionId)
        {
            return dal.Exists(DimissionId);
        }
        public static bool ExistsUser(int UserId)
        {
            return dal.ExistsUser(UserId);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ESP.HumanResource.Entity.DimissionFormInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 提交一个离职单信息
        /// </summary>
        /// <param name="model">离职单信息</param>
        /// <param name="HRAuditLog">审批日志对象</param>
        /// <returns>操作成功返回true，否则返回false</returns>
        public static bool Add(ESP.HumanResource.Entity.DimissionFormInfo model, ESP.HumanResource.Entity.HRAuditLogInfo HRAuditLog)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int dimissionid = dal.Add(model, trans);  // 保存离职单信息
                    // 保存审批日志信息
                    HRAuditLog.FormId = dimissionid;
                    HRAuditLog.FormType = (int)HRFormType.DimissionForm;
                    ESP.HumanResource.DataAccess.HRAuditLogProvider hrauditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
                    hrauditLogDal.Add(HRAuditLog, trans);

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add("HumanResource", ex.ToString(), ESP.Logging.LogLevel.Error, ex);
                    trans.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 提交一个离职单信息
        /// </summary>
        /// <param name="model">离职单信息</param>
        /// <param name="HRAuditLog">审批日志对象</param>
        /// <returns>操作成功返回true，否则返回false</returns>
        public static bool Add(ESP.HumanResource.Entity.DimissionFormInfo model, ESP.HumanResource.Entity.HRAuditLogInfo HRAuditLog,
            ESP.HumanResource.Entity.EmployeeBaseInfo empBase)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int dimissionid = dal.Add(model, trans);  // 保存离职单信息
                    model.DimissionId = dimissionid;
                    // 保存审批日志信息
                    HRAuditLog.FormId = dimissionid;
                    HRAuditLog.FormType = (int)HRFormType.DimissionForm;
                    ESP.HumanResource.DataAccess.HRAuditLogProvider hrauditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
                    hrauditLogDal.Add(HRAuditLog, trans);

                    ESP.HumanResource.BusinessLogic.EmployeeBaseManager.Update(empBase, trans);  // 更新员工状态
                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add("HumanResource", ex.ToString(), ESP.Logging.LogLevel.Error, ex);
                    trans.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(ESP.HumanResource.Entity.DimissionFormInfo model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 提交离职单信息
        /// </summary>
        /// <param name="model">离职单信息</param>
        /// <param name="hrAuditLogInfo">审批日志信息</param>
        /// <returns>如果操作成功返回true，否则返回false</returns>
        public static bool Update(ESP.HumanResource.Entity.DimissionFormInfo model, ESP.HumanResource.Entity.HRAuditLogInfo hrAuditLogInfo)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    dal.Update(model, trans);  // 保存离职单信息
                    // 保存审批日志信息
                    hrAuditLogInfo.FormId = model.DimissionId;
                    hrAuditLogInfo.FormType = (int)HRFormType.DimissionForm;
                    hrAuditLogInfo.AuditStatus = (int)AuditStatus.NotAudit;
                    ESP.HumanResource.DataAccess.HRAuditLogProvider hrauditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
                    hrauditLogDal.Add(hrAuditLogInfo, trans);

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add("HumanResource", ex.ToString(), ESP.Logging.LogLevel.Error, ex);
                    trans.Rollback();
                    return false;
                }
            }
        }
        /// <summary>
        /// 提交离职单信息
        /// </summary>
        /// <param name="model">离职单信息</param>
        /// <param name="hrAuditLogInfo">审批日志信息</param>
        /// <returns>如果操作成功返回true，否则返回false</returns>
        public static bool DimissionOverrule(ESP.HumanResource.Entity.DimissionFormInfo model, ESP.HumanResource.Entity.HRAuditLogInfo hrAuditLogInfo)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    dal.Update(model, trans);  // 保存离职单信息
                    // 保存审批日志信息
                    ESP.HumanResource.DataAccess.HRAuditLogProvider hrauditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
                    hrauditLogDal.Update(hrAuditLogInfo, trans);

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add("HumanResource", ex.ToString(), ESP.Logging.LogLevel.Error, ex);
                    trans.Rollback();
                    return false;
                }
            }
        }


        /// <summary>
        /// 提交离职单信息
        /// </summary>
        /// <param name="model">离职单信息</param>
        /// <param name="hrAuditLogInfo">审批日志信息</param>
        /// <returns>如果操作成功返回true，否则返回false</returns>
        public static bool Update(ESP.HumanResource.Entity.DimissionFormInfo model, ESP.HumanResource.Entity.HRAuditLogInfo hrAuditLogInfo,
            ESP.HumanResource.Entity.EmployeeBaseInfo empBase)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    dal.Update(model, trans);  // 保存离职单信息
                    // 保存审批日志信息
                    hrAuditLogInfo.FormId = model.DimissionId;
                    hrAuditLogInfo.FormType = (int)HRFormType.DimissionForm;
                    hrAuditLogInfo.AuditStatus = (int)AuditStatus.NotAudit;
                    ESP.HumanResource.DataAccess.HRAuditLogProvider hrauditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
                    hrauditLogDal.Add(hrAuditLogInfo, trans);

                    ESP.HumanResource.BusinessLogic.EmployeeBaseManager.Update(empBase, trans);  // 更新员工状态

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add("HumanResource", ex.ToString(), ESP.Logging.LogLevel.Error, ex);
                    trans.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 提交离职单信息
        /// </summary>
        /// <param name="model">离职单信息</param>
        /// <param name="hrAuditLogInfo">审批日志信息</param>
        /// <returns>如果操作成功返回true，否则返回false</returns>
        public static bool Update(ESP.HumanResource.Entity.DimissionFormInfo model,
            ESP.HumanResource.Entity.HRAuditLogInfo oldHRAuditLogInfo,
            ESP.HumanResource.Entity.HRAuditLogInfo newHRAuditLogInfo)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // 保存审批日志信息
                    ESP.HumanResource.DataAccess.HRAuditLogProvider hrauditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();

                    hrauditLogDal.Update(oldHRAuditLogInfo, trans);  // 修改上一级审批日志信息
                    hrauditLogDal.Add(newHRAuditLogInfo, trans);  // 添加下一级审批日志信息
                    //// 判断如果是上海广州的用户则团队审批和IT审批同时进行。
                    //if (DimissionDetailsManager.IsShanghaiGuangzhouUser(model.UserId))
                    //{
                    //    model.Status = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHRIT;
                    //    // IT部审批日志
                    //    ESP.HumanResource.Entity.HRAuditLogInfo newITAuditLogInfo = new ESP.HumanResource.Entity.HRAuditLogInfo();
                    //    newITAuditLogInfo.AuditorId = 34;
                    //    newITAuditLogInfo.AuditorName = "IT技术部";
                    //    newITAuditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;
                    //    newITAuditLogInfo.FormId = model.DimissionId;
                    //    newITAuditLogInfo.FormType = (int)ESP.HumanResource.Common.HRFormType.DimissionForm;
                    //    newITAuditLogInfo.AuditLevel = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHRIT;
                    //    hrauditLogDal.Add(newITAuditLogInfo, trans);
                    //}
                    dal.Update(model, trans);  // 保存离职单信息

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add("HumanResource", ex.ToString(), ESP.Logging.LogLevel.Error, ex);
                    trans.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 团队HR审批通过
        /// </summary>
        /// <param name="model">离职单信息</param>
        /// <param name="hrAuditLogInfo">审批日志信息</param>
        /// <returns>如果操作成功返回true，否则返回false</returns>
        public static bool Update(ESP.HumanResource.Entity.DimissionFormInfo model,
            ESP.HumanResource.Entity.HRAuditLogInfo oldHRAuditLogInfo,
            ESP.HumanResource.Entity.HRAuditLogInfo newHRAuditLogInfo,
            ESP.HumanResource.Entity.DimissionGrougHRDetailsInfo groupHRDetailInfo)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    dal.Update(model, trans);  // 保存离职单信息

                    // 保存审批日志信息
                    ESP.HumanResource.DataAccess.HRAuditLogProvider hrauditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
                    hrauditLogDal.Update(oldHRAuditLogInfo, trans);  // 修改上一级审批日志信息
                    hrauditLogDal.Add(newHRAuditLogInfo, trans);  // 添加下一级审批日志信息

                    // 添加团队HR审批信息
                    ESP.HumanResource.DataAccess.DimissionGrougHRDetailsDataProvider groupHRDetailsDal = new ESP.HumanResource.DataAccess.DimissionGrougHRDetailsDataProvider();
                    groupHRDetailsDal.Add(groupHRDetailInfo, trans);

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add("HumanResource", ex.ToString(), ESP.Logging.LogLevel.Error, ex);
                    trans.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 团队HR审批通过
        /// </summary>
        /// <param name="model">离职单信息</param>
        /// <param name="oldHRAuditLogInfo">原审批日志信息</param>
        /// <param name="newHRAuditLogInfo">人力资源审批信息</param>
        /// <param name="newAuditLogInfo">IT部审批信息</param>
        /// <param name="groupHRDetailInfo">团队审批记录信息</param>
        /// <returns>如果操作成功返回true，否则返回false</returns>
        public static bool Update(ESP.HumanResource.Entity.DimissionFormInfo model,
            ESP.HumanResource.Entity.HRAuditLogInfo oldHRAuditLogInfo,
            ESP.HumanResource.Entity.HRAuditLogInfo newHRAuditLogInfo,
            ESP.HumanResource.Entity.HRAuditLogInfo newAuditLogInfo,
            ESP.HumanResource.Entity.DimissionGrougHRDetailsInfo groupHRDetailInfo,
            ESP.HumanResource.Entity.DimissionHRDetailsInfo hrDetailInfo,
            ESP.HumanResource.Entity.DimissionITDetailsInfo itDetailInfo,
            List<ESP.HumanResource.Entity.HRAuditLogInfo> financeAuditLogList,
            List<ESP.HumanResource.Entity.DimissionIndemnityInfo> indemnityInfoList)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    dal.Update(model, trans);  // 保存离职单信息

                    // 保存审批日志信息
                    ESP.HumanResource.DataAccess.HRAuditLogProvider hrauditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
                    hrauditLogDal.Update(oldHRAuditLogInfo, trans);  // 修改上一级审批日志信息

                    hrauditLogDal.Add(newHRAuditLogInfo, trans);  // 添加下一级审批日志信息
                    hrauditLogDal.Add(newAuditLogInfo, trans);
                    // 添加团队HR审批信息
                    ESP.HumanResource.DataAccess.DimissionGrougHRDetailsDataProvider groupHRDetailsDal = new ESP.HumanResource.DataAccess.DimissionGrougHRDetailsDataProvider();
                    groupHRDetailsDal.Add(groupHRDetailInfo, trans);
                    if (indemnityInfoList != null && indemnityInfoList.Count > 0)
                    {
                        ESP.HumanResource.DataAccess.DimissionIndemnityDataProvider indemnityDal = new ESP.HumanResource.DataAccess.DimissionIndemnityDataProvider();
                        foreach (ESP.HumanResource.Entity.DimissionIndemnityInfo indemnityInfo in indemnityInfoList)
                        {
                            indemnityDal.Add(indemnityInfo, trans);
                        }
                    }

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add("HumanResource", ex.ToString(), ESP.Logging.LogLevel.Error, ex);
                    trans.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 集团HR审批通过
        /// </summary>
        /// <param name="model">离职单信息</param>
        /// <param name="hrAuditLogInfo">审批日志信息</param>
        /// <returns>如果操作成功返回true，否则返回false</returns>
        public static bool Update(ESP.HumanResource.Entity.DimissionFormInfo model,
            ESP.HumanResource.Entity.HRAuditLogInfo oldHRAuditLogInfo,
            ESP.HumanResource.Entity.HRAuditLogInfo newHRAuditLogInfo,
            ESP.HumanResource.Entity.DimissionHRDetailsInfo hrDetailInfo)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    dal.Update(model, trans);  // 保存离职单信息

                    // 保存审批日志信息
                    ESP.HumanResource.DataAccess.HRAuditLogProvider hrauditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
                    hrauditLogDal.Update(oldHRAuditLogInfo, trans);  // 修改上一级审批日志信息
                    hrauditLogDal.Add(newHRAuditLogInfo, trans);  // 添加下一级审批日志信息

                    // 添加团队HR审批信息
                    ESP.HumanResource.DataAccess.DimissionHRDetailsDataProvider groupHRDetailsDal = new ESP.HumanResource.DataAccess.DimissionHRDetailsDataProvider();
                    groupHRDetailsDal.Add(hrDetailInfo, trans);

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add("HumanResource", ex.ToString(), ESP.Logging.LogLevel.Error, ex);
                    trans.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 集团HR审批通过
        /// </summary>
        /// <param name="model">离职单信息</param>
        /// <param name="hrAuditLogInfo">审批日志信息</param>
        /// <returns>如果操作成功返回true，否则返回false</returns>
        public static bool Update(ESP.HumanResource.Entity.DimissionFormInfo model,
            ESP.HumanResource.Entity.HRAuditLogInfo oldHRAuditLogInfo,
            List<ESP.HumanResource.Entity.HRAuditLogInfo> newHRAuditLogList,
            ESP.HumanResource.Entity.DimissionHRDetailsInfo hrDetailInfo,
            ESP.HumanResource.Entity.DimissionGrougHRDetailsInfo hrGroupHRDetailInfo, ESP.HumanResource.Entity.DimissionITDetailsInfo itDetailInfo,
            List<ESP.HumanResource.Entity.DimissionIndemnityInfo> indemnityInfoList)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    dal.Update(model, trans);  // 保存离职单信息

                    // 保存审批日志信息
                    ESP.HumanResource.DataAccess.HRAuditLogProvider hrauditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
                    hrauditLogDal.Update(oldHRAuditLogInfo, trans);  // 修改上一级审批日志信息
                    if (newHRAuditLogList != null && newHRAuditLogList.Count > 0)
                    {
                        foreach (ESP.HumanResource.Entity.HRAuditLogInfo newHRAuditLogInfo in newHRAuditLogList)
                        {
                            hrauditLogDal.Add(newHRAuditLogInfo, trans);  // 添加下一级审批日志信息
                        }
                    }
                    // 添加团队HR审批信息
                    ESP.HumanResource.DataAccess.DimissionHRDetailsDataProvider groupHRDetailsDal = new ESP.HumanResource.DataAccess.DimissionHRDetailsDataProvider();
                    groupHRDetailsDal.Add(hrDetailInfo, trans);

                    if (hrGroupHRDetailInfo != null)
                    {
                        ESP.HumanResource.DataAccess.DimissionGrougHRDetailsDataProvider groupHRDal = new ESP.HumanResource.DataAccess.DimissionGrougHRDetailsDataProvider();
                        groupHRDal.Add(hrGroupHRDetailInfo, trans);
                    }

                    // 添加团队HR审批信息
                    ESP.HumanResource.DataAccess.DimissionITDetailsDataProvider itDetailsDal = new ESP.HumanResource.DataAccess.DimissionITDetailsDataProvider();
                    itDetailsDal.Add(itDetailInfo, trans);

                    if (indemnityInfoList != null && indemnityInfoList.Count > 0)
                    {
                        ESP.HumanResource.DataAccess.DimissionIndemnityDataProvider indemnityDal = new ESP.HumanResource.DataAccess.DimissionIndemnityDataProvider();
                        foreach (ESP.HumanResource.Entity.DimissionIndemnityInfo indemnityInfo in indemnityInfoList)
                        {
                            indemnityDal.Add(indemnityInfo, trans);
                        }
                    }

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add("HumanResource", ex.ToString(), ESP.Logging.LogLevel.Error, ex);
                    trans.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 集团HR审批通过
        /// </summary>
        /// <param name="model">离职单信息</param>
        /// <param name="hrAuditLogInfo">审批日志信息</param>
        /// <returns>如果操作成功返回true，否则返回false</returns>
        public static bool Update(ESP.HumanResource.Entity.DimissionFormInfo model,
            ESP.HumanResource.Entity.HRAuditLogInfo oldHRAuditLogInfo,
            ESP.HumanResource.Entity.HRAuditLogInfo newFinanceAuditLogInfo,
            ESP.HumanResource.Entity.HRAuditLogInfo newITAuditLogInfo,
            ESP.HumanResource.Entity.HRAuditLogInfo newADAuditLogInfo,
            ESP.HumanResource.Entity.DimissionHRDetailsInfo hrDetailInfo)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    dal.Update(model, trans);  // 保存离职单信息

                    // 保存审批日志信息
                    ESP.HumanResource.DataAccess.HRAuditLogProvider hrauditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
                    hrauditLogDal.Update(oldHRAuditLogInfo, trans);  // 修改上一级审批日志信息
                    if (newFinanceAuditLogInfo != null)
                        hrauditLogDal.Add(newFinanceAuditLogInfo, trans);  // 添加财务审批日志信息
                    if (newITAuditLogInfo != null)
                        hrauditLogDal.Add(newITAuditLogInfo, trans);  // 添加IT部审批日志信息
                    if (newADAuditLogInfo != null)
                        hrauditLogDal.Add(newADAuditLogInfo, trans);  // 添加行政部审批日志信息

                    // 添加团队HR审批信息
                    ESP.HumanResource.DataAccess.DimissionHRDetailsDataProvider groupHRDetailsDal = new ESP.HumanResource.DataAccess.DimissionHRDetailsDataProvider();
                    if (hrDetailInfo.DimissionHRDetailId > 0)
                        groupHRDetailsDal.Update(hrDetailInfo, trans);
                    else
                        groupHRDetailsDal.Add(hrDetailInfo, trans);

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add("HumanResource", ex.ToString(), ESP.Logging.LogLevel.Error, ex);
                    trans.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 集团HR审批通过
        /// </summary>
        /// <param name="model">离职单信息</param>
        /// <param name="hrAuditLogInfo">审批日志信息</param>
        /// <returns>如果操作成功返回true，否则返回false</returns>
        public static bool Update(ESP.HumanResource.Entity.DimissionFormInfo model,
            ESP.HumanResource.Entity.HRAuditLogInfo oldHRAuditLogInfo,
            ESP.HumanResource.Entity.HRAuditLogInfo newFinanceAuditLogInfo,
            ESP.HumanResource.Entity.HRAuditLogInfo newITAuditLogInfo,
            ESP.HumanResource.Entity.HRAuditLogInfo newADAuditLogInfo,
            ESP.HumanResource.Entity.DimissionHRDetailsInfo hrDetailInfo,
            ESP.HumanResource.Entity.EmployeeBaseInfo employeeBase,
            ESP.HumanResource.Entity.DimissionInfo dimissionModel,
            ESP.HumanResource.Entity.DimissionADDetailsInfo adDetailInfo)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    dal.Update(model, trans);  // 保存离职单信息

                    // 保存审批日志信息
                    ESP.HumanResource.DataAccess.HRAuditLogProvider hrauditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
                    hrauditLogDal.Update(oldHRAuditLogInfo, trans);  // 修改上一级审批日志信息
                    if (newFinanceAuditLogInfo != null)
                        hrauditLogDal.Add(newFinanceAuditLogInfo, trans);  // 添加财务审批日志信息
                    if (newITAuditLogInfo != null)
                        hrauditLogDal.Add(newITAuditLogInfo, trans);  // 添加IT部审批日志信息
                    if (newADAuditLogInfo != null)
                        hrauditLogDal.Add(newADAuditLogInfo, trans);  // 添加行政部审批日志信息

                    // 添加团队HR审批信息
                    ESP.HumanResource.DataAccess.DimissionHRDetailsDataProvider groupHRDetailsDal = new ESP.HumanResource.DataAccess.DimissionHRDetailsDataProvider();
                    if (hrDetailInfo != null)
                    {
                        if (hrDetailInfo.DimissionHRDetailId > 0)
                            groupHRDetailsDal.Update(hrDetailInfo, trans);
                        else
                            groupHRDetailsDal.Add(hrDetailInfo, trans);
                    }
                    if (employeeBase != null)
                    {
                        ESP.HumanResource.BusinessLogic.EmployeeBaseManager.Update(employeeBase, trans);  // 更新员工状态
                        ESP.HumanResource.Entity.UsersInfo userModel = ESP.HumanResource.BusinessLogic.UsersManager.GetModel(employeeBase.UserID);
                        userModel.Username = "$" + userModel.Username;
                        userModel.Email = "$" + userModel.Email;
                        ESP.HumanResource.BusinessLogic.UsersManager.Update(userModel, trans);
                    }
                    if (dimissionModel != null)
                    {
                        if (dimissionModel.id > 0)
                            ESP.HumanResource.BusinessLogic.DimissionManager.Update(dimissionModel, trans);
                        else
                            new ESP.HumanResource.DataAccess.DimissionDataProvider().Add(dimissionModel, trans.Connection, trans);
                    }

                    if (adDetailInfo != null)
                    {
                        ESP.HumanResource.DataAccess.DimissionADDetailsDataProvider adDal = new ESP.HumanResource.DataAccess.DimissionADDetailsDataProvider();
                        adDal.Add(adDetailInfo, trans);
                    }

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add("HumanResource", ex.ToString(), ESP.Logging.LogLevel.Error, ex);
                    trans.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 财务审批通过
        /// </summary>
        /// <param name="model">离职单信息</param>
        /// <param name="hrAuditLogInfo">审批日志信息</param>
        /// <returns>如果操作成功返回true，否则返回false</returns>
        public static bool Update(ESP.HumanResource.Entity.DimissionFormInfo model,
            ESP.HumanResource.Entity.HRAuditLogInfo oldHRAuditLogInfo,
            ESP.HumanResource.Entity.HRAuditLogInfo newHRAuditLogInfo,
            ESP.HumanResource.Entity.DimissionFinanceDetailsInfo financeDetailInfo)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    dal.Update(model, trans);  // 保存离职单信息

                    // 保存审批日志信息
                    ESP.HumanResource.DataAccess.HRAuditLogProvider hrauditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
                    hrauditLogDal.Update(oldHRAuditLogInfo, trans);  // 修改上一级审批日志信息
                    hrauditLogDal.Add(newHRAuditLogInfo, trans);  // 添加下一级审批日志信息

                    // 添加团队HR审批信息
                    ESP.HumanResource.DataAccess.DimissionFinanceDetailsDataProvider groupHRDetailsDal = new ESP.HumanResource.DataAccess.DimissionFinanceDetailsDataProvider();
                    if (financeDetailInfo.DimissionFinanceDetailId > 0)
                        groupHRDetailsDal.Update(financeDetailInfo, trans);
                    else
                        groupHRDetailsDal.Add(financeDetailInfo, trans);

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add("HumanResource", ex.ToString(), ESP.Logging.LogLevel.Error, ex);
                    trans.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 财务审批通过
        /// </summary>
        /// <param name="model">离职单信息</param>
        /// <param name="hrAuditLogInfo">审批日志信息</param>
        /// <returns>如果操作成功返回true，否则返回false</returns>
        public static bool Update(ESP.HumanResource.Entity.DimissionFormInfo model,
            ESP.HumanResource.Entity.HRAuditLogInfo oldHRAuditLogInfo,
            List<ESP.HumanResource.Entity.HRAuditLogInfo> newHRAuditLogList,
            ESP.HumanResource.Entity.DimissionFinanceDetailsInfo financeDetailInfo,
            ESP.HumanResource.Entity.EmployeeBaseInfo empBase)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    dal.Update(model, trans);  // 保存离职单信息

                    // 保存审批日志信息
                    ESP.HumanResource.DataAccess.HRAuditLogProvider hrauditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
                    hrauditLogDal.Update(oldHRAuditLogInfo, trans);  // 修改上一级审批日志信息
                    if (newHRAuditLogList != null && newHRAuditLogList.Count > 0)
                    {
                        foreach (ESP.HumanResource.Entity.HRAuditLogInfo newHRAuditLogInfo in newHRAuditLogList)
                        {
                            hrauditLogDal.Add(newHRAuditLogInfo, trans);  // 添加下一级审批日志信息
                        }
                    }

                    // 添加团队HR审批信息
                    ESP.HumanResource.DataAccess.DimissionFinanceDetailsDataProvider groupHRDetailsDal = new ESP.HumanResource.DataAccess.DimissionFinanceDetailsDataProvider();
                    if (financeDetailInfo.DimissionFinanceDetailId > 0)
                        groupHRDetailsDal.Update(financeDetailInfo, trans);
                    else
                        groupHRDetailsDal.Add(financeDetailInfo, trans);

                    ESP.HumanResource.BusinessLogic.EmployeeBaseManager.Update(empBase, trans);  // 更新员工状态

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add("HumanResource", ex.ToString(), ESP.Logging.LogLevel.Error, ex);
                    trans.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// IT部审批通过
        /// </summary>
        /// <param name="model">离职单信息</param>
        /// <param name="hrAuditLogInfo">审批日志信息</param>
        /// <returns>如果操作成功返回true，否则返回false</returns>
        public static bool Update(ESP.HumanResource.Entity.DimissionFormInfo model,
            ESP.HumanResource.Entity.HRAuditLogInfo oldHRAuditLogInfo,
            ESP.HumanResource.Entity.HRAuditLogInfo newHRAuditLogInfo,
            ESP.HumanResource.Entity.DimissionITDetailsInfo itDetailInfo)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    dal.Update(model, trans);  // 保存离职单信息

                    // 保存审批日志信息
                    ESP.HumanResource.DataAccess.HRAuditLogProvider hrauditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
                    hrauditLogDal.Update(oldHRAuditLogInfo, trans);  // 修改上一级审批日志信息
                    if (newHRAuditLogInfo != null)
                        hrauditLogDal.Add(newHRAuditLogInfo, trans);  // 添加下一级审批日志信息

                    // 添加团队HR审批信息
                    ESP.HumanResource.DataAccess.DimissionITDetailsDataProvider groupHRDetailsDal = new ESP.HumanResource.DataAccess.DimissionITDetailsDataProvider();
                    groupHRDetailsDal.Add(itDetailInfo, trans);

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add("HumanResource", ex.ToString(), ESP.Logging.LogLevel.Error, ex);
                    trans.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// IT部审批通过
        /// </summary>
        /// <param name="model">离职单信息</param>
        /// <param name="hrAuditLogInfo">审批日志信息</param>
        /// <returns>如果操作成功返回true，否则返回false</returns>
        public static bool Update(ESP.HumanResource.Entity.DimissionFormInfo model,
            ESP.HumanResource.Entity.HRAuditLogInfo oldHRAuditLogInfo,
            List<ESP.HumanResource.Entity.HRAuditLogInfo> newHRAuditLogList,
            ESP.HumanResource.Entity.DimissionITDetailsInfo itDetailInfo,
            List<ESP.HumanResource.Entity.DimissionIndemnityInfo> indemnityInfoList)
        {
            ESP.HumanResource.Entity.EmployeeBaseInfo empModel = EmployeeBaseManager.GetModel(model.UserId);

            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    dal.Update(model, trans);  // 保存离职单信息

                    // 保存审批日志信息
                    ESP.HumanResource.DataAccess.HRAuditLogProvider hrauditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
                    hrauditLogDal.Update(oldHRAuditLogInfo, trans);  // 修改上一级审批日志信息
                    if (newHRAuditLogList != null && newHRAuditLogList.Count > 0)
                    {
                        foreach (ESP.HumanResource.Entity.HRAuditLogInfo newHRAuditLogInfo in newHRAuditLogList)
                        {
                            if (newHRAuditLogInfo != null)
                                hrauditLogDal.Add(newHRAuditLogInfo, trans);  // 添加下一级审批日志信息
                        }
                    }
                    // 添加团队HR审批信息
                    ESP.HumanResource.DataAccess.DimissionITDetailsDataProvider groupHRDetailsDal = new ESP.HumanResource.DataAccess.DimissionITDetailsDataProvider();
                    if (itDetailInfo.DimissionITDetailId == 0)
                        groupHRDetailsDal.Add(itDetailInfo, trans);

                    // 停止离职员工的笔记本报销
                    ESP.Administrative.DataAccess.RefundDataProvider refund = new ESP.Administrative.DataAccess.RefundDataProvider();
                    ESP.Administrative.Entity.RefundInfo refundModel =
                        refund.GetEnableRefundList(model.UserId, ESP.Administrative.Common.RefundType.NetBookType,
                        ESP.Administrative.Common.RefundStatus.BeginStatus);
                    if (refundModel != null)
                    {
                        ESP.Framework.Entity.UserInfo userInfo = ESP.Framework.BusinessLogic.UserManager.Get();
                        refundModel.EndOperator = userInfo.UserID.ToString();
                        refundModel.EndTime = model.LastDay;
                        refundModel.LastUpdateTime = DateTime.Now;
                        refundModel.LastUpdater = userInfo.FullNameCN;
                        refundModel.LastUpdaterIP = HttpContext.Current.Request.UserHostAddress;
                        refundModel.Status = (int)ESP.Administrative.Common.RefundStatus.EndStatus;
                        refund.Update(refundModel, trans.Connection, trans);
                    }

                    //释放分机号
                    if (!string.IsNullOrEmpty(empModel.Phone1))
                    {
                        ESP.HumanResource.Entity.TELInfo tel = ESP.HumanResource.BusinessLogic.TelManager.GetModel(empModel.Phone1);
                        if (tel != null)
                        {
                            tel.Status = 1;
                            TelManager.Update(tel, trans.Connection, trans);
                        }
                    }

                    if (indemnityInfoList != null && indemnityInfoList.Count > 0)
                    {
                        ESP.HumanResource.DataAccess.DimissionIndemnityDataProvider indemnityDal = new ESP.HumanResource.DataAccess.DimissionIndemnityDataProvider();
                        foreach (ESP.HumanResource.Entity.DimissionIndemnityInfo indemnityInfo in indemnityInfoList)
                        {
                            indemnityDal.Add(indemnityInfo, trans);
                        }
                    }

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add("HumanResource", ex.ToString(), ESP.Logging.LogLevel.Error, ex);
                    trans.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 集团审批通过
        /// </summary>
        /// <param name="model">离职单信息</param>
        /// <param name="hrAuditLogInfo">审批日志信息</param>
        /// <returns>如果操作成功返回true，否则返回false</returns>
        public static bool Update(ESP.HumanResource.Entity.DimissionFormInfo model,
            ESP.HumanResource.Entity.HRAuditLogInfo oldHRAuditLogInfo,
            ESP.HumanResource.Entity.HRAuditLogInfo newHRAuditLogInfo,
            ESP.HumanResource.Entity.DimissionADDetailsInfo adDetailInfo)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    dal.Update(model, trans);  // 保存离职单信息

                    // 保存审批日志信息
                    ESP.HumanResource.DataAccess.HRAuditLogProvider hrauditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
                    hrauditLogDal.Update(oldHRAuditLogInfo, trans);  // 修改上一级审批日志信息
                    if (newHRAuditLogInfo != null)
                        hrauditLogDal.Add(newHRAuditLogInfo, trans);  // 添加下一级审批日志信息

                    // 添加团队HR审批信息
                    ESP.HumanResource.DataAccess.DimissionADDetailsDataProvider groupHRDetailsDal = new ESP.HumanResource.DataAccess.DimissionADDetailsDataProvider();
                    groupHRDetailsDal.Add(adDetailInfo, trans);

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add("HumanResource", ex.ToString(), ESP.Logging.LogLevel.Error, ex);
                    trans.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 财务总监审批通过，修改离职单信息并添加待下级审批人日志
        /// </summary>
        /// <param name="model">离职单信息</param>
        /// <param name="hrAuditLogInfo">审批日志信息</param>
        /// <returns>如果操作成功返回true，否则返回false</returns>
        public static bool Update(ESP.HumanResource.Entity.DimissionFormInfo model,
            ESP.HumanResource.Entity.HRAuditLogInfo oldHRAuditLogInfo,
            List<ESP.HumanResource.Entity.HRAuditLogInfo> newHRAuditLogList)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    dal.Update(model, trans);  // 保存离职单信息

                    // 保存审批日志信息
                    ESP.HumanResource.DataAccess.HRAuditLogProvider hrauditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
                    hrauditLogDal.Update(oldHRAuditLogInfo, trans);  // 修改上一级审批日志信息
                    if (newHRAuditLogList != null && newHRAuditLogList.Count > 0)
                    {
                        foreach (ESP.HumanResource.Entity.HRAuditLogInfo newHRAuditLogInfo in newHRAuditLogList)
                        {
                            hrauditLogDal.Add(newHRAuditLogInfo, trans);  // 添加下一级审批日志信息
                        }
                    }

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add("HumanResource", ex.ToString(), ESP.Logging.LogLevel.Error, ex);
                    trans.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(int DimissionId)
        {
            dal.Delete(DimissionId);
        }

        public static string GetFinanceAuditList(int auditId, int deptid)
        {
            return dal.GetFinanceAuditList(auditId, deptid);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.HumanResource.Entity.DimissionFormInfo GetModel(int DimissionId)
        {
            return dal.GetModel(DimissionId);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetAllList()
        {
            return dal.GetList("");
        }

        /// <summary>
        /// 通过用户编号用户的离职单
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <returns>返回用户的离职单信息</returns>
        public static ESP.HumanResource.Entity.DimissionFormInfo GetModelByUserid(int userid)
        {
            return dal.GetModelByUserid(userid);
        }

        /// <summary>
        /// 获得审批的离职单信息
        /// </summary>
        /// <param name="auditId">审批人编号</param>
        public static DataSet GetWaitAuditList(int auditId, string strWhere, List<SqlParameter> prarameters)
        {
            return dal.GetWaitAuditList(auditId, strWhere, prarameters);
        }

        /// <summary>
        /// 计算甚于年假数
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="expiryDate">截止日期</param>
        /// <returns>返回剩余年假数</returns>
        public static double GetAnnualLeaveInfo(int userId, DateTime expiryDate, out double canUse, out double used, out double annualBase)
        {
            canUse = 0;
            used = 0;
            annualBase = 0;
            // 入职日期
            try
            {
                ESP.HumanResource.Entity.EmployeeJobInfo employeeJobInfo = ESP.HumanResource.BusinessLogic.EmployeeJobManager.getModelBySysId(userId);
                ESP.HumanResource.Entity.EmployeeBaseInfo empModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(userId);

                // 用户考勤基础信息
                ESP.Administrative.BusinessLogic.UserAttBasicInfoManager userAttBasicMan = new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager();
                ESP.Administrative.Entity.UserAttBasicInfo userAttBasicInfo = userAttBasicMan.GetLastUserAttBasicInfo(userId);

                ESP.Administrative.BusinessLogic.ALAndRLManager alandrlMan = new ESP.Administrative.BusinessLogic.ALAndRLManager();
                ESP.Administrative.Entity.ALAndRLInfo alandrlInfo =
                    alandrlMan.GetALAndRLModel(userId, expiryDate.Year, (int)ESP.Administrative.Common.AAndRLeaveType.AnnualType);

                //ESP.Administrative.Entity.ALAndRLInfo rewardInfo =
                //    alandrlMan.GetALAndRLModel(userId, expiryDate.Year, (int)ESP.Administrative.Common.AAndRLeaveType.RewardType);

                annualBase = (double)alandrlInfo.LeaveNumber;

                DateTime EntryTime = new DateTime(expiryDate.Year, 1, 1);
                // decimal annualLeaveBase =5;

                int OneYearDays = 365;
                if (DateTime.IsLeapYear(expiryDate.Year))
                {
                    OneYearDays = 366;
                }

                //if (employeeJobInfo != null)
                //{
                if (empModel.JoinDate.Value.Year == expiryDate.Year)
                {
                    EntryTime = empModel.JoinDate.Value;
                    //annualBase = empModel.AnnualLeaveBase;
                    annualBase = 0;
                    //TimeSpan ts = new DateTime(employeeJobInfo.joinDate.Year, 12, 31)-(employeeJobInfo.joinDate);
                    //OneYearDays = ts.Days+1;
                }
                else
                {
                    annualBase = (double)(alandrlInfo == null ? 0 : alandrlInfo.LeaveNumber);
                }
                //}
                // 判断当前年份是否

                DateTime endDay = expiryDate.AddDays(1);
                TimeSpan span = endDay - EntryTime;


                // 截止离职日期所享有的年假总数
                double totalAnnualDays = span.TotalDays / OneYearDays * ((double)annualBase);
                canUse = totalAnnualDays;

                //int tempdays = (int)totalAnnualDays;
                //if ((tempdays + 0.5) >= totalAnnualDays)
                //    totalAnnualDays = tempdays;
                //else
                //    totalAnnualDays = tempdays + 0.5;



                // 已休年假总数
                used = (double)(alandrlInfo.LeaveNumber - alandrlInfo.RemainingNumber);

                double remainAnnual = totalAnnualDays - used;  // 剩余年假数

                if (alandrlInfo != null && alandrlInfo.LeaveNumber == 0)
                    remainAnnual = 0;

                annualBase = (double)alandrlInfo.LeaveNumber;

                return remainAnnual;
            }
            catch
            {
                return 0;
            }

        }


        //public static double GetAnnualTotalByDimission(int userId, DateTime expiryDate)
        //{
        //    // 入职日期
        //    try
        //    {
        //        ESP.HumanResource.Entity.EmployeeJobInfo employeeJobInfo = ESP.HumanResource.BusinessLogic.EmployeeJobManager.getModelBySysId(userId);
        //        // 用户考勤基础信息
        //        ESP.Administrative.BusinessLogic.UserAttBasicInfoManager userAttBasicMan = new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager();
        //        ESP.Administrative.Entity.UserAttBasicInfo userAttBasicInfo = userAttBasicMan.GetLastUserAttBasicInfo(userId);

        //        ESP.Administrative.BusinessLogic.ALAndRLManager alandrlMan = new ESP.Administrative.BusinessLogic.ALAndRLManager();
        //        ESP.Administrative.Entity.ALAndRLInfo alandrlInfo =
        //            alandrlMan.GetALAndRLModel(userId, expiryDate.Year, (int)ESP.Administrative.Common.AAndRLeaveType.AnnualType);

        //        DateTime EntryTime = new DateTime(expiryDate.Year, 1, 1);
        //        decimal annualLeaveBase = 5;
        //        if (employeeJobInfo != null)
        //        {
        //            if (employeeJobInfo.joinDate.Year == expiryDate.Year)
        //            {
        //                EntryTime = employeeJobInfo.joinDate;
        //                annualLeaveBase = alandrlInfo.LeaveNumber;
        //            }
        //            else
        //            {
        //                annualLeaveBase = alandrlInfo == null ? 0 : alandrlInfo.LeaveNumber;
        //            }
        //        }
        //        // 判断当前年份是否
        //        int OneYearDays = 365;
        //        if (DateTime.IsLeapYear(expiryDate.Year))
        //        {
        //            OneYearDays = 366;
        //        }
        //        DateTime endDay = expiryDate.AddDays(1);
        //        TimeSpan span = endDay - EntryTime;

        //        // 截止离职日期所享有的年假总数
        //        double totalAnnualDays = span.TotalDays / OneYearDays * ((double)annualLeaveBase);

        //        int tempdays = (int)totalAnnualDays;

        //        if ((tempdays + 0.5) >= totalAnnualDays)
        //            totalAnnualDays = tempdays;
        //        else
        //            totalAnnualDays = tempdays + 0.5;

        //        return totalAnnualDays;
        //    }
        //    catch
        //    {
        //        return 0;
        //    }

        //}


        /// <summary>
        /// 计算福利假数
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="expiryDate">截止日期</param>
        /// <returns>返回剩余福利假数</returns>
        public static double GetRewardLeaveInfo(int userId, DateTime expiryDate, out double canUse, out double used, out double rewardBase)
        {
            canUse = 0;
            used = 0;
            rewardBase = 0;
            try
            {
                ESP.HumanResource.Entity.EmployeeJobInfo employeeJobInfo = ESP.HumanResource.BusinessLogic.EmployeeJobManager.getModelBySysId(userId);
                ESP.HumanResource.Entity.EmployeeBaseInfo empModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(userId);

                // 用户考勤基础信息
                ESP.Administrative.BusinessLogic.UserAttBasicInfoManager userAttBasicMan = new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager();
                ESP.Administrative.Entity.UserAttBasicInfo userAttBasicInfo = userAttBasicMan.GetLastUserAttBasicInfo(userId);

                ESP.Administrative.BusinessLogic.ALAndRLManager alandrlMan = new ESP.Administrative.BusinessLogic.ALAndRLManager();

                ESP.Administrative.Entity.ALAndRLInfo rewardInfo =
                    alandrlMan.GetALAndRLModel(userId, expiryDate.Year, (int)ESP.Administrative.Common.AAndRLeaveType.RewardType);

                rewardBase = (double)rewardInfo.LeaveNumber;
                // 判断当前年份是否
                int OneYearDays = 365;
                if (DateTime.IsLeapYear(expiryDate.Year))
                {
                    OneYearDays = 366;
                }
                DateTime EntryTime = new DateTime(expiryDate.Year, 1, 1);
                decimal annualLeaveBase = 0;
                //if (employeeJobInfo != null)
                //{
                if (empModel.JoinDate.Value.Year == expiryDate.Year)
                {
                    EntryTime = empModel.JoinDate.Value;
                    annualLeaveBase = 5;
                    //TimeSpan ts = new DateTime(employeeJobInfo.joinDate.Year, 12, 31) - (employeeJobInfo.joinDate);
                    //OneYearDays = ts.Days + 1;
                }
                else
                {
                    annualLeaveBase = rewardInfo == null ? 0 : rewardInfo.LeaveNumber;
                }
                //}

                DateTime endDay = expiryDate.AddDays(1);
                TimeSpan span = endDay - EntryTime;

                // 截止离职日期所享有的年假总数
                double totalAnnualDays = span.TotalDays / OneYearDays * ((double)annualLeaveBase);
                canUse = totalAnnualDays;

                //int tempdays = (int)totalAnnualDays;
                //if ((tempdays + 0.5) >= totalAnnualDays)
                //    totalAnnualDays = tempdays;
                //else
                //    totalAnnualDays = tempdays + 0.5;


                // 已休年假总数
                used = (double)(rewardInfo.LeaveNumber - rewardInfo.RemainingNumber);
                double remainAnnual = totalAnnualDays - used;  // 剩余年假数

                if (rewardInfo != null && rewardInfo.LeaveNumber == 0)
                    remainAnnual = 0;

                return remainAnnual;
            }
            catch
            {
                return 0;
            }

        }

        /// <summary>
        /// 获得离职员工的集团人力资源部门信息
        /// </summary>
        /// <param name="dimissionDepId">离职员工部门编号</param>
        public static int GetUserHRDepartment(int dimissionDepId)
        {
            int deptid = int.Parse(System.Configuration.ConfigurationManager.AppSettings["BJDimissionHRAuditor"]);
            return deptid;
        }

        /// <summary>
        /// 获得已经提交的离职单信息
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="prarameters">查询参数</param>
        /// <returns>返回离职单信息</returns>
        public static DataSet GetSubmitDimissionList(string strWhere, List<SqlParameter> prarameters)
        {
            return dal.GetSubmitDimissionList(strWhere, prarameters);
        }

        public static DataSet GetGroupDimissionList(string strwhere, string depId)
        {
            return dal.GetGroupDimissionList(strwhere, depId);
        }

        /// <summary>
        /// 获得离职最后一天未办完手续的离职单信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="prarameters"></param>
        /// <returns></returns>
        public static DataSet GetUnfinishedDimissionList(string strWhere, List<SqlParameter> prarameters)
        {
            return dal.GetUnfinishedDimissionList(strWhere, prarameters);
        }

        /// <summary>
        /// 获得已审批通过的离职单信息
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="parameter">查询参数</param>
        /// <returns>返回已审批的离职单信息</returns>
        public static DataSet GetAuditedDimissionList(string strWhere, List<SqlParameter> parameter)
        {
            return dal.GetAuditedDimissionList(strWhere, parameter);
        }

        public static DataSet GetAllAuditedDimissionList(int currentUserId, string strWhere, List<SqlParameter> parameter)
        {
            return dal.GetAllAuditedDimissionList(currentUserId, strWhere, parameter);
        }

        /// <summary>
        /// 设置离职交接单据信息
        /// </summary>
        /// <param name="DimissionId">离职单编号</param>
        /// <returns></returns>
        public static int DoDimission(int DimissionId)
        {
            int ret = 0;
            IList<ESP.HumanResource.Entity.DimissionDetailsInfo> detailList = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.GetModelList(" DimissionId=" + DimissionId.ToString());

            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (ESP.HumanResource.Entity.DimissionDetailsInfo model in detailList)
                    {
                        switch (model.FormType)
                        {
                            case "项目号":
                                {
                                    //变更项目号负责人
                                    ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(model.FormId, trans);
                                    string logdesc = "离职交接,项目号负责人由" + projectModel.ApplicantEmployeeName + "变更为" + model.ReceiverName;
                                    projectModel.ApplicantUserID = model.ReceiverId;
                                    projectModel.ApplicantEmployeeName = model.ReceiverName;
                                    ESP.Finance.BusinessLogic.ProjectManager.Update(projectModel, trans);
                                    //添加项目号成员
                                    IList<ESP.Finance.Entity.ProjectMemberInfo> memberlist = ESP.Finance.BusinessLogic.ProjectMemberManager.GetList(" projectId=" + projectModel.ProjectId.ToString() + " and MemberUserID=" + model.ReceiverId.ToString(), new List<SqlParameter>(), trans);
                                    if (memberlist == null || memberlist.Count == 0)
                                    {
                                        ESP.Finance.Entity.ProjectMemberInfo member = new ESP.Finance.Entity.ProjectMemberInfo();
                                        member.CreateTime = DateTime.Now;
                                        member.GroupID = model.ReceiverDepartmentId;
                                        member.GroupName = model.ReceiverDepartmentName;
                                        member.MemberUserID = model.ReceiverId;
                                        member.MemberEmployeeName = model.ReceiverName;
                                        member.ProjectId = projectModel.ProjectId;
                                        member.ProjectCode = projectModel.ProjectCode;
                                        ESP.Finance.BusinessLogic.ProjectMemberManager.Add(member, trans);
                                    }
                                    //增加接手人权限
                                    ESP.Purchase.Entity.DataInfo data = ESP.Purchase.BusinessLogic.DataPermissionManager.GetDataInfoModel((int)ESP.Purchase.Common.State.DataType.Project, model.FormId, trans);
                                    ESP.Purchase.Entity.DataPermissionInfo permission = new ESP.Purchase.Entity.DataPermissionInfo();
                                    permission.DataInfoId = data.Id;
                                    permission.IsEditor = true;
                                    permission.IsViewer = true;
                                    permission.UserId = model.ReceiverId;
                                    ESP.Purchase.BusinessLogic.DataPermissionManager.addDataPermissions(permission, trans);
                                    //增加日志
                                    ESP.Finance.Entity.AuditLogInfo log = new ESP.Finance.Entity.AuditLogInfo();
                                    log.FormType = (int)ESP.Finance.Utility.FormType.Project;
                                    log.FormID = projectModel.ProjectId;
                                    log.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.Tip;
                                    log.AuditDate = DateTime.Now;
                                    log.Suggestion = logdesc;
                                    ESP.Finance.BusinessLogic.AuditLogManager.Add(log, trans);

                                    break;
                                }
                            case "支持方":
                                {
                                    //变更支持方负责人
                                    ESP.Finance.Entity.SupporterInfo supporterModel = ESP.Finance.BusinessLogic.SupporterManager.GetModel(model.FormId, trans);
                                    string logdesc = "离职交接,支持方负责人由" + supporterModel.LeaderEmployeeName + "变更为" + model.ReceiverName;
                                    supporterModel.LeaderUserID = model.ReceiverId;
                                    supporterModel.LeaderEmployeeName = model.ReceiverName;
                                    ESP.Finance.BusinessLogic.SupporterManager.UpdateDimission(supporterModel, trans);
                                    //添加支持方成员
                                    IList<ESP.Finance.Entity.SupportMemberInfo> memberlist = ESP.Finance.BusinessLogic.SupportMemberManager.GetList(" supportId=" + supporterModel.SupportID.ToString() + " and MemberUserID=" + model.ReceiverId.ToString(), new List<SqlParameter>(), trans);
                                    if (memberlist == null || memberlist.Count == 0)
                                    {
                                        ESP.Finance.Entity.SupportMemberInfo member = new ESP.Finance.Entity.SupportMemberInfo();
                                        member.CreateTime = DateTime.Now;
                                        member.GroupID = model.ReceiverDepartmentId;
                                        member.GroupName = model.ReceiverDepartmentName;
                                        member.MemberUserID = model.ReceiverId;
                                        member.MemberEmployeeName = model.ReceiverName;
                                        member.SupportID = supporterModel.SupportID;
                                        ESP.Finance.BusinessLogic.SupportMemberManager.Add(member, trans);
                                    }
                                    //增加接手人权限
                                    ESP.Purchase.Entity.DataInfo data = ESP.Purchase.BusinessLogic.DataPermissionManager.GetDataInfoModel((int)ESP.Purchase.Common.State.DataType.Supporter, model.FormId, trans);
                                    ESP.Purchase.Entity.DataPermissionInfo permission = new ESP.Purchase.Entity.DataPermissionInfo();
                                    permission.DataInfoId = data.Id;
                                    permission.IsEditor = true;
                                    permission.IsViewer = true;
                                    permission.UserId = model.ReceiverId;
                                    ESP.Purchase.BusinessLogic.DataPermissionManager.addDataPermissions(permission, trans);
                                    //增加日志
                                    ESP.Finance.Entity.AuditLogInfo log = new ESP.Finance.Entity.AuditLogInfo();
                                    log.FormType = (int)ESP.Finance.Utility.FormType.Supporter;
                                    log.FormID = supporterModel.SupportID;
                                    log.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.Tip;
                                    log.AuditDate = DateTime.Now;
                                    log.Suggestion = logdesc;
                                    ESP.Finance.BusinessLogic.AuditLogManager.Add(log, trans);

                                    break;
                                }
                            case "PR单":
                                {

                                    ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(model.FormId, trans);
                                    string logdesc = "离职交接,PR申请人由" + generalModel.requestorname + "变更为" + model.ReceiverName;
                                    generalModel.requestor = model.ReceiverId;
                                    generalModel.requestorname = model.ReceiverName;
                                    ESP.Purchase.BusinessLogic.GeneralInfoManager.Update(generalModel, trans.Connection, trans);

                                    //增加接手人权限
                                    ESP.Purchase.Entity.DataInfo data = ESP.Purchase.BusinessLogic.DataPermissionManager.GetDataInfoModel((int)ESP.Purchase.Common.State.DataType.PR, model.FormId, trans);
                                    if (data != null)
                                    {
                                        ESP.Purchase.Entity.DataPermissionInfo permission = new ESP.Purchase.Entity.DataPermissionInfo();
                                        permission.DataInfoId = data.Id;
                                        permission.IsEditor = true;
                                        permission.IsViewer = true;
                                        permission.UserId = model.ReceiverId;
                                        ESP.Purchase.BusinessLogic.DataPermissionManager.addDataPermissions(permission, trans);
                                    }
                                    //增加日志
                                    ESP.Purchase.Entity.LogInfo log = new ESP.Purchase.Entity.LogInfo();
                                    log.Des = logdesc;
                                    log.Gid = generalModel.id;
                                    log.PrNo = generalModel.PrNo;
                                    log.Status = 0;
                                    log.LogUserId = 0;
                                    log.LogMedifiedTeme = DateTime.Now;
                                    ESP.Purchase.BusinessLogic.LogManager.AddLog(log, trans);

                                    IList<ESP.Finance.Entity.ReturnInfo> returnList = ESP.Finance.BusinessLogic.ReturnManager.GetDimissionList("prid=" + generalModel.id.ToString(), trans);
                                    foreach (ESP.Finance.Entity.ReturnInfo returnModel in returnList)
                                    {
                                        logdesc = "离职交接,PN申请人由" + returnModel.RequestEmployeeName + "变更为" + model.ReceiverName;
                                        returnModel.RequestorID = model.ReceiverId;
                                        returnModel.RequestEmployeeName = model.ReceiverName;
                                        ESP.Finance.BusinessLogic.ReturnManager.UpdateDismission(returnModel, trans);
                                        //增加接手人权限
                                        ESP.Purchase.Entity.DataInfo returndata = ESP.Purchase.BusinessLogic.DataPermissionManager.GetDataInfoModel((int)ESP.Purchase.Common.State.DataType.Return, returnModel.ReturnID, trans);
                                        ESP.Purchase.Entity.DataPermissionInfo returnpermission = new ESP.Purchase.Entity.DataPermissionInfo();
                                        returnpermission.DataInfoId = returndata.Id;
                                        returnpermission.IsEditor = true;
                                        returnpermission.IsViewer = true;
                                        returnpermission.UserId = model.ReceiverId;
                                        ESP.Purchase.BusinessLogic.DataPermissionManager.addDataPermissions(returnpermission, trans);

                                        ESP.Finance.Entity.AuditLogInfo auditlog = new ESP.Finance.Entity.AuditLogInfo();
                                        auditlog.FormType = (int)ESP.Finance.Utility.FormType.Return;
                                        auditlog.FormID = returnModel.ReturnID;
                                        auditlog.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.Tip;
                                        auditlog.AuditDate = DateTime.Now;
                                        auditlog.Suggestion = logdesc;
                                        ESP.Finance.BusinessLogic.AuditLogManager.Add(auditlog, trans);
                                    }
                                    break;
                                }
                            case "收货":
                                {
                                    ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(model.FormId, trans);
                                    string logdesc = "离职交接,收货人由" + generalModel.receivername + "变更为" + model.ReceiverName;
                                    generalModel.goods_receiver = model.ReceiverId;
                                    generalModel.receivername = model.ReceiverName;
                                    ESP.Purchase.BusinessLogic.GeneralInfoManager.Update(generalModel, trans.Connection, trans);

                                    //增加接手人权限
                                    ESP.Purchase.Entity.DataInfo data = ESP.Purchase.BusinessLogic.DataPermissionManager.GetDataInfoModel((int)ESP.Purchase.Common.State.DataType.PR, model.FormId, trans);
                                    if (data != null)
                                    {
                                        ESP.Purchase.Entity.DataPermissionInfo permission = new ESP.Purchase.Entity.DataPermissionInfo();
                                        permission.DataInfoId = data.Id;
                                        permission.IsEditor = true;
                                        permission.IsViewer = true;
                                        permission.UserId = model.ReceiverId;
                                        ESP.Purchase.BusinessLogic.DataPermissionManager.addDataPermissions(permission, trans);
                                    }
                                    //增加日志
                                    ESP.Purchase.Entity.LogInfo log = new ESP.Purchase.Entity.LogInfo();
                                    log.Des = logdesc;
                                    log.Gid = generalModel.id;
                                    log.PrNo = generalModel.PrNo;
                                    log.Status = 0;
                                    log.LogUserId = 0;
                                    log.LogMedifiedTeme = DateTime.Now;
                                    ESP.Purchase.BusinessLogic.LogManager.AddLog(log, trans);
                                    break;
                                }
                            case "附加收货":
                                {
                                    ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(model.FormId, trans);
                                    string logdesc = "离职交接,附加收货人由" + generalModel.appendReceiverName + "变更为" + model.ReceiverName;
                                    generalModel.appendReceiver = model.ReceiverId;
                                    generalModel.appendReceiverName = model.ReceiverName;
                                    generalModel.appendReceiverGroup = model.ReceiverDepartmentName;

                                    ESP.Purchase.BusinessLogic.GeneralInfoManager.Update(generalModel, trans.Connection, trans);

                                    //增加接手人权限
                                    ESP.Purchase.Entity.DataInfo data = ESP.Purchase.BusinessLogic.DataPermissionManager.GetDataInfoModel((int)ESP.Purchase.Common.State.DataType.PR, model.FormId, trans);
                                    if (data != null)
                                    {
                                        ESP.Purchase.Entity.DataPermissionInfo permission = new ESP.Purchase.Entity.DataPermissionInfo();
                                        permission.DataInfoId = data.Id;
                                        permission.IsEditor = true;
                                        permission.IsViewer = true;
                                        permission.UserId = model.ReceiverId;
                                        ESP.Purchase.BusinessLogic.DataPermissionManager.addDataPermissions(permission, trans);
                                    }
                                    //增加日志
                                    ESP.Purchase.Entity.LogInfo log = new ESP.Purchase.Entity.LogInfo();
                                    log.Des = logdesc;
                                    log.Gid = generalModel.id;
                                    log.PrNo = generalModel.PrNo;
                                    log.Status = 0;
                                    log.LogUserId = 0;
                                    log.LogMedifiedTeme = DateTime.Now;
                                    ESP.Purchase.BusinessLogic.LogManager.AddLog(log, trans);
                                    break;
                                }

                        }
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message);
                }
                return ret;
            }
        }

        public static void SendProjectMail(int dimissionId)
        {
            int ret = 0;
            IList<ESP.HumanResource.Entity.DimissionDetailsInfo> detailList = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.GetModelList(" FormType in('项目号','支持方') and DimissionId=" + dimissionId.ToString());
            foreach (ESP.HumanResource.Entity.DimissionDetailsInfo model in detailList)
            {
                ESP.Finance.Entity.ProjectInfo projectModel = null;
                string desc = string.Empty;
                if (model.FormType == "项目号")
                {
                    projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(model.FormId);
                    desc = projectModel.ProjectCode + ": 项目号（" + projectModel.GroupName + "）负责人于 " + DateTime.Now.ToString("yyyy-MM-dd") + " 由 " + model.UserName + " 变更为 " + model.ReceiverName;
                }
                else
                {
                    ESP.Finance.Entity.SupporterInfo supportModel = ESP.Finance.BusinessLogic.SupporterManager.GetModel(model.FormId);
                    projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(supportModel.ProjectID);
                    desc = projectModel.ProjectCode + ": 支持方（" + supportModel.GroupName + "）负责人于 " + DateTime.Now.ToString("yyyy-MM-dd") + " 由 " + model.UserName + " 变更为 " + model.ReceiverName;
                }
                ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModel(projectModel.BranchID.Value);
                ESP.Compatible.Employee paymentEmp = new ESP.Compatible.Employee(branchModel.ProjectAccounter);
                ESP.Compatible.Employee projectEmp = null;
                List<System.Net.Mail.MailAddress> recipients = new List<System.Net.Mail.MailAddress>();
                recipients.Add(new System.Net.Mail.MailAddress(paymentEmp.EMail));
                if (branchModel.PaymentAccounter != branchModel.ProjectAccounter)
                {
                    projectEmp = new ESP.Compatible.Employee(branchModel.ProjectAccounter);
                    recipients.Add(new System.Net.Mail.MailAddress(projectEmp.EMail));
                }
                ESP.Mail.MailManager.Send("离职项目号/支持方交接", desc, false, recipients.ToArray());
            }
        }
        #endregion  成员方法
    }
}
