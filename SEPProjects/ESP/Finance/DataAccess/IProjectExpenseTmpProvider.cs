using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IProjectExpenseTmpDataProvider
    {
        int Add(ESP.Finance.Entity.ProjectExpenseTmpInfo model);
        int Update(ESP.Finance.Entity.ProjectExpenseTmpInfo model);
        int Delete(int projectExpenseId);

        ESP.Finance.Entity.ProjectExpenseTmpInfo GetModel(int projectExpenseId);

        IList<ESP.Finance.Entity.ProjectExpenseTmpInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        decimal GetTotalExpense(int projectId);

    }
}
