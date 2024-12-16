/*********************************************************
 * 类中文名称：入转调离辅助工作与人员关联操作类
 * 类详细描述：
 * 
 * 
 * 
 * 
 * 主要制作人：zhouqi
 ********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ESP.HumanResource.DataAccess;
using ESP.HumanResource.Entity;
using System.Data.SqlClient;

namespace ESP.HumanResource.BusinessLogic
{
    public class EmployeesInAuxiliariesManager
    {
        private static readonly EmployeesInAuxiliariesDataProvider dal = new EmployeesInAuxiliariesDataProvider();
        public EmployeesInAuxiliariesManager()
        { }
        #region  成员方法

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(int id, int auxiliaryid)
        {
            return dal.Exists(id, auxiliaryid);
        }
        /// <summary>
        /// 增加一条辅助工作与人员的关联数据
        /// </summary>
        public static int Add(EmployeesInAuxiliariesInfo model, LogInfo logModel)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {                                       
                    returnValue = dal.Add(model, conn, trans);

                    LogManager.AddLog(logModel, trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.ToString());
                    returnValue = 0;
                }
            }
            return returnValue; 
        }

        /// <summary>
        /// 更新一条辅助工作与人员的关联数据
        /// </summary>
        public static void Update(EmployeesInAuxiliariesInfo model)
        {
            dal.Update(model);
        }

        /// <summary>
		/// 根据Auxiliaryid删除数据
		/// </summary>
        public static int DeleteByAuxID(int auxiliaryid, SqlConnection conn, SqlTransaction trans)
        {
            return dal.DeleteByAuxID(auxiliaryid, conn, trans);
        }
        
        /// <summary>
        /// 根据用户id和辅助工作ID删除数据
        /// </summary>
        public static int Delete(int userId, int auxiliaryId, LogInfo logModel)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    returnValue = dal.Delete(userId,auxiliaryId, conn, trans);

                    LogManager.AddLog(logModel, trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.ToString());
                    returnValue = 0;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 根据id删除一条辅助工作与人员的关联数据
        /// </summary>
        public static int Delete(int id, LogInfo logModel)
        {

            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    returnValue = dal.Delete(id, conn, trans);

                    LogManager.AddLog(logModel, trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.ToString());
                    returnValue = 0;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 通过id得到一个辅助工作与人员的关联对象实体
        /// </summary>
        public static EmployeesInAuxiliariesInfo GetModel(int id)
        {

            return dal.GetModel(id);
        }

        /// <summary>
        /// 获得辅助工作与人员的关联数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        

        /// <summary>
        /// 得到一个辅助工作与人员的关联对象List
        /// </summary>
        public static List<EmployeesInAuxiliariesInfo> GetModelList(string strWhere, List<SqlParameter> parms)
        {
            return dal.GetModelList(strWhere,parms);
        }

        /// <summary>
        /// 获得辅助工作与人员的关联数据列表
        /// </summary>
        public static DataSet GetAllList()
        {
            return GetList("");
        }

        #endregion  成员方法
    }
}
