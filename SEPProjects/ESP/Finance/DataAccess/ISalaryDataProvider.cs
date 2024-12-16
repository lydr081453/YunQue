using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ISalaryDataProvider
    {
        int Add(ESP.Finance.Entity.SalaryInfo model);
        int Delete(int year, int month, int importer);
        ESP.Finance.Entity.SalaryInfo GetModel(int userid, int year,int month );
        int UpdatePassword(int userid, int year,int month, string pwd);
        IList<ESP.Finance.Entity.SalaryInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
