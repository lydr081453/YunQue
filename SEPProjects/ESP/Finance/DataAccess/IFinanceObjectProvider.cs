using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IFinanceObjectProvider
    {
        int Add(ESP.Finance.Entity.FinanceObjectInfo model);
        int Update(ESP.Finance.Entity.FinanceObjectInfo model);
        int Delete(int objectId);
        ESP.Finance.Entity.FinanceObjectInfo GetModel(int objectId);
        ESP.Finance.Entity.FinanceObjectInfo GetModel(string CredenceTypeCode, int RowLevel, string RowDesc);
        IList<ESP.Finance.Entity.FinanceObjectInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
