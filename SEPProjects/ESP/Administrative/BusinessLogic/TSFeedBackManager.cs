using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.DataAccess;
using ESP.Administrative.Entity;

namespace ESP.Administrative.BusinessLogic
{
    public class TSFeedBackManager
    {
        public readonly static TSFeedBackDataProvider Provider = new TSFeedBackDataProvider();

        public static int Add(TSFeedBackInfo model)
        {
            return Provider.Add(model);
        }

        public static List<TSFeedBackInfo> GetList(string strWhere)
        {
            return Provider.GetList(strWhere);
        }

        public static TSFeedBackInfo GetModel(int Id)
        {
            return Provider.GetModel(Id);
        }
    }
}
