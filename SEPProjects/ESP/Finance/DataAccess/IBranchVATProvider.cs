using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{

    [ESP.Configuration.Provider]
    public interface IBranchVATProvider
    {
        IList<ESP.Finance.Entity.BranchVATInfo> GetList(string strwhere);
    }
}
