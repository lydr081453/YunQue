using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.BusinessLogic
{
    public class SC_SupplierprotocolTypeManager
    {
        public static bool insertInfos(string[] supplierAndTypeIds, int supplierType, int userId)
        {
            return new ESP.Supplier.DataAccess.SC_SupplierprotocolTypeProvider().insertInfos(supplierAndTypeIds, supplierType, userId);
        }
        public static int GetListForSupplier(int supplierid, int typeid)
        {
            return new ESP.Supplier.DataAccess.SC_SupplierprotocolTypeProvider().GetListForSupplier(supplierid, typeid);
        }

        public static int GetListForSupplier(int supplierid)
        {
            return new ESP.Supplier.DataAccess.SC_SupplierprotocolTypeProvider().GetListForSupplier(supplierid);
        }
    }
}
