using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IProjectMemberDataProvider
    {
        bool Exists(string term, List<System.Data.SqlClient.SqlParameter> param);
        int Add(ESP.Finance.Entity.ProjectMemberInfo model);
        int Add(ESP.Finance.Entity.ProjectMemberInfo model,System.Data.SqlClient.SqlTransaction trans);
        int Update(ESP.Finance.Entity.ProjectMemberInfo model);
        int Update(ESP.Finance.Entity.ProjectMemberInfo model,System.Data.SqlClient.SqlTransaction trans);
        int Delete(int prjMemId);
        ESP.Finance.Entity.ProjectMemberInfo GetModel(int prjMemId);
        ESP.Finance.Entity.ProjectMemberInfo GetModelByPrjMember(int projectId, int memberUserId);
        ESP.Finance.Entity.ProjectMemberInfo GetModelByPrjMember(int projectId, int memberUserId, System.Data.SqlClient.SqlTransaction trans);
        //IList<ESP.Finance.Entity.ProjectMemberInfo> GetAllList();
        //IList<ESP.Finance.Entity.ProjectMemberInfo> GetList(string term);
        IList<ESP.Finance.Entity.ProjectMemberInfo> GetListByProject(int projectID, string term, List<System.Data.SqlClient.SqlParameter> param);
        IList<ESP.Finance.Entity.ProjectMemberInfo> GetListByProject(int projectID, string term, List<System.Data.SqlClient.SqlParameter> param,System.Data.SqlClient.SqlTransaction trans);
        IList<ESP.Finance.Entity.ProjectMemberInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        IList<ESP.Finance.Entity.ProjectMemberInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param, System.Data.SqlClient.SqlTransaction trans);

        #region is in a trans
        //bool Exists(string term, List<System.Data.SqlClient.SqlParameter> param,bool isInTrans);
        //int Add(ESP.Finance.Entity.ProjectMemberInfo model,bool isInTrans);
        //int Update(ESP.Finance.Entity.ProjectMemberInfo model,bool isInTrans);

        //ESP.Finance.Entity.ProjectMemberInfo GetModel(int prjMemId,bool isInTrans);
        //ESP.Finance.Entity.ProjectMemberInfo GetModelByPrjMember(int projectId, int memberUserId,bool isInTrans);
        //IList<ESP.Finance.Entity.ProjectMemberInfo> GetListByProject(int projectID, string term, List<System.Data.SqlClient.SqlParameter> param,bool isInTrans);
        //IList<ESP.Finance.Entity.ProjectMemberInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param,bool isIntrans);
        #endregion
    }
}
