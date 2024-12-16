using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ISupporterCostDataProvider
    {
        int Add(ESP.Finance.Entity.SupporterCostInfo model);
        int Update(ESP.Finance.Entity.SupporterCostInfo model);
        int Delete(int supporterCostId);
        ESP.Finance.Entity.SupporterCostInfo GetModel(int supporterCostId);

        decimal GetTotalCostBySupporter(int SupporterId);

        //IList<ESP.Finance.Entity.SupporterCostInfo> GetAllList();
        //IList<ESP.Finance.Entity.SupporterCostInfo> GetList(string term);
        IList<ESP.Finance.Entity.SupporterCostInfo> GetList(int SupporterId,SqlTransaction trans);
        IList<ESP.Finance.Entity.SupporterCostInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        IList<ESP.Finance.Entity.SupporterCostInfo> GetList(int SupporterId,string term, List<System.Data.SqlClient.SqlParameter> param);

        #region is in a trans
        //int Add(ESP.Finance.Entity.SupporterCostInfo model,bool isInTrans);
        //int Update(ESP.Finance.Entity.SupporterCostInfo model,bool isInTrans);
        //decimal GetTotalCostBySupporter(int SupporterId,bool isInTrans);

        //ESP.Finance.Entity.SupporterCostInfo GetModel(int supporterCostId,bool isInTrans);
        //IList<ESP.Finance.Entity.SupporterCostInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param, bool isInTrans);
        //IList<ESP.Finance.Entity.SupporterCostInfo> GetList(int SupporterId, string term, List<System.Data.SqlClient.SqlParameter> param, bool isInTrans);
        #endregion
    }
}
