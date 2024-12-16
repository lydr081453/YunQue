using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IPaymentContentDataProvider
    {
        int Add(ESP.Finance.Entity.PaymentContentInfo model);
        int Update(ESP.Finance.Entity.PaymentContentInfo model);
        int Delete(int paymentContentId);
        ESP.Finance.Entity.PaymentContentInfo GetModel(int paymentContentId);

        //IList<ESP.Finance.Entity.PaymentContentInfo> GetAllList();
        //IList<ESP.Finance.Entity.PaymentContentInfo> GetList(string term);
        IList<ESP.Finance.Entity.PaymentContentInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
