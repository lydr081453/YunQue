using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IInvoiceDetailDataProvider
    {
        int Add(ESP.Finance.Entity.InvoiceDetailInfo model);
        int Update(ESP.Finance.Entity.InvoiceDetailInfo model);
        //bool Exists(string term, List<System.Data.SqlClient.SqlParameter> param);
        int Delete(int invoiceDetailId);
        ESP.Finance.Entity.InvoiceDetailInfo GetModel(int invoiceDetailId);

        decimal GetTotalAmountByInvoice(int invoiceId);
        decimal GetTotalAmountByPayment(int PaymentID);

        //IList<ESP.Finance.Entity.InvoiceInfo> GetAllList();
        //IList<ESP.Finance.Entity.InvoiceInfo> GetList(string term);
        IList<ESP.Finance.Entity.InvoiceDetailInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);

        #region is in Trans
        //int Add(ESP.Finance.Entity.InvoiceDetailInfo model, bool isInTrans);
        //decimal GetTotalAmountByInvoice(int invoiceId,bool isInTrans);
        //decimal GetTotalAmountByPayment(int PaymentID, bool isInTrans);
        ////int UpdateStatusByPaymentID(int paymentId, int status, bool isInTrans);
        ////bool Exists(string term, List<System.Data.SqlClient.SqlParameter> param, bool isInTrans);
        //ESP.Finance.Entity.InvoiceDetailInfo GetModel(int invoiceDetailId, bool isInTrans);
        //IList<ESP.Finance.Entity.InvoiceDetailInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param, bool isInTrans);
        #endregion
    }
}
