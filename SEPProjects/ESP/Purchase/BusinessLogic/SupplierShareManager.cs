using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Purchase.Entity;
using ESP.Purchase.DataAccess;

namespace ESP.Purchase.BusinessLogic
{
   public static  class SupplierShareManager
    {
       private static SupplierShareProvider dal = new SupplierShareProvider();

       public static int UpdateStatus(int supplySupplierId, int receiverId)
       {
           return dal.UpdateStatus(supplySupplierId, receiverId);
       }
       public static SupplierShareInfo GetModel(int id)
       {
           return dal.GetModel(id);
       }
       public static List<SupplierShareInfo> GetList(string sqlwhere)
       {
           return dal.GetList(sqlwhere);
       }
    }
}
