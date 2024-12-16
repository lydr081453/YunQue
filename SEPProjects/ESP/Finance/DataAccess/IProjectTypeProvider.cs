using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IProjectTypeDataProvider
    {
        int Add(ESP.Finance.Entity.ProjectTypeInfo model);
        int Update(ESP.Finance.Entity.ProjectTypeInfo model);
        int Delete(int projectTypeId);
        ESP.Finance.Entity.ProjectTypeInfo GetModel(int projectTypeId);

        //IList<ESP.Finance.Entity.ProjectTypeInfo> GetAllList();
        //IList<ESP.Finance.Entity.ProjectTypeInfo> GetList(string term);
        IList<ESP.Finance.Entity.ProjectTypeInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
