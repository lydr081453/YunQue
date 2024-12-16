using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Reflection;

namespace ESP.Finance.BusinessLogic
{
  public static  class SupportHistoryManager
    {
      private static ESP.Finance.IDataAccess.ISupportHistoryProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.ISupportHistoryProvider>.Instance; } }

      public static int Add(ESP.Finance.Entity.SupportHistoryInfo model)
      {
          return DataProvider.Add(model);
      }

      public static int Add(ESP.Finance.Entity.SupporterInfo model,SqlTransaction trans)
      {
          ESP.Finance.Entity.SupporterInfo modelWithList = ESP.Finance.BusinessLogic.SupporterManager.GetModelWithList(model.SupportID,trans);
          SupportHistoryInfo hist = GetHist(modelWithList, trans);
          return DataProvider.Add(hist, trans);
      }


      public static ESP.Finance.Entity.SupportHistoryInfo GetModel(int supportId)
      {
          return DataProvider.GetModel(supportId);
      }
      public static IList<ESP.Finance.Entity.SupportHistoryInfo> GetListBySupport(int supportId)
      {
          return DataProvider.GetListBySupport(supportId);
      }

      private static ESP.Finance.Entity.SupportHistoryInfo GetHist(ESP.Finance.Entity.SupporterInfo support, SqlTransaction trans)
      {
          Entity.SupportHistoryInfo hist = new SupportHistoryInfo();

          if (support != null && support.SupportID > 0)
          {
              support = SupporterManager.GetModelWithList(support.SupportID, trans);//得到project的所有信息,并序列化
              hist.SupportId = support.SupportID;
              hist.CommitDate = DateTime.Now;
              hist.HistoryData = support.ObjectSerialize();
          }
          return hist;
      }
    }
}
