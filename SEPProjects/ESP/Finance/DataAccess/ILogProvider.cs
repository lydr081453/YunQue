using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ILogDataProvider
    {
        bool Exists(string term, List<System.Data.SqlClient.SqlParameter> param);
        int Add(ESP.Finance.Entity.LogInfo model);
        int Add(string operation, string tablename, string des);

        int AddException(Exception ex);
        int Update(ESP.Finance.Entity.LogInfo model);
        int Delete(int logId);
        ESP.Finance.Entity.LogInfo GetModel(int logId);


        //IList<ESP.Finance.Entity.LogInfo> GetAllList();
        //IList<ESP.Finance.Entity.LogInfo> GetList(string term);
        IList<ESP.Finance.Entity.LogInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);


        #region with sql trans
        //bool Exists(string term, List<System.Data.SqlClient.SqlParameter> param, bool isInTrans);
        //int Add(string operation, string tablename, string des,bool isInTrans);
        //int Add(ESP.Finance.Entity.LogInfo model,bool isInTrans);
        //int Update(ESP.Finance.Entity.LogInfo model, bool isInTrans);
        //int Delete(int customerId, bool isInTrans);

        //ESP.Finance.Entity.LogInfo GetModel(int logId,bool isInTrans);
        //IList<ESP.Finance.Entity.LogInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param,bool isInTrans);
        #endregion
    }
}
