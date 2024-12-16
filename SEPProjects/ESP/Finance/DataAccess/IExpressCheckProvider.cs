 
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IExpressCheckProvider
    {
        int Add(ESP.Finance.Entity.ExpressCheckInfo model);
        int Update(ESP.Finance.Entity.ExpressCheckInfo model);
        int Delete(int id);
        int Delete(int year,int month);
        ESP.Finance.Entity.ExpressCheckInfo GetModel(int id);

        IList<ESP.Finance.Entity.ExpressCheckInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}