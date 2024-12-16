using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IReturnSettingDataProvider
    {
        int Add(ESP.Finance.Entity.ReturnSettingInfo model);
        int Add(ESP.Finance.Entity.ReturnSettingInfo model,System.Data.SqlClient.SqlTransaction trans);
        int Update(ESP.Finance.Entity.ReturnSettingInfo model);
        int Delete(int returnSettingId);

        int DeleteByReturnId(int ReturnId);
        int DeleteByReturnId(int ReturnId, string term, List<System.Data.SqlClient.SqlParameter> param);
        int DeleteByReturnId(int ReturnId, string term, List<System.Data.SqlClient.SqlParameter> param,System.Data.SqlClient.SqlTransaction trans);
        ESP.Finance.Entity.ReturnSettingInfo GetModel(int returnSettingId);



        //IList<ESP.Finance.Entity.ReturnInfo> GetAllList();
        //IList<ESP.Finance.Entity.ReturnInfo> GetList(string term);
        IList<ESP.Finance.Entity.ReturnSettingInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);


        #region is in a Trans
        //int Add(ESP.Finance.Entity.ReturnSettingInfo model,bool isInTrans);
        //int DeleteByReturnId(int ReturnId,bool isInTrans);
        //int DeleteByReturnId(int ReturnId, string term, List<System.Data.SqlClient.SqlParameter> param,bool isInTrans);

        //ESP.Finance.Entity.ReturnSettingInfo GetModel(int returnSettingId,bool isInTrans);
        //IList<ESP.Finance.Entity.ReturnSettingInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param,bool isInTrans);
        #endregion
    }
}
