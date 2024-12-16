/*********************************************************
 * 类中文名称：员工入转调离时辅助工作类
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
using System.Data.SqlClient;
using ESP.HumanResource.DataAccess;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.BusinessLogic
{
    public class AuxiliaryManager
    {
        private static readonly AuxiliaryDataProvider dal = new AuxiliaryDataProvider();
        public AuxiliaryManager()
        { }
        #region  成员方法
        /// <summary>
        /// 增加一条辅助工作数据
        /// </summary>
        public static int Add(AuxiliaryInfo model, LogInfo logModel)
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
        /// 更新一条辅助工作数据
        /// </summary>
        public static int Update(AuxiliaryInfo model, LogInfo logModel)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    returnValue = dal.Update(model, conn, trans);

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
        /// 删除一条辅助工作数据
        /// </summary>
        public static int Delete(int id,LogInfo logModel)
        {

            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    returnValue = dal.Delete(id, conn, trans);

                    returnValue = EmployeesInAuxiliariesManager.DeleteByAuxID(id, conn,trans);
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
        /// 得到一个辅助工作对象实体
        /// </summary>
        public static AuxiliaryInfo GetModel(int id)
        {

            return dal.GetModel(id);
        }
        

        /// <summary>
        /// 通过查询条件获得辅助工作的数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 通过查询条件获得辅助工作的数据列表
        /// </summary>
        public static List<AuxiliaryInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            List<AuxiliaryInfo> modelList = new List<AuxiliaryInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                AuxiliaryInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new AuxiliaryInfo();
                    if (ds.Tables[0].Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(ds.Tables[0].Rows[n]["id"].ToString());
                    }
                    model.auxiliaryName = ds.Tables[0].Rows[n]["auxiliaryName"].ToString();
                    model.Description = ds.Tables[0].Rows[n]["Description"].ToString();
                    if (ds.Tables[0].Rows[n]["isDisable"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[n]["isDisable"].ToString() == "1") || (ds.Tables[0].Rows[n]["isDisable"].ToString().ToLower() == "true"))
                        {
                            model.isDisable = true;
                        }
                        else
                        {
                            model.isDisable = false;
                        }
                    }
                    if (ds.Tables[0].Rows[n]["CompanyID"].ToString() != "")
                    {
                        model.companyID = int.Parse(ds.Tables[0].Rows[n]["CompanyID"].ToString());
                    }
                    model.companyName = ds.Tables[0].Rows[n]["CompanyName"].ToString();
                    if (ds.Tables[0].Rows[n]["Apply"].ToString() != "")
                    {
                        model.apply = int.Parse(ds.Tables[0].Rows[n]["Apply"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得所有辅助工作的数据列表
        /// </summary>
        public static DataSet GetAllList()
        {
            return GetList("");
        }
 

        #endregion  成员方法
    }
}
