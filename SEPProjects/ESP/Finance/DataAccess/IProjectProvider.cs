using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.Framework.Entity;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IProjectDataProvider
    {
        bool Exists(string term, List<System.Data.SqlClient.SqlParameter> param);
        string CreateSerialCode();
        string CreateProjectCode(DateTime DeadLine,ESP.Finance.Entity.ProjectInfo model);
        int Add(ESP.Finance.Entity.ProjectInfo model);
        int Add(ESP.Finance.Entity.ProjectInfo model,System.Data.SqlClient.SqlTransaction trans);
        int Update(ESP.Finance.Entity.ProjectInfo model);
        int Update(ESP.Finance.Entity.ProjectInfo model,SqlTransaction trans);
        int UpdateWorkFlow(int projectId,int workItemID,string workItemName,int processID,int instanceID);
        int ChangeCheckContractStatus(int prjId,ESP.Finance.Utility.ProjectCheckContract status);
        int ChangeCheckContractStatus(int prjId, ESP.Finance.Utility.ProjectCheckContract status,SqlTransaction trans);
        int Delete(int prjId);
        ESP.Finance.Entity.ProjectInfo GetModel(int prjId);
        ESP.Finance.Entity.ProjectInfo GetModel(int prjId,SqlTransaction trans);
        //IList<ESP.Finance.Entity.ProjectInfo> GetAllList();
        //IList<ESP.Finance.Entity.ProjectInfo> GetList(string term);
        IList<ESP.Finance.Entity.ProjectInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        ESP.Finance.Entity.ProjectInfo GetModelByProjectCode(string projectCode);
        System.Data.DataTable GetProjectListJoinHist(int userId);
        int changeApplicant(string projectIds, int oldUserId, int newUserId, ESP.Compatible.Employee currentUser);
        int changAuditor(string projectIds, int oldUserId, int newUserId, ESP.Compatible.Employee currentUser);
        List<TaskItemInfo> GetTaskItems(string userIds);
        IList<ESP.Finance.Entity.ProjectInfo> GetWaitAuditList(string userIds, string strTerms, List<SqlParameter> parms);

        IList<ESP.Finance.Entity.ProjectInfo> GetWaitAuditList(int[] userIds);

        DataTable GetMaterialCost(string begindate, string enddate);

        IList<ESP.Finance.Entity.ProjectInfo> GetContractAuditingProjectList();
        IList<ESP.Finance.Entity.ProjectInfo> GetApplyForInvioceAuditingProjectList();
        #region is in a trans
        //bool Exists(string term, List<System.Data.SqlClient.SqlParameter> param,bool isInTrans);
        //string CreateSerialCode(bool isInTrans);
        //string CreateProjectCode(DateTime DeadLine,ESP.Finance.Entity.ProjectInfo model, bool isInTrans);
        //int Add(ESP.Finance.Entity.ProjectInfo model,bool isInTrans);
        //int Update(ESP.Finance.Entity.ProjectInfo model,bool isInTrans);
        //ESP.Finance.Entity.ProjectInfo GetModel(int prjId,bool isInTrans);
        //int UpdateWorkFlow(int projectId, int workItemID, string workItemName, int processID, int instanceID,bool isInTrans);
        //int ChangeCheckContractStatus(int prjId,Utility.ProjectCheckContract status,bool isInTrans);

        //ESP.Finance.Entity.ProjectInfo GetModel(int prjId,bool isInTrans);
        //IList<ESP.Finance.Entity.ProjectInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param,bool isInTrans);
        #endregion
    }
}
