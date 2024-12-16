using System;
using System.Data;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.DataAccess;
using ESP.Finance.Utility;

namespace ESP.Finance.BusinessLogic
{
    public static class ContractCostTmpManager
    {
        private static ESP.Finance.IDataAccess.IContractCostTmpDataProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IContractCostTmpDataProvider>.Instance; } }

        /// <summary>
        /// 更新合同表的合同成本
        /// </summary>
        /// <param name="model"></param>
        private static void UpdateContractCost(ESP.Finance.Entity.ContractCostTmpInfo model)
        {
            //更新合同表的合同成本
            int prjId = model.ProjectID ?? 0;
            int ContractId = model.ContractID ?? 0;

            Entity.ContractInfo contract = ContractManager.GetModel(ContractId);
            if (contract != null)
            {
                //trans//contract.Cost = DataProvider.GetTotalCost(prjId,true);
                contract.Cost = DataProvider.GetTotalCost(prjId);
                ESP.Finance.BusinessLogic.ContractManager.Update(contract);
            }
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>


        public static int Add(ESP.Finance.Entity.ContractCostTmpInfo model)
        {
            if (model.ProjectID == null) return 0;
            if (!CheckerManager.CheckProjectOddAmount(model.ProjectID.Value, model.Cost ?? 0))
            {
                return 0;
            }
            int res = DataProvider.Add(model);
            if (res > 0)
            {
            }
            return res;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>


        public static UpdateResult Update(ESP.Finance.Entity.ContractCostTmpInfo model)
        {
            ContractCostTmpInfo old = GetModel(model.ContractCostID);//取出数据库中的老对象
            if (model.ProjectID == null) return 0;
            if (!CheckerManager.CheckProjectOddAmount(model.ProjectID.Value, model.Cost ?? 0 - old.Cost ?? 0))//检查时用新金额减去原金额再进行比较
            {
                return UpdateResult.AmountOverflow;
            }
            int res = 0;
            try
            {
                //trans//res = DataProvider.Update(model,true);
                res = DataProvider.Update(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return UpdateResult.Failed;
            }
            if (res > 0)
            {
                //UpdateContractCost(model);//更新合同表的合同成本
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


        public static DeleteResult Delete(int ContractCostID)
        {
            Entity.ContractCostTmpInfo model = DataProvider.GetModel(ContractCostID);
            int res = 0;
            try
            {
                res = DataProvider.Delete(ContractCostID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return DeleteResult.Failed;
            }
            if (res > 0)
            {

                //UpdateContractCost(model);//更新合同表的合同成本
                return DeleteResult.Succeed;
            }
            else if (res == 0)
            {
                return DeleteResult.UnExecute;
            }
            return DeleteResult.Failed;
        }



        public static decimal GetTotalAmountByProject(int projectId)
        {
            //return DataProvider.GetTotalCost(projectId, true);
            return DataProvider.GetTotalCost(projectId);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.Finance.Entity.ContractCostTmpInfo GetModel(int ContractCostID)
        {

            return DataProvider.GetModel(ContractCostID);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ContractCostTmpInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ContractCostTmpInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ContractCostTmpInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }

        public static IList<ESP.Finance.Entity.ContractCostTmpInfo> GetListByProject(int projectId, string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetListByProject(projectId, term, param);
        }
    }
}
