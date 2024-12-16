using System;
using System.Data;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using ESP.HumanResource.DataAccess;

namespace ESP.HumanResource.BusinessLogic
{
    public class TransferManager
    {
        private static TransferDataProvider dal = new TransferDataProvider();
        public TransferManager()
        { }
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(TransferInfo model,HeadAccountInfo hc)
        {
            int ret =dal.Add(model);
            if (ret >= 1)
            {
                hc.Status = (int)ESP.HumanResource.Common.Status.HeadAccountStatus.InterView;
                (new ESP.HumanResource.BusinessLogic.HeadAccountManager()).Update(hc);
            }
            return ret;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(TransferInfo model)
        {
            return dal.Update(model);
        }

        public static bool HRConfirmIn(TransferInfo model, HRAuditLogInfo updateLogModel, HRAuditLogInfo newLogModel)
        {
            ESP.HumanResource.DataAccess.HRAuditLogProvider hrAuditLogDal = new ESP.HumanResource.DataAccess.HRAuditLogProvider();
                   
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    TransferManager.Update(model, trans);  // 修改离职单信息
                    hrAuditLogDal.Update(updateLogModel, trans);
                    hrAuditLogDal.Add(newLogModel, trans);
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

        public static int Update(TransferInfo model,SqlTransaction trans)
        {
            return dal.Update(model, trans);
        }

        public static List<ESP.HumanResource.Entity.TransferDetailsInfo> GetTransferDataByUserId(int userid)
        {
            return dal.GetTransferDataByUserId(userid);

        }



        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static int Delete(int id, HeadAccountInfo hc)
        {
            int ret = dal.Delete(id);
            if (ret >= 1)
            {
                hc.Status = (int)ESP.HumanResource.Common.Status.HeadAccountStatus.FinanceApproved;
                (new ESP.HumanResource.BusinessLogic.HeadAccountManager()).Update(hc);
            }
            return ret;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static TransferInfo GetModel(int id)
        {

            return dal.GetModel(id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<TransferInfo> GetList(string strWhere, List<SqlParameter> parms)
        {
            return dal.GetList(strWhere, parms);
        }

        public static List<TransferInfo> GetWaitAuditList(int currentUserId, string strWhere, List<SqlParameter> parms)
        {
            return dal.GetWaitAuditList(currentUserId, strWhere, parms);
        }

        #endregion  成员方法
    }
}
