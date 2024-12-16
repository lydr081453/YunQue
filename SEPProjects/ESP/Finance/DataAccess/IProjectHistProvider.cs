using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IProjectHistDataProvider
    {

        int Add(ESP.Finance.Entity.ProjectHistInfo model);
        int Add(ESP.Finance.Entity.ProjectHistInfo model,System.Data.SqlClient.SqlTransaction trans);
        int Update(ESP.Finance.Entity.ProjectHistInfo model);
        int Delete(int prjHistId);
        int GetNewVersion(int projectId);

        ESP.Finance.Entity.ProjectHistInfo GetModel(int prjHistId);

        //IList<ESP.Finance.Entity.ProjectHistInfo> GetAllList();
        //IList<ESP.Finance.Entity.ProjectHistInfo> GetList(string term);
        IList<ESP.Finance.Entity.ProjectHistInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        IList<ESP.Finance.Entity.ProjectHistInfo> GetListByProject(int projectId, string term, List<System.Data.SqlClient.SqlParameter> param);

        #region is in a trans
        //int Add(ESP.Finance.Entity.ProjectHistInfo model,bool isInTrans);
        //int Update(ESP.Finance.Entity.ProjectHistInfo model,bool isInTrans);
        //int Delete(int prjHistId,bool isInTrans);
        //int GetNewVersion(int projectId, bool isIntrans);

        //ESP.Finance.Entity.ProjectHistInfo GetModel(int prjHistId,bool isInTrans);
        //IList<ESP.Finance.Entity.ProjectHistInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param,bool isInTrans);
        #endregion
    }
}
