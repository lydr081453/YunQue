using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IProjectExpenseDataProvider
    {
        int Add(ESP.Finance.Entity.ProjectExpenseInfo model);
        int Add(ESP.Finance.Entity.ProjectExpenseInfo model, SqlTransaction trans);
        int Update(ESP.Finance.Entity.ProjectExpenseInfo model);
        int Delete(int projectExpenseId);
        int Delete(int projectExpenseId,SqlTransaction trans);

        ESP.Finance.Entity.ProjectExpenseInfo GetModel(int projectExpenseId);

        //IList<ESP.Finance.Entity.CustomerInfo> GetAllList();
        //IList<ESP.Finance.Entity.CustomerInfo> GetList(string term);
        IList<ESP.Finance.Entity.ProjectExpenseInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        decimal GetTotalExpense(int projectId);


        #region with sql trans
        //int Add(ESP.Finance.Entity.ProjectExpenseInfo model, bool isInTrans);
        //int Update(ESP.Finance.Entity.ProjectExpenseInfo model, bool isInTrans);
        //decimal GetTotalExpense(int projectId, bool isInTrans);

        //ESP.Finance.Entity.ProjectExpenseInfo GetModel(int projectExpenseId,bool isInTrans);
        //IList<ESP.Finance.Entity.ProjectExpenseInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param,bool isInTrans);
        #endregion
    }
}
