using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ITeamTaxProvider
    {
        int Add(ESP.Finance.Entity.TeamTaxInfo model);
        int Update(ESP.Finance.Entity.TeamTaxInfo model);
        int Delete(int deptId, int year, int month);
        ESP.Finance.Entity.TeamTaxInfo GetModel(int deptId, int year ,int month);
        IList<ESP.Finance.Entity.TeamTaxInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
