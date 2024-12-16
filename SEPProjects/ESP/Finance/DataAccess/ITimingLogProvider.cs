using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Entity;
namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ITimingLogProvider
    {
        bool Exists(int ID);

        int Add(TimingLogInfo model);

         int Update(TimingLogInfo model);

        int Delete(int ID);

        ESP.Finance.Entity.TimingLogInfo GetModel(int ID);
        List<ESP.Finance.Entity.TimingLogInfo> GetList(string strWhere);
    }
}
