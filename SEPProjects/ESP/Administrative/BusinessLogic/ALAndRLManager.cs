using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ESP.Administrative.Entity;
using ESP.Administrative.DataAccess;
using System.Data.SqlClient;

namespace ESP.Administrative.BusinessLogic
{
    public class ALAndRLManager
    {
        private readonly ALAndRLDataProvider dal = new ALAndRLDataProvider();
        public ALAndRLManager()
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
        public int Add(ALAndRLInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ALAndRLInfo model, SqlConnection conn, SqlTransaction trans)
        {
            return dal.Add(model, conn, trans);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ALAndRLInfo model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 更新年假信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        public void Update(ALAndRLInfo model, SqlConnection conn, SqlTransaction trans)
        {
            if (model != null)
                dal.Update(model, trans);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            dal.Delete(ID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ALAndRLInfo GetModel(int ID)
        {
            return dal.GetModel(ID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return dal.GetList("");
        }

        public List<ALAndRLInfo> GetModelList(string strWhere)
        {
            return dal.GetModelList(strWhere);
        }

        /// <summary>
        /// 获得年假统计信息
        /// </summary>
        /// <param name="userID">员工编号</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns>返回年假统计信息</returns>
        public DataSet GetAnnualLeaveInfo(int userID, string strWhere, List<SqlParameter> parameterList)
        {

            UserAttBasicInfoManager userAttBasicManager = new UserAttBasicInfoManager();
            string selUserIDs = userAttBasicManager.GetStatUserIDs(userID);
            strWhere += " AND a.UserId IN (" + selUserIDs + ")";
            return dal.GetAnnualLeaveInfo(userID, strWhere, parameterList);
        }

        /// <summary>
        /// 获得奖励假期信息
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="parameterList">查询参数</param>
        /// <returns>返回奖励假期信息集合</returns>
        public DataSet GetRewardLeaveInfo(int userID, string strWhere, List<SqlParameter> parameterList)
        {
            UserAttBasicInfoManager userAttBasicManager = new UserAttBasicInfoManager();
            string selUserIDs = userAttBasicManager.GetStatUserIDs(userID);
            strWhere += " AND a.UserId IN (" + selUserIDs + ")";
            return dal.GetRewardLeaveInfo(userID, strWhere, parameterList);
        }

        /// <summary>
        /// 保存年假和奖励假信息
        /// </summary>
        /// <param name="list">年假和奖励假信息集合</param>
        /// <returns>如果保存成功返回true,否则返回false</returns>
        public bool SaveAnnualLeaveInfo(List<ALAndRLInfo> list)
        {
            bool b = false;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                if (list != null && list.Count > 0)
                {
                    try
                    {
                        foreach (ALAndRLInfo model in list)
                        {
                            dal.Add(model, conn, trans);
                        }
                        b = true;
                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                    }
                }               
            }
            return b;
        }

        /// <summary>
        /// 获得用户年度的年假总数
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="year">年份</param>
        /// <param name="leaveType">假期类型，年假/奖励假</param>
        /// <returns>返回用户年假信息</returns>
        public ALAndRLInfo GetALAndRLModel(int userId, int year, int leaveType)
        {
            return dal.GetALAndRLModel(userId, year, leaveType,null);
        }

        /// <summary>
        /// 获得用户年度的年假总数
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="year">年份</param>
        /// <param name="leaveType">假期类型，年假/奖励假</param>
        /// <returns>返回用户年假信息</returns>
        public ALAndRLInfo GetALAndRLModel(int userId, int year, int leaveType,SqlTransaction trans)
        {
            return dal.GetALAndRLModel(userId, year, leaveType, trans);
        }

        /// <summary>
        /// 获得奖励假信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="validto">申请奖励假的结束日期</param>
        /// <param name="leaveType">奖励假类型</param>
        /// <returns>返回奖励假信息对象</returns>
        public ALAndRLInfo GetALAndRLModel(int userId, DateTime validto, int leaveType)
        {
            return dal.GetALAndRLModel(userId, validto, leaveType);
        }
        #endregion  成员方法
    }
}