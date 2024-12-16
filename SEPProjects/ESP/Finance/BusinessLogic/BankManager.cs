using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Entity;

namespace ESP.Finance.BusinessLogic
{
     
     
    public static class BankManager
    {
        private static ESP.Finance.IDataAccess.IBankDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IBankDataProvider>.Instance;}}
        private const string tablename = "BankInfo";

        

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.Finance.Entity.BankInfo GetModel(int bankID)
        {
            Entity.BankInfo bank = DataProvider.GetModel(bankID);
            return bank;
        }


        public static IList<BankInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }

        #region IBankProvider 成员

        public static int Add(BankInfo model)
        {
            return DataProvider.Add(model);
        }

        public static ESP.Finance.Utility.UpdateResult Update(BankInfo model)
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

        public static ESP.Finance.Utility.DeleteResult Delete(int bankID)
        {
            int res = 0;
            try
            {
                res = DataProvider.Delete(bankID);
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

        public static IList<BankInfo> GetAllList()
        {
            return GetList(string.Empty, null);
        }

        public static IList<BankInfo> GetList(string term)
        {
            return GetList(term, null);
        }

        #endregion
    }
}
