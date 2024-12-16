using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ESP.Administrative.Entity;
using ESP.Administrative.Common;

namespace ESP.Administrative.BusinessLogic
{
    public class LeaveManager
    {
        private readonly ESP.Administrative.DataAccess.LeaveDataProvider dal = new ESP.Administrative.DataAccess.LeaveDataProvider();
        public LeaveManager()
        { }
        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        // <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(LeaveInfo model, ApproveLogInfo approvelog, LogInfo logModel)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    returnValue = dal.Add(model, conn, trans);
                    approvelog.ApproveDateID = returnValue;
                    returnValue = new ApproveLogManager().Add(approvelog, conn, trans);
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

        // <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(LeaveInfo model, LogInfo logModel)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    returnValue = dal.Add(model, conn, trans);
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
        /// 更新一条数据
        /// </summary>
        public int Update(LeaveInfo model, ApproveLogInfo approvelog, LogInfo logModel)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    returnValue = dal.Update(model, conn, trans);
                    approvelog.ApproveDateID = model.ID;
                    returnValue = new ApproveLogManager().Add(approvelog, conn, trans);
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
        /// 更新一条数据
        /// </summary>
        public int Update(LeaveInfo model, LogInfo logModel)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    returnValue = dal.Update(model, conn, trans);
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
        /// 更新一条数据
        /// </summary>
        public void Update(ESP.Administrative.Entity.LeaveInfo model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int id, LogInfo logModel)
        {

            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    returnValue = dal.Delete(id, conn, trans);
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
        /// 得到一个对象实体
        /// </summary>
        public ESP.Administrative.Entity.LeaveInfo GetModel(int ID)
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

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<LeaveInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            List<LeaveInfo> list = new List<LeaveInfo>();
            for (int n = 0; n < ds.Tables[0].Rows.Count; n++)
            {
                LeaveInfo model = new LeaveInfo();
                model.PopupData(ds.Tables[0].Rows[n]);
                list.Add(model);
            }
            return list;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<LeaveInfo> GetModelList(List<string> listWhere)
        {
            StringBuilder strWhere = new StringBuilder(" 1=1 ");
            if (listWhere != null)
            {
                string leavetype = listWhere[0];    // 请假单类型
                string fromtime = listWhere[1];    // 开始时间
                string totime = listWhere[2];    // 开始时间
                string userid = listWhere[3];    // 用户编号
                string state = listWhere[4];    // 状态
                if (!string.IsNullOrEmpty(userid))
                { 
                    strWhere.Append(" and userid=" + userid);
                }
                if (!string.IsNullOrEmpty(state) && state != "0")
                {
                    strWhere.Append(" and leaveState=" + state);
                }
                if (!string.IsNullOrEmpty(leavetype) && leavetype != "0")
                {
                    strWhere.Append(" and LeaveType=" + leavetype);
                }
                if (!string.IsNullOrEmpty(fromtime) && !string.IsNullOrEmpty(totime))
                {
                    strWhere.Append(" and ('" + fromtime + "'<endtime and '" + totime + "'>begintime)");
                }
            }

            DataSet ds = dal.GetList(strWhere.ToString());
            List<LeaveInfo> list = new List<LeaveInfo>();
            for (int n = 0; n < ds.Tables[0].Rows.Count; n++)
            {
                LeaveInfo model = new LeaveInfo();
                model.PopupData(ds.Tables[0].Rows[n]);
                list.Add(model);
            }
            return list;
        }
        #endregion  成员方法
    }
}

