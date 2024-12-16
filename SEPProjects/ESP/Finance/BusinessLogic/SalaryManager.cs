using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
namespace ESP.Finance.BusinessLogic
{

    public static class SalaryManager
    {
        private static ESP.Finance.IDataAccess.ISalaryDataProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.ISalaryDataProvider>.Instance; } }

        public static int Add(ESP.Finance.Entity.SalaryInfo model)
        {
            return DataProvider.Add(model);
        }

        public static int Delete( int year ,int month,int importer)
        {
            return DataProvider.Delete(year, month, importer);
        }


        public static ESP.Finance.Entity.SalaryInfo GetModel(int userid , int year,int month)
        {
            return DataProvider.GetModel(userid, year, month);
        }

        public static IList<ESP.Finance.Entity.SalaryInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }

        public static int UpdatePassword(int userid, int year,int month, string pwd)
        {
            return DataProvider.UpdatePassword(userid, year,month, pwd);
        }


    }
}
