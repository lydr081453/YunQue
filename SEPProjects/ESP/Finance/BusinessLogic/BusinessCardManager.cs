using System;
using System.Data;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Linq;
using System.Data.SqlClient;
namespace ESP.Finance.BusinessLogic
{
    public static class BusinessCardManager
    {
        private static ESP.Finance.IDataAccess.IBusinessCardProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IBusinessCardProvider>.Instance; } }

        public static  int Add(BusinessCardInfo model)
        {
            return DataProvider.Add(model);
        }

        public static  int Update(BusinessCardInfo model)
        {
            return DataProvider.Update(model);
        }

        public static int Delete(int bcid)
        {
            return DataProvider.Delete(bcid);
        }

        public static BusinessCardInfo GetModel(int bcid)
        {
            return DataProvider.GetModel(bcid);
        }

        public static IList<BusinessCardInfo> GetList(string term, List<SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }
        public static int IsHaveCard(int userid)
        {
            return DataProvider.IsHaveCard(userid);
        }
        public static BusinessCardInfo GetBusinessCard(int userid)
        {
            return DataProvider.GetBusinessCard(userid);
        }
    }
}
