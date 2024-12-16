using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Entity;
using System.Data.SqlClient;

namespace ESP.Finance.BusinessLogic
{
    public static class BankCancelManager
    {
        private static ESP.Finance.IDataAccess.IBankCancelProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IBankCancelProvider>.Instance; } }
        private const string tablename = "BankCancelInfo";
        public static ESP.Finance.Entity.BankCancelInfo GetModel(int bankCancelID)
        {
            Entity.BankCancelInfo bank = DataProvider.GetModel(bankCancelID);
            return bank;
        }
        public static IList<BankCancelInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }
        public static int Add(BankCancelInfo model)
        {
            return DataProvider.Add(model);
        }

        public static int Add(BankCancelInfo model,SqlTransaction trans)
        {
            return DataProvider.Add(model,trans);
        }

        public static ESP.Finance.Utility.UpdateResult Update(BankCancelInfo model)
        {
            int res = 0;
            try
            {
                res = DataProvider.Update(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ESP.Finance.Utility.UpdateResult.Failed;
            }
            if (res > 0)
            {
                return ESP.Finance.Utility.UpdateResult.Succeed;
            }
            else if (res == 0)
            {
                return ESP.Finance.Utility.UpdateResult.UnExecute;
            }
            return ESP.Finance.Utility.UpdateResult.Failed;
        }
        public static ESP.Finance.Utility.DeleteResult Delete(int bankCancelID)
        {
            int res = 0;
            try
            {
                res = DataProvider.Delete(bankCancelID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ESP.Finance.Utility.DeleteResult.Failed;
            }
            if (res > 0)
            {
                return ESP.Finance.Utility.DeleteResult.Succeed;
            }
            else if (res == 0)
            {
                return ESP.Finance.Utility.DeleteResult.UnExecute;
            }
            return ESP.Finance.Utility.DeleteResult.Failed;
        }
        public static IList<BankCancelInfo> GetAllList()
        {
            return GetList(string.Empty, null);
        }

        public static IList<BankCancelInfo> GetList(string term)
        {
            return GetList(term, null);
        }

        public static IList<BankCancelInfo> GetBatchList(string term)
        {
            return DataProvider.GetBatchList(term, null);
        }

        public static int CommitReturnInfo(ESP.Finance.Entity.ReturnInfo returnModel, ESP.Finance.Entity.ReturnAuditHistInfo FinanceModel, ESP.Finance.Entity.BankCancelInfo bankInfo)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    ESP.Finance.Utility.UpdateResult uresult = ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel,trans);
                    ESP.Finance.Utility.DeleteResult result = ESP.Finance.BusinessLogic.ReturnAuditHistManager.DeleteByReturnID(returnModel.ReturnID,trans);
                    int ret2 = ESP.Finance.BusinessLogic.ReturnAuditHistManager.Add(FinanceModel,trans);
                    int ret3 = new DataAccess.BankCancelProvider().Add(bankInfo, trans);
                    if (uresult == ESP.Finance.Utility.UpdateResult.Succeed && result == ESP.Finance.Utility.DeleteResult.Succeed && ret2 > 0 && ret3 > 0)
                    {
                        trans.Commit();
                        return 1;
                    }
                    else
                    {
                        trans.Rollback();
                        return -1;
                    }
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }
        }

        public static void CommitReturnInfo(ESP.Finance.Entity.ReturnInfo returnModel, ESP.Finance.Entity.ReturnAuditHistInfo FinanceModel, ESP.Finance.Entity.BankCancelInfo bankInfo, SqlTransaction trans)
        {
            ESP.Finance.Utility.UpdateResult uresult = ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel, trans);
            ESP.Finance.Utility.DeleteResult result = ESP.Finance.BusinessLogic.ReturnAuditHistManager.DeleteByReturnID(returnModel.ReturnID, trans);
            ESP.Finance.BusinessLogic.ReturnAuditHistManager.Add(FinanceModel, trans);
            new DataAccess.BankCancelProvider().Add(bankInfo, trans);
        }

    }
}
