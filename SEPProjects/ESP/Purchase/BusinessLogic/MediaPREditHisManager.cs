using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

namespace ESP.Purchase.BusinessLogic
{
  public static  class MediaPREditHisManager
    {
      private static MediaPREditHisDataProvider dal = new MediaPREditHisDataProvider();
      
      public static MediaPREditHisInfo GetModel(int id)
      {
          return dal.GetModel(id);
      }
      public static int ADD(ESP.Purchase.Entity.MediaPREditHisInfo model,SqlConnection conn, SqlTransaction trans)
      {
         return dal.ADD(model,conn,trans);
      }
      public static void Update(ESP.Purchase.Entity.MediaPREditHisInfo model, SqlConnection conn, SqlTransaction trans)
      {
          dal.Update(model,conn,trans);
      }
      public static IList<MediaPREditHisInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> parms)
      {
          return dal.GetList(term, parms);
      }

      public static IList<MediaPREditHisInfo> GetList(string term, System.Data.SqlClient.SqlConnection conn,System.Data.SqlClient.SqlTransaction trans, List<System.Data.SqlClient.SqlParameter> parms)
      {
          return dal.GetList(term,conn,trans, parms);
      }
      public static MediaPREditHisInfo GetModelByNewPRID(int id)
      {
          return dal.GetModelByNewPRID(id);
      }
      public static MediaPREditHisInfo GetModelByOldPRID(int id)
      {
          return dal.GetModelByOldPRID(id);
      }
      public static IList<MediaPREditHisInfo> GetModelByOldPRIDs(int[] ids)
      {
          if(ids == null  || ids.Length == 0)
              return new List<MediaPREditHisInfo>();

          var term = new System.Text.StringBuilder(" OldPRID in (").Append(ids[0]);
          for (var i = 1; i < ids.Length; i++)
          {
              term.Append(",").Append(ids[i]);
          }
          term.Append(") ");

          return GetList(term.ToString(), new List<SqlParameter>());
      }
    }
}
