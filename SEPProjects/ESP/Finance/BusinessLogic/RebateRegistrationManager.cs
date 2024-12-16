using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
namespace ESP.Finance.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类RebateRegistrationInfo 的摘要说明。
    /// </summary>
     
     
    public static class RebateRegistrationManager
    {
        //private readonly ESP.Finance.DataAccess.RebateRegistrationDAL dal = new ESP.Finance.DataAccess.RebateRegistrationDAL();

        private static ESP.Finance.IDataAccess.IRebateRegistrationDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IRebateRegistrationDataProvider>.Instance;}}
        //private const string _dalProviderName = "RebateRegistrationDALProvider";

        


        #region  成员方法


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ESP.Finance.Entity.RebateRegistrationInfo model)
        {
            return DataProvider.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static UpdateResult Update(ESP.Finance.Entity.RebateRegistrationInfo model)
        {
            int res = 0;
            try
            {
                res = DataProvider.Update(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return UpdateResult.Failed;
            }
            if (res > 0)
            {
                return UpdateResult.Succeed;
            }
            else if (res == 0)
            {
                return UpdateResult.UnExecute;
            }
            return UpdateResult.Failed;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static DeleteResult Delete(int Id)
        {
            int res = 0;
            try
            {
                res = DataProvider.Delete(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return DeleteResult.Failed;
            }
            if (res > 0)
            {
                return DeleteResult.Succeed;
            }
            else if (res == 0)
            {
                return DeleteResult.UnExecute;
            }
            return DeleteResult.Failed;
        }

        public static int DeleteByBatch(int batchId, SqlTransaction trans)
        {
            return DataProvider.DeleteByBatch(batchId, trans);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.Finance.Entity.RebateRegistrationInfo GetModel(int Id)
        {

            return DataProvider.GetModel(Id);
        }

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<RebateRegistrationInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<RebateRegistrationInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<RebateRegistrationInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }

        public static int ImpList(PNBatchInfo batchModel, List<ESP.Finance.Entity.RebateRegistrationInfo> list)
        {
            return DataProvider.ImpList(batchModel, list);
        }

        #endregion 获得数据列表
        #endregion  成员方法
    }
}

