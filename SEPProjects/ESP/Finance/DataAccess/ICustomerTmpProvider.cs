using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{

    [ESP.Configuration.Provider]
    public interface ICustomerTmpDataProvider
    {

        bool Exists(string term, List<System.Data.SqlClient.SqlParameter> param);


        int Add(ESP.Finance.Entity.CustomerTmpInfo model);

        int Update(ESP.Finance.Entity.CustomerTmpInfo model);
        int Delete(int customerId);
        ESP.Finance.Entity.CustomerTmpInfo GetModel(int customerId);

        string CreateCustomerCode(string shorten);

        //IList<ESP.Finance.Entity.CustomerInfo> GetAllList();
        //IList<ESP.Finance.Entity.CustomerInfo> GetList(string term);
        IList<ESP.Finance.Entity.CustomerTmpInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);



        #region with sql trans
        //bool Exists(string term, List<System.Data.SqlClient.SqlParameter> param, bool isInTrans);
        //string CreateCustomerCode(string shorten,bool isInTrans);
        //int Add(ESP.Finance.Entity.CustomerTmpInfo model, bool isInTrans);
        //int Update(ESP.Finance.Entity.CustomerTmpInfo model, bool isInTrans);
        //int Delete(int customerId, bool isInTrans);
        //ESP.Finance.Entity.CustomerTmpInfo GetModel(int customerId, bool isInTrans);
        //IList<ESP.Finance.Entity.CustomerTmpInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param, bool isInTrans);
        #endregion
    }
}
