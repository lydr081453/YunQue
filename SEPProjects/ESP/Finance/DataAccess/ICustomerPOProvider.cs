using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ICustomerPODataProvider
    {
        int Add(ESP.Finance.Entity.CustomerPOInfo model);
        int Add(ESP.Finance.Entity.CustomerPOInfo model,System.Data.SqlClient.SqlTransaction trans);
        int Update(ESP.Finance.Entity.CustomerPOInfo model);
        int Delete(int poId);

        ESP.Finance.Entity.CustomerPOInfo GetModel(int poId);

        //IList<ESP.Finance.Entity.CustomerInfo> GetAllList();
        //IList<ESP.Finance.Entity.CustomerInfo> GetList(string term);
        IList<ESP.Finance.Entity.CustomerPOInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);



        #region with sql trans
        //int Add(ESP.Finance.Entity.CustomerPOInfo model, bool isInTrans);
        //int Update(ESP.Finance.Entity.CustomerPOInfo model, bool isInTrans);
        //ESP.Finance.Entity.CustomerPOInfo GetModel(int poId,bool isInTrans);
        //IList<ESP.Finance.Entity.CustomerPOInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param,bool isInTrans);
        #endregion
    }
}
