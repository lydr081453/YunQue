using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.BusinessLogic
{
  public static  class PNBatchRelationManager
    {
      private static ESP.Finance.IDataAccess.IPNBatchRelationProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IPNBatchRelationProvider>.Instance; } }
      
      public static bool Exists(int batchID, int ReturnID)
      {
          return DataProvider.Exists(batchID, ReturnID);
      }
      public static int Add(ESP.Finance.Entity.PNBatchRelationInfo model)
      {
          return DataProvider.Add(model);
      }
      public static int Add(List<ESP.Finance.Entity.PNBatchRelationInfo> relationList)
      {
          return DataProvider.Add(relationList);
      }
      public static int Update(ESP.Finance.Entity.PNBatchRelationInfo model)
      {
          return DataProvider.Update(model);
      }
      public static int Delete(int BatchRelationID)
      {
          return DataProvider.Delete(BatchRelationID);
      }
      public static int DeleteByBatchID(int BatchID)
      {
          return DataProvider.DeleteByBatchID(BatchID);
      }
      public static int DeleteByBatchID(int BatchID,SqlTransaction trans)
      {
          return DataProvider.DeleteByBatchID(BatchID,trans );
      }
      public static int DeleteByReturnID(int batchid,ESP.Finance.Entity.ReturnInfo model)
      {
          return DataProvider.DeleteByReturnID(batchid,model);
      }
      public static ESP.Finance.Entity.PNBatchRelationInfo GetModel(int BatchRelationID)
      {
          return DataProvider.GetModel(BatchRelationID);
      }
      public static ESP.Finance.Entity.PNBatchRelationInfo GetModelByReturnId(int returnId,SqlTransaction trans)
      {
          return DataProvider.GetModelByReturnId(returnId, trans);
      }
      public static IList<ESP.Finance.Entity.PNBatchRelationInfo> GetList(string strWhere, List<SqlParameter> parameters)
      {
          return DataProvider.GetList(strWhere, parameters);
      }
      public static IList<ESP.Finance.Entity.PNBatchRelationInfo> GetList(string strWhere, List<SqlParameter> parameters,SqlTransaction trans)
      {
          return DataProvider.GetList(strWhere, parameters,trans);
      }
      public static int Delete(int batchid, int returnid)
      {
          return DataProvider.Delete(batchid, returnid);
      }
    }
}
