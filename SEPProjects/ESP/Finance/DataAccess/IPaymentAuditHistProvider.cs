using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IPaymentAuditHistDataProvider
    {
        int Add(ESP.Finance.Entity.PaymentAuditHistInfo model);
        int Add(ESP.Finance.Entity.PaymentAuditHistInfo model,System.Data.SqlClient.SqlTransaction trans);
        int Update(ESP.Finance.Entity.PaymentAuditHistInfo model);
        int Update(ESP.Finance.Entity.PaymentAuditHistInfo model, System.Data.SqlClient.SqlTransaction trans);
        int Delete(int AuditId);
        ESP.Finance.Entity.PaymentAuditHistInfo GetModel(int AuditId);

        IList<ESP.Finance.Entity.PaymentAuditHistInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);

        int DeleteByPaymentId(int PaymentId);
        int DeleteByPaymentId(int PaymentId, string term, List<System.Data.SqlClient.SqlParameter> param);
        int DeleteByPaymentId(int PaymentId, string term, List<System.Data.SqlClient.SqlParameter> param, System.Data.SqlClient.SqlTransaction trans);


        #region with sql trans
        //int Add(ESP.Finance.Entity.PaymentAuditHistInfo model, bool isInTrans);
        //int Update(ESP.Finance.Entity.PaymentAuditHistInfo model, bool isInTrans);
        //int DeleteByPaymentId(int PaymentId, bool isInTrans);
        //int DeleteByPaymentId(int PaymentId, string term, List<System.Data.SqlClient.SqlParameter> param, bool isInTrans);

        //ESP.Finance.Entity.PaymentAuditHistInfo GetModel(int AuditId,bool isInTrans);
        //IList<ESP.Finance.Entity.PaymentAuditHistInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param,bool isInTrans);
        #endregion
    }
}
