using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Entity;
using ESP.HumanResource.DataAccess;

namespace ESP.HumanResource.BusinessLogic
{
    public class RejectLogManager
    {
        private static readonly RejectLogDataProvider dataProvider = new RejectLogDataProvider();

        public static int Insert(RejectLogInfo model)
        {
            return dataProvider.Insert(model);
        }

        public static List<RejectLogInfo> GetList(int userId)
        {
            return dataProvider.GetList(userId);
        }
    }
}
