using System;
using System.Data;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using ESP.HumanResource.DataAccess;

namespace ESP.HumanResource.BusinessLogic
{    
    public class LogManager
    {
        private static LogDataProvider dal = new LogDataProvider();
        public LogManager()
        {
        }

        public static int AddLog(LogInfo model, SqlTransaction trans)
        {
            return dal.Add(model, trans);
        }

        public static DataSet GetList(string strWhere, List<SqlParameter> parms)
        {
            return dal.GetList(strWhere, parms);
        }

        public static void Delete(string strWhere)
        {
            dal.Delete(strWhere);
        }

        public static LogInfo GetLogModel(string logDes, int userid, string username, int sysid, string sysusername, int status)
        {
            return dal.GetLogModel(logDes, userid, username, sysid, sysusername, status);
        }
    }
}
