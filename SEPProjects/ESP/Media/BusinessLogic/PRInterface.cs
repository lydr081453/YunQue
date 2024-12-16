using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Media.Access;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;

namespace MediaLib.Service
{
  public  class PRInterface
    {
      [AjaxPro.AjaxMethod]
      public static bool IsProjectCodeExist(int projectid)
      {
          CacheService service = new CacheService(ESP.Media.Access.Utilities.ConfigManager.CacheServicePath);
          MediaLib.Service.T_Cache cache = service.GetCacheModel(0, projectid, "", 0);
          return !(cache == null);
      }

    }
}
