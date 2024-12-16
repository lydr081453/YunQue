using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Entity;
using System.Data.SqlClient;

namespace ESP.HumanResource.BusinessLogic
{
   public class ItLogManager
    {
       private static ESP.HumanResource.DataAccess.ITLogProvider dataProvider = new ESP.HumanResource.DataAccess.ITLogProvider();

        public static ITLogInfo GetModel(int id)
        {
            return dataProvider.GetModel(id);
        }
        public static int Add(ITLogInfo model)
        {
            return dataProvider.Add(model);
        }
        public static IList<ITLogInfo> GetList(int itemId)
        {
            return dataProvider.GetList(itemId);
        }

        public static int Update(ITLogInfo model)
        {
            return dataProvider.Update(model);
        }
        public static int Delete(int id)
        {
            return dataProvider.Delete(id);
        }
    }
}
