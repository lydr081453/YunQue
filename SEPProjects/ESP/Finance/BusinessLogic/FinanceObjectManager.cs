using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Entity;
using System.Data.SqlClient;

namespace ESP.Finance.BusinessLogic
{
    public static class FinanceObjectManager
    {
        private static ESP.Finance.IDataAccess.IFinanceObjectProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IFinanceObjectProvider>.Instance; } }

        public static int Add(FinanceObjectInfo model)
        {
            return DataProvider.Add(model);
        }

        public static int Update(FinanceObjectInfo model)
        {
            return DataProvider.Update(model);
        }

        public static int Delete(int objectId)
        {
            return DataProvider.Delete(objectId);
        }

        public static FinanceObjectInfo GetModel(int objectId)
        {
            return DataProvider.GetModel(objectId);
        }
        public static ESP.Finance.Entity.FinanceObjectInfo GetModel(string CredenceTypeCode, int RowLevel, string RowDesc)
        {
            return DataProvider.GetModel(CredenceTypeCode, RowLevel, RowDesc);
        }
        public static IList<FinanceObjectInfo> GetList(string term, List<SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }
    }
}
