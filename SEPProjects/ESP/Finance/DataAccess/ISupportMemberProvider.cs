using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ISupportMemberDataProvider
    {
        bool Exists(string term, List<System.Data.SqlClient.SqlParameter> param);
        int Add(ESP.Finance.Entity.SupportMemberInfo model);
        int Add(ESP.Finance.Entity.SupportMemberInfo model, System.Data.SqlClient.SqlTransaction trans);
        int Update(ESP.Finance.Entity.SupportMemberInfo model);
        int Update(ESP.Finance.Entity.SupportMemberInfo model,System.Data.SqlClient.SqlTransaction trans);
        int Delete(int supMemId);
        ESP.Finance.Entity.SupportMemberInfo GetModel(int supMemId);
        ESP.Finance.Entity.SupportMemberInfo GetModelBySupporterMember(int SupporterId, int memberUserId);

        //IList<ESP.Finance.Entity.SupportMemberInfo> GetAllList();
        //IList<ESP.Finance.Entity.SupportMemberInfo> GetList(string term);
        IList<ESP.Finance.Entity.SupportMemberInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        IList<ESP.Finance.Entity.SupportMemberInfo> GetList(int supportId, SqlTransaction trans);
        IList<ESP.Finance.Entity.SupportMemberInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param, SqlTransaction trans);

        #region is in a trans
        //bool Exists(string term, List<System.Data.SqlClient.SqlParameter> param, bool isInTrans);
        //int Add(ESP.Finance.Entity.SupportMemberInfo model, bool isInTrans);
        //int Update(ESP.Finance.Entity.SupportMemberInfo model, bool isInTrans);



        //ESP.Finance.Entity.SupportMemberInfo GetModel(int supMemId,bool isInTrans);
        //ESP.Finance.Entity.SupportMemberInfo GetModelBySupporterMember(int SupporterId, int memberUserId,bool isInTrans);
        //IList<ESP.Finance.Entity.SupportMemberInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param,bool isInTrans);
        #endregion
    }
}
