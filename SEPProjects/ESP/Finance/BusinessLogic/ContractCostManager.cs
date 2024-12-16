using System;
using System.Data;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.DataAccess;
using ESP.Finance.Utility;
namespace ESP.Finance.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类ContractCostInfo 的摘要说明。
    /// </summary>


    public static class ContractCostManager
    {
        //private readonly ContractCostDAL dal=new ContractCostDAL();

        private static ESP.Finance.IDataAccess.IContractCostDataProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IContractCostDataProvider>.Instance; } }
        //private const string _dalProviderName = "ContractCostDALProvider";


        #region  成员方法

        /// <summary>
        /// 更新合同表的合同成本
        /// </summary>
        /// <param name="model"></param>
        private static void UpdateContractCost(ESP.Finance.Entity.ContractCostInfo model)
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


        public static int Add(ESP.Finance.Entity.ContractCostInfo model)
        {
            //decimal totalcost = DataProvider.GetTotalCost((int)model.ProjectID, true);
            //ProjectInfo prj = ProjectManager.GetModel(model.ProjectID.Value);
            //double rate = ESP.Finance.Configuration.ProjectAmountOverRate;
            //if (Convert.ToDouble(prj.TotalAmount.Value) * (1 + rate) - Convert.ToDouble(SupporterManager.GetTotalAmountByProject(model.ProjectID.Value))  < Convert.ToDouble(totalcost + model.Cost))
            //{
            //    return 0;
            //}
            if (model.ProjectID == null) return 0;
            //if (!CheckerManager.CheckProjectOddAmount(model.ProjectID.Value, model.Cost ?? 0))
            //{
            //    return 0;
            //}
            //trans//int res = DataProvider.Add(model, true);
            int res = DataProvider.Add(model);
            if (res > 0)
            {
                //UpdateContractCost(model);//更新合同表的合同成本
            }
            return res;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>


        public static UpdateResult Update(ESP.Finance.Entity.ContractCostInfo model)
        {
            //decimal totalcost = DataProvider.GetTotalCost(model.ProjectID.Value,true);
            //ProjectInfo prj = ProjectManager.GetModel(model.ProjectID.Value);
            //double rate = ESP.Finance.Configuration.ProjectAmountOverRate;
            //if (Convert.ToDouble(prj.TotalAmount.Value) * (1 + rate) - Convert.ToDouble( SupporterManager.GetTotalAmountByProject(model.ProjectID.Value) )< Convert.ToDouble(totalcost + model.Cost))
            //{
            //    return UpdateResult.AmountOverflow;
            //}
            ContractCostInfo old = GetModel(model.ContractCostID);//取出数据库中的老对象
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
            Entity.ContractCostInfo model = DataProvider.GetModel(ContractCostID);
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
        public static ESP.Finance.Entity.ContractCostInfo GetModel(int ContractCostID)
        {

            return DataProvider.GetModel(ContractCostID);
        }
        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ContractCostInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ContractCostInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ContractCostInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }

        public static IList<ESP.Finance.Entity.ContractCostInfo> GetListByProject(int projectId, string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetListByProject(projectId, term, param);
        }
        #endregion 获得数据列表

        #endregion  成员方法

        #region 验证二级物料项是否在采购系统中使用

        #endregion
    }
}

