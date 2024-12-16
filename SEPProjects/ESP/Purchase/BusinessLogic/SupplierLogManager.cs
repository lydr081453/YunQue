using System.Data;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;
using System.Collections.Generic;

namespace ESP.Purchase.BusinessLogic
{
    public static class SupplierLogManager
    {
        private static SupplierLogDataProvider dal = new SupplierLogDataProvider();
        public static int Add(SupplierLogInfo model)
        {
            return dal.Add(model);
        }
        public static IList<SupplierLogInfo> GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
    }
}
