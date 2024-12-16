using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Entity;
using System.Data.SqlClient;

namespace ESP.Finance.BusinessLogic
{
   public static class CredenceTypeManager
    {
       private static ESP.Finance.IDataAccess.ICredeneTypeProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.ICredeneTypeProvider>.Instance; } }

       public static int Add(CredenceTypeInfo model)
        {
            return DataProvider.Add(model);
        }

        public static int Update(CredenceTypeInfo model)
        {
            return DataProvider.Update(model);
        }

        public static int Delete(int typeId)
        {
            return DataProvider.Delete(typeId);
        }

        public static CredenceTypeInfo GetModel(int typeid)
        {
            return DataProvider.GetModel(typeid);
        }

        public static IList<CredenceTypeInfo> GetList(string term, List<SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }
    }
}
