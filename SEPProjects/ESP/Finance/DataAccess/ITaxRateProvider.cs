using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ITaxRateDataProvider
    {
        bool Exists(string term, List<System.Data.SqlClient.SqlParameter> param);
        int Add(ESP.Finance.Entity.TaxRateInfo model);
        int Update(ESP.Finance.Entity.TaxRateInfo model);
        //int UpdateByCustomer(ESP.Finance.Entity.TaxRateInfo model);
        int Delete(int taxrateid);
        ESP.Finance.Entity.TaxRateInfo GetModel(int taxrateid);

        ESP.Finance.Entity.TaxRateInfo GetModel( int bizTypeId, int branchId);

        //IList<ESP.Finance.Entity.TaxRateInfo> GetAllList();
        //IList<ESP.Finance.Entity.TaxRateInfo> GetList(string term);
        IList<ESP.Finance.Entity.TaxRateInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        IList<ESP.Finance.Entity.TaxRateInfo> GetList( int branchId, string term, List<System.Data.SqlClient.SqlParameter> param);

        #region is in trans
        //bool Exists(string term, List<System.Data.SqlClient.SqlParameter> param, bool isInTrans);
        //int Add(ESP.Finance.Entity.TaxRateInfo model,bool isInTrans);
        //int Update(ESP.Finance.Entity.TaxRateInfo model, bool isInTrans);
        
        //int UpdateByCustomer(ESP.Finance.Entity.TaxRateInfo model,bool isInTrans);
        //ESP.Finance.Entity.TaxRateInfo GetModel( int bizTypeId, int branchId,bool isInTrans);
        //IList<ESP.Finance.Entity.TaxRateInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        //IList<ESP.Finance.Entity.TaxRateInfo> GetList(int bizTypeId, int branchId, string term, List<System.Data.SqlClient.SqlParameter> param);
        #endregion
    }
}
