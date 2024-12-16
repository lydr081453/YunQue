using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IBranchDataProvider
    {
        int Add(ESP.Finance.Entity.BranchInfo model);
        int Update(ESP.Finance.Entity.BranchInfo model); 
        int Delete(int branchId);
        ESP.Finance.Entity.BranchInfo GetModel(int branchId);
        ESP.Finance.Entity.BranchInfo GetModel(int branchId, System.Data.SqlClient.SqlTransaction trans);
        ESP.Finance.Entity.BranchInfo GetModelByCode(string Code);
        ESP.Finance.Entity.BranchInfo GetModelByCode(string Code,System.Data.SqlClient.SqlTransaction trans);
        IList<ESP.Finance.Entity.BranchInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        string GetLevel1Users(int departmentId);
        string GetLevel2Users(int departmentId);
        string GetLevel1Users();
        string GetDimissionAuditors(int departmentId);
        string GetLevel2Users();
        string GetSalaryUsers(int departmentId);
        string GetCardUsers(int departmentId);
    }
}
