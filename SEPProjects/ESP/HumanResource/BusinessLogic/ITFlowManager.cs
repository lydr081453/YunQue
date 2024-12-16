using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Entity;
using System.Data.SqlClient;


namespace ESP.HumanResource.BusinessLogic
{
   public class ITFlowManager
    {
       private static ESP.HumanResource.DataAccess.ITFlowProvider dataProvider = new ESP.HumanResource.DataAccess.ITFlowProvider();

        public static ITFlowInfo GetModel(int id)
        {
            return dataProvider.GetModel(id);
        }
        public static int Add(ITFlowInfo model)
        {
            return dataProvider.Add(model);
        }
        public static IList<ITFlowInfo> GetList(string term, List<SqlParameter> param)
        {
            return dataProvider.GetList(term, param);
        }

        public static int Update(ITFlowInfo model)
        {
            return dataProvider.Update(model);
        }
        public static int Delete(int id)
        {
            return dataProvider.Delete(id);
        }
    }
}
