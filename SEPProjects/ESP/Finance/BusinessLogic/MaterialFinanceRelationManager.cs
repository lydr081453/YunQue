using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Entity;
using System.Data.SqlClient;

namespace ESP.Finance.BusinessLogic
{
    public static class MaterialFinanceRelationManager
    {
        private static ESP.Finance.IDataAccess.IMaterialFinanceRelationProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IMaterialFinanceRelationProvider>.Instance; } }

        public static int Add(MaterialFinanceRelationInfo model)
        {
            return DataProvider.Add(model);
        }

        public static int Update(MaterialFinanceRelationInfo model)
        {
            return DataProvider.Update(model);
        }

        public static int Delete(int typeid)
        {
            return DataProvider.Delete(typeid);
        }

        public static MaterialFinanceRelationInfo GetModel(int materialId,int materialType)
        {
            return DataProvider.GetModel(materialId, materialType);
        }

        public static IList<MaterialFinanceRelationInfo> GetList(string term, List<SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }
    }
}
