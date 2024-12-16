using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ICheckDataProvider
    {
        bool Exists(string CheckSysCode, string CheckCode);
        bool Exists(string CheckSysCode, string CheckCode,System.Data.SqlClient.SqlTransaction trans);
        int Add(ESP.Finance.Entity.CheckInfo model);
        int Add(ESP.Finance.Entity.CheckInfo model,System.Data.SqlClient.SqlTransaction trans);
        int Update(ESP.Finance.Entity.CheckInfo model);
        int Delete(int checkId);
        ESP.Finance.Entity.CheckInfo GetModel(int checkId);

        //IList<ESP.Finance.Entity.BranchInfo> GetAllList();
        //IList<ESP.Finance.Entity.BranchInfo> GetList(string term);
        IList<ESP.Finance.Entity.CheckInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);

        #region is in a trans
        //int Add(ESP.Finance.Entity.CheckInfo model, bool isInTrans);
        //int Update(ESP.Finance.Entity.CheckInfo model, bool isInTrans);

        //ESP.Finance.Entity.CheckInfo GetModel(int checkId,bool isInTrans);
        //IList<ESP.Finance.Entity.CheckInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param,bool isInTrans);
        #endregion
    }
}
