using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ESP.HumanResource.BusinessLogic
{
    public class DimissionDetailsManager
    {
        private static readonly ESP.HumanResource.DataAccess.DimissionDetailsProvider dal = new ESP.HumanResource.DataAccess.DimissionDetailsProvider();
        public DimissionDetailsManager()
        { }
        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public static int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(int DimissionDetailId)
        {
            return dal.Exists(DimissionDetailId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ESP.HumanResource.Entity.DimissionDetailsInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(ESP.HumanResource.Entity.DimissionDetailsInfo model, System.Data.SqlClient.SqlTransaction trans)
        {
            dal.Update(model, trans);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(ESP.HumanResource.Entity.DimissionDetailsInfo model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(int DimissionDetailId)
        {
            dal.Delete(DimissionDetailId);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.HumanResource.Entity.DimissionDetailsInfo GetModel(int DimissionDetailId)
        {
            return dal.GetModel(DimissionDetailId);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        public static IList<ESP.HumanResource.Entity.DimissionDetailsInfo> GetModelList(string strWhere)
        {
            return dal.GetModelList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetAllList()
        {
            return dal.GetList("");
        }

        /// <summary>
        /// 获得离职用户未处理单据信息
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <param name="dimissionId">离职单编号</param>
        /// <returns>返回未处理单据信息</returns>
        public static List<ESP.HumanResource.Entity.DimissionDetailsInfo> GetDimissionData(int userid, int dimissionId)
        {
            List<ESP.HumanResource.Entity.DimissionDetailsInfo> list = dal.GetDimissionDataByDimissionId(dimissionId);
            if (list != null && list.Count > 0)
            {
                return list;
            }

            list = dal.GetDimissionDataByUserId(userid);

            ESP.HumanResource.Entity.DimissionDetailsInfo ol = new ESP.HumanResource.Entity.DimissionDetailsInfo();
            ol.CreateTime = DateTime.Now;
            ol.Description = "线下业务相关事宜、数据、文档等资料交接";
            ol.DimissionId = dimissionId;
            ol.FormCode = "";
            ol.FormId = 0;
            ol.FormStatus = 0;
            ol.FormType = "线下业务交接";
            ol.ProjectCode = "";
            ol.ProjectId = 0;
            ol.Status = 0;
            ol.TotalPrice = 0;
            ol.UpdateStatus = 0;
            ol.Url = "";
            ol.UserId = userid;
            ol.UserName = "";
            ol.Website = "";

            list.Insert(0, ol);

            return list;
        }

        /// <summary>
        /// 通过离职单编号获得未处理的离职单据信息
        /// </summary>
        /// <param name="dimissionId">离职单编号</param>
        /// <returns>返回未处理的离职单据信息</returns>
        public static List<ESP.HumanResource.Entity.DimissionDetailsInfo> GetDimissionData(int dimissionId)
        {
            return dal.GetDimissionDataByDimissionId(dimissionId);
        }

        /// <summary>
        /// 获得用户报销单信息
        /// </summary>
        /// <param name="UserId">用户编号</param>
        /// <returns>返回用户报销单信息</returns>
        public static DataSet GetDimissionOOP(int UserId)
        {
            return dal.GetDimissionOOPByUserId(UserId);
        }

        /// <summary>
        /// 获得员工未处理的PR现金借款和现金借款
        /// </summary>
        /// <param name="UserId">用户编号</param>
        /// <returns>返回用户未处理的PR现金借款和现金借款</returns>
        public static DataSet GetDimissionTrafficFee(int UserId)
        {
            return dal.GetDimissionTrafficFeeByUserId(UserId);
        }

        /// <summary>
        /// 判断离职单是否处理等待某
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dimissionId"></param>
        /// <returns></returns>
        public static bool CheckDimissionData(int userId, int dimissionId)
        {
            ESP.HumanResource.DataAccess.DimissionFormProvider dimissionForm = new ESP.HumanResource.DataAccess.DimissionFormProvider();

            DataSet ds =
                dimissionForm.GetWaitAuditList(userId, " and DimissionId=@DimissionId ",
                new List<SqlParameter>() { new SqlParameter("@DimissionId", dimissionId) });
            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 保存离职未处理单据信息
        /// </summary>
        /// <param name="dimissionForm">离职单信息</param>
        /// <param name="dicDimission">未处理离职单据信息</param>
        /// <param name="hrAuditLogInfo">审批日志信息</param>
        /// <returns></returns>
        public static bool SavaDimissionDetailsInfo(ESP.HumanResource.Entity.DimissionFormInfo dimissionForm,
            Dictionary<string, ESP.HumanResource.Entity.DimissionDetailsInfo> dicDimission, ESP.HumanResource.Entity.HRAuditLogInfo hrAuditLogInfo)
        {
            ESP.Framework.Entity.DepartmentInfo departmentInfo =
 new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager().GetRootDepartmentID(dimissionForm.UserId);

            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    ESP.HumanResource.DataAccess.HRAuditLogProvider hrAuditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
                    hrAuditLogDal.Update(hrAuditLogInfo, trans);

                    // 保存交接单据信息
                    if (dicDimission != null && dicDimission.Count > 0)
                    {
                        foreach (KeyValuePair<string, ESP.HumanResource.Entity.DimissionDetailsInfo> pair in dicDimission)
                        {
                            pair.Value.CreateTime = DateTime.Now;
                            pair.Value.DimissionId = dimissionForm.DimissionId;
                            pair.Value.Status = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;
                            dal.Add(pair.Value, trans);
                        }
                    }
                    else
                    {
                        ESP.HumanResource.Entity.HRAuditLogInfo manangeAuditLogInfo = new ESP.HumanResource.Entity.HRAuditLogInfo();
                        
                        ESP.Framework.Entity.OperationAuditManageInfo manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(dimissionForm.UserId);
                        if (manageModel == null)
                            manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(dimissionForm.DepartmentId);

                        if (dimissionForm.PreAuditorId == dimissionForm.ManagerId)
                        {
                            List<ESP.HumanResource.Entity.HRAuditLogInfo> newHRAuditLogList = new List<ESP.HumanResource.Entity.HRAuditLogInfo>();

                            dimissionForm.Status = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitFinance;
                            dimissionForm.FinanceAuditStatus = 1; // 一级审批

                            string financeLevel1Users = "," + ESP.Finance.BusinessLogic.BranchManager.GetDimissionAuditors(departmentInfo.DepartmentID) + ",";
                            string[] financeLeve1UserArr = financeLevel1Users.Split(',');
                            foreach (string str in financeLeve1UserArr)
                            {
                                int level1UserId = 0;
                                if (int.TryParse(str, out level1UserId))
                                {
                                    ESP.Framework.Entity.UserInfo userModel = ESP.Framework.BusinessLogic.UserManager.Get(level1UserId);
                                    if (userModel != null)
                                    {
                                        ESP.HumanResource.Entity.HRAuditLogInfo newHRAuditLogInfo = new ESP.HumanResource.Entity.HRAuditLogInfo();
                                        newHRAuditLogInfo.AuditorId = userModel.UserID;
                                        newHRAuditLogInfo.AuditorName = userModel.FullNameCN;
                                        newHRAuditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;
                                        newHRAuditLogInfo.FormId = dimissionForm.DimissionId;
                                        newHRAuditLogInfo.FormType = (int)ESP.HumanResource.Common.HRFormType.DimissionForm;
                                        newHRAuditLogInfo.AuditLevel = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitFinance;

                                        hrAuditLogDal.Add(newHRAuditLogInfo, trans);
                                    }
                                }
                            }
                        }
                        else if (dimissionForm.PreAuditorId == dimissionForm.DirectorId)
                        {
                            manangeAuditLogInfo.AuditorId = manageModel.DimissionManagerId;  // 总经理审批
                            manangeAuditLogInfo.AuditorName = manageModel.DimissionManagerName;

                            dimissionForm.Status = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitManager;
                            manangeAuditLogInfo.AuditLevel = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitManager;

                            manangeAuditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;
                            manangeAuditLogInfo.FormId = dimissionForm.DimissionId;
                            manangeAuditLogInfo.FormType = (int)ESP.HumanResource.Common.HRFormType.DimissionForm;
                            hrAuditLogDal.Add(manangeAuditLogInfo, trans);
                        }
                        else
                        {
                            manangeAuditLogInfo.AuditorId = manageModel.DimissionDirectorid;  // 总监审批
                            manangeAuditLogInfo.AuditorName = manageModel.DimissionDirectorName;

                            dimissionForm.Status = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitDirector;
                            manangeAuditLogInfo.AuditLevel = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitDirector;

                            manangeAuditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;
                            manangeAuditLogInfo.FormId = dimissionForm.DimissionId;
                            manangeAuditLogInfo.FormType = (int)ESP.HumanResource.Common.HRFormType.DimissionForm;
                            hrAuditLogDal.Add(manangeAuditLogInfo, trans);
                        }
                       
                    }

                    ESP.HumanResource.DataAccess.DimissionFormProvider dimissionDal = new ESP.HumanResource.DataAccess.DimissionFormProvider();
                    dimissionDal.Update(dimissionForm, trans);  // 修改离职单信息
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
        /// 获得等待员工接收的离职单据信息
        /// </summary>
        /// <param name="userId">员工编号</param>
        /// <returns>返回用户离职未处理单据信息</returns>
        public static DataSet GetDimissionDetailsByUserId(int ReceiverId)
        {
            return dal.GetDimissionDetailsByUserId(ReceiverId);
        }

        /// <summary>
        /// 更新交接单信息同时检查是否需要修改离职单信息、添加审批记录
        /// </summary>
        /// <param name="detailInfo">交接单信息</param>
        public static bool UpdateDetailAndCheckDimission(ESP.HumanResource.Entity.DimissionDetailsInfo detailInfo)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    #region 修改交接单信息。
                    dal.Update(detailInfo, trans);
                    #endregion

                    #region 检查是否需要修改离职单信息（判断如果是最后一条交接单，就修改离职单状态，并转交下一人审批）。
                    int flag = 0;  // 标记是否需要修改离职单
                    List<ESP.HumanResource.Entity.DimissionDetailsInfo> detailList = dal.GetDimissionDataByDimissionId(detailInfo.DimissionId, trans);
                    if (detailList != null && detailList.Count > 0)
                    {
                        foreach (ESP.HumanResource.Entity.DimissionDetailsInfo detail in detailList)
                        {
                            if (detail.Status == (int)ESP.HumanResource.Common.AuditStatus.NotAudit && detail.DimissionDetailId != detailInfo.DimissionDetailId)
                            {
                                flag = 1;
                            }
                        }
                    }
                    if (flag == 0)
                    {
                        #region 修改离职单信息
                        ESP.HumanResource.DataAccess.DimissionFormProvider dimFormDal = new ESP.HumanResource.DataAccess.DimissionFormProvider();
                        ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = dimFormDal.GetModel(detailInfo.DimissionId);
                        #endregion

                        #region 添加审批日志
                        ESP.HumanResource.DataAccess.HRAuditLogProvider hrAuditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
                        ESP.HumanResource.Entity.HRAuditLogInfo hrAuditLogInfo = new ESP.HumanResource.Entity.HRAuditLogInfo();

                        ESP.Framework.Entity.OperationAuditManageInfo manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(dimissionFormInfo.UserId);
                        if (manageModel == null)
                            manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(dimissionFormInfo.DepartmentId);


                        if (dimissionFormInfo.PreAuditorId == dimissionFormInfo.ManagerId)
                        {
                            hrAuditLogInfo.AuditorId = manageModel.Hrattendanceid;  // 团队行政审批
                            hrAuditLogInfo.AuditorName = manageModel.Hrattendancename;
                            dimissionFormInfo.Status = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitGroupHR;
                            hrAuditLogInfo.AuditLevel = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitGroupHR;
                            flag = 2;
                        }
                        else if (dimissionFormInfo.DirectorId == dimissionFormInfo.PreAuditorId)
                        {
                            hrAuditLogInfo.AuditorId = manageModel.DimissionManagerId;  // 总经理审批
                            hrAuditLogInfo.AuditorName = manageModel.DimissionManagerName;
                            dimissionFormInfo.Status = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitManager;
                            hrAuditLogInfo.AuditLevel = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitManager;
                        }
                         else
                        {
                            hrAuditLogInfo.AuditorId = manageModel.DimissionDirectorid;  // 总监审批
                            hrAuditLogInfo.AuditorName = manageModel.DimissionDirectorName;
                            dimissionFormInfo.Status = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitManager;
                            hrAuditLogInfo.AuditLevel = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitManager;
                        }

                        hrAuditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;
                        hrAuditLogInfo.FormId = dimissionFormInfo.DimissionId;
                        hrAuditLogInfo.FormType = (int)ESP.HumanResource.Common.HRFormType.DimissionForm;
                        hrAuditLogDal.Add(hrAuditLogInfo, trans);
                        #endregion

                        dimFormDal.Update(dimissionFormInfo, trans);
                    }
                    #endregion

                    trans.Commit();
                    if (flag == 2)
                    {
                        // 总经理审批通过后修改相对应的交接单据信息
                        //ESP.HumanResource.BusinessLogic.DimissionFormManager.DoDimission(detailInfo.DimissionId);
                        // 发送项目号/支持方负责人变换的通知邮件。
                        //ESP.HumanResource.BusinessLogic.DimissionFormManager.SendProjectMail(detailInfo.DimissionId);
                    }
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
        /// 更新交接单信息同时检查是否需要修改离职单信息、添加审批记录
        /// </summary>
        /// <param name="detailInfo">交接单信息</param>
        public static bool UpdateDetailAndCheckDimission(List<ESP.HumanResource.Entity.DimissionDetailsInfo> detailInfos)
        {
            int tempFlag = 0;
            int dimissionId = 0;
            bool b = false;
            ESP.HumanResource.DataAccess.HRAuditLogProvider hrauditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
                    
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int flag = 0;  // 标记是否需要修改离职单
                    foreach (ESP.HumanResource.Entity.DimissionDetailsInfo detailInfo in detailInfos)
                    {
                        #region 修改交接单信息。
                        dal.Update(detailInfo, trans);
                        #endregion
                        dimissionId = detailInfo.DimissionId;

                        #region 检查是否需要修改离职单信息（判断如果是最后一条交接单，就修改离职单状态，并转交下一人审批）。
                        flag = 0;
                        List<ESP.HumanResource.Entity.DimissionDetailsInfo> detailList = dal.GetDimissionDataByDimissionId(detailInfo.DimissionId, trans);
                        if (detailList != null && detailList.Count > 0)
                        {
                            foreach (ESP.HumanResource.Entity.DimissionDetailsInfo detail in detailList)
                            {
                                if (detail.Status == (int)ESP.HumanResource.Common.AuditStatus.NotAudit && detail.DimissionDetailId != detailInfo.DimissionDetailId)
                                {
                                    flag = 1;
                                }
                            }
                        }
                        #endregion
                    }

                    if (flag == 0)
                    {
                        #region 修改离职单信息
                        ESP.HumanResource.DataAccess.DimissionFormProvider dimFormDal = new ESP.HumanResource.DataAccess.DimissionFormProvider();
                        ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = dimFormDal.GetModel(dimissionId);
                        ESP.Framework.Entity.DepartmentInfo departmentInfo = new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager().GetRootDepartmentID(dimissionFormInfo.UserId);

                        #endregion

                        #region 添加审批日志
                        ESP.HumanResource.DataAccess.HRAuditLogProvider hrAuditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
                        ESP.HumanResource.Entity.HRAuditLogInfo hrAuditLogInfo = new ESP.HumanResource.Entity.HRAuditLogInfo();
                        
                        ESP.Framework.Entity.OperationAuditManageInfo manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(dimissionFormInfo.UserId);
                        if (manageModel == null)
                            manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(dimissionFormInfo.DepartmentId);


                        if (dimissionFormInfo.PreAuditorId == manageModel.DimissionManagerId)
                        {
                            List<ESP.HumanResource.Entity.HRAuditLogInfo> newHRAuditLogList = new List<ESP.HumanResource.Entity.HRAuditLogInfo>();
       
                            dimissionFormInfo.Status = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitFinance;
                            dimissionFormInfo.FinanceAuditStatus = 1; // 一级审批

                            string financeLevel1Users = "," + ESP.Finance.BusinessLogic.BranchManager.GetDimissionAuditors(departmentInfo.DepartmentID) + ",";
                            string[] financeLeve1UserArr = financeLevel1Users.Split(',');
                            foreach (string str in financeLeve1UserArr)
                            {
                                int level1UserId = 0;
                                if (int.TryParse(str, out level1UserId))
                                {
                                    ESP.Framework.Entity.UserInfo userModel = ESP.Framework.BusinessLogic.UserManager.Get(level1UserId);
                                    if (userModel != null)
                                    {
                                        ESP.HumanResource.Entity.HRAuditLogInfo newHRAuditLogInfo = new ESP.HumanResource.Entity.HRAuditLogInfo();
                                        newHRAuditLogInfo.AuditorId = userModel.UserID;
                                        newHRAuditLogInfo.AuditorName = userModel.FullNameCN;
                                        newHRAuditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;
                                        newHRAuditLogInfo.FormId = dimissionFormInfo.DimissionId;
                                        newHRAuditLogInfo.FormType = (int)ESP.HumanResource.Common.HRFormType.DimissionForm;
                                        newHRAuditLogInfo.AuditLevel = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitFinance;

                                        hrauditLogDal.Add(newHRAuditLogInfo, trans);
                                    }
                                }
                            }

                        }
                        else if (dimissionFormInfo.PreAuditorId == manageModel.DimissionDirectorid)
                        {
                            hrAuditLogInfo.AuditorId = manageModel.DimissionManagerId;  // 总经理审批
                            hrAuditLogInfo.AuditorName = manageModel.DimissionManagerName;

                            dimissionFormInfo.Status = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitManager;
                            hrAuditLogInfo.AuditLevel = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitManager;

                            hrAuditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;
                            hrAuditLogInfo.FormId = dimissionId;
                            hrAuditLogInfo.FormType = (int)ESP.HumanResource.Common.HRFormType.DimissionForm;
                            hrAuditLogDal.Add(hrAuditLogInfo, trans);

                        }
                        else
                        {
                            hrAuditLogInfo.AuditorId = manageModel.DimissionDirectorid;  // 总监审批
                            hrAuditLogInfo.AuditorName = manageModel.DimissionDirectorName;

                            dimissionFormInfo.Status = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitDirector;
                            hrAuditLogInfo.AuditLevel = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitDirector;

                            hrAuditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;
                            hrAuditLogInfo.FormId = dimissionId;
                            hrAuditLogInfo.FormType = (int)ESP.HumanResource.Common.HRFormType.DimissionForm;
                            hrAuditLogDal.Add(hrAuditLogInfo, trans);
                        }

                        dimFormDal.Update(dimissionFormInfo, trans);

                        
                        #endregion
                    }

                    trans.Commit();
                    b = true;
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add("HumanResource", ex.ToString(), ESP.Logging.LogLevel.Error, ex);
                    trans.Rollback();
                    b = false;
                }
            }

            if (tempFlag == 2)
            {
                //ESP.Logging.Logger.Add("HumanResource", "执行修改单据方法", ESP.Logging.LogLevel.Error);
                // 总经理审批通过后修改相对应的交接单据信息
                //ESP.HumanResource.BusinessLogic.DimissionFormManager.DoDimission(dimissionId);
                // 发送项目号/支持方负责人变换的通知邮件。
                //ESP.HumanResource.BusinessLogic.DimissionFormManager.SendProjectMail(dimissionId);
            }
            return b;
        }

        /// <summary>
        /// 判断用户是否是该单据的交接人
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="formId">单据ID</param>
        /// <returns>返回值大于0表示用户是该单据的交接人，否则表示用户不是该单据的交接人</returns>
        public static int GetDimissionDetail(int userId, int formId,string formType)
        {
            return dal.GetDimissionDetail(userId, formId, formType);
        }


        /// <summary>
        /// 获得为修改的单据信息
        /// </summary>
        /// <returns></returns>
        public static List<ESP.HumanResource.Entity.DimissionDetailsInfo> GetNotUpdateDimissionDetail()
        {
            return dal.GetNotUpdateDimissionDetail();
        }
        #endregion  成员方法
    }
}
