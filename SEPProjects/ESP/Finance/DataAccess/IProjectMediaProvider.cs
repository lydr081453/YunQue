using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IProjectMediaDataProvider
    {
        int Add(ESP.Finance.Entity.ProjectMediaInfo model);
        int Update(ESP.Finance.Entity.ProjectMediaInfo model);
        int Add(ESP.Finance.Entity.ProjectMediaInfo model, SqlTransaction trans);
        int Update(ESP.Finance.Entity.ProjectMediaInfo model, SqlTransaction trans);
        int Delete(int areaid);
        ESP.Finance.Entity.ProjectMediaInfo GetModel(int areaid);

        //IList<ESP.Finance.Entity.ProjectMediaInfo> GetAllList();
        //IList<ESP.Finance.Entity.ProjectMediaInfo> GetList(string term);
        IList<ESP.Finance.Entity.ProjectMediaInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
