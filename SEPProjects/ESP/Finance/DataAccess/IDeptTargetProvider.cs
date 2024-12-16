﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IDeptTargetProvider
    {
        ESP.Finance.Entity.DeptTargetInfo GetModel(int deptid,int year);
        IList<ESP.Finance.Entity.DeptTargetInfo> GetList(string term);
    }
}