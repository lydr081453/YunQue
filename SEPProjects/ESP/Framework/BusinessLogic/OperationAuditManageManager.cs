using System.Data;
using System.Collections.Generic;
using ESP.Framework.DataAccess;
using ESP.Framework.Entity;
using System.Collections;

namespace ESP.Framework.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类OperationAuditManageManager 的摘要说明。
    /// </summary>
    public class OperationAuditManageManager
    {
        private static OperationAuditManageDataHelper dal = new ESP.Framework.DataAccess.OperationAuditManageDataHelper();

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationAuditManageManager"/> class.
        /// </summary>
        public OperationAuditManageManager()
        { }

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="Id">The id.</param>
        /// <returns></returns>
        public static bool Exists(int Id)
        {
            return dal.Exists(Id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Add(OperationAuditManageInfo model)
        {
            return dal.Add(model);
        }

        public static Hashtable GetPurchaseDirectorsByAuditor(int auditorId)
        {
            return dal.GetPurchaseDirectorsByAuditor(auditorId);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        public static void Update(OperationAuditManageInfo model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="Id">The id.</param>
        public static void Delete(int Id)
        {
            dal.Delete(Id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="Id">The id.</param>
        /// <returns></returns>
        public static OperationAuditManageInfo GetModel(int Id)
        {
            return dal.GetModel(Id);
        }

        /// <summary>
        /// 根据部门ID获得一个对象实体
        /// </summary>
        /// <param name="DepId">The dep id.</param>
        /// <returns></returns>
        public static OperationAuditManageInfo GetModelByDepId(int DepId)
        {
            return dal.GetModelByDepId(DepId);
        }
        /// <summary>
        /// 根据特殊人员设置审批路径
        /// </summary>
        /// <param name="userid">userid</param>
        /// <returns>entity</returns>
        public static OperationAuditManageInfo GetModelByUserId(int userid)
        {
            return dal.GetModelByUserId(userid);
        }

        public static OperationAuditManageInfo GetModelByProjectId(int projectId)
        {
            return dal.GetModelByProjectId(projectId);
        }

        public static OperationAuditManageInfo GetModelByDepId(int DepId,System.Data.SqlClient.SqlTransaction trans)
        {
            return dal.GetModelByDepId(DepId, trans);
        }

        public static string GetDeptByPurchaseDirector(int purchaseDirectorId)
        {
            return dal.GetDeptByPurchaseDirector(purchaseDirectorId);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        
        /// <summary>
        /// 根据用户ID获得有部门管理级的相关部门ID集
        /// </summary>
        /// <param name="strWhere">The INT UserID.</param>
        /// <returns></returns>
        public static DataSet GetDeptListByUserID(int uerid)
        {
            return dal.GetListByUserID(uerid);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static List<OperationAuditManageInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            List<OperationAuditManageInfo> modelList = new List<OperationAuditManageInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                OperationAuditManageInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new OperationAuditManageInfo();
                    model.PopupData(ds.Tables[0].Rows[n]);
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAllList()
        {
            return dal.GetList("");
        }

        /// <summary>
        /// 获得总监的sysids
        /// </summary>
        /// <returns></returns>
        public static string GetDirectorIds()
        {
            return dal.GetDirectorIds();
        }

        /// <summary>
        /// 获得总经理的sysids
        /// </summary>
        /// <returns></returns>
        public static string GetManagerIds()
        {
            return dal.GetManagerIds();
        }

        /// <summary>
        /// 获得CEO的sysids
        /// </summary>
        /// <returns></returns>
        public static string GetCEOIds()
        {
            return dal.GetCEOIds();
        }
        /// <summary>
        /// 获得行政助理userid
        /// </summary>
        /// <returns></returns>
        public static string GetReceptionIds()
        {
            return dal.GetReceptionIds();
        }

        /// <summary>
        /// 获得考勤审批人
        /// </summary>
        /// <returns></returns>
        public static string GetAttendanceId()
        {
            return dal.GetAttendanceId();
        }

        /// <summary>
        /// 获得HR审批人
        /// </summary>
        /// <returns></returns>
        public static string GetHRId()
        {
            return dal.GetHRId();
        }

          /// <summary>
        /// 获得HR助理审批人
        /// </summary>
        /// <returns></returns>
        public static string GetHRAttendanceId()
        {
            return dal.GetHRAttendanceId();
        }

        #endregion  成员方法

        public static bool GetCurrentUserIsDirector(string curUserId)
        {
            string directorids = GetDirectorIds();
            if (directorids.IndexOf(curUserId) > 0)
            {
                return true;
            }
            return false;
        }

        public static bool GetCurrentUserIsManager(string curUserId)
        {
            string manageids = GetManagerIds();
            if (manageids.IndexOf(curUserId) > 0)
            {
                return true;
            }
            return false;
        }
    }
}
