using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Entity;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IInvoiceDataProvider
    {
        int Add(ESP.Finance.Entity.InvoiceInfo model);
        int Update(ESP.Finance.Entity.InvoiceInfo model);
        bool Exists(string term, List<System.Data.SqlClient.SqlParameter> param);
        int Delete(int invoiceId);
        ESP.Finance.Entity.InvoiceInfo GetModel(int invoiceId);
        int UpdateStatusByPaymentID(int paymentId,int status);

        //IList<ESP.Finance.Entity.InvoiceInfo> GetAllList();
        //IList<ESP.Finance.Entity.InvoiceInfo> GetList(string term);
        IList<ESP.Finance.Entity.InvoiceInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
        bool InsertInvoiceAndDetail(InvoiceInfo model, List<ESP.Finance.Entity.InvoiceDetailInfo> dlist);

        #region is in Trans
        //int Add(ESP.Finance.Entity.InvoiceInfo model,bool isInTrans);
        //int UpdateStatusByPaymentID(int paymentId,int status, bool isInTrans);
        //bool Exists(string term, List<System.Data.SqlClient.SqlParameter> param, bool isInTrans);
        //ESP.Finance.Entity.InvoiceInfo GetModel(int invoiceId,bool isInTrans);
        //IList<ESP.Finance.Entity.InvoiceInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param,bool isInTrans);
        #endregion 
    }
}
