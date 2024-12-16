using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ITicketSupplierProvider
    {
        ESP.Finance.Entity.TicketSupplier GetModel(int supplierId);
        IList<ESP.Finance.Entity.TicketSupplier> GetList(string term,List<SqlParameter> param);
    }
}
