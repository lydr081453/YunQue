using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IExpenseAccountDetailHisProvider
    {
        int Add(ESP.Finance.Entity.ExpenseAccountDetailHisInfo model);
        void Update(ESP.Finance.Entity.ExpenseAccountDetailHisInfo model);
        int Delete(int id);
        ESP.Finance.Entity.ExpenseAccountDetailHisInfo GetModel(int HisID);
        DataSet GetDsList(string strWhere);
        List<ESP.Finance.Entity.ExpenseAccountDetailHisInfo> GetList(string strWhere); 
    }
}
