using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Supplier.Entity;
using ESP.Supplier.DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Supplier.BusinessLogic
{

    public class SC_SupplierSubsidiaryUsersManager
    {
        private static readonly SC_SupplierSubsidiaryUsersProvider dal = new SC_SupplierSubsidiaryUsersProvider();

        public static int Add(SC_SupplierSubsidiaryUsers model)
        {
            return dal.Add(model,null);
        }

        public static void Update(SC_SupplierSubsidiaryUsers model)
        {
            dal.Update(model, null);
        }

        public static SC_SupplierSubsidiaryUsers GetModel(int id)
        {
            return dal.GetModel(id);
        }

        public static IList<SC_SupplierSubsidiaryUsers> GetList(string strCondition)
        {
            return dal.GetList(strCondition);
        }
        public static int Delete(int id)
        {
            return dal.Delete(id,null);
        }
    }
}
