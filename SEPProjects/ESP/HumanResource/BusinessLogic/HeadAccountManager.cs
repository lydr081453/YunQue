using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Entity;
using System.Data.SqlClient;
using ESP.HumanResource.Common;
using System.Data;
using ESP.HumanResource.DataAccess;

namespace ESP.HumanResource.BusinessLogic
{
    public class HeadAccountManager
    {

        private ESP.HumanResource.DataAccess.HeadAccountDataProvider dataProvider = new ESP.HumanResource.DataAccess.HeadAccountDataProvider();
        /// <summary>
        /// 根据条件获取培训活动的集合
        /// </summary>
        /// <param name="sqlWhere">sqlwhere</param>
        /// <returns></returns>
        public List<ESP.HumanResource.Entity.HeadAccountInfo> GetList(string sqlWhere)
        {
            return dataProvider.GetModelList(sqlWhere);
        }


        /// <summary>
        /// 根据id查询指定培训活动
        /// </summary>
        /// <param name="id">活动id</param>
        /// <returns></returns>
        public ESP.HumanResource.Entity.HeadAccountInfo GetModel(int id)
        {
            return dataProvider.GetModel(id);
        }

        public ESP.HumanResource.Entity.HeadAccountInfo GetModelByUserid(int userid)
        {
            return dataProvider.GetModelByUserId(userid);
        }


        /// <summary>
        /// 添加培训活动
        /// </summary>
        /// <param name="HeadAccountInfo">要添加的活动对象</param>
        /// <returns></returns>
        public int Add(ESP.HumanResource.Entity.HeadAccountInfo model)
        {
            return dataProvider.Add(model);
        }

        /// <summary>
        /// 根据id删除活动
        /// </summary>
        /// <param name="id">活动id</param>
        /// <returns></returns>
        public int Delete(int id)
        {
            return dataProvider.Delete(id);
        }

        /// <summary>
        /// 修改活动
        /// </summary>
        public int Update(ESP.HumanResource.Entity.HeadAccountInfo model)
        {
            return dataProvider.Update(model);
        }

        public int Update(ESP.HumanResource.Entity.HeadAccountInfo model, SqlTransaction trans)
        {
            return dataProvider.Update(model, trans);
        }


        public int Audit(ESP.HumanResource.Entity.HeadAccountInfo model, ESP.HumanResource.Entity.HeadAccountLogInfo log)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    Update(model, trans);
                    new ESP.HumanResource.BusinessLogic.HeadAccountLogManager().Add(log, trans);
                    trans.Commit();
                    return 1;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }
        }

        public int Audit(ESP.HumanResource.Entity.HeadAccountInfo model,ESP.HumanResource.Entity.EmployeeBaseInfo empModel, ESP.HumanResource.Entity.HeadAccountLogInfo log)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    Update(model, trans);
                    ESP.HumanResource.BusinessLogic.EmployeeBaseManager.Update(empModel, trans);
                    new ESP.HumanResource.BusinessLogic.HeadAccountLogManager().Add(log, trans);
                    trans.Commit();
                    return 1;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }
        }




        public int AuditInterview(ESP.HumanResource.Entity.HeadAccountInfo model, ESP.HumanResource.Entity.HeadAccountLogInfo logHR, ESP.HumanResource.Entity.HeadAccountLogInfo logGroup, UsersInfo userModel, EmployeeBaseInfo empModel, EmployeesInPositionsInfo depsModel, LogInfo logModel)
        {
            EmployeeBaseDataProvider dal = new EmployeeBaseDataProvider();

            UsersDataProvider usersDal = new UsersDataProvider();

            DataSet ds = new ESP.HumanResource.BusinessLogic.EmployeeBaseManager().GetEmployeeList(" and a.idnumber='" + empModel.IDNumber + "' and a.idnumber<>''");
            if (ds.Tables[0].Rows.Count > 0)
                return 0;
            else
            {
                using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    try
                    {
                        int userID = usersDal.Add(userModel, trans);
                        empModel.UserID = userID;
                        empModel.EmployeeWelfareInfo.sysid = userID;
                        empModel.EmployeeJobInfo.sysid = userID;
                        depsModel.UserID = userID;

                        dal.Add(empModel, trans);
                        //添加员工工资、合同信息
                        EmployeeJobManager.Add(empModel.EmployeeJobInfo, trans);
                        //添加员工社保信息
                        EmployeeWelfareManager.Add(empModel.EmployeeWelfareInfo, trans);
                        EmployeesInPositionsManager.Add(depsModel, trans);
                        
                        model.OfferLetterUserId = userID;
                        Update(model, trans);
                        new ESP.HumanResource.BusinessLogic.HeadAccountLogManager().Add(logHR, trans);
                        if (logGroup != null)
                        {
                            new ESP.HumanResource.BusinessLogic.HeadAccountLogManager().Add(logGroup, trans);
                        }

                        LogManager.AddLog(logModel, trans);
                        trans.Commit();
                        return userID;
                    }
                    catch (Exception ex)
                    {
                        ESP.Logging.Logger.Add(ex.ToString());
                        trans.Rollback();
                        throw new Exception(ex.Message);
                    }
               }
            }




            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    Update(model, trans);
                    new ESP.HumanResource.BusinessLogic.HeadAccountLogManager().Add(logHR, trans);
                    new ESP.HumanResource.BusinessLogic.HeadAccountLogManager().Add(logGroup, trans);
                    trans.Commit();
                    return 1;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }
        }
    }
}
