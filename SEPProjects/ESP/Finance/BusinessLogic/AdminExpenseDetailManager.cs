using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.BusinessLogic
{
    public static class AdminExpenseDetailManager
    {
        private static ESP.Finance.IDataAccess.IAdminExpenseDetailProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IAdminExpenseDetailProvider>.Instance; } }

        public static int Update(ESP.Finance.Entity.AdminExpenseDetailInfo model)
        {
            return DataProvider.Update(model);
        }

        public static ESP.Finance.Entity.AdminExpenseDetailInfo GetModel(int AreaId, int ExpenseTypeId)
        {
            return DataProvider.GetModel(AreaId, ExpenseTypeId);
        }

        public static int Update(ESP.Finance.Entity.AdminExpenseDetailInfo model,System.Data.SqlClient.SqlTransaction trans)
        {
            return DataProvider.Update(model,trans);
        }

        public static ESP.Finance.Entity.AdminExpenseDetailInfo GetModel(int AreaId, int ExpenseTypeId, System.Data.SqlClient.SqlTransaction trans)
        {
            return DataProvider.GetModel(AreaId, ExpenseTypeId,trans);
        }
    }
}
