using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.DataAccess;
using ESP.HumanResource.Entity;
using System.Data;

namespace ESP.HumanResource.BusinessLogic
{
    public class EmpLogManager
    {
        public readonly static EmpLogProvider Provider = new EmpLogProvider();

        public static int Add(EmpLogInfo model)
        {
            return Provider.Add(model);
        }

        public static bool Delete(int Id)
        {
            return Provider.Delete(Id);
        }

        public static EmpLogInfo GetModel(int Id)
        {
            return Provider.GetModel(Id);
        }

        public static IList<EmpLogInfo> GetList(string strWhere)
        {
            return Provider.GetList(strWhere);
        }
    }
}
