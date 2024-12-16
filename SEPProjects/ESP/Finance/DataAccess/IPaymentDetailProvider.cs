using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface IPaymentDetailDataProvider
    {
        int Add(ESP.Finance.Entity.PaymentDetailInfo model);
        int Update(ESP.Finance.Entity.PaymentDetailInfo model);
        int Delete(int paymentDetailId);
        ESP.Finance.Entity.PaymentDetailInfo GetModel(int paymentDetailId);
        IList<ESP.Finance.Entity.PaymentDetailInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);
    }
}
