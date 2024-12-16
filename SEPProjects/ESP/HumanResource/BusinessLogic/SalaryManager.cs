using System;
using System.Data;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using ESP.HumanResource.DataAccess;

namespace ESP.HumanResource.BusinessLogic
{    
    public class SalaryManager
    {
        private static SalaryDataProvider dal = new SalaryDataProvider();
        public SalaryManager()
        { }
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(SalaryInfo model, SnapshotsInfo snapshots, LogInfo logModel)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {                    
                    returnValue = SnapshotsManager.Add(snapshots, trans);
                    model.salaryDetailID = returnValue;
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
        /// 更新一条数据
        /// </summary>
        public static int Update(SalaryInfo model, SnapshotsInfo snapshots, LogInfo logModel)
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
                    SnapshotsManager.Update(snapshots, trans);
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
        /// 添加导入的批量数据
        /// </summary>
        public static int AddandUpdate(List<SalaryInfo> list1, List<SalaryInfo> list2, LogInfo logModel)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (SalaryInfo salary2 in list2)
                    {
                        returnValue = SnapshotsManager.Add(salary2.Snapshots, trans);
                        salary2.salaryDetailID = returnValue;
                        returnValue = dal.Add(salary2, conn, trans);
                    }
                    foreach (SalaryInfo salary1 in list1)
                    {
                         SnapshotsManager.Update(salary1.Snapshots, trans);                        
                        returnValue = dal.Update(salary1, conn, trans);
                    }
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
        /// 删除一条数据
        /// </summary>
        public static void Delete(SalaryInfo model, LogInfo logModel)
        {

            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    SnapshotsManager.Delete(model.salaryDetailID, trans);
                    dal.Delete(model.id, conn, trans);
                    LogManager.AddLog(logModel, trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.ToString());
                }
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static SalaryInfo GetModel(int id)
        {

            return dal.GetModel(id);
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
        public static List<SalaryInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            List<SalaryInfo> modelList = new List<SalaryInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                SalaryInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SalaryInfo();
                    if (ds.Tables[0].Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(ds.Tables[0].Rows[n]["id"].ToString());
                    }
                    model.createrName = ds.Tables[0].Rows[n]["createrName"].ToString();
                    if (ds.Tables[0].Rows[n]["createDate"].ToString() != "")
                    {
                        model.createDate = DateTime.Parse(ds.Tables[0].Rows[n]["createDate"].ToString());
                    }
                    model.memo = ds.Tables[0].Rows[n]["memo"].ToString();
                    if (ds.Tables[0].Rows[n]["declareStatus"].ToString() != "")
                    {
                        model.declareStatus = int.Parse(ds.Tables[0].Rows[n]["declareStatus"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["declarer"].ToString() != "")
                    {
                        model.declarer = int.Parse(ds.Tables[0].Rows[n]["declarer"].ToString());
                    }
                    model.declarerName = ds.Tables[0].Rows[n]["declarerName"].ToString();
                    if (ds.Tables[0].Rows[n]["declareDate"].ToString() != "")
                    {
                        model.declareDate = DateTime.Parse(ds.Tables[0].Rows[n]["declareDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["groupApproveStatus"].ToString() != "")
                    {
                        model.groupApproveStatus = int.Parse(ds.Tables[0].Rows[n]["groupApproveStatus"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["groupApprover"].ToString() != "")
                    {
                        model.groupApprover = int.Parse(ds.Tables[0].Rows[n]["groupApprover"].ToString());
                    }
                    model.groupApproverName = ds.Tables[0].Rows[n]["groupApproverName"].ToString();
                    if (ds.Tables[0].Rows[n]["sysid"].ToString() != "")
                    {
                        model.sysid = int.Parse(ds.Tables[0].Rows[n]["sysid"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["groupApproveDate"].ToString() != "")
                    {
                        model.groupApproveDate = DateTime.Parse(ds.Tables[0].Rows[n]["groupApproveDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["groupHrStatus"].ToString() != "")
                    {
                        model.groupHrStatus = int.Parse(ds.Tables[0].Rows[n]["groupHrStatus"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["groupHr"].ToString() != "")
                    {
                        model.groupHr = int.Parse(ds.Tables[0].Rows[n]["groupHr"].ToString());
                    }
                    model.groupHrName = ds.Tables[0].Rows[n]["groupHrName"].ToString();
                    if (ds.Tables[0].Rows[n]["groupHrDate"].ToString() != "")
                    {
                        model.groupHrDate = DateTime.Parse(ds.Tables[0].Rows[n]["groupHrDate"].ToString());
                    }
                    model.userCode = ds.Tables[0].Rows[n]["userCode"].ToString();
                    model.sysUserName = ds.Tables[0].Rows[n]["sysUserName"].ToString();
                    model.job = ds.Tables[0].Rows[n]["job"].ToString();
                    if (ds.Tables[0].Rows[n]["payChange"].ToString() != "")
                    {
                        model.payChange = int.Parse(ds.Tables[0].Rows[n]["payChange"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["salaryDetailID"].ToString() != "")
                    {
                        model.salaryDetailID = int.Parse(ds.Tables[0].Rows[n]["salaryDetailID"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["operationDate"].ToString() != "")
                    {
                        model.operationDate = DateTime.Parse(ds.Tables[0].Rows[n]["operationDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["creater"].ToString() != "")
                    {
                        model.creater = int.Parse(ds.Tables[0].Rows[n]["creater"].ToString());
                    }
                    
                        model.nowBasePay = (ds.Tables[0].Rows[n]["nowBasePay"].ToString());
                    
                        model.nowMeritPay = (ds.Tables[0].Rows[n]["nowMeritPay"].ToString());
                    
                        model.newBasePay = (ds.Tables[0].Rows[n]["newBasePay"].ToString());
                    
                        model.newMeritPay = (ds.Tables[0].Rows[n]["newMeritPay"].ToString());
                  
                    if (ds.Tables[0].Rows[n]["companyID"].ToString() != "")
                    {
                        model.companyID = int.Parse(ds.Tables[0].Rows[n]["companyID"].ToString());
                    }
                    model.companyName = ds.Tables[0].Rows[n]["companyName"].ToString();
                    if (ds.Tables[0].Rows[n]["departmentID"].ToString() != "")
                    {
                        model.departmentID = int.Parse(ds.Tables[0].Rows[n]["departmentID"].ToString());
                    }
                    model.departmentName = ds.Tables[0].Rows[n]["departmentName"].ToString();
                    if (ds.Tables[0].Rows[n]["groupID"].ToString() != "")
                    {
                        model.groupID = int.Parse(ds.Tables[0].Rows[n]["groupID"].ToString());
                    }
                    model.groupName = ds.Tables[0].Rows[n]["groupName"].ToString();
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetAllList()
        {
            return GetList("");
        }
        #endregion  成员方法
    }
}
