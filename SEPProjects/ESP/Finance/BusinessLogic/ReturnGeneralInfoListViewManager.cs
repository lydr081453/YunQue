using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.BusinessLogic
{
    public static class ReturnGeneralInfoListViewManager
    {
        private static ESP.Finance.IDataAccess.IGetReturnGeneralInfoListDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IGetReturnGeneralInfoListDataProvider>.Instance;}}
        //private const string _dalProviderName = "ReturnGeneralInfoListDALProviderViewInfo";

        



        #region IReturnGeneralInfoListProviderViewInfo 成员

        public static IList<ESP.Finance.Entity.ReturnGeneralInfoListViewInfo> GetAllList()
        {
            return GetList(string.Empty,null);
        }

        public static IList<ESP.Finance.Entity.ReturnGeneralInfoListViewInfo> GetList(string term)
        {
            return GetList(term,null);
        }

        public static IList<ESP.Finance.Entity.ReturnGeneralInfoListViewInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
           return DataProvider.GetList(term, param);
        }

        #endregion

        #region IReturnGeneralInfoListProviderViewInfo 成员

        public static ESP.Finance.Entity.ReturnGeneralInfoListViewInfo GetModel(int id)
        {
            return DataProvider.GetModel(id);
        }

        #endregion
    }
}
