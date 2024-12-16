using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ISupporterDataProvider
    {
        int Add(ESP.Finance.Entity.SupporterInfo model);
        int Add(ESP.Finance.Entity.SupporterInfo model,System.Data.SqlClient.SqlTransaction trans);
        int Update(ESP.Finance.Entity.SupporterInfo model);
        int Update(ESP.Finance.Entity.SupporterInfo model, System.Data.SqlClient.SqlTransaction trans);
        int UpdateProjectCode(int projectId, string projectCode);
        int UpdateProjectCode(int projectId, string projectCode,System.Data.SqlClient.SqlTransaction trans);
        int UpdateWorkFlow(int SupportID, int workItemID, string workItemName, int processID, int instanceID);
        int Delete(int supporterId);

        string CreateSupporterCode();
        ESP.Finance.Entity.SupporterInfo GetModel(int supporterId);
        ESP.Finance.Entity.SupporterInfo GetModel(int supporterId,System.Data.SqlClient.SqlTransaction trans);
        int GetSupporterCount(int projectid, int supporterid, int groupid);
        decimal GetTotalAmountByProject(int projectId);

        //IList<ESP.Finance.Entity.SupporterInfo> GetAllList();
        //IList<ESP.Finance.Entity.SupporterInfo> GetList(string term);
        IList<ESP.Finance.Entity.SupporterInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        IList<ESP.Finance.Entity.SupporterInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param, System.Data.SqlClient.SqlTransaction trans);
        IList<ESP.Finance.Entity.SupporterInfo> GetListByProject(int projectID, string term, List<System.Data.SqlClient.SqlParameter> param);
        IList<ESP.Finance.Entity.SupporterInfo> GetListByProject(int projectID, string term, List<System.Data.SqlClient.SqlParameter> param, System.Data.SqlClient.SqlTransaction trans);
        System.Data.DataTable GetSupportListJoinHist(int userId);
        int changeLeader(string supportIds, int oldUserId, int newUserId, ESP.Compatible.Employee currentUser);
        int changAuditor(string supportIds, int oldUserId, int newUserId, ESP.Compatible.Employee currentUser);
        IList<ESP.Finance.Entity.SupporterInfo> GetWaitAuditList(string userIds, string strTerms, List<System.Data.SqlClient.SqlParameter> parms);
        #region is in trans
        //int Add(ESP.Finance.Entity.SupporterInfo model,bool isInTrans);
        //int Update(ESP.Finance.Entity.SupporterInfo model,bool isInTrans);
        //int UpdateProjectCode(int projectId,string projectCode,bool isInTrans);
        //int UpdateWorkFlow(int SupportID, int workItemID, string workItemName, int processID, int instanceID,bool isInTrans);
        //decimal GetTotalAmountByProject(int projectId, bool isInTrans);
        //string CreateSupporterCode(bool isInTrans);

        //ESP.Finance.Entity.SupporterInfo GetModel(int supporterId,bool isIntrans);
        //IList<ESP.Finance.Entity.SupporterInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param,bool isInTrans);
        //IList<ESP.Finance.Entity.SupporterInfo> GetListByProject(int projectID, string term, List<System.Data.SqlClient.SqlParameter> param,bool isInTrans);
        #endregion

        IList<ESP.Finance.Entity.SupporterInfo> GetWaitAuditList(int[] userIds);
    }
}
