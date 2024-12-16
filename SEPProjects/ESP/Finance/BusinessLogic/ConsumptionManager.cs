using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
namespace ESP.Finance.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类ConsumptionInfo 的摘要说明。
    /// </summary>


    public static class ConsumptionManager
    {
        private static ESP.Finance.IDataAccess.IConsumptionProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IConsumptionProvider>.Instance; } }


        #region  成员方法


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ESP.Finance.Entity.ConsumptionInfo model)
        {
            return DataProvider.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static UpdateResult Update(ESP.Finance.Entity.ConsumptionInfo model)
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

        public static UpdateResult Update(ESP.Finance.Entity.ConsumptionInfo model,SqlTransaction trans)
        {
            int res = 0;
            try
            {
                res = DataProvider.Update(model, trans);
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
        public static DeleteResult Delete(int ConsumptionId)
        {
            int res = 0;
            try
            {
                res = DataProvider.Delete(ConsumptionId);
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

        public static int DeleteByBatch(int batchId,SqlTransaction trans)
        {
            return DataProvider.DeleteByBatch(batchId, trans);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.Finance.Entity.ConsumptionInfo GetModel(int ConsumptionId)
        {

            return DataProvider.GetModel(ConsumptionId);
        }

        public static int ImpList(PNBatchInfo batchModel, List<ESP.Finance.Entity.ConsumptionInfo> list)
        { 
         return DataProvider.ImpList(batchModel,list);
        }
        

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ConsumptionInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ConsumptionInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ConsumptionInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }
        /// <summary>
        /// 获取占用成本的消耗
        /// </summary>
        /// <param name="term"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IList<ConsumptionInfo> GetCostList(string term)
        {
            return DataProvider.GetCostList(term);
        }
        /// <summary>
        /// 获取审批中的消耗计算成本
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static IList<ConsumptionInfo> GetAuditingConsumption(int projectId)
        {
            return DataProvider.GetAuditingConsumption(projectId);
        }
        #endregion 获得数据列表
        #endregion  成员方法
   
    }
}
