using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.BusinessLogic
{
     public static  class ExpenseBoarderManager
    {
         private static ESP.Finance.IDataAccess.IExpenseBoarderProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IExpenseBoarderProvider>.Instance; } }

         public static bool Exists(string CardNo, int userId)
         {
             return DataProvider.Exists(CardNo,userId);
         }
         public static int Add(ESP.Finance.Entity.ExpenseBoarderInfo model)
         {
             if (!Exists(model.CardNo,model.UserId))
                 return DataProvider.Add(model);
             else
                 return DataProvider.Update(model);
         }
         public static int Update(ESP.Finance.Entity.ExpenseBoarderInfo model)
         {
             return DataProvider.Update(model);
         }
         public static int Delete(int id)
         {
             return DataProvider.Delete(id);
         }
         public static ESP.Finance.Entity.ExpenseBoarderInfo GetModel(int Id)
         {
             return DataProvider.GetModel(Id);
         }
         public static IList<ESP.Finance.Entity.ExpenseBoarderInfo> GetList(string term)
         {
             return DataProvider.GetList(term);
         }
    }
}
