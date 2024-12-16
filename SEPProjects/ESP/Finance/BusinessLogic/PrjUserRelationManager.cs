using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.BusinessLogic
{
  public static   class PrjUserRelationManager
    {
      private static ESP.Finance.IDataAccess.IPrjUserRelationProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IPrjUserRelationProvider>.Instance; } }
      public static ESP.Finance.Entity.PrjUserRelationInfo GetModel(int PID)
      {
          return DataProvider.GetModel(PID);
      }

      /// <summary>
      /// 获得数据列表
      /// </summary>
      public static IList<ESP.Finance.Entity.PrjUserRelationInfo> GetList(string strWhere, List<System.Data.SqlClient.SqlParameter> paramlist)
      {
          return DataProvider.GetList(strWhere, paramlist);
      }
      /// <summary>
      /// 根据当前登录人获取可用的项目号首位
      /// </summary>
      /// <param name="CurrentUserID"></param>
      /// <returns></returns>
      public static string GetUsableBranchID(int CurrentUserID)
      {
          string strdept = string.Empty;
          string term = " userid=@userid and usable=1";
          List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
          System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@userid",System.Data.SqlDbType.Int,4);
          p1.Value = CurrentUserID;
          paramlist.Add(p1);
          IList<ESP.Finance.Entity.PrjUserRelationInfo> relationlist = ESP.Finance.BusinessLogic.PrjUserRelationManager.GetList(term,paramlist);
          foreach (ESP.Finance.Entity.PrjUserRelationInfo model in relationlist)
          {
              strdept += model.BranchID.ToString() + ",";
          }
          return strdept.TrimEnd(',');
      }
      /// <summary>
      /// 根据当前登录人获取可用的List
      /// </summary>
      /// <param name="CurrentUserID"></param>
      /// <returns></returns>
      public static IList<ESP.Finance.Entity.PrjUserRelationInfo> GetUsableList(int CurrentUserID)
      {
          string strdept = string.Empty;
          string term = " userid=@userid and usable=1";
          List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
          System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@userid", System.Data.SqlDbType.Int, 4);
          p1.Value = CurrentUserID;
          paramlist.Add(p1);
          return ESP.Finance.BusinessLogic.PrjUserRelationManager.GetList(term, paramlist);
        
      }
    }
}
