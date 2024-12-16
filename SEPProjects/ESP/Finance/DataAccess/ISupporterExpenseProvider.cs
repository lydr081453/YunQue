using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ISupporterExpenseDataProvider
    {
        int Add(ESP.Finance.Entity.SupporterExpenseInfo model);
        int Update(ESP.Finance.Entity.SupporterExpenseInfo model);
        int Delete(int expenseId);
        ESP.Finance.Entity.SupporterExpenseInfo GetModel(int expenseId);
        decimal GetTotalExpense(int supporterId);

        //IList<ESP.Finance.Entity.SupporterExpenseInfo> GetAllList();
        //IList<ESP.Finance.Entity.SupporterExpenseInfo> GetList(string term);
        IList<ESP.Finance.Entity.SupporterExpenseInfo> GetList(int supportId, SqlTransaction trans);
        IList<ESP.Finance.Entity.SupporterExpenseInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);

        #region with sql trans
        //int Add(ESP.Finance.Entity.SupporterExpenseInfo model, bool isInTrans);
        //int Update(ESP.Finance.Entity.SupporterExpenseInfo model, bool isInTrans);
        //decimal GetTotalExpense(int supporterId, bool isInTrans);

        //ESP.Finance.Entity.SupporterExpenseInfo GetModel(int expenseId, bool isInTrans);
        //IList<ESP.Finance.Entity.SupporterExpenseInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param, bool isInTrans);
        #endregion
    }
}
