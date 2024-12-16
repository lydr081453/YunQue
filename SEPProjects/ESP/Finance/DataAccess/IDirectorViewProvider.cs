using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IDirectorDataViewProvider
    {
        int GetDirector(int depId);
        Entity.DirectorViewInfo GetModelByDepId(int DepId);
        string GetDirectors();
        string GetManagers();
    }
}
