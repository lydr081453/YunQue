using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Purchase.DataAccess;
using System.Data.SqlClient;
using ESP.Purchase.Entity;

namespace ESP.Purchase.BusinessLogic
{
    public class PRAuthorizationManager
    {
        private static readonly PRAuthorizationProvider dal = new PRAuthorizationProvider();

        public static int Add(PRAuthorizationInfo model)
        {
            return dal.Add(model);
        }

        public static void Update(PRAuthorizationInfo model)
        {
            dal.Update(model);
        }

        public static void Delete(int Id)
        {
            dal.Delete(Id);
        }

        public static PRAuthorizationInfo GetModel(int Id)
        {
            return dal.GetModel(Id);
        }

        public static PRAuthorizationInfo GetUsedModel(int userId)
        {
            return dal.GetUsedModel(userId);
        }

        public static bool isHaveUsed(int userId)
        {
            return dal.isHaveUsed(userId);
        }

        public static List<PRAuthorizationInfo> GetList(string strWhere, List<SqlParameter> parms)
        {
            return dal.GetList(strWhere, parms);
        }
    }
}
