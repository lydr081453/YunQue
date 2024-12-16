using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Purchase.Entity;
using ESP.Purchase.DataAccess;
namespace ESP.Purchase.BusinessLogic
{
   public static  class PrintLogManager
    {
       private static PrintLogDataProvider dal = new PrintLogDataProvider();

       public static IList<ESP.Purchase.Entity.PrintLogInfo> GetList(string strWhere, List<SqlParameter> parameters)
       {
           return dal.GetList(strWhere,parameters);
       }
       public static ESP.Purchase.Entity.PrintLogInfo GetModel(int PrintID)
       {
           return dal.GetModel(PrintID);
       }
       public static ESP.Purchase.Entity.PrintLogInfo GetModelByFormID(int FormID)
       {
           return dal.GetModelByFormID(FormID,1);
       }
       public static int Delete(int PrintID) 
       {
           return dal.Delete(PrintID);
       }
       public static int Update(ESP.Purchase.Entity.PrintLogInfo model) 
       {
           return dal.Update(model);
       }
       public static int Add(ESP.Purchase.Entity.PrintLogInfo model) 
       {
           return dal.Add(model);
       }
    }
}
