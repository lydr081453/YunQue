using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IBranchProjectProvider
    {
        ESP.Finance.Entity.BranchProjectInfo GetModel(int branchid, int deptid);
        IList<ESP.Finance.Entity.BranchProjectInfo> GetList(int auditorId);
    }
}
