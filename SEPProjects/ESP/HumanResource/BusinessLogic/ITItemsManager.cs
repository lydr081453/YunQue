using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Entity;
using System.Data.SqlClient;

namespace ESP.HumanResource.BusinessLogic
{
   public class ITItemsManager
    {
       private static ESP.HumanResource.DataAccess.ITItemsDataProvider dataProvider = new ESP.HumanResource.DataAccess.ITItemsDataProvider();
       
       public static ITItemsInfo GetModel(int id)
        {
            return dataProvider.GetModel(id);
        }
        public static int Add(ITItemsInfo model)
        {
            return dataProvider.Add(model);
        }
        public static IList<ITItemsInfo> GetList(string term, List<SqlParameter> param)
        {
            return dataProvider.GetList(term, param);
        }

        public static int Update(ITItemsInfo model)
        {
            return dataProvider.Update(model);
        }
        public static int Delete(int id)
        {
            return dataProvider.Delete(id);
        }
    }
}
