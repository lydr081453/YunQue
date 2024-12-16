using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IBranchDeptProvider
    {
        ESP.Finance.Entity.BranchDeptInfo GetModel(int branchid,int deptid);
        IList<ESP.Finance.Entity.BranchDeptInfo> GetList(int userid);
    }
}
