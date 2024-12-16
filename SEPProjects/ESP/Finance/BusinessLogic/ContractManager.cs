using System;
using System.Data;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.DataAccess;
using ESP.Finance.Utility;
using System.Data.SqlClient;
namespace ESP.Finance.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类ContractInfo 的摘要说明。
    /// </summary>


    public static class ContractManager
    {
        //private readonly ContractDAL dal = new ContractDAL();

        private static ESP.Finance.IDataAccess.IContractDataProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IContractDataProvider>.Instance; } }
        //private const string _dalProviderName = "ContractDALProvider";


        #region  成员方法

        ///// <summary>
        ///// 增加一条数据
        ///// </summary>
        public static int Add(ESP.Finance.Entity.ContractInfo model)
        {
            int contractid = model.ContractID == null ? 0 : model.ContractID.Value;
            int version = DataProvider.GetNewVersion(contractid);
            model.Version = version;
            model.Usable = Convert.ToBoolean(Utility.ContractUsable.Usable);
            return DataProvider.Add(model);
        }

        public static int Add(ESP.Finance.Entity.ContractInfo model, SqlTransaction trans)
        {
            int contractid = model.ContractID == null ? 0 : model.ContractID.Value;
            int version = DataProvider.GetNewVersion(contractid);
            model.Version = version;
            model.Usable = Convert.ToBoolean(Utility.ContractUsable.Usable);
            return DataProvider.Add(model, trans);
        }

        /// <summary>
        /// 合同替换
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int ReplaceContract(ESP.Finance.Entity.ContractInfo model)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    if (model.ParentID != null && model.ParentID > 0)
                    {
                        ContractInfo parent = GetModel(model.ParentID.Value);
                        parent.Usable = Convert.ToBoolean(Utility.ContractUsable.UnUsable);
                        DataProvider.Update(parent, trans);
                    }
                    if (model.ContractID == null || model.ContractID.Value == 0)
                        returnValue = Add(model, trans);
                    else
                    {
                        DataProvider.Update(model, trans);
                        returnValue = model.ContractID.Value;
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
            }
            return returnValue;
        }

        public static UpdateResult Update(ESP.Finance.Entity.ContractInfo model)
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

        public static int UpdateContractDel(int projectID, bool Del)
        {
            return DataProvider.UpdateContractDel(projectID, Del);
        }


        public static int UpdateContractDel(int projectID, bool Del,SqlTransaction trans)
        {
            return DataProvider.UpdateContractDel(projectID, Del,trans);
        }

        public static int UpdateContractStatus(string contractIds, ESP.Finance.Utility.ContractStatus.Status status)
        {
            return DataProvider.UpdateContractStatus(contractIds, status);
        }

        public static decimal GetTotalCostByProject(int projectId)
        {
            //trans//return DataProvider.GetTotalCostByProject(projectId, true);
            return DataProvider.GetTotalCostByProject(projectId);
        }



        public static decimal GetTotalAmountByProject(int projectId, int originalContractID)
        {
            return DataProvider.GetTotalAmountByProject(projectId, originalContractID);
        }

        public static decimal GetOddAmountByProject(int projectId, int contractID, int originalContractID)
        {
            return DataProvider.GetOddAmountByProject(projectId, contractID, originalContractID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static DeleteResult Delete(int ContractID)
        {
            int res = 0;
            try
            {
                res = DataProvider.Delete(ContractID);
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

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        // 
        // 
        public static ContractInfo GetModel(int ContractID)
        {
            ContractInfo model = new ContractInfo();
            model = DataProvider.GetModel(ContractID);
            //model.Details = ContractCostManager.GetListByContract(model.ContractID, null, null);
            return model;
        }

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ContractInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ContractInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ContractInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }

        public static IList<ESP.Finance.Entity.ContractInfo> GetListByProject(int projectID, string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetListByProject(projectID, term, param);
        }
        #endregion 获得数据列表

        #endregion  成员方法
    }
}